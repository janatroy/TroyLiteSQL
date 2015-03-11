using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
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


                //string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                //dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                //BusinessLogic objChk = new BusinessLogic();

                //if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                //{
                //    btnQueuePayroll.Visible = false;
                //    btnViewPayslips.Visible = false;
                //    //grdViewAttendanceSummary.Columns[7].Visible = false;
                //    //grdViewAttendanceSummary.Columns[8].Visible = false;
                //}
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
                lblPayrollGenerationMsg.Text = string.Format("Initiate payroll for {0} {1}", ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text);
                ModalPopupPayrollGeneration.Show();
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

    private static void GeneratePayroll(int payrollId, int year, int month, DataSet dsAdditionalPayComponent)
    {
        try
        {
            AdminBusinessLogic bl = new AdminBusinessLogic(conStrIdentifier);

            if (bl.GeneratePayRoll(payrollId, year, month, dsAdditionalPayComponent))
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
                    ViewState["PayrollYear"] = ddlYear.SelectedItem.Text;
                    ViewState["PayrollMonth"] = ddlMonth.SelectedItem.Text;
                    grdViewPaySlipInfo.DataSource = dtPayslips;
                    grdViewPaySlipInfo.DataBind();
                    grdViewPaySlipInfo.Visible = true;
                    btnExportToExcel.Visible = true;
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
            SearchPayrollForTheSelectedPeriod();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void SearchPayrollForTheSelectedPeriod()
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
            btnExportToExcel.Visible = false;
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
                    txtLogMessage.Visible = false;
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

    protected void chkBoxEnableFileUpload_CheckedChanged(object sender, EventArgs e)
    {
        fileUploadpayComponent.Enabled = chkBoxEnableFileUpload.Checked;
        updPnlPayrollGeneration.Update();
    }

    protected void btnInitiatePayroll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlMonth.SelectedIndex >= 0 && ddlYear.SelectedIndex >= 0)
            {
                int year = 0;
                int month = 0;


                DataSet dsAdditionalPayComponent = new DataSet();

                if (chkBoxEnableFileUpload.Checked)
                {
                    if (!fileUploadpayComponent.HasFile)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please upload the pay component excel file.');", true);
                        return;
                    }
                    else
                    {
                        string validationMsg = string.Empty;
                        if (!GetAdditionalPayComponentDataFromFile(ref validationMsg))
                        {
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('File upload failed." + validationMsg + " Please contact your administrator.');", true);
                            grdViewPayrollLog.Visible = false;
                            txtLogMessage.Visible = true;
                            txtLogMessage.Text = "ADDITIONAL PAY COMPONENT INPUT VALIDATION RESULT." + Environment.NewLine + validationMsg;
                            modelPopupLogViewer.Show();
                            return;
                        }
                        else
                        {
                            DataTable dtEmpPayComponent = ViewState["dtEmpPayComponent"] as DataTable;
                            DataTable dtRolePayComponent = ViewState["dtRolePayComponent"] as DataTable;
                            dsAdditionalPayComponent.Tables.Add(dtEmpPayComponent.Copy());
                            dsAdditionalPayComponent.Tables.Add(dtRolePayComponent.Copy());
                        }
                    }
                }

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
                    Task taskGeneratePayroll = Task.Factory.StartNew(() => GeneratePayroll(payrollId, year, month, dsAdditionalPayComponent));
                    Task.WaitAny(new Task[] { taskGeneratePayroll });
                }
                else if (bl.QueuePayrollForTheMonth(year, month, out payrollId))
                {
                    btnQueuePayroll.Enabled = false;
                    btnViewPayslips.Enabled = false;
                    lblPayrollStatus.Text = string.Format("Queue status: {0}", "Queued");
                    // Run the GeneratePayroll async
                    Task taskGeneratePayroll = Task.Factory.StartNew(() => GeneratePayroll(payrollId, year, month, dsAdditionalPayComponent));
                    Task.WaitAny(new Task[] { taskGeneratePayroll });
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private bool GetAdditionalPayComponentDataFromFile(ref string validationMsg)
    {
        string FileName = Path.GetFileName(fileUploadpayComponent.PostedFile.FileName);
        string Extension = Path.GetExtension(fileUploadpayComponent.PostedFile.FileName);
        string FolderPath = ConfigurationManager.AppSettings["PayrollExcelUploadPath"];

        string FilePath = Server.MapPath(FolderPath + FileName);
        fileUploadpayComponent.SaveAs(FilePath);
        return ExcelToDataTable(FilePath, Extension, "Yes", ref validationMsg);

    }

    private bool ExcelToDataTable(string FilePath, string Extension, string isHDR, ref string validationMsg)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                         .ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                          .ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dtEmpPayComponent = new DataTable();
        DataTable dtRolePayComponent = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of Sheets
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        if (dtExcelSchema.Rows.Count == 3)
        {
            string wsEmpPayComponent = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            string wsRolePayComponent = dtExcelSchema.Rows[2]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + wsEmpPayComponent + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dtEmpPayComponent);

            cmdExcel.CommandText = "SELECT * From [" + wsRolePayComponent + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dtRolePayComponent);

            connExcel.Close();

            ViewState["dtEmpPayComponent"] = dtEmpPayComponent;
            ViewState["dtRolePayComponent"] = dtRolePayComponent;
            validationMsg = string.Empty;
            bool empPayCompFileResult = ValidateEmpPayComponentFile(dtEmpPayComponent, ref validationMsg);
            bool rolePayCompFileResult = ValidateRolePayComponentFile(dtRolePayComponent, ref validationMsg);

            if (empPayCompFileResult && rolePayCompFileResult)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        return false;
    }

    private bool ValidateRolePayComponentFile(DataTable dtRolePayComponent, ref string validationMsg)
    {
        var validationResult = true;

        AdminBusinessLogic bl = new AdminBusinessLogic(sDataSource);
        DataTable dtEmployeeRole = bl.GetEmployeeRolesList();

        var result = dtEmployeeRole.Rows.Cast<DataRow>()
                    .Select(row => row["ID"].ToString())
                    .ToArray();

        string filterQuery = string.Format("RoleId NOT IN ({0})", string.Join(",", result));
        DataRow[] drInvalidEmployeeRole = dtRolePayComponent.Select(filterQuery);

        if (drInvalidEmployeeRole.Length > 0)
        {
            foreach (DataRow dr in drInvalidEmployeeRole)
            {
                validationMsg += string.Format("{0} Employee Role not matches with existing records for {1}.", Environment.NewLine, dr["Role_Name"].ToString());
            }
            validationResult = false;
        }

        var results = (from dr in dtRolePayComponent.AsEnumerable()
                       where dr.Field<double>("TotalAmount").Equals("0")
                       select dr);

        if (results.Count() > 0)
        {
            foreach (DataRow dr in results)
            {
                validationMsg += string.Format("{0} TotalAmount should not be zero. Role Name '{1}'", Environment.NewLine, dr["RoleName"]);
            }
            validationResult = false;
        }
        return validationResult;
    }

    private bool ValidateEmpPayComponentFile(DataTable dtEmpPayComponent, ref string validationMsg)
    {
        var validationResult = true;
        AdminBusinessLogic bl = new AdminBusinessLogic(sDataSource);
        DataTable dtEmployee = bl.GetEmployeeList();

        var result = dtEmployee.Rows.Cast<DataRow>()
                    .Select(row => row["EmpNo"].ToString())
                    .ToArray();
        string filterQuery = string.Format("EmpNo NOT IN ({0})", string.Join(",", result));
        DataRow[] drInvalidEmployee = dtEmpPayComponent.Select(filterQuery);

        if (drInvalidEmployee.Length > 0)
        {
            foreach (DataRow dr in drInvalidEmployee)
            {
                validationMsg += string.Format("{0} Employee number not matches with existing records for {1}.", Environment.NewLine, dr["EmpName"].ToString());
            }
            validationResult = false;
        }

        var results = (from dr in dtEmpPayComponent.AsEnumerable()
                       where dr.Field<double>("TotalAmount").Equals(0)
                       select dr);
        if (results.Count() > 0)
        {
            foreach (DataRow dr in results)
            {
                validationMsg += string.Format("{0} TotalAmount should not be zero. Employee Name '{1}'", Environment.NewLine, dr["EmpName"]);
            }
            validationResult = false;
        }


        return validationResult;
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["EmpPaySlipDt"] != null)
            {
                DataTable dtPayrollDetails = Session["EmpPaySlipDt"] as DataTable;
                dtPayrollDetails.TableName = ViewState["PayrollMonth"].ToString() + ViewState["PayrollYear"].ToString();
                //DataSet dsPayrollExcelData = new DataSet ();
                //dsPayrollExcelData.Tables.Add(dtPayrollDetails.Copy());
                //ExcelHelper.ToExcel(dsPayrollExcelData, "PayrollDetails" + dtPayrollDetails.TableName + ".xlsx", Page.Response);
                ExportToExcel(dtPayrollDetails, "PayrollDetails" + dtPayrollDetails.TableName + ".xls");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Unable to export to excel. Please contact your administrator.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Unable to export to excel. Please contact your administrator.');", true);
        }
    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SearchPayrollForTheSelectedPeriod();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(DataTable dt, string filename)
    {
        if (dt.Rows.Count > 0)
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            //Response.ContentType = application/vnd.ms-excel;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
    }
}