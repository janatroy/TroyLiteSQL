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

public partial class PurchaseLevelReport1 : System.Web.UI.Page
{

    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                lblBillDate.Text = DateTime.Now.ToShortDateString();

                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtEndDate.Text = dtaa;

                //txtEndDate.Text = DateTime.Now.ToShortDateString();

                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

                    if (companyInfo != null)
                    {
                        if (companyInfo.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in companyInfo.Tables[0].Rows)
                            {
                                lblTNGST.Text = Convert.ToString(dr["TINno"]);
                                lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                                lblPhone.Text = Convert.ToString(dr["Phone"]);
                                lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                                lblAddress.Text = Convert.ToString(dr["Address"]);
                                lblCity.Text = Convert.ToString(dr["city"]);
                                lblPincode.Text = Convert.ToString(dr["Pincode"]);
                                lblState.Text = Convert.ToString(dr["state"]);

                            }
                        }
                    }
                }



                DateTime startDate, endDate;
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DateTime stdt = Convert.ToDateTime(txtStartDate.Text);
                DateTime etdt = Convert.ToDateTime(txtEndDate.Text);

                if (Request.QueryString["startDate"] != null)
                {
                    stdt = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
                }
                if (Request.QueryString["endDate"] != null)
                {
                    etdt = Convert.ToDateTime(Request.QueryString["endDate"].ToString());
                }

                startDate = Convert.ToDateTime(stdt);
                endDate = Convert.ToDateTime(etdt);

                var rptSalesReport = new ReportsBL.ReportClass();

                DataSet ds = rptSalesReport.generatePurchaseLevel1Report(startDate, endDate, sDataSource);
                gvPurchaseLevel1.DataSource = ds;
                gvPurchaseLevel1.DataBind();

                ds = rptSalesReport.generatePurchaseLevel2Report(startDate, endDate, sDataSource);
                gvPurchaseLevel2.DataSource = ds;
                gvPurchaseLevel2.DataBind();

                ds = rptSalesReport.generatePurchaseLevel3Report(startDate, endDate, sDataSource);
                gvPurchaseLevel3.DataSource = ds;
                gvPurchaseLevel3.DataBind();

                ds = rptSalesReport.generatePurchaseLevel4Report(startDate, endDate, sDataSource);
                gvPurchaseLevel4.DataSource = ds;
                gvPurchaseLevel4.DataBind();

                divPrint.Visible = true;
                div1.Visible = false;
                divmain.Visible = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvSalesLevel1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvSalesLevel2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvSalesLevel3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvSalesLevel4_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvPurchaseLevel1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvPurchaseLevel2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvPurchaseLevel3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvPurchaseLevel4_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate, endDate;
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);

            var rptSalesReport = new ReportsBL.ReportClass();

            DataSet ds = rptSalesReport.generatePurchaseLevel1Report(startDate, endDate, sDataSource);
            gvPurchaseLevel1.DataSource = ds;
            gvPurchaseLevel1.DataBind();

            ds = rptSalesReport.generatePurchaseLevel2Report(startDate, endDate, sDataSource);
            gvPurchaseLevel2.DataSource = ds;
            gvPurchaseLevel2.DataBind();

            ds = rptSalesReport.generatePurchaseLevel3Report(startDate, endDate, sDataSource);
            gvPurchaseLevel3.DataSource = ds;
            gvPurchaseLevel3.DataBind();

            ds = rptSalesReport.generatePurchaseLevel4Report(startDate, endDate, sDataSource);
            gvPurchaseLevel4.DataSource = ds;
            gvPurchaseLevel4.DataBind();

            divPrint.Visible = true;

            ExportToExcel();
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
            DateTime startDate, endDate;
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);

            var rptSalesReport = new ReportsBL.ReportClass();

            DataSet ds = rptSalesReport.generatePurchaseLevel1Report(startDate, endDate, sDataSource);
            gvPurchaseLevel1.DataSource = ds;
            gvPurchaseLevel1.DataBind();

            ds = rptSalesReport.generatePurchaseLevel2Report(startDate, endDate, sDataSource);
            gvPurchaseLevel2.DataSource = ds;
            gvPurchaseLevel2.DataBind();

            ds = rptSalesReport.generatePurchaseLevel3Report(startDate, endDate, sDataSource);
            gvPurchaseLevel3.DataSource = ds;
            gvPurchaseLevel3.DataBind();

            ds = rptSalesReport.generatePurchaseLevel4Report(startDate, endDate, sDataSource);
            gvPurchaseLevel4.DataSource = ds;
            gvPurchaseLevel4.DataBind();

            divPrint.Visible = true;
            div1.Visible = false;
            divmain.Visible = true;
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

    public void ExportToExcel()
    {

        try
        {
            Response.Clear();

            Response.Buffer = true;

            string file = "Purchase Level Report_" + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition",

             "attachment;filename=" + file);

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);





            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "&nbsp;";

            TableCell cell2 = new TableCell();

            cell2.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Purchase Level 1";

            TableCell cell3 = new TableCell();

            cell3.Text = "&nbsp;";

            TableCell cell4 = new TableCell();

            cell4.Controls.Add(gvPurchaseLevel1);



            TableCell cell5 = new TableCell();

            cell5.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Purchase Level 2";

            TableCell cell6 = new TableCell();

            cell6.Text = "&nbsp;";

            TableCell cell7 = new TableCell();

            cell7.Controls.Add(gvPurchaseLevel2);



            TableCell cell8 = new TableCell();

            cell8.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Purchase Level 3";

            TableCell cell9 = new TableCell();

            cell9.Text = "&nbsp;";

            TableCell cell10 = new TableCell();

            cell10.Controls.Add(gvPurchaseLevel3);


            TableCell cell11 = new TableCell();

            cell11.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Purchase Level 4";

            TableCell cell12 = new TableCell();

            cell12.Text = "&nbsp;";

            TableCell cell13 = new TableCell();

            cell13.Controls.Add(gvPurchaseLevel4);




            tr1.Cells.Add(cell1);

            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);

            TableRow tr3 = new TableRow();

            tr3.Cells.Add(cell3);


            TableRow tr4 = new TableRow();

            tr4.Cells.Add(cell4);

            TableRow tr5 = new TableRow();

            tr5.Cells.Add(cell5);

            TableRow tr6 = new TableRow();

            tr6.Cells.Add(cell6);

            TableRow tr7 = new TableRow();

            tr7.Cells.Add(cell7);


            TableRow tr8 = new TableRow();

            tr8.Cells.Add(cell8);

            TableRow tr9 = new TableRow();

            tr9.Cells.Add(cell9);

            TableRow tr10 = new TableRow();

            tr10.Cells.Add(cell10);


            TableRow tr11 = new TableRow();

            tr11.Cells.Add(cell11);

            TableRow tr12 = new TableRow();

            tr12.Cells.Add(cell12);

            TableRow tr13 = new TableRow();

            tr13.Cells.Add(cell13);

            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.Rows.Add(tr3);

            tb.Rows.Add(tr4);

            tb.Rows.Add(tr5);

            tb.Rows.Add(tr6);

            tb.Rows.Add(tr7);

            tb.Rows.Add(tr8);

            tb.Rows.Add(tr9);

            tb.Rows.Add(tr10);


            tb.Rows.Add(tr11);

            tb.Rows.Add(tr12);

            tb.Rows.Add(tr13);

            tb.RenderControl(hw);


            string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            Response.Write(style);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}