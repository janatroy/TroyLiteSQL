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

public partial class ExpPayment : System.Web.UI.Page
{
    Double sumAmt = 0.0;
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!Page.IsPostBack)
            {
                string connStr = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                //CheckSMSRequired();

                ddReceivedFrom.DataBind();

                GrdViewPayment.PageSize = 11;

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    GrdViewPayment.Columns[8].Visible = false;
                    GrdViewPayment.Columns[7].Visible = false;
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
                pnlEdit.Visible = false;

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

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "EXPPMT"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New ";
                }


                //loadBanks();
                loadHeading();
                loadGroup("0");

                loadLedger("0", drpBranchAdd.SelectedValue);

                //myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();


                if (Request.QueryString["myname"] != null)
                {

                    string myNam = Request.QueryString["myname"].ToString();
                    if (myNam == "NEWEXP")
                    {

                        if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
                            return;
                        }

                        ModalPopupExtender2.Show();
                        pnlEdit.Visible = true;
                        UpdateButton.Visible = false;
                        SaveButton.Visible = true;
                        ClearPanel();
                        ShowPendingBills();
                        txtTransDate.Text = DateTime.Now.ToShortDateString();
                        txtRefNo.Focus();
                        chkPayTo.SelectedValue = "Cash";
                        ddBanks.Items.Clear();
                        ddBanks.Items.Insert(0, new ListItem("Select Bank", "0"));
                        loadBanks();

                        if (chkPayTo.SelectedItem != null)
                        {
                            if (chkPayTo.SelectedItem.Text == "Cheque")
                            {
                                tblBank.Attributes.Add("class", "AdvancedSearch");
                            }
                            else
                            {
                                tblBank.Attributes.Add("class", "hidden");
                            }
                        }
                        else
                        {
                            if (tblBank != null)
                                tblBank.Attributes.Add("class", "hidden");
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

    protected void ddBanks_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadChequeNo(Convert.ToInt32(ddBanks.SelectedItem.Value));
    }

    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpBranchAdd.Items.Clear();
        drpBranchAdd.Items.Add(new ListItem("Select Branch", "0"));
        ds = bl.ListBranch();
        drpBranchAdd.DataSource = ds;
        drpBranchAdd.DataBind();
        drpBranchAdd.DataTextField = "BranchName";
        drpBranchAdd.DataValueField = "Branchcode";
    }

    private void loadChequeNo(int bnkId)
    {
        cmbChequeNo.Items.Clear();
        cmbChequeNo.Items.Add(new ListItem("Select ChequeNo", "0"));
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();       
        ds = bl.ListChequeNo(bnkId);
        cmbChequeNo.DataSource = ds;
        cmbChequeNo.DataBind();
        cmbChequeNo.DataTextField = "ChequeNo";
        cmbChequeNo.DataValueField = "ChequeNo";

    }

    private void loadHeading()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListAccHeading();
        drpHeading.DataSource = ds;
        drpHeading.DataBind();
        drpHeading.DataTextField = "Heading";
        drpHeading.DataValueField = "HeadingID";

    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        ddCriteria.SelectedIndex = 0;
    }

    private void loadGroup(string HeadingID)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListGroupForHeading(HeadingID);
        drpGroup.Items.Clear();
        drpGroup.Items.Add(new ListItem("Select Group", "0"));
        drpGroup.DataSource = ds;
        drpGroup.DataBind();
        drpGroup.DataTextField = "GroupName";
        drpGroup.DataValueField = "GroupID";

    }

    protected void drpBranchAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadLedger(drpGroup.SelectedValue,drpBranchAdd.SelectedValue);
    }

    protected void drpHeading_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadGroup(drpHeading.SelectedValue);
            if (Session["State"] == "Edit")
            {
                loadLedgerEdit(drpGroup.SelectedValue, drpBranchAdd.SelectedValue);
            }
            else
            {
                loadLedger(drpGroup.SelectedValue, drpBranchAdd.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BranchEnable_Disable()
    {
        string sCustomer = string.Empty;
        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        drpBranchAdd.ClearSelection();
        ListItem li = drpBranchAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        if (li != null) li.Selected = true;

        if (dsd.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
        {
            drpBranchAdd.Enabled = true;
        }
        else
        {
            drpBranchAdd.Enabled = false;
        }
    }

    protected void drpGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["State"] == "Edit")
            {
                loadLedgerEdit(drpGroup.SelectedValue, drpBranchAdd.SelectedValue);
            }
            else
            {
                loadLedger(drpGroup.SelectedValue, drpBranchAdd.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadLedger(string GroupID, string Branch)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListLedgerForGroupIsActive(GroupID, Branch);
        ddReceivedFrom.Items.Clear();
        ddReceivedFrom.Items.Add(new ListItem("Select Ledger", "0"));
        ddReceivedFrom.DataSource = ds;
        ddReceivedFrom.DataBind();

        ddReceivedFrom.DataTextField = "LedgerName";
        ddReceivedFrom.DataValueField = "LedgerID";

    }

    private void loadLedgerEdit(string GroupID, string Branch)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListLedgerForGroup(GroupID, Branch);
        ddReceivedFrom.Items.Clear();
        ddReceivedFrom.Items.Add(new ListItem("Select Ledger", "0"));
        ddReceivedFrom.DataSource = ds;
        ddReceivedFrom.DataBind();

        ddReceivedFrom.DataTextField = "LedgerName";
        ddReceivedFrom.DataValueField = "LedgerID";

    }

    private void ShowPendingBills()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        BusinessLogic bl = new BusinessLogic();
        var SupplierID = ddReceivedFrom.SelectedValue.Trim();

        var dsSales = bl.ListCreditPurchase(connStr.Trim(), SupplierID);

        var receivedData = bl.GetSupplierReceivedAmount(connStr);

        if (dsSales != null)
        {

            foreach (DataRow dr in receivedData.Tables[0].Rows)
            {
                var billNo = dr["BillNo"].ToString();
                var billAmount = dr["TotalAmount"].ToString();

                for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                {
                    if (billNo.Trim() == dsSales.Tables[0].Rows[i]["BillNo"].ToString())
                    {
                        dsSales.Tables[0].Rows[i].BeginEdit();
                        double val = (double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
                        dsSales.Tables[0].Rows[i]["Amount"] = val;
                        dsSales.Tables[0].Rows[i].EndEdit();

                        if (val == 0.0)
                            dsSales.Tables[0].Rows[i].Delete();
                    }
                }
                dsSales.Tables[0].AcceptChanges();
            }
        }
    }

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        

    }

    protected void btnpay_Click(object sender, EventArgs e)
    {
        try
        {
            string ExpPay = "ExpPay";
            Response.Write("<script language='javascript'> window.open('ReportExcelPayments.aspx?ID=" + ExpPay + "' , 'window','toolbar=no,status=no,menu=no,location=no,height=320,width=700,left=320,top=220 ,resizable=yes, scrollbars=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void checkPendingBills(DataSet ds)
    {
        //foreach (GridViewRow tt in GrdViewSales.Rows)
        //{
        //    if (tt.RowType == DataControlRowType.DataRow)
        //    {
        //        string billNo = tt.Cells[0].Text;

        //        bool exists = false;

        //        if (ds != null)
        //        {
        //            foreach (DataRow d in ds.Tables[0].Rows)
        //            {
        //                string bNo = d[1].ToString();

        //                if (bNo == billNo)
        //                {
        //                    exists = true;
        //                }

        //            }
        //        }

        //        if (!exists)
        //        {
        //            hdPendingCount.Value = "1";
        //            UpdatePanelPage.Update();
        //            return;
        //        }

        //    }
        //}

        //hdPendingCount.Value = "0";
        //UpdatePanelPage.Update();
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

    protected void GrdViewPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            /*//MyAccordion.Visible = false;
            frmViewAdd.Visible = true;
            frmViewAdd.DataBind();
            frmViewAdd.ChangeMode(FormViewMode.Edit);
            //GrdViewReceipt.Columns[7].Visible = false;
            lnkBtnAdd.Visible = false;
            GrdViewReceipt.Visible = false;
            //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
                //Accordion1.SelectedIndex = 1;*/
        }
    }

    //protected void txtChequeNo_DataBound(object sender, EventArgs e)
    //{
    //    DropDownList ddl = (DropDownList)sender;

    //    FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

    //    if (frmV.DataItem != null)
    //    {
    //        string ChequeNo = ((DataRowView)frmV.DataItem)["ChequeNo"].ToString();
    //        string connection = Request.Cookies["Company"].Value;
    //        BusinessLogic bl = new BusinessLogic();
 
    //        string cheque = bl.GetCheque(connection, ChequeNo);

    //        ddl.ClearSelection();

    //        ListItem li = ddl.Items.FindByValue(cheque);
    //        if (li != null) li.Selected = true;

    //    }

    //}
    //protected void ddBanks_SelectedIndexChanged(object sender, EventArgs e)
    //{
        //sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        //string BankID = ddBanks.SelectedValue;
        //loacheque(BankID);

        
    //}

    private void loacheque(string BankID)
    {
        //sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = new DataSet();
        //ds = bl.ListChequeNosBank(sDataSource, BankID);
        //txtChequeNo.Items.Clear();
        //txtChequeNo.DataSource = ds;
        //txtChequeNo.Items.Insert(0, new ListItem("Select Cheque No", "0"));
        //txtChequeNo.DataTextField = "ChequeNo";
        //txtChequeNo.DataValueField = "ChequeId";
        //txtChequeNo.DataBind();
    }

    private void loadBanks()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanksIsActive(sDataSource);

        ddBanks.Items.Clear();
        ListItem lifzzh = new ListItem("Select Bank", "0");
        lifzzh.Attributes.Add("style", "color:Black");
        ddBanks.Items.Add(lifzzh);
        ddBanks.DataSource = ds;
        ddBanks.DataBind();
        ddBanks.DataTextField = "LedgerName";
        ddBanks.DataValueField = "LedgerID";
    }

    private void loadBanksEdit()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanks(sDataSource);

        ddBanks.Items.Clear();
        ListItem lifzzh = new ListItem("Select Bank", "0");
        lifzzh.Attributes.Add("style", "color:Black");
        ddBanks.Items.Add(lifzzh);
        ddBanks.DataSource = ds;
        ddBanks.DataBind();
        ddBanks.DataTextField = "LedgerName";
        ddBanks.DataValueField = "LedgerID";
    }


    protected void GrdViewPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["State"] = "Edit";

            GridViewRow Row = GrdViewPayment.SelectedRow;
            string connection = Request.Cookies["Company"].Value;

            string state = Session["State"].ToString();

            BusinessLogic bl = new BusinessLogic();
            string recondate = Row.Cells[2].Text;
            Session["BillData"] = null;

            int Trans = Convert.ToInt32(GrdViewPayment.SelectedDataKey.Value);
           // bl.InsertChequeStatus(connection, Trans);
            loadBanksEdit();

            loadBranch();

            hdPayment.Value = Convert.ToString(GrdViewPayment.SelectedDataKey.Value);

            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                return;
            }
            else
            {
                //pnlEdit.Visible = true;
                DataSet ds = bl.GetPaymentForId(connection, int.Parse(GrdViewPayment.SelectedDataKey.Value.ToString()));
                if (ds != null)
                {
                    txtRefNo.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();
                    txtTransDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["TransDate"].ToString()).ToShortDateString();

                    int ledgerid = Convert.ToInt32(ds.Tables[0].Rows[0]["DebtorID"].ToString());
                    DataSet dsd = bl.GetLedgerGroupHeadingForId(connection, ledgerid);
                    if (dsd != null)
                    {
                        drpHeading.SelectedValue = dsd.Tables[0].Rows[0]["HeadingId"].ToString();
                        loadGroup(drpHeading.SelectedValue);

                        drpBranchAdd.SelectedValue = ds.Tables[0].Rows[0]["BranchCode"].ToString();

                        drpGroup.SelectedValue = dsd.Tables[0].Rows[0]["GroupId"].ToString();
                        loadLedgerEdit(drpGroup.SelectedValue, drpBranchAdd.SelectedValue);

                        ddReceivedFrom.SelectedValue = ds.Tables[0].Rows[0]["DebtorID"].ToString();
                    }

                    drpBranchAdd.Enabled = false;

                    txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                    //txtMobile.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    chkPayTo.SelectedValue = ds.Tables[0].Rows[0]["paymode"].ToString();
                    txtNarration.Text = ds.Tables[0].Rows[0]["Narration"].ToString();
                    //txtChequeNo.Text = ds.Tables[0].Rows[0]["ChequeNo"].ToString();

                    if (chkPayTo.SelectedItem != null)
                    {
                        if (chkPayTo.SelectedItem.Text == "Cheque")
                        {
                            tblBank.Attributes.Add("class", "AdvancedSearch");
                        }
                        else
                        {
                            tblBank.Attributes.Add("class", "hidden");
                        }
                    }
                    else
                    {
                        tblBank.Attributes.Add("class", "hidden");
                    }




                    //loadBanks();
                    //string creditorID = ds.Tables[0].Rows[0]["CreditorID"].ToString();

                    //ddBanks.ClearSelection();

                    //ListItem li = ddBanks.Items.FindByValue(creditorID);
                    //if (li != null) li.Selected = true;

                    if (ds.Tables[0].Rows[0]["CreditorID"] != null)
                    {
                        string creditorID = Convert.ToString(ds.Tables[0].Rows[0]["CreditorID"]);
                        ddBanks.ClearSelection();
                        ListItem li = ddBanks.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(creditorID));
                        if (li != null) li.Selected = true;
                    }

                    loadChequeNo(Convert.ToInt32(ddBanks.SelectedItem.Value));
                    hid1.Value = ds.Tables[0].Rows[0]["ChequeNo"].ToString();

                    if (chkPayTo.SelectedItem.Text == "Cheque")
                    {
                        if (ds.Tables[0].Rows[0]["ChequeNo"] != null)
                        {
                            // txtChequeNo.Text = ds.Tables[0].Rows[0]["Chequeno"].ToString();
                            cmbChequeNo.ClearSelection();
                            ListItem clie = new ListItem(ds.Tables[0].Rows[0]["ChequeNo"].ToString(), "0");
                            cmbChequeNo.Items.Insert(cmbChequeNo.Items.Count - 1, clie);
                            clie = cmbChequeNo.Items.FindByText(ds.Tables[0].Rows[0]["ChequeNo"].ToString());

                            if (clie != null) clie.Selected = true;
                        }
                    }


                    //loacheque(Convert.ToString(ds.Tables[0].Rows[0]["CreditorID"]));

                    //string cheque = bl.GetCheque(connection, ds.Tables[0].Rows[0]["ChequeNo"].ToString());
                    //txtChequeNo.ClearSelection();

                    //ListItem lit = txtChequeNo.Items.FindByValue(cheque);
                    //if (lit != null) lit.Selected = true;


                    //DataSet billsData = bl.GetPaymentAmountId(connection, int.Parse(GrdViewPayment.SelectedDataKey.Value.ToString()));

                    //Session["BillData"] = billsData;

                    //if (billsData.Tables[0].Rows[0]["BillNo"].ToString() == "0")
                    //{
                    //    billsData = null;
                    //}
                    //GrdBills.DataSource = billsData;
                    //GrdBills.DataBind();
                    Session["RMode"] = "Edit";
                    //ShowPendingBills();
                    //checkPendingBills(billsData);
                }

                //GrdViewReceipt.Visible = false;
                ////MyAccordion.Visible = false;
                //lnkBtnAdd.Visible = false;
                pnlEdit.Visible = true;
                UpdateButton.Visible = true;
                SaveButton.Visible = false;
                ModalPopupExtender2.Show();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkAddBills_Click(object sender, EventArgs e)
    {
        try
        {
            pnlEdit.Visible = false;
            BusinessLogic bl = new BusinessLogic();
            string conn = GetConnectionString();
            ModalPopupExtender2.Show();
            pnlEdit.Visible = true;
            if (txtAmount.Text == "")
            {

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter the Receipt Amount before Adding BillNo')", true);
                //CnrfmDel.ConfirmText = "Please enter the Receipt Amount before Adding BillNo";
                //CnrfmDel.TargetControlID = "lnkAddBills";
                txtAmount.Focus();
                return;
            }

            if (ddReceivedFrom.SelectedValue == "0")
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select the Customer before Adding Bills')", true);
                //pnlEdit.Visible = true;
                txtAmount.Focus();
                return;
            }

            //if (GrdBills.Rows.Count == 0)
            //{
            //    var ds = bl.GetPaymentAmountId(conn, -1);
            //    GrdBills.DataSource = ds;
            //    GrdBills.DataBind();
            //    GrdBills.Rows[0].Visible = false;
            //    checkPendingBills(ds);
            //}
            //pnlEdit.Visible = true;
            //GrdBills.FooterRow.Visible = true;
            //lnkAddBills.Visible = true;
            Session["RMode"] = "Add";
            //lnkBtnAdd.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //MyAccordion.Visible = true;
            pnlEdit.Visible = false;
            ModalPopupExtender2.Hide();
            //lnkBtnAdd.Visible = true;
            //lnkAddBills.Visible = true;
            GrdViewPayment.Visible = true;
            GrdViewPayment.Columns[8].Visible = true;
            ClearPanel();

            //if (Session["State"] == "Edit")
            //{
            //    string connection = Request.Cookies["Company"].Value;
            //    int trans = Convert.ToInt32(hdPayment.Value);

            //    BusinessLogic objChk = new BusinessLogic();
            //    objChk.UpdateChequeStatus(connection, trans);
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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

                if (bl.CheckUserHaveEdit(usernam, "EXPPMT"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "EXPPMT"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveView(usernam, "EXPPMT"))
                {
                    ((Image)e.Row.FindControl("lnkprint")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = true;
                }
                else
                {
                    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = false;
                }
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
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
            //    return;
            //}
            ModalPopupExtender2.Show();
            pnlEdit.Visible = true;
            //lnkBtnAdd.Visible = false;
            ////MyAccordion.Visible = false;
            //GrdViewReceipt.Visible = false;
            UpdateButton.Visible = false;
            SaveButton.Visible = true;

            loadChequeNo(0);

            ClearPanel();
            ShowPendingBills();
            //txtTransDate.Text = DateTime.Now.ToShortDateString();

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtTransDate.Text = dtaa;

            txtRefNo.Focus();
            chkPayTo.SelectedValue = "Cash";

            //txtChequeNo.Items.Clear();
            //txtChequeNo.Items.Insert(0, new ListItem("Select Cheque No", "0"));
            ddBanks.Items.Clear();
            ddBanks.Items.Insert(0, new ListItem("Select Bank", "0"));
            loadBanks();

            if (chkPayTo.SelectedItem != null)
            {
                if (chkPayTo.SelectedItem.Text == "Cheque")
                {
                    tblBank.Attributes.Add("class", "AdvancedSearch");
                }
                else
                {
                    tblBank.Attributes.Add("class", "hidden");
                }
            }
            else
            {
                if (tblBank != null)
                    tblBank.Attributes.Add("class", "hidden");
            }


            drpHeading.SelectedValue = "11";
            loadGroup("11");

            drpGroup.SelectedValue = "8";

            
            Session["State"] = "Add";

            drpBranchAdd.Enabled = true;


            loadBranch();
            BranchEnable_Disable();
            loadLedger(drpGroup.SelectedValue, drpBranchAdd.SelectedValue);
            SaveButton.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void ClearPanel()
    {
        txtRefNo.Text = "";
        txtTransDate.Text = "";
        txtNarration.Text = "";
        //txtChequeNo.Text = "";
        txtAmount.Text = "";
        ddReceivedFrom.SelectedValue = "0";
        drpHeading.SelectedValue = "0";
        drpGroup.SelectedValue = "0";
        //txtMobile.Text = "";
        ddBanks.SelectedValue = "0";
        cmbChequeNo.SelectedValue = "0";
        txtBillAdd.Text = "";
    }

    protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string debtorID = ddReceivedFrom.SelectedValue;
            BusinessLogic objBus = new BusinessLogic();

            string Mobile = objBus.GetLedgerMobileForId(Request.Cookies["Company"].Value, int.Parse(debtorID));

            //if (Mobile == "0")
            //{
            //    txtMobile.Text = "";
            //}
            //else
            //{
            //    txtMobile.Text = Mobile;
            //}

            txtAmount.Focus();

            if (chkPayTo.SelectedItem != null)
            {
                if (chkPayTo.SelectedItem.Text == "Cheque")
                {
                    tblBank.Attributes.Add("class", "AdvancedSearch");
                }
                else
                {
                    tblBank.Attributes.Add("class", "hidden");
                }
            }
            else
            {
                tblBank.Attributes.Add("class", "hidden");
            }

            ShowPendingBills();
            ModalPopupExtender2.Show();
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
            if (chkPayTo.SelectedItem.Text == "Cheque")
            {
                tblBank.Visible = true;
            }
            else
            {
                tblBank.Visible = false;
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

            BusinessLogic bl = new BusinessLogic();

            string usernam = Request.Cookies["LoggedUserName"].Value;
            string connection = Request.Cookies["Company"].Value;
            salestype = "Expense Payment";
            ScreenName = "Expense Payment";
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
                                else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Expense") || (dr["Name1"].ToString() == "Bank") || (dr["Name1"].ToString() == "Supplier") || (dr["Name1"].ToString() == "Ledger"))
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
                                //string subject = "Added - Customer Receipt in Branch " + Request.Cookies["Company"].Value;

                                string body = "\n";
                                //body += " Branch           : " + Request.Cookies["Company"].Value + "\n";
                                //body += " Trans No         : " + GrdViewReceipt.SelectedDataKey.Value + "\n";
                                //body += " User Name        : " + Request.Cookies["LoggedUserName"].Value + "\n";
                                //body += " Trans Date       : " + TransDate + "\n";
                                //body += " Amount           : " + Amount + "\n";
                                //body += " Narration        : " + Narration + "\n";
                                //body += " Customer         : " + ddReceivedFrom.SelectedItem.Text + "\n";

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

                                EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, body, fromPassword);

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
                                else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Expense") || (dr["Name1"].ToString() == "Bank") || (dr["Name1"].ToString() == "Supplier") || (dr["Name1"].ToString() == "Ledger"))
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
                    script.Append("alert('You are not allowed to delete the record. Please contact Administrator.');");

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



    private void calcSum()
    {

    }

    private double calcDatasetSum(DataSet ds)
    {
        double total = 0.0;

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Amount"] != null)
                    {
                        total = total + Convert.ToDouble(dr["Amount"].ToString());
                    }
                }
            }
        }

        return total;
    }


    private bool CheckIfBillExists(string billNo)
    {
        bool dupFlag = false;

        if (Session["BillData"] != null)
        {
            var checkDs = (DataSet)Session["BillData"];

            foreach (DataRow dR in checkDs.Tables[0].Rows)
            {
                if (dR["BillNo"] != null)
                {
                    if (dR["BillNo"].ToString().Trim() == billNo)
                    {
                        dupFlag = true;
                        break;
                    }
                }
            }
        }

        return dupFlag;
    }

    private int CheckNoOfBillExists(string billNo)
    {
        int count = 0;

        if (Session["BillData"] != null)
        {
            var checkDs = (DataSet)Session["BillData"];

            foreach (DataRow dR in checkDs.Tables[0].Rows)
            {
                if (dR["BillNo"] != null)
                {
                    if (dR["BillNo"].ToString().Trim() == billNo)
                    {
                        count = count + 1;
                    }
                }
            }
        }

        return count;
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

    bool IsFutureDate(DateTime refDate)
    {
        DateTime today = DateTime.Today;
        return (refDate.Date != today) && (refDate > today);
    }

    protected void txtTransDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string refDate = string.Empty;
            refDate = txtTransDate.Text;
            ViewState.Add("TransDate", refDate);

            if (IsFutureDate(Convert.ToDateTime(refDate)))
            {
                hddatecheck.Value = "1";
                UP1.Update();
                return;
            }
            else
            {
                hddatecheck.Value = "0";
                UP1.Update();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            SaveButton.Enabled = false;
            int ichequestatus = 0;
            DataSet dsData = (DataSet)Session["BillData"];

            if (calcDatasetSum(dsData) > double.Parse(txtAmount.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Bills amount is exceeding the Payment Amount. Please check the Bill Amount')", true);
                SaveButton.Enabled = true;
                return;
            }

            if (chkPayTo.SelectedValue == "Cheque")
            {
                if (ddBanks.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Name is Mandatory')", true);
                    SaveButton.Enabled = true;
                    return;
                }
                if (cmbChequeNo.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No  is Mandatory')", true);
                    SaveButton.Enabled = true;
                    return;
                }
            }

            //if (chkPayTo.SelectedValue == "Cheque")
            //{
            //    cvBank.Enabled = true;
            //    rvChequeNo.Enabled = true;
            //}
            //else
            //{
            //    cvBank.Enabled = false;
            //    rvChequeNo.Enabled = false;

            //}

            Page.Validate();

            if (Page.IsValid)
            {
                int DebitorID = 0;

                int CreditorID = 0;


                DebitorID = int.Parse(ddReceivedFrom.SelectedValue);

                string RefNo = txtRefNo.Text;

                DateTime TransDate = DateTime.Parse(txtTransDate.Text);


                string Paymode = string.Empty;
                double Amount = 0.0;
                string Narration = string.Empty;
                string VoucherType = string.Empty;
                string ChequeNo = string.Empty;
                string BillNo = string.Empty;

                if (chkPayTo.SelectedValue == "Cash")
                {
                    CreditorID = 1;
                    Paymode = "Cash";
                    ChequeNo = "";
                }
                else if (chkPayTo.SelectedValue == "Cheque")
                {
                    CreditorID = int.Parse(ddBanks.SelectedValue);
                    Paymode = "Cheque";
                    ChequeNo = cmbChequeNo.SelectedItem.Text;
                }

                Amount = double.Parse(txtAmount.Text);
                Narration = txtNarration.Text;
                VoucherType = "Payment";
                
                BillNo = txtBillAdd.Text;
                BusinessLogic bl = new BusinessLogic();

                string Branch = drpBranchAdd.SelectedValue;
                string connection = Request.Cookies["Company"].Value;

                if (chkPayTo.SelectedValue == "Cheque")
                {
                    if (ChequeNo != "")
                    {
                        if (bl.IsChequeNoAlreadyPresent(connection, ChequeNo))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No Already Billed')", true);
                            SaveButton.Enabled = true;
                            return;
                        }

                        if (bl.GetDamageChequeNo(connection, ChequeNo))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No is noted as damaged. Select any other Cheque No')", true);
                            SaveButton.Enabled = true;
                            return;
                        }

                        DataSet dsdat = new DataSet();
                        dsdat = bl.GetChequeNoGiven(connection, ChequeNo);
                        string datad = string.Empty;

                        Int32 set = 0;
                        Int32 setdd = 0;

                        Int32 Cheque = 0;
                        Cheque = Convert.ToInt32(cmbChequeNo.SelectedItem.Text);

                        if (dsdat != null)
                        {
                            if (dsdat.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdat.Tables[0].Rows)
                                {
                                    set = Convert.ToInt32(dr["FromChequeNo"]);
                                    setdd = Convert.ToInt32(dr["ToChequeNo"]);

                                    if ((Cheque >= set) && (Cheque <= setdd))
                                    {
                                        datad = "Y";
                                        break;
                                    }
                                    else
                                    {
                                        datad = "N";
                                    }
                                }
                                if (datad == "N")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No Not Found in Cheque Book')", true);
                                    SaveButton.Enabled = true;
                                    return;
                                }
                            }
                        }
                    }
                }


                //if (hdSMSRequired.Value == "YES")
                //{

                //    //if (txtMobile.Text != "")
                //    //    hdMobile.Value = txtMobile.Text;

                //    hdText.Value = "Thank you for Payment of Rs." + txtAmount.Text;

                //}


                string conn = GetConnectionString();
                int OutPut = 0;

                DataSet ds = (DataSet)Session["BillData"];



                //if (ds == null)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter the Bill Details')", true);
                //    return;
                //}

                string usernam = Request.Cookies["LoggedUserName"].Value;

                bl.InsertPayment(out OutPut, conn, RefNo, TransDate, DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, Paymode, BillNo, usernam, Branch);
                ichequestatus = bl.UpdateChequeused_conn(ChequeNo, CreditorID, conn);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Payment Saved Successfully. Transaction No : " + OutPut.ToString() + "');", true);

                string salestype = string.Empty;
                int ScreenNo = 0;
                string ScreenName = string.Empty;

                salestype = "Expense Payment";
                ScreenName = "Expense Payment";

             
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
                                    else if ((dr["Name1"].ToString() == "Customer")||(dr["Name1"].ToString() == "Expense")||(dr["Name1"].ToString() == "Bank")||(dr["Name1"].ToString() == "Supplier")||(dr["Name1"].ToString() == "Ledger"))
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
                                    body = Narration;
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

                                    int index = emailcontent.IndexOf("@Expense");
                                    body = ddReceivedFrom.SelectedItem.Text;
                                    if (index >= 0)
                                    {
                                        emailcontent = emailcontent.Remove(index, 8).Insert(index, body);
                                    }

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

                                    EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, body, fromPassword);

                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email sent successfully')", true);

                                }

                            }
                        }
                    }
                }

                string conn1 = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                UtilitySMS utilSMS = new UtilitySMS(conn1);
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
                                    else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Expense") || (dr["Name1"].ToString() == "Bank") || (dr["Name1"].ToString() == "Supplier") || (dr["Name1"].ToString() == "Ledger"))
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
                                    body = Narration;
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

                                    int index = smscontent.IndexOf("@Expense");
                                    body = ddReceivedFrom.SelectedItem.Text;
                                    if (index >= 0)
                                    {

                                        smscontent = smscontent.Remove(index, 8).Insert(index, body);
                                    }

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


                //if (hdSMS.Value == "YES")
                //{
                //    UtilitySMS utilSMS = new UtilitySMS(conn);
                //    string UserID = Page.User.Identity.Name;

                //    if (Session["Provider"] != null)
                //        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), hdMobile.Value, hdText.Value, true, UserID);
                //    else
                //    {
                //        if (hdMobile.Value != "")
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                //    }
                //}

                pnlEdit.Visible = false;
                ModalPopupExtender2.Hide();
                lnkBtnAdd.Visible = true;
                //MyAccordion.Visible = true;
                GrdViewPayment.Visible = true;
                GrdViewPayment.DataBind();
                ClearPanel();
                UpdatePanelPage.Update();

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
            DataSet dsData = (DataSet)Session["BillData"];
            int ichequestatus = 0;
            if (calcDatasetSum(dsData) > double.Parse(txtAmount.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Bills amount is exceeding the Payment Amount. Please check the Bill Amount')", true);
                return;
            }


            //if (chkPayTo.SelectedValue == "Cheque")
            //{
            //    cvBank.Enabled = true;
            //    rvChequeNo.Enabled = true;
            //}
            //else
            //{
            //    cvBank.Enabled = false;
            //    rvChequeNo.Enabled = false;
            //}

            if (chkPayTo.SelectedValue == "Cheque")
            {
                if (ddBanks.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Name is Mandatory')", true);
                    return;
                }
                if (cmbChequeNo.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No  is Mandatory')", true);
                    return;
                }
            }

            Page.Validate();

            if (Page.IsValid)
            {
                int DebitorID = 0;
                DebitorID = int.Parse(ddReceivedFrom.SelectedValue);

                int CreditorID = 0;

                string RefNo = txtRefNo.Text;

                DateTime TransDate = DateTime.Parse(txtTransDate.Text);


                string Paymode = string.Empty;
                double Amount = 0.0;
                string Narration = string.Empty;
                string VoucherType = string.Empty;
                string ChequeNo = string.Empty;
                string Billno = string.Empty;
                int TransNo = 0;

                if (chkPayTo.SelectedValue == "Cash")
                {
                    CreditorID = 1;
                    Paymode = "Cash";
                }
                else if (chkPayTo.SelectedValue == "Cheque")
                {
                    CreditorID = int.Parse(ddBanks.SelectedValue);
                    Paymode = "Cheque";
                }
                //#eae9e9;
                Amount = double.Parse(txtAmount.Text);
                Narration = txtNarration.Text;
                VoucherType = "Payment";
                ChequeNo = cmbChequeNo.SelectedItem.Text;

                BusinessLogic bl = new BusinessLogic();

                string connection = Request.Cookies["Company"].Value;

                TransNo = int.Parse(GrdViewPayment.SelectedDataKey.Value.ToString());

                if (chkPayTo.SelectedValue == "Cheque")
                {
                    if (ChequeNo != "")
                    {
                        if (bl.IsChequeNoAlreadyPresentForTransno(connection, ChequeNo, TransNo))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No Already Billed')", true);
                            return;
                        }

                        if (bl.GetDamageChequeNo(connection, ChequeNo))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No is noted as damaged. Select any other Cheque No')", true);
                            return;
                        }

                        DataSet dsdat = new DataSet();
                        dsdat = bl.GetChequeNoGiven(connection, ChequeNo);
                        string datad = string.Empty;

                        Int32 set = 0;
                        Int32 setdd = 0;

                        Int32 Cheque = 0;
                        Cheque = Convert.ToInt32(cmbChequeNo.SelectedItem.Text);

                        if (dsdat != null)
                        {
                            if (dsdat.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdat.Tables[0].Rows)
                                {
                                    set = Convert.ToInt32(dr["FromChequeNo"]);
                                    setdd = Convert.ToInt32(dr["ToChequeNo"]);

                                    if ((Cheque >= set) && (Cheque <= setdd))
                                    {
                                        datad = "Y";
                                        break;
                                    }
                                    else
                                    {
                                        datad = "N";
                                    }
                                }
                                if (datad == "N")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No Not Found in Cheque Book')", true);
                                    return;
                                }
                            }
                        }
                    }
                }



                //if (hdSMSRequired.Value == "YES")
                //{

                //    //if (txtMobile.Text != "")
                //    //    hdMobile.Value = txtMobile.Text;

                //    hdText.Value = "Thank you for Payment of Rs." + txtAmount.Text;

                //}

                //BusinessLogic bl = new BusinessLogic();
                string conn = GetConnectionString();
                int OutPut = 0;

                string Branch = drpBranchAdd.SelectedValue;
                DataSet ds = (DataSet)Session["BillData"];
                string usernam = Request.Cookies["LoggedUserName"].Value;
                bl.UpdatePaymentExp(out OutPut, conn, TransNo, RefNo, TransDate, DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, Paymode, Billno, usernam, Branch);
                //ichequestatus = bl.UpdateChequeused_conn(ChequeNo, CreditorID, conn);
                if (hid1.Value != ChequeNo)
                {
                    ichequestatus = bl.UpdateChequeused_conn(ChequeNo, CreditorID, conn);
                    ichequestatus = bl.RevertChequeused_conn(hid1.Value, CreditorID, conn);
                }

                string salestype = string.Empty;
                int ScreenNo = 0;
                string ScreenName = string.Empty;

                salestype = "Expense Payment";
                ScreenName = "Expense Payment";


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
                                    else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Expense") || (dr["Name1"].ToString() == "Bank") || (dr["Name1"].ToString() == "Supplier") || (dr["Name1"].ToString() == "Ledger"))
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
                                    body = Narration;
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

                                    int index = emailcontent.IndexOf("@Expense");
                                    body = ddReceivedFrom.SelectedItem.Text;
                                    if (index >= 0)
                                    {
                                        emailcontent = emailcontent.Remove(index, 8).Insert(index, body);
                                    }

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

                                    EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, body, fromPassword);

                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email sent successfully')", true);

                                }

                            }
                        }
                    }
                }

                string conn1 = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                UtilitySMS utilSMS = new UtilitySMS(conn1);
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
                                    else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Expense") || (dr["Name1"].ToString() == "Bank") || (dr["Name1"].ToString() == "Supplier") || (dr["Name1"].ToString() == "Ledger"))
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
                                    body = Narration;
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

                                    int index = smscontent.IndexOf("@Expense");
                                    body = ddReceivedFrom.SelectedItem.Text;
                                    if (index >= 0)
                                    {
                                        smscontent = smscontent.Remove(index, 8).Insert(index, body);
                                    }

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


                //if (hdSMS.Value == "YES")
                //{
                //    UtilitySMS utilSMS = new UtilitySMS(conn);
                //    string UserID = Page.User.Identity.Name;

                //    if (Session["Provider"] != null)
                //        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), hdMobile.Value, hdText.Value, true, UserID);
                //    else
                //    {
                //        if (hdMobile.Value != "")
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                //    }
                //}

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Payment Updated Successfully. Transaction No : " + OutPut.ToString() + "');", true);

                pnlEdit.Visible = false;
                //lnkBtnAdd.Visible = true;
                ////MyAccordion.Visible = true;
                //GrdViewReceipt.Visible = true;
                //ModalPopupExtender2.Hide();
                //popUp.Visible = false;
                ModalPopupExtender2.Hide();

                GrdViewPayment.DataBind();
                ClearPanel();
                UpdatePanelPage.Update();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }





    private void GetAmt()
    {
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");
        try
        {
            BusinessLogic bl = new BusinessLogic();
        }
        catch (Exception ex)
        {

        }
    }
}
