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


public partial class PrintJournal : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    private string currency = string.Empty;
    private string currencyType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet companyInfo = new DataSet();
            int ID = 0;
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
                            lblBillDate.Text = DateTime.Now.ToShortDateString();
                        }
                    }
                }
            }
            if (Request.QueryString["ID"] != null)
            {
                ID = int.Parse(Request.QueryString["ID"].ToString());

                //if (Session["JournalID"] != null)
                //{
                lblBillno.Text = ID.ToString();
                lblBillDate.Text = DateTime.Now.ToShortDateString();
                DataSet ds = bl.GetJournalForId(ID, Request.Cookies["Company"].Value);
                //GrdViewJournal.DataSource = ds;
                //GrdViewJournal.DataBind();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblRefNo.Text = ": " + ds.Tables[0].Rows[0]["RefNo"].ToString();
                    lblPayDate.Text = ": " + ds.Tables[0].Rows[0]["TransDate"].ToString();
                    lblDebtor.Text = ": " + ds.Tables[0].Rows[0]["Debi"].ToString();
                    lblCreditor.Text = ": " + ds.Tables[0].Rows[0]["Ledgername"].ToString();
                    lblAmount.Text = ": " + currencyType + " " + ds.Tables[0].Rows[0]["Amount"].ToString();
                    lblNarration.Text = ": " + ds.Tables[0].Rows[0]["Narration"].ToString();
                }
            }
            //}
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("JournalInfo.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
}
