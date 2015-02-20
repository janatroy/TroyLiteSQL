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
using System.Text;

public partial class GroupInfo : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
                    lnkBtnAddGroup.Visible = false;
                    //Label1.Visible = false;
                    //txtSearch.Visible = false;
                    //ddCriteria.Visible = false;
                    //btnSearch.Visible = false;
                    grdViewGroup.Columns[2].Visible = false;
                }


                grdViewGroup.PageSize = 8;


                //string connection = Request.Cookies["Company"].Value;
                //string usernam = Request.Cookies["LoggedUserName"].Value;
                //BusinessLogic bl = new BusinessLogic(sDataSource);

                //if (bl.CheckUserHaveAdd(usernam, "CUSTRCT"))
                //{
                //    lnkBtnAdd.Enabled = false;
                //    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                //}
                //else
                //{
                //    lnkBtnAdd.Enabled = true;
                //    lnkBtnAdd.ToolTip = "Click to Add New ";
                //}

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //grdViewGroup.Visible = true;
            frmViewDetails.Visible = false;
            //lnkBtnAddGroup.Visible = true;

            ModalPopupExtender1.Hide();

            //Label1.Visible = true;
            //txtSearch.Visible = true;
            //ddCriteria.Visible = true;
            //btnSearch.Visible = true;
            grdViewGroup.Columns[2].Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //grdViewGroup.Visible = true;
            frmViewDetails.Visible = false;

            ModalPopupExtender1.Hide();

            //lnkBtnAddGroup.Visible = true;
            //Label1.Visible = true;
            //txtSearch.Visible = true;
            //ddCriteria.Visible = true;
            //btnSearch.Visible = true;
            grdViewGroup.Columns[2].Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {

    }
    protected void frmViewDetails_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void frmViewDetails_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                lnkBtnAddGroup.Visible = true;
                //Label1.Visible = true;
                //txtSearch.Visible = true;
                //ddCriteria.Visible = true;
                //btnSearch.Visible = true;
                grdViewGroup.Visible = true;
                frmViewDetails.Visible = false;
                grdViewGroup.Columns[2].Visible = true;
                System.Threading.Thread.Sleep(1000);
                grdViewGroup.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Group Details Saved Successfully.');", true);
            }
            else
            {
                if (e.Exception != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('Group with this name already exists, Please try with a different name.');");

                    if (e.Exception.InnerException != null)
                    {
                        if (e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                    }
                }
                lnkBtnAddGroup.Visible = true;
                //Label1.Visible = true;
                //txtSearch.Visible = true;
                //ddCriteria.Visible = true;
                //btnSearch.Visible = true;
                grdViewGroup.Visible = true;
                frmViewDetails.Visible = false;
                grdViewGroup.Columns[2].Visible = true;
                e.ExceptionHandled = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewDetails_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }
    protected void frmViewDetails_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                lnkBtnAddGroup.Visible = true;
                //Label1.Visible = true;
                //txtSearch.Visible = true;
                //ddCriteria.Visible = true;
                //btnSearch.Visible = true;
                grdViewGroup.Visible = true;
                frmViewDetails.Visible = false;
                grdViewGroup.Columns[2].Visible = true;
                System.Threading.Thread.Sleep(1000);
                grdViewGroup.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Group Details Updated Successfully.');", true);
            }
            else
            {
                if (e.Exception != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('Group with this name already exists, Please try with a different name.');");

                    if (e.Exception.InnerException != null)
                    {
                        if (e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                    }
                }

                lnkBtnAddGroup.Visible = true;
                //Label1.Visible = true;
                //txtSearch.Visible = true;
                //ddCriteria.Visible = true;
                //btnSearch.Visible = true;
                grdViewGroup.Visible = true;
                frmViewDetails.Visible = false;
                grdViewGroup.Columns[2].Visible = true;
                e.ExceptionHandled = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdViewGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                frmViewDetails.Visible = true;
                frmViewDetails.ChangeMode(FormViewMode.Edit);
                frmViewDetails.DataBind();
                grdViewGroup.Columns[2].Visible = false;

                ModalPopupExtender1.Show();

                //lnkBtnAddGroup.Visible = false;
                //Label1.Visible = false;
                //txtSearch.Visible = false;
                //ddCriteria.Visible = false;
                //btnSearch.Visible = false;
                //grdViewGroup.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdGroup_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(grdViewGroup, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setInsertParameters(e);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
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

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        //if (((DropDownList)this.frmViewDetails.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ddHeading")) != null)
        //    e.InputParameters["HeadingID"] = ((DropDownList)this.frmViewDetails.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ddHeading")).SelectedValue;

        //if (((TextBox)this.frmViewDetails.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtGroupName")).Text != "")
        //    e.InputParameters["GroupName"] = ((TextBox)this.frmViewDetails.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtGroupName")).Text;

        if (((DropDownList)this.frmViewDetails.FindControl("ddHeading")) != null)
            e.InputParameters["HeadingID"] = ((DropDownList)this.frmViewDetails.FindControl("ddHeading")).SelectedValue;

        if (((TextBox)this.frmViewDetails.FindControl("txtGroupName")).Text != "")
            e.InputParameters["GroupName"] = ((TextBox)this.frmViewDetails.FindControl("txtGroupName")).Text;


        e.InputParameters["GroupID"] = grdViewGroup.SelectedDataKey.Value;

    }

    private string GetConnectionString()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        return connStr;
    }


    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        //if (((DropDownList)this.frmViewDetails.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ddIHeadingAdd")) != null)
        //    e.InputParameters["HeadingID"] = ((DropDownList)this.frmViewDetails.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ddIHeadingAdd")).SelectedValue;

        //if (((TextBox)this.frmViewDetails.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtIGroupAdd")).Text != "")
        //    e.InputParameters["GroupName"] = ((TextBox)this.frmViewDetails.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtIGroupAdd")).Text;

        if (((DropDownList)this.frmViewDetails.FindControl("ddIHeadingAdd")) != null)
            e.InputParameters["HeadingID"] = ((DropDownList)this.frmViewDetails.FindControl("ddIHeadingAdd")).SelectedValue;

        if (((TextBox)this.frmViewDetails.FindControl("txtIGroupAdd")).Text != "")
            e.InputParameters["GroupName"] = ((TextBox)this.frmViewDetails.FindControl("txtIGroupAdd")).Text;

        BusinessLogic objBus = new BusinessLogic();
        int nextSeq = (int)objBus.GetNextSequence(GetConnectionString(), "Select Max(GroupID) from tblGroups");

        e.InputParameters["GroupID"] = nextSeq + 1;
        e.InputParameters["Order"] = "0";

    }

    protected void lnkBtnAddGroup_Click(object sender, EventArgs e)
    {
        try
        {
            frmViewDetails.ChangeMode(FormViewMode.Insert);
            frmViewDetails.Visible = true;
            if (frmViewDetails.CurrentMode == FormViewMode.Insert)
            {
                grdViewGroup.Columns[2].Visible = false;
                //lnkBtnAddGroup.Visible = false;
            
                //Label1.Visible = false;
                //txtSearch.Visible = false;
                //ddCriteria.Visible = false;
                //btnSearch.Visible = false;
                //grdViewGroup.Visible = false;

                ModalPopupExtender1.Show();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdViewGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.Cells[0].Text == "Bank Accounts") || (e.Row.Cells[0].Text == "Sales A/c") ||
                    (e.Row.Cells[0].Text == "Cash in Hand") || (e.Row.Cells[0].Text == "Sundry Creditors") ||
                    (e.Row.Cells[0].Text == "Purchase A/c") || (e.Row.Cells[0].Text == "Sundry Debtors") || (e.Row.Cells[0].Text == "Cash in Hand") ||
                    (e.Row.Cells[0].Text == "General Expenses") || (e.Row.Cells[0].Text == "Current Assests")
                    )
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "GRPMST"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                //if (bl.CheckUserHaveDelete(usernam, "GRPMST"))
                //{
                //    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                //}

                //if (bl.CheckUserHaveView(usernam, "GRPMST"))
                //{
                //    ((Image)e.Row.FindControl("lnkprint")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = true;
                //}
                //else
                //{
                //    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = false;
                //}

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void ddCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //srcGridView.SelectParameters.Add(new CookieParameter("connection", "Company"));
        grdSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        grdSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }

}
