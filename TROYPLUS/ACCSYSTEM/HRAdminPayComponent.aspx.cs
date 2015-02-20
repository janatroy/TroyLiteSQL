using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PayComponent_HRAdminPay : System.Web.UI.Page
{

    public string sDataSource = string.Empty;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");

        PayCompSummaryGridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        PayCompSummaryGridSource.SelectParameters.Add(new ControlParameter("txtSearchInput", TypeCode.String, txtSearchInput.UniqueID, "Text"));
        PayCompSummaryGridSource.SelectParameters.Add(new ControlParameter("searchCriteria", TypeCode.String, ddlSearchCriteria.UniqueID, "SelectedValue"));
        PayCompSummaryGridSource.SelectParameters.Add(new CookieParameter("UserId", "LoggedUserName"));

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
                lnkBtnAddPayComp.Visible = false;
                //grdViewAttendanceSummary.Columns[7].Visible = false;
                //grdViewAttendanceSummary.Columns[8].Visible = false;
            }
            grdViewPayCompSummary.PageSize = 8;

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

    protected void lnkBtnAddPayComp_Click(object sender, EventArgs e)
    {
        try
        {
            frmPayCompAdd.ChangeMode(FormViewMode.Insert);
            frmPayCompAdd.Visible = true;
            PayCompDetailPopUp.Visible = true;
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdViewPayCompSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                frmPayCompAdd.Visible = true;
                frmPayCompAdd.ChangeMode(FormViewMode.Edit);
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
    protected void grdViewPayCompSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdViewPayCompSummary_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                grdViewPayCompSummary.DataBind();
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
    protected void grdViewPayCompSummary_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            grdViewPayCompSummary.SelectedIndex = e.RowIndex;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmPayCompAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void frmPayCompAdd_ItemCreated(object sender, EventArgs e)
    {

    }
    protected void frmPayCompAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }
    protected void frmPayCompAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                //MyAccordion.Visible = true;
                lnkBtnAddPayComp.Visible = true;
                frmPayCompAdd.Visible = false;
                grdViewPayCompSummary.Visible = true;
                System.Threading.Thread.Sleep(1000);
                grdViewPayCompSummary.DataBind();
                StringBuilder scriptMsg = new StringBuilder();
                scriptMsg.Append("alert('Pay Component Saved Successfully.');");
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
                    script.Append("alert('Pay Component with this name already exists, Please try with a different name.');");

                    if (e.Exception.InnerException != null)
                    {
                        if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                            (e.Exception.InnerException.Message.IndexOf("Pay Component Name Exists") > -1))
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
    protected void frmPayCompAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                lnkBtnAddPayComp.Visible = true;
                frmPayCompAdd.Visible = false;
                //GrdViewLedger.Visible = true;
                grdViewPayCompSummary.Visible = true;
                System.Threading.Thread.Sleep(1000);
                grdViewPayCompSummary.DataBind();
                //MyAccordion.Visible = true;
                //GrdViewLedger.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Pay Component Details Updated Successfully.');", true);
            }
            else
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('Pay Component with this name already exists, Please try with a different name.');");

                if (e.Exception.InnerException != null)
                {
                    if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                        (e.Exception.InnerException.Message.IndexOf("Pay Component Name Exists") > -1))
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
            lnkBtnAddPayComp.Visible = true;
            frmPayCompAdd.Visible = false;
            PayCompDetailPopUp.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }
    protected void PayCompSummaryGridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (grdViewPayCompSummary.SelectedDataKey != null)
                e.InputParameters["PayComponentID"] = grdViewPayCompSummary.SelectedDataKey.Value;

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmPayCompSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
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

    protected void frmPayCompSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
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
        if (((TextBox)this.frmPayCompAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtPayCompAdd")).Text != "")
            e.InputParameters["PayComponentName"] = ((TextBox)this.frmPayCompAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtPayCompAdd")).Text;

        if (((DropDownList)this.frmPayCompAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ddlPayCompTypeAdd")).Text != "")
            e.InputParameters["PayComponentTypeID"] = ((DropDownList)this.frmPayCompAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ddlPayCompTypeAdd")).SelectedItem.Value;

        if (((TextBox)this.frmPayCompAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtPayCompDescAdd")).Text != "")
            e.InputParameters["PayComponentDescription"] = ((TextBox)this.frmPayCompAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtPayCompDescAdd")).Text;

        if (((CheckBox)this.frmPayCompAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsDeductionAdd")) != null)
            e.InputParameters["IsDeduction"] = ((CheckBox)this.frmPayCompAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsDeductionAdd")).Checked;

        if (((CheckBox)this.frmPayCompAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsActiveAdd")) != null)
            e.InputParameters["IsActive"] = ((CheckBox)this.frmPayCompAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkboxIsActiveAdd")).Checked;

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((TextBox)this.frmPayCompAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtPayCompNameEdit")).Text != "")
            e.InputParameters["PayComponentName"] = ((TextBox)this.frmPayCompAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtPayCompNameEdit")).Text;

        if (((DropDownList)this.frmPayCompAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ddlPayCompType")).Text != "")
            e.InputParameters["PayComponentTypeID"] = ((DropDownList)this.frmPayCompAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ddlPayCompType")).SelectedItem.Value;

        if (((TextBox)this.frmPayCompAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtPayCmpDescEdit")).Text != "")
            e.InputParameters["PayComponentDescription"] = ((TextBox)this.frmPayCompAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtPayCmpDescEdit")).Text;

        if (((CheckBox)this.frmPayCompAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsDeductionEdit")) != null)
            e.InputParameters["IsDeduction"] = ((CheckBox)this.frmPayCompAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsDeductionEdit")).Checked;

        if (((CheckBox)this.frmPayCompAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsActiveEdit")) != null)
            e.InputParameters["IsActive"] = ((CheckBox)this.frmPayCompAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkboxIsActiveEdit")).Checked;

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
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