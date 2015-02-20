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

public partial class BundlePrintProductSalesBill : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        { 
            int iBillno = 0;
            if (Request.Cookies["Company"]  != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            if (Session["salesID"] != null)
            {
                iBillno = Convert.ToInt32(Session["salesID"]);
                GetHeaderInfo();
                string BillFormat = bl.getConfigInfo();
                if (BillFormat == "STEEL CEMENT")
                {
                    viswasfooter.Visible = true;
                    string billForm = bl.GetBillFormat(iBillno);
                    GetSalesProductDetailsVishwas(iBillno, billForm);
                }
                //else if (BillFormat == "CEMENT")
                //{
                //    dvGeneral.Visible = true;
                //    string billForm = bl.GetBillFormat(iBillno);
                //    GetSalesProductDetailsVishwas(iBillno, billForm);
                //}
                else if (BillFormat == "PREPRINTED GENERAL")
                {
                    dvHeader.Visible = false;
                    dvFooter.Visible = false;
                    GetSalesProductDetails(iBillno);
                }
                else if (BillFormat == "PREPRINTED FORMAT 2")
                {
                    dvHeader.Visible = false;
                    dvFooter.Visible = false;
                    dvHeaderPF2.Visible = true;
                    lblBillCopy.Visible = false;

                    dvDiscountTotal.Visible = false;
                    dvVatTotal.Visible = false;
                    dvFrgTotal.Visible = false;
                    dvCSTTotal.Visible = false;
                    dvPF2.Visible = true;
                    GetSalesProductDetailsPF2(iBillno);
                }
                else
                {
                    dvGeneral.Visible = true;

                    GetSalesProductDetails(iBillno);
                }
                lblBillCopy.Text = "Customer Copy";
            }
            else
            {
                Response.Redirect("BundleSalesNew.aspx");
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
        if (Session["PurchaseReturn"] != null)
        {
            if (Session["PurchaseReturn"].ToString() == "No")
                lblHeader.Text = "";
            else
                lblHeader.Text = " - (Purchase Return)";

        }
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet dsBill = new DataSet();
        dsBill = bl.GetSalesForId(Convert.ToInt32(Session["salesID"]));
        int customerID = 0;
        if (dsBill.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsBill.Tables[0].Rows)
            {

                lblBillno.Text = Convert.ToString(dr["billno"]);
                lblBillnoPF2.Text = Convert.ToString(dr["billno"]);
                lblBillDate.Text = Convert.ToDateTime(dr["billdate"]).ToShortDateString();
                lblBillDatePF2.Text = Convert.ToDateTime(dr["billdate"]).ToShortDateString();
                lblSupplier.Text = Convert.ToString(dr["Customername"]);
                //lblReason.Text = Convert.ToString(dr["PurchaseReturnReason"]);
                customerID = Convert.ToInt32(dr["CustomerID"]);
                if (dr["Freight"] != null && dr["Freight"] != DBNull.Value)
                {
                    dFr = Convert.ToDouble(dr["freight"]);
                }
                if (dr["LoadUnload"] != null && dr["LoadUnload"] != DBNull.Value)
                {
                    dUL = Convert.ToDouble(dr["LoadUnload"]);
                }
                dFreight = dFreight + dFr + dUL;

                lblFg.Text = dFreight.ToString("f2");
            }
        }

        DataSet companyInfo = new DataSet();

        if (Request.Cookies["Company"]  != null)
        {
            companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

            if (companyInfo != null)
            {
                if (companyInfo.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in companyInfo.Tables[0].Rows)
                    {
                        lblTNGST.Text = Convert.ToString(dr["TINno"]);
                        lblTinNumberPF2.Text = Convert.ToString(dr["TINno"]);
                        lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                        lblPhone.Text = Convert.ToString(dr["Phone"]);
                        lblGSTno.Text = Convert.ToString(dr["GSTno"]);
                        lblCSTNumberPF2.Text = Convert.ToString(dr["GSTno"]);
                        lblSignCompany.Text = Convert.ToString(dr["CompanyName"]);
                        lblAddress.Text = Convert.ToString(dr["Address"]);
                        lblCity.Text = Convert.ToString(dr["city"]);
                        lblPincode.Text = Convert.ToString(dr["Pincode"]);
                        lblState.Text = Convert.ToString(dr["state"]);

                    }
                }
            }
        }



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
            }
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

            dc = new DataColumn("BundleNO");
            dt.Columns.Add(dc);
            dc = new DataColumn("Coir");
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
            DataSet tblBundle = new DataSet();

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

                    tblBundle = bl.ListBundleStock(salesID, itemCode, 0, Convert.ToInt32(qty));
                    drNew["BundleNo"] = tblBundle.Tables[0].Rows[0].ItemArray[0];
                    drNew["Coir"] = tblBundle.Tables[0].Rows[0].ItemArray[1];
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
        DataSet tblBundle = new DataSet();

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

            dc = new DataColumn("BundleNO");
            dt.Columns.Add(dc);
            dc = new DataColumn("Coir");
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
                    // drNew["Qty"] = Convert.ToString(qty);
                    drNew["Qty"] = Convert.ToString(dr["qty"]);
                    drNew["Unit"] = measureUnit;
                    drNew["Amount"] = dTotal;
                    drNew["CST"] = Convert.ToString(dr["CST"]);
                    drNew["VAT"] = Convert.ToString(dr["VAT"]);

                    tblBundle = bl.ListBundleStock(salesID, itemCode, 0, Convert.ToInt32(dr["qty"]));
                    drNew["BundleNo"] = tblBundle.Tables[0].Rows[0].ItemArray[0];
                    drNew["Coir"] = tblBundle.Tables[0].Rows[0].ItemArray[2];

                    drNew["Discount"] = Convert.ToString(dr["Discount"]);

                    billDs.Tables[0].Rows.Add(drNew);
                }
            }

            gvGeneral.Visible = true;
            gvGeneral.DataSource = billDs;
            gvGeneral.DataBind();


        }

    }

    public void GetSalesProductDetailsPF2(int salesID)
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

            dc = new DataColumn("BundleNO");
            dt.Columns.Add(dc);
            dc = new DataColumn("Coir");
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
            DataSet tblBundle = new DataSet();
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

                    tblBundle = bl.ListBundleStock(salesID, itemCode, 0, Convert.ToInt32(qty));
                    drNew["BundleNo"] = tblBundle.Tables[0].Rows[0].ItemArray[0];
                    drNew["Coir"] = tblBundle.Tables[0].Rows[0].ItemArray[2];

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
            Response.Redirect("BundleSalesNew.aspx");
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
                lblRs.Text = "Rs. " + sumNet.ToString("N2");
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
                    dvFrgTotal.Visible = true;
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
                lblRs.Text = "Rs. " + sumNet.ToString("N2");
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
                    dvFrgTotal.Visible = true;
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
                lblNetTotal.Text = sumNet.ToString("f2"); // sumNet.ToString("f2");
                lblRs.Text = "Rs. " + sumNet.ToString("N2");
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
                    dvFrgTotal.Visible = true;
                else
                    dvFrgTotal.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

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
                lblRs.Text = "Rs. " + sumNet.ToString("N2");
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
                    dvFrgTotal.Visible = true;
                else
                    dvFrgTotal.Visible = false;
                dvDiscountTotal.Visible = false;
                dvVatTotal.Visible = false;
                dvFrgTotal.Visible = false;
                dvCSTTotal.Visible = false;
                dvAmount.Visible = false;
                lblGrandlbl.Text = "";
                lblRs.Text = "";
                dvFormat.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
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
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
