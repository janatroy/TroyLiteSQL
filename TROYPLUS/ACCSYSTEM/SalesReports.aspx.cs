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

public partial class SalesReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (!Page.User.IsInRole("SLSRPT"))
                    RowSalesReport.Visible = false;

                //if (!Page.User.IsInRole("SLSPDRPT"))
                //    RowSalesPerDealer.Visible = false;

                //if (!Page.User.IsInRole("SLSPERPT"))
                //    RowSalesPerExec.Visible = false;

                //if (!Page.User.IsInRole("SLSVCRPT"))
                //    RowSalesVATCST.Visible = false;

                //////////if (!Page.User.IsInRole("SLSUMRPT"))
                //////////    RowSalesSumm.Visible = false;

                //////////if (!Page.User.IsInRole("MISSDC"))
                //////////    RowMissingDC.Visible = false;

                //////////if (!Page.User.IsInRole("TOTSAL"))
                //////////    Tr13.Visible = false;

                //////////if (!Page.User.IsInRole("ZERORS"))
                //////////    Tr15.Visible = false;

                //////////if (!Page.User.IsInRole("SALTURN"))
                //////////    Tr12.Visible = false;

                //////////if (!Page.User.IsInRole("SALCOM"))
                //////////    Tr2.Visible = false;

                string usernam = Request.Cookies["LoggedUserName"].Value;

                if ((usernam == "mis1") || (usernam == "mis2") || (usernam == "mis3") || (usernam == "mis4") || (usernam == "mis5") || (usernam == "mis6") || (usernam == "mis7") || (usernam == "mis8") || (usernam == "mis9") || (usernam == "mis10") || (usernam == "mis11") || (usernam == "mis12") || (usernam == "accounts1") || (usernam == "accounts2") || (usernam == "accounts3") || (usernam == "audit") || (usernam == "ccare") || (usernam == "jeroline") || (usernam == "karthick") || (usernam == "kumaresh") || (usernam == "manikandan") || (usernam == "prince") || (usernam == "sankar"))
                    Tr2.Visible = false;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
