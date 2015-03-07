using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Xml;
using SMSLibrary;
using System.Collections.Generic;


public partial class CommissionMngmt : System.Web.UI.Page
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
    double _TotalExecComm = 0.0;
    double _TotalDiscount = 0.0;
    double _TotalVAT = 0.0;
    double _TotalCST = 0.0;
    string _currencyType = string.Empty;
    string BillingMethod = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {        
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));       

        try
        {

            if (!IsPostBack)
            {
                BusinessLogic objChk = new BusinessLogic(sDataSource);

                CheckSMSRequired();

                GrdViewCommission.PageSize = 8;
                //GrdViewSales.Attributes.Add("bordercolor", "Black");

                chkboxAll.Checked = false;

                if (!objChk.CheckSalesSeriesRequired())
                {
                    lblBillNo.Visible = true;
                    ddSeriesType.Visible = false;
                }
                else
                {
                    lblBillNo.Visible = false;
                    ddSeriesType.Visible = true;
                }

                mrBillDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                mrBillDate.MaximumValue = System.DateTime.Now.ToShortDateString();

                if (!objChk.CheckSalesSeriesRequired())
                {
                    lblBillNo.Visible = true;
                    ddSeriesType.Visible = false;
                }
                else
                {
                    DataSet ds = objChk.GetSalesSeries();
                    ddSeriesType.DataSource = ds.Tables[0];
                    ddSeriesType.DataTextField = "Type";
                    ddSeriesType.DataValueField = "ID";
                    ddSeriesType.DataBind();
                    lblBillNo.Visible = false;
                    ddSeriesType.Visible = true;
                }

                string discType = GetDiscType();

                if (discType == "PERCENTAGE")
                {
                 
                }
                else if (discType == "RUPEE")
                {
                    
                }

                BindCurrencyLabels();

                if (BarCodeRequired == "YES")
                {
                    rowBarcode.Visible = true;
                }
                else
                {
                    rowBarcode.Visible = false;
                }

                //txtBarcode.Attributes.Add("onKeyPress", " return clickButton(event,'" + cmdBarcode.ClientID + "')");
                BusinessLogic bl = new BusinessLogic(sDataSource);

                string BillFormat = bl.getConfigInfo();

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


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveAdd(usernam, "COMMNGT"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New item ";
                }


                double CommissionPercent = bl.getCommissionConfig();

                txtCommissionPercent.Text = CommissionPercent.ToString();

                loadBanks();
                loadEmp();
                //loadCustomer();
                loadSupplier();
                loadCustomers();
                loadCategories();
                LoadProducts(this, null);

                txtBillDate.Focus();
                BindGrid(0, 0);
                ViewState["SortExpression"] = "";
                ViewState["SortDirection"] = "";
                //pnlSalesForm.Visible = false;
                //PanelBill.Visible = true;
                updatePnlProduct.Update();
                updatePnlSales.Update();
                ModalPopupSales.Hide();
                ModalPopupProduct.Hide();
                CheckOffline(objChk);

                
             

            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
            //cmdBarcode.Click += new EventHandler(this.txtBarcode_Populated); //Jolo Barcode
            //txtBarcode.Attributes.Add("onblur", "txtBarcode_Populated();"); //Jolo Barcode
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtBillnoSrc.Text = "";
            BindGrid(0, 0);
            //ddCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void UpdateSalesModel()
    {
        updatePnlSales.Update();
    }
    

    private void CheckOffline(BusinessLogic objChk)
    {
        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            cmdSaveProduct.Enabled = false;
            GrdViewItems.Columns[19].Visible = false;
            GrdViewItems.Columns[18].Visible = false;
            cmdSave.Enabled = false;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            lnkBtnAdd.Visible = false;
            GrdViewCommission.Columns[11].Visible = false;
        }
    }

    #region Bind Methods

    private string GetCurrencyType()
    {
        if (Session["CurrencyType"].ToString() == "INR")
        {
            return "Rs.";
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

        if ((currency == "INR") || (currency.ToUpper() == "RS"))
        {
            lblDispTotal.Text = "Total (INR)";
            
            lblDispLoad.Text = "Loading / Unloading / Freight (INR)";
            lblDispGrandTtl.Text = "Purchase Value";
            lblsalesname.Text = "Sales Value";
        }

        if (currency == "GBP")
        {
            lblDispTotal.Text = "Total (£)";
            
            lblDispLoad.Text = "Loading / Unloading / Freight (£)";
            lblDispGrandTtl.Text = "Purchase Value";
            lblsalesname.Text = "Sales Value";
        }

    }

    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListExecutive();
        /*drpExecIncharge.DataSource = ds;
        drpExecIncharge.DataBind();
        drpExecIncharge.DataTextField = "empFirstName";
        drpExecIncharge.DataValueField = "empno";*/

        drpIncharge.DataSource = ds;
        drpIncharge.DataBind();
        drpIncharge.DataTextField = "empFirstName";
        drpIncharge.DataValueField = "empno";
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
        drpBankName.DataTextField = "LedgerName";
        drpBankName.DataValueField = "LedgerID";
        drpBankName.DataBind();

        dpfreightbank.DataSource = ds;
        dpfreightbank.DataTextField = "LedgerName";
        dpfreightbank.DataValueField = "LedgerID";
        dpfreightbank.DataBind();

        dplubank.DataSource = ds;
        dplubank.DataTextField = "LedgerName";
        dplubank.DataValueField = "LedgerID";
        dplubank.DataBind();

        ddBank1.DataSource = ds;
        ddBank1.DataTextField = "LedgerName";
        ddBank1.DataValueField = "LedgerID";
        ddBank1.DataBind();

        ddBank2.DataSource = ds;
        ddBank2.DataTextField = "LedgerName";
        ddBank2.DataValueField = "LedgerID";
        ddBank2.DataBind();

        ddBank3.DataSource = ds;
        ddBank3.DataTextField = "LedgerName";
        ddBank3.DataValueField = "LedgerID";
        ddBank3.DataBind();

        dpbank1.DataSource = ds;
        dpbank1.DataTextField = "LedgerName";
        dpbank1.DataValueField = "LedgerID";
        dpbank1.DataBind();
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


    private void loadSupplier()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListSundryCreditors(sDataSource);

        dpsupplier.Items.Clear();
        dpsupplier.Items.Add(new ListItem("Select Supplier", "0"));
        dpsupplier.DataSource = ds;
        dpsupplier.DataBind();
        dpsupplier.DataTextField = "LedgerName";
        dpsupplier.DataValueField = "LedgerID";
        //cmbCustomer.Focus();
    }

    private void loadCustomers()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListSundryDebtors(sDataSource);

        cmbCustomer.Items.Clear();
        cmbCustomer.Items.Add(new ListItem("Select Customer", "0"));
        cmbCustomer.DataSource = ds;
        cmbCustomer.DataBind();
        cmbCustomer.DataTextField = "LedgerName";
        cmbCustomer.DataValueField = "LedgerID";
        //cmbCustomer.Focus();
    }

    private void BindGrid(int strBillno, int TransNo)
    {

        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        object usernam = Session["LoggedUserName"];

        if (strBillno == 0 && TransNo == 0)
            ds = bl.GetCommission();
        else
            ds = bl.GetCommissionForId(strBillno, TransNo);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewCommission.DataSource = ds.Tables[0].DefaultView;
                GrdViewCommission.DataBind();
                //PanelBill.Visible = true;
            }
        }
        else
        {
            GrdViewCommission.DataSource = null;
            GrdViewCommission.DataBind();
            //PanelBill.Visible = true;
        }
    }

    private DataSet BindGridData(int strBillno)
    {
        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        object usernam = Session["LoggedUserName"];

        if (strBillno == 0)
            ds = bl.GetCommission();
        else
            ds = bl.GetCommissionForId(strBillno);

        //PanelBill.Visible = true;
        return ds;
    }

    #endregion

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

                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "CREDITEXD")
                {
                    Session["CREDITEXD"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString().Trim().ToUpper();
                    hdCREDITEXD.Value = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString().Trim().ToUpper();
                }

                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "OWNERMOB")
                {
                    Session["OWNERMOB"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }

            }
        }

    }

    private string GetDiscType()
    {
        DataSet appSettings;
        string discType = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "DISCTYPE")
                {
                    discType = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["DISCTYPE"] = discType.Trim().ToUpper();
                }

            }
        }

        return discType;

    }

    #region ComboBox Events
    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            GrdViewCommission.PageIndex = ((DropDownList)sender).SelectedIndex;
            int strBillno = 0;
            int strTransno = 0;
            if (txtBillnoSrc.Text.Trim() != "")
                strBillno = Convert.ToInt32(txtBillnoSrc.Text.Trim());

            if (txtTransNo.Text.Trim() != "")
                strTransno = Convert.ToInt32(txtTransNo.Text.Trim());

            BindGrid(strBillno, strTransno);
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpPurchaseReturn_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadSupplier();

            lblledgerCategory.Text = string.Empty;
            lblledgerCategory.Visible = false;
            txtOtherCusName.Text = "";
            txtOtherCusName.Visible = false;
            //cmbCustomer.Focus();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpPaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string receivedBill = "";

            if (lblBillNo.Text != "- TBA -")
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                var salesData = bl.GetCommissionForId(int.Parse(lblBillNo.Text));

                //if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                //{
                //    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                //    if (receivedBill != string.Empty)
                //    {
                //        //drpPaymode.SelectedValue = "3";
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                //        UpdatePanelPayMode.Update();
                //        return;
                //    }
                //}

            }
            else
            {
                //if (drpPaymode.SelectedValue == "4")
                //{
                //    divMultiPayment.Visible = true;
                //    divAddMPayments.Visible = true;
                //    divListMPayments.Visible = false;
                //    UpdatePanelMP.Update();
                //    lblReceivedTotal.Text = "0";
                //}
                //else
                //{

                    divAddMPayments.Visible = true;
                    divListMPayments.Visible = false;
                    divMultiPayment.Visible = false;
                    UpdatePanelMP.Update();
                //}
            }

            if (drpPaymode.SelectedValue == "3")
            {

                if (cmbCustomer.SelectedItem.Value != "0")
                {
                    GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                    ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                    UpdatePanel11.Update();
                }

            }


            if (drpPaymode.SelectedIndex == 1)
            {
                pnlBank.Visible = true;
                rvbank.Enabled = true;
                rvCredit.Enabled = true;
            }
            else
            {
                pnlBank.Visible = false;
                rvbank.Enabled = false;
                rvCredit.Enabled = false;
            }

            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void drpSuppPaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string receivedBill = "";

            if (lblBillNo.Text != "- TBA -")
            {
                //BusinessLogic bl = new BusinessLogic(sDataSource);
                //var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

                //if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                //{
                //    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                //    if (receivedBill != string.Empty)
                //    {
                //        //drpPaymode.SelectedValue = "3";
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                //        UpdatePanelPayMode.Update();
                //        return;
                //    }
                //}

            }
            else
            {
                divAddMPayments.Visible = true;
                    divListMPayments.Visible = false;
                    divMultiPayment.Visible = false;
                    UpdatePanelMP.Update();
                
            }

            if (drpPaymode.SelectedValue == "3")
            {

                //if (cmbCustomer.SelectedItem.Value != "0")
                //{
                //    GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                //    ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                //    UpdatePanel11.Update();
                //}

            }


            if (drpSuppPaymode.SelectedIndex == 1)
            {
                Pnlsuppbank.Visible = true;
                rvsuppbank.Enabled = true;
                rvsuppcredit.Enabled = true;
            }
            else
            {
                Pnlsuppbank.Visible = false;
                rvsuppbank.Enabled = false;
                rvsuppcredit.Enabled = false;
            }

            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void dplupaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string receivedBill = "";

            if (lblBillNo.Text != "- TBA -")
            {
                //BusinessLogic bl = new BusinessLogic(sDataSource);
                //var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

                //if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                //{
                //    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                //    if (receivedBill != string.Empty)
                //    {
                //        //drpPaymode.SelectedValue = "3";
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                //        UpdatePanelPayMode.Update();
                //        return;
                //    }
                //}

            }
            else
            {
                //divAddMPayments.Visible = true;
                //divListMPayments.Visible = false;
                //divMultiPayment.Visible = false;
                //UpdatePanelMP.Update();

            }

            //if (drpPaymode.SelectedValue == "3")
            //{

                //if (cmbCustomer.SelectedItem.Value != "0")
                //{
                //    GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                //    ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                //    UpdatePanel11.Update();
                //}

            //}


            if (dplupaymode.SelectedIndex == 1)
            {
                Panel5.Visible = true;
                RequiredFieldValidator8.Enabled = true;
                RequiredFieldValidator9.Enabled = true;
            }
            else
            {
                Panel5.Visible = false;
                RequiredFieldValidator8.Enabled = false;
                RequiredFieldValidator9.Enabled = false;
            }

            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void dpfreightpaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string receivedBill = "";

            if (lblBillNo.Text != "- TBA -")
            {
                //BusinessLogic bl = new BusinessLogic(sDataSource);
                //var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

                //if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                //{
                //    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                //    if (receivedBill != string.Empty)
                //    {
                //        //drpPaymode.SelectedValue = "3";
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                //        UpdatePanelPayMode.Update();
                //        return;
                //    }
                //}

            }
            else
            {
                //divAddMPayments.Visible = true;
                //divListMPayments.Visible = false;
                //divMultiPayment.Visible = false;
                //UpdatePanelMP.Update();

            }

            //if (drpPaymode.SelectedValue == "3")
            //{

                //if (cmbCustomer.SelectedItem.Value != "0")
                //{
                //    GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                //    ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                //    UpdatePanel11.Update();
                //}

            //}


            if (dpfreightpaymode.SelectedIndex == 1)
            {
                Panel4.Visible = true;
                RequiredFieldValidator6.Enabled = true;
                RequiredFieldValidator7.Enabled = true;
            }
            else
            {
                Panel4.Visible = false;
                RequiredFieldValidator6.Enabled = false;
                RequiredFieldValidator7.Enabled = false;
            }

            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExamimeCustomerCreditHistory(string CustomerId)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        var CustInfo = bl.GetLedgerInfoForId(sDataSource, int.Parse(CustomerId));


        int daysLimit = 0;

        if (CustInfo.Tables[0].Rows[0]["CreditDays"].ToString() != "")
            daysLimit = int.Parse(CustInfo.Tables[0].Rows[0]["CreditDays"].ToString());

        var DebtInfo = bl.GetCustomerDebtInfo(CustomerId);

        var CreditInfo = bl.GetCustomerCreditInfo(CustomerId);

        var ReceivedAmount = bl.GetCustomerReceivedAmount(CustomerId);

        if (CustInfo != null)
        {
            var openBalance = double.Parse(CustInfo.Tables[0].Rows[0]["OpeningBalance"].ToString());

            if (openBalance > 0)
            {
                if (DebtInfo != null && DebtInfo.Tables[0].Rows.Count > 0)
                {
                    DebtInfo.Tables[0].Rows[0].BeginEdit();
                    DebtInfo.Tables[0].Rows[0]["Amount"] = double.Parse(DebtInfo.Tables[0].Rows[0]["Amount"].ToString()) + openBalance;
                    DebtInfo.Tables[0].Rows[0].EndEdit();
                    DebtInfo.Tables[0].AcceptChanges();
                }
            }
            else
            {
                if (CreditInfo != null && CreditInfo.Tables[0].Rows.Count > 0)
                {
                    CreditInfo.Tables[0].Rows[0].BeginEdit();
                    CreditInfo.Tables[0].Rows[0]["Amount"] = double.Parse(CreditInfo.Tables[0].Rows[0]["Amount"].ToString()) + openBalance;
                    CreditInfo.Tables[0].Rows[0].EndEdit();
                    CreditInfo.Tables[0].AcceptChanges();
                }
            }
        }

        if (ReceivedAmount != null && ReceivedAmount.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ReceivedAmount.Tables[0].Rows)
            {
                var cAmount = double.Parse(dr["Amount"].ToString());
                var billNo = dr["BillNo"].ToString();
                int iDebtRow = -1;

                if (DebtInfo != null && DebtInfo.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow debitRow in DebtInfo.Tables[0].Rows)
                    {
                        iDebtRow++;

                        if (debitRow["BillNo"].ToString() == billNo)
                        {
                            var dAmount = double.Parse(debitRow["Amount"].ToString());

                            cAmount = cAmount - dAmount;

                            if (cAmount >= 0)
                            {
                                DebtInfo.Tables[0].Rows[iDebtRow].BeginEdit();
                                DebtInfo.Tables[0].Rows[iDebtRow]["Amount"] = "0";
                                DebtInfo.Tables[0].Rows[iDebtRow].EndEdit();
                                DebtInfo.Tables[0].Rows[iDebtRow].Delete();
                            }
                            else
                            {
                                DebtInfo.Tables[0].Rows[iDebtRow].BeginEdit();
                                DebtInfo.Tables[0].Rows[iDebtRow]["Amount"] = (-(cAmount)).ToString();
                                DebtInfo.Tables[0].Rows[iDebtRow].EndEdit();
                            }

                        }
                    }

                    DebtInfo.Tables[0].AcceptChanges();
                }
            }

        }

        if (CreditInfo != null)
        {
            foreach (DataRow creditRow in CreditInfo.Tables[0].Rows)
            {
                var cAmount = double.Parse(creditRow["Amount"].ToString());

                int iDebtRow = -1;

                if (DebtInfo != null && DebtInfo.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow debitRow in DebtInfo.Tables[0].Rows)
                    {

                        iDebtRow++;

                        if (ReceivedAmount != null)
                        {
                            var ifBillExists = from x in ReceivedAmount.Tables[0].AsEnumerable()
                                               where x.Field<Int32>("BillNo").ToString() == debitRow["BillNo"].ToString()
                                               select new { billNo = x.Field<Int32>("BillNo").ToString() };

                            //var dt = ifBillExists.CopyToDataTable<DataRow>().Rows.Count;
                            var cnt = "0";

                            foreach (var count in ifBillExists)
                            {
                                cnt = count.billNo;
                            }

                            if (cnt != "0")
                                continue;
                        }


                        var dAmount = double.Parse(debitRow["Amount"].ToString());

                        cAmount = cAmount - dAmount;

                        if (cAmount >= 0)
                        {
                            DebtInfo.Tables[0].Rows[iDebtRow].BeginEdit();
                            DebtInfo.Tables[0].Rows[iDebtRow]["Amount"] = "0";
                            DebtInfo.Tables[0].Rows[iDebtRow].EndEdit();
                            DebtInfo.Tables[0].Rows[iDebtRow].Delete();
                        }
                        else
                        {
                            DebtInfo.Tables[0].Rows[iDebtRow].BeginEdit();
                            DebtInfo.Tables[0].Rows[iDebtRow]["Amount"] = (-(cAmount)).ToString();
                            DebtInfo.Tables[0].Rows[iDebtRow].EndEdit();
                        }

                    }

                    DebtInfo.Tables[0].AcceptChanges();
                }


            }
        }

        if (DebtInfo != null && DebtInfo.Tables[0].Rows.Count > 0)
        {

            var firstCreditSalesDate = DateTime.Parse(DebtInfo.Tables[0].Rows[0]["TransDate"].ToString());

            var salesDate = DateTime.Parse(txtBillDate.Text);

            var totalDays = (salesDate - firstCreditSalesDate).TotalDays;

            if (totalDays > daysLimit)
            {
                hdAllowSales.Value = "NO";
            }
            else
            {
                hdAllowSales.Value = "YES";
            }
        }
        else
        {
            hdAllowSales.Value = "YES";
        }

    }

    private DataSet GenCreditHistoryDataset()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;

        dc = new DataColumn("ID");
        dt.Columns.Add(dc);
        dc = new DataColumn("TransDate");
        dt.Columns.Add(dc);
        dc = new DataColumn("Amount");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        return ds;
    }

    protected void dpsupplier_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //protected void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        BusinessLogic bl = new BusinessLogic(sDataSource);

    //        if (lblBillNo.Text != "- TBA -")
    //        {
    //            string receivedBill = "";
    //            //var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

    //            //if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
    //            //{
    //            //    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

    //            //    if (receivedBill != string.Empty)
    //            //    {
    //            //        if (cmbCustomer.Items.FindByValue(salesData.Tables[0].Rows[0]["DebtorID"].ToString()) != null)
    //            //            cmbCustomer.SelectedValue = salesData.Tables[0].Rows[0]["DebtorID"].ToString();

    //            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
    //            //        UpdatePanel21.Update();
    //            //        return;
    //            //    }
    //            //}

    //        }

    //        cmdCancel.Enabled = true;
    //        int iLedgerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
    //        DataSet ds = bl.GetExecutive(iLedgerID);

    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            drpIncharge.ClearSelection();
    //            ListItem li = drpIncharge.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["executiveincharge"]));
    //            if (li != null) li.Selected = true;

    //            if (ds.Tables[0].Rows[0]["LedgerCategory"].ToString() != "")
    //            {
    //                lblledgerCategory.Text = Convert.ToString(ds.Tables[0].Rows[0]["LedgerCategory"]);
    //                lblledgerCategory.Font.Bold = true;
    //                lblledgerCategory.Visible = false;
    //            }
    //            else
    //            {
    //                lblledgerCategory.Text = "";
    //                lblledgerCategory.Visible = false;
    //            }
    //            // krishnavelu 26 June
    //            txtOtherCusName.Text = "";

    //            //if (cmbCustomer.SelectedItem.Text.ToUpper() == "OTHERS")
    //            //{
    //            //    txtOtherCusName.Visible = true;
    //            //    txtOtherCusName.Focus();
    //            //    txtOtherCusName.Text = "";
    //            //}
    //            //else
    //            //{
    //                txtOtherCusName.Visible = false;
    //                txtOtherCusName.Text = "";
    //            //}

    //            if (ds.Tables[0].Rows[0]["CreditLimit"] != null)
    //            {
    //                if (ds.Tables[0].Rows[0]["CreditLimit"].ToString() != "")
    //                    hdCustCreditLimit.Value = ds.Tables[0].Rows[0]["CreditLimit"].ToString();
    //                else
    //                    hdCustCreditLimit.Value = "0";
    //            }
    //            else
    //                hdCustCreditLimit.Value = "0";

    //            //if (cmbCustomer.SelectedItem.Value != "0")
    //            //{
    //            //    GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
    //            //    ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
    //            //}

    //        }

    //        //DataSet customerDs = bl.getAddressInfo(iLedgerID);
    //        //string address = string.Empty;

    //        //if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
    //        //{
    //        //    //if (customerDs.Tables[0].Rows[0]["Add1"] != null)
    //        //    //    txtAddress.Text = customerDs.Tables[0].Rows[0]["Add1"].ToString() + Environment.NewLine;

    //        //    //if (customerDs.Tables[0].Rows[0]["Add2"] != null)
    //        //    //    txtAddress2.Text = address + customerDs.Tables[0].Rows[0]["Add2"].ToString() + Environment.NewLine;

    //        //    //if (customerDs.Tables[0].Rows[0]["Add3"] != null)
    //        //    //    txtAddress3.Text = address + customerDs.Tables[0].Rows[0]["Add3"].ToString() + Environment.NewLine;

    //        //    //txtAddress.Text = address;

    //        //    if (customerDs.Tables[0].Rows[0]["Mobile"] != null)
    //        //    {
    //        //        hdContact.Value = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
    //        //        //txtCustPh.Text = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
    //        //    }
    //        //}
    //        else
    //        {
    //            //txtAddress.Text = string.Empty;
    //            //txtAddress2.Text = string.Empty;
    //            //txtAddress3.Text = string.Empty;
    //            //txtCustPh.Text = string.Empty;
    //        }
    //        errPanel.Visible = false;
    //        ErrMsg.Text = "";
            

    //    }
    //    catch (Exception ex)
    //    {

    //        errPanel.Visible = true;
    //        ErrMsg.Text = ex.Message.ToString().Trim();
    //    }

    //}


    protected void txtBarcode_Populated(object sender, EventArgs e) //Jolo Barcode
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        bool dupFlag = false;
        DataSet checkDs;
        string itemCode = string.Empty;
        DataSet ds = new DataSet();
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        itemCode = bl.GetItemCode(connection, this.txtBarcode.Text);

        if ((itemCode == string.Empty) || (itemCode == "0"))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product not found. Please try again.');", true);
            return;
        }

        cmbProdAdd.SelectedValue = itemCode;

        DataSet roleDs = new DataSet();
        cmdDelete.Enabled = false;
        try
        {

            if (cmbProdAdd.SelectedIndex != 0)
            {

                itemCode = cmbProdAdd.SelectedItem.Value;
                double chk = bl.getStockInfo(itemCode,"");
                if (chk <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Current Stock Limit : " + chk + "')", true);
                    ResetProduct();
                    updatePnlProduct.Update();
                    return;
                }
                if (Session["productDs"] != null)
                {
                    checkDs = (DataSet)Session["productDs"];

                    foreach (DataRow dR in checkDs.Tables[0].Rows)
                    {
                        if (dR["itemCode"] != null)
                        {
                            if (dR["itemCode"].ToString().Trim() == itemCode)
                            {
                                dupFlag = true;
                                break;
                            }
                        }
                    }
                }
                if (!dupFlag)
                {
                    hdOpr.Value = "New";
                    hdCurrRole.Value = "";
                    ds = bl.ListProductDetails(itemCode);

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
                        }
                        else
                        {
                            lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                            lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["vat"]);
                            lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                            txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);

                        }
                        hdStock.Value = Convert.ToString(ds.Tables[0].Rows[0]["Stock"]);

                    }
                }
                else
                {
                    cmbProdAdd.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);
                }
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void UpdateProducts(string CatType)
    {
        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet NewDs = new DataSet();

        if (Session["productDs"] != null)
        {
            ds = (DataSet)Session["productDs"];


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                ds.Tables[0].Rows[i].BeginEdit();

                NewDs = bl.ListSalesProductDetails(ds.Tables[0].Rows[i]["itemCode"].ToString(), CatType);

                DataRow drNew = NewDs.Tables[0].Rows[0];

                //dTotal = Convert.ToDouble(ds.Tables[0].Rows[i]["Qty"]) * Convert.ToDouble(drNew["Rate"].ToString());

                ds.Tables[0].Rows[i]["Rate"] = drNew["Rate"].ToString();
                ds.Tables[0].Rows[i]["Discount"] = drNew["Discount"].ToString();
                ds.Tables[0].Rows[i]["Vat"] = drNew["Vat"].ToString();
                ds.Tables[0].Rows[i].EndEdit();
                ds.Tables[0].Rows[i].AcceptChanges();

            }

            GrdViewItems.DataSource = ds;
            GrdViewItems.DataBind();
            Session["productDs"] = ds;
        }

    }

    protected void cmbProdAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupSales.Show();
        ModalPopupProduct.Show();
        
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        DataSet roleDs = new DataSet();
        string itemCode = string.Empty;
        DataSet checkDs;
        bool dupFlag = false;
        cmdDelete.Enabled = false;
        try
        {

            if (true)
            {
                if (cmbProdAdd.SelectedItem != null)
                {
                    itemCode = cmbProdAdd.SelectedItem.Value.Trim();

                    if (itemCode == "0")
                    {
                        ResetProduct();
                        return;
                    }

                    //double chk = bl.getStockInfo(itemCode);

                    //txtstock.Text = Convert.ToString(chk);

                    //if (chk <= 0)
                    //{
                    //    if (e != null)
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Current Stock Limit for ItemCode : " + cmbProdAdd.SelectedValue.Trim() + " is " + chk + "')", true);
                    //    //LoadProducts(this,null);
                    //    ResetProduct();
                    //    updatePnlProduct.Update();
                    //    return;
                    //}
                    if (e != null)
                    {
                        if (Session["productDs"] != null)
                        {
                            checkDs = (DataSet)Session["productDs"];

                            foreach (DataRow dR in checkDs.Tables[0].Rows)
                            {
                                if (dR["itemCode"] != null)
                                {
                                    if (dR["itemCode"].ToString().Trim() == itemCode)
                                    {
                                        dupFlag = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //if (!dupFlag)
                    //{
                        hdOpr.Value = "New";
                        hdCurrRole.Value = "";
                        ds = bl.ListSalesProductDetails(cmbProdAdd.SelectedItem.Value.Trim(), lblledgerCategory.Text);

                        string category = lblledgerCategory.Text;

                        if (ds != null)
                        {
                            //lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                            lblProdDescAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);
                            txtExecCharge.Text = Convert.ToString(ds.Tables[0].Rows[0]["ExecutiveCommission"]);
                            //lblUnitMrmnt.Text = Convert.ToString(ds.Tables[0].Rows[0]["Measure_Unit"]);

                            if (category == "Dealer")
                            {
                                lblDisAdd.Text = "0";
                                lblVATAdd.Text = "0";
                                lblCSTAdd.Text = "0";
                                
                                txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerRate"]);
                            }
                            else
                            {
                                lblDisAdd.Text = "0";
                                lblVATAdd.Text = "0";
                                lblCSTAdd.Text = "0";
                                
                                txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);
                            }

                            txtQtyAdd.Text = "0";
                            txtQtyAdd.Focus();
                            hdStock.Value = Convert.ToString(ds.Tables[0].Rows[0]["Stock"]);

                            DataSet catData = bl.GetProductForId(sDataSource, cmbProdAdd.SelectedItem.Value.Trim());

                            if (catData != null)
                            {
                                //cmbModel.SelectedValue = catData.Tables[0].Rows[0]["Model"].ToString();
                                //cmbBrand.SelectedValue = catData.Tables[0].Rows[0]["ProductDesc"].ToString();
                                //cmbProdName.SelectedValue = catData.Tables[0].Rows[0]["ProductName"].ToString();

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
                    //}
                    //else
                    //{
                    //    cmbProdAdd.SelectedIndex = 0;
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);
                    //}
                }
                else
                {
                    ClearFilter();
                }
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    #endregion

    #region Button Events

    protected void cmdCancelProduct_Click(object sender, EventArgs e)
    {
        try
        {
            //ResetProduct();
            //cmbProdAdd.Enabled = true;
            //cmdUpdateProduct.Enabled = false;
            //cmdSaveProduct.Enabled = true;
            //cmdSaveProduct.Visible = true;
            //cmdUpdateProduct.Visible = false;
            //cmdCancelProduct.Visible = false;
            ModalPopupProduct.Hide();
            //updatePnlProduct.Update();
            //ModalPopupSales.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdUpdateProduct_Click(object sender, EventArgs e)
    {
        GrdViewItems.Columns[19].Visible = true;
        GrdViewItems.Columns[18].Visible = true;

        string connection = string.Empty;
        string recondate = string.Empty;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        double stock = 0;
        DataTable dt;
        DataRow drNew;
        DataColumn dc;

        string sDiscount = "";
        string sVat = "";
        string sCST = "";
        double dTotal = 0;
        string roleFlag = string.Empty;
        DataSet dsRole = new DataSet();
        string strRole = string.Empty;
        string strQty = string.Empty;
        string strExecutive = string.Empty;// krishnavelu 26 June
        string strExecName = string.Empty;
        string execCharge = "";// krishnavelu 26 June
        bool dupFlag = false;
        string itemCode = string.Empty;
        int curRow = 0;
        string sCreditCardno = string.Empty;
        int iPaymode = 0;
        int iBank = 0;

        try
        {
            if (Page.IsValid)
            {
                //stock = bl.getStockInfo(cmbProdAdd.SelectedItem.Value);

                if (Request.Cookies["Company"]  != null)
                    connection = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("Login.aspx");

                recondate = txtBillDate.Text.Trim();

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }

                if (txtRateAdd.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You have entered Product Rate as Zero.')", true);
                }

                //double chk = bl.getStockInfo(cmbProdAdd.SelectedItem.Value);
                //double curQty = Convert.ToDouble(txtQtyAdd.Text);
                ///*Start March 15 Modification */
                //double QtyEdit = Convert.ToDouble(hdEditQty.Value);
                //chk = chk + QtyEdit;
                ///*End March 15 Modification */
                //if (curQty > chk)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty. for ItemCode : " + cmbProdAdd.SelectedItem.Value + " is Greater than Stock Limit of : " + chk + "')", true);
                //    return;
                //}

                DataSet ds = (DataSet)Session["productDs"];
                pnlProduct.Visible = true;

                txtQtyAdd.ReadOnly = false;

                if (strQty.EndsWith(","))
                {
                    strQty = strQty.Remove(strQty.Length - 1, 1);
                }
                if (lblDisAdd.Text.Trim() != "")
                    sDiscount = lblDisAdd.Text;
                else
                    sDiscount = "0";

                if (lblVATAdd.Text.Trim() != "")
                    sVat = lblVATAdd.Text;
                else
                    sVat = "0";

                if (lblCSTAdd.Text.Trim() != "")
                    sCST = lblCSTAdd.Text;
                else
                    sCST = "0";

                // krishnavelu 26 June
                if (txtExecCharge.Text.Trim() != "")
                    execCharge = txtExecCharge.Text;
                else
                    execCharge = "0";

                string iBankg = string.Empty;
                iPaymode = Convert.ToInt32(drpPaymode.SelectedItem.Value);

                if (iPaymode == 2)
                {
                    sCreditCardno = Convert.ToString(txtCreditCardNo.Text);
                    iBankg = drpBankName.SelectedItem.Text;
                    iBank = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    rvbank.Enabled = true;
                    rvCredit.Enabled = true;
                }
                else
                {
                    iBankg = "";
                    rvbank.Enabled = false;
                    rvCredit.Enabled = false;
                }

                /*
                if (drpExecIncharge.SelectedItem.Value == "0")
                {
                    strExecutive = drpExecIncharge.SelectedItem.Value;
                    strExecName = " -NA- ";
                }
                else
                {
                    strExecutive = drpExecIncharge.SelectedItem.Value;
                    strExecName = drpExecIncharge.SelectedItem.Text;
                }*/


                ds = (DataSet)Session["productDs"];


                // prodItem = cmbProdAdd.SelectedItem.Text.Split('-');

                curRow = Convert.ToInt32(hdCurrentRow.Value);

                ds.Tables[0].Rows[curRow].BeginEdit();
                dTotal = Convert.ToDouble(txtQtyAdd.Text) * Convert.ToDouble(txtRateAdd.Text);

                string disType = GetDiscType();

                if (disType == "RUPEE")
                {
                    double discPer = double.Parse(sDiscount);
                    discPer = (discPer / dTotal) * 100;

                    if (double.IsNaN(discPer))
                        discPer = 0;

                    sDiscount = discPer.ToString();
                }

                ds.Tables[0].Rows[curRow]["itemCode"] = Convert.ToString(cmbProdAdd.SelectedItem.Value);
                ds.Tables[0].Rows[curRow]["CommissionNo"] = hdsales.Value;
                ds.Tables[0].Rows[curRow]["ProductName"] = cmbProdName.SelectedValue;
                ds.Tables[0].Rows[curRow]["ProductDesc"] = cmbProdAdd.SelectedItem.Value;
                ds.Tables[0].Rows[curRow]["Qty"] = txtQtyAdd.Text.Trim();
                ds.Tables[0].Rows[curRow]["CustomerName"] = cmbCustomer.SelectedItem.Text;
                ds.Tables[0].Rows[curRow]["CustomerID"] = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                ds.Tables[0].Rows[curRow]["OtherCusName"] = txtOtherCusName.Text;
                ds.Tables[0].Rows[curRow]["SellingPaymode"] = drpPaymode.SelectedItem.Text;
                ds.Tables[0].Rows[curRow]["CardNo"] = sCreditCardno;
                ds.Tables[0].Rows[curRow]["BankName"] = iBankg;
                ds.Tables[0].Rows[curRow]["BankId"] = iBank;
                ds.Tables[0].Rows[curRow]["Rate"] = txtRateAdd.Text.Trim();
                ds.Tables[0].Rows[curRow]["Discount"] = sDiscount;
                ds.Tables[0].Rows[curRow]["ExecCharge"] = execCharge;// krishnavelu 26 June
                ds.Tables[0].Rows[curRow]["VAT"] = sVat;
                ds.Tables[0].Rows[curRow]["CST"] = sCST;
                // ds.Tables[0].Rows[curRow]["Roles"] = strRole;
                ds.Tables[0].Rows[curRow]["IsRole"] = "N";
                ds.Tables[0].Rows[curRow]["Total"] = Convert.ToString(dTotal);
                ds.Tables[0].Rows[curRow]["Bundles"] = "0";
                ds.Tables[0].Rows[curRow]["Rods"] = "0";
                ds.Tables[0].Rows[curRow].EndEdit();
                ds.Tables[0].Rows[curRow].AcceptChanges();

                GrdViewItems.DataSource = ds;
                GrdViewItems.DataBind();
                //calcSum();

                //Session["roledata"] = null;

                ResetProduct();
                cmbProdAdd.Enabled = true;
                cmdUpdateProduct.Enabled = false;
                //cmdCancelProduct.Visible = false;
                cmdSaveProduct.Enabled = true;
                cmdSaveProduct.Visible = true;
                //Label2.Visible = true;
                cmdUpdateProduct.Visible = false;
                //Label3.Visible = false;
                calcSum();
                hdPrevSalesTotal.Value = lblNet.Text;
                ModalPopupProduct.Hide();
                UpdatePanel9.Update();
            }

            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private DataSet EmptyDataSet()
    {

        var ds = new DataSet();
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        var dt = new DataTable();

        DataRow drNew;
        DataColumn dc;

        dc = new DataColumn("itemCode");
        dt.Columns.Add(dc);

        dc = new DataColumn("Billno");
        dt.Columns.Add(dc);

        dc = new DataColumn("ProductName");
        dt.Columns.Add(dc);

        dc = new DataColumn("ProductDesc");
        dt.Columns.Add(dc);

        dc = new DataColumn("Qty");
        dt.Columns.Add(dc);

        dc = new DataColumn("Rate");
        dt.Columns.Add(dc);

        dc = new DataColumn("Measure_unit");
        dt.Columns.Add(dc);

        dc = new DataColumn("Discount");
        dt.Columns.Add(dc);

        // krishnavelu 26 June
        dc = new DataColumn("ExecCharge");
        dt.Columns.Add(dc);

        dc = new DataColumn("VAT");
        dt.Columns.Add(dc);

        dc = new DataColumn("CST");
        dt.Columns.Add(dc);

        dc = new DataColumn("Roles");
        dt.Columns.Add(dc);

        dc = new DataColumn("IsRole");
        dt.Columns.Add(dc);

        dc = new DataColumn("Total");
        dt.Columns.Add(dc);

        dc = new DataColumn("Bundles");
        dt.Columns.Add(dc);

        dc = new DataColumn("Rods");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        for (int i = 0; i < 5; i++)
        {
            drNew = dt.NewRow();


            string disType = GetDiscType();

            drNew["itemCode"] = string.Empty;
            drNew["Billno"] = "";
            drNew["ProductName"] = string.Empty;
            drNew["ProductDesc"] = string.Empty;
            drNew["Qty"] = "";
            drNew["Measure_Unit"] = "";
            drNew["Rate"] = string.Empty;
            drNew["Discount"] = string.Empty;
            drNew["ExecCharge"] = string.Empty;
            drNew["VAT"] = string.Empty;
            drNew["CST"] = string.Empty;
            drNew["Roles"] = hdCurrRole.Value;
            drNew["IsRole"] = "N";
            drNew["Total"] = string.Empty;
            drNew["Bundles"] = "";
            drNew["Rods"] = "";
            ds.Tables[0].Rows.Add(drNew);
        }
        ds.Tables[0].AcceptChanges();

        return ds;
    }

    protected void cmdSaveProduct_Click(object sender, EventArgs e)
    {
        GrdViewItems.Columns[18].Visible = true;
        GrdViewItems.Columns[19].Visible = true;

        string connection = string.Empty;
        string recondate = string.Empty;
        double stock = 0;
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string sDiscount = "";
        string sVat = "";
        string sCST = "";
        double dTotal = 0;
        string[] prodItem;
        string roleFlag = string.Empty;
        DataSet dsRole = new DataSet();
        string strRole = string.Empty;
        string strQty = string.Empty;
        string strMeasureUnit = string.Empty;
        string strExecutive = string.Empty;// krishnavelu 26 June
        string strExecName = string.Empty;
        string execCharge = "";// krishnavelu 26 June
        string sCreditCardno = string.Empty;
        int iPaymode = 0;
        int iBank = 0;
        string iPaymodeg = string.Empty;
        bool dupFlag = false;
        DataSet ds;
        hdOpr.Value = "New";
        string itemCode = string.Empty;
        try
        {
            if (Page.IsValid)
            {
                ModalPopupSales.Show();

                if (Request.Cookies["Company"]  != null)
                    connection = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("Login.aspx");

                recondate = txtBillDate.Text.Trim();

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }

                if (Session["productDs"] != null)
                {
                    DataSet checkDs = (DataSet)Session["productDs"];

                    foreach (DataRow dR in checkDs.Tables[0].Rows)
                    {
                        if (dR["itemCode"] != null)
                        {
                            if (dR["itemCode"].ToString().Trim() == cmbProdAdd.SelectedValue.Trim())
                            {
                                dupFlag = true;
                                break;
                            }
                        }
                    }
                }

                //if (dupFlag)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);
                //    return;
                //}

                prodItem = cmbProdAdd.SelectedItem.Text.Split('-');
                //double chk = bl.getStockInfo(cmbProdAdd.SelectedValue);
                //double curQty = Convert.ToDouble(txtQtyAdd.Text);

                //if (curQty > chk)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected qty is greater than stock.Current Stock : " + chk + "')", true);
                //    return;
                //}


                //if (cmbCategory.SelectedItem.Text != "GIFT")
                //{
                //    if (txtRateAdd.Text == "0")
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Rate as Zero is not permitted.')", true);
                //        return;
                //    }
                //}
                //else
                //    if (txtRateAdd.Text == "0")
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You Entered Product Rate as Zero.')", true);
                //    }

                cmbProdAdd.Enabled = true;
                pnlProduct.Visible = true;
                txtQtyAdd.ReadOnly = false;
                roleFlag = "N";
                strRole = "NO ROLE";

                if (hdStock.Value != "")
                    stock = Convert.ToDouble(hdStock.Value);

                if (lblDisAdd.Text.Trim() != "")
                {
                    sDiscount = lblDisAdd.Text;
                }
                else
                    sDiscount = "0";

                //if (lblUnitMrmnt.Text.Trim() != "")
                //  strMeasureUnit = lblUnitMrmnt.Text.Trim();

                if (lblVATAdd.Text.Trim() != "")
                    sVat = lblVATAdd.Text;
                else
                    sVat = "0";

                if (lblCSTAdd.Text.Trim() != "")
                    sCST = lblCSTAdd.Text;
                else
                    sCST = "0";

                if (txtExecCharge.Text.Trim() != "")
                    execCharge = txtExecCharge.Text;
                else
                    execCharge = "0";

                string iBankg = string.Empty;
                iPaymode = Convert.ToInt32(drpPaymode.SelectedItem.Value);

                if (iPaymode == 2)
                {
                    sCreditCardno = Convert.ToString(txtCreditCardNo.Text);
                    iBankg = drpBankName.SelectedItem.Text;
                    iBank = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    rvbank.Enabled = true;
                    rvCredit.Enabled = true;
                }
                else
                {
                    iBankg = "";
                    rvbank.Enabled = false;
                    rvCredit.Enabled = false;
                }

                if (Session["productDs"] == null)
                {
                    ds = new DataSet();

                    dt = new DataTable();

                    dc = new DataColumn("itemCode");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("CommissionNo");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("ProductName");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("ProductDesc");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Qty");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Rate");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("CustomerName");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("CustomerID");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("OtherCusName");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("SellingPaymode");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("CardNo");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("BankName");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("BankID");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Measure_unit");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Discount", typeof(double));
                    dt.Columns.Add(dc);

                    // krishnavelu 26 June
                    dc = new DataColumn("ExecCharge");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("VAT");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("CST");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Roles");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("IsRole");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Total");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Bundles");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Rods");
                    dt.Columns.Add(dc);

                    ds.Tables.Add(dt);

                    drNew = dt.NewRow();
                    dTotal = Convert.ToDouble(txtQtyAdd.Text) * Convert.ToDouble(txtRateAdd.Text);

                    string disType = GetDiscType();

                    if (disType == "RUPEE")
                    {
                        double discPer = double.Parse(sDiscount);
                        discPer = (discPer / dTotal) * 100;

                        if (double.IsNaN(discPer))
                            discPer = 0;

                        sDiscount = discPer.ToString();
                    }

                    drNew["itemCode"] = Convert.ToString(cmbProdAdd.SelectedValue);
                    drNew["CommissionNo"] = hdsales.Value;
                    drNew["ProductName"] = cmbProdName.SelectedValue;
                    drNew["ProductDesc"] = cmbProdAdd.SelectedItem.Text;
                    drNew["Qty"] = txtQtyAdd.Text.Trim();
                    drNew["CustomerName"] = cmbCustomer.SelectedItem.Text;
                    drNew["CustomerID"] = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                    drNew["OtherCusName"] = txtOtherCusName.Text;
                    drNew["SellingPaymode"] = drpPaymode.SelectedItem.Text;
                    drNew["CardNo"] = sCreditCardno;
                    drNew["BankName"] = iBankg;
                    drNew["BankId"] = iBank;
                    drNew["Measure_Unit"] = "";
                    drNew["Rate"] = txtRateAdd.Text.Trim();
                    drNew["Discount"] = sDiscount;
                    drNew["ExecCharge"] = execCharge;
                    drNew["VAT"] = sVat;
                    drNew["CST"] = sCST;
                    drNew["Roles"] = hdCurrRole.Value;
                    drNew["IsRole"] = "N";
                    drNew["Total"] = Convert.ToString(dTotal);
                    drNew["Bundles"] = "0";
                    drNew["Rods"] = "0";
                    ds.Tables[0].Rows.Add(drNew);
                    Session["productDs"] = ds;

                }
                else
                {
                    ds = (DataSet)Session["productDs"];
                    drNew = ds.Tables[0].NewRow();
                    dTotal = Convert.ToDouble(txtQtyAdd.Text) * Convert.ToDouble(txtRateAdd.Text);

                    string disType = GetDiscType();

                    if (disType == "RUPEE")
                    {
                        double discPer = double.Parse(sDiscount);
                        discPer = (discPer / dTotal) * 100;

                        if (double.IsNaN(discPer))
                            discPer = 0;

                        sDiscount = discPer.ToString();
                    }

                    drNew["itemCode"] = Convert.ToString(cmbProdAdd.SelectedValue);
                    drNew["CommissionNo"] = hdsales.Value;
                    drNew["ProductName"] = cmbProdName.SelectedValue;
                    drNew["ProductDesc"] = cmbProdAdd.SelectedItem.Text;
                    drNew["Qty"] = txtQtyAdd.Text.Trim();
                    drNew["CustomerName"] = cmbCustomer.SelectedItem.Text;
                    drNew["CustomerID"] = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                    drNew["OtherCusName"] = txtOtherCusName.Text;
                    drNew["SellingPaymode"] = drpPaymode.SelectedItem.Text;
                    drNew["CardNo"] = sCreditCardno;
                    drNew["BankName"] = iBankg;
                    drNew["BankId"] = iBank;
                    drNew["Measure_Unit"] = "";//lblUnitMrmnt.Text.Trim();
                    drNew["Rate"] = txtRateAdd.Text.Trim();
                    drNew["Discount"] = sDiscount;
                    drNew["ExecCharge"] = execCharge;
                    drNew["VAT"] = sVat;
                    drNew["CST"] = sCST;
                    drNew["Roles"] = hdCurrRole.Value;
                    drNew["IsRole"] = "N";
                    drNew["Total"] = Convert.ToString(dTotal);
                    drNew["Bundles"] = "0";
                    drNew["Rods"] = "0";
                    ds.Tables[0].Rows.Add(drNew);

                }

                //cmdSaveProduct.Visible = true;
                //cmdUpdateProduct.Visible = false;
                //cmdCancelProduct.Visible = false;
                GrdViewItems.DataSource = ds;
                GrdViewItems.DataBind();
                calcSum();
                ResetProduct();
                hdPrevSalesTotal.Value = lblNet.Text;
                //updatePnlProduct.Update();
                ModalPopupProduct.Hide();
                UpdatePanel9.Update();
                //updatePnlSales.Update();
                //ModalPopupSales.Show();

            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private void GetCustomerCreditHistory(string CustomerID)
    {

        double custCreditLimit = double.Parse(hdCustCreditLimit.Value);
        int sCustomerID = Convert.ToInt32(CustomerID);
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet dsCredit = bl.GetCustomerDealerCreditAmount(sCustomerID, 1, sDataSource);
        double balance = 0.0;

        if (dsCredit != null)
        {
            if (dsCredit.Tables[0].Rows.Count > 0)
            {
                double currDebit = double.Parse(dsCredit.Tables[0].Rows[0]["Debit"].ToString());
                double currCredit = double.Parse(dsCredit.Tables[0].Rows[0]["Credit"].ToString());

                balance = currDebit - currCredit;
                //double cDiffamt = currCredit - currDebit;


                //if (dDiffamt >= 0)
                //{
                //    balance = dDiffamt;
                //}
                //if (cDiffamt >= 0)
                //{
                //    balance = cDiffamt;
                //}

            }

        }

        hdBalance.Value = balance.ToString();

        UpdatePanel11.Update();
    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        { 
        string connection = Request.Cookies["Company"].Value;
        ModalPopupSales.Show();


        if (!Helper.IsLicenced(connection))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
            return;
        }

        if (Session["productDs"] == null)
        {
            cmdSaveProduct.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before save')", true);
            return;
        }

        string smsTEXT = string.Empty;
        string serType = string.Empty;
        string MultiPayment = string.Empty;
        string recondate = string.Empty;
        string purchaseReturn = string.Empty;
        string intTrans = string.Empty;
        string prReason = string.Empty;
        string executive = string.Empty;
        string sBilldate = string.Empty;
        string sCustomerAddress = string.Empty;

        string sCustomerContact = string.Empty;
        string sOtherCusName = string.Empty;// krishnavelu 26 June
        int sCustomerID = 0;
        int ssupplierID = 0;
        double dTotalAmt = 0;
        string sCustomerName = string.Empty;
        string deliveryNote = string.Empty;
        int iPaymode = 0;
        string sCreditCardno = string.Empty;

        int ssupppaymode = 0;
        string ssuppcardno = string.Empty;
        string sfreightcardno = string.Empty;
        string slucardno = string.Empty;
        string check = string.Empty;

        int sfreightpaymode = 0;
        int slupaymode = 0;
        double dFreight = 0;
        double dLU = 0;
        int iBank = 0;
        int ssuppbank = 0;
        int slubank = 0;
        int sfreightbank = 0;
        int iSales = 0;
        DataSet ds;
        string Series = "";
        DataSet receiptData = null;
        DataSet billData = null;

        double CommissionValue = 0;
        string Remarks = string.Empty;

            if (Page.IsValid)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                recondate = txtBillDate.Text.Trim(); ;

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }

                double salamount = 0;
                CommissionValue = Convert.ToDouble(txtcommissionval.Text);
                Remarks = txtremarks.Text;
                
                iSales = Convert.ToInt32(hdsales.Value);
                sBilldate = txtBillDate.Text.Trim();
                iPaymode = 0;
                dTotalAmt = Convert.ToDouble(hdTotalAmt.Value.Trim());
                sCustomerName = "";
                sCustomerID = 0;
                dTotalAmt = Convert.ToDouble(lblNet.Text);
                salamount=Convert.ToDouble(lblsales.Text);
                ssupplierID = Convert.ToInt32(dpsupplier.SelectedItem.Value);
                string subname = string.Empty;
                subname = dpsupplier.SelectedItem.Text;
                ssupppaymode = Convert.ToInt32(drpSuppPaymode.SelectedItem.Value);
                if (chkboxAll.Checked == true)
                {
                    check = "yes";
                }
                else
                {
                    check = "no";
                }

                sOtherCusName = "";// krishnavelu 26 June
                smsTEXT = " ";
                Series = ddSeriesType.SelectedValue;
                int cnt = 0;

                if (intTrans == "YES")
                    cnt = cnt + 1;
                if (deliveryNote == "YES")
                    cnt = cnt + 1;
                if (purchaseReturn == "YES")
                    cnt = cnt + 1;

                //if (cnt > 1)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Either one of Purchase Return or Delivery Note or Internal Transfer should be selected')", true);
                //    tabs2.ActiveTabIndex = 1;
                //    //updatePnlSales.Update();
                //    return;
                //}

                
                if (iPaymode == 2)
                {
                    sCreditCardno = "";
                    iBank = 0;
                    rvbank.Enabled = true;
                    rvCredit.Enabled = true;
                }
                else
                {
                    //Paymode as Cash
                    rvbank.Enabled = false;
                    rvCredit.Enabled = false;
                }
                if (ssupppaymode == 2)
                {
                    ssuppcardno = Convert.ToString(txtsuppcard.Text);
                    ssuppbank = Convert.ToInt32(dpbank1.SelectedItem.Value);
                    rvsuppbank.Enabled = true;
                    rvsuppcredit.Enabled = true;
                }
                else
                {
                    //Paymode as Cash
                    rvsuppbank.Enabled = false;
                    rvsuppcredit.Enabled = false;
                }


                sfreightpaymode = Convert.ToInt32(dpfreightpaymode.SelectedItem.Value);
                slupaymode= Convert.ToInt32(dplupaymode.SelectedItem.Value);
                if (sfreightpaymode == 2)
                {
                    sfreightcardno = Convert.ToString(txtfreightcardno.Text);
                    sfreightbank = Convert.ToInt32(dpfreightbank.SelectedItem.Value);
                    RequiredFieldValidator7.Enabled = true;
                    RequiredFieldValidator6.Enabled = true;
                }
                else
                {
                    //Paymode as Cash
                    RequiredFieldValidator7.Enabled = false;
                    RequiredFieldValidator6.Enabled = false;
                }

                if (slupaymode == 2)
                {
                    slucardno = Convert.ToString(txtlucardno.Text);
                    slubank = Convert.ToInt32(dplubank.SelectedItem.Value);
                    RequiredFieldValidator8.Enabled = true;
                    RequiredFieldValidator9.Enabled = true;
                }
                else
                {
                    //Paymode as Cash
                    RequiredFieldValidator8.Enabled = false;
                    RequiredFieldValidator9.Enabled = false;
                }

                Page.Validate("salesval");

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
                    bankPanel.Update();
                    ModalPopupSales.Show();
                    return;
                }

                /*March18*/
                if (txtFreight.Text.Trim() != "")
                    dFreight = Convert.ToDouble(txtFreight.Text.Trim());

                if (txtLU.Text.Trim() != "")
                    dLU = Convert.ToDouble(txtLU.Text.Trim());
                ///*March18*/
                //dTotalAmt = dTotalAmt + dFreight + dLU;


                sfreightpaymode = Convert.ToInt32(dpfreightpaymode.SelectedItem.Value);
                slupaymode = Convert.ToInt32(dplupaymode.SelectedItem.Value);

                string username = Request.Cookies["LoggedUserName"].Value;
                //ds = (DataSet)GrdViewItems.DataSource;
                if (Session["productDs"] != null)
                {
                    ds = (DataSet)Session["productDs"];

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int billNo = bl.InsertCommission(Series, sBilldate, sCustomerID, sCustomerName, iPaymode, sCreditCardno, iBank, ssupplierID, ssupppaymode, ssuppcardno, ssuppbank, salamount, dTotalAmt, dFreight, dLU, ds, sOtherCusName, sfreightpaymode, sfreightcardno, sfreightbank, slupaymode, slucardno, slubank, CommissionValue, Remarks, username, subname, check);

                            if (billNo == -1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock Limit is Less')", true);
                                return;
                            }
                            else
                            {
                                string conn = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                                UtilitySMS utilSMS = new UtilitySMS(conn);
                                string UserID = Page.User.Identity.Name;

                                if (hdSMS.Value == "YES")
                                {
                                    int i = ds.Tables[0].Rows.Count;

                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        smsTEXT = smsTEXT + dr["ProductName"].ToString() + " " + dr["Qty"].ToString() + " Qty @ " + GetCurrencyType() + "." + GetProductTotalExVAT(double.Parse(dr["Qty"].ToString()), double.Parse(dr["Rate"].ToString()), double.Parse(dr["Discount"].ToString()));
                                        i = i - 1;

                                        if (i != 0)
                                            smsTEXT = smsTEXT + ", ";
                                    }

                                    smsTEXT = smsTEXT + ". Total Bill Amount is " + GetCurrencyType() + "." + lblNet.Text;
                                    smsTEXT = smsTEXT + " . The Bill No. is " + billNo.ToString();

                                    if (Session["Provider"] != null)
                                    {
                                        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), sCustomerContact, smsTEXT, true, UserID);
                                    }
                                    else
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);


                                }

                                if (hdCreditSMS.Value == "YES")
                                {
                                    if (Session["Provider"] != null)
                                    {
                                        if (Session["OWNERMOB"] != null)
                                        {
                                            sCustomerContact = Session["OWNERMOB"].ToString();
                                            smsTEXT = "Warning SMS : " + cmbCustomer.SelectedItem.Text + " has crossed his Credit Limit of " + GetCurrencyType() + hdCustCreditLimit.Value;
                                            utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), sCustomerContact, smsTEXT, false, UserID);
                                        }
                                    }

                                }

                                Reset();
                                ResetProduct();
                                cmdPrint.Enabled = false;
                                Session["salesID"] = billNo.ToString();
                                Session["PurchaseReturn"] = purchaseReturn;
                                Session["productDs"] = null;
                                //MyAccordion.Visible = true;
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Details Saved Successfully. Your Bill No. is " + billNo.ToString() + "')", true);
                                //Response.Redirect("PrintProductCommission.aspx?SID=" + billNo.ToString() + "&RT=" + purchaseReturn);
                                Response.Redirect("ProductCommPurchaseBill.aspx?SID=" + billNo.ToString() + "&RT=" + "V");
                            }
                        }
                        else
                        {
                            cmdSaveProduct.Enabled = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before save')", true);
                            ModalPopupSales.Show();
                            updatePnlSales.Update();
                            return;
                        }
                    }
                }
                else
                {
                    cmdSaveProduct.Enabled = true;
                    ModalPopupSales.Show();
                    updatePnlSales.Update();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before update')", true);
                    return;
                }
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
            ModalPopupSales.Hide();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }
    }

    public string GetProductTotalExVAT(double qty, double rate, double discount)
    {
        double tot = 0;
        tot = (qty * rate) - ((qty * rate) * (discount / 100));
        return tot.ToString("#0.00");
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        string connection = Request.Cookies["Company"].Value;
        string recondate = string.Empty;
        string purchaseReturn = string.Empty;
        string intTrans = string.Empty;
        string deliveryNote = string.Empty;
        string prReason = string.Empty;
        string executive = string.Empty;
        string sBilldate = string.Empty;
        string sCustomerAddress = string.Empty;
        string check = string.Empty;
        string sCustomerAddress2 = string.Empty;
        string sCustomerAddress3 = string.Empty;

        string executivename = string.Empty;

        string despatchedfrom = string.Empty;
        double fixedtotal = 0.0;
        int manualno = 0;

        string sCustomerContact = string.Empty;
        int sCustomerID = 0;
        double dTotalAmt = 0;
        string sCustomerName = string.Empty;
        int iPaymode = 0;
        string sCreditCardno = string.Empty;

        int ssupppaymode = 0;
        string ssuppcardno = string.Empty;
        string sfreightcardno = string.Empty;
        string slucardno = string.Empty;

        int sfreightpaymode = 0;
        int ssuppbank = 0;
        int slubank = 0;
        int sfreightbank = 0;
        double dFreight = 0;
        double dLU = 0;
        double salamount;
        int ssupplierID = 0;
        int slupaymode = 0;

        int iBank = 0;
        int iSales = 0;
        string userID = string.Empty;
        DataSet ds;

        string sOtherCusName = string.Empty;// krishnavelu 26 June

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
            if (Page.IsValid)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                recondate = txtBillDate.Text.Trim(); ;

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }

                string smsTEXT = "This is the Electronic Receipt for your purchase. The Details are : ";
                dFreight = Convert.ToDouble(txtFreight.Text);
                dLU = Convert.ToDouble(txtLU.Text);
                purchaseReturn = drpPurchaseReturn.SelectedValue;
                intTrans = drpIntTrans.SelectedValue;
                deliveryNote = ddDeliveryNote.SelectedValue;
                prReason = txtPRReason.Text;
                executive = drpIncharge.SelectedValue;
                iSales = Convert.ToInt32(hdsales.Value);
                sBilldate = txtBillDate.Text.Trim();
                iPaymode = Convert.ToInt32(drpPaymode.SelectedItem.Value);
                dTotalAmt = Convert.ToDouble(hdTotalAmt.Value.Trim());

                salamount = Convert.ToDouble(lblsales.Text);
                ssupplierID = Convert.ToInt32(dpsupplier.SelectedItem.Value);
                string subname = string.Empty;
                subname = dpsupplier.SelectedItem.Text;
                ssupppaymode = Convert.ToInt32(drpSuppPaymode.SelectedItem.Value);
                double CommissionValue = 0;

                if (chkboxAll.Checked == true)
                {
                    check = "yes";
                }
                else
                {
                    check = "no";
                }

                CommissionValue = Convert.ToDouble(txtcommissionval.Text);
                string Remarks = string.Empty;
                Remarks = txtremarks.Text;

                executivename = drpIncharge.SelectedItem.Text;

                sCustomerName = cmbCustomer.SelectedItem.Text;
                sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                dTotalAmt = Convert.ToDouble(lblNet.Text);
                //dTotalAmt = dTotalAmt + dFreight + dLU;
                sOtherCusName = "";// krishnavelu 26 June
                userID = Page.User.Identity.Name;
                int cnt = 0;

                if (intTrans == "YES")
                    cnt = cnt + 1;
                if (deliveryNote == "YES")
                    cnt = cnt + 1;
                if (purchaseReturn == "YES")
                    cnt = cnt + 1;

                //if (cnt > 1)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Either one of Purchase Return or Delivery Note or Internal Transfer should be selected')", true);
                //    tabs2.ActiveTabIndex = 1;
                //    //updatePnlSales.Update();
                //    return;
                //}
                sfreightpaymode = Convert.ToInt32(dpfreightpaymode.SelectedItem.Value);
                slupaymode = Convert.ToInt32(dplupaymode.SelectedItem.Value);

                if (iPaymode == 2)
                {
                    sCreditCardno = Convert.ToString(txtCreditCardNo.Text);
                    iBank = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    rvbank.Enabled = true;
                    rvCredit.Enabled = true;
                }
                else
                {
                    rvbank.Enabled = false;
                    rvCredit.Enabled = false;
                }

                if (ssupppaymode == 2)
                {
                    ssuppcardno = Convert.ToString(txtsuppcard.Text);
                    ssuppbank = Convert.ToInt32(dpbank1.SelectedItem.Value);
                    rvsuppbank.Enabled = true;
                    rvsuppcredit.Enabled = true;
                }
                else
                {
                    //Paymode as Cash
                    rvsuppbank.Enabled = false;
                    rvsuppcredit.Enabled = false;
                }


                sfreightpaymode = Convert.ToInt32(dpfreightpaymode.SelectedItem.Value);
                slupaymode = Convert.ToInt32(dplupaymode.SelectedItem.Value);
                if (sfreightpaymode == 2)
                {
                    sfreightcardno = Convert.ToString(txtfreightcardno.Text);
                    sfreightbank = Convert.ToInt32(dpfreightbank.SelectedItem.Value);
                    RequiredFieldValidator7.Enabled = true;
                    RequiredFieldValidator6.Enabled = true;
                }
                else
                {
                    //Paymode as Cash
                    RequiredFieldValidator7.Enabled = false;
                    RequiredFieldValidator6.Enabled = false;
                }

                if (slupaymode == 2)
                {
                    slucardno = Convert.ToString(txtlucardno.Text);
                    slubank = Convert.ToInt32(dplubank.SelectedItem.Value);
                    RequiredFieldValidator8.Enabled = true;
                    RequiredFieldValidator9.Enabled = true;
                }
                else
                {
                    //Paymode as Cash
                    RequiredFieldValidator8.Enabled = false;
                    RequiredFieldValidator9.Enabled = false;
                }



                string username = Request.Cookies["LoggedUserName"].Value;

                Page.Validate("salesval");

                if (!Page.IsValid)
                {
                    StringBuilder msg = new StringBuilder();

                    foreach (IValidator validator in Page.Validators)
                    {
                        if (!validator.IsValid)
                        {
                            msg.Append("" + validator.ErrorMessage);
                            msg.Append("\\n");
                        }
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg.ToString() + "');", true);
                    return;
                }


                //ds = (DataSet)GrdViewItems.DataSource;
                int CommissionNo = Convert.ToInt32(hdsales.Value);
                if (Session["productDs"] != null)
                {
                    ds = (DataSet)Session["productDs"];

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //int billNo = bl.UpdateSalesNew(hdSeries.Value, bill, sBilldate, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, iPaymode, sCreditCardno, iBank, dTotalAmt, purchaseReturn, prReason, Convert.ToInt32(executive), dFreight, dLU, ds, sOtherCusName, intTrans, userID, deliveryNote, sCustomerAddress2, sCustomerAddress3, executivename, receiptData, despatchedfrom, fixedtotal, manualno);

                            int billNo = bl.UpdateCommission(hdSeries.Value, CommissionNo, sBilldate, sCustomerID, sCustomerName, iPaymode, sCreditCardno, iBank, ssupplierID, ssupppaymode, ssuppcardno, ssuppbank, salamount, dTotalAmt, dFreight, dLU, ds, sOtherCusName, sfreightpaymode, sfreightcardno, sfreightbank, slupaymode, slucardno, slubank, CommissionValue, Remarks, username, subname, check);

                            if (billNo == -1)
                            {
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock Limit is Less')", true);
                                //return;
                            }
                            else if (billNo == -3)
                            {
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You have reached the maximum sales series. Please increase the maximum limit and try again.')", true);
                                //return;
                            }
                            else
                            {
                                string conn = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                                UtilitySMS utilSMS = new UtilitySMS(conn);
                                string UserID = Page.User.Identity.Name;

                                if (hdSMS.Value == "YES")
                                {

                                    int i = ds.Tables[0].Rows.Count;

                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        smsTEXT = smsTEXT + dr["ProductName"].ToString() + " " + dr["Qty"].ToString() + " Qty @ " + GetCurrencyType() + "." + GetProductTotalExVAT(double.Parse(dr["Qty"].ToString()), double.Parse(dr["Rate"].ToString()), double.Parse(dr["Discount"].ToString()));
                                        i = i - 1;

                                        if (i != 0)
                                            smsTEXT = smsTEXT + ", ";
                                    }

                                    smsTEXT = smsTEXT + ". Total Bill Amount is " + GetCurrencyType() + "." + lblNet.Text;
                                    smsTEXT = smsTEXT + " . The Bill No. is " + billNo.ToString();

                                    if (Session["Provider"] != null)
                                    {
                                        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), sCustomerContact, smsTEXT, true, UserID);
                                    }
                                    else
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);

                                }

                                if (hdCreditSMS.Value == "YES")
                                {
                                    if (Session["Provider"] != null)
                                    {
                                        if (Session["OWNERMOB"] != null)
                                        {
                                            sCustomerContact = Session["OWNERMOB"].ToString();
                                            smsTEXT = "Warning SMS : " + cmbCustomer.SelectedItem.Text + " has crossed his Credit Limit of " + GetCurrencyType() + hdCustCreditLimit.Value;
                                            utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), sCustomerContact, smsTEXT, true, UserID);
                                        }
                                    }
                                }

                                Reset();
                                ResetProduct();
                                Session["salesID"] = billNo.ToString();
                                Session["PurchaseReturn"] = purchaseReturn;
                                Session["productDs"] = null;
                                //MyAccordion.Visible = true;
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Details Saved Successfully. Your Bill No. is " + billNo.ToString() + "')", true);
                                //Response.Redirect("PrintProductCommission.aspx?SID=" + billNo.ToString() + "&RT=" + purchaseReturn);
                                Response.Redirect("ProductCommPurchaseBill.aspx?SID=" + billNo.ToString() + "&RT=" + "V");
                            }
                        }
                        else
                        {
                            cmdSaveProduct.Enabled = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before update')", true);
                            return;
                        }
                    }
                }
                else
                {
                    cmdSaveProduct.Enabled = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before update')", true);
                    return;
                }
            }

            errPanel.Visible = false;
            ErrMsg.Text = "";
            ModalPopupSales.Hide();
            
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }

    }

    protected void cmdPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string connection = Request.Cookies["Company"].Value;
                BusinessLogic bl = new BusinessLogic();
                string recondate = txtBillDate.Text.Trim();
                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }
                string purchaseReturn = drpPurchaseReturn.SelectedValue;
                Session["salesID"] = hdsales.Value;
                Session["roleDs"] = null;
                Response.Redirect("ProductCommPurchaseBill.aspx?SID=" + hdsales.Value + "&RT=" + "V");
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    
    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        try
        {

            //GrdViewItems.Columns[11].Visible = true;
            //GrdViewItems.Columns[12].Visible = true;

            //lnkBtnAdd.Visible = true;
            // hdMode.Value = "New";
            Reset();
            // lblTotalSum.Text = "0";
            ResetProduct();
            txtBillDate.Text = DateTime.Now.ToShortDateString();
            cmbProdAdd.Enabled = true;
            //cmdUpdateProduct.Enabled = false;
            //cmdSaveProduct.Enabled = true;
            //cmdCancel.Enabled = false; 
            //PanelCmd.Visible = false;
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();

            Session["productDs"] = null;

            cmdSave.Enabled = true;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            cmdPrint.Enabled = false;
            
            lblTotalSum.Text = "0";
            
            lblNet.Text = "0";
            lblFreight.Text = "0";
            errPanel.Visible = false;
            ErrMsg.Text = "";

            //pnlSalesForm.Visible = false;
            //PanelBill.Visible = true;
            //pnlSearch.Visible = true;
            //lnkBtnAdd.Visible = true;
            //MyAccordion.Visible = true;
            BusinessLogic objBus = new BusinessLogic();
            //CheckOffline(objBus);
            //UpdatePnlMaster.Update();
            //updatePnlSales.Update();
            ModalPopupSales.Hide();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            ClearFilter();
            cmbCategory.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void ClearFilter()
    {
        //cmbCategory.SelectedIndex = 0;
        cmbProdAdd.Items.Clear();
        lblProdDescAdd.Text = "";
        cmbBrand.Items.Clear();
        cmbModel.Items.Clear();
        txtstock.Text = "";
        cmbProdName.Items.Clear();
        txtRateAdd.Text = "";
        txtExecCharge.Text = "";
        txtQtyAdd.Text = "";
        lblDisAdd.Text = "";
        lblVATAdd.Text = "";
        lblCSTAdd.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //txtIntialQty.Text = ""; //roleDs.Tables[0].Rows[0]["Qty_bought"].ToString();
            cmdSaveProduct.Enabled = true;
            cmbProdAdd.Enabled = true;
            cmdUpdateProduct.Enabled = false;
            ResetProduct();
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkAddProduct_Click(object sender, EventArgs e)
    {
        try
        {
            string receivedBill = "";

            if (lblBillNo.Text != "- TBA -")
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                var salesData = bl.GetCommissionForId(int.Parse(lblBillNo.Text));

                //if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                //{
                //    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                //    if (receivedBill != string.Empty)
                //    {
                //        //drpPaymode.SelectedValue = "3";
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                //        UpdatePanelPayMode.Update();
                //        return;
                //    }
                //}
            }

            AddNewProd.Enabled = true;
            //ModalPopupSales.Show();
            ResetProduct();
            ClearFilter();
            cmbCategory.SelectedIndex = 0;
            cmbModel.Enabled = true;
            cmbBrand.Enabled = true;
            cmbProdName.Enabled = true;
            cmbCategory.Enabled = true;
            BtnClearFilter.Enabled = true;

            cmdSaveProduct.Visible = true;
            //Label2.Visible = true;
            cmdSaveProduct.Enabled = true;
            cmdUpdateProduct.Visible = false;
            //Label3.Visible = false;
            updatePnlProduct.Update();
            ModalPopupProduct.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
   
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        BusinessLogic objChk = new BusinessLogic(sDataSource);

        if (objChk.CheckSalesSeriesRequired())
        {
            if (!objChk.CheckSalesSeriesOpen())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Bill Series has reached maximum limit. Please increase the Bill Series and try again.');", true);
                cmdCancel_Click(this, null);
                return;
            }
        }


        try
        {
            //lnkBtnAdd.Visible = false;
            //pnlSalesForm.Visible = true;
            //PanelBill.Visible = false;
            //pnlSearch.Visible = false;
            // hdMode.Value = "New";
            Reset();

            chkboxAll.Checked = false;

            // lblTotalSum.Text = "0";
            lblTotalSum.Text = "0";
            
            lblFreight.Text = "0";
            lblBillNo.Text = "- TBA -";
            //txtBillDate.Focus();
            //MyAccordion.Visible = false;
            rowReason.Visible = false;

            ResetProduct();
            //txtBillDate.Text = DateTime.Now.ToShortDateString();

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtBillDate.Text = dtaa;

            //txtCustPh.Text = string.Empty;
            cmbProdAdd.Enabled = true;
            cmdUpdateProduct.Enabled = false;
            cmdSaveProduct.Enabled = true;
            //cmdCancelProduct.Visible = false;
            cmdUpdateProduct.Visible = false;
            //Label3.Visible = false;
            cmdSaveProduct.Visible = true;
            //Label2.Visible = true;
            //cmdCancelProduct.Visible = false;
            PanelCmd.Visible = true;
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();
            Session["productDs"] = null;

            //if (drpPaymode.Items.FindByValue("4") == null)
            //{
            //    ListItem it = new ListItem("Multiple Payment", "4");
            //    drpPaymode.Items.Add(it);
            //}

            drpPaymode.Enabled = true;

            divMultiPayment.Visible = false;
            divAddMPayments.Visible = false;
            divListMPayments.Visible = false;

            GrdViewReceipt.DataSource = null;
            GrdViewReceipt.DataBind();

            cmdSave.Enabled = true;
            cmdCancel.Enabled = true;
            cmdDelete.Enabled = false;

            cmdSave.Visible = true;
            cmdDelete.Visible = false;
            cmdUpdate.Visible = false;
            cmdPrint.Enabled = false;
            cmdPrint.Visible = true;

            cmdPrint.Enabled = false;
            errPanel.Visible = false;
            ErrMsg.Text = "";
            pnlBank.Visible = false;

            txtBillDate.Focus();
            ModalPopupSales.Show();

            //SetInitialRow();

            EmptyRow();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    #endregion

    #region General Methods
    private bool paymodeVisible(string paymode)
    {
        if (paymode.ToUpper() != "2")
        {
            pnlBank.Visible = false;
            return false;
        }
        else
        {
            pnlBank.Visible = true;
            return true;
        }
    }

    private bool paymode1Visible(string paymode)
    {
        if (paymode.ToUpper() != "2")
        {
            Pnlsuppbank.Visible = false;
            return false;
        }
        else
        {
            Pnlsuppbank.Visible = true;
            return true;
        }
    }

    private bool paymode2Visible(string paymode)
    {
        if (paymode.ToUpper() != "2")
        {
            Panel4.Visible = false;
            return false;
        }
        else
        {
            Panel4.Visible = true;
            return true;
        }
    }

    private bool paymode3Visible(string paymode)
    {
        if (paymode.ToUpper() != "2")
        {
            Panel5.Visible = false;
            return false;
        }
        else
        {
            Panel5.Visible = true;
            return true;
        }
    }

    private void Reset()
    {
        dpsupplier.SelectedIndex = 0;
        cmbCustomer.SelectedIndex = 0;
        drpPaymode.SelectedIndex = 0;
        dpbank1.SelectedIndex = 0;
        drpSuppPaymode.SelectedIndex = 0;
        drpBankName.SelectedIndex = 0;
        drpPurchaseReturn.SelectedIndex = 0;
        dpfreightbank.SelectedIndex = 0;
        dplubank.SelectedIndex = 0;
        txtCreditCardNo.Text = "";
        txtPRReason.Text = "";
        txtremarks.Text = "";
        txtfreightcardno.Text = "";
        txtlucardno.Text = "";
        //lblUnitMrmnt.Text = "";
        lblledgerCategory.Text = "";
        txtFreight.Text = "0";
        txtLU.Text = "0";
        txtcommissionvalue.Text = "0";
    }

    private void ResetProduct()
    {
        //lblProdNameAdd.Text = "";
        if (cmbProdAdd.Items.Count > 0)
            cmbProdAdd.SelectedIndex = 0;

        if (cmbModel.Items.Count > 0)
            cmbModel.SelectedIndex = 0;

        if (cmbBrand.Items.Count > 0)
            cmbBrand.SelectedIndex = 0;

        if (cmbProdName.Items.Count > 0)
            cmbProdName.SelectedIndex = 0;

        lblProdDescAdd.Text = "";
        lblDisAdd.Text = "0";
        lblVATAdd.Text = "0";
        lblCSTAdd.Text = "0";
        lblDisAdd.Text = "0";
        txtQtyAdd.Text = "0";
        txtRateAdd.Text = "0";
        txtExecCharge.Text = "0";
        drpIncharge.SelectedValue = "0";

        cmbCustomer.SelectedIndex = 0;
        drpPaymode.SelectedIndex = 0;
        txtCreditCardNo.Text = "";
        txtOtherCusName.Text = "";
        drpBankName.SelectedIndex = 0;

        //lblUnitMrmnt.Text = "";
        //cmbProdAdd.SelectedIndex = 0;
    }

    public string GetTotal(double qty, double rate)
    {
        double dis = 0;
        double disRate = 0;
        double vat = 0;
        double cst = 0;
        double tot = 0;
      
        tot = (qty * rate);
            
        amtTotal = amtTotal + Convert.ToDouble(tot);
        
        disTotalRate = qty * rate;
        hdTotalAmt.Value = amtTotal.ToString("#0.00");
        return tot.ToString("#0.00");       
    }

    public string GetTotalVat532013(double qty, double rate, double discount, double VAT, double CST)
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
        //}
    }


    public string GetTotalVatExclsive(double qty, double rate, double discount, double VAT, double CST)
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
        //}
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
    private void GenerateRoleDs()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dcQty = new DataColumn("Qty");
        DataColumn dcRole = new DataColumn("RoleID");

        dt.Columns.Add(dcQty);
        dt.Columns.Add(dcRole);

        ds.Tables.Add(dt);
        Session["roledata"] = ds;
    }

    private DataSet GenerateBillData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        dc = new DataColumn("ReceiptNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("BillNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("Amount");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        return ds;
    }

    private DataSet GenerateReceiptData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc = new DataColumn("RefNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("TransDate");
        dt.Columns.Add(dc);

        dc = new DataColumn("DebitorID");
        dt.Columns.Add(dc);

        dc = new DataColumn("CreditorID");
        dt.Columns.Add(dc);

        dc = new DataColumn("Amount");
        dt.Columns.Add(dc);

        dc = new DataColumn("Narration");
        dt.Columns.Add(dc);

        dc = new DataColumn("VoucherType");
        dt.Columns.Add(dc);

        dc = new DataColumn("ChequeNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("Paymode");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        return ds;
    }

    #endregion

    #region GridViewItems

    protected void GrdViewItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strItemCode = string.Empty;
        string strRoleFlag = string.Empty;
        DataSet ds = new DataSet();
        int billno = 0;
        GridViewRow row = GrdViewItems.SelectedRow;

        BusinessLogic bl = new BusinessLogic(sDataSource);

        try
        {

            string receivedBill = "";

            if (lblBillNo.Text != "- TBA -")
            {
                var salesData = bl.GetCommissionForId(int.Parse(lblBillNo.Text));

                //if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                //{
                //    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                //    if (receivedBill != string.Empty)
                //    {
                //        //drpPaymode.SelectedValue = "3";
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                //        UpdatePanelPayMode.Update();
                //        return;
                //    }
                //}
            }

            ModalPopupProduct.Show();
            hdCurrentRow.Value = Convert.ToString(row.DataItemIndex);

            if (row.Cells[0].Text != "&nbsp;")
            {
                strItemCode = row.Cells[0].Text.Replace("&quot;", "\"").Trim();
                cmbProdAdd.ClearSelection();
                ListItem li = cmbProdAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(strItemCode.Trim()));
                if (li != null) li.Selected = true;
                cmbProdAdd.Enabled = false;
                cmdSaveProduct.Enabled = false;
                cmdUpdateProduct.Enabled = true;
                cmdSaveProduct.Visible = false;
                //Label2.Visible = false;
                cmdUpdateProduct.Visible = true;
                //Label3.Visible = true;
                //cmdCancelProduct.Visible = true;
                DataSet catData = bl.GetProductForId(sDataSource, strItemCode);
                if (catData != null)
                {
                    cmbCategory.SelectedValue = catData.Tables[0].Rows[0]["CategoryID"].ToString();
                    cmbModel.Enabled = false;
                    cmbBrand.Enabled = false;
                    cmbProdName.Enabled = false;
                    BtnClearFilter.Enabled = false;
                    cmbCategory.Enabled = false;
                    LoadProducts(this, null);
                }

                if ((catData.Tables[0].Rows[0]["ItemCode"] != null) && (catData.Tables[0].Rows[0]["ItemCode"].ToString() != ""))
                {
                    if (cmbProdAdd.Items.FindByValue(catData.Tables[0].Rows[0]["ItemCode"].ToString().Trim()) != null)
                    {
                        cmbProdAdd.ClearSelection();
                        cmbProdAdd.Items.FindByValue(catData.Tables[0].Rows[0]["ItemCode"].ToString().Trim()).Selected = true;
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

                if ((catData.Tables[0].Rows[0]["Model"] != null) && (catData.Tables[0].Rows[0]["Model"].ToString() != ""))
                {
                    if (cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()) != null)
                    {
                        cmbModel.ClearSelection();
                        if (!cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()).Selected)
                            cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()).Selected = true;

                    }
                }

            }
            txtRateAdd.Text = row.Cells[3].Text;
            txtQtyAdd.Text = row.Cells[4].Text;
            txtQtyAdd.Focus();
            /*Start March 15 Modification */
            hdEditQty.Value = row.Cells[4].Text;
            /*End March 15 Modification */
            //lblUnitMrmnt.Text = row.Cells[6].Text;

            string disType = GetDiscType();

            if (disType == "RUPEE")
            {

                DataSet prod = (DataSet)Session["productDs"];

                IEnumerable<DataRow> query = null;

                query = from data in prod.Tables[0].AsEnumerable() where data.Field<string>("itemCode") == strItemCode select data;

                DataTable dt = query.CopyToDataTable<DataRow>();

                //cvDisc.Enabled = false;
                //double R = double.Parse(row.Cells[3].Text.Trim());
                //double Q = double.Parse(row.Cells[4].Text);
                //double discPer = double.Parse(row.Cells[7].Text);

                double R = double.Parse(dt.Rows[0]["Rate"].ToString());
                double Q = double.Parse(dt.Rows[0]["Qty"].ToString());
                double discPer = double.Parse(dt.Rows[0]["Discount"].ToString());

                //(qty * rate) * (discount / 100)
                discPer = (R * Q) * (discPer / 100);

                //double dTotal = double.Parse(((Label)row.FindControl("lbltotal")).Text);
                //discPer = (discPer * dTotal) / 100;

                lblDisAdd.Text = discPer.ToString("#0.00");
            }
            else if (disType == "PERCENTAGE")
            {
                lblDisAdd.Text = row.Cells[7].Text;
                //cvDisc.Enabled = true;
            }

            txtExecCharge.Text = row.Cells[5].Text;
            lblVATAdd.Text = row.Cells[8].Text;
            lblCSTAdd.Text = row.Cells[9].Text;
            //lblProdNameAdd.Text = row.Cells[1].Text;
            lblProdDescAdd.Text = row.Cells[1].Text;

            string sdrpPaymode = string.Empty;
            string sdPaymode = string.Empty;
            string dBank = string.Empty;

            string ddd = string.Empty;

            txtCreditCardNo.Text = row.Cells[14].Text;
            ddd = row.Cells[10].Text;
            txtOtherCusName.Text = row.Cells[12].Text;

            int cdd = bl.GetCuForId(ddd);
            string ddddd = cdd.ToString();

            cmbCustomer.ClearSelection();
            ListItem lih = cmbCustomer.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(ddddd));
            if (lih != null) lih.Selected = true;


             sdrpPaymode = row.Cells[13].Text;
             dBank = row.Cells[15].Text;

             int cdddd = bl.GetCuForId(dBank);
             string ddddddd = cdddd.ToString();

             //sCustomer = Convert.ToString(custid);
             if (sdrpPaymode == "Cash")
             {
                 sdPaymode = "1"; 
             }
             else if (sdrpPaymode == "Bank / Credit Card")
             {
                 sdPaymode = "2"; 
             }
             else if (sdrpPaymode == "Credit")
             {
                 sdPaymode = "3"; 
             }

             drpPaymode.ClearSelection();
             ListItem pLi = drpPaymode.Items.FindByValue(sdPaymode.Trim());
             if (pLi != null) pLi.Selected = true;

             if (paymodeVisible(sdPaymode))
             {
                 drpBankName.ClearSelection();
                 ListItem cli = drpBankName.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(ddddddd));
                if (cli != null) cli.Selected = true;
             }


            billno = Convert.ToInt32(hdsales.Value);
            hdOpr.Value = "Edit";

            errPanel.Visible = false;
            ErrMsg.Text = "";
            UpdatePanel11.Update();
            updatePnlProduct.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    protected void GrdViewItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds;
        int billno = 0;
        string strItemcode = string.Empty;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        try
        {
            string receivedBill = "";

            if (lblBillNo.Text != "- TBA -")
            {
                var salesData = bl.GetCommissionForId(int.Parse(lblBillNo.Text));

                //if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                //{
                //    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                //    if (receivedBill != string.Empty)
                //    {
                //        //drpPaymode.SelectedValue = "3";
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                //        UpdatePanelPayMode.Update();
                //        return;
                //    }
                //}
            }

            if (Session["productDs"] != null)
            {

                GridViewRow row = GrdViewItems.Rows[e.RowIndex];

                // strRole = GrdViewItems.DataKeys[row.DataItemIndex].Value.ToString();
                strItemcode = row.Cells[0].Text;
                billno = Convert.ToInt32(hdsales.Value);
                int rowsAff = bl.DeleteCommissionProduct(billno, strItemcode);
                if (rowsAff == -1)
                {
                    ds = (DataSet)Session["productDs"];
                    ds.Tables[0].Rows[GrdViewItems.Rows[e.RowIndex].DataItemIndex].Delete();
                    ds.Tables[0].AcceptChanges();
                    GrdViewItems.DataSource = ds;
                    GrdViewItems.DataBind();
                    Session["productDs"] = ds;
                    cmdSaveProduct.Enabled = true;
                    calcSum();
                    UpdatePanelSalesItems.Update();
                    UpdatePanelTotalSummary.Update();
                    UpdatePanel9.Update();
                }

            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    #endregion

    #region GridSales
    protected void GrdViewCommission_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewCommission.PageIndex = e.NewPageIndex;
            int strBillno = 0;
            int strTransno = 0;

            if (txtBillnoSrc.Text.Trim() != "")
                strBillno = Convert.ToInt32(txtBillnoSrc.Text.Trim());
            else
                strBillno = 0;

            if (txtTransNo.Text.Trim() != "")
                strTransno = Convert.ToInt32(txtTransNo.Text.Trim());
            else
                strTransno = 0;

            BindGrid(strBillno, strTransno);
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewCommission_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewCommission, e.Row, this);
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow product = ((System.Data.DataRowView)e.Row.DataItem).Row;

                if (e.Row.FindControl("lbltotal") != null)
                {
                    if (((Label)(e.Row.FindControl("lbltotal"))).Text != "")
                        _TotalSummary = _TotalSummary + Convert.ToDouble(((Label)(e.Row.FindControl("lbltotal"))).Text);
                }


                if (product[8] != null)
                {
                    //if (product[8].ToString() != "")
                    //_TotalExecComm = _TotalExecComm + Convert.ToDouble(product[8].ToString());
                }

                if (product[7] != null)
                {
                    //if (product[7].ToString() != "")
                    //_TotalDiscount = _TotalDiscount + Convert.ToDouble(product[7].ToString());
                }

                if (product[9] != null)
                {
                    //if (product[9].ToString() != "")
                    //_TotalVAT = _TotalVAT + Convert.ToDouble(product[9].ToString());
                }

                if (product[10] != null)
                {
                    //if (product[10].ToString() != "")
                    //_TotalCST = _TotalCST + Convert.ToDouble(product[10].ToString());
                }

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                calcSum();
                /*
                ((Label)e.Row.FindControl("lbltotalSummary")).Text = GetCurrencyType() + _TotalSummary.ToString();
                e.Row.Cells[5].Text = GetCurrencyType() + _TotalExecComm.ToString();
                e.Row.Cells[7].Text = GetCurrencyType() + lblTotalDis.Text;
                //e.Row.Cells[6].Text = _TotalDiscount.ToString() + "%";
                e.Row.Cells[9].Text = GetCurrencyType() + lblTotalCST.Text;
                //e.Row.Cells[9].Text = _TotalCST.ToString() + "%";
                //e.Row.Cells[8].Text = _TotalVAT.ToString() + "%";
                e.Row.Cells[8].Text =  GetCurrencyType() + lblTotalVAT.Text;
                e.Row.Cells[3].Text = GetCurrencyType() + lblDispTotalRate.Text;*/
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewCommission_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string paymode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paymode"));
                //Label payMode = (Label)e.Row.FindControl("lblPaymode");
                //if (paymode == "1")
                //    payMode.Text = "Cash";
                //else if (paymode == "2")
                //    payMode.Text = "Bank";
                //else
                //{
                //    payMode.Text = "Credit";

                //    if (e.Row.FindControl("hdPaymode") != null)
                //    {
                //        var x = ((HiddenField)e.Row.FindControl("hdPaymode")).Value;

                //        if (x == "YES")
                //            payMode.Text = "MultiPayment";

                //    }
                //}


                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "COMMNGT"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "COMMNGT"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveView(usernam, "COMMNGT"))
                {
                    ((Image)e.Row.FindControl("lnkprint")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = true;
                }
                else
                {
                    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = false;
                }

            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void GrdViewCommission_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            string expression = ViewState["SortExpression"].ToString();
            string direction = ViewState["SortDirection"].ToString();

            Image sortImage = new Image();
            //        sortImage.Width = Unit.Pixel(10);

            if (e.SortExpression != expression)
            {
                expression = e.SortExpression;
                direction = "Asc";
                sortImage.ImageUrl = "App_Themes/DefaultTheme/Images/sort_asc.gif";
                sortImage.AlternateText = "Ascending Order";
            }
            else
            {
                if (direction == "Asc")
                {
                    expression = e.SortExpression;
                    direction = "Desc";
                    sortImage.ImageUrl = "App_Themes/DefaultTheme/Images/sort_asc.gif";
                    sortImage.AlternateText = "Ascending Order";

                }
                else
                {
                    expression = e.SortExpression;
                    direction = "Asc";
                    sortImage.ImageUrl = "App_Themes/DefaultTheme/Images/sort_desc.gif";
                    sortImage.AlternateText = "Descending Order";
                }
            }

            ViewState["SortExpression"] = expression;
            ViewState["SortDirection"] = direction;

            int BillNo = 0;

            if (txtBillnoSrc.Text != "")
                BillNo = Convert.ToInt32(txtBillnoSrc.Text);



            var data = BindGridData(BillNo);
            //var sortedData = from x in data.Tables[0].AsEnumerable()
            //                 orderby x.Field<string>(expression) 
            //                 select x;

            var sortedData = data.Tables[0].DefaultView;

            sortedData.Sort = expression + " " + direction;

            GrdViewCommission.DataSource = sortedData;
            GrdViewCommission.DataBind();
            GrdViewCommission.HeaderRow.Cells[GetSortColumnIndex(expression)].Controls.Add(sortImage);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected int GetSortColumnIndex(string sortExpresssion)
    {
        foreach (DataControlField field in GrdViewCommission.Columns)
        {
            if (field.SortExpression == sortExpresssion)
                return GrdViewCommission.Columns.IndexOf(field);

        }

        return -1;
    }


    protected void GrdViewCommission_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        { 
        Session["Show"] = "Hide";

        BusinessLogic objChk = new BusinessLogic(sDataSource);
        chkboxAll.Checked = false;

        //if (objChk.CheckSalesSeriesRequired())
        //{
        //    if (!objChk.CheckSalesSeriesOpen())
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Bill Series has reached maximum limit. Please increase the Bill Series and try again.');", true);
        //        cmdCancel_Click(this, null);
        //        return;
        //    }
        //}

        string strPaymode = string.Empty;
        string supPaymode = string.Empty;
        string frPaymode = string.Empty;

        string luPaymode = string.Empty;
        string MultiPaymode = string.Empty;
        string sCustomer = string.Empty;
        string sSupplier = string.Empty;
        int CommissionID = 0;
        string connection = Request.Cookies["Company"].Value;
        GridViewRow row = GrdViewCommission.SelectedRow;
        DataSet itemDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string recondate = row.Cells[1].Text;
        cmdPrint.Enabled = true;
        cmdUpdate.Enabled = true;
        cmdUpdate.Visible = true;
        cmdDelete.Enabled = true;
        cmdSave.Visible = false;
        PanelCmd.Visible = true;
        //lnkBtnAdd.Visible = false;
        cmdCancel.Enabled = true;
        //MyAccordion.Visible = false;

        //PanelBill.Visible = false;
        //pnlSalesForm.Visible = true;
        //pnlSearch.Visible = false;
        ddSeriesType.Visible = false;
        lblBillNo.Visible = true;


            if (Page.User.IsInRole("DELSALES"))
            {
                cmdUpdate.Enabled = true;
                cmdDelete.Enabled = true;
            }
            else
            {
                DataSet dsRoles = bl.GetMasterRoles(sDataSource);
                bool isDelRoleExists = false;

                foreach (DataRow dr in dsRoles.Tables[0].Rows)
                {
                    if (dr["Role"].ToString().ToUpper() == "DELSALES")
                    {
                        isDelRoleExists = true;
                    }
                }

                if (isDelRoleExists)
                {
                    cmdUpdate.Enabled = false;
                    cmdDelete.Enabled = false;
                }
                else
                {
                    cmdUpdate.Enabled = true;
                    cmdDelete.Enabled = true;
                }

            }

            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                return;
            }

            if (GrdViewCommission.SelectedDataKey.Value != null && GrdViewCommission.SelectedDataKey.Value.ToString() != "")
                CommissionID = Convert.ToInt32(GrdViewCommission.SelectedDataKey.Value.ToString());


            DataSet ds = bl.GetCommissionForId(CommissionID);

            hdsales.Value = CommissionID.ToString();
            //txtBillDate.Focus();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["CommissionDate"] != null)
                        txtBillDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CommissionDate"]).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["CommissionNo"] != null)
                        lblBillNo.Text = ds.Tables[0].Rows[0]["CommissionNo"].ToString();

                    loadSupplier();
                    //loadBanks();

                    if (ds.Tables[0].Rows[0]["checked"].ToString() == "no")
                    {
                        chkboxAll.Checked = false;
                    }
                    else
                    {
                        chkboxAll.Checked = true;
                    }

                    if (ds.Tables[0].Rows[0]["CustomerID"] != null)
                    {
                        sCustomer = Convert.ToString(ds.Tables[0].Rows[0]["CustomerID"]);
                        cmbCustomer.ClearSelection();
                        ListItem li = cmbCustomer.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }

                    if (ds.Tables[0].Rows[0]["SupplierID"] != null)
                    {
                        sSupplier = Convert.ToString(ds.Tables[0].Rows[0]["SupplierID"]);
                        dpsupplier.ClearSelection();
                        ListItem lil = dpsupplier.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sSupplier));
                        if (lil != null) lil.Selected = true;
                    }

                    // krishnavelu 26 June
                    txtOtherCusName.Text = ""; // krishnavelu 26 June
                    txtremarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["remarks"]);
                    txtcommissionval.Text = Convert.ToString(ds.Tables[0].Rows[0]["Comissionvalue"]);
                    txtcommissionvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["Comissionvalue"]);

                    //if (sCustomer.ToUpper() == "OTHERS")
                    //    txtOtherCusName.Visible = true;
                    //else
                        txtOtherCusName.Visible = false;

                    strPaymode = ds.Tables[0].Rows[0]["SellingPaymode"].ToString();

                    hdSeries.Value = ds.Tables[0].Rows[0]["SeriesID"].ToString();

                    string cuscredid = string.Empty;

                    drpPaymode.ClearSelection();
                    ListItem pLi = drpPaymode.Items.FindByValue(strPaymode.Trim());
                    if (pLi != null) pLi.Selected = true;                 

                    if (paymodeVisible(strPaymode))
                    {
                        if (ds.Tables[0].Rows[0]["CusCardNo"] != null)
                            txtCreditCardNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CusCardNo"]);
                        if (ds.Tables[0].Rows[0]["CusDebtorID"] != null)
                        {
                            //drpBankName.ClearSelection();
                            //ListItem cli = drpBankName.Items.FindByText(HttpUtility.HtmlDecode(Convert.ToString(ds.Tables[0].Rows[0]["CusDebtorID"])));

                            //if (cli != null) cli.Selected = true;

                            cuscredid = Convert.ToString(ds.Tables[0].Rows[0]["CusDebtorID"]);
                            drpBankName.ClearSelection();
                            ListItem cli = drpBankName.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(cuscredid));
                            if (cli != null) cli.Selected = true;

                            //rvbank.Enabled = true;
                            //rvCredit.Enabled = true;
                        }
                    }


                    supPaymode = ds.Tables[0].Rows[0]["SupplierPaymode"].ToString();
                    drpSuppPaymode.ClearSelection();
                    ListItem supLi = drpSuppPaymode.Items.FindByValue(supPaymode.Trim());
                    if (supLi != null) supLi.Selected = true;

                    string supcredid = string.Empty;
                    string frcredid = string.Empty;

                    string lucredid = string.Empty;

                    if (paymode1Visible(supPaymode))
                    {
                        if (ds.Tables[0].Rows[0]["supCardNo"] != null)
                            txtsuppcard.Text = Convert.ToString(ds.Tables[0].Rows[0]["supCardNo"]);
                        if (ds.Tables[0].Rows[0]["SupCreditorID"] != null)
                        {
                            //dpbank1.ClearSelection();
                            //ListItem cli = dpbank1.Items.FindByText(HttpUtility.HtmlDecode(Convert.ToString(ds.Tables[0].Rows[0]["SupCreditorID"])));

                            //if (cli != null) cli.Selected = true;
                            ////rvbank.Enabled = true;
                            ////rvCredit.Enabled = true;
                            supcredid = Convert.ToString(ds.Tables[0].Rows[0]["SupCreditorID"]);
                            dpbank1.ClearSelection();
                            ListItem cli = dpbank1.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(supcredid));
                            if (cli != null) cli.Selected = true;
                        }
                    }


                    frPaymode = ds.Tables[0].Rows[0]["FreightPaymode"].ToString();
                    dpfreightpaymode.ClearSelection();
                    ListItem yLi = dpfreightpaymode.Items.FindByValue(frPaymode.Trim());
                    if (yLi != null) yLi.Selected = true;

                    if (paymode2Visible(frPaymode))
                    {
                        if (ds.Tables[0].Rows[0]["frCardNo"] != null)
                            txtfreightcardno.Text = Convert.ToString(ds.Tables[0].Rows[0]["frCardNo"]);
                        if (ds.Tables[0].Rows[0]["FrCreditorID"] != null)
                        {
                            //dpfreightbank.ClearSelection();
                            //ListItem cli = dpfreightbank.Items.FindByText(HttpUtility.HtmlDecode(Convert.ToString(ds.Tables[0].Rows[0]["FrDebtorID"])));

                            //if (cli != null) cli.Selected = true;

                            frcredid = Convert.ToString(ds.Tables[0].Rows[0]["FrCreditorID"]);
                            dpfreightbank.ClearSelection();
                            ListItem clli = dpfreightbank.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(frcredid));
                            if (clli != null) clli.Selected = true;

                            //rvbank.Enabled = true;
                            //rvCredit.Enabled = true;
                        }
                    }

                    luPaymode = ds.Tables[0].Rows[0]["LoadUnloadPaymode"].ToString();
                    dplupaymode.ClearSelection();
                    ListItem yyLi = dplupaymode.Items.FindByValue(luPaymode.Trim());
                    if (yyLi != null) yyLi.Selected = true;

                    if (paymode3Visible(luPaymode))
                    {
                        if (ds.Tables[0].Rows[0]["luCardNo"] != null)
                            txtlucardno.Text = Convert.ToString(ds.Tables[0].Rows[0]["luCardNo"]);
                        if (ds.Tables[0].Rows[0]["luCreditorID"] != null)
                        {
                            //dplubank.ClearSelection();
                            //ListItem cli = dplubank.Items.FindByText(HttpUtility.HtmlDecode(Convert.ToString(ds.Tables[0].Rows[0]["luDebtorID"])));

                            //if (cli != null) cli.Selected = true;

                            lucredid = Convert.ToString(ds.Tables[0].Rows[0]["luCreditorID"]);
                            dplubank.ClearSelection();
                            ListItem ci = dplubank.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(lucredid));
                            if (ci != null) ci.Selected = true;

                            //rvbank.Enabled = true;
                            //rvCredit.Enabled = true;
                        }
                    }

                    if (ds.Tables[0].Rows[0]["Freight"] != null)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["Freight"]) != "")
                            txtFreight.Text = Convert.ToString(ds.Tables[0].Rows[0]["Freight"]);
                        else
                            txtFreight.Text = "0";

                    }
                    else
                        txtFreight.Text = "0";

                    if (ds.Tables[0].Rows[0]["LoadUnload"] != null)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["LoadUnload"]) != "")
                            txtLU.Text = Convert.ToString(ds.Tables[0].Rows[0]["LoadUnload"]);
                        else
                            txtLU.Text = "0";
                    }
                    else
                        txtLU.Text = "0";

                    //hdContact.Value = row.Cells[13].Text;
                    itemDs = formProduct(CommissionID);

                    
                    pnlProduct.Visible = true;
                    Session["productDs"] = itemDs;

                    GrdViewItems.DataSource = itemDs;
                    GrdViewItems.DataBind();
                    

                    errPanel.Visible = false;
                    ErrMsg.Text = "";

                    cmdUpdateProduct.Enabled = false;
                    cmdSaveProduct.Enabled = true;
                    
                    cmdUpdateProduct.Visible = false;
                    
                    cmdSaveProduct.Visible = true;
                    
                    hdPrevSalesTotal.Value = lblNet.Text;

                    if (bl.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                    {
                        cmdSaveProduct.Enabled = false;
                        if (GrdViewItems.Columns[19] != null)
                            GrdViewItems.Columns[19].Visible = false;
                        if (GrdViewItems.Columns[18] != null)
                            GrdViewItems.Columns[18].Visible = false;
                        cmdSave.Enabled = false;
                        cmdDelete.Enabled = false;
                        cmdUpdate.Enabled = false;
                        //lnkBtnAdd.Visible = false;
                        AddNewProd.Enabled = false;
                    }

                }
            }
            updatePnlProduct.Update();
            UpdatePanel11.Update();
            updatePnlSales.Update();
            ModalPopupProduct.Hide();
            ModalPopupSales.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public DataSet formProduct(int salesID)
    {
        DataSet ds;
        DataSet dsRole;
        DataSet itemDs = new DataSet();
        DataTable dt;
        DataRow dr;
        DataColumn dc;

        double dTotal = 0;
        double dQty = 0;
        double dRate = 0;
        string strRole = string.Empty;
        string roleFlag = string.Empty;
        string strBundles = string.Empty;
        int iBundle = 0;
        int iRod = 0;

        double stock = 0;
        int billno = 0;
        string strItemCode = string.Empty;
        Session["roleDs"] = null;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetCommissionItemsForId(salesID);


        if (ds != null)
        {
            dt = new DataTable();

            dc = new DataColumn("itemCode");
            dt.Columns.Add(dc);

            dc = new DataColumn("CommissionNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductName");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductDesc");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("CustomerName");
            dt.Columns.Add(dc);

            dc = new DataColumn("CustomerID");
            dt.Columns.Add(dc);

            dc = new DataColumn("OtherCusName");
            dt.Columns.Add(dc);

            dc = new DataColumn("SellingPaymode");
            dt.Columns.Add(dc);

            dc = new DataColumn("CardNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("BankName");
            dt.Columns.Add(dc);

            dc = new DataColumn("BankID");
            dt.Columns.Add(dc);

            dc = new DataColumn("Measure_Unit");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Discount", typeof(double));
            dt.Columns.Add(dc);

            // krishnavelu 26 June
            dc = new DataColumn("ExecCharge");
            dt.Columns.Add(dc);

            dc = new DataColumn("VAT");
            dt.Columns.Add(dc);

            dc = new DataColumn("CST");
            dt.Columns.Add(dc);

            dc = new DataColumn("Roles");
            dt.Columns.Add(dc);

            dc = new DataColumn("IsRole");
            dt.Columns.Add(dc);

            dc = new DataColumn("Bundles");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rods");
            dt.Columns.Add(dc);
            dc = new DataColumn("Total");
            dt.Columns.Add(dc);



            itemDs.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    dr = itemDs.Tables[0].NewRow();

                    if (dR["Qty"] != null)
                        dQty = Convert.ToDouble(dR["Qty"]);
                    if (dR["Rate"] != null)
                        dRate = Convert.ToDouble(dR["Rate"]);

                    dTotal = dQty * dRate;
                    if (dR["ItemCode"] != null)
                    {
                        strItemCode = Convert.ToString(dR["ItemCode"]);
                        dr["itemCode"] = strItemCode;
                    }
                    if (dR["CommissionNo"] != null)
                    {
                        billno = Convert.ToInt32(dR["CommissionNo"]);
                        dr["CommissionNo"] = Convert.ToString(billno);
                    }
                    if (dR["ProductName"] != null)
                        dr["ProductName"] = Convert.ToString(dR["ProductName"]);
                    if (dR["ProductDesc"] != null)
                        dr["ProductDesc"] = Convert.ToString(dR["ProductDesc"]);

                    // krishnavelu 26 June
                    /*
                    if (dR["ExecIncharge"] != null && dR["ExecIncharge"].ToString() != "")
                    {
                        dr["ExecIncharge"] = Convert.ToString(dR["ExecIncharge"]);
                        
                        if (dR["ExecName"] != null && dR["ExecName"].ToString() != "")
                        {
                            dr["ExecName"] = Convert.ToString(dR["ExecName"]);
                        }
                    }
                    else
                    {
                        dr["ExecIncharge"] = "0";
                        dr["ExecName"] = " --NA-- ";
                    }*/

                    dr["Qty"] = dQty.ToString();
                    dr["Rate"] = dRate.ToString();

                     dr["Discount"] = "0";

                    
                        dr["Measure_Unit"] = "0";

                    
                        dr["VAT"] = "0";

                    // krishnavelu 26 June
                    
                        
                            dr["ExecCharge"] = "0";

                            if (dR["CustomerName"] != null)
                                dr["CustomerName"] = Convert.ToString(dR["CustomerName"]);

                            if (dR["CustomerID"] != null)
                                dr["CustomerID"] = Convert.ToInt32(dR["CustomerID"]);

                            if (dR["BankName"] != null)
                                dr["BankName"] = Convert.ToString(dR["BankName"]);

                            if (dR["SellingPaymode"] != null)
                                dr["SellingPaymode"] = Convert.ToString(dR["SellingPaymode"]);

                            if (dR["CardNo"] != null)
                                dr["CardNo"] = Convert.ToString(dR["CardNo"]);

                            if (dR["BankID"] != null)
                                dr["BankID"] = Convert.ToInt32(dR["BankID"]);

                            if (dR["OtherCustName"] != null)
                                dr["OtherCusName"] = Convert.ToString(dR["OtherCustName"]);
                    
                        dr["CST"] = "0";

                    
                        dr["Bundles"] = "0";
                    
                        dr["Rods"] = "0";
                    if (dR["isrole"] != null)
                    {
                        roleFlag = Convert.ToString(dR["isrole"]);
                        dr["IsRole"] = roleFlag;

                    }

                    //
                    if (roleFlag == "Y")
                    {

                        dsRole = bl.ListRoles(billno, strItemCode);
                        if (dsRole != null)
                        {
                            if (dsRole.Tables[0] != null)
                            {
                                foreach (DataRow drole in dsRole.Tables[0].Rows)
                                {
                                    strRole = strRole + drole["RoleID"].ToString() + "_" + drole["Qty"].ToString() + ",";

                                }

                            }
                        }


                        if (strRole.EndsWith(","))
                        {
                            strRole = strRole.Remove(strRole.Length - 1, 1);
                        }

                    }
                    else
                    {
                        strRole = "NO ROLE";
                    }

                    if (hdStock.Value != "")
                        stock = Convert.ToDouble(hdStock.Value);
                    dr["Roles"] = strRole;
                    dr["Total"] = Convert.ToString(dTotal);
                    itemDs.Tables[0].Rows.Add(dr);
                    strRole = "";
                }
            }
        }
        return itemDs;


    }
    #endregion

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

    private void calcSum()
    {
        Double sumAmt = 0;
        //Double sumTAmt = 0;
        Double sumVat = 0;
        Double sumRate = 0;
        Double sumExecComm = 0;
        Double sumCST = 0;
        Double sumDis = 0;
        Double sumNet = 0;
        DataSet ds = new DataSet();
        // ds.ReadXml(Server.MapPath("Reports\\" + hdFilename.Value + "_productsales.xml"));
        ds = (DataSet)GrdViewItems.DataSource;
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Total"] != null)
                        sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDouble(dr["Qty"]), Convert.ToDouble(dr["Rate"])));
                    //sumTAmt = sumTAmt + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"]));
                    sumDis = sumDis + GetDis();
                    sumVat = sumVat + GetVat();
                    sumCST = sumCST + GetCST();
                    sumRate = sumRate + GetTotalRate();
                }
            }
        }
        double dFreight = 0;
        double dLU = 0;
        double sumLUFreight = 0;

        double dcommission = 0;
        double CommissionPer = 0;
        double salval = 0;
        if (txtFreight.Text.Trim() != "")
        {
            dFreight = Convert.ToDouble(txtFreight.Text.Trim());
        }
        if (txtLU.Text.Trim() != "")
        {
            dLU = Convert.ToDouble(txtLU.Text.Trim());
        }

        if (txtCommissionPercent.Text.Trim() != "")
        {
            CommissionPer = Convert.ToDouble(txtCommissionPercent.Text.Trim());
        }

        
        salval = sumAmt;
        sumLUFreight = dFreight + dLU;

        sumNet = sumNet + sumAmt ;

        if(chkboxAll.Checked == false)
        {
            //if ((txtcommissionvalue.Text.Trim() == "0") || (txtcommissionvalue.Text.Trim() == "") || (txtcommissionvalue.Text.Trim() == "0.00") || (txtcommissionvalue.Text.Trim() == "0.0"))
            //{
                dcommission = (sumNet * (CommissionPer / 100));
            //}
            //else
            //    dcommission = Convert.ToDouble(txtcommissionvalue.Text);
        }
        else
        {
            dcommission = Convert.ToDouble(txtcommissionvalue.Text);
        }

        sumNet = sumNet - dcommission;

        sumNet = sumNet - dFreight - dLU;

        lblTotalSum.Text = sumAmt.ToString("#0.00");
        
        lblFreight.Text = sumLUFreight.ToString("#0.00"); // dFreight.ToString("#0.00");
        lblNet.Text = sumNet.ToString("#0.00");
        txtcommissionval.Text = dcommission.ToString("#0.00");

        txtcommissionvalue.Text = dcommission.ToString("#0.00");

        lblsales.Text = salval.ToString("#0.00");
        hdPrevSalesTotal.Value = lblNet.Text;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            int BillNo = 0;
            int TransNo = 0;

            if (!string.IsNullOrEmpty(txtBillnoSrc.Text))
                BillNo = Convert.ToInt32(txtBillnoSrc.Text);

            if (!string.IsNullOrEmpty(txtTransNo.Text))
                TransNo = Convert.ToInt32(txtTransNo.Text);

            BindGrid(BillNo, TransNo);
            //Accordion1.SelectedIndex = 0;
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();

            lblTotalSum.Text = "0";
            
            lblFreight.Text = "0";
            lblNet.Text = "0";

            //PanelBill.Visible = true;
            //PanelCmd.Visible = false;
            //lnkBtnAdd.Visible = true;

            Reset();

            ResetProduct();

            cmbProdAdd.Enabled = true;
            cmdUpdateProduct.Enabled = false;
            cmdSaveProduct.Enabled = true;

            Session["productDs"] = null;

            cmdSave.Enabled = true;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            cmdPrint.Enabled = false;
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtLU_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["productDs"] != null)
            {
                GrdViewItems.DataSource = (DataSet)Session["productDs"];
                GrdViewItems.DataBind();
                calcSum();
                UpdatePanelTotalSummary.Update();
            }
            UpdatePanel10.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtcommissionvalue_TextChanged(object sender, EventArgs e)
    {
        try
        {
            chkboxAll.Checked = true;
            if (Session["productDs"] != null)
            {
                GrdViewItems.DataSource = (DataSet)Session["productDs"];
                GrdViewItems.DataBind();
                calcSum();
                UpdatePanelTotalSummary.Update();
            }
            //UpdatePanel9.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtFreight_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["productDs"] != null)
            {
                GrdViewItems.DataSource = (DataSet)Session["productDs"];
                GrdViewItems.DataBind();
                calcSum();
                hdPrevSalesTotal.Value = lblNet.Text;
                UpdatePanelTotalSummary.Update();

            }
            UpdatePanel10.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewCommission_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string connection = Request.Cookies["Company"].Value;
                string recondate = string.Empty;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                int sBillNo = Convert.ToInt32(GrdViewCommission.DataKeys[e.RowIndex].Value.ToString());
                string UserID = Page.User.Identity.Name;

                var salesData = bl.GetCommissionForId(sBillNo);

                if (salesData != null && salesData.Tables[0].Rows.Count > 0)
                {

                    recondate = salesData.Tables[0].Rows[0]["CommissionDate"].ToString();

                    if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid. Please contact Administrator.')", true);
                        return;
                    }

                    //if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                    //{
                    //    var receivedBill = bl.IsAmountPaidForBill(sBillNo.ToString());

                    //    if (receivedBill != string.Empty)
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                    //        return;
                    //    }
                    //}

                    //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

                    bl.DeleteCommission(sBillNo, UserID);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Commission Details Deleted Successfully. Commission No. was " + sBillNo.ToString() + "')", true);
                    BindGrid(0, 0);
                    Session["ProductDs"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void LoadProducts(object sender, EventArgs e)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
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

    protected void LoadForProduct(object sender, EventArgs e)
    {
        //string itemCode = cmbProdAdd.SelectedValue;
        //cmbModel.SelectedValue = itemCode;
        //cmbBrand.SelectedValue = itemCode;
        cmbProdAdd_SelectedIndexChanged(sender, e);
    }

    protected void txtBillDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbCustomer.SelectedItem.Value != "0")
            {
                GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                UpdatePanel11.Update();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtRAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //TextBox txt = (TextBox)sender;

            //if(txt.Text != "")
            //{
            //    var total = double.Parse( lblReceivedTotal.Text);

            //    total = total + double.Parse(txt.Text);
            //    lblReceivedTotal.Text = total.ToString();
            //}

            var total = 0.0;

            if (txtAmount1.Text != "")
                total += double.Parse(txtAmount1.Text);
            if (txtAmount2.Text != "")
                total += double.Parse(txtAmount2.Text);
            if (txtCashAmount.Text != "")
                total += double.Parse(txtCashAmount.Text);
            if (txtCashAmount.Text != "")
                total += double.Parse(txtCashAmount.Text);

            lblReceivedTotal.Text = total.ToString();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void cmdcat_click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("CustomerInfo.aspx?myname=" + "NEWCUS");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void cmdprod_click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ProductMaster.aspx?myname=" + "NEWP");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("itemcode", typeof(string)));
        dt.Columns.Add(new DataColumn("ProductName", typeof(string)));
        dt.Columns.Add(new DataColumn("ProductDesc", typeof(string)));
        dt.Columns.Add(new DataColumn("Rate", typeof(Double)));
        dt.Columns.Add(new DataColumn("Qty", typeof(Double)));
        dt.Columns.Add(new DataColumn("ExecCharge", typeof(string)));
        dt.Columns.Add(new DataColumn("Measure_Unit", typeof(string)));
        dt.Columns.Add(new DataColumn("Discount", typeof(Double)));
        dt.Columns.Add(new DataColumn("Vat", typeof(Double)));
        dt.Columns.Add(new DataColumn("CST", typeof(Double)));
        dt.Columns.Add(new DataColumn("Roles", typeof(string)));

        dr = dt.NewRow();
        dr["itemcode"] = string.Empty;
        dr["ProductName"] = string.Empty;
        dr["ProductDesc"] = string.Empty;
        dr["Rate"] = 0;
        dr["Qty"] = 0;
        dr["ExecCharge"] = string.Empty;
        dr["Measure_Unit"] = string.Empty;
        dr["Discount"] = 0;
        dr["Vat"] = 0;
        dr["CST"] = 0;
        dr["Roles"] = hdCurrRole.Value;
        
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        GrdViewItems.DataSource = dt;
        GrdViewItems.DataBind();
    }



    private void EmptyRow()
    {


        var ds = new DataSet();
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        var dt = new DataTable();

        DataRow drNew;
        DataColumn dc;

        dc = new DataColumn("itemCode");
        dt.Columns.Add(dc);

        dc = new DataColumn("Billno");
        dt.Columns.Add(dc);

        dc = new DataColumn("ProductName");
        dt.Columns.Add(dc);

        dc = new DataColumn("ProductDesc");
        dt.Columns.Add(dc);

        dc = new DataColumn("Qty");
        dt.Columns.Add(dc);

        dc = new DataColumn("Rate");
        dt.Columns.Add(dc);

        dc = new DataColumn("CustomerName");
        dt.Columns.Add(dc);

        dc = new DataColumn("CustomerID");
        dt.Columns.Add(dc);

        dc = new DataColumn("OtherCusName");
        dt.Columns.Add(dc);

        dc = new DataColumn("SellingPaymode");
        dt.Columns.Add(dc);

        dc = new DataColumn("CardNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("BankName");
        dt.Columns.Add(dc);

        dc = new DataColumn("BankID");
        dt.Columns.Add(dc);

        dc = new DataColumn("Measure_unit");
        dt.Columns.Add(dc);

        dc = new DataColumn("Discount");
        dt.Columns.Add(dc);

        dc = new DataColumn("ExecCharge");
        dt.Columns.Add(dc);

        dc = new DataColumn("VAT");
        dt.Columns.Add(dc);

        dc = new DataColumn("CST");
        dt.Columns.Add(dc);

        dc = new DataColumn("Roles");
        dt.Columns.Add(dc);

        dc = new DataColumn("IsRole");
        dt.Columns.Add(dc);

        dc = new DataColumn("Total");
        dt.Columns.Add(dc);

        dc = new DataColumn("Bundles");
        dt.Columns.Add(dc);

        dc = new DataColumn("Rods");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        drNew = dt.NewRow();

        string disType = GetDiscType();
        string textvalue = null;

        drNew["itemCode"] = string.Empty;
        drNew["Billno"] = "";
        drNew["ProductName"] = string.Empty;
        drNew["ProductDesc"] = string.Empty;
        drNew["CustomerID"] = Convert.ToInt32(textvalue);
        drNew["OtherCusName"] = string.Empty;
        drNew["SellingPaymode"] = string.Empty;
        drNew["CardNo"] = string.Empty;
        drNew["BankName"] = string.Empty;
        drNew["CustomerName"] = string.Empty;
        drNew["BankID"] = Convert.ToDouble(textvalue);
        drNew["Qty"] = Convert.ToDouble(textvalue);
        drNew["Measure_Unit"] = string.Empty;
        drNew["Rate"] = Convert.ToDouble(textvalue);
        drNew["Discount"] = Convert.ToDouble(textvalue);
        drNew["ExecCharge"] = Convert.ToDouble(textvalue);
        drNew["VAT"] = Convert.ToDouble(textvalue);
        drNew["CST"] = Convert.ToDouble(textvalue);
        drNew["Roles"] = hdCurrRole.Value;
        drNew["IsRole"] = "N";
        drNew["Total"] = string.Empty;
        drNew["Bundles"] = "";
        drNew["Rods"] = "";
            
        ds.Tables[0].Rows.Add(drNew);

        ds.Tables[0].AcceptChanges();

        GrdViewItems.Columns[19].Visible = false;
        GrdViewItems.Columns[18].Visible = false;

        GrdViewItems.DataSource = ds;
        GrdViewItems.DataBind();

        GrdViewItems.Rows[0].Cells[4].Text = null;
        GrdViewItems.Rows[0].Cells[3].Text = null;
        GrdViewItems.Rows[0].Cells[5].Text = null;
        GrdViewItems.Rows[0].Cells[7].Text = null;
        GrdViewItems.Rows[0].Cells[9].Text = null;
        GrdViewItems.Rows[0].Cells[10].Text = null;
        GrdViewItems.Rows[0].Cells[8].Text = null;
    }
    private void LoadForTotal()
    {
        double vatper;
        double Total;
        double VatAmt;

        double subtot;
        if (lblVATAdd.Text == "14.5")
        {
            vatper = 1.145;
            subtot = (Convert.ToDouble(txttotal.Text) - Convert.ToDouble(lblDisAdd.Text));
            txtsubtot.Text = subtot.ToString("#0.00");
            Total = ((Convert.ToDouble(txttotal.Text) - Convert.ToDouble(lblDisAdd.Text)) / vatper);
            txtRateAdd.Text = Total.ToString("#0.00");
            VatAmt = (Convert.ToDouble(txttotal.Text) - Convert.ToDouble(txtRateAdd.Text));
            TextBox1.Text = VatAmt.ToString("#0.00");
        }
        if (lblVATAdd.Text == "5")
        {
            vatper = 1.05;
            Total = ((Convert.ToDouble(txttotal.Text) - Convert.ToDouble(lblDisAdd.Text)) / vatper);
            txtRateAdd.Text = Total.ToString("#0.00");
            VatAmt = (Convert.ToDouble(txttotal.Text) - Convert.ToDouble(txtRateAdd.Text));
            TextBox1.Text = VatAmt.ToString("#0.00");
        }
    }

    protected void BtnGet_Click(object sender, EventArgs e)
    {
        try
        {
            LoadForTotal();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}

