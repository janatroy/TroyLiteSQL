using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Collections.Generic;
using TroyLite.Inventory.BusinessLayer;
using TroyLite.Inventory.Entities;
//using NLog;

public partial class TimeSheet : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    //NLog.Logger ObjNLog = LogManager.GetLogger("TimeSheet");

    #region "Page Init and Load events"

    // Persist data for every postback when click save or navigate buttons.
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GrdTse.Init += GrdTse_Init;
            // gridWeeklyTimesheets.Init += gridWeeklyTimesheets_Init;
        }
    }

    protected void gridWeeklyTimesheets_Init(object sender, EventArgs e)
    {

        try
        {
            if (IsPostBack)
            {
                Bindweeklytimesheet(GetEmployeeNumber(), "", "Init");
                //ObjNLog.Info(string.Format("Bindweeklytimesheet executed Sucessfully, EmployeeNumber : {0}", GetEmployeeNumber()));
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    // Page load event for initilization
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                #region "Load Tab Master 1"
                GrdTse.PageSize = 8;

                ResetSearch();
                BindTse();
                //ObjNLog.Info("BindTse bounded the TimeSheet entries on GrdTse excuted.");
                // loadEmp();
                DisableForOffline();

                WeeklyCalendar.VisibleDate = DateTime.Now;
                WeeklyCalendar.SelectedDate = DateTime.Now;

                BusinessLogic bl = new BusinessLogic(sDataSource);

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveAdd(usernam, "TMENTRY"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New item ";
                }
                #endregion "Load Tab Master 1"
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }


    }

    #endregion

    #region "Tab 1 - Weekly TimeSheet Entry"
    protected void GrdTse_Init(object sender, EventArgs e)
    {
        //   if (IsPostBack)
        //     BindTse();
    }

    private void BindTse()
    {

        try
        {

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLayer bl = new BusinessLayer(sDataSource);

            int EmployeeNumber = GetEmployeeNumber();

            string sCriteriaKey = string.Empty;
            string sCriteriaValue = string.Empty;
            string sType = string.Empty;

            if (ddlTimeSheetKeyField.Text != "-- All --")
            {
                sCriteriaKey = Convert.ToString(ddlTimeSheetKeyField.SelectedItem.Value).Split(':').ElementAt<string>(0);
                sCriteriaValue = txtTimeSheetValueField.Text;
                sType = Convert.ToString(ddlTimeSheetKeyField.SelectedItem.Value).Split(':').ElementAt<string>(1);
            }

            List<SearchTimeSheetEntity> lstSearchTSE = new List<SearchTimeSheetEntity>();

            lstSearchTSE = bl.SearchTimeSheetEfforts(EmployeeNumber, sCriteriaKey, sCriteriaValue, sType);

            //ObjNLog.Info(string.Format("SearchTimeSheetEfforts method executed Sucessfully, No of Timesheet entries: {1}, EmployeeNumber:{0}", EmployeeNumber, lstSearchTSE.Count.ToString()));

            UpdatePanelPage.Update();

            tbMain.Visible = true;
            GrdTse.Visible = true;

            pnsTime.Style["display"] = "none";

            tbMain.TabIndex = 0;

            GrdTse.DataSource = lstSearchTSE;
            GrdTse.DataBind();
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
            throw ex;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindTse();
            //ObjNLog.Info("BindTse method executed Successfully, Bounded data on GrdTse for {0}", GetEmployeeNumber());
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised from {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string strMessage;
                int iResult = SaveOrSubmitTimeSheet("Saved", out strMessage);

                string TSEDate = WeeklyCalendar.SelectedDate.ToShortDateString();

                if (iResult > 0)
                {
                    //ObjNLog.Info("SaveOrSubmitTimeSheet executed. Time Sheet entry updated Successfully....in Saved Mode");

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Time Sheet Entry Details Saved Successfully. Employee No " + GetEmployeeNumber().ToString() + " Date=" + TSEDate + "');", true);
                    Reset();
                    ResetSearch();
                    BindTse();

                    //ObjNLog.Info("BindTse method bounded GrdTse view Successfully");

                    pnlSearch.Style["display"] = "block";
                    pnlSearch.Visible = true;
                    pnlSearch.Enabled = true;

                    lnkBtnAdd.Visible = true;
                    tbMain.Visible = true;
                    GrdTse.Enabled = true;
                    GrdTse.Visible = true;
                    pnlTimesheetEntryList.Visible = true;
                    pnlTimesheetEntryList.Enabled = true;

                    ModalPopupExtender1.Hide();
                    UpdatePanelPage.Update();
                }
                else
                {
                    //ObjNLog.Info("Error occurred while updating Timesheet entry..");

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Error occurred while added Timesheet entry.." + GetEmployeeNumber().ToString() + " and Date " + TSEDate + "');", true);
                    //UpdatePanelTS.Update();
                    //ModalPopupExtender1.Show();
                }
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Reset();
            pnlTimesheetEntryList.Visible = true; // show grid list
            pnsTime.Style["display"] = "none";

            pnlSearch.Style["display"] = "block";
            pnlSearch.Visible = true;
            pnlSearch.Enabled = true;

            lnkBtnAdd.Visible = true;
            tbMain.Visible = true;
            GrdTse.Enabled = true;
            GrdTse.Visible = true;
            pnlTimesheetEntryList.Visible = true;
            pnlTimesheetEntryList.Enabled = true;

            ModalPopupExtender1.Hide();
            UpdatePanelPage.Update();

            //ObjNLog.Info("TimeSheet entry not updated. Cancel button invoked.");
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int EmployeeNumber = GetEmployeeNumber();

            pnlTimesheetEntryList.Visible = true;
            GrdTse.Visible = true;
            tbMain.Visible = true;
            pnsTse.Visible = true;

            pnsTime.Style["display"] = "block";
            pnlTimesheetEntryList.Style["display"] = "block";
            pnlTimeSheetApproval.Style["display"] = "none";

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

            Employeerow.Style["display"] = "block";
            Rowrejectreason.Visible = false;

            Bindweeklytimesheet(EmployeeNumber, "", "Add");

            //ObjNLog.Info("Bindweeklytimesheet method executed! Add new Timesheet entry for Employee Number: {0}, Add Mode.", EmployeeNumber);

            ModalPopupExtender1.Show();
            UpdatePanelPage.Update();
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    void Bindweeklytimesheet(int EmployeeNumber, string sFromToDateDesc, string sMode)
    {
        try
        {
            DateTime dtBegining;
            DateTime dtEnd;

            if (WeeklyCalendar.SelectedDate != null)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                BusinessLayer bl = new BusinessLayer(sDataSource);

                string sWeekID = string.Empty;
                DateTime dtSelectedDate;

                DataSet dstEmployeeDetails = bl.GetEmployeeDetail(EmployeeNumber);
                //ObjNLog.Info(string.Format("GetEmployeeDetail method executed successfully, Employee Number : {0}", EmployeeNumber));

                if (dstEmployeeDetails != null && dstEmployeeDetails.Tables.Count > 0 && dstEmployeeDetails.Tables[0].Rows.Count > 0)
                    lblEmployee.Text = dstEmployeeDetails.Tables[0].Rows[0]["EmpFirstname"].ToString() + " " + dstEmployeeDetails.Tables[0].Rows[0]["EmpSurname"].ToString();

                if (sMode == "Edit")
                    dtSelectedDate = bl.GetSelectedDate(EmployeeNumber, sFromToDateDesc, out sWeekID);
                else
                    dtSelectedDate = WeeklyCalendar.SelectedDate;
                
                DateTimeExtension.GetWeek(dtSelectedDate, new CultureInfo("fr-FR"), out dtBegining, out dtEnd);

                Bindweeklytimesheetentry(EmployeeNumber, true, dtBegining, dtEnd, string.Empty);
            }
            else
            {
                DateTimeExtension.GetWeek(DateTime.Now, new CultureInfo("fr-FR"), out dtBegining, out dtEnd);
                Bindweeklytimesheetentry(EmployeeNumber, true, dtBegining, dtEnd, string.Empty);
            }
            
            //ObjNLog.Info(string.Format("BindWeeklytimesheetentry executed Successfully, in {0} Mode", sMode));
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    void Approveweeklytimesheet(int EmployeeNumber, string sFromToDateDesc, string sMode)
    {
        try
        {
            DateTime dtBegining;
            DateTime dtEnd;

            if (WeeklyCalendar.SelectedDate != null)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                BusinessLayer bl = new BusinessLayer(sDataSource);

                string sWeekID = string.Empty;

                DateTime dtSelectedDate;

                if (sMode == "Edit")
                    dtSelectedDate = bl.GetSelectedDate(EmployeeNumber, sFromToDateDesc, out sWeekID);
                else
                    dtSelectedDate = DateTime.Now;

                DateTimeExtension.GetWeek(dtSelectedDate, new CultureInfo("fr-FR"), out dtBegining, out dtEnd);

                Bindweeklytimesheetentry(EmployeeNumber, true, dtBegining, dtEnd, string.Empty);
            }
            else
            {
                DateTimeExtension.GetWeek(DateTime.Now, new CultureInfo("fr-FR"), out dtBegining, out dtEnd);
                Bindweeklytimesheetentry(EmployeeNumber, true, dtBegining, dtEnd, string.Empty);
            }

            //ObjNLog.Info(string.Format("Approveweeklytimesheet executed Successfully for Employee Number {2}, in {0} Mode, Date Range {1}", sMode, sFromToDateDesc, EmployeeNumber));
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }
    }

    protected void GrdTse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = GrdTse.SelectedRow;
            string TseDate = string.Empty;

            int empNo = Convert.ToInt32(GrdTse.DataKeys[0].Value.ToString());

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            DataSet ds = bl.GetTSEDet(empNo, TseDate, "");

            BindTse();

            lnkBtnAdd.Visible = false;
            pnsTse.Visible = true;

            pnsTime.Visible = true;
            pnsTime.Enabled = true;

            pnlTimesheetEntryList.Visible = false;
            pnlTimesheetEntryList.Enabled = false;

            lnkBtnAdd.Visible = false;

            GrdTse.Visible = false;
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdTse.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindTse();
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdTse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdTse.PageIndex = e.NewPageIndex;
            BindTse();
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    protected void GrdTse_RowCreated(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdTse, e.Row, this);
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void GrdTse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gridView = (GridView)sender;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                string strApproved = "return IsSubmitted('" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Approved")) + "', '{0}');";
                ((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = string.Format(strApproved, "Edited");
                //((ImageButton)e.Row.FindControl("lnkB")).OnClientClick = string.Format(strApproved, "Deleted");
                ((ImageButton)e.Row.FindControl("lnkB")).CommandName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Approved"));

                ((ImageButton)e.Row.FindControl("btnEdit")).CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DateRange"));
                ((ImageButton)e.Row.FindControl("lnkB")).CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "WeekID"));

                if (bl.CheckUserHaveEdit(usernam, "TMENTRY"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "TMENTRY"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                if (Convert.ToString(e.Row.Cells[3].Text).ToLower() == "saved")
                    e.Row.Cells[1].Text = "";
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //Edit event of GrdTse griview
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            string strDateRange = Convert.ToString(((ImageButton)sender).CommandArgument);

            //ObjNLog.Info(string.Format("Edit functionality invoked for Date Range = {0}", strDateRange));

            pnlTimesheetEntryList.Visible = true;
            pnsTime.Style["display"] = "block";
            pnlTimeSheetEntry.Style["display"] = "block";
            pnlTimeSheetApproval.Style["display"] = "none";

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

            Employeerow.Style["display"] = "block";
            Rowrejectreason.Visible = false;

            Bindweeklytimesheet(GetEmployeeNumber(), strDateRange, "Edit");

            //ObjNLog.Info(string.Format("Edit functionality: Bindweeklytimesheet method executed successfully"));

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bindweeklytimesheet method executed successfully.')", true);

            ModalPopupExtender1.Show();
            UpdatePanelPage.Update();
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkB_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string strWeekID = Convert.ToString(((ImageButton)sender).CommandArgument);
            string strStatus = Convert.ToString(((ImageButton)sender).CommandName);

            if (strStatus.ToLower() == "submitted")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Submitted Record cannot be Deleted!');", true);
            }
            else
            {
                string strMessage;
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                BusinessLayer bl = new BusinessLayer(sDataSource);

                WeeklyTimeSheetEntity clsWTSE = new WeeklyTimeSheetEntity();
                int EmployeeNumber = GetEmployeeNumber();

                clsWTSE.EmployeeNumber = EmployeeNumber ;
                clsWTSE.WeekID = strWeekID;

                int iResult = bl.DeleteWeeklyTimesheet(clsWTSE, out strMessage);

                if (iResult > 0)
                {
                    //ObjNLog.Info(string.Format("Time Sheet Entry Details deleted successfully for Employee Number {0}", EmployeeNumber));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Time Sheet Entry Details deleted Successfully.');", true);
                    BindTse();

                    btnSave.Enabled = true;

                    Reset();
                    ResetSearch();
                    BindTse();
                    GrdTse.Visible = true;

                    pnlSearch.Style["display"] = "block";
                    pnlSearch.Visible = true;
                    pnlSearch.Enabled = true;

                    lnkBtnAdd.Visible = true;
                    tbMain.Visible = true;
                    GrdTse.Enabled = true;
                    GrdTse.Visible = true;
                    pnlTimesheetEntryList.Visible = true;
                    pnlTimesheetEntryList.Enabled = true;


                    UpdatePanelPage.Update();
                }else
                {
                    //ObjNLog.Error(string.Format("Error occurred while deleting Time Sheet Details"));
                }
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void Bindweeklytimesheetentry(int EmployeeNumber, bool isCalender1, DateTime dBegining, DateTime dEnd, string sSelectMode)
    {
        try
        {
            int iStartIndex = 1;
            string sStartIndexDate = string.Empty;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            DataSet dsEmployeeWeeklyEfforts = new DataSet();
            DataTable dtReport = new DataTable();
            BusinessLayer bl = new BusinessLayer(sDataSource);

            DateTime dtSelectedDate;
            dsEmployeeWeeklyEfforts = bl.GetTSEWeekDataByEmpno(EmployeeNumber, dBegining.ToString(), dEnd.ToString(), DateTimeExtension.CurrentWeekwithYear(dBegining), out dtSelectedDate);

            if (dsEmployeeWeeklyEfforts != null)
            {
                if (dsEmployeeWeeklyEfforts.Tables[0].Columns.Count > 1 && dsEmployeeWeeklyEfforts.Tables[0].Rows.Count > 0)
                {
                    WeeklyCalendar.VisibleDate = dtSelectedDate;
                    WeeklyCalendar.SelectedDate = dtSelectedDate;

                    dtReport = dsEmployeeWeeklyEfforts.Tables[0];
                }
                else
                {
                    iStartIndex = 1;
                    dsEmployeeWeeklyEfforts = GetWeekEntry(dBegining);

                    dtReport = dsEmployeeWeeklyEfforts.Tables[0];
                }
            }
            else
            {
                iStartIndex = 1;
                dsEmployeeWeeklyEfforts = GetWeekEntry(dBegining);

                dtReport = dsEmployeeWeeklyEfforts.Tables[0];
            }

            // clear any existing columns
            gridWeeklyTimesheets.Columns.Clear();

            for (int i = 0; i < dtReport.Columns.Count; i++)
            {
                TemplateField tf = new TemplateField();
                // create the data rows
                tf.ItemTemplate =
                    new DynamicSGridViewLabelTemplate(DataControlRowType.DataRow,
                    dtReport.Columns[i].ColumnName, dtReport.Columns[i].DataType.Name);
                // create the header
                tf.HeaderTemplate =
                    new DynamicSGridViewLabelTemplate(DataControlRowType.Header,
                    dtReport.Columns[i].ColumnName, dtReport.Columns[i].DataType.Name);
                // add to the GridView
                gridWeeklyTimesheets.Columns.Add(tf);
            }

            gridWeeklyTimesheets.RowCreated += gridWeeklyTimesheets_RowCreated;

            // bind and display the data
            gridWeeklyTimesheets.DataSource = dtReport;
            gridWeeklyTimesheets.DataBind();

            //ObjNLog.Info(string.Format("Bindweeklytimesheetentry method completed, No. of records binded: {0}", dtReport.Rows.Count.ToString()));

            gridWeeklyTimesheets.Visible = true;
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }
    }

    DataSet GetWeekEntry(DateTime dtWorkDateStartIndex)
    {
        DataSet dsEmployeeWeeklyEfforts = new DataSet();
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataTable dtWeekHoursDesc = new DataTable();

            dsEmployeeWeeklyEfforts.Tables.Add(dtWeekHoursDesc);

            DataColumn dcText = new DataColumn();
            dcText.ColumnName = "Range";
            dcText.Caption = "Range";
            dcText.DataType = typeof(string);
            dsEmployeeWeeklyEfforts.Tables[0].Columns.Add(dcText);

            for (int jj = 0; jj < 7; jj++)
            {
                dcText = new DataColumn();

                dcText.ColumnName = DateTime.Parse(dtWorkDateStartIndex.ToString()).ToString("dd/MM/yyyy");
                dcText.Caption = DateTime.Parse(dtWorkDateStartIndex.ToString()).ToString("dd/MM/yyyy");
                dcText.DataType = typeof(string);

                dsEmployeeWeeklyEfforts.Tables[0].Columns.Add(dcText);
                dtWorkDateStartIndex = Convert.ToDateTime(dtWorkDateStartIndex.AddDays(1).ToShortDateString().ToString());
            }

            DataSet dstRangeLookup = bl.GetTimeSheetRangeLookup();

            DataRow drRange = null;
            if (dstRangeLookup != null && dstRangeLookup.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dstRangeLookup.Tables[0].Rows.Count; i++)
                {
                    drRange = dsEmployeeWeeklyEfforts.Tables[0].NewRow();

                    drRange["Range"] = dstRangeLookup.Tables[0].Rows[i]["Range"].ToString();

                    dsEmployeeWeeklyEfforts.Tables[0].Rows.Add(drRange);
                }
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }

        return dsEmployeeWeeklyEfforts;
    }

    protected void gridWeeklyTimesheets_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                gridWeeklyTimesheets.Columns[0].HeaderStyle.Width = new Unit(90);

                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    string sWorkDate = ((DynamicSGridViewLabelTemplate)(((System.Web.UI.WebControls.TemplateField)(gridWeeklyTimesheets.Columns[i])).ItemTemplate)).columnName;
                    string sHoursDesc = DataBinder.Eval(e.Row.DataItem, sWorkDate).ToString();

                    DataControlFieldCell tcWorkDate = (DataControlFieldCell)(e.Row.Cells[i]);
                    TextBox txtWorkDate = (TextBox)(e.Row.Cells[i].Controls[0]);

                    int rowIndex = Convert.ToInt32(e.Row.DataItemIndex) + 1;

                    string txtOnKeyPress = "setHeight(this," + rowIndex + "," + i.ToString() + ");";
                    string evtHandler = "updateValue(this," + rowIndex + "," + i.ToString() + ")";
                    string txtBlur = "setTextblur(this," + rowIndex + "," + i.ToString() + ");";

                    txtWorkDate.TextMode = TextBoxMode.MultiLine;
                    txtWorkDate.Width = Unit.Pixel(152);
                    txtWorkDate.Height = Unit.Pixel(30);

                    txtWorkDate.Text = sHoursDesc;
                    txtWorkDate.ID = "txtDesc" + i.ToString();

                    txtWorkDate.Attributes.Add("onFocus", evtHandler);
                    txtWorkDate.Attributes.Add("onKeypress", txtOnKeyPress);
                    txtWorkDate.Attributes.Add("onBlur", txtBlur);

                    tcWorkDate.Controls.Add(txtWorkDate);
                }
            }

            #region "Old Code"
            //        string sHoursDesc;

            //        DateTime dtWorkDate = WeeklyCalendar.SelectedDate;

            //        if (e.Row.RowType == DataControlRowType.DataRow)
            //        {
            //            if (ViewState["Mode"] == "Add")
            //                dtWorkDate = Convert.ToDateTime(ViewState["StartweekDate"]);
            //            else if (ViewState["Mode"] == "Edit")
            //                dtWorkDate = Convert.ToDateTime(ViewState["StartweekDate"]);

            //            //sHoursDesc = ((string)(DataBinder.Eval(e.Row.DataItem, dtWorkDate.ToShortDateString().Replace("/", "-")).ToString()));

            //            TableCell tcWorkDate;
            //            //TextBox txtBox = new TextBox();

            //            //tcWorkDate = (DataControlFieldCell)(e.Row.Controls[1]);

            //            //txtBox.TextMode = TextBoxMode.MultiLine;
            //            //txtBox.Width = Unit.Pixel(95);
            //            //txtBox.Height = Unit.Pixel(30);

            //            //txtBox.Text = sHoursDesc;
            //            //txtBox.ID = "txtDesc";

            //            ////txtBox.Load += txtBox_Load;
            //            ////txtBox.TextChanged += txtBox_TextChanged;

            //            ////txtBox.Attributes.Add("onclick", "Func1(this);");
            //            //txtBox.Attributes.Add("onKeypress", "setHeight(this);");
            //            //txtBox.Attributes.Add("onBlur", "setTextblur(this);");

            //            ////TextBox txtDesc1 = (TextBox) e.Row.FindControl("txtDesc");

            //            ////if (txtBox!=null)
            //            //    tcWorkDate.Controls.Add(txtBox);
            //            //else

            //            for (int i = 1; i < e.Row.Cells.Count; i++)
            //            {
            //                string sWorkDate = ((DynamicGridViewTextTemplate)(((System.Web.UI.WebControls.TemplateField)(gridWeeklyTimesheets.Columns[i])).ItemTemplate))._ColName;

            //                sHoursDesc = DataBinder.Eval(e.Row.DataItem, sWorkDate).ToString();

            //                tcWorkDate = (DataControlFieldCell)(e.Row.Cells[i]);

            //                TextBox txtWorkDate = (TextBox)(e.Row.Cells[i].Controls[0]);
            //                //tcWorkDate.Controls.Clear();

            //                txtWorkDate.TextMode = TextBoxMode.MultiLine;
            //                txtWorkDate.Width = Unit.Pixel(161);
            //                txtWorkDate.Height = Unit.Pixel(30);

            //                txtWorkDate.Text = sHoursDesc;
            //                txtWorkDate.ID = "txtDesc";

            //                //txtBox.Load += txtBox_Load;
            //                //txtBox.TextChanged += txtBox_TextChanged;

            //                //txtBox.Attributes.Add("onclick", "Func1(this);");
            //                int rowIndex = Convert.ToInt32(e.Row.DataItemIndex) + 1;
            //                //string evtHandler = "updateValue('" + gridWeeklyTimesheets.ClientID + "'," + rowIndex + ")";

            //                string txtOnKeyPress = "setHeight(this," + rowIndex + "," + i.ToString() + ");";
            //                string evtHandler = "updateValue(this," + rowIndex + "," + i.ToString() + ")";

            //                txtWorkDate.Attributes.Add("onFocus", evtHandler);

            //                txtWorkDate.Attributes.Add("onKeypress", txtOnKeyPress);

            //                string txtBlur = "setTextblur(this," + rowIndex + "," + i.ToString() + ");";
            //                txtWorkDate.Attributes.Add("onBlur", txtBlur);

            //                tcWorkDate.Controls.Add(txtWorkDate);
            //            }

            //            //string evtHandler = string.Empty;

            //            //int rowIndex = Convert.ToInt32(e.Row.DataItemIndex) + 1;
            //            //for (int i = 1; i < e.Row.Cells.Count; i++)
            //            //{
            //               // evtHandler = "updateValue('" + gridWeeklyTimesheets.ClientID + "'," + rowIndex + ")";

            //              //  TextBox txtDesc = (TextBox)e.Row.FindControl("txtDay" + i.ToString());
            /////                txtDesc.Attributes.Add("onKeypress", evtHandler);
            //   //         }

            //            //if ((DataBinder.Eval(e.Row.DataItem, "BetweenRange") != null) && (DataBinder.Eval(e.Row.DataItem, "BetweenRange").ToString() == "Authorize"))
            //            //{
            //            //    for (int i = 1; i < e.Row.Cells.Count; i++)
            //            //    {
            //            //        var firstCell = e.Row.Cells[i];
            //            //        firstCell.Controls.Clear();

            //            //        DropDownList ddlAuthorize = new DropDownList();
            //            //        ddlAuthorize.Width = Unit.Pixel(55);

            //            //        ddlAuthorize.Items.Add("Yes");
            //            //        ddlAuthorize.Items.Add("No");

            //            //        firstCell.Controls.Add(ddlAuthorize);
            //            //    }
            //            //}
            //        }
            #endregion
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
        }
    }

    protected void WeeklyCalendar_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            ShowWeeklyCalendar();
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    void ShowWeeklyCalendar()
    {
        try
        {
            DateTime dtBegining;
            DateTime dtEnd;

            if (!WeeklyCalendar.SelectedDate.ToString().Equals("01/01/0001"))
            {
                DateTimeExtension.GetWeek(WeeklyCalendar.SelectedDate, new CultureInfo("fr-FR"), out dtBegining, out dtEnd);
                int EmployeeNumber = GetEmployeeNumber();


                Bindweeklytimesheetentry(EmployeeNumber, true, dtBegining, dtEnd, string.Empty);
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", "TimeSheet.aspx", ex.Message));
            throw ex;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                bool IsTxtEmpty = false;

                for (int iRow = 0; iRow < 9; iRow++)
                {
                    for (int iCol = 1; iCol < 8; iCol++)
                    {
                        if (string.IsNullOrWhiteSpace(((TextBox)gridWeeklyTimesheets.Rows[iRow].Cells[iCol].Controls[0]).Text))
                        {
                            IsTxtEmpty = true;
                            break;
                        }
                    }

                    if (IsTxtEmpty) break;
                }

                if (IsTxtEmpty)
                {
                    lblTimeSheetErrorMessage.Text = "Enter all the effort description before submitting the TimeSheet.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter Effort Descriptions.');", true);
                }
                else
                {
                    string strNotEnteredMessage;
                    string strMessage;

                    lblTimeSheetErrorMessage.Text = "";

                    #region Getting Weeks which are all not Submitted prior to current Week of Saving

                    bool blnTobeSubmitted = GetWeeksNotEntered(out strNotEnteredMessage);

                    #endregion Getting Weeks which are all not Submitted prior to current Week of Saving

                    int iResult = 0;
                    if (blnTobeSubmitted)
                    {
                        iResult = SaveOrSubmitTimeSheet("Submitted", out strMessage);
                        //ObjNLog.Info("Previous timesheet details entered with submitted status, hence, entered time sheet entry stored In Submitted Mode!");
                    }
                    else
                    {
                        iResult = SaveOrSubmitTimeSheet("Saved", out strMessage);
                        //ObjNLog.Info("Previous timesheet details not entered or not in submitted status, still entered time sheet entry stored In Saved Mode!");
                    }


                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + strNotEnteredMessage + "');", true);

                    Reset();
                    ResetSearch();
                    BindTse();

                    pnlSearch.Style["display"] = "block";
                    pnlSearch.Visible = true;
                    pnlSearch.Enabled = true;

                    lnkBtnAdd.Visible = true;
                    tbMain.Visible = true;
                    GrdTse.Enabled = true;
                    GrdTse.Visible = true;
                    pnlTimesheetEntryList.Visible = true;
                    pnlTimesheetEntryList.Enabled = true;

                    ModalPopupExtender1.Hide();
                    UpdatePanelPage.Update();
                }
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private bool GetWeeksNotEntered(out string strMessage)
    {
        bool blnTobeSubmitted = false;
        try
        {
            BusinessLayer clsBusinessLayer = new BusinessLayer(sDataSource);
            string strRetMessage = string.Empty;

            strMessage = "TimeSheet Submitted Successfully";

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            strRetMessage = clsBusinessLayer.GetTSWeekNotEnteredByEmp(GetEmployeeNumber(), DateTimeExtension.CurrentWeekwithYear(WeeklyCalendar.SelectedDate), WeeklyCalendar.SelectedDate);

            //ObjNLog.Info(strRetMessage);

            if (strRetMessage == "") blnTobeSubmitted = true;
            else strMessage = strRetMessage;
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }

        return blnTobeSubmitted;
    }

    #endregion "Tab 1 - Weekly TimeSheet"

    #region "Tab 2 - TimeSheet Approval"
    private void BindSubTseForApproval()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        BusinessLayer bl = new BusinessLayer(sDataSource);

        int EmployeeNumber = GetEmployeeNumber();
        string sCriteriaKey = string.Empty;
        string sCriteriaValue = string.Empty;
        string sType = string.Empty;

        if (ddlTimeSheetApproval.Text != "-- All --")
        {
            sCriteriaKey = Convert.ToString(ddlTimeSheetApproval.SelectedItem.Value).Split(':').ElementAt<string>(0);
            sCriteriaValue = txtTimeSheetApproval.Text;
            sType = Convert.ToString(ddlTimeSheetApproval.SelectedItem.Value).Split(':').ElementAt<string>(1);
        }

        List<SearchTimeSheetEntity> lstSearchTSA = new List<SearchTimeSheetEntity>();
        lstSearchTSA = bl.SearchTimeSheetApproval(EmployeeNumber, sCriteriaKey, sCriteriaValue, sType);

        
        GrdSubTSe.DataSource = lstSearchTSA;
        GrdSubTSe.DataBind();

        //ObjNLog.Info(string.Format("SearchTimeSheetApproval Method executed successfully! Employee Number = {0}, Search Criteria Field = {1}, Search Criteria Value = {2}, Type = {3}", EmployeeNumber, sCriteriaKey, sCriteriaValue, sType));
    }

    protected void GrdSubTSe_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gridView = (GridView)sender;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((ImageButton)e.Row.FindControl("lnkAppr")).CommandArgument = Convert.ToString(((SearchTimeSheetEntity)e.Row.DataItem).Empno);
            ((ImageButton)e.Row.FindControl("lnkAppr")).CommandName = Convert.ToString(((SearchTimeSheetEntity)e.Row.DataItem).DateRange);
        }
    }

    protected void lnkApproveLineItem_Click(object sender, ImageClickEventArgs e)
    {

        try
        {

            int iSubEmpNumber = Convert.ToInt32(((ImageButton)sender).CommandArgument);
            string sFromtodateDesc = Convert.ToString(((ImageButton)sender).CommandName);

            Bindweeklytimesheet(iSubEmpNumber, sFromtodateDesc, "Edit");
            hndEmpno.Value = Convert.ToString(iSubEmpNumber);
            tdTimeSheetTitle.InnerText = "TimeSheet Approval";
            lblTimeSheetErrorMessage.Visible = false;
            tdSubmitted.Visible = false;


            Employeerow.Style["display"] = "block";

            txtReject.Text = "";

            ModalPopupExtender1.Show();
            Rowrejectreason.Visible = true;
            UpdatePanelPage.Update();

        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            Employeerow.Style["display"] = "block";
            //tdRejected.Style["display"] = "block";
            Rowrejectreason.Style["display"] = "block";

            //Employeerow.Visible = true;
            //Rowrejectreason.Visible = true;
            ApproveOrRejectTimeSheet("Rejected");

            //ObjNLog.Info("Reject time sheet executed");
            //BindSubTseForApproval();

            //tbMain.Visible = true;
            //tbMain.ActiveTabIndex = 1;
            //GrdSubTSe.Visible = true;
            //GrdSubTSe.Enabled = true;

            //ModalPopupExtender1.Hide();
            ////Rowrejectreason.Style["display"] = "block";
            //UpdatePanelPage.Update();
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                ApproveOrRejectTimeSheet("Approved");

                tdTimeSheetTitle.InnerText = "TimeSheet Approval";
                lblTimeSheetErrorMessage.Visible = false;
                tdSubmitted.Visible = false;

                Employeerow.Style["display"] = "block";
                //tdRejected.Style["display"] = "block";
                Rowrejectreason.Style["display"] = "block";

                pnlSearch.Style["display"] = "none";
                pnlSearch.Visible = false;
                pnlSearch.Enabled = false;

                tbMain.Visible = true;
                GrdSubTSe.Enabled = true;
                GrdSubTSe.Visible = true;
                pnlTimesheetEntryList.Visible = false;
                pnlTimesheetEntryList.Enabled = false;

                pnlTimeSheetEntry.Style["display"] = "none";
                pnlTimeSheetApproval.Style["display"] = "block";
                pnlTimesheetApprove.Style["display"] = "block";


                tdTimeSheetTitle.InnerText = "TimeSheet Approval";
                lblTimeSheetErrorMessage.Visible = false;

                txtReject.Text = "";
                BindSubTseForApproval();
                //ObjNLog.Info("Approved time sheet executed");
                ModalPopupExtender1.Hide();
                UpdatePanelPage.Update();
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void ApproveOrRejectTimeSheet(string strStatus)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            BusinessLayer bl = new BusinessLayer(sDataSource);

            DateTime dtBegining;
            DateTime dtEnd;
            DateTimeExtension.GetWeek(WeeklyCalendar.SelectedDate, new CultureInfo("fr-FR"), out dtBegining, out dtEnd);
            string exMessage;

            WeeklyTimeSheetEntity clsWTSE = new WeeklyTimeSheetEntity();
            clsWTSE.EmployeeNumber = Convert.ToInt32(hndEmpno.Value);
            clsWTSE.WeekID = DateTimeExtension.CurrentWeekwithYear(WeeklyCalendar.SelectedDate);
            clsWTSE.BeginingweekDate = dtBegining;
            clsWTSE.EndweekDate = dtEnd;

            if (strStatus == "Rejected")
                clsWTSE.IsApproved = false;
            else
                clsWTSE.IsApproved = true;

            clsWTSE.Rejectreason = (strStatus == "Rejected") ? txtReject.Text : "";

            int iResult = bl.UpdateApprovedStatusOfTimesheet(clsWTSE, strStatus, out exMessage);

            //if (exMessage != null && exMessage.Length > 0)
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + exMessage + "');", true);
            //else
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + strStatus + "'Status Updated Successfully');", true);
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }
    }

    #endregion "Tab 2 - TimeSheet Approval"

    #region Common Misc Methods


    public int GetEmployeeNumber()
    {
        int EmployeeNumber = 0;
        string sUserId = string.Empty;

        try
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            BusinessLogic clsBusiness = new BusinessLogic(sDataSource);

            if (Request.Cookies["UserId"] != null)
                sUserId = Request.Cookies["UserId"].Value.ToString();

            DataSet dstEmployeeInfo = clsBusiness.GetEmployeeDetailByUserID(sUserId);

            if (dstEmployeeInfo != null)
            {
                if (dstEmployeeInfo.Tables[0].Rows.Count > 0)
                {
                    EmployeeNumber = Convert.ToInt32(dstEmployeeInfo.Tables[0].Rows[0]["Empno"]);
                }
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }

        return EmployeeNumber;
    }


    private void ResetSearch()
    {
        //txtSDate.Text = "";
        //txtSEmpno.Text = "";
        //drpsApproved.SelectedIndex = 1;
    }
    public void Reset()
    {

        //drpIncharge.SelectedIndex = 0;
        //txtDate.Text = "";
        //txtBefore8.Text = "";
        //txt8to9.Text = "";
        //txt9to10.Text = "";
        //txt10to11.Text = "";
        //txt11to12.Text = "";
        //txt12to1.Text = "";
        //txtPM1to2.Text = "";
        //txtPM2to3.Text = "";
        //txtPM3to4.Text = "";
        //txtPM4to5.Text = "";
        //txtPM5to6.Text = "";
        //txtPM6to7.Text = "";
        //txtPM7to8.Text = "";
        //txtPM8to9.Text = "";
        //txtPM9to10.Text = "";
        //txtPMafter10.Text = "";
        //drpapproved.SelectedIndex = 0;
        //btnSave.Enabled = true;
        //btnUpdate.Enabled = false;

        //btnDailySave.Enabled = true;
        //btnDailyUpdate.Enabled = false;

        //btnCancel.Enabled = true;
        //lnkBtnAdd.Visible = true;

        //pnsTime.Visible = false;
        //  pnsTime.Style["display"] = "none";
        //pnsDaily.Visible = false;
        // pnlTimesheetEntryList.Visible = true; //show grid...
        // pnsTse.Visible = false;
        //pnsApprov.Visible = false;
        //pnsSave.Visible = false;



    }

    private void DisableForOffline()
    {
        try
        {
            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            BusinessLogic objChk = new BusinessLogic(sDataSource);

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                //ObjNLog.Info("CheckForOffline method identified the Offline mode. Page is working for Offline mode.");
                lnkBtnAdd.Visible = false;
                btnSave.Enabled = false;
                // btnUpdate.Enabled = false;

                //btnDailySave.Enabled = false;
                //btnDailyUpdate.Enabled = false;
            }
            else
            {
                //ObjNLog.Info("CheckForOffline method identified not in Offline mode. Page is working for Online mode.");
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }
    }

    private void loadEmp()
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();

            ds = bl.ListExecutive();


            //drpIncharge.DataSource = ds;
            //drpIncharge.DataBind();
            //drpIncharge.DataTextField = "empFirstName";
            //drpIncharge.DataValueField = "empno";
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }
    }

    string GetConCatStr(string strNum)
    {
        string strCon1 = "";
        if (strNum.EndsWith("1")) strCon1 = "st";
        else if (strNum.EndsWith("2")) strCon1 = "nd";
        else if (strNum.EndsWith("3")) strCon1 = "rd";
        else strCon1 = "th";
        return strCon1;
    }

    #endregion

    #region "Active tab changed"
    protected void tbMain_ActiveTabChanged(object sender, EventArgs e)
    {
        try
        {
            if (tbMain.ActiveTabIndex == 0)
            {
                pnlTimeSheetEntry.Style["display"] = "block";
                pnlTimeSheetApproval.Style["display"] = "none";
                tdTimeSheetTitle.InnerText = "TimeSheet Entry";
                lblTimeSheetErrorMessage.Visible = true;
                lblTimeSheetErrorMessage.Text = "";
                tdSubmitted.Style["display"] = "block";
                Employeerow.Style["display"] = "block";
                Rowrejectreason.Style["display"] = "none";
                //tdRejected.Style["display"] = "none";
                //tdSubmitted.Style["Visibility"] = "visible";
                //tdRejected.Style["Visibility"] = "hidden";
                BindTse();
            }
            else if (tbMain.ActiveTabIndex == 1)
            {
                pnlTimeSheetEntry.Style["display"] = "none";
                pnlTimeSheetApproval.Style["display"] = "block";
                tdTimeSheetTitle.InnerText = "TimeSheet Approval";
                lblTimeSheetErrorMessage.Visible = false;
                tdSubmitted.Style["display"] = "none";
                Employeerow.Style["display"] = "block";
                //tdRejected.Style["display"] = "block";
                Rowrejectreason.Style["display"] = "block";
                //tdRejected.Style["Visibility"] = "visible";
                //tdSubmitted.Style["Visibility"] = "hidden";
                txtReject.Text = "";
                BindSubTseForApproval();
            }
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0} ", ex.Message));
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    #endregion

    #region "Save WeeklyTimesheets, TimeSheets, TimesheetEffortDescription Details"
    private int SaveOrSubmitTimeSheet(string strSaveMode, out string strMessage)
    {
        int iResult = 0;
        try
        {
            int EmployeeNumber = GetEmployeeNumber();

            DateTime dtBegining;
            DateTime dtEnd;
            DateTimeExtension.GetWeek(WeeklyCalendar.SelectedDate, new CultureInfo("fr-FR"), out dtBegining, out dtEnd);

            string TSEDate = string.Empty;
            DataTable dtTable = new DataTable();

            string value = string.Empty;
            string exMessage = string.Empty;
            string strWeekID = DateTimeExtension.CurrentWeekwithYear(WeeklyCalendar.SelectedDate);

            // EmployeeNumber 1, 2 and 3
            // WeekID, 1, 2 and 3
            // WorkDate - 2 and 3
            // EffortDescription - 3
            #region TimeSheet Data Manipulation

            List<WeeklyTimeSheetEntity> lstWeeklyTimeSheet = new List<WeeklyTimeSheetEntity>();
            List<TimesheetEntity> lstTimeSheet = new List<TimesheetEntity>();
            List<TimesheetEffortDescriptionEntity> lstTimeSheetEffortDesc = new List<TimesheetEffortDescriptionEntity>();

            WeeklyTimeSheetEntity clsWTSE = null;
            TimesheetEntity clsTSE = null;
            TimesheetEffortDescriptionEntity clsTSEDE = null;

            clsWTSE = new WeeklyTimeSheetEntity();

            clsWTSE.EmployeeNumber = EmployeeNumber;
            clsWTSE.WeekID = strWeekID;
            clsWTSE.BeginingweekDate = dtBegining;
            clsWTSE.EndweekDate = dtEnd;
            clsWTSE.Fromdatetodesc = DateTimeExtension.WeekStartToEndDateString(dtBegining, dtEnd);
            clsWTSE.UserGroup = "";
            clsWTSE.StatusWeekly = strSaveMode;
            clsWTSE.SelectedDate = WeeklyCalendar.SelectedDate;
            clsWTSE.DateSubmitted = DateTime.Now; //for both save & submit applying current date, while showing suppressed for edit

            lstWeeklyTimeSheet.Add(clsWTSE);

            for (int j = 1; j < this.gridWeeklyTimesheets.Columns.Count; j++)
            {
                GridViewRow grdHeader = (GridViewRow)gridWeeklyTimesheets.HeaderRow;
                Literal sWorkDate = (Literal)grdHeader.Cells[j].Controls[0];

                clsTSE = new TimesheetEntity();

                clsTSE.Empno = EmployeeNumber;
                clsTSE.TSDate = dtBegining.AddDays(j - 1);
                clsTSE.WeekID = strWeekID;
                //clsTSE.Hours - clsTSE.Rate - clsTSE.RateFactor - clsTSE.Overtime 
                lstTimeSheet.Add(clsTSE);

                for (int i = 0; i < this.gridWeeklyTimesheets.Rows.Count; i++)
                {
                    DataControlFieldCell dcfcWorkEffortDesc = (DataControlFieldCell)gridWeeklyTimesheets.Rows[i].Cells[j];
                    TextBox txtEffortDesc = dcfcWorkEffortDesc.Controls[0] as TextBox;

                    clsTSEDE = new TimesheetEffortDescriptionEntity();
                    clsTSEDE.Empno = EmployeeNumber;
                    clsTSEDE.TSDate = clsTSE.TSDate;
                    clsTSEDE.RangeID = i + 1;
                    clsTSEDE.RangeDescription = txtEffortDesc.Text;
                    clsTSEDE.WeekID = strWeekID;

                    lstTimeSheetEffortDesc.Add(clsTSEDE);
                }
            }

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            BusinessLayer clsBusinessLayer = new BusinessLayer(sDataSource);

            iResult = clsBusinessLayer.SaveWeeklyTimesheetEfforts(lstWeeklyTimeSheet, lstTimeSheet, lstTimeSheetEffortDesc, out exMessage);

            strMessage = exMessage;

            #endregion  TimeSheet Data Manipulation
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }

        return iResult;
    }

    #endregion
    protected void ddlTimeSheetKeyField_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTimeSheetKeyField.SelectedItem.Text != "-- All --")
        {
            if (ddlTimeSheetKeyField.SelectedItem.Value.Split(':').ElementAt<string>(1).ToLower() == "datetime")
                tdCalendar.Style["visibility"] = "visible";
            else
                tdCalendar.Style["visibility"] = "hidden";
        }
    }
    protected void btnApprovalSearch_Click(object sender, EventArgs e)
    {
        BindSubTseForApproval();
    }
    protected void ddlTimeSheetApproval_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtTimeSheetValueField.Text = "";
            BindTse();
            //ddCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnClrFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtTimeSheetApproval.Text = "";
            //BindTse();
            //ddCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}

