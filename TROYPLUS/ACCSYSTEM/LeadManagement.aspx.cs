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

public partial class LeadManagement : System.Web.UI.Page
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
                myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

                BindDropdownList();
                Session["contactDs"] = null;

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
                BindGrid("", "");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ComboBox2.SelectedValue == "2")
            {
                rowcall.Visible = true;
                ModalPopupContact.Show();
            }
            else
            {
                rowcall.Visible = false;
                ModalPopupContact.Show();
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

        ds = bl.ListLeadMaster(connection, textSearch, dropDown);

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
        LeadBusinessLogic bl = new LeadBusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        
        ds = bl.GetDropdownList(sDataSource, "CONTACT");
        cmbModeOfContact.DataSource = ds;
        cmbModeOfContact.DataBind();
        cmbModeOfContact.DataTextField = "TextValue";
        cmbModeOfContact.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "PERRES");
        cmbPersonalResp.DataSource = ds;
        cmbPersonalResp.DataBind();
        cmbPersonalResp.DataTextField = "TextValue";
        cmbPersonalResp.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "BUSTYPE");
        cmbBussType.DataSource = ds;
        cmbBussType.DataBind();
        cmbBussType.DataTextField = "TextValue";
        cmbBussType.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "BRNCH");
        cmbBranch.DataSource = ds;
        cmbBranch.DataBind();
        cmbBranch.DataTextField = "TextValue";
        cmbBranch.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "LSTCMP");
        cmbLastCompAction.DataSource = ds;
        cmbLastCompAction.DataBind();
        cmbLastCompAction.DataTextField = "TextValue";
        cmbLastCompAction.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "LSTCMP");
        cmblastaction.DataSource = ds;
        cmblastaction.DataBind();
        cmblastaction.DataTextField = "TextValue";
        cmblastaction.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "NXTAXN");
        cmbNextAction.DataSource = ds;
        cmbNextAction.DataBind();
        cmbNextAction.DataTextField = "TextValue";
        cmbNextAction.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "NXTAXN");
        cmbnxtaction.DataSource = ds;
        cmbnxtaction.DataBind();
        cmbnxtaction.DataTextField = "TextValue";
        cmbnxtaction.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "CATEGRY");
        cmbCategory.DataSource = ds;
        cmbCategory.DataBind();
        cmbCategory.DataTextField = "TextValue";
        cmbCategory.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "STATUS");
        cmbStatus.DataSource = ds;
        cmbStatus.DataBind();
        cmbStatus.DataTextField = "TextValue";
        cmbStatus.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "STATUS");
        cmbnewstatus.DataSource = ds;
        cmbnewstatus.DataBind();
        cmbnewstatus.DataTextField = "TextValue";
        cmbnewstatus.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "INFO3");
        ddlinfo3.DataSource = ds;
        ddlinfo3.DataBind();
        ddlinfo3.DataTextField = "TextValue";
        ddlinfo3.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "INFO4");
        ddlinfo4.DataSource = ds;
        ddlinfo4.DataBind();
        ddlinfo4.DataTextField = "TextValue";
        ddlinfo4.DataValueField = "TextValue";

        ds = bl.GetDropdownList(sDataSource, "INFO5");
        ddlinfo5.DataSource = ds;
        ddlinfo5.DataBind();
        ddlinfo5.DataTextField = "TextValue";
        ddlinfo5.DataValueField = "TextValue";

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
            Session["LeadID"] = "0";
            Session["contactDs"] = null;
            ShowLeadContactInfo();
            //txtCreationDate.Text = DateTime.Now.ToShortDateString();

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtCreationDate.Text = dtaa;

            txtCreationDate.Focus();

            txtLeadNo.Text = "- TBA -";
            DropDownList1.SelectedItem.Text = "NO";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void Reset()
    {
        //txtLeadNo.Text = "";
        txtAddress.Text = "";
        txtProspCustName.Text = "";
        txtMobile.Text = "";
        txtEmail.Text = "";
        txtLandline.Text = "";
        cmbModeOfContact.SelectedIndex = 0;
        cmbPersonalResp.SelectedIndex = 0;
        cmbBussType.SelectedIndex = 0;
        cmbBranch.SelectedIndex = 0;
        cmbStatus.SelectedIndex = 0;
        cmbLastCompAction.SelectedIndex = 0;
        cmbNextAction.SelectedIndex = 0;
        cmbCategory.SelectedIndex = 0;

        txtInfo1.Text = "";
        txtInfo2.Text = "";
        ddlinfo3.SelectedIndex = 0;

        ddlinfo4.SelectedIndex = 0;
        ddlinfo5.SelectedIndex = 0;

        DropDownList1.SelectedValue = "1";
        TextBox1.Text = "";
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
                GrdViewLeadContact.DataSource = ds.Tables[0];
                GrdViewLeadContact.DataBind();
            }

        }
        else
        {
            GrdViewLeadContact.DataSource = null;
            GrdViewLeadContact.DataBind();
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

                dc = new DataColumn("CallBackFlag");
                dt.Columns.Add(dc);

                dc = new DataColumn("CallBackDate");
                dt.Columns.Add(dc);

                ds.Tables.Add(dt);

                drNew = dt.NewRow();

                drNew["ContactRefID"] = 1;
                drNew["ContactedDate"] = txtContactedDate.Text;
                drNew["ContactSummary"] = txtContactSummary.Text;
                drNew["CallBackFlag"] = ComboBox2.SelectedItem.Text;
                DropDownList1.SelectedItem.Text = ComboBox2.SelectedItem.Text;

                if (cmbnewstatus.SelectedValue == "0")
                {

                }
                else
                {
                    cmbStatus.SelectedValue = cmbnewstatus.SelectedValue;
                }
                if (cmblastaction.SelectedValue == "0")
                {

                }
                else
                {
                    cmbLastCompAction.SelectedValue = cmblastaction.SelectedValue;
                }
                if (cmbnxtaction.SelectedValue == "0")
                {

                }
                else
                {
                    cmbNextAction.SelectedValue = cmbnxtaction.SelectedValue;
                }

                if (ComboBox2.SelectedItem.Text == "NO")
                {
                    drNew["CallBackDate"] = "";
                    TextBox1.Text = "";
                    DropDownList1.SelectedValue = "1";
                }
                else
                {
                    drNew["CallBackDate"] = txtcallback.Text;
                    TextBox1.Text = txtcallback.Text;
                    DropDownList1.SelectedValue = "2";
                }
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
                drNew["ContactedDate"] = txtContactedDate.Text;
                drNew["ContactSummary"] = txtContactSummary.Text;
                drNew["CallBackFlag"] = ComboBox2.SelectedItem.Text;
                DropDownList1.SelectedItem.Text = ComboBox2.SelectedItem.Text;

                if (cmbnewstatus.SelectedValue == "0")
                {

                }
                else
                {
                    cmbStatus.SelectedValue = cmbnewstatus.SelectedValue;
                }
                if (cmblastaction.SelectedValue == "0")
                {

                }
                else
                {
                    cmbLastCompAction.SelectedValue = cmblastaction.SelectedValue;
                }
                if (cmbnxtaction.SelectedValue == "0")
                {

                }
                else
                {
                    cmbNextAction.SelectedValue = cmbnxtaction.SelectedValue;
                }

                if (ComboBox2.SelectedItem.Text == "NO")
                {
                    drNew["CallBackDate"] = "";
                    TextBox1.Text = "";
                    DropDownList1.SelectedValue = "1";
                }
                else
                {
                    drNew["CallBackDate"] = txtcallback.Text;
                    TextBox1.Text = txtcallback.Text;
                    DropDownList1.SelectedValue = "2";
                }
                ds.Tables[0].Rows.Add(drNew);
                Session["contactDs"] = ds;
            }


            //this.GrdViewLeadContact.Visible = true;

            GrdViewLeadContact.DataSource = ds.Tables[0];
            GrdViewLeadContact.DataBind();

            ModalPopupContact.Hide();
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
            int currentRow = int.Parse(hdCurrentRow.Value);

            ds.Tables[0].Rows[currentRow]["ContactedDate"] = txtContactedDate.Text;
            ds.Tables[0].Rows[currentRow]["ContactSummary"] = txtContactSummary.Text;
            ds.Tables[0].Rows[currentRow]["CallBackFlag"] = ComboBox2.SelectedItem.Text;
            if (ComboBox2.SelectedItem.Text == "NO")
            {
                ds.Tables[0].Rows[currentRow]["CallBackDate"] = "";
                TextBox1.Text = "";
                DropDownList1.SelectedItem.Text = "NO";
            }
            else
            {
                ds.Tables[0].Rows[currentRow]["CallBackDate"] = txtcallback.Text;
                TextBox1.Text = txtcallback.Text;
                DropDownList1.SelectedItem.Text = "YES";
            }

            if (cmbnewstatus.SelectedValue == "0")
            {

            }
            else
            {
                cmbStatus.SelectedValue = cmbnewstatus.SelectedValue;
            }
            if (cmblastaction.SelectedValue == "0")
            {

            }
            else
            {
                cmbLastCompAction.SelectedValue = cmblastaction.SelectedValue;
            }
            if (cmbnxtaction.SelectedValue == "0")
            {

            }
            else
            {
                cmbNextAction.SelectedValue = cmbnxtaction.SelectedValue;
            }

            ds.Tables[0].Rows[currentRow].EndEdit();
            ds.Tables[0].Rows[currentRow].AcceptChanges();

            GrdViewLeadContact.DataSource = ds.Tables[0];
            GrdViewLeadContact.DataBind();
            ModalPopupContact.Hide();
            Session["contactDs"] = ds;
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
            cmdSaveContact.Visible = true;
            cmdUpdateContact.Visible = false;
            updatePnlContact.Update();

            txtContactedDate.Text = string.Empty;
            txtContactSummary.Text = string.Empty;
            ComboBox2.SelectedValue = "1";
            txtcallback.Text = string.Empty;
            rowcall.Visible = false;

            ModalPopupContact.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLeadContact_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            DataSet ds = new DataSet();
            GridViewRow row = GrdViewLeadContact.SelectedRow;

            hdCurrentRow.Value = Convert.ToString(row.DataItemIndex);

            txtContactedDate.Text = row.Cells[1].Text;
            txtContactSummary.Text = row.Cells[2].Text;
            if (row.Cells[3].Text == "NO")
            {
                ComboBox2.SelectedValue = "1";
                rowcall.Visible = false;
            }
            else
            {
                ComboBox2.SelectedValue = "2";
                rowcall.Visible = true;
            }

            if (row.Cells[4].Text == "&nbsp;")
            {
                txtcallback.Text = "";
            }
            else
            {
                txtcallback.Text = row.Cells[4].Text;
            }
            cmdSaveContact.Visible = false;
            cmdUpdateContact.Visible = true;
            ModalPopupContact.Show();
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
                ds.Tables[0].Rows[GrdViewLeadContact.Rows[e.RowIndex].DataItemIndex].Delete();
                ds.Tables[0].AcceptChanges();
                GrdViewLeadContact.DataSource = ds;
                GrdViewLeadContact.DataBind();
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
        try
        {
            ModalPopupContact.Hide();
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

                if (bl.CheckUserHaveDelete(usernam, "LDMNGT"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
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

    protected void GrdViewLead_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow row = GrdViewLead.Rows[e.RowIndex];
            string leadID = row.Cells[0].Text;


            string userID = string.Empty;
            userID = Page.User.Identity.Name;
            LeadBusinessLogic bl = new LeadBusinessLogic(GetConnectionString());
            bl.DeleteLeadMaster(leadID, userID);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Lead Management Details Deleted Successfully')", true);
            BindGrid("", "");
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
            var leadID = GrdViewLead.SelectedDataKey.Value.ToString();
            LeadBusinessLogic bl = new LeadBusinessLogic(GetConnectionString());

            DataSet dsDetails = bl.GetLeadMasterDetails(leadID);

            if (dsDetails != null && dsDetails.Tables[0].Rows.Count > 0)
            {
                txtLeadNo.Text = dsDetails.Tables[0].Rows[0]["LeadID"].ToString();
                //txtCreationDate.Text = DateTime.Parse(dsDetails.Tables[0].Rows[0]["CreationDate"].ToString()).ToShortDateString();
                txtCreationDate.Text = Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["CreationDate"]).ToString("dd/MM/yyyy");
                txtProspCustName.Text = dsDetails.Tables[0].Rows[0]["ProspectCustName"].ToString();
                txtMobile.Text = dsDetails.Tables[0].Rows[0]["Mobile"].ToString();
                txtAddress.Text = dsDetails.Tables[0].Rows[0]["Address"].ToString();
                txtEmail.Text = dsDetails.Tables[0].Rows[0]["Email"].ToString();
                txtLandline.Text = dsDetails.Tables[0].Rows[0]["Landline"].ToString();

                if (cmbModeOfContact.Items.FindByValue(dsDetails.Tables[0].Rows[0]["ModeOfContact"].ToString().Trim()) != null)
                    cmbModeOfContact.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["ModeOfContact"]);
                if (cmbPersonalResp.Items.FindByValue(dsDetails.Tables[0].Rows[0]["PersonalResponsible"].ToString().Trim()) != null)
                    cmbPersonalResp.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["PersonalResponsible"]);
                if (cmbBussType.Items.FindByValue(dsDetails.Tables[0].Rows[0]["BusinessType"].ToString().Trim()) != null)
                    cmbBussType.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["BusinessType"]);
                if (cmbBranch.Items.FindByValue(dsDetails.Tables[0].Rows[0]["Branch"].ToString().Trim()) != null)
                    cmbBranch.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["Branch"]);
                if (cmbStatus.Items.FindByValue(dsDetails.Tables[0].Rows[0]["Status"].ToString().Trim()) != null)
                    cmbStatus.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["Status"]);
                if (cmbLastCompAction.Items.FindByValue(dsDetails.Tables[0].Rows[0]["LastCompletedAction"].ToString().Trim()) != null)
                    cmbLastCompAction.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["LastCompletedAction"]);
                if (cmbNextAction.Items.FindByValue(dsDetails.Tables[0].Rows[0]["NextAction"].ToString().Trim()) != null)
                    cmbNextAction.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["NextAction"]);
                if (cmbCategory.Items.FindByValue(dsDetails.Tables[0].Rows[0]["Category"].ToString().Trim()) != null)
                    cmbCategory.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["Category"]);


                if (cmblastaction.Items.FindByValue(dsDetails.Tables[0].Rows[0]["LastCompletedAction"].ToString().Trim()) != null)
                    cmblastaction.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["LastCompletedAction"]);
                if (cmbnxtaction.Items.FindByValue(dsDetails.Tables[0].Rows[0]["NextAction"].ToString().Trim()) != null)
                    cmbnxtaction.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["NextAction"]);
                if (cmbnewstatus.Items.FindByValue(dsDetails.Tables[0].Rows[0]["Status"].ToString().Trim()) != null)
                    cmbnewstatus.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["Status"]);


                txtInfo1.Text = dsDetails.Tables[0].Rows[0]["Information1"].ToString();
                txtInfo2.Text = dsDetails.Tables[0].Rows[0]["Information2"].ToString();
                if (ddlinfo3.Items.FindByValue(dsDetails.Tables[0].Rows[0]["Information3"].ToString().Trim()) != null)
                    ddlinfo3.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["Information3"]);

                if (ddlinfo4.Items.FindByValue(dsDetails.Tables[0].Rows[0]["Information4"].ToString().Trim()) != null)
                    ddlinfo4.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["Information4"]);
                if (ddlinfo5.Items.FindByValue(dsDetails.Tables[0].Rows[0]["Information5"].ToString().Trim()) != null)
                    ddlinfo5.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["Information5"]);


                //if (DropDownList1.Items.FindByValue(dsDetails.Tables[0].Rows[0]["callbackflag"].ToString().Trim()) != null)

                string tt = Convert.ToString(dsDetails.Tables[0].Rows[0]["callbackflag"]);
                if (tt == "NO")
                {
                    DropDownList1.SelectedItem.Text = "NO";
                    //DropDownList1.SelectedValue = Convert.ToString(dsDetails.Tables[0].Rows[0]["callbackflag"]);
                }
                else
                {
                    DropDownList1.SelectedItem.Text = "YES";
                }

                TextBox1.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["Callbackdate"]);

                dsDetails = bl.GetLeadContacts(leadID);

                if (dsDetails != null && dsDetails.Tables[0].Rows.Count > 0)
                {
                    GrdViewLeadContact.DataSource = dsDetails.Tables[0];
                    GrdViewLeadContact.DataBind();
                    Session["contactDs"] = dsDetails;
                }

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

        string userID = string.Empty;

        string info1 = string.Empty;
        string info2 = string.Empty;
        string info3 = string.Empty;

        string info4 = string.Empty;
        string info5 = string.Empty;

        DataSet dsContact;

        try
        {

            if (Session["contactDs"] == null || ((DataSet)Session["contactDs"]).Tables[0].Rows.Count < 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter atleast one Lead Contact and try again.')", true);
                return;
            }

            if (Page.IsValid)
            {
                if (txtLeadNo.Text != string.Empty)
                    LeadID = int.Parse(txtLeadNo.Text);

                creationDate = DateTime.Parse(txtCreationDate.Text);
                //creationDate = txtCreationDate.Text.Trim();
                prospectCustomer = txtProspCustName.Text;
                address = txtAddress.Text;
                mobile = txtMobile.Text;
                landline = txtLandline.Text;
                email = txtEmail.Text;
                modeOfContact = cmbModeOfContact.SelectedValue;
                personalResponsible = cmbPersonalResp.Text;
                businessType = cmbBussType.SelectedValue;
                branch = cmbBranch.SelectedValue;
                if (cmbnewstatus.SelectedValue == "0")
                {
                    status = cmbStatus.SelectedValue;
                }
                else
                {
                    status = cmbnewstatus.SelectedValue;
                }
                if (cmblastaction.SelectedValue == "0")
                {
                    LastCompletedAction = cmbLastCompAction.SelectedValue;
                }
                else
                {
                    LastCompletedAction = cmblastaction.SelectedValue;
                }
                if (cmbnxtaction.SelectedValue == "0")
                {
                    nextAction = cmbNextAction.SelectedValue;
                }
                else
                {
                    nextAction = cmbnxtaction.SelectedValue;
                }
                category = cmbCategory.SelectedValue;

                dsContact = (DataSet)Session["contactDs"];

                string connStr = GetConnectionString();


                info1 = txtInfo1.Text;
                info2 = txtInfo2.Text;
                info3 = ddlinfo3.SelectedValue;
                info4 = ddlinfo4.SelectedValue;
                info5 = ddlinfo5.SelectedValue;

                string callbackflag = DropDownList1.SelectedItem.Text;
                string callbackdate = TextBox1.Text;

                userID = Page.User.Identity.Name;

                LeadBusinessLogic bl = new LeadBusinessLogic(connStr);



                bl.UpdateLeadMaster(LeadID, creationDate, prospectCustomer, address, mobile, landline, email, modeOfContact, personalResponsible, businessType, branch, status, LastCompletedAction, nextAction, category, dsContact, userID, info1, info2, info3, info4, info5, callbackflag, callbackdate);
                //bl.UpdateLeadMaster(LeadID, creationDate, prospectCustomer, address, mobile, landline, email, modeOfContact, personalResponsible, businessType, branch, status, LastCompletedAction, nextAction, category, dsContact, userID, info1, info2, info3, info4, info5);

                //GrdViewLead.DataBind();
                //System.Threading.Thread.Sleep(1000);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Lead Details Updated successfully.')", true);

                //UpdatePanelPage.Update();
                BindGrid("", "");

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

        try
        {

            if (Session["contactDs"] == null || ((DataSet)Session["contactDs"]).Tables[0].Rows.Count < 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter atleast one Lead Contact and try again.')", true);
                return;
            }

            if (Page.IsValid)
            {
                //if(txtLeadNo.Text != string.Empty)
                    //LeadID = int.Parse(txtLeadNo.Text);

                creationDate = DateTime.Parse(txtCreationDate.Text);
                //creationDate = txtCreationDate.Text.Trim();
                prospectCustomer = txtProspCustName.Text;
                address = txtAddress.Text;
                mobile = txtMobile.Text;
                landline = txtLandline.Text;
                email = txtEmail.Text;
                modeOfContact = cmbModeOfContact.SelectedValue;
                personalResponsible = cmbPersonalResp.Text;
                businessType = cmbBussType.SelectedValue;
                branch = cmbBranch.SelectedValue;
                if (cmbnewstatus.SelectedValue == "0")
                {
                    status = cmbStatus.SelectedValue;
                }
                else
                {
                    status = cmbnewstatus.SelectedValue;
                }
                if (cmblastaction.SelectedValue == "0")
                {
                    LastCompletedAction = cmbLastCompAction.SelectedValue;
                }
                else
                {
                    LastCompletedAction = cmblastaction.SelectedValue;
                }
                if (cmbnxtaction.SelectedValue == "0")
                {
                    nextAction = cmbNextAction.SelectedValue;
                }
                else
                {
                    nextAction = cmbnxtaction.SelectedValue;
                }
                category = cmbCategory.SelectedValue;

                info1 = txtInfo1.Text;
                info2 = txtInfo2.Text;

                info3 = ddlinfo3.SelectedValue;
                info4 = ddlinfo4.SelectedValue;
                info5 = ddlinfo5.SelectedValue;

                string callbackflag = DropDownList1.SelectedItem.Text;
                string callbackdate = TextBox1.Text;

                dsContact = (DataSet)Session["contactDs"];

                string connStr = GetConnectionString();

                LeadBusinessLogic bl = new LeadBusinessLogic(connStr);

                //bl.AddUpdateLeadMaster(LeadID, creationDate, prospectCustomer, address, mobile, landline, email, modeOfContact, personalResponsible, businessType, branch, status, LastCompletedAction, nextAction, category, dsContact, info1, info2, info3, info4, info5);
                bl.AddUpdateLeadMaster(LeadID, creationDate, prospectCustomer, address, mobile, landline, email, modeOfContact, personalResponsible, businessType, branch, status, LastCompletedAction, nextAction, category, dsContact, info1, info2, info3, info4, info5, callbackflag, callbackdate);


                GrdViewLead.DataBind();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Lead Details saved successfully.')", true);

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