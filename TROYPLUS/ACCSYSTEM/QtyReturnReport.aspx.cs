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

public partial class QtyReturnReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!IsPostBack)
            {
                //AccessDataSource1.DataFile = sDataSource;
                hdFilename.Value = "Reports//" + System.Guid.NewGuid().ToString() + ConfigurationSettings.AppSettings["QtyRetReportFileName"].ToString();
                loadLedger();
            }
            if (hdToDelete.Value == "BrowserClose")
            {
                deleteFile();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate, endDate;
            int iLedgerID = 0;
            string sLedgerName = string.Empty;

            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            //    string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            string sXmlPath = Server.MapPath(hdFilename.Value);
            string sXmlNodeName = "ReturnQty";
            ReportsBL.ReportClass rptQtyReturn;


            iLedgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
            sLedgerName = drpLedgerName.SelectedItem.Text;

            rptQtyReturn = new ReportsBL.ReportClass();
            rptQtyReturn.generateQtyReturnReport(iLedgerID, sXmlNodeName, sDataSource, sXmlPath);

            Session["dataset"] = hdFilename.Value;
            Session["Filename"] = hdFilename.Value;
            Session["LedgerID"] = iLedgerID.ToString();
            Session["Ledger"] = drpLedgerName.SelectedItem.Text;
            Response.Redirect("PrintQtyReturnReport.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadLedger()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListLedger();
        drpLedgerName.DataSource = ds;
        drpLedgerName.DataBind();
        drpLedgerName.DataTextField = "LedgerName";
        drpLedgerName.DataValueField = "LedgerID";

    }

    private void deleteFile()
    {
        if (Session["Filename"] != null)
        {
            string delFilename = Session["Filename"].ToString();
            if (File.Exists(Server.MapPath(delFilename)))
                File.Delete(Server.MapPath(delFilename));
        }
    }
}
