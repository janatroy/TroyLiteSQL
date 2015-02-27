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

public partial class TaskTypes : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

        if (!Page.IsPostBack)
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            string connStr = string.Empty;

            if (Request.Cookies["Company"]  != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                lnkBtnAdd.Visible = false;
                GrdViewLedger.Columns[7].Visible = false;
                GrdViewLedger.Columns[8].Visible = false;
            }

            GrdViewLedger.PageSize=8;

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);

            if (bl.CheckUserHaveAdd(usernam, "Ttypes"))
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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        ddCriteria.SelectedIndex = 0;

        //BtnClearFilter.Visible = false;
    }

    private string GetConnectionString()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"]  != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        return connStr;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void frmViewAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void frmViewAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        if (e.Exception == null)
        {
            //MyAccordion.Visible = true;
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            GrdViewLedger.Visible = true;
            System.Threading.Thread.Sleep(1000);
            GrdViewLedger.DataBind();
            StringBuilder scriptMsg = new StringBuilder();
            scriptMsg.Append("alert('Task Type Information Saved Successfully.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), scriptMsg.ToString(), true);
        }
        else
        {
            if (e.Exception != null)
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('Task Type with this name already exists, Please try with a different name.');");

                if (e.Exception.InnerException != null)
                {
                    if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                        (e.Exception.InnerException.Message.IndexOf("Task Type Exists") > -1))
                    {
                        e.ExceptionHandled = true;
                        e.KeepInInsertMode = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                        ModalPopupExtender1.Show();
                        return;
                    }
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

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (GrdViewLedger.SelectedDataKey != null)
            e.InputParameters["Task_Type_Id"] = GrdViewLedger.SelectedDataKey.Value;


        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }

    protected void GrdViewLedger_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception == null)
        {
            GrdViewLedger.DataBind();
        }
        else
        {
            if (e.Exception.InnerException != null)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), e.Exception.Message.ToString(), true);

                e.ExceptionHandled = true;
            }
        }
    }

    protected void GrdViewLedger_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GrdViewLedger.SelectedIndex = e.RowIndex;
    }

    protected void frmViewAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception == null)
        {
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            GrdViewLedger.Visible = true;
            System.Threading.Thread.Sleep(1000);
            //MyAccordion.Visible = true;
            GrdViewLedger.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Task Type Details Updated Successfully.');", true);
        }
        else
        {

            StringBuilder script = new StringBuilder();
            script.Append("alert('Task Type with this name already exists, Please try with a different name.');");

            if (e.Exception.InnerException != null)
            {
                if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                    (e.Exception.InnerException.Message.IndexOf("Task Types Exists") > -1))
                {
                    e.ExceptionHandled = true;
                    e.KeepInEditMode = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                    ModalPopupExtender1.Show();
                    return;
                }

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Exception: " + e.Exception.Message + e.Exception.StackTrace, true);
            e.ExceptionHandled = true;
            e.KeepInEditMode = true;

        }
    }
    protected void frmViewAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }
    protected void frmViewAdd_ItemCreated(object sender, EventArgs e)
    {
        //if (!DealerRequired())
        //{
        //    if (((DropDownList)this.frmViewAdd.FindControl("drpLedgerCat")) != null)
        //    {
        //        ((DropDownList)this.frmViewAdd.FindControl("drpLedgerCat")).Items.Remove(new ListItem("Dealer", "Dealer"));
        //    }
        //}
    }

    private Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
        {
            return root;
        }

        foreach (Control c in root.Controls)
        {
            Control t = FindControlRecursive(c, id);
            if (t != null)
            {
                return t;
            }
        }

        return null;
    }


    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setInsertParameters(e);
    }

    private bool DealerRequired()
    {
        DataSet appSettings;
        string dealerRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "DEALER")
                {
                    dealerRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }

        if (dealerRequired == "YES")
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        //if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
        //    return;
        //}
        ModalPopupExtender1.Show();
        frmViewAdd.ChangeMode(FormViewMode.Insert);
        frmViewAdd.Visible = true;
        if (frmViewAdd.CurrentMode == FormViewMode.Insert)
        {
            //GrdViewLedger.Visible = false;
            //lnkBtnAdd.Visible = false;
            ////MyAccordion.Visible = false;
        }
    }
    protected void GrdViewLedger_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            frmViewAdd.Visible = true;
            frmViewAdd.ChangeMode(FormViewMode.Edit);
            //GrdViewLedger.Visible = false;
            //lnkBtnAdd.Visible = false;
            ////MyAccordion.Visible = false;
            ModalPopupExtender1.Show();
            //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
            //Accordion1.SelectedIndex = 1;
        }
    }

    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        this.setUpdateParameters(e);
    }

    protected void GrdViewLedger_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            PresentationUtils.SetPagerButtonStates(GrdViewLedger, e.Row, this);
        }

    }
    protected void GrdViewLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BusinessLogic bl = new BusinessLogic(GetConnectionString());

            if (bl.CheckIfTaskTypeUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
            {
                ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
            }

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;

            if (bl.CheckUserHaveEdit(usernam, "Ttypes"))
            {
                ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
            }

            if (bl.CheckUserHaveDelete(usernam, "Ttypes"))
            {
                ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
            }    
        }
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        //MyAccordion.Visible = true;
        frmViewAdd.Visible = false;
        lnkBtnAdd.Visible = true;
        GrdViewLedger.Visible = true;
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {

    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        //MyAccordion.Visible = true;
        lnkBtnAdd.Visible = true;
        frmViewAdd.Visible = false;
        GrdViewLedger.Visible = true;

    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((TextBox)this.frmViewAdd.FindControl("txtTaskTypeNameAdd")).Text != "")
            e.InputParameters["Task_Type_Name"] = ((TextBox)this.frmViewAdd.FindControl("txtTaskTypeNameAdd")).Text;

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
        
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((TextBox)this.frmViewAdd.FindControl("txtTaskTypeName")).Text != "")
            e.InputParameters["Task_Type_Name"] = ((TextBox)this.frmViewAdd.FindControl("txtTaskTypeName")).Text;

        e.InputParameters["Task_Type_Id"] = GrdViewLedger.SelectedDataKey.Value;


        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }



}
