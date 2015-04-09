using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Leave_AdminLeaveType : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");

        LeaveSummaryGridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        LeaveSummaryGridSource.SelectParameters.Add(new ControlParameter("txtSearchInput", TypeCode.String, txtSearchInput.UniqueID, "Text"));
        LeaveSummaryGridSource.SelectParameters.Add(new ControlParameter("searchCriteria", TypeCode.String, ddlSearchCriteria.UniqueID, "SelectedValue"));
        LeaveSummaryGridSource.SelectParameters.Add(new CookieParameter("UserId", "LoggedUserName"));

        // AttendanceDetails source input parameters
        //LeaveSummaryGridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        //LeaveSummaryGridSource.SelectParameters.Add(new Parameter("year", TypeCode.Int32, DateTime.Now.Year.ToString()));
        //LeaveSummaryGridSource.SelectParameters.Add(new Parameter("month", TypeCode.Int32, DateTime.Now.Month.ToString()));
        //LeaveSummaryGridSource.SelectParameters.Add(new CookieParameter("userId", "LoggedUserName"));

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
                lnkBtnAddLeave.Visible = false;
                //grdViewAttendanceSummary.Columns[7].Visible = false;
                //grdViewAttendanceSummary.Columns[8].Visible = false;
            }
            grdViewLeaveSummary.PageSize = 8;

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);

            if (bl.CheckUserHaveAdd(usernam, "LVTP"))
            {
                lnkBtnAddLeave.Enabled = false;
                lnkBtnAddLeave.ToolTip = "You are not allowed to make Add New ";
            }
            else
            {
                lnkBtnAddLeave.Enabled = true;
                lnkBtnAddLeave.ToolTip = "Click to Add New ";
            }



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


    protected void lnkBtnAddLeave_Click(object sender, EventArgs e)
    {
        try
        {
            //LeaveDetailPopUp.Visible = true;
            //ModalPopupExtender1.Show();

            frmLeaveAdd.ChangeMode(FormViewMode.Insert);
            frmLeaveAdd.Visible = true;
            LeaveDetailPopUp.Visible = true;
            ModalPopupExtender1.Show();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void frmLeaveSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
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

    protected void frmLeaveSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
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

    protected void frmLeaveAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }

    protected void frmLeaveAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                //MyAccordion.Visible = true;
                lnkBtnAddLeave.Visible = true;
                frmLeaveAdd.Visible = false;
                grdViewLeaveSummary.Visible = true;
                System.Threading.Thread.Sleep(1000);
                grdViewLeaveSummary.DataBind();
                StringBuilder scriptMsg = new StringBuilder();
                scriptMsg.Append("alert('Leave Type Saved Successfully.');");
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
                    script.Append("alert('Leave Type with this name already exists, Please try with a different name.');");

                    if (e.Exception.InnerException != null)
                    {
                        if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                            (e.Exception.InnerException.Message.IndexOf("Leave Type Name Exists") > -1))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + e.Exception.InnerException.Message.ToString() + "');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Expection :" + e.Exception.InnerException + "');", true);
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

    protected void frmLeaveAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                lnkBtnAddLeave.Visible = true;
                frmLeaveAdd.Visible = false;
                //GrdViewLedger.Visible = true;
                grdViewLeaveSummary.Visible = true;
                System.Threading.Thread.Sleep(1000);
                grdViewLeaveSummary.DataBind();
                //MyAccordion.Visible = true;
                //GrdViewLedger.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Leave Details Updated Successfully.');", true);
            }
            else
            {

                StringBuilder script = new StringBuilder();
                script.Append("alert('Leave with this name already exists, Please try with a different name.');");

                if (e.Exception.InnerException != null)
                {
                    if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                        (e.Exception.InnerException.Message.IndexOf("Leave Exists") > -1))
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

    protected void frmLeaveAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }

    protected void frmLeaveAdd_ItemCreated(object sender, EventArgs e)
    {
        try
        {
            //if (!DealerRequired())
            //{
            //    if (((DropDownList)this.frmLeaveAdd.FindControl("drpLedgerCat")) != null)
            //    {
            //        ((DropDownList)this.frmViewAdd.FindControl("drpLedgerCat")).Items.Remove(new ListItem("Dealer", "Dealer"));
            //    }
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmLeaveSource_Updating1(object sender, ObjectDataSourceMethodEventArgs e)
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

    protected void frmLeaveSource_Inserting1(object sender, ObjectDataSourceMethodEventArgs e)
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

    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //MyAccordion.Visible = true;
            lnkBtnAddLeave.Visible = true;
            frmLeaveAdd.Visible = false;
            LeaveDetailPopUp.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((TextBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtLeaveTypeNameAdd")).Text != "")
            //if (((TextBox)this.frmViewAdd.FindControl("txtLdgrNameAdd")).Text != "")
            e.InputParameters["LeaveTypeName"] = ((TextBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtLeaveTypeNameAdd")).Text;

        if (((TextBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtLeaveTypeDescription")).Text != "")
            e.InputParameters["LeaveDescription"] = ((TextBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtLeaveTypeDescription")).Text;
        else
            e.InputParameters["LeaveDescription"] = ((TextBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtLeaveTypeDescription")).Text;



        if (((CheckBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsPayable")) != null)
            e.InputParameters["IsPayable"] = ((CheckBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsPayable")).Checked;

        if (((CheckBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsActive")) != null)
            e.InputParameters["IsActive"] = ((CheckBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsActive")).Checked;

        if (((CheckBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsEncashable")) != null)
            e.InputParameters["IsEncashable"] = ((CheckBox)this.frmLeaveAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsEncashable")).Checked;


        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((TextBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("LeaveNameEdit")).Text != "")
            //if (((TextBox)this.frmViewAdd.FindControl("txtLdgrNameAdd")).Text != "")
            e.InputParameters["LeaveTypeName"] = ((TextBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("LeaveNameEdit")).Text;

        if (((TextBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtLeaveTypeDescriptionEdit")).Text != "")
            e.InputParameters["LeaveDescription"] = ((TextBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtLeaveTypeDescriptionEdit")).Text;
        else
            e.InputParameters["LeaveDescription"] = ((TextBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtLeaveTypeDescriptionEdit")).Text;



        if (((CheckBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsPayableEdit")) != null)
            e.InputParameters["IsPayable"] = ((CheckBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsPayableEdit")).Checked;

        if (((CheckBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsActiveEdit")) != null)
            e.InputParameters["IsActive"] = ((CheckBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsActiveEdit")).Checked;

        if (((CheckBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsEncashableEdit")) != null)
            e.InputParameters["IsEncashable"] = ((CheckBox)this.frmLeaveAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsEncashableEdit")).Checked;


        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }

    protected void grdViewLeaveSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                frmLeaveAdd.Visible = true;
                frmLeaveAdd.ChangeMode(FormViewMode.Edit);
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

    protected void grdViewLeaveSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            bool status = (bool)row["IsDefault"];

            if (status == false)
            {

                Image btn_Edit_Diasable = (Image)e.Row.FindControl("btnEditDisabled");
                btn_Edit_Diasable.Enabled = true ;
            }
            else
            {
                Image btn_Edit_Enable = (Image)e.Row.FindControl("btnEdit");
                btn_Edit_Enable.Enabled = true;
            }
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;

            if (bl.CheckUserHaveEdit(usernam, "LVTP"))
            {
                ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
            }
        }
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {

    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {

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