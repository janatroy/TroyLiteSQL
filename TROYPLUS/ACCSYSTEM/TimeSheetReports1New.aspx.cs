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

public partial class TimeSheetReports1New : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");
           
            int empNO = 0;
            int managerID = 0;
            string empName =string.Empty;
            string managerName = string.Empty;
            string sWeekID = string.Empty;
            string sApproved = string.Empty;
            
            BusinessLogic bl = new BusinessLogic(sDataSource);


            if (Request.QueryString["sWeekID"] != null)
            {
                sWeekID = Request.QueryString["sWeekID"].ToString();

                lblSelectedWeek.Text = DateTimeExtension.GetFormatWeekForGivenWeekID(sWeekID); 
            }
            if (Request.QueryString["empNO"] != null)
            {
                empNO = Convert.ToInt32(Request.QueryString["empNO"].ToString());

                lblEmployeeNumber.Text = empNO.ToString();
            }
            if (Request.QueryString["smanagerID"] != null)
            {
                managerID = Convert.ToInt32(Request.QueryString["smanagerID"].ToString());
            }
            if (Request.QueryString["sApproved"] != null)
            {
                sApproved = Request.QueryString["sApproved"].ToString();

                lblApprovedStatus.Text = sApproved.ToString();
            }
            bl.GetEmployeeMangerName(empNO, out empName, managerID, out managerName);
            lblEmployeeName.Text = empName;
            lblArroverName.Text = managerName;

            DataSet ds = bl.generateTSEReportDSNew(sWeekID, empNO, managerID, sApproved, sDataSource);
            gvTSE.DataSource = ds;
            gvTSE.DataBind();
        }
    }
    protected void gvTSE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            //For first column set to 200 px
            TableCell cell = e.Row.Cells[0];
            cell.Width = new Unit("85px");

            //For others set to 50 px
            //You can set all the width individually

            for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
            {
                //Mind that i used i=1 not 0 because the width of cells(0) has already been set
                TableCell cell2 = e.Row.Cells[i];
                cell2.Width = new Unit("105px");
            }
        }
    }
}
