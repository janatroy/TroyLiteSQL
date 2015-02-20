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

public partial class PurchaseSummaryReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    double dSNetRate = 0;
    double dSVatRate = 0;

    double dSCSTRate = 0;
    double dSFrRate = 0;
    double dSLURate = 0;
    double dSGrandRate = 0;
    double dSDiscountRate = 0;

    double dSCDiscountRate = 0;
    double dSCNetRate = 0;
    double dSQty = 0;
    double dSCVatRate = 0;

    double dSCCSTRate = 0;
    double dSCFrRate = 0;
    double dSCLURate = 0;
    double dSCGrandRate = 0;
    int tempBillno = 0;
    string strBillno = string.Empty;
    int cnt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!Page.IsPostBack)
            {
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                //txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtEndDate.Text = dtaa;

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

    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            bindData();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            //string filename = "Sales Report.xls";
            string filename = "Purchase Summary" + DateTime.Now.ToString() + ".xls";
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

    public void bindData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandTotal = 0;
        double producttot = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double netTotal = 0;
        double vatTotal = 0;
        double discountTotal = 0;
        double cstTotal = 0;
        double freightTotal = 0;
        double loadTotal = 0;

        double qtyTotal = 0;
        double qty1Total = 0;

        double rate1Total = 0;
        double net1Total = 0;
        double vat1Total = 0;
        double discount1Total = 0;
        double cst1Total = 0;
        double freight1Total = 0;
        double load1Total = 0;

        string GroupBy = string.Empty;
        string field2 = string.Empty;

        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        string Types = string.Empty;
        string options = string.Empty;

        //objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        if ((cmbDisplayCat.SelectedItem.Text) == "Daywise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblpurchase.billdate,";
            GroupBy = "tblpurchase.billdate,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Categorywise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblcategories.categoryname,";
            GroupBy = "tblcategories.categoryname,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Brandwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblproductmaster.productdesc,";
            GroupBy = "tblproductmaster.productdesc,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Modelwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblproductmaster.model,";
            GroupBy = "tblproductmaster.model,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Billwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblpurchase.billno,";
            GroupBy = "tblpurchase.billno,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Supplierwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblledger.ledgername,";
            GroupBy = "tblledger.ledgername,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Itemwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblproductmaster.productname,";
            GroupBy = "tblproductmaster.productname,";
        }


        if ((cmbDisplayItem.SelectedItem.Text) == "Brandwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayItem.SelectedItem.Text));
            field2 = field2 + "tblproductmaster.productdesc,";
            GroupBy = GroupBy + "tblproductmaster.productdesc";
        }
        else if ((cmbDisplayItem.SelectedItem.Text) == "Modelwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayItem.SelectedItem.Text));
            field2 = field2 + "tblproductmaster.model,";
            GroupBy = GroupBy + "tblproductmaster.model";
        }
        else if ((cmbDisplayItem.SelectedItem.Text) == "Billwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayItem.SelectedItem.Text));
            field2 = field2 + "tblpurchase.billno,";
            GroupBy = GroupBy + "tblpurchase.billno";
        }
        else if ((cmbDisplayItem.SelectedItem.Text) == "Supplierwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayItem.SelectedItem.Text));
            field2 = field2 + "tblledger.ledgername,";
            GroupBy = GroupBy + "tblledger.ledgername";
        }
        else if ((cmbDisplayItem.SelectedItem.Text) == "Itemwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayItem.SelectedItem.Text));
            field2 = field2 + "tblproductmaster.productname,";
            GroupBy = GroupBy + "tblproductmaster.productname";
        }

        dt.Columns.Add(new DataColumn("Sales Rate"));
        dt.Columns.Add(new DataColumn("Qty"));
        dt.Columns.Add(new DataColumn("Net Rate"));
        dt.Columns.Add(new DataColumn("Discount Rate"));
        dt.Columns.Add(new DataColumn("Vat Rate"));
        dt.Columns.Add(new DataColumn("CST Rate"));
        dt.Columns.Add(new DataColumn("Freight"));
        dt.Columns.Add(new DataColumn("Loading/Unloading"));
        dt.Columns.Add(new DataColumn("Total"));


        double dNetRate = 0;
        double dVatRate = 0;
        double dDisRate = 0;
        double dCSTRate = 0;
        double dFrRate = 0;
        double dLURate = 0;
        double dGrandRate = 0;
        double dDiscountRate = 0;
        lblErr.Text = "";
        string category = string.Empty;
        category = Convert.ToString(cmbDisplayCat.SelectedItem.Text);
        var secondLevel = cmbDisplayItem.SelectedItem.Text.Trim();

        string intTrans = "";
        string salesRet = "";
        string delNote = "";

        if (chkIntTrans.Checked)
            intTrans = "YES";
        else
            intTrans = "NO";

        if (chkSalesReturn.Checked)
            salesRet = "YES";
        else
            salesRet = "NO";

        if (chkDeliveryNote.Checked)
            delNote = "YES";
        else
            delNote = "NO";


        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        BusinessLogic bl = new BusinessLogic(sDataSource);
        
        //ds = bl.SecondLevel(field2, Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote, GroupBy);


        ds = bl.SecondLevelPurchase(field2, Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote, GroupBy);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((cmbDisplayCat.SelectedItem.Text) == "Daywise")
                    {
                        fLvlValueTemp = dr["billdate"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Categorywise")
                    {
                        fLvlValueTemp = dr["Categoryname"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Brandwise")
                    {
                        fLvlValueTemp = dr["productdesc"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Modelwise")
                    {
                        fLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Billwise")
                    {
                        fLvlValueTemp = dr["Billno"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Supplierwise")
                    {
                        fLvlValueTemp = dr["ledgername"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Itemwise")
                    {
                        fLvlValueTemp = dr["productname"].ToString().ToUpper().Trim();
                    }


                    if ((cmbDisplayItem.SelectedItem.Text) == "Brandwise")
                    {
                        sLvlValueTemp = dr["productdesc"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayItem.SelectedItem.Text) == "Modelwise")
                    {
                        sLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayItem.SelectedItem.Text) == "Billwise")
                    {
                        sLvlValueTemp = dr["Billno"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayItem.SelectedItem.Text) == "Supplierwise")
                    {
                        sLvlValueTemp = dr["ledgername"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayItem.SelectedItem.Text) == "Itemwise")
                    {
                        sLvlValueTemp = dr["productname"].ToString().ToUpper().Trim();
                    }


                    //if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    //(sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    //{
                    //    DataRow dr_final889 = dt.NewRow();
                    //    dt.Rows.Add(dr_final889);

                    //    DataRow dr_final8 = dt.NewRow();

                    //    dr_final8[cmbDisplayCat.SelectedItem.Text] = "";
                    //    dr_final8[cmbDisplayItem.SelectedItem.Text] = "Total : " + sLvlValue;
                    //    dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));
                    //    dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));
                    //    dr_final8["Net Rate"] = (Convert.ToDouble(netTotal));
                    //    dr_final8["Discount Rate"] = (Convert.ToDouble(discountTotal));
                    //    dr_final8["Vat Rate"] = (Convert.ToDouble(vatTotal));
                    //    dr_final8["CST Rate"] = (Convert.ToDouble(cstTotal));
                    //    dr_final8["Freight"] = (Convert.ToDouble(freightTotal));
                    //    dr_final8["Loading/Unloading"] = (Convert.ToDouble(loadTotal));
                    //    dr_final8["Total"] = Convert.ToString(Convert.ToDecimal(producttot));

                    //    producttot = 0;
                    //    qtyTotal = 0;
                    //    netTotal = 0;
                    //    discountTotal = 0;
                    //    vatTotal = 0;
                    //    cstTotal = 0;
                    //    freightTotal = 0;
                    //    rateTotal = 0;
                    //    loadTotal = 0;
                    //    dt.Rows.Add(dr_final8);

                    //    DataRow dr_final888 = dt.NewRow();
                    //    dt.Rows.Add(dr_final888);
                    //}

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final879 = dt.NewRow();
                        dt.Rows.Add(dr_final879);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8[cmbDisplayCat.SelectedItem.Text] = "Total : " + fLvlValue;
                        dr_final8[cmbDisplayItem.SelectedItem.Text] = "";

                        dr_final8["Sales Rate"] = (Convert.ToDouble(rate1Total));
                        dr_final8["Qty"] = Convert.ToDouble(qty1Total);
                        dr_final8["Net Rate"] = (Convert.ToDouble(net1Total));
                        dr_final8["Discount Rate"] = (Convert.ToDouble(discount1Total));
                        dr_final8["Vat Rate"] = (Convert.ToDouble(vat1Total));
                        dr_final8["CST Rate"] = (Convert.ToDouble(cst1Total));
                        dr_final8["Freight"] = (Convert.ToDouble(freight1Total));
                        dr_final8["Loading/Unloading"] = (Convert.ToDouble(load1Total));
                        dr_final8["Total"] = Convert.ToDouble(brandTotal);



                        brandTotal = 0;
                        rate1Total = 0;
                        net1Total = 0;
                        vat1Total = 0;
                        discount1Total = 0;
                        cst1Total = 0;
                        freight1Total = 0;
                        load1Total = 0;
                        qty1Total = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12[cmbDisplayCat.SelectedItem.Text] = fLvlValueTemp;
                    dr_final12[cmbDisplayItem.SelectedItem.Text] = sLvlValueTemp;

                    dr_final12["Sales Rate"] = (Convert.ToDouble(dr["PRate"]));
                    dr_final12["Qty"] = Convert.ToDouble(dr["quantity"]);
                    dr_final12["Net Rate"] = (Convert.ToDouble(dr["NetPurchaseRate"]));
                    dr_final12["Discount Rate"] = (Convert.ToDouble(dr["actualdiscount"]));
                    dr_final12["Vat Rate"] = (Convert.ToDouble(dr["ActualVat"]));
                    dr_final12["CST Rate"] = (Convert.ToDouble(dr["Actualcst"]));
                    dr_final12["Freight"] = (Convert.ToDouble(dr["sumfreight"]));
                    dr_final12["Loading/Unloading"] = (Convert.ToDouble(dr["Loading"]));
                    dr_final12["Total"] = (Convert.ToDouble(dr["NetPurchaseRate"])) + (Convert.ToDouble(dr["ActualVat"])) + (Convert.ToDouble(dr["Actualcst"])) - (Convert.ToDouble(dr["actualdiscount"])) + Convert.ToDouble(dr["Loading"]) + (Convert.ToDouble(dr["sumfreight"]));

                    brandTotal = brandTotal + ((Convert.ToDouble(dr["NetPurchaseRate"])) + (Convert.ToDouble(dr["ActualVat"])) + (Convert.ToDouble(dr["Actualcst"])) - (Convert.ToDouble(dr["actualdiscount"])) + Convert.ToDouble(dr["Loading"]) + (Convert.ToDouble(dr["sumfreight"])));
                    load1Total = load1Total + (Convert.ToDouble(dr["Loading"]));
                    freight1Total = freight1Total + (Convert.ToDouble(dr["SumFreight"]));
                    cst1Total = cst1Total + (Convert.ToDouble(dr["Actualcst"]));
                    vat1Total = vat1Total + (Convert.ToDouble(dr["ActualVat"]));
                    discount1Total = discount1Total + (Convert.ToDouble(dr["actualdiscount"]));
                    net1Total = net1Total + (Convert.ToDouble(dr["NetPurchaseRate"]));
                    rate1Total = rate1Total + (Convert.ToDouble(dr["pRate"]));

                    loadTotal = loadTotal + (Convert.ToDouble(dr["Loading"]));
                    freightTotal = freightTotal + (Convert.ToDouble(dr["sumfreight"]));
                    cstTotal = cstTotal + (Convert.ToDouble(dr["Actualcst"]));
                    vatTotal = vatTotal + (Convert.ToDouble(dr["ActualVat"]));
                    discountTotal = discountTotal + (Convert.ToDouble(dr["actualdiscount"]));
                    netTotal = netTotal + (Convert.ToDouble(dr["NetPurchaseRate"]));
                    rateTotal = rateTotal + (Convert.ToDouble(dr["pRate"]));

                    CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["quantity"]);
                    total = total + ((Convert.ToDouble(dr["NetPurchaseRate"])) + (Convert.ToDouble(dr["ActualVat"])) + (Convert.ToDouble(dr["Actualcst"])) - (Convert.ToDouble(dr["actualdiscount"])) + Convert.ToDouble(dr["Loading"]) + (Convert.ToDouble(dr["sumfreight"])));
                    qty1Total = qty1Total + Convert.ToDouble(dr["quantity"]);


                    dt.Rows.Add(dr_final12);
                }
            }

            //DataRow dr_final879 = dt.NewRow();
            //dt.Rows.Add(dr_final879);

            //DataRow dr_final89 = dt.NewRow();
            //dr_final89[cmbDisplayCat.SelectedItem.Text] = "";
            //dr_final89[cmbDisplayItem.SelectedItem.Text] = "Total : " + sLvlValue;

            //dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));
            //dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));
            //dr_final89["Net Rate"] = (Convert.ToDouble(netTotal));
            //dr_final89["Discount Rate"] = (Convert.ToDouble(discountTotal));
            //dr_final89["Vat Rate"] = (Convert.ToDouble(vatTotal));
            //dr_final89["CST Rate"] = (Convert.ToDouble(cstTotal));
            //dr_final89["Freight"] = (Convert.ToDouble(freightTotal));
            //dr_final89["Loading/Unloading"] = (Convert.ToDouble(loadTotal));
            //dr_final89["Total"] = Convert.ToString(Convert.ToDecimal(producttot));

            //producttot = 0;
            //qtyTotal = 0;
            //netTotal = 0;
            //discountTotal = 0;
            //vatTotal = 0;
            //cstTotal = 0;
            //freightTotal = 0;
            //rateTotal = 0;
            //loadTotal = 0;

            //dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799[cmbDisplayCat.SelectedItem.Text] = "Total : " + fLvlValue;
            dr_final8799[cmbDisplayItem.SelectedItem.Text] = "";

            dr_final8799["Sales Rate"] = (Convert.ToDouble(rate1Total));
            dr_final8799["Qty"] = Convert.ToDouble(qty1Total);
            dr_final8799["Net Rate"] = (Convert.ToDouble(net1Total));
            dr_final8799["Discount Rate"] = (Convert.ToDouble(discount1Total));
            dr_final8799["Vat Rate"] = (Convert.ToDouble(vat1Total));
            dr_final8799["CST Rate"] = (Convert.ToDouble(cst1Total));
            dr_final8799["Freight"] = (Convert.ToDouble(freight1Total));
            dr_final8799["Loading/Unloading"] = (Convert.ToDouble(load1Total));
            dr_final8799["Total"] = Convert.ToDouble(brandTotal);


            brandTotal = 0;
            rate1Total = 0;
            net1Total = 0;
            vat1Total = 0;
            discount1Total = 0;
            cst1Total = 0;
            freight1Total = 0;
            load1Total = 0;
            qty1Total = 0;
            rateTot = 0;
            qty1Total = 0;

            brandTotal = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789[cmbDisplayCat.SelectedItem.Text] = "Grand Total : ";
            dr_final789[cmbDisplayItem.SelectedItem.Text] = "";

            dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal));
            dr_final789["Qty"] = Convert.ToDouble(CategoryqtyTotal);
            dr_final789["Net Rate"] = (Convert.ToDouble(netTotal));
            dr_final789["Discount Rate"] = (Convert.ToDouble(discountTotal));
            dr_final789["Vat Rate"] = (Convert.ToDouble(vatTotal));
            dr_final789["CST Rate"] = (Convert.ToDouble(cstTotal));
            dr_final789["Freight"] = (Convert.ToDouble(freightTotal));
            dr_final789["Loading/Unloading"] = (Convert.ToDouble(loadTotal));
            dr_final789["Total"] = Convert.ToDouble(total);

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DateTime startDate, endDate;
                string category = string.Empty;

                string intTrans = "";
                string salesRet = "";
                string delNote = "";

                if (chkIntTrans.Checked)
                    intTrans = "YES";
                else
                    intTrans = "NO";

                if (chkSalesReturn.Checked)
                    salesRet = "YES";
                else
                    salesRet = "NO";

                if (chkDeliveryNote.Checked)
                    delNote = "YES";
                else
                    delNote = "NO";

                startDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                endDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                category = Convert.ToString(cmbDisplayCat.SelectedItem.Text);
                DataSet BillDs = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                var secondLevel = cmbDisplayItem.SelectedItem.Text.Trim();

                if (category == "Daywise")
                {
                    BillDs = bl.FirstLevelDaywisePurchase(startDate, endDate, salesRet, intTrans, delNote);
                }
                else if (category == "Categorywise")
                {
                    BillDs = bl.FirstLevelCategorywisePurchase(startDate, endDate, salesRet, intTrans, delNote);
                }
                else if (category == "Brandwise")
                {
                    BillDs = bl.FirstLevelBrandwisePurchase(startDate, endDate, salesRet, intTrans, delNote);
                }
                else if (category == "Modelwise")
                {
                    BillDs = bl.FirstLevelModelwisePurchase(startDate, endDate, salesRet, intTrans, delNote);
                }
                else if (category == "Billwise")
                {
                    BillDs = bl.FirstLevelBillwisePurchase(startDate, endDate, salesRet, intTrans, delNote);
                }
                else if (category == "Supplierwise")
                {
                    BillDs = bl.FirstLevelCustomerwisePurchase(startDate, endDate, salesRet, intTrans, delNote);
                }
                /*Start Itemwise*/
                else if (category == "Itemwise")
                {
                    BillDs = bl.FirstLevelItemwisePurchase(startDate, endDate, salesRet, intTrans, delNote);


                }
                /*End Itemwise*/

                //gvMain.DataSource = BillDs;
                //gvMain.DataBind();
                divPrint.Visible = false;
                divmain.Visible = false;
                lblErr.Text = "";

                div1.Visible = true;

                Response.Write("<script language='javascript'> window.open('PurchaseSummaryReport1.aspx?category=" + category + "&secondLevel=" + secondLevel + "&salesRet=" + salesRet + "&intTrans=" + intTrans + "&delNote=" + delNote + "&startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    public void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dNetRate = 0;
            double dVatRate = 0;
            double dDisRate = 0;
            double dCSTRate = 0;
            double dFrRate = 0;
            double dLURate = 0;
            double dGrandRate = 0;
            double dDiscountRate = 0;
            lblErr.Text = "";
            string category = string.Empty;
            category = Convert.ToString(cmbDisplayCat.SelectedItem.Text);
            var secondLevel = cmbDisplayItem.SelectedItem.Text.Trim();

            string intTrans = "";
            string salesRet = "";
            string delNote = "";

            if (chkIntTrans.Checked)
                intTrans = "YES";
            else
                intTrans = "NO";

            if (chkSalesReturn.Checked)
                salesRet = "YES";
            else
                salesRet = "NO";

            if (chkDeliveryNote.Checked)
                delNote = "YES";
            else
                delNote = "NO";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "NetPurchaseRate") != DBNull.Value)
                    dNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetPurchaseRate"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualVat") != DBNull.Value)
                    dVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualVat"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualDiscount") != DBNull.Value)
                    dDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "SalesDiscount") != DBNull.Value)
                    dDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualCST") != DBNull.Value)
                    dCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));
                /* Start March 17 */
                if (DataBinder.Eval(e.Row.DataItem, "SumFreight") != DBNull.Value)
                    dFrRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumFreight"));
                if (DataBinder.Eval(e.Row.DataItem, "Loading") != DBNull.Value)
                    dLURate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Loading"));

                /* End March 17 */


                dGrandRate = dDiscountRate + dVatRate + dCSTRate; // +dFrRate + dLURate;


                dSNetRate = dSNetRate + dNetRate;
                dSVatRate = dSVatRate + dVatRate;
                dSDiscountRate = dSDiscountRate + dDisRate;
                dSCSTRate = dSCSTRate + dCSTRate;



                GridView gv = e.Row.FindControl("gvSecond") as GridView;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                Label lblLink = (Label)e.Row.FindControl("lblLink");

                DataSet ds = new DataSet();
                if (category == "Daywise")
                {
                    DateTime startDate;
                    startDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "LinkName"));
                    //ds = bl.SecondLevelDaywisePurchase(startDate, salesRet, intTrans, delNote);

                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelDaywiseBillWisePurchase(startDate, salesRet, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelDaywiseModelWisePurchase(startDate, salesRet, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelDaywiseBrandWisePurchase(startDate, salesRet, intTrans, delNote);
                    else if (secondLevel == "Supplierwise")
                        ds = bl.SecondLevelDaywiseCustWisePurchase(startDate, salesRet, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelDaywiseItemWisePurchase(startDate, salesRet, intTrans, delNote);
                    //else if (secondLevel == "Daywise")
                    //    ds = bl.SecondLevelDaywiseDayWise(startDate, salesRet, intTrans, delNote);

                    lblLink.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName", "{0:dd/MM/yyyy}"));
                }
                else if (category == "Categorywise")
                {
                    //ds = bl.SecondLevelCategorywisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    
                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelCategorywiseBillWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelCategorywiseModelWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelCategorywiseBrandWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    else if (secondLevel == "Supplierwise")
                        ds = bl.SecondLevelCategorywiseCustWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelCategorywiseItemWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    //else if (secondLevel == "Daywise")
                    //    ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Category", "BillDate", salesRet, intTrans, delNote);
                }
                else if (category == "Brandwise")
                {
                    //ds = bl.SecondLevelBrandwisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    
                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelBrandwiseBillWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelBrandwiseModelWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelBrandwiseBrandWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    else if (secondLevel == "Supplierwise")
                        ds = bl.SecondLevelBrandWiseCustomerWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductDesc", "CustomerName", salesRet, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelBrandWiseItemWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductDesc", "ProductName", salesRet, intTrans, delNote);
                    //else if (secondLevel == "Daywise")
                    //    ds = bl.SecondLevelBrandWiseDayWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "ProductDesc", "BillDate", salesRet, intTrans, delNote);
                }
                else if (category == "Modelwise")
                {
                    //ds = bl.SecondLevelModelwisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);

                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelModelwiseBillWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelModelwiseModelWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelModelwiseBrandWisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);
                    else if (secondLevel == "Supplierwise")
                        ds = bl.SecondLevelGeneralProductWisePur(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "Model", "ledgername", salesRet, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelModelWiseItemWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "Model", "ProductName", salesRet, intTrans, delNote);
                    //else if (secondLevel == "Daywise")
                    //    ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Model", "BillDate", salesRet, intTrans, delNote);
                }
                else if (category == "Billwise")
                {
                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelGeneralSalesPurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "BillNo", salesRet, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelGeneralSalesPurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "Model", salesRet, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelGeneralSalesPurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "ProductDesc", salesRet, intTrans, delNote);
                    else if (secondLevel == "Supplierwise")
                        ds = bl.SecondLevelGeneralSalesPurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "ledgername", salesRet, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelGeneralSalesPurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "ProductName", salesRet, intTrans, delNote);
                    //else if (secondLevel == "Daywise")
                    //    ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "BillNo", "BillDate", salesRet, intTrans, delNote);


                    //ds = bl.SecondLevelBillwisePurchase(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LinkName")), salesRet, intTrans, delNote);
                }
                else if (category == "Supplierwise")
                {
                    //ds = bl.SecondLevelCustomerwisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);

                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelGeneralProductWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "ledgername", "BillNo", salesRet, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelGeneralProductWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "ledgername", "Model", salesRet, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelGeneralProductWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "ledgername", "ProductDesc", salesRet, intTrans, delNote);
                    else if (secondLevel == "Supplierwise")
                        ds = bl.SecondLevelGeneralProductWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "ledgername", "ledgername", salesRet, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelGeneralProductWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "ledgername", "ProductName", salesRet, intTrans, delNote);
                    //else if (secondLevel == "Daywise")
                    //    ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "ledgername", "BillDate", salesRet, intTrans, delNote);
                }
                /*Start Itemwise*/
                else if (category == "Itemwise")
                {
                    //ds = bl.SecondLevelItemwisePurchase(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), salesRet, intTrans, delNote);

                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelGeneralSalesItemWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "itemcode", "BillNo", salesRet, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelGeneralSalesItemWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "itemcode", "Model", salesRet, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelGeneralSalesItemWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "itemcode", "ProductDesc", salesRet, intTrans, delNote);
                    else if (secondLevel == "Supplierwise")
                        ds = bl.SecondLevelGeneralSalesItemWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "itemcode", "ledgername", salesRet, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelGeneralSalesItemWisePurchase(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "itemcode", "ProductName", salesRet, intTrans, delNote);
                    //else if (secondLevel == "Daywise")
                    //    ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "BillDate", salesRet, intTrans, delNote);

                    var tempRow = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray;
                    Label lblProdName = (Label)e.Row.FindControl("lblProductName");
                    lblProdName.Text = " | " + DataBinder.Eval(e.Row.DataItem, "ProductName").ToString();

                }
                /*End Itemwise*/

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        gv.DataBind();
                    }
                }


                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                /*Start March 17 */
                Label lblFreightRate = (Label)e.Row.FindControl("lblFreightRate");
                Label lblLURate = (Label)e.Row.FindControl("lblLURate");
                lblFreightRate.Text = dSCFrRate.ToString("f2");
                lblLURate.Text = dSCLURate.ToString("f2");
                dSFrRate = dSFrRate + dSCFrRate;
                dSLURate = dSLURate + dSCLURate;
                dGrandRate = dGrandRate + dSCFrRate + dSCLURate;
                /*end March 17 */


                dSGrandRate = dSGrandRate + dGrandRate;
                lblTotal.Text = dGrandRate.ToString("f2");
                dGrandRate = 0;
                dSCFrRate = 0;
                dSCLURate = 0;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                /*Start March 17 */
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                /*End March 17 */
                e.Row.Cells[1].Text = dSNetRate.ToString("f2");
                e.Row.Cells[2].Text = dSDiscountRate.ToString("f2");
                e.Row.Cells[3].Text = dSVatRate.ToString("f2");
                e.Row.Cells[4].Text = dSCSTRate.ToString("f2");
                /*Start March 17 */
                e.Row.Cells[5].Text = dSFrRate.ToString("f2");
                e.Row.Cells[6].Text = dSLURate.ToString("f2");
                e.Row.Cells[7].Text = dSGrandRate.ToString("f2");
                /*End March 17 */
                strBillno = "";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void gvSecond_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dNetRate = 0;
            double dVatRate = 0;
            double dDisRate = 0;
            double dQuantity = 0;
            double dCSTRate = 0;
            double dFrRate = 0;
            double dLURate = 0;
            double dGrandRate = 0;
            double dDiscountRate = 0;

            lblErr.Text = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Quantity") != DBNull.Value)
                    dQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                if (DataBinder.Eval(e.Row.DataItem, "NetPurchaseRate") != DBNull.Value)
                    dNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetPurchaseRate"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualVat") != DBNull.Value)
                    dVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualVat"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualDiscount") != DBNull.Value)
                    dDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "SalesDiscount") != DBNull.Value)
                    dDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualCST") != DBNull.Value)
                    dCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));


                Label lblTotal = (Label)e.Row.FindControl("lblTotal");

                /*Start March 17*/
                Label lblFreightRate = (Label)e.Row.FindControl("lblFreightRate");
                Label lblLURate = (Label)e.Row.FindControl("lblLURate");

                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PurchaseID")) == tempBillno)
                {
                    lblFreightRate.Visible = false;
                    lblLURate.Visible = false;

                }
                else
                {
                    strBillno = strBillno + tempBillno + ",";
                    string delim = ",";
                    char[] delimA = delim.ToCharArray();
                    string[] arr = strBillno.Split(delimA);
                    int chkcnt = 0;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i].ToString() != "")
                        {
                            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PurchaseID")) != Convert.ToInt32(arr[i]))
                            {
                                chkcnt = 0;
                            }
                            else
                            {
                                chkcnt = chkcnt + 1;
                                break;
                            }
                        }
                    }
                    if (chkcnt == 0)
                    {
                        if (lblFreightRate.Text.Trim() != "")
                            dSCFrRate = dSCFrRate + Convert.ToDouble(lblFreightRate.Text.Trim());
                        if (lblLURate.Text.Trim() != "")
                            dSCLURate = dSCLURate + Convert.ToDouble(lblLURate.Text.Trim());
                    }
                    lblFreightRate.Visible = true;
                    lblLURate.Visible = true;
                    tempBillno = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PurchaseID"));
                }



                /*End March 17*/


                dGrandRate = dDiscountRate + dVatRate + dCSTRate; // +dFrRate + dLURate;
                dSQty = dSQty + dQuantity;
                dSCNetRate = dSCNetRate + dNetRate;
                dSCVatRate = dSCVatRate + dVatRate;
                dSCDiscountRate = dSCDiscountRate + dDisRate;
                dSCCSTRate = dSCCSTRate + dCSTRate;


                dSCGrandRate = dSCGrandRate + dGrandRate;
                lblTotal.Text = dGrandRate.ToString("f2");
                /*start March 17*/
                dGrandRate = 0;
                /*End March 17 */
                //e.Row.Cells[6].Visible = false;
                //e.Row.Cells[7].Visible = false;

                cnt = cnt + 1;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {


                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                /*start March 17*/
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                /*End March 17*/
                //dSCGrandRate = dSCGrandRate + dSCFrRate + dSCLURate;
                e.Row.Cells[4].Text = dSQty.ToString("f2");
                e.Row.Cells[5].Text = dSCNetRate.ToString("f2");
                e.Row.Cells[6].Text = dSCDiscountRate.ToString("f2");
                e.Row.Cells[7].Text = dSCVatRate.ToString("f2");
                e.Row.Cells[8].Text = dSCCSTRate.ToString("f2");
                /*start March 17*/
                e.Row.Cells[9].Text = dSCFrRate.ToString("f2");
                e.Row.Cells[10].Text = dSCLURate.ToString("f2");
                e.Row.Cells[11].Text = dSCGrandRate.ToString("f2");
                /*End March 17*/

                dSCDiscountRate = 0;
                dSCNetRate = 0;
                dSCVatRate = 0;
                dSQty = 0;
                dSCCSTRate = 0;
                //dSCFrRate = 0;
                //dSCLURate = 0;
                dSCGrandRate = 0;


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
