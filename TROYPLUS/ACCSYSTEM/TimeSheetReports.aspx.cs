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

public partial class TimeSheetReports : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                loadEmp();
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    
    protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
            div1.Visible = true;
            divPrint.Visible = false;
            divmain.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListExecutive();
        drpIncharge.DataSource = ds;
        drpIncharge.DataBind();
        drpIncharge.DataTextField = "empFirstName";
        drpIncharge.DataValueField = "empno";
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            int empNO = 0;
            string dTSEStartDate = string.Empty;
            string dTSEEndDate = string.Empty;
            string sApproved = string.Empty;



            if (drpIncharge.Text.Trim() != string.Empty)
                empNO = Convert.ToInt32(drpIncharge.Text.Trim());

            if (txtStartDate.Text.Trim() != string.Empty)
                dTSEStartDate = txtStartDate.Text.Trim();

            if (txtEndDate.Text.Trim() != string.Empty)
                dTSEEndDate = txtEndDate.Text.Trim();

            if (drpsApproved.Text.Trim() != string.Empty)
                sApproved = drpsApproved.Text.Trim();

            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = bl.generateTSEReportDS(dTSEStartDate, dTSEEndDate, empNO, sApproved, sDataSource);
            gvTSE.DataSource = ds;
            gvTSE.DataBind();

            ExportToExcel();
            //DataSet ds = rptTSEReport.generateTSEReportDS(dTSEStartDate, dTSEEndDate, empNO, sApproved, sDataSource);        
            //gvTSE.DataSource = ds;
            //gvTSE.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnRep_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            int empNO = 0;
            string dTSEStartDate = string.Empty;
            string dTSEEndDate = string.Empty;
            string sApproved = string.Empty;



            if (drpIncharge.Text.Trim() != string.Empty)
                empNO = Convert.ToInt32(drpIncharge.Text.Trim());

            if (txtStartDate.Text.Trim() != string.Empty)
                dTSEStartDate = txtStartDate.Text.Trim();

            if (txtEndDate.Text.Trim() != string.Empty)
                dTSEEndDate = txtEndDate.Text.Trim();

            if (drpsApproved.Text.Trim() != string.Empty)
                sApproved = drpsApproved.Text.Trim();

            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = bl.generateTSEReportDS(dTSEStartDate, dTSEEndDate, empNO, sApproved, sDataSource);
            gvTSE.DataSource = ds;
            gvTSE.DataBind();

            div1.Visible = true;
            divmain.Visible = false;
            //DataSet ds = rptTSEReport.generateTSEReportDS(dTSEStartDate, dTSEEndDate, empNO, sApproved, sDataSource);        
            //gvTSE.DataSource = ds;
            //gvTSE.DataBind();

            Response.Write("<script language='javascript'> window.open('TimeSheetReports1.aspx?empNO=" + empNO + "&sApproved=" + sApproved + "&dTSEStartDate=" + Convert.ToDateTime(dTSEStartDate) + "&dTSEEndDate=" + Convert.ToDateTime(dTSEEndDate) + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvTSE_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void ExportToExcel()
    {
        try
        {
            Response.Clear();

            Response.Buffer = true;



            Response.AddHeader("content-disposition",

             "attachment;filename=TimeSheetReport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Time Sheet Report";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvTSE);



            tr1.Cells.Add(cell1);

            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);



            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

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


    protected void gvTSE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}
