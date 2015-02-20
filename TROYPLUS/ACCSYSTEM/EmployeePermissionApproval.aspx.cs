using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeePermissionApproval : System.Web.UI.Page
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

    #region Private Methods

    private void BindPermissionSummaryGrid()
    {
        grdViewPermissionSummary.DataSource = null;
        grdViewPermissionSummary.DataBind();

        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet ds = bl.GetPermissionRequestsSummary(usernam);
        if (ds != null && ds.Tables.Count > 0)
        {
            grdViewPermissionSummary.DataSource = ds.Tables[0];
            grdViewPermissionSummary.DataBind();
            UpdatePanelMain.Update();
        }
    }

    private bool UpdatePermission()
    {
        string connection = Request.Cookies["Company"].Value;
        string EmployeeNo = Request.Cookies["LoggedUserName"].Value;
        string PermissionId = hdfPermissionId.Value;
        string status = hdfPermissionRequestResponse.Value;
        string approverComments = txtApproverComments.Text.Trim();

        BusinessLogic bl = new BusinessLogic(connection);
        int result = bl.UpdatePermissionStatus(PermissionId, status, approverComments);

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
            //txtStartDate.Text = date.ToShortDateString();
            txtStartDate.Text = date.ToString("hh:mm");
            ddlStartDateSession.SelectedValue = date.ToString("tt");
        }
        //ddlStartDateSession.SelectedValue = dt.Rows[0]["StartDateSession"].ToString();

        if (DateTime.TryParse(dt.Rows[0]["EndTime"].ToString(), out date))
        {
            //txtEndDate.Text = date.ToShortDateString();
            txtEndDate.Text = date.ToString("hh:mm");
            ddlStartDateSession.SelectedValue = date.ToString("tt");
        }
        //ddlEndDateSession.SelectedValue = dt.Rows[0]["EndDateSession"].ToString();

        //ddlPerType.SelectedValue = dt.Rows[0]["LeaveTypeId"].ToString();
        txtReason.Text = dt.Rows[0]["Reason"].ToString();
        lblEmployeeName.Text = dt.Rows[0]["EmployeeName"].ToString();
        hdfEmpNo.Value = dt.Rows[0]["EmployeeNo"].ToString();
        txtEmailContact.Text = dt.Rows[0]["EmailContact"].ToString();
        txtPhoneContact.Text = dt.Rows[0]["PhoneContact"].ToString();
        txtApproverComments.Text = dt.Rows[0]["ApproverComments"].ToString();
    }

    private void ClearPopupData()
    {
        hdfPermissionId.Value = string.Empty;
        txtStartDate.Text = string.Empty;
        ddlStartDateSession.SelectedValue = "AM";
        txtEndDate.Text = string.Empty;
        ddlEndDateSession.SelectedValue = "AM";

        txtReason.Text = string.Empty;
        lblEmployeeName.Text = string.Empty;
        hdfEmpNo.Value = string.Empty;
        txtEmailContact.Text = string.Empty;
        txtPhoneContact.Text = string.Empty;
        txtApproverComments.Text = string.Empty;
    }

    #endregion

    protected void grdViewPermissionSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int permissionId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("ApprovePermission"))
            {
                PopupDialogBindData(permissionId);
                ModalPopupExtender1.Show();
                PermissionDetailPopUp.Visible = true;
                lblPopupTitle.Text = "Approve Permssion Request";
                hdfPermissionRequestResponse.Value = "Approved";
                ConfirmApprove.ConfirmText = "Are you sure to Approve this Permission ?";
            }
            else if (e.CommandName.Equals("RejectPermission"))
            {
                PopupDialogBindData(permissionId);
                ModalPopupExtender1.Show();
                PermissionDetailPopUp.Visible = true;
                lblPopupTitle.Text = "Reject Permssion Request";
                hdfPermissionRequestResponse.Value = "Rejected";
                ConfirmApprove.ConfirmText = "Are you sure to Reject this Permssion ?";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (UpdatePermission())
        {
            ClearPopupData();
            BindPermissionSummaryGrid();
            UpdatePanelMain.Update();
        }
    }
}