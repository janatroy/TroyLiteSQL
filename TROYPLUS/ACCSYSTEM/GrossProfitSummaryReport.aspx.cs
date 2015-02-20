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

public partial class GetPurchaseRate : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    double dSNetRate = 0;
    double dSQty = 0;
    double dSVatRate = 0;
    double dSCSTRate = 0;
    double dSFrRate = 0;
    double dSLURate = 0;
    double dSGrandRate = 0;
    double dSDiscountRate = 0;
    double dSPNetRate = 0;
    double dSPVatRate = 0;
    double dSPCSTRate = 0;
    double dSPGrandRate = 0;
    double dSPDiscountRate = 0;
    double dSPCNetRate = 0;
    double dSPCVatRate = 0;
    double dSPCCSTRate = 0;
    double dSPCGrandRate = 0;
    double dSPCDiscountRate = 0;
    double dSPLURate = 0;
    double dSPFrRate = 0;
    double dSCDiscountRate = 0;
    double dSCNetRate = 0;
    double dSCVatRate = 0;
    double dSGrossProfit = 0;
    double dSCGrossProfit = 0;
    double dSCCSTRate = 0;
    double dSCFrRate = 0;
    double dSCLURate = 0;
    double dSPCFrRate = 0;
    double dSPCLURate = 0;
    double dSCGrandRate = 0;
    int tempBillno = 0;
    string strBillno = string.Empty;
    int cnt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
                //gvMain.DataSource = ds;
                //gvMain.DataBind();
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
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
            if (Page.IsValid)
            {

                pnlContent.Visible = true;
                DataSet dsDummy = GetPurchaseForSales();

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DateTime startDate, endDate;
                string category = string.Empty;
                lblMessage.Text = "";
                startDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                endDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                category = Convert.ToString(cmbDisplayCat.SelectedItem.Text);
                DataSet BillDs = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (category == "Daywise")
                {
                    BillDs = bl.FirstLevelGrossReportDaywise(startDate, endDate);

                }
                else if (category == "Categorywise")
                {
                    BillDs = bl.FirstLevelGrossReportCategorywise(startDate, endDate);
                }
                else if (category == "Brandwise")
                {
                    BillDs = bl.FirstLevelGrossReportBrandwise(startDate, endDate);
                }
                else if (category == "Modelwise")
                {
                    BillDs = bl.FirstLevelGrossReportModelwise(startDate, endDate);
                }
                else if (category == "Billwise")
                {
                    BillDs = bl.FirstLevelGrossReportBillwise(startDate, endDate);
                }
                else if (category == "Customerwise")
                {
                    BillDs = bl.FirstLevelGrossReportCustomerwise(startDate, endDate);
                }
                else if (category == "Executivewise")
                {
                    BillDs = bl.FirstLevelGrossReportExecutivewise(startDate, endDate);
                }
                /*Start Itemwise*/
                else if (category == "Itemwise")
                {
                    BillDs = bl.FirstLevelGrossReportItemwise(startDate, endDate);


                }
                /*End Itemwise*/
                if (BillDs != null)
                {
                    if (BillDs.Tables[0].Rows.Count > 0)
                    {
                        gvMain.DataSource = BillDs;
                        gvMain.DataBind();

                    }
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public DataSet GetPurchaseForSales()
    {
        double salesQty = 0;
        string salesItemCode = string.Empty;
        double salesRate = 0;
        double salesDiscount = 0;
        double salesVat = 0;
        double salesCst = 0;
        double salesLoading = 0;
        double salesFreight = 0;
        string salesBillDate = string.Empty;
        string salesExecutive = string.Empty;

        string salesCustomer = string.Empty;

        int purchaseCnt = 0;
        DataRow[] SearchProductRow;
        DataRow[] LFRrow;
        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string company = string.Empty;
        DataSet salesDs = bl.GetAllSales();
        DataSet productDs = new DataSet();
        DataSet dsCharges = new DataSet();
        DataSet purchaseDs = new DataSet();

        bl.DeleteGP();

        if (rdoRate.SelectedValue != "NLP")
            purchaseDs = bl.GetAllPurchase();
        else
            purchaseDs = bl.GetAllPurchaseForNLP();

        DataSet openingStockDs = bl.GetAllOpeningStock();
        productDs = bl.GetProductGPForId();

        if (rdoRate.SelectedValue != "NLP")
            dsCharges = bl.GetPurchaseChargesTotal();
        else
            dsCharges = bl.GetPurchaseChargesTotalForNLP();

        int opCnt = 0;
        double opQty = 0;
        double purchaseQty = 0;
        DataSet GrossProfitDs = new DataSet();
        DataTable dt;
        DataRow dr;
        DataColumn dc;

        try
        {
            dt = new DataTable();

            dc = new DataColumn("SalesBillno");
            dt.Columns.Add(dc);

            dc = new DataColumn("SalesBillDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemCode");
            dt.Columns.Add(dc);

            dc = new DataColumn("SalesRate");
            dt.Columns.Add(dc);

            dc = new DataColumn("SalesQty");
            dt.Columns.Add(dc);

            dc = new DataColumn("SalesDiscount");
            dt.Columns.Add(dc);

            dc = new DataColumn("SalesVat");
            dt.Columns.Add(dc);

            dc = new DataColumn("SalesCst");
            dt.Columns.Add(dc);

            dc = new DataColumn("SalesLoading");
            dt.Columns.Add(dc);

            dc = new DataColumn("SalesFreight");
            dt.Columns.Add(dc);

            //dc = new DataColumn("CustomerID");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("Customername");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("Executive");
            //dt.Columns.Add(dc);

            dc = new DataColumn("PurchaseID");
            dt.Columns.Add(dc);
            dc = new DataColumn("PurchaseBillDate");
            dt.Columns.Add(dc);
            dc = new DataColumn("PurchaseRate");
            dt.Columns.Add(dc);

            dc = new DataColumn("PurchaseVat");
            dt.Columns.Add(dc);
            dc = new DataColumn("PurchaseCst");
            dt.Columns.Add(dc);
            dc = new DataColumn("PurchaseDiscount");
            dt.Columns.Add(dc);
            dc = new DataColumn("PurchaseLoading");
            dt.Columns.Add(dc);

            dc = new DataColumn("PurchaseFreight");
            dt.Columns.Add(dc);


            GrossProfitDs.Tables.Add(dt);


            if (salesDs != null)
            {
                if (salesDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow SalesRow in salesDs.Tables[0].Rows)
                    {
                        salesQty = 0;
                        salesItemCode = string.Empty;
                        salesRate = 0;
                        salesDiscount = 0;
                        salesVat = 0;
                        salesCst = 0;
                        salesLoading = 0;
                        salesFreight = 0;
                        salesBillDate = string.Empty;
                        salesExecutive = string.Empty;

                        salesCustomer = string.Empty;

                        opCnt = 0;
                        if (SalesRow["ItemCode"] != null)
                        {
                            salesItemCode = Convert.ToString(SalesRow["ItemCode"]).Trim();
                        }
                        if (SalesRow["Qty"] != null)
                        {
                            salesQty = Convert.ToDouble(SalesRow["Qty"]);
                        }
                        if (SalesRow["Rate"] != null)
                        {
                            salesRate = Convert.ToDouble(SalesRow["Rate"]);
                        }
                        if (SalesRow["Discount"] != null)
                        {
                            salesDiscount = Convert.ToDouble(SalesRow["Discount"]);
                        }
                        if (SalesRow["Vat"] != null)
                        {
                            salesVat = Convert.ToDouble(SalesRow["Vat"]);
                        }
                        if (SalesRow["Cst"] != null && SalesRow["Cst"] != DBNull.Value)
                        {
                            salesCst = Convert.ToDouble(SalesRow["Cst"]);
                        }
                        if (SalesRow["LoadUnload"] != null && SalesRow["LoadUnload"] != DBNull.Value)
                        {
                            salesLoading = Convert.ToDouble(SalesRow["LoadUnload"]);
                        }
                        if (SalesRow["Freight"] != null && SalesRow["Freight"] != DBNull.Value)
                        {
                            salesFreight = Convert.ToDouble(SalesRow["Freight"]);
                        }
                        if (SalesRow["BillDate"] != null)
                        {
                            salesBillDate = Convert.ToString(SalesRow["BillDate"]);
                        }

                        dr = GrossProfitDs.Tables[0].NewRow();
                        dr["SalesBillno"] = SalesRow["Billno"].ToString();
                        dr["SalesBillDate"] = SalesRow["BillDate"].ToString();
                        dr["ItemCode"] = salesItemCode;
                        dr["SalesRate"] = salesRate.ToString();
                        dr["SalesDiscount"] = salesDiscount.ToString();
                        dr["SalesVat"] = salesVat.ToString();
                        dr["SalesCst"] = salesCst.ToString();
                        dr["SalesFreight"] = salesFreight.ToString();
                        dr["SalesLoading"] = salesLoading.ToString();
                        //dr["Executive"] = salesExecutive;
                        //dr["CustomerID"] = Convert.ToString(salesCustomerID);
                        //dr["CustomerName"] = salesCustomer;







                        if (Request.Cookies["Company"] != null)
                            company = Request.Cookies["Company"].Value;
                        if (openingStockDs != null)
                        {
                            foreach (DataRow OpRow in openingStockDs.Tables[0].Rows)
                            {
                                if (salesItemCode.Trim() == Convert.ToString(OpRow["ItemCode"]).Trim())
                                {
                                    opQty = Convert.ToDouble(OpRow["OpeningStock"]);
                                    if (opQty > 0)
                                    {
                                        if (salesQty >= opQty)
                                        {
                                            dr["PurchaseID"] = "0";
                                            /* Start March 17*/
                                            SearchProductRow = productDs.Tables[0].Select("Itemcode='" + salesItemCode.Trim() + "'");
                                            /* End March 17*/
                                            salesQty = salesQty - opQty;
                                            if (SearchProductRow != null)
                                            {

                                                dr["SalesQty"] = opQty.ToString();
                                                if (SearchProductRow[0]["Rate"] != null)
                                                {
                                                    if (SearchProductRow[0]["Rate"] != DBNull.Value)
                                                    {
                                                        if (Convert.ToString(SearchProductRow[0]["Rate"]) != "")
                                                        {
                                                            dr["PurchaseRate"] = Convert.ToString(SearchProductRow[0]["Rate"]);
                                                        }
                                                        else
                                                        {
                                                            dr["PurchaseRate"] = "0";
                                                        }
                                                    }
                                                }
                                                if (SearchProductRow[0]["Vat"] != null)
                                                {
                                                    if (SearchProductRow[0]["Vat"] != DBNull.Value)
                                                    {

                                                        if (Convert.ToString(SearchProductRow[0]["Vat"]) != "")
                                                            dr["PurchaseVat"] = Convert.ToString(SearchProductRow[0]["Vat"]);
                                                        else
                                                            dr["PurchaseVat"] = "0";
                                                    }
                                                }
                                                if (SearchProductRow[0]["Cst"] != null)
                                                {
                                                    if (SearchProductRow[0]["Cst"] != DBNull.Value)
                                                    {

                                                        if (Convert.ToString(SearchProductRow[0]["Cst"]) != "")
                                                            dr["PurchaseCst"] = Convert.ToString(SearchProductRow[0]["Cst"]);
                                                        else
                                                            dr["PurchaseCst"] = "0";
                                                    }
                                                }
                                                if (SearchProductRow[0]["Discount"] != null)
                                                {
                                                    if (SearchProductRow[0]["Discount"] != DBNull.Value)
                                                    {
                                                        if (Convert.ToString(SearchProductRow[0]["Discount"]) != "")
                                                            dr["PurchaseDiscount"] = Convert.ToString(SearchProductRow[0]["Discount"]);
                                                        else
                                                            dr["PurchaseDiscount"] = "0";
                                                    }
                                                }
                                                dr["PurchaseBillDate"] = "01/01/2001";

                                                dr["PurchaseLoading"] = "0";
                                                dr["PurchaseFreight"] = "0";

                                                GrossProfitDs.Tables[0].Rows.Add(dr);

                                                //bl.InsertGP(dr);


                                            }
                                            /* End March 17*/

                                            opQty = 0;
                                            openingStockDs.Tables[0].Rows[opCnt].BeginEdit();
                                            openingStockDs.Tables[0].Rows[opCnt]["OpeningStock"] = opQty;
                                            openingStockDs.Tables[0].Rows[opCnt].EndEdit();
                                            openingStockDs.Tables[0].Rows[opCnt].AcceptChanges();
                                        }
                                        else
                                        {
                                            dr["PurchaseID"] = "0";

                                            /* Start March 17*/
                                            SearchProductRow = productDs.Tables[0].Select("Itemcode='" + salesItemCode.Trim() + "'");
                                            /* End March 17*/
                                            opQty = opQty - salesQty;
                                            dr["SalesQty"] = salesQty.ToString();

                                            if (SearchProductRow[0]["Rate"] != null)
                                            {
                                                if (SearchProductRow[0]["Rate"] != DBNull.Value)
                                                {
                                                    if (Convert.ToString(SearchProductRow[0]["Rate"]) != "")
                                                        dr["PurchaseRate"] = Convert.ToString(SearchProductRow[0]["Rate"]);
                                                    else
                                                        dr["PurchaseRate"] = "0";
                                                }
                                            }
                                            if (SearchProductRow[0]["Vat"] != null)
                                            {
                                                if (SearchProductRow[0]["Vat"] != DBNull.Value)
                                                {

                                                    if (Convert.ToString(SearchProductRow[0]["Vat"]) != "")
                                                        dr["PurchaseVat"] = Convert.ToString(SearchProductRow[0]["Vat"]);
                                                    else
                                                        dr["PurchaseVat"] = "0";
                                                }
                                            }
                                            if (SearchProductRow[0]["Cst"] != null)
                                            {
                                                if (SearchProductRow[0]["Cst"] != DBNull.Value)
                                                {

                                                    if (Convert.ToString(SearchProductRow[0]["Cst"]) != "")
                                                        dr["PurchaseCst"] = Convert.ToString(SearchProductRow[0]["Cst"]);
                                                    else
                                                        dr["PurchaseCst"] = "0";
                                                }
                                            }
                                            if (SearchProductRow[0]["Discount"] != null)
                                            {
                                                if (SearchProductRow[0]["Discount"] != DBNull.Value)
                                                {
                                                    if (Convert.ToString(SearchProductRow[0]["Discount"]) != "")
                                                        dr["PurchaseDiscount"] = Convert.ToString(SearchProductRow[0]["Discount"]);
                                                    else
                                                        dr["PurchaseDiscount"] = "0";
                                                }
                                            }
                                            dr["PurchaseBillDate"] = "01/01/2001";

                                            dr["PurchaseLoading"] = "0";
                                            dr["PurchaseFreight"] = "0";

                                            GrossProfitDs.Tables[0].Rows.Add(dr);

                                            //bl.InsertGP(dr);

                                            openingStockDs.Tables[0].Rows[opCnt].BeginEdit();
                                            openingStockDs.Tables[0].Rows[opCnt]["OpeningStock"] = opQty;
                                            openingStockDs.Tables[0].Rows[opCnt].EndEdit();
                                            openingStockDs.Tables[0].Rows[opCnt].AcceptChanges();
                                            salesQty = 0;
                                            /* End March 17*/
                                        }
                                    }
                                    break;
                                }
                                opCnt = opCnt + 1;
                            } //OPening Stock Row
                        }
                        if (salesQty > 0)
                        {

                            if (purchaseDs != null)
                            {
                                if (purchaseDs.Tables[0].Rows.Count > 0)
                                {

                                    /* Start March 25 */
                                    foreach (DataRow purchaseRow in purchaseDs.Tables[0].Rows)
                                    /*End March 25*/
                                    {
                                        if (salesQty > 0)
                                        {

                                            if (salesItemCode.Trim() == Convert.ToString(purchaseRow["ItemCode"]).Trim())
                                            {
                                                purchaseQty = Convert.ToDouble(purchaseRow["Qty"]);
                                                if (purchaseQty > 0)
                                                {

                                                    dr = GrossProfitDs.Tables[0].NewRow();
                                                    dr["SalesBillno"] = SalesRow["Billno"].ToString();
                                                    dr["SalesBillDate"] = SalesRow["BillDate"].ToString();
                                                    dr["ItemCode"] = salesItemCode;
                                                    dr["SalesRate"] = salesRate;
                                                    dr["SalesDiscount"] = salesDiscount.ToString();
                                                    dr["SalesVat"] = salesVat.ToString();
                                                    dr["SalesCst"] = salesCst.ToString();
                                                    dr["SalesFreight"] = salesFreight.ToString();
                                                    dr["SalesLoading"] = salesLoading.ToString();
                                                    //dr["Executive"] = salesExecutive;
                                                    //dr["CustomerID"] = salesCustomerID;
                                                    //dr["CustomerName"] = salesCustomer;

                                                    if (salesQty >= purchaseQty)
                                                    {

                                                        salesQty = salesQty - purchaseQty;
                                                        dr["SalesQty"] = purchaseQty.ToString();
                                                        dr["PurchaseID"] = Convert.ToString(purchaseRow["PurchaseID"]);
                                                        dr["PurchaseRate"] = Convert.ToString(purchaseRow["PurchaseRate"]);
                                                        dr["PurchaseVat"] = Convert.ToString(purchaseRow["VAT"]);
                                                        dr["PurchaseCst"] = Convert.ToString(purchaseRow["Cst"]);
                                                        dr["PurchaseDiscount"] = Convert.ToString(purchaseRow["Discount"]);
                                                        dr["PurchaseBillDate"] = Convert.ToString(purchaseRow["BillDate"]);
                                                        /*Start March 17*/


                                                        LFRrow = null;
                                                        LFRrow = dsCharges.Tables[0].Select("Itemcode='" + salesItemCode.Trim() + "' and purchaseID='" + Convert.ToInt32(purchaseRow["PurchaseID"]).ToString() + "'");

                                                        if (LFRrow != null && LFRrow.Length > 0)
                                                        {
                                                            if (LFRrow[0]["PurchaseLoading"] != null)
                                                            {
                                                                if (Convert.ToString(LFRrow[0]["PurchaseLoading"]) != "NaN")
                                                                    dr["PurchaseLoading"] = Convert.ToString(LFRrow[0]["PurchaseLoading"]);
                                                                else
                                                                    dr["PurchaseLoading"] = "0";
                                                            }
                                                            else
                                                            {
                                                                dr["PurLoading"] = "0";
                                                            }

                                                            if (LFRrow[0]["PurchaseFreight"] != null && Convert.ToString(LFRrow[0]["PurchaseFreight"]) != "NaN")
                                                            {
                                                                if (Convert.ToString(LFRrow[0]["PurchaseFreight"]) != "NaN")
                                                                    dr["PurchaseFreight"] = Convert.ToString(LFRrow[0]["PurchaseFreight"]);
                                                                else
                                                                    dr["PurchaseFreight"] = "0";

                                                            }
                                                            else
                                                            {
                                                                dr["PurchaseFreight"] = "0";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            dr["PurchaseLoading"] = "0";
                                                            dr["PurchaseFreight"] = "0";
                                                        }
                                                        /*End March 17*/
                                                        GrossProfitDs.Tables[0].Rows.Add(dr);
                                                        /*Start March 17*/
                                                        //bl.InsertGP(dr);
                                                        /*End March 17*/
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].BeginEdit();
                                                        purchaseDs.Tables[0].Rows[purchaseCnt]["Qty"] = "0";
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].EndEdit();
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].AcceptChanges();
                                                        //purchaseDs.Tables[0].Rows[purchaseCnt].Delete();
                                                        purchaseCnt = purchaseCnt + 1;
                                                    }
                                                    else
                                                    {
                                                        purchaseQty = purchaseQty - salesQty;
                                                        dr["SalesQty"] = salesQty.ToString();
                                                        dr["PurchaseID"] = Convert.ToString(purchaseRow["PurchaseID"]);
                                                        dr["PurchaseRate"] = Convert.ToString(purchaseRow["PurchaseRate"]);
                                                        dr["PurchaseVat"] = Convert.ToString(purchaseRow["VAT"]);
                                                        dr["PurchaseCst"] = Convert.ToString(purchaseRow["Cst"]);
                                                        dr["PurchaseDiscount"] = Convert.ToString(purchaseRow["Discount"]);
                                                        dr["PurchaseBillDate"] = Convert.ToString(Convert.ToDateTime(purchaseRow["BillDate"]));
                                                        /*Start March 17*/
                                                        LFRrow = dsCharges.Tables[0].Select("Itemcode='" + salesItemCode.Trim() + "' and purchaseID='" + Convert.ToInt32(purchaseRow["PurchaseID"]).ToString() + "'");
                                                        if (LFRrow != null && LFRrow.Length > 0)
                                                        {
                                                            if (LFRrow[0]["PurchaseLoading"] != null)
                                                            {
                                                                if (Convert.ToString(LFRrow[0]["PurchaseLoading"]) != "NaN")
                                                                    dr["PurchaseLoading"] = Convert.ToString(LFRrow[0]["PurchaseLoading"]);
                                                                else
                                                                    dr["PurchaseLoading"] = "0";
                                                            }
                                                            else
                                                            {
                                                                dr["PurchaseLoading"] = "0";
                                                            }

                                                            if (LFRrow[0]["PurchaseFreight"] != null && Convert.ToString(LFRrow[0]["PurchaseFreight"]) != "NaN")
                                                            {
                                                                if (Convert.ToString(LFRrow[0]["PurchaseFreight"]) != "NaN")
                                                                    dr["PurchaseFreight"] = Convert.ToString(LFRrow[0]["PurchaseFreight"]);
                                                                else
                                                                    dr["PurchaseFreight"] = "0";

                                                            }
                                                            else
                                                            {
                                                                dr["PurchaseFreight"] = "0";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            dr["PurchaseLoading"] = "0";
                                                            dr["PurchaseFreight"] = "0";
                                                        }
                                                        /*End March 17*/
                                                        GrossProfitDs.Tables[0].Rows.Add(dr);
                                                        /*Start March 17*/
                                                        //bl.InsertGP(dr);
                                                        /*End March 17*/
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].BeginEdit();
                                                        purchaseDs.Tables[0].Rows[purchaseCnt]["Qty"] = purchaseQty;
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].EndEdit();
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].AcceptChanges();
                                                        salesQty = 0;
                                                        break;
                                                    }
                                                    //GrossProfitDs.Tables[0].Rows.Add(dr);
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            break;
                                        }

                                    } //Purchase Row


                                }
                            }



                        }


                    } //Sales Row 
                }
            }
            bl.InsertGP(GrossProfitDs);

            return GrossProfitDs;
        }
        catch (Exception ex)
        {

            lblMessage.Text = ex.Message;
            return GrossProfitDs;
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
            /*Start March 17*/
            double dPFrRate = 0;
            double dPLURate = 0;
            /*End March 17*/

            double dGrandRate = 0;
            double dDiscountRate = 0;
            lblMessage.Text = "";
            double dPNetRate = 0;
            double dPVatRate = 0;
            double dPDisRate = 0;
            double dPCSTRate = 0;
            double dPDiscountRate = 0;
            double dPGrandRate = 0;
            double dGrossProfit = 0;
            double PurchaseQuantity = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (DataBinder.Eval(e.Row.DataItem, "NetRate") != DBNull.Value)
                    dNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualVat") != DBNull.Value)
                    dVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualVat"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualDiscount") != DBNull.Value)
                    dDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "SalesDis") != DBNull.Value)
                    dDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesDis"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualCST") != DBNull.Value)
                    dCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));
                if (DataBinder.Eval(e.Row.DataItem, "SumFreight") != DBNull.Value)
                    dFrRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumFreight"));
                if (DataBinder.Eval(e.Row.DataItem, "Loading") != DBNull.Value)
                    dLURate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Loading"));

                dGrandRate = dDiscountRate + dVatRate + dCSTRate;
                dSNetRate = dSNetRate + dNetRate;
                dSVatRate = dSVatRate + dVatRate;
                dSDiscountRate = dSDiscountRate + dDisRate;
                dSCSTRate = dSCSTRate + dCSTRate;


                if (DataBinder.Eval(e.Row.DataItem, "PurchaseNetRate") != DBNull.Value)
                    dPNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseNetRate"));
                if (DataBinder.Eval(e.Row.DataItem, "PurchaseActualVat") != DBNull.Value)
                    dPVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseActualVat"));
                if (DataBinder.Eval(e.Row.DataItem, "PurchaseActualDiscount") != DBNull.Value)
                    dPDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseActualDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "PurchaseDis") != DBNull.Value)
                    dPDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseDis"));
                if (DataBinder.Eval(e.Row.DataItem, "PurchaseActualCST") != DBNull.Value)
                    dPCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseActualCST"));
                /*Start March 17*/
                if (DataBinder.Eval(e.Row.DataItem, "SumPurchaseFreight") != DBNull.Value)
                    dPFrRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumPurchaseFreight"));
                if (DataBinder.Eval(e.Row.DataItem, "PurLoading") != DBNull.Value)
                    dPLURate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurLoading"));
                if (DataBinder.Eval(e.Row.DataItem, "Quantity") != DBNull.Value)
                    PurchaseQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                /*End March 17*/


                dSPNetRate = dSPNetRate + dPNetRate;
                dSPVatRate = dSPVatRate + dPVatRate;
                dSPDiscountRate = dSPDiscountRate + dPDisRate;
                dSPCSTRate = dSPCSTRate + dPCSTRate;


                /* Start GP March 16 */
                //dPGrandRate = dPDiscountRate + dPVatRate + dPCSTRate;
                dPGrandRate = dPDiscountRate + dPCSTRate + dPFrRate + dPLURate;
                /* End GP March 16 */
                GridView gv = e.Row.FindControl("gvSecond") as GridView;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                Label lblLink = (Label)e.Row.FindControl("lblLink");
                string category = string.Empty;
                category = Convert.ToString(cmbDisplayCat.SelectedItem.Text);
                DataSet ds = new DataSet();
                if (category == "Daywise")
                {
                    DateTime startDate, endDate;
                    startDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "LinkName"));
                    endDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                    ds = bl.SecondLevelGrossReportDaywise(startDate);
                    lblLink.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName", "{0:dd/MM/yyyy}"));
                }
                else if (category == "Categorywise")
                {


                    ds = bl.SecondLevelGrossReportCategorywise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
                }
                else if (category == "Brandwise")
                {


                    ds = bl.SecondLevelGrossReportBrandwise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
                }
                else if (category == "Modelwise")
                {


                    ds = bl.SecondLevelGrossReportModelwise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
                }
                else if (category == "Billwise")
                {
                    lblLink.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName", "{0:f2}"));
                    ds = bl.SecondLevelGrossReportBillwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LinkName")));

                }
                else if (category == "Customerwise")
                {
                    ds = bl.SecondLevelGrossReportCustomerwise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
                }
                else if (category == "Executivewise")
                {
                    ds = bl.SecondLevelGrossReportExecutivewise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
                }
                /*Start Itemwise*/
                else if (category == "Itemwise")
                {
                    ds = bl.SecondLevelGrossReportItemwise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));

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
                Label lblFreightRate = (Label)e.Row.FindControl("lblFreightRate");
                Label lblLURate = (Label)e.Row.FindControl("lblLURate");
                Label lblPurchaseFreightRate = (Label)e.Row.FindControl("lblPurchaseFreightRate");
                Label lblPurchaseLURate = (Label)e.Row.FindControl("lblPurchaseLURate");
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label lblPTotal = (Label)e.Row.FindControl("lblPurchaseTotal");
                Label lblGP = (Label)e.Row.FindControl("lblGP");
                lblFreightRate.Text = dSCFrRate.ToString("f2");
                lblLURate.Text = dSCLURate.ToString("f2");

                //dPFrRate = PurchaseQuantity * dPFrRate;
                //dPLURate = PurchaseQuantity * dPLURate;
                dSPFrRate = dSPFrRate + dPFrRate;
                dSPLURate = dSPLURate + dPLURate;
                dSPCFrRate = 0;
                dSPCLURate = 0;
                lblPurchaseFreightRate.Text = dPFrRate.ToString("f2");
                lblPurchaseLURate.Text = dPLURate.ToString("f2");

                dSFrRate = dSFrRate + dSCFrRate;
                dSLURate = dSLURate + dSCLURate;
                dGrandRate = dGrandRate + dSCFrRate + dSCLURate;

                dSGrandRate = dSGrandRate + dGrandRate;
                dSPGrandRate = dSPGrandRate + dPGrandRate;

                lblTotal.Text = dGrandRate.ToString("f2");
                lblPTotal.Text = dPGrandRate.ToString("f2");

                dGrossProfit = dDiscountRate - dPGrandRate; // -dPVatRate;
                dSGrossProfit = dSGrossProfit + dGrossProfit;

                lblGP.Text = dGrossProfit.ToString("f2");
                dGrandRate = 0;
                dPGrandRate = 0;
                dGrossProfit = 0;
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
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[1].Text = dSNetRate.ToString("f2");
                e.Row.Cells[2].Text = dSDiscountRate.ToString("f2");
                e.Row.Cells[3].Text = dSVatRate.ToString("f2");
                e.Row.Cells[4].Text = dSCSTRate.ToString("f2");
                e.Row.Cells[5].Text = dSFrRate.ToString("f2");
                e.Row.Cells[6].Text = dSLURate.ToString("f2");
                e.Row.Cells[7].Text = dSGrandRate.ToString("f2");

                e.Row.Cells[8].Text = dSPNetRate.ToString("f2");
                e.Row.Cells[9].Text = dSPDiscountRate.ToString("f2");
                e.Row.Cells[10].Text = dSPVatRate.ToString("f2");
                e.Row.Cells[11].Text = dSPCSTRate.ToString("f2");
                e.Row.Cells[12].Text = dSPFrRate.ToString("f2");



                e.Row.Cells[13].Text = dSPLURate.ToString("f2");
                e.Row.Cells[14].Text = dSPGrandRate.ToString("f2");
                e.Row.Cells[15].Text = dSGrossProfit.ToString("f2");
                if (dSGrossProfit > 0)
                    lblGrossPL.Text = "Profit: " + dSGrossProfit.ToString("f2");
                else
                    lblGrossPL.Text = "Loss: " + Math.Abs(dSGrossProfit).ToString("f2");
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
            double dCGrandRate = 0;
            double dDiscountRate = 0;
            lblMessage.Text = "";
            lblErr.Text = "";
            double dPNetRate = 0;
            double dPVatRate = 0;
            double dPDisRate = 0;
            double dPCSTRate = 0;
            double dPDiscountRate = 0;
            double dPCGrandRate = 0;
            double dCGrossProfit = 0;
            double PurchaseQuantity = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Quantity") != DBNull.Value)
                    dQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                if (DataBinder.Eval(e.Row.DataItem, "NetRate") != DBNull.Value)
                    dNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualVat") != DBNull.Value)
                    dVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualVat"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualDiscount") != DBNull.Value)
                    dDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "SalesDis") != DBNull.Value)
                    dDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesDis"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualCST") != DBNull.Value)
                    dCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));
                //if (DataBinder.Eval(e.Row.DataItem, "SumFreight") != DBNull.Value)
                //    dFrRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumFreight"));
                //if (DataBinder.Eval(e.Row.DataItem, "Loading") != DBNull.Value)
                //    dLURate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Loading"));
                if (DataBinder.Eval(e.Row.DataItem, "PurchaseNetRate") != DBNull.Value)
                    dPNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseNetRate"));
                if (DataBinder.Eval(e.Row.DataItem, "PurchaseActualVat") != DBNull.Value)
                    dPVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseActualVat"));
                if (DataBinder.Eval(e.Row.DataItem, "PurchaseActualDiscount") != DBNull.Value)
                    dPDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseActualDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "PurchaseDis") != DBNull.Value)
                    dPDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseDis"));
                if (DataBinder.Eval(e.Row.DataItem, "PurchaseActualCST") != DBNull.Value)
                    dPCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseActualCST"));
                if (DataBinder.Eval(e.Row.DataItem, "Quantity") != DBNull.Value)
                    PurchaseQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Quantity"));


                dSQty = dSQty + dQuantity;
                dSPCNetRate = dSPCNetRate + dPNetRate;
                dSPCVatRate = dSPCVatRate + dPVatRate;
                dSPCDiscountRate = dSPCDiscountRate + dPDisRate;
                dSPCCSTRate = dSPCCSTRate + dPCSTRate;
                //dPCGrandRate = dPDiscountRate + dPVatRate + dPCSTRate;
                dPCGrandRate = dPDiscountRate + dPCSTRate;

                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label lblFreightRate = (Label)e.Row.FindControl("lblFreightRate");
                Label lblLURate = (Label)e.Row.FindControl("lblLURate");

                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SalesBillNo")) == tempBillno)
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
                            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SalesBillNo")) != Convert.ToInt32(arr[i]))
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
                        {
                            dSCFrRate = dSCFrRate + Convert.ToDouble(lblFreightRate.Text.Trim());
                            dCGrandRate = dCGrandRate + Convert.ToDouble(lblFreightRate.Text.Trim());
                        }
                        if (lblLURate.Text.Trim() != "")
                        {
                            dSCLURate = dSCLURate + Convert.ToDouble(lblLURate.Text.Trim());
                            dCGrandRate = dCGrandRate + Convert.ToDouble(lblLURate.Text.Trim());
                        }

                    }
                    lblFreightRate.Visible = true;
                    lblLURate.Visible = true;
                    tempBillno = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SalesBillNo"));
                }

                // }
                /*Start March 17*/
                Label lblPurchaseFreightRate = (Label)e.Row.FindControl("lblPurchaseFreightRate");
                Label lblPurchaseLURate = (Label)e.Row.FindControl("lblPurchaseLURate");
                if (lblPurchaseFreightRate.Text.Trim() != "")
                {
                    dSPCFrRate = dSPCFrRate + (Convert.ToDouble(lblPurchaseFreightRate.Text.Trim()) * PurchaseQuantity);
                    dPCGrandRate = dPCGrandRate + (Convert.ToDouble(lblPurchaseFreightRate.Text.Trim()) * PurchaseQuantity);
                    lblPurchaseFreightRate.Text = (Convert.ToDouble(lblPurchaseFreightRate.Text.Trim()) * PurchaseQuantity).ToString("f2");
                }
                if (lblPurchaseLURate.Text.Trim() != "")
                {
                    dSPCLURate = dSPCLURate + (Convert.ToDouble(lblPurchaseLURate.Text.Trim()) * PurchaseQuantity);
                    dPCGrandRate = dPCGrandRate + (Convert.ToDouble(lblPurchaseLURate.Text.Trim()) * PurchaseQuantity);
                    lblPurchaseLURate.Text = (Convert.ToDouble(lblPurchaseLURate.Text.Trim()) * PurchaseQuantity).ToString("f2");
                }
                /*End Start March 17*/

                //Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label lblPTotal = (Label)e.Row.FindControl("lblPurchaseTotal");
                Label lblGP = (Label)e.Row.FindControl("lblGP");

                dSCNetRate = dSCNetRate + dNetRate;
                dSCVatRate = dSCVatRate + dVatRate;
                dSCDiscountRate = dSCDiscountRate + dDisRate;
                dSCCSTRate = dSCCSTRate + dCSTRate;
                dCGrandRate = dCGrandRate + dDiscountRate + dVatRate + dCSTRate;// +dFrRate + dLURate;
                dSPCGrandRate = dSPCGrandRate + dPCGrandRate;
                dSCGrandRate = dSCGrandRate + dCGrandRate;

                lblTotal.Text = dCGrandRate.ToString("f2");
                lblPTotal.Text = dPCGrandRate.ToString("f2");

                dCGrossProfit = dDiscountRate - dPCGrandRate;
                dSCGrossProfit = dSCGrossProfit + dCGrossProfit;

                lblGP.Text = dCGrossProfit.ToString("f2");
                //dCGrandRate = 0;
                //dPCGrandRate = 0;
                cnt = cnt + 1;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {


                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
                //dSCGrandRate = dSCGrandRate + dSCFrRate + dSCLURate;
                e.Row.Cells[4].Text = dSQty.ToString("f2");
                e.Row.Cells[5].Text = dSCNetRate.ToString("f2");
                e.Row.Cells[6].Text = dSCDiscountRate.ToString("f2");
                e.Row.Cells[7].Text = dSCVatRate.ToString("f2");
                e.Row.Cells[8].Text = dSCCSTRate.ToString("f2");
                e.Row.Cells[9].Text = dSCFrRate.ToString("f2");
                e.Row.Cells[10].Text = dSCLURate.ToString("f2");
                e.Row.Cells[11].Text = dSCGrandRate.ToString("f2");

                e.Row.Cells[13].Text = dSPCNetRate.ToString("f2");
                e.Row.Cells[14].Text = dSPCDiscountRate.ToString("f2");
                e.Row.Cells[15].Text = dSPCVatRate.ToString("f2");
                e.Row.Cells[16].Text = dSPCCSTRate.ToString("f2");
                e.Row.Cells[17].Text = dSPCFrRate.ToString("f2");
                e.Row.Cells[18].Text = dSPCLURate.ToString("f2");
                e.Row.Cells[19].Text = dSPCGrandRate.ToString("f2");
                e.Row.Cells[20].Text = dSCGrossProfit.ToString("f2");

                dSCGrossProfit = 0;

                dSCDiscountRate = 0;
                dSCNetRate = 0;
                dSCVatRate = 0;
                dSCCSTRate = 0;
                dSCGrandRate = 0;
                dSQty = 0;
                dSPCNetRate = 0;
                dSPCDiscountRate = 0;
                dSPCVatRate = 0;
                dSPCCSTRate = 0;
                dSPCGrandRate = 0;
                //dSPCFrRate = 0;
                //dSPCLURate = 0;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
