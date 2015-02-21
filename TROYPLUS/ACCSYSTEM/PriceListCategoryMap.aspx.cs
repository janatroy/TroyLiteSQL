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

public partial class PriceListCategoryMap : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
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
                    lnkBtnAdd.Visible = false;
                    GrdViewLedger.Columns[7].Visible = false;
                    GrdViewLedger.Columns[8].Visible = false;
                }

                GrdViewLedger.PageSize = 8;

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                //if (bl.CheckUserHaveAdd(usernam, "CATMST"))
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
        try
        {
            txtSearch.Text = "";
            ddCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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
        try
        {
            if (e.Exception == null && check == false)
            {
                //MyAccordion.Visible = true;
                lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
                GrdViewLedger.Visible = true;
                System.Threading.Thread.Sleep(1000);
                GrdViewLedger.DataBind();
                StringBuilder scriptMsg = new StringBuilder();
                scriptMsg.Append("alert('Mapping Information Saved Successfully.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), scriptMsg.ToString(), true);
            }
            else
            {
                if (e.Exception != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('Mapping with this customer category already exists, Please try with a different name.');");

                    ModalPopupExtender1.Show();

                    if (e.Exception.InnerException != null)
                    {
                        if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                            (e.Exception.InnerException.Message.IndexOf("Mapping with this customer category already exists") > -1))
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

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdViewLedger.SelectedDataKey != null)
                e.InputParameters["ID"] = GrdViewLedger.SelectedDataKey.Value;

            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connection = Request.Cookies["Company"].Value;

            string CusCategory_Name = string.Empty;

            DataSet ds = bl.GetMappingInfoForId(connection, Convert.ToInt32(GrdViewLedger.SelectedDataKey.Value));
            if(ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    CusCategory_Name = Convert.ToString(ds.Tables[0].Rows[0]["CusCategory_Name"]);
                }
            }

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

            e.InputParameters["CusCategory_Name"] = CusCategory_Name;

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLedger_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLedger_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GrdViewLedger.SelectedIndex = e.RowIndex;
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
            if (e.Exception == null && check == false)
            {
                lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
                GrdViewLedger.Visible = true;
                System.Threading.Thread.Sleep(1000);
                //MyAccordion.Visible = true;
                GrdViewLedger.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Mapping Details Updated Successfully.');", true);
            }
            else
            {

                StringBuilder script = new StringBuilder();
                script.Append("alert('Mapping with this customer category already exists, Please try with a different name.');");

                ModalPopupExtender1.Show();

                if (e.Exception.InnerException != null)
                {
                    if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                        (e.Exception.InnerException.Message.IndexOf("Mapping with this customer category already exists") > -1))
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

    bool check = false;
    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            string category = ((TextBox)this.frmViewAdd.FindControl("drpCustomerCategoryAdd")).Text;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connection = Request.Cookies["Company"].Value;

            if (bl.IsCategoryFoundinMapping(connection, category))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Custmer Category already found')", true);
                ModalPopupExtender1.Show();
                frmViewAdd.Visible = true;
                check = true;
                frmViewAdd.ChangeMode(FormViewMode.Insert);
                e.Cancel = true;
                return;
            }

            this.setInsertParameters(e);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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
        try
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
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewLedger_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            string category = ((TextBox)this.frmViewAdd.FindControl("drpCustomerCategory")).Text;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connection = Request.Cookies["Company"].Value;

            string text = ((TextBox)this.frmViewAdd.FindControl("TextBox2")).Text;

            if (category == text)
            {

            }
            else
            {
                if (bl.IsCategoryFoundinMapping(connection, category))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Custmer Category already found')", true);
                    ModalPopupExtender1.Show();
                    frmViewAdd.Visible = true;
                    check = true;
                    frmViewAdd.ChangeMode(FormViewMode.Insert);
                    e.Cancel = true;
                    return;
                }
            }

            this.setUpdateParameters(e);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLedger_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewLedger, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(GetConnectionString());

                if (bl.CheckIfCustomerCategoryUsed(Convert.ToString(((HiddenField)e.Row.FindControl("HiddenField2")).Value)))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                //if (bl.CheckUserHaveEdit(usernam, "CATMST"))
                //{
                //    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                //}

                //if (bl.CheckUserHaveDelete(usernam, "CATMST"))
                //{
                //    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                //}
            }
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
            //MyAccordion.Visible = true;
            frmViewAdd.Visible = false;
            lnkBtnAdd.Visible = true;
            GrdViewLedger.Visible = true;
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
            //MyAccordion.Visible = true;
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            GrdViewLedger.Visible = true;
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

    protected void drpCustomerCategoryAdd_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            BusinessLogic bl = new BusinessLogic(GetConnectionString());
            string connection = Request.Cookies["Company"].Value;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string trans = ((DataRowView)frmV.DataItem)["CustomerCategory_Value"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(trans);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpCustomerCategory_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            BusinessLogic bl = new BusinessLogic(GetConnectionString());
            string connection = Request.Cookies["Company"].Value;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string trans = ((DataRowView)frmV.DataItem)["CustomerCategory_Value"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(trans);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpPriceListAdd_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            BusinessLogic bl = new BusinessLogic(GetConnectionString());
            string connection = Request.Cookies["Company"].Value;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string trans = ((DataRowView)frmV.DataItem)["Id"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(trans);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpPriceList_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            BusinessLogic bl = new BusinessLogic(GetConnectionString());
            string connection = Request.Cookies["Company"].Value;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string trans = ((DataRowView)frmV.DataItem)["Id"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(trans);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((DropDownList)this.frmViewAdd.FindControl("drpPriceListAdd")) != null)
            e.InputParameters["PriceList_Id"] = ((DropDownList)this.frmViewAdd.FindControl("drpPriceListAdd")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("drpPriceListAdd")) != null)
            e.InputParameters["PriceList_Name"] = ((DropDownList)this.frmViewAdd.FindControl("drpPriceListAdd")).SelectedItem.Text;

        if (((TextBox)this.frmViewAdd.FindControl("drpCustomerCategoryAdd")) != null)
            e.InputParameters["CustomerCategory_Value"] = ((TextBox)this.frmViewAdd.FindControl("drpCustomerCategoryAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("drpCustomerCategoryAdd")) != null)
            e.InputParameters["CustomerCategory_Name"] = ((TextBox)this.frmViewAdd.FindControl("drpCustomerCategoryAdd")).Text;

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
        
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((DropDownList)this.frmViewAdd.FindControl("drpPriceList")) != null)
            e.InputParameters["PriceList_Id"] = ((DropDownList)this.frmViewAdd.FindControl("drpPriceList")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("drpPriceList")) != null)
            e.InputParameters["PriceList_Name"] = ((DropDownList)this.frmViewAdd.FindControl("drpPriceList")).SelectedItem.Text;

        if (((TextBox)this.frmViewAdd.FindControl("drpCustomerCategory")) != null)
            e.InputParameters["CustomerCategory_Value"] = ((TextBox)this.frmViewAdd.FindControl("drpCustomerCategory")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("drpCustomerCategory")) != null)
            e.InputParameters["CustomerCategory_Name"] = ((TextBox)this.frmViewAdd.FindControl("drpCustomerCategory")).Text;

        e.InputParameters["ID"] = GrdViewLedger.SelectedDataKey.Value;

        e.InputParameters["CusCategory_ID"] = ((TextBox)this.frmViewAdd.FindControl("TextBox1")).Text;

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }



}
