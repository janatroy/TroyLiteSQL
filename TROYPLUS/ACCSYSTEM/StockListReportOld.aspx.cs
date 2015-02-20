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

public partial class StockListReportOld : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DataSet companyInfo = new DataSet();
                loadProducts();
                //divReport.Visible = false;
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

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
    
    private void loadProducts()
    {

        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        //ReportsBL.ReportClass report = new ReportsBL.ReportClass();

        BusinessLogic bl = new BusinessLogic();

        DataSet ds = new DataSet();
        ds = bl.getProductList(sDataSource);
        drpLedgerName.DataSource = ds;
        drpLedgerName.DataBind();
        drpLedgerName.DataTextField = "Productcode";
        drpLedgerName.DataValueField = "Product";

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            lblClosingStock.Text = "0";
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            ReportsBL.ReportClass report = new ReportsBL.ReportClass();
            double openingStock = 0;
            string[] itemAr = drpLedgerName.SelectedItem.Text.Split('|');

            string itemCode = Convert.ToString(itemAr[0]).Trim();
            lblModel.Text = Convert.ToString(itemAr[2]).Trim();
            openingStock = Convert.ToDouble(report.getOpeningStock(sDataSource, itemCode)) +
                (Convert.ToDouble(report.getOpeningStockPurchase(sDataSource, itemCode, startDate)) - Convert.ToDouble(report.getOpeningStockSales(sDataSource, itemCode, startDate)));

            lblOpenStock.Text = Convert.ToString(openingStock);
            string[] stkArray = drpLedgerName.SelectedItem.Value.Split('@');
            lblItem.Text = Convert.ToString(stkArray[0]).Trim();
            lblClosingStockPM.Text = Convert.ToString(stkArray[1]).Trim();
            hdStock.Value = Convert.ToString(openingStock);


            if (chkvalue.Checked == false)
            {
                DataSet dsLedger = report.getProductStockList(sDataSource, itemCode, startDate, endDate);
                gvLedger.DataSource = dsLedger;
                gvLedger.DataBind();
                divReport.Visible = true;

                //#region Export To Excel
                //if (dsLedger.Tables[0].Rows.Count > 0)
                //{
                //    DataTable dt = new DataTable();
                //    dt.Columns.Add(new DataColumn("Bill Date"));
                //    dt.Columns.Add(new DataColumn("Purchase / Sale"));
                //    dt.Columns.Add(new DataColumn("Bill No."));
                //    dt.Columns.Add(new DataColumn("Qty."));
                //    dt.Columns.Add(new DataColumn("Ledger Name"));

                //    foreach (DataRow dr in dsLedger.Tables[0].Rows)
                //    {
                //        DataRow dr_export = dt.NewRow();
                //        dr_export["Bill Date"] = dr["billdate"];
                //        dr_export["Purchase / Sale"] = dr["'Purchase/Sale'"];
                //        dr_export["Bill No."] = dr["billno"];
                //        dr_export["Qty."] = dr["qty"];
                //        dr_export["Ledger Name"] = dr["LedgerName"];
                //        dt.Rows.Add(dr_export);
                //    }

                //    DataRow dr_lastexport = dt.NewRow();
                //    dr_lastexport["Bill Date"] = "";
                //    dr_lastexport["Purchase / Sale"] = "";
                //    dr_lastexport["Bill No."] = "";
                //    dr_lastexport["Qty."] = "";
                //    dr_lastexport["Ledger Name"] = "";
                //    ExportToExcel("StockListReport.xls", dt);
                //}
                //#endregion
            }
            else
            {
                BusinessLogic bl = new BusinessLogic();
                DataSet dsLedger = bl.getProductStockList(sDataSource, itemCode, startDate, endDate);

                #region Export To Excel
                if (dsLedger.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Bill Date"));
                    dt.Columns.Add(new DataColumn("Purchase / Sale"));
                    dt.Columns.Add(new DataColumn("Bill No."));
                    dt.Columns.Add(new DataColumn("Qty."));
                    dt.Columns.Add(new DataColumn("Ledger Name"));
                    dt.Columns.Add(new DataColumn("Purchase Value"));
                    dt.Columns.Add(new DataColumn("Sales Value"));

                    foreach (DataRow dr in dsLedger.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["Bill Date"] = dr["billdate"];
                        dr_export["Purchase / Sale"] = dr["'Purchase/Sale'"];
                        dr_export["Bill No."] = dr["billno"];
                        dr_export["Qty."] = dr["qty"];
                        dr_export["Ledger Name"] = dr["LedgerName"];
                        if (dr["'Purchase/Sale'"].ToString() == "PURCHASE")
                        {
                            dr_export["Purchase Value"] = dr["rate"];
                            dr_export["Sales Value"] = "";
                        }
                        else if (dr["'Purchase/Sale'"].ToString() == "SALES")
                        {
                            dr_export["Sales Value"] = dr["rate"];
                            dr_export["Purchase Value"] = "";
                        }
                        dt.Rows.Add(dr_export);
                    }

                    DataRow dr_lastexport = dt.NewRow();
                    dr_lastexport["Bill Date"] = "";
                    dr_lastexport["Purchase / Sale"] = "";
                    dr_lastexport["Bill No."] = "";
                    dr_lastexport["Qty."] = "";
                    dr_lastexport["Ledger Name"] = "";
                    dr_lastexport["Purchase Value"] = "";
                    dr_lastexport["Sales Value"] = "";

                    ExportToExcel("StockListReport.xls", dt);
                }
                #endregion
            }
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

    protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label closeQty = (Label)e.Row.FindControl("lblClosingStock");
            double Qty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
            string flag = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "'Purchase/Sale'"));
            double clStck = 0;
            if (flag == "PURCHASE")
            {
                clStck = Qty + Convert.ToDouble(hdStock.Value);
                closeQty.Text = Convert.ToString(clStck);
                hdStock.Value = Convert.ToString(clStck);
            }
            if (flag == "SALES")
            {
                clStck = Convert.ToDouble(hdStock.Value) - Qty;
                closeQty.Text = Convert.ToString(clStck);
                hdStock.Value = Convert.ToString(clStck);
            }


            if (closeQty != null)
                lblClosingStock.Text = closeQty.Text;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
