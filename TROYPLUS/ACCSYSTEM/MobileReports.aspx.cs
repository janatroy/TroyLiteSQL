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

public partial class MobileReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnStockLedger_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("StockListReport.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void BtnLedger_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("LedgerReport.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void BtnBack_Click(object sender, EventArgs e)
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
    protected void BtnSalesSummary_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("SalesSummaryReport.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void BtnPurchaseRpt_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("PurchaseSummaryReport.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
