using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class TaskEntry : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
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

                BindWME("", "");
                loadEmp();
                DisableForOffline();
                //rvASDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //rvASDate.MaximumValue = System.DateTime.Now.ToShortDateString();
                //rvAEDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //rvAEDate.MaximumValue = System.DateTime.Now.ToShortDateString();


                GrdWME.PageSize = 8;

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveAdd(usernam, "TCreate"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New item ";
                }


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void DisableForOffline()
    {
        string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        BusinessLogic objChk = new BusinessLogic(sDataSource);

        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
            lnkBtnAdd.Visible = false;
            GrdWME.Columns[10].Visible = false;
            GrdWME.Columns[11].Visible = false;
        }
    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        ddCriteria.SelectedIndex = 0;
        BindWME("", "");
    }

    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        DataSet dsd = new DataSet();
        DataSet dst = new DataSet();
        DataSet dstd = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpIncharge.Items.Clear();
        drpIncharge.Items.Add(new ListItem("Select Owner", "0"));
       // ds = bl.ListExecutive();
        string Usernam = Request.Cookies["LoggedUserName"].Value;
        ds = bl.ListOwner(connection,Usernam);
        drpIncharge.DataSource = ds;
        drpIncharge.DataBind();
        drpIncharge.DataTextField = "empFirstName";
        drpIncharge.DataValueField = "empno";

        drpTaskType.Items.Clear();
        drpTaskType.Items.Add(new ListItem("Select Task Type", "0"));
        dsd = bl.ListTaskTypesInfo(connection, "", "");
        drpTaskType.DataSource = dsd;
        drpTaskType.DataBind();
        drpTaskType.DataTextField = "Task_Type_Name";
        drpTaskType.DataValueField = "Task_Type_Id";

        drpProjectCode.Items.Clear();
        string Username = Request.Cookies["LoggedUserName"].Value;
        drpProjectCode.Items.Add(new ListItem("Select Project Name"));
      //  dst = bl.GetProjectList(connection, "", "",Username);
        dst = bl.getfilterproject(Username);
        drpProjectCode.DataSource = dst;
        drpProjectCode.DataBind();
        drpProjectCode.DataTextField = "Project_Name";
        drpProjectCode.DataValueField = "Project_Id";

        //drpDependencyTask.Items.Clear();
        //drpDependencyTask.Items.Add(new ListItem("Select Dependency Task", "0"));
        //dstd = bl.GetTaskList(connection, "", "");
        //drpDependencyTask.DataSource = dstd;
        //drpDependencyTask.DataBind();
        //drpDependencyTask.DataTextField = "Task_Name";
        //drpDependencyTask.DataValueField = "Task_Id";

        //drpsIncharge.DataSource = ds;
        //drpsIncharge.DataBind();
        //drpsIncharge.DataTextField = "empFirstName";
        //drpsIncharge.DataValueField = "empno";
    }

    private void BindWME(string textSearch, string dropDown)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        string connection = Request.Cookies["Company"].Value;

        string Username = Request.Cookies["LoggedUserName"].Value;

        DataSet ds = bl.GetUserTaskList(connection, textSearch, dropDown,Username);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdWME.DataSource = ds;
                GrdWME.DataBind();
            }
        }
        else
        {
            GrdWME.DataSource = null;
            GrdWME.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string textSearch = string.Empty;
            string dropDown = string.Empty;

            textSearch = txtSearch.Text;
            dropDown = ddCriteria.SelectedValue;

            BindWME(textSearch, dropDown);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    DateTime TaskDate;
    DateTime EWStartDate;
    DateTime EWEndDate;
    protected void btnSave_Click(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);

       
       
        int Owner = 0;
        string TaskCode = string.Empty;
        string TaskName = string.Empty;
        string ActStartDate = string.Empty;
        string ActEndDate = string.Empty;
        string TaskDesc = string.Empty;
        string IsActive = string.Empty;
        int ProjectCode = 0;
        int TaskType = 0;
        int DependencyTask = 0;
        int effortdays = 0;

        try
        {
            if (Page.IsValid )
            {

                int PId = 0;
                if (drpProjectCode.Text.Trim() != string.Empty)
                    PId = Convert.ToInt32(drpProjectCode.SelectedValue);

                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DataSet dsttd = bl.GetProjectForId(connection, PId);
                if (dsttd != null)
                {
                    string sss = Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Project_Date"].ToString()).ToShortDateString();
                    if (dsttd.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToDateTime(txtEWstartDate.Text) == Convert.ToDateTime(sss))
                        //if (Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Project_Date"]) => Convert.ToDateTime(txtEWstartDate.Text))
                        {
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Validation Message(s) \\n\\n - Task Start Date should be greater than or equal to Selected Project Created Date');", true);
                            //ModalPopupExtender1.Show();
                            //tbMain.Visible = true;
                            //return;
                        }
                        else if (Convert.ToDateTime(txtEWstartDate.Text) < Convert.ToDateTime(sss))
                        {
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Task Start Date should be greater than or equal to selected Project Created Date');", true);
                            //ModalPopupExtender1.Show();
                            //tbMain.Visible = true;
                            //return;
                        }
                        //if (Convert.ToDateTime(txtEWEndDate.Text) < Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Project_Date"]))
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Validation Message(s) \\n\\n -Task End Date should be greater than or equal to Selected Task Start Date');", true);
                        //    ModalPopupExtender1.Show();
                        //    tbMain.Visible = true;
                        //    return;
                        //}
                        //if (Convert.ToDateTime(txtCDate.Text) < Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Project_Date"]))
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Date should be Greater than to the Selected Project Date');", true);
                        //    ModalPopupExtender1.Show();
                        //    tbMain.Visible = true;
                        //    return;
                        //}   
                       
                    }
                }

              

                if (drpProjectCode.Text.Trim() != string.Empty)
                    ProjectCode = Convert.ToInt32(drpProjectCode.Text.Trim());
                if (drpDependencyTask.Text.Trim() != string.Empty)
                    DependencyTask = Convert.ToInt32(drpDependencyTask.Text.Trim());
                if (txtCDate.Text.Trim() != string.Empty)
                    TaskDate = Convert.ToDateTime(txtCDate.Text.Trim().ToString());
                if (txtEWstartDate.Text.Trim() != string.Empty)
                    EWStartDate = Convert.ToDateTime(txtEWstartDate.Text.Trim().ToString());
                if (txtEWEndDate.Text.Trim() != string.Empty)
                    EWEndDate = Convert.ToDateTime(txtEWEndDate.Text.Trim().ToString());
                if (drpIncharge.Text.Trim() != string.Empty)
                    Owner = Convert.ToInt32(drpIncharge.Text.Trim());
                if (txtTaskID.Text.Trim() != string.Empty)
                    TaskCode = txtTaskID.Text.Trim();
                if (txtTaskName.Text.Trim() != string.Empty)
                    TaskName = txtTaskName.Text.Trim();
                if (drpTaskType.Text.Trim() != string.Empty)
                    TaskType = Convert.ToInt32(drpTaskType.Text.Trim());
                if (drpIsActive.Text.Trim() != string.Empty)
                    IsActive = drpIsActive.Text.Trim();
                if (txtTaskDesc.Text.Trim() != string.Empty)
                    TaskDesc = txtTaskDesc.Text.Trim();
                if (Taskeffortdays.Text.Trim() != string.Empty)
                    effortdays = Convert.ToInt32(Taskeffortdays.Text.Trim());

                string Username = Request.Cookies["LoggedUserName"].Value;

                //DateTime TaskDate1;
                //DateTime EWStartDate1;
                //DateTime EWEndDate1;

                //DataSet checkemp = bl.SearchWME(WorkId, empNO, EWStartDate, EWEndDate, CreationDate, status);

                //if (checkemp == null || checkemp.Tables[0].Rows.Count == 0)
                //{
                bl.InsertTaskEntry(ProjectCode, TaskDate, EWStartDate, EWEndDate, Owner, TaskCode, TaskType, IsActive, DependencyTask, TaskDesc, Username, TaskName,effortdays);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Entry Details Saved Successfully.');", true);
                Reset();
                ResetSearch();
                BindWME("", "");
                //MyAccordion.Visible = true;
                tbMain.Visible = false;
                GrdWME.Visible = true;
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Duplicate work Management Entry EmpNo " + empNO + " ');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int TaskType = 0;
            //string TaskDate = string.Empty;
            //string EWStartDate = string.Empty;
            //string EWEndDate = string.Empty;
            int Owner = 0;
            string Workdet = string.Empty;
            //string ActStartDate = string.Empty;
            //string ActEndDate = string.Empty;
            string TaskCode = string.Empty;
            string TaskName = string.Empty;
            string status = string.Empty;
            string TaskDesc = string.Empty;
            string IsActive = string.Empty;
           // string unitofmeasure = string.Empty;
            int ProjectCode = 0;
            int DependencyTask = 0;
            int Task_Id = 0;
            int effortdays = 0;

            if (Page.IsValid)
            {

                int PId = 0;
                if (drpProjectCode.Text.Trim() != string.Empty)
                    PId = Convert.ToInt32(drpProjectCode.SelectedValue);

                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DataSet dsttd = bl.GetProjectForId(connection, PId);
                if (dsttd != null)
                {
                    if (dsttd.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToDateTime(txtEWstartDate.Text) == Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Project_Date"]))
                        {
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Validation Message(s) \\n\\n -Task Expected Start Date should be Greater than or equal to Selected Project Expected Start Date.');", true);
                            //ModalPopupExtender1.Show();
                            //tbMain.Visible = true;
                            //return;
                        }
                        else if(Convert.ToDateTime(txtEWstartDate.Text) < Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Project_Date"]))
                        {
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Expected Start Date should be Greater than or equal to Selected Project Expected Start Date.');", true);
                            //ModalPopupExtender1.Show();
                            //tbMain.Visible = true;
                            //return;
                        }
                        //if (Convert.ToDateTime(txtEWEndDate.Text) < Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Project_Date"]))
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Validation Message(s) \\n\\n -Task Expected End Date should be Greater than or equal to Task End Date.');", true);
                        //    ModalPopupExtender1.Show();
                        //    tbMain.Visible = true;
                        //    return;
                        //}
                        //if (Convert.ToDateTime(txtCDate.Text) < Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Project_Date"]))
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Date should be Greater than to the Selected Project Date');", true);
                        //    ModalPopupExtender1.Show();
                        //    tbMain.Visible = true;
                        //    return;
                        //}
                    }
                }


                if (drpProjectCode.Text.Trim() != string.Empty)
                    ProjectCode = Convert.ToInt32(drpProjectCode.Text.Trim());
                if (drpDependencyTask.Text.Trim() != string.Empty)
                    DependencyTask = Convert.ToInt32(drpDependencyTask.Text.Trim());
                if (txtCDate.Text.Trim() != string.Empty)
                    TaskDate =Convert.ToDateTime(txtCDate.Text.Trim().ToString());
                if (txtEWstartDate.Text.Trim() != string.Empty)
                    EWStartDate = Convert.ToDateTime(txtEWstartDate.Text.Trim().ToString());
                if (txtEWEndDate.Text.Trim() != string.Empty)
                    EWEndDate =Convert.ToDateTime(txtEWEndDate.Text.Trim().ToString());
                if (drpIncharge.Text.Trim() != string.Empty)
                    Owner = Convert.ToInt32(drpIncharge.Text.Trim());
                if (txtTaskID.Text.Trim() != string.Empty)
                    TaskCode = txtTaskID.Text.Trim();
                if (txtTaskName.Text.Trim() != string.Empty)
                    TaskName = txtTaskName.Text.Trim();
                if (drpTaskType.Text.Trim() != string.Empty)
                    TaskType = Convert.ToInt32(drpTaskType.Text.Trim());
                if (drpIsActive.Text.Trim() != string.Empty)
                    IsActive = drpIsActive.Text.Trim();
                if (txtTaskDesc.Text.Trim() != string.Empty)
                    TaskDesc = txtTaskDesc.Text.Trim();
                if (Taskeffortdays.Text.Trim() != string.Empty)
                    effortdays = Convert.ToInt32(Taskeffortdays.Text.Trim());
                //if (drpunitmeasure.Text.Trim() != string.Empty)
                //    unitofmeasure = drpunitmeasure.Text.Trim();

                string Username = Request.Cookies["LoggedUserName"].Value;

                Task_Id = int.Parse(GrdWME.SelectedDataKey.Value.ToString());

                bl.UpdateTaskEntry(ProjectCode, TaskDate, EWStartDate, EWEndDate, Owner, TaskCode, TaskType, IsActive, DependencyTask, TaskDesc, Username, Task_Id, TaskName,effortdays);


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Entry Details Updated Successfully.');", true);
                Reset();
                ResetSearch();
                BindWME("", "");
                //MyAccordion.Visible = true;
                tbMain.Visible = false;
                GrdWME.Visible = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }



    //protected void txtEWEndDate_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //if (IsPostBack)
    //        //{

    //        string startdate = Convert.ToDateTime(txtEWstartDate.Text).ToString("dd/MM/yyyy");

    //        string enddate = Convert.ToDateTime(txtEWEndDate.Text).ToString("dd/MM/yyyy");

    //        TimeSpan ts = Convert.ToDateTime(enddate) - Convert.ToDateTime(startdate);

    //        int days = Convert.ToInt32(ts.TotalDays);
    //        if (days > 0)
    //        {
    //            Taskeffortdays.Text = Convert.ToString(days);

    //        }
    //        else
    //        {
    //            days = 0;
    //            Taskeffortdays.Text = Convert.ToString(days);
    //        }

    //        //}
    //        UpdatePanel3.Update();

    //    }
    //    catch (Exception ex)
    //    {
    //        TroyLiteExceptionManager.HandleException(ex);
    //    }

    //}


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //MyAccordion.Visible = true;
            tbMain.Visible = false;
            GrdWME.Visible = true;
            Reset();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void ResetSearch()
    {
        
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtsCreationDate.Text = "";
        drpsStatus.SelectedIndex = 0;
    }
    public void Reset()
    {
        drpIncharge.SelectedIndex = 0;

        pnsSave.Visible = false;
        lnkBtnAdd.Visible = true;
        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        btnUpdate.Enabled = false;
        drpIsActive.SelectedIndex = 0;
        txtTaskID.Text = "";
        txtCDate.Text = "";
        txtEWstartDate.Text = "";
        txtEWEndDate.Text = "";
        drpTaskType.SelectedIndex = 0;
        txtTaskName.Text = "";
       // drpDependencyTask.SelectedIndex = 0;
        drpProjectCode.SelectedIndex = 0;
        txtTaskDesc.Text = "";
        Taskeffortdays.Text = "";
    }

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdWME.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindWME("", "");

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdWME_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        { 
            GrdWME.PageIndex = e.NewPageIndex;
            BindWME("","");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdWME_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdWME, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdWME_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gridView = (GridView)sender;

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    BusinessLogic bl = new BusinessLogic(sDataSource);
            //    string connection = Request.Cookies["Company"].Value;
            //    string usernam = Request.Cookies["LoggedUserName"].Value;

            //    if (bl.CheckUserHaveEdit(usernam, "WMENTRY"))
            //    {
            //        ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
            //        ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
            //    }

            //    if (bl.CheckUserHaveDelete(usernam, "WMENTRY"))
            //    {
            //        ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
            //        ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
            //    }
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdWME_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

           // int Task_Id = Convert.ToInt32(GrdWME.Rows[e.RowIndex].Cells[0].Text);

            int Task_Id = Convert.ToInt32(GrdWME.DataKeys[e.RowIndex].Value.ToString());

            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.DeleteTaskDetails(Task_Id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Entry Deleted Successfully.');", true);
            BindWME("", "");
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
           

            Reset();
            headtitle.Text = "Add New Task Details";
            tabMaster.HeaderText = "New Task Details";
            txtCDate.Text = DateTime.Now.ToShortDateString();
            btnUpdate.Enabled = false;
            tbMain.Visible = true;
            pnsSave.Visible = true;
            drpIsActive.Enabled = false;
            btnCancel.Enabled = true;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnSave.Enabled = true;
            unitofmeasureheading.Text = "";
            //drpunitmeasure.Visible = true;
            
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            loadEmp();
            
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdWME_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int Task_Id = 0;
            headtitle.Text = "Update Task Details";
            tabMaster.HeaderText = "Update Task Details";
            string connection = Request.Cookies["Company"].Value;

            if (GrdWME.SelectedDataKey.Value != null && GrdWME.SelectedDataKey.Value.ToString() != "")
                Task_Id = Convert.ToInt32(GrdWME.SelectedDataKey.Value.ToString());

            DataSet ds = bl.GetTaskForId(connection, Task_Id);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Expected_Start_Date"] != null)
                        txtEWstartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Expected_Start_Date"]).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["Expected_End_Date"] != null)
                        txtEWEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Expected_End_Date"]).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["Task_Date"] != null)
                        txtCDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Task_Date"]).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["Task_Code"] != null)
                        txtTaskID.Text = ds.Tables[0].Rows[0]["Task_Code"].ToString();

                    if (ds.Tables[0].Rows[0]["Task_Name"] != null)
                        txtTaskName.Text = ds.Tables[0].Rows[0]["Task_Name"].ToString();

                    if (ds.Tables[0].Rows[0]["Effort_Task_Days"] != null)
                        Taskeffortdays.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["Effort_Task_Days"]).ToString();

                    if (ds.Tables[0].Rows[0]["Project_Code"] != null)
                    {
                        string sCs = Convert.ToString(ds.Tables[0].Rows[0]["Project_Code"]);
                        drpProjectCode.ClearSelection();
                        ListItem li = drpProjectCode.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCs));
                        if (li != null) li.Selected = true;
                    }
                    //if (ds.Tables[0].Rows[0]["Unit_Of_Measure"] != null)
                    //    drpunitmeasure.SelectedValue = ds.Tables[0].Rows[0]["Unit_Of_Measure"].ToString();

                    if (ds.Tables[0].Rows[0]["Task_Description"] != null)
                        txtTaskDesc.Text = ds.Tables[0].Rows[0]["Task_Description"].ToString();

                    int Project_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Project_Code"]);
                    drpDependencyTask.Items.Clear();
                    drpDependencyTask.Items.Add(new ListItem("Select Dependency Task", "0"));
                    DataSet dsd = bl.GetDependencytask(connection, Project_Id);
                    drpDependencyTask.DataSource = dsd;
                    drpDependencyTask.DataBind();
                    drpDependencyTask.DataTextField = "Task_Name";
                    drpDependencyTask.DataValueField = "Task_Id";
                    UpdatePanel2.Update();


                    if (ds.Tables[0].Rows[0]["Dependency_Task"] != null)
                    {
                        string sCu = Convert.ToString(ds.Tables[0].Rows[0]["Dependency_Task"]);
                        drpDependencyTask.ClearSelection();
                        ListItem li = drpDependencyTask.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCu));
                        if (li != null) li.Selected = true;
                    }

                    if (ds.Tables[0].Rows[0]["IsActive"] != null)
                        drpIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();

                    if (ds.Tables[0].Rows[0]["Task_Type"] != null)
                    {
                        string sCus = Convert.ToString(ds.Tables[0].Rows[0]["Task_Type"]);
                        drpTaskType.ClearSelection();
                        ListItem li = drpTaskType.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCus));
                        if (li != null) li.Selected = true;
                    }

                    if (ds.Tables[0].Rows[0]["Owner"] != null)
                    {
                        string sCustomer = Convert.ToString(ds.Tables[0].Rows[0]["Owner"]);
                        drpIncharge.ClearSelection();
                        ListItem li = drpIncharge.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }
                    DataSet dst = bl.GetDependencytask(connection, Project_Id);
                    if (dst != null)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            unitofmeasureheading.Text = "(" + Convert.ToString(dst.Tables[0].Rows[0]["Unit_Of_Measure"].ToString()) + ")";
                            UpdatePanel4.Update();
                        }
                    }

                }
            }

            
            btnUpdate.Enabled = true;
            pnsSave.Visible = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = false;
            tbMain.Visible = true;
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }



    protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckTaskUpdateUsed(int.Parse(((HiddenField)e.Row.FindControl("TaskID")).Value)))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "TCreate"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "TCreate"))
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


    protected void drpprojectcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int Project_Id = 0;

            string connection = Request.Cookies["Company"].Value;


            Project_Id = Convert.ToInt32(drpProjectCode.SelectedValue);
            //if (GrdWME.SelectedDataKey.Value != null && GrdWME.SelectedDataKey.Value.ToString() != "")
            //    Project_Id = Convert.ToInt32(GrdWME.SelectedDataKey.Value.ToString());

            drpDependencyTask.Items.Clear();
            drpDependencyTask.Items.Add(new ListItem("Select Dependency Task", "0"));
            DataSet ds = bl.GetDependencytask(connection, Project_Id);

            drpDependencyTask.DataSource = ds;
            drpDependencyTask.DataBind();
            drpDependencyTask.DataTextField = "Task_Name";
            drpDependencyTask.DataValueField = "Task_Id";
            UpdatePanel2.Update();
            DataSet dst =bl.GetProjectForId (connection, Project_Id);
            if (dst != null)
            {
                if (dst.Tables[0].Rows.Count > 0)
                {
                    unitofmeasureheading.Text ="("+ Convert.ToString(dst.Tables[0].Rows[0]["Unit_Of_Measure"].ToString())+")";
                    UpdatePanel4.Update();
                }
            }
        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
        // UpdatePanel2.Update();
    }

}
