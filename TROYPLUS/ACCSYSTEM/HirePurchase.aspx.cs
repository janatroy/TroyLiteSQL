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

public partial class HirePurchase : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    string dbfileName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (!Page.IsPostBack)
            {
                mrBillDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                mrBillDate.MaximumValue = System.DateTime.Now.ToShortDateString();

                //myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

                BindDropdownList();
                Session["contactDs"] = null;

                loadBanks();

                GrdViewLead.PageSize = 7;
                loadSupplier();
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "HIPUR"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New ";
                }
                BindGrid("", "");

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

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearch.Text = "";
            BindGrid("", "");
            ddCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadSupplier()
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
        cmbCustomer.Focus();
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

    protected void txtdocchr_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtintamt.Text));
            txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtintamt.Text));
            UpdatePanel7.Update();

            txtdown1.Text = Convert.ToString(Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtinipay.Text));
            UpdatePanel16.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtupfront_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtintamt.Text));
            txtdown.Text = Convert.ToString(Convert.ToDouble(txtupfront.Text) + Convert.ToDouble(txtemi.Text));
            UpdatePanel13.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtemi_TextChanged(object sender, EventArgs e)
    {
        try
        {

            //txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtintamt.Text));
            txtdown.Text = Convert.ToString(Convert.ToDouble(txtupfront.Text) + Convert.ToDouble(txtemi.Text));
            UpdatePanel13.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtpuramt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(txtpuramt.Text).Length > 0)
            {
                if (Convert.ToString(txtinipay.Text).Length > 0)
                {
                    Txtlnamt.Text = Convert.ToString(Convert.ToDouble(txtpuramt.Text) - Convert.ToDouble(txtinipay.Text));
                }
                else
                {
                    Txtlnamt.Text = Convert.ToString(txtpuramt.Text);
                }
                UpdatePanel4.Update();
            }
            txtintamt_TextChanged(this, null);
            txtnoinst1_TextChanged(this, null);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtinipay_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(txtinipay.Text).Length > 0)
            {
                if (Convert.ToString(txtpuramt.Text).Length > 0)
                {
                    Txtlnamt.Text = Convert.ToString(Convert.ToDouble(txtpuramt.Text) - Convert.ToDouble(txtinipay.Text));
                }
                else
                {
                    Txtlnamt.Text = Convert.ToString(txtinipay.Text);
                }
                UpdatePanel4.Update();

                txtdown1.Text = Convert.ToString(Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtinipay.Text));
                UpdatePanel16.Update();

            }
            txtintamt_TextChanged(this, null);
            txtnoinst1_TextChanged(this, null);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtintamt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtintamt.Text));
            UpdatePanel7.Update();

            txtnoinst1_TextChanged(this, null);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtnoinst1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double txteachmonth = Convert.ToDouble(txtfinpay.Text) / Convert.ToDouble(txtnoinst1.Text);
            Txteach.Text = txteachmonth.ToString("#0");
            UpdatePanel8.Update();

            if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
            {
                DataSet ds = new DataSet();
                DataTable dt;
                DataRow drNew;
                dt = new DataTable();
                DataColumn dc;
                int ii = 1;
                string dtaa1 = string.Empty;

                dc = new DataColumn("ChequeNo");
                dt.Columns.Add(dc);

                dc = new DataColumn("DueDate");
                dt.Columns.Add(dc);

                dc = new DataColumn("Amount");
                dt.Columns.Add(dc);

                dc = new DataColumn("Cancelled");
                dt.Columns.Add(dc);

                dc = new DataColumn("Narration");
                dt.Columns.Add(dc);

                for (int i = 0; i < Convert.ToDouble(txtnoinst1.Text); i++)
                {

                    DataRow dr_final1312 = dt.NewRow();
                    dr_final1312["ChequeNo"] = "";

                    string aa = txtduedate.Text.ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");

                    if (ii == 1)
                    {
                        dr_final1312["DueDate"] = dtaa;
                    }
                    else
                    {
                        string dtt = Convert.ToDateTime(dtaa1).ToString("dd/MM/yyyy");
                        dr_final1312["DueDate"] = dtt;
                    }


                    dr_final1312["Amount"] = Txteach.Text;

                    //CheckBox txthh = (CheckBox)GrdViewItems.FindControl("chkboxCancelled");
                    //txthh.Checked = false;
                    //bool check = false;
                    //check = false;

                    //if (txthh.Checked)
                    //{
                    //    check = txthh.Checked;
                    //}
                    //else
                    //{
                    //    check = false;
                    //}

                    dr_final1312["Cancelled"] = "N";
                    dr_final1312["Narration"] = "";

                    if (ii == 1)
                    {
                        dtaa1 = Convert.ToDateTime(dtaa).AddMonths(1).ToString();
                    }
                    else
                    {
                        dtaa1 = Convert.ToDateTime(dtaa1).AddMonths(1).ToString();
                    }

                    ii = ii + 1;

                    dt.Rows.Add(dr_final1312);
                }

                ds.Tables.Add(dt);

                GrdViewItems.DataSource = ds;
                GrdViewItems.DataBind();


                for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                {
                    DropDownList txt = (DropDownList)GrdViewItems.Rows[vLoop].FindControl("chkboxCancelled");

                    txt.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtemiper_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double txteachemi = Convert.ToDouble(Txteach.Text) * Convert.ToDouble(txtemiper.Text);
            txtemi.Text = txteachemi.ToString("#0");
            txtdown.Text = Convert.ToString(Convert.ToDouble(txtupfront.Text) + Convert.ToDouble(txtemi.Text));
            UpdatePanel14.Update();
            UpdatePanel13.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    //{
    //    if (GrdViewLead.SelectedDataKey != null)
    //        e.InputParameters["Slno"] = GrdViewLead.SelectedDataKey.Value;

    //    e.InputParameters["usernam"] = Request.Cookies["LoggedUserName"].Value;
    //}

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

        ds = bl.GetHireList(connection, textSearch, dropDown);

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


    private void BindDropdownList()
    {
        //LeadBusinessLogic bl = new LeadBusinessLogic(sDataSource);
        //DataSet ds = new DataSet();
        
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

        //ds = bl.GetDropdownList(sDataSource, "NXTAXN");
        //cmbNextAction.DataSource = ds;
        //cmbNextAction.DataBind();
        //cmbNextAction.DataTextField = "TextValue";
        //cmbNextAction.DataValueField = "TextValue";

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

            Txtlnamt.Enabled = true;
            txtpuramt.Enabled = true;
            txtdocchr.Enabled = true;
            txtintamt.Enabled = true;
            txtfinpay.Enabled = true;
            txtnoinst1.Enabled = true;
            txtinipay.Enabled = true;
            txtdatepay.Enabled = true;
            txtduedate.Enabled = false;
            Txteach.Enabled = true;
            txtothers.Enabled = true;
            txtdown.Enabled = true;
            txtemi.Enabled = true;
            txtupfront.Enabled = true;
            txtemiper.Enabled = true;
            txtdown1.Enabled = true;
            txtBillDate.Enabled = false;
            cmbCustomer.Enabled = true;
            txtAccountNumber.Enabled = true;
            txtBranchName.Enabled = true;
            txtIFSCCode.Enabled = true;
            txtMobile.Enabled = true;
            txtdob.Enabled = false;
            drpPaymode.Enabled = true;
            drpBankName.Enabled = true;

            ModalPopupExtender2.Show();
            UpdateButton.Visible = false;
            AddButton.Visible = true;
            UpdateButton.Visible = false;
            Session["LeadID"] = "0";
            Session["contactDs"] = null;
            ShowLeadContactInfo();
            //txtCreationDate.Text = DateTime.Now.ToShortDateString();
            TabPanel2.Visible = false;

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtBillDate.Text = dtaa;

            txtdatepay.Text = dtaa;
            txtdatepay.Enabled = false;

            txtbillnonew.Focus();

            lblBillNo.Text = "- TBA -";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void Reset()
    {
        //txtLeadNo.Text = "";
        //txtAddress.Text = "";
        //txtProspCustName.Text = "";
        //txtMobile.Text = "";
        //txtEmail.Text = "";
        //txtLandline.Text = "";
        //cmbModeOfContact.SelectedIndex = 0;
        //cmbPersonalResp.SelectedIndex = 0;
        //cmbBussType.SelectedIndex = 0;
        //cmbBranch.SelectedIndex = 0;
        //cmbStatus.SelectedIndex = 0;
        //cmbLastCompAction.SelectedIndex = 0;
        //cmbNextAction.SelectedIndex = 0;
        //cmbCategory.SelectedIndex = 0;

        //txtInfo1.Text = "";
        //txtInfo2.Text = "";
        //ddlinfo3.SelectedIndex = 0;

        //ddlinfo4.SelectedIndex = 0;
        //ddlinfo5.SelectedIndex = 0;

        Txtlnamt.Text = "0";
        txtpuramt.Text = "0";
        txtdocchr.Text = "0";
        txtintamt.Text = "0";
        txtfinpay.Text = "0";
        txtnoinst1.Text = "0";
        txtinipay.Text = "0";
        txtdatepay.Text = "";
        txtduedate.Text = "";
        Txteach.Text = "0";
        txtothers.Text = "";

        txtdown.Text = "0";
        txtemi.Text = "0";
        txtupfront.Text = "0";

        txtemiper.Text = "0";
        txtdown1.Text = "0";
        txtBillDate.Text = DateTime.Now.ToShortDateString();

        cmbCustomer.SelectedIndex = 0;
        txtAccountNumber.Text = "";
        txtBranchName.Text = "";
        txtIFSCCode.Text = "";
        txtMobile.Text = "";
        txtdob.Text = "";
        drpPaymode.SelectedIndex = 0;
        drpBankName.SelectedIndex = 0;
    }

    private void ShowLeadContactInfo()
    {
        string connStr = GetConnectionString();

        if (Session["LeadID"] != null && Session["LeadID"].ToString() != "0")
        {
            LeadBusinessLogic bl = new LeadBusinessLogic(connStr);
            DataSet ds = bl.ListLeadContact(Session["LeadID"].ToString());

            if (ds != null)
            {
                //GrdViewLeadContact.DataSource = ds.Tables[0];
                //GrdViewLeadContact.DataBind();
            }

        }
        else
        {
            //GrdViewLeadContact.DataSource = null;
            //GrdViewLeadContact.DataBind();
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

                dc = new DataColumn("ContactRefID");
                dt.Columns.Add(dc);

                dc = new DataColumn("ContactedDate");
                dt.Columns.Add(dc);

                dc = new DataColumn("ContactSummary");
                dt.Columns.Add(dc);

                ds.Tables.Add(dt);

                drNew = dt.NewRow();

                drNew["ContactRefID"] = 1;
                //drNew["ContactedDate"] = txtContactedDate.Text;
                //drNew["ContactSummary"] = txtContactSummary.Text;
                ds.Tables[0].Rows.Add(drNew);
                Session["contactDs"] = ds;
            }
            else
            {
                ds = (DataSet)Session["contactDs"];

                int maxID = 0;

                if (ds.Tables[0].Rows.Count > 0)
                    maxID = int.Parse(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ContactRefID"].ToString());

                drNew = ds.Tables[0].NewRow();
                drNew["ContactRefID"] = maxID + 1;
                //drNew["ContactedDate"] = txtContactedDate.Text;
                //drNew["ContactSummary"] = txtContactSummary.Text;
                ds.Tables[0].Rows.Add(drNew);
                Session["contactDs"] = ds;
            }


            //this.GrdViewLeadContact.Visible = true;

            //GrdViewLeadContact.DataSource = ds.Tables[0];
            //GrdViewLeadContact.DataBind();

            //ModalPopupContact.Hide();
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
            //int currentRow = int.Parse(hdCurrentRow.Value);

            //ds.Tables[0].Rows[currentRow]["ContactedDate"] = txtContactedDate.Text;
            //ds.Tables[0].Rows[currentRow]["ContactSummary"] = txtContactSummary.Text;
            //ds.Tables[0].Rows[currentRow].EndEdit();
            //ds.Tables[0].Rows[currentRow].AcceptChanges();

            //GrdViewLeadContact.DataSource = ds.Tables[0];
            //GrdViewLeadContact.DataBind();
            //ModalPopupContact.Hide();
            Session["contactDs"] = ds;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkAddContact_Click(object sender, EventArgs e)
    {
        //cmdSaveContact.Visible = true;
        //cmdUpdateContact.Visible = false;
        //updatePnlContact.Update();

        //txtContactedDate.Text = string.Empty;
        //txtContactSummary.Text = string.Empty;

        //ModalPopupContact.Show();
    }

    protected void GrdViewLeadContact_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DataSet ds = new DataSet();
        //GridViewRow row = GrdViewLeadContact.SelectedRow;
        
        //hdCurrentRow.Value = Convert.ToString(row.DataItemIndex);

        //txtContactedDate.Text = row.Cells[1].Text;
        //txtContactSummary.Text = row.Cells[2].Text;
        //cmdSaveContact.Visible = false;
        //cmdUpdateContact.Visible = true;
        //ModalPopupContact.Show();

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


    protected void GrdViewLeadContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                ds = (DataSet)Session["contactDs"];
                //ds.Tables[0].Rows[GrdViewLeadContact.Rows[e.RowIndex].DataItemIndex].Delete();
                //ds.Tables[0].AcceptChanges();
                //GrdViewLeadContact.DataSource = ds;
                //GrdViewLeadContact.DataBind();
                Session["contactDs"] = ds;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void cmdCancelContact_Click(object sender, EventArgs e)
    {
        //ModalPopupContact.Hide();
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

                if (bl.CheckUserHaveEdit(usernam, "HIPUR"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "HIPUR"))
                {
                    //((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    //((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }
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

    protected void GrdViewLead_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //GridViewRow row = GrdViewLead.Rows[e.RowIndex];
            string usernam = Request.Cookies["LoggedUserName"].Value;

            int Slno = (int)GrdViewLead.DataKeys[e.RowIndex].Value;
            string connection = Request.Cookies["Company"].Value;
            string userID = string.Empty;
            userID = Page.User.Identity.Name;
            BusinessLogic bl = new BusinessLogic(GetConnectionString());
            bl.DeleteHirePurchase(connection, Slno, userID);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Hire Purchase Details Deleted Successfully')", true);
            BindGrid("", "");

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;


            salestype = "Hire Purchase";
            ScreenName = "Hire Purchase";

            double Amount = 0;
            string BillDate = string.Empty;
            string Billno = string.Empty;
            
            string PayTo = string.Empty;
            
            int DebitorID = 0;
            string CustomerName = string.Empty;

            DataSet ds = bl.GetHirePurchaseForId(Slno);

            if (ds != null)
            {
                Billno = Convert.ToString(ds.Tables[0].Rows[0]["Billno"].ToString());
                //PayTo = ds.Tables[0].Rows[0]["paymode"].ToString();
                Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["puramt"]);
                DebitorID = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerID"]);
                CustomerName = Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                BillDate = ds.Tables[0].Rows[0]["BillDate"].ToString();
            }



            bool mobile1 = false;
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
                            mobile1 = Convert.ToBoolean(dr["mobile"]);
                            Email = Convert.ToBoolean(dr["Email"]);
                            emailsubject = Convert.ToString(dr["emailsubject"]);
                            emailcontent = Convert.ToString(dr["emailcontent"]);

                            if (ScreenType == 1)
                            {
                                if (dr["Name1"].ToString() == "Customer")
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

                                int index312 = emailcontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = emailcontent.IndexOf("@Date");
                                body = BillDate.ToString();
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


                                //int index111 = emailcontent.IndexOf("@PayMode");
                                //body = drpPaymode.SelectedItem.Value;
                                //emailcontent = emailcontent.Remove(index111, 8).Insert(index111, body);

                                int index1 = emailcontent.IndexOf("@Amount");
                                body = Amount.ToString();
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
                            mobile1 = Convert.ToBoolean(dr["mobile"]);
                            smscontent = Convert.ToString(dr["smscontent"]);

                            if (ScreenType == 1)
                            {
                                if (dr["Name1"].ToString() == "Customer")
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

                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = smscontent.IndexOf("@Date");
                                body = BillDate.ToString();
                                if (index2 >= 0)
                                {
                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                }

                                int index = smscontent.IndexOf("@Customer");
                                body = CustomerName;
                                if (index >= 0)
                                {
                                    smscontent = emailcontent.Remove(index, 9).Insert(index, body);
                                }


                                //int index111 = smscontent.IndexOf("@PayMode");
                                //body = drpPaymode.SelectedItem.Value;
                                //smscontent = smscontent.Remove(index111, 8).Insert(index111, body);

                                int index1 = smscontent.IndexOf("@Amount");
                                body = Amount.ToString();
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
            BusinessLogic bl = new BusinessLogic(GetConnectionString());
            int Billno = Convert.ToInt32(GrdViewLead.SelectedDataKey.Value);

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            DataSet ds = bl.GetHirePurchaseForId(Billno);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["slno"] != null)
                        lblBillNo.Text = ds.Tables[0].Rows[0]["slno"].ToString();

                    if (ds.Tables[0].Rows[0]["BillNoNew"] != null)
                        txtbillnonew.Text = Convert.ToString(ds.Tables[0].Rows[0]["BillNoNew"]);

                    if (ds.Tables[0].Rows[0]["BranchRefNo"] != null)
                        txtbranchrefno.Text = Convert.ToString(ds.Tables[0].Rows[0]["BranchRefNo"]);

                    if (ds.Tables[0].Rows[0]["lnamt"] != null)
                        Txtlnamt.Text = Convert.ToString(ds.Tables[0].Rows[0]["lnamt"]);

                    //double down = Convert.ToDouble(txtdown.Text);
                    //double down1 = Convert.ToDouble(txtdown1.Text);
                    //double upfront = Convert.ToDouble(txtupfront.Text);
                    //double emi = Convert.ToDouble(txtemi.Text);
                    //double emiper = Convert.ToDouble(txtemiper.Text);

                    if (ds.Tables[0].Rows[0]["down"] != null)
                        txtdown.Text = Convert.ToString(ds.Tables[0].Rows[0]["down"]);
                    if (ds.Tables[0].Rows[0]["down1"] != null)
                        txtdown1.Text = Convert.ToString(ds.Tables[0].Rows[0]["down1"]);
                    if (ds.Tables[0].Rows[0]["upfront"] != null)
                        txtupfront.Text = Convert.ToString(ds.Tables[0].Rows[0]["upfront"]);
                    if (ds.Tables[0].Rows[0]["emi"] != null)
                        txtemi.Text = Convert.ToString(ds.Tables[0].Rows[0]["emi"]);
                    if (ds.Tables[0].Rows[0]["emiper"] != null)
                        txtemiper.Text = Convert.ToString(ds.Tables[0].Rows[0]["emiper"]);


                    if (ds.Tables[0].Rows[0]["puramt"] != null)
                        txtpuramt.Text = Convert.ToString(ds.Tables[0].Rows[0]["puramt"]);

                    if (ds.Tables[0].Rows[0]["dochr"] != null)
                        txtdocchr.Text = Convert.ToString(ds.Tables[0].Rows[0]["dochr"]);

                    if (ds.Tables[0].Rows[0]["intamt"] != null)
                        txtintamt.Text = Convert.ToString(ds.Tables[0].Rows[0]["intamt"]);

                    if (ds.Tables[0].Rows[0]["finpay"] != null)
                        txtfinpay.Text = Convert.ToString(ds.Tables[0].Rows[0]["finpay"]);

                    if (ds.Tables[0].Rows[0]["noinst"] != null)
                        txtnoinst1.Text = Convert.ToString(ds.Tables[0].Rows[0]["noinst"]);

                    if (ds.Tables[0].Rows[0]["inipay"] != null)
                        txtinipay.Text = Convert.ToString(ds.Tables[0].Rows[0]["inipay"]);

                    if (ds.Tables[0].Rows[0]["paydate"] != null)
                        txtdatepay.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["paydate"].ToString()).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["startdate"] != null)
                        txtduedate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["startdate"].ToString()).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["eachpay"] != null)
                        Txteach.Text = Convert.ToString(ds.Tables[0].Rows[0]["eachpay"]);

                    if (ds.Tables[0].Rows[0]["others"] != null)
                        txtothers.Text = Convert.ToString(ds.Tables[0].Rows[0]["others"]);

                    if (ds.Tables[0].Rows[0]["BillDate"] != null)
                        txtBillDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BillDate"].ToString()).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["DOB"] != null)
                        txtdob.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["Dayofpayment"] != null)
                        txtpaydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Dayofpayment"].ToString()).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["mobile"] != null)
                        txtMobile.Text = Convert.ToString(ds.Tables[0].Rows[0]["mobile"]);

                    if (ds.Tables[0].Rows[0]["BranchName"] != null)
                        txtBranchName.Text = Convert.ToString(ds.Tables[0].Rows[0]["BranchName"]);

                    if (ds.Tables[0].Rows[0]["IFSCCode"] != null)
                        txtIFSCCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["IFSCCode"]);

                    if (ds.Tables[0].Rows[0]["AccountNumber"] != null)
                        txtAccountNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["AccountNumber"]);

                    int BankID = 0;
                    if ((ds.Tables[0].Rows[0]["BankID"] != null) && (ds.Tables[0].Rows[0]["BankID"].ToString() != ""))
                    {
                        BankID = Convert.ToInt32(ds.Tables[0].Rows[0]["BankID"].ToString());
                        drpBankName.ClearSelection();
                        ListItem li = drpBankName.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(BankID.ToString()));
                        if (li != null) li.Selected = true;
                    }

                    int Paymode = 0;
                    if ((ds.Tables[0].Rows[0]["PaymentMode"] != null) && (ds.Tables[0].Rows[0]["PaymentMode"].ToString() != ""))
                    {
                        Paymode = Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentMode"].ToString());
                        drpPaymode.ClearSelection();
                        ListItem li = drpPaymode.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(Paymode.ToString()));
                        if (li != null) li.Selected = true;
                    }

                    int CustomerID = 0;
                    if ((ds.Tables[0].Rows[0]["CustomerID"] != null) && (ds.Tables[0].Rows[0]["CustomerID"].ToString() != ""))
                    {
                        CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerID"].ToString());
                        cmbCustomer.ClearSelection();
                        ListItem li = cmbCustomer.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(CustomerID.ToString()));
                        if (li != null) li.Selected = true;
                    }

                    if ((drpPaymode.SelectedValue == "3") || (drpPaymode.SelectedValue == "2"))
                    {
                        DataSet dsinstall = bl.Getinstalldetails(Billno);
                        if (dsinstall != null && dsinstall.Tables[0].Rows.Count > 0)
                        {
                            GrdViewItems.DataSource = dsinstall.Tables[0];
                            GrdViewItems.DataBind();

                            for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                            {
                                TextBox txt2 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDueDate");
                                txt2.Enabled = false;

                                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                                DropDownList txt = (DropDownList)GrdViewItems.Rows[vLoop].FindControl("chkboxCancelled");

                                if (Convert.ToDateTime(txt2.Text) >= Convert.ToDateTime(dtaa))
                                {
                                    txt.Enabled = true;
                                }
                                else
                                {
                                    txt.Enabled = false;
                                }

                                TextBox txt1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtChequeNo");
                                txt1.Enabled = false;

                                TextBox txt3 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtAmount");
                                txt3.Enabled = false;

                                TextBox txt32 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtRemarks");
                                txt32.Enabled = false;
                            }

                        }
                        else
                        {
                            GrdViewItems.DataSource = null;
                            GrdViewItems.DataBind();
                        }
                    }
                    else
                    {
                        GrdViewItems.DataSource = null;
                        GrdViewItems.DataBind();
                    }




                    if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
                    {
                        TabPanel2.Visible = true;
                        TabPanel2.Enabled = true;
                        if (drpPaymode.SelectedValue == "2")
                        {
                            lllklkl.Visible = true;
                            lllklkll.Visible = false;
                            lllklklll.Visible = false;
                        }
                        else if (drpPaymode.SelectedValue == "3")
                        {
                            lllklkl.Visible = true;
                            lllklkll.Visible = true;
                            lllklklll.Visible = true;
                        }
                    }
                    else
                        TabPanel2.Visible = false;



                    string connection = Request.Cookies["Company"].Value;
                    DataSet dsd = bl.GetReceiptForHireId(connection, Billno, CustomerID);
                    if (dsd != null && dsd.Tables[0].Rows.Count > 0)
                    {
                        Txtlnamt.Enabled = false;
                        txtpuramt.Enabled = false;
                        txtdocchr.Enabled = false;
                        txtintamt.Enabled = false;
                        txtfinpay.Enabled = false;
                        txtnoinst1.Enabled = false;
                        txtinipay.Enabled = false;
                        txtdatepay.Enabled = false;
                        txtduedate.Enabled = false;
                        Txteach.Enabled = false;
                        txtothers.Enabled = false;
                        txtdown.Enabled = false;
                        txtemi.Enabled = false;
                        txtupfront.Enabled = false;
                        txtemiper.Enabled = false;
                        txtdown1.Enabled = false;
                        txtBillDate.Enabled = false;
                        cmbCustomer.Enabled = false;
                        txtAccountNumber.Enabled = false;
                        txtBranchName.Enabled = false;
                        txtIFSCCode.Enabled = false;
                        txtMobile.Enabled = false;
                        txtdob.Enabled = false;
                        drpPaymode.Enabled = false;
                        drpBankName.Enabled = false;

                        if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
                        {
                            for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                            {
                                TextBox txt2 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDueDate");

                                for (int i = 0; i < dsd.Tables[0].Rows.Count; i++)
                                {
                                    DropDownList txt = (DropDownList)GrdViewItems.Rows[vLoop].FindControl("chkboxCancelled");

                                    if (Convert.ToDateTime(txt2.Text) == Convert.ToDateTime(dsd.Tables[0].Rows[i]["TransDate"]))
                                    {
                                        txt.Enabled = false;
                                    }
                                    //else
                                    //{
                                    //    txt.Enabled = true;
                                    //}
                                }

                            }
                        }
                    }
                    else
                    {
                        Txtlnamt.Enabled = true;
                        txtpuramt.Enabled = true;
                        txtdocchr.Enabled = true;
                        txtintamt.Enabled = true;
                        txtfinpay.Enabled = true;
                        txtnoinst1.Enabled = true;
                        txtinipay.Enabled = true;
                        txtdatepay.Enabled = true;
                        txtduedate.Enabled = false;
                        Txteach.Enabled = true;
                        txtothers.Enabled = true;
                        txtdown.Enabled = true;
                        txtemi.Enabled = true;
                        txtupfront.Enabled = true;
                        txtemiper.Enabled = true;
                        txtdown1.Enabled = true;
                        txtBillDate.Enabled = false;
                        cmbCustomer.Enabled = true;
                        txtAccountNumber.Enabled = true;
                        txtBranchName.Enabled = true;
                        txtIFSCCode.Enabled = true;
                        txtMobile.Enabled = true;
                        txtdob.Enabled = false;
                        drpPaymode.Enabled = true;
                        drpBankName.Enabled = true;
                    }

                    DataSet dsDetails = bl.GetLeadContacts(Billno);

                    if (dsDetails != null && dsDetails.Tables[0].Rows.Count > 0)
                    {
                        //GrdViewLeadContact.DataSource = dsDetails.Tables[0];
                        //GrdViewLeadContact.DataBind();
                        Session["contactDs"] = dsDetails;
                    }

                    UpdateButton.Visible = true;
                    AddButton.Visible = false;
                    ModalPopupExtender2.Show();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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
        DateTime creationDate;
        int LeadID = 0;
        DataSet dsContact;

        try
        {
            //if (Session["contactDs"] == null || ((DataSet)Session["contactDs"]).Tables[0].Rows.Count < 1)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter atleast one Lead Contact and try again.')", true);
            //    return;
            //}

            if (Page.IsValid)
            {
                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                GridViewRow row = GrdViewLead.SelectedRow;

                int BillNo = Convert.ToInt32(GrdViewLead.SelectedDataKey.Value);

                string Username = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);


                string Customer = string.Empty;
                Customer = cmbCustomer.SelectedItem.Text;

                //if (bl.IsCustomerFoundInHire(connection, Customer))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Already done a Hire Purchase for this Customer');", true);
                //    return;
                //}


                if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
                {
                    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                    {
                        TextBox txt1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtChequeNo");
                        TextBox txt2 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDueDate");
                        TextBox txt3 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtAmount");
                        TextBox txt32 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtRemarks");

                        if (txt1.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Cheque No');", true);
                            return;
                        }
                        else if (txt2.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Due Date');", true);
                            return;
                        }
                        else if (txt3.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Amount');", true);
                            return;
                        }
                        else if (txt32.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Narration');", true);
                            return;
                        }
                    }
                }



                DataSet dstest = new DataSet();
                string Chequeno1 = "";
                if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
                {
                    DataTable dtt;
                    DataRow drNewt;

                    DataColumn dct;

                    dtt = new DataTable();

                    dct = new DataColumn("ChequeNo");
                    dtt.Columns.Add(dct);

                    dstest.Tables.Add(dtt);

                    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                    {
                        TextBox txt1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtChequeNo");
                        Chequeno1 = txt1.Text;

                        drNewt = dtt.NewRow();
                        drNewt["ChequeNo"] = Chequeno1;
                        dstest.Tables[0].Rows.Add(drNewt);
                    }


                    int i = 1;
                    int ii = 1;
                    string ChequeNo2 = string.Empty;
                    foreach (DataRow dr in dstest.Tables[0].Rows)
                    {
                        ChequeNo2 = Convert.ToString(dr["ChequeNo"]);

                        if ((ChequeNo2 == null) || (ChequeNo2 == ""))
                        {
                        }
                        else
                        {
                            foreach (DataRow drd in dstest.Tables[0].Rows)
                            {
                                if (ii == i)
                                {
                                }
                                else
                                {
                                    if (ChequeNo2 == Convert.ToString(drd["ChequeNo"]))
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No - " + ChequeNo2 + " - already exists in the grid.');", true);
                                        return;
                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                        i = i + 1;
                        ii = 1;
                    }
                }




                double ccnoinst = Convert.ToDouble(txtnoinst1.Text);
                double cceachpay = Convert.ToDouble(Txteach.Text);
                double Amount123 = 0;
                if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
                {
                    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                    {
                        TextBox txt3 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtAmount");
                        Amount123 = Amount123 + Convert.ToDouble(txt3.Text);
                    }

                    if (Amount123 != (ccnoinst * cceachpay))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Amount not match with total of installment amounts');", true);
                        return;
                    }
                }


                string dstartdate = string.Empty;
                string sBilldate = string.Empty;
                sBilldate = txtBillDate.Text.Trim();

                dstartdate = txtduedate.Text.Trim();
                if (Convert.ToDateTime(dstartdate) < Convert.ToDateTime(sBilldate))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Start due date cannot be less than Bill date');", true);
                    return;
                }

                double dlnAmt = 0;
                double dpurAmt = 0;
                double ddochr = 0;
                double dintamt = 0;
                double dfinpay = 0;
                double dnoinst = 0;
                double dpay = 0;
                double deachpay = 0;
                string txtoth = string.Empty;
               
                string dpaydate = string.Empty;                
                string sCustomerName = string.Empty;
                int sCustomerID = 0;
                string ddob = string.Empty;
                string dmobile = string.Empty;
                int bankid = 0;
                int paymode = 0;
                string AccountNumber = "";
                string IFSCCode = "";
                string BranchName = "";
                string Dayofpayment = "";
               
                dlnAmt = Convert.ToDouble(Txtlnamt.Text);
                sCustomerName = cmbCustomer.SelectedItem.Text;
                sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                dpurAmt = Convert.ToDouble(txtpuramt.Text);
                ddochr = Convert.ToDouble(txtdocchr.Text);
                dintamt = Convert.ToDouble(txtintamt.Text);
                dfinpay = Convert.ToDouble(txtfinpay.Text);
                dnoinst = Convert.ToDouble(txtnoinst1.Text);
                txtoth = txtothers.Text;
                dpay = Convert.ToDouble(txtinipay.Text);
                dpaydate = txtdatepay.Text.Trim();
                deachpay = Convert.ToDouble(Txteach.Text);
                ddob = txtdob.Text.Trim();
                dmobile = txtMobile.Text.Trim();


                if (dpurAmt == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cost amount cannot be Zero value');", true);
                    return;
                }
                if (dintamt == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Interest amount cannot be Zero value');", true);
                    return;
                }
                if (dfinpay == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Amount to collect cannot be Zero value');", true);
                    return;
                }
                if (dnoinst == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Of Installment cannot be Zero value');", true);
                    return;
                }

                string BillNoNew =string.Empty;
                string BranchRefNo = string.Empty;

                paymode = Convert.ToInt32(drpPaymode.SelectedItem.Value);

                if (drpPaymode.SelectedValue == "3")
                {
                    bankid = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    BranchName = txtBranchName.Text;
                    Dayofpayment = txtpaydate.Text.Trim();
                    IFSCCode = txtIFSCCode.Text;
                    AccountNumber = txtAccountNumber.Text;
                }
                else if (drpPaymode.SelectedValue == "2")
                {
                    bankid = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    Dayofpayment = txtduedate.Text.Trim();
                    IFSCCode = "0";
                    AccountNumber = "0";
                    BranchName = "";
                }
                else
                {
                    bankid = 0;
                    Dayofpayment = txtduedate.Text.Trim();
                    IFSCCode = "0";
                    AccountNumber = "0";
                    BranchName = "";
                }

                BillNoNew = txtbillnonew.Text;
                BranchRefNo = txtbranchrefno.Text;



                int iSlno = 0;
                int iSllno = Convert.ToInt32(lblBillNo.Text);
                double down = Convert.ToDouble(txtdown.Text);
                double down1 = Convert.ToDouble(txtdown1.Text);
                double upfront = Convert.ToDouble(txtupfront.Text);
                double emi = Convert.ToDouble(txtemi.Text);
                double emiper = Convert.ToDouble(txtemiper.Text);

                dsContact = (DataSet)Session["contactDs"];


                DataSet ds;
                ds = new DataSet();

                if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
                {

                    DataTable dt;
                    DataRow drNew;

                    DataColumn dc;

                    dt = new DataTable();

                    dc = new DataColumn("ChequeNo");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("DueDate");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Amount");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Cancelled");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Narration");
                    dt.Columns.Add(dc);

                    ds.Tables.Add(dt);
                    string Cancelled = "";
                    string Chequeno = "";
                    string DueDate = "";
                    double Amount = 0;
                    string Narration = "";

                    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                    {
                        DropDownList txt = (DropDownList)GrdViewItems.Rows[vLoop].FindControl("chkboxCancelled");
                        if (txt.SelectedValue == "N")
                        {
                            Cancelled = "N";
                        }
                        else
                        {
                            Cancelled = "Y";
                        }

                        TextBox txt1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtChequeNo");
                        Chequeno = txt1.Text;

                        TextBox txt2 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDueDate");
                        DueDate = txt2.Text;

                        TextBox txt3 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtAmount");
                        Amount = Convert.ToDouble(txt3.Text);

                        TextBox txt32 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtRemarks");
                        Narration = txt32.Text;

                        drNew = dt.NewRow();
                        drNew["ChequeNo"] = Chequeno;
                        drNew["DueDate"] = DueDate;
                        drNew["Amount"] = Amount;
                        drNew["Cancelled"] = Cancelled;
                        drNew["Narration"] = Narration;
                        ds.Tables[0].Rows.Add(drNew);
                    }
                }
                else
                {
                    ds = null;
                }


                iSlno = bl.UpdateHirePurchase(iSllno, sBilldate, sCustomerID, sCustomerName, dpurAmt, dlnAmt, ddochr, dintamt, dfinpay, dnoinst, txtoth, dpay, dpaydate, dstartdate, deachpay, Username, dsContact, BillNoNew, BranchRefNo, down, down1, emi, emiper, upfront, ddob, dmobile, bankid, BranchName, AccountNumber, Dayofpayment, IFSCCode, paymode, ds);



                string salestype = string.Empty;
                int ScreenNo = 0;
                string ScreenName = string.Empty;


                salestype = "Hire Purchase";
                ScreenName = "Hire Purchase";


                bool mobile1 = false;
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
                                mobile1 = Convert.ToBoolean(dr["mobile"]);
                                Email = Convert.ToBoolean(dr["Email"]);
                                emailsubject = Convert.ToString(dr["emailsubject"]);
                                emailcontent = Convert.ToString(dr["emailcontent"]);

                                if (ScreenType == 1)
                                {
                                    if (dr["Name1"].ToString() == "Customer")
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

                                    int index312 = emailcontent.IndexOf("@User");
                                    body = Username;
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


                                    int index111 = emailcontent.IndexOf("@PayMode");
                                    body = drpPaymode.SelectedItem.Value;
                                    if (index111 >= 0)
                                    {
                                        emailcontent = emailcontent.Remove(index111, 8).Insert(index111, body);
                                    }

                                    int index1 = emailcontent.IndexOf("@Amount");
                                    body = txtpuramt.Text;
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
                                mobile1 = Convert.ToBoolean(dr["mobile"]);
                                smscontent = Convert.ToString(dr["smscontent"]);

                                if (ScreenType == 1)
                                {
                                    if (dr["Name1"].ToString() == "Customer")
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

                                    int index312 = smscontent.IndexOf("@User");
                                    body = Username;
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
                                        smscontent = emailcontent.Remove(index, 9).Insert(index, body);
                                    }


                                    int index111 = smscontent.IndexOf("@PayMode");
                                    body = drpPaymode.SelectedItem.Value;
                                    if (index111 >= 0)
                                    {
                                        smscontent = smscontent.Remove(index111, 8).Insert(index111, body);
                                    }

                                    int index1 = smscontent.IndexOf("@Amount");
                                    body = txtpuramt.Text;
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


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Hire Purchase Details Updated successfully.')", true);

                BindGrid("", "");
                UpdatePanelPage.Update();
                GrdViewLead.DataBind();

                ModalPopupExtender2.Hide();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }
    }

    protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[3].Visible = false;
            //TextBox txt = (TextBox)e.Row.FindControl("txtDate");
            //TextBox txtet = (TextBox)e.Row.FindControl("txtResult");
            //if ((txt.Text != "") || (txtet.Text != ""))
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Blue;
            //}
            //else
            //{
            //    e.Row.ForeColor = System.Drawing.Color.SteelBlue;
            //}
        }
    }

    protected void drpPaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
            {
                TabPanel2.Visible = true;
                TabPanel2.Enabled = true;
                if (drpPaymode.SelectedValue == "2")
                {
                    lllklkl.Visible = true;
                    lllklkll.Visible = false;
                    lllklklll.Visible = false;
                }
                else if (drpPaymode.SelectedValue == "3")
                {
                    lllklkl.Visible = true;
                    lllklkll.Visible = true;
                    lllklklll.Visible = true;
                }
            }
            else
                TabPanel2.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtBillDate_TextChanged(object sender, EventArgs e)
    {
        //if (cmbCustomer.SelectedItem.Value != "0")
        //{
        //    GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
        //    ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
        //    UpdatePanel11.Update();
        //}
    }

    protected void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int iLedgerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
            BusinessLogic bl = new BusinessLogic(sDataSource);

            DataSet customerDs = bl.getAddressInfo(iLedgerID);
            string address = string.Empty;

            if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            {
                if (customerDs.Tables[0].Rows[0]["Add1"] != null)
                    txtMobile.Text = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadBanks()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBankLedgerpaymnet();
        drpBankName.DataSource = ds;
        drpBankName.DataTextField = "LedgerName";
        drpBankName.DataValueField = "LedgerID";
        drpBankName.DataBind();

    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        try
        { 
        DateTime creationDate;
        int LeadID = 0;
        string prospectCustomer = string.Empty;
        string address = string.Empty;
        string mobile = string.Empty;
        string landline = string.Empty;
        string email = string.Empty;
        string modeOfContact = string.Empty;
        string personalResponsible = string.Empty;
        string businessType = string.Empty;
        string branch = string.Empty;
        string status = string.Empty;
        string LastCompletedAction = string.Empty;
        //string creationDate = string.Empty;
        string nextAction = string.Empty;
        string category = string.Empty;
        DataSet dsContact;

        string info1 = string.Empty;
        string info2 = string.Empty;
        string info3 = string.Empty;

        string info4 = string.Empty;
        string info5 = string.Empty;


            //if (Session["contactDs"] == null || ((DataSet)Session["contactDs"]).Tables[0].Rows.Count < 1)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter atleast one Lead Contact and try again.')", true);
            //    return;
            //}

            

            if (Page.IsValid)
            {
                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                int dcatid = 0;
                string AccountNo = string.Empty;
                int dopening = 0;
                string ditemCode = string.Empty;
                string dProductName = string.Empty;
                string dProductDesc = string.Empty;
                string dmodel = string.Empty;

                string Username = Request.Cookies["LoggedUserName"].Value;

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string Customer = string.Empty;
                Customer = cmbCustomer.SelectedItem.Text;

                if (bl.IsCustomerFoundInHire(connection, Customer))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Already done a Hire Purchase for this Customer');", true);
                    return;
                }

                string billnonew = string.Empty;
                string branchrefno = string.Empty;
                billnonew = txtbillnonew.Text;

                branchrefno = txtbranchrefno.Text;

                if (bl.IsBillFoundInHire(connection, billnonew, "Billno"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bill No already found in Hire Purchase');", true);
                    return;
                }

                if (bl.IsBillFoundInHire(connection, branchrefno, "branchref"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Branch Ref No already found in Hire Purchase');", true);
                    return;
                }


                if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
                {
                    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                    {
                        TextBox txt1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtChequeNo");
                        TextBox txt2 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDueDate");
                        TextBox txt3 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtAmount");
                        TextBox txt32 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtRemarks");

                        if (txt1.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Cheque No');", true);
                            return;
                        }
                        else if (txt2.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Due Date');", true);
                            return;
                        }
                        else if (txt3.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Amount');", true);
                            return;
                        }
                        else if (txt32.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Narration');", true);
                            return;
                        }
                    }
                }



                DataSet dstest = new DataSet();
                string Chequeno1 = "";
                if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
                {
                    DataTable dtt;
                    DataRow drNewt;

                    DataColumn dct;
                    
                    dtt = new DataTable();

                    dct = new DataColumn("ChequeNo");
                    dtt.Columns.Add(dct);

                    dstest.Tables.Add(dtt);

                    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                    {
                        TextBox txt1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtChequeNo");
                        Chequeno1 = txt1.Text;

                        drNewt = dtt.NewRow();
                        drNewt["ChequeNo"] = Chequeno1;
                        dstest.Tables[0].Rows.Add(drNewt);
                    }


                    int i = 1;
                    int ii = 1;
                    string ChequeNo2 = string.Empty;
                    foreach (DataRow dr in dstest.Tables[0].Rows)
                    {
                        ChequeNo2 = Convert.ToString(dr["ChequeNo"]);

                        if ((ChequeNo2 == null) || (ChequeNo2 == ""))
                        {
                        }
                        else
                        {
                            foreach (DataRow drd in dstest.Tables[0].Rows)
                            {
                                if (ii == i)
                                {
                                }
                                else
                                {
                                    if (ChequeNo2 == Convert.ToString(drd["ChequeNo"]))
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No - " + ChequeNo2 + " - already exists in the grid.');", true);
                                        return;
                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                        i = i + 1;
                        ii = 1;
                    }
                }




                double ccnoinst = Convert.ToDouble(txtnoinst1.Text);
                double cceachpay = Convert.ToDouble(Txteach.Text);
                double Amount123 = 0;
                if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
                {
                    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                    {
                        TextBox txt3 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtAmount");
                        Amount123 = Amount123 + Convert.ToDouble(txt3.Text);
                    }

                    if (Amount123 != (ccnoinst * cceachpay))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Amount not match with total of installment amounts');", true);
                        return;
                    }
                }
                



                string dstartdate = string.Empty;
                string sBilldate = string.Empty;
                sBilldate = txtBillDate.Text.Trim();

                dstartdate = txtduedate.Text.Trim();
                if (Convert.ToDateTime(dstartdate) < Convert.ToDateTime(sBilldate))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Start due date cannot be less than Bill date');", true);
                    return;
                }

                double dlnAmt = 0;
                double dpurAmt = 0;
                double ddochr = 0;
                double dintamt = 0;
                double dfinpay = 0;
                double dnoinst = 0;
                double dpay = 0;
                double deachpay = 0;
                string txtoth = string.Empty;
               
                string dpaydate = string.Empty;                
                string sCustomerName = string.Empty;
                int sCustomerID = 0;
                string ddob = string.Empty;
                string dmobile = string.Empty;
                int bankid = 0;
                int paymode = 0;
                string AccountNumber = "";
                string IFSCCode = "";
                string BranchName = "";
                string Dayofpayment = "";
                
                dlnAmt = Convert.ToDouble(Txtlnamt.Text);
                sCustomerName = cmbCustomer.SelectedItem.Text;
                sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                dpurAmt = Convert.ToDouble(txtpuramt.Text);
                ddochr = Convert.ToDouble(txtdocchr.Text);
                dintamt = Convert.ToDouble(txtintamt.Text);
                dfinpay = Convert.ToDouble(txtfinpay.Text);
                dnoinst = Convert.ToDouble(txtnoinst1.Text);
                txtoth = txtothers.Text;
                dpay = Convert.ToDouble(txtinipay.Text);
                dpaydate = txtdatepay.Text.Trim();                
                ddob = txtdob.Text.Trim();
                dmobile = txtMobile.Text.Trim();
                deachpay = Convert.ToDouble(Txteach.Text);
                
                paymode = Convert.ToInt32(drpPaymode.SelectedItem.Value);




                if (dpurAmt == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cost amount cannot be Zero value');", true);
                    return;
                }
                if (dintamt == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Interest amount cannot be Zero value');", true);
                    return;
                }
                if (dfinpay == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Amount to collect cannot be Zero value');", true);
                    return;
                }
                if (dnoinst == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Of Installment cannot be Zero value');", true);
                    return;
                }

                if (drpPaymode.SelectedValue == "3")
                {
                    bankid = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    BranchName = txtBranchName.Text;
                    Dayofpayment = txtpaydate.Text.Trim();
                    IFSCCode = txtIFSCCode.Text;
                    AccountNumber = txtAccountNumber.Text;
                }
                else if (drpPaymode.SelectedValue == "2")
                {
                    bankid = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    Dayofpayment = txtduedate.Text.Trim();
                    IFSCCode = "0";
                    AccountNumber = "0";
                    BranchName="";
                }
                else
                {
                    bankid = 0;
                    Dayofpayment = txtduedate.Text.Trim();
                    IFSCCode = "0";
                    AccountNumber = "0";
                    BranchName = "";
                }

                

                dsContact = (DataSet)Session["contactDs"];

                string connStr = GetConnectionString();

                double down = Convert.ToDouble(txtdown.Text);
                double down1 = Convert.ToDouble(txtdown1.Text);
                double upfront = Convert.ToDouble(txtupfront.Text);
                double emi = Convert.ToDouble(txtemi.Text);
                double emiper = Convert.ToDouble(txtemiper.Text);

                //bl.AddUpdateLeadMaster(LeadID, creationDate, prospectCustomer, address, mobile, landline, email, modeOfContact, personalResponsible, businessType, branch, status, LastCompletedAction, nextAction, category, dsContact, info1, info2, info3, info4, info5);

                DataSet ds;
                ds = new DataSet();

                if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
                {

                    DataTable dt;
                    DataRow drNew;

                    DataColumn dc;

                    dt = new DataTable();

                    dc = new DataColumn("ChequeNo");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("DueDate");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Amount");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Cancelled");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Narration");
                    dt.Columns.Add(dc);

                    ds.Tables.Add(dt);
                    string Cancelled = "";
                    string Chequeno = "";
                    string DueDate = "";
                    double Amount = 0;
                    string Narration = "";

                    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                    {
                        DropDownList txt = (DropDownList)GrdViewItems.Rows[vLoop].FindControl("chkboxCancelled");
                        if (txt.SelectedValue == "N")
                        {
                            Cancelled = "N";
                        }
                        else
                        {
                            Cancelled = "Y";
                        }

                        TextBox txt1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtChequeNo");
                        Chequeno = txt1.Text;

                        TextBox txt2 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDueDate");
                        DueDate = txt2.Text;

                        TextBox txt3 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtAmount");
                        Amount = Convert.ToDouble(txt3.Text);

                        TextBox txt32 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtRemarks");
                        Narration = txt32.Text;

                        drNew = dt.NewRow();
                        drNew["ChequeNo"] = Chequeno;
                        drNew["DueDate"] = DueDate;
                        drNew["Amount"] = Amount;
                        drNew["Cancelled"] = Cancelled;
                        drNew["Narration"] = Narration;
                        ds.Tables[0].Rows.Add(drNew);
                    }
                }
                else
                {
                    ds = null;
                }

                int billNo = bl.InsertHirePurchase(sBilldate, sCustomerID, sCustomerName, dpurAmt, dlnAmt, ddochr, dintamt, dfinpay, dnoinst, txtoth, dpay, dpaydate, dstartdate, deachpay, Username, dsContact, billnonew, branchrefno, down, down1, emi, emiper, upfront,ddob,dmobile,bankid,BranchName,AccountNumber,Dayofpayment,IFSCCode,paymode, ds);

                GrdViewLead.DataBind();


                string salestype = string.Empty;
                int ScreenNo = 0;
                string ScreenName = string.Empty;


                salestype = "Hire Purchase";
                ScreenName = "Hire Purchase";
                

                bool mobile1 = false;
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
                                mobile1 = Convert.ToBoolean(dr["mobile"]);
                                Email = Convert.ToBoolean(dr["Email"]);
                                emailsubject = Convert.ToString(dr["emailsubject"]);
                                emailcontent = Convert.ToString(dr["emailcontent"]);

                                if (ScreenType == 1)
                                {
                                    if (dr["Name1"].ToString() == "Customer")
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

                                    int index312 = emailcontent.IndexOf("@User");
                                    body = Username;
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


                                    int index111 = emailcontent.IndexOf("@PayMode");
                                    body = drpPaymode.SelectedItem.Value;
                                    if (index111 >= 0)
                                    {
                                        emailcontent = emailcontent.Remove(index111, 8).Insert(index111, body);
                                    }

                                    int index1 = emailcontent.IndexOf("@Amount");
                                    body = txtpuramt.Text;
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
                                mobile1 = Convert.ToBoolean(dr["mobile"]);
                                smscontent = Convert.ToString(dr["smscontent"]);

                                if (ScreenType == 1)
                                {
                                    if (dr["Name1"].ToString() == "Customer")
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

                                    int index312 = smscontent.IndexOf("@User");
                                    body = Username;
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
                                        smscontent = emailcontent.Remove(index, 9).Insert(index, body);
                                    }


                                    int index111 = smscontent.IndexOf("@PayMode");
                                    body = drpPaymode.SelectedItem.Value;
                                    if (index111 >= 0)
                                    {
                                        smscontent = smscontent.Remove(index111, 8).Insert(index111, body);
                                    }

                                    int index1 = smscontent.IndexOf("@Amount");
                                    body = txtpuramt.Text;
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


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Hire Purchase Details saved successfully.')", true);

                BindGrid("", "");
                UpdatePanelPage.Update();


                ModalPopupExtender2.Hide();

                
                //return;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }

    }
}