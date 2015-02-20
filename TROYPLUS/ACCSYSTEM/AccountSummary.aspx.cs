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
using System.Xml.Linq;
using System.IO;
using SMSLibrary;

public partial class AccountSummary : System.Web.UI.Page
{

    Double SumCashPurchase = 0.0d;
    Double SumChequePurchase = 0.0d;
    Double SumCreditPurchase = 0.0d;
    Double SumCashSales = 0.0d;
    Double SumChequeSales = 0.0d;
    Double SumCreditSales = 0.0d;
    Double SumCashPaid = 0.0d;
    Double SumChequePaid = 0.0d;
    Double SumCashRec = 0.0d;
    Double SumChequeRec = 0.0d;
    Double grossTotal = 0.0d;
    Double SumVat = 0.0d;

    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
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
                                lblBillDate.Text = DateTime.Now.ToShortDateString();
                            }
                        }
                    }
                }

                System.DateTime dt = System.DateTime.Now.Date;
                txtStartDate.Text = string.Format("{0:dd/MM/yyyy}", dt);

                System.DateTime dtt = System.DateTime.Now.Date;

                txtEndDate.Text = string.Format("{0:dd/MM/yyyy}", dtt);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void Reset()
    {
        lblsumCashPaid.Text = string.Empty;
        lblsumCashRec.Text = string.Empty;
        lblsumChequePaid.Text = string.Empty;
        lblsumChequeRec.Text = string.Empty;
        lblsumCashSales.Text = string.Empty;
        lblsumChequeSales.Text = string.Empty;
        lblsumCreditSales.Text = string.Empty;
        lblsumCreditPurchase.Text = string.Empty;
        lblsumCashPurchase.Text = string.Empty;
        lblsumChequePurchase.Text = string.Empty;

        lblGrandCashPaid.Text = string.Empty;
        lblGrandCashRec.Text = string.Empty;
        lblGrandChequePaid.Text = string.Empty;
        lblGrandChequeRecPaid.Text = string.Empty;
        lblGrandCashSales.Text = string.Empty;
        lblGrandChequeSales.Text = string.Empty;
        lblGrandCreditSales.Text = string.Empty;
        lblGrandCreditPurchase.Text = string.Empty;
        lblGrandCashPurchase.Text = string.Empty;
        lblGrandCheqPurchase.Text = string.Empty;
        sumVatTotal.Text = string.Empty;
        lblVatTotal.Text = string.Empty;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            Reset();
            divPrint.Visible = true;
            DateTime startDate, endDate;
            string salesreturn = string.Empty;
            string purchasereturn = string.Empty;
            if (chkPurchase.Checked)
                purchasereturn = "Yes";
            else
                purchasereturn = "No";

            if (chkSales.Checked)
                salesreturn = "Yes";
            else
                salesreturn = "No";

            //string sDataSource = sDatasource;//ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            GenerateCashPurchase(sDataSource, startDate, endDate, salesreturn);
            GenerateChequePurchase(sDataSource, startDate, endDate, salesreturn);
            GenarateCreditPurchase(sDataSource, startDate, endDate, salesreturn);
            GenerateCashSales(sDataSource, startDate, endDate, purchasereturn);
            GenerateChequeSales(sDataSource, startDate, endDate, purchasereturn);
            GenarateCreditSales(sDataSource, startDate, endDate, purchasereturn);

            GenerateCashPaid(sDataSource, startDate, endDate, salesreturn);
            GenerateChequePaid(sDataSource, startDate, endDate, salesreturn);

            GenerateCashReceived(sDataSource, startDate, endDate, purchasereturn);
            GenerateChequeReceived(sDataSource, startDate, endDate, purchasereturn);

            GenerateItemwiseSales(sDataSource, startDate, endDate, purchasereturn);
            GenerateItemwisePurchase(sDataSource, startDate, endDate, salesreturn);

            GenerateGrossProfit(sDataSource, startDate, endDate, salesreturn, purchasereturn);
            GenerateVat(sDataSource, startDate, endDate, salesreturn);
            lblsumCashPaid.Text = lblGrandCashPaid.Text;
            lblsumCashRec.Text = lblGrandCashRec.Text;
            lblsumChequePaid.Text = lblGrandChequePaid.Text;
            lblsumChequeRec.Text = lblGrandChequeRecPaid.Text;
            lblsumCashSales.Text = lblGrandCashSales.Text;
            lblsumChequeSales.Text = lblGrandChequeSales.Text;
            lblsumCreditSales.Text = lblGrandCreditSales.Text;
            lblsumCreditPurchase.Text = lblGrandCreditPurchase.Text;
            lblsumCashPurchase.Text = lblGrandCashPurchase.Text;
            lblsumChequePurchase.Text = lblGrandCheqPurchase.Text;
            sumVatTotal.Text = lblVatTotal.Text;

            UtilitySMS utilSMS = new UtilitySMS();
            string UserID = "Report";
            string smsTEXT = "Business Transaction Details between " + startDate.ToShortDateString() + " and " + endDate.ToShortDateString() + " ";
            string sCustomerContact = "";
            string ownerMobile = "";

            if (lblsumCashPaid.Text != "")
                smsTEXT = smsTEXT + "Total Cash Paid :" + lblsumCashPaid.Text + " ";

            if (lblsumCashRec.Text != "")
                smsTEXT = smsTEXT + "Total Cash Received :" + lblsumCashRec.Text + " ";
            if (lblsumChequePaid.Text != "")
                smsTEXT = smsTEXT + "Total Cheque Paid :" + lblsumChequePaid.Text + " ";
            if (lblsumChequeRec.Text != "")
                smsTEXT = smsTEXT + "Total Cash Received :" + lblsumChequeRec.Text + " ";
            if (lblsumCashSales.Text != "")
                smsTEXT = smsTEXT + "Total Cash Sales :" + lblsumCashSales.Text + " ";
            if (lblsumChequeSales.Text != "")
                smsTEXT = smsTEXT + "Total Cheque Sales :" + lblsumChequeSales.Text + " ";
            if (lblsumCreditSales.Text != "")
                smsTEXT = smsTEXT + "Total Credit Sales :" + lblsumCreditSales.Text + " ";
            if (lblsumCreditPurchase.Text != "")
                smsTEXT = smsTEXT + "Total Credit Purchase :" + lblsumCreditPurchase.Text + " ";
            if (lblsumCashPurchase.Text != "")
                smsTEXT = smsTEXT + "Total Cash Purchase :" + lblsumCashPurchase.Text + " ";
            if (lblsumChequePurchase.Text != "")
                smsTEXT = smsTEXT + "Total Cheque Purchase :" + lblsumChequePurchase.Text + " ";

            if (Session["AppSettings"] != null)
            {
                DataSet appSettings = (DataSet)Session["AppSettings"];

                for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
                {
                    if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "OWNERMOB")
                    {
                        ownerMobile = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    }
                }
            }

            if (Session["Provider"] != null)
            {
                if (ownerMobile != "" && ownerMobile.Length == 10)
                    utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), ownerMobile, smsTEXT, false, UserID);
            }
            else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    #region Report Calling Methods
    protected void GenerateCashPurchase(string sDataSource, DateTime startDate, DateTime endDate, string sType)
    {
        ReportsBL.ReportClass rptCash = new ReportsBL.ReportClass();

        DataSet ds = rptCash.generatePurchaseReport(1, sDataSource, startDate, endDate, sType);
        gvCashPurchase.DataSource = ds;
        gvCashPurchase.DataBind();
    }
    protected void GenerateChequePurchase(string sDataSource, DateTime startDate, DateTime endDate, string sType)
    {
        ReportsBL.ReportClass rptCash = new ReportsBL.ReportClass();

        DataSet ds = rptCash.generatePurchaseReport(2, sDataSource, startDate, endDate, sType);
        gvChequePurchase.DataSource = ds;
        gvChequePurchase.DataBind();
    }
    protected void GenarateCreditPurchase(string sDataSource, DateTime startDate, DateTime endDate, string sType)
    {
        ReportsBL.ReportClass rptCash = new ReportsBL.ReportClass();

        DataSet ds = rptCash.generatePurchaseReport(3, sDataSource, startDate, endDate, sType);
        gvCreditPurchase.DataSource = ds;
        gvCreditPurchase.DataBind();
    }
    protected void GenerateCashSales(string sDataSource, DateTime startDate, DateTime endDate, string sType)
    {
        ReportsBL.ReportClass rptCash = new ReportsBL.ReportClass();

        DataSet ds = rptCash.generateSalesReport(1, sDataSource, startDate, endDate, sType);
        gvCashSales.DataSource = ds;
        gvCashSales.DataBind();
    }
    protected void GenerateChequeSales(string sDataSource, DateTime startDate, DateTime endDate, string sType)
    {
        ReportsBL.ReportClass rptCash = new ReportsBL.ReportClass();

        DataSet ds = rptCash.generateSalesReport(2, sDataSource, startDate, endDate, sType);
        gvChequeSales.DataSource = ds;
        gvChequeSales.DataBind();
    }
    protected void GenarateCreditSales(string sDataSource, DateTime startDate, DateTime endDate, string sType)
    {
        ReportsBL.ReportClass rptCash = new ReportsBL.ReportClass();

        DataSet ds = rptCash.generateSalesReport(3, sDataSource, startDate, endDate, sType);
        gvCreditSales.DataSource = ds;
        gvCreditSales.DataBind();
    }

    protected void GenerateCashPaid(string sDataSource, DateTime startDate, DateTime endDate, string sType)
    {
        ReportsBL.ReportClass rptCash = new ReportsBL.ReportClass();

        DataSet ds = rptCash.generateCashPaid(sDataSource, startDate, endDate, sType);
        gvCashPaid.DataSource = ds;
        gvCashPaid.DataBind();
    }
    protected void GenerateChequePaid(string sDataSource, DateTime startDate, DateTime endDate, string sType)
    {
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        DataSet ds = rpt.generateChequePaid(sDataSource, startDate, endDate, sType);
        gvChequePaid.DataSource = ds;
        gvChequePaid.DataBind();
    }
    protected void GenerateCashReceived(string sDataSource, DateTime startDate, DateTime endDate, string pType)
    {
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        DataSet ds = rpt.generateCashReceived(sDataSource, startDate, endDate, pType);
        gvCashReceived.DataSource = ds;
        gvCashReceived.DataBind();
    }
    protected void GenerateChequeReceived(string sDataSource, DateTime startDate, DateTime endDate, string pType)
    {
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        DataSet ds = rpt.generateChequeReceived(sDataSource, startDate, endDate, pType);
        gvChequeReceived.DataSource = ds;
        gvChequeReceived.DataBind();
    }
    protected void GenerateItemwiseSales(string sDataSource, DateTime startDate, DateTime endDate, string pType)
    {
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        DataSet ds = rpt.itemwiseSales(sDataSource, startDate, endDate, pType);
        gvSales.DataSource = ds;
        gvSales.DataBind();
    }
    protected void GenerateItemwisePurchase(string sDataSource, DateTime startDate, DateTime endDate, string sType)
    {
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        DataSet ds = rpt.itemwisePurchase(sDataSource, startDate, endDate, sType);
        gvPurchase.DataSource = ds;
        gvPurchase.DataBind();
    }
    protected void GenerateGrossProfit(string sDataSource, DateTime startDate, DateTime endDate, string sType, string pType)
    {
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        DataSet ds = null; // rpt.GrossProfit(sDataSource, startDate, endDate, sType, pType);
        gvGross.DataSource = ds;
        gvGross.DataBind();
    }
    protected void GenerateVat(string sDataSource, DateTime startDate, DateTime endDate, string sType)
    {
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        DataSet ds = null; // rpt.generateVatReport(sDataSource, startDate, endDate, sType);
        grdVat.DataSource = ds;
        grdVat.DataBind();
    }


    #endregion

    #region Row bund Events
    protected void gvCashPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            // sumDbl = 0;
            //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
                Label lblPurchaseID = e.Row.FindControl("lblPurchaseID") as Label;
                int purchaseID = Convert.ToInt32(lblPurchaseID.Text);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();
                DataSet ds = rptProduct.getProducts(purchaseID, sDataSource);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                SumCashPurchase = SumCashPurchase + Convert.ToDouble(lblTotalAmt.Text);
                lblGrandCashPurchase.Text = "Rs. " + SumCashPurchase.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvChequePurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
                Label lblPurchaseID = e.Row.FindControl("lblPurchaseID") as Label;
                int purchaseID = Convert.ToInt32(lblPurchaseID.Text);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();
                DataSet ds = rptProduct.getProducts(purchaseID, sDataSource);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                SumChequePurchase = SumChequePurchase + Convert.ToDouble(lblTotalAmt.Text);
                lblGrandCheqPurchase.Text = "Rs. " + SumChequePurchase.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvCreditPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
                Label lblPurchaseID = e.Row.FindControl("lblPurchaseID") as Label;
                int purchaseID = Convert.ToInt32(lblPurchaseID.Text);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();
                DataSet ds = rptProduct.getProducts(purchaseID, sDataSource);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                SumCreditPurchase = SumCreditPurchase + Convert.ToDouble(lblTotalAmt.Text);
                lblGrandCreditPurchase.Text = "Rs. " + SumCreditPurchase.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvCashSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
                Label lblPurchaseID = e.Row.FindControl("lblBillno") as Label;
                int billno = Convert.ToInt32(lblPurchaseID.Text);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();
                DataSet ds = rptProduct.getProductsSales(billno, sDataSource);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                SumCashSales = SumCashSales + Convert.ToDouble(lblTotalAmt.Text);
                lblGrandCashSales.Text = "Rs. " + SumCashSales.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvChequeSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
                Label lblPurchaseID = e.Row.FindControl("lblBillno") as Label;
                int billno = Convert.ToInt32(lblPurchaseID.Text);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();
                DataSet ds = rptProduct.getProductsSales(billno, sDataSource);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                SumChequeSales = SumChequeSales + Convert.ToDouble(lblTotalAmt.Text);
                lblGrandChequeSales.Text = "Rs. " + SumChequeSales.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvCreditSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
                Label lblPurchaseID = e.Row.FindControl("lblBillno") as Label;
                int billno = Convert.ToInt32(lblPurchaseID.Text);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();
                DataSet ds = rptProduct.getProductsSales(billno, sDataSource);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                SumCreditSales = SumCreditSales + Convert.ToDouble(lblTotalAmt.Text);
                lblGrandCreditSales.Text = "Rs. " + SumCreditSales.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvCashPaid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            // string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
                SumCashPaid = SumCashPaid + Convert.ToDouble(lblTotalAmt.Text);
                lblGrandCashPaid.Text = "Rs. " + SumCashPaid.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdVat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            // string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblTotalVat = e.Row.FindControl("lblDiffVat") as Label;
                SumVat = SumVat + Convert.ToDouble(lblTotalVat.Text);
                lblVatTotal.Text = SumVat.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvChequePaid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
                SumChequePaid = SumChequePaid + Convert.ToDouble(lblTotalAmt.Text);
                lblGrandChequePaid.Text = "Rs. " + SumChequePaid.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvCashReceived_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
                SumCashRec = SumCashRec + Convert.ToDouble(lblTotalAmt.Text);
                lblGrandCashRec.Text = "Rs. " + SumCashRec.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvChequeReceived_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource = ConfigurationManager.AppSettings["DataSource"].ToString(); // Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblTotalAmt = e.Row.FindControl("lblTotal") as Label;
                SumChequeRec = SumChequeRec + Convert.ToDouble(lblTotalAmt.Text);
                lblGrandChequeRecPaid.Text = "Rs. " + SumChequeRec.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Double sumValue = 0.0;
            Double sumRateQty = 0.0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblQty = e.Row.FindControl("lblQty") as Label;
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblDisc = e.Row.FindControl("lblDisc") as Label;
                Label lblVat = e.Row.FindControl("lblVat") as Label;
                Label lblValue = e.Row.FindControl("lblValue") as Label;
                sumRateQty = Convert.ToDouble(lblRate.Text) * Convert.ToDouble(lblQty.Text);
                sumValue = sumRateQty - (sumRateQty * (Convert.ToDouble(lblDisc.Text) / 100)) + (sumRateQty * (Convert.ToDouble(lblVat.Text) / 100));
                lblValue.Text = Convert.ToString(sumValue);
                lblValue.Text = String.Format("{0:0.00}", lblValue.Text);

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvGross_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Double sumValue = 0.0;
            Double sumRateQty = 0.0;
            Double grossSales = 0.0;
            Double grossPurchase = 0.0;
            Double grossProfit = 0.0;
            Double qty = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblBuy = e.Row.FindControl("lblBuyValue") as Label;
                Label lblRate = e.Row.FindControl("lblBuyRate") as Label;
                Label lblDisc = e.Row.FindControl("lblDisc") as Label;
                Label lblVat = e.Row.FindControl("lblVat") as Label;
                Label lblQty = e.Row.FindControl("lblQty") as Label;
                Label lblGrossProfit = e.Row.FindControl("lblGrossProfit") as Label;
                qty = Convert.ToDouble(lblQty.Text);
                sumRateQty = Convert.ToDouble(lblRate.Text) * qty;
                sumValue = sumRateQty - (sumRateQty * (Convert.ToDouble(lblDisc.Text) / 100)) + (sumRateQty * (Convert.ToDouble(lblVat.Text) / 100));
                lblBuy.Text = Convert.ToString(sumValue);
                lblBuy.Text = String.Format("{0:0.00}", lblBuy.Text);
                grossPurchase = sumValue;

                sumValue = 0.0;
                sumRateQty = 0.0;

                Label lblSoldValue = e.Row.FindControl("lblSoldValue") as Label;
                Label lblSoldRate = e.Row.FindControl("lblSoldRate") as Label;
                Label lblSoldDisc = e.Row.FindControl("lblSoldDisc") as Label;
                Label lblSoldVat = e.Row.FindControl("lblSoldVat") as Label;

                sumRateQty = Convert.ToDouble(lblSoldRate.Text) * qty;
                sumValue = sumRateQty - (sumRateQty * (Convert.ToDouble(lblSoldDisc.Text) / 100)) + (sumRateQty * (Convert.ToDouble(lblSoldVat.Text) / 100));
                lblSoldValue.Text = Convert.ToString(sumValue);
                lblSoldValue.Text = String.Format("{0:0.00}", lblSoldValue.Text);
                grossSales = sumValue;
                grossProfit = grossSales - grossPurchase;

                grossTotal = grossTotal + grossProfit;
                lblGrossProfit.Text = Convert.ToString(grossProfit);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossTotal = e.Row.FindControl("lblGrossTotal") as Label;
                lblGrossTotal.Text = Convert.ToString(grossTotal);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    #endregion



}
