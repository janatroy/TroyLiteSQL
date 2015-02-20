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

public partial class WorkReport : System.Web.UI.Page
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

    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListExecutive();
        drpsIncharge.DataSource = ds;
        drpsIncharge.DataBind();
        drpsIncharge.DataTextField = "empFirstName";
        drpsIncharge.DataValueField = "empno";
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

    public void ExportToExcel()
    {
        try
        {
            Response.Clear();

            Response.Buffer = true;



            Response.AddHeader("content-disposition",

             "attachment;filename=WorkReport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Work Report";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvWME);

            

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


    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);


            int empNo = 0;
            string dExpStrdate = string.Empty;
            string dExpEnddate = string.Empty;
            string dCreatdate = string.Empty;
            string status = string.Empty;



            if (drpsIncharge.Text.Trim() != string.Empty)
                empNo = Convert.ToInt32(drpsIncharge.Text.Trim());


            if (txtStartDate.Text.Trim() != string.Empty)
                dExpStrdate = txtStartDate.Text.Trim();

            if (txtEndDate.Text.Trim() != string.Empty)
                dExpEnddate = txtEndDate.Text.Trim();


            if (drpsIncharge.Text.Trim() != string.Empty)
                status = drpsStatus.Text.Trim();



            DataSet ds = bl.generateWMEReportDS(empNo, dExpStrdate, dExpEnddate, status);
            gvWME.DataSource = ds;
            gvWME.DataBind();

            div1.Visible = true;
            divmain.Visible = false;


            Response.Write("<script language='javascript'> window.open('WorkReport1.aspx?empNo=" + empNo + "&status=" + status + "&dExpStrdate=" + Convert.ToDateTime(dExpStrdate) + "&dExpEnddate=" + Convert.ToDateTime(dExpEnddate) + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
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
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);


            int empNo = 0;
            string dExpStrdate = string.Empty;
            string dExpEnddate = string.Empty;
            string dCreatdate = string.Empty;
            string status = string.Empty;



            if (drpsIncharge.Text.Trim() != string.Empty)
                empNo = Convert.ToInt32(drpsIncharge.Text.Trim());


            if (txtStartDate.Text.Trim() != string.Empty)
                dExpStrdate = txtStartDate.Text.Trim();

            if (txtEndDate.Text.Trim() != string.Empty)
                dExpEnddate = txtEndDate.Text.Trim();


            if (drpsIncharge.Text.Trim() != string.Empty)
                status = drpsStatus.Text.Trim();



            DataSet ds = bl.generateWMEReportDS(empNo, dExpStrdate, dExpEnddate, status);
            gvWME.DataSource = ds;
            gvWME.DataBind();

            ExportToExcel();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvTSE_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvTSE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}
