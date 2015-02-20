using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeePermission : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

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
                lnkBtnApplyPermission.Visible = false;
                //grdViewAttendanceSummary.Columns[7].Visible = false;
                //grdViewAttendanceSummary.Columns[8].Visible = false;
            }
            grdViewPermissionSummary.PageSize = 8;

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            BindPermissionSummaryGrid();
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

    protected void lnkBtnApplyPermission_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender1.Show();
            PermissionDetailPopUp.Visible = true;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            UserInfo userInfo = bl.GetUserInfoByName(Request.Cookies["LoggedUserName"].Value);

            lblApproverName.Text = userInfo.ManagerEmpName;
            hdfApproverEmpNo.Value = userInfo.ManagerEmpNo.ToString();

            ViewState["PopupMode"] = "NEW";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdViewPermissionSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int permissionId = 0;
            if (e.CommandName.Equals("EditPermission"))
            {
                if (int.TryParse(e.CommandArgument.ToString(), out permissionId))
                {
                    PopupDialogBindData(permissionId);
                    ModalPopupExtender1.Show();
                    PermissionDetailPopUp.Visible = true;
                    ViewState["PopupMode"] = "UPDATE";
                }
            }
            else if (e.CommandName.Equals("CancelPermission"))
            {
                if (int.TryParse(e.CommandArgument.ToString(), out permissionId))
                {
                    if (CancelPermission(permissionId))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your leave request has been cancelled successfully');", true);
                        BindPermissionSummaryGrid();
                        UpdatePanelMain.Update();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnApplyPermission_Click(object sender, EventArgs e)
    {
        string errorMsg = string.Empty;
        try
        {
            if (ViewState["PopupMode"] != null && ViewState["PopupMode"].ToString() == "NEW")
            {
                if (InsertPermission(ref errorMsg))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your Permission request has been submitted successfully');", true);
                    ClearPopupData();
                    BindPermissionSummaryGrid();
                    UpdatePanelMain.Update();
                }
                else
                {
                    ModalPopupExtender1.Show();
                    PermissionDetailPopUp.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + errorMsg + "');", true);
                }
            }
            else if (ViewState["PopupMode"] != null && ViewState["PopupMode"].ToString() == "UPDATE")
            {
                if (UpdatePermission(ref errorMsg))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your Permission request has been updated successfully');", true);
                    ClearPopupData();
                    BindPermissionSummaryGrid();
                    UpdatePanelMain.Update();

                }
                else
                {
                    ModalPopupExtender1.Show();
                    PermissionDetailPopUp.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + errorMsg + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            ModalPopupExtender1.Show();
            PermissionDetailPopUp.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    #region Private Methods

    private void BindPermissionSummaryGrid()
    {
        grdViewPermissionSummary.DataSource = null;
        grdViewPermissionSummary.DataBind();

        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet ds = bl.GetPermissionSummary(usernam);
        if (ds != null && ds.Tables.Count > 0)
        {
            grdViewPermissionSummary.DataSource = ds.Tables[0];
            grdViewPermissionSummary.DataBind();
            UpdatePanelMain.Update();
        }
    }

    private bool InsertPermission(ref string ErrorMsg)
    {
        string connection = Request.Cookies["Company"].Value;
        string EmployeeNo = Request.Cookies["LoggedUserName"].Value;
        DateTime PermissionDate = DateTime.Parse(txtPermissionDate.Text.Trim());

        //DateTime StartTime = DateTime.Parse(txtStartTime.Text.Trim());        

        string StartTimeSession = ddlStartTimeSession.SelectedValue.Trim();

        DateTime StartTime = new DateTime();
        DateTime.TryParse(txtStartTime.Text.Trim() + StartTimeSession, out StartTime);

        //DateTime EndTime = DateTime.Parse(txtEndTime.Text.Trim());

        string EndTimeSession = ddlEndTimeSession.SelectedValue.Trim();

        DateTime EndTime = new DateTime();
        DateTime.TryParse(txtEndTime.Text.Trim() + EndTimeSession, out EndTime);

        if (StartTime > EndTime)
        {
            ErrorMsg = "Start time cannot be greater than End time";
            return false;
        }

        DateTime DateApplied = DateTime.Now;

        string Reason = txtReason.Text.Trim();
        string Approver = hdfApproverEmpNo.Value.Trim();
        string EmailContact = txtEmailContact.Text.Trim();
        string PhoneContact = txtPhoneContact.Text.Trim();

        BusinessLogic bl = new BusinessLogic(connection);

        TimeSpan tr = EndTime - StartTime;

        if (bl.IsPermissionValid(EmployeeNo, PermissionDate))
        {
            if (bl.IsPermissionHourValid(EmployeeNo, Convert.ToInt32(tr.Hours)))
            {
                int leaveId = bl.ApplyPermission(EmployeeNo, StartTime, EndTime, PermissionDate, Reason, Approver, EmailContact, PhoneContact);
                if (leaveId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    private bool UpdatePermission(ref string ErrorMsg)
    {
        //string connection = Request.Cookies["Company"].Value;
        //string EmployeeNo = Request.Cookies["LoggedUserName"].Value;
        //string leaveId = hdfPermissionId.Value;

        //DateTime PermissionDate = DateTime.Parse(txtPermissionDate.Text.Trim());
        //DateTime StartTime = DateTime.Parse(txtStartTime.Text.Trim());
        //string StartTimeSession = ddlStartTimeSession.SelectedValue.Trim();
        //DateTime EndTime = DateTime.Parse(txtEndTime.Text.Trim());
        //string EndTimeSession = ddlEndTimeSession.SelectedValue.Trim();


        string connection = Request.Cookies["Company"].Value;
        string EmployeeNo = Request.Cookies["LoggedUserName"].Value;
        DateTime PermissionDate = DateTime.Parse(txtPermissionDate.Text.Trim());

        string permissionId = hdfPermissionId.Value;

        //DateTime StartTime = DateTime.Parse(txtStartTime.Text.Trim());        

        string StartTimeSession = ddlStartTimeSession.SelectedValue.Trim();

        DateTime StartTime = new DateTime();
        DateTime.TryParse(txtStartTime.Text.Trim() + StartTimeSession, out StartTime);

        //DateTime EndTime = DateTime.Parse(txtEndTime.Text.Trim());

        string EndTimeSession = ddlEndTimeSession.SelectedValue.Trim();

        DateTime EndTime = new DateTime();
        DateTime.TryParse(txtEndTime.Text.Trim() + EndTimeSession, out EndTime);

        if (StartTime > EndTime)
        {
            ErrorMsg = "Start time cannot be greater than End time";
            return false;
        }

        DateTime DateApplied = DateTime.Now;

        string Reason = txtReason.Text.Trim();
        string Approver = hdfApproverEmpNo.Value.Trim();
        string EmailContact = txtEmailContact.Text.Trim();
        string PhoneContact = txtPhoneContact.Text.Trim();

        BusinessLogic bl = new BusinessLogic(connection);
        int result = bl.UpdatePermission(permissionId, EmployeeNo, StartTime, EndTime, PermissionDate, Reason, Approver, EmailContact, PhoneContact);

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CancelPermission(int permissionId)
    {
        string connection = Request.Cookies["Company"].Value;

        BusinessLogic bl = new BusinessLogic(connection);
        int result = bl.DeleteLeave(permissionId.ToString());

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PopupDialogBindData(int permissionId)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataTable dt = bl.GetPermissionDetailsById(permissionId);
        DateTime date;

        hdfPermissionId.Value = permissionId.ToString();
        if (DateTime.TryParse(dt.Rows[0]["StartTime"].ToString(), out date))
        {
            txtStartTime.SelectedValue = date.ToString("hh:mm");
            ddlStartTimeSession.SelectedValue = date.ToString("tt");
        }

        if (DateTime.TryParse(dt.Rows[0]["EndTime"].ToString(), out date))
        {
            txtEndTime.SelectedValue = date.ToString("hh:mm");
            ddlEndTimeSession.SelectedValue = date.ToString("tt");
        }

        if (DateTime.TryParse(dt.Rows[0]["DateApplied"].ToString(), out date))
        {
            txtPermissionDate.Text = date.ToString();
        }

        txtReason.Text = dt.Rows[0]["Reason"].ToString();
        lblApproverName.Text = dt.Rows[0]["ApproverName"].ToString();
        hdfApproverEmpNo.Value = dt.Rows[0]["Approver"].ToString();
        txtEmailContact.Text = dt.Rows[0]["EmailContact"].ToString();
        txtPhoneContact.Text = dt.Rows[0]["PhoneContact"].ToString();
    }

    private void ClearPopupData()
    {
        hdfPermissionId.Value = string.Empty;
        txtStartTime.Text = string.Empty;
        ddlStartTimeSession.SelectedValue = "AM";
        txtEndTime.Text = string.Empty;
        ddlEndTimeSession.SelectedValue = "AM";

        txtReason.Text = string.Empty;
        lblApproverName.Text = string.Empty;
        hdfApproverEmpNo.Value = string.Empty;
        txtEmailContact.Text = string.Empty;
        txtPhoneContact.Text = string.Empty;
    }
    #endregion
}