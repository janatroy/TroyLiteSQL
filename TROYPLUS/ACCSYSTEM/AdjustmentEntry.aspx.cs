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

public partial class AdjustmentEntry : System.Web.UI.Page
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

            if (!Page.IsPostBack)
            {
                txtDateEntered.Text = DateTime.Now.ToString("dd/MM/yyyy");

                if (Session["Area"] != null)
                    ddArea.SelectedValue = Session["Area"].ToString();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void lnkBtncancel_Click(object sender, EventArgs e)
    {

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

        int amount = 0;
        string reason = "";
        string dateEntered = "";
        int slNo = 0;
        string code = "";
        string area = "";

        try
        {

            if (ddArea.SelectedValue != "0")
            {
                area = ddArea.SelectedValue;
                Session["Area"] = ddArea.SelectedValue;
            }

            if (txtCustCode.Text != "")
                code = txtCustCode.Text;

            if (ddReason.SelectedValue != "0")
                reason = ddReason.SelectedValue;

            if (txtAmount.Text != "")
                amount = int.Parse(txtAmount.Text);

            if (txtDateEntered.Text != "")
                dateEntered = txtDateEntered.Text;

            int balance = BalanceAmount(area, code);

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

                        // Execute the commands.
                        command.CommandText = string.Format("INSERT INTO Adjustment VALUES('{0}',{1},{2},'{3}','{4}')", area.Replace("'", "''"), code, amount, reason, dateEntered);
                        command.ExecuteNonQuery();
                        balance = balance = balance + amount;
                        command.CommandText = string.Format("UPDATE CustomerMaster SET Balance = {0} WHERE area = '{1}' and code = {2}", balance, area.Replace("'", "''"), code);
                        command.ExecuteNonQuery();

                        // Commit the transaction.
                        transaction.Commit();
                        errorDisplay.AddItem("Adjustments added successfully", DisplayIcons.GreenTick, false);
                        ResetFormControlValues(this);

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            // Attempt to roll back the transaction.
                            transaction.Rollback();
                            errorDisplay.AddItem("Exception while Adding Adjustment : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                        }
                        catch (Exception ep)
                        {
                            // Do nothing here; transaction is not active.
                            errorDisplay.AddItem("Exception while Rollback Adjustment : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
                        }
                    }
                    // The connection is automatically closed when the
                    // code exits the using block.
                }

                if (Session["Area"] != null)
                    ddArea.SelectedValue = Session["Area"].ToString();

            }
            else
            {
                errorDisplay.AddItem("Customer Code and Area dosent match. Please check the details again.", DisplayIcons.Error, true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
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

            string query = "Select balance FROM CustomerMaster WHERE area = '" + area.Replace("'", "''") + "' and code = " + code;

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data.Tables[0].Rows.Count > 0)
                return int.Parse(data.Tables[0].Rows[0][0].ToString());
            else
                return -1;
            //reader = cmd.ExecuteReader();

            //datagrid.DataSource = reader;
            //datagrid.DataBind();
        }
        finally
        {
            //if (reader != null) 
            //    reader.Close();

            if (conn != null)
                conn.Close();
        }
    }
}
