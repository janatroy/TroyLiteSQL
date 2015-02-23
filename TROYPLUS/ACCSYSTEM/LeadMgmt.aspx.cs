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

public partial class LeadMgmt : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    string dbfileName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (!Page.IsPostBack)
            {
                //myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

                BindDropdownList();
                Session["contactDs"] = null;
                Session["CompetitorDs"] = null;
                Session["ActivityDs"] = null;
                Session["ProductDs"] = null;
                Session["Date"] = null;
                loadDropDowns();
                //loadStages();
                //loadPotentialAmount();
                loadEmp();
                GrdViewLead.PageSize = 8;

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "LDMNGT"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New ";
                }
                BindGrid("Open", "DocStatus");
                drpLeadStatus.Enabled = true;
                drpStatus.Enabled = true;
                loadInformation3();
                loadInformation4();
                loadBusinessType();
                loadCategory();
                loadArea();
                loadInterestlevel();
                txtInformation1.Text = "";
                txtContactName.Text = "";

                CheckSMSRequired();

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

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "EMAILREQ")
                {
                    emailRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["EMAILREQUIRED"] = emailRequired.Trim().ToUpper();
                }

                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "OWNERMOB")
                {
                    Session["OWNERMOB"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }

            }
        }
    }

    private void loadInformation3()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpInformation3.Items.Clear();
        drpInformation3.Items.Add(new ListItem("Select Information 2", "0"));
        ds = bl.ListInformation2();
        drpInformation3.DataSource = ds;
        drpInformation3.DataBind();
        drpInformation3.DataTextField = "TextValue";
        drpInformation3.DataValueField = "ID";
    }

    private void loadInformation4()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpInformation4.Items.Clear();
        drpInformation4.Items.Add(new ListItem("Select Information 3", "0"));
        ds = bl.ListInformation3();
        drpInformation4.DataSource = ds;
        drpInformation4.DataBind();
        drpInformation4.DataTextField = "TextValue";
        drpInformation4.DataValueField = "ID";
    }

    private void loadBusinessType()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpBusinessType.Items.Clear();
        drpBusinessType.Items.Add(new ListItem("Select Business Type", "0"));
        ds = bl.ListBusinessType();
        drpBusinessType.DataSource = ds;
        drpBusinessType.DataBind();
        drpBusinessType.DataTextField = "TextValue";
        drpBusinessType.DataValueField = "ID";
    }

    private void loadCategory()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpCategory.Items.Clear();
        drpCategory.Items.Add(new ListItem("Select Category", "0"));
        ds = bl.ListCategory();
        drpCategory.DataSource = ds;
        drpCategory.DataBind();
        drpCategory.DataTextField = "TextValue";
        drpCategory.DataValueField = "ID";
    }

    private void loadArea()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpArea.Items.Clear();
        drpArea.Items.Add(new ListItem("Select Area", "0"));
        ds = bl.ListArea();
        drpArea.DataSource = ds;
        drpArea.DataBind();
        drpArea.DataTextField = "TextValue";
        drpArea.DataValueField = "ID";
    }

    private void loadInterestlevel()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpIntLevel.Items.Clear();
        drpIntLevel.Items.Add(new ListItem("Select Interest level", "0"));
        ds = bl.ListInterestLevel();
        drpIntLevel.DataSource = ds;
        drpIntLevel.DataBind();
        drpIntLevel.DataTextField = "TextValue";
        drpIntLevel.DataValueField = "ID";
    }

    protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ComboBox2.SelectedValue == "2")
            //{
            //    rowcall.Visible = true;
            //    ModalPopupContact.Show();
            //}
            //else
            //{
            //    rowcall.Visible = false;
            //    ModalPopupContact.Show();
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int iLedgerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
            DataSet customerDs = bl.getAddressInfo(iLedgerID);

            if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            {
                if (customerDs.Tables[0].Rows[0]["Add1"] != null)
                    txtAddress.Text = customerDs.Tables[0].Rows[0]["Add1"].ToString();

                //if (customerDs.Tables[0].Rows[0]["ContactName"] != null)
                //    txtContactName.Text =  customerDs.Tables[0].Rows[0]["ContactName"].ToString();

                if (customerDs.Tables[0].Rows[0]["phone"] != null)
                    txtTelephone.Text = customerDs.Tables[0].Rows[0]["phone"].ToString();

                if (customerDs.Tables[0].Rows[0]["Mobile"] != null)
                {
                    txtMobile.Text = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
                }
            }
            else
            {
                txtAddress.Text = string.Empty;
                // txtContactName.Text = string.Empty;
                txtTelephone.Text = string.Empty;
                txtMobile.Text = string.Empty;
            }

            DataSet Ds = bl.getSalesForId(iLedgerID);
            if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            {
                // txtTotalAmount.Text = Ds.Tables[0].Rows[0]["rate"].ToString();
            }
            else
            {
                // txtTotalAmount.Text = "0";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    private void loadDropDowns()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListSundryDebtorsExceptIsActive(sDataSource);
        cmbCustomer.DataSource = ds;
        cmbCustomer.DataBind();
        cmbCustomer.DataTextField = "LedgerName";
        cmbCustomer.DataValueField = "LedgerID";


    }

    private void loadStages()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet dsd = new DataSet();

        // drpStageName.Items.Clear();
        //drpStageName.Items.Add(new ListItem("Select Stage Name", "0"));
        dsd = bl.ListStagesSetup(sDataSource, "N", 0);
        //drpStageName.DataSource = dsd;
        //drpStageName.DataBind();
        //drpStageName.DataTextField = "Stage_Name";
        //drpStageName.DataValueField = "Stage_Setup_Id";
    }

    private void loadPotentialAmount()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet dsd = new DataSet();

        //txtStagePotentialAmount.Items.Clear();
        //txtStagePotentialAmount.Items.Add(new ListItem("Select Potential Amount", "0"));
        //dsd = bl.ListPotentialAmount(sDataSource, "N", 0);
        //txtStagePotentialAmount.DataSource = dsd;
        //txtStagePotentialAmount.DataBind();
        //txtStagePotentialAmount.DataTextField = "Potential_Amount";
        //txtStagePotentialAmount.DataValueField = "Potential_Amount";
    }

    private void loadproduct()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet dsd = new DataSet();

        drpproduct.Items.Clear();
        drpproduct.Items.Add(new ListItem("Select Product", "0"));
        dsd = bl.ListProducts(sDataSource, "", "");
        drpproduct.DataSource = dsd;
        drpproduct.DataBind();
        drpproduct.DataTextField = "ProductName";
        drpproduct.DataValueField = "ItemCode";
    }

    private void loadActivities()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet dsd = new DataSet();
        DataSet ds = new DataSet();

        LeadBusinessLogic bll = new LeadBusinessLogic(sDataSource);


        drpActivityName.Items.Clear();
        drpActivityName.Items.Add(new ListItem("Select Activity Name", "0"));
        //dsd = bl.ListActivitySetup(sDataSource, "N", 0);

        dsd = bll.GetDropdownList(sDataSource, "ACTIVITY");

        drpActivityName.DataSource = dsd;
        drpActivityName.DataBind();
        //OLD code
        //drpActivityName.DataTextField = "Activity_Name";
        //drpActivityName.DataValueField = "Activity_Setup_Id";
        //New code
        drpActivityName.DataTextField = "TextValue";
        drpActivityName.DataValueField = "TextValue";

        drpNextActivity.Items.Clear();
        drpNextActivity.Items.Add(new ListItem("Select Next Activity", "0"));
        drpNextActivity.DataSource = dsd;
        drpNextActivity.DataBind();
        drpNextActivity.DataTextField = "TextValue";
        drpNextActivity.DataValueField = "TextValue";

        drpActivityEmployee.Items.Clear();
        drpActivityEmployee.Items.Add(new ListItem("Select Employee", "0"));
        ds = bl.ListExecutive();
        drpActivityEmployee.DataSource = ds;
        drpActivityEmployee.DataBind();
        drpActivityEmployee.DataTextField = "empFirstName";
        drpActivityEmployee.DataValueField = "empno";

    }

    protected void drpStageName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet dsd = new DataSet();
            // int iStageID = Convert.ToInt32(drpStageName.SelectedItem.Value);

            //dsd = bl.ListStagesSetup(sDataSource, "Y", iStageID);

            if (dsd != null && dsd.Tables[0].Rows.Count > 0)
            {
                //if (dsd.Tables[0].Rows[0]["Stage_Perc"] != null)
                //txtStagePerc.Text = dsd.Tables[0].Rows[0]["Stage_Perc"].ToString();
            }
            //UpdatePanel1.Update();

            // double calculation = (Convert.ToDouble(txtStagePotentialAmount.Text) * Convert.ToDouble(txtStagePerc.Text)) / 100;
            // txtStageWeightedAmount.Text = Convert.ToString(calculation);
            // UpdatePanel8.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void txtStagePotentialAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            // double calculation = (Convert.ToDouble(txtStagePotentialAmount.Text) * Convert.ToDouble(txtStagePerc.Text)) / 100;
            // txtStageWeightedAmount.Text = Convert.ToString(calculation);
            // UpdatePanel8.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void drpPredictedClosingPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime date = Convert.ToDateTime(txtCreationDate.Text);

            //string dtaa = Convert.ToDateTime(date).ToString("dd/MM/yyyy");              
            //DateTime calculat = Convert.ToDateTime(dtaa);

            //if (drpPredictedClosingPeriod.SelectedValue == "Days")
            //{
            //    int days = Convert.ToInt32(txtPredictedClosing.Text);

            //    DateTime dat = date.AddDays(days);
            //    txtPredictedClosingDate.Text = dat.ToString("dd/MM/yyyy");
            //}
            //else if (drpPredictedClosingPeriod.SelectedValue == "Months")
            //{
            //    int Months = Convert.ToInt32(txtPredictedClosing.Text);

            //    DateTime dat = date.AddMonths(Months);
            //    txtPredictedClosingDate.Text = dat.ToString("dd/MM/yyyy");
            //}
            //else if (drpPredictedClosingPeriod.SelectedValue == "Weeks")
            //{
            //    int Weeks = Convert.ToInt32(txtPredictedClosing.Text);
            //    Weeks = 7 * Weeks;
            //    DateTime dat = date.AddDays(Weeks);
            //    txtPredictedClosingDate.Text = dat.ToString("dd/MM/yyyy");
            //}

            //UpdatePanel123.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpStatus.SelectedValue == "Closed")
            {
                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                txtClosingDate.Text = dtaa;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void txtStagePerc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            // double calculation = (Convert.ToDouble(txtStagePotentialAmount.Text) * Convert.ToDouble(txtStagePerc.Text)) /100;
            // txtStageWeightedAmount.Text = Convert.ToString(calculation);
            // UpdatePanel8.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtPredictedClosing_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime date = Convert.ToDateTime(txtCreationDate.Text);

            //string dtaa = Convert.ToDateTime(date).ToString("dd/MM/yyyy");              
            //DateTime calculat = Convert.ToDateTime(dtaa);

            //if(drpPredictedClosingPeriod.SelectedValue == "Days")
            //{
            //    int days = Convert.ToInt32(txtPredictedClosing.Text);

            //    DateTime dat = date.AddDays(days);
            //    txtPredictedClosingDate.Text = dat.ToString("dd/MM/yyyy");
            //}
            //else if (drpPredictedClosingPeriod.SelectedValue == "Months")
            //{
            //    int Months = Convert.ToInt32(txtPredictedClosing.Text);

            //    DateTime dat = date.AddMonths(Months);
            //    txtPredictedClosingDate.Text = dat.ToString("dd/MM/yyyy");
            //}
            //else if (drpPredictedClosingPeriod.SelectedValue == "Weeks")
            //{
            //    int Weeks = Convert.ToInt32(txtPredictedClosing.Text);
            //    Weeks = 7 * Weeks;
            //    DateTime dat = date.AddDays(Weeks);
            //    txtPredictedClosingDate.Text = dat.ToString("dd/MM/yyyy");
            //}


            //UpdatePanel123.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtPredictedClosingDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //string stdate = ((TextBox)this.FindControl("tabs2").FindControl("tabMaster").FindControl("txtCreationDate")).Text;
            //DateTime calculat = Convert.ToDateTime(stdate);
            //calculat.AddDays(Convert.ToUInt32(txtPredictedClosing.Text));
            //txtPredictedClosingDate.Text = Convert.ToString(calculat);
            //UpdatePanel10.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpNextActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpActivityName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
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
            string textt = string.Empty;
            string dropd = string.Empty;

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;

            BindGrid(textt, dropd);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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
        LeadBusinessLogic bl = new LeadBusinessLogic(sDataSource);

        object usernam = Session["LoggedUserName"];

        ds = bl.ListLead(connection, textSearch, dropDown);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewLead.DataSource = ds.Tables[0].DefaultView;
                GrdViewLead.DataBind();
            }
        }
        else
        {
            GrdViewLead.DataSource = null;
            GrdViewLead.DataBind();
        }
    }

    protected void GrdCompetitors_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.Pager)
            //{
            //    PresentationUtils.SetPagerButtonStates(GrdCompetitors, e.Row, this);
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdCompetitors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //if (e.CommandName == "Cancel")
            //{
            //    if (GrdCompetitors.Rows.Count == 0)
            //    {
            //        DataSet ds = new DataSet();
            //        DataTable dt = new DataTable();
            //        DataColumn dc;

            //        dc = new DataColumn("Competitor_Name");
            //        dt.Columns.Add(dc);
            //        dc = new DataColumn("Competitor_Id");
            //        dt.Columns.Add(dc);
            //        ds.Tables.Add(dt);

            //        DataRow dr = ds.Tables[0].NewRow();

            //        dr["Competitor_Name"] = "";
            //        dr["Competitor_Id"] = 0;
            //        ds.Tables[0].Rows.InsertAt(dr, 0);

            //        GrdCompetitors.DataSource = ds;
            //        GrdCompetitors.DataBind();
            //        GrdCompetitors.Rows[0].Visible = false;
            //    }

            //    GrdCompetitors.FooterRow.Visible = false;
            //    lnkBtnCompetitor.Visible = true;
            //}
            //else if (e.CommandName == "Insert")
            //{
            //    if (!Page.IsValid)
            //    {
            //        foreach (IValidator validator in Page.Validators)
            //        {
            //            if (!validator.IsValid)
            //            {
            //                //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        BusinessLogic objBus = new BusinessLogic();

            //        string unit = ((TextBox)GrdCompetitors.FooterRow.FindControl("txtAddUnit")).Text;

            //        string sQl = string.Format("Insert Into tblCompetitors(Competitor_Name) Values('{0}')", unit);
            //        string connection = Request.Cookies["Company"].Value;

            //        //srcGridView.InsertParameters.Add("sQl", TypeCode.String, sQl);
            //        //srcGridView.InsertParameters.Add("connection", TypeCode.String, GetConnectionString());

            //        try
            //        {
            //            //srcGridView.Insert();
            //            objBus.InsertUnitRecord(sQl, connection);
            //            System.Threading.Thread.Sleep(1000);
            //            GrdCompetitors.DataBind();
            //            lnkBtnCompetitor.Visible = true;
            //        }
            //        catch (Exception ex)
            //        {
            //            if (ex.InnerException != null)
            //            {
            //                StringBuilder script = new StringBuilder();
            //                script.Append("alert('Competitors with this name already exists, Please try with a different name.');");

            //                if (ex.InnerException.Message.IndexOf("duplicate values in the index") > -1)
            //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

            //                return;
            //            }
            //        }
            //        //lnkBtnAdd.Visible = true;
            //    }


            //}
            //else if (e.CommandName == "Update")
            //{
            //    if (!Page.IsValid)
            //    {
            //        foreach (IValidator validator in Page.Validators)
            //        {
            //            if (!validator.IsValid)
            //            {
            //                //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
            //            }
            //        }
            //        return;
            //    }
            //}
            //else if (e.CommandName == "Edit")
            //{
            //    lnkBtnCompetitor.Visible = false;
            //}
            //else if (e.CommandName == "Page")
            //{
            //    //lnkBtnAdd.Visible = true;
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdCompetitors_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        try
        {
            //System.Threading.Thread.Sleep(1000);
            //GrdCompetitors.DataBind();
            //lnkBtnCompetitor.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdCompetitors_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //if (!Page.IsValid)
            //{
            //    foreach (IValidator validator in Page.Validators)
            //    {
            //        if (!validator.IsValid)
            //        {
            //            //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
            //        }
            //    }

            //}
            //else
            //{

            //    string unit = ((TextBox)GrdCompetitors.Rows[e.RowIndex].FindControl("txtUnit")).Text;
            //    int Id = Convert.ToInt32(GrdCompetitors.DataKeys[e.RowIndex].Value);

            //    BusinessLogic objBus = new BusinessLogic();
            //    string connection = Request.Cookies["Company"].Value;
            //    objBus.UpdateCompetitors(connection, unit, Id);

            //    //srcGridView.UpdateMethod = "UpdateCompetitors";
            //    //srcGridView.UpdateParameters.Add("connection", TypeCode.String, GetConnectionString());
            //    //srcGridView.UpdateParameters.Add("Unit", TypeCode.String, unit);
            //    //srcGridView.UpdateParameters.Add("ID", TypeCode.Int32, Id);
            //    //lnkBtnAdd.Visible = true;

            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindDropdownList()
    {
        LeadBusinessLogic bl = new LeadBusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        //ds = bl.GetDropdownList(sDataSource, "CONTACT");
        //cmbModeOfContact.DataSource = ds;
        //cmbModeOfContact.DataBind();
        //cmbModeOfContact.DataTextField = "TextValue";
        //cmbModeOfContact.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "PERRES");
        //cmbPersonalResp.DataSource = ds;
        //cmbPersonalResp.DataBind();
        //cmbPersonalResp.DataTextField = "TextValue";
        //cmbPersonalResp.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "BUSTYPE");
        //cmbBussType.DataSource = ds;
        //cmbBussType.DataBind();
        //cmbBussType.DataTextField = "TextValue";
        //cmbBussType.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "BRNCH");
        //cmbBranch.DataSource = ds;
        //cmbBranch.DataBind();
        //cmbBranch.DataTextField = "TextValue";
        //cmbBranch.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "LSTCMP");
        //cmbLastCompAction.DataSource = ds;
        //cmbLastCompAction.DataBind();
        //cmbLastCompAction.DataTextField = "TextValue";
        //cmbLastCompAction.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "LSTCMP");
        //cmblastaction.DataSource = ds;
        //cmblastaction.DataBind();
        //cmblastaction.DataTextField = "TextValue";
        //cmblastaction.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "NXTAXN");
        //cmbNextAction.DataSource = ds;
        //cmbNextAction.DataBind();
        //cmbNextAction.DataTextField = "TextValue";
        //cmbNextAction.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "NXTAXN");
        //cmbnxtaction.DataSource = ds;
        //cmbnxtaction.DataBind();
        //cmbnxtaction.DataTextField = "TextValue";
        //cmbnxtaction.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "CATEGRY");
        //cmbCategory.DataSource = ds;
        //cmbCategory.DataBind();
        //cmbCategory.DataTextField = "TextValue";
        //cmbCategory.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "STATUS");
        //cmbStatus.DataSource = ds;
        //cmbStatus.DataBind();
        //cmbStatus.DataTextField = "TextValue";
        //cmbStatus.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "STATUS");
        //cmbnewstatus.DataSource = ds;
        //cmbnewstatus.DataBind();
        //cmbnewstatus.DataTextField = "TextValue";
        //cmbnewstatus.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "INFO3");
        //ddlinfo3.DataSource = ds;
        //ddlinfo3.DataBind();
        //ddlinfo3.DataTextField = "TextValue";
        //ddlinfo3.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "INFO4");
        //ddlinfo4.DataSource = ds;
        //ddlinfo4.DataBind();
        //ddlinfo4.DataTextField = "TextValue";
        //ddlinfo4.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "INFO5");
        //ddlinfo5.DataSource = ds;
        //ddlinfo5.DataBind();
        //ddlinfo5.DataTextField = "TextValue";
        //ddlinfo5.DataValueField = "TextValue";

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
            Reset();
            ModalPopupExtender2.Show();
            UpdateButton.Visible = false;
            AddButton.Visible = true;
            UpdateButton.Visible = false;
            Session["Lead_No"] = "0";
            Session["contactDs"] = null;
            ShowLeadContactInfo();

            Session["CompetitorDs"] = null;
            GrdViewLeadCompetitor.DataSource = null;
            GrdViewLeadCompetitor.DataBind();

            Session["ActivityDs"] = null;
            GrdViewLeadActivity.DataSource = null;
            GrdViewLeadActivity.DataBind();

            Session["ProductDs"] = null;
            GrdViewLeadproduct.DataSource = null;
            GrdViewLeadproduct.DataBind();

            Session["Date"] = "Add";

            //txtCreationDate.Text = DateTime.Now.ToShortDateString();

            // BtnAddStage.Visible = true;
            // pnlStage.Visible = false;
            // GrdViewLeadStage.Visible = true;
            loadEmp();

            // BtnAddproduct.Visible = true;
            GrdViewLeadproduct.Visible = true;
            pnlproduct.Visible = false;

            // BtnAddCompetitor.Visible = true;
            GrdViewLeadCompetitor.Visible = true;
            pnlCompetitor.Visible = false;

            // BtnAddActivity.Visible = true;
            GrdViewLeadActivity.Visible = true;
            pnlActivity.Visible = false;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn("Competitor_Name");
            dt.Columns.Add(dc);
            dc = new DataColumn("Competitor_Id");
            dt.Columns.Add(dc);
            ds.Tables.Add(dt);

            DataRow dr = ds.Tables[0].NewRow();

            dr["Competitor_Name"] = "";
            dr["Competitor_Id"] = 0;
            ds.Tables[0].Rows.InsertAt(dr, 0);

            //GrdCompetitors.DataSource = ds;
            //GrdCompetitors.DataBind();
            //GrdCompetitors.Rows[0].Visible = false;

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtCreationDate.Text = dtaa;


            string DBname = string.Empty;
            DBname = GetCurrentDBName(sDataSource);
            string a = string.Empty;
            string b = string.Empty;
            for (int i = 0; i < DBname.Length; i++)
            {
                if (Char.IsDigit(DBname[i]))
                    b += DBname[i];
                else
                    a += DBname[i];
            }


            //txtBranch.Text = a;
            drpLeadStatus.Enabled = false;
            drpStatus.Enabled = false;

            txtLeadNo.Text = "Auto Generated.No need to enter";
            txtCreationDate.Focus();
            BindStage();
            FirstGridViewRow_ProductTab();
            FirstGridViewRow_CompetitorsTab();
            FirstGridViewRow_ActivityTab();
            //DropDownList1.SelectedItem.Text = "NO";
            loadInformation3();
            loadInformation4();
            loadBusinessType();
            loadCategory();
            loadArea();
            loadInterestlevel();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpIncharge.Items.Clear();
        drpIncharge.Items.Add(new ListItem("Select Employee", "0"));
        ds = bl.ListExecutive();
        drpIncharge.DataSource = ds;
        drpIncharge.DataBind();
        drpIncharge.DataTextField = "empFirstName";
        drpIncharge.DataValueField = "empno";
    }

    public string GetCurrentDBName(string con)
    {
        string str = con; // "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\DemoDev\\Accsys\\App_Data\\sairama.mdb;Persist Security Info=True;Jet OLEDB:Database Password=moonmoon";
        string str2 = string.Empty;
        if (str.Contains(".mdb"))
        {
            str2 = str.Substring(str.IndexOf("Data Source"), str.IndexOf("Persist", 0));
            str2 = str2.Substring(str2.LastIndexOf("\\") + 1, str2.IndexOf(";") - str2.LastIndexOf("\\"));
            if (str2.EndsWith(";"))
            {
                str2 = str2.Remove(str2.Length - 5, 5);
            }
        }
        return str2;
    }

    protected void lnkBtnCompetitor_Click(object sender, EventArgs e)
    {
        try
        {

            //if (GrdCompetitors.Rows.Count == 0)
            //{
            //    DataSet ds = new DataSet();
            //    DataTable dt = new DataTable();
            //    DataColumn dc;

            //    dc = new DataColumn("Competitor_Name");
            //    dt.Columns.Add(dc);
            //    dc = new DataColumn("Competitor_Id");
            //    dt.Columns.Add(dc);
            //    ds.Tables.Add(dt);

            //    DataRow dr = ds.Tables[0].NewRow();

            //    dr["Competitor_Name"] = "";
            //    dr["Competitor_Id"] = 0;
            //    ds.Tables[0].Rows.InsertAt(dr, 0);

            //    GrdCompetitors.DataSource = ds;
            //    GrdCompetitors.DataBind();
            //    GrdCompetitors.Rows[0].Visible = false;
            //}


            //GrdCompetitors.FooterRow.Visible = true;
            //lnkBtnCompetitor.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void GrdStage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //GrdStage.PageIndex = e.NewPageIndex;
            //BindStage();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdStage_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.Pager)
            //{
            //    PresentationUtils.SetPagerButtonStates(GrdStage, e.Row, this);
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdStage_DataBound(object sender, EventArgs e)
    {


    }


    protected void GrdStage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //int divisionID = (int)GrdStage.DataKeys[e.RowIndex].Value;
            //bl.DeleteDivision(sDataSource, divisionID);
            //GrdStage.DataSource = bl.ListDivisions();
            //GrdStage.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //pnlStage.Visible = true;
            //btnDivSave.Visible = false;
            //btnDivUpdate.Visible = true;
            //GrdStage.Visible = false;
            //BtnAddStage.Visible = false;

            //sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //hdBtnAddStage.Value = GrdStage.SelectedDataKey.Value.ToString();
            //DataSet ds = bl.GetDivisionForId(sDataSource, int.Parse(GrdStage.SelectedDataKey.Value.ToString()));

            //if (ds != null)
            //{
            //txtDivision.Text = ds.Tables[0].Rows[0]["DivisionName"].ToString();
            //txtDivAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            //txtDivCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
            //txtDivState.Text = ds.Tables[0].Rows[0]["State"].ToString();
            //txtDivPinNo.Text = ds.Tables[0].Rows[0]["PinCode"].ToString();
            //txtDivPhoneNo.Text = ds.Tables[0].Rows[0]["Phone"].ToString();
            //txtDivEmail.Text = ds.Tables[0].Rows[0]["eMail"].ToString();
            //txtDivFax.Text = ds.Tables[0].Rows[0]["Fax"].ToString();
            //txtDivTinNo.Text = ds.Tables[0].Rows[0]["TINNo"].ToString();
            //txtDivGSTNo.Text = ds.Tables[0].Rows[0]["GSTNo"].ToString();
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnDivSave_Click(object sender, EventArgs e)
    {
        try
        {
            //sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            //BusinessLogic bl = new BusinessLogic(sDataSource);

            ////bl.InsertDivision(sDataSource, txtDivision.Text, txtDivAddress.Text, txtDivCity.Text, txtDivState.Text, txtDivPinNo.Text, txtDivPhoneNo.Text, txtDivFax.Text, txtDivEmail.Text, txtDivTinNo.Text, txtDivGSTNo.Text);
            //System.Threading.Thread.Sleep(1000);
            //DataSet ds = bl.ListDivisions();
            //GrdStage.DataSource = ds;
            //GrdStage.DataBind();
            //pnlStage.Visible = false;
            //GrdStage.Visible = true;
            //BtnAddStage.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnAddStage_Click(object sender, EventArgs e)
    {
        try
        {
            ResetStage();
            //BtnAddStage.Visible = false;
            //cmdSaveContact.Visible = true;
            //cmdUpdateContact.Visible = false;
            //pnlStage.Visible = true;
            //GrdViewLeadStage.Visible = false;
            loadStages();

            //txtStagePotentialAmount.Text = txtPotentialPotAmount.Text;

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnAddCompetitor_Click(object sender, EventArgs e)
    {
        try
        {
            ResetCompetitor();
            // BtnAddCompetitor.Visible = false;
            cmdSaveCompetitor.Visible = true;
            cmdUpdateCompetitor.Visible = false;
            pnlCompetitor.Visible = true;
            GrdViewLeadCompetitor.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnAddActivity_Click(object sender, EventArgs e)
    {
        try
        {
            loadActivities();
            ResetActivity();
            // BtnAddActivity.Visible = false;
            cmdSaveActivity.Visible = true;
            cmdUpdateActivity.Visible = false;
            pnlActivity.Visible = true;
            GrdViewLeadActivity.Visible = false;

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnAddproduct_Click(object sender, EventArgs e)
    {
        try
        {
            loadproduct();
            Resetproduct();
            BtnAddproduct.Visible = false;
            cmdSaveproduct.Visible = true;
            cmdUpdateproduct.Visible = false;
            pnlproduct.Visible = true;
            GrdViewLeadproduct.Visible = false;
            //ModalPopupExtender2.Show();
            //ModalPopupProduct.Show();
            //updatePnlProduct.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnDivCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //pnlStage.Visible = false;
            //GrdStage.Visible = true;
            //BtnAddStage.Visible = true;
            //ResetStage();
            //BindStage();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindStage()
    {
        //sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        //BusinessLogic bl = new BusinessLogic(sDataSource);

        //GrdStage.DataSource = bl.ListDivisions();
        ///*if(ds.Tables[0].Rows.Count > 1)
        //    GrdDiv.DataSource = ds;
        //else
        //    GrdDiv.DataSource = null;*/

        //GrdStage.DataBind();
    }

    private void ResetStage()
    {
        // hdBtnAddStage.Value = string.Empty;
        // txtStageStartDate.Text = string.Empty;
        //  txtStageEndDate.Text = string.Empty;
        //  drpStageName.SelectedIndex = 0;
        // txtStagePerc.Text = "0";
        // txtStagePotentialAmount.Text = "0";
        // txtStageWeightedAmount.Text = "0";
        //  txtStageRemarks.Text = string.Empty;
    }

    private void ResetCompetitor()
    {
        HiddenField2.Value = string.Empty;
        drpThreatLevel.SelectedIndex = 0;
        txtCompetitorName.Text = string.Empty;
        txtCompetitorRemarks.Text = string.Empty;
    }

    private void Resetproduct()
    {
        HiddenField6.Value = string.Empty;
        drpproduct.SelectedIndex = 0;
    }

    private void ResetActivity()
    {
        HiddenField4.Value = string.Empty;
        drpActivityName.SelectedIndex = 0;
        drpNextActivity.SelectedIndex = 0;
        txtActivityDate.Text = string.Empty;
        txtActivityLocation.Text = string.Empty;
        //txtActivityEndDate.Text = string.Empty;
        drpActivityEmployee.SelectedIndex = 0;
        txtNextActivityDate.Text = string.Empty;
        //drpFollowUp.SelectedIndex = 0;
        txtActivityRemarks.Text = string.Empty;
    }

    protected void btnDivUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            //sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            //BusinessLogic bl = new BusinessLogic(sDataSource);

            ////bl.UpdateDivision(sDataSource, int.Parse(hdDivision.Value.ToString()), txtDivision.Text, txtDivAddress.Text, txtDivCity.Text, txtDivState.Text, txtDivPinNo.Text, txtDivPhoneNo.Text, txtDivFax.Text, txtDivEmail.Text, txtDivTinNo.Text, txtDivGSTNo.Text);
            //System.Threading.Thread.Sleep(1000);
            //DataSet ds = bl.ListDivisions();
            //GrdStage.DataSource = ds;
            //GrdStage.DataBind();
            //pnlStage.Visible = false;
            //GrdStage.Visible = true;
            //BtnAddStage.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private void Reset()
    {
        txtLeadNo.Text = "";
        txtAddress.Text = "";
        cmbCustomer.SelectedIndex = 0;
        txtMobile.Text = "";
        txtLeadName.Text = "";
        txtTelephone.Text = "";
        //drpInterestLevel.SelectedIndex = 0;
        drpIncharge.SelectedIndex = 0;
        drpLeadStatus.SelectedIndex = 0;
        drpStatus.SelectedIndex = 0;
        //drpPredictedClosingPeriod.SelectedIndex = 0;
        //txtTotalAmount.Text = "0";
        // txtClosingPer.Text = "0";
        txtClosingDate.Text = "";
        //txtContactName.Text = "";
        //txtPotentialPotAmount.Text = "";
        //txtPotentialWeightedAmount.Text = "";
        txtPredictedClosingDate.Text = "";
        //txtPredictedClosing.Text = "";
        //txtBranch.Text = "";
        chk.Checked = true;
        txtBPName.Text = "";
        cmbCustomer.Visible = true;
        txtBPName.Visible = false;
        txtInformation1.Text = "";
        txtContactName.Text = "";
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (chk.Checked == false)
        {
            txtBPName.Visible = true;
            cmbCustomer.Visible = false;
            // txtContactName.Enabled = true;
            txtAddress.Enabled = true;
            txtMobile.Enabled = true;
            txtTelephone.Enabled = true;
        }
        else
        {
            cmbCustomer.Visible = true;
            txtBPName.Visible = false;
            // txtContactName.Enabled = false;
            txtAddress.Enabled = false;
            txtMobile.Enabled = false;
            txtTelephone.Enabled = false;
        }
    }

    private void ShowLeadContactInfo()
    {
        string connStr = GetConnectionString();

        if (Session["Lead_No"] != null && Session["Lead_No"].ToString() != "0")
        {
            LeadBusinessLogic bl = new LeadBusinessLogic(connStr);
            //DataSet ds = bl.ListLeadContact(Session["Lead_No"].ToString());

            //if (ds != null)
            //{
            //    GrdViewLeadStage.DataSource = ds.Tables[0];
            //    GrdViewLeadStage.DataBind();
            //}
            // GrdViewLeadStage.DataSource = null;
            // GrdViewLeadStage.DataBind();
        }
        else
        {
            // GrdViewLeadStage.DataSource = null;
            // GrdViewLeadStage.DataBind();
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

    protected void cmdSaveContact_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;
            ds = new DataSet();

            if (Session["contactDs"] == null)
            {
                dt = new DataTable();

                dc = new DataColumn("Stage_Id");
                dt.Columns.Add(dc);

                dc = new DataColumn("Stage_Name");
                dt.Columns.Add(dc);

                dc = new DataColumn("Stage_Perc");
                dt.Columns.Add(dc);

                dc = new DataColumn("Start_Date");
                dt.Columns.Add(dc);

                dc = new DataColumn("End_Date");
                dt.Columns.Add(dc);

                dc = new DataColumn("Potential_Amount");
                dt.Columns.Add(dc);

                dc = new DataColumn("Weighted_Amount");
                dt.Columns.Add(dc);

                dc = new DataColumn("Remarks");
                dt.Columns.Add(dc);

                dc = new DataColumn("Stage_Setup_Id");
                dt.Columns.Add(dc);

                ds.Tables.Add(dt);

                drNew = dt.NewRow();

                drNew["Stage_Id"] = 1;
                // drNew["Stage_Name"] = drpStageName.SelectedItem.Text;
                // drNew["Stage_Setup_Id"] = drpStageName.SelectedValue;
                // drNew["Stage_Perc"] = txtStagePerc.Text;
                //txtClosingPer.Text = txtStagePerc.Text;
                // txtPotentialWeightedAmount.Text = txtStageWeightedAmount.Text;
                // drNew["Remarks"] = txtStageRemarks.Text;
                // drNew["Weighted_Amount"] = txtStageWeightedAmount.Text;
                // drNew["Potential_Amount"] = txtStagePotentialAmount.Text;
                // drNew["Start_Date"] = txtStageStartDate.Text;
                // drNew["End_Date"] = txtStageEndDate.Text;
                ds.Tables[0].Rows.Add(drNew);
                Session["contactDs"] = ds;
            }
            else
            {
                ds = (DataSet)Session["contactDs"];

                int maxID = 0;

                if (ds.Tables[0].Rows.Count > 0)
                    maxID = int.Parse(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["Stage_Id"].ToString());

                drNew = ds.Tables[0].NewRow();
                drNew["Stage_Id"] = maxID + 1;
                //drNew["Stage_Name"] = drpStageName.SelectedItem.Text;
                // drNew["Stage_Setup_Id"] = drpStageName.SelectedValue;
                // drNew["Stage_Perc"] = txtStagePerc.Text;
                //txtClosingPer.Text = txtStagePerc.Text;
                //txtPotentialWeightedAmount.Text = txtStageWeightedAmount.Text;
                //drNew["Remarks"] = txtStageRemarks.Text;
                //drNew["Weighted_Amount"] = txtStageWeightedAmount.Text;
                //drNew["Potential_Amount"] = txtStagePotentialAmount.Text;
                //drNew["Start_Date"] = txtStageStartDate.Text;
                //drNew["End_Date"] = txtStageEndDate.Text;
                ds.Tables[0].Rows.Add(drNew);
                Session["contactDs"] = ds;
            }

            //GrdViewLeadStage.DataSource = ds.Tables[0];
            //GrdViewLeadStage.DataBind();

            //pnlStage.Visible = false;
            //GrdViewLeadStage.Visible = true;
            //BtnAddStage.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdSaveCompetitor_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;
            ds = new DataSet();

            if (Session["CompetitorDs"] == null)
            {
                dt = new DataTable();

                dc = new DataColumn("Competitor_Id");
                dt.Columns.Add(dc);

                dc = new DataColumn("Competitor_Name");
                dt.Columns.Add(dc);

                dc = new DataColumn("Threat_Level");
                dt.Columns.Add(dc);

                dc = new DataColumn("Remarks");
                dt.Columns.Add(dc);

                ds.Tables.Add(dt);

                drNew = dt.NewRow();

                drNew["Competitor_Id"] = 1;
                drNew["Competitor_Name"] = txtCompetitorName.Text;
                drNew["Threat_Level"] = drpThreatLevel.SelectedItem.Text;
                if (txtCompetitorRemarks.Text == "")
                {
                    drNew["Remarks"] = "";
                }
                else
                {
                    drNew["Remarks"] = txtCompetitorRemarks.Text;
                }
                ds.Tables[0].Rows.Add(drNew);
                Session["CompetitorDs"] = ds;
            }
            else
            {
                ds = (DataSet)Session["CompetitorDs"];

                int maxID = 0;

                if (ds.Tables[0].Rows.Count > 0)
                    maxID = int.Parse(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["Competitor_Id"].ToString());

                drNew = ds.Tables[0].NewRow();
                drNew["Competitor_Id"] = maxID + 1;
                drNew["Competitor_Name"] = txtCompetitorName.Text;
                drNew["Threat_Level"] = drpThreatLevel.SelectedItem.Text;
                if (txtCompetitorRemarks.Text == "")
                {
                    drNew["Remarks"] = "";
                }
                else
                {
                    drNew["Remarks"] = txtCompetitorRemarks.Text;
                }
                ds.Tables[0].Rows.Add(drNew);
                Session["CompetitorDs"] = ds;
            }

            GrdViewLeadCompetitor.DataSource = ds.Tables[0];
            GrdViewLeadCompetitor.DataBind();

            pnlCompetitor.Visible = false;
            GrdViewLeadCompetitor.Visible = true;
            //BtnAddCompetitor.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdSaveproduct_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;
            ds = new DataSet();

            if (Session["ProductDs"] == null)
            {
                dt = new DataTable();

                dc = new DataColumn("Product_interest_Id");
                dt.Columns.Add(dc);

                dc = new DataColumn("Product_Name");
                dt.Columns.Add(dc);

                dc = new DataColumn("SlNo");
                dt.Columns.Add(dc);

                dc = new DataColumn("Product_Id");
                dt.Columns.Add(dc);

                ds.Tables.Add(dt);

                drNew = dt.NewRow();

                drNew["Product_interest_Id"] = 1;
                drNew["Product_Name"] = drpproduct.SelectedItem.Text;
                drNew["SlNo"] = 1;
                drNew["Product_Id"] = drpproduct.SelectedValue;
                ds.Tables[0].Rows.Add(drNew);
                Session["ProductDs"] = ds;
            }
            else
            {
                ds = (DataSet)Session["ProductDs"];

                int maxID = 0;

                if (ds.Tables[0].Rows.Count > 0)
                    maxID = int.Parse(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["Product_interest_Id"].ToString());

                drNew = ds.Tables[0].NewRow();
                drNew["Product_interest_Id"] = maxID + 1;
                drNew["Product_Name"] = drpproduct.SelectedItem.Text;
                drNew["SlNo"] = maxID + 1;
                drNew["Product_Id"] = drpproduct.SelectedValue;
                ds.Tables[0].Rows.Add(drNew);
                Session["ProductDs"] = ds;
            }

            GrdViewLeadproduct.DataSource = ds.Tables[0];
            GrdViewLeadproduct.DataBind();

            pnlproduct.Visible = false;
            GrdViewLeadproduct.Visible = true;
            // BtnAddproduct.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdUpdateContact_Click(object sender, EventArgs e)
    {
        try
        {
            var ds = (DataSet)Session["contactDs"];
            // int currentRow = int.Parse(hdCurrentRow.Value);

            //ds.Tables[0].Rows[currentRow]["Stage_Name"] = drpStageName.SelectedItem.Text;
            //ds.Tables[0].Rows[currentRow]["Stage_Setup_Id"] = drpStageName.SelectedValue;
            //ds.Tables[0].Rows[currentRow]["Stage_Perc"] = txtStagePerc.Text;
            //ds.Tables[0].Rows[currentRow]["Remarks"] = txtStageRemarks.Text;
            //ds.Tables[0].Rows[currentRow]["Weighted_Amount"] = txtStageWeightedAmount.Text;
            //ds.Tables[0].Rows[currentRow]["Potential_Amount"] = txtStagePotentialAmount.Text;
            //ds.Tables[0].Rows[currentRow]["Start_Date"] = txtStageStartDate.Text;
            //ds.Tables[0].Rows[currentRow]["End_Date"] = txtStageEndDate.Text;

            //ds.Tables[0].Rows[currentRow].EndEdit();
            // ds.Tables[0].Rows[currentRow].AcceptChanges();

            //GrdViewLeadStage.DataSource = ds.Tables[0];
            //GrdViewLeadStage.DataBind();
            //pnlStage.Visible = false;
            Session["contactDs"] = ds;

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //txtPotentialWeightedAmount.Text = dr["Weighted_Amount"].ToString();
                    }
                }
            }

            // pnlStage.Visible = false;
            //GrdViewLeadStage.Visible = true;
            //BtnAddStage.Visible = true;

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdUpdateCompetitor_Click(object sender, EventArgs e)
    {
        try
        {
            var ds = (DataSet)Session["CompetitorDs"];
            int currentRow = int.Parse(HiddenField1.Value);

            ds.Tables[0].Rows[currentRow]["Competitor_Name"] = txtCompetitorName.Text;
            ds.Tables[0].Rows[currentRow]["Threat_Level"] = drpThreatLevel.SelectedItem.Text;
            if (txtCompetitorRemarks.Text == "")
            {
                ds.Tables[0].Rows[currentRow]["Remarks"] = "";
            }
            else
            {
                ds.Tables[0].Rows[currentRow]["Remarks"] = txtCompetitorRemarks.Text;
            }

            ds.Tables[0].Rows[currentRow].EndEdit();
            ds.Tables[0].Rows[currentRow].AcceptChanges();

            GrdViewLeadCompetitor.DataSource = ds.Tables[0];
            GrdViewLeadCompetitor.DataBind();
            pnlCompetitor.Visible = false;
            Session["CompetitorDs"] = ds;

            pnlCompetitor.Visible = false;
            GrdViewLeadCompetitor.Visible = true;
            // BtnAddCompetitor.Visible = true;

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdUpdateproduct_Click(object sender, EventArgs e)
    {
        try
        {
            var ds = (DataSet)Session["ProductDs"];
            int currentRow = int.Parse(HiddenField5.Value);

            ds.Tables[0].Rows[currentRow]["Product_Name"] = drpproduct.SelectedItem.Text;
            ds.Tables[0].Rows[currentRow]["Product_Id"] = drpproduct.SelectedValue;
            //ds.Tables[0].Rows[currentRow]["SlNo"] = txtCompetitorRemarks.Text;

            ds.Tables[0].Rows[currentRow].EndEdit();
            ds.Tables[0].Rows[currentRow].AcceptChanges();

            GrdViewLeadproduct.DataSource = ds.Tables[0];
            GrdViewLeadproduct.DataBind();
            pnlproduct.Visible = false;
            Session["ProductDs"] = ds;

            pnlproduct.Visible = false;
            GrdViewLeadproduct.Visible = true;
            //BtnAddproduct.Visible = true;

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdSaveActivity_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;
            ds = new DataSet();

            if (Session["ActivityDs"] == null)
            {
                dt = new DataTable();

                dc = new DataColumn("Activity_Id");
                dt.Columns.Add(dc);

                dc = new DataColumn("Activity_Name");
                dt.Columns.Add(dc);

                dc = new DataColumn("Activity_Name_Id");
                dt.Columns.Add(dc);

                dc = new DataColumn("Start_Date");
                dt.Columns.Add(dc);

                dc = new DataColumn("End_Date");
                dt.Columns.Add(dc);

                dc = new DataColumn("Activity_Location");
                dt.Columns.Add(dc);

                dc = new DataColumn("Next_Activity");
                dt.Columns.Add(dc);

                dc = new DataColumn("Next_Activity_Id");
                dt.Columns.Add(dc);

                dc = new DataColumn("NextActivity_Date");
                dt.Columns.Add(dc);

                dc = new DataColumn("FollowUp");
                dt.Columns.Add(dc);

                dc = new DataColumn("Emp_Name");
                dt.Columns.Add(dc);

                dc = new DataColumn("Emp_No");
                dt.Columns.Add(dc);

                dc = new DataColumn("Remarks");
                dt.Columns.Add(dc);

                ds.Tables.Add(dt);

                drNew = dt.NewRow();

                drNew["Activity_Id"] = 1;
                drNew["Activity_Name"] = drpActivityName.SelectedItem.Text;
                drNew["Activity_Name_Id"] = drpActivityName.SelectedValue;
                drNew["Start_Date"] = txtActivityDate.Text;
                //drNew["End_Date"] = txtActivityEndDate.Text;
                drNew["Activity_Location"] = txtActivityLocation.Text;
                drNew["Next_Activity"] = drpNextActivity.SelectedItem.Text;
                drNew["Next_Activity_Id"] = drpNextActivity.SelectedValue;
                drNew["NextActivity_Date"] = txtNextActivityDate.Text;
                // drNew["FollowUp"] = drpFollowUp.SelectedValue;
                drNew["Emp_Name"] = drpActivityEmployee.SelectedItem.Text;
                drNew["Emp_No"] = drpActivityEmployee.SelectedValue;
                drNew["Remarks"] = txtActivityRemarks.Text;
                ds.Tables[0].Rows.Add(drNew);
                Session["ActivityDs"] = ds;
            }
            else
            {
                ds = (DataSet)Session["ActivityDs"];

                int maxID = 0;

                if (ds.Tables[0].Rows.Count > 0)
                    maxID = int.Parse(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["Activity_Id"].ToString());

                drNew = ds.Tables[0].NewRow();
                drNew["Activity_Id"] = maxID + 1;
                drNew["Activity_Name"] = drpActivityName.SelectedItem.Text;
                drNew["Activity_Name_Id"] = drpActivityName.SelectedValue;
                drNew["Start_Date"] = txtActivityDate.Text;
                //drNew["End_Date"] = txtActivityEndDate.Text;
                drNew["Activity_Location"] = txtActivityLocation.Text;
                drNew["Next_Activity"] = drpNextActivity.SelectedItem.Text;
                drNew["Next_Activity_Id"] = drpNextActivity.SelectedValue;
                drNew["NextActivity_Date"] = txtNextActivityDate.Text;
                // drNew["FollowUp"] = drpFollowUp.SelectedValue;
                drNew["Emp_Name"] = drpActivityEmployee.SelectedItem.Text;
                drNew["Emp_No"] = drpActivityEmployee.SelectedValue;
                drNew["Remarks"] = txtActivityRemarks.Text;
                ds.Tables[0].Rows.Add(drNew);
                Session["ActivityDs"] = ds;
            }

            GrdViewLeadActivity.DataSource = ds.Tables[0];
            GrdViewLeadActivity.DataBind();

            pnlActivity.Visible = false;
            GrdViewLeadActivity.Visible = true;
            //BtnAddActivity.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdUpdateActivity_Click(object sender, EventArgs e)
    {
        try
        {
            var ds = (DataSet)Session["ActivityDs"];
            int currentRow = int.Parse(HiddenField3.Value);

            ds.Tables[0].Rows[currentRow]["Activity_Name"] = drpActivityName.SelectedItem.Text;
            ds.Tables[0].Rows[currentRow]["Activity_Name_Id"] = drpActivityName.SelectedValue;
            ds.Tables[0].Rows[currentRow]["Start_Date"] = txtActivityDate.Text;
            //ds.Tables[0].Rows[currentRow]["End_Date"] = txtActivityEndDate.Text;
            ds.Tables[0].Rows[currentRow]["Activity_Location"] = txtActivityLocation.Text;
            ds.Tables[0].Rows[currentRow]["Next_Activity"] = drpNextActivity.SelectedItem.Text;
            ds.Tables[0].Rows[currentRow]["Next_Activity_Id"] = drpNextActivity.SelectedValue;
            ds.Tables[0].Rows[currentRow]["NextActivity_Date"] = txtNextActivityDate.Text;
            // ds.Tables[0].Rows[currentRow]["FollowUp"] = drpFollowUp.SelectedValue;
            ds.Tables[0].Rows[currentRow]["Emp_Name"] = drpActivityEmployee.SelectedItem.Text;
            ds.Tables[0].Rows[currentRow]["Emp_No"] = drpActivityEmployee.SelectedValue;
            ds.Tables[0].Rows[currentRow]["Remarks"] = txtActivityRemarks.Text;

            ds.Tables[0].Rows[currentRow].EndEdit();
            ds.Tables[0].Rows[currentRow].AcceptChanges();

            GrdViewLeadActivity.DataSource = ds.Tables[0];
            GrdViewLeadActivity.DataBind();
            pnlActivity.Visible = false;
            Session["ActivityDs"] = ds;

            pnlActivity.Visible = false;
            GrdViewLeadActivity.Visible = true;
            //BtnAddActivity.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void lnkAddContact_Click(object sender, EventArgs e)
    {
        try
        {
            //cmdSaveContact.Visible = true;
            //cmdUpdateContact.Visible = false;
            //updatePnlContact.Update();

            //txtContactedDate.Text = string.Empty;
            //txtContactSummary.Text = string.Empty;
            //ComboBox2.SelectedValue = "1";
            //txtcallback.Text = string.Empty;
            //rowcall.Visible = false;

            //ModalPopupContact.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLeadStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadStages();
            DataSet ds = new DataSet();
            // GridViewRow row = GrdViewLeadStage.SelectedRow;

            //hdCurrentRow.Value = Convert.ToString(row.DataItemIndex);

            //txtStageEndDate.Text = row.Cells[1].Text;
            //txtStageStartDate.Text = row.Cells[0].Text;
            //drpStageName.SelectedItem.Text = row.Cells[2].Text;
            //drpStageName.SelectedValue = row.Cells[3].Text;
            //txtStagePerc.Text = row.Cells[4].Text;
            //txtStageRemarks.Text = row.Cells[7].Text;
            //txtStageWeightedAmount.Text = row.Cells[6].Text;
            //txtStagePotentialAmount.Text = row.Cells[5].Text;

            //cmdSaveContact.Visible = false;
            //cmdUpdateContact.Visible = true;
            //pnlStage.Visible = true;
            //GrdViewLeadStage.Visible = false;
            //BtnAddStage.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLeadCompetitor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            GridViewRow row = GrdViewLeadCompetitor.SelectedRow;

            HiddenField1.Value = Convert.ToString(row.DataItemIndex);

            txtCompetitorName.Text = row.Cells[0].Text;
            drpThreatLevel.SelectedValue = row.Cells[1].Text;
            if (row.Cells[2].Text == "&nbsp;")
            {
                txtCompetitorRemarks.Text = "";
            }
            else
            {
                txtCompetitorRemarks.Text = row.Cells[2].Text;
            }

            cmdSaveCompetitor.Visible = false;
            cmdUpdateCompetitor.Visible = true;
            pnlCompetitor.Visible = true;
            GrdViewLeadCompetitor.Visible = false;
            // BtnAddCompetitor.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLeadproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            GridViewRow row = GrdViewLeadproduct.SelectedRow;

            HiddenField5.Value = Convert.ToString(row.DataItemIndex);

            drpproduct.SelectedValue = row.Cells[2].Text;

            cmdSaveproduct.Visible = false;
            cmdUpdateproduct.Visible = true;
            pnlproduct.Visible = true;
            GrdViewLeadproduct.Visible = false;
            // BtnAddproduct.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLeadActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadActivities();
            DataSet ds = new DataSet();
            GridViewRow row = GrdViewLeadActivity.SelectedRow;

            HiddenField3.Value = Convert.ToString(row.DataItemIndex);

            drpActivityName.SelectedValue = row.Cells[1].Text;
            txtActivityDate.Text = row.Cells[2].Text;
            // txtActivityEndDate.Text = row.Cells[3].Text;
            txtNextActivityDate.Text = row.Cells[7].Text;
            txtActivityLocation.Text = row.Cells[4].Text;
            drpNextActivity.SelectedValue = row.Cells[6].Text;
            drpActivityEmployee.SelectedValue = row.Cells[11].Text;
            //drpFollowUp.SelectedValue = row.Cells[8].Text;
            txtActivityRemarks.Text = row.Cells[9].Text;

            cmdSaveActivity.Visible = false;
            cmdUpdateActivity.Visible = true;
            pnlActivity.Visible = true;
            GrdViewLeadActivity.Visible = false;
            //  BtnAddActivity.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void GrdViewLeadContact_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Pager)
    //    {
    //        PresentationUtils.SetPagerButtonStates(GrdViewLeadContact, e.Row, this);
    //    }
    //}

    //protected void GrdViewLeadContact_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRow product = ((System.Data.DataRowView)e.Row.DataItem).Row;
    //    }
    //}


    protected void GrdViewLeadStage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (Session["contactDs"] != null)
            {
                string connStr = string.Empty;
                DataSet ds;

                /*
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                GridViewRow row = GrdViewLeadContact.Rows[e.RowIndex];
                string refID = row.Cells[0].Text;
                LeadBusinessLogic bl = new LeadBusinessLogic(connStr);
                bl.DeleteLeadContact(refID);*/

                //ds = (DataSet)Session["contactDs"];
                //ds.Tables[0].Rows[GrdViewLeadContact.Rows[e.RowIndex].DataItemIndex].Delete();
                //ds.Tables[0].AcceptChanges();
                //GrdViewLeadContact.DataSource = ds;
                //GrdViewLeadContact.DataBind();
                //Session["contactDs"] = ds;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLeadCompetitor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataCompetitors();
        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            int rowIndex2 = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex2]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable2"] = dt;
                GrdViewLeadCompetitor.DataSource = dt;
                GrdViewLeadCompetitor.DataBind();

                for (int i = 0; i < GrdViewLeadCompetitor.Rows.Count - 1; i++)
                {
                    GrdViewLeadCompetitor.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousDataCompetitors();
            }
        }
    }

    protected void GrdViewLeadproduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataProduct();
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable1"] = dt;
                GrdViewLeadproduct.DataSource = dt;
                GrdViewLeadproduct.DataBind();

                for (int i = 0; i < GrdViewLeadproduct.Rows.Count - 1; i++)
                {
                    GrdViewLeadproduct.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousDataProduct();
            }
        }
    }

    protected void GrdViewLeadActivity_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataActivity();
        if (ViewState["CurrentTable3"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable3"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable3"] = dt;
                GrdViewLeadActivity.DataSource = dt;
                GrdViewLeadActivity.DataBind();

                for (int i = 0; i < GrdViewLeadCompetitor.Rows.Count - 1; i++)
                {
                    GrdViewLeadActivity.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousDataActivity();
            }
        }
    }

    protected void cmdCancelContact_Click(object sender, EventArgs e)
    {
        try
        {
            //ModalPopupContact.Hide();
            //pnlStage.Visible = false;
            //GrdViewLeadStage.Visible = true;
            //BtnAddStage.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdCancelCompetitor_Click(object sender, EventArgs e)
    {
        try
        {
            pnlCompetitor.Visible = false;
            GrdViewLeadCompetitor.Visible = true;
            // BtnAddCompetitor.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdCancelActivity_Click(object sender, EventArgs e)
    {
        try
        {
            pnlActivity.Visible = false;
            GrdViewLeadActivity.Visible = true;
            // BtnAddActivity.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdCancelproduct_Click(object sender, EventArgs e)
    {
        try
        {
            pnlproduct.Visible = false;
            GrdViewLeadproduct.Visible = true;
            //BtnAddproduct.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "LeadManagement_" + DateTime.Now.ToString() + ".xls";

            LeadBusinessLogic bl = new LeadBusinessLogic(GetConnectionString());

            int leadid = 0;

            DataSet ds = bl.ListLeadMasterContacts(GetConnectionString(), txtSearch.Text, ddCriteria.SelectedValue);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("LeadID"));
                    dt.Columns.Add(new DataColumn("CreationDate"));
                    dt.Columns.Add(new DataColumn("ProspectCustName"));
                    dt.Columns.Add(new DataColumn("Address"));
                    dt.Columns.Add(new DataColumn("Mobile"));
                    dt.Columns.Add(new DataColumn("Landline"));
                    dt.Columns.Add(new DataColumn("Email"));
                    dt.Columns.Add(new DataColumn("ModeOfContact"));
                    dt.Columns.Add(new DataColumn("PersonalResponsible"));
                    dt.Columns.Add(new DataColumn("BusinessType"));
                    dt.Columns.Add(new DataColumn("Branch"));
                    dt.Columns.Add(new DataColumn("Status"));
                    dt.Columns.Add(new DataColumn("LastCompletedAction"));
                    dt.Columns.Add(new DataColumn("NextAction"));
                    dt.Columns.Add(new DataColumn("Category"));
                    dt.Columns.Add(new DataColumn("ContactedDate"));
                    dt.Columns.Add(new DataColumn("ContactSummary"));

                    DataRow dr_export1 = dt.NewRow();
                    dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (leadid == Convert.ToInt32(dr["LeadId"]))
                        {
                            DataRow dr_export111 = dt.NewRow();
                            dr_export111["LeadID"] = "";
                            dr_export111["CreationDate"] = "";
                            dr_export111["ProspectCustName"] = "";
                            dr_export111["Address"] = "";
                            dr_export111["Mobile"] = "";
                            dr_export111["Landline"] = "";
                            dr_export111["Email"] = "";
                            dr_export111["ModeOfContact"] = "";
                            dr_export111["PersonalResponsible"] = "";
                            dr_export111["BusinessType"] = "";
                            dr_export111["Branch"] = "";
                            dr_export111["Email"] = "";
                            dr_export111["Status"] = "";
                            dr_export111["LastCompletedAction"] = "";
                            dr_export111["NextAction"] = "";
                            dr_export111["Category"] = "";
                            dr_export111["ContactedDate"] = dr["ContactedDate"];
                            dr_export111["ContactSummary"] = dr["ContactSummary"];
                            dt.Rows.Add(dr_export111);
                        }
                        else
                        {
                            DataRow dr_export = dt.NewRow();
                            dr_export["LeadID"] = dr["LeadID"];
                            dr_export["CreationDate"] = dr["CreationDate"];
                            dr_export["ProspectCustName"] = dr["ProspectCustName"];
                            dr_export["Address"] = dr["Address"];
                            dr_export["Mobile"] = dr["Mobile"];
                            dr_export["Landline"] = dr["Landline"];
                            dr_export["Email"] = dr["Email"];
                            dr_export["ModeOfContact"] = dr["ModeOfContact"];
                            dr_export["PersonalResponsible"] = dr["PersonalResponsible"];
                            dr_export["BusinessType"] = dr["BusinessType"];
                            dr_export["Branch"] = dr["Branch"];
                            dr_export["Email"] = dr["Email"];
                            dr_export["Status"] = dr["Status"];
                            dr_export["LastCompletedAction"] = dr["LastCompletedAction"];
                            dr_export["NextAction"] = dr["NextAction"];
                            dr_export["Category"] = dr["Category"];
                            dr_export["ContactedDate"] = dr["ContactedDate"];
                            dr_export["ContactSummary"] = dr["ContactSummary"];
                            dt.Rows.Add(dr_export);
                        }
                        leadid = Convert.ToInt32(dr["LeadId"]);
                    }

                    ExportToExcel(filename, dt);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }

            //if (dt.Rows.Count > 0)
            //{
            //    DataTable dt = ds.Tables[0];

            //    System.IO.StringWriter tw = new System.IO.StringWriter();
            //    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

            //    DataGrid dgGrid = new DataGrid();
            //    dgGrid.DataSource = dt;
            //    dgGrid.DataBind();

            //    //Get the HTML for the control.
            //    dgGrid.RenderControl(hw);

            //    //Write the HTML back to the browser.
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            //    this.EnableViewState = false;
            //    Response.Write(tw.ToString());
            //    Response.End();
            //    UpdatePanelPage.Update();
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(string filename, DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();
            dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
            dgGrid.HeaderStyle.Font.Bold = true;
            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void lnkAddBills_Click(object sender, EventArgs e)
    {
        /*
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

        if (GrdBills.Rows.Count == 0)
        {
            var ds = bl.GetReceivedAmountId(conn, 0);
            GrdBills.DataSource = ds;
            GrdBills.DataBind();
            GrdBills.Rows[0].Visible = false;
            checkPendingBills(ds);
        }
        pnlEdit.Visible = true;
        GrdBills.FooterRow.Visible = true;
        lnkAddBills.Visible = true;
        Session["RMode"] = "Add";
        //lnkBtnAdd.Visible = false;*/
    }

    protected void GrdViewLead_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "LDMNGT"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                //if (bl.CheckUserHaveDelete(usernam, "LDMNGT"))
                //{
                //    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                //}
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLead_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void GrdViewLead_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //GridViewRow row = GrdViewLead.Rows[e.RowIndex];
            //string leadID = row.Cells[0].Text;


            //string userID = string.Empty;
            //userID = Page.User.Identity.Name;
            //LeadBusinessLogic bl = new LeadBusinessLogic(GetConnectionString());
            //bl.DeleteLeadMaster(leadID, userID);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Lead Management Details Deleted Successfully')", true);
            //BindGrid("", "");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLead_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewLead.PageIndex = e.NewPageIndex;

            string textt = string.Empty;
            string dropd = string.Empty;

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;

            BindGrid(textt, dropd);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdViewLead.PageIndex = ((DropDownList)sender).SelectedIndex;

            string textt = string.Empty;
            string dropd = string.Empty;

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;

            BindGrid(textt, dropd);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = GrdViewLead.SelectedRow;
            int LeadNo = Convert.ToInt32(GrdViewLead.SelectedDataKey.Value.ToString());
            LeadBusinessLogic bl = new LeadBusinessLogic(GetConnectionString());

            string connection = Request.Cookies["Company"].Value;

            DataSet itemDs = new DataSet();
            DataSet itemDscom = new DataSet();
            DataSet itemDsAct = new DataSet();
            DataSet dsDetails = bl.GetLeadDetails(connection,LeadNo);
            string sCustomer = string.Empty;

            if (dsDetails != null && dsDetails.Tables[0].Rows.Count > 0)
            {
                Session["Date"] = "Edit";

                txtLeadNo.Text = dsDetails.Tables[0].Rows[0]["Lead_No"].ToString();
                txtCreationDate.Text = Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["Start_Date"]).ToString("dd/MM/yyyy");

                if (dsDetails.Tables[0].Rows[0]["chec"].ToString() == "Y")
                {
                    cmbCustomer.SelectedValue = dsDetails.Tables[0].Rows[0]["Bp_Id"].ToString();
                    txtBPName.Visible = false;
                    cmbCustomer.Visible = true;
                    chk.Checked = true;
                }
                else
                {
                    txtBPName.Text = dsDetails.Tables[0].Rows[0]["Bp_Name"].ToString();
                    txtBPName.Visible = true;
                    cmbCustomer.Visible = false;
                    chk.Checked = false;
                }

                //chk.Checked = Convert.ToBoolean(dsDetails.Tables[0].Rows[0]["chec"]);   
                txtMobile.Text = dsDetails.Tables[0].Rows[0]["Mobile"].ToString();
                txtAddress.Text = dsDetails.Tables[0].Rows[0]["Address"].ToString();
                txtTelephone.Text = dsDetails.Tables[0].Rows[0]["Telephone"].ToString();
                txtLeadName.Text = dsDetails.Tables[0].Rows[0]["Lead_Name"].ToString();
                txtContactName.Text = dsDetails.Tables[0].Rows[0]["Contact_Name"].ToString();

                if ((Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["Closing_Date"])) == Convert.ToDateTime("01/01/2000"))
                {
                    txtClosingDate.Text = "";
                }
                else
                {
                    txtClosingDate.Text = Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["Closing_Date"]).ToString("dd/MM/yyyy");
                }

                if ((Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["Predicted_Closing_Date"])) == Convert.ToDateTime("01/01/2000"))
                {
                    txtClosingDate.Text = "";
                }
                else
                {
                    txtPredictedClosingDate.Text = Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["Predicted_Closing_Date"]).ToString("dd/MM/yyyy");
                }
                txtInformation1.Text = dsDetails.Tables[0].Rows[0]["Information1"].ToString();
                drpLeadStatus.SelectedValue = dsDetails.Tables[0].Rows[0]["Lead_Status"].ToString();
                drpStatus.SelectedValue = dsDetails.Tables[0].Rows[0]["Doc_Status"].ToString();
                drpIncharge.SelectedValue = dsDetails.Tables[0].Rows[0]["Emp_Id"].ToString();
                drpInformation3.SelectedValue = dsDetails.Tables[0].Rows[0]["Information3"].ToString();
                drpInformation4.SelectedValue = dsDetails.Tables[0].Rows[0]["Information4"].ToString();
                drpBusinessType.SelectedValue = dsDetails.Tables[0].Rows[0]["BusinessType"].ToString();
                drpCategory.SelectedValue = dsDetails.Tables[0].Rows[0]["Category"].ToString();
                drpArea.SelectedValue = dsDetails.Tables[0].Rows[0]["Area"].ToString();
                drpIntLevel.SelectedValue = dsDetails.Tables[0].Rows[0]["InterestLevel"].ToString();

                drpLeadStatus.Enabled = true;
                drpStatus.Enabled = false;

                //load product tab details
                itemDs = Producttab(connection,LeadNo);
                Session["ProductDs"] = itemDs;
                GrdViewLeadproduct.DataSource = itemDs;
                GrdViewLeadproduct.DataBind();
                pnlproduct.Visible = false;

                for (int vLoop = 0; vLoop < GrdViewLeadproduct.Rows.Count; vLoop++)
                {

                    DropDownList drpProduct = (DropDownList)GrdViewLeadproduct.Rows[vLoop].FindControl("drpproduct");
                    Label txtPrdID = (Label)GrdViewLeadproduct.Rows[vLoop].FindControl("txtPrdId");

                    if (itemDs.Tables[0].Rows[vLoop]["PrdID"] != null)
                    {
                        sCustomer = Convert.ToString(itemDs.Tables[0].Rows[vLoop]["PrdID"]);
                       // sCustomer = Convert.ToString(itemDs.Tables[0].Rows[vLoop]["Prd"]);
                        //drpProduct.SelectedValue = Convert.ToString(sCustomer);
                        drpProduct.ClearSelection();
                        ListItem li = drpProduct.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }
                   // drpProduct.Text = sCustomer;
                    //drpproduct.SelectedItem.Text = itemDs.Tables[0].Rows[vLoop]["Prd"].ToString();
                    txtPrdID.Text = itemDs.Tables[0].Rows[vLoop]["PrdID"].ToString();
                   // drpProduct.SelectedValue =Convert.ToString( sCustomer);
                }
                //close product details

                //load competitors tab
                itemDscom = Competitortab(connection,LeadNo);
                Session["CompetitorDs"] = itemDscom;
                GrdViewLeadCompetitor.DataSource = itemDscom;
                GrdViewLeadCompetitor.DataBind();
                GrdViewLeadCompetitor.Visible = true;
                pnlCompetitor.Visible = false;

                for (int vLoop = 0; vLoop < GrdViewLeadCompetitor.Rows.Count; vLoop++)
                {
                    TextBox txtComName = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtComeName");
                    TextBox txtThrlvl = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtThrLvl");
                    TextBox txtOuestrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtOurStrWeakness");
                    TextBox txtComstrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtCompStrWeakness");
                    TextBox txtremarks = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtRemarks");

                    txtComName.Text = itemDscom.Tables[0].Rows[vLoop]["ComName"].ToString();
                    txtThrlvl.Text = itemDscom.Tables[0].Rows[vLoop]["ThrLvl"].ToString();
                    txtOuestrweak.Text = itemDscom.Tables[0].Rows[vLoop]["OurStrWeak"].ToString();
                    txtComstrweak.Text = itemDscom.Tables[0].Rows[vLoop]["ComStrWeak"].ToString();
                    txtremarks.Text = itemDscom.Tables[0].Rows[vLoop]["Remarks"].ToString();
                }
                //close competitor tab             

                //load activity tab
                itemDsAct = Activitytab(connection,LeadNo);
                Session["ActivityDs"] = itemDsAct;
                GrdViewLeadActivity.DataSource = itemDsAct;
                GrdViewLeadActivity.DataBind();
                GrdViewLeadActivity.Visible = true;
                pnlActivity.Visible = false;

                for (int vLoop = 0; vLoop < GrdViewLeadActivity.Rows.Count; vLoop++)
                {
                    DropDownList drpactivityName = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpactivityName");
                    TextBox txtactivityLoc = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiLoc");
                    TextBox txtactivityDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiDate");
                    DropDownList drpnxtActivity = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpnxtActivity");
                    TextBox txtnxtActDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtNxtActyDate");
                    DropDownList drpemployee = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpemployee");
                    //TextBox txtmodeofCnt = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtModrofcnt");
                    DropDownList drpmodeofCnt = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpModrofcnt");
                    DropDownList drpinfo2 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo2");
                    DropDownList drpinfo5 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo5");
                    TextBox txtremarks = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtremarks");

                    if (itemDsAct.Tables[0].Rows[vLoop]["ActNameID"] != null)
                    {
                        sCustomer = Convert.ToString(itemDsAct.Tables[0].Rows[vLoop]["ActNameID"]);
                        drpactivityName.ClearSelection();
                        ListItem li = drpactivityName.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }
                    txtactivityLoc.Text = itemDsAct.Tables[0].Rows[vLoop]["ActLoc"].ToString();
                    txtactivityDate.Text = Convert.ToDateTime(itemDsAct.Tables[0].Rows[vLoop]["ActDate"]).ToString("dd/MM/yyyy");
                    if (itemDsAct.Tables[0].Rows[vLoop]["NxtActID"] != null)
                    {
                        sCustomer = Convert.ToString(itemDsAct.Tables[0].Rows[vLoop]["NxtActID"]);
                        drpnxtActivity.ClearSelection();
                        ListItem li = drpnxtActivity.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }
                    txtnxtActDate.Text = Convert.ToDateTime(itemDsAct.Tables[0].Rows[vLoop]["NxtActDte"]).ToString("dd/MM/yyyy");
                    if (itemDsAct.Tables[0].Rows[vLoop]["EmpID"] != null)
                    {
                        sCustomer = Convert.ToString(itemDsAct.Tables[0].Rows[vLoop]["EmpID"]);
                        drpemployee.ClearSelection();
                        ListItem li = drpemployee.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }
                    //txtmodeofCnt.Text = itemDsAct.Tables[0].Rows[vLoop]["MdeofCnt"].ToString();
                    if (itemDsAct.Tables[0].Rows[vLoop]["MdeofCnt"] != null)
                    {
                        sCustomer = Convert.ToString(itemDsAct.Tables[0].Rows[vLoop]["MdeofCnt"]);
                        drpmodeofCnt.ClearSelection();
                        ListItem li = drpmodeofCnt.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }
                    if (itemDsAct.Tables[0].Rows[vLoop]["Info2"] != null)
                    {
                        sCustomer = Convert.ToString(itemDsAct.Tables[0].Rows[vLoop]["Info2"]);
                        drpinfo2.ClearSelection();
                        ListItem li = drpinfo2.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }
                    if (itemDsAct.Tables[0].Rows[vLoop]["Info5"] != null)
                    {
                        sCustomer = Convert.ToString(itemDsAct.Tables[0].Rows[vLoop]["Info5"]);
                        drpinfo5.ClearSelection();
                        ListItem li = drpinfo5.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }
                    txtremarks.Text = itemDsAct.Tables[0].Rows[vLoop]["Remarks"].ToString();
                }
                //close activity tab

                UpdateButton.Visible = true;
                AddButton.Visible = false;
                ModalPopupExtender2.Show();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public DataSet Producttab(string connection,int salesID)
    {
        DataSet ds;

        DataSet itemDs = new DataSet();
        DataTable dt;
        DataRow dr;
        DataColumn dc;
        int m = 0;
        string strRole = string.Empty;
        string roleFlag = string.Empty;
        string strBundles = string.Empty;

        string strItemCode = string.Empty;
        LeadBusinessLogic bl = new LeadBusinessLogic(sDataSource);
       // string connection = Request.Cookies["Company"].Value;

        ds = bl.GetLeadProduct(connection,salesID);


        if (ds != null)
        {
            dt = new DataTable();

            dc = new DataColumn("Prd");
            dt.Columns.Add(dc);

            dc = new DataColumn("PrdID");
            dt.Columns.Add(dc);

            dc = new DataColumn("RowNumber");
            dt.Columns.Add(dc);

            itemDs.Tables.Add(dt);
            ViewState["CurrentTable1"] = dt;

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    m = m + 1;
                    dr = itemDs.Tables[0].NewRow();


                    if (dR["Product_ID"] != null)
                        dr["PrdID"] = Convert.ToString(dR["Product_ID"]);
                    if (dR["Product_Name"] != null)
                        dr["Prd"] = Convert.ToString(dR["Product_Name"]);
                    if (dR["Product_interest_Id"] != null)
                        dr["RowNumber"] = m.ToString();// Convert.ToString(dR["Product_interest_Id"]);

                    itemDs.Tables[0].Rows.Add(dr);
                    strRole = "";
                }
            }
        }
        return itemDs;


    }

    public DataSet Competitortab(string connection,int salesID)
    {
        DataSet ds;

        DataSet itemDscom = new DataSet();
        DataTable dt;
        DataRow dr;
        DataColumn dc;
        int p = 0;
        string strRole = string.Empty;
        string roleFlag = string.Empty;
        string strBundles = string.Empty;

        string strItemCode = string.Empty;
        LeadBusinessLogic bl = new LeadBusinessLogic(sDataSource);

        ds = bl.GetLeadCompetitor(connection,salesID);


        if (ds != null)
        {
            dt = new DataTable();

            dc = new DataColumn("ComName");
            dt.Columns.Add(dc);

            dc = new DataColumn("ThrLvl");
            dt.Columns.Add(dc);

            dc = new DataColumn("OurStrWeak");
            dt.Columns.Add(dc);

            dc = new DataColumn("ComStrWeak");
            dt.Columns.Add(dc);

            dc = new DataColumn("RowNumber");
            dt.Columns.Add(dc);

            dc = new DataColumn("Remarks");
            dt.Columns.Add(dc);

            itemDscom.Tables.Add(dt);
            ViewState["CurrentTable2"] = dt;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    p = p + 1;
                    dr = itemDscom.Tables[0].NewRow();


                    if (dR["Competitor_Name"] != null)
                        dr["ComName"] = Convert.ToString(dR["Competitor_Name"]);
                    if (dR["Threat_Level"] != null)
                        dr["ThrLvl"] = Convert.ToString(dR["Threat_Level"]);
                    if (dR["OurStr_Weak"] != null)
                        dr["OurStrWeak"] = Convert.ToString(dR["OurStr_Weak"]);
                    if (dR["ComStr_Weak"] != null)
                        dr["ComStrWeak"] = Convert.ToString(dR["ComStr_Weak"]);
                    if (dR["Competitor_Id"] != null)
                        dr["RowNumber"] = p.ToString();// Convert.ToString(dR["Competitor_Id"]);
                    if (dR["Remarks"] != null)
                        dr["Remarks"] = Convert.ToString(dR["Remarks"]);

                    itemDscom.Tables[0].Rows.Add(dr);
                    strRole = "";
                }
            }
        }
        return itemDscom;


    }

    public DataSet Activitytab(string connection,int salesID)
    {
        DataSet ds;

        DataSet itemDsAct = new DataSet();
        DataTable dt;
        DataRow dr;
        DataColumn dc;
        int j = 0;
        string strRole = string.Empty;
        string roleFlag = string.Empty;
        string strBundles = string.Empty;

        string strItemCode = string.Empty;
        LeadBusinessLogic bl = new LeadBusinessLogic(sDataSource);

        ds = bl.GetLeadActivity(connection,salesID);


        if (ds != null)
        {
            dt = new DataTable();

            dc = new DataColumn("ActName");
            dt.Columns.Add(dc);

            dc = new DataColumn("ActNameID");
            dt.Columns.Add(dc);

            dc = new DataColumn("ActLoc");
            dt.Columns.Add(dc);

            dc = new DataColumn("ActDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("NxtAct");
            dt.Columns.Add(dc);

            dc = new DataColumn("NxtActID");
            dt.Columns.Add(dc);

            dc = new DataColumn("NxtActDte");
            dt.Columns.Add(dc);

            dc = new DataColumn("Emp");
            dt.Columns.Add(dc);

            dc = new DataColumn("EmpID");
            dt.Columns.Add(dc);

            dc = new DataColumn("MdeofCnt");
            dt.Columns.Add(dc);

            dc = new DataColumn("Info2");
            dt.Columns.Add(dc);

            dc = new DataColumn("Info5");
            dt.Columns.Add(dc);

            dc = new DataColumn("Remarks");
            dt.Columns.Add(dc);

            dc = new DataColumn("RowNumber");
            dt.Columns.Add(dc);

            itemDsAct.Tables.Add(dt);
            ViewState["CurrentTable3"] = dt;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    j = j + 1;
                    dr = itemDsAct.Tables[0].NewRow();

                    if (dR["Activity_Name"] != null)
                        dr["ActName"] = Convert.ToString(dR["Activity_Name"]);
                    if (dR["Activity_Name_Id"] != null)
                        dr["ActNameID"] = Convert.ToString(dR["Activity_Name_Id"]);
                    if (dR["Activity_Location"] != null)
                        dr["ActLoc"] = Convert.ToString(dR["Activity_Location"]);
                    if (dR["Activity_Date"] != null)
                        dr["ActDate"] = Convert.ToString(dR["Activity_Date"]);
                    if (dR["Next_Activity"] != null)
                        dr["NxtAct"] = Convert.ToString(dR["Next_Activity"]);
                    if (dR["Next_Activity_Id"] != null)
                        dr["NxtActID"] = Convert.ToString(dR["Next_Activity_Id"]);
                    if (dR["NextActivity_Date"] != null)
                        dr["NxtActDte"] = Convert.ToString(dR["NextActivity_Date"]);
                    if (dR["Emp_Name"] != null)
                        dr["Emp"] = Convert.ToString(dR["Emp_Name"]);
                    if (dR["Emp_No"] != null)
                        dr["EmpID"] = Convert.ToString(dR["Emp_No"]);
                    if (dR["ModeofContact"] != null)
                        dr["MdeofCnt"] = Convert.ToString(dR["ModeofContact"]);
                    if (dR["Information2"] != null)
                        dr["Info2"] = Convert.ToString(dR["Information2"]);
                    if (dR["Information5"] != null)
                        dr["Info5"] = Convert.ToString(dR["Information5"]);
                    if (dR["Remarks"] != null)
                        dr["Remarks"] = Convert.ToString(dR["Remarks"]);
                    if (dR["Activity_Id"] != null)
                        dr["RowNumber"] = j.ToString();// Convert.ToString(dR["Activity_Id"]);

                    itemDsAct.Tables[0].Rows.Add(dr);
                    strRole = "";
                }
            }
        }
        return itemDsAct;


    }

    protected void GrdViewLead_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GrdViewLead_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewLead, e.Row, this);
            }
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
            ModalPopupExtender2.Hide();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        DateTime startDate;
        DateTime ClosingDate;
        int LeadNo = 0;
        string LeadName = string.Empty;
        string address = string.Empty;
        string mobile = string.Empty;
        string Telephone = string.Empty;
        string BpName = string.Empty;
        string EmpName = string.Empty;
        string LeadStatus = string.Empty;
        string Status = string.Empty;
        string branch = string.Empty;
        string status = string.Empty;
        string ContactName = string.Empty;
        string info1 = string.Empty;

        DataSet dsStages;
        DataSet dss;
        DataSet dss1;
        DataSet dss2;
        int BpId = 0;
        int EmpId = 0;
        int info3 = 0;
        int info4 = 0;
        int businesstype = 0;
        int category = 0;
        int area = 0;
        int intLevel = 0;

        double TotalAmount = 0;
        int ClosingPer = 0;
        DataSet dsCompetitor;
        DataSet dsActivity;

        DataSet dsProduct;


        DateTime PredictedClosingDate;
        int PredictedClosing = 0;
        string PredictedClosingPeriod = string.Empty;
        string InterestLevel = string.Empty;
        double PotentialPotAmount = 0;
        double PotentialWeightedAmount = 0;

        try
        {
            if (Page.IsValid)
            {
                if (txtLeadNo.Text != string.Empty)
                    LeadNo = int.Parse(txtLeadNo.Text);

                startDate = DateTime.Parse(txtCreationDate.Text);
                LeadName = txtLeadName.Text;
                address = txtAddress.Text;
                mobile = txtMobile.Text;
                Telephone = txtTelephone.Text;

                string check = string.Empty;

                if (chk.Checked == false)
                {
                    BpName = txtBPName.Text;
                    BpId = 0;
                    check = "N";
                }
                else
                {
                    BpName = cmbCustomer.SelectedItem.Text;
                    BpId = Convert.ToInt32(cmbCustomer.SelectedValue);
                    check = "Y";
                }

                ContactName = txtContactName.Text;
                EmpId = Convert.ToInt32(drpIncharge.SelectedValue);
                EmpName = drpIncharge.SelectedItem.Text;
                Status = drpStatus.SelectedValue;

                LeadStatus = drpLeadStatus.SelectedValue;


                if (txtClosingDate.Text == "")
                {
                    ClosingDate = DateTime.Parse("01/01/2000");
                }
                else
                {
                    ClosingDate = DateTime.Parse(txtClosingDate.Text);
                }

                if (txtPredictedClosingDate.Text == "")//
                {
                    PredictedClosingDate = DateTime.Parse("01/01/2000");
                }
                else
                {
                    PredictedClosingDate = DateTime.Parse(txtPredictedClosingDate.Text);
                }


                info1 = txtInformation1.Text;
                info3 = Convert.ToInt32(drpInformation3.SelectedValue);//
                info4 = Convert.ToInt32(drpInformation4.SelectedValue);//
                businesstype = Convert.ToInt32(drpBusinessType.SelectedValue);//
                category = Convert.ToInt32(drpCategory.SelectedValue);//
                area = Convert.ToInt32(drpArea.SelectedValue);//
                intLevel = Convert.ToInt32(drpIntLevel.SelectedValue);//

                string usernam = Request.Cookies["LoggedUserName"].Value;
                dsStages = (DataSet)Session["contactDs"];


                dss = (DataSet)Session["ProductDs"];
                
                //&&&&&& Product tab Insert Dataset &&&&&&&&&&&&&&&&& 
                if (Session["ProductDs"] != null) // New code
                {
                    dss = (DataSet)Session["ProductDs"];

                    if (dss != null)
                    {

                        for (int vLoop = 0; vLoop < GrdViewLeadproduct.Rows.Count; vLoop++)
                        {
                            DropDownList drpProduct = (DropDownList)GrdViewLeadproduct.Rows[vLoop].FindControl("drpproduct");
                            Label txtPrdID = (Label)GrdViewLeadproduct.Rows[vLoop].FindControl("txtPrdId");
                        }


                        //DataSet dss;
                        DataTable dt;
                        DataRow drNew;

                        DataColumn dc;

                        dss = new DataSet();
                        dt = new DataTable();

                        dc = new DataColumn("Prd");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("PrdID");
                        dt.Columns.Add(dc);

                        dss.Tables.Add(dt);

                        for (int vLoop = 0; vLoop < GrdViewLeadproduct.Rows.Count; vLoop++)
                        {
                            DropDownList drpProduct = (DropDownList)GrdViewLeadproduct.Rows[vLoop].FindControl("drpproduct");
                            Label txtPrdID = (Label)GrdViewLeadproduct.Rows[vLoop].FindControl("txtPrdId");

                            drNew = dt.NewRow();
                            drNew["Prd"] = Convert.ToString(drpProduct.SelectedItem.Text);
                            drNew["PrdID"] = txtPrdID.Text;
                            dss.Tables[0].Rows.Add(drNew);
                        }
                    }
                }

                for (int vLoop = 0; vLoop < GrdViewLeadproduct.Rows.Count; vLoop++)
                {
                    DropDownList drpProduct = (DropDownList)GrdViewLeadproduct.Rows[vLoop].FindControl("drpproduct");
                    Label txtPrdID = (Label)GrdViewLeadproduct.Rows[vLoop].FindControl("txtPrdId");

                    int col = vLoop + 1;
                    //if (drpProduct.SelectedValue != "0" && txtPrdID.Text != "")
                    //{
                        if (drpProduct.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Product in row " + col + " ')", true);
                            return;
                        }
                        else if (txtPrdID.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill ProductID in row " + col + " ')", true);
                            return;
                        }
                    //}
                }

                //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&


                dss1 = (DataSet)Session["CompetitorDs"];
                //&&&&&& Competitors tab Insert Dataset &&&&&&&&&&&&&&&&& 
                if (Session["CompetitorDs"] != null) // New code
                {
                    dss1 = (DataSet)Session["CompetitorDs"];

                    if (dss1 != null)
                    {
                        for (int vLoop = 0; vLoop < GrdViewLeadCompetitor.Rows.Count; vLoop++)
                        {
                            TextBox txtComName = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtComeName");
                            TextBox txtThrlvl = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtThrLvl");
                            TextBox txtOuestrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtOurStrWeakness");
                            TextBox txtComstrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtCompStrWeakness");
                            TextBox txtremarks = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtRemarks");
                        }

                        //DataSet dss1;
                        DataTable dt;
                        DataRow drNew;

                        DataColumn dc;

                        dss1 = new DataSet();
                        dt = new DataTable();

                        dc = new DataColumn("ComName");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ThrLvl");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("OurStrWeak");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ComStrWeak");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Remarks");
                        dt.Columns.Add(dc);

                        dss1.Tables.Add(dt);

                        for (int vLoop = 0; vLoop < GrdViewLeadCompetitor.Rows.Count; vLoop++)
                        {
                            TextBox txtComName = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtComeName");
                            TextBox txtThrlvl = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtThrLvl");
                            TextBox txtOuestrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtOurStrWeakness");
                            TextBox txtComstrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtCompStrWeakness");
                            TextBox txtremarks = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtRemarks");


                            drNew = dt.NewRow();
                            drNew["ComName"] = txtComName.Text;
                            drNew["ThrLvl"] = txtThrlvl.Text;
                            drNew["OurStrWeak"] = txtOuestrweak.Text;
                            drNew["ComStrWeak"] = txtComstrweak.Text;
                            drNew["Remarks"] = txtremarks.Text;
                            dss1.Tables[0].Rows.Add(drNew);
                        }
                    }
                }

                for (int vLoop = 0; vLoop < GrdViewLeadCompetitor.Rows.Count; vLoop++)
                {
                    TextBox txtComName = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtComeName");
                    TextBox txtThrlvl = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtThrLvl");
                    TextBox txtOuestrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtOurStrWeakness");
                    TextBox txtComstrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtCompStrWeakness");
                    TextBox txtremarks = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtRemarks");

                    int col = vLoop + 1;


                    if (txtComName.Text != "" || txtThrlvl.Text != "" || txtOuestrweak.Text != "" || txtComstrweak.Text != "" || txtremarks.Text != "")
                    {
                        if (txtComName.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Competitor Name in row " + col + " ')", true);
                            return;
                        }
                        else if (txtThrlvl.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Thread Level in row " + col + " ')", true);
                            return;
                        }
                        else if (txtOuestrweak.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Our Strengths and Weaknesses in row " + col + " ')", true);
                            return;
                        }
                        else if (txtComstrweak.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Competitor's Strengths and Weaknesses in row " + col + " ')", true);
                            return;
                        }
                        else if (txtremarks.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Remarks in row " + col + " ')", true);
                            return;
                        }
                    }
                }

                //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&


                dss2 = (DataSet)Session["ActivityDs"];
                //&&&&&& Activity tab Insert Dataset &&&&&&&&&&&&&&&&& 
                if (Session["ActivityDs"] != null) // New code
                {
                    dss2 = (DataSet)Session["ActivityDs"];

                    if (dss2 != null)
                    {
                        for (int vLoop = 0; vLoop < GrdViewLeadActivity.Rows.Count; vLoop++)
                        {
                            DropDownList drpactivityName = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpactivityName");
                            TextBox txtactivityLoc = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiLoc");
                            TextBox txtactivityDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiDate");
                            DropDownList drpnxtActivity = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpnxtActivity");
                            TextBox txtnxtActDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtNxtActyDate");
                            DropDownList drpemployee = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpemployee");
                            //TextBox txtmodeofCnt = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtModrofcnt");
                            DropDownList drpmodeofCnt = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpModrofcnt");
                            DropDownList drpinfo2 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo2");
                            DropDownList drpinfo5 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo5");
                            TextBox txtremarks = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtremarks");
                        }

                        // DataSet dss2;
                        DataTable dt;
                        DataRow drNew;

                        DataColumn dc;

                        dss2 = new DataSet();
                        dt = new DataTable();

                        dc = new DataColumn("ActName");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ActNameID");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ActLoc");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ActDate");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("NxtAct");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("NxtActID");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("NxtActDte");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Emp");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("EmpID");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("MdeofCnt");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Info2");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Info5");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Remarks");
                        dt.Columns.Add(dc);

                        dss2.Tables.Add(dt);

                        for (int vLoop = 0; vLoop < GrdViewLeadActivity.Rows.Count; vLoop++)
                        {
                            DropDownList drpactivityName = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpactivityName");
                            TextBox txtactivityLoc = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiLoc");
                            TextBox txtactivityDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiDate");
                            DropDownList drpnxtActivity = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpnxtActivity");
                            TextBox txtnxtActDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtNxtActyDate");
                            DropDownList drpemployee = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpemployee");
                            //TextBox txtmodeofCnt = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtModrofcnt");
                            DropDownList drpmodeofCnt = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpModrofcnt");
                            DropDownList drpinfo2 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo2");
                            DropDownList drpinfo5 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo5");
                            TextBox txtremarks = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtremarks");


                            drNew = dt.NewRow();
                            drNew["ActName"] = Convert.ToString(drpactivityName.SelectedItem.Text);
                            drNew["ActNameID"] = Convert.ToInt32(drpactivityName.SelectedItem.Value);
                            drNew["ActLoc"] = txtactivityLoc.Text;
                            drNew["ActDate"] = txtactivityDate.Text;
                            drNew["NxtAct"] = Convert.ToString(drpnxtActivity.SelectedItem.Text);
                            drNew["NxtActID"] = Convert.ToInt32(drpnxtActivity.SelectedItem.Value);
                            drNew["NxtActDte"] = txtnxtActDate.Text;
                            drNew["Emp"] = Convert.ToString(drpemployee.SelectedItem.Text);
                            drNew["EmpID"] = Convert.ToInt32(drpemployee.SelectedItem.Value);
                            drNew["MdeofCnt"] = Convert.ToString(drpmodeofCnt.SelectedItem.Value);
                            drNew["Info2"] = Convert.ToString(drpinfo2.SelectedItem.Value);
                            drNew["Info5"] = Convert.ToString(drpinfo5.SelectedItem.Value);
                            drNew["Remarks"] = txtremarks.Text;
                            dss2.Tables[0].Rows.Add(drNew);
                        }
                    }
                }


                for (int vLoop = 0; vLoop < GrdViewLeadActivity.Rows.Count; vLoop++)
                {
                    DropDownList drpactivityName = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpactivityName");
                    TextBox txtactivityLoc = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiLoc");
                    TextBox txtactivityDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiDate");
                    DropDownList drpnxtActivity = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpnxtActivity");
                    TextBox txtnxtActDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtNxtActyDate");
                    DropDownList drpemployee = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpemployee");
                    //TextBox txtmodeofCnt = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtModrofcnt");
                    DropDownList drpmodeofCnt = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpModrofcnt");
                    DropDownList drpinfo2 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo2");
                    DropDownList drpinfo5 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo5");
                    TextBox txtremarks = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtremarks");

                    int col = vLoop + 1;

                    if (drpactivityName.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Lead Activity in row " + col + " ')", true);
                        return;
                    }
                    //else if (txtactivityLoc.Text == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Lead Location in row " + col + " ')", true);
                    //    return;
                    //}
                    else if (txtactivityDate.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Lead Activity Date in row " + col + " ')", true);
                        return;
                    }
                    else if (drpnxtActivity.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Follow-up Activity in row " + col + " ')", true);
                        return;
                    }
                    else if (txtnxtActDate.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Follow-up Activity Date in row " + col + " ')", true);
                        return;
                    }
                    else if (drpemployee.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Employee Responsible in row " + col + " ')", true);
                        return;
                    }
                    else if (drpmodeofCnt.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Mode of contact in row " + col + " ')", true);
                        return;
                    }
                    //else if (drpinfo2.SelectedValue == "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Additional Information4 in row " + col + " ')", true);
                    //    return;
                    //}
                    //else if (drpinfo5.SelectedValue == "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Additional Information5 in row " + col + " ')", true);
                    //    return;
                    //}
                    else if (txtremarks.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Remarks in row " + col + " ')", true);
                        return;
                    }
                }

                //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&              

                string connStr = GetConnectionString();
                LeadBusinessLogic bl = new LeadBusinessLogic(connStr);

                string connection = Request.Cookies["Company"].Value;

                
                bl.UpdateLead(connection,LeadNo, startDate, LeadName, address, mobile, Telephone, BpName, BpId, ContactName, EmpId, EmpName, Status, LeadStatus, ClosingDate, PredictedClosingDate, info1, info3, info4, businesstype, category, area, intLevel, usernam, dss1, dss2, dss, check);
                               

                string salestype = string.Empty;
                int ScreenNo = 0;
                string ScreenName = string.Empty;

              //  string connection = Request.Cookies["Company"].Value;
                BusinessLogic bl1 = new BusinessLogic();

                salestype = "Lead Management";
                ScreenName = "Lead Management";

                bool mobile1 = false;
                bool Email = false;
                string emailsubject = string.Empty;

                string emailcontent = string.Empty;
                if (check == "Y")
                {
                    if (hdEmailRequired.Value == "YES")
                    {
                        DataSet dsd = bl1.GetLedgerInfoForId(connection, BpId);
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


                        DataSet dsdd = bl1.GetDetailsForScreenNo(connection, ScreenName, "");
                        if (dsdd != null)
                        {
                            if (dsdd.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdd.Tables[0].Rows)
                                {
                                    ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                    mobile1 = Convert.ToBoolean(dr["mobile"]);
                                    Email = Convert.ToBoolean(dr["Email"]);
                                    emailsubject = Convert.ToString(dr["emailsubject"]);
                                    emailcontent = Convert.ToString(dr["emailcontent"]);

                                    if (ScreenType == 1)
                                    {
                                        if (dr["Name1"].ToString() == "Sales Executive")
                                        {
                                            toAddress = toAdd;
                                        }
                                        else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger"))
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
                                            int index132 = emailcontent.IndexOf("@EmpName");
                                        
                                        body = EmpName;
                                        if (index132 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index132, 8).Insert(index132, body);
                                        }
                                        int index312 = emailcontent.IndexOf("@User");
                                        body = usernam;
                                        if (index312 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                        }
                                        int index2 = emailcontent.IndexOf("@LeadName");
                                        body = LeadName;
                                        if (index2 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index2, 9).Insert(index2, body);
                                        }
                                        int index = emailcontent.IndexOf("@BpName");
                                        body = BpName;
                                        if (index >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index, 7).Insert(index, body);
                                        }
                                        int index1 = emailcontent.IndexOf("@Status");
                                        body = Convert.ToString(Status);
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
                }


                string conn1 = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                UtilitySMS utilSMS = new UtilitySMS(conn1);
                string UserID = Page.User.Identity.Name;

                string smscontent = string.Empty;
                if (check == "Y")
                {
                    if (hdSMSRequired.Value == "YES")
                    {
                        DataSet dsd = bl1.GetLedgerInfoForId(connection, BpId);
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


                        DataSet dsdd = bl1.GetDetailsForScreenNo(connection, ScreenName, "");
                        if (dsdd != null)
                        {
                            if (dsdd.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdd.Tables[0].Rows)
                                {
                                    ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                    mobile1 = Convert.ToBoolean(dr["mobile"]);
                                    smscontent = Convert.ToString(dr["smscontent"]);

                                    if (ScreenType == 1)
                                    {
                                        if (dr["Name1"].ToString() == "Sales Executive")
                                        {
                                            toAddress = toAdd;
                                        }
                                        else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger"))
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
                                    if (mobile1 == true)
                                    {

                                        string body = "\n";

                                        int index123 = smscontent.IndexOf("@Branch");
                                        body = Request.Cookies["Company"].Value;
                                        if (index123 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                        }

                                        int index132 = smscontent.IndexOf("@EmpName");
                                        body = EmpName;
                                        if (index132 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index132, 8).Insert(index132, body);
                                        }

                                        int index312 = smscontent.IndexOf("@User");
                                        body = usernam;
                                        if (index312 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                        }

                                        int index2 = smscontent.IndexOf("@LeadName");
                                        body = LeadName;
                                        if (index2 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index2, 9).Insert(index2, body);
                                        }

                                        int index = smscontent.IndexOf("@BpName");
                                        body = BpName;
                                        if (index >= 0)
                                        {
                                            smscontent = smscontent.Remove(index, 7).Insert(index, body);
                                        }

                                        int index1 = emailcontent.IndexOf("@Status");
                                        body = Convert.ToString(Status);
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



                GrdViewLead.DataBind();
              
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Lead Details Updated successfully.')", true);

                UpdatePanelPage.Update();
                BindGrid("Open", "DocStatus");
                ModalPopupExtender2.Hide();

                return;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }
    }

    protected void AddTheRef_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("LeadReference.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        DateTime startDate;
        DateTime ClosingDate;
        int LeadNo = 0;
        string LeadName = string.Empty;
        string address = string.Empty;
        string mobile = string.Empty;
        string Telephone = string.Empty;
        string BpName = string.Empty;
        string EmpName = string.Empty;
        string LeadStatus = string.Empty;
        string Status = string.Empty;
        string branch = string.Empty;
        string status = string.Empty;
        string ContactName = string.Empty;
        string info1 = string.Empty;

        DataSet dsStages;
        DataSet dss;
        DataSet dss1;
        DataSet dss2;
        int BpId = 0;
        int EmpId = 0;
        int info3 = 0;
        int info4 = 0;
        int businesstype = 0;
        int category = 0;
        int area = 0;
        int intLevel = 0;

        double TotalAmount = 0;
        int ClosingPer = 0;
        DataSet dsActivity;
        DataSet dsCompetitor;

        DataSet dsProduct;
        DateTime PredictedClosingDate;
        int PredictedClosing = 0;
        string PredictedClosingPeriod = string.Empty;
        string InterestLevel = string.Empty;
        double PotentialPotAmount = 0;
        double PotentialWeightedAmount = 0;

        try
        {

            if (Page.IsValid)
            {
                //if (txtLeadNo.Text != string.Empty)
                //    LeadNo = int.Parse(txtLeadNo.Text);

                startDate = DateTime.Parse(txtCreationDate.Text);
                LeadName = txtLeadName.Text;//lead ref
                address = txtAddress.Text;//
                mobile = txtMobile.Text;//
                Telephone = txtTelephone.Text;//

                string check = string.Empty;

                if (chk.Checked == false)//
                {
                    BpName = txtBPName.Text;
                    BpId = 0;
                    check = "N";
                }
                else//
                {
                    BpName = cmbCustomer.SelectedItem.Text;
                    BpId = Convert.ToInt32(cmbCustomer.SelectedValue);
                    check = "Y";
                }

                ContactName = txtContactName.Text;
                EmpId = Convert.ToInt32(drpIncharge.SelectedValue);//
                EmpName = drpIncharge.SelectedItem.Text;
                Status = drpStatus.SelectedValue;//doc status
                LeadStatus = drpLeadStatus.SelectedValue;//   

                if (txtClosingDate.Text == "")//
                {
                    ClosingDate = DateTime.Parse("01/01/2000");
                }
                else
                {
                    ClosingDate = DateTime.Parse(txtClosingDate.Text);
                }

                if (txtPredictedClosingDate.Text == "")//
                {
                    PredictedClosingDate = DateTime.Parse("01/01/2000");
                }
                else
                {
                    PredictedClosingDate = DateTime.Parse(txtPredictedClosingDate.Text);
                }

                // PredictedClosingDate = DateTime.Parse(txtPredictedClosingDate.Text);//
                info1 = txtInformation1.Text;
                info3 = Convert.ToInt32(drpInformation3.SelectedValue);//
                info4 = Convert.ToInt32(drpInformation4.SelectedValue);//
                businesstype = Convert.ToInt32(drpBusinessType.SelectedValue);//
                category = Convert.ToInt32(drpCategory.SelectedValue);//
                area = Convert.ToInt32(drpArea.SelectedValue);//
                intLevel = Convert.ToInt32(drpIntLevel.SelectedValue);//


                string usernam = Request.Cookies["LoggedUserName"].Value;

                dsStages = (DataSet)Session["contactDs"];


                dss = (DataSet)Session["ProductDs"];
                //&&&&&& Product tab Insert Dataset &&&&&&&&&&&&&&&&& 
                if (Session["ProductDs"] == null) // New code
                {
                    dss = (DataSet)Session["ProductDs"];

                    if (dss == null)
                    {

                        for (int vLoop = 0; vLoop < GrdViewLeadproduct.Rows.Count; vLoop++)
                        {
                            DropDownList drpProduct = (DropDownList)GrdViewLeadproduct.Rows[vLoop].FindControl("drpproduct");
                            Label txtPrdID = (Label)GrdViewLeadproduct.Rows[vLoop].FindControl("txtPrdId");
                        }


                        //DataSet dss;
                        DataTable dt;
                        DataRow drNew;

                        DataColumn dc;

                        dss = new DataSet();
                        dt = new DataTable();

                        dc = new DataColumn("Prd");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("PrdID");
                        dt.Columns.Add(dc);

                        dss.Tables.Add(dt);

                        for (int vLoop = 0; vLoop < GrdViewLeadproduct.Rows.Count; vLoop++)
                        {
                            DropDownList drpProduct = (DropDownList)GrdViewLeadproduct.Rows[vLoop].FindControl("drpproduct");
                            Label txtPrdID = (Label)GrdViewLeadproduct.Rows[vLoop].FindControl("txtPrdId");

                            drNew = dt.NewRow();
                            drNew["Prd"] = Convert.ToString(drpProduct.SelectedItem.Text);
                            drNew["PrdID"] = txtPrdID.Text;
                            dss.Tables[0].Rows.Add(drNew);
                        }
                    }
                }

                for (int vLoop = 0; vLoop < GrdViewLeadproduct.Rows.Count; vLoop++)
                {
                    DropDownList drpProduct = (DropDownList)GrdViewLeadproduct.Rows[vLoop].FindControl("drpproduct");
                    Label txtPrdID = (Label)GrdViewLeadproduct.Rows[vLoop].FindControl("txtPrdId");

                    int col = vLoop + 1;

                    //if (drpProduct.SelectedValue != "0" && txtPrdID.Text != "")
                    //{
                        if (drpProduct.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Product in row " + col + " ')", true);
                            return;
                        }
                        else if (txtPrdID.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill ProductID in row " + col + " ')", true);
                            return;
                        }
                    //}                   
                }

                //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&


                dss1 = (DataSet)Session["CompetitorDs"];
                //&&&&&& Competitors tab Insert Dataset &&&&&&&&&&&&&&&&& 
                if (Session["CompetitorDs"] == null) // New code
                {
                    dss1 = (DataSet)Session["CompetitorDs"];

                    if (dss1 == null)
                    {
                        for (int vLoop = 0; vLoop < GrdViewLeadCompetitor.Rows.Count; vLoop++)
                        {
                            TextBox txtComName = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtComeName");
                            TextBox txtThrlvl = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtThrLvl");
                            TextBox txtOuestrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtOurStrWeakness");
                            TextBox txtComstrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtCompStrWeakness");
                            TextBox txtremarks = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtRemarks");
                        }

                        //DataSet dss1;
                        DataTable dt;
                        DataRow drNew;

                        DataColumn dc;

                        dss1 = new DataSet();
                        dt = new DataTable();

                        dc = new DataColumn("ComName");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ThrLvl");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("OurStrWeak");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ComStrWeak");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Remarks");
                        dt.Columns.Add(dc);

                        dss1.Tables.Add(dt);

                        for (int vLoop = 0; vLoop < GrdViewLeadCompetitor.Rows.Count; vLoop++)
                        {
                            TextBox txtComName = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtComeName");
                            TextBox txtThrlvl = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtThrLvl");
                            TextBox txtOuestrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtOurStrWeakness");
                            TextBox txtComstrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtCompStrWeakness");
                            TextBox txtremarks = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtRemarks");


                            drNew = dt.NewRow();
                            drNew["ComName"] = txtComName.Text;
                            drNew["ThrLvl"] = txtThrlvl.Text;
                            drNew["OurStrWeak"] = txtOuestrweak.Text;
                            drNew["ComStrWeak"] = txtComstrweak.Text;
                            drNew["Remarks"] = txtremarks.Text;
                            dss1.Tables[0].Rows.Add(drNew);
                        }
                    }
                }

                for (int vLoop = 0; vLoop < GrdViewLeadCompetitor.Rows.Count; vLoop++)
                {
                    TextBox txtComName = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtComeName");
                    TextBox txtThrlvl = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtThrLvl");
                    TextBox txtOuestrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtOurStrWeakness");
                    TextBox txtComstrweak = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtCompStrWeakness");
                    TextBox txtremarks = (TextBox)GrdViewLeadCompetitor.Rows[vLoop].FindControl("txtRemarks");

                    int col = vLoop + 1;

                    if (txtComName.Text != "" || txtThrlvl.Text != "" || txtOuestrweak.Text != "" || txtComstrweak.Text != "" || txtremarks.Text != "")
                    {

                        if (txtComName.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Competitor Name in row " + col + " ')", true);
                            return;
                        }
                        else if (txtThrlvl.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Threat Level in row " + col + " ')", true);
                            return;
                        }
                        else if (txtOuestrweak.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Our Strengths and Weaknesses in row " + col + " ')", true);
                            return;
                        }
                        else if (txtComstrweak.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Competitor's Strengths and Weaknesses in row " + col + " ')", true);
                            return;
                        }
                        else if (txtremarks.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Remarks in row " + col + " ')", true);
                            return;
                        }
                    }
                }

                //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                dss2 = (DataSet)Session["ActivityDs"];
                //&&&&&& Activity tab Insert Dataset &&&&&&&&&&&&&&&&& 
                if (Session["ActivityDs"] == null) // New code
                {
                    dss2 = (DataSet)Session["ActivityDs"];

                    if (dss2 == null)
                    {
                        for (int vLoop = 0; vLoop < GrdViewLeadActivity.Rows.Count; vLoop++)
                        {
                            DropDownList drpactivityName = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpactivityName");
                            TextBox txtactivityLoc = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiLoc");
                            TextBox txtactivityDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiDate");
                            DropDownList drpnxtActivity = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpnxtActivity");
                            TextBox txtnxtActDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtNxtActyDate");
                            DropDownList drpemployee = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpemployee");
                            //TextBox txtmodeofCnt = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtModrofcnt");
                            DropDownList drpmodeofCnt = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpModrofcnt");
                            DropDownList drpinfo2 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo2");
                            DropDownList drpinfo5 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo5");
                            TextBox txtremarks = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtremarks");
                        }

                        // DataSet dss2;
                        DataTable dt;
                        DataRow drNew;

                        DataColumn dc;

                        dss2 = new DataSet();
                        dt = new DataTable();

                        dc = new DataColumn("ActName");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ActNameID");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ActLoc");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("ActDate");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("NxtAct");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("NxtActID");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("NxtActDte");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Emp");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("EmpID");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("MdeofCnt");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Info2");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Info5");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Remarks");
                        dt.Columns.Add(dc);

                        dss2.Tables.Add(dt);

                        for (int vLoop = 0; vLoop < GrdViewLeadActivity.Rows.Count; vLoop++)
                        {
                            DropDownList drpactivityName = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpactivityName");
                            TextBox txtactivityLoc = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiLoc");
                            TextBox txtactivityDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiDate");
                            DropDownList drpnxtActivity = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpnxtActivity");
                            TextBox txtnxtActDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtNxtActyDate");
                            DropDownList drpemployee = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpemployee");
                            //TextBox txtmodeofCnt = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtModrofcnt");
                            DropDownList drpmodeofCnt = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpModrofcnt");
                            DropDownList drpinfo2 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo2");
                            DropDownList drpinfo5 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo5");
                            TextBox txtremarks = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtremarks");


                            drNew = dt.NewRow();
                            drNew["ActName"] = Convert.ToString(drpactivityName.SelectedItem.Text);
                            drNew["ActNameID"] = Convert.ToInt32(drpactivityName.SelectedItem.Value);
                            drNew["ActLoc"] = txtactivityLoc.Text;
                            drNew["ActDate"] = txtactivityDate.Text;
                            drNew["NxtAct"] = Convert.ToString(drpnxtActivity.SelectedItem.Text);
                            drNew["NxtActID"] = Convert.ToInt32(drpnxtActivity.SelectedItem.Value);
                            drNew["NxtActDte"] = txtnxtActDate.Text;
                            drNew["Emp"] = Convert.ToString(drpemployee.SelectedItem.Text);
                            drNew["EmpID"] = Convert.ToInt32(drpemployee.SelectedItem.Value);
                            drNew["MdeofCnt"] = Convert.ToString(drpmodeofCnt.SelectedItem.Value);
                            drNew["Info2"] = Convert.ToString(drpinfo2.SelectedItem.Value);
                            drNew["Info5"] = Convert.ToString(drpinfo5.SelectedItem.Value);
                            drNew["Remarks"] = txtremarks.Text;
                            dss2.Tables[0].Rows.Add(drNew);
                        }
                    }
                }


                for (int vLoop = 0; vLoop < GrdViewLeadActivity.Rows.Count; vLoop++)
                {
                    DropDownList drpactivityName = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpactivityName");
                    TextBox txtactivityLoc = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiLoc");
                    TextBox txtactivityDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtActiDate");
                    DropDownList drpnxtActivity = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpnxtActivity");
                    TextBox txtnxtActDate = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtNxtActyDate");
                    DropDownList drpemployee = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpemployee");
                    //TextBox txtmodeofCnt = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtModrofcnt");
                    DropDownList drpmodeofCnt = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpModrofcnt");
                    DropDownList drpinfo2 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo2");
                    DropDownList drpinfo5 = (DropDownList)GrdViewLeadActivity.Rows[vLoop].FindControl("drpinfo5");
                    TextBox txtremarks = (TextBox)GrdViewLeadActivity.Rows[vLoop].FindControl("txtremarks");

                    int col = vLoop + 1;

                    if (drpactivityName.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Lead Activity in row " + col + " ')", true);
                        return;
                    }
                    //else if (txtactivityLoc.Text == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Lead Location in row " + col + " ')", true);
                    //    return;
                    //}
                    else if (txtactivityDate.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Lead Activity Date in row " + col + " ')", true);
                        return;
                    }
                    else if (drpnxtActivity.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Follow-up Activity in row " + col + " ')", true);
                        return;
                    }
                    else if (txtnxtActDate.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Follow-up Activity Date in row " + col + " ')", true);
                        return;
                    }
                    else if (drpemployee.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Employee Responsible in row " + col + " ')", true);
                        return;
                    }
                    else if (drpmodeofCnt.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Mode of contact in row " + col + " ')", true);
                        return;
                    }
                    //else if (drpinfo2.SelectedValue == "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Additional Information4 in row " + col + " ')", true);
                    //    return;
                    //}
                    //else if (drpinfo5.SelectedValue == "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Additional Information5 in row " + col + " ')", true);
                    //    return;
                    //}
                    else if (txtremarks.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Remarks in row " + col + " ')", true);
                        return;
                    }
                }

                //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&


                string connStr = GetConnectionString();

                LeadBusinessLogic bl = new LeadBusinessLogic(connStr);
                //pass dss for prodct tab
                //pass dss1 for completor tab
                //pass dss2 for activity tab
                string connection = Request.Cookies["Company"].Value;

                // bl.AddLead(LeadNo, startDate, LeadName, address, mobile, Telephone, BpName, BpId, ContactName, EmpId, EmpName, Status, branch, LeadStatus, TotalAmount, ClosingPer, ClosingDate, PredictedClosing, PredictedClosingDate, PotentialPotAmount, PotentialWeightedAmount, PredictedClosingPeriod, InterestLevel, usernam, dsStages, dss1, dss2, dss, check);
                bl.AddLead(connection,LeadNo, startDate, LeadName, address, mobile, Telephone, BpName, BpId, ContactName, EmpId, EmpName, Status, LeadStatus, ClosingDate, PredictedClosingDate, info1, info3, info4, businesstype, category, area, intLevel, usernam, dss1, dss2, dss, check);


                string salestype = string.Empty;
                int ScreenNo = 0;
                string ScreenName = string.Empty;

              //  string connection = Request.Cookies["Company"].Value;
                BusinessLogic bl1 = new BusinessLogic();

                salestype = "Lead Management";
                ScreenName = "Lead Management";

                bool mobile1 = false;
                bool Email = false;
                string emailsubject = string.Empty;

                string emailcontent = string.Empty;
                if (check == "Y")
                {
                    if (hdEmailRequired.Value == "YES")
                    {
                        DataSet dsd = bl1.GetLedgerInfoForId(connection, BpId);
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


                        DataSet dsdd = bl1.GetDetailsForScreenNo(connection, ScreenName, "");
                        if (dsdd != null)
                        {
                            if (dsdd.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdd.Tables[0].Rows)
                                {
                                    ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                    mobile1 = Convert.ToBoolean(dr["mobile"]);
                                    Email = Convert.ToBoolean(dr["Email"]);
                                    emailsubject = Convert.ToString(dr["emailsubject"]);
                                    emailcontent = Convert.ToString(dr["emailcontent"]);

                                    if (ScreenType == 1)
                                    {
                                        if (dr["Name1"].ToString() == "Sales Executive")
                                        {
                                            toAddress = toAdd;
                                        }
                                        else if ((dr["Name1"].ToString() == "Customer")||(dr["Name1"].ToString() == "Ledger"))
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
                                        int index132 = emailcontent.IndexOf("@EmpName");
                                        body = EmpName;
                                        if (index132 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index132, 8).Insert(index132, body);
                                        }

                                        int index312 = emailcontent.IndexOf("@User");
                                        body = usernam;
                                        if (index312 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                        }
                                        int index2 = emailcontent.IndexOf("@LeadName");
                                        body = LeadName;
                                        if (index2 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index2, 9).Insert(index2, body);
                                        }
                                        int index = emailcontent.IndexOf("@BpName");
                                        body = BpName;
                                        if (index >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index, 7).Insert(index, body);
                                        }
                                        int index1 = emailcontent.IndexOf("@Status");
                                        body = Convert.ToString(Status);
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
                }

                string conn1 = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                UtilitySMS utilSMS = new UtilitySMS(conn1);
                string UserID = Page.User.Identity.Name;

                string smscontent = string.Empty;
                if (check == "Y")
                {
                    if (hdSMSRequired.Value == "YES")
                    {
                        DataSet dsd = bl1.GetLedgerInfoForId(connection, BpId);
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


                        DataSet dsdd = bl1.GetDetailsForScreenNo(connection, ScreenName, "");
                        if (dsdd != null)
                        {
                            if (dsdd.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdd.Tables[0].Rows)
                                {
                                    ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                    mobile1 = Convert.ToBoolean(dr["mobile"]);
                                    smscontent = Convert.ToString(dr["smscontent"]);

                                    if (ScreenType == 1)
                                    {
                                        if (dr["Name1"].ToString() == "Sales Executive")
                                        {
                                            toAddress = toAdd;
                                        }
                                        else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger"))
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
                                    if (mobile1 == true)
                                    {

                                        string body = "\n";

                                        int index123 = smscontent.IndexOf("@Branch");
                                        body = Request.Cookies["Company"].Value;
                                        if (index123 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                        }
                                        int index132 = smscontent.IndexOf("@EmpName");
                                        body = EmpName;
                                        if (index132 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index132, 8).Insert(index132, body);
                                        }
                                        int index312 = smscontent.IndexOf("@User");
                                        body = usernam;
                                        if (index312 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                        }

                                        int index2 = smscontent.IndexOf("@LeadName");
                                        body = LeadName;
                                        if (index2 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index2, 9).Insert(index2, body);

                                        }

                                        int index = smscontent.IndexOf("@BpName");
                                        body = BpName;
                                        if (index >= 0)
                                        {
                                            smscontent = smscontent.Remove(index, 7).Insert(index, body);
                                        }
                                        int index1 = emailcontent.IndexOf("@Status");
                                        body = Convert.ToString(Status);
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


                GrdViewLead.DataBind();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Lead Details saved successfully.')", true);

                BindGrid("Open", "DocStatus");
                UpdatePanelPage.Update();
                ModalPopupExtender2.Hide();

                return;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }
    }
    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        ddCriteria.SelectedIndex = 0;
        BindGrid("", "");
    }


    private void AddNewRowProduct()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable1.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                {
                    DropDownList drpProduct =
                     (DropDownList)GrdViewLeadproduct.Rows[rowIndex].Cells[1].FindControl("drpproduct");
                    Label txtPrdID =
                      (Label)GrdViewLeadproduct.Rows[rowIndex].Cells[2].FindControl("txtPrdId");

                    drCurrentRow = dtCurrentTable1.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable1.Rows[i - 1]["Prd"] = drpProduct.SelectedValue;
                    dtCurrentTable1.Rows[i - 1]["PrdID"] = txtPrdID.Text;


                    rowIndex++;
                }
                dtCurrentTable1.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable1;

                GrdViewLeadproduct.DataSource = dtCurrentTable1;
                GrdViewLeadproduct.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataProduct();
    }

    private void AddNewRowCompetitors()
    {
        int rowIndex2 = 0;

        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable2.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable2.Rows.Count; i++)
                {
                    TextBox txtComName =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[1].FindControl("txtComeName");
                    TextBox txtThrlvl =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[2].FindControl("txtThrLvl");
                    TextBox txtOuestrweak =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[3].FindControl("txtOurStrWeakness");
                    TextBox txtComstrweak =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[4].FindControl("txtCompStrWeakness");
                    TextBox txtremarks =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[5].FindControl("txtRemarks");


                    drCurrentRow = dtCurrentTable2.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable2.Rows[i - 1]["ComName"] = txtComName.Text;
                    dtCurrentTable2.Rows[i - 1]["ThrLvl"] = txtThrlvl.Text;
                    dtCurrentTable2.Rows[i - 1]["OurStrWeak"] = txtOuestrweak.Text;
                    dtCurrentTable2.Rows[i - 1]["ComStrWeak"] = txtComstrweak.Text;
                    dtCurrentTable2.Rows[i - 1]["Remarks"] = txtremarks.Text;

                    rowIndex2++;
                }
                dtCurrentTable2.Rows.Add(drCurrentRow);
                ViewState["CurrentTable2"] = dtCurrentTable2;

                GrdViewLeadCompetitor.DataSource = dtCurrentTable2;
                GrdViewLeadCompetitor.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataCompetitors();
    }

    private void AddNewRowActivity()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable3"] != null)
        {
            DataTable dtCurrentTable3 = (DataTable)ViewState["CurrentTable3"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable3.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable3.Rows.Count; i++)
                {
                    DropDownList drpactivityName =
                     (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[1].FindControl("drpactivityName");
                    TextBox txtactivityLoc =
                      (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[2].FindControl("txtActiLoc");
                    TextBox txtactivityDate =
                      (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[3].FindControl("txtActiDate");
                    DropDownList drpnxtActivity =
                     (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[4].FindControl("drpnxtActivity");
                    TextBox txtnxtActDate =
                     (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[5].FindControl("txtNxtActyDate");
                    DropDownList drpemployee =
                     (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[6].FindControl("drpemployee");
                    //TextBox txtmodeofCnt =
                    //  (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[4].FindControl("txtModrofcnt");
                    DropDownList drpmodeofCnt =
                    (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[7].FindControl("drpModrofcnt");
                    DropDownList drpinfo2 =
                       (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[8].FindControl("drpinfo2");
                    DropDownList drpinfo5 =
                       (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[9].FindControl("drpinfo5");
                    TextBox txtremarks =
                      (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[10].FindControl("txtremarks");


                    drCurrentRow = dtCurrentTable3.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable3.Rows[i - 1]["ActName"] = drpactivityName.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["ActLoc"] = txtactivityLoc.Text;
                    dtCurrentTable3.Rows[i - 1]["ActDate"] = txtactivityDate.Text;
                    dtCurrentTable3.Rows[i - 1]["NxtAct"] = drpnxtActivity.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["NxtActDte"] = txtnxtActDate.Text;
                    dtCurrentTable3.Rows[i - 1]["Emp"] = drpemployee.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["MdeofCnt"] = drpmodeofCnt.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["Info2"] = drpinfo2.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["Info5"] = drpinfo5.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["Remarks"] = txtremarks.Text;

                    rowIndex++;
                }
                dtCurrentTable3.Rows.Add(drCurrentRow);
                ViewState["CurrentTable3"] = dtCurrentTable3;

                GrdViewLeadActivity.DataSource = dtCurrentTable3;
                GrdViewLeadActivity.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataActivity();
    }

    private void SetPreviousDataProduct()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList drpProduct =
                     (DropDownList)GrdViewLeadproduct.Rows[rowIndex].Cells[1].FindControl("drpproduct");
                    Label txtPrdID =
                      (Label)GrdViewLeadproduct.Rows[rowIndex].Cells[2].FindControl("txtPrdId");

                    drpProduct.SelectedValue = dt.Rows[i]["Prd"].ToString();
                    txtPrdID.Text = dt.Rows[i]["PrdID"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    private void SetPreviousDataCompetitors()
    {
        int rowIndex2 = 0;
        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable2"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox txtComName =
                     (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[1].FindControl("txtComeName");
                    TextBox txtThrlvl =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[2].FindControl("txtThrLvl");
                    TextBox txtOuestrweak =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[3].FindControl("txtOurStrWeakness");
                    TextBox txtComstrweak =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[4].FindControl("txtCompStrWeakness");
                    TextBox txtremarks =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[5].FindControl("txtRemarks");


                    txtComName.Text = dt.Rows[i]["ComName"].ToString();
                    txtThrlvl.Text = dt.Rows[i]["ThrLvl"].ToString();
                    txtOuestrweak.Text = dt.Rows[i]["OurStrWeak"].ToString();
                    txtComstrweak.Text = dt.Rows[i]["ComStrWeak"].ToString();
                    txtremarks.Text = dt.Rows[i]["Remarks"].ToString();

                    rowIndex2++;
                }
            }
        }
    }

    private void SetPreviousDataActivity()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable3"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable3"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList drpactivityName =
                    (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[1].FindControl("drpactivityName");
                    TextBox txtactivityLoc =
                      (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[2].FindControl("txtActiLoc");
                    TextBox txtactivityDate =
                      (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[3].FindControl("txtActiDate");
                    DropDownList drpnxtActivity =
                     (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[4].FindControl("drpnxtActivity");
                    TextBox txtnxtActDate =
                     (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[5].FindControl("txtNxtActyDate");
                    DropDownList drpemployee =
                     (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[6].FindControl("drpemployee");
                    //TextBox txtmodeofCnt =
                    //  (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[4].FindControl("txtModrofcnt");
                    DropDownList drpmodeofCnt =
                   (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[7].FindControl("drpModrofcnt");
                    DropDownList drpinfo2 =
                       (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[8].FindControl("drpinfo2");
                    DropDownList drpinfo5 =
                       (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[9].FindControl("drpinfo5");
                    TextBox txtremarks =
                      (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[10].FindControl("txtremarks");


                    drpactivityName.SelectedValue = dt.Rows[i]["ActName"].ToString();
                    txtactivityLoc.Text = dt.Rows[i]["ActLoc"].ToString();
                    txtactivityDate.Text = dt.Rows[i]["ActDate"].ToString();
                    drpnxtActivity.SelectedValue = dt.Rows[i]["NxtAct"].ToString();
                    txtnxtActDate.Text = dt.Rows[i]["NxtActDte"].ToString();
                    drpemployee.SelectedValue = dt.Rows[i]["Emp"].ToString();
                    drpmodeofCnt.SelectedValue = dt.Rows[i]["MdeofCnt"].ToString();
                    drpinfo2.SelectedValue = dt.Rows[i]["Info2"].ToString();
                    drpinfo5.SelectedValue = dt.Rows[i]["Info5"].ToString();
                    txtremarks.Text = dt.Rows[i]["Remarks"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    private void SetRowDataProduct()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable1.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                {
                    DropDownList DrpProduct =
                    (DropDownList)GrdViewLeadproduct.Rows[rowIndex].Cells[1].FindControl("drpproduct");
                    Label txtprdID =
                     (Label)GrdViewLeadproduct.Rows[rowIndex].Cells[2].FindControl("txtPrdId");


                    drCurrentRow = dtCurrentTable1.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable1.Rows[i - 1]["Prd"] = DrpProduct.SelectedValue;
                    dtCurrentTable1.Rows[i - 1]["PrdID"] = txtprdID.Text;
                    rowIndex++;

                }

                ViewState["CurrentTable1"] = dtCurrentTable1;
                GrdViewLeadproduct.DataSource = dtCurrentTable1;
                GrdViewLeadproduct.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataProduct();
    }

    private void SetRowDataCompetitors()
    {
        int rowIndex2 = 0;

        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable2.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable2.Rows.Count; i++)
                {
                    TextBox txtComName =
                    (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[1].FindControl("txtComeName");
                    TextBox txtThrlvl =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[2].FindControl("txtThrLvl");
                    TextBox txtOuestrweak =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[3].FindControl("txtOurStrWeakness");
                    TextBox txtComstrweak =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[4].FindControl("txtCompStrWeakness");
                    TextBox txtremarks =
                      (TextBox)GrdViewLeadCompetitor.Rows[rowIndex2].Cells[5].FindControl("txtRemarks");


                    drCurrentRow = dtCurrentTable2.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable2.Rows[i - 1]["ComName"] = txtComName.Text;
                    dtCurrentTable2.Rows[i - 1]["ThrLvl"] = txtThrlvl.Text;
                    dtCurrentTable2.Rows[i - 1]["OurStrWeak"] = txtOuestrweak.Text;
                    dtCurrentTable2.Rows[i - 1]["ComStrWeak"] = txtComstrweak.Text;
                    dtCurrentTable2.Rows[i - 1]["Remarks"] = txtremarks.Text;
                    rowIndex2++;

                }

                ViewState["CurrentTable2"] = dtCurrentTable2;
                GrdViewLeadCompetitor.DataSource = dtCurrentTable2;
                GrdViewLeadCompetitor.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataCompetitors();
    }

    private void SetRowDataActivity()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable3"] != null)
        {
            DataTable dtCurrentTable3 = (DataTable)ViewState["CurrentTable3"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable3.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable3.Rows.Count; i++)
                {
                    DropDownList drpactivityName =
                   (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[1].FindControl("drpactivityName");
                    TextBox txtactivityLoc =
                      (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[2].FindControl("txtActiLoc");
                    TextBox txtactivityDate =
                      (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[3].FindControl("txtActiDate");
                    DropDownList drpnxtActivity =
                     (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[4].FindControl("drpnxtActivity");
                    TextBox txtnxtActDate =
                     (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[5].FindControl("txtNxtActyDate");
                    DropDownList drpemployee =
                     (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[6].FindControl("drpemployee");
                    //TextBox txtmodeofCnt =
                    //  (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[4].FindControl("txtModrofcnt");
                    DropDownList drpmodeofCnt =
                    (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[7].FindControl("drpModrofcnt");
                    DropDownList drpinfo2 =
                       (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[8].FindControl("drpinfo2");
                    DropDownList drpinfo5 =
                       (DropDownList)GrdViewLeadActivity.Rows[rowIndex].Cells[9].FindControl("drpinfo5");
                    TextBox txtremarks =
                      (TextBox)GrdViewLeadActivity.Rows[rowIndex].Cells[10].FindControl("txtremarks");


                    drCurrentRow = dtCurrentTable3.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable3.Rows[i - 1]["ActName"] = drpactivityName.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["ActLoc"] = txtactivityLoc.Text;
                    dtCurrentTable3.Rows[i - 1]["ActDate"] = txtactivityDate.Text;
                    dtCurrentTable3.Rows[i - 1]["NxtAct"] = drpnxtActivity.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["NxtActDte"] = txtnxtActDate.Text;
                    dtCurrentTable3.Rows[i - 1]["Emp"] = drpemployee.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["MdeofCnt"] = drpmodeofCnt.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["Info2"] = drpinfo2.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["Info5"] = drpinfo5.SelectedValue;
                    dtCurrentTable3.Rows[i - 1]["Remarks"] = txtremarks.Text;

                    rowIndex++;

                }

                ViewState["CurrentTable3"] = dtCurrentTable3;
                GrdViewLeadActivity.DataSource = dtCurrentTable3;
                GrdViewLeadActivity.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataActivity();
    }


    protected void GrdViewLeadproduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();

            ds = bl.ListProducts(sDataSource, "", "");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl = (DropDownList)e.Row.FindControl("drpproduct");
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
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void FirstGridViewRow_ProductTab()
    {
        DataTable dt1 = new DataTable();
        DataRow dr = null;
        dt1.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt1.Columns.Add(new DataColumn("Prd", typeof(string)));
        dt1.Columns.Add(new DataColumn("PrdID", typeof(string)));
        dr = dt1.NewRow();
        dr["RowNumber"] = 1;
        dr["Prd"] = string.Empty;
        dr["PrdID"] = string.Empty;

        dt1.Rows.Add(dr);

        ViewState["CurrentTable1"] = dt1;

        GrdViewLeadproduct.DataSource = dt1;
        GrdViewLeadproduct.DataBind();
        GrdViewLeadproduct.Visible = true;
    }

    private void FirstGridViewRow_CompetitorsTab()
    {
        DataTable dt3 = new DataTable();
        DataRow dr = null;
        dt3.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt3.Columns.Add(new DataColumn("ComName", typeof(string)));
        dt3.Columns.Add(new DataColumn("ThrLvl", typeof(string)));
        dt3.Columns.Add(new DataColumn("OurStrWeak", typeof(string)));
        dt3.Columns.Add(new DataColumn("ComStrWeak", typeof(string)));
        dt3.Columns.Add(new DataColumn("Remarks", typeof(string)));

        dr = dt3.NewRow();
        dr["RowNumber"] = 1;
        dr["ComName"] = string.Empty;
        dr["ThrLvl"] = string.Empty;
        dr["OurStrWeak"] = string.Empty;
        dr["ComStrWeak"] = string.Empty;
        dr["Remarks"] = string.Empty;

        dt3.Rows.Add(dr);

        ViewState["CurrentTable2"] = dt3;

        GrdViewLeadCompetitor.DataSource = dt3;
        GrdViewLeadCompetitor.DataBind();
        GrdViewLeadCompetitor.Visible = true;
    }

    private void FirstGridViewRow_ActivityTab()
    {
        DataTable dt2 = new DataTable();
        DataRow dr = null;
        dt2.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt2.Columns.Add(new DataColumn("ActName", typeof(string)));
        dt2.Columns.Add(new DataColumn("ActLoc", typeof(string)));
        dt2.Columns.Add(new DataColumn("ActDate", typeof(string)));
        dt2.Columns.Add(new DataColumn("NxtAct", typeof(string)));
        dt2.Columns.Add(new DataColumn("NxtActDte", typeof(string)));
        dt2.Columns.Add(new DataColumn("Emp", typeof(string)));
        dt2.Columns.Add(new DataColumn("MdeofCnt", typeof(string)));
        dt2.Columns.Add(new DataColumn("Info2", typeof(string)));
        dt2.Columns.Add(new DataColumn("Info5", typeof(string)));
        dt2.Columns.Add(new DataColumn("Remarks", typeof(string)));

        dr = dt2.NewRow();
        dr["RowNumber"] = 1;
        dr["ActName"] = string.Empty;
        dr["ActLoc"] = string.Empty;
        dr["ActDate"] = string.Empty;
        dr["NxtAct"] = string.Empty;
        dr["NxtActDte"] = string.Empty;
        dr["Emp"] = string.Empty;
        dr["MdeofCnt"] = string.Empty;
        dr["Info2"] = string.Empty;
        dr["Info5"] = string.Empty;
        dr["Remarks"] = string.Empty;

        dt2.Rows.Add(dr);

        ViewState["CurrentTable3"] = dt2;

        GrdViewLeadActivity.DataSource = dt2;
        GrdViewLeadActivity.DataBind();
        GrdViewLeadActivity.Visible = true;
    }
    protected void GrdViewLeadCompetitor_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GrdViewLeadActivity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet dsactname = new DataSet();
            DataSet dsnextacty = new DataSet();
            DataSet dsEmp = new DataSet();
            DataSet dsinfo2 = new DataSet();
            DataSet dsMoofcnt = new DataSet();
            DataSet dsinfo5 = new DataSet();
            string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();


            dsactname = bl.ListActivityName();
            //dsnextacty = bl.ListNextActivity();
            dsEmp = bl.ListExecutive();
            dsinfo2 = bl.ListInformation4();
            dsinfo5 = bl.ListInformation5();
            dsMoofcnt = bl.ListModeofContact();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl3 = (DropDownList)e.Row.FindControl("drpactivityName");
                ddl3.Items.Clear();
                ListItem lifzzh = new ListItem("Select Activity Name", "0");
                lifzzh.Attributes.Add("style", "color:Black");
                ddl3.Items.Add(lifzzh);
                ddl3.DataSource = dsactname;
                ddl3.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl3.DataBind();
                ddl3.DataTextField = "TextValue";
                ddl3.DataValueField = "ID";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl1 = (DropDownList)e.Row.FindControl("drpnxtActivity");
                ddl1.Items.Clear();
                ListItem lifzzh1 = new ListItem("Select Next Activity", "0");
                lifzzh1.Attributes.Add("style", "color:Black");
                ddl1.Items.Add(lifzzh1);
                ddl1.DataSource = dsactname;
                ddl1.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl1.DataBind();
                ddl1.DataTextField = "TextValue";
                ddl1.DataValueField = "ID";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl2 = (DropDownList)e.Row.FindControl("drpemployee");
                ddl2.Items.Clear();
                ListItem lifzzh2 = new ListItem("Select Employee", "0");
                lifzzh2.Attributes.Add("style", "color:Black");
                ddl2.Items.Add(lifzzh2);
                ddl2.DataSource = dsEmp;
                ddl2.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl2.DataBind();
                ddl2.DataTextField = "empFirstName";
                ddl2.DataValueField = "empno";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl3 = (DropDownList)e.Row.FindControl("drpinfo2");
                ddl3.Items.Clear();
                ListItem lifzzh3 = new ListItem("Select Information4", "0");
                lifzzh3.Attributes.Add("style", "color:Black");
                ddl3.Items.Add(lifzzh3);
                ddl3.DataSource = dsinfo2;
                ddl3.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl3.DataBind();
                ddl3.DataTextField = "TextValue";
                ddl3.DataValueField = "ID";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl5 = (DropDownList)e.Row.FindControl("drpinfo5");
                ddl5.Items.Clear();
                ListItem lifzzh5 = new ListItem("Select Information5", "0");
                lifzzh5.Attributes.Add("style", "color:Black");
                ddl5.Items.Add(lifzzh5);
                ddl5.DataSource = dsinfo5;
                ddl5.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl5.DataBind();
                ddl5.DataTextField = "TextValue";
                ddl5.DataValueField = "ID";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl6 = (DropDownList)e.Row.FindControl("drpModrofcnt");
                ddl6.Items.Clear();
                ListItem lifzzh6 = new ListItem("Select Mode of Contact", "0");
                lifzzh6.Attributes.Add("style", "color:Black");
                ddl6.Items.Add(lifzzh6);
                ddl6.DataSource = dsMoofcnt;
                ddl6.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl6.DataBind();
                ddl6.DataTextField = "TextValue";
                ddl6.DataValueField = "ID";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowProduct();
    }
    protected void drpproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = GrdViewLeadproduct.Rows.Count; i == GrdViewLeadproduct.Rows.Count; i++)
        {
            DropDownList DrpProduct =
              (DropDownList)GrdViewLeadproduct.Rows[i - 1].Cells[1].FindControl("drpproduct");
            Label txtPrdID =
              (Label)GrdViewLeadproduct.Rows[i - 1].Cells[2].FindControl("txtPrdId");

            txtPrdID.Text = DrpProduct.SelectedValue;
        }
    }
    protected void ButtonAddCom_Click(object sender, EventArgs e)
    {
        AddNewRowCompetitors();
    }
    protected void ButtonAddActivity_Click(object sender, EventArgs e)
    {
        AddNewRowActivity();
    }
    protected void drpLeadStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpLeadStatus.Text == "Open")
        {
            txtClosingDate.Text = "";
            drpStatus.Text = "Open";
            drpStatus.Enabled = false;
        }
        else
        {
            txtClosingDate.Text = DateTime.Now.ToShortDateString();
            drpStatus.Text = "Closed";
            drpStatus.Enabled = false;
        }
    }
}