using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeTimesheetEntry : System.Web.UI.Page
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


                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAddTimesheet.Visible = false;
                    //grdViewTimesheetSummary.Columns[7].Visible = false;
                    //grdViewTimesheetSummary.Columns[8].Visible = false;
                }
                grdViewTimesheetSummary.PageSize = 8;

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                BindTimesheetSummaryFilterList();
                if (ddlSearchCriteria.SelectedIndex >= 0)
                {
                    BindTimesheetSummaryGrid(ddlSearchCriteria.SelectedValue);
                }
                //if (bl.CheckUserHaveAdd(usernam, "SUPPINFO"))
                //{
                //    lnkBtnAddTimesheet.Enabled = false;
                //    lnkBtnAddTimesheet.ToolTip = "You are not allowed to make Add New ";
                //}
                //else
                //{
                //    lnkBtnAddTimesheet.Enabled = true;
                //    lnkBtnAddTimesheet.ToolTip = "Click to Add New ";
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

    protected void grdViewTimesheetSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnSaveTimesheet_Click(object sender, EventArgs e)
    {

    }
    protected void lnkBtnAddTimesheet_Click(object sender, EventArgs e)
    {
        try
        {
            string connection = Request.Cookies["Company"].Value;
            string username = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(connection);
            if (!bl.IsTimesheetSummaryExists(username, DateTime.Today))
            {
                List<DataTable> dtTimesheetDetails = bl.GetNewTimesheetDetailsForMonth(DateTime.Today, username);
                BindGridViews(dtTimesheetDetails);
                BindTimeControls();
                ModalPopupExtender1.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    @"alert('Timesheet has been already available for the current week.');", true);
            }
        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindGridViews(List<DataTable> dtTimesheetDetails)
    {
        foreach (DataTable dtTsForTheDay in dtTimesheetDetails)
        {
            switch (dtTsForTheDay.DisplayExpression)
            {

                case "1":
                    GridViewTimesheetDetailDay1.Visible = true;
                    GridViewTimesheetDetailDay1.DataSource = dtTsForTheDay;
                    GridViewTimesheetDetailDay1.DataBind();
                    ViewState["TimeSheetDetailsDay1"] = dtTsForTheDay;
                    lblDay1Header.Text = dtTsForTheDay.TableName;
                    lblTSDateTime.Text = dtTsForTheDay.TableName;
                    break;
                case "2":
                    GridViewTimesheetDetailDay2.Visible = true;
                    GridViewTimesheetDetailDay2.DataSource = dtTsForTheDay;
                    GridViewTimesheetDetailDay2.DataBind();
                    ViewState["TimeSheetDetailsDay2"] = dtTsForTheDay;
                    lblDay2Header.Text = dtTsForTheDay.TableName;
                    break;
                case "3":
                    GridViewTimesheetDetailDay3.Visible = true;
                    GridViewTimesheetDetailDay3.DataSource = dtTsForTheDay;
                    GridViewTimesheetDetailDay3.DataBind();
                    ViewState["TimeSheetDetailsDay3"] = dtTsForTheDay;
                    lblDay3Header.Text = dtTsForTheDay.TableName;
                    break;
                case "4":
                    GridViewTimesheetDetailDay4.Visible = true;
                    GridViewTimesheetDetailDay4.DataSource = dtTsForTheDay;
                    GridViewTimesheetDetailDay4.DataBind();
                    ViewState["TimeSheetDetailsDay4"] = dtTsForTheDay;
                    lblDay4Header.Text = dtTsForTheDay.TableName;
                    break;
                case "5":
                    GridViewTimesheetDetailDay5.Visible = true;
                    GridViewTimesheetDetailDay5.DataSource = dtTsForTheDay;
                    GridViewTimesheetDetailDay5.DataBind();
                    ViewState["TimeSheetDetailsDay5"] = dtTsForTheDay;
                    lblDay5Header.Text = dtTsForTheDay.TableName;
                    break;
                case "6":
                    GridViewTimesheetDetailDay6.Visible = true;
                    GridViewTimesheetDetailDay6.DataSource = dtTsForTheDay;
                    GridViewTimesheetDetailDay6.DataBind();
                    ViewState["TimeSheetDetailsDay6"] = dtTsForTheDay;
                    lblDay6Header.Text = dtTsForTheDay.TableName;
                    break;
                case "7":
                    GridViewTimesheetDetailDay7.Visible = true;
                    GridViewTimesheetDetailDay7.DataSource = dtTsForTheDay;
                    GridViewTimesheetDetailDay7.DataBind();
                    ViewState["TimeSheetDetailsDay7"] = dtTsForTheDay;
                    lblDay7Header.Text = dtTsForTheDay.TableName;
                    break;
                default:

                    break;
            }
        }
    }

    private void BindTimeControls()
    {
        List<string> hoursList = GetHoursList();
        List<string> minutesList = GetMinuteList();

        ddlStartTimeHour.DataSource = hoursList;
        ddlStartTimeHour.DataBind();
        ddlStartTimeHour.SelectedValue = "00";
        ddlStartTimeMinute.DataSource = minutesList;
        ddlStartTimeMinute.DataBind();
        ddlStartTimeMinute.SelectedValue = "00";

        ddlEndTimeHour.DataSource = hoursList;
        ddlEndTimeHour.DataBind();
        ddlEndTimeHour.SelectedValue = "00";
        ddlEndTimeMinute.DataSource = minutesList;
        ddlEndTimeMinute.DataBind();
        ddlEndTimeMinute.SelectedValue = "00";

        ddlTotalTimeHour.DataSource = hoursList;
        ddlTotalTimeHour.DataBind();
        ddlTotalTimeHour.SelectedValue = "00";
        ddlTotalTimeMinute.DataSource = minutesList;
        ddlTotalTimeMinute.DataBind();
        ddlTotalTimeMinute.SelectedValue = "00";
    }

    protected void ddlSearchCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdViewTimesheetSummary_RowCommand1(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewTimesheetDetailDay1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    private void BindTimesheetSummaryGrid(string TimesheetYear)
    {
        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet ds = bl.GetTimesheetSummary(TimesheetYear, usernam);
        if (ds != null && ds.Tables.Count > 0)
        {
            grdViewTimesheetSummary.DataSource = ds.Tables[0];
            grdViewTimesheetSummary.DataBind();
            UpdatePanelMain.Update();
        }
        else
        {
            grdViewTimesheetSummary.DataSource = null;
            grdViewTimesheetSummary.DataBind();
            UpdatePanelMain.Update();
        }

    }

    private void BindTimesheetSummaryFilterList()
    {
        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataTable dt = bl.GetYearsConstant();
        if (dt != null)
        {
            ddlSearchCriteria.DataSource = dt;
            ddlSearchCriteria.DataTextField = "Year";
            ddlSearchCriteria.DataValueField = "Year";
            ddlSearchCriteria.DataBind();
            ddlSearchCriteria.SelectedValue = DateTime.Today.Year.ToString();
            UpdatePanelMain.Update();
        }
    }

    protected void btnAddEntry_Click(object sender, EventArgs e)
    {
        string errMsg = string.Empty;
        lblStatus.Text = string.Empty;
        if (HasValidInput(ref errMsg))
        {
            AddTsEntryToGrid();
            ClearTsEntryFields();
            updtPnlTSEntry.Update();
        }
        else
        {
            lblStatus.Text = errMsg;
        }
    }

    private void AddTsEntryToGrid()
    {
        if (ViewState["TimeSheetDetailsDay1"] != null)
        {
            DataTable dtTimesheetDetail = ViewState["TimeSheetDetailsDay1"] as DataTable;
            DataRow drNewTSEntry = dtTimesheetDetail.NewRow();

            //  Update values from the controls
            //  Id, TimeSheetSummaryId, TsDate, StartTime, EndTime, TotalMinutes, Description, Status, ApproverComments, IsActive
            drNewTSEntry["Id"] = string.IsNullOrEmpty(hdnfTSDetailId.Value) ? "0" : hdnfTSDetailId.Value;
            drNewTSEntry["TimeSheetSummaryId"] = string.IsNullOrEmpty(hdnfTSSummaryId.Value) ? "0" : hdnfTSSummaryId.Value;
            drNewTSEntry["TsDate"] = lblTSDateTime.Text;
            if (rbtnTimeEntry1.Checked)
            {
                DateTime startTime = GetStartTime();
                drNewTSEntry["StartTime"] = startTime.ToString("hh:ss tt");

                DateTime endTime = GetEndTime();
                drNewTSEntry["EndTime"] = endTime.ToString("hh:ss tt");

                double totalHours= GetTotalDurationInHours(startTime, endTime);
                drNewTSEntry["TotalHours"] = totalHours;
            }
            else
            {
                drNewTSEntry["EndTime"] = string.Empty;
                drNewTSEntry["StartTime"] = string.Empty;
                double totalHours = GetTotalDurationInHours();
                drNewTSEntry["TotalHours"] = totalHours.ToString();
            }
            drNewTSEntry["Description"] = txtDescription.Text.Trim();
            drNewTSEntry["Status"] = string.IsNullOrEmpty(hdnfStatus.Value) ? "Entered" : hdnfStatus.Value;
            drNewTSEntry["ApproverComments"] = string.IsNullOrEmpty(hdnfApproverCmts.Value) ? "NA" : hdnfApproverCmts.Value;
            drNewTSEntry["IsActive"] = true;

            dtTimesheetDetail.Rows.Add(drNewTSEntry);
            ViewState["TimeSheetDetailsDay1"] = dtTimesheetDetail;

            GridViewTimesheetDetailDay1.DataSource = dtTimesheetDetail;
            GridViewTimesheetDetailDay1.DataBind();
        }
    }

    private double GetTotalDurationInHours(DateTime startTime, DateTime endTime)
    {
        TimeSpan dateDiff = endTime - startTime;
        return dateDiff.TotalHours;
    }

    private void ClearTsEntryFields()
    {
        txtDescription.Text = string.Empty;
        rbtnTimeEntry1.Checked = true;
        BindTimeControls();
        hdnfApproverCmts.Value = string.Empty;
        hdnfStatus.Value = string.Empty;
        hdnfIsNewEntry.Value = string.Empty;
        hdnfTSDetailId.Value = string.Empty;
        lblStatus.Text = string.Empty;

        btnAddEntry.Text = "Add";
    }

    private double GetTotalDurationInHours()
    {
        if (ddlTotalTimeHour.SelectedValue.Equals("00") && ddlTotalTimeMinute.SelectedValue.Equals("00"))
        {
            return 0;
        }
        else
        {
            int hour = 0;
            int.TryParse(ddlTotalTimeHour.SelectedValue, out hour);

            int minute = 0;
            int.TryParse(ddlTotalTimeMinute.SelectedValue, out minute);

            double totalHours = 0;
            totalHours = (hour) + (minute / 60);
            return totalHours;
        }
    }

    private bool HasValidInput(ref string errMsg)
    {
        if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
        {
            if (rbtnTimeEntry1.Checked)
            {
                DateTime startDateTime = GetStartTime();
                DateTime endDateTime = GetEndTime();

                if (startDateTime >= endDateTime)
                {
                    errMsg = "Start time should not be greater than or equal to End time";
                    return false;
                }
            }
            else if (rbtnTimeEntry2.Checked)
            {
                if (ddlTotalTimeHour.SelectedValue.Equals("00") && ddlTotalTimeMinute.SelectedValue.Equals("00"))
                {
                    errMsg = "Total time duration should not be zero";
                    return false;
                }
            }
        }
        else
        {
            errMsg = "Description is mandatory";
            return false;
        }
        return true;
    }

    private DateTime GetStartTime()
    {
        DateTime startDateTime;
        int hour = int.Parse(ddlStartTimeHour.SelectedValue);
        int minute = int.Parse(ddlStartTimeMinute.SelectedValue);
        string meridian = ddlStartTimeMeridian.SelectedValue;

        if (DateTime.TryParse(lblTSDateTime.Text, out startDateTime))
        {
            string dateTimeString = string.Format("{0}-{1}-{2} {3}:{4}:{5} {6}", startDateTime.Day, startDateTime.Month, startDateTime.Year, hour, minute, 0, meridian);
            DateTime.TryParse(dateTimeString, out startDateTime);
        }
        return startDateTime;
    }

    private DateTime GetEndTime()
    {
        DateTime endDateTime;
        int hour = int.Parse(ddlEndTimeHour.SelectedValue);
        int minute = int.Parse(ddlEndTimeMinute.SelectedValue);
        string meridian = ddlEndTimeMeridian.SelectedValue;

        if (DateTime.TryParse(lblTSDateTime.Text, out endDateTime))
        {
            string dateTimeString = string.Format("{0}-{1}-{2} {3}:{4}:{5} {6}", endDateTime.Day, endDateTime.Month, endDateTime.Year, hour, minute, 0, meridian);
            DateTime.TryParse(dateTimeString, out endDateTime);
        }
        return endDateTime;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearTsEntryFields();
    }

    protected void rbtnTimeEntry_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnTimeEntry1.Checked)
        {
            pnlStartTImeControls.Enabled = true;
            pnlEndTimeControls.Enabled = true;
            pnlTotalTimeControls.Enabled = false;
        }
        else if (rbtnTimeEntry2.Checked)
        {
            pnlEndTimeControls.Enabled = false;
            pnlStartTImeControls.Enabled = false;
            pnlTotalTimeControls.Enabled = true;
        }
        updtPnlTSEntry.Update();
    }

    private List<string> GetHoursList()
    {
        List<string> hourList = new List<string>();
        int hours = 0;
        while (hours <= 12)
        {
            hourList.Add(hours.ToString("00"));
            hours++;
        }
        return hourList;
    }

    private List<string> GetMinuteList()
    {
        List<string> minuteList = new List<string>();
        int minute = 0;
        while (minute <= 60)
        {
            minuteList.Add(minute.ToString("00"));
            minute = minute + 5;
        }
        return minuteList;
    }

    protected void GridViewTimesheetDetailDay1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = int.Parse(e.CommandArgument.ToString());
        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        if (e.CommandName.Equals("EditRecord"))
        {
            EditTSEntryDetail(id,row);
            updtPnlTSEntry.Update();
        }
        else if (e.CommandName.Equals("DeleteRecord"))
        {

        }
    }

    private void EditTSEntryDetail(int id, GridViewRow row)
    {
        hdnfTSDetailId.Value = row.Cells[0].Text;
        lblTSDateTime.Text = row.Cells[1].Text;
        SetStartTime(row.Cells[2].Text);
        SetEndTime(row.Cells[3].Text);
        txtDescription.Text = row.Cells[4].Text;
        SetTotalHours(row.Cells[5].Text);
        hdnfStatus.Value = row.Cells[6].Text;
        hdnfApproverCmts.Value = row.Cells[7].Text;

        if (string.IsNullOrEmpty(row.Cells[2].Text) || string.IsNullOrEmpty(row.Cells[3].Text))
        {
            rbtnTimeEntry1.Checked = false;                
        }

        btnAddEntry.Text = "Update";
    }

    private void SetTotalHours(string inputValue)
    {
        double totalHours = 0.0;
        if (double.TryParse(inputValue, out totalHours))
        {
            double hour = totalHours / 1;
            double minute = (totalHours % 1) * 60;
            ddlTotalTimeHour.SelectedValue = hour.ToString("00");
            ddlTotalTimeMinute.SelectedValue = minute.ToString("00");
        }
    }

    private void SetEndTime(string inputValue)
    {
        if (!string.IsNullOrEmpty(inputValue))
        {
            //"hh:ss tt"
            ddlEndTimeHour.SelectedValue = inputValue.Split(new string[] { ":" }, StringSplitOptions.None)[0];
            ddlEndTimeMinute.SelectedValue = inputValue.Split(new string[] { ":" }, StringSplitOptions.None)[1].Substring(0, 2);
            ddlEndTimeMeridian.SelectedValue = inputValue.Split(new string[] { " " }, StringSplitOptions.None)[1];
        }
    }

    private void SetStartTime(string inputValue)
    {
        if (!string.IsNullOrEmpty(inputValue))
        {
            //"hh:ss tt"
            ddlStartTimeHour.SelectedValue = inputValue.Split(new string[] { ":" }, StringSplitOptions.None)[0];
            ddlStartTimeMinute.SelectedValue = inputValue.Split(new string[] { ":" }, StringSplitOptions.None)[1].Substring(0, 2);
            ddlStartTimeMeridian.SelectedValue = inputValue.Split(new string[] { " " }, StringSplitOptions.None)[1];
        }
        }

}