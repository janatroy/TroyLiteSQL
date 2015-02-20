using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using DataAccessLayer;
using SMSLibrary;
using System.Text;

public partial class BulkSMS : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!Page.IsPostBack)
            {
                loadCustomer();
                ChangeRdo(this, null);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void BtnSendSMS_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            UtilitySMS utilSMS = new UtilitySMS();
            //int LedgerID = int.Parse(cmbCustomer.SelectedValue);
            //double balance = double.Parse(txtOpenBal.Text);

            //string crORDR = ddCRDR.SelectedValue;
            string mobileNos = string.Empty;
            string smsText = string.Empty;

            if (Session["Provider"] == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                return;
            }

            if (rdoSMSType.SelectedValue == "NORM")
            {
                DataSet ds = new DataSet();

                if (cmbCustomer.SelectedValue == "ALLCUST")
                {
                    //All Customers
                    ds = bl.GetGroupCreditDebitData(1, "Customer", sDataSource);
                }
                else if (cmbCustomer.SelectedValue == "ALLDEL")
                {
                    //All Dealers
                    ds = bl.GetGroupCreditDebitData(1, "Dealer", sDataSource);
                }
                else if (cmbCustomer.SelectedValue == "ALLDELSUPP")
                {
                    //both Dealers and customers
                    ds = bl.GetGroupCreditDebitData(1, "CustomerDealer", sDataSource);
                }
                else if (cmbCustomer.SelectedValue == "ALLSDC")
                {
                    //both Dealers and customers
                    ds = bl.GetGroupCreditDebitData(2, "CustomerDealerSupplier", sDataSource);
                }
                else
                {
                    ds = bl.GetGroupCreditDebitData(int.Parse(cmbCustomer.SelectedValue), "Ledger", sDataSource);
                }

                if (ds != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((dr["Mobile"] != null) && (dr["Mobile"].ToString().Length == 10))
                        {
                            mobileNos = mobileNos + dr["Mobile"].ToString() + ",";

                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Customer Information not found.');", true);
                }

                smsText = txtMessage.Text;

                string UserID = Page.User.Identity.Name;

                if (Session["Provider"] != null)
                {
                    utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), mobileNos, smsText, false, UserID);
                }

            }
            else
            {
                string strOper = ddOper.SelectedValue;
                double balance = double.Parse(txtOpenBal.Text);

                DataSet ds = new DataSet();

                ds = bl.GetCustomerDealerMobileNos(1, sDataSource);


                if (ds != null)
                {
                    int i = 0;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        i++;
                        double custDebit = 0.0;
                        double custCredit = 0.0;
                        double creditDiff = 0.0;

                        if (dr["Debit"] != null)
                        {
                            custDebit = double.Parse(dr["Debit"].ToString());
                        }
                        if (dr["Credit"] != null)
                        {
                            custCredit = double.Parse(dr["Credit"].ToString());
                        }

                        creditDiff = custCredit - custDebit;

                        if (strOper == ">")
                        {
                            if (creditDiff > balance)
                            {
                                if ((dr["Mobile"] != null) && (dr["Mobile"].ToString().Length == 10))
                                    mobileNos = mobileNos + dr["Mobile"].ToString() + ",";
                            }
                        }
                        if (strOper == ">=")
                        {
                            if (creditDiff >= balance)
                            {
                                if ((dr["Mobile"] != null) && (dr["Mobile"].ToString().Length == 10))
                                    mobileNos = mobileNos + dr["Mobile"].ToString() + ",";
                            }
                        }
                        if (strOper == "=")
                        {
                            if (creditDiff == balance)
                            {
                                if ((dr["Mobile"] != null) && (dr["Mobile"].ToString().Length == 10))
                                    mobileNos = mobileNos + dr["Mobile"].ToString() + ",";
                            }
                        }

                        string UserID = Page.User.Identity.Name;

                        smsText = "Your current outstanding Debt Amount is " + GetCurrencyType() + creditDiff.ToString() + " . Please arrange to pay at the earliest. Thank You.";

                        if (Session["Provider"] != null)
                        {
                            utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), mobileNos, smsText, false, UserID);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Customer Information not found.');", true);
                }


            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('SMS Sent Successfully.');", true);
            txtMessage.Text = "";
            cmbCustomer.SelectedIndex = 0;
            ddSMSTemplate.SelectedIndex = 0;
            txtOpenBal.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private string GetCurrencyType()
    {
        if (Session["CurrencyType"] != null)
        {
            if (Session["CurrencyType"].ToString() == "INR")
            {
                return "Rs.";
            }
            else if (Session["CurrencyType"].ToString() == "GBP")
            {
                return "£";
            }
            else
            {
                return string.Empty;
            }
        }
        else
            return string.Empty;

    }

    protected void ChangeRdo(object sender, EventArgs e)
    {
        loadCustomer();

        if (rdoSMSType.SelectedValue == "DEBT")
        {
            RowSMSTemplate.Visible = false;
            RowOustanding.Visible = true;
            RowText.Visible = false;
            txtOpenBal.Text = "";
        }
        else
        {
            RowSMSTemplate.Visible = true;
            RowOustanding.Visible = false;
            RowText.Visible = true;
            txtMessage.Text = "";
            ddSMSTemplate.SelectedValue = "0";
        }

    }

    protected void ddSMSTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();
            ds = bl.GetSMSText(sDataSource, ddSMSTemplate.SelectedValue);

            if (ds != null)
            {
                txtMessage.Text = ds.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                txtMessage.Text = "";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadCustomer()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListCreditorDebitorForSMS(sDataSource);

        cmbCustomer.Items.Clear();

        if (rdoSMSType.SelectedValue == "DEBT")
        {
            ListItem li1 = new ListItem("All Customers", "ALLCUST");
            ListItem li2 = new ListItem("All Dealers", "ALLDEL");
            ListItem li3 = new ListItem("All Dealers/Customers", "ALLDELSUPP");
            cmbCustomer.Items.Add(li1);
            cmbCustomer.Items.Add(li2);
            cmbCustomer.Items.Add(li3);
            cmbCustomer.DataBind();
        }
        else
        {
            ListItem li1 = new ListItem("All Customers", "ALLCUST");
            ListItem li2 = new ListItem("All Dealers", "ALLDEL");
            ListItem li3 = new ListItem("All Dealers/Customers", "ALLDELSUPP");
            ListItem li4 = new ListItem("All Supplier/Dealers/Customers", "ALLSDC");

            cmbCustomer.Items.Add(li1);
            cmbCustomer.Items.Add(li2);
            cmbCustomer.Items.Add(li3);
            cmbCustomer.Items.Add(li4);

            cmbCustomer.DataSource = ds;
            cmbCustomer.DataBind();
            cmbCustomer.DataTextField = "LedgerName";
            cmbCustomer.DataValueField = "LedgerID";
        }
    }

    protected void BtnGetNos_Click(object sender, EventArgs e)
    {

    }
}
