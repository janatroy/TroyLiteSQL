using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeRole_HRAdminRoleList : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        EmpRoleSummaryGridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        EmpRoleSummaryGridSource.SelectParameters.Add(new ControlParameter("txtSearchInput", TypeCode.String, txtSearchInput.UniqueID, "Text"));
        EmpRoleSummaryGridSource.SelectParameters.Add(new ControlParameter("searchCriteria", TypeCode.String, ddlSearchCriteria.UniqueID, "SelectedValue"));
        EmpRoleSummaryGridSource.SelectParameters.Add(new CookieParameter("UserId", "LoggedUserName"));

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        String url = Request.ServerVariables["URL"];
        url = url.Remove(0, url.LastIndexOf("/") + 1);

        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        if (!Page.IsPostBack)
        {
            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");


            string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                lnkBtnAddEmpRole.Visible = false;
                //grdViewAttendanceSummary.Columns[7].Visible = false;
                //grdViewAttendanceSummary.Columns[8].Visible = false;
            }
            grdViewEmpRoleSummary.PageSize = 8;

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);

            //if (bl.CheckUserHaveAdd(usernam, "SUPPINFO"))
            //{
            //    lnkBtnAddAttendance.Enabled = false;
            //    lnkBtnAddAttendance.ToolTip = "You are not allowed to make Add New ";
            //}
            //else
            //{
            //    lnkBtnAddAttendance.Enabled = true;
            //    lnkBtnAddAttendance.ToolTip = "Click to Add New ";
            //}

            if (Request.QueryString["myname"] != null)
            {

                string myNam = Request.QueryString["myname"].ToString();
                if (myNam == "NEWSUP")
                {
                    if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
                        return;
                    }
                }
            }
        }
    }

    protected void grdViewEmpRoleSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                frmEmpRoleAdd.Visible = true;
                frmEmpRoleAdd.ChangeMode(FormViewMode.Edit);
                ModalPopupExtender1.Show();

            }
            else if (e.CommandName == "ManageLeave")
            {
                string RoleName = string.Empty;
                int RoleId = 0;

                //FormViewManageLeave.Visible = true;
                //FormViewManageLeave.ChangeMode(FormViewMode.Insert);

                string[] args = e.CommandArgument.ToString().Split(new char[] { ':' });
                if (args.Length == 2)
                {
                    RoleName = args[0];

                    RoleId = Convert.ToInt32(args[1]);
                }

                GetEmployeeLeave(RoleId);

                txtManageLeaveRoleName.Text = RoleName;
                txtManageLeaveRoleID.Value = RoleId.ToString();

                ModalPopupExtender2.Show();
                EmpRoleLeavePopUp.Visible = true;
            }
            else if (e.CommandName == "ManagePay")
            {
                string RoleName = string.Empty;
                int RoleId = 0;

                //FormViewManageLeave.Visible = true;
                //FormViewManageLeave.ChangeMode(FormViewMode.Insert);

                string[] args = e.CommandArgument.ToString().Split(new char[] { ':' });
                if (args.Length == 2)
                {
                    RoleName = args[0];

                    RoleId = Convert.ToInt32(args[1]);
                }

                GetPayComponent(string.Empty);

                GetRolePayComponent(RoleId);

                txtManagePayCompRoleName.Text = RoleName;
                ManagePayRoleID.Value = RoleId.ToString();

                txtfrmDate.Enabled = false;
                btnEditDate.Enabled = false;
                txtDeclaredAmt.Enabled = false;

                txtfrmDate.Text = null;
                txtDeclaredAmt.Text = null;

                ModalPopupExtender3.Show();
                EmployeePayCompPopUp.Visible = true;
            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdViewEmpRoleSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grdViewEmpRoleSummary_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grdViewEmpRoleSummary.SelectedIndex = e.RowIndex;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdViewEmpRoleSummary_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                grdViewEmpRoleSummary.DataBind();
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

    protected void frmEmpRoleAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }

    protected void frmEmpRoleAdd_ItemCreated(object sender, EventArgs e)
    {

    }

    protected void frmEmpRoleAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }

    protected void frmEmpRoleAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                //MyAccordion.Visible = true;
                lnkBtnAddEmpRole.Visible = true;
                frmEmpRoleAdd.Visible = false;
                grdViewEmpRoleSummary.Visible = true;
                System.Threading.Thread.Sleep(1000);
                grdViewEmpRoleSummary.DataBind();
                StringBuilder scriptMsg = new StringBuilder();
                scriptMsg.Append("alert('Employee Role Saved Successfully.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), scriptMsg.ToString(), true);


                if (Request.QueryString["myname"] != null)
                {

                    string myNam = Request.QueryString["myname"].ToString();
                    if (myNam == "NEWSUP")
                    {
                        Response.Redirect("Purchase.aspx?myname=" + "NEWPUR");
                    }
                }
            }
            else
            {
                if (e.Exception != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('Role with this name already exists, Please try with a different name.');");

                    if (e.Exception.InnerException != null)
                    {
                        if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                            (e.Exception.InnerException.Message.IndexOf("Role Name Exists") > -1))
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Exception: " + e.Exception.Message + e.Exception.StackTrace, true);
                    }
                }
                e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmEmpRoleAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                lnkBtnAddEmpRole.Visible = true;
                frmEmpRoleAdd.Visible = false;
                //GrdViewLedger.Visible = true;
                grdViewEmpRoleSummary.Visible = true;
                System.Threading.Thread.Sleep(1000);
                grdViewEmpRoleSummary.DataBind();
                //MyAccordion.Visible = true;
                //GrdViewLedger.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Employee Role Details Updated Successfully.');", true);
            }
            else
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('Employee Role with this name already exists, Please try with a different name.');");

                if (e.Exception.InnerException != null)
                {
                    if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                        (e.Exception.InnerException.Message.IndexOf("Role Name Exists") > -1))
                    {
                        e.ExceptionHandled = true;
                        e.KeepInEditMode = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                        return;
                    }

                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Exception: " + e.Exception.Message + e.Exception.StackTrace, true);
                e.ExceptionHandled = true;
                e.KeepInEditMode = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {

    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {

    }

    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            lnkBtnAddEmpRole.Visible = true;
            frmEmpRoleAdd.Visible = false;
            EmpRoleDetailPopUp.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }

    protected void EmpRoleSummaryGridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (grdViewEmpRoleSummary.SelectedDataKey != null)
                e.InputParameters["ID"] = grdViewEmpRoleSummary.SelectedDataKey.Value;

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmEmpRoleSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setInsertParameters(e);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void frmEmpRoleSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setUpdateParameters(e);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkBtnAddEmpRole_Click(object sender, EventArgs e)
    {
        try
        {
            frmEmpRoleAdd.ChangeMode(FormViewMode.Insert);
            frmEmpRoleAdd.Visible = true;
            EmpRoleDetailPopUp.Visible = true;
            ModalPopupExtender1.Show();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((TextBox)this.frmEmpRoleAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtEmpRoleAdd")).Text != "")
            e.InputParameters["Role_Name"] = ((TextBox)this.frmEmpRoleAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtEmpRoleAdd")).Text;

        if (((TextBox)this.frmEmpRoleAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtRemarksAdd")).Text != "")
            e.InputParameters["Remarks"] = ((TextBox)this.frmEmpRoleAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtRemarksAdd")).Text;

        if (((CheckBox)this.frmEmpRoleAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsActiveAdd")) != null)
            e.InputParameters["Is_Active"] = ((CheckBox)this.frmEmpRoleAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsActiveAdd")).Checked;

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((TextBox)this.frmEmpRoleAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtEmpRoleNameEdit")).Text != "")
            e.InputParameters["Role_Name"] = ((TextBox)this.frmEmpRoleAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtEmpRoleNameEdit")).Text;

        if (((TextBox)this.frmEmpRoleAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtEmpRemarksEdit")).Text != "")
            e.InputParameters["Remarks"] = ((TextBox)this.frmEmpRoleAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtEmpRemarksEdit")).Text;

        if (((CheckBox)this.frmEmpRoleAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsActiveEdit")) != null)
            e.InputParameters["Is_Active"] = ((CheckBox)this.frmEmpRoleAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsActiveEdit")).Checked;

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchInput.Text = "";
            ddlSearchCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void FormViewManageLeave_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }

    protected void FormViewManageLeave_ItemCreated(object sender, EventArgs e)
    {

    }

    protected void FormViewManageLeave_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }

    protected void FormViewManageLeave_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {

    }

    protected void FormViewManageLeave_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {

    }

    protected void frmEmpRoleManageLeaveSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {

    }

    protected void frmEmpRoleManageLeaveSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {

    }

    protected void manageLeaveCancelBtn_Click(object sender, EventArgs e)
    {
        ManageLeaveGridView.DataBind();
    }

    protected void manageLeaveSaveBtn_Click(object sender, EventArgs e)
    {
        try
        {
            int RoleId = Convert.ToInt32(txtManageLeaveRoleID.Value);

            DataTable dt = new DataTable();

            dt.Columns.Add("LeaveType_ID", typeof(int));
            dt.Columns.Add("Role_ID", typeof(int));
            dt.Columns.Add("EffectiveDate", typeof(DateTime));
            dt.Columns.Add("AllowedCount", typeof(int));

            for (int i = 0; i < ManageLeaveGridView.Rows.Count; i++)
            {
                TextBox LeaveEffDate = ManageLeaveGridView.Rows[i].FindControl("txtLeaveEffDate") as TextBox;

                TextBox LeaveCount = ManageLeaveGridView.Rows[i].FindControl("txtLeaveCountAdd") as TextBox;

                Label LeaveTypeID = ManageLeaveGridView.Rows[i].FindControl("txtLeaveTypeID") as Label;

                dt.Rows.Add(LeaveTypeID.Text, RoleId, LeaveEffDate.Text, LeaveCount.Text);
            }

            BusinessLogic bl = new BusinessLogic(sDataSource);

            bl.InsertEmployeeRoleLeave(dt, RoleId);

            StringBuilder scriptMsg = new StringBuilder();
            scriptMsg.Append("alert('Employee Role Leave Saved Successfully.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), scriptMsg.ToString(), true);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(Failed :" + ex.Message.ToString() + "');", true);
        }
    }

    protected void managePayCompAssignbtn_Click(object sender, EventArgs e)
    {

    }

    protected void addSelectedBtn_Click(object sender, EventArgs e)
    {
        //Get Selected Index in Grid
        int isValid = 0;
        string Message = string.Empty;
        try
        {
            if (txtfrmDate.Text == null || txtfrmDate.Text == string.Empty)
            {
                isValid = 1;
                Message += " From Date is mandatory.";
            }

            if (txtDeclaredAmt.Text == null || txtDeclaredAmt.Text == string.Empty)
            {
                isValid = 1;
                Message += " Declared Amount is mandatory.";
            }

            if (isValid == 0)
            {

                int roleId = Convert.ToInt32(ManagePayRoleID.Value);

                if (ManagePayComponentGrid.SelectedDataKey.Value != null)
                {
                    int payCompId = Convert.ToInt32(ManagePayComponentGrid.SelectedDataKey.Value);

                    DateTime frmDate = Convert.ToDateTime(txtfrmDate.Text);

                    int declaredAmount = Convert.ToInt32(txtDeclaredAmt.Text);

                    BusinessLogic bL = new BusinessLogic(sDataSource);

                    bL.InsertRolePayComp(roleId, payCompId, frmDate, declaredAmount);

                    GetRolePayComponent(roleId);

                    txtDeclaredAmt.Text = null;
                    txtfrmDate.Text = null;
                    txtfrmDate.Enabled = false;
                    btnEditDate.Enabled = false;
                    txtDeclaredAmt.Enabled = false;
                    ManagePayComponentGrid.SelectedIndex = -1;

                    ModalPopupExtender3.Show();
                    EmployeePayCompPopUp.Visible = true;
                }
                else
                {
                    StringBuilder scriptMsg = new StringBuilder();
                    scriptMsg.Append("alert('Please select paycomponent');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), scriptMsg.ToString(), true);
                }
            }
            else
            {
                //txtDeclaredAmt.Text = null;
                //txtfrmDate.Text = null;
                //txtfrmDate.Enabled = false;
                //btnEditDate.Enabled = false;
                //txtDeclaredAmt.Enabled = false;
                //ManagePayComponentGrid.SelectedIndex = -1;

                ModalPopupExtender3.Show();
                EmployeePayCompPopUp.Visible = true;

                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Validation : "+ Message +"');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(Failed :" + ex.Message.ToString() +"');", true);
        }
    }

    protected void CancelRolePayMapBtn_Click(object sender, EventArgs e)
    {
       
    }

    protected void removeSelectedBtn_Click(object sender, EventArgs e)
    {
        try
        {
            if (PayCompRolePayGrid.SelectedDataKey != null)
            {
                int roleId = Convert.ToInt32(ManagePayRoleID.Value);

                int payCompId = Convert.ToInt32(PayCompRolePayGrid.SelectedDataKey.Value);

                BusinessLogic bL = new BusinessLogic(sDataSource);

                bL.DeleteRolePayComp(payCompId, roleId);

                txtDeclaredAmt.Text = null;
                txtfrmDate.Text = null;
                txtfrmDate.Enabled = false;
                btnEditDate.Enabled = false;
                txtDeclaredAmt.Enabled = false;
                PayCompRolePayGrid.SelectedIndex = -1;
                roleSearch.Text = "";

                GetPayComponent(string.Empty);

                GetRolePayComponent(roleId);

                ModalPopupExtender3.Show();
                EmployeePayCompPopUp.Visible = true;

            }
            else
            {
                ModalPopupExtender3.Show();
                EmployeePayCompPopUp.Visible = true;
                StringBuilder scriptMsg = new StringBuilder();
                scriptMsg.Append("alert('Please select paycomponent');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), scriptMsg.ToString(), true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(Failed :" + ex.Message.ToString() + "');", true);
        }
    }

    #region Private Methods

    public void GetEmployeeLeave(int roleId)
    {
        string connection = Request.Cookies["Company"].Value;

        BusinessLogic bLogic = new BusinessLogic(sDataSource);

        DataSet ds = new DataSet();
        ds = bLogic.GetEmpManageLeaveInfoByID(roleId);        

        if (ds != null)
        {
            ManageLeaveGridView.DataSource = ds.Tables[0];
        }
        else
        {
            ManageLeaveGridView.DataSource = ds;
        }

        ManageLeaveGridView.DataBind();
    }

    public void GetPayComponent(string searchTxt)
    {
        BusinessLogic bLogic = new BusinessLogic(sDataSource);

        DataSet ds = new DataSet();
        ds = bLogic.GetPayCompForRoleManage(searchTxt);        

        if (ds != null)
        {
            ManagePayComponentGrid.DataSource = ds.Tables[0];
        }
        else
        {
            ManagePayComponentGrid.DataSource = ds;
        }

        ManagePayComponentGrid.DataBind();
    }

    public void GetRolePayComponent(int roleId)
    {
        BusinessLogic bLogic = new BusinessLogic(sDataSource);

        DataSet ds = new DataSet();
        ds = bLogic.GetRolePayComp(roleId);

        if (ds != null)
        {
            PayCompRolePayGrid.DataSource = ds.Tables[0];
        }
        else
        {
            PayCompRolePayGrid.DataSource = ds;
        }
        PayCompRolePayGrid.DataBind();
    }

    #endregion

    protected void ManagePayComponentGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in ManagePayComponentGrid.Rows)
        {
            if (row.RowIndex == ManagePayComponentGrid.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                row.ToolTip = string.Empty;

                //ViewState["PayCompId"] = (int)ManagePayComponentGrid.DataKeys[ManagePayComponentGrid.SelectedIndex].Value;

                txtfrmDate.Enabled = true;
                btnEditDate.Enabled = true;
                txtDeclaredAmt.Enabled = true;

                ModalPopupExtender3.Show();
                EmployeePayCompPopUp.Visible = true;

            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                row.ToolTip = "Click to select this row.";

                ModalPopupExtender3.Show();
                EmployeePayCompPopUp.Visible = true;
            }
        }

    }

    protected void ManagePayComponentGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(ManagePayComponentGrid, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }

    protected void SearchRoleBtn_Click(object sender, EventArgs e)
    {
        if (roleSearch.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter Pay Component Name');", true);
            ModalPopupExtender3.Show();
            EmployeePayCompPopUp.Visible = true;
        }
        else
        {
            GetPayComponent(roleSearch.Text);
            
            ModalPopupExtender3.Show();
            EmployeePayCompPopUp.Visible = true;
        }

    }
    protected void PayCompRolePayGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in PayCompRolePayGrid.Rows)
        {
            if (row.RowIndex == PayCompRolePayGrid.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                row.ToolTip = string.Empty;

                //ViewState["PayCompId"] = (int)ManagePayComponentGrid.DataKeys[ManagePayComponentGrid.SelectedIndex].Value;

                //txtfrmDate.Enabled = true;
                //btnEditDate.Enabled = true;
                //txtDeclaredAmt.Enabled = true;

                ModalPopupExtender3.Show();
                EmployeePayCompPopUp.Visible = true;

            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                row.ToolTip = "Click to select this row.";

                ModalPopupExtender3.Show();
                EmployeePayCompPopUp.Visible = true;
            }
        }
    }
    protected void PayCompRolePayGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(PayCompRolePayGrid, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }
}