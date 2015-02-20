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

public partial class PrintSalesReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double SumCashSales = 0.0d;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            printPreview();
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

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void printPreview()
    {

        DataSet ds = new DataSet();
        ReportsBL.ReportClass rptSalesReport;
        rptSalesReport = new ReportsBL.ReportClass();
        //string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");


        if (Session["startDate"] != null)
        {
            lblStartDate.Text = Session["startDate"].ToString();
        }
        if (Session["endDate"] != null)
        {
            lblEndDate.Text = Session["endDate"].ToString();
        }

        lblBillDate.Text = DateTime.Now.ToShortDateString();

        ds = rptSalesReport.generateSalesReport(sDataSource, Convert.ToDateTime(lblStartDate.Text), Convert.ToDateTime(lblEndDate.Text));
        gvSales.DataSource = ds;
        gvSales.DataBind();

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
                sumRateQty = Convert.ToDouble(lblRate.Text) * Convert.ToInt32(lblQty.Text);
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("SalesReport.aspx");

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }



    protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource =  Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblPurchaseID = e.Row.FindControl("lblBillno") as Label;
                Label lblPaymode = e.Row.FindControl("lblPaymode") as Label;
                if (lblPaymode.Text == "1")
                {
                    lblPaymode.Text = "Cash";
                }
                else if (lblPaymode.Text == "2")
                {
                    lblPaymode.Text = "Bank";
                }
                else
                {
                    lblPaymode.Text = "Credit";
                }
                int billno = Convert.ToInt32(lblPurchaseID.Text);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();
                DataSet ds = rptProduct.getProductsSales(billno, sDataSource);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                //SumCashSales = SumCashSales + Convert.ToDouble(lblTotalAmt.Text);
                //lblGrandCashSales.Text = "Rs. " + SumCashSales.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
