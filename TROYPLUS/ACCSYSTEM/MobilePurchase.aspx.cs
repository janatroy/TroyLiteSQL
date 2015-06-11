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
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;

public partial class MobilePurchase : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    private double amtTotal = 0;
    double disTotalRate = 0.0;
    public double rateTotal = 0;
    public double vatTotal = 0;
    public double disTotal = 0;
    public double cstTotal = 0;
    string BarCodeRequired = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/MobileLogin.aspx");

            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (!IsPostBack)
            {
                BindCurrencyLabels();

                rvBillDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                rvBillDate.MaximumValue = System.DateTime.Now.ToShortDateString();

                loadSupplier("Sundry Creditors");
                loadProducts();
                loadBanks();
                txtBillno.Focus();
                txtBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadSupplier(string SundryType)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        if (SundryType == "Sundry Debtors")
            ds = bl.ListSundryDebtors(sDataSource);

        if (SundryType == "Sundry Creditors")
            ds = bl.ListSundryCreditors(sDataSource);

        cmbSupplier.Items.Clear();
        cmbSupplier.Items.Add(new ListItem(" -- Please Select -- ", "0")); cmbSupplier.DataSource = ds;
        cmbSupplier.DataBind();
        cmbSupplier.DataTextField = "LedgerName";
        cmbSupplier.DataValueField = "LedgerID";

    }


    private void loadBanks()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanks();
        cmbBankName.DataSource = ds;
        cmbBankName.DataBind();
        cmbBankName.DataTextField = "LedgerName";
        cmbBankName.DataValueField = "LedgerID";

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("MobileDefault.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadProducts()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListProducts();
        cmbProdAdd.DataSource = ds;
        cmbProdAdd.DataBind();

        cmbProdAdd.DataTextField = "ProductName";
        cmbProdAdd.DataValueField = "ItemCode";

    }

    private void BindCurrencyLabels()
    {
        DataSet appSettings;
        string currency = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "CURRENCY")
                {
                    currency = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["CurrencyType"] = currency;
                }

                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "BARCODE")
                {
                    BarCodeRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }

        if (currency == "INR")
        {
            lblDispTotal.Text = "Total (INR)";
            lblDispDisRate.Text = "Discounted Rate (INR)";
            lblDispIncVAT.Text = "Inclusive VAT (INR)";
            lblDispIncCST.Text = "Inclusive CST (INR)";
            lblDispLoad.Text = "Loading / Unloading / Freight (INR)";
            lblDispGrandTtl.Text = "GRAND Total (INR)";
        }

        if (currency == "GBP")
        {
            lblDispTotal.Text = "Total (£)";
            lblDispDisRate.Text = "Discounted Rate (£)";
            lblDispIncVAT.Text = "Inclusive VAT (£)";
            lblDispIncCST.Text = "Inclusive CST (£)";
            lblDispLoad.Text = "Loading / Unloading / Freight (£)";
            lblDispGrandTtl.Text = "GRAND Total (£)";

        }

    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            int iPurchaseId = 0;
            string connection = string.Empty;

            if (Page.IsValid)
            {
                connection = Request.Cookies["Company"].Value;
                BusinessLogic bll = new BusinessLogic();

                string recondate = txtBillDate.Text.Trim();
                string salesReturn = string.Empty;
                string srReason = string.Empty;

                if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is Invalid.Please contact Administrator.')", true);
                    return;
                }

                string sBillno = string.Empty;
                string sInvoiceno = string.Empty;

                int iSupplier = 0;
                int iPaymode = 0;
                string[] sDate;
                string[] IDate;

                string sChequeno = string.Empty;
                int iBank = 0;
                int iPurchase = 0;
                double dTotalAmt = 0;
                double TQty = 0;
                double TRate = 0;
                double TDisc = 0;
                double TVat = 0;
                double TCst = 0;

                //iPurchase = Convert.ToInt32(hdPurchase.Value);
                sBillno = txtBillno.Text.Trim();

                sInvoiceno = txtInvoiveNo.Text.Trim();

                DateTime sBilldate;
                DateTime sInvoicedate;

                string delim = "/";
                char[] delimA = delim.ToCharArray();
                CultureInfo culture = new CultureInfo("pt-BR");

                salesReturn = "NO";
                srReason = string.Empty;
                iPaymode = Convert.ToInt32(cmdPaymode.SelectedItem.Value);

                if (iPaymode == 2)
                {
                    sChequeno = Convert.ToString(txtChequeNo.Text);
                    iBank = Convert.ToInt32(cmbBankName.SelectedItem.Value);
                    rvCheque.Enabled = true;
                    rvbank.Enabled = true;
                }
                else
                {
                    rvbank.Enabled = false;
                    rvCheque.Enabled = false;
                }

                Page.Validate("purchaseval");

                if (!Page.IsValid)
                {
                    StringBuilder msg = new StringBuilder();

                    foreach (IValidator validator in Page.Validators)
                    {
                        if (!validator.IsValid)
                        {
                            msg.Append(" - " + validator.ErrorMessage);
                            msg.Append("\\n");
                        }
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg.ToString() + "');", true);
                    return;
                }

                try
                {
                    sDate = txtBillDate.Text.Trim().Split(delimA);
                    sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

                    IDate = txtInvoiveDate.Text.Trim().Split(delimA);
                    sInvoicedate = new DateTime(Convert.ToInt32(IDate[2].ToString()), Convert.ToInt32(IDate[1].ToString()), Convert.ToInt32(IDate[0].ToString()));

                }
                catch (Exception ex)
                {
                    Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
                    return;
                }
                iSupplier = Convert.ToInt32(cmbSupplier.SelectedItem.Value);

                //if (lblTotalSum.Text != string.Empty || lblTotalSum.Text != "0")
                //dTotalAmt = Convert.ToDouble(lblTotalSum.Text);
                /*Start Purchase Loading / Unloading Freight Change - March 16*/
                double dFreight = 0;
                double dLU = 0;
                /*March18*/
                if (txtFreight.Text.Trim() != "")
                    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
                if (txtLU.Text.Trim() != "")
                    dLU = Convert.ToDouble(txtLU.Text.Trim());

                string usernam = Request.Cookies["LoggedUserName"].Value;

                TQty = double.Parse(txtQtyAdd.Text);
                dTotalAmt = double.Parse(GetTotal(double.Parse(txtQtyAdd.Text), double.Parse(txtRateAdd.Text), double.Parse(lblDisAdd.Text), double.Parse(lblVATAdd.Text), 0.0)) + dFreight + dLU;
                /*End Purchase Loading / Unloading Freight Change - Marcxth 16*/

                ProdData ds = new ProdData();

                DataRow drNew = ds.Tables["PurchaseProductDs"].NewRow();

                drNew["itemCode"] = cmbProdAdd.SelectedValue.ToString();
                //drNew["ProductName"] = cmbProdAdd.SelectedItem.Text;
                drNew["ProductDesc"] = lblProdNameAdd.Text;
                drNew["PurchaseRate"] = txtRateAdd.Text.Trim();
                drNew["NLP"] = txtRateAdd.Text.Trim();
                drNew["Qty"] = txtQtyAdd.Text.Trim();
                drNew["Measure_Unit"] = String.Empty;
                drNew["Discount"] = lblDisAdd.Text;
                drNew["isRole"] = "N";
                drNew["VAT"] = lblVATAdd.Text;
                drNew["CST"] = "0";
                drNew["Total"] = GetTotal(Convert.ToDouble(txtQtyAdd.Text.Trim()), Convert.ToDouble(txtRateAdd.Text.Trim()), Convert.ToDouble(lblDisAdd.Text), Convert.ToDouble(lblVATAdd.Text), 0);

                string narration2 = string.Empty;

                ds.Tables["PurchaseProductDs"].Rows.Add(drNew);

                if (ds.Tables["PurchaseProductDs"] != null)
                {
                    if (ds != null)
                    {
                        /*March 18*/
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            /*March 18*/
                            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
                            BusinessLogic bl = new BusinessLogic(sDataSource);
                            int cntB = bl.isDuplicateBill(sBillno, iSupplier);
                            if (cntB > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Duplicate Bill Number')", true);
                                return;
                            }
                            /*Start Purchase Loading / Unloading Freight Change - March 16*/
                  //          //iPurchaseId = bl.InsertPurchase(sBillno, sBilldate, iSupplier, iPaymode, sChequeno, iBank, dTotalAmt, salesReturn, srReason, dFreight, dLU, 0, "NO", ds, "NO", sInvoiceno, sInvoicedate, 0, 0, 0, 0, usernam, narration2, 0, "", "", "", null, "", "", "", "", "",false);
                            /*End Purchase Loading / Unloading Freight Change - March 16*/
                            Reset();

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Saved Successfully. Bill No. is " + sBillno.ToString() + "')", true);
                            Session["SalesReturn"] = salesReturn.ToUpper();

                            ds.Tables["PurchaseProductDs"].Reset();
                            //Response.Redirect("ProductPurchaseBill.aspx?SID=" + iPurchaseId.ToString() + "&RT=" + salesReturn);
                            /*March 18*/
                        }
                        /*March 18*/
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No products is choosed for the bill')", true);
                        }
                        /*March 18*/
                    }

                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void Reset()
    {
        txtBillno.Text = "";
        txtBillDate.Text = "";
        txtFreight.Text = "0";
        txtLU.Text = "0";
        /*Start Purchase Loading / Unloading Freight Change - March 16*/
        foreach (Control control in cmbSupplier.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        /*End Purchase Loading / Unloading Freight Change - March 16*/

        //cmbSupplier.SelectedItem.Text = "";
        cmbSupplier.ClearSelection();

        foreach (Control control in cmdPaymode.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "1";
        }
        cmdPaymode.ClearSelection();

        foreach (Control control in cmbBankName.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        cmbBankName.ClearSelection();

        foreach (Control control in cmbProdAdd.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        cmbProdAdd.ClearSelection();
        txtChequeNo.Text = ""; // cmbBankName.SelectedItem.Text;
        //BindGrid(txtBillnoSrc.Text);
        //Accordion1.SelectedIndex = 1;
        lblProdNameAdd.Text = "";
        txtRateAdd.Text = "";
        txtQtyAdd.Text = "";
        lblDisAdd.Text = "";
        lblVATAdd.Text = "";
        txtLU.Text = "";
        txtFreight.Text = "";
        lblNet.Text = "";
        txtBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtBillno.Focus();
    }

    protected void cmdPaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmdPaymode.SelectedValue == "2")
            {
                pnlBank.Visible = true;
            }
            else
            {
                pnlBank.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public string GetTotal(double qty, double rate, double discount, double VAT, double CST)
    {
        double dis = 0;
        double vat = 0;
        double cst = 0;
        double tot = 0;
        double disRate = 0;

        tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));
        // tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));
        disRate = (qty * rate) - ((qty * rate) * (discount / 100));
        dis = ((qty * rate) * (discount / 100));

        vat = (disRate * (VAT / 100));
        cst = (disRate * (CST / 100));
        amtTotal = amtTotal + Convert.ToDouble(tot);
        disTotal = dis;
        rateTotal = rateTotal + rate;
        vatTotal = vat;
        cstTotal = cst;
        disTotalRate = qty * rate;
        //hdTotalAmt.Value = amtTotal.ToString("#0.00");
        //lblGrandTotal.Text = Convert.ToString(Convert.ToDecimal(tot) +Convert.ToDecimal(hdTotalAmt.Value));
        return tot.ToString("#0.00");
    }

    public double GetTotalRate()
    {
        return disTotalRate;
    }

    public double GetSum()
    {
        return amtTotal;// Convert.ToDouble(hdTotalAmt.Value);
    }
    public double GetDis()
    {
        return disTotal;
    }
    public double GetRate()
    {
        return rateTotal;
    }
    public double GetVat()
    {
        return vatTotal;
    }
    public double GetCST()
    {
        return cstTotal;
    }



    protected void cmbProdAdd_SelectedIndexChanged(object sender, EventArgs e)
    {   /*
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);

        string itemCode = string.Empty;
        DataSet ds = new DataSet();

        if (cmbProdAdd.SelectedIndex != 0)
        {
            itemCode = cmbProdAdd.SelectedItem.Value;

            ds = bl.ListPurProductDetails(cmbProdAdd.SelectedItem.Value);

            if (ds != null)
            {
                lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);
                lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["BuyDiscount"]);
                lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Buyvat"]);
                txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["BuyRate"]);
                hdStock.Value = Convert.ToString(ds.Tables[0].Rows[0]["Stock"]);
                //lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                txtLU.Text = "0";
                txtFreight.Text = "0";

                /*if (lblCSTAdd.Text.Trim() == "")
                {
                    lblCSTAdd.Text = "0";
                }
                if (lblVATAdd.Text.Trim() == "")
                {
                    lblVATAdd.Text = "0";
                }
                if (lblDisAdd.Text.Trim() == "")
                {
                    lblDisAdd.Text = "0";
                }

                txtQtyAdd.Text = "0";

            }
            else
            {
                lblProdNameAdd.Text = "";
                //lblProdDescAdd.Text = "";
                lblDisAdd.Text = "";
                lblVATAdd.Text = "";
                txtRateAdd.Text = "";
                //lblUnitMrmnt.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product details not found.')", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select the Product.')", true);
        }

        txtQtyAdd.Focus();*/
    }

    protected void cmdTotal_Click(object sender, System.EventArgs e)
    {
        try
        {
            calcSum();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void calcSum()
    {
        Double sumAmt = 0;
        //Double sumTAmt = 0;
        Double sumVat = 0;
        Double sumDis = 0;
        Double sumRate = 0;
        Double sumCST = 0;
        Double sumNet = 0;
        DataSet ds = new DataSet();
        //ds.ReadXml(Server.MapPath("Reports\\" + hdFilename.Value + "_product.xml"));
        /*
        ds = (DataSet)GrdViewItems.DataSource;

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Total"] != null)
                        sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDouble(dr["Qty"]), Convert.ToDouble(dr["PurchaseRate"]), Convert.ToDouble(dr["Discount"]), Convert.ToDouble(dr["VAT"]), Convert.ToDouble(dr["CST"])));
                    //sumTAmt = sumTAmt + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["PurchaseRate"]));
                    sumDis = sumDis + GetDis();
                    sumVat = sumVat + GetVat();
                    sumCST = sumCST + GetCST();
                    sumRate = sumRate + GetTotalRate();
                }
            }
        }*/

        string itemCode = cmbProdAdd.SelectedValue.ToString();

        string PurchaseRate = txtRateAdd.Text.Trim();
        string NLP = txtRateAdd.Text.Trim();
        string Qty = txtQtyAdd.Text.Trim();
        string Discount = lblDisAdd.Text;
        string VAT = lblVATAdd.Text;
        string CST = "0";

        if (PurchaseRate == "")
            PurchaseRate = "0";
        if (NLP == "")
            NLP = "0";
        if (Qty == "")
            Qty = "0";
        if (Discount == "")
            Discount = "0";
        if (VAT == "")
            VAT = "0";

        sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDouble(Qty), Convert.ToDouble(PurchaseRate), Convert.ToDouble(Discount), Convert.ToDouble(VAT), 0));
        //sumTAmt = sumTAmt + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["PurchaseRate"]));
        sumDis = sumDis + GetDis();
        sumVat = sumVat + GetVat();
        sumCST = sumCST + GetCST();
        sumRate = sumRate + GetTotalRate();

        /*Start Purchase Loading / Unloading Freight Change - March 16*/
        double dFreight = 0;
        double dLU = 0;
        double sumLUFreight = 0;
        if (txtFreight.Text.Trim() != "")
        {
            dFreight = Convert.ToDouble(txtFreight.Text.Trim());
        }
        if (txtLU.Text.Trim() != "")
        {
            dLU = Convert.ToDouble(txtLU.Text.Trim());
        }
        sumLUFreight = dFreight + dLU;
        sumNet = sumNet + sumAmt + dFreight + dLU; ;
        /*End Purchase Loading / Unloading Freight Change - March 16*/

        lblTotalSum.Text = sumAmt.ToString("#0.00");
        lblTotalDis.Text = sumDis.ToString("#0.00");
        lblDispTotalRate.Text = sumRate.ToString("#0.00");
        lblTotalVAT.Text = sumVat.ToString("#0.00");
        lblTotalCST.Text = sumCST.ToString("#0.00");
        lblNet.Text = sumNet.ToString("#0.00");
        /*Start Purchase Loading / Unloading Freight Change - March 16*/
        lblFreight.Text = sumLUFreight.ToString("#0.00");
        /*End Purchase Loading / Unloading Freight Change - March 16*/
    }

    protected void refresh_Click(object sender, EventArgs e)
    {
        try
        {
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            BusinessLogic bl = new BusinessLogic(sDataSource);

            string itemCode = string.Empty;
            DataSet ds = new DataSet();

            if (cmbProdAdd.SelectedIndex != 0)
            {
                itemCode = cmbProdAdd.SelectedItem.Value;

                ds = bl.ListPurProductDetails(cmbProdAdd.SelectedItem.Value);

                if (ds != null)
                {
                    lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);
                    lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["BuyDiscount"]);
                    lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Buyvat"]);
                    txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["BuyRate"]);
                    hdStock.Value = Convert.ToString(ds.Tables[0].Rows[0]["Stock"]);
                    //lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                    txtLU.Text = "0";
                    txtFreight.Text = "0";

                    /*if (lblCSTAdd.Text.Trim() == "")
                    {
                        lblCSTAdd.Text = "0";
                    }*/
                    if (lblVATAdd.Text.Trim() == "")
                    {
                        lblVATAdd.Text = "0";
                    }
                    if (lblDisAdd.Text.Trim() == "")
                    {
                        lblDisAdd.Text = "0";
                    }

                    txtQtyAdd.Text = "0";

                }
                else
                {
                    lblProdNameAdd.Text = "";
                    //lblProdDescAdd.Text = "";
                    lblDisAdd.Text = "";
                    lblVATAdd.Text = "";
                    txtRateAdd.Text = "";
                    //lblUnitMrmnt.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product details not found.')", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select the Product.')", true);
            }

            txtQtyAdd.Focus();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
