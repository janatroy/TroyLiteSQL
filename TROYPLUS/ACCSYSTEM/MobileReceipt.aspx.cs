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
using System.Text;
using SMSLibrary;

public partial class MobileReceipt : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                tblBank.Attributes.Add("class", "hidden");
                myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

                string connStr = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                CheckSMSRequired();

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                if (Session["SMSREQUIRED"] != null)
                {
                    if (Session["SMSREQUIRED"].ToString() == "NO")
                        hdSMSRequired.Value = "NO";
                    else
                        hdSMSRequired.Value = "YES";
                }
                else
                {
                    hdSMSRequired.Value = "NO";
                }
                txtTransDate.Text = DateTime.Now.ToShortDateString();
                txtRefNo.Focus();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void CheckSMSRequired()
    {
        DataSet appSettings;
        string smsRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }

            }
        }

    }

    protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string debtorID = ComboBox2.SelectedValue;
            BusinessLogic objBus = new BusinessLogic();

            string Mobile = objBus.GetLedgerMobileForId(Request.Cookies["Company"].Value, int.Parse(debtorID));

            txtMobile.Text = Mobile;
            txtAmount.Focus();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void InsertButton_Click(object sender, EventArgs e)
    {
        try
        {
            string conn = string.Empty;

            if (Request.Cookies["Company"] != null)
            {
                conn = Request.Cookies["Company"].Value;
            }

            int CreditorID = int.Parse(ComboBox2.SelectedValue);

            string RefNo = txtRefNo.Text;


            DateTime TransDate = DateTime.Parse(txtTransDate.Text);

            //ViewState.Add("TransDate", DateTime.Parse(txtTransDate.Text).ToString("dd/MM/yyyy"));

            int DebitorID = 0;
            string Paymode = string.Empty;

            if (chkPayTo.SelectedValue == "Cash")
            {
                DebitorID = 1;
                Paymode = "Cash";
            }
            else if (chkPayTo.SelectedValue == "Cheque")
            {
                DebitorID = int.Parse(ddBanks.SelectedValue);
                Paymode = "Cheque";
                rvChequeNo.Enabled = true;
                cvBank.Enabled = true;
                PanelBank.Visible = true;
                Page.Validate();

                if (!Page.IsValid)
                {
                    return;
                }
            }

            Double Amount = Double.Parse(txtAmount.Text);

            string Narration = txtNarration.Text;

            string ChequeNo = txtChequeNo.Text;

            string VoucherType = "Receipt";

            if (hdSMSRequired.Value == "YES")
            {
                hdMobile.Value = txtMobile.Text;
                hdText.Value = "Thank you for Payment of Rs." + txtAmount.Text;
            }

            BusinessLogic objBus = new BusinessLogic(conn);
            int transNo = 0;
            objBus.InsertReceipt(out transNo, conn, RefNo, TransDate, DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, Paymode);

            if (hdSMS.Value == "YES")
            {
                string connStr = objBus.CreateConnectionString(Request.Cookies["Company"].Value);

                UtilitySMS utilSMS = new UtilitySMS(connStr);
                string UserID = string.Empty;
                utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), hdMobile.Value, hdText.Value, true, UserID);
            }

            txtAmount.Text = string.Empty;
            txtRefNo.Text = string.Empty;
            ComboBox2.SelectedIndex = 0;
            txtTransDate.Text = DateTime.Now.ToShortDateString();
            txtMobile.Text = "";
            txtChequeNo.Text = "";
            txtNarration.Text = "";
            txtRefNo.Focus();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Receipt details Saved Successfully. Trans. No. : " + transNo + "');", true);

        //}
        //catch (Exception ex)
        //{
        //    if (ex.InnerException != null)
        //    {
        //        StringBuilder script = new StringBuilder();
        //        script.Append("alert('You are not allowed to enter the payment with this date. Please contact Supervisor.');");

        //        if (ex.InnerException.Message.IndexOf("Invalid Date") > -1)
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
        //        else
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + ex.InnerException.Message + "');", true);

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + ex.Message.ToString() + "');", true);
        //    }
        //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void chkPayTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkPayTo.SelectedItem.Text == "Cheque")
            {
                PanelBank.Visible = true;
            }
            else
            {
                PanelBank.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("MobileDefault.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
