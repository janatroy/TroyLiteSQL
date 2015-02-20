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

public partial class CashEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"]  != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");

        try
        {
            srcArea.ConnectionString = connStr;
            srcReason.ConnectionString = connStr;

            if (!Page.IsPostBack)
            {
                txtDatePaid.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDiscount.Text = "0";

                if (Session["Area"] != null)
                    ddArea.SelectedValue = Session["Area"].ToString();
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

        if (txtDiscount.Text != "0" && ddReason.SelectedValue == "0")
        {
            cvReason.Enabled = true;
            Page.Validate();
        }

        if (!Page.IsValid)
        {
            foreach (IValidator validator in Page.Validators)
            {
                if (!validator.IsValid)
                {
                    errorDisplay.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
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
                datepaid = txtDatePaid.Text;

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
                            // Execute the commands.
                            command.CommandText = string.Format("INSERT INTO CashDetails VALUES({0},{1},'{2}',{3},{4},'{5}','{6}','{7}','{8}')", slNo + 1, code, area.Replace("'", "''"), amount, discount, reason, datepaid, dateEntered, billNo);
                            command.ExecuteNonQuery();
                            balance = balance - amount - discount;
                            command.CommandText = string.Format("UPDATE CustomerMaster SET Balance = {0} WHERE area = '{1}' and code = {2}", balance, area.Replace("'", "''"), code);
                            command.ExecuteNonQuery();

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
                            catch (Exception ep)
                            {
                                // Do nothing here; transaction is not active.
                                errorDisplay.AddItem("Exception while Adding Cash : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
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

}
