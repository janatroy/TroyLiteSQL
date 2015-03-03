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

public partial class TaskUpdates : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
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
                loadBranch();
                DisableForOffline();
                //rvASDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //rvASDate.MaximumValue = System.DateTime.Now.ToShortDateString();
                //rvAEDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //rvAEDate.MaximumValue = System.DateTime.Now.ToShortDateString();


                GrdWME.PageSize = 8;

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                //if (bl.CheckUserHaveAdd(usernam, "Tupdate"))
                //{
                //    lnkBtnAdd.Enabled = false;
                //    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                //}
                //else
                //{
                //    lnkBtnAdd.Enabled = true;
                //    lnkBtnAdd.ToolTip = "Click to Add New item ";
                //}

                //if (bl.CheckUserHaveEdit(usernam, "Tupdate"))
                //{
                //    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                //}


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpBranch.Items.Clear();
        drpBranch.Items.Add(new ListItem("Select Branch", "0"));
        ds = bl.ListBranch();
        drpBranch.DataSource = ds;
        drpBranch.DataBind();
        drpBranch.DataTextField = "BranchName";
        drpBranch.DataValueField = "Branchcode";
    }


    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        ddCriteria.SelectedIndex = 0;
        BindWME("", "");
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
          //  lnkBtnAdd.Visible = false;
            GrdWME.Columns[10].Visible = false;
            GrdWME.Columns[11].Visible = false;
        }
    }

    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        DataSet dsd = new DataSet();
        DataSet dst = new DataSet();
        DataSet dstd = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        ds = bl.ListExecutive();
        drpIncharge.DataSource = ds;
        drpIncharge.DataBind();
        drpIncharge.DataTextField = "empFirstName";
        drpIncharge.DataValueField = "empno";

        dsd = bl.ListTaskStatusInfo(connection, "", "");
        drpTaskStatus.DataSource = dsd;
        drpTaskStatus.DataBind();
        drpTaskStatus.DataTextField = "Task_Status_Name";
        drpTaskStatus.DataValueField = "Task_Status_Id";

        dsd = bl.ListTaskTypesInfo(connection, "", "");
        drpTaskType.DataSource = dsd;
        drpTaskType.DataBind();
        drpTaskType.DataTextField = "Task_Type_Name";
        drpTaskType.DataValueField = "Task_Type_Id";

        string Username = Request.Cookies["LoggedUserName"].Value;
        dst = bl.GetProjectList(connection, "", "");
        drpProjectCode.DataSource = dst;
        drpProjectCode.DataBind();
        drpProjectCode.DataTextField = "Project_Name";
        drpProjectCode.DataValueField = "Project_Id";

        dstd = bl.GetTaskList(connection, "", "");
        drpDependencyTask.DataSource = dstd;
        drpDependencyTask.DataBind();
        drpDependencyTask.DataTextField = "Task_Name";
        drpDependencyTask.DataValueField = "Task_Id";

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

        DataSet ds = bl.GetUsersTaskList(connection, textSearch, dropDown, Username);

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        string TaskDate = string.Empty;
        string EWStartDate = string.Empty;
        string EWEndDate = string.Empty;
        int Owner = 0;
        string TaskId = string.Empty;
        string ActStartDate = string.Empty;
        string ActEndDate = string.Empty;
        string TaskDesc = string.Empty;
        string IsActive = string.Empty;
        int ProjectCode = 0;
        int TaskType = 0;
        int DependencyTask = 0;

        try
        {
            if (Page.IsValid)
            {
                
                //string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                //if (bl.IsProjectCodeAlreadyFound(connection, ProjectCode))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Project Code already found');", true);

                //    ModalPopupExtender1.Show();
                //    tbMain.Visible = true;
                //    return;                   
                //}

                //if (drpProjectCode.Text.Trim() != string.Empty)
                //    ProjectCode = Convert.ToInt32(drpProjectCode.Text.Trim());
                //if (drpDependencyTask.Text.Trim() != string.Empty)
                //    DependencyTask = Convert.ToInt32(drpDependencyTask.Text.Trim());
                //if (txtCDate.Text.Trim() != string.Empty)
                //    TaskDate = txtCDate.Text.Trim();
                //if (txtEWstartDate.Text.Trim() != string.Empty)
                //    EWStartDate = txtEWstartDate.Text.Trim();
                //if (txtEWEndDate.Text.Trim() != string.Empty)
                //    EWEndDate = txtEWEndDate.Text.Trim();
                //if (drpIncharge.Text.Trim() != string.Empty)
                //    Owner = Convert.ToInt32(drpIncharge.Text.Trim());
                //if (txtTaskID.Text.Trim() != string.Empty)
                //    TaskId = txtTaskID.Text.Trim();
                //if (drpTaskType.Text.Trim() != string.Empty)
                //    TaskType = Convert.ToInt32(drpTaskType.Text.Trim());
                //if (drpIsActive.Text.Trim() != string.Empty)
                //    IsActive = drpIsActive.Text.Trim();
                //if (txtTaskDesc.Text.Trim() != string.Empty)
                //    TaskDesc = txtTaskDesc.Text.Trim();

                //string Username = Request.Cookies["LoggedUserName"].Value;

                //DataSet checkemp = bl.SearchWME(WorkId, empNO, EWStartDate, EWEndDate, CreationDate, status);

                //if (checkemp == null || checkemp.Tables[0].Rows.Count == 0)
                //{
                //bl.InsertTaskUpdateEntry(ProjectCode, TaskDate, EWStartDate, EWEndDate, Owner, TaskId, TaskType, IsActive, DependencyTask, TaskDesc, Username);

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Updates Details Saved Successfully');", true);
                //Reset();
                //ResetSearch();
                //BindWME("", "");
                ////MyAccordion.Visible = true;
                //tbMain.Visible = false;
                //GrdWME.Visible = true;
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

    
    DateTime ActualStartDate;
   // DateTime ActualEndDate;
  //  DateTime Taskupdate;
    DateTime ActStartDate;
    DateTime ActEndDate;
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int TaskStatus = 0;
            //string TaskUpdateDate = string.Empty;
            //string ActualStartDate = string.Empty;
            string ActualEndDate = string.Empty;
            string Taskupdate = string.Empty;
            //string ActStartDate = string.Empty;
            //string ActEndDate = string.Empty;
            string TaskId = string.Empty;
            string status = string.Empty;
            string BlockingReason = string.Empty;
            string Blockedflag = string.Empty;
            int Per = 0;
            int Task_Id = 0;
            int Effortlastupdate = 0;
            int effortremain = 0;
            DateTime TaskUpdateDate;

            if (Page.IsValid)
            {

                int PId = 0;
                if (GrdWME.SelectedDataKey.Value != null && GrdWME.SelectedDataKey.Value.ToString() != "")
                    PId = Convert.ToInt32(GrdWME.SelectedDataKey.Value.ToString());
                //if (drpProjectCode.Text.Trim() != string.Empty)
                //    PId = Convert.ToInt32(drpProjectCode.SelectedValue);

                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DataSet dsttd = bl.GetTaskForId(connection, PId);
                if (dsttd != null)
                {
                    string sss = Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Expected_Start_Date"].ToString()).ToShortDateString();
                    string ss = Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Expected_End_Date"].ToString()).ToShortDateString();
                    if (dsttd.Tables[0].Rows.Count > 0)
                    {
                        if ((drpTaskStatus.SelectedItem.Text == "Completed" || drpTaskStatus.SelectedItem.Text == "completed") && (txtActualEndDate.Text == null || txtActualEndDate.Text == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Actual End Date Cannot be Left Blank');", true);
                            ModalPopupExtender1.Show();
                            tbMain.Visible = true;
                            return;

                            if (Convert.ToDateTime(txtActualStartDate.Text) == Convert.ToDateTime(sss))
                            {

                            }
                            else if (Convert.ToDateTime(txtActualStartDate.Text) < Convert.ToDateTime(sss))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Actual Start Date should be greater than or equal to Task Expected Start Date');", true);
                                ModalPopupExtender1.Show();
                                tbMain.Visible = true;
                                return;
                            }
                        }


                            //if (Convert.ToDateTime(txtActualEndDate.Text) == Convert.ToDateTime(ss))
                            ////if (Convert.ToDateTime(dsttd.Tables[0].Rows[0]["Project_Date"]) => Convert.ToDateTime(txtEWstartDate.Text))
                            //{
                            //}
                            //else if (Convert.ToDateTime(txtActualEndDate.Text) < Convert.ToDateTime(ss))
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Actual End Date should be greater than or equal to Task Actual Start Date');", true);
                            //    ModalPopupExtender1.Show();
                            //    tbMain.Visible = true;
                            //    return;
                            //}

                            if (drpBlockedflag.SelectedValue == "N")
                            {
                                txtBlockingReason.Text = string.Empty;
                            }
                            else
                            {
                                if (txtBlockingReason.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter Blocked Reason.It cannot be Left blank.');", true);
                                    ModalPopupExtender1.Show();
                                    tbMain.Visible = true;
                                    return;
                                }

                            }


                            int effremain = 0;
                            if (txteffortremain.Text.Trim() != string.Empty)
                                effremain = Convert.ToInt32(txteffortremain.Text.Trim());

                            if (Convert.ToString(drpTaskStatus.SelectedItem.Text) == "Completed" || Convert.ToString(drpTaskStatus.SelectedItem.Text) == "completed")
                            {
                                if ((effremain == null) || (Convert.ToInt32(effremain) == 0))
                                {

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Effort Remaining Value should be either 0 or blank for the Task to be set as Completed');", true);
                                    ModalPopupExtender1.Show();
                                    tbMain.Visible = true;
                                    return;

                                }
                                //if(Convert.ToInt32(txteffortremain.Text) == 0)
                                //{

                                //}
                                //else
                                //{
                                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Validation Message(s) \\n\\n - Please Remove Effort Remain %');", true);
                                //ModalPopupExtender1.Show();
                                //tbMain.Visible = true;
                                //return;

                                //}

                            }
                            else
                            {
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Validation Message(s) \\n\\n - Please Remove Effort Remain %');", true);
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


                txtTaskUpdateDate.Text = DateTime.Now.ToShortDateString();
                TaskUpdateDate = Convert.ToDateTime(txtTaskUpdateDate.Text.Trim().ToString());

                if (txtTaskupdate.Text.Trim() != string.Empty)
                    Taskupdate =txtTaskupdate.Text.Trim().ToString();
                //if (txtTaskUpdateDate.Text.Trim() != string.Empty)
                //    TaskUpdateDate =Convert.ToDateTime(txtTaskUpdateDate.Text.Trim().ToString());
                if (txtActualStartDate.Text.Trim() != string.Empty)
                    ActualStartDate =Convert.ToDateTime(txtActualStartDate.Text.Trim().ToString());
                if (txtActualEndDate.Text.Trim() != string.Empty)
                    ActualEndDate =txtActualEndDate.Text.Trim();
                if (txtPer.Text.Trim() != string.Empty)
                    Per = Convert.ToInt32(txtPer.Text.Trim());
                if (drpTaskStatus.Text.Trim() != string.Empty)
                    TaskStatus = Convert.ToInt32(drpTaskStatus.Text.Trim());
                if (drpBlockedflag.Text.Trim() != string.Empty)
                    Blockedflag = drpBlockedflag.Text.Trim();
                if (txtBlockingReason.Text.Trim() != string.Empty)
                    BlockingReason = txtBlockingReason.Text.Trim();
                if (txtlastupdate.Text.Trim() != string.Empty)
                    Effortlastupdate =Convert.ToInt32(txtlastupdate.Text.Trim());
                if (txteffortremain.Text.Trim() != string.Empty)
                    effortremain =Convert.ToInt32( txteffortremain.Text.Trim());

                string Username = Request.Cookies["LoggedUserName"].Value;

                Task_Id = int.Parse(GrdWME.SelectedDataKey.Value.ToString());

                bl.UpdateTaskUpdateEntry(TaskUpdateDate, ActualStartDate, ActualEndDate, Per, TaskStatus, Blockedflag, Taskupdate, BlockingReason, Username,Effortlastupdate,effortremain, Task_Id);


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Update Details Updated Successfully');", true);
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
      //  lnkBtnAdd.Visible = false;
        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        btnUpdate.Enabled = false;
        drpIsActive.SelectedIndex = 0;
        //txtTaskID.Text = "";
        //txtCDate.Text = "";
        Taskeffortdays.Text = "";
        TextBox1.Text = "";
        txtEWstartDate.Text = "";
        txtEWEndDate.Text = "";
        drpTaskType.SelectedIndex = 0;
        drpDependencyTask.SelectedIndex = 0;
        drpProjectCode.SelectedIndex = 0;
        txtTaskDesc.Text = "";
        txteffortremain.Text = "";
        txtlastupdate.Text = "";
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdWME.PageIndex = e.NewPageIndex;
            BindWME("", "");
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

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "Tupdate"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "Tupdate"))
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

    protected void GrdWME_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            int Task_Id = Convert.ToInt32(GrdWME.Rows[e.RowIndex].Cells[0].Text);

            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.DeleteTaskDetails(Task_Id);
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

            txtBlockingReason.Enabled = false;

            txtTaskUpdateDate.Text = DateTime.Now.ToShortDateString();
            btnUpdate.Enabled = false;
            tbMain.Visible = true;
            pnsSave.Visible = true;
            
            btnCancel.Enabled = true;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnSave.Enabled = true;
            
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            

            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpBlockedflag_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBlockedflag.SelectedValue == "N")
        {
            txtBlockingReason.Enabled = false;
            txtBlockingReason.Text = string.Empty;
        }
        else
        {
            txtBlockingReason.Enabled = true;
        }
        UpdatePanel1.Update();
    }

    protected void GrdWME_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtActualEndDate.Text = "";
            txtActualStartDate.Text = "";
            txtBlockingReason.Text = "";
            txtPer.Text = "";
            txtTaskupdate.Text = "";
            drpBlockedflag.SelectedIndex = 0;
            drpTaskStatus.SelectedIndex = 0;
            txtTaskUpdateDate.Text = "";
            txtlastupdate.Text = "";
            txteffortremain.Text = "0";
            Taskeffortdays.Text = "";
            

            BusinessLogic bl = new BusinessLogic(sDataSource);
            int Task_Id = 0;
            int Project_Id = 0;

           // Project_Id = Convert.ToInt32(drpProjectCode.SelectedValue);

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

                    //if (ds.Tables[0].Rows[0]["Task_Date"] != null)
                    //{
                    //    txtCDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Task_Date"]).ToString("dd/MM/yyyy");
                    //    TextBox1.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Task_Date"]).ToString("dd/MM/yyyy");
                    //}

                    if (ds.Tables[0].Rows[0]["Effort_Task_Days"] != null)
                        Taskeffortdays.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["Effort_Task_Days"]).ToString();

                    if (ds.Tables[0].Rows[0]["Task_Name"] != null)
                        txtTaskName.Text = ds.Tables[0].Rows[0]["Task_Name"].ToString();

                    //if (ds.Tables[0].Rows[0]["Effort_Spend_Last_Update"] != null)
                    //    txtlastupdate.Text = ds.Tables[0].Rows[0]["Effort_Spend_Last_Update"].ToString();

                    //if (ds.Tables[0].Rows[0]["Effort_Remaining"] != null)
                    //    txteffortremain.Text = ds.Tables[0].Rows[0]["Effort_Remaining"].ToString();

                    if (ds.Tables[0].Rows[0]["Project_Code"] != null)
                    {
                        string sCs = Convert.ToString(ds.Tables[0].Rows[0]["Project_Code"]);
                        drpProjectCode.ClearSelection();
                        ListItem li = drpProjectCode.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCs));
                        if (li != null) li.Selected = true;
                    }

                    if (ds.Tables[0].Rows[0]["Task_Description"] != null)
                        txtTaskDesc.Text = ds.Tables[0].Rows[0]["Task_Description"].ToString();

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

                    if (ds.Tables[0].Rows[0]["BranchCode"] != null)
                    {
                        string sBranchCode = Convert.ToString(ds.Tables[0].Rows[0]["BranchCode"]);
                        drpBranch.ClearSelection();
                        ListItem li = drpBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sBranchCode));
                        if (li != null) li.Selected = true;
                    }

                    drpDependencyTask.Items.Clear();
                    drpDependencyTask.Items.Add(new ListItem("Select Dependency Task", "0"));
                    DataSet dsp = bl.GetDependencytaskupdate(connection, Project_Id);

                    drpDependencyTask.DataSource = dsp;
                    drpDependencyTask.DataBind();
                    drpDependencyTask.DataTextField = "Task_Name";
                    drpDependencyTask.DataValueField = "Task_Id";
                    UpdatePanel2.Update();
                    string sCs1 = Convert.ToString(ds.Tables[0].Rows[0]["Project_Code"]);
                    Project_Id = Convert.ToInt32(sCs1);
                    DataSet dst = bl.GetProjectForId(connection, Project_Id);
                    if ( dst!= null)
                    {
                        //if (dst.Tables[0].Rows.Count > 0)
                        //{
                        //    unitofmeasureheading.Text = "(" + Convert.ToString(dst.Tables[0].Rows[0]["Unit_Of_Measure"].ToString()) + ")";
                        //    UpdatePanel4.Update();
                        //}
                        //else
                        
                            unitofmeasureheading.Text = "(" + Convert.ToString(dst.Tables[0].Rows[0]["Unit_Of_Measure"].ToString()) + ")";
                            UpdatePanel4.Update();
                            Uniteffortremain.Text = "(" + Convert.ToString(dst.Tables[0].Rows[0]["Unit_Of_Measure"].ToString()) + ")";
                            UpdatePanel6.Update();
                            unitlastupdate.Text = "(" + Convert.ToString(dst.Tables[0].Rows[0]["Unit_Of_Measure"].ToString()) + ")";
                            UpdatePanel5.Update();
                        
                    }
                    else
                    {
                        unitofmeasureheading.Text = "(" + Convert.ToString(dst.Tables[0].Rows[0]["Unit_Of_Measure"].ToString()) + ")";
                        UpdatePanel4.Update();
                        Uniteffortremain.Text = "(" + Convert.ToString(dst.Tables[0].Rows[0]["Unit_Of_Measure"].ToString()) + ")";
                        UpdatePanel6.Update();
                        unitlastupdate.Text = "(" + Convert.ToString(dst.Tables[0].Rows[0]["Unit_Of_Measure"].ToString()) + ")";
                        UpdatePanel5.Update();
                    }
                }
            }


            DataSet dsd = bl.GetTaskUpdateForId(connection, Task_Id);

            if (dsd != null)
            {
                if (dsd.Tables[0].Rows.Count > 0)
                {
                    if (dsd.Tables[0].Rows[0]["Actual_Start_Date"] != null)
                        txtActualStartDate.Text = Convert.ToDateTime(dsd.Tables[0].Rows[0]["Actual_Start_Date"]).ToString("dd/MM/yyyy");

                    if (dsd.Tables[0].Rows[0]["Actual_End_Date"] != null)
                        txtActualEndDate.Text =dsd.Tables[0].Rows[0]["Actual_End_Date"].ToString();

                    if (dsd.Tables[0].Rows[0]["Task_Update_Date"] != null)
                        txtTaskUpdateDate.Text = Convert.ToDateTime(dsd.Tables[0].Rows[0]["Task_Update_Date"]).ToString("dd/MM/yyyy");

                    if (dsd.Tables[0].Rows[0]["Task_Status"] != null)
                    {
                        string sCus = Convert.ToString(dsd.Tables[0].Rows[0]["Task_Status"]);
                        drpTaskStatus.ClearSelection();
                        ListItem li = drpTaskStatus.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCus));
                        if (li != null) li.Selected = true;
                    }

                    if (dsd.Tables[0].Rows[0]["Per_of_Completion"] != null)
                        txtPer.Text = dsd.Tables[0].Rows[0]["Per_of_Completion"].ToString();

                    if (dsd.Tables[0].Rows[0]["Effort_Spend_Last_Update"] != null)
                        txtlastupdate.Text = dsd.Tables[0].Rows[0]["Effort_Spend_Last_Update"].ToString();

                    if (dsd.Tables[0].Rows[0]["Effort_Remaining"] != null)
                        txteffortremain.Text = dsd.Tables[0].Rows[0]["Effort_Remaining"].ToString();

                    if (dsd.Tables[0].Rows[0]["Task_update"] != null)
                        txtTaskupdate.Text = dsd.Tables[0].Rows[0]["Task_update"].ToString();

                    if (dsd.Tables[0].Rows[0]["Blocking_Reason"] != null)
                        txtBlockingReason.Text = dsd.Tables[0].Rows[0]["Blocking_Reason"].ToString();

                    if (dsd.Tables[0].Rows[0]["Blocked_Flag"] != null)
                        drpBlockedflag.SelectedValue = dsd.Tables[0].Rows[0]["Blocked_Flag"].ToString();

                    if (drpBlockedflag.SelectedValue == "N")
                    {
                        txtBlockingReason.Enabled = false;
                    }
                    else
                    {
                        txtBlockingReason.Enabled = true;
                    }

                    if(txtActualStartDate.Text==null)
                    {
                        ImageButton2.Enabled = true;
                    }
                    else
                    {
                        ImageButton2.Enabled = false;
                    }
                    UpdatePanel1.Update();
                    txtActualEndDate.Text = "";
                   // txtActualStartDate.Text = "";
                    txtBlockingReason.Text = "";
                    txtPer.Text = "";
                    txtTaskupdate.Text = "";
                    drpBlockedflag.SelectedIndex = 0;
                    drpTaskStatus.SelectedIndex = 0;
                    // txtTaskUpdateDate.Text = "";
                    txtlastupdate.Text = "";
                    txteffortremain.Text = "0";
                    Taskeffortdays.Text = "";
                }
                else
                {
                    txtBlockingReason.Enabled = false;
                    txtTaskUpdateDate.Text = DateTime.Now.ToShortDateString();
                }
            }
            else
            {
                txtBlockingReason.Enabled = false;
                txtTaskUpdateDate.Text = DateTime.Now.ToShortDateString();
                ImageButton2.Enabled = true;
            }



            DataSet dsdtt = bl.GetTaskUpdateHistoryList(connection, Task_Id);

             if (dsdtt != null)
             {
                 if (dsdtt.Tables[0].Rows.Count > 0)
                 {
                     GridView1.DataSource = dsdtt;
                     GridView1.DataBind();
                 }
                 else
                 {
                     GridView1.DataSource = null;
                     GridView1.DataBind();
                 }
             }
             else
             {
                 GridView1.DataSource = null;
                 GridView1.DataBind();
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

   
}
