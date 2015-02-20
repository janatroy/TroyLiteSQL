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

public partial class TimeSheetReportsNew : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

    #region "Control Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            loadEmp();
            loadManager();       
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
    
    protected void btndet_Click(object sender, EventArgs e)
    {
        div1.Visible = true;
        divPrint.Visible = false;
        divmain.Visible = false;
    }
    
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        int empNO = 0;
        int managerID = 0;
        string empName = string.Empty;
        string managerName = string.Empty;
        string sWeekID = string.Empty;
        string dTSEDateRange = string.Empty;
        string sApproved = string.Empty;

        if (ddlEmployee.Text.Trim() != string.Empty)
        {
            empNO = Convert.ToInt32(ddlEmployee.Text.Trim());
            lblEmployeeNumber.Text = empNO.ToString();
        }

        if (ddlApprover.Text.Trim() != string.Empty)
        {
            managerID = Convert.ToInt32(ddlApprover.Text.Trim());
        }

        if (txtDateRange.Text.Trim() != string.Empty)
        {
            dTSEDateRange = txtDateRange.Text.Trim();
            sWeekID = DateTimeExtension.CurrentWeekwithYear(Convert.ToDateTime(txtDateRange.Text));
            lblSelectedWeek.Text =DateTimeExtension.GetFormatWeekForGivenWeekID(sWeekID);
        }

       if (drpsApproved.Text.Trim() != string.Empty)
        {
            sApproved = drpsApproved.Text.Trim();
            lblApprovedStatus.Text = sApproved.ToString();
        }

        BusinessLogic bl = new BusinessLogic(sDataSource);
        bl.GetEmployeeMangerName(empNO, out empName, managerID, out managerName);
        lblEmployeeName.Text = empName;
        lblArroverName.Text = managerName;

        if (bl.IsManagerForThisEmployee(empNO, managerID))
        {
            DataSet ds = bl.generateTSEReportDSNew(sWeekID, empNO, managerID, sApproved, sDataSource);
            gvTSE.DataSource = ds;
            gvTSE.DataBind();

            ExportToExcel();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected Employee is not reporting to this Approver. Report cannot be generated.');", true);
        }
    }

    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        int empNO = 0;
        int managerID = 0;
        string dTSEDateRange = string.Empty;
        string sApproved = string.Empty;
        string sWeekID = string.Empty;

        if (ddlEmployee.Text.Trim() != string.Empty)
        {
            empNO = Convert.ToInt32(ddlEmployee.Text.Trim());
            lblEmployeeNumber.Text = empNO.ToString();
        }

        if (ddlApprover.Text.Trim() != string.Empty)
        {
            managerID = Convert.ToInt32(ddlApprover.Text.Trim());
        }

        if (txtDateRange.Text.Trim() != string.Empty)
        {
            dTSEDateRange = txtDateRange.Text.Trim();
            sWeekID = DateTimeExtension.CurrentWeekwithYear(Convert.ToDateTime(dTSEDateRange));
            lblSelectedWeek.Text = dTSEDateRange.ToString();
        }

        if (drpsApproved.Text.Trim() != string.Empty)
        {
            sApproved = drpsApproved.Text.Trim();
            lblApprovedStatus.Text = sApproved.ToString();
        }

        div1.Visible = true;
        divmain.Visible = false;

          BusinessLogic bl = new BusinessLogic(sDataSource);

          if (bl.IsManagerForThisEmployee(empNO, managerID))
          {
              Response.Write("<script language='javascript'> window.open('TimeSheetReports1.aspx?empNO=" + empNO + "&sApproved=" + sApproved + "&smanagerID= " + managerID + "&sWeekID=" + sWeekID + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
          }
          else
          {
              ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected Employee is not reporting to this Approver. Report cannot be generated.');", true);
          }
    }

    #endregion "Control Events"

    #region "Supporting Methods"
    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListExecutive();
        ddlEmployee.DataSource = ds;
        ddlEmployee.DataBind();
        ddlEmployee.DataTextField = "empFirstName";
        ddlEmployee.DataValueField = "empno";
    }

    private void loadManager()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListManagers();
        ddlApprover.DataSource = ds;
        ddlApprover.DataBind();
        ddlApprover.DataTextField = "ManagerFirstName";
        ddlApprover.DataValueField = "ManagerID";
    }

    public void ExportToExcel()
    {
        try
        {
            Response.Clear();

            Response.Buffer = true;
            
            Response.AddHeader("content-disposition","attachment;filename=TimeSheetReport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Table tb = new Table();

            TableCell cell1 = new TableCell();
            cell1.Controls.Add(tblTimeSheet);

            TableCell cell2 = new TableCell();
            cell2.Text = "<br/><br/>";

            TableCell cell3 = new TableCell();
            cell3.Controls.Add(gvTSE);

            TableRow tr1 = new TableRow();
            tr1.Cells.Add(cell1);
            tb.Rows.Add(tr1);

            TableRow tr2 = new TableRow();
            tr2.Cells.Add(cell2);
            tb.Rows.Add(tr2);

            TableRow tr3 = new TableRow();
            tr3.Cells.Add(cell3);
            tb.Rows.Add(tr3);

            tb.RenderControl(hw);

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            Response.Write(style);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();

            //ExportToExcel(ds);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
        }
    }

    #endregion "Supporting Methods"

   
}
