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

public partial class CustomerSales : System.Web.UI.Page
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
    string EnableVat = string.Empty;
    string EnableDiscount = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));


            if (!IsPostBack)
            {
                BusinessLogic objChk = new BusinessLogic(sDataSource);

                CheckSMSRequired();
                GrdViewSales.PageSize = 8;

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

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                mrBillDate.MaximumValue = dtaa;


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
                    lblDiscType.Text = "Disc(%)";
                    cvDisc.Enabled = true;
                }
                else if (discType == "RUPEE")
                {
                    cvDisc.Enabled = false;
                    lblDiscType.Text = "Disc(INR)";
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

                BillingMethod = bl.getConfigInfoMethod();
                if (BillingMethod == "VAT INCLUSIVE")
                {
                    rowTotal.Visible = false;
                    rowTotal1.Visible = false;
                    Labelll.Text = "VAT INCLUSIVE";
                }
                else
                {
                    rowTotal.Visible = false;
                    rowTotal1.Visible = false;
                    Labelll.Text = "VAT EXCLUSIVE";
                }

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



                if (Session["EMAILREQUIRED"] != null)
                {
                    if (Session["EMAILREQUIRED"].ToString() == "NO")
                        hdEmailRequired.Value = "NO";
                    else
                        hdEmailRequired.Value = "YES";
                }
                else
                {
                    hdEmailRequired.Value = "NO";
                }


                EnableVat = bl.getEnableVatConfig();
                if (EnableVat == "YES")
                {
                    lblVATAdd.Enabled = true;
                }
                else
                {
                    lblVATAdd.Enabled = false;
                }

                EnableDiscount = bl.getEnableDiscountConfig();
                if (EnableDiscount == "YES")
                {
                    lblDisAdd.Enabled = true;
                }
                else
                {
                    lblDisAdd.Enabled = false;
                }

                if (Session["NEWSALES"] != null)
                {
                    if (Session["NEWSALES"].ToString() == "Y")
                    {
                        ModalPopupMethod.Show();
                    }
                    else
                    {
                        txtBillDate.Text = DateTime.Now.ToShortDateString();
                    }
                }
                else
                {

                    txtBillDate.Text = DateTime.Now.ToShortDateString();
                }
                loadBanks();
                loadEmp();
                
                //loadCustomer();
                loadSupplier("Sundry Debtors");
                loadCategories();
                LoadProducts(this, null);

                rowmanual.Visible = false;
                txtBillDate.Focus();
                //BindGrid(0, 0);
                BindGrid("", "");
                ViewState["SortExpression"] = "";
                ViewState["SortDirection"] = "";
                //pnlSalesForm.Visible = false;
                //PanelBill.Visible = true;
                updatePnlProduct.Update();
                updatePnlSales.Update();
                ModalPopupSales.Hide();
                ModalPopupProduct.Hide();
                CheckOffline(objChk);

                ModalPopupMethod.Hide();


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveAdd(usernam, "SALES"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New item ";
                }



                if (Request.QueryString["myname"] != null)
                {
                    string myNam = Request.QueryString["myname"].ToString();
                    if (myNam == "NEWSAL")
                    {
                        ModalPopupMethod.Show();
                    }
                }
                FirstGridViewRow();

                //BusinessLogic bl = new BusinessLogic(sDataSource);
                DataSet dss = new DataSet();
                dss = bl.ListCreditorDebitor_DrptxtCheck(sDataSource);
                drpMobile1.DataSource = dss.Tables[0].DefaultView;
                drpMobile1.DataTextField = "LedgerName";
                drpMobile1.DataValueField = "LedgerID";
                drpMobile1.DataBind();
                ListItem item = new ListItem("-Select Customer-", "-1");
                drpMobile1.Items.Insert(0, item);

                BindList();
                //AddNewRow();                
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

    private void BindList()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataTable table = new DataTable();
        table = bl.ListCreditorDebitor_Lv(sDataSource);
        //ListView2.DataSource = table;
        //ListView2.DataBind();
    }


    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
        //ListView2.Visible = true;
        //ListView2.Items.Clear();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataTable table = new DataTable();
        table = bl.ListCreditorDebitor_LvSearch(sDataSource, TextBox4.Text);
        //ListView2.DataSource = table;
        //ListView2.DataBind();
    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        BindGrid("", "");
        ddCriteria.SelectedIndex = 0;
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
            GrdViewItems.Columns[14].Visible = false;
            GrdViewItems.Columns[15].Visible = false;
            cmdSave.Enabled = false;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            lnkBtnAdd.Visible = false;
            GrdViewSales.Columns[11].Visible = false;
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
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "CURRENCY")
                {
                    currency = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    _currencyType = currency;
                    Session["CurrencyType"] = currency;
                }

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "BARCODE")
                {
                    BarCodeRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }

        if ((currency == "INR") || (currency.ToUpper() == "RS"))
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

    private void loadPurchaseID()
    {
        drpPurID.Items.Clear();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListPurchaseID();
        drpPurID.Items.Clear();
        drpPurID.Items.Add(new ListItem("Select Purchase InvoiceNo", "0"));
        drpPurID.DataSource = ds;
        drpPurID.DataBind();
        drpPurID.DataTextField = "PurchaseID";
        drpPurID.DataValueField = "PurchaseID";
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

        //drpIncharge.DataSource = ds;
        //drpIncharge.DataBind();
        //drpIncharge.DataTextField = "empFirstName";
        //drpIncharge.DataValueField = "empno";
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
        ds = bl.ListBankLedgerpaymnetIsActive();
        drpBankName.Items.Clear();
        drpBankName.Items.Add(new ListItem("Select Bank", "0"));
        drpBankName.DataSource = ds;
        drpBankName.DataTextField = "LedgerName";
        drpBankName.DataValueField = "LedgerID";
        drpBankName.DataBind();

        ddBank1.Items.Clear();
        ddBank1.Items.Add(new ListItem("Select Bank", "0"));
        ddBank1.DataSource = ds;
        ddBank1.DataTextField = "LedgerName";
        ddBank1.DataValueField = "LedgerID";
        ddBank1.DataBind();

        ddBank2.Items.Clear();
        ddBank2.Items.Add(new ListItem("Select Bank", "0"));
        ddBank2.DataSource = ds;
        ddBank2.DataTextField = "LedgerName";
        ddBank2.DataValueField = "LedgerID";
        ddBank2.DataBind();

        ddBank3.Items.Clear();
        ddBank3.Items.Add(new ListItem("Select Bank", "0"));
        ddBank3.DataSource = ds;
        ddBank3.DataTextField = "LedgerName";
        ddBank3.DataValueField = "LedgerID";
        ddBank3.DataBind();

    }

    private void loadBanksEdit()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBankLedgerpaymnet();
        drpBankName.Items.Clear();
        drpBankName.Items.Add(new ListItem("Select Bank", "0"));
        drpBankName.DataSource = ds;
        drpBankName.DataTextField = "LedgerName";
        drpBankName.DataValueField = "LedgerID";
        drpBankName.DataBind();

        ddBank1.Items.Clear();
        ddBank1.Items.Add(new ListItem("Select Bank", "0"));
        ddBank1.DataSource = ds;
        ddBank1.DataTextField = "LedgerName";
        ddBank1.DataValueField = "LedgerID";
        ddBank1.DataBind();

        ddBank2.Items.Clear();
        ddBank2.Items.Add(new ListItem("Select Bank", "0"));
        ddBank2.DataSource = ds;
        ddBank2.DataTextField = "LedgerName";
        ddBank2.DataValueField = "LedgerID";
        ddBank2.DataBind();

        ddBank3.Items.Clear();
        ddBank3.Items.Add(new ListItem("Select Bank", "0"));
        ddBank3.DataSource = ds;
        ddBank3.DataTextField = "LedgerName";
        ddBank3.DataValueField = "LedgerID";
        ddBank3.DataBind();

    }

    private void loadCategories()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic();
        DataSet ds = new DataSet();
        string method = string.Empty;

        if (Session["Method"] == "Add")
        {
            method = "Add";
        }
        else if (Session["Method"] == "Edit")
        {
            method = "Edit";
        }
        ds = bl.ListCategory(sDataSource, method);
        cmbCategory.DataTextField = "CategoryName";
        cmbCategory.DataValueField = "CategoryID";
        cmbCategory.DataSource = ds;
        cmbCategory.DataBind();
    }


    private void loadSupplier(string SundryType)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string connection = Request.Cookies["Company"].Value;
        DataSet dsd = new DataSet();
        dsd = bl.ListCusCategory(connection);
        drpCustomerCategoryAdd.Items.Clear();
        drpCustomerCategoryAdd.Items.Add(new ListItem("Select Customer Category", "0"));
        drpCustomerCategoryAdd.DataSource = dsd;
        drpCustomerCategoryAdd.DataBind();
        drpCustomerCategoryAdd.DataTextField = "CusCategory_Name";
        drpCustomerCategoryAdd.DataValueField = "CusCategory_Value";

        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

        DataSet ds = new DataSet();

        if (SundryType == "Sundry Debtors")
        {
            if (drpIntTrans.SelectedValue == "YES")
            {
                //ds = bl.ListSundryDebtorsTransfer(sDataSource);
                ds = bl.ListLedgersTransferIsActive(sDataSource);
            }
            else if (ddDeliveryNote.SelectedValue == "YES")
            {
                //ds = bl.ListSundryDebtorsDc(sDataSource);
                ds = bl.ListSundryLedgersDcIsActive(sDataSource);
            }
            else
            {
                ds = bl.ListSundryDebtorsExceptIsActive(sDataSource);
            }
        }

        if (SundryType == "Sundry Creditors")
        {
            //ds = bl.ListSundryCreditors(sDataSource);
            ds = bl.ListSundryCreditorsExceptIsActive(sDataSource);
        }

        cmbCustomer.Items.Clear();
        cmbCustomer.Items.Add(new ListItem("Select Customer", "0"));
        cmbCustomer.DataSource = ds;
        cmbCustomer.DataBind();
        cmbCustomer.DataTextField = "LedgerName";
        cmbCustomer.DataValueField = "LedgerID";
        //cmbCustomer.Focus();

        drpMobile.Items.Clear();
        drpMobile.Items.Add(new ListItem("Select Mobile", "0"));
        drpMobile.DataSource = ds;
        drpMobile.DataBind();
        drpMobile.DataTextField = "Mobile";
        drpMobile.DataValueField = "LedgerID";


    }

    private void loadSupplierEdit(string SundryType)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string connection = Request.Cookies["Company"].Value;
        DataSet dsd = new DataSet();
        dsd = bl.ListCusCategory(connection);
        drpCustomerCategoryAdd.Items.Clear();
        drpCustomerCategoryAdd.Items.Add(new ListItem("Select Customer Category", "0"));
        drpCustomerCategoryAdd.DataSource = dsd;
        drpCustomerCategoryAdd.DataBind();
        drpCustomerCategoryAdd.DataTextField = "CusCategory_Name";
        drpCustomerCategoryAdd.DataValueField = "CusCategory_Value";

        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

        DataSet ds = new DataSet();

        if (SundryType == "Sundry Debtors")
        {
            if (drpIntTrans.SelectedValue == "YES")
            {
                //ds = bl.ListSundryDebtorsTransfer(sDataSource);
                ds = bl.ListLedgersTransfer(sDataSource);
            }
            else if (ddDeliveryNote.SelectedValue == "YES")
            {
                //ds = bl.ListSundryDebtorsDc(sDataSource);
                ds = bl.ListSundryLedgersDc(sDataSource);
            }
            else
            {
                ds = bl.ListSundryDebtorsExcept(sDataSource);
            }
        }

        if (SundryType == "Sundry Creditors")
        {
            //ds = bl.ListSundryCreditors(sDataSource);
            ds = bl.ListSundryCreditorsExcept(sDataSource);
        }

        cmbCustomer.Items.Clear();
        cmbCustomer.Items.Add(new ListItem("Select Customer", "0"));
        cmbCustomer.DataSource = ds;
        cmbCustomer.DataBind();
        cmbCustomer.DataTextField = "LedgerName";
        cmbCustomer.DataValueField = "LedgerID";
        //cmbCustomer.Focus();

        drpMobile.Items.Clear();
        drpMobile.Items.Add(new ListItem("Select Mobile", "0"));
        drpMobile.DataSource = ds;
        drpMobile.DataBind();
        drpMobile.DataTextField = "Mobile";
        drpMobile.DataValueField = "LedgerID";


    }

    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);
    //    //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
    //    GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
    //    //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
    //    GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
    //    GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    //}

    private void BindGrid(string textSearch, string dropDown)
    {
        string connection = Request.Cookies["Company"].Value;

        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        object usernam = Session["LoggedUserName"];

        //if (textSearch == "")
        //ds = bl.GetSales();
        ds = bl.GetSalesList(connection, textSearch, dropDown);
        //else
        //    ds = bl.GetSalesForId(textSearch, dropDown);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewSales.DataSource = ds.Tables[0].DefaultView;
                GrdViewSales.DataBind();
                //PanelBill.Visible = true;
            }
        }
        else
        {
            GrdViewSales.DataSource = null;
            GrdViewSales.DataBind();
            //PanelBill.Visible = true;
        }
    }

    private DataSet BindGridData(int strBillno)
    {
        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        object usernam = Session["LoggedUserName"];

        if (strBillno == 0)
            ds = bl.GetSales();
        else
            ds = bl.GetSalesForId(strBillno);

        //PanelBill.Visible = true;
        return ds;
    }

    #endregion


    private void CheckSMSRequired()
    {
        DataSet appSettings;
        string smsRequired = string.Empty;
        string emailRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "EMAILREQ")
                {
                    emailRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["EMAILREQUIRED"] = emailRequired.Trim().ToUpper();
                }

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "CREDITEXD")
                {
                    Session["CREDITEXD"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString().Trim().ToUpper();
                    hdCREDITEXD.Value = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString().Trim().ToUpper();
                }

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "OWNERMOB")
                {
                    Session["OWNERMOB"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }

            }
        }
        else
        {
            BusinessLogic bl = new BusinessLogic();
            DataSet ds = bl.GetAppSettings(Request.Cookies["Company"].Value);

            if (ds != null)
                Session["AppSettings"] = ds;

            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "EMAILREQ")
                {
                    emailRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["EMAILREQUIRED"] = emailRequired.Trim().ToUpper();
                }

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "CREDITEXD")
                {
                    Session["CREDITEXD"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString().Trim().ToUpper();
                    hdCREDITEXD.Value = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString().Trim().ToUpper();
                }

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "OWNERMOB")
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
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "DISCTYPE")
                {
                    discType = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["DISCTYPE"] = discType.Trim().ToUpper();
                }

            }
        }
        else if (Session["AppSettings"] == null)
        {
            BusinessLogic bl = new BusinessLogic();
            DataSet ds = bl.GetAppSettings(Request.Cookies["Company"].Value);

            if (ds != null)
                Session["AppSettings"] = ds;

            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "DISCTYPE")
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
            GrdViewSales.PageIndex = ((DropDownList)sender).SelectedIndex;
            //int strBillno = 0;
            //int strTransno = 0;
            //if (txtBillnoSrc.Text.Trim() != "")
            //    strBillno = Convert.ToInt32(txtBillnoSrc.Text.Trim());

            //if (txtTransNo.Text.Trim() != "")
            //    strTransno = Convert.ToInt32(txtTransNo.Text.Trim());

            string textt = string.Empty;
            string dropd = string.Empty;

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;

            BindGrid(textt, dropd);
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
            if (drpPurchaseReturn.SelectedValue == "NO")
            {
                rowReason.Visible = false;
                loadSupplier("Sundry Debtors");
            }
            else
            {
                rowReason.Visible = true;
                loadSupplier("Sundry Creditors");
            }
            txtAddress.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtAddress3.Text = string.Empty;
            txtCustPh.Text = string.Empty;
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

    protected void ddBank1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtCCard1.Text = "0";
            txtRefNo1.Text = "0";
            txtRefNo1.Focus();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddBank2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtCCard2.Text = "0";
            txtRefNo2.Text = "0";
            txtRefNo2.Focus();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddBank3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtCCard3.Text = "0";
            txtRefNo3.Text = "0";
            txtRefNo3.Focus();
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

            if (lblBillNo.Text != "Auto Generated.No need to enter")
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

                if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                {
                    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                    if (receivedBill != string.Empty)
                    {
                        //////drpPaymode.SelectedValue = "3";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                        //UpdatePanelPayMode.Update();
                        //return;
                    }
                }

                if (drpPaymode.SelectedValue == "4")
                {
                    divMultiPayment.Visible = true;
                    divAddMPayments.Visible = true;
                    divListMPayments.Visible = false;
                    UpdatePanelMP.Update();
                    lblReceivedTotal.Text = "0";
                }


            }
            else
            {
                if (drpPaymode.SelectedValue == "4")
                {
                    divMultiPayment.Visible = true;
                    divAddMPayments.Visible = true;
                    divListMPayments.Visible = false;
                    UpdatePanelMP.Update();
                    lblReceivedTotal.Text = "0";
                    //txtCCard3.Text = "0";
                    //txtCCard2.Text = "0";
                    //txtCCard1.Text = "0";
                }
                else
                {

                    divAddMPayments.Visible = true;
                    divListMPayments.Visible = false;
                    divMultiPayment.Visible = false;
                    UpdatePanelMP.Update();
                }
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
    bool aa;
    protected void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            BusinessLogic bl = new BusinessLogic(sDataSource);

            if (lblBillNo.Text != "Auto Generated.No need to enter")
            {
                string receivedBill = "";
                var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

                if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                {
                    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                    if (receivedBill != string.Empty)
                    {
                        if (cmbCustomer.Items.FindByValue(salesData.Tables[0].Rows[0]["DebtorID"].ToString()) != null)
                            cmbCustomer.SelectedValue = salesData.Tables[0].Rows[0]["DebtorID"].ToString();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                        UpdatePanel21.Update();
                        return;
                    }
                }

            }
            if (grvStudentDetails.Rows.Count == 1)
            {
                cmdCancel.Enabled = true;
                int iLedgerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                DataSet ds = bl.GetExecutive(iLedgerID);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //drpIncharge.ClearSelection();
                    //ListItem li = drpIncharge.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["executiveincharge"]));
                    //if (li != null) li.Selected = true;

                    drpMobile.ClearSelection();
                    ListItem lit = drpMobile.Items.FindByValue(Convert.ToString(iLedgerID));
                    if (lit != null) lit.Selected = true;

                    if (ds.Tables[0].Rows[0]["LedgerCategory"].ToString() != "")
                    {
                        lblledgerCategory.Text = Convert.ToString(ds.Tables[0].Rows[0]["LedgerCategory"]);
                        lblledgerCategory.Font.Bold = true;
                        lblledgerCategory.Visible = false;
                        drpCustomerCategoryAdd.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["LedgerCategory"]);
                    }
                    else
                    {
                        lblledgerCategory.Text = "";
                        lblledgerCategory.Visible = false;
                        drpCustomerCategoryAdd.SelectedValue = "";
                    }
                    // krishnavelu 26 June
                    txtOtherCusName.Text = cmbCustomer.SelectedItem.Text;

                    if (cmbCustomer.SelectedItem.Text.ToUpper() == "OTHERS")
                    {
                        txtOtherCusName.Visible = true;
                        txtOtherCusName.Focus();
                        txtOtherCusName.Text = "";
                    }
                    else
                    {
                        txtOtherCusName.Visible = false;
                        txtOtherCusName.Text = "";
                    }

                    if (ds.Tables[0].Rows[0]["CreditLimit"] != null)
                    {
                        if (ds.Tables[0].Rows[0]["CreditLimit"].ToString() != "")
                            hdCustCreditLimit.Value = ds.Tables[0].Rows[0]["CreditLimit"].ToString();
                        else
                            hdCustCreditLimit.Value = "0";
                    }
                    else
                        hdCustCreditLimit.Value = "0";

                    if (cmbCustomer.SelectedItem.Value != "0")
                    {
                        GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                        ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                    }

                }

                DataSet customerDs = bl.getAddressInfo(iLedgerID);
                string address = string.Empty;

                if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
                {
                    if (customerDs.Tables[0].Rows[0]["Add1"] != null)
                        txtAddress.Text = customerDs.Tables[0].Rows[0]["Add1"].ToString() + Environment.NewLine;

                    if (customerDs.Tables[0].Rows[0]["Add2"] != null)
                        txtAddress2.Text = address + customerDs.Tables[0].Rows[0]["Add2"].ToString() + Environment.NewLine;

                    if (customerDs.Tables[0].Rows[0]["Add3"] != null)
                        txtAddress3.Text = address + customerDs.Tables[0].Rows[0]["Add3"].ToString() + Environment.NewLine;

                    //txtAddress.Text = address;

                    if (customerDs.Tables[0].Rows[0]["Mobile"] != null)
                    {
                        hdContact.Value = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
                        txtCustPh.Text = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
                    }
                }
                else
                {
                    txtAddress.Text = string.Empty;
                    txtAddress2.Text = string.Empty;
                    txtAddress3.Text = string.Empty;
                    txtCustPh.Text = string.Empty;
                }
            }
            else if (grvStudentDetails.Rows.Count > 1)
            {
                cmdCancel.Enabled = true;
                int iLedgerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                DataSet ds = bl.GetExecutive(iLedgerID);
                if (ds.Tables[0].Rows[0]["LedgerCategory"].ToString() == drpCustomerCategoryAdd.SelectedValue)
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        //drpIncharge.ClearSelection();
                        //ListItem li = drpIncharge.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["executiveincharge"]));
                        //if (li != null) li.Selected = true;

                        drpMobile.ClearSelection();
                        ListItem lit = drpMobile.Items.FindByValue(Convert.ToString(iLedgerID));
                        if (lit != null) lit.Selected = true;

                        if (ds.Tables[0].Rows[0]["LedgerCategory"].ToString() != "")
                        {
                            lblledgerCategory.Text = Convert.ToString(ds.Tables[0].Rows[0]["LedgerCategory"]);
                            lblledgerCategory.Font.Bold = true;
                            lblledgerCategory.Visible = false;
                            drpCustomerCategoryAdd.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["LedgerCategory"]);
                        }
                        else
                        {
                            lblledgerCategory.Text = "";
                            lblledgerCategory.Visible = false;
                            drpCustomerCategoryAdd.SelectedValue = "";
                        }
                        // krishnavelu 26 June
                        txtOtherCusName.Text = cmbCustomer.SelectedItem.Text;

                        if (cmbCustomer.SelectedItem.Text.ToUpper() == "OTHERS")
                        {
                            txtOtherCusName.Visible = true;
                            txtOtherCusName.Focus();
                            txtOtherCusName.Text = "";
                        }
                        else
                        {
                            txtOtherCusName.Visible = false;
                            txtOtherCusName.Text = "";
                        }

                        if (ds.Tables[0].Rows[0]["CreditLimit"] != null)
                        {
                            if (ds.Tables[0].Rows[0]["CreditLimit"].ToString() != "")
                                hdCustCreditLimit.Value = ds.Tables[0].Rows[0]["CreditLimit"].ToString();
                            else
                                hdCustCreditLimit.Value = "0";
                        }
                        else
                            hdCustCreditLimit.Value = "0";

                        if (cmbCustomer.SelectedItem.Value != "0")
                        {
                            GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                            ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ajax", "<script language='javascript'>Confirm();</script>", false);
                    string confirmValue = Request.Form["confirm_value"];

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected Customer category is different from previous.Do you want to continue?')", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "confirm('Selected Customer category is different from previous.Do you want to continue?')", true);
                    if (confirmValue == "Yes")
                    {
                        FirstGridViewRow();
                    }
                    else if (confirmValue == "No")
                    {
                        DataSet customerDs = bl.getAddressInfo(iLedgerID);
                        string address = string.Empty;

                        if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
                        {
                            if (customerDs.Tables[0].Rows[0]["Add1"] != null)
                                txtAddress.Text = customerDs.Tables[0].Rows[0]["Add1"].ToString() + Environment.NewLine;

                            if (customerDs.Tables[0].Rows[0]["Add2"] != null)
                                txtAddress2.Text = address + customerDs.Tables[0].Rows[0]["Add2"].ToString() + Environment.NewLine;

                            if (customerDs.Tables[0].Rows[0]["Add3"] != null)
                                txtAddress3.Text = address + customerDs.Tables[0].Rows[0]["Add3"].ToString() + Environment.NewLine;

                            //txtAddress.Text = address;

                            if (customerDs.Tables[0].Rows[0]["Mobile"] != null)
                            {
                                hdContact.Value = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
                                txtCustPh.Text = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
                            }
                        }
                        else
                        {
                            txtAddress.Text = string.Empty;
                            txtAddress2.Text = string.Empty;
                            txtAddress3.Text = string.Empty;
                            txtCustPh.Text = string.Empty;
                        }
                    }
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void drpMobile_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            BusinessLogic bl = new BusinessLogic(sDataSource);

            if (lblBillNo.Text != "Auto Generated.No need to enter")
            {
                string receivedBill = "";
                var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

                if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                {
                    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                    if (receivedBill != string.Empty)
                    {
                        if (cmbCustomer.Items.FindByValue(salesData.Tables[0].Rows[0]["DebtorID"].ToString()) != null)
                            cmbCustomer.SelectedValue = salesData.Tables[0].Rows[0]["DebtorID"].ToString();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                        UpdatePanel21.Update();
                        return;
                    }
                }

            }

            cmdCancel.Enabled = true;
            int iLedgerID = Convert.ToInt32(drpMobile.SelectedItem.Value);
            DataSet ds = bl.GetExecutive(iLedgerID);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //drpIncharge.ClearSelection();
                //ListItem li = drpIncharge.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["executiveincharge"]));
                //if (li != null) li.Selected = true;

                cmbCustomer.ClearSelection();
                ListItem lit = cmbCustomer.Items.FindByValue(Convert.ToString(iLedgerID));
                if (lit != null) lit.Selected = true;

                if (ds.Tables[0].Rows[0]["LedgerCategory"].ToString() != "")
                {
                    lblledgerCategory.Text = Convert.ToString(ds.Tables[0].Rows[0]["LedgerCategory"]);
                    lblledgerCategory.Font.Bold = true;
                    lblledgerCategory.Visible = false;
                    drpCustomerCategoryAdd.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["LedgerCategory"]);
                }
                else
                {
                    lblledgerCategory.Text = "";
                    lblledgerCategory.Visible = false;
                    drpCustomerCategoryAdd.SelectedValue = "";
                }
                // krishnavelu 26 June
                txtOtherCusName.Text = cmbCustomer.SelectedItem.Text;

                if (cmbCustomer.SelectedItem.Text.ToUpper() == "OTHERS")
                {
                    txtOtherCusName.Visible = true;
                    txtOtherCusName.Focus();
                    txtOtherCusName.Text = "";
                }
                else
                {
                    txtOtherCusName.Visible = false;
                    txtOtherCusName.Text = "";
                }

                if (ds.Tables[0].Rows[0]["CreditLimit"] != null)
                {
                    if (ds.Tables[0].Rows[0]["CreditLimit"].ToString() != "")
                        hdCustCreditLimit.Value = ds.Tables[0].Rows[0]["CreditLimit"].ToString();
                    else
                        hdCustCreditLimit.Value = "0";
                }
                else
                    hdCustCreditLimit.Value = "0";

                if (cmbCustomer.SelectedItem.Value != "0")
                {
                    GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                    ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                }

            }

            DataSet customerDs = bl.getAddressInfo(iLedgerID);
            string address = string.Empty;

            if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            {
                if (customerDs.Tables[0].Rows[0]["Add1"] != null)
                    txtAddress.Text = customerDs.Tables[0].Rows[0]["Add1"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["Add2"] != null)
                    txtAddress2.Text = address + customerDs.Tables[0].Rows[0]["Add2"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["Add3"] != null)
                    txtAddress3.Text = address + customerDs.Tables[0].Rows[0]["Add3"].ToString() + Environment.NewLine;

                //txtAddress.Text = address;

                if (customerDs.Tables[0].Rows[0]["Mobile"] != null)
                {
                    hdContact.Value = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
                    txtCustPh.Text = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
                }
            }
            else
            {
                txtAddress.Text = string.Empty;
                txtAddress2.Text = string.Empty;
                txtAddress3.Text = string.Empty;
                txtCustPh.Text = string.Empty;
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void txtBarcode_Populated(object sender, EventArgs e) //Jolo Barcode
    {
        try
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


            if (cmbProdAdd.SelectedIndex != 0)
            {

                itemCode = cmbProdAdd.SelectedItem.Value;
                double chk = bl.getStockInfo(itemCode);
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

                    //string category = lblledgerCategory.Text;
                    string category = drpCustomerCategoryAdd.SelectedValue;
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

                NewDs = bl.ListSalesProductPriceDetails(ds.Tables[0].Rows[i]["itemCode"].ToString(), CatType);

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
        try
        {
            ModalPopupSales.Show();
            ModalPopupProduct.Show();
            ModalPopupMethod.Show();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();
            DataSet roleDs = new DataSet();
            string itemCode = string.Empty;
            DataSet checkDs;
            bool dupFlag = false;
            cmdDelete.Enabled = false;


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

                    double chk = bl.getStockInfo(itemCode);

                    txtstock.Text = Convert.ToString(chk);

                    if (chk <= 0)
                    {
                        if (e != null)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Current Stock Limit for ItemCode : " + cmbProdAdd.SelectedValue.Trim() + " is " + chk + "')", true);
                        //LoadProducts(this,null);
                        ResetProduct();
                        updatePnlProduct.Update();
                        return;
                    }
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
                    if (!dupFlag)
                    {
                        hdOpr.Value = "New";
                        hdCurrRole.Value = "";

                        //ds = bl.ListSalesProductPriceDetails(cmbProdAdd.SelectedItem.Value.Trim(), lblledgerCategory.Text);

                        ds = bl.ListSalesProductPriceDetails(cmbProdAdd.SelectedItem.Value.Trim(), drpCustomerCategoryAdd.SelectedValue);

                        string category = drpCustomerCategoryAdd.SelectedValue;

                        if (ds != null)
                        {
                            //lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                            lblProdDescAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);
                            txtExecCharge.Text = Convert.ToString(ds.Tables[0].Rows[0]["ExecutiveCommission"]);
                            //lblUnitMrmnt.Text = Convert.ToString(ds.Tables[0].Rows[0]["Measure_Unit"]);

                            //if (category == "Dealer")
                            //{
                            //    if ((optionmethod.SelectedValue == "NormalSales") || (optionmethod.SelectedValue == "PurchaseReturn") || (optionmethod.SelectedValue == "ManualSales"))
                            //    {
                            //        //lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerDiscount"]);
                            //        lblDisAdd.Text = "0";
                            //        lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Dealervat"]);
                            //        lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                            //    }
                            //    else
                            //    {
                            //        lblDisAdd.Text = "0";
                            //        lblVATAdd.Text = "0";
                            //        lblCSTAdd.Text = "0";
                            //    }
                            //    txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerRate"]);
                            //}
                            //else
                            //{
                            if ((optionmethod.SelectedValue == "NormalSales") || (optionmethod.SelectedValue == "PurchaseReturn") || (optionmethod.SelectedValue == "ManualSales"))
                            {
                                //lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                                lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                                lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["vat"]);
                                lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                            }
                            else
                            {
                                lblDisAdd.Text = "0";
                                lblVATAdd.Text = "0";
                                lblCSTAdd.Text = "0";
                            }
                            txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);

                            //}

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
                    }
                    else
                    {
                        //cmbProdAdd.SelectedIndex = 0;
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);

                        hdOpr.Value = "New";
                        hdCurrRole.Value = "";
                        ds = bl.ListSalesProductPriceDetails(cmbProdAdd.SelectedItem.Value.Trim(), drpCustomerCategoryAdd.SelectedValue);

                        string category = drpCustomerCategoryAdd.SelectedValue;

                        if (ds != null)
                        {
                            //lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                            lblProdDescAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);
                            txtExecCharge.Text = Convert.ToString(ds.Tables[0].Rows[0]["ExecutiveCommission"]);
                            //lblUnitMrmnt.Text = Convert.ToString(ds.Tables[0].Rows[0]["Measure_Unit"]);

                            //if (category == "Dealer")
                            //{
                            //    if ((optionmethod.SelectedValue == "NormalSales") || (optionmethod.SelectedValue == "PurchaseReturn") || (optionmethod.SelectedValue == "ManualSales"))
                            //    {
                            //        //lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerDiscount"]);
                            //        lblDisAdd.Text = "0";
                            //        lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Dealervat"]);
                            //        lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                            //    }
                            //    else
                            //    {
                            //        lblDisAdd.Text = "0";
                            //        lblVATAdd.Text = "0";
                            //        lblCSTAdd.Text = "0";
                            //    }
                            //    txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerRate"]);
                            //}
                            //else
                            //{
                            if ((optionmethod.SelectedValue == "NormalSales") || (optionmethod.SelectedValue == "PurchaseReturn") || (optionmethod.SelectedValue == "ManualSales"))
                            {
                                //lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                                lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                                lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["vat"]);
                                lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                            }
                            else
                            {
                                lblDisAdd.Text = "0";
                                lblVATAdd.Text = "0";
                                lblCSTAdd.Text = "0";
                            }
                            txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);

                            //}

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

                    }
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
        try
        {

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
            string vatamt = "";
            string sTotalmrp = "";

            double sVatamount = 0;
            string sSubtot = "";
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

            if (Page.IsValid)
            {
                stock = bl.getStockInfo(cmbProdAdd.SelectedItem.Value);

                if (Request.Cookies["Company"] != null)
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


                if ((drpnormalsales.SelectedItem.Text == "YES") || (drpPurchaseReturn.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                {
                    if (lblVATAdd.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Vat Cannot be Zero %.')", true);
                        ModalPopupProduct.Show();
                        return;
                    }
                }

                if ((drpnormalsales.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                {
                    double EXCLUSIVErate1 = 0;
                    double EXCrate1 = 0;
                    if (Labelll.Text == "VAT EXCLUSIVE")
                    {
                        EXCLUSIVErate1 = (Convert.ToDouble(txtRateAdd.Text)) + ((Convert.ToDouble(txtRateAdd.Text)) * (Convert.ToDouble(lblVATAdd.Text) / 100));
                    }
                    else
                    {
                        EXCLUSIVErate1 = (Convert.ToDouble(txtRateAdd.Text));
                    }
                    DataSet dst = bl.ListProductMRPPrices(cmbProdAdd.SelectedItem.Value.Trim());
                    if (dst != null)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drt in dst.Tables[0].Rows)
                            {
                                EXCrate1 = Convert.ToDouble(drt["rate"]);
                            }
                        }
                    }
                    if (EXCLUSIVErate1 > EXCrate1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be greater than " + EXCrate1 + "')", true);
                        ModalPopupProduct.Show();
                        return;
                    }
                }

                string usernam = Request.Cookies["LoggedUserName"].Value;
                if (bl.CheckIfUserCanDoDeviation(usernam))
                {

                }
                else
                {
                    if ((drpnormalsales.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                    {
                        double devvalue = 0;

                        DataSet dsd = bl.getRateInformation(cmbProdAdd.SelectedValue);
                        if (dsd.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drt in dsd.Tables[0].Rows)
                            {
                                devvalue = Convert.ToDouble(drt["deviation"]);
                            }
                        }

                        if (devvalue == 0)
                        {
                            DataSet dsrate = bl.getRateInfo(cmbProdAdd.SelectedValue);
                            double rate1 = 0;
                            double dp = 0;
                            double overall = 0;
                            double per = 0;
                            double rate2 = 0;

                            if (dsrate.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsrate.Tables[0].Rows)
                                {
                                    dp = Convert.ToDouble(dr["dealerrate"]);
                                    per = Convert.ToDouble(dr["deviation"]);
                                    rate2 = (dp * per) / 100;
                                    rate1 = Convert.ToDouble(txtRateAdd.Text);
                                    //rate2 = dp - overall;

                                    //rate2 = dp + overall;

                                    if (Labelll.Text == "VAT EXCLUSIVE")
                                    {
                                        rate1 = (Convert.ToDouble(txtRateAdd.Text)) + ((Convert.ToDouble(txtRateAdd.Text)) * (Convert.ToDouble(lblVATAdd.Text) / 100));
                                    }

                                    if (rate1 < rate2)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be less than dealer rate Rs. " + rate2 + "')", true);
                                        ModalPopupProduct.Show();
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            DataSet dsrate = bl.getRateInformation(cmbProdAdd.SelectedValue);
                            double rate1 = 0;
                            double dp = 0;
                            double overall = 0;
                            double per = 0;
                            double rate2 = 0;

                            if (dsrate.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsrate.Tables[0].Rows)
                                {
                                    dp = Convert.ToDouble(dr["dealerrate"]);
                                    per = Convert.ToDouble(dr["deviation"]);
                                    rate2 = (dp * per) / 100;
                                    rate1 = Convert.ToDouble(txtRateAdd.Text);
                                    //rate2 = dp - overall;

                                    //rate2 = dp + overall;

                                    if (Labelll.Text == "VAT EXCLUSIVE")
                                    {
                                        rate1 = (Convert.ToDouble(txtRateAdd.Text)) + ((Convert.ToDouble(txtRateAdd.Text)) * (Convert.ToDouble(lblVATAdd.Text) / 100));
                                    }

                                    if (rate1 < rate2)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be less than dealer rate Rs. " + rate2 + "')", true);
                                        ModalPopupProduct.Show();
                                        return;
                                    }
                                }
                            }
                        }

                    }
                }

                double chk = bl.getStockInfo(cmbProdAdd.SelectedItem.Value);
                double curQty = Convert.ToDouble(txtQtyAdd.Text);
                /*Start March 15 Modification */
                double QtyEdit = Convert.ToDouble(hdEditQty.Value);
                chk = chk + QtyEdit;
                /*End March 15 Modification */
                if (curQty > chk)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty. for ItemCode : " + cmbProdAdd.SelectedItem.Value + " is Greater than Stock Limit of : " + chk + "')", true);
                    ModalPopupProduct.Show();
                    return;
                }



                GrdViewItems.Columns[13].Visible = true;
                GrdViewItems.Columns[14].Visible = true;




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

                //if (txttotal.Text.Trim() != "")
                //    sTotalmrp = txttotal.Text;
                //else
                //    sTotalmrp = "0";

                //if (txtsubtot.Text.Trim() != "")
                //    sSubtot = txtsubtot.Text;
                //else
                //    sSubtot = "0";

                BillingMethod = bl.getConfigInfoMethod();
                double vatper;
                double vatinclusiverate = 0;

                if (Labelll.Text == "VAT INCLUSIVE")
                {

                    if (lblVATAdd.Text == "14.5")
                    {
                        vatper = 1.145;
                        vatinclusiverate = (((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - Convert.ToDouble(lblDisAdd.Text)) / vatper);
                        //sVatamount = ((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - vatinclusiverate);
                        sVatamount = (vatinclusiverate * 14.5) / 100;
                        vatamt = sVatamount.ToString("#0.00");
                    }
                    else if (lblVATAdd.Text == "5")
                    {
                        vatper = 1.05;
                        vatinclusiverate = (((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - Convert.ToDouble(lblDisAdd.Text)) / vatper);
                        //sVatamount = ((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - vatinclusiverate);
                        sVatamount = (vatinclusiverate * 5) / 100;
                        vatamt = sVatamount.ToString("#0.00");
                    }
                    else
                    {
                        vatper = Convert.ToDouble(lblVATAdd.Text);
                        vatper = ((vatper) + 100) / 100;
                        vatinclusiverate = (((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - Convert.ToDouble(lblDisAdd.Text)) / vatper);
                        //sVatamount = ((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - vatinclusiverate);
                        sVatamount = (vatinclusiverate * Convert.ToDouble(lblVATAdd.Text)) / 100;
                        vatamt = sVatamount.ToString("#0.00");
                    }
                }
                else
                {
                    sVatamount = (Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - ((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) * (Convert.ToDouble(sDiscount) / 100));
                    sVatamount = (sVatamount * (Convert.ToDouble(lblVATAdd.Text) / 100));
                    vatamt = sVatamount.ToString("#0.00");
                }

                //if (TextBox1.Text.Trim() != "")
                //    sVatamount = TextBox1.Text;
                //else
                //    sVatamount = "0";

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
                ds.Tables[0].Rows[curRow]["Billno"] = hdsales.Value;
                ds.Tables[0].Rows[curRow]["ProductName"] = cmbProdAdd.SelectedItem.Value;
                ds.Tables[0].Rows[curRow]["ProductDesc"] = cmbProdName.SelectedValue;
                ds.Tables[0].Rows[curRow]["Qty"] = txtQtyAdd.Text.Trim();

                if (Labelll.Text == "VAT INCLUSIVE")
                {
                    double incrate = vatinclusiverate / Convert.ToDouble(txtQtyAdd.Text);
                    ds.Tables[0].Rows[curRow]["Rate"] = incrate.ToString("#0.00");
                }
                else
                {
                    ds.Tables[0].Rows[curRow]["Rate"] = txtRateAdd.Text.Trim();
                }

                ds.Tables[0].Rows[curRow]["Discount"] = sDiscount;
                ds.Tables[0].Rows[curRow]["ExecCharge"] = execCharge;// krishnavelu 26 June
                ds.Tables[0].Rows[curRow]["VAT"] = sVat;
                ds.Tables[0].Rows[curRow]["CST"] = sCST;
                // ds.Tables[0].Rows[curRow]["Roles"] = strRole;
                ds.Tables[0].Rows[curRow]["IsRole"] = "N";
                ds.Tables[0].Rows[curRow]["Total"] = Convert.ToString(dTotal);
                ds.Tables[0].Rows[curRow]["Bundles"] = "0";
                ds.Tables[0].Rows[curRow]["Totalmrp"] = txtRateAdd.Text.Trim();
                ds.Tables[0].Rows[curRow]["Rods"] = "0";
                ds.Tables[0].Rows[curRow]["Vatamount"] = vatamt;
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
                UpdatePanelTotalSummary.Update();
            }

            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (chk.Checked == false)
        {
            txtCustomerName.Visible = true;
            cmbCustomer.Visible = false;

            txtCustomerId.Visible = true;
            drpMobile.Visible = false;
        }
        else
        {
            cmbCustomer.Visible = true;
            txtCustomerName.Visible = false;

            drpMobile.Visible = true;
            txtCustomerId.Visible = false;
        }
        //UpdatePanel21.Update();
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
        try
        {

            string connection = string.Empty;
            string recondate = string.Empty;
            double stock = 0;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string sDiscount = "";
            string sVat = "";
            //string sVatamount = "";
            string sTotalmrp = "";

            string sSubtot = "";
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

            string vatamt = string.Empty;
            double sVatamount = 0;

            bool dupFlag = false;
            DataSet ds;
            hdOpr.Value = "New";
            string itemCode = string.Empty;

            if (Page.IsValid)
            {
                ModalPopupSales.Show();

                if (Request.Cookies["Company"] != null)
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

                if (dupFlag)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);
                    return;
                }


                prodItem = cmbProdAdd.SelectedItem.Text.Split('-');
                double chk = bl.getStockInfo(cmbProdAdd.SelectedValue);
                double curQty = Convert.ToDouble(txtQtyAdd.Text);

                if (curQty > chk)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected qty is greater than stock.Current Stock : " + chk + "')", true);
                    ModalPopupProduct.Show();
                    return;
                }



                GrdViewItems.Columns[13].Visible = true;
                GrdViewItems.Columns[14].Visible = true;



                if (cmbCategory.SelectedItem.Text != "GIFT")
                {
                    if (txtRateAdd.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Rate as Zero is not permitted.')", true);
                        return;
                    }
                }
                else
                    if (txtRateAdd.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You Entered Product Rate as Zero.')", true);
                    }


                //if ((optionmethod.SelectedValue == "NormalSales") || (optionmethod.SelectedValue == "PurchaseReturn") || (optionmethod.SelectedValue == "ManualSales"))
                if ((drpnormalsales.SelectedItem.Text == "YES") || (drpPurchaseReturn.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                {
                    if (lblVATAdd.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Vat Cannot be Zero %.')", true);
                        ModalPopupProduct.Show();
                        return;
                    }
                }

                if ((drpnormalsales.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                {
                    double EXCLUSIVErate1 = 0;
                    double EXCrate1 = 0;
                    if (Labelll.Text == "VAT EXCLUSIVE")
                    {
                        EXCLUSIVErate1 = (Convert.ToDouble(txtRateAdd.Text)) + ((Convert.ToDouble(txtRateAdd.Text)) * (Convert.ToDouble(lblVATAdd.Text) / 100));
                    }
                    else
                    {
                        EXCLUSIVErate1 = (Convert.ToDouble(txtRateAdd.Text));
                    }
                    DataSet dst = bl.ListProductMRPPrices(cmbProdAdd.SelectedItem.Value.Trim());
                    if (dst != null)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drt in dst.Tables[0].Rows)
                            {
                                EXCrate1 = Convert.ToDouble(drt["rate"]);
                            }
                        }
                    }
                    if (EXCLUSIVErate1 > EXCrate1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be greater than " + EXCrate1 + "')", true);
                        ModalPopupProduct.Show();
                        return;
                    }
                }

                string usernam = Request.Cookies["LoggedUserName"].Value;
                if (bl.CheckIfUserCanDoDeviation(usernam))
                {

                }
                else
                {
                    if ((drpnormalsales.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                    {
                        double devvalue = 0;

                        DataSet dsd = bl.getRateInformation(cmbProdAdd.SelectedValue);
                        if (dsd.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drt in dsd.Tables[0].Rows)
                            {
                                devvalue = Convert.ToDouble(drt["deviation"]);
                            }
                        }

                        if (devvalue == 0)
                        {
                            DataSet dsrate = bl.getRateInfo(cmbProdAdd.SelectedValue);
                            double rate1 = 0;
                            double dp = 0;
                            double overall = 0;
                            double per = 0;
                            double rate2 = 0;

                            if (dsrate.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsrate.Tables[0].Rows)
                                {
                                    dp = Convert.ToDouble(dr["dealerrate"]);
                                    per = Convert.ToDouble(dr["deviation"]);
                                    rate2 = (dp * per) / 100;
                                    rate1 = Convert.ToDouble(txtRateAdd.Text);
                                    //rate2 = dp - overall;

                                    //rate2 = dp + overall;

                                    if (Labelll.Text == "VAT EXCLUSIVE")
                                    {
                                        rate1 = (Convert.ToDouble(txtRateAdd.Text)) + ((Convert.ToDouble(txtRateAdd.Text)) * (Convert.ToDouble(lblVATAdd.Text) / 100));
                                    }

                                    if (rate1 < rate2)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be less than dealer rate Rs. " + rate2 + " ')", true);
                                        ModalPopupProduct.Show();
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            DataSet dsrate = bl.getRateInformation(cmbProdAdd.SelectedValue);
                            double rate1 = 0;
                            double dp = 0;
                            double overall = 0;
                            double per = 0;
                            double rate2 = 0;

                            if (dsrate.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsrate.Tables[0].Rows)
                                {
                                    dp = Convert.ToDouble(dr["dealerrate"]);
                                    per = Convert.ToDouble(dr["deviation"]);
                                    rate2 = (dp * per) / 100;
                                    rate1 = Convert.ToDouble(txtRateAdd.Text);
                                    //rate2 = dp - overall;

                                    //rate2 = dp + overall;

                                    if (Labelll.Text == "VAT EXCLUSIVE")
                                    {
                                        rate1 = (Convert.ToDouble(txtRateAdd.Text)) + ((Convert.ToDouble(txtRateAdd.Text)) * (Convert.ToDouble(lblVATAdd.Text) / 100));
                                    }

                                    if (rate1 < rate2)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be less than dealer rate Rs. " + rate2 + " ')", true);
                                        ModalPopupProduct.Show();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }

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

                //if (txttotal.Text.Trim() != "")
                //    sTotalmrp = txttotal.Text;
                //else
                //    sTotalmrp = "0";

                //if (txtsubtot.Text.Trim() != "")
                //    sSubtot = txtsubtot.Text;
                //else
                //    sSubtot = "0";



                BillingMethod = bl.getConfigInfoMethod();
                double vatper;
                double vatinclusiverate = 0;

                if (lblVATAdd.Text.Trim() != "")
                    sVat = lblVATAdd.Text;
                else
                    sVat = "0";

                if (Labelll.Text == "VAT INCLUSIVE")
                {

                    if (lblVATAdd.Text == "14.5")
                    {
                        vatper = 1.145;
                        vatinclusiverate = (((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - Convert.ToDouble(lblDisAdd.Text)) / vatper);
                        //sVatamount = ((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - vatinclusiverate);
                        sVatamount = (vatinclusiverate * 14.5) / 100;
                        vatamt = sVatamount.ToString("#0.00");
                    }
                    else if (lblVATAdd.Text == "5")
                    {
                        vatper = 1.05;
                        vatinclusiverate = (((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - Convert.ToDouble(lblDisAdd.Text)) / vatper);
                        //sVatamount = ((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - vatinclusiverate);
                        sVatamount = (vatinclusiverate * 5) / 100;
                        vatamt = sVatamount.ToString("#0.00");
                    }
                    else
                    {
                        vatper = Convert.ToDouble(lblVATAdd.Text);
                        vatper = ((vatper) + 100) / 100;
                        vatinclusiverate = (((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - Convert.ToDouble(lblDisAdd.Text)) / vatper);
                        //sVatamount = ((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - vatinclusiverate);
                        sVatamount = (vatinclusiverate * Convert.ToDouble(lblVATAdd.Text)) / 100;
                        vatamt = sVatamount.ToString("#0.00");
                    }

                }
                else
                {
                    sVatamount = (Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) - ((Convert.ToDouble(txtRateAdd.Text) * (Convert.ToDouble(txtQtyAdd.Text))) * (Convert.ToDouble(sDiscount) / 100));
                    sVatamount = (sVatamount * (Convert.ToDouble(lblVATAdd.Text) / 100));
                    vatamt = sVatamount.ToString("#0.00");
                }




                if (lblCSTAdd.Text.Trim() != "")
                    sCST = lblCSTAdd.Text;
                else
                    sCST = "0";

                if (txtExecCharge.Text.Trim() != "")
                    execCharge = txtExecCharge.Text;
                else
                    execCharge = "0";

                if (Session["productDs"] == null)
                {
                    ds = new DataSet();

                    dt = new DataTable();

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

                    dc = new DataColumn("Totalmrp");
                    dt.Columns.Add(dc);

                    //dc = new DataColumn("SubTotal");
                    //dt.Columns.Add(dc);

                    dc = new DataColumn("VatAmount");
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
                    drNew["Billno"] = hdsales.Value;
                    drNew["ProductName"] = cmbProdAdd.SelectedItem.Text;
                    drNew["ProductDesc"] = cmbProdName.SelectedValue;
                    drNew["Qty"] = txtQtyAdd.Text.Trim();
                    drNew["Measure_Unit"] = "";

                    if (Labelll.Text == "VAT INCLUSIVE")
                    {
                        double incrate = vatinclusiverate / Convert.ToDouble(txtQtyAdd.Text);
                        drNew["Rate"] = incrate.ToString("#0.00");
                    }
                    else
                    {
                        drNew["Rate"] = txtRateAdd.Text.Trim();
                    }

                    drNew["Discount"] = sDiscount;
                    drNew["ExecCharge"] = execCharge;
                    drNew["VAT"] = sVat;
                    drNew["CST"] = sCST;
                    drNew["Roles"] = hdCurrRole.Value;
                    drNew["IsRole"] = "N";
                    drNew["Total"] = Convert.ToString(dTotal);
                    drNew["Bundles"] = "0";
                    drNew["Rods"] = "0";
                    drNew["Totalmrp"] = txtRateAdd.Text.Trim();
                    //drNew["SubTotal"] = sSubtot;
                    drNew["VatAmount"] = vatamt;
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
                    drNew["Billno"] = hdsales.Value;
                    drNew["ProductName"] = lblProdDescAdd.Text;
                    drNew["ProductDesc"] = lblProdDescAdd.Text;
                    drNew["Qty"] = txtQtyAdd.Text.Trim();
                    drNew["Measure_Unit"] = "";//lblUnitMrmnt.Text.Trim();

                    if (Labelll.Text == "VAT INCLUSIVE")
                    {
                        double incrate = vatinclusiverate / Convert.ToDouble(txtQtyAdd.Text);
                        drNew["Rate"] = incrate.ToString("#0.00");
                    }
                    else
                    {
                        drNew["Rate"] = txtRateAdd.Text.Trim();
                    }

                    drNew["Discount"] = sDiscount;
                    drNew["ExecCharge"] = execCharge;
                    drNew["VAT"] = sVat;
                    drNew["CST"] = sCST;
                    drNew["Roles"] = hdCurrRole.Value;
                    drNew["IsRole"] = "N";
                    drNew["Total"] = Convert.ToString(dTotal);
                    drNew["Bundles"] = "0";
                    drNew["Rods"] = "0";
                    drNew["Totalmrp"] = txtRateAdd.Text.Trim();
                    //drNew["SubTotal"] = sSubtot;
                    drNew["VatAmount"] = vatamt;
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
                UpdatePanelTotalSummary.Update();
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
            string checkdate = txtBillDate.Text.Trim();

            //if (checkdate == "01/12/2013")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot make Sales bill for this Date')", true);
            //    return;
            //}

            //DateTime checkdate2 = Convert.ToDateTime(txtBillDate.Text.Trim());
            //if (checkdate2 >= Convert.ToDateTime("01/12/2014"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot make Sales bill for this Date')", true);
            //    return;
            //}


            string connection = Request.Cookies["Company"].Value;
            ModalPopupSales.Show();


            //////if (!Helper.IsLicenced(connection))
            //////{
            //////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
            //////    return;
            //////}

            //for new row add testing
            //if (Session["productDs"] == null)
            //{
            //    cmdSaveProduct.Enabled = true;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before save')", true);
            //    return;
            //}

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

            //Senthil
            string sCustomerAddress2 = string.Empty;
            string sCustomerAddress3 = string.Empty;
            string executivename = string.Empty;

            string sCustomerContact = string.Empty;
            string sOtherCusName = string.Empty;// krishnavelu 26 June
            int sCustomerID = 0;
            double dTotalAmt = 0;
            string sCustomerName = string.Empty;
            string deliveryNote = string.Empty;
            int iPaymode = 0;
            string sCreditCardno = string.Empty;
            string snarr = string.Empty;
            string Types = string.Empty;

            string despatchedfrom = string.Empty;
            double fixedtotal = 0.0;
            int manualno = 0;
            string cuscategory = string.Empty;

            double dfixedtotal = 0.0;

            double dFreight = 0;
            double dLU = 0;
            int iBank = 0;
            int iSales = 0;
            DataSet ds;
            string Series = "";
            DataSet receiptData = null;
            DataSet billData = null;


            if (Page.IsValid)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                recondate = txtBillDate.Text.Trim(); ;

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }

                if (Convert.ToDouble(txtfixedtotal.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter FixedTotal')", true);
                    return;
                }

                if (chk.Checked == false)
                {
                    if (txtCustomerName.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Customer Name')", true);
                        return;
                    }
                }
                else
                {
                    if (cmbCustomer.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Customer')", true);
                        return;
                    }
                }

                purchaseReturn = drpPurchaseReturn.SelectedValue;
                prReason = txtPRReason.Text.Trim();
                iSales = Convert.ToInt32(hdsales.Value);
                sBilldate = txtBillDate.Text.Trim();
                iPaymode = Convert.ToInt32(drpPaymode.SelectedItem.Value);
                dTotalAmt = 4; //Convert.ToDouble(hdTotalAmt.Value.Trim());  // for test
                sCustomerAddress = txtAddress.Text.Trim();
                sCustomerAddress2 = txtAddress2.Text.Trim();//Senthil
                sCustomerAddress3 = txtAddress3.Text.Trim();//Senthil
                //sCustomerContact = hdContact.Value.Trim();
                sCustomerContact = txtCustPh.Text;

                dTotalAmt = Convert.ToDouble(lblNet.Text);
                //executive = drpIncharge.SelectedValue;

                cuscategory = drpCustomerCategoryAdd.SelectedValue;

                Types = Labelll.Text;
                string NormalSales = string.Empty;
                string ManualSales = string.Empty;

                string Paymode = drpPaymode.SelectedItem.Text;

                despatchedfrom = txtdespatced.Text;
                snarr = txtnarr.Text;
                fixedtotal = Convert.ToDouble(txtfixedtotal.Text);

                manualno = Convert.ToInt32(txtmanual.Text);

                //if (chkboxManual.Checked == true)
                //{
                //    manual = "YES";
                //}
                //else
                //{
                //    manual = "NO";
                //}

                //Senthil
                //executivename = drpIncharge.SelectedItem.Text;

                intTrans = drpIntTrans.SelectedValue;
                deliveryNote = ddDeliveryNote.SelectedValue;
                sOtherCusName = txtOtherCusName.Text;// krishnavelu 26 June
                smsTEXT = "This is the Electronic Receipt for your purchase. The Details are : ";
                Series = ddSeriesType.SelectedValue;
                int cnt = 0;

                if (intTrans == "YES")
                    cnt = cnt + 1;
                if (deliveryNote == "YES")
                    cnt = cnt + 1;
                if (purchaseReturn == "YES")
                    cnt = cnt + 1;

                if (cnt > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Either one of Purchase Return or Delivery Note or Internal Transfer should be selected')", true);
                    tabs2.ActiveTabIndex = 1;
                    //updatePnlSales.Update();
                    return;
                }

                //Paymode as Bank

                if (iPaymode == 4)
                {
                    var recAmount = double.Parse(lblReceivedTotal.Text);
                    var salesAmount = double.Parse(lblNet.Text);

                    if ((ddBank1.SelectedValue == "0" && txtAmount1.Text == "") &&
                        (ddBank2.SelectedValue == "0" && txtAmount2.Text == "") &&
                        (ddBank3.SelectedValue == "0" && txtAmount3.Text == "") &&
                        txtCashAmount.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter the Customer Payment details and try again.')", true);
                        tabs2.ActiveTabIndex = 1;
                        //updatePnlSales.Update();
                        return;
                    }

                    if (recAmount > salesAmount)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Customer Payment should not be Greater than Total Sales Amount.')", true);
                        tabs2.ActiveTabIndex = 1;
                        //updatePnlSales.Update();
                        return;
                    }

                    receiptData = GenerateReceiptData();

                    if (ddBank1.SelectedValue != "0" && txtCCard1.Text != "" && txtAmount1.Text != "0")
                    {
                        DataRow dr = receiptData.Tables[0].NewRow();
                        dr["RefNo"] = "";
                        dr["TransDate"] = recondate;
                        dr["DebitorID"] = ddBank1.SelectedValue;
                        dr["CreditorID"] = cmbCustomer.SelectedValue;
                        dr["Amount"] = txtAmount1.Text;
                        dr["Narration"] = "";
                        dr["VoucherType"] = "Receipt";
                        dr["ChequeNo"] = txtCCard1.Text;
                        dr["SFRefNo"] = txtRefNo1.Text;
                        dr["Paymode"] = "Cheque";

                        receiptData.Tables[0].Rows.Add(dr);
                    }

                    if (ddBank2.SelectedValue != "0" && txtCCard2.Text != "" && txtAmount2.Text != "0")
                    {
                        DataRow dr = receiptData.Tables[0].NewRow();
                        dr["RefNo"] = "";
                        dr["TransDate"] = recondate;
                        dr["DebitorID"] = ddBank2.SelectedValue;
                        dr["CreditorID"] = cmbCustomer.SelectedValue;
                        dr["Amount"] = txtAmount2.Text;
                        dr["Narration"] = "";
                        dr["VoucherType"] = "Receipt";
                        dr["ChequeNo"] = txtCCard2.Text;
                        dr["Paymode"] = "Cheque";
                        dr["SFRefNo"] = txtRefNo2.Text;

                        receiptData.Tables[0].Rows.Add(dr);
                    }

                    if (ddBank3.SelectedValue != "0" && txtCCard3.Text != "" && txtAmount1.Text != "0")
                    {
                        DataRow dr = receiptData.Tables[0].NewRow();
                        dr["RefNo"] = "";
                        dr["TransDate"] = recondate;
                        dr["DebitorID"] = ddBank3.SelectedValue;
                        dr["CreditorID"] = cmbCustomer.SelectedValue;
                        dr["Amount"] = txtAmount3.Text;
                        dr["Narration"] = "";
                        dr["VoucherType"] = "Receipt";
                        dr["ChequeNo"] = txtCCard3.Text;
                        dr["Paymode"] = "Cheque";
                        dr["SFRefNo"] = txtRefNo3.Text;

                        receiptData.Tables[0].Rows.Add(dr);
                    }

                    if (!string.IsNullOrEmpty(txtCashAmount.Text) && txtCashAmount.Text != "0")
                    {
                        DataRow dr = receiptData.Tables[0].NewRow();
                        dr["RefNo"] = "";
                        dr["TransDate"] = recondate;
                        dr["DebitorID"] = "1";
                        dr["CreditorID"] = cmbCustomer.SelectedValue;
                        dr["Amount"] = txtCashAmount.Text;
                        dr["Narration"] = "";
                        dr["VoucherType"] = "Receipt";
                        dr["ChequeNo"] = "";
                        dr["Paymode"] = "Cash";
                        dr["SFRefNo"] = TextBox5.Text;

                        receiptData.Tables[0].Rows.Add(dr);
                    }

                    receiptData.Tables[0].AcceptChanges();

                    iPaymode = 3;
                    MultiPayment = "YES";
                }
                else
                {
                    MultiPayment = "NO";
                }

                if (iPaymode == 2)
                {
                    sCreditCardno = Convert.ToString(txtCreditCardNo.Text);
                    iBank = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    rvbank.Enabled = true;
                    rvCredit.Enabled = true;
                }
                else
                {
                    //Paymode as Cash
                    rvbank.Enabled = false;
                    rvCredit.Enabled = false;
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
                /*March18*/
                //dTotalAmt = dTotalAmt + dFreight + dLU;


                if ((intTrans == "NO") && (deliveryNote == "NO") && (purchaseReturn == "NO"))
                {
                    //if (optionmethod.SelectedValue == "ManualSales")
                    //{
                    if (drpmanualsales.SelectedValue == "YES") 
                    {
                        ManualSales = "YES";
                        NormalSales = "NO";
                    }
                    else if (drpnormalsales.SelectedValue == "YES")
                    {
                        NormalSales = "YES";
                        ManualSales = "NO";
                    }
                }
                else
                {
                    NormalSales = "NO";
                    ManualSales = "NO";
                }

                dfixedtotal = Convert.ToDouble(txtfixedtotal.Text);

                BusinessLogic blg = new BusinessLogic(sDataSource);

                double checktotal = 0;
                double difftotal = 0;

                difftotal = dfixedtotal - dTotalAmt;
                checktotal = (difftotal / dfixedtotal) * 100;

                double Percent = blg.getconfigpercent();
                double Amt = blg.getconfigamt();

                double ddd = Math.Min(difftotal, checktotal);

                double TotalAmt = (dfixedtotal - dTotalAmt);
                string Total = TotalAmt.ToString("#0.00");

                //for test
                //if (Convert.ToDouble(Total) > 1)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Difference Between FixedTotal And GrandTotal Cannot be more than 1')", true);
                //    return;
                //}
                //else if (Convert.ToDouble(Total) < -1)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Difference Between FixedTotal And GrandTotal Cannot be less than 1')", true);
                //    return;
                //}

                //if (difftotal < 0)
                //{
                //    if (difftotal < -10)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Difference Between FixedTotal And GrandTotal Cannot be more than " + Amt + "')", true);
                //        return;
                //    }
                //}

                //if (ddd == difftotal)
                //{
                //    if (difftotal > Amt)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Difference Between FixedTotal And GrandTotal Cannot be more than " + Amt + "')", true);
                //        return;
                //    }
                //}
                //else if (ddd == checktotal)
                //{
                //    if (checktotal > Percent)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Difference Between FixedTotal And GrandTotal Cannot be more than" + Percent + "% ')", true);
                //        return;
                //    }
                //}

                string check = "Y";
                string DuplicateCopy = "N";
                string CName = txtCustomerName.Text;

                int CustomerIdMobile = 0;
                if (chk.Checked == false)
                {
                    if (txtCustomerId.Text != "")
                    {
                        CustomerIdMobile = Convert.ToInt32(txtCustomerId.Text);
                    }
                }
                else
                {
                    CustomerIdMobile = 0;
                }

                string discType = GetDiscType();


                string usernam = Request.Cookies["LoggedUserName"].Value;

                //ds = (DataSet)GrdViewItems.DataSource;
                //if (Session["productDs"] != null)/////////// Previous code
                if (Session["productDs"] == null) // New code
                {
                    ds = (DataSet)Session["productDs"];

                    if (ds == null) //(ds != null)//Previous code
                    {
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        if (chk.Checked == false)
                        {
                            if (bl.IsLedgerAlreadyFound(connection, CName))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Customer " + CName + " with this name already exists.');", true);
                                return;
                            }

                            sCustomerID = bl.InsertCustomerInfoDirect(connection, CName, CName, 1, 0, 0, 0, "", CName, sCustomerAddress, sCustomerAddress2, sCustomerAddress3, "", "Customer", 0, "", sCustomerContact, 0, 0, "NO", "NO", "NO", CName, usernam, "YES", "", 3);
                            sCustomerName = txtCustomerName.Text;
                        }
                        else
                        {
                            sCustomerName = cmbCustomer.SelectedItem.Text;
                            sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                        }

                        //&&&&&& Sales Item Table Insert Dataset &&&&&&&&&&&&&&&&&                          


                        for (int vLoop = 0; vLoop < grvStudentDetails.Rows.Count; vLoop++)
                        {
                            DropDownList drpProduct = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpPrd");
                            DropDownList drpIncharge = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpIncharge");
                            TextBox txtDesc = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDesc");
                            TextBox txtRate = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRate");
                            TextBox txtTotalPrice = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTot");
                            TextBox txtStock = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtStock");
                            TextBox txtQty = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtQty");
                            TextBox txtExeComm = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtExeComm");
                            TextBox txtDisPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDisPre");
                            TextBox txtVATPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATPre");
                            TextBox txtCSTPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtCSTPre");
                            TextBox txtPrBefVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtPrBefVAT");
                            TextBox txtVATAmt = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATAmt");
                            TextBox txtRtVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRtVAT");
                            TextBox txtTotal = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTotal");

                            int col = vLoop + 1;

                            if (drpProduct.SelectedValue == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Product in row " + col + " ')", true);
                                return;
                            }
                            else if (drpIncharge.SelectedValue == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Employee in row " + col + " ')", true);
                                return;
                            }
                            else if (txtDesc.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Description in row " + col + " ')", true);
                                return;
                            }
                            else if (txtRate.Text == "" || txtRate.Text == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Rate in row " + col + " ')", true);
                                return;
                            }
                            else if (txtTotalPrice.Text == "" || txtTotalPrice.Text == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Price is empty in row " + col + " ')", true);
                                return;
                            }
                            else if (txtStock.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock is empty in row " + col + " ')", true);
                                return;
                            }
                            else if (txtQty.Text == "" || txtQty.Text == "0")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Quantity in row " + col + " ')", true);
                                return;
                            }
                            else if (Convert.ToInt32(txtQty.Text) > Convert.ToInt32(txtStock.Text))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given qty is greater than stock in row " + col + " ')", true);
                                return;
                            }
                            else if (txtExeComm.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Executive Commission in row " + col + " ')", true);
                                return;
                            }
                            else if (txtDisPre.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Discount Percentage in row " + col + " ')", true);
                                return;
                            }
                            else if (txtVATPre.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill VAT Percentage in row " + col + " ')", true);
                                return;
                            }
                            else if (txtCSTPre.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill CST Percentage in row " + col + " ')", true);
                                return;
                            }
                            else if (txtPrBefVAT.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Price Before VAT Amount in row " + col + " ')", true);
                                return;
                            }
                            else if (txtVATAmt.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill VAT Amount in row " + col + " ')", true);
                                return;
                            }
                            else if (txtRtVAT.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Row Total in row " + col + " ')", true);
                                return;
                            }
                            else if (txtTotal.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Total in row " + col + " ')", true);
                                return;
                            }

                            if ((drpnormalsales.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                            {
                                double EXCLUSIVErate1 = 0;
                                double EXCrate1 = 0;
                                double ratewithqty = 0;
                                if (Labelll.Text == "VAT EXCLUSIVE")
                                {
                                    ratewithqty = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);
                                    //EXCLUSIVErate1 = (Convert.ToDouble(txtRate.Text)) + ((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtVATAmt.Text) / 100));
                                    EXCLUSIVErate1 = (Convert.ToDouble(ratewithqty)) + ((Convert.ToDouble(ratewithqty)) * (Convert.ToDouble(txtVATPre.Text) / 100));
                                }
                                else
                                {
                                    // EXCLUSIVErate1 = Convert.ToDouble(txtRate.Text);
                                    ratewithqty = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);
                                    EXCLUSIVErate1 = Convert.ToDouble(ratewithqty);
                                }

                                DataSet dst = bl.ListProductMRPPrices(drpProduct.SelectedItem.Value.Trim());
                                if (dst != null)
                                {
                                    if (dst.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow drt in dst.Tables[0].Rows)
                                        {
                                            EXCrate1 = Convert.ToDouble(drt["rate"]);
                                        }
                                    }
                                }
                                if (EXCLUSIVErate1 > EXCrate1)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be greater than " + EXCrate1 + " in row " + col + "')", true);
                                    checkflag = true;
                                    return;
                                }
                            }

                            //string usernam = Request.Cookies["LoggedUserName"].Value;
                            if (bl.CheckIfUserCanDoDeviation(usernam))
                            {

                            }
                            else
                            {
                                if ((drpnormalsales.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                                {
                                    double devvalue = 0;

                                    DataSet dsd = bl.getRateInformation(drpProduct.SelectedValue);
                                    if (dsd.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow drt in dsd.Tables[0].Rows)
                                        {
                                            devvalue = Convert.ToDouble(drt["deviation"]);
                                        }
                                    }

                                    if (devvalue == 0)
                                    {
                                        DataSet dsrate = bl.getRateInfo(drpProduct.SelectedValue);
                                        double rate1 = 0;
                                        double dp = 0;
                                        double overall = 0;
                                        double per = 0;
                                        double rate2 = 0;

                                        if (dsrate.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dsrate.Tables[0].Rows)
                                            {
                                                dp = Convert.ToDouble(dr["dealerrate"]);
                                                per = Convert.ToDouble(dr["deviation"]);
                                                rate2 = (dp * per) / 100;
                                                // rate1 = Convert.ToDouble(txtRate.Text);

                                                rate1 = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);

                                                if (Labelll.Text == "VAT EXCLUSIVE")
                                                {
                                                    // rate1 = (Convert.ToDouble(txtRate.Text)) + ((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtVATAmt.Text) / 100));
                                                    rate1 = (Convert.ToDouble(rate1)) + ((Convert.ToDouble(rate1)) * (Convert.ToDouble(txtVATPre.Text) / 100));
                                                }

                                                if (rate1 < rate2)
                                                {
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be less than dealer rate Rs. " + rate2 + " ')", true);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        DataSet dsrate = bl.getRateInformation(drpProduct.SelectedValue);
                                        double rate1 = 0;
                                        double dp = 0;
                                        double overall = 0;
                                        double per = 0;
                                        double rate2 = 0;

                                        if (dsrate.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dsrate.Tables[0].Rows)
                                            {
                                                dp = Convert.ToDouble(dr["dealerrate"]);
                                                per = Convert.ToDouble(dr["deviation"]);
                                                rate2 = (dp * per) / 100;
                                                // rate1 = Convert.ToDouble(txtRate.Text);

                                                rate1 = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);

                                                if (Labelll.Text == "VAT EXCLUSIVE")
                                                {
                                                    // rate1 = (Convert.ToDouble(txtRate.Text)) + ((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtVATAmt.Text) / 100));
                                                    rate1 = (Convert.ToDouble(rate1)) + ((Convert.ToDouble(rate1)) * (Convert.ToDouble(txtVATPre.Text) / 100));
                                                }

                                                if (rate1 < rate2)
                                                {
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be less than dealer rate Rs. " + rate2 + " ')", true);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        DataSet dss;
                        DataTable dt;
                        DataRow drNew;

                        DataColumn dc;

                        dss = new DataSet();

                        dt = new DataTable();

                        dc = new DataColumn("Prd");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Emp");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Desc");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Rate");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("TotPrice");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Stock");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Qty");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ExeComm");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("DisPre");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("VATPre");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("CSTPre");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("PrBefVATAmt");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("VATAmt");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("RtVAT");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Tot");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Prdname");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("MeasureUnit");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Roles");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("IsRole");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Bundles");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Rods");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("TotalMrp");
                        dt.Columns.Add(dc);

                        dss.Tables.Add(dt);

                        for (int vLoop = 0; vLoop < grvStudentDetails.Rows.Count; vLoop++)
                        {
                            DropDownList drpProduct = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpPrd");
                            DropDownList drpIncharge = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpIncharge");
                            TextBox txtDesc = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDesc");
                            TextBox txtRate = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRate");
                            TextBox txtTotalPrice = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTot");
                            TextBox txtStock = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtStock");
                            TextBox txtQty = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtQty");
                            TextBox txtExeComm = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtExeComm");
                            TextBox txtDisPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDisPre");
                            TextBox txtVATPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATPre");
                            TextBox txtCSTPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtCSTPre");
                            TextBox txtPrBefVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtPrBefVAT");
                            TextBox txtVATAmt = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATAmt");
                            TextBox txtRtVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRtVAT");
                            TextBox txtTotal = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTotal");


                            drNew = dt.NewRow();
                            drNew["Prd"] = Convert.ToString(drpProduct.SelectedItem.Value);
                            drNew["Emp"] = Convert.ToString(drpIncharge.SelectedItem.Value);
                            drNew["Desc"] = txtDesc.Text;
                            drNew["Rate"] = txtRate.Text;
                            drNew["TotPrice"] = txtTotalPrice.Text;
                            drNew["Stock"] = txtStock.Text;
                            drNew["Qty"] = txtQty.Text;
                            drNew["ExeComm"] = txtExeComm.Text;
                            drNew["DisPre"] = txtDisPre.Text;
                            drNew["VATPre"] = txtVATPre.Text;
                            drNew["CSTPre"] = txtCSTPre.Text;
                            drNew["PrBefVATAmt"] = txtPrBefVAT.Text;
                            drNew["VATAmt"] = txtVATAmt.Text;
                            drNew["RtVAT"] = txtRtVAT.Text;
                            drNew["Tot"] = txtTotal.Text;

                            drNew["Prdname"] = drpProduct.Text;
                            drNew["MeasureUnit"] = " ";
                            drNew["Roles"] = "1";
                            drNew["IsRole"] = "N";
                            drNew["Bundles"] = "0";
                            drNew["Rods"] = "0";
                            drNew["TotalMrp"] = txtTotal.Text;
                            dss.Tables[0].Rows.Add(drNew);
                        }
                        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                        int billNo = bl.InsertSalesNewSeries(Series, sBilldate, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, iPaymode, sCreditCardno, iBank, dTotalAmt, purchaseReturn, prReason, dFreight, dLU, dss, sOtherCusName, intTrans, receiptData, MultiPayment, deliveryNote, sCustomerAddress2, sCustomerAddress3, executivename, despatchedfrom, fixedtotal, manualno, dTotalAmt, usernam, ManualSales, NormalSales, Types, snarr, DuplicateCopy, check, CustomerIdMobile, cuscategory, discType);



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

                            string salestype = string.Empty;
                            int ScreenNo = 0;
                            string ScreenName = string.Empty;

                            if (purchaseReturn == "YES")
                            {
                                salestype = "Purchase Return";
                                ScreenName = "Purchase Return";
                            }
                            else if (intTrans == "YES")
                            {
                                salestype = "Internal Transfer";
                                ScreenName = "Internal Transfer Sales";
                            }
                            else if (deliveryNote == "YES")
                            {
                                salestype = "Delivery Note";
                                ScreenName = "Delivery Note Sales";
                            }
                            else if (ManualSales == "YES")
                            {
                                salestype = "Manual Sales";
                                ScreenName = "Manual Sales";
                            }
                            else if (NormalSales == "YES")
                            {
                                salestype = "Normal Sales";
                                ScreenName = "Normal Sales";
                            }

                            bool mobile = false;
                            bool Email = false;
                            string emailsubject = string.Empty;

                            string emailcontent = string.Empty;
                            if (hdEmailRequired.Value == "YES")
                            {
                                DataSet dsd = bl.GetLedgerInfoForId(connection, sCustomerID);
                                var toAddress = "";
                                var toAdd = "";
                                Int32 ModeofContact = 0;
                                int ScreenType = 0;

                                if (dsd != null)
                                {
                                    if (dsd.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in dsd.Tables[0].Rows)
                                        {
                                            toAdd = dr["EmailId"].ToString();
                                            ModeofContact = Convert.ToInt32(dr["ModeofContact"]);
                                        }
                                    }
                                }


                                DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                                if (dsdd != null)
                                {
                                    if (dsdd.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in dsdd.Tables[0].Rows)
                                        {
                                            ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                            mobile = Convert.ToBoolean(dr["mobile"]);
                                            Email = Convert.ToBoolean(dr["Email"]);
                                            emailsubject = Convert.ToString(dr["emailsubject"]);
                                            emailcontent = Convert.ToString(dr["emailcontent"]);

                                            if (ScreenType == 1)
                                            {
                                                if (dr["Name1"].ToString() == "Sales Executive")
                                                {
                                                    toAddress = toAdd;
                                                }
                                                else if (dr["Name1"].ToString() == "Customer")
                                                {
                                                    if (ModeofContact == 2)
                                                    {
                                                        toAddress = toAdd;
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    toAddress = toAdd;
                                                }
                                            }
                                            else
                                            {
                                                toAddress = dr["EmailId"].ToString();
                                            }
                                            if (Email == true)
                                            {
                                                string body = "\n";
                                                
                                                int index123 = emailcontent.IndexOf("@Branch");
                                                body = Request.Cookies["Company"].Value;
                                                if (index123 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                                }

                                                int sno = 1;
                                                string prd = string.Empty;
                                                int index322 = emailcontent.IndexOf("@Product");
                                                if (dss != null)
                                                {
                                                    if (dss.Tables[0].Rows.Count > 0)
                                                    {
                                                        //emailcontent = emailcontent.Remove(index322, 8).Insert(index322, body);

                                                        foreach (DataRow drd in dss.Tables[0].Rows)
                                                        {
                                                            
                                                            //body = drd["PrdName"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["Rate"].ToString();
                                                            prd = prd + "\n";
                                                            prd = prd + drd["PrdName"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["Rate"].ToString();
                                                            prd = prd + "\n";
                                                            
                                                        }
                                                        if (index322 >= 0)
                                                        {
                                                            emailcontent = emailcontent.Remove(index322, 8).Insert(index322, prd);
                                                        }
                                                    }
                                                }

                                                int index312 = emailcontent.IndexOf("@User");
                                                body = usernam;
                                                if (index312 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                                }

                                                int index2 = emailcontent.IndexOf("@Date");
                                                body = txtBillDate.Text;
                                                if (index2 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                                }

                                                int index = emailcontent.IndexOf("@Customer");
                                                body = sCustomerName;
                                                if (index >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index, 9).Insert(index, body);
                                                }

                                                int index1 = emailcontent.IndexOf("@Amount");
                                                body = txttotal.Text;
                                                if (index1 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index1, 7).Insert(index1, body);
                                                }

                                                string smtphostname = ConfigurationManager.AppSettings["SmtpHostName"].ToString();
                                                int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPortNumber"]);
                                                var fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

                                                string fromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();

                                                EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, emailcontent, fromPassword);

                                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email sent successfully')", true);

                                            }

                                        }
                                    }
                                }
                            }

                            string smscontent = string.Empty;
                            if (hdSMSRequired.Value == "YES")
                            {
                                DataSet dsd = bl.GetLedgerInfoForId(connection, sCustomerID);
                                var toAddress = "";
                                var toAdd = "";
                                Int32 ModeofContact = 0;
                                int ScreenType = 0;

                                if (dsd != null)
                                {
                                    if (dsd.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in dsd.Tables[0].Rows)
                                        {
                                            toAdd = dr["Mobile"].ToString();
                                            ModeofContact = Convert.ToInt32(dr["ModeofContact"]);
                                        }
                                    }
                                }


                                DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                                if (dsdd != null)
                                {
                                    if (dsdd.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in dsdd.Tables[0].Rows)
                                        {
                                            ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                            mobile = Convert.ToBoolean(dr["mobile"]);
                                            smscontent = Convert.ToString(dr["smscontent"]);

                                            if (ScreenType == 1)
                                            {
                                                if (dr["Name1"].ToString() == "Sales Executive")
                                                {
                                                    toAddress = toAdd;
                                                }
                                                else if (dr["Name1"].ToString() == "Customer")
                                                {
                                                    if (ModeofContact == 1)
                                                    {
                                                        toAddress = toAdd;
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    toAddress = toAdd;
                                                }
                                            }
                                            else
                                            {
                                                toAddress = dr["mobile"].ToString();
                                            }
                                            if (mobile == true)
                                            {
                                                
                                                string body = "\n";

                                                int index123 = smscontent.IndexOf("@Branch");
                                                body = Request.Cookies["Company"].Value;
                                                if (index123 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                                }




                                                int index312 = smscontent.IndexOf("@User");
                                                body = usernam;
                                                if (index312 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                                }

                                                int index2 = smscontent.IndexOf("@Date");
                                                body = txtBillDate.Text;
                                                if (index2 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                                }

                                                int index = smscontent.IndexOf("@Customer");
                                                body = sCustomerName;
                                                if (index >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index, 9).Insert(index, body);
                                                }

                                                int index1 = smscontent.IndexOf("@Amount");
                                                body = txttotal.Text;
                                                if (index1 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index1, 7).Insert(index1, body);
                                                }

                                                int i = dss.Tables[0].Rows.Count;

                                                int index322 = smscontent.IndexOf("@Product");

                                                foreach (DataRow drd in dss.Tables[0].Rows)
                                                {
                                                    smsTEXT = smsTEXT + drd["PrdName"].ToString() + " " + drd["Qty"].ToString() + " Qty @ " + GetCurrencyType() + "." + GetProductTotalExVAT(double.Parse(drd["Qty"].ToString()), double.Parse(drd["Rate"].ToString()), double.Parse(drd["Discount"].ToString()));
                                                    i = i - 1;

                                                    if (i != 0)
                                                        smsTEXT = smsTEXT + ", ";
                                                }

                                                smsTEXT = smsTEXT + ". Total Bill Amount is " + GetCurrencyType() + "." + lblNet.Text;
                                                smsTEXT = smsTEXT + " . The Bill No. is " + billNo.ToString();
                                                if (index322 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index322, 8).Insert(index322, smsTEXT);
                                                }

                                                if (Session["Provider"] != null)
                                                {
                                                    utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), toAddress, smscontent, true, UserID);
                                                }
                                                

                                            }

                                        }
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
                            Response.Redirect("PrintProductSalesBill.aspx?SID=" + billNo.ToString() + "&RT=" + purchaseReturn);


                        }
                        //} // For testing
                        //else
                        //{
                        //    cmdSaveProduct.Enabled = true;
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before save')", true);
                        //    ModalPopupSales.Show();
                        //    updatePnlSales.Update();
                        //    return;
                        //}
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

            ModalPopupMethod.Hide();
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
        try
        {
            string checkdate = txtBillDate.Text.Trim(); ;

            //if (checkdate == "01/12/2013")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot make Sales bill for this Date')", true);
            //    return;
            //}

            //DateTime checkdate2 = Convert.ToDateTime(txtBillDate.Text.Trim());
            //if (checkdate2 >= Convert.ToDateTime("01/12/2014"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot make Sales bill for this Date')", true);
            //    return;
            //}

            string connection = Request.Cookies["Company"].Value;
            string recondate = string.Empty;
            string purchaseReturn = string.Empty;
            string intTrans = string.Empty;
            string deliveryNote = string.Empty;
            string prReason = string.Empty;
            string executive = string.Empty;
            string sBilldate = string.Empty;
            string sCustomerAddress = string.Empty;

            //Senthil
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
            double dFreight = 0;
            double dLU = 0;
            int iBank = 0;
            int iSales = 0;
            string userID = string.Empty;
            DataSet ds;
            string Types = string.Empty;
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


            if (Page.IsValid)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                recondate = txtBillDate.Text.Trim(); ;

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }

                if (Convert.ToDouble(txtfixedtotal.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter FixedTotal')", true);
                    return;
                }

                if (chk.Checked == false)
                {
                    if (txtCustomerName.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Customer Name')", true);
                        return;
                    }
                }
                else
                {
                    if (cmbCustomer.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Customer')", true);
                        return;
                    }
                }



                ////////////////////////////////////////////////////
                if (Session["productDs"] != null)
                {
                    DataSet dsddd = (DataSet)Session["productDs"];

                    if (dsddd != null)
                    {
                        if (dsddd.Tables[0].Rows.Count > 0)
                        {
                        }
                        else
                        {
                            cmdSaveProduct.Enabled = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before update')", true);
                            return;
                        }
                    }
                    else
                    {
                        cmdSaveProduct.Enabled = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before update')", true);
                        return;
                    }
                }
                else
                {
                    cmdSaveProduct.Enabled = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before update')", true);
                    return;
                }
                //////////////////////////////////////////////////////


                string cuscategory = string.Empty;


                string snarr = string.Empty;
                string smsTEXT = "This is the Electronic Receipt for your purchase. The Details are : ";
                dFreight = Convert.ToDouble(txtFreight.Text);
                dLU = Convert.ToDouble(txtLU.Text);
                purchaseReturn = drpPurchaseReturn.SelectedValue;
                intTrans = drpIntTrans.SelectedValue;
                deliveryNote = ddDeliveryNote.SelectedValue;
                prReason = txtPRReason.Text;
                //executive = drpIncharge.SelectedValue;
                iSales = Convert.ToInt32(hdsales.Value);
                sBilldate = txtBillDate.Text.Trim();
                iPaymode = Convert.ToInt32(drpPaymode.SelectedItem.Value);
                dTotalAmt = Convert.ToDouble(hdTotalAmt.Value.Trim());
                sCustomerAddress = txtAddress.Text.Trim();
                snarr = txtnarr.Text;
                Types = Labelll.Text;

                string Paymode = drpPaymode.SelectedItem.Text;

                sCustomerAddress2 = txtAddress2.Text.Trim();// Senthil
                sCustomerAddress3 = txtAddress3.Text.Trim();// Senthil
                //executivename = drpIncharge.SelectedItem.Text;

                despatchedfrom = txtdespatced.Text;
                fixedtotal = Convert.ToDouble(txtfixedtotal.Text);

                manualno = Convert.ToInt32(txtmanual.Text);
                //if (chkboxManual.Checked == true)
                //{
                //    manual = "YES";
                //}
                //else
                //{
                //    manual = "NO";
                //}

                //sCustomerContact = hdContact.Value.Trim();
                sCustomerContact = txtCustPh.Text;
                sCustomerName = cmbCustomer.SelectedItem.Text;
                sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                dTotalAmt = Convert.ToDouble(lblNet.Text);
                //dTotalAmt = dTotalAmt + dFreight + dLU;
                sOtherCusName = txtOtherCusName.Text;// krishnavelu 26 June
                userID = Page.User.Identity.Name;
                int cnt = 0;

                cuscategory = drpCustomerCategoryAdd.SelectedValue;

                if (intTrans == "YES")
                    cnt = cnt + 1;
                if (deliveryNote == "YES")
                    cnt = cnt + 1;
                if (purchaseReturn == "YES")
                    cnt = cnt + 1;

                if (cnt > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Either one of Purchase Return or Delivery Note or Internal Transfer should be selected')", true);
                    tabs2.ActiveTabIndex = 1;
                    //updatePnlSales.Update();
                    return;
                }

                //if (iPaymode == 4)
                //{
                //    iPaymode = 3;
                //}


                ////////////////////////////////////////////////////////////

                double dfixedtotal = 0;
                dfixedtotal = Convert.ToDouble(txtfixedtotal.Text);

                BusinessLogic blg = new BusinessLogic(sDataSource);

                double checktotal = 0;
                double difftotal = 0;

                difftotal = dfixedtotal - dTotalAmt;
                checktotal = (difftotal / dfixedtotal) * 100;

                double Percent = blg.getconfigpercent();
                double Amt = blg.getconfigamt();

                double ddd = Math.Min(difftotal, checktotal);

                //if (difftotal < 0)
                //{
                //    if (difftotal < -10)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Difference Between FixedTotal And GrandTotal Cannot be more than " + Amt + "')", true);
                //        return;
                //    }
                //}

                //if (ddd == difftotal)
                //{
                //    if (difftotal > Amt)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Difference Between FixedTotal And GrandTotal Cannot be more than " + Amt + "')", true);
                //        return;
                //    }
                //}
                //else if (ddd == checktotal)
                //{
                //    if (checktotal > Percent)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Difference Between FixedTotal And GrandTotal Cannot be more than" + Percent + "% ')", true);
                //        return;
                //    }
                //}


                double TotalAmt = (dfixedtotal - dTotalAmt);
                string Total = TotalAmt.ToString("#0.00");

                //if (Convert.ToDouble(Total) > 1)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Difference Between FixedTotal And GrandTotal Cannot be more than 1')", true);
                //    return;
                //}
                //else if (Convert.ToDouble(Total) < -1)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Difference Between FixedTotal And GrandTotal Cannot be less than 1')", true);
                //    return;
                //}

                ///////////////////////////////////////////







                DataSet receiptData = null;
                string MultiPayment = string.Empty;
                if (iPaymode == 4)
                {
                    var recAmount = double.Parse(lblReceivedTotal.Text);
                    var salesAmount = double.Parse(txtfixedtotal.Text);

                    if ((ddBank1.SelectedValue == "0" && txtAmount1.Text == "") &&
                        (ddBank2.SelectedValue == "0" && txtAmount2.Text == "") &&
                        (ddBank3.SelectedValue == "0" && txtAmount3.Text == "") &&
                        txtCashAmount.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter the Customer Payment details and try again.')", true);
                        tabs2.ActiveTabIndex = 1;
                        //updatePnlSales.Update();
                        return;
                    }

                    if (recAmount > salesAmount)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Customer Payment should not be Greater than Total Sales Amount.')", true);
                        tabs2.ActiveTabIndex = 1;
                        //updatePnlSales.Update();
                        return;
                    }


                    string userna = Request.Cookies["LoggedUserName"].Value;
                    DataSet dat = new DataSet();
                    dat = bl.ListReceiptsForBillNoOrder(lblBillNo.Text);
                    int TransN = 0;

                    if (dat.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dat.Tables[0].Rows)
                        {
                            TransN = Convert.ToInt32(dr["TransNo"]);
                            bl.DeleteCustReceiptSales(connection, TransN, false, userna);
                        }
                    }
                    else
                    {
                    }



                    receiptData = GenerateReceiptData();

                    if (ddBank1.SelectedValue != "0" && txtCCard1.Text != "" && txtAmount1.Text != "0")
                    {
                        DataRow dr = receiptData.Tables[0].NewRow();
                        dr["RefNo"] = "";
                        dr["TransDate"] = recondate;
                        dr["DebitorID"] = ddBank1.SelectedValue;
                        dr["CreditorID"] = cmbCustomer.SelectedValue;
                        dr["Amount"] = txtAmount1.Text;
                        dr["Narration"] = "";
                        dr["VoucherType"] = "Receipt";
                        dr["ChequeNo"] = txtCCard1.Text;
                        dr["Paymode"] = "Cheque";
                        dr["SFRefNo"] = txtRefNo1.Text;

                        receiptData.Tables[0].Rows.Add(dr);
                    }

                    if (ddBank2.SelectedValue != "0" && txtCCard2.Text != "" && txtAmount2.Text != "0")
                    {
                        DataRow dr = receiptData.Tables[0].NewRow();
                        dr["RefNo"] = "";
                        dr["TransDate"] = recondate;
                        dr["DebitorID"] = ddBank2.SelectedValue;
                        dr["CreditorID"] = cmbCustomer.SelectedValue;
                        dr["Amount"] = txtAmount2.Text;
                        dr["Narration"] = "";
                        dr["VoucherType"] = "Receipt";
                        dr["ChequeNo"] = txtCCard2.Text;
                        dr["Paymode"] = "Cheque";
                        dr["SFRefNo"] = txtRefNo2.Text;

                        receiptData.Tables[0].Rows.Add(dr);
                    }

                    if (ddBank3.SelectedValue != "0" && txtCCard3.Text != "" && txtAmount1.Text != "0")
                    {
                        DataRow dr = receiptData.Tables[0].NewRow();
                        dr["RefNo"] = "";
                        dr["TransDate"] = recondate;
                        dr["DebitorID"] = ddBank3.SelectedValue;
                        dr["CreditorID"] = cmbCustomer.SelectedValue;
                        dr["Amount"] = txtAmount3.Text;
                        dr["Narration"] = "";
                        dr["VoucherType"] = "Receipt";
                        dr["ChequeNo"] = txtCCard3.Text;
                        dr["Paymode"] = "Cheque";
                        dr["SFRefNo"] = txtRefNo3.Text;

                        receiptData.Tables[0].Rows.Add(dr);
                    }

                    if (!string.IsNullOrEmpty(txtCashAmount.Text) && txtCashAmount.Text != "0")
                    {
                        DataRow dr = receiptData.Tables[0].NewRow();
                        dr["RefNo"] = "";
                        dr["TransDate"] = recondate;
                        dr["DebitorID"] = "1";
                        dr["CreditorID"] = cmbCustomer.SelectedValue;
                        dr["Amount"] = txtCashAmount.Text;
                        dr["Narration"] = "";
                        dr["VoucherType"] = "Receipt";
                        dr["ChequeNo"] = "";
                        dr["Paymode"] = "Cash";
                        dr["SFRefNo"] = TextBox5.Text;

                        receiptData.Tables[0].Rows.Add(dr);
                    }

                    receiptData.Tables[0].AcceptChanges();

                    iPaymode = 3;
                    MultiPayment = "YES";
                }
                else if (iPaymode == 3)
                {
                    string username2 = Request.Cookies["LoggedUserName"].Value;

                    int TransN = 0;
                    string Multipay = "YES";
                    DataSet dsttt = bl.GetSalesForIdMulti(Convert.ToInt32(lblBillNo.Text), Multipay);

                    if (dsttt != null)
                    {
                        if (dsttt.Tables[0].Rows.Count > 0)
                        {
                            DataSet dat = new DataSet();
                            dat = bl.ListReceiptsForBillNoOrder(lblBillNo.Text);
                            TransN = 0;

                            if (dat.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dat.Tables[0].Rows)
                                {
                                    TransN = Convert.ToInt32(dr["TransNo"]);
                                    bl.DeleteCustReceiptSales(connection, TransN, false, username2);
                                }
                            }
                            else
                            {
                            }
                            MultiPayment = "NO";
                        }
                    }


                    DataSet datee = new DataSet();
                    datee = bl.ListReceiptsForBillNoOrder(lblBillNo.Text);

                    TransN = 0;


                    if (datee.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in datee.Tables[0].Rows)
                        {
                            TransN = Convert.ToInt32(dr["TransNo"]);
                            bl.DeleteCustReceivedAmount(connection, TransN, false, username2);
                        }
                    }
                    else
                    {
                    }



                }
                else
                {
                    string Multipay = "YES";

                    DataSet dsttt = bl.GetSalesForIdMulti(Convert.ToInt32(lblBillNo.Text), Multipay);
                    if (dsttt != null)
                    {
                        if (dsttt.Tables[0].Rows.Count > 0)
                        {
                            string username2 = Request.Cookies["LoggedUserName"].Value;

                            DataSet dat = new DataSet();
                            dat = bl.ListReceiptsForBillNoOrder(lblBillNo.Text);
                            int TransN = 0;

                            if (dat.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dat.Tables[0].Rows)
                                {
                                    TransN = Convert.ToInt32(dr["TransNo"]);
                                    bl.DeleteCustReceiptSales(connection, TransN, false, username2);
                                }
                            }
                            else
                            {
                            }

                            MultiPayment = "NO";
                        }
                    }
                }





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



                string check = "Y";
                string DuplicateCopy = "N";
                string CName = txtCustomerName.Text;

                int CustomerIdMobile = 0;
                if (chk.Checked == false)
                {
                    if (txtCustomerId.Text != "")
                    {
                        CustomerIdMobile = Convert.ToInt32(txtCustomerId.Text);
                    }
                }
                else
                {
                    CustomerIdMobile = 0;
                }

                string discType = GetDiscType();


                string usernam = Request.Cookies["LoggedUserName"].Value;


                //ds = (DataSet)GrdViewItems.DataSource;
                int bill = Convert.ToInt32(hdsales.Value);
                if (Session["productDs"] != null)
                {
                    ds = (DataSet)Session["productDs"];

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (chk.Checked == false)
                            {
                                if (bl.IsLedgerAlreadyFound(connection, CName))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Customer " + CName + " with this name already exists.');", true);
                                    return;
                                }

                                sCustomerID = bl.InsertCustomerInfoDirect(connection, CName, CName, 1, 0, 0, 0, "", CName, sCustomerAddress, sCustomerAddress2, sCustomerAddress3, "", "Customer", 0, "", sCustomerContact, 0, 0, "NO", "NO", "NO", CName, usernam, "YES", "", 3);
                                sCustomerName = txtCustomerName.Text;
                            }
                            else
                            {
                                sCustomerName = cmbCustomer.SelectedItem.Text;
                                sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                            }


                            //&&&&&& Sales Item Table Insert Dataset &&&&&&&&&&&&&&&&&                          


                            for (int vLoop = 0; vLoop < grvStudentDetails.Rows.Count; vLoop++)
                            {
                                DropDownList drpProduct = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpPrd");
                                DropDownList drpIncharge = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpIncharge");
                                TextBox txtDesc = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDesc");
                                TextBox txtRate = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRate");
                                TextBox txtTotalPrice = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTot");
                                TextBox txtStock = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtStock");
                                TextBox txtQty = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtQty");
                                TextBox txtExeComm = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtExeComm");
                                TextBox txtDisPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDisPre");
                                TextBox txtVATPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATPre");
                                TextBox txtCSTPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtCSTPre");
                                TextBox txtPrBefVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtPrBefVAT");
                                TextBox txtVATAmt = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATAmt");
                                TextBox txtRtVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRtVAT");
                                TextBox txtTotal = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTotal");

                                int col = vLoop + 1;

                                if (drpProduct.SelectedValue == "0")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Product in row " + col + " ')", true);
                                    return;
                                }
                                else if (drpIncharge.SelectedValue == "0")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Employee in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtDesc.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Description in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtRate.Text == "" || txtRate.Text == "0")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Rate in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtTotalPrice.Text == "" || txtTotalPrice.Text == "0")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Price is empty in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtStock.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock is empty in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtQty.Text == "" || txtQty.Text == "0")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Quantity in row " + col + " ')", true);
                                    return;
                                }
                                else if (Convert.ToInt32(txtQty.Text) > Convert.ToInt32(txtStock.Text))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given qty is greater than stock in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtExeComm.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Executive Commission in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtDisPre.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Discount Percentage in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtVATPre.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill VAT Percentage in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtCSTPre.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill CST Percentage in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtPrBefVAT.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Price Before VAT Amount in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtVATAmt.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill VAT Amount in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtRtVAT.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Row Total in row " + col + " ')", true);
                                    return;
                                }
                                else if (txtTotal.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Total in row " + col + " ')", true);
                                    return;
                                }

                                if ((drpnormalsales.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                                {
                                    double EXCLUSIVErate1 = 0;
                                    double EXCrate1 = 0;
                                    double ratewithqty = 0;
                                    if (Labelll.Text == "VAT EXCLUSIVE")
                                    {
                                        ratewithqty = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);
                                        //EXCLUSIVErate1 = (Convert.ToDouble(txtRate.Text)) + ((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtVATAmt.Text) / 100));
                                        EXCLUSIVErate1 = (Convert.ToDouble(ratewithqty)) + ((Convert.ToDouble(ratewithqty)) * (Convert.ToDouble(txtVATPre.Text) / 100));
                                    }
                                    else
                                    {
                                        // EXCLUSIVErate1 = Convert.ToDouble(txtRate.Text);
                                        ratewithqty = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);
                                        EXCLUSIVErate1 = Convert.ToDouble(ratewithqty);
                                    }

                                    DataSet dst = bl.ListProductMRPPrices(drpProduct.SelectedItem.Value.Trim());
                                    if (dst != null)
                                    {
                                        if (dst.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow drt in dst.Tables[0].Rows)
                                            {
                                                EXCrate1 = Convert.ToDouble(drt["rate"]);
                                            }
                                        }
                                    }
                                    if (EXCLUSIVErate1 > EXCrate1)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be greater than " + EXCrate1 + " in row " + col + "')", true);
                                        checkflag = true;
                                        return;
                                    }
                                }


                                //string usernam = Request.Cookies["LoggedUserName"].Value;
                                if (bl.CheckIfUserCanDoDeviation(usernam))
                                {

                                }
                                else
                                {
                                    if ((drpnormalsales.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                                    {
                                        double devvalue = 0;

                                        DataSet dsd = bl.getRateInformation(drpProduct.SelectedValue);
                                        if (dsd.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow drt in dsd.Tables[0].Rows)
                                            {
                                                devvalue = Convert.ToDouble(drt["deviation"]);
                                            }
                                        }

                                        if (devvalue == 0)
                                        {
                                            DataSet dsrate = bl.getRateInfo(drpProduct.SelectedValue);
                                            double rate1 = 0;
                                            double dp = 0;
                                            double overall = 0;
                                            double per = 0;
                                            double rate2 = 0;

                                            if (dsrate.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataRow dr in dsrate.Tables[0].Rows)
                                                {
                                                    dp = Convert.ToDouble(dr["dealerrate"]);
                                                    per = Convert.ToDouble(dr["deviation"]);
                                                    rate2 = (dp * per) / 100;
                                                    // rate1 = Convert.ToDouble(txtRate.Text);

                                                    rate1 = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);

                                                    if (Labelll.Text == "VAT EXCLUSIVE")
                                                    {
                                                        // rate1 = (Convert.ToDouble(txtRate.Text)) + ((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtVATAmt.Text) / 100));
                                                        rate1 = (Convert.ToDouble(rate1)) + ((Convert.ToDouble(rate1)) * (Convert.ToDouble(txtVATPre.Text) / 100));
                                                    }

                                                    if (rate1 < rate2)
                                                    {
                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be less than dealer rate Rs. " + rate2 + " ')", true);
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DataSet dsrate = bl.getRateInformation(drpProduct.SelectedValue);
                                            double rate1 = 0;
                                            double dp = 0;
                                            double overall = 0;
                                            double per = 0;
                                            double rate2 = 0;

                                            if (dsrate.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataRow dr in dsrate.Tables[0].Rows)
                                                {
                                                    dp = Convert.ToDouble(dr["dealerrate"]);
                                                    per = Convert.ToDouble(dr["deviation"]);
                                                    rate2 = (dp * per) / 100;
                                                    // rate1 = Convert.ToDouble(txtRate.Text);

                                                    rate1 = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);

                                                    if (Labelll.Text == "VAT EXCLUSIVE")
                                                    {
                                                        // rate1 = (Convert.ToDouble(txtRate.Text)) + ((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtVATAmt.Text) / 100));
                                                        rate1 = (Convert.ToDouble(rate1)) + ((Convert.ToDouble(rate1)) * (Convert.ToDouble(txtVATPre.Text) / 100));
                                                    }

                                                    if (rate1 < rate2)
                                                    {
                                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be less than dealer rate Rs. " + rate2 + " ')", true);
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                            DataSet dss;
                            DataTable dt;
                            DataRow drNew;

                            DataColumn dc;

                            dss = new DataSet();

                            dt = new DataTable();

                            dc = new DataColumn("Prd");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("Emp");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("Desc");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("Rate");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("TotPrice");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("Stock");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("Qty");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("ExeComm");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("DisPre");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("VATPre");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("CSTPre");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("PrBefVATAmt");
                            dt.Columns.Add(dc);


                            dc = new DataColumn("VATAmt");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("RtVAT");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("Tot");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("Prdname");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("MeasureUnit");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("Roles");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("IsRole");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("Bundles");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("Rods");
                            dt.Columns.Add(dc);

                            dc = new DataColumn("TotalMrp");
                            dt.Columns.Add(dc);

                            dss.Tables.Add(dt);

                            for (int vLoop = 0; vLoop < grvStudentDetails.Rows.Count; vLoop++)
                            {
                                DropDownList drpProduct = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpPrd");
                                DropDownList drpIncharge = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpIncharge");
                                TextBox txtDesc = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDesc");
                                TextBox txtRate = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRate");
                                TextBox txtTotalPrice = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTot");
                                TextBox txtStock = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtStock");
                                TextBox txtQty = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtQty");
                                TextBox txtExeComm = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtExeComm");
                                TextBox txtDisPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDisPre");
                                TextBox txtVATPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATPre");
                                TextBox txtCSTPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtCSTPre");
                                TextBox txtPrBefVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtPrBefVAT");
                                TextBox txtVATAmt = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATAmt");
                                TextBox txtRtVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRtVAT");
                                TextBox txtTotal = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTotal");


                                drNew = dt.NewRow();
                                drNew["Prd"] = Convert.ToString(drpProduct.SelectedItem.Value);
                                drNew["Emp"] = Convert.ToString(drpIncharge.SelectedItem.Value);
                                drNew["Desc"] = txtDesc.Text;
                                drNew["Rate"] = txtRate.Text;
                                drNew["TotPrice"] = txtTotalPrice.Text;
                                drNew["Stock"] = txtStock.Text;
                                drNew["Qty"] = txtQty.Text;
                                drNew["ExeComm"] = txtExeComm.Text;
                                drNew["DisPre"] = txtDisPre.Text;
                                drNew["VATPre"] = txtVATPre.Text;
                                drNew["CSTPre"] = txtCSTPre.Text;
                                drNew["PrBefVATAmt"] = txtPrBefVAT.Text;
                                drNew["VATAmt"] = txtVATAmt.Text;
                                drNew["RtVAT"] = txtRtVAT.Text;
                                drNew["Tot"] = txtTotal.Text;

                                drNew["Prdname"] = drpProduct.Text;
                                drNew["MeasureUnit"] = " ";
                                drNew["Roles"] = "1";
                                drNew["IsRole"] = "N";
                                drNew["Bundles"] = "0";
                                drNew["Rods"] = "0";
                                drNew["TotalMrp"] = txtTotal.Text;
                                dss.Tables[0].Rows.Add(drNew);
                            }
                            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                            //old code
                            //int billNo = bl.UpdateSalesNew(hdSeries.Value, bill, sBilldate, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, iPaymode, sCreditCardno, iBank, dTotalAmt, purchaseReturn, prReason, Convert.ToInt32(executive), dFreight, dLU, dss, sOtherCusName, intTrans, userID, deliveryNote, sCustomerAddress2, sCustomerAddress3, executivename, receiptData, despatchedfrom, fixedtotal, manualno, dTotalAmt, usernam, MultiPayment, Types, snarr, cuscategory);

                            int billNo = bl.UpdateSalesNew(hdSeries.Value, bill, sBilldate, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, iPaymode, sCreditCardno, iBank, dTotalAmt, purchaseReturn, prReason, dFreight, dLU, dss, sOtherCusName, intTrans, userID, receiptData, MultiPayment, deliveryNote, sCustomerAddress2, sCustomerAddress3, executivename, despatchedfrom, fixedtotal, manualno, dTotalAmt, usernam, Types, snarr, DuplicateCopy, check, CustomerIdMobile, cuscategory, discType);



                            if (billNo == -1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock Limit is Less')", true);
                                return;
                            }
                            else if (billNo == -3)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You have reached the maximum sales series. Please increase the maximum limit and try again.')", true);
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
                                            utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), sCustomerContact, smsTEXT, true, UserID);
                                        }
                                    }
                                }



                                string salestype = string.Empty;
                                int ScreenNo = 0;
                                string ScreenName = string.Empty;

                                if (purchaseReturn == "YES")
                                {
                                    salestype = "Purchase Return";
                                    ScreenName = "Purchase Return";
                                }
                                else if (intTrans == "YES")
                                {
                                    salestype = "Internal Transfer";
                                    ScreenName = "Internal Transfer Sales";
                                }
                                else if (deliveryNote == "YES")
                                {
                                    salestype = "Delivery Note";
                                    ScreenName = "Delivery Note Sales";
                                }
                                else if (drpmanualsales.SelectedValue == "YES")
                                {
                                    salestype = "Manual Sales";
                                    ScreenName = "Manual Sales";
                                }
                                else if (drpnormalsales.SelectedValue == "YES")
                                {
                                    salestype = "Normal Sales";
                                    ScreenName = "Normal Sales";
                                }

                                bool mobile = false;
                                bool Email = false;
                                string emailsubject = string.Empty;

                                string emailcontent = string.Empty;
                                if (hdEmailRequired.Value == "YES")
                                {
                                    DataSet dsd = bl.GetLedgerInfoForId(connection, sCustomerID);
                                    var toAddress = "";
                                    var toAdd = "";
                                    Int32 ModeofContact = 0;
                                    int ScreenType = 0;

                                    if (dsd != null)
                                    {
                                        if (dsd.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dsd.Tables[0].Rows)
                                            {
                                                toAdd = dr["EmailId"].ToString();
                                                ModeofContact = Convert.ToInt32(dr["ModeofContact"]);
                                            }
                                        }
                                    }


                                    DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                                    if (dsdd != null)
                                    {
                                        if (dsdd.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dsdd.Tables[0].Rows)
                                            {
                                                ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                                mobile = Convert.ToBoolean(dr["mobile"]);
                                                Email = Convert.ToBoolean(dr["Email"]);
                                                emailsubject = Convert.ToString(dr["emailsubject"]);
                                                emailcontent = Convert.ToString(dr["emailcontent"]);

                                                if (ScreenType == 1)
                                                {
                                                    if (dr["Name1"].ToString() == "Sales Executive")
                                                    {
                                                        toAddress = toAdd;
                                                    }
                                                    else if (dr["Name1"].ToString() == "Customer")
                                                    {
                                                        if (ModeofContact == 2)
                                                        {
                                                            toAddress = toAdd;
                                                        }
                                                        else
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        toAddress = toAdd;
                                                    }
                                                }
                                                else
                                                {
                                                    toAddress = dr["EmailId"].ToString();
                                                }
                                                if (Email == true)
                                                {
                                                    string body = "\n";

                                                    int index123 = emailcontent.IndexOf("@Branch");
                                                    body = Request.Cookies["Company"].Value;
                                                    if (index123 >= 0)
                                                    {
                                                        emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                                    }

                                                    int sno = 1;
                                                    string prd = string.Empty;
                                                    int index322 = emailcontent.IndexOf("@Product");
                                                    if (dss != null)
                                                    {
                                                        if (dss.Tables[0].Rows.Count > 0)
                                                        {
                                                            //emailcontent = emailcontent.Remove(index322, 8).Insert(index322, body);

                                                            foreach (DataRow drd in dss.Tables[0].Rows)
                                                            {

                                                                //body = drd["PrdName"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["Rate"].ToString();
                                                                prd = prd + "\n";
                                                                prd = prd + drd["PrdName"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["Rate"].ToString();
                                                                prd = prd + "\n";

                                                            }
                                                            if (index322 >= 0)
                                                            {
                                                                emailcontent = emailcontent.Remove(index322, 8).Insert(index322, prd);
                                                            }
                                                        }
                                                    }

                                                    int index312 = emailcontent.IndexOf("@User");
                                                    body = usernam;
                                                    if (index312 >= 0)
                                                    {
                                                        emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                                    }

                                                    int index2 = emailcontent.IndexOf("@Date");
                                                    body = txtBillDate.Text;
                                                    if (index2 >= 0)
                                                    {

                                                        emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                                    }

                                                    int index = emailcontent.IndexOf("@Customer");
                                                    body = sCustomerName;
                                                    if (index >= 0)
                                                    {
                                                        emailcontent = emailcontent.Remove(index, 9).Insert(index, body);
                                                    }

                                                    int index1 = emailcontent.IndexOf("@Amount");
                                                    body = txttotal.Text;
                                                    if (index1 >= 0)
                                                    {
                                                        emailcontent = emailcontent.Remove(index1, 7).Insert(index1, body);
                                                    }

                                                    string smtphostname = ConfigurationManager.AppSettings["SmtpHostName"].ToString();
                                                    int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPortNumber"]);
                                                    var fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

                                                    string fromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();

                                                    EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, emailcontent, fromPassword);

                                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email sent successfully')", true);

                                                }

                                            }
                                        }
                                    }
                                }

                                string smscontent = string.Empty;
                                if (hdSMSRequired.Value == "YES")
                                {
                                    DataSet dsd = bl.GetLedgerInfoForId(connection, sCustomerID);
                                    var toAddress = "";
                                    var toAdd = "";
                                    Int32 ModeofContact = 0;
                                    int ScreenType = 0;

                                    if (dsd != null)
                                    {
                                        if (dsd.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dsd.Tables[0].Rows)
                                            {
                                                toAdd = dr["Mobile"].ToString();
                                                ModeofContact = Convert.ToInt32(dr["ModeofContact"]);
                                            }
                                        }
                                    }


                                    DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                                    if (dsdd != null)
                                    {
                                        if (dsdd.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dsdd.Tables[0].Rows)
                                            {
                                                ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                                mobile = Convert.ToBoolean(dr["mobile"]);
                                                smscontent = Convert.ToString(dr["smscontent"]);

                                                if (ScreenType == 1)
                                                {
                                                    if (dr["Name1"].ToString() == "Sales Executive")
                                                    {
                                                        toAddress = toAdd;
                                                    }
                                                    else if (dr["Name1"].ToString() == "Customer")
                                                    {
                                                        if (ModeofContact == 1)
                                                        {
                                                            toAddress = toAdd;
                                                        }
                                                        else
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        toAddress = toAdd;
                                                    }
                                                }
                                                else
                                                {
                                                    toAddress = dr["mobile"].ToString();
                                                }
                                                if (mobile == true)
                                                {

                                                    string body = "\n";

                                                    int index123 = smscontent.IndexOf("@Branch");
                                                    body = Request.Cookies["Company"].Value;
                                                    if (index123 >= 0)
                                                    {
                                                        smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                                    }




                                                    int index312 = smscontent.IndexOf("@User");
                                                    body = usernam;
                                                    if (index312 >= 0)
                                                    {
                                                        smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                                    }

                                                    int index2 = smscontent.IndexOf("@Date");
                                                    body = txtBillDate.Text;
                                                    if (index2 >= 0)
                                                    {
                                                        smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                                    }

                                                    int index = smscontent.IndexOf("@Customer");
                                                    body = sCustomerName;
                                                    if (index >= 0)
                                                    {
                                                        smscontent = smscontent.Remove(index, 9).Insert(index, body);
                                                    }

                                                    int index1 = smscontent.IndexOf("@Amount");
                                                    body = txttotal.Text;
                                                    if (index1 >= 0)
                                                    {
                                                        smscontent = smscontent.Remove(index1, 7).Insert(index1, body);
                                                    }

                                                    int i = dss.Tables[0].Rows.Count;

                                                    int index322 = smscontent.IndexOf("@Product");

                                                    foreach (DataRow drd in dss.Tables[0].Rows)
                                                    {
                                                        smsTEXT = smsTEXT + drd["PrdName"].ToString() + " " + drd["Qty"].ToString() + " Qty @ " + GetCurrencyType() + "." + GetProductTotalExVAT(double.Parse(drd["Qty"].ToString()), double.Parse(drd["Rate"].ToString()), double.Parse(drd["Discount"].ToString()));
                                                        i = i - 1;

                                                        if (i != 0)
                                                            smsTEXT = smsTEXT + ", ";
                                                    }

                                                    smsTEXT = smsTEXT + ". Total Bill Amount is " + GetCurrencyType() + "." + lblNet.Text;
                                                    smsTEXT = smsTEXT + " . The Bill No. is " + billNo.ToString();
                                                    if (index322 >= 0)
                                                    {
                                                        smscontent = smscontent.Remove(index322, 8).Insert(index322, smsTEXT);
                                                    }

                                                    if (Session["Provider"] != null)
                                                    {
                                                        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), toAddress, smscontent, true, UserID);
                                                    }


                                                }

                                            }
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
                                Response.Redirect("PrintProductSalesBill.aspx?SID=" + billNo.ToString() + "&RT=" + purchaseReturn);
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

            ModalPopupMethod.Hide();

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
                Response.Redirect("PrintProductSalesBill.aspx?SID=" + hdsales.Value + "&RT=" + purchaseReturn);
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdCancelMethod_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupMethod.Hide();
            Response.Redirect("CustomerSales.aspx");
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

            GrdViewItems.Columns[13].Visible = true;
            GrdViewItems.Columns[14].Visible = true;

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
            lblTotalDis.Text = "0";
            lblTotalCST.Text = "0";
            lblTotalSum.Text = "0";
            lblTotalVAT.Text = "0";
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
            if (Session["Show"] == "Hide")
            {
                ModalPopupMethod.Hide();
            }
            else
            {
                ModalPopupMethod.Show();
            }
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
        string receivedBill = "";

        try
        {
            if (lblBillNo.Text != "Auto Generated.No need to enter")
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

                if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                {
                    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                    if (receivedBill != string.Empty)
                    {
                        //////drpPaymode.SelectedValue = "3";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                        //UpdatePanelPayMode.Update();
                        //return;
                    }
                }
            }

            if (chk.Checked == true)
            {
                if (cmbCustomer.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select customer before adding products');", true);
                    return;
                }
            }
            else if (chk.Checked == false)
            {
                if (txtCustomerName.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter customer before adding products');", true);
                    return;
                }
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
    protected void cmdMethod_Click(object sender, EventArgs e)
    {
        try
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


            //////////////////////////////////////////////////////////////////////

            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;

            if (optionmethod.SelectedValue == "InternalTransfer")
            {
                if (bl.CheckUserHaveOptions(usernam, "INTSAL"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to make Internal Transfer');", true);
                    return;
                }
            }
            else if (optionmethod.SelectedValue == "DeliveryNote")
            {
                if (bl.CheckUserHaveOptions(usernam, "DCSAL"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to make Delivery Note');", true);
                    return;
                }
            }
            else if (optionmethod.SelectedValue == "PurchaseReturn")
            {
                if (bl.CheckUserHaveOptions(usernam, "PURRET"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to make Purchase Return');", true);
                    return;
                }
            }

            ////////////////////////////////////////////////////////////////////////


            //lnkBtnAdd.Visible = false;
            //pnlSalesForm.Visible = true;
            //PanelBill.Visible = false;
            //pnlSearch.Visible = false;
            // hdMode.Value = "New";
            Reset();
            // lblTotalSum.Text = "0";
            lblTotalSum.Text = "0";
            lblTotalDis.Text = "0";
            lblTotalVAT.Text = "0";
            lblTotalCST.Text = "0";
            lblFreight.Text = "0";
            lblBillNo.Text = "Auto Generated.No need to enter";
            //txtBillDate.Focus();
            //MyAccordion.Visible = false;
            rowReason.Visible = false;

            txtCashAmount.Text = "";
            txtAmount3.Text = "";
            txtAmount2.Text = "";
            txtAmount1.Text = "";
            txtCCard1.Text = "";
            txtCCard2.Text = "";
            txtCCard3.Text = "";
            ddBank1.SelectedValue = "0";
            ddBank2.SelectedValue = "0";
            ddBank3.SelectedValue = "0";

            ResetProduct();
            //txtBillDate.Text = DateTime.Now.ToShortDateString();

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtBillDate.Text = dtaa;

            txtCustPh.Text = string.Empty;
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

            if (drpPaymode.Items.FindByValue("4") == null)
            {
                ListItem it = new ListItem("Multiple Payment", "4");
                drpPaymode.Items.Add(it);
            }

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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

            //SetInitialRow();



            EmptyRow();


            if (optionmethod.SelectedValue == "NormalSales")
            {
                drpnormalsales.ClearSelection();
                ListItem clitt = drpnormalsales.Items.FindByValue(Convert.ToString("YES"));
                if (clitt != null) clitt.Selected = true;

                drpmanualsales.ClearSelection();
                ListItem clittt = drpmanualsales.Items.FindByValue(Convert.ToString("NO"));
                if (clittt != null) clittt.Selected = true;

                drpIntTrans.ClearSelection();
                ListItem cli = drpIntTrans.Items.FindByValue(Convert.ToString("NO"));
                if (cli != null) cli.Selected = true;

                drpPurchaseReturn.ClearSelection();
                ListItem c = drpPurchaseReturn.Items.FindByValue(Convert.ToString("NO"));
                if (c != null) c.Selected = true;

                ddDeliveryNote.ClearSelection();
                ListItem cl = ddDeliveryNote.Items.FindByValue(Convert.ToString("NO"));
                if (cl != null) cl.Selected = true;

                drpIntTrans.Enabled = false;
                drpPurchaseReturn.Enabled = false;
                ddDeliveryNote.Enabled = false;
                drpnormalsales.Enabled = false;
                drpmanualsales.Enabled = false;

                rowReason.Visible = false;
                //lblVATAdd.Enabled = true;
                rowmanual.Visible = false;
                drpPurID.Items.Clear();

            }
            else if (optionmethod.SelectedValue == "InternalTransfer")
            {
                drpIntTrans.ClearSelection();
                ListItem cli = drpIntTrans.Items.FindByValue(Convert.ToString("YES"));
                if (cli != null) cli.Selected = true;

                drpPurchaseReturn.ClearSelection();
                ListItem c = drpPurchaseReturn.Items.FindByValue(Convert.ToString("NO"));
                if (c != null) c.Selected = true;

                ddDeliveryNote.ClearSelection();
                ListItem cl = ddDeliveryNote.Items.FindByValue(Convert.ToString("NO"));
                if (cl != null) cl.Selected = true;

                drpnormalsales.ClearSelection();
                ListItem clitt = drpnormalsales.Items.FindByValue(Convert.ToString("NO"));
                if (clitt != null) clitt.Selected = true;

                drpmanualsales.ClearSelection();
                ListItem clittt = drpmanualsales.Items.FindByValue(Convert.ToString("NO"));
                if (clittt != null) clittt.Selected = true;

                //lblVATAdd.Enabled = false;
                drpIntTrans.Enabled = false;
                drpPurchaseReturn.Enabled = false;
                drpnormalsales.Enabled = false;
                drpmanualsales.Enabled = false;
                ddDeliveryNote.Enabled = false;

                rowReason.Visible = false;
                rowmanual.Visible = false;

                drpPaymode.SelectedValue = "3";
                drpPurID.Items.Clear();

            }
            else if (optionmethod.SelectedValue == "DeliveryNote")
            {
                ddDeliveryNote.ClearSelection();
                ListItem cl = ddDeliveryNote.Items.FindByValue(Convert.ToString("YES"));
                if (cl != null) cl.Selected = true;

                drpIntTrans.ClearSelection();
                ListItem cli = drpIntTrans.Items.FindByValue(Convert.ToString("NO"));
                if (cli != null) cli.Selected = true;

                drpPurchaseReturn.ClearSelection();
                ListItem c = drpPurchaseReturn.Items.FindByValue(Convert.ToString("NO"));
                if (c != null) c.Selected = true;

                drpnormalsales.ClearSelection();
                ListItem clitt = drpnormalsales.Items.FindByValue(Convert.ToString("NO"));
                if (clitt != null) clitt.Selected = true;

                drpmanualsales.ClearSelection();
                ListItem clittt = drpmanualsales.Items.FindByValue(Convert.ToString("NO"));
                if (clittt != null) clittt.Selected = true;

                //lblVATAdd.Enabled = true;
                drpIntTrans.Enabled = false;
                drpPurchaseReturn.Enabled = false;
                drpnormalsales.Enabled = false;
                drpmanualsales.Enabled = false;
                ddDeliveryNote.Enabled = false;

                rowReason.Visible = false;
                rowmanual.Visible = false;

                drpPaymode.SelectedValue = "3";
                drpPurID.Items.Clear();

            }
            else if (optionmethod.SelectedValue == "PurchaseReturn")
            {
                drpPurchaseReturn.ClearSelection();
                ListItem cliddd = drpPurchaseReturn.Items.FindByValue(Convert.ToString("YES"));
                if (cliddd != null) cliddd.Selected = true;

                ddDeliveryNote.ClearSelection();
                ListItem cl = ddDeliveryNote.Items.FindByValue(Convert.ToString("NO"));
                if (cl != null) cl.Selected = true;

                drpIntTrans.ClearSelection();
                ListItem cli = drpIntTrans.Items.FindByValue(Convert.ToString("NO"));
                if (cli != null) cli.Selected = true;

                drpnormalsales.ClearSelection();
                ListItem clitt = drpnormalsales.Items.FindByValue(Convert.ToString("NO"));
                if (clitt != null) clitt.Selected = true;

                drpmanualsales.ClearSelection();
                ListItem clittt = drpmanualsales.Items.FindByValue(Convert.ToString("NO"));
                if (clittt != null) clittt.Selected = true;

                //lblVATAdd.Enabled = false;
                drpIntTrans.Enabled = false;
                drpnormalsales.Enabled = false;
                drpmanualsales.Enabled = false;
                drpPurchaseReturn.Enabled = false;
                ddDeliveryNote.Enabled = false;

                rowReason.Visible = true;
                rowmanual.Visible = false;

                loadPurchaseID();

            }
            else if (optionmethod.SelectedValue == "ManualSales")
            {
                drpIntTrans.ClearSelection();
                ListItem cli = drpIntTrans.Items.FindByValue(Convert.ToString("NO"));
                if (cli != null) cli.Selected = true;

                drpPurchaseReturn.ClearSelection();
                ListItem c = drpPurchaseReturn.Items.FindByValue(Convert.ToString("NO"));
                if (c != null) c.Selected = true;

                ddDeliveryNote.ClearSelection();
                ListItem cl = ddDeliveryNote.Items.FindByValue(Convert.ToString("NO"));
                if (cl != null) cl.Selected = true;

                drpnormalsales.ClearSelection();
                ListItem clitt = drpnormalsales.Items.FindByValue(Convert.ToString("NO"));
                if (clitt != null) clitt.Selected = true;

                drpmanualsales.ClearSelection();
                ListItem clittt = drpmanualsales.Items.FindByValue(Convert.ToString("YES"));
                if (clittt != null) clittt.Selected = true;

                drpIntTrans.Enabled = false;
                drpPurchaseReturn.Enabled = false;
                ddDeliveryNote.Enabled = false;
                drpnormalsales.Enabled = false;
                drpmanualsales.Enabled = false;

                rowReason.Visible = false;
                //lblVATAdd.Enabled = true;
                rowmanual.Visible = true;
                drpPurID.Items.Clear();
            }

            if (drpPurchaseReturn.SelectedValue == "NO")
            {
                loadSupplier("Sundry Debtors");
            }
            else
            {
                loadSupplier("Sundry Creditors");
            }

            loadBanks();
            FirstGridViewRow();
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            //    string connection = string.Empty;
            //string recondate = string.Empty;
            //double stock = 0;
            //DataTable dt;
            //DataRow drNew;
            //DataColumn dc;
            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //string sDiscount = "";
            //string sVat = "";
            //string sCST = "";
            //double dTotal = 0;
            //string[] prodItem;
            //string roleFlag = string.Empty;
            //DataSet dsRole = new DataSet();
            //string strRole = string.Empty;
            //string strQty = string.Empty;
            //string strMeasureUnit = string.Empty;
            //string strExecutive = string.Empty;// krishnavelu 26 June
            //string strExecName = string.Empty;
            //string execCharge = "";// krishnavelu 26 June

            //bool dupFlag = false;
            //DataSet ds;
            //hdOpr.Value = "New";
            //string itemCode = string.Empty;

            //    if (Session["productDs"] != null)
            //    {
            //        DataSet checkDs = (DataSet)Session["productDs"];

            //        foreach (DataRow dR in checkDs.Tables[0].Rows)
            //        {
            //            if (dR["itemCode"] != null)
            //            {
            //                if (dR["itemCode"].ToString().Trim() == cmbProdAdd.SelectedValue.Trim())
            //                {
            //                    dupFlag = true;
            //                    break;
            //                }
            //            }
            //        }
            //    }




            //    if (Session["productDs"] == null)
            //    {
            //        ds = new DataSet();

            //        dt = new DataTable();

            //        dc = new DataColumn("itemCode");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("Billno");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("ProductName");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("ProductDesc");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("Qty");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("Rate");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("Measure_unit");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("Discount");
            //        dt.Columns.Add(dc);

            //        // krishnavelu 26 June
            //        dc = new DataColumn("ExecCharge");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("VAT");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("CST");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("Roles");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("IsRole");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("Total");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("Bundles");
            //        dt.Columns.Add(dc);

            //        dc = new DataColumn("Rods");
            //        dt.Columns.Add(dc);

            //        ds.Tables.Add(dt);

            //        drNew = dt.NewRow();
            //        //dTotal = Convert.ToDouble(txtQtyAdd.Text) * Convert.ToDouble(txtRateAdd.Text);

            //        //string disType = GetDiscType();



            //        drNew["itemCode"] = "";
            //        drNew["Billno"] = 0;
            //        drNew["ProductName"] = 0;
            //        drNew["ProductDesc"] = "";
            //        drNew["Qty"] = "";
            //        drNew["Measure_Unit"] = "";
            //        drNew["Rate"] = 0;
            //        drNew["Discount"] = 0;
            //        drNew["ExecCharge"] = 0;
            //        drNew["VAT"] = 0;
            //        drNew["CST"] = 0;
            //        drNew["Roles"] = 0;
            //        drNew["IsRole"] = "N";
            //        drNew["Total"] = 0;
            //        drNew["Bundles"] = "0";
            //        drNew["Rods"] = "0";
            //        ds.Tables[0].Rows.Add(drNew);
            //        Session["productDs"] = ds;

            //    }
            //    else
            //    {
            //        ds = (DataSet)Session["productDs"];
            //        drNew = ds.Tables[0].NewRow();
            //        dTotal = Convert.ToDouble(txtQtyAdd.Text) * Convert.ToDouble(txtRateAdd.Text);

            //        string disType = GetDiscType();



            //        drNew["itemCode"] = Convert.ToString(cmbProdAdd.SelectedValue);
            //        drNew["Billno"] = hdsales.Value;
            //        drNew["ProductName"] = lblProdDescAdd.Text;
            //        drNew["ProductDesc"] = lblProdDescAdd.Text;
            //        drNew["Qty"] = txtQtyAdd.Text.Trim();
            //        drNew["Measure_Unit"] = "";//lblUnitMrmnt.Text.Trim();
            //        drNew["Rate"] = txtRateAdd.Text.Trim();
            //        drNew["Discount"] = sDiscount;
            //        drNew["ExecCharge"] = execCharge;
            //        drNew["VAT"] = sVat;
            //        drNew["CST"] = sCST;
            //        drNew["Roles"] = hdCurrRole.Value;
            //        drNew["IsRole"] = "N";
            //        drNew["Total"] = Convert.ToString(dTotal);
            //        drNew["Bundles"] = "0";
            //        drNew["Rods"] = "0";
            //        ds.Tables[0].Rows.Add(drNew);

            //    }

            //    //cmdSaveProduct.Visible = true;
            //    //cmdUpdateProduct.Visible = false;
            //    //cmdCancelProduct.Visible = false;
            //    GrdViewItems.DataSource = ds;
            //    GrdViewItems.DataBind();


            //    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Show"] = "No";
            optionmethod.SelectedIndex = 0;
            Session["Method"] = "Add";

            BusinessLogic bl = new BusinessLogic(sDataSource);

            BillingMethod = bl.getConfigInfoMethod();
            if (BillingMethod == "VAT INCLUSIVE")
            {
                Labelll.Text = "VAT INCLUSIVE";
            }
            else
            {
                Labelll.Text = "VAT EXCLUSIVE";
            }
            FirstGridViewRow();
            ModalPopupMethod.Show();
            //ModalPopupExtender1.Show();
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

    private void Reset()
    {

        txtAddress.Text = "";
        txtAddress2.Text = "";
        txtAddress3.Text = "";
        cmbCustomer.SelectedIndex = 0;
        drpPaymode.SelectedIndex = 0;
        drpBankName.SelectedIndex = 0;
        drpPurchaseReturn.SelectedIndex = 0;
        txtCreditCardNo.Text = "";
        txtPRReason.Text = "";
        //lblUnitMrmnt.Text = "";
        lblledgerCategory.Text = "";
        txtFreight.Text = "0";
        txtLU.Text = "0";
        txtfixedtotal.Text = "";
        txtdespatced.Text = "";
        txtnarr.Text = "";
        chk.Checked = true;
        txtCustomerName.Text = "";
        cmbCustomer.Visible = true;
        txtCustomerName.Visible = false;
        txtCustomerId.Text = "";
        txtCustomerId.Visible = false;
        drpMobile.Visible = true;
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
        //drpIncharge.SelectedValue = "0";

        txttotal.Text = "0";
        txtsubtot.Text = "0";
        TextBox1.Text = "0";
        //lblUnitMrmnt.Text = "";
        //cmbProdAdd.SelectedIndex = 0;
    }

    //public string GetTotal(double qty, double rate, double discount, double VAT, double CST)
    //{
    //    double tot = 0;
    //    tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));
    //    amtTotal = amtTotal + Convert.ToDouble(tot);
    //    disTotal = disTotal + discount;
    //    rateTotal = rateTotal + rate;
    //    vatTotal = vatTotal + VAT;
    //    cstTotal = cstTotal + CST;
    //    hdTotalAmt.Value = amtTotal.ToString("#0.00");
    //    //lblGrandTotal.Text = Convert.ToString(Convert.ToDecimal(tot) +Convert.ToDecimal(hdTotalAmt.Value));
    //    return tot.ToString("#0.00");
    //}

    public string GetTotal(double qty, double rate, double discount, double VAT, double CST, double Totalmrp)
    {
        double dis = 0;
        double disRate = 0;
        double vat = 0;
        double cst = 0;
        double tot = 0;
        double sVatamount = 0;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        BillingMethod = bl.getConfigInfoMethod();

        if (Labelll.Text == "VAT INCLUSIVE")
        {
            double vatper = 0;
            double vatinclusiverate = 0;
            if (VAT == 14.5)
            {
                vatper = 1.145;
                vatinclusiverate = ((qty * Totalmrp) - ((qty * Totalmrp) * (discount / 100))) / vatper;
                //sVatamount = ((qty * rate) - ((qty * rate) * (discount / 100))) - vatinclusiverate;
                sVatamount = (vatinclusiverate * 14.5) / 100;
            }
            else if (VAT == 5)
            {
                vatper = 1.05;
                vatinclusiverate = ((qty * Totalmrp) - ((qty * Totalmrp) * (discount / 100))) / vatper;
                //sVatamount = ((qty * rate) - ((qty * rate) * (discount / 100))) - vatinclusiverate;
                sVatamount = (vatinclusiverate * 5) / 100;
            }
            else
            {
                vatper = (VAT + 100) / 100;
                vatinclusiverate = ((qty * Totalmrp) - ((qty * Totalmrp) * (discount / 100))) / vatper;
                //sVatamount = ((qty * rate) - ((qty * rate) * (discount / 100))) - vatinclusiverate;
                sVatamount = (vatinclusiverate * VAT) / 100;
            }

            tot = (vatinclusiverate) + sVatamount + ((qty * Totalmrp) * (CST / 100));

            disRate = (qty * Totalmrp);
            dis = ((qty * Totalmrp) * (discount / 100));
            vat = (disRate * (VAT / 100));
            cst = (disRate * (CST / 100));
            amtTotal = amtTotal + Convert.ToDouble(tot);
            disTotal = dis;
            rateTotal = rateTotal + Totalmrp;
            vatTotal = vat;
            cstTotal = cst;
            disTotalRate = qty * Totalmrp;
            hdTotalAmt.Value = amtTotal.ToString("#0.00");
            return tot.ToString("#0.00");
        }
        else
        {

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
    }

    public string GetTotalVat532013(double qty, double rate, double discount, double VAT, double CST)
    {
        //if (Session["myname"] == "Sam")
        //{

        //    string tot = "";


        //    return tot.ToString();
        //}
        //else
        //{
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


    //public string GetTotal22(string qty,string rate,string discount,string VAT,string CST)
    //{
    //    double dis = 0;
    //    double disRate = 0;
    //    double vat = 0;
    //    double cst = 0;
    //    double tot = 0;
    //    tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));

    //    // tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));
    //    disRate = (qty * rate) - ((qty * rate) * (discount / 100));
    //    dis = ((qty * rate) * (discount / 100));

    //    vat = (disRate * (VAT / 100));
    //    cst = (disRate * (CST / 100));
    //    amtTotal = amtTotal + Convert.ToDouble(tot);
    //    disTotal = dis;
    //    rateTotal = rateTotal + rate;
    //    vatTotal = vat;
    //    cstTotal = cst;
    //    disTotalRate = qty * rate;
    //    hdTotalAmt.Value = amtTotal.ToString("#0.00");
    //    //lblGrandTotal.Text = Convert.ToString(Convert.ToDecimal(tot) +Convert.ToDecimal(hdTotalAmt.Value));
    //    return tot.ToString("#0.00");
    //}



    public string GetTotalVatExclsive(double qty, double rate, double discount, double VAT, double CST)
    {
        //if (Session["myname"] == "Sam")
        //{

        //    string tot = "";


        //    return tot.ToString();
        //}
        //else
        //{
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

        dc = new DataColumn("SFRefNo");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        return ds;
    }

    #endregion

    #region GridViewItems

    protected void GrdViewItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strItemCode = string.Empty;
            string strRoleFlag = string.Empty;
            DataSet ds = new DataSet();
            int billno = 0;
            GridViewRow row = GrdViewItems.SelectedRow;

            BusinessLogic bl = new BusinessLogic(sDataSource);



            string receivedBill = "";

            if (lblBillNo.Text != "Auto Generated.No need to enter")
            {
                var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

                if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                {
                    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                    if (receivedBill != string.Empty)
                    {
                        //////drpPaymode.SelectedValue = "3";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                        //UpdatePanelPayMode.Update();
                        //return;
                    }
                }
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
                    //LoadProducts(this, null);
                    LoadAllProducts(this, null);
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
            txtRateAdd.Text = row.Cells[11].Text;
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

                cvDisc.Enabled = false;
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
                cvDisc.Enabled = true;
            }

            txtExecCharge.Text = row.Cells[5].Text;
            lblVATAdd.Text = row.Cells[8].Text;
            lblCSTAdd.Text = row.Cells[9].Text;
            //lblProdNameAdd.Text = row.Cells[1].Text;
            lblProdDescAdd.Text = row.Cells[1].Text;

            TextBox1.Text = row.Cells[10].Text;
            txtsubtot.Text = row.Cells[11].Text;
            txttotal.Text = row.Cells[12].Text;

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

            if (lblBillNo.Text != "Auto Generated.No need to enter")
            {
                var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

                if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                {
                    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

                    if (receivedBill != string.Empty)
                    {
                        //////drpPaymode.SelectedValue = "3";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                        //UpdatePanelPayMode.Update();
                        //return;
                    }
                }
            }

            if (Session["productDs"] != null)
            {

                GridViewRow row = GrdViewItems.Rows[e.RowIndex];

                // strRole = GrdViewItems.DataKeys[row.DataItemIndex].Value.ToString();
                strItemcode = row.Cells[0].Text;
                billno = Convert.ToInt32(hdsales.Value);
                int rowsAff = bl.DeleteSalesProduct(billno, strItemcode);
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
    protected void GrdViewSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewSales.PageIndex = e.NewPageIndex;

            //int strBillno = 0;
            //int strTransno = 0;

            //if (txtBillnoSrc.Text.Trim() != "")
            //    strBillno = Convert.ToInt32(txtBillnoSrc.Text.Trim());
            //else
            //    strBillno = 0;

            //if (txtTransNo.Text.Trim() != "")
            //    strTransno = Convert.ToInt32(txtTransNo.Text.Trim());
            //else
            //    strTransno = 0;

            string textt = string.Empty;
            string dropd = string.Empty;

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;

            BindGrid(textt, dropd);
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewSales_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewSales, e.Row, this);
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
                    if (product[8].ToString() != "")
                        _TotalExecComm = _TotalExecComm + Convert.ToDouble(product[8].ToString());
                }

                if (product[7] != null)
                {
                    if (product[7].ToString() != "")
                        _TotalDiscount = _TotalDiscount + Convert.ToDouble(product[7].ToString());
                }

                if (product[9] != null)
                {
                    if (product[9].ToString() != "")
                        _TotalVAT = _TotalVAT + Convert.ToDouble(product[9].ToString());
                }

                if (product[10] != null)
                {
                    if (product[10].ToString() != "")
                        _TotalCST = _TotalCST + Convert.ToDouble(product[10].ToString());
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
    protected void GrdViewSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string paymode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paymode"));
                Label payMode = (Label)e.Row.FindControl("lblPaymode");
                if (paymode == "1")
                    payMode.Text = "Cash";
                else if (paymode == "2")
                    payMode.Text = "Bank";
                else
                {
                    payMode.Text = "Credit";

                    if (e.Row.FindControl("hdPaymode") != null)
                    {
                        var x = ((HiddenField)e.Row.FindControl("hdPaymode")).Value;

                        if (x == "YES")
                            payMode.Text = "MultiPayment";

                    }
                }

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "SALES"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "SALES"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveView(usernam, "SALES"))
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


    protected void GrdViewSales_Sorting(object sender, GridViewSortEventArgs e)
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

            //if (txtBillnoSrc.Text != "")
            if (ddCriteria.SelectedValue == "BillNo")
                BillNo = Convert.ToInt32(txtSearch.Text);



            var data = BindGridData(BillNo);
            //var sortedData = from x in data.Tables[0].AsEnumerable()
            //                 orderby x.Field<string>(expression) 
            //                 select x;

            var sortedData = data.Tables[0].DefaultView;

            sortedData.Sort = expression + " " + direction;

            GrdViewSales.DataSource = sortedData;
            GrdViewSales.DataBind();
            GrdViewSales.HeaderRow.Cells[GetSortColumnIndex(expression)].Controls.Add(sortImage);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected int GetSortColumnIndex(string sortExpresssion)
    {
        foreach (DataControlField field in GrdViewSales.Columns)
        {
            if (field.SortExpression == sortExpresssion)
                return GrdViewSales.Columns.IndexOf(field);

        }

        return -1;
    }


    protected void GrdViewSales_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["Show"] = "Hide";
            drpIntTrans.Enabled = true;
            drpPurchaseReturn.Enabled = true;

            ddDeliveryNote.Enabled = true;
            Session["Method"] = "Edit";

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

            string strPaymode = string.Empty;
            string MultiPaymode = string.Empty;
            string sCustomer = string.Empty;
            int salesID = 0;
            string connection = Request.Cookies["Company"].Value;
            GridViewRow row = GrdViewSales.SelectedRow;
            DataSet itemDs = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string recondate = row.Cells[2].Text;
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

            if (GrdViewSales.SelectedDataKey.Value != null && GrdViewSales.SelectedDataKey.Value.ToString() != "")
                salesID = Convert.ToInt32(GrdViewSales.SelectedDataKey.Value.ToString());


            DataSet ds = bl.GetSalesForId(salesID);

            hdsales.Value = salesID.ToString();
            //txtBillDate.Focus();

            string scuscategory = string.Empty;
            loadBanksEdit();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Types"] != null)
                        Labelll.Text = Convert.ToString(ds.Tables[0].Rows[0]["Types"]);
                    else
                        Labelll.Text = "";

                    if (ds.Tables[0].Rows[0]["BillDate"] != null)
                        txtBillDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["BillNo"] != null)
                        lblBillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();

                    if (ds.Tables[0].Rows[0]["InternalTransfer"] != null)
                    {
                        drpIntTrans.ClearSelection();
                        ListItem cli = drpIntTrans.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["InternalTransfer"]));

                        if (cli != null) cli.Selected = true;
                    }
                    else
                        //drpIncharge.SelectedIndex = 0;

                        if (ds.Tables[0].Rows[0]["ManualSales"] != null)
                        {
                            drpmanualsales.ClearSelection();
                            ListItem cli = drpmanualsales.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["ManualSales"]));

                            if (cli != null) cli.Selected = true;
                        }
                        else
                            drpmanualsales.SelectedIndex = 0;

                    if (ds.Tables[0].Rows[0]["NormalSales"] != null)
                    {
                        drpnormalsales.ClearSelection();
                        ListItem cli = drpnormalsales.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["NormalSales"]));

                        if (cli != null) cli.Selected = true;
                    }
                    else
                        drpnormalsales.SelectedIndex = 0;

                    if (ds.Tables[0].Rows[0]["DeliveryNote"] != null)
                    {
                        ddDeliveryNote.ClearSelection();
                        ListItem cli = ddDeliveryNote.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["DeliveryNote"]));

                        if (cli != null) cli.Selected = true;
                    }
                    else
                        ddDeliveryNote.SelectedIndex = 0;

                    if (ds.Tables[0].Rows[0]["PurchaseReturn"] != null)
                    {
                        drpPurchaseReturn.ClearSelection();
                        if (drpPurchaseReturn.Items.FindByValue(ds.Tables[0].Rows[0]["PurchaseReturn"].ToString().ToUpper().Trim()) != null)
                            drpPurchaseReturn.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PurchaseReturn"]).ToUpper();
                    }

                    if (drpPurchaseReturn.SelectedValue == "NO")
                    {
                        loadSupplierEdit("Sundry Debtors");
                    }
                    else
                    {
                        loadSupplierEdit("Sundry Creditors");
                    }

                    if (ds.Tables[0].Rows[0]["CustomerID"] != null)
                    {
                        sCustomer = Convert.ToString(ds.Tables[0].Rows[0]["CustomerID"]);
                        cmbCustomer.Visible = true;
                        cmbCustomer.ClearSelection();
                        ListItem li = cmbCustomer.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }

                    if (ds.Tables[0].Rows[0]["cuscategory"] != null)
                    {
                        scuscategory = Convert.ToString(ds.Tables[0].Rows[0]["cuscategory"]);
                        drpCustomerCategoryAdd.ClearSelection();
                        ListItem li = drpCustomerCategoryAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(scuscategory));
                        if (li != null) li.Selected = true;
                    }

                    if (ds.Tables[0].Rows[0]["CustomerAddress"] != null)
                        txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerAddress"]);
                    else
                        txtAddress.Text = "";

                    if (ds.Tables[0].Rows[0]["CustomerAddress2"] != null)
                        txtAddress2.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerAddress2"]);
                    else
                        txtAddress2.Text = "";

                    if (ds.Tables[0].Rows[0]["CustomerIdMobile"] != null)
                        txtCustomerId.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerIdMobile"]);
                    else
                        txtCustomerId.Text = "";

                    if (Convert.ToString(ds.Tables[0].Rows[0]["Check1"]) == "Y")
                        chk.Checked = true;
                    else
                        chk.Checked = false;

                    if (ds.Tables[0].Rows[0]["CustomerAddress3"] != null)
                        txtAddress3.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerAddress3"]);
                    else
                        txtAddress3.Text = "";

                    if (ds.Tables[0].Rows[0]["CustomerContacts"] != null)
                        txtCustPh.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerContacts"]);

                    if (ds.Tables[0].Rows[0]["despatchedfrom"] != null)
                        txtdespatced.Text = Convert.ToString(ds.Tables[0].Rows[0]["despatchedfrom"]);

                    if (ds.Tables[0].Rows[0]["narration2"] != null)
                        txtnarr.Text = Convert.ToString(ds.Tables[0].Rows[0]["narration2"]);

                    if (ds.Tables[0].Rows[0]["Amount"] != null)
                        txtfixedtotal.Text = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
                    lblNet.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"]).ToString("#0.00");

                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["manualNo"]) == 0)
                    {
                        rowmanual.Visible = false;
                    }
                    else if (ds.Tables[0].Rows[0]["manualNo"] != null)
                    {
                        txtmanual.Text = Convert.ToString(ds.Tables[0].Rows[0]["manualno"]);
                        rowmanual.Visible = true;
                    }


                    //if (ds.Tables[0].Rows[0]["manual"] != null)
                    //{
                    //    if (ds.Tables[0].Rows[0]["manual"] == "YES")
                    //        chkboxManual.Checked = true;
                    //    else
                    //        chkboxManual.Checked = false;
                    //}

                    // krishnavelu 26 June
                    txtOtherCusName.Text = Convert.ToString(ds.Tables[0].Rows[0]["OtherCusName"]); // krishnavelu 26 June

                    if (sCustomer.ToUpper() == "OTHERS")
                        txtOtherCusName.Visible = true;
                    else
                        txtOtherCusName.Visible = false;

                    strPaymode = ds.Tables[0].Rows[0]["Paymode"].ToString();

                    hdSeries.Value = ds.Tables[0].Rows[0]["SeriesID"].ToString();

                    drpPaymode.ClearSelection();
                    ListItem pLi = drpPaymode.Items.FindByValue(strPaymode.Trim());

                    if (pLi != null) pLi.Selected = true;

                    if (ds.Tables[0].Rows[0]["MultiPayment"] != null)
                        MultiPaymode = ds.Tables[0].Rows[0]["MultiPayment"].ToString();

                    if (MultiPaymode == "YES")
                    {

                        if (drpPaymode.Items.FindByValue("4") != null)
                        {
                            drpPaymode.SelectedValue = "4";
                        }
                        else
                        {
                            ListItem it = new ListItem("Multiple Payment", "4");
                            drpPaymode.Items.Add(it);
                            drpPaymode.SelectedValue = "4";
                        }

                        drpPaymode.Enabled = false;
                    }
                    else
                    {
                        //ListItem item = drpPaymode.Items.FindByValue("4");
                        //drpPaymode.Items.Remove(item);
                        //drpPaymode.Enabled = true;
                    }

                    if (paymodeVisible(strPaymode))
                    {
                        if (ds.Tables[0].Rows[0]["CreditCardNo"] != null)
                            txtCreditCardNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CreditCardNo"]);
                        if (ds.Tables[0].Rows[0]["Debtor"] != null)
                        {


                            drpBankName.ClearSelection();
                            ListItem cli = drpBankName.Items.FindByText(HttpUtility.HtmlDecode(Convert.ToString(ds.Tables[0].Rows[0]["Debtor"])));

                            if (cli != null) cli.Selected = true;
                            //rvbank.Enabled = true;
                            //rvCredit.Enabled = true;
                        }
                    }

                    if (drpPurchaseReturn.SelectedValue == "YES")
                    {
                        rowReason.Visible = true;

                        if (ds.Tables[0].Rows[0]["PurchaseReturnReason"] != null)
                            txtPRReason.Text = Convert.ToString(ds.Tables[0].Rows[0]["PurchaseReturnReason"]);
                        else
                            txtPRReason.Text = "";
                    }
                    else
                    {
                        rowReason.Visible = false;
                    }

                    //if (ds.Tables[0].Rows[0]["Executive"] != null)
                    //{
                    //drpIncharge.ClearSelection();
                    //ListItem cli = drpIncharge.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["Executive"]));

                    // if (cli != null) cli.Selected = true;
                    //}
                    //else
                    //drpIncharge.SelectedIndex = 0;



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



                    if (drpIntTrans.SelectedItem.Text == "YES")
                    {
                        optionmethod.SelectedIndex = 1;
                        lblVATAdd.Enabled = false;
                    }
                    else if (ddDeliveryNote.SelectedItem.Text == "YES")
                    {
                        optionmethod.SelectedIndex = 2;
                        lblVATAdd.Enabled = true;
                    }
                    else if (drpPurchaseReturn.SelectedItem.Text == "YES")
                    {
                        optionmethod.SelectedIndex = 3;
                        lblVATAdd.Enabled = true;
                    }
                    else if (ds.Tables[0].Rows[0]["ManualSales"] == "YES")
                    {
                        optionmethod.SelectedIndex = 4;
                        lblVATAdd.Enabled = true;
                    }
                    else if (ds.Tables[0].Rows[0]["NormalSales"] == "YES")
                    {
                        optionmethod.SelectedIndex = 0;
                        lblVATAdd.Enabled = true;
                    }


                    drpIntTrans.Enabled = false;
                    ddDeliveryNote.Enabled = false;
                    drpPurchaseReturn.Enabled = false;
                    drpmanualsales.Enabled = false;
                    drpnormalsales.Enabled = false;

                    //hdContact.Value = row.Cells[13].Text;
                    itemDs = formProduct(salesID);

                    if (cmbCustomer.SelectedItem.Value != "0")
                    {
                        if (drpPaymode.SelectedValue.Trim() == "3")
                        {
                            GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                            ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                        }
                    }
                    hdPrevMode.Value = drpPaymode.SelectedValue.Trim();
                    hdCustCreditLimit.Value = Convert.ToString(bl.GetCustomerCreditLimit(sDataSource, cmbCustomer.SelectedItem.Value));
                    pnlProduct.Visible = true;
                    Session["productDs"] = itemDs;


                    grvStudentDetails.DataSource = itemDs;
                    grvStudentDetails.DataBind();

                    for (int vLoop = 0; vLoop < grvStudentDetails.Rows.Count; vLoop++)
                    {
                        DropDownList drpProduct = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpPrd");
                        DropDownList drpIncharge = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpIncharge");
                        TextBox txtDesc = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDesc");
                        TextBox txtRate = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRate");
                        TextBox txtTotalPrice = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTot");
                        TextBox txtStock = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtStock");
                        TextBox txtQty = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtQty");
                        TextBox txtExeComm = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtExeComm");
                        TextBox txtDisPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDisPre");
                        TextBox txtVATPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATPre");
                        TextBox txtCSTPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtCSTPre");
                        TextBox txtVATAmt = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATAmt");
                        TextBox txtPrBefVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtPrBefVAT");
                        TextBox txtRtVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRtVAT");
                        TextBox txtTotal = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTotal");


                        if (itemDs.Tables[0].Rows[vLoop]["ItemCode"] != null)
                        {
                            sCustomer = Convert.ToString(itemDs.Tables[0].Rows[vLoop]["ItemCode"]);
                            drpProduct.ClearSelection();
                            ListItem li = drpProduct.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                            if (li != null) li.Selected = true;
                        }

                        if (itemDs.Tables[0].Rows[vLoop]["executivename"] != null)
                        {
                            sCustomer = Convert.ToString(itemDs.Tables[0].Rows[vLoop]["executivename"]);
                            drpIncharge.ClearSelection();
                            ListItem li = drpIncharge.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                            if (li != null) li.Selected = true;
                        }

                        // drpProduct.Text = itemDs.Tables[0].Rows[vLoop]["ItemCode"].ToString();
                        txtDesc.Text = itemDs.Tables[0].Rows[vLoop]["ProductDesc"].ToString();
                        txtRate.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["Rate"].ToString()).ToString("#0.00");
                        txtTotalPrice.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["TotalPrice"].ToString()).ToString("#0.00");
                        txtStock.Text = itemDs.Tables[0].Rows[vLoop]["Stock"].ToString();
                        txtQty.Text = itemDs.Tables[0].Rows[vLoop]["Qty"].ToString();
                        txtExeComm.Text = itemDs.Tables[0].Rows[vLoop]["ExecCharge"].ToString();
                        txtDisPre.Text = itemDs.Tables[0].Rows[vLoop]["Discount"].ToString();
                        txtVATPre.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["Vat"].ToString()).ToString("#0.00");
                        txtCSTPre.Text = itemDs.Tables[0].Rows[vLoop]["CST"].ToString();
                        txtPrBefVAT.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["PriceBeforeVATAmt"].ToString()).ToString("#0.00");
                        txtVATAmt.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["Vatamount"].ToString()).ToString("#0.00");
                        txtRtVAT.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["Subtotal"].ToString()).ToString("#0.00");
                        txtTotal.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["Totalmrp"].ToString()).ToString("#0.00");
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
                    //GrdViewItems.DataSource = itemDs;
                    //GrdViewItems.DataBind();
                    //calcSum();

                    errPanel.Visible = false;
                    ErrMsg.Text = "";

                    cmdUpdateProduct.Enabled = false;
                    cmdSaveProduct.Enabled = true;
                    //cmdCancelProduct.Visible = false;
                    cmdUpdateProduct.Visible = false;
                    //Label3.Visible = false;
                    cmdSaveProduct.Visible = true;
                    //Label2.Visible = true;
                    //cmdCancelProduct.Visible = false;

                    if (drpPaymode.SelectedValue.Trim() == "3")
                    {
                        divMultiPayment.Visible = true;
                        divAddMPayments.Visible = false;
                        divListMPayments.Visible = true;

                        GrdViewReceipt.DataSource = bl.ListReceiptsForBillNoOrder(lblBillNo.Text);
                        GrdViewReceipt.DataBind();
                    }
                    else if (drpPaymode.SelectedValue.Trim() == "4")
                    {
                        drpPaymode.Enabled = true;
                        txtCashAmount.Text = "";
                        ddBank1.SelectedValue = "0";
                        txtAmount1.Text = "";
                        txtCCard1.Text = "";
                        ddBank2.SelectedValue = "0";
                        txtAmount2.Text = "";
                        txtCCard2.Text = "";
                        ddBank3.SelectedValue = "0";
                        txtAmount3.Text = "";
                        txtCCard3.Text = "";
                        //ddBank1.SelectedItem.Text = "Select Bank";
                        //ddBank2.SelectedItem.Text = "Select Bank";
                        //ddBank3.SelectedItem.Text = "Select Bank";

                        divMultiPayment.Visible = true;
                        divAddMPayments.Visible = true;
                        divListMPayments.Visible = true;
                        //GrdViewReceipt.DataSource = bl.ListReceiptsForBillNoOrder(lblBillNo.Text);
                        //GrdViewReceipt.DataBind();

                        GrdViewReceipt.Visible = false;

                        DataSet receiptData = new DataSet();
                        receiptData = bl.ListReceiptsForBillNoOrder(lblBillNo.Text);
                        string sDebitor = string.Empty;

                        int gg = 1;
                        if (receiptData.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in receiptData.Tables[0].Rows)
                            {
                                if (dr["Paymode"].ToString() == "Cash")
                                {
                                    txtCashAmount.Text = dr["Amount"].ToString();
                                    TextBox5.Text = dr["SFRefNo"].ToString();
                                }
                                else if (dr["Paymode"].ToString() == "Cheque")
                                {
                                    if (gg == 1)
                                    {
                                        sDebitor = Convert.ToString(dr["DebtorId"]);
                                        ddBank1.ClearSelection();
                                        ListItem lli = ddBank1.Items.FindByValue(sDebitor);
                                        if (lli != null) lli.Selected = true;

                                        txtRefNo1.Text = dr["SFRefNo"].ToString();
                                        txtAmount1.Text = dr["Amount"].ToString();
                                        txtCCard1.Text = dr["ChequeNo"].ToString();
                                    }
                                    if (gg == 2)
                                    {

                                        sDebitor = Convert.ToString(dr["DebtorId"]);
                                        ddBank2.ClearSelection();
                                        ListItem gli = ddBank2.Items.FindByValue(sDebitor);
                                        if (gli != null) gli.Selected = true;

                                        txtAmount2.Text = dr["Amount"].ToString();
                                        txtCCard2.Text = dr["ChequeNo"].ToString();
                                        txtRefNo2.Text = dr["SFRefNo"].ToString();
                                    }
                                    if (gg == 3)
                                    {

                                        sDebitor = Convert.ToString(dr["DebtorId"]);
                                        ddBank3.ClearSelection();
                                        ListItem wli = ddBank3.Items.FindByValue(sDebitor);
                                        if (wli != null) wli.Selected = true;

                                        txtAmount3.Text = dr["Amount"].ToString();
                                        txtCCard3.Text = dr["ChequeNo"].ToString();
                                        txtRefNo3.Text = dr["SFRefNo"].ToString();
                                    }
                                    gg = gg + 1;
                                }

                            }

                            var total = 0.0;

                            if (txtAmount1.Text != "")
                                total += double.Parse(txtAmount1.Text);
                            if (txtAmount2.Text != "")
                                total += double.Parse(txtAmount2.Text);
                            if (txtAmount3.Text != "")
                                total += double.Parse(txtAmount3.Text);
                            if (txtCashAmount.Text != "")
                                total += double.Parse(txtCashAmount.Text);

                            lblReceivedTotal.Text = total.ToString();

                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        divMultiPayment.Visible = false;
                        divAddMPayments.Visible = false;
                        divListMPayments.Visible = false;
                    }

                    hdPrevSalesTotal.Value = lblNet.Text;

                    if (bl.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                    {
                        cmdSaveProduct.Enabled = false;
                        if (GrdViewItems.Columns[14] != null)
                            GrdViewItems.Columns[14].Visible = false;
                        if (GrdViewItems.Columns[15] != null)
                            GrdViewItems.Columns[15].Visible = false;
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
            ModalPopupMethod.Show();
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
        string strEmpName = string.Empty;
        Session["roleDs"] = null;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetSalesItemsForId(salesID);


        if (ds != null)
        {
            dt = new DataTable();

            dc = new DataColumn("ItemCode");
            dt.Columns.Add(dc);

            dc = new DataColumn("executivename");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductDesc");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rate");
            dt.Columns.Add(dc);

            dc = new DataColumn("TotalPrice");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("ExecCharge");
            dt.Columns.Add(dc);

            dc = new DataColumn("Discount");
            dt.Columns.Add(dc);

            dc = new DataColumn("Vat");
            dt.Columns.Add(dc);

            dc = new DataColumn("CST");
            dt.Columns.Add(dc);

            dc = new DataColumn("PriceBeforeVATAmt");
            dt.Columns.Add(dc);


            dc = new DataColumn("Vatamount");
            dt.Columns.Add(dc);

            dc = new DataColumn("Subtotal");
            dt.Columns.Add(dc);

            dc = new DataColumn("Totalmrp");
            dt.Columns.Add(dc);

            dc = new DataColumn("Productname");
            dt.Columns.Add(dc);

            dc = new DataColumn("Measure_Unit");
            dt.Columns.Add(dc);

            dc = new DataColumn("RoleID");
            dt.Columns.Add(dc);

            dc = new DataColumn("IsRole");
            dt.Columns.Add(dc);

            dc = new DataColumn("Bundles");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rods");
            dt.Columns.Add(dc);

            dc = new DataColumn("billno");
            dt.Columns.Add(dc);

            dc = new DataColumn("Stock");
            dt.Columns.Add(dc);

            dc = new DataColumn("RowNumber");
            dt.Columns.Add(dc);

            itemDs.Tables.Add(dt);
            ViewState["CurrentTable"] = dt;
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
                        dr["ItemCode"] = strItemCode;
                    }
                    if (dR["executivename"] != null)
                    {
                        strEmpName = Convert.ToString(dR["executivename"]);
                        dr["executivename"] = strEmpName;
                    }
                    if (dR["billno"] != null)
                    {
                        billno = Convert.ToInt32(dR["billno"]);
                        dr["billno"] = Convert.ToString(billno);
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

                    if (dR["TotalPrice"] != null)
                        dr["TotalPrice"] = Convert.ToString(dR["TotalPrice"]);

                    if (dR["Discount"] != null)
                        dr["Discount"] = Convert.ToString(dR["Discount"]);

                    if (dR["Measure_Unit"] != null)
                        dr["Measure_Unit"] = Convert.ToString(dR["Measure_Unit"]);

                    if (dR["Vat"] != null)
                        dr["Vat"] = Convert.ToString(dR["Vat"]);

                    if (dR["PriceBeforeVATAmt"] != null)
                        dr["PriceBeforeVATAmt"] = Convert.ToDouble(dR["PriceBeforeVATAmt"]);


                    if (dR["Totalmrp"] != null)
                        dr["Totalmrp"] = Convert.ToDouble(dR["Totalmrp"]);

                    //if (dR["SubTotal"] != null)
                    //    dr["SubTotal"] = Convert.ToString(dR["SubTotal"]);

                    if (dR["Vatamount"] != null)
                        dr["Vatamount"] = Convert.ToString(dR["Vatamount"]);

                    // krishnavelu 26 June
                    if (dR["ExecCharge"] != null)
                    {
                        if (dR["ExecCharge"].ToString() != string.Empty)
                            dr["ExecCharge"] = Convert.ToString(dR["ExecCharge"]);
                        else
                            dr["ExecCharge"] = "0";
                    }
                    else
                        dr["ExecCharge"] = "0";

                    if (dR["CST"] != null)
                        dr["CST"] = Convert.ToString(dR["CST"]);

                    if (dR["Bundles"] != null)
                        dr["Bundles"] = Convert.ToString(dR["Bundles"]);
                    if (dR["Rods"] != null)
                        dr["Rods"] = Convert.ToString(dR["Rods"]);
                    if (dR["SlNo"] != null)
                        dr["RowNumber"] = Convert.ToString(dR["SlNo"]);
                    if (dR["IsRole"] != null)
                    {
                        roleFlag = Convert.ToString(dR["IsRole"]);
                        dr["IsRole"] = roleFlag;

                    }

                    if (dR["Stock"] != null)
                        dr["Stock"] = Convert.ToInt32(dR["Stock"]);

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
                    dr["RoleID"] = strRole;
                    dr["Subtotal"] = Convert.ToDouble(dR["Totalmrp"]);// Convert.ToString(dTotal);
                    itemDs.Tables[0].Rows.Add(dr);
                    strRole = "";
                }
            }
        }
        return itemDs;


    }

    public DataSet formProductPR(int salesID)
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
        string strEmpName = string.Empty;
        Session["roleDs"] = null;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        //ds = bl.GetSalesItemsForId(salesID);
        ds = bl.GetPurchaseItemsForId(salesID);

        if (ds != null)
        {
            dt = new DataTable();

            dc = new DataColumn("ItemCode");
            dt.Columns.Add(dc);

            //dc = new DataColumn("executivename");
            //dt.Columns.Add(dc);

            dc = new DataColumn("ProductDesc");
            dt.Columns.Add(dc);

            dc = new DataColumn("PurchaseRate");
            dt.Columns.Add(dc);

            //dc = new DataColumn("TotalPrice");
            //dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            //dc = new DataColumn("ExecCharge");
            //dt.Columns.Add(dc);

            dc = new DataColumn("Discount");
            dt.Columns.Add(dc);

            dc = new DataColumn("Vat");
            dt.Columns.Add(dc);

            dc = new DataColumn("CST");
            dt.Columns.Add(dc);

            //dc = new DataColumn("PriceBeforeVATAmt");
            //dt.Columns.Add(dc);


            dc = new DataColumn("Vatamount");
            dt.Columns.Add(dc);

            //dc = new DataColumn("Subtotal");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("Totalmrp");
            //dt.Columns.Add(dc);

            dc = new DataColumn("Productname");
            dt.Columns.Add(dc);

            dc = new DataColumn("Measure_Unit");
            dt.Columns.Add(dc);

            dc = new DataColumn("RoleID");
            dt.Columns.Add(dc);

            dc = new DataColumn("IsRole");
            dt.Columns.Add(dc);

            //dc = new DataColumn("Bundles");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("Rods");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("billno");
            //dt.Columns.Add(dc);

            dc = new DataColumn("Model");
            dt.Columns.Add(dc);

            dc = new DataColumn("RowNumber");
            dt.Columns.Add(dc);

            itemDs.Tables.Add(dt);
            ViewState["CurrentTable"] = dt;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    dr = itemDs.Tables[0].NewRow();

                    if (dR["Qty"] != null)
                        dQty = Convert.ToDouble(dR["Qty"]);
                    if (dR["PurchaseRate"] != null)
                        dRate = Convert.ToDouble(dR["PurchaseRate"]);

                    dTotal = dQty * dRate;
                    if (dR["ItemCode"] != null)
                    {
                        strItemCode = Convert.ToString(dR["ItemCode"]);
                        dr["ItemCode"] = strItemCode;
                    }
                    //if (dR["executivename"] != null)
                    //{
                    //    strEmpName = Convert.ToString(dR["executivename"]);
                    //    dr["executivename"] = strEmpName;
                    //}
                    //if (dR["billno"] != null)
                    //{
                    //    billno = Convert.ToInt32(dR["billno"]);
                    //    dr["billno"] = Convert.ToString(billno);
                    //}
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
                    dr["PurchaseRate"] = dRate.ToString();

                    //if (dR["TotalPrice"] != null)
                    //    dr["TotalPrice"] = Convert.ToString(dR["TotalPrice"]);

                    if (dR["Discount"] != null)
                        dr["Discount"] = Convert.ToString(dR["Discount"]);

                    if (dR["Measure_Unit"] != null)
                        dr["Measure_Unit"] = Convert.ToString(dR["Measure_Unit"]);

                    if (dR["Vat"] != null)
                        dr["Vat"] = Convert.ToString(dR["Vat"]);

                    //if (dR["PriceBeforeVATAmt"] != null)
                    //    dr["PriceBeforeVATAmt"] = Convert.ToDouble(dR["PriceBeforeVATAmt"]);


                    //if (dR["Totalmrp"] != null)
                    //    dr["Totalmrp"] = Convert.ToDouble(dR["Totalmrp"]);

                    //if (dR["SubTotal"] != null)
                    //    dr["SubTotal"] = Convert.ToString(dR["SubTotal"]);

                    if (dR["discamt"] != null)
                        dr["Vatamount"] = Convert.ToString(dR["discamt"]);

                    // krishnavelu 26 June
                    //if (dR["ExecCharge"] != null)
                    //{
                    //    if (dR["ExecCharge"].ToString() != string.Empty)
                    //        dr["ExecCharge"] = Convert.ToString(dR["ExecCharge"]);
                    //    else
                    //        dr["ExecCharge"] = "0";
                    //}
                    //else
                    //    dr["ExecCharge"] = "0";

                    if (dR["CST"] != null)
                        dr["CST"] = Convert.ToString(dR["CST"]);

                    //if (dR["Bundles"] != null)
                    //    dr["Bundles"] = Convert.ToString(dR["Bundles"]);
                    //if (dR["Rods"] != null)
                    //    dr["Rods"] = Convert.ToString(dR["Rods"]);
                    //if (dR["SlNo"] != null)
                    //    dr["RowNumber"] = Convert.ToString(dR["SlNo"]);
                    if (dR["IsRole"] != null)
                    {
                        roleFlag = Convert.ToString(dR["IsRole"]);
                        dr["IsRole"] = roleFlag;

                    }

                    //if (dR["Model"] != null)
                    //    dr["Model"] = Convert.ToInt32(dR["Model"]);

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
                    dr["RoleID"] = strRole;
                    // dr["Subtotal"] = Convert.ToDouble(dR["Totalmrp"]);// Convert.ToString(dTotal);
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


    protected void ViewProduct_Click(object sender, EventArgs e)
    {
        ModalPopupMethod.Hide();
        ModalPopupSales.Hide();
        //ModalPopupProductselect.Show();
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
                        sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDouble(dr["Qty"]), Convert.ToDouble(dr["Rate"]), Convert.ToDouble(dr["Discount"]), Convert.ToDouble(dr["VAT"]), Convert.ToDouble(dr["CST"]), Convert.ToDouble(dr["Totalmrp"])));
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
        hdPrevSalesTotal.Value = lblNet.Text;
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string textt = string.Empty;
            string dropd = string.Empty;
            //int BillNo = 0;

            //int TransNo = 0;

            //if (!string.IsNullOrEmpty(txtBillnoSrc.Text))
            //    BillNo = Convert.ToInt32(txtBillnoSrc.Text);

            //if (!string.IsNullOrEmpty(txtTransNo.Text))
            //    TransNo = Convert.ToInt32(txtTransNo.Text);

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;

            BindGrid(textt, dropd);
            //Accordion1.SelectedIndex = 0;
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();

            lblTotalSum.Text = "0";
            lblTotalDis.Text = "0";
            lblTotalVAT.Text = "0";
            lblTotalCST.Text = "0";
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

            //GrdViewReceipt.DataBind();
            //GrdViewItems.DataSource = null;
            //GrdViewItems.DataBind();

            //lblTotalSum.Text = "0";
            //lblTotalDis.Text = "0";
            //lblTotalVAT.Text = "0";
            //lblTotalCST.Text = "0";
            //lblFreight.Text = "0";
            //lblNet.Text = "0";

            //Reset();

            //ResetProduct();

            //cmbProdAdd.Enabled = true;
            //cmdUpdateProduct.Enabled = false;
            //cmdSaveProduct.Enabled = true;

            //Session["productDs"] = null;

            //cmdSave.Enabled = true;
            //cmdDelete.Enabled = false;
            //cmdUpdate.Enabled = false;
            //cmdPrint.Enabled = false;
            //errPanel.Visible = false;
            //ErrMsg.Text = "";
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
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewSales_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                //GrdViewSales.SelectedIndex = e.RowIndex;

                //int sBillNo = 0;

                string connection = Request.Cookies["Company"].Value;
                string recondate = string.Empty;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                //if (GrdViewSales.SelectedDataKey != null)
                //{
                int sBillNo = Convert.ToInt32(GrdViewSales.DataKeys[e.RowIndex].Value.ToString());
                //int sBillNo = Convert.ToInt32(GrdViewSales.SelectedDataKey.Value);
                //}

                string UserID = Page.User.Identity.Name;

                string usernam = Request.Cookies["LoggedUserName"].Value;

                var salesData = bl.GetSalesForId(sBillNo);

                if (salesData != null && salesData.Tables[0].Rows.Count > 0)
                {

                    recondate = salesData.Tables[0].Rows[0]["BillDate"].ToString();

                    if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid. Please contact Administrator.')", true);
                        return;
                    }

                    if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
                    {
                        var receivedBill = bl.IsAmountPaidForBill(sBillNo.ToString());

                        if (receivedBill != string.Empty)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                            return;
                        }
                    }
                }

                //GridSource.DeleteMethod = "DeleteSalesNew";
                //GridSource.DeleteParameters.Add("connection", TypeCode.String, GetConnectionString());
                //GridSource.DeleteParameters.Add("BillNo", TypeCode.Int32, sBillNo.ToString());
                //GridSource.DeleteParameters.Add("UserID", TypeCode.String, UserID);
                //GridSource.Delete();

                //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

                bl.DeleteSalesNew(connection, sBillNo, UserID);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Details Deleted Successfully. Bill No. was " + sBillNo.ToString() + "')", true);
                BindGrid("", "");

                string salestype = string.Empty;
                int ScreenNo = 0;
                string ScreenName = string.Empty;

               

                double Amount = 0;
                string InternalTransfer = string.Empty;
                string Billno = string.Empty;
                string deliveryNote = string.Empty;
                string purchaseReturn = string.Empty;
                string NormalSales = string.Empty;
                string PayTo = string.Empty;
                string ManualSales = string.Empty;
                int DebitorID = 0;
                string CustomerName = string.Empty;

                DataSet ds = bl.GetSalesForId(sBillNo, 0);

                DataSet dss = bl.GetSalesItemsForId(sBillNo);

                if (ds != null)
                {
                    Billno = Convert.ToString(ds.Tables[0].Rows[0]["Billno"].ToString());
                    NormalSales = Convert.ToString(ds.Tables[0].Rows[0]["NormalSales"]);
                    ManualSales = Convert.ToString(ds.Tables[0].Rows[0]["ManualSales"]);
                    InternalTransfer = Convert.ToString(ds.Tables[0].Rows[0]["InternalTransfer"]);
                    deliveryNote = Convert.ToString(ds.Tables[0].Rows[0]["deliveryNote"]);
                    purchaseReturn = Convert.ToString(ds.Tables[0].Rows[0]["purchaseReturn"]);
                    PayTo = ds.Tables[0].Rows[0]["paymode"].ToString();
                    Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"]);
                    DebitorID = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerID"]);
                    CustomerName = Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                    if (purchaseReturn == "YES")
                    {
                        salestype = "Purchase Return";
                        ScreenName = "Purchase Return";
                    }
                    else if (InternalTransfer == "YES")
                    {
                        salestype = "Internal Transfer";
                        ScreenName = "Internal Transfer Sales";
                    }
                    else if (deliveryNote == "YES")
                    {
                        salestype = "Delivery Note";
                        ScreenName = "Delivery Note Sales";
                    }
                    else if (ManualSales == "YES")
                    {
                        salestype = "Manual Sales";
                        ScreenName = "Manual Sales";
                    }
                    else if (NormalSales == "YES")
                    {
                        salestype = "Normal Sales";
                        ScreenName = "Normal Sales";
                    }

                }


                bool mobile = false;
                bool Email = false;
                string emailsubject = string.Empty;

                string emailcontent = string.Empty;
                if (hdEmailRequired.Value == "YES")
                {
                    DataSet dsd = bl.GetLedgerInfoForId(connection, DebitorID);
                    var toAddress = "";
                    var toAdd = "";
                    Int32 ModeofContact = 0;
                    int ScreenType = 0;

                    if (dsd != null)
                    {
                        if (dsd.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsd.Tables[0].Rows)
                            {
                                toAdd = dr["EmailId"].ToString();
                                ModeofContact = Convert.ToInt32(dr["ModeofContact"]);
                            }
                        }
                    }


                    DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                    if (dsdd != null)
                    {
                        if (dsdd.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsdd.Tables[0].Rows)
                            {
                                ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                mobile = Convert.ToBoolean(dr["mobile"]);
                                Email = Convert.ToBoolean(dr["Email"]);
                                emailsubject = Convert.ToString(dr["emailsubject"]);
                                emailcontent = Convert.ToString(dr["emailcontent"]);

                                if (ScreenType == 1)
                                {
                                    if (dr["Name1"].ToString() == "Sales Executive")
                                    {
                                        toAddress = toAdd;
                                    }
                                    else if (dr["Name1"].ToString() == "Customer")
                                    {
                                        if (ModeofContact == 2)
                                        {
                                            toAddress = toAdd;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        toAddress = toAdd;
                                    }
                                }
                                else
                                {
                                    toAddress = dr["EmailId"].ToString();
                                }
                                if (Email == true)
                                {
                                   
                                    string body = "\n";

                                    int index123 = emailcontent.IndexOf("@Branch");
                                    body = Request.Cookies["Company"].Value;
                                    if (index123 >= 0)
                                    {
                                        emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                    }

                                    int sno = 1;
                                    string prd = string.Empty;
                                    int index322 = emailcontent.IndexOf("@Product");
                                    if (dss != null)
                                    {
                                        if (dss.Tables[0].Rows.Count > 0)
                                        {
                                            //emailcontent = emailcontent.Remove(index322, 8).Insert(index322, body);

                                            foreach (DataRow drd in dss.Tables[0].Rows)
                                            {

                                                //body = drd["PrdName"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["Rate"].ToString();
                                                prd = prd + "\n";
                                                prd = prd + drd["Itemcode"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["Rate"].ToString();
                                                prd = prd + "\n";
                                            }
                                            if (index322 >= 0)
                                            {
                                                emailcontent = emailcontent.Remove(index322, 8).Insert(index322, prd);
                                            }
                                        }
                                    }

                                    int index312 = emailcontent.IndexOf("@User");
                                    body = usernam;
                                    if (index312 >= 0)
                                    {
                                        emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                    }

                                    int index2 = emailcontent.IndexOf("@Date");
                                    body = txtBillDate.Text;
                                    if (index2 >= 0)
                                    {
                                        emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                    }

                                    int index = emailcontent.IndexOf("@Customer");
                                    body = CustomerName;
                                    if (index >= 0)
                                    {
                                        emailcontent = emailcontent.Remove(index, 9).Insert(index, body);
                                    }

                                    int index1 = emailcontent.IndexOf("@Amount");
                                    body = txttotal.Text;
                                    if (index1 >= 0)
                                    {
                                        emailcontent = emailcontent.Remove(index1, 7).Insert(index1, body);
                                    }

                                    string smtphostname = ConfigurationManager.AppSettings["SmtpHostName"].ToString();
                                    int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPortNumber"]);
                                    var fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

                                    string fromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();

                                    EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, emailcontent, fromPassword);

                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email sent successfully')", true);

                                }

                            }
                        }
                    }
                }

                string conn = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                UtilitySMS utilSMS = new UtilitySMS(conn);
                

                string smscontent = string.Empty;
                if (hdSMSRequired.Value == "YES")
                {
                    DataSet dsd = bl.GetLedgerInfoForId(connection, DebitorID);
                    var toAddress = "";
                    var toAdd = "";
                    Int32 ModeofContact = 0;
                    int ScreenType = 0;

                    if (dsd != null)
                    {
                        if (dsd.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsd.Tables[0].Rows)
                            {
                                toAdd = dr["Mobile"].ToString();
                                ModeofContact = Convert.ToInt32(dr["ModeofContact"]);
                            }
                        }
                    }


                    DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                    if (dsdd != null)
                    {
                        if (dsdd.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsdd.Tables[0].Rows)
                            {
                                ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                mobile = Convert.ToBoolean(dr["mobile"]);
                                smscontent = Convert.ToString(dr["smscontent"]);

                                if (ScreenType == 1)
                                {
                                    if (dr["Name1"].ToString() == "Sales Executive")
                                    {
                                        toAddress = toAdd;
                                    }
                                    else if (dr["Name1"].ToString() == "Customer")
                                    {
                                        if (ModeofContact == 1)
                                        {
                                            toAddress = toAdd;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        toAddress = toAdd;
                                    }
                                }
                                else
                                {
                                    toAddress = dr["mobile"].ToString();
                                }
                                if (mobile == true)
                                {

                                    string body = "\n";

                                    int index123 = smscontent.IndexOf("@Branch");
                                    body = Request.Cookies["Company"].Value;
                                    if (index123 >= 0)
                                    {
                                        smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                    }




                                    int index312 = smscontent.IndexOf("@User");
                                    body = usernam;
                                    if (index312 >= 0)
                                    {
                                        smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                    }

                                    int index2 = smscontent.IndexOf("@Date");
                                    body = txtBillDate.Text;
                                    if (index2 >= 0)
                                    {
                                        smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                    }

                                    int index = smscontent.IndexOf("@Customer");
                                    body = CustomerName;
                                    if (index >= 0)
                                    {
                                        smscontent = smscontent.Remove(index, 9).Insert(index, body);
                                    }

                                    int index1 = smscontent.IndexOf("@Amount");
                                    body = txttotal.Text;
                                    if (index1 >= 0)
                                    {
                                        smscontent = smscontent.Remove(index1, 7).Insert(index1, body);
                                    }

                                    int i = dss.Tables[0].Rows.Count;

                                    int index322 = smscontent.IndexOf("@Product");
                                    string smsTEXT = string.Empty;

                                    foreach (DataRow drd in dss.Tables[0].Rows)
                                    {
                                        smsTEXT = smsTEXT + drd["PrdName"].ToString() + " " + drd["Qty"].ToString() + " Qty @ " + GetCurrencyType() + "." + GetProductTotalExVAT(double.Parse(drd["Qty"].ToString()), double.Parse(drd["Rate"].ToString()), double.Parse(drd["Discount"].ToString()));
                                        i = i - 1;

                                        if (i != 0)
                                            smsTEXT = smsTEXT + ", ";
                                    }

                                    smsTEXT = smsTEXT + ". Total Bill Amount is " + GetCurrencyType() + "." + lblNet.Text;
                                    smsTEXT = smsTEXT + " . The Bill No. is " + Billno.ToString();

                                    smscontent = smscontent.Remove(index322, 8).Insert(index322, smsTEXT);

                                    if (Session["Provider"] != null)
                                    {
                                        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), toAddress, smscontent, true, UserID);
                                    }


                                }

                            }
                        }
                    }
                }


                Session["ProductDs"] = null;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private string GetConnectionString()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        return connStr;
    }

    //private void setDeleteParameters(ObjectDataSourceMethodEventArgs e)
    //{
    //    if (GrdViewSales.SelectedDataKey != null)
    //        e.InputParameters["BillNo"] = GrdViewSales.SelectedDataKey.Value;

    //    e.InputParameters["UserID"] = Request.Cookies["LoggedUserName"].Value;
    //}

    //protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    //{


    //    this.setDeleteParameters(e);
    //}

    protected void LoadProducts(object sender, EventArgs e)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        string CategoryID = cmbCategory.SelectedValue;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        string method = string.Empty;

        if (Session["Method"] == "Add")
        {
            method = "Add";
        }
        else if (Session["Method"] == "Edit")
        {
            method = "Edit";
        }

        ds = bl.ListProductsForCategoryIDOnlyStock(CategoryID, method);
        cmbProdAdd.Items.Clear();
        cmbProdAdd.DataSource = ds;
        cmbProdAdd.Items.Insert(0, new ListItem("Select ItemCode", "0"));
        cmbProdAdd.DataTextField = "ItemCode";
        cmbProdAdd.DataValueField = "ItemCode";
        cmbProdAdd.DataBind();

        ds = bl.ListModelsForCategoryIDOnlyStock(CategoryID, method);
        cmbModel.Items.Clear();
        cmbModel.DataSource = ds;
        cmbModel.Items.Insert(0, new ListItem("Select Model", "0"));
        cmbModel.DataTextField = "Model";
        cmbModel.DataValueField = "Model";
        cmbModel.DataBind();

        ds = bl.ListBrandsForCategoryIDOnlyStock(CategoryID, method);
        cmbBrand.Items.Clear();
        cmbBrand.DataSource = ds;
        cmbBrand.Items.Insert(0, new ListItem("Select Brand", "0"));
        cmbBrand.DataTextField = "ProductDesc";
        cmbBrand.DataValueField = "ProductDesc";
        cmbBrand.DataBind();

        ds = bl.ListProdNameForCategoryIDOnlyStock(CategoryID, method);
        cmbProdName.Items.Clear();
        cmbProdName.DataSource = ds;
        cmbProdName.Items.Insert(0, new ListItem("Select ItemName", "0"));
        cmbProdName.DataTextField = "ProductName";
        cmbProdName.DataValueField = "ProductName";
        cmbProdName.DataBind();

        LoadForProduct(this, null);
    }

    protected void LoadAllProducts(object sender, EventArgs e)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        string CategoryID = cmbCategory.SelectedValue;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string method = string.Empty;

        if (Session["Method"] == "Add")
        {
            method = "Add";
        }
        else if (Session["Method"] == "Edit")
        {
            method = "Edit";
        }
        ds = bl.ListProductsForCategoryID(CategoryID, method);
        cmbProdAdd.Items.Clear();
        cmbProdAdd.DataSource = ds;
        cmbProdAdd.Items.Insert(0, new ListItem("Select ItemCode", "0"));
        cmbProdAdd.DataTextField = "ItemCode";
        cmbProdAdd.DataValueField = "ItemCode";
        cmbProdAdd.DataBind();

        ds = bl.ListModelsForCategoryID(CategoryID, method);
        cmbModel.Items.Clear();
        cmbModel.DataSource = ds;
        cmbModel.Items.Insert(0, new ListItem("Select Model", "0"));
        cmbModel.DataTextField = "Model";
        cmbModel.DataValueField = "Model";
        cmbModel.DataBind();

        ds = bl.ListBrandsForCategoryID(CategoryID, method);
        cmbBrand.Items.Clear();
        cmbBrand.DataSource = ds;
        cmbBrand.Items.Insert(0, new ListItem("Select Brand", "0"));
        cmbBrand.DataTextField = "ProductDesc";
        cmbBrand.DataValueField = "ProductDesc";
        cmbBrand.DataBind();

        ds = bl.ListProdNameForCategoryID(CategoryID, method);
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
        string method = string.Empty;

        if (Session["Method"] == "Add")
        {
            method = "Add";
        }
        else if (Session["Method"] == "Edit")
        {
            method = "Edit";
        }

        ds = bl.ListModelsForBrandOnlyStock(brand, CategoryID, method);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbModel.Items.Clear();
            cmbModel.DataSource = ds;
            cmbModel.DataTextField = "Model";
            cmbModel.DataValueField = "Model";
            cmbModel.DataBind();
        }

        ds = bl.ListProdcutsForBrandOnlyStock(brand, CategoryID, method);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbProdAdd.Items.Clear();
            cmbProdAdd.DataSource = ds;
            cmbProdAdd.DataTextField = "ItemCode";
            cmbProdAdd.DataValueField = "ItemCode";
            cmbProdAdd.DataBind();
        }

        ds = bl.ListProdcutNameForBrandOnlyStock(brand, CategoryID, method);
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
        string method = string.Empty;

        if (Session["Method"] == "Add")
        {
            method = "Add";
        }
        else if (Session["Method"] == "Edit")
        {
            method = "Edit";
        }

        ds = bl.ListProdcutsForModelOnlyStock(model, CategoryID, method);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbProdAdd.Items.Clear();
            cmbProdAdd.DataSource = ds;
            cmbProdAdd.DataTextField = "ItemCode";
            cmbProdAdd.DataValueField = "ItemCode";
            cmbProdAdd.DataBind();
        }

        ds = bl.ListBrandsForModelOnlyStock(model, CategoryID, method);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbBrand.Items.Clear();
            cmbBrand.DataSource = ds;
            cmbBrand.DataTextField = "ProductDesc";
            cmbBrand.DataValueField = "ProductDesc";
            cmbBrand.DataBind();
        }

        ds = bl.ListProductNameForModelOnlyStock(model, CategoryID, method);
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
        string method = string.Empty;

        if (Session["Method"] == "Add")
        {
            method = "Add";
        }
        else if (Session["Method"] == "Edit")
        {
            method = "Edit";
        }
        ds = bl.ListProdcutsForProductNameOnlyStock(prodName, CategoryID, method);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbProdAdd.Items.Clear();
            cmbProdAdd.DataSource = ds;
            cmbProdAdd.DataTextField = "ItemCode";
            cmbProdAdd.DataValueField = "ItemCode";
            cmbProdAdd.DataBind();
        }

        ds = bl.ListBrandsForProductNameOnlyStock(prodName, CategoryID, method);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            cmbBrand.Items.Clear();
            cmbBrand.DataSource = ds;
            cmbBrand.DataTextField = "ProductDesc";
            cmbBrand.DataValueField = "ProductDesc";
            cmbBrand.DataBind();
        }

        ds = bl.ListModelsForProductNameOnlyStock(prodName, CategoryID, method);
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
            string connection = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;
            else
                Response.Redirect("Login.aspx");

            string recondate = string.Empty;
            BusinessLogic bl = new BusinessLogic();

            recondate = txtBillDate.Text.Trim();

            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                return;
            }

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
        //TextBox txt = (TextBox)sender;

        //if(txt.Text != "")
        //{
        //    var total = double.Parse( lblReceivedTotal.Text);

        //    total = total + double.Parse(txt.Text);
        //    lblReceivedTotal.Text = total.ToString();
        //}

        try
        {
            var total = 0.0;

            if (txtAmount1.Text != "")
                total += double.Parse(txtAmount1.Text);
            if (txtAmount2.Text != "")
                total += double.Parse(txtAmount2.Text);
            if (txtAmount3.Text != "")
                total += double.Parse(txtAmount3.Text);
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

        dc = new DataColumn("Totalmrp");
        dt.Columns.Add(dc);

        dc = new DataColumn("Rods");
        dt.Columns.Add(dc);

        dc = new DataColumn("VatAmount");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        drNew = dt.NewRow();

        string disType = GetDiscType();

        drNew["itemCode"] = string.Empty;
        drNew["Billno"] = "";
        drNew["ProductName"] = string.Empty;
        drNew["ProductDesc"] = string.Empty;
        string textvalue = null;
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
        drNew["Totalmrp"] = Convert.ToDouble(textvalue);
        drNew["Rods"] = "";
        drNew["VatAmount"] = Convert.ToDouble(textvalue);

        ds.Tables[0].Rows.Add(drNew);

        ds.Tables[0].AcceptChanges();

        GrdViewItems.Columns[13].Visible = false;
        GrdViewItems.Columns[14].Visible = false;

        GrdViewItems.DataSource = ds;
        GrdViewItems.DataBind();

        GrdViewItems.Rows[0].Cells[4].Text = null;
        GrdViewItems.Rows[0].Cells[3].Text = null;
        GrdViewItems.Rows[0].Cells[5].Text = null;
        GrdViewItems.Rows[0].Cells[7].Text = null;
        GrdViewItems.Rows[0].Cells[9].Text = null;
        GrdViewItems.Rows[0].Cells[10].Text = null;
        GrdViewItems.Rows[0].Cells[11].Text = null;
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
        //LoadForTotal();

    }

    bool checkflag = false;

    private void AddNewRow()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        for (int vLoop = 0; vLoop < grvStudentDetails.Rows.Count; vLoop++)
        {
            DropDownList drpProduct = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpPrd");
            TextBox txtQty = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtQty");
            DropDownList drpIncharge = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpIncharge");
            TextBox txtDesc = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDesc");
            TextBox txtRate = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRate");
            TextBox txtTot = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTot");
            TextBox txtStock = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtStock");
            TextBox txtExeComm = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtExeComm");
            TextBox txtDisPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDisPre");
            TextBox txtVATPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATPre");
            TextBox txtCSTPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtCSTPre");
            TextBox txtVATAmt = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATAmt");
            TextBox txtPrBefVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtPrBefVAT");
            TextBox txtRtVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRtVAT");
            TextBox txtTotal = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTotal");

            int col = vLoop + 1;

            if (drpProduct.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Product in row " + col + " ')", true);
                drpProduct.Focus();
                checkflag = true;
                return;
            }
            else if (txtDesc.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Description in row " + col + " ')", true);
                txtDesc.Focus();
                checkflag = true;
                return;
            }
            if (drpIncharge.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Employee in row " + col + " ')", true);
                drpIncharge.Focus();
                checkflag = true;
                return;
            }
            else if (txtRate.Text == "" || txtRate.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Rate in row " + col + " ')", true);
                txtRate.Focus();
                checkflag = true;
                return;
            }
            else if (txtTot.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Price is empty in row " + col + " ')", true);
                txtStock.Focus();
                checkflag = true;
                return;
            }
            else if (txtStock.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock is empty in row " + col + " ')", true);
                txtStock.Focus();
                checkflag = true;
                return;
            }
            else if (txtQty.Text == "" || txtQty.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Quantity in row " + col + " ')", true);
                txtQty.Focus();
                checkflag = true;
                return;
            }
            else if (Convert.ToInt32(txtQty.Text) > Convert.ToInt32(txtStock.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given qty is greater than stock in row " + col + " ')", true);
                txtQty.Focus();
                checkflag = true;
                return;
            }
            else if (txtExeComm.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Executive Commission in row " + col + " ')", true);
                checkflag = true;
                return;
            }
            else if (txtDisPre.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Discount Percentage in row " + col + " ')", true);
                checkflag = true;
                return;
            }
            //else if (txtVATPre.Text == "" || txtVATPre.Text == "0")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill VAT Percentage in row " + col + " ')", true);
            //    checkflag = true;
            //    return;
            //}
            else if (txtCSTPre.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill CST Percentage in row " + col + " ')", true);
                checkflag = true;
                return;
            }
            else if (txtPrBefVAT.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Price Before VAT Amount in row " + col + " ')", true);
                checkflag = true;
                return;
            }
            else if (txtVATAmt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill VAT Amount in row " + col + " ')", true);
                checkflag = true;
                return;
            }
            else if (txtRtVAT.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Rate with VAT in row " + col + " ')", true);
                checkflag = true;
                return;
            }
            else if (txtTotal.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Total in row " + col + " ')", true);
                checkflag = true;
                return;
            }


            if ((drpnormalsales.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
            {
                double EXCLUSIVErate1 = 0;
                double EXCrate1 = 0;
                double ratewithqty = 0;
                if (Labelll.Text == "VAT EXCLUSIVE")
                {
                    ratewithqty = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);
                    //EXCLUSIVErate1 = (Convert.ToDouble(txtRate.Text)) + ((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtVATAmt.Text) / 100));
                    EXCLUSIVErate1 = (Convert.ToDouble(ratewithqty)) + ((Convert.ToDouble(ratewithqty)) * (Convert.ToDouble(txtVATPre.Text) / 100));
                }
                else
                {
                    // EXCLUSIVErate1 = Convert.ToDouble(txtRate.Text);
                    ratewithqty = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);
                    EXCLUSIVErate1 = Convert.ToDouble(ratewithqty);
                }

                DataSet dst = bl.ListProductMRPPrices(drpProduct.SelectedItem.Value.Trim());
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drt in dst.Tables[0].Rows)
                        {
                            EXCrate1 = Convert.ToDouble(drt["rate"]);
                        }
                    }
                }
                if (EXCLUSIVErate1 > EXCrate1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be greater than " + EXCrate1 + "')", true);
                    checkflag = true;
                    return;
                }
            }


            string usernam = Request.Cookies["LoggedUserName"].Value;
            if (bl.CheckIfUserCanDoDeviation(usernam))
            {

            }
            else
            {
                if ((drpnormalsales.SelectedItem.Text == "YES") || (drpmanualsales.SelectedItem.Text == "YES"))
                {
                    double devvalue = 0;
                    // for product deviation
                    DataSet dsd = bl.getRateInformation(drpProduct.SelectedValue);
                    if (dsd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drt in dsd.Tables[0].Rows)
                        {
                            devvalue = Convert.ToDouble(drt["deviation"]);
                        }
                    }

                    if (devvalue == 0)
                    {
                        //for brand deviation
                        DataSet dsrate = bl.getRateInfo(drpProduct.SelectedValue);
                        double rate1 = 0;
                        double dp = 0;
                        double overall = 0;
                        double per = 0;
                        double rate2 = 0;

                        if (dsrate.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsrate.Tables[0].Rows)
                            {
                                dp = Convert.ToDouble(dr["dealerrate"]);
                                per = Convert.ToDouble(dr["deviation"]);
                                rate2 = (dp * per) / 100;
                                // rate1 = Convert.ToDouble(txtRate.Text);

                                rate1 = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);

                                if (Labelll.Text == "VAT EXCLUSIVE")
                                {
                                    // rate1 = (Convert.ToDouble(txtRate.Text)) + ((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtVATAmt.Text) / 100));
                                    rate1 = (Convert.ToDouble(rate1)) + ((Convert.ToDouble(rate1)) * (Convert.ToDouble(txtVATPre.Text) / 100));
                                }

                                if (rate1 < rate2)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be less than dealer rate Rs. " + rate2 + " ')", true);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        DataSet dsrate = bl.getRateInformation(drpProduct.SelectedValue);
                        double rate1 = 0;
                        double dp = 0;
                        double overall = 0;
                        double per = 0;
                        double rate2 = 0;

                        if (dsrate.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsrate.Tables[0].Rows)
                            {
                                dp = Convert.ToDouble(dr["dealerrate"]);
                                per = Convert.ToDouble(dr["deviation"]);
                                rate2 = (dp * per) / 100;
                                // rate1 = Convert.ToDouble(txtRate.Text);
                                rate1 = Convert.ToDouble(txtRate.Text) * Convert.ToInt32(txtQty.Text) / Convert.ToInt32(txtQty.Text);

                                if (Labelll.Text == "VAT EXCLUSIVE")
                                {
                                    // rate1 = (Convert.ToDouble(txtRate.Text)) + ((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtVATAmt.Text) / 100));
                                    rate1 = (Convert.ToDouble(rate1)) + ((Convert.ToDouble(rate1)) * (Convert.ToDouble(txtVATPre.Text) / 100));
                                }

                                if (rate1 < rate2)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate cannot be less than dealer rate Rs. " + rate2 + " ')", true);
                                    checkflag = true;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (checkflag == false)
        {
            // NEW Row Add
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList DrpProduct =
                         (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("drpPrd");
                        TextBox TextBoxQty =
                             (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("txtQty");
                        DropDownList drpIncharge =
                         (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("drpIncharge");
                        TextBox TextBoxDesc =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtDesc");
                        TextBox TextBoxRate =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("txtRate");
                        TextBox TextBoxTotalPrice =
                        (TextBox)grvStudentDetails.Rows[rowIndex].Cells[6].FindControl("txtTot");
                        TextBox TextBoxStock =
                         (TextBox)grvStudentDetails.Rows[rowIndex].Cells[7].FindControl("txtStock");
                        TextBox TextBoxExeComm =
                             (TextBox)grvStudentDetails.Rows[rowIndex].Cells[8].FindControl("txtExeComm");
                        TextBox TextBoxDisPre =
                            (TextBox)grvStudentDetails.Rows[rowIndex].Cells[9].FindControl("txtDisPre");
                        TextBox TextBoxVATPre =
                           (TextBox)grvStudentDetails.Rows[rowIndex].Cells[10].FindControl("txtVATPre");
                        TextBox TextBoxCSTPre =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[11].FindControl("txtCSTPre");
                        TextBox TextBoxPrBefVAT =
                            (TextBox)grvStudentDetails.Rows[rowIndex].Cells[12].FindControl("txtPrBefVAT");
                        TextBox TextBoxVATAmt =
                         (TextBox)grvStudentDetails.Rows[rowIndex].Cells[13].FindControl("txtVATAmt");
                        TextBox TextBoxRtVAT =
                        (TextBox)grvStudentDetails.Rows[rowIndex].Cells[14].FindControl("txtRtVAT");
                        TextBox TextBoxTotal =
                       (TextBox)grvStudentDetails.Rows[rowIndex].Cells[15].FindControl("txtTotal");


                        if (Convert.ToInt32(TextBoxQty.Text) > Convert.ToInt32(TextBoxStock.Text))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given qty is greater than stock.Current Stock : " + TextBoxStock.Text + "')", true);
                            return;
                        }

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["ItemCode"] = DrpProduct.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Qty"] = TextBoxQty.Text;
                        dtCurrentTable.Rows[i - 1]["executivename"] = drpIncharge.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ProductDesc"] = TextBoxDesc.Text;
                        dtCurrentTable.Rows[i - 1]["Rate"] = TextBoxRate.Text;
                        dtCurrentTable.Rows[i - 1]["TotalPrice"] = TextBoxTotalPrice.Text;
                        dtCurrentTable.Rows[i - 1]["Stock"] = TextBoxStock.Text;
                        dtCurrentTable.Rows[i - 1]["ExecCharge"] = TextBoxExeComm.Text;
                        dtCurrentTable.Rows[i - 1]["Discount"] = TextBoxDisPre.Text;
                        dtCurrentTable.Rows[i - 1]["Vat"] = TextBoxVATPre.Text;
                        dtCurrentTable.Rows[i - 1]["CST"] = TextBoxCSTPre.Text;
                        dtCurrentTable.Rows[i - 1]["PriceBeforeVATAmt"] = TextBoxPrBefVAT.Text;
                        dtCurrentTable.Rows[i - 1]["Vatamount"] = TextBoxVATAmt.Text;
                        dtCurrentTable.Rows[i - 1]["Subtotal"] = TextBoxRtVAT.Text;
                        dtCurrentTable.Rows[i - 1]["Totalmrp"] = TextBoxTotal.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grvStudentDetails.DataSource = dtCurrentTable;
                    grvStudentDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList DrpProduct =
                    (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("drpPrd");
                    TextBox TextBoxQty =
                     (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("txtQty");
                    DropDownList drpIncharge =
                    (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("drpIncharge");
                    TextBox TextBoxDesc =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtDesc");
                    TextBox TextBoxRate =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("txtRate");
                    TextBox TextBoxTotalPrice =
                     (TextBox)grvStudentDetails.Rows[rowIndex].Cells[6].FindControl("txtTot");
                    TextBox TextBoxStock =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[7].FindControl("txtStock");
                    TextBox TextBoxExeComm =
                     (TextBox)grvStudentDetails.Rows[rowIndex].Cells[8].FindControl("txtExeComm");
                    TextBox TextBoxDisPre =
                    (TextBox)grvStudentDetails.Rows[rowIndex].Cells[9].FindControl("txtDisPre");
                    TextBox TextBoxVATPre =
                   (TextBox)grvStudentDetails.Rows[rowIndex].Cells[10].FindControl("txtVATPre");
                    TextBox TextBoxCSTPre =
                  (TextBox)grvStudentDetails.Rows[rowIndex].Cells[11].FindControl("txtCSTPre");
                    TextBox TextBoxPrBefVAT =
                            (TextBox)grvStudentDetails.Rows[rowIndex].Cells[12].FindControl("txtPrBefVAT");
                    TextBox TextBoxVATAmt =
                 (TextBox)grvStudentDetails.Rows[rowIndex].Cells[12].FindControl("txtVATAmt");
                    TextBox TextBoxRtVAT =
                (TextBox)grvStudentDetails.Rows[rowIndex].Cells[13].FindControl("txtRtVAT");
                    TextBox TextBoxTotal =
               (TextBox)grvStudentDetails.Rows[rowIndex].Cells[14].FindControl("txtTotal");


                    DrpProduct.SelectedValue = dt.Rows[i]["ItemCode"].ToString();
                    TextBoxQty.Text = dt.Rows[i]["Qty"].ToString();
                    drpIncharge.SelectedValue = dt.Rows[i]["executivename"].ToString();
                    TextBoxDesc.Text = dt.Rows[i]["ProductDesc"].ToString();
                    TextBoxRate.Text = dt.Rows[i]["Rate"].ToString();
                    TextBoxTotalPrice.Text = dt.Rows[i]["TotalPrice"].ToString();
                    TextBoxStock.Text = dt.Rows[i]["Stock"].ToString();
                    TextBoxExeComm.Text = dt.Rows[i]["ExecCharge"].ToString();
                    TextBoxDisPre.Text = dt.Rows[i]["Discount"].ToString();
                    TextBoxVATPre.Text = dt.Rows[i]["Vat"].ToString();
                    TextBoxCSTPre.Text = dt.Rows[i]["CST"].ToString();
                    TextBoxPrBefVAT.Text = dt.Rows[i]["PriceBeforeVATAmt"].ToString();
                    TextBoxVATAmt.Text = dt.Rows[i]["Vatamount"].ToString();
                    TextBoxRtVAT.Text = dt.Rows[i]["Subtotal"].ToString();
                    TextBoxTotal.Text = dt.Rows[i]["Totalmrp"].ToString();
                    rowIndex++;

                }
            }
        }
    }

    protected void grvStudentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);

            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                grvStudentDetails.DataSource = dt;
                grvStudentDetails.DataBind();

                for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
                {
                    grvStudentDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }

                SetPreviousData();
                txtLU_TextChanged(sender, e);
            }
            else
            {
                DropDownList DrpProduct =
                  (DropDownList)grvStudentDetails.Rows[0].Cells[1].FindControl("drpPrd");
                TextBox TextBoxQty =
                 (TextBox)grvStudentDetails.Rows[0].Cells[2].FindControl("txtQty");
                DropDownList drpIncharge =
                (DropDownList)grvStudentDetails.Rows[0].Cells[3].FindControl("drpIncharge");
                TextBox TextBoxDesc =
                  (TextBox)grvStudentDetails.Rows[0].Cells[4].FindControl("txtDesc");
                TextBox TextBoxRate =
                  (TextBox)grvStudentDetails.Rows[0].Cells[5].FindControl("txtRate");
                TextBox TextBoxTotalPrice =
                 (TextBox)grvStudentDetails.Rows[0].Cells[6].FindControl("txtTot");
                TextBox TextBoxStock =
                  (TextBox)grvStudentDetails.Rows[0].Cells[7].FindControl("txtStock");
                TextBox TextBoxExeComm =
                 (TextBox)grvStudentDetails.Rows[0].Cells[8].FindControl("txtExeComm");
                TextBox TextBoxDisPre =
                 (TextBox)grvStudentDetails.Rows[0].Cells[9].FindControl("txtDisPre");
                TextBox TextBoxVATPre =
                 (TextBox)grvStudentDetails.Rows[0].Cells[10].FindControl("txtVATPre");
                TextBox TextBoxCSTPre =
                 (TextBox)grvStudentDetails.Rows[0].Cells[11].FindControl("txtCSTPre");
                TextBox TextBoxPrBefVAT =
                   (TextBox)grvStudentDetails.Rows[0].Cells[12].FindControl("txtPrBefVAT");
                TextBox TextBoxVATAmt =
                    (TextBox)grvStudentDetails.Rows[0].Cells[12].FindControl("txtVATAmt");
                TextBox TextBoxRtVAT =
                   (TextBox)grvStudentDetails.Rows[0].Cells[13].FindControl("txtRtVAT");
                TextBox TextBoxTotal =
                    (TextBox)grvStudentDetails.Rows[0].Cells[14].FindControl("txtTotal");


                DrpProduct.SelectedIndex = 0;
                TextBoxQty.Text = "";
                drpIncharge.SelectedIndex = 0;
                TextBoxDesc.Text = "";
                TextBoxRate.Text = "";
                TextBoxTotalPrice.Text = "";
                TextBoxStock.Text = "";
                TextBoxExeComm.Text = "";
                TextBoxDisPre.Text = "";
                TextBoxVATPre.Text = "";
                TextBoxCSTPre.Text = "";
                TextBoxPrBefVAT.Text = "";
                TextBoxVATAmt.Text = "";
                TextBoxRtVAT.Text = "";
                TextBoxTotal.Text = "";
            }
        }
    }
    double tablerate;
    protected void drpPrd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iq = 1;
        int ii = 1;
        string itemc = string.Empty;
        BusinessLogic bll = new BusinessLogic(sDataSource);
        EnableDiscount = bll.getEnableDiscountConfig();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable"];
            for (int vloop = 0; vloop < grvStudentDetails.Rows.Count; vloop++)
            {
                DropDownList DrpProduct =
                 (DropDownList)grvStudentDetails.Rows[vloop].FindControl("drpPrd");

                itemc = DrpProduct.Text;
                if ((itemc == null) || (itemc == ""))
                {
                }
                else
                {
                    for (int vloop1 = 0; vloop1 < grvStudentDetails.Rows.Count; vloop1++)
                    {
                        DropDownList DrpProduct1 =
                         (DropDownList)grvStudentDetails.Rows[vloop1].FindControl("drpPrd");
                        if (ii == iq)
                        {
                        }
                        else
                        {
                            if (itemc == DrpProduct1.Text)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product - " + itemc + " - already exists in the Grid.');", true);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
                                return;

                            }
                        }
                        ii = ii + 1;
                    }
                }
                iq = iq + 1;
                ii = 1;
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }


        for (int i = grvStudentDetails.Rows.Count; i == grvStudentDetails.Rows.Count; i++)
        {
            DropDownList DrpProduct =
              (DropDownList)grvStudentDetails.Rows[i - 1].Cells[1].FindControl("drpPrd");
            TextBox TextBoxQty =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[2].FindControl("txtQty");
            DropDownList drpIncharge =
              (DropDownList)grvStudentDetails.Rows[i - 1].Cells[3].FindControl("drpIncharge");
            TextBox TextBoxDesc =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[4].FindControl("txtDesc");
            TextBox TextBoxRate =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[5].FindControl("txtRate");
            TextBox TextBoxTotalPrice =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[6].FindControl("txtTot");
            TextBox TextBoxStock =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[7].FindControl("txtStock");
            TextBox TextBoxExeComm =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[8].FindControl("txtExeComm");
            TextBox TextBoxDisPre =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[9].FindControl("txtDisPre");
            TextBox TextBoxVATPre =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[10].FindControl("txtVATPre");
            TextBox TextBoxCSTPre =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[11].FindControl("txtCSTPre");
            TextBox TextBoxVATAmt =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[12].FindControl("txtVATAmt");
            TextBox TextBoxRtVAT =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[13].FindControl("txtRtVAT");
            TextBox TextBoxTotal =
              (TextBox)grvStudentDetails.Rows[i - 1].Cells[14].FindControl("txtTotal");

            BusinessLogic bl = new BusinessLogic(sDataSource);
            // DataSet customerDs = bl.getProdInfo(Convert.ToString(DrpProduct.SelectedItem.Value));
            if (cmbCustomer.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Customer Name in Invoice Header Details tab');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
                DrpProduct.SelectedIndex = 0;
            }
            else
            {
                DataSet customerDs = bl.ListSalesProductPriceDetails(DrpProduct.SelectedItem.Value.Trim(), drpCustomerCategoryAdd.SelectedValue);


                string address = string.Empty;

                if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
                {

                    if (customerDs.Tables[0].Rows[0]["ProductDesc"] != null)
                        TextBoxDesc.Text = customerDs.Tables[0].Rows[0]["ProductDesc"].ToString();

                    if (ddDeliveryNote.SelectedValue != "YES")
                    {
                        if (customerDs.Tables[0].Rows[0]["Rate"] != null)
                            TextBoxRate.Text = Convert.ToDouble(customerDs.Tables[0].Rows[0]["Rate"].ToString()).ToString("#0.00");
                    }
                    else
                    {
                        if (customerDs.Tables[0].Rows[0]["Rate"] != null)
                            TextBoxRate.Text = "0.00";
                    }

                    if (customerDs.Tables[0].Rows[0]["Stock"] != null)
                        TextBoxStock.Text = customerDs.Tables[0].Rows[0]["Stock"].ToString();

                    if (customerDs.Tables[0].Rows[0]["ExecutiveCommission"] != null)
                        TextBoxExeComm.Text = customerDs.Tables[0].Rows[0]["ExecutiveCommission"].ToString();

                    if (EnableDiscount == "YES")
                    {
                        if (customerDs.Tables[0].Rows[0]["Discount"] != null)
                            TextBoxDisPre.Text = customerDs.Tables[0].Rows[0]["Discount"].ToString();
                    }
                    else
                    {
                        if (customerDs.Tables[0].Rows[0]["Discount"] != null)
                            TextBoxDisPre.Text = "0";// customerDs.Tables[0].Rows[0]["Discount"].ToString() + Environment.NewLine;
                    }

                    if (customerDs.Tables[0].Rows[0]["VAT"] != null)
                        TextBoxVATPre.Text = customerDs.Tables[0].Rows[0]["VAT"].ToString();

                    if (customerDs.Tables[0].Rows[0]["CST"] != null)
                        TextBoxCSTPre.Text = customerDs.Tables[0].Rows[0]["CST"].ToString();

                }
                else
                {
                    TextBoxDesc.Text = string.Empty;
                    TextBoxRate.Text = string.Empty;
                    TextBoxExeComm.Text = string.Empty;
                    TextBoxDisPre.Text = string.Empty;
                    TextBoxVATPre.Text = string.Empty;
                    TextBoxCSTPre.Text = string.Empty;
                }
                TextBoxQty.Focus();
                errPanel.Visible = false;
                ErrMsg.Text = "";
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
    }

    protected void grvStudentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();
            DataSet dsEmp = new DataSet();

            ds = bl.ListProdForDynammicrow(sDataSource);
            dsEmp = bl.ListExecutive();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl = (DropDownList)e.Row.FindControl("drpPrd");
                ddl.Items.Clear();
                ListItem lifzzh = new ListItem("Select Product", "0");
                lifzzh.Attributes.Add("style", "color:Black");
                ddl.Items.Add(lifzzh);
                ddl.DataSource = ds;
                ddl.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl.DataBind();
                ddl.DataTextField = "ProductName";
                ddl.DataValueField = "ItemCode";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl1 = (DropDownList)e.Row.FindControl("drpIncharge");
                ddl1.Items.Clear();
                ListItem lifzzh1 = new ListItem("Select Employee", "0");
                lifzzh1.Attributes.Add("style", "color:Black");
                ddl1.Items.Add(lifzzh1);
                ddl1.DataSource = dsEmp;
                ddl1.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl1.DataBind();
                ddl1.DataTextField = "empFirstName";
                ddl1.DataValueField = "empno";
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void SetRowData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList DrpProduct =
                    (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("drpPrd");
                    TextBox TextBoxQty =
                     (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("txtQty");
                    DropDownList drpIncharge =
                   (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("drpIncharge");
                    TextBox TextBoxDesc =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtDesc");
                    TextBox TextBoxRate =
                      (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("txtRate");
                    TextBox TextBoxTotalPrice =
                   (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("txtTot");
                    TextBox TextBoxStock =
                     (TextBox)grvStudentDetails.Rows[rowIndex].Cells[6].FindControl("txtStock");
                    TextBox TextBoxExeComm =
                     (TextBox)grvStudentDetails.Rows[rowIndex].Cells[7].FindControl("txtExeComm");
                    TextBox TextBoxDisPre =
                    (TextBox)grvStudentDetails.Rows[rowIndex].Cells[8].FindControl("txtDisPre");
                    TextBox TextBoxVATPre =
                   (TextBox)grvStudentDetails.Rows[rowIndex].Cells[9].FindControl("txtVATPre");
                    TextBox TextBoxCSTPre =
                  (TextBox)grvStudentDetails.Rows[rowIndex].Cells[10].FindControl("txtCSTPre");
                    TextBox TextBoxPrBefVAT =
                 (TextBox)grvStudentDetails.Rows[0].Cells[12].FindControl("txtPrBefVAT");
                    TextBox TextBoxVATAmt =
                 (TextBox)grvStudentDetails.Rows[rowIndex].Cells[11].FindControl("txtVATAmt");
                    TextBox TextBoxRtVAT =
                (TextBox)grvStudentDetails.Rows[rowIndex].Cells[12].FindControl("txtRtVAT");
                    TextBox TextBoxTotal =
               (TextBox)grvStudentDetails.Rows[rowIndex].Cells[13].FindControl("txtTotal");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["ItemCode"] = DrpProduct.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Qty"] = TextBoxQty.Text;
                    dtCurrentTable.Rows[i - 1]["executivename"] = drpIncharge.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["ProductDesc"] = TextBoxDesc.Text;
                    dtCurrentTable.Rows[i - 1]["Rate"] = TextBoxRate.Text;
                    dtCurrentTable.Rows[i - 1]["TotalPrice"] = TextBoxTotalPrice.Text;
                    dtCurrentTable.Rows[i - 1]["Stock"] = TextBoxStock.Text;
                    dtCurrentTable.Rows[i - 1]["ExecCharge"] = TextBoxExeComm.Text;
                    dtCurrentTable.Rows[i - 1]["Discount"] = TextBoxDisPre.Text;
                    dtCurrentTable.Rows[i - 1]["Vat"] = TextBoxVATPre.Text;
                    dtCurrentTable.Rows[i - 1]["CST"] = TextBoxCSTPre.Text;
                    dtCurrentTable.Rows[i - 1]["PriceBeforeVATAmt"] = TextBoxPrBefVAT.Text;
                    dtCurrentTable.Rows[i - 1]["Vatamount"] = TextBoxVATAmt.Text;
                    dtCurrentTable.Rows[i - 1]["Subtotal"] = TextBoxRtVAT.Text;
                    dtCurrentTable.Rows[i - 1]["Totalmrp"] = TextBoxTotal.Text;
                    rowIndex++;

                }

                ViewState["CurrentTable"] = dtCurrentTable;
                grvStudentDetails.DataSource = dtCurrentTable;
                grvStudentDetails.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
    }

    private void FirstGridViewRow()
    {
        BusinessLogic bll = new BusinessLogic(sDataSource);
        EnableDiscount = bll.getEnableDiscountConfig();
        string discType = GetDiscType();

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
        dt.Columns.Add(new DataColumn("Qty", typeof(string)));
        dt.Columns.Add(new DataColumn("executivename", typeof(string)));
        dt.Columns.Add(new DataColumn("ProductDesc", typeof(string)));
        dt.Columns.Add(new DataColumn("RtnQty", typeof(string)));
        dt.Columns.Add(new DataColumn("Rate", typeof(string)));
        dt.Columns.Add(new DataColumn("TotalPrice", typeof(string)));
        dt.Columns.Add(new DataColumn("Stock", typeof(string)));
        dt.Columns.Add(new DataColumn("ExecCharge", typeof(string)));
        dt.Columns.Add(new DataColumn("Discount", typeof(string)));
        dt.Columns.Add(new DataColumn("Vat", typeof(string)));
        dt.Columns.Add(new DataColumn("CST", typeof(string)));
        dt.Columns.Add(new DataColumn("PriceBeforeVATAmt", typeof(string)));
        dt.Columns.Add(new DataColumn("Vatamount", typeof(string)));
        dt.Columns.Add(new DataColumn("Subtotal", typeof(string)));
        dt.Columns.Add(new DataColumn("Totalmrp", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["ItemCode"] = string.Empty;
        dr["Qty"] = string.Empty;
        dr["executivename"] = string.Empty;
        dr["ProductDesc"] = string.Empty;
        dr["RtnQty"] = string.Empty;
        dr["Rate"] = string.Empty;
        dr["TotalPrice"] = string.Empty;
        dr["Stock"] = string.Empty;
        dr["ExecCharge"] = string.Empty;
        dr["Discount"] = string.Empty;
        dr["Vat"] = string.Empty;
        dr["CST"] = string.Empty;
        dr["PriceBeforeVATAmt"] = string.Empty;
        dr["Vatamount"] = string.Empty;
        dr["Subtotal"] = string.Empty;
        dr["Totalmrp"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        grvStudentDetails.DataSource = dt;
               
        
        if (drpPurchaseReturn.SelectedValue != "YES")
        {
            grvStudentDetails.Columns[3].Visible = true;
            grvStudentDetails.Columns[4].Visible = true;
            grvStudentDetails.Columns[8].Visible = true;
            grvStudentDetails.Columns[9].Visible = true;
            grvStudentDetails.Columns[10].Visible = true;
            grvStudentDetails.Columns[5].Visible = false;
            grvStudentDetails.Columns[13].Visible = true;

            grvStudentDetails.Columns[7].HeaderText = "Total Price";
            grvStudentDetails.Columns[14].HeaderText = "VAT Amount";

            if (EnableDiscount == "YES")
            {
                grvStudentDetails.Columns[10].Visible = true;
            }
            else
            {
                grvStudentDetails.Columns[10].Visible = false;
            }

            if (discType == "PERCENTAGE")
            {
                grvStudentDetails.Columns[10].HeaderText = "Disc(%)";
            }
            else if (discType == "RUPEE")
            {
                grvStudentDetails.Columns[10].HeaderText = "Disc(INR)";
            }
        }
        else
        {
            grvStudentDetails.Columns[3].Visible = false;
            grvStudentDetails.Columns[4].Visible = false;
            grvStudentDetails.Columns[8].Visible = false;
            grvStudentDetails.Columns[9].Visible = false;
            grvStudentDetails.Columns[10].Visible = false;
            grvStudentDetails.Columns[13].Visible = false;
            grvStudentDetails.Columns[7].HeaderText = "Disc(%)";
            grvStudentDetails.Columns[14].HeaderText = "Discount Amount";
            grvStudentDetails.Columns[5].Visible = true;
        }
        grvStudentDetails.DataBind();
    }

    Double sumAmt = 0;
    //Double sumTAmt = 0;
    Double sumVat = 0;
    Double sumRate = 0;
    Double sumExecComm = 0;
    Double sumCST = 0;
    Double sumDis = 0;
    Double sumNet = 0;
    bool vatcheck = false;
    double caldisamt;
    double vatinclusiverate;

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        if (optionmethod.SelectedValue != "PurchaseReturn")
        {
            string discType = GetDiscType();

            //for (int i = grvStudentDetails.Rows.Count; i == grvStudentDetails.Rows.Count; i++)       
            for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
            {
                DropDownList DrpProduct =
                  (DropDownList)grvStudentDetails.Rows[i].Cells[1].FindControl("drpPrd");
                TextBox TextBoxQty =
                  (TextBox)grvStudentDetails.Rows[i].Cells[2].FindControl("txtQty");
                DropDownList drpIncharge =
                (DropDownList)grvStudentDetails.Rows[i].Cells[3].FindControl("drpIncharge");
                TextBox TextBoxDesc =
                  (TextBox)grvStudentDetails.Rows[i].Cells[4].FindControl("txtDesc");
                TextBox TextBoxRate =
                  (TextBox)grvStudentDetails.Rows[i].Cells[5].FindControl("txtRate");
                TextBox TextBoxTotalPrice =
                 (TextBox)grvStudentDetails.Rows[i].Cells[6].FindControl("txtTot");
                TextBox TextBoxStock =
                (TextBox)grvStudentDetails.Rows[i].Cells[7].FindControl("txtStock");
                TextBox TextBoxExeComm =
                  (TextBox)grvStudentDetails.Rows[i].Cells[8].FindControl("txtExeComm");
                TextBox TextBoxDisPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[9].FindControl("txtDisPre");
                TextBox TextBoxVATPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[10].FindControl("txtVATPre");
                TextBox TextBoxCSTPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[11].FindControl("txtCSTPre");
                TextBox txtPrBefVAT =
                  (TextBox)grvStudentDetails.Rows[i].Cells[12].FindControl("txtPrBefVAT");
                TextBox TextBoxVATAmt =
                  (TextBox)grvStudentDetails.Rows[i].Cells[13].FindControl("txtVATAmt");
                TextBox TextBoxRtVAT =
                  (TextBox)grvStudentDetails.Rows[i].Cells[14].FindControl("txtRtVAT");
                TextBox TextBoxTotal =
                  (TextBox)grvStudentDetails.Rows[i].Cells[15].FindControl("txtTotal");


                if (Convert.ToInt32(TextBoxQty.Text) > Convert.ToInt32(TextBoxStock.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given qty is greater than Stock')", true);
                    return;
                }


                if (Labelll.Text == "VAT INCLUSIVE")
                {
                    vatcheck = true;
                    TextBoxTotal.Text = Convert.ToString(Convert.ToDouble(TextBoxRate.Text) * Convert.ToDouble(TextBoxQty.Text));
                    TextBoxTotalPrice.Text = Convert.ToDouble(Convert.ToDouble(TextBoxRate.Text) * Convert.ToDouble(TextBoxQty.Text)).ToString("#0.00");
                    TextBoxRtVAT.Text = Convert.ToString(Convert.ToDouble(TextBoxRate.Text) * Convert.ToDouble(TextBoxQty.Text));
                    if (discType == "PERCENTAGE")
                    {
                        caldisamt = Convert.ToDouble(TextBoxTotal.Text) * Convert.ToDouble(TextBoxDisPre.Text) / 100;
                    }
                    else if (discType == "RUPEE")
                    {
                        caldisamt = Convert.ToDouble(TextBoxDisPre.Text);
                    }
                    double calnet = Convert.ToDouble(TextBoxTotal.Text) - caldisamt;
                    double vatper = Convert.ToDouble(TextBoxVATPre.Text);
                    double vatper1 = vatper + 100;
                    double vatinclusiverate = calnet * vatper / vatper1;
                    double sVatamount = calnet - vatinclusiverate;
                    if (ddDeliveryNote.SelectedValue != "YES")
                    {
                        TextBoxVATAmt.Text = vatinclusiverate.ToString("#0.00");
                        TextBoxRtVAT.Text = calnet.ToString("#0.00");
                        txtPrBefVAT.Text = sVatamount.ToString("#0.00");
                        TextBoxTotal.Text = calnet.ToString("#0.00");
                    }
                    else
                    {
                        TextBoxVATAmt.Text = "0.00";
                        TextBoxRtVAT.Text = "0.00";
                        txtPrBefVAT.Text = "0.00";
                        TextBoxTotal.Text = "0.00";
                    }
                }
                else if (Labelll.Text == "VAT EXCLUSIVE")
                {
                    vatcheck = true;
                    TextBoxRtVAT.Text = Convert.ToString(Convert.ToDouble(TextBoxRate.Text) * Convert.ToDouble(TextBoxQty.Text));
                    TextBoxTotalPrice.Text = Convert.ToDouble(Convert.ToDouble(TextBoxRate.Text) * Convert.ToDouble(TextBoxQty.Text)).ToString("#0.00");
                    if (discType == "PERCENTAGE")
                    {
                        vatinclusiverate = Convert.ToDouble(TextBoxRtVAT.Text) * Convert.ToDouble(TextBoxDisPre.Text) / 100;
                    }
                    else if (discType == "RUPEE")
                    {
                        vatinclusiverate = Convert.ToDouble(TextBoxDisPre.Text);
                    }
                    double vatinclusiverate3 = Convert.ToDouble(TextBoxRtVAT.Text) - vatinclusiverate;
                    double vatinclusiverate1 = Convert.ToDouble(vatinclusiverate3) * Convert.ToDouble(TextBoxVATPre.Text) / 100;
                    double vatinclusiverate2 = vatinclusiverate1 + vatinclusiverate3;
                    if (ddDeliveryNote.SelectedValue != "YES")
                    {
                        TextBoxVATAmt.Text = vatinclusiverate1.ToString("#0.00");
                        txtPrBefVAT.Text = vatinclusiverate3.ToString("#0.00");
                        TextBoxRtVAT.Text = vatinclusiverate2.ToString("#0.00");
                        TextBoxTotal.Text = vatinclusiverate2.ToString("#0.00");
                    }
                    else
                    {
                        TextBoxVATAmt.Text = "0.00";
                        txtPrBefVAT.Text = "0.00";
                        TextBoxRtVAT.Text = "0.00";
                        TextBoxTotal.Text = "0.00";
                    }
                }

            }
            sumAmt = 0;
            for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
            {
                DropDownList DrpProduct =
                  (DropDownList)grvStudentDetails.Rows[i].Cells[1].FindControl("drpPrd");
                TextBox TextBoxQty =
                 (TextBox)grvStudentDetails.Rows[i].Cells[2].FindControl("txtQty");
                DropDownList drpIncharge =
                 (DropDownList)grvStudentDetails.Rows[i].Cells[3].FindControl("drpIncharge");
                TextBox TextBoxDesc =
                  (TextBox)grvStudentDetails.Rows[i].Cells[4].FindControl("txtDesc");
                TextBox TextBoxRate =
                  (TextBox)grvStudentDetails.Rows[i].Cells[5].FindControl("txtRate");
                TextBox TextBoxStock =
                (TextBox)grvStudentDetails.Rows[i].Cells[6].FindControl("txtStock");
                TextBox TextBoxExeComm =
                  (TextBox)grvStudentDetails.Rows[i].Cells[7].FindControl("txtExeComm");
                TextBox TextBoxDisPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[8].FindControl("txtDisPre");
                TextBox TextBoxVATPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[9].FindControl("txtVATPre");
                TextBox TextBoxCSTPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[10].FindControl("txtCSTPre");
                TextBox TextBoxVATAmt =
                  (TextBox)grvStudentDetails.Rows[i].Cells[11].FindControl("txtVATAmt");
                TextBox TextBoxRtVAT =
                  (TextBox)grvStudentDetails.Rows[i].Cells[12].FindControl("txtRtVAT");
                TextBox TextBoxTotal =
                  (TextBox)grvStudentDetails.Rows[i].Cells[13].FindControl("txtTotal");


                if (TextBoxTotal.Text != null)
                    // sumAmt = Convert.ToDouble(GetTotal(Convert.ToDouble(TextBoxQty.Text), Convert.ToDouble(TextBoxRtVAT.Text), Convert.ToDouble(TextBoxDisPre.Text), Convert.ToDouble(TextBoxVATPre.Text), Convert.ToDouble(TextBoxCSTPre.Text), Convert.ToDouble(TextBoxTotal.Text)));
                    sumAmt = Convert.ToDouble(TextBoxTotal.Text);

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
                hdPrevSalesTotal.Value = lblNet.Text;
                UpdatePanelTotalSummary.Update();
                errPanel.Visible = false;
                ErrMsg.Text = "";
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }
    }
    protected void drpIncharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        if (optionmethod.SelectedValue != "PurchaseReturn")
        {
            string discType = GetDiscType();

            //for (int i = grvStudentDetails.Rows.Count; i == grvStudentDetails.Rows.Count; i++)
            for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
            {
                DropDownList DrpProduct =
                  (DropDownList)grvStudentDetails.Rows[i].Cells[1].FindControl("drpPrd");
                TextBox TextBoxQty =
                  (TextBox)grvStudentDetails.Rows[i].Cells[2].FindControl("txtQty");
                DropDownList drpIncharge =
                (DropDownList)grvStudentDetails.Rows[i].Cells[3].FindControl("drpIncharge");
                TextBox TextBoxDesc =
                  (TextBox)grvStudentDetails.Rows[i].Cells[4].FindControl("txtDesc");
                TextBox TextBoxRate =
                  (TextBox)grvStudentDetails.Rows[i].Cells[5].FindControl("txtRate");
                TextBox TextBoxTotalPrice =
                (TextBox)grvStudentDetails.Rows[i].Cells[6].FindControl("txtTot");
                TextBox TextBoxStock =
                (TextBox)grvStudentDetails.Rows[i].Cells[7].FindControl("txtStock");
                TextBox TextBoxExeComm =
                  (TextBox)grvStudentDetails.Rows[i].Cells[8].FindControl("txtExeComm");
                TextBox TextBoxDisPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[9].FindControl("txtDisPre");
                TextBox TextBoxVATPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[10].FindControl("txtVATPre");
                TextBox TextBoxCSTPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[11].FindControl("txtCSTPre");
                TextBox txtPrBefVAT =
                (TextBox)grvStudentDetails.Rows[i].Cells[12].FindControl("txtPrBefVAT");
                TextBox TextBoxVATAmt =
                  (TextBox)grvStudentDetails.Rows[i].Cells[13].FindControl("txtVATAmt");
                TextBox TextBoxRtVAT =
                  (TextBox)grvStudentDetails.Rows[i].Cells[14].FindControl("txtRtVAT");
                TextBox TextBoxTotal =
                  (TextBox)grvStudentDetails.Rows[i].Cells[15].FindControl("txtTotal");

                //if (Convert.ToInt32(TextBoxQty.Text) > Convert.ToInt32(TextBoxStock.Text))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given qty is greater than stock.Current Stock : " + TextBoxStock.Text + "')", true);
                //    return;
                //}
                if (TextBoxRate.Text != "")
                {
                    if (Labelll.Text == "VAT INCLUSIVE")
                    {
                        if (vatcheck == false)
                        {
                            vatcheck = true;
                            TextBoxTotal.Text = Convert.ToString(Convert.ToDouble(TextBoxRate.Text) * Convert.ToDouble(TextBoxQty.Text));
                            TextBoxTotalPrice.Text = Convert.ToDouble(Convert.ToDouble(TextBoxRate.Text) * Convert.ToDouble(TextBoxQty.Text)).ToString("#0.00");
                            TextBoxRtVAT.Text = Convert.ToString(Convert.ToDouble(TextBoxRate.Text) * Convert.ToDouble(TextBoxQty.Text));
                            if (discType == "PERCENTAGE")
                            {
                                caldisamt = Convert.ToDouble(TextBoxTotal.Text) * Convert.ToDouble(TextBoxDisPre.Text) / 100;
                            }
                            else if (discType == "RUPEE")
                            {
                                caldisamt = Convert.ToDouble(TextBoxDisPre.Text);
                            }
                            double calnet = Convert.ToDouble(TextBoxTotal.Text) - caldisamt;
                            double vatper = Convert.ToDouble(TextBoxVATPre.Text);
                            double vatper1 = vatper + 100;
                            double vatinclusiverate = calnet * vatper / vatper1;
                            //double vatinclusiverate = (((Convert.ToDouble(TextBoxRate.Text) * (Convert.ToDouble(TextBoxQty.Text))) - Convert.ToDouble(TextBoxDisPre.Text)) / vatper1);
                            double sVatamount = calnet - vatinclusiverate;
                            if (ddDeliveryNote.SelectedValue != "YES")
                            {
                                TextBoxVATAmt.Text = vatinclusiverate.ToString("#0.00");
                                TextBoxRtVAT.Text = calnet.ToString("#0.00");
                                txtPrBefVAT.Text = sVatamount.ToString("#0.00");
                                TextBoxTotal.Text = calnet.ToString("#0.00");
                                vatcheck = false;
                            }
                            else
                            {
                                TextBoxVATAmt.Text = "0.00";
                                TextBoxRtVAT.Text = "0.00";
                                txtPrBefVAT.Text = "0.00";
                                TextBoxTotal.Text = "0.00";
                                vatcheck = false;
                            }

                        }
                    }
                    else if (Labelll.Text == "VAT EXCLUSIVE")
                    {
                        if (vatcheck == false)
                        {
                            vatcheck = true;
                            TextBoxRtVAT.Text = Convert.ToString(Convert.ToDouble(TextBoxRate.Text) * Convert.ToDouble(TextBoxQty.Text));
                            TextBoxTotalPrice.Text = Convert.ToDouble(Convert.ToDouble(TextBoxRate.Text) * Convert.ToDouble(TextBoxQty.Text)).ToString("#0.00");
                            if (discType == "PERCENTAGE")
                            {
                                vatinclusiverate = Convert.ToDouble(TextBoxRtVAT.Text) * Convert.ToDouble(TextBoxDisPre.Text) / 100;
                            }
                            else if (discType == "RUPEE")
                            {
                                vatinclusiverate = Convert.ToDouble(TextBoxDisPre.Text);
                            }
                            double vatinclusiverate3 = Convert.ToDouble(TextBoxRtVAT.Text) - vatinclusiverate;
                            double vatinclusiverate1 = Convert.ToDouble(vatinclusiverate3) * Convert.ToDouble(TextBoxVATPre.Text) / 100;
                            double vatinclusiverate2 = vatinclusiverate1 + vatinclusiverate3;
                            if (ddDeliveryNote.SelectedValue != "YES")
                            {
                                TextBoxVATAmt.Text = vatinclusiverate1.ToString("#0.00");
                                txtPrBefVAT.Text = vatinclusiverate3.ToString("#0.00");
                                TextBoxRtVAT.Text = vatinclusiverate2.ToString("#0.00");
                                TextBoxTotal.Text = vatinclusiverate2.ToString("#0.00");
                                vatcheck = false;
                            }
                            else
                            {
                                TextBoxVATAmt.Text = "0.00";
                                txtPrBefVAT.Text = "0.00";
                                TextBoxRtVAT.Text = "0.00";
                                TextBoxTotal.Text = "0.00";
                                vatcheck = false;
                            }
                        }
                    }
                }
            }
            sumAmt = 0;
            sumNet = 0;
            for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
            {
                DropDownList DrpProduct =
                 (DropDownList)grvStudentDetails.Rows[i].Cells[1].FindControl("drpPrd");
                TextBox TextBoxQty =
                  (TextBox)grvStudentDetails.Rows[i].Cells[2].FindControl("txtQty");
                DropDownList drpIncharge =
                (DropDownList)grvStudentDetails.Rows[i].Cells[3].FindControl("drpIncharge");
                TextBox TextBoxDesc =
                  (TextBox)grvStudentDetails.Rows[i].Cells[4].FindControl("txtDesc");
                TextBox TextBoxRate =
                  (TextBox)grvStudentDetails.Rows[i].Cells[5].FindControl("txtRate");
                TextBox TextBoxTotalPrice =
                (TextBox)grvStudentDetails.Rows[i].Cells[6].FindControl("txtTot");
                TextBox TextBoxStock =
                (TextBox)grvStudentDetails.Rows[i].Cells[7].FindControl("txtStock");
                TextBox TextBoxExeComm =
                  (TextBox)grvStudentDetails.Rows[i].Cells[8].FindControl("txtExeComm");
                TextBox TextBoxDisPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[9].FindControl("txtDisPre");
                TextBox TextBoxVATPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[10].FindControl("txtVATPre");
                TextBox TextBoxCSTPre =
                  (TextBox)grvStudentDetails.Rows[i].Cells[11].FindControl("txtCSTPre");
                TextBox txtPrBefVAT =
                (TextBox)grvStudentDetails.Rows[i].Cells[12].FindControl("txtPrBefVAT");
                TextBox TextBoxVATAmt =
                  (TextBox)grvStudentDetails.Rows[i].Cells[13].FindControl("txtVATAmt");
                TextBox TextBoxRtVAT =
                  (TextBox)grvStudentDetails.Rows[i].Cells[14].FindControl("txtRtVAT");
                TextBox TextBoxTotal =
                  (TextBox)grvStudentDetails.Rows[i].Cells[15].FindControl("txtTotal");

                if (TextBoxRate.Text != "")
                {
                    if (TextBoxTotal.Text != null)
                        // sumAmt = Convert.ToDouble(GetTotal(Convert.ToDouble(TextBoxQty.Text), Convert.ToDouble(TextBoxRtVAT.Text), Convert.ToDouble(TextBoxDisPre.Text), Convert.ToDouble(TextBoxVATPre.Text), Convert.ToDouble(TextBoxCSTPre.Text), Convert.ToDouble(TextBoxTotal.Text)));
                        sumAmt = Convert.ToDouble(TextBoxTotal.Text);

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
                    hdPrevSalesTotal.Value = lblNet.Text;
                    UpdatePanelTotalSummary.Update();
                    errPanel.Visible = false;
                    ErrMsg.Text = "";
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }
    }

    protected void txtLU_TextChanged(object sender, EventArgs e)
    {
        sumAmt = 0;
        for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
        {
            DropDownList DrpProduct =
              (DropDownList)grvStudentDetails.Rows[i].Cells[1].FindControl("drpPrd");
            TextBox TextBoxQty =
              (TextBox)grvStudentDetails.Rows[i].Cells[2].FindControl("txtQty");
            DropDownList drpIncharge =
            (DropDownList)grvStudentDetails.Rows[i].Cells[3].FindControl("drpIncharge");
            TextBox TextBoxDesc =
              (TextBox)grvStudentDetails.Rows[i].Cells[4].FindControl("txtDesc");
            TextBox TextBoxRate =
              (TextBox)grvStudentDetails.Rows[i].Cells[5].FindControl("txtRate");
            TextBox TextBoxTotalPrice =
            (TextBox)grvStudentDetails.Rows[i].Cells[6].FindControl("txtTot");
            TextBox TextBoxStock =
            (TextBox)grvStudentDetails.Rows[i].Cells[7].FindControl("txtStock");
            TextBox TextBoxExeComm =
              (TextBox)grvStudentDetails.Rows[i].Cells[8].FindControl("txtExeComm");
            TextBox TextBoxDisPre =
              (TextBox)grvStudentDetails.Rows[i].Cells[9].FindControl("txtDisPre");
            TextBox TextBoxVATPre =
              (TextBox)grvStudentDetails.Rows[i].Cells[10].FindControl("txtVATPre");
            TextBox TextBoxCSTPre =
              (TextBox)grvStudentDetails.Rows[i].Cells[11].FindControl("txtCSTPre");
            TextBox txtPrBefVAT =
            (TextBox)grvStudentDetails.Rows[i].Cells[12].FindControl("txtPrBefVAT");
            TextBox TextBoxVATAmt =
              (TextBox)grvStudentDetails.Rows[i].Cells[13].FindControl("txtVATAmt");
            TextBox TextBoxRtVAT =
              (TextBox)grvStudentDetails.Rows[i].Cells[14].FindControl("txtRtVAT");
            TextBox TextBoxTotal =
              (TextBox)grvStudentDetails.Rows[i].Cells[15].FindControl("txtTotal");


            if (TextBoxTotal.Text != null && TextBoxTotal.Text != "")
                // sumAmt = Convert.ToDouble(GetTotal(Convert.ToDouble(TextBoxQty.Text), Convert.ToDouble(TextBoxRtVAT.Text), Convert.ToDouble(TextBoxDisPre.Text), Convert.ToDouble(TextBoxVATPre.Text), Convert.ToDouble(TextBoxCSTPre.Text), Convert.ToDouble(TextBoxTotal.Text)));
                sumAmt = sumAmt + Convert.ToDouble(TextBoxTotal.Text);
        }
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
        hdPrevSalesTotal.Value = lblNet.Text;
        UpdatePanelTotalSummary.Update();
        errPanel.Visible = false;
        ErrMsg.Text = "";

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
    }

    private void getfootercalc()
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
    }

    //protected void tabs2_ActiveTabChanged(object sender, EventArgs e)
    //{
    //    if (cmbCustomer.SelectedValue == "0")
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Customer Name in Invoice Header Details tab');", true);
    //    }
    //}   

    protected void drpPurID_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sCustomer = string.Empty;
        string scuscategory = string.Empty;
        string strPaymode = string.Empty;
        string MultiPaymode = string.Empty;
        DataSet itemDs = new DataSet();

        int purchaseID = Convert.ToInt32(drpPurID.SelectedValue);
        BusinessLogic bl = new BusinessLogic(sDataSource);
        /*Start Purchase Loading / Unloading Freight Change - March 16*/
        DataSet ds = bl.GetPurchaseForId(purchaseID);
        //DataSet dss = bl.GetPurchaseItemsForId(purchaseID);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //if (ds.Tables[0].Rows[0]["Types"] != null)
                //    Labelll.Text = Convert.ToString(ds.Tables[0].Rows[0]["Types"]);
                //else
                //    Labelll.Text = "";

                if (ds.Tables[0].Rows[0]["BillDate"] != null)
                    txtBillDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");

                if (ds.Tables[0].Rows[0]["BillNo"] != null)
                    lblBillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();

                if (ds.Tables[0].Rows[0]["InternalTransfer"] != null)
                {
                    drpIntTrans.ClearSelection();
                    ListItem cli = drpIntTrans.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["InternalTransfer"]));

                    if (cli != null) cli.Selected = true;
                }
                else
                    //drpIncharge.SelectedIndex = 0;

                    //if (ds.Tables[0].Rows[0]["ManualSales"] != null)
                    //{
                    //    drpmanualsales.ClearSelection();
                    //    ListItem cli = drpmanualsales.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["ManualSales"]));

                    //    if (cli != null) cli.Selected = true;
                    //}
                    //else
                    //    drpmanualsales.SelectedIndex = 0;

                    //if (ds.Tables[0].Rows[0]["NormalSales"] != null)
                    //{
                    //    drpnormalsales.ClearSelection();
                    //    ListItem cli = drpnormalsales.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["NormalSales"]));

                    //    if (cli != null) cli.Selected = true;
                    //}
                    //else
                    //    drpnormalsales.SelectedIndex = 0;

                    if (ds.Tables[0].Rows[0]["DeliveryNote"] != null)
                    {
                        ddDeliveryNote.ClearSelection();
                        ListItem cli = ddDeliveryNote.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["DeliveryNote"]));

                        if (cli != null) cli.Selected = true;
                    }
                    else
                        ddDeliveryNote.SelectedIndex = 0;

                //if (ds.Tables[0].Rows[0]["PurchaseReturn"] != null)
                //{
                //    drpPurchaseReturn.ClearSelection();
                //    if (drpPurchaseReturn.Items.FindByValue(ds.Tables[0].Rows[0]["PurchaseReturn"].ToString().ToUpper().Trim()) != null)
                //        drpPurchaseReturn.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PurchaseReturn"]).ToUpper();
                //}

                if (drpPurchaseReturn.SelectedValue == "NO")
                {
                    loadSupplierEdit("Sundry Debtors");
                }
                else
                {
                    loadSupplierEdit("Sundry Creditors");
                }

                if (ds.Tables[0].Rows[0]["SupplierID"] != null)
                {
                    sCustomer = Convert.ToString(ds.Tables[0].Rows[0]["SupplierID"]);
                    cmbCustomer.Visible = true;
                    //cmbCustomer.ClearSelection();
                    ListItem li = cmbCustomer.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                    if (li != null) li.Selected = true;
                }

                //if (ds.Tables[0].Rows[0]["cuscategory"] != null)
                //{
                //    scuscategory = Convert.ToString(ds.Tables[0].Rows[0]["cuscategory"]);
                //    drpCustomerCategoryAdd.ClearSelection();
                //    ListItem li = drpCustomerCategoryAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(scuscategory));
                //    if (li != null) li.Selected = true;
                //}

                //if (ds.Tables[0].Rows[0]["CustomerAddress"] != null)
                //    txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerAddress"]);
                //else
                //    txtAddress.Text = "";

                //if (ds.Tables[0].Rows[0]["CustomerAddress2"] != null)
                //    txtAddress2.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerAddress2"]);
                //else
                //    txtAddress2.Text = "";

                //if (ds.Tables[0].Rows[0]["CustomerIdMobile"] != null)
                //    txtCustomerId.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerIdMobile"]);
                //else
                //    txtCustomerId.Text = "";

                //if (Convert.ToString(ds.Tables[0].Rows[0]["Check1"]) == "Y")
                //    chk.Checked = true;
                //else
                //    chk.Checked = false;

                //if (ds.Tables[0].Rows[0]["CustomerAddress3"] != null)
                //    txtAddress3.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerAddress3"]);
                //else
                //    txtAddress3.Text = "";

                //if (ds.Tables[0].Rows[0]["CustomerContacts"] != null)
                //    txtCustPh.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerContacts"]);

                //if (ds.Tables[0].Rows[0]["despatchedfrom"] != null)
                //    txtdespatced.Text = Convert.ToString(ds.Tables[0].Rows[0]["despatchedfrom"]);

                if (ds.Tables[0].Rows[0]["narration2"] != null)
                    txtnarr.Text = Convert.ToString(ds.Tables[0].Rows[0]["narration2"]);

                //if (ds.Tables[0].Rows[0]["Amount"] != null)
                //    txtfixedtotal.Text = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
                //lblNet.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"]).ToString("#0.00");

                //if (Convert.ToInt32(ds.Tables[0].Rows[0]["manualNo"]) == 0)
                //{
                //    rowmanual.Visible = false;
                //}
                //else if (ds.Tables[0].Rows[0]["manualNo"] != null)
                //{
                //    txtmanual.Text = Convert.ToString(ds.Tables[0].Rows[0]["manualno"]);
                //    rowmanual.Visible = true;
                //}


                //if (ds.Tables[0].Rows[0]["manual"] != null)
                //{
                //    if (ds.Tables[0].Rows[0]["manual"] == "YES")
                //        chkboxManual.Checked = true;
                //    else
                //        chkboxManual.Checked = false;
                //}

                // krishnavelu 26 June
                //txtOtherCusName.Text = Convert.ToString(ds.Tables[0].Rows[0]["OtherCusName"]); // krishnavelu 26 June

                if (sCustomer.ToUpper() == "OTHERS")
                    txtOtherCusName.Visible = true;
                else
                    txtOtherCusName.Visible = false;

                strPaymode = ds.Tables[0].Rows[0]["Paymode"].ToString();

                //hdSeries.Value = ds.Tables[0].Rows[0]["SeriesID"].ToString();

                drpPaymode.ClearSelection();
                ListItem pLi = drpPaymode.Items.FindByValue(strPaymode.Trim());

                if (pLi != null) pLi.Selected = true;

                //if (ds.Tables[0].Rows[0]["MultiPayment"] != null)
                //    MultiPaymode = ds.Tables[0].Rows[0]["MultiPayment"].ToString();

                //if (MultiPaymode == "YES")
                //{

                //    if (drpPaymode.Items.FindByValue("4") != null)
                //    {
                //        drpPaymode.SelectedValue = "4";
                //    }
                //    else
                //    {
                //        ListItem it = new ListItem("Multiple Payment", "4");
                //        drpPaymode.Items.Add(it);
                //        drpPaymode.SelectedValue = "4";
                //    }

                //    drpPaymode.Enabled = false;
                //}
                //else
                //{
                //    //ListItem item = drpPaymode.Items.FindByValue("4");
                //    //drpPaymode.Items.Remove(item);
                //    //drpPaymode.Enabled = true;
                //}

                if (paymodeVisible(strPaymode))
                {
                    //if (ds.Tables[0].Rows[0]["CreditCardNo"] != null)
                    //    txtCreditCardNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CreditCardNo"]);
                    if (ds.Tables[0].Rows[0]["Creditor"] != null)
                    {


                        drpBankName.ClearSelection();
                        ListItem cli = drpBankName.Items.FindByText(HttpUtility.HtmlDecode(Convert.ToString(ds.Tables[0].Rows[0]["Creditor"])));

                        if (cli != null) cli.Selected = true;
                        //rvbank.Enabled = true;
                        //rvCredit.Enabled = true;
                    }
                }

                //if (drpPurchaseReturn.SelectedValue == "YES")
                //{
                //    rowReason.Visible = true;

                //    if (ds.Tables[0].Rows[0]["PurchaseReturnReason"] != null)
                //        txtPRReason.Text = Convert.ToString(ds.Tables[0].Rows[0]["PurchaseReturnReason"]);
                //    else
                //        txtPRReason.Text = "";
                //}
                //else
                //{
                //    rowReason.Visible = false;
                //}

                //if (ds.Tables[0].Rows[0]["Executive"] != null)
                //{
                //drpIncharge.ClearSelection();
                //ListItem cli = drpIncharge.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["Executive"]));

                // if (cli != null) cli.Selected = true;
                //}
                //else
                //drpIncharge.SelectedIndex = 0;



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



                if (drpIntTrans.SelectedItem.Text == "YES")
                {
                    optionmethod.SelectedIndex = 1;
                    lblVATAdd.Enabled = false;
                }
                else if (ddDeliveryNote.SelectedItem.Text == "YES")
                {
                    optionmethod.SelectedIndex = 2;
                    lblVATAdd.Enabled = true;
                }
                else if (drpPurchaseReturn.SelectedItem.Text == "YES")
                {
                    optionmethod.SelectedIndex = 3;
                    lblVATAdd.Enabled = true;
                }
                else if (ds.Tables[0].Rows[0]["ManualSales"] == "YES")
                {
                    optionmethod.SelectedIndex = 4;
                    lblVATAdd.Enabled = true;
                }
                else if (ds.Tables[0].Rows[0]["NormalSales"] == "YES")
                {
                    optionmethod.SelectedIndex = 0;
                    lblVATAdd.Enabled = true;
                }

                drpIntTrans.Enabled = false;
                ddDeliveryNote.Enabled = false;
                drpPurchaseReturn.Enabled = false;
                drpmanualsales.Enabled = false;
                drpnormalsales.Enabled = false;

                //hdContact.Value = row.Cells[13].Text;
                itemDs = formProductPR(purchaseID);

                if (cmbCustomer.SelectedItem.Value != "0")
                {
                    if (drpPaymode.SelectedValue.Trim() == "3")
                    {
                        GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                        ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
                    }
                }
                hdPrevMode.Value = drpPaymode.SelectedValue.Trim();
                hdCustCreditLimit.Value = Convert.ToString(bl.GetCustomerCreditLimit(sDataSource, cmbCustomer.SelectedItem.Value));
                pnlProduct.Visible = true;
                Session["productDs"] = itemDs;


                grvStudentDetails.DataSource = itemDs;
                grvStudentDetails.DataBind();


                for (int vLoop = 0; vLoop < grvStudentDetails.Rows.Count; vLoop++)
                {
                    DropDownList drpProduct = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpPrd");
                    DropDownList drpIncharge = (DropDownList)grvStudentDetails.Rows[vLoop].FindControl("drpIncharge");
                    TextBox txtDesc = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDesc");
                    TextBox txtRate = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRate");
                    TextBox txtTotalPrice = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTot");
                    TextBox txtStock = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtStock");
                    TextBox txtQty = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtQty");
                    TextBox txtExeComm = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtExeComm");
                    TextBox txtDisPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtDisPre");
                    TextBox txtVATPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATPre");
                    TextBox txtCSTPre = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtCSTPre");
                    TextBox txtVATAmt = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtVATAmt");
                    TextBox txtPrBefVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtPrBefVAT");
                    TextBox txtRtVAT = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtRtVAT");
                    TextBox txtTotal = (TextBox)grvStudentDetails.Rows[vLoop].FindControl("txtTotal");


                    if (itemDs.Tables[0].Rows[vLoop]["ItemCode"] != null)
                    {
                        sCustomer = Convert.ToString(itemDs.Tables[0].Rows[vLoop]["ItemCode"]);
                        drpProduct.ClearSelection();
                        ListItem liw = drpProduct.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (liw != null) liw.Selected = true;
                    }

                    //if (itemDs.Tables[0].Rows[vLoop]["executivename"] != null)
                    //{
                    //    sCustomer = Convert.ToString(itemDs.Tables[0].Rows[vLoop]["executivename"]);
                    //    drpIncharge.ClearSelection();
                    //    ListItem li = drpIncharge.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                    //    if (li != null) li.Selected = true;
                    //}

                    // drpProduct.Text = itemDs.Tables[0].Rows[vLoop]["ItemCode"].ToString();
                    txtDesc.Text = itemDs.Tables[0].Rows[vLoop]["ProductDesc"].ToString();
                    txtRate.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["PurchaseRate"].ToString()).ToString("#0.00");
                    //txtTotalPrice.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["TotalPrice"].ToString()).ToString("#0.00");
                    // txtStock.Text = itemDs.Tables[0].Rows[vLoop]["Stock"].ToString();
                    txtQty.Text = itemDs.Tables[0].Rows[vLoop]["Qty"].ToString();
                    //txtExeComm.Text = itemDs.Tables[0].Rows[vLoop]["ExecCharge"].ToString();
                    txtDisPre.Text = itemDs.Tables[0].Rows[vLoop]["Discount"].ToString();
                    txtVATPre.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["Vat"].ToString()).ToString("#0.00");
                    txtCSTPre.Text = itemDs.Tables[0].Rows[vLoop]["CST"].ToString();
                    // txtPrBefVAT.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["PriceBeforeVATAmt"].ToString()).ToString("#0.00");
                    txtVATAmt.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["Vatamount"].ToString()).ToString("#0.00");
                    //txtRtVAT.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["Subtotal"].ToString()).ToString("#0.00");
                    // txtTotal.Text = Convert.ToDouble(itemDs.Tables[0].Rows[vLoop]["Totalmrp"].ToString()).ToString("#0.00");
                }



                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
                //GrdViewItems.DataSource = itemDs;
                //GrdViewItems.DataBind();
                //calcSum();

                errPanel.Visible = false;
                ErrMsg.Text = "";

                cmdUpdateProduct.Enabled = false;
                cmdSaveProduct.Enabled = true;
                //cmdCancelProduct.Visible = false;
                cmdUpdateProduct.Visible = false;
                //Label3.Visible = false;
                cmdSaveProduct.Visible = true;
                //Label2.Visible = true;
                //cmdCancelProduct.Visible = false;
                

                if (drpPaymode.SelectedValue.Trim() == "3")
                {
                    divMultiPayment.Visible = true;
                    divAddMPayments.Visible = false;
                    divListMPayments.Visible = true;

                    GrdViewReceipt.DataSource = bl.ListReceiptsForBillNoOrder(lblBillNo.Text);
                    GrdViewReceipt.DataBind();
                }
                else if (drpPaymode.SelectedValue.Trim() == "4")
                {
                    drpPaymode.Enabled = true;
                    txtCashAmount.Text = "";
                    ddBank1.SelectedValue = "0";
                    txtAmount1.Text = "";
                    txtCCard1.Text = "";
                    ddBank2.SelectedValue = "0";
                    txtAmount2.Text = "";
                    txtCCard2.Text = "";
                    ddBank3.SelectedValue = "0";
                    txtAmount3.Text = "";
                    txtCCard3.Text = "";
                    //ddBank1.SelectedItem.Text = "Select Bank";
                    //ddBank2.SelectedItem.Text = "Select Bank";
                    //ddBank3.SelectedItem.Text = "Select Bank";

                    divMultiPayment.Visible = true;
                    divAddMPayments.Visible = true;
                    divListMPayments.Visible = true;
                    //GrdViewReceipt.DataSource = bl.ListReceiptsForBillNoOrder(lblBillNo.Text);
                    //GrdViewReceipt.DataBind();

                    GrdViewReceipt.Visible = false;

                    DataSet receiptData = new DataSet();
                    receiptData = bl.ListReceiptsForBillNoOrder(lblBillNo.Text);
                    string sDebitor = string.Empty;

                    int gg = 1;
                    if (receiptData.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in receiptData.Tables[0].Rows)
                        {
                            if (dr["Paymode"].ToString() == "Cash")
                            {
                                txtCashAmount.Text = dr["Amount"].ToString();
                                TextBox5.Text = dr["SFRefNo"].ToString();
                            }
                            else if (dr["Paymode"].ToString() == "Cheque")
                            {
                                if (gg == 1)
                                {
                                    sDebitor = Convert.ToString(dr["DebtorId"]);
                                    ddBank1.ClearSelection();
                                    ListItem lli = ddBank1.Items.FindByValue(sDebitor);
                                    if (lli != null) lli.Selected = true;

                                    txtRefNo1.Text = dr["SFRefNo"].ToString();
                                    txtAmount1.Text = dr["Amount"].ToString();
                                    txtCCard1.Text = dr["ChequeNo"].ToString();
                                }
                                if (gg == 2)
                                {

                                    sDebitor = Convert.ToString(dr["DebtorId"]);
                                    ddBank2.ClearSelection();
                                    ListItem gli = ddBank2.Items.FindByValue(sDebitor);
                                    if (gli != null) gli.Selected = true;

                                    txtAmount2.Text = dr["Amount"].ToString();
                                    txtCCard2.Text = dr["ChequeNo"].ToString();
                                    txtRefNo2.Text = dr["SFRefNo"].ToString();
                                }
                                if (gg == 3)
                                {

                                    sDebitor = Convert.ToString(dr["DebtorId"]);
                                    ddBank3.ClearSelection();
                                    ListItem wli = ddBank3.Items.FindByValue(sDebitor);
                                    if (wli != null) wli.Selected = true;

                                    txtAmount3.Text = dr["Amount"].ToString();
                                    txtCCard3.Text = dr["ChequeNo"].ToString();
                                    txtRefNo3.Text = dr["SFRefNo"].ToString();
                                }
                                gg = gg + 1;
                            }

                        }

                        var total = 0.0;

                        if (txtAmount1.Text != "")
                            total += double.Parse(txtAmount1.Text);
                        if (txtAmount2.Text != "")
                            total += double.Parse(txtAmount2.Text);
                        if (txtAmount3.Text != "")
                            total += double.Parse(txtAmount3.Text);
                        if (txtCashAmount.Text != "")
                            total += double.Parse(txtCashAmount.Text);

                        lblReceivedTotal.Text = total.ToString();

                    }
                    else
                    {
                    }
                }
                else
                {
                    divMultiPayment.Visible = false;
                    divAddMPayments.Visible = false;
                    divListMPayments.Visible = false;
                }

                hdPrevSalesTotal.Value = lblNet.Text;

                if (bl.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    cmdSaveProduct.Enabled = false;
                    if (GrdViewItems.Columns[14] != null)
                        GrdViewItems.Columns[14].Visible = false;
                    if (GrdViewItems.Columns[15] != null)
                        GrdViewItems.Columns[15].Visible = false;
                    cmdSave.Enabled = false;
                    cmdDelete.Enabled = false;
                    cmdUpdate.Enabled = false;
                    //lnkBtnAdd.Visible = false;
                    AddNewProd.Enabled = false;
                }
                updatePnlSales.Update();
            }          
        }
    }
}

