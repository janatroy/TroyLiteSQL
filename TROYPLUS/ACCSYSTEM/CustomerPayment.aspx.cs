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
using SMSLibrary;

public partial class CustomerPayment : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

        try
        {
            if (!Page.IsPostBack)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                string connStr = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                CheckSMSRequired();

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    GrdViewPayment.Columns[8].Visible = false;
                    GrdViewPayment.Columns[7].Visible = false;
                }

                GrdViewPayment.PageSize = 11;

                GrdViewPayment.Attributes.Add("bordercolor", "Black");

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveAdd(usernam, "CUSTPMT"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New item ";
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

                //loadCustomer();

            }
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
            txtSearch.Text = "";
            ddCriteria.SelectedIndex = 0;

            //if (ddCriteria.SelectedItem.Text == "Transaction Date")
            //{
            //    //txtdate.EnableViewState = 1;
            //    txtdatet.Enabled = true;
            //}
            //else
            //{
            //    txtdatet.Enabled = false;
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddBanks_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadChequeNoEdit(Convert.ToInt32(ddBanks.SelectedItem.Value));
        //ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "AdvancedAdd('" + ((HtmlTable)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("tblBankAdd")) + "');", true);
    }

    protected void ddBanksAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadChequeNo(Convert.ToInt32(ddBanksAdd.SelectedItem.Value));
        //ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "AdvancedAdd('" + ((HtmlTable)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("tblBankAdd")) + "');", true);
    }

    protected void drpBranchAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        string connection = Request.Cookies["Company"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListSundryDebtorsExceptIsActive(connection, ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).SelectedValue);
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).Items.Clear();
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).Items.Add(new ListItem("Select Customer", "0"));
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataSource = ds;
        
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataTextField = "LedgerName";
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataValueField = "LedgerID";
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataBind();

        ((UpdatePanel)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("UpdatePanel1")).Update();
        ((UpdatePanel)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("UpdatePanel4")).Update();
        ((UpdatePanel)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("UpdatePanel6")).Update();
    }

    private void loadChequeNo(int bnkId)
    {

        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cmbChequeNo1")).Items.Clear();
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cmbChequeNo1")).Items.Add(new ListItem("Select ChequeNo", "0"));
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        //ds = bl.ListChequeNo(bnkId);
        ds = bl.ListChequeNosBank(sDataSource,Convert.ToString(bnkId));
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cmbChequeNo1")).DataSource = ds;
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cmbChequeNo1")).DataBind();
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cmbChequeNo1")).DataTextField = "ChequeNo";
        ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cmbChequeNo1")).DataValueField = "ChequeNo";
        ((UpdatePanel)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("UpdatePanel6")).Update();
    }

    private void loadChequeNoEdit(int bnkId)
    {

        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).Items.Clear();
        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).Items.Add(new ListItem("Select ChequeNo", "0"));
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        //ds = bl.ListChequeNo(bnkId);
        ds = bl.ListChequeNosBank(sDataSource, Convert.ToString(bnkId));
       

        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).DataSource = ds;
        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).DataBind();
        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).DataTextField = "ChequeNo";
        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).DataValueField = "ChequeNo";

        //ListItem clie = new ListItem(ds.Tables[0].Rows[0]["ChequeNo"].ToString(), "0");
        //((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).Items.Insert(((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).Items.Count - 1, clie);


        //((UpdatePanel)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("UpdatePanel2")).Update();



        //ListItem clie = new ListItem(ds.Tables[0].Rows[0]["ChequeNo"].ToString(), "0");
        //cmbChequeNo.Items.Insert(cmbChequeNo.Items.Count - 1, clie);
        //clie = cmbChequeNo.Items.FindByText(ds.Tables[0].Rows[0]["ChequeNo"].ToString());


    }


    private void loadBranchEdit()
    {

       
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        string connection = Request.Cookies["Company"].Value;

        ds = bl.ListSundryDebitors(connection, ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("drpBranch")).SelectedValue);
        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).Items.Clear();
        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).Items.Add(new ListItem("Select Customer", "0"));
        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).DataSource = ds;

        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).DataTextField = "LedgerName";
        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).DataValueField = "LedgerID";
        ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).DataBind();
   

    }

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

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "OWNERMOB")
                {
                    Session["OWNERMOB"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }

            }
        }
    }

    protected override void OnInit(EventArgs e)
     {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
        GridSource.SelectParameters.Add(new CookieParameter("branch", "Branch"));
    }

    protected void btnpay_Click(object sender, EventArgs e)
    {
        try
        {
            string CustPay = "CustPay";
            Response.Write("<script language='javascript'> window.open('ReportExcelPayments.aspx?ID=" + CustPay + "' , 'window','toolbar=no,status=no,menu=no,location=no,height=320,width=700,left=320,top=220 ,resizable=yes, scrollbars=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow Row = GrdViewPayment.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();
            string recondate = Row.Cells[2].Text;
            hdPayment.Value = Convert.ToString(GrdViewPayment.SelectedDataKey.Value);
            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.ReadOnly);
                return;

            }
            else
            {
                frmViewAdd.Visible = true;
                //GrdViewPayment.Visible = false;
                ////MyAccordion.Visible = false;
                frmViewAdd.DataBind();
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                //lnkBtnAdd.Visible = false;
                ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //bool IsFutureDate(DateTime refDate)
    //{
    //    DateTime today = DateTime.Today;
    //    return (refDate.Date != today) && (refDate > today);
    //}

    //protected void txtTransDateAdd_TextChanged(object sender, EventArgs e)
    //{
    //    string refDate = string.Empty;
    //    refDate = txtTransDateAdd.Text;
    //    ViewState.Add("TransDate", refDate);

    //    UpdatePanel upp = ((UpdatePanel)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("UP1"));
    //    if (IsFutureDate(Convert.ToDateTime(refDate)))
    //    {
    //        hddatecheck.Value = "1";
    //        upp.Update();
    //        return;
    //    }
    //    else
    //    {
    //        hddatecheck.Value = "0";
    //        upp.Update();
    //    }
    //}

    protected void GetRefNo_Click(object sender, EventArgs e)
    {
        //cmdSaveContact.Visible = true;
        //cmdUpdateContact.Visible = false;
        //updatePnlContact.Update();

        //txtContactedDate.Text = string.Empty;

        //ModalPopupContact.Show();
        //ModalPopupExtender1.Show();
    }

    protected void cmdCancelContact_Click(object sender, EventArgs e)
    {
        //ModalPopupContact.Hide();
        //ModalPopupExtender1.Show();
    }


    protected void cmdUpdateContact_Click(object sender, EventArgs e)
    {
        //ModalPopupContact.Hide();
        //ModalPopupExtender1.Show();

        //string sCustomer = string.Empty;
        //string strPaymode = string.Empty;
        //string strPaymodef = string.Empty;

        //try
        //{
        //    BusinessLogic bl = new BusinessLogic(sDataSource);
        //    //TextBox testcheck = (TextBox)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtoldref");
        //    int iRefID = Convert.ToInt32(txtContactedDate.Text);

        //    string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        //    DataSet customerDs = bl.GetPaymentForRef(connection, iRefID);
        //    string address = string.Empty;

        //    if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
        //    {

        //        if (customerDs.Tables[0].Rows[0]["RefNo"] != null)
        //            txtRefNoAdd.Text = customerDs.Tables[0].Rows[0]["RefNo"].ToString() + Environment.NewLine;

        //        if (customerDs.Tables[0].Rows[0]["TransDate"] != null)
        //        {
        //            string aa = customerDs.Tables[0].Rows[0]["TransDate"].ToString().ToUpper().Trim();
        //            string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
        //            txtTransDateAdd.Text = dtaa + Environment.NewLine;
        //        }

        //        if (customerDs.Tables[0].Rows[0]["DebtorID"] != null)
        //        {
        //            //ComboBox2Add.SelectedItem.Value = customerDs.Tables[0].Rows[0]["DebtorID"].ToString() + Environment.NewLine;
        //            //ComboBox2Add.Text = customerDs.Tables[0].Rows[0]["Debi"].ToString() + Environment.NewLine;

        //            sCustomer = Convert.ToString(customerDs.Tables[0].Rows[0]["DebtorID"]);
        //            ComboBox2Add.ClearSelection();
        //            ListItem li = ComboBox2Add.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        //            if (li != null) li.Selected = true;
        //        }

        //        if (customerDs.Tables[0].Rows[0]["Amount"] != null)
        //            txtAmountAdd.Text = customerDs.Tables[0].Rows[0]["Amount"].ToString() + Environment.NewLine;

        //        if (customerDs.Tables[0].Rows[0]["Paymode"] != null)
        //        {
        //            //if (customerDs.Tables[0].Rows[0]["Paymode"].ToString() == "Cash")
        //            //    chkPayToAdd.SelectedItem.Value = "Cash" + Environment.NewLine;
        //            //else
        //            //    chkPayToAdd.SelectedItem.Value = "Cheque" + Environment.NewLine;

        //            strPaymodef = customerDs.Tables[0].Rows[0]["Paymode"].ToString();
        //            chkPayToAdd.ClearSelection();
        //            ListItem piLi = chkPayToAdd.Items.FindByValue(strPaymodef.Trim());
        //            if (piLi != null) piLi.Selected = true;




        //        }

        //        if (customerDs.Tables[0].Rows[0]["Narration"] != null)
        //            txtNarrationAdd.Text = customerDs.Tables[0].Rows[0]["Narration"].ToString() + Environment.NewLine;

        //        if (customerDs.Tables[0].Rows[0]["CreditorId"] != null)
        //        {
        //            //ddBanksAdd.SelectedItem.Value = customerDs.Tables[0].Rows[0]["CreditorId"] + Environment.NewLine;

        //            strPaymode = Convert.ToString(customerDs.Tables[0].Rows[0]["CreditorId"]);
        //            ddBanksAdd.ClearSelection();
        //            ListItem pLi = ddBanksAdd.Items.FindByValue(strPaymode.Trim());
        //            if (pLi != null) pLi.Selected = true;
        //        }

        //        if (customerDs.Tables[0].Rows[0]["ChequeNo"] != null)
        //            txtChequeNoAdd.Text = customerDs.Tables[0].Rows[0]["ChequeNo"].ToString() + Environment.NewLine;

        //        if (customerDs.Tables[0].Rows[0]["BillNo"] != null)
        //            txtBillAdd.Text = customerDs.Tables[0].Rows[0]["BillNo"].ToString() + Environment.NewLine;


        //        ModalPopupExtender1.Show();
        //        //if (strPaymodef == "Cheque")
        //        //{
        //        //    //RadioButtonList chk = (RadioButtonList)sender;
        //        //    if (strPaymodef == "Cheque")
        //        //    {
        //        //        //Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd");
        //        //        PanelBankAdd.Visible = true;
        //        //    }
        //        //}

        //        //chkPayToAdd.SelectedIndexChanged += new EventHandler(chkPayToAdd_SelectedIndexChanged);

        //        //chkPayToAdd_SelectedIndexChanged(this.chkPayToAdd, new EventArgs());


        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('RefNo Not Found');", true);
        //        ModalPopupContact.Show();
        //    }

        //}
        //catch (Exception ex)
        //{

        //}
    }

    protected void cmdSaveContact_Click(object sender, EventArgs e)
    {

        //ModalPopupContact.Hide();
        //ModalPopupExtender1.Show();

        //string sCustomer = string.Empty;
        //string strPaymode = string.Empty;
        //string strPaymodef = string.Empty;

        //try
        //{
        //    BusinessLogic bl = new BusinessLogic(sDataSource);
        //    //TextBox testcheck = (TextBox)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtoldref");
        //    int iRefID = Convert.ToInt32(txtContactedDate.Text);

        //    string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        //    DataSet customerDs = bl.GetPaymentForRef(connection, iRefID);
        //    string address = string.Empty;

        //    if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
        //    {

        //        if (customerDs.Tables[0].Rows[0]["RefNo"] != null)
        //            txtRefNoAdd.Text = customerDs.Tables[0].Rows[0]["RefNo"].ToString() + Environment.NewLine;

        //        if (customerDs.Tables[0].Rows[0]["TransDate"] != null)
        //        {
        //            string aa = customerDs.Tables[0].Rows[0]["TransDate"].ToString().ToUpper().Trim();
        //            string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
        //            txtTransDateAdd.Text = dtaa + Environment.NewLine;
        //        }

        //        if (customerDs.Tables[0].Rows[0]["DebtorID"] != null)
        //        {
        //            //ComboBox2Add.SelectedItem.Value = customerDs.Tables[0].Rows[0]["DebtorID"].ToString() + Environment.NewLine;
        //            //ComboBox2Add.Text = customerDs.Tables[0].Rows[0]["Debi"].ToString() + Environment.NewLine;

        //            sCustomer = Convert.ToString(customerDs.Tables[0].Rows[0]["DebtorID"]);
        //            ComboBox2Add.ClearSelection();
        //            ListItem li = ComboBox2Add.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        //            if (li != null) li.Selected = true;
        //        }

        //        if (customerDs.Tables[0].Rows[0]["Amount"] != null)
        //            txtAmountAdd.Text = customerDs.Tables[0].Rows[0]["Amount"].ToString() + Environment.NewLine;

        //        if (customerDs.Tables[0].Rows[0]["Paymode"] != null)
        //        {
        //            //if (customerDs.Tables[0].Rows[0]["Paymode"].ToString() == "Cash")
        //            //    chkPayToAdd.SelectedItem.Value = "Cash" + Environment.NewLine;
        //            //else
        //            //    chkPayToAdd.SelectedItem.Value = "Cheque" + Environment.NewLine;

        //            strPaymodef = customerDs.Tables[0].Rows[0]["Paymode"].ToString();
        //            chkPayToAdd.ClearSelection();
        //            ListItem piLi = chkPayToAdd.Items.FindByValue(strPaymodef.Trim());
        //            if (piLi != null) piLi.Selected = true;




        //        }

        //        if (customerDs.Tables[0].Rows[0]["Narration"] != null)
        //            txtNarrationAdd.Text = customerDs.Tables[0].Rows[0]["Narration"].ToString() + Environment.NewLine;

        //        if (customerDs.Tables[0].Rows[0]["CreditorId"] != null)
        //        {
        //            //ddBanksAdd.SelectedItem.Value = customerDs.Tables[0].Rows[0]["CreditorId"] + Environment.NewLine;

        //            strPaymode = Convert.ToString(customerDs.Tables[0].Rows[0]["CreditorId"]);
        //            ddBanksAdd.ClearSelection();
        //            ListItem pLi = ddBanksAdd.Items.FindByValue(strPaymode.Trim());
        //            if (pLi != null) pLi.Selected = true;
        //        }

        //        if (customerDs.Tables[0].Rows[0]["ChequeNo"] != null)
        //            txtChequeNoAdd.Text = customerDs.Tables[0].Rows[0]["ChequeNo"].ToString() + Environment.NewLine;

        //        if (customerDs.Tables[0].Rows[0]["BillNo"] != null)
        //            txtBillAdd.Text = customerDs.Tables[0].Rows[0]["BillNo"].ToString() + Environment.NewLine;


        //        ModalPopupExtender1.Show();
        //        //if (strPaymodef == "Cheque")
        //        //{
        //        //    //RadioButtonList chk = (RadioButtonList)sender;
        //        //    if (strPaymodef == "Cheque")
        //        //    {
        //        //        //Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd");
        //        //        PanelBankAdd.Visible = true;
        //        //    }
        //        //}

        //        //chkPayToAdd.SelectedIndexChanged += new EventHandler(chkPayToAdd_SelectedIndexChanged);

        //        //chkPayToAdd_SelectedIndexChanged(this.chkPayToAdd, new EventArgs());


        //    }
        //    else
        //    {

        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('RefNo Not Found');", true);
        //        ModalPopupContact.Show();
        //    }

        //}
        //catch (Exception ex)
        //{

        //}

    }

    protected void GrdViewPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    frmViewAdd.Visible = true;
        //    frmViewAdd.ChangeMode(FormViewMode.Edit);
        //    GrdViewPayment.Columns[8].Visible = false;
        //    lnkBtnAdd.Visible = false;

        //    //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
        //    //    Accordion1.SelectedIndex = 1;
        //}
    }
    protected void GrdViewPayment_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewPayment, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gridView = (GridView)sender;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                int cellIndex = -1;
                foreach (DataControlField field in gridView.Columns)
                {
                    if (field.SortExpression == gridView.SortExpression)
                    {
                        cellIndex = gridView.Columns.IndexOf(field);
                    }
                    else if (field.SortExpression != "")
                    {
                        e.Row.Cells[gridView.Columns.IndexOf(field)].CssClass = "headerstyle";
                    }

                }

                if (cellIndex > -1)
                {
                    //  this is a header row,
                    //  set the sort style
                    e.Row.Cells[cellIndex].CssClass =
                        gridView.SortDirection == SortDirection.Ascending
                        ? "sortascheaderstyle" : "sortdescheaderstyle";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEditNote(connection, usernam, "CUSTPMT"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDeleteNote(connection, usernam, "CUSTPMT"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveViewNote(connection, usernam, "CUSTPMT"))
                {
                    ((Image)e.Row.FindControl("lnkprint")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = true;
                }
                //else
                //{
                //    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = false;
                //}
            }
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
            //if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
            // {
            //     ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
            //     return;
            // }

            frmViewAdd.ChangeMode(FormViewMode.Insert);
            frmViewAdd.Visible = true;
            //GrdViewPayment.Visible = false;
            //MyAccordion.Visible = false;
            //if (frmViewAdd.CurrentMode == FormViewMode.Insert)
            //{
            //    lnkBtnAdd.Visible = false;
            //}
            ModalPopupExtender1.Show();

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;

            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet dst = new DataSet();
            dst = bl.ListBranch(connection, usernam);
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).Items.Clear();
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).Items.Add(new ListItem("Select Branch", "0"));
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).DataSource = dst;
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).DataTextField = "BranchName";
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).DataValueField = "BranchCode";
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).DataBind();

            string sCustomer = string.Empty;

            DataSet ds = bl.GetBranch(connection, usernam);

            sCustomer = Convert.ToString(ds.Tables[0].Rows[0]["DefaultBranchCode"]);
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).ClearSelection();
            ListItem li = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
            if (li != null) li.Selected = true;

            if (ds.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
            {
                ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).Enabled = true;
            }
            else
            {
                ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).Enabled = false;
            }

            
            DataSet dstt = new DataSet();
            dstt = bl.ListSundryDebtorsExceptIsActive(connection, ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).SelectedValue);
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).Items.Clear();
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).Items.Add(new ListItem("Select Customer", "0"));
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataSource = dstt;

            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataTextField = "LedgerName";
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataValueField = "LedgerID";
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataBind();

            ((UpdatePanel)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("UpdatePanel1")).Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewPayment.Visible = true;
            //MyAccordion.Visible = true;
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;

           // frmViewAdd.ChangeMode(FormViewMode.Insert);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }

    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setUpdateParameters(e);

            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bll = new BusinessLogic();
            string recondate = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text;
            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to Update Payment with this Date. Please contact Supervisor.');", true);
                return;
            }

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;


            salestype = "Customer Payment";
            ScreenName = "Customer Payment";

            string emailcontent = string.Empty;
            BusinessLogic bl = new BusinessLogic();

            bool mobile = false;
            bool Email = false;
            string emailsubject = string.Empty;

            string usernam = Request.Cookies["LoggedUserName"].Value;
            if (hdEmailRequired.Value == "YES")
            {
                DataSet dsd = bl.GetLedgerInfoForId(connection, Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedValue));
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
                                else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier"))
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

                                int index132 = emailcontent.IndexOf("@Narration");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtNarration")).Text;
                                if (index132 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = emailcontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = emailcontent.IndexOf("@Date");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text;
                                if (index2 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                }

                                int index = emailcontent.IndexOf("@Ledger");
                                body = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedItem.Text;
                                if (index >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index, 7).Insert(index, body);
                                }

                                int index1 = emailcontent.IndexOf("@Amount");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtAmount")).Text;
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
            string UserID = Page.User.Identity.Name;

            string smscontent = string.Empty;
            if (hdSMSRequired.Value == "YES")
            {
                DataSet dsd = bl.GetLedgerInfoForId(connection, Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedValue));
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
                                else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier"))
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

                                int index132 = smscontent.IndexOf("@Narration");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtNarration")).Text;
                                if (index132 >= 0)
                                {
                                    smscontent = smscontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = smscontent.IndexOf("@Date");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text;
                                if (index2 >= 0)
                                {
                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                }

                                int index = smscontent.IndexOf("@Ledger");
                                body = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedItem.Text;
                                if (index >= 0)
                                {
                                    smscontent = smscontent.Remove(index, 7).Insert(index, body);
                                }

                                int index1 = smscontent.IndexOf("@Amount");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtAmount")).Text;
                                if (index1 >= 0)
                                {
                                    smscontent = smscontent.Remove(index1, 7).Insert(index1, body);
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

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (((RadioButtonList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkPayToAdd")).SelectedValue == "Cheque")
            {
                int ChequeNo =0;
                ChequeNo = Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cmbChequeNo1")).SelectedValue);
                int bankname = 0;
                bankname = Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ddBanksAdd")).SelectedValue);

                if ((ChequeNo == 0) && (bankname == 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Name And Cheque No Mandatory');", true);
                    ModalPopupExtender1.Show();
                    return;
                }
                else if (ChequeNo == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No Mandatory');", true);
                    ModalPopupExtender1.Show();
                    return;
                }
                else if (bankname == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Name Mandatory');", true);
                    ModalPopupExtender1.Show();
                    return;
                }
            }

            this.setInsertParameters(e);

            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bll = new BusinessLogic();
            string recondate = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text;

            ViewState.Add("TransDate", recondate);

            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to Insert Payment with this Date. Please contact Supervisor.');", true);
                return;
            }

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;


            salestype = "Customer Payment";
            ScreenName = "Customer Payment";

            string emailcontent = string.Empty;
            BusinessLogic bl = new BusinessLogic();

            bool mobile = false;
            bool Email = false;
            string emailsubject = string.Empty;

            string usernam = Request.Cookies["LoggedUserName"].Value;
            if (hdEmailRequired.Value == "YES")
            {
                DataSet dsd = bl.GetLedgerInfoForId(connection, Convert.ToInt32(ComboBox2Add.SelectedValue));
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
                                else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier"))
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

                                int index132 = emailcontent.IndexOf("@Narration");
                                body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtNarrationAdd")).Text;
                                if (index132 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = emailcontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = emailcontent.IndexOf("@Date");
                                body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text;
                                if (index2 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                }

                                int index = emailcontent.IndexOf("@Ledger");
                                body = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).SelectedItem.Text;
                                if (index >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index, 7).Insert(index, body);
                                }

                                int index1 = emailcontent.IndexOf("@Amount");
                                body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtAmountAdd")).Text;
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
            string UserID = Page.User.Identity.Name;

            string smscontent = string.Empty;
            if (hdSMSRequired.Value == "YES")
            {
                DataSet dsd = bl.GetLedgerInfoForId(connection, Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("ComboBox2Add")).SelectedValue));
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
                                else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier"))
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

                                int index132 = smscontent.IndexOf("@Narration");
                                body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtNarrationAdd")).Text;
                                if (index132 >= 0)
                                {
                                    smscontent = smscontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = smscontent.IndexOf("@Date");
                                body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text;
                                if (index2 >= 0)
                                {
                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);

                                }
                                int index = smscontent.IndexOf("@Ledger");
                                body = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).SelectedItem.Text;
                                if (index >= 0)
                                {
                                    smscontent = smscontent.Remove(index, 7).Insert(index, body);
                                }

                                int index1 = smscontent.IndexOf("@Amount");
                                body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtAmountAdd")).Text;
                                if (index1 >= 0)
                                {
                                    smscontent = smscontent.Remove(index1, 7).Insert(index1, body);
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


        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void frmViewAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void frmViewAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
                System.Threading.Thread.Sleep(1000);
                GrdViewPayment.DataBind();
                //MyAccordion.Visible = true;
                GrdViewPayment.Visible = true;
            }
            else
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('You are not allowed to Update this record. Please contact Supervisor.');");

                if (e.Exception.InnerException != null)
                {
                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") == -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);
                }

                e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                lnkBtnAdd.Visible = false;
                frmViewAdd.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
                System.Threading.Thread.Sleep(1000);
                GrdViewPayment.DataBind();
                //MyAccordion.Visible = true;
                GrdViewPayment.Visible = true;
            }
            else
            {
                e.KeepInEditMode = true;

                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to Update this record. Please contact Supervisor.');");

                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") == -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);

                    e.ExceptionHandled = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (((RadioButtonList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkPayTo")).SelectedValue == "Cheque")
            {
                int ChequeNo = 0;
                ChequeNo = Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).SelectedValue);
                int bankname = 0;
                bankname = Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ddBanks")).SelectedValue);

                if ((ChequeNo == 0) && (bankname == 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Name And Cheque No Mandatory');", true);
                    ModalPopupExtender1.Show();
                    return;
                }
                else if (ChequeNo == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No Mandatory');", true);
                    ModalPopupExtender1.Show();
                    return;
                }
                else if (bankname == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Name Mandatory');", true);
                    ModalPopupExtender1.Show();
                    return;
                }
            }

            if (((RadioButtonList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkPayTo")).SelectedValue == "Cheque")
            {
                ((CompareValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cvBank")).Enabled = true;
                ((RequiredFieldValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("rvChequeNo")).Enabled = true;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("tblBank");
                table.Attributes.Add("class", "AdvancedSearch");

            }
            else
            {
                ((CompareValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cvBank")).Enabled = false;
                ((RequiredFieldValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("rvChequeNo")).Enabled = false;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("tblBank");
                table.Attributes.Add("class", "hidden");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewPayment.Visible = true;
            frmViewAdd.Visible = false;
            lnkBtnAdd.Visible = true;
            //MyAccordion.Visible = true;

            frmViewAdd.ChangeMode(FormViewMode.Edit);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")) != null)
            e.InputParameters["DebitorID"] = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtRefNo")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text);

        if (((RadioButtonList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkPayTo")).SelectedValue == "Cash")
        {
            e.InputParameters["CreditorID"] = "1";
            e.InputParameters["Paymode"] = "Cash";
            e.InputParameters["ChequeNo"] = "";
        }
        else if (((RadioButtonList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkPayTo")).SelectedValue == "Cheque")
        {
            Panel bnkPanel = (Panel)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank");

            if (bnkPanel != null)
            {
                e.InputParameters["CreditorID"] = ((DropDownList)bnkPanel.FindControl("ddBanks")).SelectedValue;
                e.InputParameters["Paymode"] = "Cheque";
            }

            //if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtChequeNo")).Text != "")
            //    e.InputParameters["ChequeNo"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtChequeNo")).Text;

            if (((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")) != null)
                e.InputParameters["ChequeNo"] = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).SelectedValue;

        }

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtNarration")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtNarration")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditAddTab").FindControl("txtBill")).Text != "")
            e.InputParameters["Billno"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditAddTab").FindControl("txtBill")).Text;

        e.InputParameters["VoucherType"] = "Payment";

        e.InputParameters["TransNo"] = Convert.ToInt32(GrdViewPayment.SelectedDataKey.Value);

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

        if (((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("drpBranch")) != null)
            e.InputParameters["BranchCode"] = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("drpBranch")).SelectedValue;

    }
    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")) != null)
            e.InputParameters["DebitorID"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtRefNoAdd")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtRefNoAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text);

        if (((RadioButtonList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkPayToAdd")).SelectedValue == "Cash")
        {
            e.InputParameters["CreditorID"] = "1";
            e.InputParameters["PaymentMode"] = "Cash";
        }
        else if (((RadioButtonList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkPayToAdd")).SelectedValue == "Cheque")
        {
            Panel bnkPanel = (Panel)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd");

            if (bnkPanel != null)
            {
                e.InputParameters["CreditorID"] = ((DropDownList)bnkPanel.FindControl("ddBanksAdd")).SelectedValue;
                e.InputParameters["PaymentMode"] = "Cheque";
            }
        }

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtAmountAdd")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtAmountAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtNarrationAdd")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtNarrationAdd")).Text;

        //if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtChequeNoAdd")).Text != "")
        //    e.InputParameters["ChequeNo"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtChequeNoAdd")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cmbChequeNo1")) != null)
            e.InputParameters["ChequeNo"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cmbChequeNo1")).SelectedValue;


        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsAddTab").FindControl("txtBillAdd")).Text != "")
            e.InputParameters["Billno"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsAddTab").FindControl("txtBillAdd")).Text;
        else
            e.InputParameters["Billno"] = string.Empty;
        
        e.InputParameters["VoucherType"] = "Payment";

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")) != null)
            e.InputParameters["BranchCode"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).SelectedValue;

    }

    protected void ComboBox2_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string debtorID = ((DataRowView)frmV.DataItem)["DebtorID"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(debtorID);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddBanks_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string creditorID = ((DataRowView)frmV.DataItem)["CreditorID"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(creditorID);
                if (li != null) li.Selected = true;

                string dfg = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ddBanks")).SelectedValue;
                loadChequeNoEdit(Convert.ToInt32(dfg)); 

            }

           
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkPayToAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            if (chk.SelectedItem.Text == "Cheque")
            {
                HtmlTable test = (HtmlTable)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("tblBankAdd");
                test.Visible = true;
                PanelBankAdd.Visible = true;
                tblBankAdd.Visible = true;
                UpdatePanel test1 = (UpdatePanel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("UpdatePanel2");
                UpdatePanel test2 = (UpdatePanel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("UpdatePanel4");
                UpdatePanel test3 = (UpdatePanel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("UpdatePanel5");
                test1.Update();
                test2.Update();
                test3.Update();               
            }
            else
            {
                HtmlTable test = (HtmlTable)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("tblBankAdd");
                test.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void chkPayTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            if (chk.SelectedItem.Text == "Cheque")
            {
                Panel test = (Panel)frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank");

                if (test != null)
                    test.Visible = true;
            }
            else
            {
                Panel test = (Panel)frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank");

                if (test != null)
                    test.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkPayTo_DataBound(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)chk.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string paymode = ((DataRowView)frmV.DataItem)["paymode"].ToString();
                chk.ClearSelection();

                ListItem li = chk.Items.FindByValue(paymode);
                if (li != null) li.Selected = true;

            }

            if (chk.SelectedItem != null)
            {
                if (chk.SelectedItem.Text == "Cheque")
                {
                    //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                    HtmlTable table = (HtmlTable)((Panel)frmV.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank")).FindControl("tblBank");

                    if (table != null)
                        table.Attributes.Add("class", "AdvancedSearch");
                }
                else
                {
                    if (frmV.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank") != null)
                    {
                        HtmlTable table = (HtmlTable)frmV.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank").FindControl("tblBank");

                        if (table != null)
                            table.Attributes.Add("class", "hidden");
                    }
                }
            }
            else
            {
                //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                //test.Visible = false;
                HtmlTable table = (HtmlTable)frmV.FindControl("tblBank");
                table.Attributes.Add("class", "hidden");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkPayToAdd_DataBound(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)chk.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string paymode = ((DataRowView)frmV.DataItem)["paymode"].ToString();
                chk.ClearSelection();

                ListItem li = chk.Items.FindByValue(paymode);
                if (li != null) li.Selected = true;

            }

            if (chk.SelectedItem != null)
            {
                if (chk.SelectedItem.Text == "Cheque")
                {
                    //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                    HtmlTable table = (HtmlTable)((Panel)frmV.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd")).FindControl("tblBankAdd");

                    if (table != null)
                        table.Attributes.Add("class", "AdvancedSearch");
                }
                else
                {
                    HtmlTable table = (HtmlTable)frmV.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd").FindControl("tblBankAdd");

                    if (table != null)
                        table.Attributes.Add("class", "hidden");
                }
            }
            else
            {
                //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                //test.Visible = false;
                HtmlTable table = (HtmlTable)frmV.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd").FindControl("tblBankAdd");

                if (table != null)
                    table.Attributes.Add("class", "hidden");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmViewAdd_ItemCreated(object sender, EventArgs e)
    {
        try
        {
            if (this.frmViewAdd.FindControl("tablInsert") != null)
            {
                if (this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd") != null)
                {

                    if (ViewState["TransDate"] == null)
                    {
                        DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                        string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                        ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text = dtaa;

                        //((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");

                    }
                    else
                        ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text = ViewState["TransDate"].ToString();

                }

                if (this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain") != null)
                {
                    if (this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("myRangeValidatorAdd") != null)
                    {
                        ((RangeValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("myRangeValidatorAdd")).MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                        ((RangeValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("myRangeValidatorAdd")).MaximumValue = System.DateTime.Now.ToShortDateString();
                    }
                }

            }

            if (this.frmViewAdd.FindControl("tabEdit") != null)
            {
                if (this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("myRangeValidator") != null)
                {
                    ((RangeValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("myRangeValidator")).MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                    ((RangeValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("myRangeValidator")).MaximumValue = System.DateTime.Now.ToShortDateString();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {
        try
        {
            frmViewAdd.ChangeMode(FormViewMode.Insert);
            if (((RadioButtonList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkPayToAdd")).SelectedValue == "Cheque")
            {
                //((CompareValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cvBankAdd")).Enabled = true;
                //((RequiredFieldValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("rvChequeNoAdd")).Enabled = true;
                //HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("tblBankAdd");

                //if (table != null)
                //    table.Attributes.Add("class", "AdvancedSearch");

            }
            else
            {
                //((CompareValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cvBankAdd")).Enabled = false;
                //((RequiredFieldValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("rvChequeNoAdd")).Enabled = false;
                //HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("tblBankAdd");

                //if (table != null)
                //    table.Attributes.Add("class", "hidden");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            rvSearch.Enabled = true;
            Page.Validate();

            if (Page.IsValid)
            {
                GrdViewPayment.DataBind();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GrdViewPayment.SelectedIndex = e.RowIndex;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdViewPayment.SelectedDataKey != null)
                e.InputParameters["TransNo"] = Convert.ToInt32(GrdViewPayment.SelectedDataKey.Value);

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;

            string usernam = Request.Cookies["LoggedUserName"].Value;

            BusinessLogic bl = new BusinessLogic();

            string connection = Request.Cookies["Company"].Value;
            salestype = "Customer Payment";
            ScreenName = "Customer Payment";

            int DebitorID = 0;
            string TransDate = string.Empty;
            double Amount = 0;
            string PayTo = string.Empty;
            DataSet ds = bl.GetPaymentForId(connection, int.Parse(GrdViewPayment.SelectedDataKey.Value.ToString()));
            if (ds != null)
            {
                TransDate = Convert.ToString(ds.Tables[0].Rows[0]["TransDate"].ToString());
                DebitorID = Convert.ToInt32(ds.Tables[0].Rows[0]["DebtorID"]);
                Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"]);
                PayTo = ds.Tables[0].Rows[0]["paymode"].ToString();
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

                                int index132 = emailcontent.IndexOf("@PayMode");
                                body = PayTo;
                                if (index132 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = emailcontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = emailcontent.IndexOf("@Date");
                                body = TransDate.ToString();
                                if (index2 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                }

                                //int index = emailcontent.IndexOf("@Customer");
                                //body = ddReceivedFrom.SelectedItem.Text;
                                //emailcontent = emailcontent.Remove(index, 9).Insert(index, body);

                                int index1 = emailcontent.IndexOf("@Amount");
                                body = Convert.ToString(Amount);
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
            string UserID = Page.User.Identity.Name;

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

                                int index132 = smscontent.IndexOf("@PayMode");
                                body = PayTo;
                                if (index132 >= 0)
                                {
                                    smscontent = smscontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = smscontent.IndexOf("@Date");
                                body = TransDate.ToString();
                                if (index2 >= 0)
                                {
                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                }

                                //int index = emailcontent.IndexOf("@Customer");
                                //body = ddReceivedFrom.SelectedItem.Text;
                                //emailcontent = emailcontent.Remove(index, 9).Insert(index, body);

                                int index1 = smscontent.IndexOf("@Amount");
                                body = Convert.ToString(Amount);
                                if (index1 >= 0)
                                {
                                    smscontent = smscontent.Remove(index1, 7).Insert(index1, body);
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

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmViewAdd_DataBound(object sender, EventArgs e)
    {
        frmViewAdd_ModeChanged(sender, e);
    }
    protected void frmViewAdd_ModeChanged(object sender, EventArgs e)
    {
        if (frmViewAdd.CurrentMode == FormViewMode.Insert)
        {
            string usernam = Request.Cookies["LoggedUserName"].Value;

            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connection = Request.Cookies["Company"].Value;

            DataSet dst = bl.ListBranch(connection, usernam);
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).Items.Clear();
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).Items.Add(new ListItem("All", "All"));
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).DataSource = dst;
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).DataTextField = "BranchName";
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).DataValueField = "BranchCode";
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).DataBind();

            string sCustomer = string.Empty;

            DataSet ds = bl.GetBranch(connection, usernam);

            sCustomer = Convert.ToString(ds.Tables[0].Rows[0]["DefaultBranchCode"]);
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).ClearSelection();
            ListItem li = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
            if (li != null) li.Selected = true;

            if (ds.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
            {
                ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).Enabled = true;
            }
            else
            {
                ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).Enabled = false;
            }


          
            DataSet dstt = new DataSet();
            dstt = bl.ListSundryDebtorsExceptIsActive(connection, ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("drpBranchAdd")).SelectedValue);

            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).Items.Clear();
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).Items.Add(new ListItem("Select Customer", "0"));
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataSource = dstt;

            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataTextField = "LedgerName";
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataValueField = "LedgerID";
            ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).DataBind();



            ((UpdatePanel)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("UpdatePanel1")).Update();

            string dfg = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ddBanksAdd")).SelectedValue;
            loadChequeNo(Convert.ToInt32(dfg));

        }
        if (frmViewAdd.CurrentMode == FormViewMode.Edit)
        {
            //string dfg = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ddBanksAdd")).SelectedValue;
            //loadChequeNoEdit(Convert.ToInt32(dfg));

            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //string connection = Request.Cookies["Company"].Value;
            //DataSet dstt = new DataSet();
            //dstt = bl.ListSundryDebitors(connection, ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("drpBranch")).SelectedValue);
            //((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).Items.Clear();
            //((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).Items.Add(new ListItem("Select Customer", "0"));
            //((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).DataSource = dstt;

            //((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).DataTextField = "LedgerName";
            //((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).DataValueField = "LedgerID";
            //((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).DataBind();
        }
        
    }

    private bool CheckDate(DateTime dateTime)
    {
        BusinessLogic objBus = new BusinessLogic();
        return objBus.IsValidDate(Session["Connection"].ToString(), dateTime);
    }

    protected void GrdViewPayment_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                GrdViewPayment.DataBind();
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to delete the record. Please contact Supervisor.');");

                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                    e.ExceptionHandled = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            string Show = string.Empty;
            Show = "Y";
            if (e.OutputParameters["NewTransNo"] != null)
            {
                if (e.OutputParameters["NewTransNo"].ToString() != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Payment Saved Successfully. Transaction No : " + e.OutputParameters["NewTransNo"].ToString() + "');", true);
                    //Response.Redirect("PrintPayment.aspx?ID=" + e.OutputParameters["NewTransNo"].ToString() + "&RT=" + Show);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            string Show = string.Empty;
            Show = "Y";
            if (e.OutputParameters["NewTransNo"] != null)
            {
                if (e.OutputParameters["NewTransNo"].ToString() != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Payment Updated Successfully. New Transaction No : " + e.OutputParameters["NewTransNo"].ToString() + "');", true);
                    //Response.Redirect("PrintPayment.aspx?ID=" + e.OutputParameters["NewTransNo"].ToString() + " ");
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkboxAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked == true)
            {
                Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("Panelcopy");
                test.Visible = true;
                ModalPopupExtender1.Show();
            }
            else
            {
                Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("Panelcopy");
                test.Visible = false;
            }

            //if (chkboxAll.Checked == true)
            //{
            //    Panelcopy.Visible = true;
            //    ModalPopupExtender1.Show();
            //}
            //else if (chkboxAll.Checked == false)
            //{
            //    Panelcopy.Visible = false;
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //private void loadCustomer()
    //{
    //    //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    DataSet ds = new DataSet();
    //    ds = bl.ListCustomerPaymentsRef(sDataSource);
    //    combooldrefno.DataSource = ds;
    //    combooldrefno.DataBind();
    //    combooldrefno.DataTextField = "RefNo";
    //    combooldrefno.DataValueField = "TransNo";
    //}

    protected void TextChanged(object sender, EventArgs e)
    {
        try
        { 
        string sCustomer = string.Empty;
        string strPaymode = string.Empty;
        string strPaymodef = string.Empty;

            BusinessLogic bl = new BusinessLogic(sDataSource);
            TextBox testcheck = (TextBox)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtoldref");
            int iRefID = Convert.ToInt32(testcheck.Text);

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            DataSet customerDs = bl.GetPaymentForRef(connection, iRefID);
            string address = string.Empty;

            if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            {

                if (customerDs.Tables[0].Rows[0]["RefNo"] != null)
                    txtRefNoAdd.Text = customerDs.Tables[0].Rows[0]["RefNo"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["TransDate"] != null)
                {
                    string aa = customerDs.Tables[0].Rows[0]["TransDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    txtTransDateAdd.Text = dtaa + Environment.NewLine;
                }

                if (customerDs.Tables[0].Rows[0]["DebtorID"] != null)
                {
                    //ComboBox2Add.SelectedItem.Value = customerDs.Tables[0].Rows[0]["DebtorID"].ToString() + Environment.NewLine;
                    //ComboBox2Add.Text = customerDs.Tables[0].Rows[0]["Debi"].ToString() + Environment.NewLine;

                    sCustomer = Convert.ToString(customerDs.Tables[0].Rows[0]["DebtorID"]);
                    ComboBox2Add.ClearSelection();
                    ListItem li = ComboBox2Add.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                    if (li != null) li.Selected = true;
                }

                if (customerDs.Tables[0].Rows[0]["Amount"] != null)
                    txtAmountAdd.Text = customerDs.Tables[0].Rows[0]["Amount"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["Paymode"] != null)
                {
                    //if (customerDs.Tables[0].Rows[0]["Paymode"].ToString() == "Cash")
                    //    chkPayToAdd.SelectedItem.Value = "Cash" + Environment.NewLine;
                    //else
                    //    chkPayToAdd.SelectedItem.Value = "Cheque" + Environment.NewLine;

                    strPaymodef = customerDs.Tables[0].Rows[0]["Paymode"].ToString();
                    chkPayToAdd.ClearSelection();
                    ListItem piLi = chkPayToAdd.Items.FindByValue(strPaymodef.Trim());
                    if (piLi != null) piLi.Selected = true;




                }

                if (customerDs.Tables[0].Rows[0]["Narration"] != null)
                    txtNarrationAdd.Text = customerDs.Tables[0].Rows[0]["Narration"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["CreditorId"] != null)
                {
                    //ddBanksAdd.SelectedItem.Value = customerDs.Tables[0].Rows[0]["CreditorId"] + Environment.NewLine;

                    strPaymode = Convert.ToString(customerDs.Tables[0].Rows[0]["CreditorId"]);
                    ddBanksAdd.ClearSelection();
                    ListItem pLi = ddBanksAdd.Items.FindByValue(strPaymode.Trim());
                    if (pLi != null) pLi.Selected = true;
                }

                if (customerDs.Tables[0].Rows[0]["ChequeNo"] != null)
                    txtChequeNoAdd.Text = customerDs.Tables[0].Rows[0]["ChequeNo"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["BillNo"] != null)
                    txtBillAdd.Text = customerDs.Tables[0].Rows[0]["BillNo"].ToString() + Environment.NewLine;


                ModalPopupExtender1.Show();
                //if (strPaymodef == "Cheque")
                //{
                //    //RadioButtonList chk = (RadioButtonList)sender;
                //    if (strPaymodef == "Cheque")
                //    {
                //        //Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd");
                //        PanelBankAdd.Visible = true;
                //    }
                //}

                //chkPayToAdd.SelectedIndexChanged += new EventHandler(chkPayToAdd_SelectedIndexChanged);

                //chkPayToAdd_SelectedIndexChanged(this.chkPayToAdd, new EventArgs());


            }
            else
            {
                txtRefNoAdd.Text = string.Empty;
                txtTransDateAdd.Text = string.Empty;
                ComboBox2Add.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpBranch_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string creditorID = ((DataRowView)frmV.DataItem)["BranchCode"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(creditorID);
                if (li != null) li.Selected = true;

                string dfg = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("drpBranch")).SelectedValue;
                loadBranchEdit(); 

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void combooldrefno_SelectedIndexChanged(object sender, EventArgs e)
    //{
        

    //}


    protected void ddBanksAdd_DataBound(object sender, EventArgs e)
    {
        //loadChequeNo(Convert.ToInt32(ddBanksAdd.SelectedItem.Value));       
    }
    protected void cmbChequeNo_DataBound(object sender, EventArgs e)
    {
        try
        {  
            DropDownList ddl = (DropDownList)sender;
           
            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;
            //loadChequeNo(Convert.ToInt32(((DataRowView)frmV.DataItem)["CreditorID"].ToString()));

            //string dfg = ((DataRowView)frmV.DataItem)["CreditorID"].ToString();
            //loadChequeNoEdit(Convert.ToInt32(dfg));
          
            if (frmV.DataItem != null)
            {
               
                string creditorID = ((DataRowView)frmV.DataItem)["ChequeNo"].ToString();
                ddl.ClearSelection();
                ListItem li = new ListItem(((DataRowView)frmV.DataItem)["ChequeNo"].ToString(), "0");
                ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).Items.Insert(((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cmbChequeNo")).Items.Count - 1, li);
                li = ddl.Items.FindByText(((DataRowView)frmV.DataItem)["ChequeNo"].ToString());
                if (li != null) li.Selected = true;

              
                //ListItem clie = new ListItem(ds.Tables[0].Rows[0]["Chequeno"].ToString(), "0");
                //cmbChequeNo.Items.Insert(cmbChequeNo.Items.Count - 1, clie);
                //clie = cmbChequeNo.Items.FindByText(ds.Tables[0].Rows[0]["Chequeno"].ToString());

                //if (clie != null) clie.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void ddCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCriteria.SelectedItem.Text == "Transaction Date")
        {
            //txtdate.EnableViewState = 1;
            //txtdate.Enabled = true;
        }
        else
        {
           //txtdate.Enabled = false;
        }
    }
  
}
