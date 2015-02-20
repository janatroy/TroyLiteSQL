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
using SMSLibrary;

public partial class MobileSales : System.Web.UI.Page
{

    public string sDataSource = string.Empty;
    double amtTotal = 0.0;
    double disTotal = 0.0;
    double disTotalRate = 0.0;
    double vatTotal = 0.0;
    double cstTotal = 0.0;
    double rateTotal = 0.0;
    string dbfileName = string.Empty;
    string BarCodeRequired = string.Empty;
    double _TotalSummary = 0.0;
    double _TotalDiscount = 0.0;
    double _TotalVAT = 0.0;
    double _TotalCST = 0.0;
    string _currencyType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/MobileLogin.aspx");

            dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();
            CheckSMSRequired();

            if (!IsPostBack)
            {
                BindCurrencyLabels();

                Reset();
                lblTotalSum.Text = "0";
                lblTotalDis.Text = "0";
                lblTotalVAT.Text = "0";
                lblTotalCST.Text = "0";
                lblFreight.Text = "0";
                lblNet.Text = "0";
                //ResetProduct();
                txtBillDate.Text = DateTime.Now.ToShortDateString();
                cmbProdAdd.Enabled = true;

                if (Session["SMSREQUIRED"] != null)
                {
                    if (Session["SMSREQUIRED"].ToString() == "NO")
                        hdSMSRequired.Value = "NO";
                    else
                        hdSMSRequired.Value = "YES";
                }
                else
                {
                    hdSMSRequired.Value = "NO";
                }

                if (Session["BillDate"] != null)
                    txtBillDate.Text = Session["BillDate"].ToString();

                loadBanks();
                loadSupplier("Sundry Debtors");
                loadProducts();
                txtBillDate.Focus();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    #region Bind Methods

    private void CheckSMSRequired()
    {
        DataSet appSettings;
        string smsRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }

            }
        }

    }

    private string GetCurrencyType()
    {
        if (Session["CurrencyType"].ToString() == "INR")
        {
            return "Rs";
        }
        else if (Session["CurrencyType"].ToString() == "GBP")
        {
            return "£";
        }
        else
        {
            return string.Empty;
        }

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
                    _currencyType = currency;
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

    private void loadCustomer()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListCreditorDebitor(sDataSource);
        cmbCustomer.DataSource = ds;
        cmbCustomer.DataBind();
        cmbCustomer.DataTextField = "LedgerName";
        cmbCustomer.DataValueField = "LedgerID";
    }
    private void loadBanks()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanks();
        drpBankName.DataSource = ds;
        drpBankName.DataBind();
        drpBankName.DataTextField = "LedgerName";
        drpBankName.DataValueField = "LedgerID";

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

    private void loadSupplier(string SundryType)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        if (SundryType == "Sundry Debtors")
            ds = bl.ListSundryDebtors(sDataSource);

        if (SundryType == "Sundry Creditors")
            ds = bl.ListSundryCreditors(sDataSource);

        cmbCustomer.Items.Clear();
        cmbCustomer.Items.Add(new ListItem(" -- Select Customer -- ", "0"));
        cmbCustomer.DataSource = ds;
        cmbCustomer.DataBind();
        cmbCustomer.DataTextField = "LedgerName";
        cmbCustomer.DataValueField = "LedgerID";
        //cmbCustomer.Focus();
    }

    #endregion

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

    public double GetSum()
    {
        return amtTotal;// Convert.ToDouble(hdTotalAmt.Value);
    }
    public double GetTotalRate()
    {
        return disTotalRate;
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

    private void calcSum()
    {
        Double sumAmt = 0;
        //Double sumTAmt = 0;
        Double sumVat = 0;
        Double sumRate = 0;
        Double sumCST = 0;
        Double sumDis = 0;
        Double sumNet = 0;

        sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDouble(txtQtyAdd.Text), Convert.ToDouble(txtRateAdd.Text), Convert.ToDouble(lblDisAdd.Text), Convert.ToDouble(lblVATAdd.Text), 0));
        sumDis = sumDis + GetDis();
        sumVat = sumVat + GetVat();
        sumCST = sumCST + GetCST();
        sumRate = sumRate + GetTotalRate();

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
        sumNet = sumNet + sumAmt + dFreight + dLU;
        lblTotalSum.Text = sumAmt.ToString("#0.00");
        lblTotalDis.Text = sumDis.ToString("#0.00");
        lblDispTotalRate.Text = sumRate.ToString("#0.00");
        lblTotalVAT.Text = sumVat.ToString("#0.00");
        lblTotalCST.Text = sumCST.ToString("#0.00");
        lblFreight.Text = sumLUFreight.ToString("#0.00"); // dFreight.ToString("#0.00");
        lblNet.Text = sumNet.ToString("#0.00");

    }

    protected void cmbProdAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        string itemCode = string.Empty;

        try
        {

            if (cmbProdAdd.SelectedIndex != 0)
            {

                itemCode = cmbProdAdd.SelectedItem.Value;
                double chk = bl.getStockInfo(itemCode);
                if (chk <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Current Stock Limit : " + chk + "')", true);
                    //ResetProduct();
                    return;
                }

                ds = bl.ListSalesProductPriceDetails(cmbProdAdd.SelectedItem.Value, lblledgerCategory.Text);

                string category = lblledgerCategory.Text;

                if (ds != null)
                {
                    //lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                    lblProdDescAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);

                    if (category == "Dealer")
                    {
                        lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerDiscount"]);
                        lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Dealervat"]);
                        txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerRate"]);
                        //lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                    }
                    else
                    {
                        lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                        lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["vat"]);
                        //lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                        txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);

                    }

                    hdStock.Value = Convert.ToString(ds.Tables[0].Rows[0]["Stock"]);
                    txtFreight.Text = "0";
                    txtLU.Text = "0";
                }

            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

        txtQtyAdd.Focus();
    }

    private void Reset()
    {
        cmbCustomer.SelectedIndex = 0;
        drpPaymode.SelectedIndex = 0;
        drpBankName.SelectedIndex = 0;
        //drpPurchaseReturn.SelectedIndex = 0;
        cmbProdAdd.SelectedIndex = 0;
        lblProdDescAdd.Text = "";
        txtRateAdd.Text = "";
        txtCreditCardNo.Text = "";
        lblVATAdd.Text = "";
        lblDisAdd.Text = "";
        txtAddress.Text = "";
        hdContact.Value = "";
        txtCustPh.Text = "";
        txtQtyAdd.Text = "";
        lblledgerCategory.Text = "";
        txtFreight.Text = "";
        txtLU.Text = "";
        lblNet.Text = "";
    }

    protected void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //cmdCancel.Enabled = true;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int iLedgerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
            DataSet ds = bl.GetExecutive(iLedgerID);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["LedgerCategory"].ToString() != "")
                {
                    lblledgerCategory.Text = Convert.ToString(ds.Tables[0].Rows[0]["LedgerCategory"]);
                    lblledgerCategory.Font.Bold = true;
                    lblledgerCategory.Visible = true;
                }
                else
                {
                    lblledgerCategory.Text = "";
                    lblledgerCategory.Visible = false;
                }
                // krishnavelu 26 June
                /*txtOtherCusName.Text = cmbCustomer.SelectedItem.Text;
                if (cmbCustomer.SelectedItem.Text.ToUpper() == "OTHERS")
                {
                    txtOtherCusName.Visible = true;
                    //txtOtherCusName.Focus();
                    txtOtherCusName.Text = "";
                }
                else
                {
                    txtOtherCusName.Visible = false;
                    txtOtherCusName.Text = "";
                }*/

            }
            DataSet customerDs = bl.getAddressInfo(iLedgerID);
            string address = string.Empty;

            if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            {
                if (customerDs.Tables[0].Rows[0]["Add1"] != null)
                    address = customerDs.Tables[0].Rows[0]["Add1"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["Add2"] != null)
                    address = address + customerDs.Tables[0].Rows[0]["Add2"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["Add3"] != null)
                    address = address + customerDs.Tables[0].Rows[0]["Add3"].ToString() + Environment.NewLine;

                txtAddress.Text = address;

                if (customerDs.Tables[0].Rows[0]["Mobile"] != null)
                {
                    hdContact.Value = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
                    txtCustPh.Text = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
                }
            }
            else
            {
                txtAddress.Text = string.Empty;
                txtCustPh.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
        txtCustPh.Focus();
    }

    public string GetTotal(double qty, double rate, double discount, double VAT, double CST)
    {
        double dis = 0;
        double disRate = 0;
        double vat = 0;
        double cst = 0;
        double tot = 0;
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
        hdTotalAmt.Value = amtTotal.ToString("#0.00");
        //lblGrandTotal.Text = Convert.ToString(Convert.ToDecimal(tot) +Convert.ToDecimal(hdTotalAmt.Value));
        return tot.ToString("#0.00");
    }

    public string GetProductTotalExVAT(double qty, double rate, double discount)
    {
        double tot = 0;
        tot = (qty * rate) - ((qty * rate) * (discount / 100));
        return tot.ToString("#0.00");
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

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        { 
        string connection = Request.Cookies["Company"].Value;
        string serType = string.Empty;
        string recondate = string.Empty;
        string purchaseReturn = string.Empty;
        string prReason = string.Empty;
        string executive = string.Empty;
        string sBilldate = string.Empty;
        string sCustomerAddress = string.Empty;

        //Senthil
        string despatchedfrom = string.Empty;
        double fixedtotal = 0.0;
        int manualno = 0;

        string sCustomerAddress2 = string.Empty;
        string sCustomerAddress3 = string.Empty;
        string executivename = string.Empty;
        
        string sCustomerContact = string.Empty;
        string sOtherCusName = string.Empty;// krishnavelu 26 June
        int sCustomerID = 0;
        double dTotalAmt = 0;
        string sCustomerName = string.Empty;
        int iPaymode = 0;
        string sCreditCardno = string.Empty;
        double dFreight = 0;
        double dLU = 0;
        int iBank = 0;
        int iSales = 0;
        //DataSet ds;

            if (Page.IsValid)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                recondate = txtBillDate.Text.Trim(); ;

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }

                sBilldate = txtBillDate.Text.Trim();
                iPaymode = Convert.ToInt32(drpPaymode.SelectedItem.Value);
                dTotalAmt = Convert.ToDouble(hdTotalAmt.Value.Trim());
                sCustomerAddress = txtAddress.Text.Trim();
                sCustomerAddress2 = txtAddress2.Text.Trim();
                sCustomerAddress3 = txtAddress3.Text.Trim();
                sCustomerContact = hdContact.Value.Trim();
                sCustomerContact = txtCustPh.Text;
                sCustomerName = cmbCustomer.SelectedItem.Text;
                sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                calcSum();
                dTotalAmt = Convert.ToDouble(lblTotalSum.Text);
                executive = "0";
                sOtherCusName = string.Empty;
                executivename = string.Empty;
                purchaseReturn = "NO";
                string snarr = string.Empty;

                //Paymode as Bank
                if (iPaymode == 2)
                {
                    sCreditCardno = Convert.ToString(txtCreditCardNo.Text);
                    iBank = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    rvBank.Enabled = true;
                    rvCheque.Enabled = true;
                }
                else
                {
                    //Paymode as Cash
                    rvBank.Enabled = false;
                    rvCheque.Enabled = false;
                }

                /*March18*/
                if (txtFreight.Text.Trim() != "")
                    dFreight = Convert.ToDouble(txtFreight.Text.Trim());

                if (txtLU.Text.Trim() != "")
                    dLU = Convert.ToDouble(txtLU.Text.Trim());
                /*March18*/
                dTotalAmt = dTotalAmt + dFreight + dLU;

                ProdDataSales ds = new ProdDataSales();

                string user = string.Empty;
                DataRow drNew = ds.Tables["productDs"].NewRow();

                drNew["itemCode"] = cmbProdAdd.SelectedValue.ToString();
                //drNew["ProductName"] = cmbProdAdd.SelectedItem.Text;
                drNew["ProductDesc"] = lblProdDescAdd.Text;
                drNew["Rate"] = txtRateAdd.Text.Trim();
                drNew["Qty"] = txtQtyAdd.Text.Trim();
                drNew["Measure_Unit"] = String.Empty;
                drNew["Discount"] = lblDisAdd.Text;
                drNew["IsRole"] = "N";
                drNew["Rods"] = "0";
                drNew["VAT"] = lblVATAdd.Text;
                drNew["CST"] = "0";
                drNew["Bundles"] = "0";
                drNew["ExecCharge"] = "0";
                drNew["Total"] = GetTotal(Convert.ToDouble(txtQtyAdd.Text.Trim()), Convert.ToDouble(txtRateAdd.Text.Trim()), Convert.ToDouble(lblDisAdd.Text), Convert.ToDouble(lblVATAdd.Text), 0);

                ds.Tables["ProductDs"].Rows.Add(drNew);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //old code
                        //int billNo = bl.InsertSalesNewSeries("", sBilldate, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, iPaymode, sCreditCardno, iBank, dTotalAmt, purchaseReturn, prReason, int.Parse(executive), dFreight, dLU, ds, sOtherCusName, "NO", null, "NO", "NO", sCustomerAddress2, sCustomerAddress3, executivename, despatchedfrom, fixedtotal, manualno, 0, user, "NO", "NO","",snarr,"","",0,"");
                       
                       // int billNo = bl.InsertSalesNewSeries("", sBilldate, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, iPaymode, sCreditCardno, iBank, dTotalAmt, purchaseReturn, prReason, int.Parse(executive), dFreight, dLU,ds, sOtherCusName, "NO", null, "NO", "NO", sCustomerAddress2, sCustomerAddress3, executivename, despatchedfrom, fixedtotal, manualno, 0, user, "NO", "NO", "", snarr, "", "", 0, "");
                        int billNo = 1;
                        if (billNo == -1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock Limit is Less')", true);
                            return;
                        }
                        else
                        {
                            Reset();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Details Saved Successfully. Your Bill No. is " + billNo.ToString() + "')", true);

                        }

                        if (hdSMS.Value == "YES")
                        {
                            string conn = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                            string smsTEXT = smsTEXT = "This is the Electronic Receipt for your purchase. The Details are : ";
                            //"Thank you for Purchasing with us. Total Purchase Amount Rs." + lblNet.Text;
                            smsTEXT = smsTEXT + lblProdDescAdd.Text + " " + txtQtyAdd.Text + " Qty @ " + GetCurrencyType() + "." + GetProductTotalExVAT(double.Parse(txtQtyAdd.Text), double.Parse(txtRateAdd.Text), double.Parse(lblDisAdd.Text));

                            smsTEXT = smsTEXT + ". Total Bill Amount including Tax,Freight is " + GetCurrencyType() + "." + lblNet.Text;
                            smsTEXT = smsTEXT + " . The Bill No. is " + billNo.ToString();

                            UtilitySMS utilSMS = new UtilitySMS(conn);
                            string UserID = Page.User.Identity.Name;

                            if (Session["Provider"] != null)
                            {
                                utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), sCustomerContact, smsTEXT, true, UserID);
                            }
                            else
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before save')", true);
                        return;
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before update')", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
