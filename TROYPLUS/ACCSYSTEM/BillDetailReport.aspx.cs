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

public partial class BillDetailReport : System.Web.UI.Page
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            GenerateDs();
            Session["dataset"] = System.Configuration.ConfigurationManager.AppSettings["BillDetailsFileName"].ToString();
            Session["startDate"] = txtStartDate.Text;
            Session["endDate"] = txtEndDate.Text;
            Response.Redirect("PrintBillReport.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void GenerateDs()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"]  != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/frm_Login.aspx");

        string sArea = string.Empty;
        string sDataSource = connStr.Remove(0, 45);
        string sXmlPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BillDetailsFileName"].ToString());
        string sXmlNodeName = "BillDetails";

        DateTime startDate, endDate;
        CustomerReportBL.ReportClass custReport;

        sArea = drpArea.SelectedItem.Text;

        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        custReport = new CustomerReportBL.ReportClass();
        custReport.generateBillDetailsReport(sArea, startDate, endDate, sXmlNodeName, sDataSource, sXmlPath);
        DataSet ds = new DataSet();
        ds.ReadXml(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BillDetailsFileName"].ToString()), XmlReadMode.InferSchema);
        //return ds;

    }

}
