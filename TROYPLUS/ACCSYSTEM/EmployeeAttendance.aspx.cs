using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Attendance_EmployeeAttendance : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.OnInit(e);
            //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
            //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                //    lnkBtnAddAttendance.Visible = false;
                //    //grdViewAttendanceSummary.Columns[7].Visible = false;
                //    //grdViewAttendanceSummary.Columns[8].Visible = false;
                //}
                grdViewAttendanceSummary.PageSize = 8;

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                BindAttendanceSummaryFilterList();
                if (ddlSearchCriteria.SelectedIndex >= 0)
                {
                    BindAttendanceSummaryGrid(ddlSearchCriteria.SelectedValue);
                }
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
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkBtnAddAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            string connection = Request.Cookies["Company"].Value;
            string username = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(connection);
            if (!bl.IsAttendanceSummaryExists(username, DateTime.Today.Year.ToString(), DateTime.Today.Month.ToString()))
            {
                DataTable dtAttendanceDetails = bl.GetNewAttendanceDetailsForMonth(connection, DateTime.Today.Year, DateTime.Today.Month, username).Tables[0];

                ViewState["AttendanceYear"] = DateTime.Today.Year.ToString();
                ViewState["AttendanceMonth"] = DateTime.Today.Month.ToString();
                Session["DtAttendanceDetails"] = dtAttendanceDetails;
                GridViewAttendanceDetail.DataSource = dtAttendanceDetails;

                ChangeGridColumnHeaderText();
                GridViewAttendanceDetail.DataBind();
                hdnfIsNewAttendance.Value = "1";

                GridViewAttendanceDetail.Visible = true;
                AttendanceDetailPopUp.Visible = true;
                ModalPopupExtender1.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    @"alert('Attendance has been already available for the period " + DateTime.Today.ToString("MMM", CultureInfo.InvariantCulture) + "-" + DateTime.Today.Year.ToString() + "');", true);
            }
        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridViewAttendanceDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex >= 0)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Controls.Count > 1)
                    {
                        if (cell.Controls[1].ToString().Equals("System.Web.UI.WebControls.Button"))
                        {
                            Button btn = cell.Controls[1] as Button;
                            if (!btn.Text.Equals(string.Empty))
                            {
                                ToggleAttendanceMark(btn);
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

    protected void grdViewAttendanceSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("EditRecord"))
            {
                int attendanceID = 0;
                string[] args = e.CommandArgument.ToString().Split(new char[] { ':' });
                if (args.Length == 4)
                {
                    if (int.TryParse(args[0], out attendanceID))
                    {
                        hdnfAttendanceID.Value = attendanceID.ToString();
                        string connection = Request.Cookies["Company"].Value;
                        string username = Request.Cookies["LoggedUserName"].Value;
                        BusinessLogic bl = new BusinessLogic(connection);
                        DataSet dtAttendanceDetail = bl.GetAttendanceDetails(attendanceID, username);
                        if (dtAttendanceDetail != null)
                        {
                            hdnfIsNewAttendance.Value = "0";
                            string status = args[3];

                            // Make attendance details readonly.
                            SetAttendanceDetailsAsReadOnly(status.Equals("Submitted"));


                            ViewState["AttendanceYear"] = args[1];
                            ViewState["AttendanceMonth"] = args[2];
                            Session["DtAttendanceDetails"] = dtAttendanceDetail.Tables[0];
                            GridViewAttendanceDetail.DataSource = dtAttendanceDetail.Tables[0];

                            ChangeGridColumnHeaderText();
                            GridViewAttendanceDetail.DataBind();

                            GridViewAttendanceDetail.Visible = true;
                            AttendanceDetailPopUp.Visible = true;
                            ModalPopupExtender1.Show();
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

    private void SetAttendanceDetailsAsReadOnly(bool yes)
    {
        GridViewAttendanceDetail.Enabled = !yes;
        btnSaveAttendance.Visible = !yes;
        btnSubmitAttendance.Visible = !yes;
        lblUserHint.Visible = !yes;
        if (yes)
        {
            lblStatus.Text = "Submitted attendance could not be modified.";
        }
        else
        {
            lblStatus.Text = string.Empty;
        }

    }

    protected void ToggleAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            lblStatus.Text = string.Empty;
            ToggleAttendanceMark(sender as Button, true);
            updPnlAttendanceDeailsGrid.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSaveAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            string validationMsg = string.Empty;
            if (SaveAttendanceDetails() > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    @"alert('Attendance has been saved successfully');", true);
                if (ddlSearchCriteria.SelectedIndex >= 0)
                {
                    BindAttendanceSummaryGrid(ddlSearchCriteria.SelectedValue);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                  @"alert('Unable to save attendance details. Please contact your Administrator.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSubmitAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            InitiateAttendanceSubmission();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void InitiateAttendanceSubmission()
    {
        int attendanceID = -1;
        string validationMsg = string.Empty;
        attendanceID = SaveAttendanceDetails();
        if (attendanceID > 0)
        {
            if (SubmitAttendance(attendanceID, ref validationMsg))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
               @"alert('Attendance details saved and submitted successfully');", true);
                if (ddlSearchCriteria.SelectedIndex >= 0)
                {
                    BindAttendanceSummaryGrid(ddlSearchCriteria.SelectedValue);
                }
            }
            else
            {
                lblAutoApplyLeaveMsg.Text = "ATTENDANCE SUBMISSION FAILED" + Environment.NewLine + validationMsg + Environment.NewLine + "Click Submit to apply leave automatically for the above mentioned employee";
                ModalPopupAutoApplyLeave.Show();
                ModalPopupExtender1.Show();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                                        @"alert('Attendance details saved but not submitted.');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                @"alert('Unable to save attendance details. Please contact your Administrator.');", true);
        }
    }

    protected void btnAutoApplyLeave_Click(object sender, EventArgs e)
    {
        try
        {
            // Apply leaves.
            if (CheckAndAutoApplyLeaveForAbsent())
            {
                // Submit attendance.
                InitiateAttendanceSubmission();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private bool CheckAndAutoApplyLeaveForAbsent()
    {
        DataTable dtAttendanceDetail = GetReArrangedDataTable(Session["DtAttendanceDetails"] as DataTable);
        DataRow[] drAbsentRecords = dtAttendanceDetail.Select("Remarks = 'Absent'");
        if (drAbsentRecords.Count() > 0)
        {
            string connection = Request.Cookies["Company"].Value;
            string username = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(connection);
            foreach (DataRow dr in drAbsentRecords)
            {
                string empNo = dr[0].ToString();
                DateTime absentDate = DateTime.Parse(dr[1].ToString());
                string leaveTypeId = ConfigurationManager.AppSettings["AutoApplyLeaveType"];
                if (string.IsNullOrEmpty(leaveTypeId))
                {
                    leaveTypeId = "1";
                }
                if (bl.ApplyLeave(empNo, absentDate, "FN", absentDate, "AN", DateTime.Now, leaveTypeId, "Leave auto applied by " + username, string.Empty, string.Empty, string.Empty, "Approved") > 0)
                {
                    dr["Remarks"] = "Leave";
                    dr.AcceptChanges();
                }
            }
            dtAttendanceDetail.AcceptChanges();

        }
        return true;
    }

    protected void ddlSearchCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSearchCriteria.SelectedIndex >= 0)
            {
                BindAttendanceSummaryGrid(ddlSearchCriteria.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    #region Private Methods
    private void ToggleAttendanceMark(Button btnSender, bool isUpdate = false)
    {
        if (btnSender != null)
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            // verify the value and change the next applicable value for the button
            if (btnSender.Text == "Present")
            {
                // Present can be changed to Absent only.
                if (isUpdate)
                {
                    btnSender.Text = "Absent";
                    btnSender.CssClass = "btnBts btnBts-warning";
                }
                else
                    btnSender.CssClass = "btnBts btnBts-default";
            }
            else if (btnSender.Text == "Absent")
            {
                if (isUpdate)
                {
                    btnSender.Text = "Present";
                    btnSender.CssClass = "btnBts btnBts-default";
                }
                else
                    btnSender.CssClass = "btnBts btnBts-warning";
            }
            //else if (btnSender.Text == "Leave")
            //{
            //    // Leave can be updated to Present provided week-off or comp-off.
            //    if (isUpdate)
            //    {
            //        CheckCompOffOptionsForTheRequest(btnSender);
            //    }
            //    else { btnSender.CssClass = "btnBts btnBts-warning"; }

            //}
            else if (btnSender.Text == "Week Off")
            {
                if (isUpdate)
                {
                    DateTime requestDate = getDateValueForTheRequest(btnSender);
                    if (bl.IsHoliday(requestDate))
                    {
                        btnSender.Text = "Holiday";
                        btnSender.CssClass = "btnBts btnBts-info";
                    }
                    else
                    {
                        CheckCompOffOptionsForTheRequest(btnSender);
                    }
                }
                else { btnSender.CssClass = "btnBts btnBts-success"; }

            }
            else if (btnSender.Text == "Holiday")
            {
                if (isUpdate)
                {
                    CheckCompOffOptionsForTheRequest(btnSender);
                }
                else
                    btnSender.CssClass = "btnBts btnBts-info";

            }
            else if (btnSender.Text == "NA")
            {
                btnSender.CssClass = "btnBts btnBts-disabled";
            }
        }
    }

    private void CheckCompOffOptionsForTheRequest(Button btnSender)
    {
        string preState = btnSender.CommandArgument.Split(new string[] { "//" }, StringSplitOptions.None)[2];
        if (preState.Equals("Week Off") || preState.Equals("Leave") || preState.Equals("Holiday"))
        {
            if (!ShowCompOffRotaPopup(btnSender))
            {
                btnSender.Text = "Present";
                btnSender.CssClass = "btnBts btnBts-default";
            }
        }
        else
        {
            btnSender.Text = "Present";
            btnSender.CssClass = "btnBts btnBts-default";
        }
    }

    private DateTime getDateValueForTheRequest(Button btnSender)
    {
        int year;
        int.TryParse(ViewState["AttendanceYear"].ToString(), out year);
        int month;
        int.TryParse(ViewState["AttendanceMonth"].ToString(), out month);
        int dayOfMonth;
        int.TryParse(btnSender.ID.ToLower().Replace("button", string.Empty), out dayOfMonth);
        DateTime requestDate;
        DateTime.TryParse(string.Format("{0}-{1}-{2}", year, month, dayOfMonth), out requestDate);
        return requestDate;
    }

    private bool ShowCompOffRotaPopup(Button btnSender)
    {
        if (btnSender != null)
        {
            // Get the date value for the current request.

            DateTime requestDate = getDateValueForTheRequest(btnSender);

            hdnfRequestDayInfo.Value = requestDate.ToShortDateString();
            hdnfCompOffRotaEmployeeNo.Value = btnSender.CommandArgument.Split(new char[] { '/' })[0];
            hdnfRequestSenderId.Value = btnSender.ID;

            DataTable consequenceDates = GetConsequenceOneWeekDate(requestDate);
            ddlRotaAlternativeDays.DataSource = consequenceDates;
            ddlRotaAlternativeDays.DataTextField = "LongDate";
            ddlRotaAlternativeDays.DataValueField = "ShortDate";
            ddlRotaAlternativeDays.DataBind();

            updPnlRotaCompOff.Update();
            CompOffModalPopupExtender.Show();
            return true;

        }
        return false;
    }

    private DataTable GetConsequenceOneWeekDate(DateTime requestDate)
    {
        int index = 1;
        DataTable resultDates = new DataTable();
        resultDates.Columns.Add("ShortDate");
        resultDates.Columns.Add("LongDate");

        do
        {
            DataRow row = resultDates.NewRow();
            row[0] = requestDate.AddDays(index).ToShortDateString();
            row[1] = requestDate.AddDays(index).ToLongDateString();
            resultDates.Rows.Add(row);
            index++;
        } while (index <= 7);
        return resultDates;
    }

    private void ChangeGridColumnHeaderText()
    {
        DataTable dtGridSrc = GridViewAttendanceDetail.DataSource as DataTable;
        foreach (DataControlField column in GridViewAttendanceDetail.Columns)
        {
            if (dtGridSrc.Columns.Contains(column.AccessibleHeaderText))
            {
                column.HeaderText = GetColumnHeaderTextForCell(column.AccessibleHeaderText, GridViewAttendanceDetail.DataSource as DataTable);

                if (column.HeaderText.Equals("NA"))
                {
                    column.Visible = false;
                }
            }
            else
            {
                column.Visible = false;
            }
        }
        hdnfIsGridLoaded.Value = "1";
    }

    private void ResetGridColumnHeaderText()
    {
        int dayIndex = 1;
        foreach (DataControlField column in GridViewAttendanceDetail.Columns)
        {
            if (!(column.HeaderText.Equals("EmployeeNo") || column.HeaderText.Equals("Employee")))
            {
                column.HeaderText = "Day" + dayIndex.ToString();
                dayIndex++;
                column.Visible = true;
            }
        }
    }

    private string GetColumnHeaderTextForCell(string colHeaderName, DataTable dtSource)
    {
        DateTime dateValue;
        if (DateTime.TryParse(dtSource.Columns[colHeaderName].Caption, out dateValue))
        {
            return dateValue.ToShortDateString() + "\r\n" + dateValue.DayOfWeek;
        }
        else
        {
            return dtSource.Columns[colHeaderName].Caption;
        }
    }

    private int SaveAttendanceDetails()
    {
        int attendanceId = 0;
        if (Session["DtAttendanceDetails"] != null)
        {
            DataTable dtAttendanceDetail = GetReArrangedDataTable(Session["DtAttendanceDetails"] as DataTable);
            // Update records in the database
            string connection = Request.Cookies["Company"].Value;
            string username = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(connection);
            bool createSummary = false;
            if (hdnfIsNewAttendance.Value.Equals("1"))
                createSummary = true;

            if (bl.SaveAttendanceDetail(dtAttendanceDetail, username, string.Empty, ViewState["AttendanceYear"].ToString(),
                ViewState["AttendanceMonth"].ToString(), createSummary, out attendanceId))
            {
                if (!createSummary)
                    int.TryParse(hdnfAttendanceID.Value, out attendanceId);
                else
                {
                    hdnfAttendanceID.Value = attendanceId.ToString();
                }
                hdnfIsNewAttendance.Value = string.Empty;
                return attendanceId;
            }
        }
        return attendanceId;
    }

    private DataTable GetReArrangedDataTable(DataTable dtGridSource)
    {
        DataTable dtAttendanceDetail = new DataTable();
        dtAttendanceDetail.Columns.Add(new DataColumn("EmpNo"));
        dtAttendanceDetail.Columns.Add(new DataColumn("Date"));
        dtAttendanceDetail.Columns.Add(new DataColumn("Remarks"));

        int rowIndex = 0;
        foreach (GridViewRow row in GridViewAttendanceDetail.Rows)
        {
            string[] rowItem = new string[3];
            int cellIndex = 0;
            foreach (TableCell cell in row.Cells)
            {
                if (cellIndex == 0)
                {
                    rowItem[0] = dtGridSource.Rows[rowIndex][cellIndex].ToString();
                }

                if (cell.Controls.Count > 1)
                {
                    if (cell.Controls[1].ToString().Equals("System.Web.UI.WebControls.Button"))
                    {
                        Button btn = cell.Controls[1] as Button;
                        if (!btn.Text.Equals(string.Empty) && !btn.Text.Equals("NA"))
                        {
                            // Get date value
                            string dateValue = GridViewAttendanceDetail.Columns[cellIndex].HeaderText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).First();
                            rowItem[1] = dateValue;
                            // Get attendance mark
                            rowItem[2] = btn.Text;

                            dtAttendanceDetail.Rows.Add(rowItem.ToArray());
                        }
                    }
                }
                cellIndex++;

            }
            rowIndex++;
        }
        return dtAttendanceDetail;
    }

    private bool SubmitAttendance(int attendanceID, ref string validationMsg)
    {
        string connection = Request.Cookies["Company"].Value;
        string username = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic(connection);
        return bl.SubmitAttendance(attendanceID, username, ViewState["AttendanceYear"].ToString(),
                ViewState["AttendanceMonth"].ToString(), ref validationMsg);
    }

    private void BindAttendanceSummaryGrid(string attendanceYear)
    {
        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet ds = bl.GetAttendanceSummary(attendanceYear, usernam);
        if (ds != null && ds.Tables.Count > 0)
        {
            grdViewAttendanceSummary.DataSource = ds.Tables[0];
            grdViewAttendanceSummary.DataBind();
            UpdatePanelMain.Update();
        }
        else
        {
            grdViewAttendanceSummary.DataSource = null;
            grdViewAttendanceSummary.DataBind();
            UpdatePanelMain.Update();
        }

    }

    private void BindAttendanceSummaryFilterList()
    {
        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataTable dt = bl.GetAttendanceYearList(usernam);
        if (dt != null)
        {
            ddlSearchCriteria.DataSource = dt;
            ddlSearchCriteria.DataBind();
            UpdatePanelMain.Update();
        }
    }

    #endregion

    protected void btnApproveCompOff_Click(object sender, EventArgs e)
    {
        try
        {
            string errorMessage = string.Empty;
            if (ValidateAndSaveRotaCompOffEntries(hdnfCompOffRotaEmployeeNo.Value, ref errorMessage))
            {
                CompOffModalPopupExtender.Hide();
                if (SaveAttendanceDetails() > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    @"alert('Comp-off/Rota and the attendance details has been saved successfully.');", true);
                    if (ddlSearchCriteria.SelectedIndex >= 0)
                    {
                        BindAttendanceSummaryGrid(ddlSearchCriteria.SelectedValue);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                   @"alert('Unable to save attendance details, please contact your Administrator.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                 @"alert('" + errorMessage + " /r/nUnable to save comp-off/rota details, please contact your Administrator.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void UpdateCompOffRotaRequestStatus(bool isRotaUpdate = false)
    {
        string btnSenderId = hdnfRequestSenderId.Value;
        foreach (GridViewRow row in GridViewAttendanceDetail.Rows)
        {
            Label lblEmployeeNo = row.FindControl("lblEmployeeNo") as Label;
            if (lblEmployeeNo.Text.Equals(hdnfCompOffRotaEmployeeNo.Value))
            {
                Control btnCtrl = row.FindControl(btnSenderId);
                if (btnCtrl != null)
                {
                    Button btnSender = (btnCtrl as Button);
                    btnSender.Text = "Present";
                    btnSender.CssClass = "btnBts btnBts-default";

                    if (isRotaUpdate)
                    {
                        DateTime dtShiftedDate;
                        if (DateTime.TryParse(ddlRotaAlternativeDays.SelectedValue, out dtShiftedDate))
                        {
                            string shiftedDateControlId = "Button" + dtShiftedDate.Day.ToString();
                            Control ctrlShiftedDateBtn = row.FindControl(shiftedDateControlId);
                            if (ctrlShiftedDateBtn != null)
                            {
                                Button btnShiftedDate = ctrlShiftedDateBtn as Button;
                                btnShiftedDate.Text = "Week Off";
                                btnShiftedDate.CssClass = "btnBts btnBts-success";
                            }
                        }
                    }
                    break;
                }
            }
        }
    }

    private bool ValidateAndSaveRotaCompOffEntries(string empNo, ref string errorMessage)
    {
        if (rbtnCompOff.Checked)
        {
            var reason = txtCompOffReason.Text.Trim();
            if (reason.Equals(string.Empty))
            {
                errorMessage = "Please provide the comp-off reason";
                txtCompOffReason.Focus();
                return false;
            }
            else
            {
                DateTime dtCompOffSourceDate;
                DateTime.TryParse(hdnfRequestDayInfo.Value, out dtCompOffSourceDate);
                if (AddCompOffForTheEmployee(empNo, dtCompOffSourceDate, reason))
                {
                    UpdateCompOffRotaRequestStatus();
                    return true;
                }
                else
                    return false;
            }
        }
        else if (rbtnRota.Checked)
        {
            DateTime dtRotaSourceDate;
            DateTime.TryParse(hdnfRequestDayInfo.Value, out dtRotaSourceDate);
            DateTime rotaAlternativeDate;
            DateTime.TryParse(ddlRotaAlternativeDays.SelectedValue, out rotaAlternativeDate);

            if (SetWeekOffRotaForTheEmployee(empNo, dtRotaSourceDate, rotaAlternativeDate))
            {
                UpdateCompOffRotaRequestStatus(true);
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

    private bool SetWeekOffRotaForTheEmployee(string empNo, DateTime dtRotaSourceDate, DateTime rotaAlternativeDate)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        var supervisorUserName = Request.Cookies["LoggedUserName"].Value;
        var supervisorEmpNo = bl.GetUserInfoByName(supervisorUserName).EmpNo.ToString();

        if (bl.AddWeekOffRotaForTheEmployee(empNo, supervisorEmpNo, dtRotaSourceDate, rotaAlternativeDate))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool AddCompOffForTheEmployee(string empNo, DateTime dtCompOffSourceDate, string reason)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        var supervisorUserName = Request.Cookies["LoggedUserName"].Value;
        var supervisorEmpNo = bl.GetUserInfoByName(supervisorUserName).EmpNo.ToString();

        if (bl.AddCompOffForTheEmployee(empNo, supervisorEmpNo, dtCompOffSourceDate, reason))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    protected void ModalPopupExtender1_Unload(object sender, EventArgs e)
    {
        try
        {
            lblStatus.Text = string.Empty;
            ResetGridColumnHeaderText();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void rbtnRotaCompOff_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnCompOff.Checked)
            {
                divCompOffContainer.Visible = true;
                divRotaContaiiner.Visible = false;
            }
            else if (rbtnRota.Checked)
            {
                divRotaContaiiner.Visible = true;
                divCompOffContainer.Visible = false;
            }
            updPnlRotaCompOff.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnCancelCompOff_Click(object sender, EventArgs e)
    {
        try
        {
            hdnfRequestDayInfo.Value = string.Empty;
            hdnfRequestSenderId.Value = string.Empty;
            hdnfCompOffRotaEmployeeNo.Value = string.Empty;
            txtCompOffReason.Text = string.Empty;
            lblStatus.Text = string.Empty;
            ddlRotaAlternativeDays.Items.Clear();
            CompOffModalPopupExtender.Hide();
            updPnlRotaCompOff.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}