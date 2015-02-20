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

public partial class StockReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (!Page.User.IsInRole("REORDRPT"))
                    RowReoderlevel.Visible = false;

                if (!Page.User.IsInRole("STKRPT"))
                    RowStockReport.Visible = false;

                if (!Page.User.IsInRole("STKAGE"))
                    RowStockAging.Visible = false;

                if (!Page.User.IsInRole("STKLEDRPT"))
                    RowstockLedger.Visible = false;

                if (!Page.User.IsInRole("STKRECRPT"))
                    RowStockRecon.Visible = false;

                if (!Page.User.IsInRole("STKVERRPT"))
                    RowStockVerification.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
