using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeLeaveApproval : System.Web.UI.Page
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


            //string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
            //dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            //BusinessLogic objChk = new BusinessLogic();

            //if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            //{

            //    //grdViewAttendanceSummary.Columns[7].Visible = false;
            //    //grdViewAttendanceSummary.Columns[8].Visible = false;
            //}
            grdViewLeaveSummary.PageSize = 8;

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            BindLeaveSummaryGrid();
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

    protected void grdViewLeaveSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int leaveId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("ApproveLeave"))
            {
                PopupDialogBindData(leaveId);
                ModalPopupExtender1.Show();
                LeaveDetailPopUp.Visible = true;
                lblPopupTitle.Text = "Approve Leave Request";
                hdfLeaveRequestResponse.Value = "Approved";
                ConfirmApprove.ConfirmText = "Are you sure to Approve this Leave ?";
            }
            else if (e.CommandName.Equals("RejectLeave"))
            {
                PopupDialogBindData(leaveId);
                ModalPopupExtender1.Show();
                LeaveDetailPopUp.Visible = true;
                lblPopupTitle.Text = "Reject Leave Request";
                hdfLeaveRequestResponse.Value = "Rejected";
                ConfirmApprove.ConfirmText = "Are you sure to Reject this Leave ?";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (UpdateLeave())
        {
            ClearPopupData();
            BindLeaveSummaryGrid();
            UpdatePanelMain.Update();
        }
    }


    #region Private Methods

    private void BindLeaveSummaryGrid()
    {
        grdViewLeaveSummary.DataSource = null;
        grdViewLeaveSummary.DataBind();

        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet ds = bl.GetLeaveRequestsSummaryForTheSupervisor(usernam);
        if (ds != null && ds.Tables.Count > 0)
        {
            grdViewLeaveSummary.DataSource = ds.Tables[0];
            grdViewLeaveSummary.DataBind();
            UpdatePanelMain.Update();
        }
    }

    private bool UpdateLeave()
    {
        string connection = Request.Cookies["Company"].Value;
        string EmployeeNo = Request.Cookies["LoggedUserName"].Value;
        string leaveId = hdfLeaveId.Value;
        string status = hdfLeaveRequestResponse.Value;
        string approverComments = txtApproverComments.Text.Trim();

        BusinessLogic bl = new BusinessLogic(connection);
        int result = bl.UpdateLeaveStatus(leaveId, status, approverComments);

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PopupDialogBindData(int leaveId)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataTable dt = bl.GetLeaveDetailsById(leaveId);

        DateTime date;

        hdfLeaveId.Value = leaveId.ToString();
        if (DateTime.TryParse(dt.Rows[0]["StartDate"].ToString(), out date))
        {
            txtStartDate.Text = date.ToShortDateString();
        }
        ddlStartDateSession.SelectedValue = dt.Rows[0]["StartDateSession"].ToString();

        if (DateTime.TryParse(dt.Rows[0]["EndDate"].ToString(), out date))
        {
            txtEndDate.Text = date.ToShortDateString();
        }
        ddlEndDateSession.SelectedValue = dt.Rows[0]["EndDateSession"].ToString();

        ddlLeaveType.SelectedValue = dt.Rows[0]["LeaveTypeId"].ToString();
        txtReason.Text = dt.Rows[0]["Reason"].ToString();
        lblEmployeeName.Text = dt.Rows[0]["EmployeeName"].ToString();
        hdfEmpNo.Value = dt.Rows[0]["EmployeeNo"].ToString();
        txtEmailContact.Text = dt.Rows[0]["EmailContact"].ToString();
        txtPhoneContact.Text = dt.Rows[0]["PhoneContact"].ToString();
        txtApproverComments.Text = dt.Rows[0]["ApproverComments"].ToString();
    }
    private void ClearPopupData()
    {
        hdfLeaveId.Value = string.Empty;
        txtStartDate.Text = string.Empty;
        ddlStartDateSession.SelectedValue = "FN";
        txtEndDate.Text = string.Empty;
        ddlEndDateSession.SelectedValue = "AN";

        txtReason.Text = string.Empty;
        lblEmployeeName.Text = string.Empty;
        hdfEmpNo.Value = string.Empty;
        txtEmailContact.Text = string.Empty;
        txtPhoneContact.Text = string.Empty;
        txtApproverComments.Text = string.Empty;
    }

    #endregion

}