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

public partial class ProjectEntry : System.Web.UI.Page
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

                if (bl.CheckUserHaveAdd(usernam, "Project"))
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
            btnsavereturn.Enabled = false;
            btnUpdate.Enabled = false;
            lnkBtnAdd.Visible = false;
            GrdWME.Columns[10].Visible = false;
            GrdWME.Columns[11].Visible = false;
        }
    }

    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        string Username = Request.Cookies["LoggedUserName"].Value;

        ds = bl.ListManager(Username);

        //ds = bl.ListExecutive();
        drpIncharge.DataSource = ds;
        drpIncharge.DataBind();
        drpIncharge.DataTextField = "empFirstName";
        drpIncharge.DataValueField = "empno";

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

        DataSet ds = bl.GetUserProjectList(connection, textSearch, dropDown,Username);

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

            //if(txtSearch.Text=="")
            //{
            //    BtnClearFilter.Enabled = false;
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    DateTime ProjectDate;
    DateTime EWStartDate;
    DateTime EWEndDate ;
    //DateTime ActStartDate;
    //DateTime ActEndDate;
    protected void btnSave_Click(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        //string ProjectDate = string.Empty;
        //string EWStartDate = string.Empty;
        //string EWEndDate = string.Empty;
        int empNO = 0;
        string ProjectName = string.Empty;
        string ActStartDate = string.Empty;
        string ActEndDate = string.Empty;
        string ProjectDesc = string.Empty;
        string Projectstatus = string.Empty;
        string ProjectCode = string.Empty;
        string unitofmeasure = string.Empty;
        int EffortDays = 0;
      //  string ActStartDate = string.Empty;
       // string ActStartDate = string.Empty;
        string ProjectRecordClosedDate = string.Empty;
        int ActEffortDays = 0;
        int DueEffortDate = 0;
        int StartDelayDate = 0;
        int EndDueDate = 0;
        int OverDueDate = 0;
    

        try
        {
            //btnSave.Enabled = false;

            if (Page.IsValid)
            {
                if (txtProjectCode.Text.Trim() != string.Empty)
                    ProjectCode = txtProjectCode.Text.Trim();

                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                if (bl.IsProjectCodeAlreadyFound(connection, ProjectCode))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Project ID \\'"+txtProjectCode.Text+"\\' already exists');", true);

                    ModalPopupExtender1.Show();
                    tbMain.Visible = true;
                    return;

                    
                }



                if (txtEffortDays.Text.Trim() != string.Empty)
                    EffortDays = Convert.ToInt32(txtEffortDays.Text.Trim());

                if (Convert.ToInt32(EffortDays) == 0 || Convert.ToInt32(EffortDays) == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Estimate Effort days should not Zero Or Blank');", true);

                    ModalPopupExtender1.Show();
                    tbMain.Visible = true;
                    return;

                }

                
                if (txtCDate.Text.Trim() != string.Empty)
                    ProjectDate =Convert.ToDateTime( txtCDate.Text.Trim().ToString());
                if (txtEWstartDate.Text.Trim() != string.Empty)
                    EWStartDate =Convert.ToDateTime( txtEWstartDate.Text.Trim().ToString());
                if (txtEWEndDate.Text.Trim() != string.Empty)
                    EWEndDate = Convert.ToDateTime( txtEWEndDate.Text.Trim().ToString());
                if (drpIncharge.Text.Trim() != string.Empty)
                    empNO = Convert.ToInt32(drpIncharge.Text.Trim());
                if (txtProjectName.Text.Trim() != string.Empty)
                    ProjectName = txtProjectName.Text.Trim();
                if (txtEffortDays.Text.Trim() != string.Empty)
                    EffortDays = Convert.ToInt32(txtEffortDays.Text.Trim());
                if (drpProjectstatus.Text.Trim() != string.Empty)
                    Projectstatus = drpProjectstatus.Text.Trim();
                if (txtProjectDesc.Text.Trim() != string.Empty)
                    ProjectDesc = txtProjectDesc.Text.Trim();
                //if (txtCLSDate.Text.Trim() != string.Empty)
                //    ProjectRecordClosedDate = txtCLSDate.Text.Trim();
                if (txtactdate.Text.Trim() != string.Empty)
                    ActStartDate =txtactdate.Text.Trim();
                if (txtacenddate.Text.Trim() != string.Empty)
                    ActEndDate =txtacenddate.Text.Trim();
                if (drpunitmeasure.Text.Trim() != string.Empty)
                    unitofmeasure = drpunitmeasure.Text.Trim();
                //if (txtacteffortdays.Text.Trim() != string.Empty)
                //    ActEffortDays = Convert.ToInt32(txtacteffortdays.Text.Trim());
                //if (txtproclodate.Text.Trim() != string.Empty)
                //    DueEffortDate = Convert.ToInt32(txtproclodate.Text.Trim());
                //if (txtdelaydate.Text.Trim() != string.Empty)
                //    StartDelayDate = Convert.ToInt32(txtdelaydate.Text.Trim());
                //if (txtduedays.Text.Trim() != string.Empty)
                //    EndDueDate = Convert.ToInt32(txtduedays.Text.Trim());
                //if (txtoverduedays.Text.Trim() != string.Empty)
                //    OverDueDate = Convert.ToInt32(txtoverduedays.Text.Trim());

                string Username = Request.Cookies["LoggedUserName"].Value;

                //DataSet checkemp = bl.SearchWME(WorkId, empNO, EWStartDate, EWEndDate, CreationDate, status);

                //if (checkemp == null || checkemp.Tables[0].Rows.Count == 0)
                //{
                bl.InsertProjectEntry(ProjectCode, ProjectDate, EWStartDate, EWEndDate, empNO, ProjectName, EffortDays, Projectstatus, ProjectDesc, Username, ActStartDate, ActEndDate,unitofmeasure);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('New Project Details Saved Successfully.');", true);
                    Reset();
                    ResetSearch();
                    BindWME("", "");
                    //MyAccordion.Visible = true;
                    tbMain.Visible = false;
                    GrdWME.Visible = true;
                    lnkBtnAdd_Click(sender, e);
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
            int WorkId = 0;
            //string ProjectDate = string.Empty;
            //string EWStartDate = string.Empty;
            //string EWEndDate = string.Empty;
            int empNO = 0;
            string Workdet = string.Empty;
            string ActStartDate = string.Empty;
            string ActEndDate = string.Empty;
            string ProjectName = string.Empty;
            string status = string.Empty;
            string ProjectDesc = string.Empty;
            string Projectstatus = string.Empty;
            string ProjectCode = string.Empty;
            string unitofmeasure = string.Empty;
            int EffortDays = 0;
            int Project_Id = 0;
            string ProjectRecordClosedDate = string.Empty;
            int ActEffortDays = 0;
            int DueEffortDate = 0;
            int StartDelayDate = 0;
            int EndDueDate = 0;
            int OverDueDate = 0;

            if (Page.IsValid)
            {              
                if (txtProjectCode.Text.Trim() != string.Empty)
                    ProjectCode = txtProjectCode.Text.Trim();
                if (txtCDate.Text.Trim() != string.Empty)
                    ProjectDate =Convert.ToDateTime( txtCDate.Text.Trim().ToString());
                if (txtEWstartDate.Text.Trim() != string.Empty)
                    EWStartDate =Convert.ToDateTime(txtEWstartDate.Text.Trim().ToString());
                if (txtEWEndDate.Text.Trim() != string.Empty)
                    EWEndDate =Convert.ToDateTime( txtEWEndDate.Text.Trim().ToString());
                if (drpIncharge.Text.Trim() != string.Empty)
                    empNO = Convert.ToInt32(drpIncharge.Text.Trim());
                if (txtProjectName.Text.Trim() != string.Empty)
                    ProjectName = txtProjectName.Text.Trim();
                if (txtEffortDays.Text.Trim() != string.Empty)
                    EffortDays = Convert.ToInt32(txtEffortDays.Text.Trim());
                if (drpProjectstatus.Text.Trim() != string.Empty)
                    Projectstatus = drpProjectstatus.Text.Trim();
                if (txtProjectDesc.Text.Trim() != string.Empty)
                    ProjectDesc = txtProjectDesc.Text.Trim();
                if (drpunitmeasure.Text.Trim() != string.Empty)
                    unitofmeasure = drpunitmeasure.Text.Trim();

                //if (txtCLSDate.Text.Trim() != string.Empty)
                //    ProjectRecordClosedDate = txtCLSDate.Text.Trim();
                if (txtactdate.Text.Trim() != string.Empty)
                    ActStartDate =txtactdate.Text.Trim();
                if (txtacenddate.Text.Trim() != string.Empty)
                    ActEndDate = txtacenddate.Text.Trim();
                //if (txtacteffortdays.Text.Trim() != string.Empty)
                //    ActEffortDays = Convert.ToInt32(txtacteffortdays.Text.Trim());
                //if (txtproclodate.Text.Trim() != string.Empty)
                //    DueEffortDate = Convert.ToInt32(txtproclodate.Text.Trim());
                //if (txtdelaydate.Text.Trim() != string.Empty)
                //    StartDelayDate = Convert.ToInt32(txtdelaydate.Text.Trim());
                //if (txtduedays.Text.Trim() != string.Empty)
                //    EndDueDate = Convert.ToInt32(txtduedays.Text.Trim());
                //if (txtoverduedays.Text.Trim() != string.Empty)
                //    OverDueDate = Convert.ToInt32(txtoverduedays.Text.Trim());

                string Username = Request.Cookies["LoggedUserName"].Value;

                Project_Id = int.Parse(GrdWME.SelectedDataKey.Value.ToString());

                bl.UpdateProjectEntry(ProjectCode, ProjectDate, EWStartDate, EWEndDate, empNO, ProjectName, EffortDays, Projectstatus, ProjectDesc, Username, Project_Id, ActStartDate, ActEndDate,unitofmeasure);


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Project Details Updated Successfully.');", true);
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
        lnkBtnAdd.Visible = true;
        btnSave.Enabled = true;
        btnsavereturn.Enabled = true;
        btnCancel.Enabled = true;
        btnUpdate.Enabled = false;
        txtProjectCode.Text = "";
        txtProjectDesc.Text = "";
        txtCDate.Text = "";
        txtEWstartDate.Text = "";
        txtEWEndDate.Text = "";
        txtProjectName.Text = "";
        txtEffortDays.Text = "0";
        //txtCLSDate.Text = "";
        txtactdate.Text = "";
        txtacenddate.Text = "";
        //txtacteffortdays.Text = "";
        //txtproclodate.Text = "";
        ////txtdelaydate.Text = "";
        //txtduedays.Text = "";
        //txtoverduedays.Text = "";
        drpProjectstatus.SelectedIndex = 0;
        txtProjectCode.Enabled = true;
        //drpmeasure_SelectedIndexChanged = "";

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


    protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckTaskNameUsed(int.Parse(((HiddenField)e.Row.FindControl("ProjectID")).Value)))
   
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }
           
                //BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "Project"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "Project"))
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

            //int Project_ID = Convert.ToInt32(GrdWME.Rows[e.RowIndex].Cells[0].Text);
            int project_ID = Convert.ToInt32(GrdWME.DataKeys[e.RowIndex].Value.ToString());

           //int  Project_ID = Convert.ToInt32(GrdWME.SelectedDataKey.Value.ToString());

            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.DeleteProjectDetails(project_ID);
            BindWME("", "");
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            btnsavereturn.Enabled = true;
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
           
            headtitle.Text = "Add New Project";
            tabMaster.HeaderText = "New Project Details";
            txtCDate.Text = DateTime.Now.ToShortDateString();
            btnUpdate.Enabled = false;
            tbMain.Visible = true;
            pnsSave.Visible = true;
            drpProjectstatus.Enabled = false;
            btnCancel.Enabled = true;
            btnSave.Visible = true;
            btnsavereturn.Visible = true;
            btnUpdate.Visible = false;
            btnSave.Enabled = true;
            btnsavereturn.Enabled = true;
            drpunitmeasure.Visible = true;
            //estimateheading.Text = "Estimate duration in(Days)";
           
            //txtduedays.Enabled = false;
            
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            

            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        ddCriteria.SelectedIndex = 0;
        BindWME("", "");
    }
    //protected void txtCLsDate_OnTextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string startdate = Convert.ToDateTime(txtCLSDate.Text).ToString("dd/MM/yyyy hh:mm tt");

    //        string enddate = Convert.ToDateTime(txtCDate.Text).ToString("dd/MM/yyyy hh:mm tt");

    //        TimeSpan ts = Convert.ToDateTime(startdate) - Convert.ToDateTime(enddate);

    //        int days = Convert.ToInt32(ts.TotalDays);
    //        if (days > 0)
    //        {
    //            txtproclodate.Text = Convert.ToString(days);

    //        }
    //        else
    //        {
    //            days = 0;
    //            txtproclodate.Text = Convert.ToString(days);
    //        }
    //        UpdatePanel5.Update();
    //    }
    //    catch (Exception ex)
    //    {
    //        TroyLiteExceptionManager.HandleException(ex);
    //    }
    //}

    //protected void txtActEndDate_OnTextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string startdate = Convert.ToDateTime(txtactdate.Text).ToString("dd/MM/yyyy");

    //        string enddate = Convert.ToDateTime(txtacenddate.Text).ToString("dd/MM/yyyy");

    //        TimeSpan ts = Convert.ToDateTime(enddate) - Convert.ToDateTime(startdate);
    //        int days = Convert.ToInt32(ts.TotalDays);
    //        if (days > 0)
    //        {
    //            txtacteffortdays.Text = Convert.ToString(days);

    //        }
    //        else
    //        {
    //            days = 0;
    //            txtacteffortdays.Text = Convert.ToString(days);

    //        }

    //        //int days = Convert.ToInt32(ts.TotalDays);

    //        //txtEffortDays.Text = Convert.ToString(days);
    //        UpdatePanel3.Update();
    //    }
    //    catch (Exception ex)
    //    {
    //        TroyLiteExceptionManager.HandleException(ex);
    //    }
    //}






    //protected void txtActDate_OnTextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string startdate = Convert.ToDateTime(txtactdate.Text).ToString("dd/MM/yyyy");

    //        string enddate = Convert.ToDateTime(txtCDate.Text).ToString("dd/MM/yyyy");

    //        TimeSpan ts = Convert.ToDateTime(enddate) - Convert.ToDateTime(startdate);

    //        int days = Convert.ToInt32(ts.TotalDays);
    //        if (days > 0)
    //        {
    //            txtacteffortdays.Text = Convert.ToString(days);

    //        }
    //        else
    //        {
    //            days = 0;
    //            txtacteffortdays.Text = Convert.ToString(days);
    //        }
    //        //string startdate1 = Convert.ToDateTime(txtactdate.Text).ToString("dd/MM/yyyy");

    //        //string enddate1 = Convert.ToDateTime(txtEWstartDate.Text).ToString("dd/MM/yyyy");

    //        //TimeSpan ts1 = Convert.ToDateTime(enddate1) - Convert.ToDateTime(startdate1);

    //        //int days1 = Convert.ToInt32(ts1.TotalDays);
    //        //if (days1 > 0)
    //        //{
    //        //    txtacteffortdays.Text = Convert.ToString(days1);

    //        //}
    //        //else
    //        //{
    //        //    days1 = 0;
    //        //    txtacteffortdays.Text = Convert.ToString(days1);
    //        //}
    //        UpdatePanel3.Update();
    //        string startdate1 = Convert.ToDateTime(txtEWstartDate.Text).ToString("dd/MM/yyyy");

    //        string enddate1 = Convert.ToDateTime(txtactdate.Text).ToString("dd/MM/yyyy");

    //        TimeSpan ts1 = Convert.ToDateTime(enddate1) - Convert.ToDateTime(startdate1);
    //        int days1 = Convert.ToInt32(ts1.TotalDays);
    //        if (days1 > 0)
    //        {
    //            txtdelaydate.Text = Convert.ToString(days1);

    //        }
    //        else
    //        {
    //            days1 = 0;
    //            txtdelaydate.Text = Convert.ToString(days1);

    //        }
    //        UpdatePanel6.Update();
    //        // UpdatePanel3.Update();
    //    }
    //    catch (Exception ex)
    //    {
    //        TroyLiteExceptionManager.HandleException(ex);
    //    }

    //}


    //protected void txtActEndDate_OnTextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string startdate = Convert.ToDateTime(txtactdate.Text).ToString("dd/MM/yyyy");

    //        string enddate = Convert.ToDateTime(txtacenddate.Text).ToString("dd/MM/yyyy");

    //        TimeSpan ts = Convert.ToDateTime(enddate) - Convert.ToDateTime(startdate);
    //        int days = Convert.ToInt32(ts.TotalDays);
    //        if (days > 0)
    //        {
    //            txtacteffortdays.Text = Convert.ToString(days);

    //        }
    //        else
    //        {
    //            days = 0;
    //            txtacteffortdays.Text = Convert.ToString(days);

    //        }

           
    //        UpdatePanel3.Update();
      


    //        string startdate1 = Convert.ToDateTime(txtEWstartDate.Text).ToString("dd/MM/yyyy");

    //        string enddate1 = Convert.ToDateTime(txtactdate.Text).ToString("dd/MM/yyyy");

    //        TimeSpan ts1 = Convert.ToDateTime(enddate1) - Convert.ToDateTime(startdate1);
    //        int days1 = Convert.ToInt32(ts1.TotalDays);
    //        if (days1 > 0)
    //        {
    //            txtdelaydate.Text = Convert.ToString(days1);

    //        }
    //        else
    //        {
    //            days1 = 0;
    //            txtdelaydate.Text = Convert.ToString(days1);

    //        }
    //        UpdatePanel6.Update();



    //        string startdate2 = Convert.ToDateTime(txtacenddate.Text).ToString("dd/MM/yyyy");

    //        DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
    //        string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

    //        TimeSpan ts2 = Convert.ToDateTime(dtaa) - Convert.ToDateTime(startdate2);
    //        int days2 = Convert.ToInt32(ts2.TotalDays);
    //        if (days2 > 0)
    //        {
    //            txtoverduedays.Text = Convert.ToString(days2);

    //        }
    //        else
    //        {
                
    //            txtoverduedays.Text = Convert.ToString(days2);

    //        }
    //        UpdatePanel8.Update();

    //        string startdate12 = Convert.ToDateTime(txtEWEndDate.Text).ToString("dd/MM/yyyy");

    //        string enddate12 = Convert.ToDateTime(txtacenddate.Text).ToString("dd/MM/yyyy");

    //        TimeSpan ts12 = Convert.ToDateTime(enddate12) - Convert.ToDateTime(startdate12);
    //        int days12 = Convert.ToInt32(ts12.TotalDays);
    //        if (days12 > 0)
    //        {
    //            txtduedays.Text = Convert.ToString(days12);

    //        }
    //        else
    //        {
    //            days12 = 0;
    //            txtduedays.Text = Convert.ToString(days12);

    //        }
    //        UpdatePanel7.Update();

    //    }
           
    //    catch (Exception ex)
    //    {
    //        TroyLiteExceptionManager.HandleException(ex);
    //    }

    //}



    //protected void drpmeasure_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (drpunitmeasure.SelectedValue == "Days")
    //        {
    //            estimateheading.Text = "Estimate Duration in(Days)";
    //        }
    //        else if (drpunitmeasure.SelectedValue == "Hours")
    //        {
    //            estimateheading.Text = "Estimate Duration in(Hours)";
    //        }
    //        UpdatePanel2.Update();
    //    }
    //    catch (Exception ex)
    //    {
    //        TroyLiteExceptionManager.HandleException(ex);
    //    }

    //}


    //protected void drpmeasure_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int date = Convert.ToInt32(txtEffortDays.Text);

    //        //string dtaa = Convert.ToDateTime(date).ToString("dd/MM/yyyy");              
    //        //DateTime calculat = Convert.ToDateTime(dtaa);

    //        if (drpunitmeasure.SelectedValue == "Days")
    //        {
    //            int days = Convert.ToInt32(txtEffortDays.Text);

    //            days = days / 24;

                

    //            txtEffortDays.Text = days.ToString();

    //            //if (drpunitmeasure.SelectedValue == "Days")
    //            //{
    //            //    estimateheading.Text= "Estimate Duration in(Days)";

    //            //}
    //            //estimateheading.Text = "Estimate Duration in(days)";
    //        }
         
               
    //        //else if (drpunitmeasure.SelectedValue == "Months")
    //        //{
    //        //    int Months = Convert.ToInt32(txtEffortDays.Text);

    //        //    //DateTime dat =Convert.ToString(date.AddMonths(Months));
    //        //    txtEffortDays.Text = Months.ToString();
    //        //}
    //        else if (drpunitmeasure.SelectedValue == "Hours")
    //        {
    //            int hours = Convert.ToInt32(txtEffortDays.Text);

    //            hours = 24 * hours;
    //            estimateheading.Text = "Estimate Duration in(Hours)";
              
    //            //DateTime dat = Convert.ToDateTime(hours.ToS);
    //            //TimeSpan ts = TimeSpan.FromHours(12);
    //            //DateTime dt = Convert.ToDateTime(ts.ToString());
    //            //Weeks = 7 * Weeks;
    //            //DateTime dat = date.AddDays(Weeks);
    //            txtEffortDays.Text = hours.ToString();
    //            //estimateheading.Text = "Estimate Duration in (hours)";
    //            //if (drpunitmeasure.SelectedValue == "Hours")
    //            //{
    //            //    estimateheading.Text = "Estimate Duration in(Hours)";

    //            //}
    //        }
           

    //        UpdatePanel2.Update();
    //    }
        
    //    catch (Exception ex)
    //    {
    //        TroyLiteExceptionManager.HandleException(ex);
    //    }

    //}





    //protected void txtEWstartDate_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string startdate = Convert.ToDateTime(txtEWstartDate.Text).ToString("dd/MM/yyyy");

    //        string enddate = Convert.ToDateTime(txtEWEndDate.Text).ToString("dd/MM/yyyy");

    //        TimeSpan ts = Convert.ToDateTime(enddate) - Convert.ToDateTime(startdate);
           
    //        int days = Convert.ToInt32(ts.TotalDays);
    //        if (days > 0)
    //        {
    //            txtEffortDays.Text = Convert.ToString(days);
                
    //        }
    //        else
    //        {
    //            days = 0;
    //            txtEffortDays.Text = Convert.ToString(days);
    //        }
    //        UpdatePanel2.Update();
    //    }
    //    catch (Exception ex)
    //    {
    //        TroyLiteExceptionManager.HandleException(ex);
    //    }
    //}

    //protected void txtEWEndDate_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string startdate = Convert.ToDateTime(txtEWstartDate.Text).ToString("dd/MM/yyyy");

    //        string enddate = Convert.ToDateTime(txtEWEndDate.Text).ToString("dd/MM/yyyy");

    //        TimeSpan ts = Convert.ToDateTime(enddate) - Convert.ToDateTime(startdate);
    //        int days = Convert.ToInt32(ts.TotalDays);
    //        if (days > 0)
    //        {
    //            txtEffortDays.Text = Convert.ToString(days);

    //        }
    //        else
    //        {
    //            days = 0;
    //            txtEffortDays.Text =Convert.ToString(days);
                
    //        }
    //        UpdatePanel2.Update();
    //        //int days = Convert.ToInt32(ts.TotalDays);

    //        //txtEffortDays.Text = Convert.ToString(days);
    //        UpdatePanel2.Update();


    //        string startdate1 = Convert.ToDateTime(txtEWEndDate.Text).ToString("dd/MM/yyyy");

    //        string enddate1 = Convert.ToDateTime(txtacenddate.Text).ToString("dd/MM/yyyy");

    //        TimeSpan ts1 = Convert.ToDateTime(enddate1) - Convert.ToDateTime(startdate1);
    //        int days1 = Convert.ToInt32(ts1.TotalDays);
    //        if (days1 > 0)
    //        {
    //            txtduedays.Text = Convert.ToString(days1);

    //        }
    //        else
    //        {
    //            days1 = 0;
    //            txtduedays.Text = Convert.ToString(days1);

    //        }
    //        UpdatePanel7.Update();

    //    }
    //    catch (Exception ex)
    //    {
    //        TroyLiteExceptionManager.HandleException(ex);
    //    }
    //}

    protected void GrdWME_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int Project_ID = 0;

            headtitle.Text = "Update Project Details";
            tabMaster.HeaderText = "Update Project Details";
            estimateheading.Visible=false;

            string connection = Request.Cookies["Company"].Value;

            if (GrdWME.SelectedDataKey.Value != null && GrdWME.SelectedDataKey.Value.ToString() != "")
                Project_ID = Convert.ToInt32(GrdWME.SelectedDataKey.Value.ToString());

            DataSet ds = bl.GetProjectForId(connection,Project_ID);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Expected_Start_Date"] != null)
                        txtEWstartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Expected_Start_Date"]).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["Expected_End_Date"] != null)
                        txtEWEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Expected_End_Date"]).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["Project_Date"] != null)
                        txtCDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Project_Date"]).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["Project_Name"] != null)
                        txtProjectName.Text = ds.Tables[0].Rows[0]["Project_Name"].ToString();

                    if (ds.Tables[0].Rows[0]["Project_Code"] != null)
                        txtProjectCode.Text = ds.Tables[0].Rows[0]["Project_Code"].ToString();

                    txtProjectCode.Enabled = false;

                    if (ds.Tables[0].Rows[0]["Project_Description"] != null)
                        txtProjectDesc.Text = ds.Tables[0].Rows[0]["Project_Description"].ToString();

                    if (ds.Tables[0].Rows[0]["Project_Description"] != null)
                        txtProjectDesc.Text = ds.Tables[0].Rows[0]["Project_Description"].ToString();

                    if (ds.Tables[0].Rows[0]["Project_Status"] != null)
                        drpProjectstatus.SelectedValue = ds.Tables[0].Rows[0]["Project_Status"].ToString();

                    if (ds.Tables[0].Rows[0]["Expected_Effort_Days"] != null)
                        txtEffortDays.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["Expected_Effort_Days"]).ToString();

                    //if(ds.Tables[0].Rows[0]["Project_End_Date"]!=null)
                    //    txtCLSDate.Text=ds.Tables[0].Rows[0]["Project_End_Date"].ToString();

                    if (ds.Tables[0].Rows[0]["Actual_Start_Date"] != null)
                        txtactdate.Text = ds.Tables[0].Rows[0]["Actual_Start_Date"].ToString();

                    if (ds.Tables[0].Rows[0]["Actual_End_Date"] != null)
                        txtacenddate.Text = ds.Tables[0].Rows[0]["Actual_End_Date"].ToString();

                    if (ds.Tables[0].Rows[0]["Unit_Of_Measure"] != null)
                        drpunitmeasure.SelectedValue = ds.Tables[0].Rows[0]["Unit_Of_Measure"].ToString();

                     //if (ds.Tables[0].Rows[0]["Actual_Effort_Days"] != null)
                     //    txtacteffortdays.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["Actual_Effort_Days"]).ToString();

                     //if (ds.Tables[0].Rows[0]["Project_Closed"] != null)
                     //    txtProjectDesc.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["Project_Closed"]).ToString();

                     //if (ds.Tables[0].Rows[0]["Actual_Start_Delay"] != null)
                     //    txtdelaydate.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["Actual_Start_Delay"]).ToString();

                     //if (ds.Tables[0].Rows[0]["Actual_End_Delay"] != null)
                     //    txtduedays.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["Actual_End_Delay"]).ToString();

                     //if (ds.Tables[0].Rows[0]["Over_Due_Days"] != null)
                     //    txtoverduedays.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["Over_Due_Days"]).ToString();

                    if (ds.Tables[0].Rows[0]["Project_Manager_Id"] != null)
                    {
                        string sCustomer = Convert.ToString(ds.Tables[0].Rows[0]["Project_Manager_Id"]);
                        drpIncharge.ClearSelection();
                        ListItem li = drpIncharge.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }
                }
            }

            drpIncharge.Enabled = false;
            drpProjectstatus.Enabled = true;
            btnUpdate.Enabled = true;
            pnsSave.Visible = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = false;
            btnsavereturn.Enabled = false;
            tbMain.Visible = true;
            btnSave.Visible = false;
            btnsavereturn.Visible = false;
            btnUpdate.Visible = true;
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

        //try
        //{
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "Projects_" + DateTime.Now.ToString() + ".xls";

            BusinessLogic objBL;
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            DataSet ds = new DataSet();

            string connection = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;
            else
                Response.Redirect("Login.aspx");

            ds = objBL.GetProjectList(connection, "", "");           
           
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Project Name"));
                    //dt.Columns.Add(new DataColumn("CreationDate"));
                    //dt.Columns.Add(new DataColumn("ProspectCustName"));
                    //dt.Columns.Add(new DataColumn("Address"));
                    //dt.Columns.Add(new DataColumn("Mobile"));
                    //dt.Columns.Add(new DataColumn("Landline"));
                    //dt.Columns.Add(new DataColumn("Email"));
                    //dt.Columns.Add(new DataColumn("ModeOfContact"));
                    //dt.Columns.Add(new DataColumn("PersonalResponsible"));
                    //dt.Columns.Add(new DataColumn("BusinessType"));
                    //dt.Columns.Add(new DataColumn("Branch"));
                    //dt.Columns.Add(new DataColumn("Status"));
                    //dt.Columns.Add(new DataColumn("LastCompletedAction"));
                    //dt.Columns.Add(new DataColumn("NextAction"));
                    //dt.Columns.Add(new DataColumn("Category"));
                    //dt.Columns.Add(new DataColumn("ContactedDate"));
                    //dt.Columns.Add(new DataColumn("ContactSummary"));

                    DataRow dr_export1 = dt.NewRow();
                    dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                            DataRow dr_export = dt.NewRow();
                            dr_export["Project Name"] = dr["Project_Name"];
                            //dr_export["CreationDate"] = dr["CreationDate"];
                            //dr_export["ProspectCustName"] = dr["ProspectCustName"];
                            //dr_export["Address"] = dr["Address"];
                            //dr_export["Mobile"] = dr["Mobile"];
                            //dr_export["Landline"] = dr["Landline"];
                            //dr_export["Email"] = dr["Email"];
                            //dr_export["ModeOfContact"] = dr["ModeOfContact"];
                            //dr_export["PersonalResponsible"] = dr["PersonalResponsible"];
                            //dr_export["BusinessType"] = dr["BusinessType"];
                            //dr_export["Branch"] = dr["Branch"];
                            //dr_export["Email"] = dr["Email"];
                            //dr_export["Status"] = dr["Status"];
                            //dr_export["LastCompletedAction"] = dr["LastCompletedAction"];
                            //dr_export["NextAction"] = dr["NextAction"];
                            //dr_export["Category"] = dr["Category"];
                            //dr_export["ContactedDate"] = dr["ContactedDate"];
                            //dr_export["ContactSummary"] = dr["ContactSummary"];
                            dt.Rows.Add(dr_export);
                    }

                    ExportToExcel(filename, dt);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }
        //}
        //catch (Exception ex)
        //{
        //    TroyLiteExceptionManager.HandleException(ex);
        //}
    }

    public void ExportToExcel(string filename, DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();
            dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
            dgGrid.HeaderStyle.Font.Bold = true;
            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
    }


    protected void btnsavereturn_Click(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        //string ProjectDate = string.Empty;
        //string EWStartDate = string.Empty;
        //string EWEndDate = string.Empty;
        int empNO = 0;
        string ProjectName = string.Empty;
        string ActStartDate = string.Empty;
        string ActEndDate = string.Empty;
        string ProjectDesc = string.Empty;
        string Projectstatus = string.Empty;
        string ProjectCode = string.Empty;
        string unitofmeasure = string.Empty;
        int EffortDays = 0;
        //string ActStartDate = string.Empty;
        //string ActStartDate = string.Empty;
        string ProjectRecordClosedDate = string.Empty;
        int ActEffortDays = 0;
        int DueEffortDate = 0;
        int StartDelayDate = 0;
        int EndDueDate = 0;
        int OverDueDate = 0;


        try
        {
            if (Page.IsValid)
            {
                if (txtProjectCode.Text.Trim() != string.Empty)
                    ProjectCode = txtProjectCode.Text.Trim();

                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                if (bl.IsProjectCodeAlreadyFound(connection, ProjectCode))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Project ID \\'" + txtProjectCode.Text + "\\' already exists');", true);

                   // ModalPopupExtender1.Show();
                    tbMain.Visible = true;
                    ModalPopupExtender1.Show();
                    return;


                }


                if (txtCDate.Text.Trim() != string.Empty)
                    ProjectDate =Convert.ToDateTime( txtCDate.Text.Trim().ToString());
                if (txtEWstartDate.Text.Trim() != string.Empty)
                    EWStartDate =Convert.ToDateTime( txtEWstartDate.Text.Trim().ToString());
                if (txtEWEndDate.Text.Trim() != string.Empty)
                    EWEndDate =Convert.ToDateTime( txtEWEndDate.Text.Trim().ToString());
                if (drpIncharge.Text.Trim() != string.Empty)
                    empNO = Convert.ToInt32(drpIncharge.Text.Trim());
                if (txtProjectName.Text.Trim() != string.Empty)
                    ProjectName = txtProjectName.Text.Trim();
                if (txtEffortDays.Text.Trim() != string.Empty)
                    EffortDays = Convert.ToInt32(txtEffortDays.Text.Trim());
                if (drpProjectstatus.Text.Trim() != string.Empty)
                    Projectstatus = drpProjectstatus.Text.Trim();
                if (txtProjectDesc.Text.Trim() != string.Empty)
                    ProjectDesc = txtProjectDesc.Text.Trim();
                //if (txtCLSDate.Text.Trim() != string.Empty)
                //    ProjectRecordClosedDate = txtCLSDate.Text.Trim();
                if (txtactdate.Text.Trim() != string.Empty)
                    ActStartDate =txtactdate.Text.Trim();
                if (txtacenddate.Text.Trim() != string.Empty)
                    ActEndDate =txtacenddate.Text.Trim();
                if (drpunitmeasure.Text.Trim() != string.Empty)
                    unitofmeasure = drpunitmeasure.Text.Trim();
                //if (txtacteffortdays.Text.Trim() != string.Empty)
                //    ActEffortDays = Convert.ToInt32(txtacteffortdays.Text.Trim());
                //if (txtproclodate.Text.Trim() != string.Empty)
                //    DueEffortDate = Convert.ToInt32(txtproclodate.Text.Trim());
                //if (txtdelaydate.Text.Trim() != string.Empty)
                //    StartDelayDate = Convert.ToInt32(txtdelaydate.Text.Trim());
                //if (txtduedays.Text.Trim() != string.Empty)
                //    EndDueDate = Convert.ToInt32(txtduedays.Text.Trim());
                //if (txtoverduedays.Text.Trim() != string.Empty)
                //    OverDueDate = Convert.ToInt32(txtoverduedays.Text.Trim());

                string Username = Request.Cookies["LoggedUserName"].Value;

                //DataSet checkemp = bl.SearchWME(WorkId, empNO, EWStartDate, EWEndDate, CreationDate, status);

                //if (checkemp == null || checkemp.Tables[0].Rows.Count == 0)
                //{
                bl.InsertProjectEntry(ProjectCode, ProjectDate, EWStartDate, EWEndDate, empNO, ProjectName, EffortDays, Projectstatus, ProjectDesc, Username, ActStartDate, ActEndDate, unitofmeasure);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('New Project Details Saved Successfully.');", true);
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
               // btnSave_Click(sender,e);
                
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    //protected void Dashboard_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("DashBoard.aspx");
    //}
    //protected void bulkupload_Click(object sender, EventArgs e)
    //{
       
    //    Response.Redirect("DashBoard.aspx");
    //    bulk.Text = "OnConstruction";
       
    //}
    //protected void printall_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("DashBoard.aspx");
    //}
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (txtSearch.Text == "" || txtSearch.Text == " ")
        {
            BtnClearFilter.Enabled = false;
        }
        else
        {
            BtnClearFilter.Enabled = true;
        }
    }
}
