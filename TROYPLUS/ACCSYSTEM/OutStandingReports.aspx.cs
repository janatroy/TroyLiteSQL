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

public partial class OutStandingReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (!Page.User.IsInRole("OUTRPT"))
                    RowOutStanding.Visible = false;

                //if (!Page.User.IsInRole("OUTBDRPT"))
                //    RowOutStandingBalanceDealer.Visible = false;

                //if (!Page.User.IsInRole("OUTBERPT"))
                //    RowOutStandingBalanceExec.Visible = false;

                //if (!Page.User.IsInRole("EXECOUT"))
                //    RowExecOut.Visible = false;

                if (!Page.User.IsInRole("OUTAGE"))
                    RowAging.Visible = false;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
