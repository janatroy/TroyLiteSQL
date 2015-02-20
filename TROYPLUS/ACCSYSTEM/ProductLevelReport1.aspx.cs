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

public partial class ProductLevelReport1 : System.Web.UI.Page
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
                //lblBillDate.Text = DateTime.Now.ToShortDateString();

                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                //txtEndDate.Text = DateTime.Now.ToShortDateString();

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtEndDate.Text = dtaa;

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
                DataSet ds = rptSalesReport.generateSalesLevel1Report(startDate, endDate, sDataSource);
                gvSalesLevel1.DataSource = ds;
                gvSalesLevel1.DataBind();


                ds = rptSalesReport.generateSalesLevel2Report(startDate, endDate, sDataSource);
                gvSalesLevel2.DataSource = ds;
                gvSalesLevel2.DataBind();



                ds = rptSalesReport.generateSalesLevel3Report(startDate, endDate, sDataSource);
                gvSalesLevel3.DataSource = ds;
                gvSalesLevel3.DataBind();



                ds = rptSalesReport.generateSalesLevel4Report(startDate, endDate, sDataSource);
                gvSalesLevel4.DataSource = ds;
                gvSalesLevel4.DataBind();


                div1.Visible = false;
                divPrint.Visible = true;
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
            DataSet ds = rptSalesReport.generateSalesLevel1Report(startDate, endDate, sDataSource);
            gvSalesLevel1.DataSource = ds;
            gvSalesLevel1.DataBind();

            //#region Export To Excel
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add(new DataColumn("Product Name"));
            //    dt.Columns.Add(new DataColumn("Sold Qty."));
            //    dt.Columns.Add(new DataColumn("Total Amount"));
            //    dt.Columns.Add(new DataColumn("Avg. Rate"));
            //    dt.Columns.Add(new DataColumn("Avg. Profit"));

            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DataRow dr_export = dt.NewRow();
            //        dr_export["Product Name"] = dr["ProductName"];
            //        dr_export["Sold Qty."] = dr["SoldQty"];
            //        dr_export["Total Amount"] = dr["TotalAmount"];
            //        dr_export["Avg. Rate"] = dr["AvgRate"];
            //        dr_export["Avg. Profit"] = dr["AvgProfit"];
            //        dt.Rows.Add(dr_export);
            //    }

            //    DataRow dr_lastexport = dt.NewRow();
            //    dr_lastexport["Product Name"] = "";
            //    dr_lastexport["Sold Qty."] = "";
            //    dr_lastexport["Total Amount"] = "";
            //    dr_lastexport["Avg. Rate"] = "";
            //    dr_lastexport["Avg. Profit"] = "";
            //    ExportToExcel("SalesLevel1Report.xls", dt);
            //}
            //#endregion

            ds = rptSalesReport.generateSalesLevel2Report(startDate, endDate, sDataSource);
            gvSalesLevel2.DataSource = ds;
            gvSalesLevel2.DataBind();

            //#region Export To Excel
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add(new DataColumn("Product Name"));
            //    dt.Columns.Add(new DataColumn("Product Desc."));
            //    dt.Columns.Add(new DataColumn("Qty. Sold"));
            //    dt.Columns.Add(new DataColumn("Total Amount"));
            //    dt.Columns.Add(new DataColumn("Avg. Rate"));
            //    dt.Columns.Add(new DataColumn("Avg. Profit"));

            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DataRow dr_export = dt.NewRow();
            //        dr_export["Product Name"] = dr["ProductName"];
            //        dr_export["Product Desc."] = dr["ProductDesc"];
            //        dr_export["Qty. Sold"] = dr["QtySold"];
            //        dr_export["Total Amount"] = dr["TotalAmount"];
            //        dr_export["Avg. Rate"] = dr["AvgRate"];
            //        dr_export["Avg. Profit"] = dr["AvgProfit"];
            //        dt.Rows.Add(dr_export);
            //    }

            //    DataRow dr_lastexport = dt.NewRow();
            //    dr_lastexport["Product Name"] = "";
            //    dr_lastexport["Product Desc."] = "";
            //    dr_lastexport["Qty. Sold"] = "";
            //    dr_lastexport["Total Amount"] = "";
            //    dr_lastexport["Avg. Rate"] = "";
            //    dr_lastexport["Avg. Profit"] = "";
            //    ExportToExcel("SalesLevel2Report.xls", dt);
            //}
            //#endregion


            ds = rptSalesReport.generateSalesLevel3Report(startDate, endDate, sDataSource);
            gvSalesLevel3.DataSource = ds;
            gvSalesLevel3.DataBind();

            //#region Export To Excel
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add(new DataColumn("Product Name"));
            //    dt.Columns.Add(new DataColumn("Product Desc."));
            //    dt.Columns.Add(new DataColumn("Model"));
            //    dt.Columns.Add(new DataColumn("Qty. Sold"));
            //    dt.Columns.Add(new DataColumn("Total Amount"));
            //    dt.Columns.Add(new DataColumn("Avg. Rate"));
            //    dt.Columns.Add(new DataColumn("Avg. Profit"));

            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DataRow dr_export = dt.NewRow();
            //        dr_export["Product Name"] = dr["ProductName"];
            //        dr_export["Product Desc."] = dr["ProductDesc"];
            //        dr_export["Model"] = dr["Model"];
            //        dr_export["Qty. Sold"] = dr["QtySold"];
            //        dr_export["Total Amount"] = dr["TotalAmount"];
            //        dr_export["Avg. Rate"] = dr["AvgRate"];
            //        dr_export["Avg. Profit"] = dr["AvgProfit"];
            //        dt.Rows.Add(dr_export);
            //    }

            //    DataRow dr_lastexport = dt.NewRow();
            //    dr_lastexport["Product Name"] = "";
            //    dr_lastexport["Product Desc."] = "";
            //    dr_lastexport["Model"] = "";
            //    dr_lastexport["Qty. Sold"] = "";
            //    dr_lastexport["Total Amount"] = "";
            //    dr_lastexport["Avg. Rate"] = "";
            //    dr_lastexport["Avg. Profit"] = "";
            //    ExportToExcel("SalesLevel3Report.xls", dt);
            //}
            //#endregion

            ds = rptSalesReport.generateSalesLevel4Report(startDate, endDate, sDataSource);
            gvSalesLevel4.DataSource = ds;
            gvSalesLevel4.DataBind();

            //#region Export To Excel
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add(new DataColumn("Product Name"));
            //    dt.Columns.Add(new DataColumn("Product Desc."));
            //    dt.Columns.Add(new DataColumn("Model"));
            //    dt.Columns.Add(new DataColumn("Bill No."));
            //    dt.Columns.Add(new DataColumn("Bill Date"));
            //    dt.Columns.Add(new DataColumn("Qty. Sold"));
            //    dt.Columns.Add(new DataColumn("Total Amount"));
            //    dt.Columns.Add(new DataColumn("Avg. Rate"));
            //    dt.Columns.Add(new DataColumn("Avg. Profit"));

            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DataRow dr_export = dt.NewRow();
            //        dr_export["Product Name"] = dr["ProductName"];
            //        dr_export["Product Desc."] = dr["ProductDesc"];
            //        dr_export["Model"] = dr["Model"];
            //        dr_export["Bill No."] = dr["billno"];
            //        dr_export["Bill Date"] = dr["BillDate"];
            //        dr_export["Qty. Sold"] = dr["QtySold"];
            //        dr_export["Total Amount"] = dr["TotalAmount"];
            //        dr_export["Avg. Rate"] = dr["AvgRate"];
            //        dr_export["Avg. Profit"] = dr["AvgProfit"];
            //        dt.Rows.Add(dr_export);
            //    }

            //    DataRow dr_lastexport = dt.NewRow();
            //    dr_lastexport["Product Name"] = "";
            //    dr_lastexport["Product Desc."] = "";
            //    dr_lastexport["Model"] = "";
            //    dr_lastexport["Bill No."] = "";
            //    dr_lastexport["Bill Date"] = "";
            //    dr_lastexport["Qty. Sold"] = "";
            //    dr_lastexport["Total Amount"] = "";
            //    dr_lastexport["Avg. Rate"] = "";
            //    dr_lastexport["Avg. Profit"] = "";
            //    ExportToExcel("SalesLevel4Report.xls", dt);
            //}
            //#endregion

            div1.Visible = false;
            divPrint.Visible = true;
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
            DataSet ds = rptSalesReport.generateSalesLevel1Report(startDate, endDate, sDataSource);
            //gvSalesLevel1.DataSource = ds;
            //gvSalesLevel1.DataBind();

            #region Export To Excel
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Product Name"));
                dt.Columns.Add(new DataColumn("Sold Qty."));
                dt.Columns.Add(new DataColumn("Total Amount"));
                dt.Columns.Add(new DataColumn("Avg. Rate"));
                dt.Columns.Add(new DataColumn("Avg. Profit"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["Product Name"] = dr["ProductName"];
                    dr_export["Sold Qty."] = dr["SoldQty"];
                    dr_export["Total Amount"] = dr["TotalAmount"];
                    dr_export["Avg. Rate"] = dr["AvgRate"];
                    dr_export["Avg. Profit"] = dr["AvgProfit"];
                    dt.Rows.Add(dr_export);
                }

                DataRow dr_lastexport = dt.NewRow();
                dr_lastexport["Product Name"] = "";
                dr_lastexport["Sold Qty."] = "";
                dr_lastexport["Total Amount"] = "";
                dr_lastexport["Avg. Rate"] = "";
                dr_lastexport["Avg. Profit"] = "";
                ExportToExcel("SalesLevel1Report.xls", dt);
            }
            #endregion

            ds = rptSalesReport.generateSalesLevel2Report(startDate, endDate, sDataSource);
            //gvSalesLevel2.DataSource = ds;
            //gvSalesLevel2.DataBind();

            #region Export To Excel
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Product Name"));
                dt.Columns.Add(new DataColumn("Product Desc."));
                dt.Columns.Add(new DataColumn("Qty. Sold"));
                dt.Columns.Add(new DataColumn("Total Amount"));
                dt.Columns.Add(new DataColumn("Avg. Rate"));
                dt.Columns.Add(new DataColumn("Avg. Profit"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["Product Name"] = dr["ProductName"];
                    dr_export["Product Desc."] = dr["ProductDesc"];
                    dr_export["Qty. Sold"] = dr["QtySold"];
                    dr_export["Total Amount"] = dr["TotalAmount"];
                    dr_export["Avg. Rate"] = dr["AvgRate"];
                    dr_export["Avg. Profit"] = dr["AvgProfit"];
                    dt.Rows.Add(dr_export);
                }

                DataRow dr_lastexport = dt.NewRow();
                dr_lastexport["Product Name"] = "";
                dr_lastexport["Product Desc."] = "";
                dr_lastexport["Qty. Sold"] = "";
                dr_lastexport["Total Amount"] = "";
                dr_lastexport["Avg. Rate"] = "";
                dr_lastexport["Avg. Profit"] = "";
                ExportToExcel("SalesLevel2Report.xls", dt);
            }
            #endregion


            ds = rptSalesReport.generateSalesLevel3Report(startDate, endDate, sDataSource);
            //gvSalesLevel3.DataSource = ds;
            //gvSalesLevel3.DataBind();

            #region Export To Excel
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Product Name"));
                dt.Columns.Add(new DataColumn("Product Desc."));
                dt.Columns.Add(new DataColumn("Model"));
                dt.Columns.Add(new DataColumn("Qty. Sold"));
                dt.Columns.Add(new DataColumn("Total Amount"));
                dt.Columns.Add(new DataColumn("Avg. Rate"));
                dt.Columns.Add(new DataColumn("Avg. Profit"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["Product Name"] = dr["ProductName"];
                    dr_export["Product Desc."] = dr["ProductDesc"];
                    dr_export["Model"] = dr["Model"];
                    dr_export["Qty. Sold"] = dr["QtySold"];
                    dr_export["Total Amount"] = dr["TotalAmount"];
                    dr_export["Avg. Rate"] = dr["AvgRate"];
                    dr_export["Avg. Profit"] = dr["AvgProfit"];
                    dt.Rows.Add(dr_export);
                }

                DataRow dr_lastexport = dt.NewRow();
                dr_lastexport["Product Name"] = "";
                dr_lastexport["Product Desc."] = "";
                dr_lastexport["Model"] = "";
                dr_lastexport["Qty. Sold"] = "";
                dr_lastexport["Total Amount"] = "";
                dr_lastexport["Avg. Rate"] = "";
                dr_lastexport["Avg. Profit"] = "";
                ExportToExcel("SalesLevel3Report.xls", dt);
            }
            #endregion

            ds = rptSalesReport.generateSalesLevel4Report(startDate, endDate, sDataSource);
            //gvSalesLevel4.DataSource = ds;
            //gvSalesLevel4.DataBind();

            #region Export To Excel
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Product Name"));
                dt.Columns.Add(new DataColumn("Product Desc."));
                dt.Columns.Add(new DataColumn("Model"));
                dt.Columns.Add(new DataColumn("Bill No."));
                dt.Columns.Add(new DataColumn("Bill Date"));
                dt.Columns.Add(new DataColumn("Qty. Sold"));
                dt.Columns.Add(new DataColumn("Total Amount"));
                dt.Columns.Add(new DataColumn("Avg. Rate"));
                dt.Columns.Add(new DataColumn("Avg. Profit"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["Product Name"] = dr["ProductName"];
                    dr_export["Product Desc."] = dr["ProductDesc"];
                    dr_export["Model"] = dr["Model"];
                    dr_export["Bill No."] = dr["billno"];
                    dr_export["Bill Date"] = dr["BillDate"];
                    dr_export["Qty. Sold"] = dr["QtySold"];
                    dr_export["Total Amount"] = dr["TotalAmount"];
                    dr_export["Avg. Rate"] = dr["AvgRate"];
                    dr_export["Avg. Profit"] = dr["AvgProfit"];
                    dt.Rows.Add(dr_export);
                }

                DataRow dr_lastexport = dt.NewRow();
                dr_lastexport["Product Name"] = "";
                dr_lastexport["Product Desc."] = "";
                dr_lastexport["Model"] = "";
                dr_lastexport["Bill No."] = "";
                dr_lastexport["Bill Date"] = "";
                dr_lastexport["Qty. Sold"] = "";
                dr_lastexport["Total Amount"] = "";
                dr_lastexport["Avg. Rate"] = "";
                dr_lastexport["Avg. Profit"] = "";
                ExportToExcel("SalesLevel4Report.xls", dt);
            }
            #endregion

            //divPrint.Visible = true;

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(string filename, DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }

    }
}