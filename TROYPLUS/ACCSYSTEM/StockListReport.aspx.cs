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

public partial class StockListReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                loadProducts();
                divReport.Visible = false;
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                //txtEndDate.Text = DateTime.Now.ToShortDateString();

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtEndDate.Text = dtaa;

            }
            if (!IsPostBack)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);

                LoadProducts(this, null);
                loadCategories();
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

    public void ExportToExcel()
    {

        try
        {
            Response.Clear();

            Response.Buffer = true;

            string file = "Stock Ledger Report_" + DateTime.Now.ToString() + ".xls";

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

            cell2.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     Stock Ledger Report for the Model " + lblModel.Text;


            TableCell cell3 = new TableCell();

            cell3.Text = "&nbsp;";
            

            TableCell cell4 = new TableCell();

            cell4.Controls.Add(gvLedger);


            TableCell cell5 = new TableCell();

            cell5.Text = "&nbsp;";

            TableCell cell6 = new TableCell();

            cell6.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Item Name : &nbsp;&nbsp;&nbsp; " + lblItem.Text;

            TableCell cell7 = new TableCell();

            cell7.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Model &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : &nbsp;&nbsp;&nbsp;" + lblModel.Text;

            TableCell cell8 = new TableCell();

            cell8.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Opening Stock : &nbsp;&nbsp;&nbsp;" + lblOpenStock.Text;


            TableCell cell9 = new TableCell();

            cell9.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Closing Stock as per below : &nbsp;&nbsp;&nbsp;" + lblClosingStock.Text;

            TableCell cell10 = new TableCell();

            cell10.Text = "Closing stock as per product master : &nbsp;&nbsp;&nbsp;" + lblClosingStockPM.Text;



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

    public void ExportToExcelValue()
    {

        try
        {
            Response.Clear();

            Response.Buffer = true;

            string file = "Stock Ledger Report_" + DateTime.Now.ToString() + ".xls";

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

            cell2.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblCompany.Text;

            TableCell cell3 = new TableCell();

            cell3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblAddress.Text;
            
            TableCell cell4 = new TableCell();

            cell4.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblCity.Text + " - " + lblPincode.Text;

            TableCell cell5 = new TableCell();
            
            cell5.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     Stock Ledger Report for the Model " + lblModel.Text;

            TableCell cell6 = new TableCell();

            cell6.Controls.Add(gvledgerwithvalue);


            TableCell cell7 = new TableCell();

            cell7.Text = "&nbsp;";

            TableCell cell8 = new TableCell();

            cell8.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Item Name : &nbsp;&nbsp;&nbsp; " + lblItem.Text;
            
            TableCell cell9 = new TableCell();

            cell9.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Model &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : &nbsp;&nbsp;&nbsp;" + lblModel.Text;

            TableCell cell10 = new TableCell();

            cell10.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Opening Stock : &nbsp;&nbsp;&nbsp;" + lblOpenStock.Text;


            TableCell cell11 = new TableCell();

            cell11.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Closing Stock as per below : &nbsp;&nbsp;&nbsp;" + lblClosingStock.Text;

            TableCell cell12 = new TableCell();

            cell12.Text = "Closing stock as per product master : &nbsp;&nbsp;&nbsp;" + lblClosingStockPM.Text;



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


    private void loadCategories()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic();
        DataSet ds = new DataSet();
        ds = bl.ListCategory(sDataSource, "");
        cmbCategory.DataTextField = "CategoryName";
        cmbCategory.DataValueField = "CategoryID";
        cmbCategory.DataSource = ds;
        cmbCategory.DataBind();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            //cmbBrand.SelectedIndex = 0;
            //cmbCategory.Items.Clear();
            //cmbProdAdd.Items.Clear();
            //cmbProdName.Items.Clear();
            //cmbModel.Items.Clear();
            cmbCategory.SelectedIndex = 0;
            LoadProducts(this, null);

            gvLedger.DataSource = null;
            gvLedger.DataBind();

            lblModel.Text = "";
            lblOpenStock.Text = "";
            lblItem.Text = "";
            lblClosingStockPM.Text = "";
            lblClosingStock.Text = "";
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

    protected void LoadProducts(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        string CategoryID = cmbCategory.SelectedValue;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListProductsForCategoryID(CategoryID, "");
        cmbProdAdd.Items.Clear();
        cmbProdAdd.DataSource = ds;
        cmbProdAdd.Items.Insert(0, new ListItem("Select ItemCode", "0"));
        cmbProdAdd.DataTextField = "ItemCode";
        cmbProdAdd.DataValueField = "ItemCode";
        cmbProdAdd.DataBind();

        ds = bl.ListModelsForCategoryID(CategoryID, "");
        cmbModel.Items.Clear();
        cmbModel.DataSource = ds;
        cmbModel.Items.Insert(0, new ListItem("Select Model", "0"));
        cmbModel.DataTextField = "Model";
        cmbModel.DataValueField = "Model";
        cmbModel.DataBind();

        ds = bl.ListBrandsForCategoryID(CategoryID, "");
        cmbBrand.Items.Clear();
        cmbBrand.DataSource = ds;
        cmbBrand.Items.Insert(0, new ListItem("Select Brand", "0"));
        cmbBrand.DataTextField = "ProductDesc";
        cmbBrand.DataValueField = "ProductDesc";
        cmbBrand.DataBind();

        ds = bl.ListProdNameForCategoryID(CategoryID, "");
        cmbProdName.Items.Clear();
        cmbProdName.DataSource = ds;
        cmbProdName.Items.Insert(0, new ListItem("Select ItemName", "0"));
        cmbProdName.DataTextField = "ProductName";
        cmbProdName.DataValueField = "ProductName";
        cmbProdName.DataBind();

        LoadForProduct(this, null);
    }

    protected void LoadForBrand(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string brand = cmbBrand.SelectedValue;
        string CategoryID = cmbCategory.SelectedValue;
        //DataSet catData = bl.GetProductForId(sDataSource, itemCode);
        //cmbProdAdd.SelectedValue = itemCode;
        //cmbModel.SelectedValue = itemCode;
        DataSet ds = new DataSet();
        ds = bl.ListModelsForBrand(brand, CategoryID, "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbModel.Items.Clear();
            cmbModel.DataSource = ds;
            cmbModel.DataTextField = "Model";
            cmbModel.DataValueField = "Model";
            cmbModel.DataBind();
        }

        ds = bl.ListProdcutsForBrand(brand, CategoryID, "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbProdAdd.Items.Clear();
            cmbProdAdd.DataSource = ds;
            cmbProdAdd.DataTextField = "ItemCode";
            cmbProdAdd.DataValueField = "ItemCode";
            cmbProdAdd.DataBind();
        }

        ds = bl.ListProdcutNameForBrand(brand, CategoryID, "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbProdName.Items.Clear();
            cmbProdName.DataSource = ds;
            cmbProdName.DataTextField = "ProductName";
            cmbProdName.DataValueField = "ProductName";
            cmbProdName.DataBind();
        }
        cmbProdAdd_SelectedIndexChanged(this, null);

    }

    protected void LoadForModel(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string model = cmbModel.SelectedValue;
        string CategoryID = cmbCategory.SelectedValue;
        DataSet ds = new DataSet();

        ds = bl.ListProdcutsForModel(model, CategoryID, "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbProdAdd.Items.Clear();
            cmbProdAdd.DataSource = ds;
            cmbProdAdd.DataTextField = "ItemCode";
            cmbProdAdd.DataValueField = "ItemCode";
            cmbProdAdd.DataBind();
        }

        ds = bl.ListBrandsForModel(model, CategoryID, "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbBrand.Items.Clear();
            cmbBrand.DataSource = ds;
            cmbBrand.DataTextField = "ProductDesc";
            cmbBrand.DataValueField = "ProductDesc";
            cmbBrand.DataBind();
        }

        ds = bl.ListProductNameForModel(model, CategoryID, "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbProdName.Items.Clear();
            cmbProdName.DataSource = ds;
            cmbProdName.DataTextField = "ProductName";
            cmbProdName.DataValueField = "ProductName";
            cmbProdName.DataBind();
        }
        cmbProdAdd_SelectedIndexChanged(this, null);
    }

    protected void LoadForProductName(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string prodName = cmbProdName.SelectedValue;
        string CategoryID = cmbCategory.SelectedValue;
        DataSet ds = new DataSet();

        ds = bl.ListProdcutsForProductName(prodName, CategoryID, "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbProdAdd.Items.Clear();
            cmbProdAdd.DataSource = ds;
            cmbProdAdd.DataTextField = "ItemCode";
            cmbProdAdd.DataValueField = "ItemCode";
            cmbProdAdd.DataBind();
        }

        ds = bl.ListBrandsForProductName(prodName, CategoryID, "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbBrand.Items.Clear();
            cmbBrand.DataSource = ds;
            cmbBrand.DataTextField = "ProductDesc";
            cmbBrand.DataValueField = "ProductDesc";
            cmbBrand.DataBind();
        }

        ds = bl.ListModelsForProductName(prodName, CategoryID, "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbModel.Items.Clear();
            cmbModel.DataSource = ds;
            cmbModel.DataTextField = "Model";
            cmbModel.DataValueField = "Model";
            cmbModel.DataBind();
        }
        cmbProdAdd_SelectedIndexChanged(this, null);
    }

    protected void cmbProdAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();
            DataSet roleDs = new DataSet();
            string itemCode = string.Empty;
            DataSet checkDs;

            if (cmbProdAdd.SelectedItem != null)
            {
                itemCode = cmbProdAdd.SelectedItem.Value.Trim();

                DataSet catData = bl.GetProductForId(sDataSource, cmbProdAdd.SelectedItem.Value.Trim());

                if (catData != null)
                {
                    if ((catData.Tables[0].Rows[0]["Model"] != null) && (catData.Tables[0].Rows[0]["Model"].ToString() != ""))
                    {
                        if (cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()) != null)
                        {
                            cmbModel.ClearSelection();
                            if (!cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()).Selected)
                                cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()).Selected = true;

                        }
                    }

                    if ((catData.Tables[0].Rows[0]["ProductDesc"] != null) && (catData.Tables[0].Rows[0]["ProductDesc"].ToString() != ""))
                    {
                        if (cmbBrand.Items.FindByValue(catData.Tables[0].Rows[0]["ProductDesc"].ToString().Trim()) != null)
                        {
                            cmbBrand.ClearSelection();
                            if (!cmbBrand.Items.FindByValue(catData.Tables[0].Rows[0]["ProductDesc"].ToString().Trim()).Selected)
                                cmbBrand.Items.FindByValue(catData.Tables[0].Rows[0]["ProductDesc"].ToString().Trim()).Selected = true;
                        }
                    }

                    if ((catData.Tables[0].Rows[0]["ProductName"] != null) && (catData.Tables[0].Rows[0]["ProductName"].ToString() != ""))
                    {
                        if (cmbProdName.Items.FindByValue(catData.Tables[0].Rows[0]["ProductName"].ToString().Trim()) != null)
                        {
                            cmbProdName.ClearSelection();
                            if (!cmbProdName.Items.FindByValue(catData.Tables[0].Rows[0]["ProductName"].ToString().Trim()).Selected)
                                cmbProdName.Items.FindByValue(catData.Tables[0].Rows[0]["ProductName"].ToString().Trim()).Selected = true;
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

    protected void LoadForProduct(object sender, EventArgs e)
    {
        //string itemCode = cmbProdAdd.SelectedValue;
        //cmbModel.SelectedValue = itemCode;
        //cmbBrand.SelectedValue = itemCode;
        cmbProdAdd_SelectedIndexChanged(sender, e);
    }

    protected void btnExl_Click(object sender, EventArgs e)
    {
        try
        {
            double currStock;
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            lblClosingStock.Text = "0";
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            ReportsBL.ReportClass report = new ReportsBL.ReportClass();
            double openingStock = 0;
            string[] itemAr = drpLedgerName.SelectedItem.Text.Split('|');

            BusinessLogic bl = new BusinessLogic(sDataSource);

            //string itemCode = Convert.ToString(itemAr[0]).Trim();
            string itemCode = cmbProdAdd.Text;
            lblModel.Text = cmbModel.Text;
            openingStock = Convert.ToDouble(report.getOpeningStock(sDataSource, itemCode,"")) +
                (Convert.ToDouble(bl.getOpeningStockPurchase(sDataSource, itemCode, startDate)) - Convert.ToDouble(report.getOpeningStockSales(sDataSource, itemCode, startDate)));

            lblOpenStock.Text = Convert.ToString(openingStock);

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();


            currStock = bl.getStockInfo(itemCode,"");

            string[] stkArray = drpLedgerName.SelectedItem.Value.Split('@');
            lblItem.Text = cmbProdName.Text;
            lblClosingStockPM.Text = Convert.ToString(currStock);
            hdStock.Value = Convert.ToString(openingStock);


            if ((chkvalue.Checked == false) && (chktrans.Checked == false))
            {
                DataSet dsLedger = bl.getProductStockList(sDataSource, itemCode, startDate, endDate);
                gvLedger.DataSource = dsLedger;
                gvLedger.DataBind();

                divReport.Visible = false;

                ExportToExcel();
                #region Export To Excel
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
                #endregion
            }
            else if ((chktrans.Checked == true) && (chkvalue.Checked == true))
            {

                DataSet dsLedger = bl.getProductStockList(sDataSource, itemCode, startDate, endDate);
                gvledgerwithvalue.DataSource = dsLedger;
                gvledgerwithvalue.DataBind();

                divReport.Visible = false;

                ExportToExcelValue();
                #region Export To Excel
                //if (dsLedger.Tables[0].Rows.Count > 0)
                //{
                //    DataTable dt = new DataTable();
                //    dt.Columns.Add(new DataColumn("Bill Date"));
                //    dt.Columns.Add(new DataColumn("Purchase / Sale"));
                //    dt.Columns.Add(new DataColumn("Bill No."));
                //    dt.Columns.Add(new DataColumn("Qty."));
                //    dt.Columns.Add(new DataColumn("Ledger Name"));
                //    dt.Columns.Add(new DataColumn("Purchase Value"));
                //    dt.Columns.Add(new DataColumn("Sales Value"));

                //    foreach (DataRow dr in dsLedger.Tables[0].Rows)
                //    {
                //        DataRow dr_export = dt.NewRow();
                //        dr_export["Bill Date"] = dr["billdate"];
                //        dr_export["Purchase / Sale"] = dr["'Purchase/Sale'"];
                //        dr_export["Bill No."] = dr["billno"];
                //        dr_export["Qty."] = dr["qty"];
                //        dr_export["Ledger Name"] = dr["LedgerName"];
                //        if (dr["'Purchase/Sale'"].ToString() == "PURCHASE")
                //        {
                //            dr_export["Purchase Value"] = dr["rate"];
                //            dr_export["Sales Value"] = "";
                //        }
                //        else if (dr["'Purchase/Sale'"].ToString() == "SALES")
                //        {
                //            dr_export["Sales Value"] = dr["rate"];
                //            dr_export["Purchase Value"] = "";
                //        }
                //        dt.Rows.Add(dr_export);
                //    }

                //    DataRow dr_lastexport = dt.NewRow();
                //    dr_lastexport["Bill Date"] = "";
                //    dr_lastexport["Purchase / Sale"] = "";
                //    dr_lastexport["Bill No."] = "";
                //    dr_lastexport["Qty."] = "";
                //    dr_lastexport["Ledger Name"] = "";
                //    dr_lastexport["Purchase Value"] = "";
                //    dr_lastexport["Sales Value"] = "";

                //    ExportToExcel("StockListReport.xls", dt);
                //}
                #endregion
            }
            else
            {
                DataSet dsLedger = bl.getProductStockList(sDataSource, itemCode, startDate, endDate);
                gvledgerwithvalue.DataSource = dsLedger;
                gvledgerwithvalue.DataBind();

                divReport.Visible = false;

                ExportToExcelValue();
                #region Export To Excel
                //if (dsLedger.Tables[0].Rows.Count > 0)
                //{
                //    DataTable dt = new DataTable();
                //    dt.Columns.Add(new DataColumn("Bill Date"));
                //    dt.Columns.Add(new DataColumn("Purchase / Sale"));
                //    dt.Columns.Add(new DataColumn("Bill No."));
                //    dt.Columns.Add(new DataColumn("Qty."));
                //    dt.Columns.Add(new DataColumn("Ledger Name"));
                //    dt.Columns.Add(new DataColumn("Purchase Value"));
                //    dt.Columns.Add(new DataColumn("Sales Value"));

                //    foreach (DataRow dr in dsLedger.Tables[0].Rows)
                //    {
                //        DataRow dr_export = dt.NewRow();
                //        if ((dr["'Purchase/Sale'"].ToString() == "IN") || (dr["'Purchase/Sale'"].ToString() == "OUT"))
                //        {
                //            dr_export["Bill Date"] = dr["cdate"];
                //        }
                //        else
                //        {
                //            dr_export["Bill Date"] = dr["billdate"];
                //        }
                //        dr_export["Purchase / Sale"] = dr["'Purchase/Sale'"];
                //        if ((dr["'Purchase/Sale'"].ToString() == "IN") || (dr["'Purchase/Sale'"].ToString() == "OUT"))
                //        {
                //            dr_export["Bill No."] = dr["compid"];
                //        }
                //        else
                //        {
                //            dr_export["Bill No."] = dr["billno"];
                //        }
                //        dr_export["Qty."] = dr["qty"];
                //        dr_export["Ledger Name"] = dr["LedgerName"];
                //        if (dr["'Purchase/Sale'"].ToString() == "PURCHASE")
                //        {
                //            dr_export["Purchase Value"] = dr["rate"];
                //            dr_export["Sales Value"] = "";
                //        }
                //        else if (dr["'Purchase/Sale'"].ToString() == "SALES")
                //        {
                //            dr_export["Sales Value"] = dr["rate"];
                //            dr_export["Purchase Value"] = "";
                //        }
                //        dt.Rows.Add(dr_export);
                //    }

                //    DataRow dr_lastexport = dt.NewRow();
                //    dr_lastexport["Bill Date"] = "";
                //    dr_lastexport["Purchase / Sale"] = "";
                //    dr_lastexport["Bill No."] = "";
                //    dr_lastexport["Qty."] = "";
                //    dr_lastexport["Ledger Name"] = "";
                //    dr_lastexport["Purchase Value"] = "";
                //    dr_lastexport["Sales Value"] = "";

                //    ExportToExcel("StockListReport.xls", dt);
                //}
                #endregion
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
            if (cmbProdAdd.SelectedItem.Text == "Select ItemCode")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select any one ItemCode')", true);
                return;
            }

            double currStock;
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            lblClosingStock.Text = "0";
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            ReportsBL.ReportClass report = new ReportsBL.ReportClass();
            double openingStock = 0;
            string[] itemAr = drpLedgerName.SelectedItem.Text.Split('|');

            BusinessLogic bl = new BusinessLogic(sDataSource);

            //string itemCode = Convert.ToString(itemAr[0]).Trim();
            string itemCode = cmbProdAdd.Text;
            lblModel.Text = cmbModel.Text;
            openingStock = Convert.ToDouble(report.getOpeningStock(sDataSource, itemCode,"")) +
                (Convert.ToDouble(bl.getOpeningStockPurchase(sDataSource, itemCode, startDate)) - Convert.ToDouble(report.getOpeningStockSales(sDataSource, itemCode, startDate)));

            lblOpenStock.Text = Convert.ToString(openingStock);

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();


            currStock = bl.getStockInfo(itemCode,"");

            string[] stkArray = drpLedgerName.SelectedItem.Value.Split('@');
            lblItem.Text = cmbProdName.Text;
            lblClosingStockPM.Text = Convert.ToString(currStock);
            hdStock.Value = Convert.ToString(openingStock);
            string Item = cmbProdName.Text;
            string Model = cmbModel.Text;

            if ((chkvalue.Checked == false) && (chktrans.Checked == false))
            {
                //DataSet dsLedger = report.getProductStockList(sDataSource, itemCode, startDate, endDate);
                //gvLedger.DataSource = dsLedger;
                //gvLedger.DataBind();
                divReport.Visible = false;

                Response.Write("<script language='javascript'> window.open('StockListReport1.aspx?itemCode=" + itemCode + "&Model=" + Model + "&Item=" + Item + "&startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");

                //if (dsLedger.Tables[0].Rows.Count > 0)
                //{

                //}
                //else
                //{
                //    lblClosingStock.Text = "0";
                //}

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
            else if ((chktrans.Checked == true) && (chkvalue.Checked == true))
            {

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
            else
            {
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
                        if ((dr["'Purchase/Sale'"].ToString() == "IN") || (dr["'Purchase/Sale'"].ToString() == "OUT"))
                        {
                            dr_export["Bill Date"] = dr["cdate"];
                        }
                        else
                        {
                            dr_export["Bill Date"] = dr["billdate"];
                        }
                        dr_export["Purchase / Sale"] = dr["'Purchase/Sale'"];
                        if ((dr["'Purchase/Sale'"].ToString() == "IN") || (dr["'Purchase/Sale'"].ToString() == "OUT"))
                        {
                            dr_export["Bill No."] = dr["compid"];
                        }
                        else
                        {
                            dr_export["Bill No."] = dr["billno"];
                        }
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

    protected void gvledgerwithvalue_RowDataBound(object sender, GridViewRowEventArgs e)
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
