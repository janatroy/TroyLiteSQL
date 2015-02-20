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

public partial class ProductCommPurchaseBill : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    public double dTot = 0;
    public double dVat = 0;
    public double dRat = 0;
    public double dNet = 0;
    public double dQty = 0;
    public double dDis = 0;
    public double dCST = 0;
    public double sumVat = 0;
    private double amtTotal = 0;
    public double rateTotal = 0;
    public double vatTotal = 0;
    public double disTotal = 0;
    public double cstTotal = 0;
    public double disTot = 0;
    public double disTotamt = 0;
    private string currency = string.Empty;
    private string currencyType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        { 
        int iBillno = 0;

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //if (Request.QueryString["Req"] != null)
        //{
        //    btnBack.Visible = false;
        //}
        if (Request.QueryString["RT"] == "NO")
        {
            btnBack.Visible = false;
        }
        if (!IsPostBack)
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            BusinessLogic bl = new BusinessLogic(sDataSource);
            BindCurrencyLabels();

            if (Request.QueryString["SID"] != null)
            {
                iBillno = Convert.ToInt32(Request.QueryString["SID"].ToString());
                GetHeaderInfo();
                string BillFormat = bl.getConfigInfo();

                // viswasfooter.Visible = false;
                GetPurchaseProductDetails(iBillno);

            }
            else
            {
                Response.Redirect("CommissionMngmt.aspx");
            }
        }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindCurrencyLabels()
    {
        DataSet appSettings;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "CURRENCY")
                {
                    currency = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }

        if (currency == "INR")
        {
            lblCurrTotal.Text = "(INR)";
            lblCurrVAT.Text = "(INR)";
            lblCurrLoad.Text = "(INR)";
            lblCurrDisp.Text = "(INR)";
            lblCurrCST.Text = "(INR)";
            lblCurrGrandTTL.Text = "(INR)";
            currencyType = "Rs";
        }

        if (currency == "GBP")
        {
            lblCurrTotal.Text = "(£)";
            lblCurrVAT.Text = "(£)";
            lblCurrLoad.Text = "(£)";
            lblCurrDisp.Text = "(£)";
            lblCurrCST.Text = "(£)";
            lblCurrGrandTTL.Text = "(£)";
            currencyType = "£";
        }

    }

    public void GetHeaderInfo()
    {
        double dFreight = 0;
        double dFr = 0;
        double dUL = 0;

        double dComm = 0;

        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet dsBill = new DataSet();
        dsBill = bl.GetCommissionForId(Convert.ToInt32(Request.QueryString["SID"]));
        int supplierID = 0;

        if (dsBill.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsBill.Tables[0].Rows)
            {

                lblBillno.Text = Convert.ToString(dr["CommissionNo"]);
                lblBillDate.Text = Convert.ToDateTime(dr["Commissiondate"]).ToShortDateString();
                //lblTransNo.Text = dr["TransNo"].ToString();

                //lblReason.Text = Convert.ToString(dr["PurchaseReturnReason"]);
                
                        lblHeader.Text = "Purchase Bill";
                    
                //divInvoiceType.InnerHtml = "<b>" + "Commission - Sales " + "</b>";
                supplierID = Convert.ToInt32(dr["SupplierID"]);

                lblSupplier.Text = bl.supplierName(sDataSource, supplierID);
                /*Start Purchase Loading / Unloading Freight Change - March 16*/
                if (dr["Freight"] != null && dr["Freight"] != DBNull.Value)
                {
                    dFr = Convert.ToDouble(dr["freight"]);
                }
                if (dr["LoadUnload"] != null && dr["LoadUnload"] != DBNull.Value)
                {
                    dUL = Convert.ToDouble(dr["LoadUnload"]);
                }
                dFreight = dFreight + dFr + dUL;

                if (dr["Comissionvalue"] != null && dr["Comissionvalue"] != DBNull.Value)
                {
                    dComm = Convert.ToDouble(dr["Comissionvalue"]);
                }
                lblcomm.Text = dComm.ToString("f2");

                lblFg.Text = dUL.ToString("f2");
                lblfght.Text = dFr.ToString("f2");

                /*End Purchase Loading / Unloading Freight Change - March 16*/
                string notes = "";

                //if (dr["DeliveryNote"].ToString() == "YES")
                //{
                //    notes = notes + "Delivery Note" + "</br>";
                //}
                //if (dr["InternalTransfer"].ToString() == "YES")
                //{
                //    notes = notes + "Internal Transfer" + "</br>";
                //}
                //if (dr["SalesReturn"].ToString() == "YES")
                //{
                //    notes = notes + "Sales Return" + "</br>";
                //}

                divNotes.InnerHtml = notes;
            }
        }

        DataSet companyInfo = new DataSet();

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
                        lblSignCompany.Text = Convert.ToString(dr["CompanyName"]);
                        lblAddress.Text = Convert.ToString(dr["Address"]);
                        lblCity.Text = Convert.ToString(dr["city"]);
                        lblPincode.Text = Convert.ToString(dr["Pincode"]);
                        lblState.Text = Convert.ToString(dr["state"]);

                    }
                }
            }
        }



        DataSet dsSupplier = bl.getAddressInfo(supplierID);
        if (dsSupplier != null)
        {
            if (dsSupplier.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSupplier.Tables[0].Rows)
                {
                    lblSuppAdd1.Text = Convert.ToString(dr["Add1"]);
                    lblSuppAdd2.Text = Convert.ToString(dr["Add2"]);
                    lblSuppAdd3.Text = Convert.ToString(dr["Add3"]);
                    lblSuppPh.Text = Convert.ToString(dr["Phone"]);
                    if (dr["COntactname"].ToString() != string.Empty)
                        lblSupplier.Text = Convert.ToString(dr["ContactName"]);
                }
            }
        }

    }




    public void GetPurchaseProductDetails(int purchaseID)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetCommissionItemsForId(purchaseID);

        if (ds != null)
        {

            dt = new DataTable();

            dc = new DataColumn("Particulars");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("Unit");
            dt.Columns.Add(dc);

            dc = new DataColumn("Discount");
            dt.Columns.Add(dc);

            dc = new DataColumn("DiscAmt");
            dt.Columns.Add(dc);

            dc = new DataColumn("VAT");
            dt.Columns.Add(dc);

            dc = new DataColumn("CST");
            dt.Columns.Add(dc);

            dc = new DataColumn("NetRate");
            dt.Columns.Add(dc);

            dc = new DataColumn("PurchaseRate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Amount");
            dt.Columns.Add(dc);

            dc = new DataColumn("Bundles");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rods");
            dt.Columns.Add(dc);

            billDs.Tables.Add(dt);

            string itemCode = string.Empty;
            string sParticulars = string.Empty;
            double dRate = 0;
            double dNetRate = 0;
            int iBundles = 0;
            int iRods = 0;
            double qty = 0;
            double dAmout = 0;
            double dVat = 0;

            double dFreight = 0;
            double dTotal = 0;
            string measureUnit = string.Empty;
            string tempItemCode = string.Empty;
            string strItemCode = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    if (dr["itemCode"] != null)
                    {
                        itemCode = Convert.ToString(dr["ItemCode"]);
                        sParticulars = bl.getBillProductName(itemCode);
                        //measureUnit = bl.getBillProductUnit(itemCode);
                        measureUnit = "0";
                    }
                    ////salesDs = bl.GetProductPurchaseBill(purchaseID, itemCode);
                    ////foreach (DataRow salesRow in salesDs.Tables[0].Rows)
                    ////{


                    ////qty = Convert.ToDouble(dr["Quantity"]);
                    ////    if (dr["PurchaseRate"] != null)
                    ////    {
                    ////        dRate = Convert.ToDouble(salesRow["PurchaseRate"]);
                    ////    }
                    ////    if (salesRow["SumVat"] != null)
                    ////    {
                    ////        dNetRate = Convert.ToDouble(salesRow["SumVat"]) / qty;
                    ////    }

                    ////    dTotal = dRate * qty;
                    ////    }
                    if (dr["Rate"] != null)
                    {
                        dRate = Convert.ToDouble(dr["Rate"]);
                    }
                    
                        dNetRate = 0;
                    
                    qty = Convert.ToDouble(dr["Qty"]);
                    dTotal = dRate * qty;
                    //if (tempItemCode != itemCode)
                    //{
                    //    strItemCode = strItemCode + tempItemCode + ",";
                    //    string delim = ",";
                    //    char[] delimA = delim.ToCharArray();
                    //    string[] arr = strItemCode.Split(delimA);
                    //    int chkcnt = 0;
                    //    for (int i = 0; i < arr.Length; i++)
                    //    {
                    //        if (arr[i].ToString() != "")
                    //        {
                    //            if (itemCode != Convert.ToString(arr[i]))
                    //            {
                    //                chkcnt = 0;
                    //            }
                    //            else
                    //            {
                    //                chkcnt = chkcnt + 1;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //    if (chkcnt == 0)
                    //    {
                    drNew = dt.NewRow();
                    drNew["Particulars"] = sParticulars;
                    drNew["PurchaseRate"] = dRate.ToString("f2");
                    drNew["NetRate"] = dNetRate.ToString("f2");
                    drNew["Discount"] = "0";
                    drNew["DiscAmt"] = "0";
                    drNew["CST"] = "0";
                    drNew["Qty"] = Convert.ToString(qty);
                    drNew["VAT"] = "0";
                    drNew["Unit"] = "0";
                    drNew["Amount"] = dTotal.ToString("f2");
                    billDs.Tables[0].Rows.Add(drNew);
                    // }
                    // tempItemCode = itemCode;
                    // }


                }
            }

            gvGeneral.Visible = true;
            gvGeneral.DataSource = billDs;
            gvGeneral.DataBind();


        }

    }

    protected void ddDivsions_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet companyInfo = new DataSet();
            int iBillno = 0;

            if (Request.QueryString["SID"] != null)
            {
                iBillno = Convert.ToInt32(Request.QueryString["SID"].ToString());
                string RTT = Convert.ToString(Request.QueryString["RT"]);
                if (RTT == "V")
                    Response.Redirect("PrintProductCommission.aspx?SID=" + iBillno + "&RT=" + "V");
                else
                    Response.Redirect("PrintProductCommission.aspx?SID=" + iBillno + "&RT=" + "NO");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    int iBillno = 0;

    //    if (Request.QueryString["SID"] != null)
    //    {
    //        iBillno = Convert.ToInt32(Request.QueryString["SID"].ToString());
    //        Response.Redirect("PrintProductSalesBill.aspx?SID=" + iBillno + "&RT=" + "");
    //    }
        
    //}

    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    int iBillno = 0;

    //    if (Request.QueryString["SID"] != null)
    //    {
    //        iBillno = Convert.ToInt32(Request.QueryString["SID"].ToString());
    //        Response.Redirect("PrintProductSalesBill.aspx?SID=" + iBillno + "&RT=" + "");
    //    }
        

    //}


    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Session["NEWPURCHASE"] = "Y";
            Session["PurchaseBillDate"] = lblBillDate.Text;
            Response.Redirect("CommissionMngmt.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public double GetSum(double rate, double vat, double cst)
    {
        double tot = 0;
        tot = rate + (rate * (vat / 100)) + (rate * (cst / 100));
        return tot;
    }
    public double GetDiscount(double qty, double rate, double discount)
    {
        double dis = 0;
        dis = (qty * rate) - ((qty * rate) * (discount / 100));
        return dis;
    }
    public double GetCSTVAT(double rate, double cst)
    {

        cst = (rate * (cst / 100));
        return cst;
    }
    protected void gvGeneral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double sumNet = 0;
            double dFr = 0;
            double vat = 0;
            double cst = 0;
            double discount = 0;
            double discamt = 0;
            double purchaseRate = 0;
            double currDis = 0;
            double currVat = 0;
            double dcomm = 0;
            double ddu = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value)
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));
                if (DataBinder.Eval(e.Row.DataItem, "Discount") != DBNull.Value)
                    discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                if (DataBinder.Eval(e.Row.DataItem, "Discamt") != DBNull.Value)
                    discamt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discamt"));
                if (DataBinder.Eval(e.Row.DataItem, "PurchaseRate") != DBNull.Value)
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseRate"));

                dTot = dTot + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                //dVat = dVat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate"));
                dRat = dRat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseRate"));

                dQty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                currDis = GetDiscount(dQty, purchaseRate, discount);
                dDis = dDis + currDis;
                dCST = dCST + GetCSTVAT(currDis, cst);
                disTot = disTot + ((dQty * purchaseRate) * (discount / 100));
                disTotamt = discamt;
                cstTotal = cstTotal + dCST;
                currVat = GetCSTVAT(currDis, vat);
                vatTotal = vatTotal + currVat;
                amtTotal = amtTotal + GetSum(currDis, vat, cst);
                sumVat = sumVat + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate")) - Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PurchaseRate"))) * dQty;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                dFr = Convert.ToDouble(lblfght.Text);

                ddu = Convert.ToDouble(lblFg.Text);

                dcomm = Convert.ToDouble(lblcomm.Text);

                sumNet = dDis - dFr - ddu - dcomm;

                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dTot.ToString("f2");
                lblGrandVat.Text = Math.Abs(vatTotal).ToString("f2");
                lblGrandDiscount.Text = disTot.ToString("f2");
                lbldiscamt.Text = disTotamt.ToString("f2");
                lblGrandCst.Text = dCST.ToString("f2");
                /*Start Purchase Loading / Unloading Freight Change - March 16*/
                lblNetTotal.Text = sumNet.ToString("f2");
                lblRs.Text = currencyType + " " + sumNet.ToString("N2");
                /*End Purchase Loading / Unloading Freight Change - March 16*/

                lblAmt.Text = dTot.ToString("f2");
                if (dDis > 0)
                    dvDiscountTotal.Visible = false;
                else
                    dvDiscountTotal.Visible = false;

                if (disTotamt > 0)
                    DvDiscAmt.Visible = false;
                else
                    DvDiscAmt.Visible = false;

                if (dCST > 0)
                    dvCSTTotal.Visible = true;
                else
                    dvCSTTotal.Visible = false;

                if (Convert.ToDouble(lblGrandVat.Text) > 0)
                    dvVatTotal.Visible = true;
                else
                    dvVatTotal.Visible = false;

                if (ddu > 0)
                    Div1.Visible = true;
                else
                    Div1.Visible = true;

                if (dFr > 0)
                    dvFrgTotal.Visible = true;
                else
                    dvFrgTotal.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}