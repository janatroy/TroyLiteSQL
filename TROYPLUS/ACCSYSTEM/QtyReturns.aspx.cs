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

public partial class QtyReturns : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                string connStr = string.Empty;
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    GrdViewReturns.Columns[4].Visible = false;
                    GrdViewReturns.Columns[5].Visible = false;
                }


                GrdViewReturns.PageSize = 9;

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "ITMTRK"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New ";
                }
            }
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
            frmViewAdd.ChangeMode(FormViewMode.Insert);
            frmViewAdd.Visible = true;
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        GridSource.SelectParameters.Add(new ControlParameter("LedgerID", TypeCode.String, ddSearcLedger.UniqueID, "SelectedValue"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewReturns.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            frmViewAdd.Visible = false;
            lnkBtnAdd.Visible = true;
            GrdViewReturns.Visible = true;
            //MyAccordion.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {

    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            GrdViewReturns.Visible = true;
            //MyAccordion.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewReturns_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewReturns_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                GrdViewReturns.DataBind();
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    //script.Append("alert('You are not allowed to delete the record. Please contact Supervisor.');");
                    script.Append(e.Exception.Message);
                    script.Append(e.Exception.StackTrace);
                    script.Append(e.Exception.InnerException.Message);
                    script.Append(e.Exception.InnerException.StackTrace);

                    //if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
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

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdViewReturns.SelectedDataKey != null)
                e.InputParameters["ReturnID"] = GrdViewReturns.SelectedDataKey.Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void GrdViewReturns_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GrdViewReturns_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "ITMTRK"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "ITMTRK"))
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
    protected void frmViewAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void frmViewAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                lnkBtnAdd.Visible = true;
                //MyAccordion.Visible = true;
                frmViewAdd.Visible = false;
                GrdViewReturns.Visible = true;
                System.Threading.Thread.Sleep(1000);
                GrdViewReturns.DataBind();
            }
            else
            {
                if (e.Exception != null)
                {
                    StringBuilder script = new StringBuilder();
                    //script.Append("alert('Ledger with this name already exists, Please try with a different name.');");

                    if (e.Exception.InnerException != null)
                    {
                        if (e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Exception: " + e.Exception.Message, true);
                    }
                }

                lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
                GrdViewReturns.Visible = true;
                e.ExceptionHandled = true;
                //MyAccordion.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmViewAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                lnkBtnAdd.Visible = true;
                //MyAccordion.Visible = true;
                frmViewAdd.Visible = false;
                GrdViewReturns.Visible = true;
                System.Threading.Thread.Sleep(1000);
                GrdViewReturns.DataBind();
            }
            else
            {
                if (e.Exception != null)
                {
                    StringBuilder script = new StringBuilder();
                    //script.Append("alert('Product with this name already exists, Please try with a different name.');");

                    if (e.Exception.InnerException != null)
                    {
                        if (e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Exception: " + e.Exception.Message, true);
                    }
                }

                lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
                GrdViewReturns.Visible = true;
                e.ExceptionHandled = true;
                //MyAccordion.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmbLedger_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string debtorID = ((DataRowView)frmV.DataItem)["LedgerID"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(debtorID);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmViewAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }

    protected void frmViewAdd_ItemCreated(object sender, EventArgs e)
    {

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

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((DropDownList)this.frmViewAdd.FindControl("cmbLedgerAdd")) != null)
            e.InputParameters["LedgerID"] = ((DropDownList)this.frmViewAdd.FindControl("cmbLedgerAdd")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtDateEnteredAdd")).Text != "")
            e.InputParameters["DateEntered"] = ((TextBox)this.frmViewAdd.FindControl("txtDateEnteredAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtQtyAdd")).Text != "")
            e.InputParameters["Qty"] = ((TextBox)this.frmViewAdd.FindControl("txtQtyAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtCommentsAdd")).Text != "")
            e.InputParameters["Comments"] = ((TextBox)this.frmViewAdd.FindControl("txtCommentsAdd")).Text;

    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("cmbLedger")) != null)
            e.InputParameters["LedgerID"] = ((DropDownList)this.frmViewAdd.FindControl("cmbLedger")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtDateEntered")).Text != "")
            e.InputParameters["DateEntered"] = ((TextBox)this.frmViewAdd.FindControl("txtDateEntered")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtQty")).Text != "")
            e.InputParameters["Qty"] = ((TextBox)this.frmViewAdd.FindControl("txtQty")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtComments")).Text != "")
            e.InputParameters["Comments"] = ((TextBox)this.frmViewAdd.FindControl("txtComments")).Text;

        e.InputParameters["ReturnID"] = GrdViewReturns.SelectedDataKey.Value;

    }

}
