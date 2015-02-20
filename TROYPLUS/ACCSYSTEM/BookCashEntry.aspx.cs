using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using DataAccessLayer;

public partial class BookCashEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            srcArea.ConnectionString = connStr;
            srcReason.ConnectionString = connStr;
            srcBook.ConnectionString = connStr;

            if (!Page.IsPostBack)
            {
                txtDatePaid.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDiscount.Text = "0";

                if (Session["Area"] != null)
                    ddArea.SelectedValue = Session["Area"].ToString();

                if (Request.Cookies["BookID"] != null)
                {
                    if (ddBook.Items.FindByValue(Request.Cookies["BookID"].Value.ToString()) != null)
                        ddBook.Items.FindByValue(Request.Cookies["BookID"].Value.ToString()).Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void ResetFormControlValues(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.Controls.Count > 0)
            {
                ResetFormControlValues(c);
            }
            else
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((TextBox)c).Text = "";
                        break;
                    case "System.Web.UI.WebControls.RadioButtonList":
                        {
                            //((RadioButtonList)c).SelectedValue = "Y";
                            for (int j = 0; j < ((RadioButtonList)c).Items.Count; j++)
                            {
                                ((RadioButtonList)c).Items[j].Selected = false;

                            }
                            break;
                        }
                    case "System.Web.UI.WebControls.DropDownList":
                        ((DropDownList)c).SelectedValue = "0";
                        break;

                }
            }
        }
    }

    private void ExecuteNonQuery(string sql)
    {
        OleDbConnection conn = null;
        try
        {
            string connStr = string.Empty;

            if (Request.Cookies["Company"]  != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (conn != null) conn.Close();
        }
    }

    protected void lnkBtnSave_Click(object sender, EventArgs e)
    {


        string billNo = "";
        int amount = 0;
        int discount = 0;
        string reason = "";
        string datepaid = string.Empty;
        string dateEntered = "";
        Int64 slNo = 0;
        string code = "";
        string area = "";
        int BookID = 0;

        if (txtDiscount.Text != "0" && ddReason.SelectedValue == "0")
        {
            cvReason.Enabled = true;
            Page.Validate();
        }
        else if (txtDiscount.Text == "0")
        {
            cvReason.Enabled = false;
            Page.Validate();
        }

        if (!Page.IsValid)
        {
            foreach (IValidator validator in Page.Validators)
            {
                if (!validator.IsValid)
                {
                    errorDisplay.AddItem(validator.ErrorMessage, DisplayIcons.Error, false);
                }
            }
        }
        else
        {

            if (txtCustCode.Text != "")
                code = txtCustCode.Text;

            if (ddArea.SelectedValue != "0")
            {
                area = ddArea.SelectedValue;
                Session["Area"] = ddArea.SelectedValue;

            }
            if (txtBillNo.Text != "")
                billNo = txtBillNo.Text;

            if (ddReason.SelectedValue != "0")
                reason = ddReason.SelectedValue;
            else
                reason = " ";

            if (txtDiscount.Text != "")
                discount = int.Parse(txtDiscount.Text);

            if (txtAmount.Text != "")
                amount = int.Parse(txtAmount.Text);

            if (txtDatePaid.Text != "")
            {
                datepaid = txtDatePaid.Text;
                Page.Response.Cookies.Add(new HttpCookie("DatePaid", txtDatePaid.Text));
            }
            if (ddBook.SelectedValue != "0")
            {
                BookID = int.Parse(ddBook.SelectedValue);
                Page.Response.Cookies.Add(new HttpCookie("BookID", ddBook.SelectedValue));
            }

            dateEntered = DateTime.Now.ToString("dd/MM/yyyy");

            try
            {

                int balance = BalanceAmount(area.Replace("'", "''"), code);

                if (balance != -1)
                {
                    string connStr = string.Empty;

                    if (Request.Cookies["Company"]  != null)
                        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                    else
                        Response.Redirect("~/frm_Login.aspx");

                    using (OleDbConnection connection = new OleDbConnection(connStr))
                    {
                        OleDbCommand command = new OleDbCommand();
                        OleDbTransaction transaction = null;
                        OleDbDataAdapter adapter = null;

                        // Set the Connection to the new OleDbConnection.
                        command.Connection = connection;

                        // Open the connection and execute the transaction.
                        try
                        {
                            connection.Open();

                            // Start a local transaction with ReadCommitted isolation level.
                            transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                            // Assign transaction object for a pending local transaction.
                            command.Connection = connection;
                            command.Transaction = transaction;

                            command.CommandText = "Select MAX(slNo) FROM CashDetails";
                            DataSet data = new DataSet();
                            adapter = new OleDbDataAdapter(command);
                            adapter.Fill(data);

                            if (data != null)
                            {
                                if (data.Tables[0].Rows.Count > 0)
                                {
                                    if (data.Tables[0].Rows[0][0].ToString() != "")
                                        slNo = Int64.Parse(data.Tables[0].Rows[0][0].ToString());
                                    else
                                        slNo = 0;
                                }
                            }
                            else
                            {
                                throw new Exception("Unable to get Max SlNo");
                            }


                            //Check the cash entered for this book is not crossing the maximum cash entered for this book
                            command.CommandText = string.Format("Select SUM(amount)as amount from CashDetails Where BookID={0}", BookID);
                            string currTotalAmount = command.ExecuteScalar().ToString();

                            int CurrTotalAmount = 0;

                            if (currTotalAmount != "")
                                CurrTotalAmount = int.Parse(currTotalAmount);

                            command.CommandText = string.Format("Select Amount from tblBook Where BookID={0}", BookID);
                            int BookCash = (int)command.ExecuteScalar();

                            if (BookCash < (CurrTotalAmount + amount))
                            {
                                transaction.Rollback();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cash you entered is more than the Total Cash for this Book. Please try again! ')", true);
                                return;
                            }

                            // Execute the commands.
                            command.CommandText = string.Format("INSERT INTO CashDetails VALUES({0},{1},'{2}',{3},{4},'{5}','{6}','{7}','{8}',{9})", slNo + 1, code, area.Replace("'", "''"), amount, discount, reason, datepaid, dateEntered, billNo, BookID);
                            command.ExecuteNonQuery();
                            balance = balance - amount - discount;
                            command.CommandText = string.Format("UPDATE CustomerMaster SET Balance = {0} WHERE area = '{1}' and code = {2}", balance, area.Replace("'", "''"), code);
                            command.ExecuteNonQuery();

                            command.CommandText = string.Format("Select EndEntry from tblBook Where BookID={0}", BookID);
                            int EndEntry = (int)command.ExecuteScalar();

                            //if ((EndEntry.ToString() != billNo)|| (BookCash != (CurrTotalAmount + amount)))
                            //{

                            //}
                            if ((EndEntry.ToString() == billNo) || (BookCash == (CurrTotalAmount + amount)))
                            {
                                command.CommandText = string.Format("UPDATE tblBook SET BookStatus = 'Closed' WHERE BookID = {0}", BookID);
                                command.ExecuteNonQuery();
                                ddBook.Items.Remove(new ListItem(ddBook.SelectedItem.Text, ddBook.SelectedValue));

                                if (EndEntry.ToString() == billNo)
                                    errorDisplay.AddItem("You have entered the Maximum Bill No. for this Book. Book is closed now.", DisplayIcons.GreenTick, false);
                                else if (BookCash == (CurrTotalAmount + amount))
                                    errorDisplay.AddItem("You have entered the Maximum Amount for this Book. Book is closed now.", DisplayIcons.GreenTick, false);
                            }
                            else
                            {
                                command.CommandText = string.Format("UPDATE tblBook SET NextEntry = {0} WHERE BookID = {1}", int.Parse(billNo) + 1, BookID);
                                command.ExecuteNonQuery();
                            }
                            // Commit the transaction.
                            transaction.Commit();
                            errorDisplay.AddItem("Cash details added successfully", DisplayIcons.GreenTick, false);
                            ResetFormControlValues(this);

                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                // Attempt to roll back the transaction.
                                transaction.Rollback();
                                errorDisplay.AddItem("Exception while Adding Cash : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                            }
                            catch (Exception ext)
                            {
                                TroyLiteExceptionManager.HandleException(ex);
                            }
                        }
                        // The connection is automatically closed when the
                        // code exits the using block.
                    }

                    //slNo = GetNextSeqNo();

                    //string sqlInsert = string.Format("INSERT INTO CashDetails VALUES({0},{1},'{2}',{3},{4},'{5}','{6}','{7}','{8}')", slNo + 1, code, area.Replace("'","''"), amount, discount, reason, datepaid, dateEntered, billNo);

                    //ExecuteNonQuery(sqlInsert);

                    //balance = balance - amount - discount;

                    //string updateSql = string.Format("UPDATE CustomerMaster SET Balance = {0} WHERE area = '{1}' and code = {2}", balance, area.Replace("'", "''") , code);

                    //ExecuteNonQuery(updateSql);

                    //errDisp.AddItem("Cash details added successfully", DisplayIcons.GreenTick, true);

                }
                else
                {
                    errorDisplay.AddItem("Customer Code and Area dosent match, please check the details again", DisplayIcons.Error, true);
                }


                if (Session["Area"] != null)
                    ddArea.SelectedValue = Session["Area"].ToString();

                if (Page.Request.Cookies["DatePaid"] != null)
                    txtDatePaid.Text = Page.Request.Cookies["DatePaid"].Value;

                txtDiscount.Text = "0";

                if (Page.Request.Cookies["BookID"] != null)
                {
                    if (ddBook.Items.FindByValue(Request.Cookies["BookID"].Value.ToString()) != null)
                        ddBook.SelectedValue = Request.Cookies["BookID"].Value;
                }
                ddBook_SelectedIndexChanged(this, null);

            }
            catch (Exception ex)
            {
                TroyLiteExceptionManager.HandleException(ex);
            }
        }

    }

    private int GetNextSeqNo()
    {
        OleDbConnection conn = null;
        OleDbDataReader reader = null;
        OleDbDataAdapter da = null;
        DataSet data = new DataSet();

        try
        {
            string connStr = string.Empty;
            if (Request.Cookies["Company"]  != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");


            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select MAX(slNo) FROM CashDetails";

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data.Tables[0].Rows.Count > 0)
                return int.Parse(data.Tables[0].Rows[0][0].ToString());
            else
                return 0;

        }
        finally
        {
            if (conn != null)
                conn.Close();
        }
    }

    private int BalanceAmount(string area, string code)
    {
        OleDbConnection conn = null;
        OleDbDataReader reader = null;
        OleDbDataAdapter da = null;
        DataSet data = new DataSet();

        try
        {
            string connStr = string.Empty;
            if (Request.Cookies["Company"]  != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select balance FROM CustomerMaster WHERE area = '" + area + "' and code = " + code;

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data.Tables[0].Rows.Count > 0)
                return int.Parse(data.Tables[0].Rows[0][0].ToString());
            else
                return -1;

        }
        finally
        {

            if (conn != null)
                conn.Close();
        }
    }

    private DataSet GetCustDetails(string area, string code)
    {
        OleDbConnection conn = null;
        OleDbDataAdapter da = null;
        DataSet data = new DataSet();

        try
        {
            string connStr = string.Empty;
            if (Request.Cookies["Company"]  != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select name,balance FROM CustomerMaster WHERE area = '" + area + "' and code = " + code;

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data != null)
                return data;
            else
                return null;

        }
        finally
        {

            if (conn != null)
                conn.Close();
        }
    }

    protected void btnDetails_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            bool isValid = true;

            if (ddArea.SelectedValue == "0")
            {
                isValid = false;
            }
            if (txtCustCode.Text == "")
            {
                isValid = false;
            }

            if (isValid)
            {
                DataSet ds = new DataSet();
                ds = GetCustDetails(ddArea.SelectedValue.Replace("'", "''"), txtCustCode.Text);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCustName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    txtCustBalance.Text = ds.Tables[0].Rows[0]["balance"].ToString();
                    txtBillNo.Focus();
                }
                else
                {
                    txtCustName.Text = "";
                    txtCustBalance.Text = "";
                    txtCustCode.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Customer details not found. Please try again! ')", true);
                    return;
                }
            }
            else
            {
                txtCustName.Text = "";
                txtCustBalance.Text = "";
                txtCustCode.Focus();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please search by entering Customer Code and Area! ')", true);
                return;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void ddBook_SelectedIndexChanged(object sender, EventArgs e)
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"]  != null)
            connStr = Request.Cookies["Company"].Value;
        else
            Response.Redirect("~/frm_Login.aspx");

        if (ddBook.SelectedValue != "0")
        {
            try
            {
                BusinessLogic objBus = new BusinessLogic();
                DataSet ds = objBus.GetNextBillNo(connStr, int.Parse(ddBook.SelectedValue));

                if (ds != null)
                    txtBillNo.Text = ds.Tables[0].Rows[0]["NextEntry"].ToString();
                else
                    errorDisplay.AddItem("Error in Getting NextBill Entry.");


                DBManager manager = new DBManager(DataProvider.OleDb);
                manager.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
                manager.Open();

                object TotalCash = manager.ExecuteScalar(CommandType.Text, "Select Amount From tblBook Where BookID=" + ddBook.SelectedValue);

                object CashEntered = manager.ExecuteScalar(CommandType.Text, "Select SUM(amount)as amount from CashDetails Where BookID=" + ddBook.SelectedValue);

                int TotalKash = 0;
                int EntetedKash = 0;

                if (TotalCash != null)
                {
                    lblTotalAmount.Text = TotalCash.ToString();
                    TotalKash = int.Parse(TotalCash.ToString());
                }

                if (CashEntered != null)
                {
                    if (CashEntered.ToString() != "")
                    {
                        lblCashEnteted.Text = CashEntered.ToString();
                        EntetedKash = int.Parse(CashEntered.ToString());
                    }
                    else
                    {
                        lblCashEnteted.Text = "0";
                        EntetedKash = 0;
                    }
                }

                int cashRem = TotalKash - EntetedKash;
                lblCashRem.Text = cashRem.ToString();

                manager.Dispose();
            }
            catch (Exception ex)
            {
                TroyLiteExceptionManager.HandleException(ex);
            }
        }
    }
}
