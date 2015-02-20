using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeePayroll : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    private static string conStrIdentifier = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String url = Request.ServerVariables["URL"];
            url = url.Remove(0, url.LastIndexOf("/") + 1);

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            conStrIdentifier = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

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
                    btnQueuePayroll.Visible = false;
                    btnViewPayslips.Visible = false;
                    //grdViewAttendanceSummary.Columns[7].Visible = false;
                    //grdViewAttendanceSummary.Columns[8].Visible = false;
                }
                grdViewPaySlipInfo.PageSize = 8;

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                BindAttendanceSummaryFilterList();

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

    private void BindAttendanceSummaryFilterList()
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);

            DataTable dt = bl.GetAllMonths();
            if (dt != null)
            {
                ddlMonth.DataSource = dt;
                ddlMonth.DataBind();
                UpdatePanelMain.Update();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnQueuePayroll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlMonth.SelectedIndex >= 0 && ddlYear.SelectedIndex >= 0)
            {
                int year = 0;
                int month = 0;

                int.TryParse(ddlYear.SelectedValue, out year);
                int.TryParse(ddlMonth.SelectedValue, out month);

                BusinessLogic bl = new BusinessLogic(sDataSource);
                int payrollId = 0;
                DataTable dtPayrollQueue = bl.GetPayrollQueueForTheMonth(year, month);
                if (dtPayrollQueue != null && dtPayrollQueue.Rows.Count > 0)
                {
                    int.TryParse(dtPayrollQueue.Rows[0][0].ToString(), out payrollId);
                    btnQueuePayroll.Enabled = false;
                    btnViewPayslips.Enabled = false;
                    lblPayrollStatus.Text = string.Format("Queue status: {0}", "Queued");
                    // Run the GeneratePayroll async
                    Task taskGeneratePayroll = Task.Factory.StartNew(() => GeneratePayroll(payrollId, year, month));
                    Task.WaitAny(new Task[] { taskGeneratePayroll });
                }
                else if (bl.QueuePayrollForTheMonth(year, month, out payrollId))
                {
                    btnQueuePayroll.Enabled = false;
                    btnViewPayslips.Enabled = false;
                    lblPayrollStatus.Text = string.Format("Queue status: {0}", "Queued");
                    // Run the GeneratePayroll async
                    Task taskGeneratePayroll = Task.Factory.StartNew(() => GeneratePayroll(payrollId, year, month));
                    Task.WaitAny(new Task[] { taskGeneratePayroll });
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindPaySlips()
    {
        grdViewPaySlipInfo.DataSource = null;
        grdViewPaySlipInfo.DataBind();
        if (Session["EmpPaySlipDt"] != null)
        {
            grdViewPaySlipInfo.DataSource = (Session["EmpPaySlipDt"] as DataTable);
            grdViewPaySlipInfo.DataBind();
            grdViewPaySlipInfo.Visible = true;
        }
    }

    private static void GeneratePayroll(int payrollId, int year, int month)
    {
        try
        {
            AdminBusinessLogic bl = new AdminBusinessLogic(conStrIdentifier);
            if (bl.GeneratePayRoll(payrollId, year, month))
            {

            }
        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnViewPayslips_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(hdfPayrollId.Value))
            {
                int payrollId = 0;
                if (int.TryParse(hdfPayrollId.Value, out payrollId))
                {
                    BusinessLogic bl = new BusinessLogic(sDataSource);
                    DataTable dtPayslips = bl.GetAllPaySlipForThePayroll(payrollId);
                    Session["EmpPaySlipDt"] = dtPayslips;

                    grdViewPaySlipInfo.DataSource = dtPayslips;
                    grdViewPaySlipInfo.DataBind();
                    grdViewPaySlipInfo.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSearchpayroll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlMonth.SelectedIndex >= 0 && ddlYear.SelectedIndex >= 0)
            {
                int year = 0;
                int month = 0;

                int.TryParse(ddlYear.SelectedValue, out year);
                int.TryParse(ddlMonth.SelectedValue, out month);
                btnQueuePayroll.Enabled = false;
                btnViewPayslips.Enabled = false;
                btnViewLog.Visible = false;
                lblPayrollStatus.Text = string.Empty;
                grdViewPaySlipInfo.Visible = false;

                BusinessLogic bl = new BusinessLogic(sDataSource);
                DataTable dtPayrollQueue = bl.GetPayrollQueueForTheMonth(year, month);
                if (dtPayrollQueue != null && dtPayrollQueue.Rows.Count > 0)
                {
                    btnQueuePayroll.Enabled = false;
                    string queueStatus = dtPayrollQueue.Rows[0]["Status"].ToString();
                    if (queueStatus == "Completed")
                    {
                        hdfPayrollId.Value = dtPayrollQueue.Rows[0][0].ToString();
                        btnViewPayslips.Enabled = true;
                        lblPayrollStatus.Text = string.Format("Payroll status: {0}.", queueStatus);
                    }
                    else if (queueStatus == "Failed")
                    {
                        hdfPayrollId.Value = dtPayrollQueue.Rows[0][0].ToString();
                        lblPayrollStatus.Text = string.Format("Payroll status: {0}. Please Contact Administrator.", queueStatus);
                        btnQueuePayroll.Enabled = true;
                        btnViewLog.Visible = true;
                    }
                    else if (queueStatus == "Queued")
                    {
                        lblPayrollStatus.Text = string.Format("Payroll status: {0}. Please wait till the payroll generation process initiated.", queueStatus);
                        btnQueuePayroll.Enabled = true;
                    }
                    else if (queueStatus == "In Progress")
                    {
                        lblPayrollStatus.Text = string.Format("Payroll status: {0}. Please wait till the payroll generation process gets completed.", queueStatus);
                        btnQueuePayroll.Enabled = true;
                    }
                }
                else
                {
                    lblPayrollStatus.Text = string.Format("Payroll Not Initiated");
                    btnQueuePayroll.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdViewPaySlipInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdViewPaySlipInfo.PageIndex = e.NewPageIndex;
            BindPaySlips();
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
            grdViewPaySlipInfo.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindPaySlips();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdViewPaySlipInfo_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(grdViewPaySlipInfo, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnViewLog_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(hdfPayrollId.Value))
            {
                int payrollId = 0;
                if (int.TryParse(hdfPayrollId.Value, out payrollId))
                {
                    BusinessLogic bl = new BusinessLogic(sDataSource);
                    DataTable dtPayslips = bl.GetPayrollProcessLog(payrollId);
                    grdViewPayrollLog.DataSource = dtPayslips;
                    grdViewPayrollLog.DataBind();
                    grdViewPayrollLog.Visible = true;
                    modelPopupLogViewer.Show();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            throw ex;
        }
    }
}