#region Populate Dynamic Grid and Databinding

/// <summary>
/// Dynamic Gridview Populate and Databinding....
/// </summary>
public class DynamicSGridViewLabelTemplate : ITemplate
{
    public DataControlRowType templateType;
    public string columnName;
    public string dataType;

    public DynamicSGridViewLabelTemplate(DataControlRowType type,
        string colname, string DataType)
    {
        templateType = type;
        columnName = colname;
        dataType = DataType;
    }

    public void InstantiateIn(System.Web.UI.Control container)
    {
        DataControlFieldCell hc = null;

        if (columnName == "Range")
        {
            switch (templateType)
            {
                case DataControlRowType.Header:
                    // build the header for this column
                    Literal lc = new Literal();
                    lc.Text = "<b>" + columnName + "</b>";
                    container.Controls.Add(lc);
                    break;
                case DataControlRowType.DataRow:
                    // build one row in this column
                    Label l = new Label();
                    switch (dataType)
                    {
                        case "DateTime":
                            l.CssClass = "ReportNoWrap";
                            break;
                        case "Double":
                            hc = (DataControlFieldCell)container;
                            hc.CssClass = l.CssClass = "ReportNoWrapRightJustify";
                            break;
                        case "Int16":
                        case "Int32":
                            hc = (DataControlFieldCell)container;
                            hc.CssClass = l.CssClass = "ReportNoWrapRightJustify";
                            break;
                        case "String":
                            l.CssClass = "ReportNoWrap";
                            break;
                    }
                    // register an event handler to perform the data binding
                    l.DataBinding += new EventHandler(this.l_DataBinding);
                    container.Controls.Add(l);
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (templateType)
            {
                case DataControlRowType.Header:
                    // build the header for this column
                    Literal lc = new Literal();
                    lc.Text = "<b>" + columnName + "</b>";
                    container.Controls.Add(lc);
                    break;
                case DataControlRowType.DataRow:
                    // build one row in this column
                    TextBox txt = new TextBox();
                    switch (dataType)
                    {
                        case "DateTime":
                            txt.CssClass = "ReportNoWrap";
                            break;
                        case "Double":
                            hc = (DataControlFieldCell)container;
                            hc.CssClass = txt.CssClass = "ReportNoWrapRightJustify";
                            break;
                        case "Int16":
                        case "Int32":
                            hc = (DataControlFieldCell)container;
                            hc.CssClass = txt.CssClass = "ReportNoWrapRightJustify";
                            break;
                        case "String":
                            txt.CssClass = "ReportNoWrap";
                            break;
                    }
                    // register an event handler to perform the data binding
                    txt.DataBinding += new EventHandler(this.l_DataBinding);
                    container.Controls.Add(txt);
                    break;
                default:
                    break;
            }
        }
    }

    private void l_DataBinding(Object sender, EventArgs e)
    {
        string RawValue = string.Empty;
        GridViewRow row = null;
        TextBox txt = null;
        Label l = null;

        if (columnName == "Range")
        {
            l = (Label)sender;
            row = (GridViewRow)l.NamingContainer;

            RawValue = DataBinder.Eval(row.DataItem, columnName).ToString();

            switch (dataType)
            {
                case "DateTime":
                    l.Text = String.Format("{0:d}", DateTime.Parse(RawValue));
                    break;
                case "Double":
                    l.Text = String.Format("{0:###,###,##0.00}",
                        Double.Parse(RawValue));
                    break;
                case "Int16":
                case "Int32":
                    l.Text = RawValue;
                    break;
                case "String":
                    l.Text = RawValue;
                    break;
            }
        }
        else
        {

            txt = (TextBox)sender;
            row = (GridViewRow)txt.NamingContainer;

            RawValue = DataBinder.Eval(row.DataItem, columnName).ToString();

            switch (dataType)
            {
                case "DateTime":
                    txt.Text = String.Format("{0:d}", DateTime.Parse(RawValue));
                    break;
                case "Double":
                    txt.Text = String.Format("{0:###,###,##0.00}",
                        Double.Parse(RawValue));
                    break;
                case "Int16":
                case "Int32":
                    txt.Text = RawValue;
                    break;
                case "String":
                    txt.Text = RawValue;
                    break;
            }
        }
    }
}


#endregion