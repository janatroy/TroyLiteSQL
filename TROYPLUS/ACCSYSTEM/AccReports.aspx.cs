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

public partial class AccReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (!Page.User.IsInRole("TRLBRPT"))
                    RowTrialBalance.Visible = false;

                if (!Page.User.IsInRole("BALSHTRPT"))
                    RowBalanceSheet.Visible = false;

                if (!Page.User.IsInRole("PLRPT"))
                    RowProfitLoss.Visible = false;

                if (!Page.User.IsInRole("BAKSTRPT"))
                    RowBankStatement.Visible = false;

                if (!Page.User.IsInRole("CACCRPT"))
                    RowCashAccount.Visible = false;

                if (!Page.User.IsInRole("LEDRPT"))
                    RowLedgerReport.Visible = false;

                if (!Page.User.IsInRole("DYBKRPT"))
                    RowDayBook.Visible = false;

                //if (!Page.User.IsInRole("SALTAX"))
                //    Tr14.Visible = false;

                if (!Page.User.IsInRole("EXPRPT"))
                    Tr11.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
