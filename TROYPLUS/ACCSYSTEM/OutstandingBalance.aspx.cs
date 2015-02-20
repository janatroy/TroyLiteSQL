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

public partial class OutstandingBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string connStr = string.Empty;
            if (!Page.IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/frm_Login.aspx");

                srcArea.ConnectionString = connStr;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public DataSet GenerateDs()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");

        string sArea = string.Empty;
        string sDataSource = connStr.Remove(0, 45);
        string sXmlPath = Server.MapPath(ConfigurationSettings.AppSettings["OutstandingBalanceFileName"].ToString());
        string sXmlNodeName = "Outstanding";
        CustomerReportBL.ReportClass outsReport;

        sArea = drpArea.SelectedItem.Text;
        outsReport = new CustomerReportBL.ReportClass();
        outsReport.generateOutstandingReport(sArea.Replace("'", "''"), sXmlNodeName, sDataSource, sXmlPath);
        DataSet ds = new DataSet();
        Session["OutStandingFilename"] = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["OutstandingBalanceFileName"].ToString());
        ds.ReadXml(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["OutstandingBalanceFileName"].ToString()), XmlReadMode.InferSchema);
        return ds;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = GenerateDs();
            Session["dataset"] = System.Configuration.ConfigurationManager.AppSettings["OutstandingBalanceFileName"].ToString();

            Response.Redirect("PrintOutstandingReport.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
