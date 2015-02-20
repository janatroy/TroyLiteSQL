using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Holiday_HRAdminHoliday : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");

        HolidaySummaryGridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        HolidaySummaryGridSource.SelectParameters.Add(new ControlParameter("txtSearchInput", TypeCode.String, txtSearchInput.UniqueID, "Text"));
        HolidaySummaryGridSource.SelectParameters.Add(new ControlParameter("searchCriteria", TypeCode.String, ddlSearchCriteria.UniqueID, "SelectedValue"));
        HolidaySummaryGridSource.SelectParameters.Add(new CookieParameter("UserId", "LoggedUserName"));

    }

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
                lnkBtnAddHoliday.Visible = false;
                //grdViewAttendanceSummary.Columns[7].Visible = false;
                //grdViewAttendanceSummary.Columns[8].Visible = false;
            }
            grdViewHolidaySummary.PageSize = 8;

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);

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

    protected void lnkBtnAddHoliday_Click(object sender, EventArgs e)
    {
        try
        {
            //LeaveDetailPopUp.Visible = true;
            //ModalPopupExtender1.Show();

            frmHolidayAdd.ChangeMode(FormViewMode.Insert);
            frmHolidayAdd.Visible = true;
            HolidayDetailPopUp.Visible = true;
            ModalPopupExtender1.Show();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdViewHolidaySummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                frmHolidayAdd.Visible = true;
                frmHolidayAdd.ChangeMode(FormViewMode.Edit);
                ModalPopupExtender1.Show();
                //GrdViewLedger.Visible = false;
                //lnkBtnAdd.Visible = false;
                ////MyAccordion.Visible = false;
                //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
                //Accordion1.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdViewHolidaySummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
	if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            DateTime statusDate = (DateTime)row["Date"];
            
            int result = DateTime.Compare(statusDate.Date, DateTime.Now.Date);

            if (result < 0)
            {
                Image btn_Del_Diasable = (Image)e.Row.FindControl("lnkBDisabled");
                btn_Del_Diasable.Enabled = true;
            }
            else
            {
                Image btn_Del_Enable = (Image)e.Row.FindControl("lnkB");
                btn_Del_Enable.Enabled = true;
            }
        }	
    }
    protected void frmHolidayAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void frmHolidayAdd_ItemCreated(object sender, EventArgs e)
    {

    }
    protected void frmHolidayAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }
    protected void frmHolidayAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                //MyAccordion.Visible = true;
                lnkBtnAddHoliday.Visible = true;
                frmHolidayAdd.Visible = false;
                grdViewHolidaySummary.Visible = true;
                System.Threading.Thread.Sleep(1000);
                grdViewHolidaySummary .DataBind();
                StringBuilder scriptMsg = new StringBuilder();
                scriptMsg.Append("alert('Holiday Saved Successfully.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), scriptMsg.ToString(), true);


                if (Request.QueryString["myname"] != null)
                {

                    string myNam = Request.QueryString["myname"].ToString();
                    if (myNam == "NEWSUP")
                    {
                        Response.Redirect("Purchase.aspx?myname=" + "NEWPUR");
                    }
                }
            }
            else
            {
                if (e.Exception != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('Holiday with this name already exists, Please try with a different name.');");

                    if (e.Exception.InnerException != null)
                    {
                        if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                            (e.Exception.InnerException.Message.IndexOf("Holiday Name Exists") > -1))
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Exception: " + e.Exception.Message + e.Exception.StackTrace, true);
                    }
                }
                e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmHolidayAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
		
            if (e.Exception == null)
            {
                lnkBtnAddHoliday.Visible = true;
                frmHolidayAdd.Visible = false;
                //GrdViewLedger.Visible = true;
                grdViewHolidaySummary.Visible = true;
                System.Threading.Thread.Sleep(1000);
                grdViewHolidaySummary.DataBind();
                //MyAccordion.Visible = true;
                //GrdViewLedger.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Holiday Details Updated Successfully.');", true);
            }
            else
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('Holiday with this name already exists, Please try with a different name.');");

                if (e.Exception.InnerException != null)
                {
                    if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                        (e.Exception.InnerException.Message.IndexOf("Holiday Name Exists") > -1))
                    {
                        e.ExceptionHandled = true;
                        e.KeepInEditMode = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                        return;
                    }

                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Exception: " + e.Exception.Message + e.Exception.StackTrace, true);
		
                e.ExceptionHandled = true;
                e.KeepInEditMode = true;
	
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {

    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {

    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //MyAccordion.Visible = true;
            lnkBtnAddHoliday.Visible = true;
            frmHolidayAdd.Visible = false;
            HolidayDetailPopUp.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }

    protected void frmHolidaySource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setUpdateParameters(e);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmHolidaySource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setInsertParameters(e);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((TextBox)this.frmHolidayAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtHolidayDateAdd")).Text != "")
            e.InputParameters["Date"] = ((TextBox)this.frmHolidayAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtHolidayDateAdd")).Text;

        if (((TextBox)this.frmHolidayAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtHolidayNameAdd")).Text != "")
            e.InputParameters["Holiday_Name"] = ((TextBox)this.frmHolidayAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtHolidayNameAdd")).Text;

        if (((TextBox)this.frmHolidayAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtRemarksAdd")).Text != "")
            e.InputParameters["Remarks"] = ((TextBox)this.frmHolidayAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtRemarksAdd")).Text;
       
        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((TextBox)this.frmHolidayAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtHolidayDateEdit")).Text != "")
            //if (((TextBox)this.frmViewAdd.FindControl("txtLdgrNameAdd")).Text != "")
            e.InputParameters["Date"] = ((TextBox)this.frmHolidayAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtHolidayDateEdit")).Text;

        if (((TextBox)this.frmHolidayAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtHolidayNameEdit")).Text != "")
            e.InputParameters["Holiday_Name"] = ((TextBox)this.frmHolidayAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtHolidayNameEdit")).Text;

        if (((TextBox)this.frmHolidayAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtHolidayRemarksEdit")).Text != "")
            e.InputParameters["Remarks"] = ((TextBox)this.frmHolidayAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtHolidayRemarksEdit")).Text;

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }

    protected void HolidaySummaryGridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (grdViewHolidaySummary.SelectedDataKey != null)
                e.InputParameters["Holiday_ID"] = grdViewHolidaySummary.SelectedDataKey.Value;

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdViewHolidaySummary_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                grdViewHolidaySummary.DataBind();
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to delete the record. Please contact Administrator.');");

                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                    e.ExceptionHandled = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdViewHolidaySummary_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grdViewHolidaySummary.SelectedIndex = e.RowIndex;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchInput.Text = "";
            ddlSearchCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}