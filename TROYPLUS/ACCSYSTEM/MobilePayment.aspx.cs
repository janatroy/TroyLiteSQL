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

public partial class MobilePayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                myRangeValidatorAdd.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                myRangeValidatorAdd.MaximumValue = System.DateTime.Now.ToShortDateString();

                string connStr = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/MobileLogin.aspx");

                tblBankAdd.Attributes.Add("class", "hidden");
                txtRefNoAdd.Focus();
                txtTransDateAdd.Text = DateTime.Now.ToString("dd/MM/yyyy");

                CheckSMSRequired();

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

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {

    }

    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setInsertParameters(e);

            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bll = new BusinessLogic();
            string recondate = DateTime.Now.ToShortDateString();

            ViewState.Add("TransDate", recondate);

            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to Insert Payment with this Date. Please contact Supervisor.');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            if (e.OutputParameters["NewTransNo"] != null)
            {
                if (e.OutputParameters["NewTransNo"].ToString() != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Payment Saved Successfully. Transaction No : " + e.OutputParameters["NewTransNo"].ToString() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkPayTo_DataBound(object sender, EventArgs e)
    {

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

            int DebitorID = int.Parse(ComboBox2Add.SelectedValue);

            string RefNo = txtRefNoAdd.Text;

            DateTime TransDate = DateTime.Parse(txtTransDateAdd.Text);

            //ViewState.Add("TransDate", DateTime.Parse(txtTransDate.Text).ToString("dd/MM/yyyy"));

            int CreditorID = 0;
            string Paymode = string.Empty;

            if (chkPayToAdd.SelectedValue == "Cash")
            {
                CreditorID = 1;
                Paymode = "Cash";
            }
            else if (chkPayToAdd.SelectedValue == "Cheque")
            {
                CreditorID = int.Parse(ddBanksAdd.SelectedValue);
                Paymode = "Cheque";
                rvChequeNoAdd.Enabled = true;
                cvBankAdd.Enabled = true;
                PanelBankAdd.Visible = true;
                Page.Validate();

                if (!Page.IsValid)
                {
                    return;
                }
            }

            Double Amount = Double.Parse(txtAmountAdd.Text);

            string Narration = txtNarrationAdd.Text;

            string ChequeNo = txtChequeNoAdd.Text;

            string VoucherType = "Payment";

            BusinessLogic objBus = new BusinessLogic(conn);
            int transNo = 0;
            objBus.InsertPayment(out transNo, conn, RefNo, TransDate, DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, Paymode, "0", Request.Cookies["LoggedUserName"].Value);

            txtAmountAdd.Text = string.Empty;
            txtRefNoAdd.Text = string.Empty;
            ComboBox2Add.SelectedIndex = 0;
            txtTransDateAdd.Text = DateTime.Now.ToShortDateString();
            txtChequeNoAdd.Text = "";
            txtNarrationAdd.Text = "";
            txtRefNoAdd.Focus();
            txtTransDateAdd.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Payment details Saved Successfully. Trans. No. : " + transNo + "');", true);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {

    }

    protected void chkPayToAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            if (chk.SelectedItem.Text == "Cheque")
            {
                PanelBankAdd.Visible = true;
            }
            else
            {
                PanelBankAdd.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkPayToAdd_DataBound(object sender, EventArgs e)
    {
        try
        {
            tblBankAdd.Attributes.Add("class", "hidden");
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
