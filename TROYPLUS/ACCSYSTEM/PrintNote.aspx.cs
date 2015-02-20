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
using System.IO;

public partial class PrintNote : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    private string currency = string.Empty;
    private string currencyType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                GetCurrencyType();

                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

                    if (companyInfo != null)
                    {
                        if (companyInfo.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in companyInfo.Tables[0].Rows)
                            {
                                lblTNGST.Text = Convert.ToString(dr["TINno"]);
                                lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                                lblPhone.Text = Convert.ToString(dr["Phone"]);
                                lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                                lblAddress.Text = Convert.ToString(dr["Address"]);
                                lblCity.Text = Convert.ToString(dr["city"]);
                                lblPincode.Text = Convert.ToString(dr["Pincode"]);
                                lblState.Text = Convert.ToString(dr["state"]);
                                lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                        }
                    }
                }
                printPreview();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void GetCurrencyType()
    {
        DataSet appSettings;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "CURRENCY")
                {
                    currency = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }

        if (currency == "INR")
        {
            currencyType = "Rs";
        }
        if (currency == "GBP")
        {
            currencyType = "£";
        }
    }

    protected void printPreview()
    {
        string sFilename = string.Empty;
        DataSet ds = new DataSet();
        int ID = 0;

        if (Request.QueryString["ID"] != null)
        {
            ID = int.Parse(Request.QueryString["ID"].ToString());
        }

        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetCDNoteForId(Request.Cookies["Company"].Value, ID);

        if (ds.Tables[0].Rows.Count == 1)
        {
            if (ds.Tables[0].Rows[0]["CDType"].ToString() == "Debit")
                lblNoteType.Text = "Debit Note";
            else
                lblNoteType.Text = "Credit Note";

            lblRefNo.Text = ": " + ds.Tables[0].Rows[0]["RefNo"].ToString();
            lblLedger.Text = ": " + ds.Tables[0].Rows[0]["LedgerName"].ToString();
            lblAmount.Text = ": " + currencyType + " " + ds.Tables[0].Rows[0]["Amount"].ToString();
            lblNoteDate.Text = ": " + DateTime.Parse(ds.Tables[0].Rows[0]["NoteDate"].ToString()).ToShortDateString();
            lblNote.Text = ": " + ds.Tables[0].Rows[0]["Note"].ToString();
        }

    }

}
