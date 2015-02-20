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

public partial class ProfitAndLossReportaspx : System.Web.UI.Page
{
    Double grossTotal = 0.0d;
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateTime dtStart;
            int mnth = 0;
            double dy = 0;
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                if (DateTime.Now.Month < 4)
                    mnth = DateTime.Now.Month + 4 + (4 - DateTime.Now.Month);
                else
                    mnth = DateTime.Now.Month - 4;

                mnth = -mnth - 1;
                dtStart = DateTime.Now.AddMonths(mnth);
                dy = DateTime.Now.Day;
                dy = -(dy - 1);
                txtStartDate.Text = dtStart.AddDays(dy).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
                setProfitAndLoss();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void setProfitAndLoss()
    {
        double purchaseTot = 0.0;
        double salesTot = 0.0;
        double expensesTot = 0.0d;
        double closingstockTotal = 0.0d;


        DateTime startDate, endDate;
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        purchaseTot = rpt.GetPurchaseTotal(sDataSource, startDate, endDate);
        salesTot = rpt.GetSalesTotal(sDataSource, startDate, endDate);
        expensesTot = rpt.GetExpenseTotal(sDataSource, startDate, endDate);
        closingstockTotal = rpt.GetClosingStockTotal(sDataSource);

        lblPurchaseTotal.Text = purchaseTot.ToString("f2");
        lblSalesTotal.Text = salesTot.ToString("f2");
        lblExpensesTotal.Text = expensesTot.ToString("f2");
        //lblClosingStock.Text = closingstockTotal.ToString("f2");
        lblFromDate.Text = startDate.ToShortDateString();
        lblToDate.Text = endDate.ToShortDateString();
        GenerateGrossProfit(sDataSource, startDate, endDate);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate, endDate;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            setProfitAndLoss();
            //GenerateGrossProfit(sDataSource, startDate, endDate);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GenerateGrossProfit(string sDataSource, DateTime startDate, DateTime endDate)
    {
        double sumAmt = 0.0d;
        double netProfit = 0.0d;
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        DataSet ds = rpt.GrossProfitAndLoss(sDataSource, startDate, endDate);
        gvGross.DataSource = ds;
        gvGross.DataBind();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (Convert.ToString(dr["VoucherType"]) == "Sales")
                        sumAmt = sumAmt + (Convert.ToDouble(dr["SoldRate"]) - Convert.ToDouble(dr["BuyRate"]));
                    else if (Convert.ToString(dr["VoucherType"]) == "Sales Return")
                        sumAmt = sumAmt - (Convert.ToDouble(dr["SoldRate"]) - Convert.ToDouble(dr["BuyRate"]));
                    else if (Convert.ToString(dr["VoucherType"]) == "Purchase Return")
                    {
                        if (Convert.ToDouble(dr["SoldRate"]) > Convert.ToDouble(dr["BuyRate"]))
                        {
                            sumAmt = sumAmt + (Convert.ToDouble(dr["SoldRate"]) - Convert.ToDouble(dr["BuyRate"]));
                        }
                        else
                        {
                            sumAmt = sumAmt - (Convert.ToDouble(dr["SoldRate"]) - Convert.ToDouble(dr["BuyRate"]));
                        }
                    }

                }
            }
        }
        lblGPTotal.Text = sumAmt.ToString("f2");
        lblGP.Text = lblGPTotal.Text;
        netProfit = sumAmt - Convert.ToDouble(lblExpensesTotal.Text);
        if (netProfit > 0)
        {
            lblNeProfit.Text = netProfit.ToString("f2");
            lblP.Text = "Net Profit";
            lblP.Style.Add("Color", "Red");
            lblNeProfit.Style.Add("Color", "Red");
            lblP.Visible = true;
            lblNeProfit.Visible = true;
            lblL.Visible = false;
            lblNeLoss.Visible = false;

        }
        else
        {
            lblL.Text = "Net Loss";
            lblNeLoss.Text = Math.Abs(netProfit).ToString("f2");
            lblL.Style.Add("Color", "Red");
            lblNeLoss.Style.Add("Color", "Red");
            lblP.Visible = false;
            lblNeProfit.Visible = false;
            lblL.Visible = true;
            lblNeLoss.Visible = true;
        }
    }
    protected void gvGross_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Double grossProfit = 0.0;
            double sumvalue = 0.0;
            double soldValue = 0.0;
            double buyValue = 0.0;
            int qty = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label lblRate = e.Row.FindControl("lblBuyRate") as Label;


                Label lblQty = e.Row.FindControl("lblQty") as Label;
                Label lblGrossProfit = e.Row.FindControl("lblGrossProfit") as Label;
                Label lblSoldRate = e.Row.FindControl("lblSoldRate") as Label;
                Label lblBuyRate = e.Row.FindControl("lblBuyRate") as Label;
                Label lblVoucherType = e.Row.FindControl("lblVoucherType") as Label;
                //lblSoldRate.Text = String.Format("{0:f2}", lblSoldRate.Text);
                qty = Convert.ToInt32(lblQty.Text);
                if (lblSoldRate != null && lblSoldRate.Text != "")
                    soldValue = Convert.ToDouble(lblSoldRate.Text);
                if (lblBuyRate != null && lblBuyRate.Text != "")
                    buyValue = Convert.ToDouble(lblBuyRate.Text);

                grossProfit = soldValue - buyValue;

                if (lblVoucherType.Text == "Sales")
                    grossTotal = grossTotal + grossProfit;
                else if (lblVoucherType.Text == "Sales Return")
                    grossTotal = grossTotal - grossProfit;
                else if (lblVoucherType.Text == "Purchase Return")
                {
                    if (soldValue > buyValue)
                    {
                        grossTotal = grossTotal + grossProfit;
                    }
                    else
                    {
                        grossTotal = grossTotal - grossProfit;
                    }
                }

                //else
                //    grossTotal = grossTotal - soldValue;
                //lblBuy.Text = String.Format("{0:0.00}", lblBuy.Text);


                lblGrossProfit.Text = grossProfit.ToString("f2");

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossTotal = e.Row.FindControl("lblGrossTotal") as Label;
                lblGrossTotal.Text = grossTotal.ToString("f2");
                //sumvalue = Convert.ToDouble(lblGPTotal.Text) + grossTotal;
                //lblGPTotal.Text = grossTotal.ToString("f2") ; //sumvalue.ToString("f2");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvGross_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvGross.PageIndex = e.NewPageIndex;
            DateTime startDate, endDate;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            GenerateGrossProfit(sDataSource, startDate, endDate);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
