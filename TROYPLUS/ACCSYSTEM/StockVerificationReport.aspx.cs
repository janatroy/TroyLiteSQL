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

public partial class StockVerificationReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GetProductsStock();
            }

            if (!IsPostBack)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
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
                                lblBillDate.Text = DateTime.Now.ToShortDateString();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    public void GetProductsStock()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        ReportsBL.ReportClass report = new ReportsBL.ReportClass();
        DataSet ds = new DataSet();
        ds = report.getProductStockVerify(sDataSource);
        string strItemCode = string.Empty;
        DataColumn dcArr;
        double salesQty = 0;
        double purchaseQty = 0;
        double InQty = 0;
        double OutQty = 0;
        double arrQty = 0;
        double accQty = 0;
        double opStock = 0;
        dcArr = new DataColumn("ArrStock");
        ds.Tables[0].Columns.Add(dcArr);
        if (ds != null)
        {
            if (ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["itemCode"] != null)
                    {
                        strItemCode = dr["itemCode"].ToString();
                        accQty = Convert.ToDouble(dr["Stock"]);
                        opStock = report.getOpeningStock(sDataSource, strItemCode);
                        salesQty = report.getStockSales(sDataSource, strItemCode, DateTime.Now);
                        purchaseQty = report.getStockPurchase(sDataSource, strItemCode, DateTime.Now);
                        InQty = report.getStockIN(sDataSource, strItemCode, DateTime.Now);
                        OutQty = report.getStockOUT(sDataSource, strItemCode, DateTime.Now);
                        arrQty = opStock + (purchaseQty - salesQty) + (OutQty - InQty);
                        dr["ArrStock"] = arrQty.ToString();

                    }
                }
            }
        }

        GrdViewItems.DataSource = ds;
        GrdViewItems.DataBind();
        GrdViewItems.Visible = false;

        #region Export To Excel
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Product Code"));
            dt.Columns.Add(new DataColumn("Product Name"));
            dt.Columns.Add(new DataColumn("Stock In Hand"));
            dt.Columns.Add(new DataColumn("Arrived Stock"));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataRow dr_export = dt.NewRow();
                dr_export["Product Code"] = dr["ItemCode"];
                dr_export["Product Name"] = dr["Product"];
                dr_export["Stock In Hand"] = dr["Stock"];
                dr_export["Arrived Stock"] = dr["ArrStock"];
                dt.Rows.Add(dr_export);
            }

            DataRow dr_lastexport = dt.NewRow();
            dr_lastexport["Product Code"] = "";
            dr_lastexport["Product Name"] = "";
            dr_lastexport["Stock In Hand"] = "";
            dr_lastexport["Arrived Stock"] = "";
            ExportToExcel("Stock Verification Report.xls", dt);
        }
        #endregion


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
            dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
            dgGrid.HeaderStyle.Font.Bold = true;
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


    //protected void GrdViewSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GrdViewItems.PageIndex = e.NewPageIndex;
    //    GrdViewItems.DataBind();
    //}

    protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double arrQty = 0;
            double accQty = 0;
            GridViewRow row = e.Row;
            accQty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Stock"));
            arrQty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ArrStock"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (arrQty == accQty)
                {

                    row.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //bindGrid();

            GrdViewItems.PageIndex = e.NewPageIndex;
            GrdViewItems.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}

