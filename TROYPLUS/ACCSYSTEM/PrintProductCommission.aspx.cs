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

public partial class PrintProductCommission : System.Web.UI.Page
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


            if (Request.QueryString["RT"] == "NO")
            {
                btnBack.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                lblDivisions.Visible = true;
                ddDivsions.Visible = true;
                divDiv.Visible = true;

            }

            if (Request.QueryString["SID"] != null)
            {
                iBillno = Convert.ToInt32(Request.QueryString["SID"].ToString());
                GetHeaderInfo();
                string BillFormat = bl.getConfigInfo();

                if (BillFormat == "STEEL CEMENT")
                {
                    GENREALFORMAT.Visible = true;
                    viswasfooter600.Visible = true;
                    string billForm = bl.GetBillFormat(iBillno);
                    GetSalesProductDetailsVishwas(iBillno, billForm);
                }
                else if (BillFormat == "PREPRINTED GENERAL")
                {
                    GENREALFORMAT.Visible = true;
                    dvHeader600.Visible = false;
                    dvFooter600.Visible = false;
                    GetSalesProductDetails(iBillno);
                }
                else if (BillFormat == "JANDJ BILL FORMAT")
                {
                    GENREALFORMAT.Visible = true;
                    dvHeader600.Visible = false;
                    dvFooter600.Visible = false;
                    dvJandJFormatHeader.Visible = true;
                    GetSalesProdDetailsJJFormat(iBillno);
                }
                else if (BillFormat == "A4FORMAT")
                {
                    A4FORMAT.Visible = true;
                    GetSalesDetailsA4Format(iBillno);
                }
                else if (BillFormat == "PREPRINTED FORMAT 2")
                {
                    PREPRINTEDFORMAT2.Visible = true;
                    dvHeader.Visible = false;
                    dvFooter.Visible = false;
                    dvHeaderPF2.Visible = true;
                    lblBillCopy.Visible = false;
                    divFooterPF2.Visible = true;
                    dvDiscountTotal.Visible = false;
                    dvVatTotal.Visible = false;
                    dvFrgTotal.Visible = false;
                    dvCSTTotal.Visible = false;
                    dvPF2.Visible = true;
                    GetSalesProductDetailsPF2(iBillno);
                }
                else if (BillFormat == "FORMAT 3")
                {
                    gvFormat3.Visible = true;
                    GetSalesProductDetailsF3(iBillno);
                }
                else if (BillFormat == "GENERAL FORMAT 1")
                {
                    GENREALFORMAT.Visible = true;
                    dvGeneral600.Visible = true;
                    trTINCST600.Visible = false;
                    trTINCST1600.Visible = true;
                    trCST600.Visible = false;
                    trVATTAX600.Visible = false;
                    GetSalesProductDetails(iBillno);
                }
                else if (BillFormat == "GENERAL FORMAT 2")
                {
                    GENREALFORMAT.Visible = true;
                    dvGeneral600.Visible = true;
                    trTINCST600.Visible = true;
                    //trTINCST1.Visible = true;
                    trCST600.Visible = false;
                    trVATTAX600.Visible = false;
                    GetSalesProductDetailsGN2(iBillno);
                }
                else
                {
                    GENREALFORMAT.Visible = true;
                    //trTINCST600.Visible = true;
                    dvGeneral600.Visible = true;
                    GetSalesDetails600(iBillno);
                    dvTotal600.Visible = true;
                    dvFooter600.Visible = true;
                }
                lblBillCopy.Text = "Customer Copy";
            }
            else
            {
                Response.Redirect("CommissionMngmt.aspx");
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

        double dComm = 0;

        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet dsBill = new DataSet();
        dsBill = bl.GetCommissionForId(Convert.ToInt32(Request.QueryString["SID"].ToString()));

        int customerID = 0;
        string custAdd = string.Empty;

        if ((dsBill != null) && (dsBill.Tables[0].Rows.Count > 0))
        {
            foreach (DataRow dr in dsBill.Tables[0].Rows)
            {

                lblBillno.Text = Convert.ToString(dr["CommissionNo"]);
                lbljjBillno.Text = Convert.ToString(dr["CommissionNo"]);
                lblBillno600.Text = Convert.ToString(dr["CommissionNo"]);
                lblBillNo1600.Text = Convert.ToString(dr["CommissionNo"]);
                lblBillnoPF2.Text = Convert.ToString(dr["CommissionNo"]);
                lblBillDate.Text = Convert.ToDateTime(dr["Commissiondate"]).ToShortDateString();
                lbljjBillDate.Text = Convert.ToDateTime(dr["Commissiondate"]).ToShortDateString();
                lblBillDate600.Text = Convert.ToDateTime(dr["Commissiondate"]).ToShortDateString();
                lblBillDatePF2.Text = Convert.ToDateTime(dr["Commissiondate"]).ToShortDateString();
                //lblSupplier.Text = Convert.ToString(dr["Customername"]);
                //lblSupplier.Text = Convert.ToString(dr["OtherCusName"]);// krishnavelu 26 June
                //lblReason.Text = Convert.ToString(dr["PurchaseReturnReason"]);


                    lblHeader.Text = "";


                customerID = Convert.ToInt32(dr["CustomerID"]);
                if (dr["Freight"] != null && dr["Freight"] != DBNull.Value)
                {
                    dFr = Convert.ToDouble(dr["freight"]);
                }
                if (dr["LoadUnload"] != null && dr["LoadUnload"] != DBNull.Value)
                {
                    dUL = Convert.ToDouble(dr["LoadUnload"]);
                }
                if (dr["Comissionvalue"] != null && dr["Comissionvalue"] != DBNull.Value)
                {
                    dComm = Convert.ToDouble(dr["Comissionvalue"]);
                }
                dFreight = dFreight + dFr + dUL;

                
                lblFg.Text = dFreight.ToString("f2");
                lblFg600.Text = dFreight.ToString("f2");
                lblcomm.Text = dComm.ToString("f2");

                DataSet dsCust = bl.GetLedgerInfoForId(sDataSource, customerID);

                if (dsCust != null)
                {
                    lblCustTIN.Text = dsCust.Tables[0].Rows[0]["TinNumber"].ToString();
                    lblSuppTin.Text = dsCust.Tables[0].Rows[0]["TinNumber"].ToString();
                    lblCustTIN600.Text = dsCust.Tables[0].Rows[0]["TinNumber"].ToString();
                    lbljjCustTIN600.Text = dsCust.Tables[0].Rows[0]["TinNumber"].ToString();
                }

                if (Convert.ToString(dr["CustomerName"]) != "OTHERS")
                {
                    lblSupplier.Text = Convert.ToString(dr["CustomerName"]);
                    lblSupplier600.Text = Convert.ToString(dr["CustomerName"]);
                    lbljjSupplier600.Text = Convert.ToString(dr["CustomerName"]);
                    //lblSupplierPF2.Text = Convert.ToString(dr["ContactName"]);
                }
                else
                {
                    lblSupplier.Text = Convert.ToString(dr["OtherCusName"]);
                    lblSupplier600.Text = Convert.ToString(dr["OtherCusName"]);
                    lbljjSupplier600.Text = Convert.ToString(dr["OtherCusName"]);
                    //lblSupplierPF2.Text = Convert.ToString(dr["ContactName"]);
                }
                //lblSuppPh.Text = Convert.ToString(dr["CustomerContacts"]);
                //lblSuppPh600.Text = Convert.ToString(dr["CustomerContacts"]);
                //lbljjSuppPh600.Text = Convert.ToString(dr["CustomerContacts"]);

                //custAdd = Convert.ToString(dr["CustomerAddress"]);
                string[] address = new string[3] { "", "", "" };
                address = custAdd.Split(new char[] { '\n' });

                if (address.Length >= 1)
                {
                    if (address[0] != string.Empty)
                    {
                        lblSuppAdd1.Text = address[0];
                        lbljjSuppAdd1600.Text = address[0];
                        lblSuppAdd1600.Text = address[0];
                    }
                }

                if (address.Length >= 2)
                {
                    if (address[1] != string.Empty)
                    {
                        lblSuppAdd2.Text = address[1];
                        lbljjSuppAdd2600.Text = address[1];
                        lblSuppAdd2600.Text = address[1];
                    }
                }

                if (address.Length >= 3)
                {
                    if (address[2] != string.Empty)
                    {
                        lblSuppAdd3.Text = address[2];
                        lbljjSuppAdd3600.Text = address[2];
                        lblSuppAdd3600.Text = address[2];
                    }
                }
                if (address.Length >= 1)
                {
                    if (address[0] != string.Empty)
                        lblSuppAdd1PF2.Text = address[0];
                }
                if (address.Length >= 2)
                {
                    if (address[1] != string.Empty)
                        lblSuppAdd2PF2.Text = address[1];
                }
                if (address.Length >= 3)
                {
                    if (address[2] != string.Empty)
                        lblSuppAdd3PF2.Text = address[2];
                }

                //lblSuppPhPF2.Text = Convert.ToString(dr["CustomerContacts"]);

                string notes = "";

                //if (dr["DeliveryNote"].ToString() == "YES")
                //{
                //    notes = notes + "Delivery Note (Only For Service)" + "</br>";
                //}
                //if (dr["InternalTransfer"].ToString() == "YES")
                //{
                //    notes = notes + "Internal Transfer (Transfer between Branches)" + "</br>";
                //}
                //if (dr["purchasereturn"].ToString() == "YES")
                //{
                //    notes = notes + "Purchase Return" + "</br>";
                //}

                //divNotes.InnerHtml = notes;

                //if (dr["DeliveryNote"].ToString() == "YES")
                //{
                //    divInvoiceType.InnerHtml = "<b>" + "DELIVERY NOTE (Only For Service)" + "</b>";
                //}
                //else if (dr["InternalTransfer"].ToString() == "YES")
                //{
                //    divInvoiceType.InnerHtml = "<b>" + "INTERNAL TRANSFER (Transfer between Branches)" + "</b>";
                //}
                //else if (dr["purchasereturn"].ToString() == "YES")
                //{
                //    divInvoiceType.InnerHtml = "<b>" + "PURCHASE RETURN" + "</b>";
                //}
                //else
                //{
                //    divInvoiceType.InnerHtml = "<b>" + "VAT TAX INVOICE" + "</b>";
                //}

                divInvoiceType.InnerHtml = "<b>" + "Sales Bill" + "</b>";

                if (dr["Sellingpaymode"].ToString() == "1")
                {
                    paymode.InnerHtml = "Payment Mode: Cash";
                    divBankPaymode.Visible = false;
                }
                else if (dr["Sellingpaymode"].ToString() == "2")
                {
                    paymode.InnerHtml = "Payment Mode: Bank / Credit Card";
                    divBankPaymode.Visible = true;
                    divCreditCardNo.InnerHtml = dr["CusCardNo"].ToString();
                    divBankName.InnerHtml = dr["CustomerName"].ToString();
                }
                else if (dr["Sellingpaymode"].ToString() == "3")
                {
                    paymode.InnerHtml = "Payment Mode: Credit";
                    divBankPaymode.Visible = false;
                }

                //if (dr["MultiPayment"].ToString() == "YES")
                //{
                //    paymode.InnerHtml = "Payment Mode: Multipayment";
                //    divMultiPayment.Visible = true;
                //    GrdViewReceipt.DataSource = bl.ListReceiptsForBillNo(dr["billno"].ToString());
                //    GrdViewReceipt.DataBind();
                //}
                //else
                //{
                //    divMultiPayment.Visible = false;
                //}
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
                        lblTNGST600.Text = Convert.ToString(dr["TINno"]);
                        lblTinNumberPF2.Text = Convert.ToString(dr["TINno"]);
                        lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                        lblCompany600.Text = Convert.ToString(dr["CompanyName"]);
                        lblPhone.Text = Convert.ToString(dr["Phone"]);
                        lblPhone600.Text = Convert.ToString(dr["Phone"]);
                        lblGSTno.Text = Convert.ToString(dr["GSTno"]);
                        lblGSTno600.Text = Convert.ToString(dr["GSTno"]);
                        lblCSTNumberPF2.Text = Convert.ToString(dr["GSTno"]);
                        lblSignCompany.Text = Convert.ToString(dr["CompanyName"]);
                        lblSignCompany600.Text = Convert.ToString(dr["CompanyName"]);
                        lblAddress.Text = Convert.ToString(dr["Address"]);
                        lblAddress600.Text = Convert.ToString(dr["Address"]);
                        lblCity.Text = Convert.ToString(dr["city"]);
                        lblCity600.Text = Convert.ToString(dr["city"]);
                        lblPincode.Text = Convert.ToString(dr["Pincode"]);
                        lblPincode600.Text = Convert.ToString(dr["Pincode"]);
                        lblState.Text = Convert.ToString(dr["state"]);
                        lblState600.Text = Convert.ToString(dr["state"]);

                    }
                }
            }
        }



        /*
        DataSet dsSupplier = bl.getAddressInfo(customerID);
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
                    lblSuppAdd1PF2.Text = Convert.ToString(dr["Add1"]);
                    lblSuppAdd2PF2.Text = Convert.ToString(dr["Add2"]);
                    lblSuppAdd3PF2.Text = Convert.ToString(dr["Add3"]);
                    lblSuppPhPF2.Text = Convert.ToString(dr["Phone"]);
                    lblSuppTin.Text = Convert.ToString(dr["Tinnumber"]);
                    if (dr["Contactname"].ToString() != string.Empty)
                    {
                        lblSupplier.Text = Convert.ToString(dr["ContactName"]);
                        lblSupplierPF2.Text = Convert.ToString(dr["ContactName"]);
                    
                }
            }
        }*/

    }

    protected void GrdViewReceipt_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewReceipt, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    public void GetSalesProductDetailsVishwas(int salesID, string sType)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetSalesItemsForId(salesID);

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

            dc = new DataColumn("VAT");
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
                    drNew["Rate"] = dRate.ToString("f2"); // ("f2");
                    drNew["NetRate"] = dNetRate.ToString("f4"); // ("f2");
                    drNew["Bundles"] = Convert.ToString(dr["Bundles"]);
                    drNew["Rods"] = Convert.ToString(dr["Rods"]);
                    drNew["Qty"] = Convert.ToString(qty);
                    drNew["Unit"] = measureUnit;
                    drNew["CST"] = Convert.ToString(dr["CST"]);
                    drNew["VAT"] = Convert.ToString(dr["VAT"]);

                    drNew["Discount"] = Convert.ToString(dr["Discount"]);
                    drNew["Amount"] = dTotal;
                    //Session["PrintUnit"] = Convert.ToString(dr["Measure_Unit"]);
                    billDs.Tables[0].Rows.Add(drNew);
                }
            }
            if (sType == "STEEL")
            {

                gvViswas.Visible = true;
                gvViswas.DataSource = billDs;
                gvViswas.DataBind();
                dvGeneral.Visible = false;
            }
            else
            {
                viswasfooter.Visible = true;
                //dvGeneral.Visible = true;
                gvVishwasCement.Visible = true;
                gvVishwasCement.DataSource = billDs;
                gvVishwasCement.DataBind();
            }
        }

    }


    public void GetSalesProductDetailsGN2(int salesID)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetSalesItemsForId(salesID);

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

            dc = new DataColumn("VAT");
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
            double qty = 0;
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
                    drNew["Rate"] = dRate.ToString("f2");
                    drNew["NetRate"] = dNetRate.ToString("f2");
                    drNew["Bundles"] = Convert.ToString(dr["Bundles"]);
                    drNew["Rods"] = Convert.ToString(dr["Rods"]);
                    drNew["Qty"] = Convert.ToString(qty);
                    drNew["Unit"] = measureUnit;
                    drNew["Amount"] = dTotal;
                    drNew["CST"] = Convert.ToString(dr["CST"]);
                    drNew["VAT"] = Convert.ToString(dr["VAT"]);

                    drNew["Discount"] = Convert.ToString(dr["Discount"]);

                    billDs.Tables[0].Rows.Add(drNew);
                }
            }

            gvGenNoUnitRate.Visible = true;
            gvGenNoUnitRate.DataSource = billDs;
            gvGenNoUnitRate.DataBind();


        }

    }

    public void GetSalesProductDetails(int salesID)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetSalesItemsForId(salesID);

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

            dc = new DataColumn("VAT");
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
                    drNew["Rate"] = dRate.ToString("f2");
                    drNew["NetRate"] = dNetRate.ToString("f2");
                    drNew["Bundles"] = Convert.ToString(dr["Bundles"]);
                    drNew["Rods"] = Convert.ToString(dr["Rods"]);
                    drNew["Qty"] = Convert.ToString(qty);
                    drNew["Unit"] = measureUnit;
                    drNew["Amount"] = dTotal;
                    drNew["CST"] = Convert.ToString(dr["CST"]);
                    drNew["VAT"] = Convert.ToString(dr["VAT"]);

                    drNew["Discount"] = Convert.ToString(dr["Discount"]);

                    billDs.Tables[0].Rows.Add(drNew);
                }
            }

            gvGeneral600.Visible = true;
            gvGeneral600.DataSource = billDs;
            gvGeneral600.DataBind();


        }

    }

    public void GetSalesProdDetailsJJFormat(int salesID)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetSalesItemsForId(salesID);

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

            dc = new DataColumn("VAT");
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
            double qty = 0;
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
                    drNew["Rate"] = dRate.ToString("f2");
                    drNew["NetRate"] = dNetRate.ToString("f2");
                    drNew["Bundles"] = Convert.ToString(dr["Bundles"]);
                    drNew["Rods"] = Convert.ToString(dr["Rods"]);
                    drNew["Qty"] = Convert.ToString(qty);
                    drNew["Unit"] = measureUnit;
                    drNew["Amount"] = dTotal;
                    drNew["CST"] = Convert.ToString(dr["CST"]);
                    drNew["VAT"] = Convert.ToString(dr["VAT"]);

                    drNew["Discount"] = Convert.ToString(dr["Discount"]);

                    billDs.Tables[0].Rows.Add(drNew);
                }
            }

            gvJJFormat.Visible = true;
            gvJJFormat.DataSource = billDs;
            gvJJFormat.DataBind();


        }

    }

    public void GetSalesDetails600(int salesID)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetCommissionItemsForId(salesID);

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

            dc = new DataColumn("VAT");
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
                    salesDs = bl.GetProductCommissionBill(salesID, itemCode);
                    qty = Convert.ToDouble(dr["Qty"]);
                    dRate = Convert.ToDouble(dr["Rate"]);
                    //if (dr["Rate"] != null)
                    //{
                    //    dRate = Convert.ToDouble(salesDs.Tables[0].Rows[0]["Rate"]);
                    //}
                    //if (salesDs.Tables[0].Rows[0]["SumVat"] != null)
                    //{
                    //    dNetRate = Convert.ToDouble(salesDs.Tables[0].Rows[0]["SumVat"]) / qty;
                    //}

                    dTotal = dRate * qty;

                    drNew["Particulars"] = sParticulars;
                    drNew["Rate"] = dRate.ToString("f2");
                    drNew["NetRate"] = dNetRate.ToString("f2");
                    drNew["Bundles"] = "0";
                    drNew["Rods"] = "0";
                    drNew["Qty"] = Convert.ToString(qty);
                    drNew["Unit"] = "0";
                    drNew["Amount"] = dTotal.ToString("f2");
                    drNew["CST"] = "0";
                    drNew["VAT"] = "0";

                    drNew["Discount"] = "0";

                    billDs.Tables[0].Rows.Add(drNew);
                }
            }

            gvGeneral600.Visible = true;
            gvGeneral600.DataSource = billDs;
            gvGeneral600.DataBind();


        }

    }

    public void GetSalesDetailsA4Format(int salesID)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetCommissionItemsForId(salesID);

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

            dc = new DataColumn("VAT");
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
                    drNew["Rate"] = dRate.ToString("f2");
                    drNew["NetRate"] = dNetRate.ToString("f2");
                    drNew["Bundles"] = Convert.ToString(dr["Bundles"]);
                    drNew["Rods"] = Convert.ToString(dr["Rods"]);
                    drNew["Qty"] = Convert.ToString(qty);
                    drNew["Unit"] = measureUnit;
                    drNew["Amount"] = dTotal;
                    drNew["CST"] = Convert.ToString(dr["CST"]);
                    drNew["VAT"] = Convert.ToString(dr["VAT"]);

                    drNew["Discount"] = Convert.ToString(dr["Discount"]);

                    billDs.Tables[0].Rows.Add(drNew);
                }
            }

            gvGeneral.Visible = true;
            gvGeneral.DataSource = billDs;
            gvGeneral.DataBind();


        }

    }

    //KRISHNAVELU 12 - JULY - 2010
    public void GetSalesProductDetailsF3(int salesID)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetCommissionItemsForId(salesID);

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

            dc = new DataColumn("VAT");
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
                    drNew["Rate"] = dRate.ToString("f2");
                    drNew["NetRate"] = dNetRate.ToString("f2");
                    drNew["Bundles"] = Convert.ToString(dr["Bundles"]);
                    drNew["Rods"] = Convert.ToString(dr["Rods"]);
                    drNew["Qty"] = Convert.ToString(qty);
                    drNew["Unit"] = measureUnit;
                    drNew["Amount"] = dTotal;
                    drNew["CST"] = Convert.ToString(dr["CST"]);
                    drNew["VAT"] = Convert.ToString(dr["VAT"]);

                    drNew["Discount"] = Convert.ToString(dr["Discount"]);

                    billDs.Tables[0].Rows.Add(drNew);
                }
            }

            gvFormat3.Visible = true;
            gvFormat3.DataSource = billDs;
            gvFormat3.DataBind();


        }

    }

    //END KRISHNAVELU 12 - JULY - 2010
    public void GetSalesProductDetailsPF2(int salesID)
    {
        DataSet ds = new DataSet();
        DataSet billDs = new DataSet();
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        DataSet salesDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetCommissionItemsForId(salesID);

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

            dc = new DataColumn("VAT");
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
                    drNew["Rate"] = dRate.ToString("f2");
                    drNew["NetRate"] = dNetRate.ToString("f2");
                    drNew["Bundles"] = Convert.ToString(dr["Bundles"]);
                    drNew["Rods"] = Convert.ToString(dr["Rods"]);
                    drNew["Qty"] = Convert.ToString(qty);
                    drNew["Unit"] = measureUnit;
                    drNew["Amount"] = dTotal;
                    drNew["CST"] = Convert.ToString(dr["CST"]);
                    drNew["VAT"] = Convert.ToString(dr["VAT"]);

                    drNew["Discount"] = Convert.ToString(dr["Discount"]);

                    billDs.Tables[0].Rows.Add(drNew);
                }
            }
            int cnt = 10 - billDs.Tables[0].Rows.Count;
            for (int i = 0; i < cnt; i++)
            {
                drNew = dt.NewRow();
                drNew["Particulars"] = "";
                drNew["Rate"] = "";
                drNew["NetRate"] = "";
                drNew["Bundles"] = "";
                drNew["Rods"] = "";
                drNew["Qty"] = "";
                drNew["Unit"] = "";
                drNew["Amount"] = "";
                drNew["CST"] = "";
                drNew["VAT"] = "";

                drNew["Discount"] = "";
                billDs.Tables[0].Rows.Add(drNew);
            }
            gvGeneralPF2.Visible = true;
            gvGeneralPF2.DataSource = billDs;
            gvGeneralPF2.DataBind();


        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Session["NEWSALES"] = "Y";
            Session["BillDate"] = lblBillDate.Text;
            Response.Redirect("CommissionMngmt.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvVishwas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //double sumNet = 0;
            //double dFr = 0;
            //double vat = 0;


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    dTot = dTot + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            //    dVat = dVat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate"));
            //    dRat = dRat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
            //    dQty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
            //    if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value)
            //        vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
            //    if(vat >0)
            //    lblVatDisplay.Text = vat + " % "; 
            //    sumVat = sumVat + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate")) - Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"))) * dQty;
            //}
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    dFr = Convert.ToDouble(lblFg.Text);

            //    sumNet = dTot + sumVat + dFr;

            //    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            //    e.Row.Cells[6].Text = dTot.ToString("f2");
            //    lblGrandVat.Text = Math.Abs(sumVat).ToString("f2");
            //    lblNetTotal.Text = sumNet.ToString("f2");
            //    lblRs.Text = sumNet.ToString("N2");
            //    lblAmt.Text = dTot.ToString("f2");

            //    //if (dCST > 0)
            //    //    dvCSTTotal.Visible = true;
            //    //else
            //    //    dvCSTTotal.Visible = false;

            //    if (Convert.ToDouble(lblGrandVat.Text) > 0)
            //        dvVatTotal.Visible = true;
            //    else
            //        dvVatTotal.Visible = false;

            //    if (dFr > 0)
            //        dvFrgTotal.Visible = true;
            //    else
            //        dvFrgTotal.Visible = false;

            //}
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
                if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value)
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));
                if (DataBinder.Eval(e.Row.DataItem, "Discount") != DBNull.Value)
                    discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value)
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                if (vat > 0)
                    lblVatDisplay.Text = vat + " % ";
                if (cst > 0)
                    lblCSTDisplay.Text = cst + " % ";

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
                dFr = Convert.ToDouble(lblFg.Text);

                sumNet = dDis + vatTotal + dFr + dCST;

                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = dTot.ToString("f2");
                lblGrandVat.Text = Math.Abs(vatTotal).ToString("f2");
                lblGrandDiscount.Text = disTot.ToString("f2");
                lblGrandCst.Text = dCST.ToString("f2");
                lblNetTotal.Text = sumNet.ToString("f2"); // sumNet.ToString("f2");
                //lblNetTotal.Text = String.Format("{0:0,0}", sumNet);       
                lblCurrRs.Text = currencyType + sumNet.ToString("N2");
                lblAmt.Text = dTot.ToString("f2");

                if (dDis > 0)
                    dvDiscountTotal.Visible = true;
                else
                    dvDiscountTotal.Visible = false;

                if (dCST > 0)
                    dvCSTTotal.Visible = true;
                else
                    dvCSTTotal.Visible = false;

                if (Convert.ToDouble(lblGrandVat.Text) > 0)
                    dvVatTotal.Visible = true;
                else
                    dvVatTotal.Visible = false;

                if (dFr > 0)
                    dvFrgTotal.Visible = false;
                else
                    dvFrgTotal.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void gvVishwasCement_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //double sumNet = 0;
            //double dFr = 0;
            //double vat = 0;

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    dTot = dTot + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            //    dVat = dVat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate"));
            //    dRat = dRat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
            //    dQty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
            //    if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value)
            //        vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
            //    if (vat > 0)
            //        lblVatDisplay.Text = vat + " % "; 
            //    sumVat = sumVat + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate")) - Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"))) * dQty;
            //}
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    dFr = Convert.ToDouble(lblFg.Text);  

            //    sumNet = dTot + sumVat + dFr;

            //    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //    e.Row.Cells[4].Text = dTot.ToString("f2");
            //    lblGrandVat.Text = Math.Abs(sumVat).ToString("f2");
            //    lblNetTotal.Text = sumNet.ToString("f2");
            //    lblRs.Text = sumNet.ToString("N2");
            //    lblAmt.Text = dTot.ToString("f2");
            //    if (Convert.ToDouble(lblGrandVat.Text) > 0)
            //        dvVatTotal.Visible = true;
            //    else
            //        dvVatTotal.Visible = false;

            //    if (dFr > 0)
            //        dvFrgTotal.Visible = true;
            //    else
            //        dvFrgTotal.Visible = false;

            //}
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
                if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value)
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));
                if (DataBinder.Eval(e.Row.DataItem, "Discount") != DBNull.Value)
                    discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value)
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                if (vat > 0)
                    lblVatDisplay.Text = vat + " % ";
                if (cst > 0)
                    lblCSTDisplay.Text = cst + " % ";

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
                dFr = Convert.ToDouble(lblFg.Text);

                sumNet = dDis + vatTotal + dFr + dCST;

                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = dTot.ToString("f2");
                lblGrandVat.Text = Math.Abs(vatTotal).ToString("f2");
                lblGrandDiscount.Text = disTot.ToString("f2");
                lblGrandCst.Text = dCST.ToString("f2");
                lblNetTotal.Text = sumNet.ToString("f2"); // sumNet.ToString("f2");
                lblCurrRs.Text = currencyType + " " + sumNet.ToString("N2");
                lblAmt.Text = dTot.ToString("f2");

                if (dDis > 0)
                    dvDiscountTotal.Visible = true;
                else
                    dvDiscountTotal.Visible = false;

                if (dCST > 0)
                    dvCSTTotal.Visible = true;
                else
                    dvCSTTotal.Visible = false;

                if (Convert.ToDouble(lblGrandVat.Text) > 0)
                    dvVatTotal.Visible = true;
                else
                    dvVatTotal.Visible = false;

                if (dFr > 0)
                    dvFrgTotal.Visible = false;
                else
                    dvFrgTotal.Visible = false;
            }
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
            double purchaseRate = 0;
            double currDis = 0;
            double currVat = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value)
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));
                if (DataBinder.Eval(e.Row.DataItem, "Discount") != DBNull.Value)
                    discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value)
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                if (vat > 0)
                    lblVatDisplay.Text = vat + " % ";
                if (cst > 0)
                    lblCSTDisplay.Text = cst + " % ";

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
                dFr = Convert.ToDouble(lblFg.Text);

                sumNet = dDis + vatTotal + dFr + dCST;

                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dTot.ToString("f2");
                lblGrandVat.Text = Math.Abs(vatTotal).ToString("f2");
                lblGrandDiscount.Text = disTot.ToString("f2");
                lblGrandCst.Text = dCST.ToString("f2");
                //lblNetTotal.Text = sumNet.ToString("f2"); // sumNet.ToString("f2");
                lblNetTotal.Text = String.Format("{0:0,0}", sumNet);
                //lblCurrRs.Text = currencyType + " "+ sumNet.ToString("N2");
                lblCurrRs.Text = currencyType + " " + String.Format("{0:0,0}", sumNet);
                lblAmt.Text = dTot.ToString("f2");

                if (dDis > 0)
                    dvDiscountTotal.Visible = true;
                else
                    dvDiscountTotal.Visible = false;

                if (dCST > 0)
                    dvCSTTotal.Visible = true;
                else
                    dvCSTTotal.Visible = false;

                if (Convert.ToDouble(lblGrandVat.Text) > 0)
                    dvVatTotal.Visible = true;
                else
                    dvVatTotal.Visible = false;

                if (dFr > 0)
                    dvFrgTotal.Visible = false;
                else
                    dvFrgTotal.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvGeneral600_RowDataBound(object sender, GridViewRowEventArgs e)
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
            double sumNet2 = 0;
            double dcomm = 0;
            double dlu = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value)
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));
                if (DataBinder.Eval(e.Row.DataItem, "Discount") != DBNull.Value)
                    discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value)
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                if (vat > 0)
                    lblVatDisplay.Text = vat + " % ";
                if (cst > 0)
                    lblCSTDisplay.Text = cst + " % ";

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
                dFr = Convert.ToDouble(lblFg.Text);
                //dlu = Convert.ToDouble(lbllu.Text);
                dcomm = Convert.ToDouble(lblcomm.Text);
                sumNet = dDis;
                sumNet2 = dDis - dFr - dcomm;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dTot.ToString("f2");
                lblGrandVat600.Text = Math.Abs(vatTotal).ToString("f2");
                lblGrandDiscount600.Text = disTot.ToString("f2");
                lblGrandCst600.Text = dCST.ToString("f2");
                //lblNetTotal.Text = sumNet.ToString("f2"); // sumNet.ToString("f2");
                lblNetTotal600.Text = sumNet.ToString("f2");
                lblpur.Text = String.Format("{0:0,0}", sumNet2);
                //lblCurrRs.Text = currencyType + " "+ sumNet.ToString("N2");
                lblCurrRs600.Text = currencyType + " " + String.Format("{0:0,0}", sumNet);
                lblAmt600.Text = dTot.ToString("f2");

                if (dDis > 0)
                    dvDiscountTotal600.Visible = false;
                else
                    dvDiscountTotal600.Visible = false;

                if (dCST > 0)
                    dvCSTTotal600.Visible = false;
                else
                    dvCSTTotal600.Visible = false;

                if (Convert.ToDouble(lblGrandVat600.Text) > 0)
                    dvVatTotal600.Visible = false;
                else
                    dvVatTotal600.Visible = false;

                if (dFr > 0)
                    dvFrgTotal600.Visible = false;
                else
                    dvFrgTotal600.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvGenNoUnitRate_RowDataBound(object sender, GridViewRowEventArgs e)
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
            double sumNet2 = 0;
            double dcomm = 0;
            double dlu = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value)
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));
                if (DataBinder.Eval(e.Row.DataItem, "Discount") != DBNull.Value)
                    discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value)
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                if (vat > 0)
                    lblVatDisplay.Text = vat + " % ";
                if (cst > 0)
                    lblCSTDisplay.Text = cst + " % ";

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
                dFr = Convert.ToDouble(lblFg.Text);
                //dlu = Convert.ToDouble(lblFg.Text);
                dcomm = Convert.ToDouble(lblcomm.Text);
                sumNet = dDis;
                sumNet2 = dDis - dFr - dcomm;

                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dTot.ToString("f2");
                lblGrandVat600.Text = Math.Abs(vatTotal).ToString("f2");
                lblGrandDiscount600.Text = disTot.ToString("f2");
                lblGrandCst600.Text = dCST.ToString("f2");
                lblNetTotal600.Text = sumNet.ToString("f2"); // sumNet.ToString("f2");
                //lblNetTotal600.Text = String.Format("{0:0,0}", sumNet);
                lblpur.Text = String.Format("{0:0,0}", sumNet2);
                lblCurrRs600.Text = currencyType + " " + String.Format("{0:0,0}", sumNet);
                lblAmt600.Text = dTot.ToString("f2");

                if (dDis > 0)
                    dvDiscountTotal600.Visible = false;
                else
                    dvDiscountTotal600.Visible = false;

                if (dCST > 0)
                    dvCSTTotal600.Visible = false;
                else
                    dvCSTTotal600.Visible = false;

                if (Convert.ToDouble(lblGrandVat600.Text) > 0)
                    dvVatTotal600.Visible = false;
                else
                    dvVatTotal600.Visible = false;

                if (dFr > 0)
                    dvFrgTotal600.Visible = false;
                else
                    dvFrgTotal600.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //KRISHNAVELU 12 - JULY - 2010

    protected void gvFormat3_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value)
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value)
                    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));
                if (DataBinder.Eval(e.Row.DataItem, "Discount") != DBNull.Value)
                    discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value)
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                if (vat > 0)
                    lblVatDisplay.Text = vat + " % ";
                if (cst > 0)
                    lblCSTDisplay.Text = cst + " % ";

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
                dFr = Convert.ToDouble(lblFg.Text);

                sumNet = dDis + vatTotal + dFr + dCST;

                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dTot.ToString("f2");
                lblGrandVat.Text = Math.Abs(vatTotal).ToString("f2");
                lblGrandDiscount.Text = disTot.ToString("f2");
                lblGrandCst.Text = dCST.ToString("f2");
                lblNetTotal.Text = sumNet.ToString("f2"); // sumNet.ToString("f2");
                if ((Convert.ToDouble(lblGrandVat.Text) == 0) && (Convert.ToDouble(lblGrandCst.Text) == 0))
                {
                    lblCurrRs.Text = "Exempted " + currencyType + " " + sumNet.ToString("N2");
                }
                else
                {
                    lblCurrRs.Text = "" + currencyType + " " + sumNet.ToString("N2");
                }

                //lblRs.Text = "Rs. " + sumNet.ToString("N2");
                lblAmt.Text = dTot.ToString("f2");

                if (dDis > 0)
                    dvDiscountTotal.Visible = true;
                else
                    dvDiscountTotal.Visible = false;

                if (dCST > 0)
                    dvCSTTotal.Visible = true;
                else
                    dvCSTTotal.Visible = false;

                if (Convert.ToDouble(lblGrandVat.Text) > 0)
                    dvVatTotal.Visible = true;
                else
                    dvVatTotal.Visible = false;

                if (dFr > 0)
                    dvFrgTotal.Visible = false;
                else
                    dvFrgTotal.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    //END KRISHNAVELU 12 - JULY - 2010

    protected void gvGeneralPF2_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (DataBinder.Eval(e.Row.DataItem, "VAT") != DBNull.Value && Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VAT")) != "")
                    vat = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "VAT"));
                if (DataBinder.Eval(e.Row.DataItem, "CST") != DBNull.Value && Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CST")) != "")
                    cst = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CST"));
                if (DataBinder.Eval(e.Row.DataItem, "Discount") != DBNull.Value && Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Discount")) != "")
                    discount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                if (DataBinder.Eval(e.Row.DataItem, "Rate") != DBNull.Value && Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Rate")) != "")
                    purchaseRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                Label lblAmtt = (Label)e.Row.FindControl("lblAmtt");

                if (vat > 0)
                    lblVatDisplay.Text = vat + " % ";
                if (cst > 0)
                    lblCSTDisplay.Text = cst + " % ";
                if (DataBinder.Eval(e.Row.DataItem, "Amount") != "")
                    dTot = dTot + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                if (DataBinder.Eval(e.Row.DataItem, "NetRate") != "")
                    dVat = dVat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate"));
                if (DataBinder.Eval(e.Row.DataItem, "Rate") != "")
                    dRat = dRat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                if (DataBinder.Eval(e.Row.DataItem, "Qty") != "")
                    dQty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                currDis = GetDiscount(dQty, purchaseRate, discount);
                dDis = dDis + currDis;
                dCST = dCST + GetCSTVAT(currDis, cst);
                disTot = disTot + ((dQty * purchaseRate) * (discount / 100));
                cstTotal = cstTotal + dCST;
                currVat = GetCSTVAT(currDis, vat);
                vatTotal = vatTotal + currVat;
                amtTotal = amtTotal + GetSum(currDis, vat, cst);
                if (purchaseRate > 0)
                    lblAmtt.Text = GetSum(currDis, vat, cst).ToString("f2");
                //sumVat = sumVat + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate")) - Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"))) * dQty;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                dFr = Convert.ToDouble(lblFg.Text);

                sumNet = dDis + vatTotal + dFr + dCST;

                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = dTot.ToString("f2");
                lblGrandVat.Text = Math.Abs(vatTotal).ToString("f2");
                lblGrandDiscount.Text = disTot.ToString("f2");
                lblGrandCst.Text = dCST.ToString("f2");
                lblNetTotal.Text = sumNet.ToString("f2"); // sumNet.ToString("f2");
                lblTotalPf2.Text = sumNet.ToString("f2"); // sumNet.ToString("f2");
                lblCurrRs.Text = currencyType + ". " + sumNet.ToString("N2");
                lblAmt.Text = dTot.ToString("f2");

                if (dDis > 0)
                    dvDiscountTotal.Visible = true;
                else
                    dvDiscountTotal.Visible = false;

                if (dCST > 0)
                    dvCSTTotal.Visible = true;
                else
                    dvCSTTotal.Visible = false;

                if (Convert.ToDouble(lblGrandVat.Text) > 0)
                    dvVatTotal.Visible = true;
                else
                    dvVatTotal.Visible = false;

                if (dFr > 0)
                    dvFrgTotal.Visible = false;
                else
                    dvFrgTotal.Visible = false;
                dvDiscountTotal.Visible = false;
                dvVatTotal.Visible = false;
                dvFrgTotal.Visible = false;
                dvCSTTotal.Visible = false;
                dvAmount.Visible = false;
                lblCurrGrandTTL.Text = "";
                lblCurrRs.Text = "";
                dvFormat.Visible = false;
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

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        try
        {
            lblBillCopy.Text = txtCopy.Text;
            lblBillCopyF2.Text = txtCopy.Text;
            lblBoxes.Text = txtBoxes.Text;
            lblLorry.Text = txtLorryName.Text;
            lblRemarks.Text = txtRemarks.Text;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //private void loadDivisions()
    //{
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    DataSet ds = new DataSet();

    //    ds = bl.ListDivisions();
    //    //ds.Tables[0].Rows[0].Delete();
    //    ddDivsions.DataSource = ds;
    //    ddDivsions.DataBind();
    //    ddDivsions.DataTextField = "DivisionName";
    //    ddDivsions.DataValueField = "DivisionID";
    //}
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
                    Response.Redirect("ProductCommPurchaseBill.aspx?SID=" + iBillno + "&RT=" + "V");
                else if (RTT != "V")
                    Response.Redirect("ProductCommPurchaseBill.aspx?SID=" + iBillno + "&RT=" + "NO");
                else
                    Response.Redirect("ProductCommPurchaseBill.aspx?SID=" + iBillno + "&RT=" + "NO");
            }

            //companyInfo = bl.GetDivisionForId(sDataSource, int.Parse(ddDivsions.SelectedValue));

            //if (companyInfo != null)
            //{
            //    if (companyInfo.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in companyInfo.Tables[0].Rows)
            //        {
            //            lblTNGST.Text = Convert.ToString(dr["TINno"]);
            //            lblTinNumberPF2.Text = Convert.ToString(dr["TINno"]);
            //            lblCompany.Text = Convert.ToString(dr["DivisionName"]);
            //            lblPhone.Text = Convert.ToString(dr["Phone"]);
            //            lblGSTno.Text = Convert.ToString(dr["GSTno"]);
            //            lblCSTNumberPF2.Text = Convert.ToString(dr["GSTno"]);
            //            lblSignCompany.Text = Convert.ToString(dr["DivisionName"]);
            //            lblAddress.Text = Convert.ToString(dr["Address"]);
            //            lblCity.Text = Convert.ToString(dr["city"]);
            //            lblPincode.Text = Convert.ToString(dr["Pincode"]);
            //            lblState.Text = Convert.ToString(dr["state"]);

            //            lblTNGST600.Text = Convert.ToString(dr["TINno"]);

            //            lblCompany600.Text = Convert.ToString(dr["DivisionName"]);
            //            lblPhone600.Text = Convert.ToString(dr["Phone"]);
            //            lblGSTno600.Text = Convert.ToString(dr["GSTno"]);

            //            lblSignCompany600.Text = Convert.ToString(dr["DivisionName"]);
            //            lblAddress600.Text = Convert.ToString(dr["Address"]);
            //            lblCity600.Text = Convert.ToString(dr["city"]);
            //            lblPincode600.Text = Convert.ToString(dr["Pincode"]);
            //            lblState600.Text = Convert.ToString(dr["state"]);

            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
