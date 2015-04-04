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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public partial class ProductSalesBill : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    public double dTot = 0;
    public double dVat = 0;
    public double dRat = 0;
    public double dNet = 0;
    public double dQty = 0;
    public double sumVat = 0;

    public double dDis = 0;
    public double dCST = 0;
    private double amtTotal = 0;
    public double rateTotal = 0;
    public double vatTotal = 0;
    public double disTotal = 0;
    public double cstTotal = 0;
    public double disTot = 0;
    private string currency = string.Empty;
    private string currencyType = string.Empty;
    string BillingMethod = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int iBillno = 0;

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            BusinessLogic bl = new BusinessLogic(sDataSource);

            BindCurrencyLabels();

            if (Request.QueryString["Req"] != null)
            {
                btnBack.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                loadDivisions();

                if (ddDivsions.Items.Count == 1)
                {
                    lblDivisions.Visible = false;
                    ddDivsions.Visible = false;
                    divDiv.Visible = false;
                }
                else
                {
                    lblDivisions.Visible = true;
                    ddDivsions.Visible = true;
                    divDiv.Visible = true;
                }
            }

            if (Request.QueryString["SID"] != null)
            {
                iBillno = Convert.ToInt32(Request.QueryString["SID"].ToString());
                string branchCode = Request.QueryString["BID"].ToString();

                if (ddDivsions.SelectedIndex != 0)
                {
                    FillDivision();
                }

                GetHeaderInfo();
                GetSalesDetailsA4Format(iBillno, branchCode);
                GetTotalSales(iBillno, branchCode);
                GetCustomerCareDetails();
                A4FORMAT.Visible = true;

                string BillFormat = bl.getConfigInfo();
            }
            else
            {
                //Response.Redirect("CustomerSales.aspx");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void GetHeaderInfo()
    {
        lblPurchaseOrder.Text = "";
        lblPaymentDue.Text = "";

        double dFreight = 0;
        double dFr = 0;
        double dUL = 0;
        double totalll = 0;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet dsBill = new DataSet();

        DataSet billSales = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;

        string branchCode = Request.QueryString["BID"].ToString();
        int billNo = Convert.ToInt32(Request.QueryString["SID"].ToString());

        dsBill = bl.GetSalesForId(billNo, branchCode);
        //dsBill = bl.GetSalesForId(1);

        int customerID = 0;
        string custAdd = string.Empty;

        if ((dsBill != null) && (dsBill.Tables[0].Rows.Count > 0))
        {
            foreach (DataRow dr in dsBill.Tables[0].Rows)
            {
                //lblBillno.Text = Convert.ToString(dr["billno"]);                
                //lblBillDate.Text = Convert.ToDateTime(dr["billdate"]).ToShortDateString();               

                lblSupplierName.Text = Convert.ToString(dr["Customername"]);
                lblSupplierNameEx.Text = Convert.ToString(dr["Customername"]);

                lblShipToName.Text = Convert.ToString(dr["Customername"]);
                lblShipToNameEx.Text = Convert.ToString(dr["Customername"]);

                lblSupplierCmpnyName.Text = Convert.ToString(dr["Customername"]);
                lblSupplierCmpnyNameEx.Text = Convert.ToString(dr["Customername"]);

                lblShipToCmpnyName.Text = Convert.ToString(dr["Customername"]);
                lblShipToCmpnyNameEx.Text = Convert.ToString(dr["Customername"]);

                custAdd = Convert.ToString(dr["CustomerAddress"]);
                string[] address = new string[3] { "", "", "" };
                address = custAdd.Split(new char[] { '\n' });

                string address2 = string.Empty;
                address2 = Convert.ToString(dr["CustomerAddress2"]);

                string address3 = string.Empty;
                address3 = Convert.ToString(dr["CustomerAddress3"]);

                if (address.Length >= 1)
                {
                    if (address[0] != string.Empty)
                    {
                        lblSupplierAddr1.Text = address[0];
                        lblShipToAddr1.Text = address[0];
                    }
                }

                if (address2 != null)
                {
                    lblSupplierAddr2.Text = address2;
                    lblShipToAddr2.Text = address2;
                }

                if (address3 != null)
                {
                    //lblSuppAdd3.Text = address3;
                }

                lblSupplierPhn.Text = Convert.ToString(dr["CustomerContacts"]);
                lblSupplierPhnEx.Text = Convert.ToString(dr["CustomerContacts"]);

                lblShipToPhn.Text = Convert.ToString(dr["CustomerContacts"]);
                lblShipToPhnEx.Text = Convert.ToString(dr["CustomerContacts"]);

                lblBillDate.Text = Convert.ToString(dr["BillDate"]);
                lblBillDateEx.Text = Convert.ToString(dr["BillDate"]);

                lblInvoice.Text = Convert.ToString(dr["BillNo"]);
                lblInvoiceEx.Text = Convert.ToString(dr["BillNo"]);

                lblCustomerID.Text = Convert.ToString(dr["CustomerID"]);
                lblCustomerIDEx.Text = Convert.ToString(dr["CustomerID"]);

                if (dr["paymode"].ToString() == "1")
                {
                    lblPayMode.Text = "Cash";
                    divBankPaymode.Visible = false;

                    lblPayModeEx.Text = "Cash";
                    divBankPaymodeEx.Visible = false;
                }
                else if (dr["paymode"].ToString() == "2")
                {
                    lblPayMode.Text = "Bank / Credit Card";
                    divBankPaymode.Visible = true;
                    divCreditCardNo.InnerHtml = dr["CreditCardNo"].ToString();
                    divBankName.InnerHtml = dr["Debtor"].ToString();

                    lblPayModeEx.Text = "Bank / Credit Card";
                    divBankPaymodeEx.Visible = true;
                    divCreditCardNoEx.InnerHtml = dr["CreditCardNo"].ToString();
                    divBankNameEx.InnerHtml = dr["Debtor"].ToString();
                }
                else if (dr["paymode"].ToString() == "3")
                {
                    lblPayMode.Text = "Cheque";
                    divBankPaymode.Visible = false;

                    lblPayModeEx.Text = "Cheque";
                    divBankPaymodeEx.Visible = false;
                }

                //if (dr["MultiPayment"].ToString() == "YES")
                //{
                //    lblPayMode.Text = "Multipayment";
                //    divMultiPayment.Visible = true;
                //    GrdViewReceipt.DataSource = bl.ListReceiptsForBillNoOrder(dr["billno"].ToString());
                //    //GrdViewReceipt.DataSource = bl.ListReceiptsForBillNo(dr["billno"].ToString());
                //    GrdViewReceipt.DataBind();

                //    lblPayModeEx.Text = "Multipayment";
                //    divMultiPaymentEx.Visible = true;
                //    GrdViewReceiptEx.DataSource = bl.ListReceiptsForBillNoOrder(dr["billno"].ToString());
                //    //GrdViewReceipt.DataSource = bl.ListReceiptsForBillNo(dr["billno"].ToString());
                //    GrdViewReceiptEx.DataBind();
                //}
                //else
                //{
                //    divMultiPayment.Visible = false;
                //    divMultiPaymentEx.Visible = false;
                //}
            }

            if ((dsBill != null) && (dsBill.Tables[0].Rows.Count > 0))
            {
                dt = new DataTable();

                dc = new DataColumn("SalesPerson");
                dt.Columns.Add(dc);

                dc = new DataColumn("ShippingMethod");
                dt.Columns.Add(dc);

                dc = new DataColumn("ShippingTerms");
                dt.Columns.Add(dc);

                dc = new DataColumn("PaymentMode");
                dt.Columns.Add(dc);

                dc = new DataColumn("DueDate");
                dt.Columns.Add(dc);

                dc = new DataColumn("DeliveryDate");
                dt.Columns.Add(dc);

                billSales.Tables.Add(dt);

                DataTable table = dsBill.Tables[0];
                DataRow row = table.Rows[0];

                drNew = dt.NewRow();

                drNew["SalesPerson"] = GetEmployeeName(billNo, branchCode);

                drNew["ShippingMethod"] = string.Empty;
                drNew["ShippingTerms"] = string.Empty;

                int payMode = Convert.ToInt32(row["Paymode"]);
                string paymentMode = string.Empty;
                if (payMode == 1)
                {
                    paymentMode = "Cash";
                }
                else if (payMode == 2)
                {
                    paymentMode = "Credit Card";
                }
                else if (payMode == 3)
                {
                    paymentMode = "Cheque";
                }
                else
                {
                    paymentMode = "Multi-Payment";
                }


                drNew["PaymentMode"] = paymentMode;

                drNew["DueDate"] = string.Empty;
                drNew["DeliveryDate"] = string.Empty;

                billSales.Tables[0].Rows.Add(drNew);
            }

            gvGeneral.Visible = true;
            gvGeneral.DataSource = billSales;
            gvGeneral.DataBind();

            gvGeneralEx.Visible = true;
            gvGeneralEx.DataSource = billSales;
            gvGeneralEx.DataBind();
        }
    }

    public void GetSalesDetailsA4Format(int salesID, string branchCode)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetSalesItemsForPurId(salesID, branchCode);

        if (ds != null)
        {

            dt = new DataTable();

            dc = new DataColumn("ProductName");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductDesc");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductItem");
            dt.Columns.Add(dc);

            dc = new DataColumn("Particulars");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("Unit");
            dt.Columns.Add(dc);

            dc = new DataColumn("Discount");
            dt.Columns.Add(dc);

            dc = new DataColumn("VAT");
            dt.Columns.Add(dc);

            dc = new DataColumn("VATAmount");
            dt.Columns.Add(dc);

            dc = new DataColumn("CST");
            dt.Columns.Add(dc);

            dc = new DataColumn("NetRate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rate");
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

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    drNew = dt.NewRow();
                    if (dr["itemCode"] != null)
                    {
                        itemCode = Convert.ToString(dr["ItemCode"]);
                        sParticulars = bl.getBillProductName(itemCode);
                        measureUnit = bl.getBillProductUnit(itemCode);
                    }
                    salesDs = bl.GetProductSalesBill(salesID, itemCode);
                    qty = Convert.ToDouble(salesDs.Tables[0].Rows[0]["Quantity"]);
                    if (dr["Rate"] != null)
                    {
                        dRate = Convert.ToDouble(salesDs.Tables[0].Rows[0]["Rate"]);
                    }
                    if (salesDs.Tables[0].Rows[0]["SumVat"] != null)
                    {
                        dNetRate = Convert.ToDouble(salesDs.Tables[0].Rows[0]["SumVat"]) / qty;
                    }

                    dTotal = dRate * qty;

                    drNew["Particulars"] = sParticulars;
                    drNew["ProductName"] = Convert.ToString(dr["ProductName"]);
                    drNew["ProductDesc"] = Convert.ToString(dr["ProductDesc"]);

                    drNew["ProductItem"] = Convert.ToString(dr["ProductName"]) + " - " + Convert.ToString(dr["ProductDesc"]);

                    drNew["Rate"] = dRate.ToString("f2");
                    drNew["NetRate"] = dNetRate.ToString("f2");
                    drNew["Bundles"] = Convert.ToString(dr["Bundles"]);
                    drNew["Rods"] = Convert.ToString(dr["Rods"]);
                    drNew["Qty"] = Convert.ToString(qty);
                    drNew["Unit"] = measureUnit;
                    drNew["Amount"] = dTotal;
                    drNew["CST"] = Convert.ToString(dr["CST"]);
                    drNew["VAT"] = Convert.ToString(dr["VAT"]);
                    drNew["VATAmount"] = Convert.ToString(dr["Vatamount"]);

                    drNew["Discount"] = Convert.ToString(dr["Discount"]);

                    billDs.Tables[0].Rows.Add(drNew);
                }

                if (billDs.Tables[0].Rows.Count < 10)
                {
                    int currRowCnt = billDs.Tables[0].Rows.Count;

                    for (int i = currRowCnt; i < 10; i++)
                    {
                        drNew = dt.NewRow();

                        drNew["Particulars"] = string.Empty;
                        drNew["ProductName"] = string.Empty;
                        drNew["ProductDesc"] = string.Empty;
                        drNew["Rate"] = string.Empty;
                        drNew["NetRate"] = string.Empty;
                        drNew["Bundles"] = string.Empty;
                        drNew["Rods"] = string.Empty;
                        drNew["Qty"] = string.Empty;
                        drNew["Unit"] = string.Empty;
                        drNew["Amount"] = string.Empty;
                        drNew["CST"] = string.Empty;
                        drNew["VAT"] = string.Empty;
                        drNew["VATAmount"] = string.Empty;

                        drNew["Discount"] = string.Empty;

                        billDs.Tables[0].Rows.Add(drNew);
                    }
                }
            }

            gvItem.Visible = true;
            gvItem.DataSource = billDs;
            gvItem.DataBind();

            gvItemEx.Visible = true;
            gvItemEx.DataSource = billDs;
            gvItemEx.DataBind();
        }
    }

    public void GetTotalSales(int BillNo, string BranchCode)
    {
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetSalesForId(BillNo, BranchCode);

        //GetSalesItemsForIdRet - dicount , vatTotal, lblSubTotal(mrp) 


        int payMode;

        if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {                
                lblTotal.Text = Convert.ToString(dr["Total"]);

                payMode = Convert.ToInt32(dr["PayMode"]);                
            }
        }

        ds1 = bl.GetSalesItemsForIdRet(BillNo, BranchCode);

       // GetSalesItemsForIdRet - dicount , vatTotal, lblSubTotal(mrp) 

        if ((ds1 != null) && (ds1.Tables[0].Rows.Count > 0))
        {
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                lblSubTotal.Text = Convert.ToString(dr["TotalMrp"]);
              //lblSalesTax.Text = Convert.ToString(dr["Tax"]);

                lblDiscount.Text = Convert.ToString(dr["Discount"]);                
            }
        }
        

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Session["NEWSALES"] = "Y";
            //Session["BillDate"] = lblBillDate.Text;
            Response.Redirect("CustomerSales.aspx");
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

    private void BindCurrencyLabels()
    {
        DataSet appSettings;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "CURRENCY")
                {
                    currency = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }

        if (currency == "INR")
        {
            //lblCurrTotal.Text = "(INR)";
            //lblCurrVAT.Text = "(INR)";
            //lblCurrLoad.Text = "(INR)";
            //lblCurrDisp.Text = "(INR)";
            //lblCurrCST.Text = "(INR)";
            //lblCurrGrandTTL.Text = "(INR)";
            currencyType = "Rs";
        }

        if (currency == "GBP")
        {
            //lblCurrTotal.Text = "(£)";
            //lblCurrVAT.Text = "(£)";
            //lblCurrLoad.Text = "(£)";
            //lblCurrDisp.Text = "(£)";
            //lblCurrCST.Text = "(£)";
            //lblCurrGrandTTL.Text = "(£)";
            currencyType = "£";
        }

    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        try
        {
            int iBillno = Convert.ToInt32(Request.QueryString["SID"].ToString());
            string branchCode = Request.QueryString["BID"].ToString();

            GetHeaderInfo();
            GetSalesDetailsA4Format(iBillno, branchCode);
            GetTotalSales(iBillno, branchCode);
            GetCustomerCareDetails();
            A4FORMAT.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadDivisions()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListDivisions();
        //ds.Tables[0].Rows[0].Delete();
        ddDivsions.DataSource = ds;
        ddDivsions.DataBind();
        ddDivsions.DataTextField = "DivisionName";
        ddDivsions.DataValueField = "DivisionID";
        ddDivsions.SelectedIndex = 1;
    }

    protected void ddDivsions_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int iBillno = Convert.ToInt32(Request.QueryString["SID"].ToString());

            string branchCode = Request.QueryString["BID"].ToString();

            FillDivision();

            GetSalesDetailsA4Format(iBillno, branchCode);
            GetTotalSales(iBillno, branchCode);
            GetCustomerCareDetails();
            A4FORMAT.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvGeneral_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double sumNet = 0;
            double dFr = 0;
            double vat = 0;
            double cst = 0;
            double discount = 0;
            double purchaseRate = 0;
            double currDis = 0;
            double currVat = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "VAT") != "")
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                //if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                //    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));
                if (DataBinder.Eval(e.Row.DataItem, "Discount") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "Discount") != "")
                    discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "Rate") != "")
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                //if (vat > 0)
                   // lblSalesTaxRate.Text = vat + " % ";
                //if (cst > 0)
                //    lblSalesTaxRate.Text = cst + " % ";

                vat += vat;
               
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //dFr = Convert.ToDouble(lblFg.Text);

                sumNet = dDis + vatTotal + dFr + dCST;

                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dTot.ToString("f2");

                lblSalesTax.Text = vat.ToString();              

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvGeneral_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    private void GetCustomerCareDetails()
    {
        lblCustName.Text = "Customer Care";
        lblCustPhn.Text = "89400 09090, 97879 70707";
        lblCustMailID.Text = "customercare@benitco.com";
        lblCustTiming.Text = "Mon to Sat, 10am to 6pm.";
    }

    private string GetEmployeeName(int billNo, string branchCode)
    {
        string empName = string.Empty;


        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataTable dt = bl.GetSalesItemsForIdRet(billNo, branchCode).Tables[0];
        DataRow row = dt.Rows[0];

        int empNo = Convert.ToInt32(row["executivename"].ToString());

        var empDetails = bl.GetEmployeeDetails(empNo);

        empName = Convert.ToString(empDetails.Tables[0].Rows[0]["empFirstName"]);

        return empName;
    }

    private void FillDivision()
    {

        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet companyInfo = new DataSet();

        companyInfo = bl.GetDivisionForId(sDataSource, int.Parse(ddDivsions.SelectedValue));

        if (companyInfo != null)
        {
            if (companyInfo.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in companyInfo.Tables[0].Rows)
                {
                    lblCompany.Text = Convert.ToString(dr["DivisionName"]);
                    lblCompanyEx.Text = Convert.ToString(dr["DivisionName"]);

                    lblPhn.Text = Convert.ToString(dr["Phone"]);
                    lblPhnEx.Text = Convert.ToString(dr["Phone"]);

                    lblAddress.Text = Convert.ToString(dr["Address"]);
                    lblAddressEx.Text = Convert.ToString(dr["Address"]);

                    lblCity.Text = Convert.ToString(dr["city"]);
                    lblCityEx.Text = Convert.ToString(dr["city"]);

                    lblPincode.Text = Convert.ToString(dr["Pincode"]);
                    lblPincodeEx.Text = Convert.ToString(dr["Pincode"]);

                    lblState.Text = Convert.ToString(dr["state"]);
                    lblStateEx.Text = Convert.ToString(dr["state"]);
                }
            }
        }
    }

    protected void gvGeneralEx_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvItemEx_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double sumNet = 0;
            double dFr = 0;
            double vat = 0;
            double cst = 0;
            double discount = 0;
            double purchaseRate = 0;
            double currDis = 0;
            double currVat = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "VAT") != "")
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                //if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                //    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));
                if (DataBinder.Eval(e.Row.DataItem, "Discount") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "Discount") != "")
                    discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "Rate") != "")
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                if (vat > 0)
                    lblSalesTaxRateEx.Text = vat + " % ";
                //if (cst > 0)
                //    lblSalesTaxRate.Text = cst + " % ";

                dTot = dTot + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dVat = dVat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate"));
                dRat = dRat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                dQty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                currDis = GetDiscount(dQty, purchaseRate, discount);
                dDis = dDis + currDis;
                dCST = dCST + GetCSTVAT(currDis, cst);
                disTot = disTot + ((dQty * purchaseRate) * (discount / 100));
                cstTotal = cstTotal + dCST;
                currVat = GetCSTVAT(currDis, vat);
                vatTotal = vatTotal + currVat;
                amtTotal = amtTotal + GetSum(currDis, vat, cst);
                sumVat = sumVat + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate")) - Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"))) * dQty;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //dFr = Convert.ToDouble(lblFg.Text);

                sumNet = dDis + vatTotal + dFr + dCST;

                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dTot.ToString("f2");

                lblSalesTaxEx.Text = Math.Abs(vatTotal).ToString("f2");

                lblDiscountEx.Text = disTot.ToString("f2");

                //lblGrandCst.Text = dCST.ToString("f2");

                lblTotalEx.Text = String.Format("{0:0,0}", sumNet);
                //lblCurrRs.Text = currencyType + " " + String.Format("{0:0,0}", sumNet);

               // lblSubTotalEx.Text = dTot.ToString("f2");

                if (dDis > 0)
                    lblDiscountEx.Visible = true;
                else
                    lblDiscountEx.Visible = false;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void PrintDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (PrintDropDownList.SelectedValue == "2")
        {
            divPrint.Visible = false;
            divPrintEx.Visible = true;
        }
        else if (PrintDropDownList.SelectedValue == "1")
        {
            divPrint.Visible = true;
            divPrintEx.Visible = false;
        }
    }
}
