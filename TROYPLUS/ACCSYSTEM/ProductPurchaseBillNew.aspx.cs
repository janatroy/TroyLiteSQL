using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductPurchaseBillNew : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    public double dTot = 0;
    public double dVat = 0;
    public double dRat = 0;
    public double dNet = 0;
    public double dQty = 0;
    public double sumVat = 0;
    public double freight = 0;
    public double loadunload = 0;
    public double tot = 0;
    public double disamt = 0;
    public double disamnt = 0;
    public double purchaserate=0;

    public double vatinclusiverate3;
    public double vatinclusiverate1;
    public double vatinclusiverate5;
    public double vatinclusiverate6;
    public double vatinclusiverate7;
    public double vatinclusiverate4;


    public double vatinclusiverate3j;
    public double vatinclusiverate1j;
    public double vatinclusiverate5j;
    public double vatinclusiverate6j;
    public double vatinclusiverate7j;
    public double vatinclusiverate4j;

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

    private bool isvalid = false;

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
                                lblHead.Text = Convert.ToString(dr["CompanyName"]);
                                // Label5.Text = Convert.ToString(dr["CompanyName"]);
                                lblPhone.Text = Convert.ToString(dr["Phone"]);
                                lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                                lblAddress.Text = Convert.ToString(dr["Address"]);
                                lblCity.Text = Convert.ToString(dr["city"]) + "-" + Convert.ToString(dr["Pincode"]);
                                // lblPincode.Text = Convert.ToString(dr["Pincode"]);
                                lblState.Text = Convert.ToString(dr["state"]);

                            }
                        }
                    }
                }

                if (ddDivsions.Items.Count == 1)
                {
                    lblDivisions.Visible = false;
                    ddDivsions.Visible = false;
                    divDiv.Visible = false;
                }
                else
                {
                    lblDivisions.Visible = false;
                    ddDivsions.Visible = true;
                    divDiv.Visible = false;
                }
            }

            if (Request.QueryString["SID"] != null)
            {
                iBillno = Convert.ToInt32(Request.QueryString["SID"].ToString());
                string branchCode = Request.QueryString["BID"].ToString();

                //if (ddDivsions.SelectedIndex != 0)
                //{
                FillDivision();
                //}

                GetHeaderInfo();
                GetPurchaseDetailsA4Format(iBillno, branchCode);
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

        dsBill = bl.GetPurchaseForpurchaseId(billNo, branchCode);

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

                //  lblShipToName.Text = Convert.ToString(dr["Customername"]);
                lblShipToNameEx.Text = Convert.ToString(dr["Customername"]);

                lblSupplierAddr1.Text = Convert.ToString(dr["CustomerAddress"]);
                lblSupplierAddr1.Text = Convert.ToString(dr["CustomerAddress"]);

                // lblShipToAddr1.Text = Convert.ToString(dr["CustomerAddress"]);
                // lblShipToAddr1.Text = Convert.ToString(dr["CustomerAddress"]);

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
                        // lblShipToAddr1.Text = address[0];
                    }
                }

                if (address2 != null)
                {
                    lblSupplierAddr2.Text = address2;
                    // lblShipToAddr2.Text = address2;
                }

                if (address3 != null)
                {
                    //lblSuppAdd3.Text = address3;
                }

                lblSupplierPhn.Text = Convert.ToString(dr["CustomerIdMobile"]);
                lblSupplierPhnEx.Text = Convert.ToString(dr["CustomerIdMobile"]);

                // lblShipToPhn.Text = Convert.ToString(dr["CustomerIdMobile"]);
                lblShipToPhnEx.Text = Convert.ToString(dr["CustomerIdMobile"]);

                lblBillDate.Text = Convert.ToDateTime(dr["billdate"]).ToString("dd/MM/yyyy");
                lblBillDateEx.Text = Convert.ToString(dr["BillDate"]);

                lblInvoice.Text = branchCode + "-" + Convert.ToString(dr["BillNo"]);
                lblInvoiceEx.Text = branchCode + "-" + Convert.ToString(dr["BillNo"]);

                lblCustomerID.Text = Convert.ToDateTime(dr["Invoicedate"]).ToString("dd/MM/yyyy");
                lblCustomerIDEx.Text = Convert.ToString(dr["Invoicedate"]);

              //  lblCustomerID.Text = Convert.ToString(dr["CreditorID"]);
               // lblCustomerIDEx.Text = Convert.ToString(dr["CreditorID"]);

                lblPaymentDue.Text = Convert.ToString(dr["Transno"]);

                lblPurchaseOrder.Text = Convert.ToString(dr["ponumber"]);

                lblpurchaseid.Text = Convert.ToString(dr["PurchaseId"]);

                Label62.Text = "(Used for DC Pur Ret)";


                lblPayMode.Text = Convert.ToString(dr["narration2"]);

                //if (dr["paymode"].ToString() == "1")
                //{
                //    lblPayMode.Text = "Cash";
                //    divBankPaymode.Visible = false;

                //    lblPayModeEx.Text = "Cash";
                //    divBankPaymodeEx.Visible = false;
                //}
                //else if (dr["paymode"].ToString() == "2")
                //{
                //    lblPayMode.Text = "Bank / Credit Card";
                //    divBankPaymode.Visible = true;
                //    divCreditCardNo.InnerHtml = dr["ChequeNo"].ToString();
                //    divBankName.InnerHtml = dr["Creditor"].ToString();

                //    lblPayModeEx.Text = "Bank / Credit Card";
                //    divBankPaymodeEx.Visible = true;
                //    divCreditCardNoEx.InnerHtml = dr["ChequeNo"].ToString();
                //    divBankNameEx.InnerHtml = dr["Creditor"].ToString();
                //}
                //else if (dr["paymode"].ToString() == "3")
                //{
                //    lblPayMode.Text = "Credit";
                //    divBankPaymode.Visible = false;

                //    lblPayModeEx.Text = "Credit";
                //    divBankPaymodeEx.Visible = false;
                //}

                //if (dr["MultiPayment"].ToString() == "YES")
                //{
                //    lblPayMode.Text = "Multipayment";
                //    divMultiPayment.Visible = true;
                //    GrdViewReceipt.DataSource = bl.ListReceiptsForBillNoOrder(dr["billno"].ToString(), dr["Branchcode"].ToString());
                //    //GrdViewReceipt.DataSource = bl.ListReceiptsForBillNo(dr["billno"].ToString());
                //    GrdViewReceipt.DataBind();

                //    lblPayModeEx.Text = "Multipayment";
                //    divMultiPaymentEx.Visible = true;
                //    GrdViewReceiptEx.DataSource = bl.ListReceiptsForBillNoOrder(dr["billno"].ToString(), dr["Branchcode"].ToString());
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

                drNew["SalesPerson"] = GetEmployeeName(Convert.ToInt32(row["CreditorID"]));

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

            gvGeneral.Visible = false;
            gvGeneral.DataSource = billSales;
            gvGeneral.DataBind();

            gvGeneralEx.Visible = false;
            gvGeneralEx.DataSource = billSales;
            gvGeneralEx.DataBind();
        }
    }

    public void GetPurchaseDetailsA4Format(int salesID, string branchCode)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetPurchaseItemsForId(salesID, branchCode);

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

            dc = new DataColumn("SalesPerson");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("TotalPrice");
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

            //dc = new DataColumn("PurchaseDiscount");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("discamt");
            //dt.Columns.Add(dc);

            billDs.Tables.Add(dt);

            string itemCode = string.Empty;
            string sParticulars = string.Empty;
            double dRate = 0;
            double dVAT = 0;
            double dVATAmt = 0;
            double dDisc = 0;
            double dNetRate = 0;
            double dTotprice = 0;
            int iBundles = 0;
            int iRods = 0;
            double qty = 0;
            double dAmout = 0;
            double dVat = 0;
            double rate1 = 0;
            double rate2 = 0;
            double rate3 = 0;

            double dFreight = 0;
            double dTotal = 0;
            string measureUnit = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    drNew = dt.NewRow();
                    if (ds.Tables[0].Rows[i]["itemCode"] != null)
                    {
                        itemCode = Convert.ToString(ds.Tables[0].Rows[i]["itemCode"]);
                        sParticulars = bl.getBillProductName(itemCode);
                        measureUnit = bl.getBillProductUnit(itemCode);
                    }

                    salesDs = bl.GetProductPurchaseBill(salesID, itemCode);

                    //qty = Convert.ToDouble(salesDs.Tables[0].Rows[0]["Quantity"]);

                    if (ds.Tables[0].Rows[i]["Qty"] != null)
                    {
                        qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                    }
                    else
                    {
                        qty = 0;
                    }

                    if (salesDs.Tables[0].Rows[0]["PurchaseRate"] != null)
                    {
                        dRate = Convert.ToDouble(salesDs.Tables[0].Rows[0]["PurchaseRate"]);
                    }

                    if (salesDs.Tables[0].Rows[0]["PurchaseDiscount"] != null)
                    {
                        rate1 = Convert.ToInt32(salesDs.Tables[0].Rows[0]["PurchaseDiscount"]);
                    }

                    if (salesDs.Tables[0].Rows[0]["discamt"] != null)
                    {
                        rate2 = Convert.ToInt32(salesDs.Tables[0].Rows[0]["discamt"]);
                    }

                    rate3 = rate1 - rate2;

                    if (salesDs.Tables[0].Rows[0]["SumVat"] != null)
                    {
                        dNetRate = Convert.ToDouble(salesDs.Tables[0].Rows[0]["SumVat"]) / qty;
                    }

                    if (ds.Tables[0].Rows[i]["VAT"] != null)
                    {
                        dVAT = Convert.ToDouble(ds.Tables[0].Rows[i]["VAT"]);
                    }

                    //if (ds.Tables[0].Rows[i]["VATAmount"] != null)
                    //{
                    dVATAmt = rate3 *dVAT/100;
                    //}

                    if (ds.Tables[0].Rows[i]["Discount"] != null)
                    {
                        dDisc = Convert.ToDouble(ds.Tables[0].Rows[i]["Discount"]);
                    }

                    //if (ds.Tables[0].Rows[i]["TotalPrice"] != null)
                    //{
                    //    dTotprice = Convert.ToDouble(ds.Tables[0].Rows[i]["TotalPrice"]);
                    //}

                    //if (ds.Tables[0].Rows[i]["Totalmrp"] != null)
                    //{
                    //    dAmout = Convert.ToDouble(ds.Tables[0].Rows[i]["Totalmrp"]);
                    //}


                    dTotal = dRate * qty;

                    drNew["Particulars"] = sParticulars;
                    drNew["ProductName"] = Convert.ToString(ds.Tables[0].Rows[i]["ProductName"]);
                    drNew["ProductDesc"] = Convert.ToString(ds.Tables[0].Rows[i]["ProductDesc"]);

                    drNew["ProductItem"] = Convert.ToString(ds.Tables[0].Rows[i]["ProductDesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]) + " - " + Convert.ToString(ds.Tables[0].Rows[i]["Model"]);

                    drNew["SalesPerson"] = GetEmployeeName(Convert.ToInt32(ds.Tables[0].Rows[i]["SupplierID"]));

                    drNew["Rate"] = dRate.ToString("f2");
                    drNew["NetRate"] = dNetRate.ToString("f2");
                    //drNew["Bundles"] = Convert.ToString(ds.Tables[0].Rows[i]["Bundles"]);
                    //drNew["Rods"] = Convert.ToString(ds.Tables[0].Rows[i]["Rods"]);
                    drNew["Qty"] = Convert.ToString(qty);
                   // drNew["Unit"] = measureUnit;
                    drNew["TotalPrice"] = dTotal.ToString("f2");
                    drNew["Amount"] = dTotal.ToString("f2");  //dTotal;
                    drNew["CST"] = Convert.ToString(ds.Tables[0].Rows[i]["CST"]);
                    drNew["VAT"] = dVAT.ToString("f1");  //Convert.ToString(dr["VAT"]);;
                    drNew["VATAmount"] = dVATAmt.ToString("f2");

                    drNew["Discount"] = Convert.ToString(ds.Tables[0].Rows[i]["Discount"]);// dDisc.ToString("f2");

                    billDs.Tables[0].Rows.Add(drNew);
                }

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    drNew = dt.NewRow();
                //    if (dr["itemCode"] != null)
                //    {
                //        itemCode = Convert.ToString(dr["ItemCode"]);
                //        sParticulars = bl.getBillProductName(itemCode);
                //        measureUnit = bl.getBillProductUnit(itemCode);
                //    }
                //    salesDs = bl.GetProductSalesBill(salesID, itemCode);
                //    qty = Convert.ToDouble(salesDs.Tables[0].Rows[0]["Quantity"]);
                //    if (dr["Rate"] != null)
                //    {
                //        dRate = Convert.ToDouble(salesDs.Tables[0].Rows[0]["Rate"]);
                //    }
                //    if (salesDs.Tables[0].Rows[0]["SumVat"] != null)
                //    {
                //        dNetRate = Convert.ToDouble(salesDs.Tables[0].Rows[0]["SumVat"]) / qty;
                //    }

                //    if (dr["VAT"] != null)
                //    {
                //        dVAT = Convert.ToDouble(ds.Tables[0].Rows[0]["VAT"]);
                //    }

                //    if (dr["VATAmount"] != null)
                //    {
                //        dVATAmt = Convert.ToDouble(ds.Tables[0].Rows[0]["VATAmount"]);
                //    }

                //    if (dr["Discount"] != null)
                //    {
                //        dDisc = Convert.ToDouble(ds.Tables[0].Rows[0]["Discount"]);
                //    }

                //    if (dr["TotalPrice"] != null)
                //    {
                //        dTotprice = Convert.ToDouble(ds.Tables[0].Rows[0]["TotalPrice"]);
                //    }

                //    if (dr["Totalmrp"] != null)
                //    {
                //        dAmout = Convert.ToDouble(ds.Tables[0].Rows[0]["Totalmrp"]);
                //    }


                //    dTotal = dRate * qty;

                //    drNew["Particulars"] = sParticulars;
                //    drNew["ProductName"] = Convert.ToString(dr["ProductName"]);
                //    drNew["ProductDesc"] = Convert.ToString(dr["ProductDesc"]);

                //    drNew["ProductItem"] = Convert.ToString(dr["ProductName"]) + " - " + Convert.ToString(dr["ProductDesc"]);

                //    drNew["SalesPerson"] = GetEmployeeName(Convert.ToInt32(dr["executivename"]));

                //    drNew["Rate"] = dRate.ToString("f2");
                //    drNew["NetRate"] = dNetRate.ToString("f2");
                //    drNew["Bundles"] = Convert.ToString(dr["Bundles"]);
                //    drNew["Rods"] = Convert.ToString(dr["Rods"]);
                //    drNew["Qty"] = Convert.ToString(qty);
                //    drNew["Unit"] = measureUnit;
                //    drNew["TotalPrice"] = dTotprice.ToString("f2");
                //    drNew["Amount"] = dTotal.ToString("f2");  //dTotal;
                //    drNew["CST"] = Convert.ToString(dr["CST"]);
                //    drNew["VAT"] = dVAT.ToString("#0.00");  //Convert.ToString(dr["VAT"]);;
                //    drNew["VATAmount"] = dVATAmt.ToString("f2");

                //    drNew["Discount"] = Convert.ToString(dr["Discount"]);// dDisc.ToString("f2");

                //    billDs.Tables[0].Rows.Add(drNew);
                //}

                if (billDs.Tables[0].Rows.Count < 20)
                {
                    int currRowCnt = billDs.Tables[0].Rows.Count;

                    for (int i = currRowCnt; i < 20; i++)
                    {
                        drNew = dt.NewRow();

                        drNew["Particulars"] = string.Empty;
                        drNew["ProductName"] = string.Empty;
                        drNew["ProductDesc"] = string.Empty;
                        drNew["SalesPerson"] = string.Empty;
                        drNew["Rate"] = string.Empty;
                        drNew["NetRate"] = string.Empty;
                        drNew["Bundles"] = string.Empty;
                        drNew["Rods"] = string.Empty;
                        drNew["Qty"] = string.Empty;
                        drNew["TotalPrice"] = string.Empty;
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

        ds = bl.GetPurchaseForIdnewprint(BillNo, BranchCode);

        //GetSalesItemsForIdRet - dicount , vatTotal, lblSubTotal(mrp) 
        double sumNet = 0;
        double dFr = 0;
        double vat = 0;
        double cst = 0;
        double discount = 0;
        double discamt = 0;
        double purchaseRate = 0;
        double currDis = 0;
        double currVat = 0;
        double disTotamt = 0;
        double damt = 0;

        // int payMode;

        //  double vat;
       
        // double cst;

        // double loadunload;

        double dis;
        double qty;
        if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
        {

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                tot = Convert.ToDouble(dr["TotalAmt"]);
                freight = Convert.ToDouble(dr["Freight"]);
                loadunload = Convert.ToDouble(dr["LoadUnLoad"]);
                lblTotal.Text = tot.ToString("#0.00");// Convert.ToString(dr["Total"]);
                lblTotalEx.Text = tot.ToString("#0.00");
                //  payMode = Convert.ToInt32(dr["PayMode"]);
                vat = Convert.ToDouble(dr["VAT"]);
                purchaserate =  Convert.ToDouble(dr["PurchaseRate"]);
                cst = Convert.ToDouble(dr["CST"]);

                disamnt = Convert.ToDouble(dr["discamount"]);
                disamt = Convert.ToDouble(dr["discamt"]);
                dis = Convert.ToDouble(dr["discount"]);
                qty = Convert.ToDouble(dr["Qty"]);

                damt = ((Convert.ToDouble(dr["TotalWORndOff"]) - Convert.ToDouble(dr["discamount"])));

                damt = (((Convert.ToDouble(dr["TotalWORndOff"]) - Convert.ToDouble(dr["discamount"])) * (Convert.ToDouble(dr["discper"]) / 100)));

                if (Convert.ToString(dr["internaltransfer"]) == "YES")
                {
                    Label10.Text = "Internal Transfer Purchase Note";
                    Label11.Text = "Original";
                }
                else if (Convert.ToString(dr["DeliveryNote"]) == "YES")
                {
                    Label10.Text = "Delivery Note";
                    Label11.Text = "Original";

                }
                else if (Convert.ToString(dr["SalesReturn"]) == "YES")
                {
                    Label10.Text = "Sales Return";
                    Label11.Text = "Original";

                }
                else
                {
                    Label10.Text = "Purchase Invoice";
                    Label11.Text = "Original";
                }

                currDis = GetDiscount(qty, purchaserate, dis, disamt);
              
                
                dDis = dDis + currDis;
               
                
                dCST = dCST + GetCSTVAT(currDis, cst);
               
                
                disTot = disTot + ((qty * purchaserate) * (dis / 100));
               
                
                disTotamt = disTotamt + disamt;
               
                
                cstTotal = cstTotal + dCST;
              
                
                currVat = GetCSTVAT(currDis, vat);
               
                
                vatTotal = vatTotal + currVat;
               
                
                amtTotal = amtTotal + GetSum(currDis, vat, cst);
                // freight = freight + Convert.ToDouble(dr["Freight"]);
                //  loadunload = loadunload + Convert.ToDouble(dr["Freight"]);


                //   if (txtQty.Text == "") txtQty.Text = "0";
                //   if (txtRtnQty.Text == "") txtRtnQty.Text = "0";
                //  if (txtRate.Text == "") txtRate.Text = "0";
                //  if (txtNLP.Text == "") txtNLP.Text = "0";
                //  if (txtDisPre.Text == "") txtDisPre.Text = "0";
                //  if (txtVATPre.Text == "") txtVATPre.Text = "0";
                //  if (txtCSTPre.Text == "") txtCSTPre.Text = "0";
                //  if (txtVATPre.Text == "0.00" && txtCSTPre.Text == "0.00") inpHide.Value = "1";


                //  if (txtDiscAmt.Text == "") txtDiscAmt.Text = "0";
                vatinclusiverate3 = vatinclusiverate3 + Convert.ToDouble(purchaserate) * Convert.ToDouble(qty);
                vatinclusiverate1 = vatinclusiverate1 + Convert.ToDouble(vatinclusiverate3) - (Convert.ToDouble(vatinclusiverate3) * Convert.ToDouble(dis) / 100);
                vatinclusiverate5 = vatinclusiverate5 + Convert.ToDouble(vatinclusiverate1) - Convert.ToDouble(disamt);
                vatinclusiverate6 = vatinclusiverate6  + (Convert.ToDouble(vatinclusiverate5) * Convert.ToDouble(vat) / 100);
                //vatinclusiverate7 = vatinclusiverate7 + (Convert.ToDouble(vatinclusiverate5) * Convert.ToDouble(cst) / 100);
                //vatinclusiverate4 = vatinclusiverate4 + Convert.ToDouble(vatinclusiverate5) + Convert.ToDouble(vatinclusiverate6) + Convert.ToDouble(vatinclusiverate7);
                // txtTotal.Text = vatinclusiverate4.ToString("#0.00");

                //vatinclusiverate3j = vatinclusiverate3 + vatinclusiverate3;
                //vatinclusiverate1j = vatinclusiverate1 + vatinclusiverate1;
                //vatinclusiverate5j = vatinclusiverate5 + vatinclusiverate5;
                //vatinclusiverate6j = vatinclusiverate6 + vatinclusiverate6;
                //vatinclusiverate7j = vatinclusiverate7 + vatinclusiverate7;
                //vatinclusiverate4j = vatinclusiverate4 + vatinclusiverate4;


                if (dDis > 0)
                    dvDiscountTotal.Visible = true;
                else
                    dvDiscountTotal.Visible = false;
                //}
                if (disTotamt > 0)
                    DvDiscAmt.Visible = true;
                else
                    DvDiscAmt.Visible = false;

                if (dCST > 0)
                    dvCSTTotal.Visible = true;
                else
                    dvCSTTotal.Visible = false;

                if (damt == -1)
                {
                    Label4.Text = "0";
                    Div6.Visible = false;
                }
                else
                {
                    if (Convert.ToDouble(damt) > 0)
                    {
                        Label8.Text = damt.ToString("#0.00");
                        Div6.Visible = true;
                        Label7.Text = Convert.ToString(dr["discper"]) + " %)";
                    }
                    else
                    {
                        Div6.Visible = false;
                    }
                }

                //if (Convert.ToDouble(lblGrandVat.Text) > 0)
                 //   dvVatTotal.Visible = true;
             //   else
                 //   dvVatTotal.Visible = false;
                
                if ((freight > 0) || (loadunload > 0))
                    dvFrgTotal.Visible = true;
                else
                    dvFrgTotal.Visible = false;
            }
            lblAmt.Text = vatinclusiverate3.ToString("#0.00");
            lblGrandDiscount.Text =disTot.ToString("#0.00");
            lbldiscamt.Text = disTotamt.ToString("#0.00");
            lblGrandVat.Text =vatTotal.ToString("#0.00");
            lblFg.Text =(Convert.ToDouble(freight) + Convert.ToDouble(loadunload)).ToString("#0.00");
            lblGrandCst.Text = Convert.ToString(dCST);
            // Label6.Text=Convert.ToString(amtTotal);
         //   Label8.Text = Convert.ToString(amtTotal);
            dicsamntlbl.Text = disamnt.ToString("#0.00");
        }
        



        ds1 = bl.GetSalesItemsForIdRet(BillNo, BranchCode); //change


        if ((ds1 != null) && (ds1.Tables[0].Rows.Count > 0))
        {
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                //lblSubTotal.Text = Convert.ToString(dr["TotalMrp"]);
                //lblSalesTax.Text = Convert.ToString(dr["Tax"]);

                //lblDiscount.Text = Convert.ToString(dr["Discount"]);
            }
        }


    }

    public double GetSum(double rate, double vat, double cst)
    {
        double tot = 0;
        tot = rate + (rate * (vat / 100)) + (rate * (cst / 100));
        return tot;
    }
    public double GetDiscount(double qty, double rate, double discount, double discamt)
    {
        double dis = 0;
        dis = (qty * rate) - ((qty * rate) * (discount / 100)) - discamt;
        return dis;
    }
    public double GetCSTVAT(double rate, double cst)
    {

        cst = (rate * (cst / 100));
        return cst;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Session["NEWPURCHASE"] = "Y";
            //Session["BillDate"] = lblBillDate.Text;
            Response.Redirect("purchase.aspx");
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
            GetPurchaseDetailsA4Format(iBillno, branchCode);
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

        ds = bl.GetBranchDivisions();

        ds.Tables[0].Rows[0].Delete();

        //ds.Tables[0].Rows[0].Delete();

        ddDivsions.DataSource = ds;
        ddDivsions.DataBind();
        ddDivsions.DataTextField = "BranchName";
        ddDivsions.DataValueField = "BranchID";
        ddDivsions.SelectedIndex = 1;
    }

    protected void ddDivsions_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int iBillno = Convert.ToInt32(Request.QueryString["SID"].ToString());

            string branchCode = Request.QueryString["BID"].ToString();

            FillDivision();

            GetPurchaseDetailsA4Format(iBillno, branchCode);
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
                {
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                }
                else
                {
                    vat = 0;
                }
                //if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                //    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));


                if (DataBinder.Eval(e.Row.DataItem, "Discount") == "" || Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Discount")) == 0)
                {
                    //discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                    if (isvalid != true)
                    {
                       // gvItem.Columns[7].Visible = false;
                    }
                    //discountLbl.Visible = false;
                }
                else
                {
                   // gvItem.Columns[7].Visible = true;
                    isvalid = true;
                }

                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "Rate") != "")
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                //if (vat > 0)
                // lblSalesTaxRate.Text = vat + " % ";
                //if (cst > 0)
                //    lblSalesTaxRate.Text = cst + " % ";

                dVat = dVat + vat;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //dFr = Convert.ToDouble(lblFg.Text);

                if (isvalid == false)
                {
                   // gvItem.Columns[7].Visible = false;
                    //  discountLbl.Visible = false;
                }

                sumNet = dDis + vatTotal + dFr + dCST;

                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dTot.ToString("f2");

                //lblSalesTax.Text = dVat.ToString();
                dVat = 0;
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

    private string GetEmployeeName(int empNo)
    {
        string empName = string.Empty;

        BusinessLogic bl = new BusinessLogic(sDataSource);

        //var empDetails = bl.supplierName(sDataSource, empNo);

        empName = bl.supplierName(sDataSource, empNo); ;

        return empName;
    }

    private void FillDivision()
    {

        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet companyInfo = new DataSet();
        string branchCode = Request.QueryString["BID"].ToString();
        companyInfo = bl.GetBranchDetailsForId(sDataSource, branchCode);

        if (companyInfo != null)
        {
            if (companyInfo.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in companyInfo.Tables[0].Rows)
                {
                    lblCompany.Text = Convert.ToString(dr["BranchName"]);
                    lblCompanyEx.Text = Convert.ToString(dr["BranchName"]);

                    lblAddress1.Text = Convert.ToString(dr["BranchAddress1"]);
                    lblAddress1Ex.Text = Convert.ToString(dr["BranchAddress1"]);

                    lblAddress2.Text = Convert.ToString(dr["BranchAddress2"]);
                    lblAddress2Ex.Text = Convert.ToString(dr["BranchAddress2"]);

                    if (dr["BranchAddress3"].ToString() == "")
                    {
                        address3.Visible = false;
                        address3Ex.Visible = false;

                    }
                    else
                    {
                        lblAddress3.Text = Convert.ToString(dr["BranchAddress3"]);
                        lblAddress3Ex.Text = Convert.ToString(dr["BranchAddress3"]);
                    }

                    lblLocation.Text = Convert.ToString(dr["BranchLocation"]);
                    lblLocationEx.Text = Convert.ToString(dr["BranchLocation"]);

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
                {
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                }
                else
                {
                    vat = 0;
                }
                //if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                //    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));


                if (DataBinder.Eval(e.Row.DataItem, "Discount") == "" || Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Discount")) == 0)
                {
                    //discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                    if (isvalid != true)
                    {
                        gvItemEx.Columns[7].Visible = false;
                    }
                    //discountLbl.Visible = false;
                }
                else
                {
                    gvItemEx.Columns[7].Visible = true;
                    isvalid = true;
                }

                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "Rate") != "")
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                //if (vat > 0)
                // lblSalesTaxRate.Text = vat + " % ";
                //if (cst > 0)
                //    lblSalesTaxRate.Text = cst + " % ";

                dVat = dVat + vat;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //dFr = Convert.ToDouble(lblFg.Text);

                if (isvalid == false)
                {
                    gvItemEx.Columns[7].Visible = false;
                    //  discountLbl.Visible = false;
                }

                sumNet = dDis + vatTotal + dFr + dCST;

                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dTot.ToString("f2");

                //lblSalesTax.Text = dVat.ToString();
                dVat = 0;
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