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

public partial class BankInfo : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    public string EnableOpbalance = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
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
                    lnkBtnAdd.Visible = false;
                    GrdViewLedger.Columns[7].Visible = false;
                    GrdViewLedger.Columns[8].Visible = false;
                }

                GrdViewLedger.PageSize = 9;


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "BANKINFO"))
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


            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //string connection = Request.Cookies["Company"].Value;

            
           
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
                scriptMsg.Append("alert('Bank Details Saved Successfully.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), scriptMsg.ToString(), true);
            }
            else
            {
                if (e.Exception != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('Bank with this name already exists, Please try with a different name.');");

                    if (e.Exception.InnerException != null)
                    {
                        if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                            (e.Exception.InnerException.Message.IndexOf("Ledger Exists") > -1))
                        {
                            e.KeepInInsertMode = true;
                            e.ExceptionHandled = true;
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
                e.InputParameters["LedgerID"] =Convert.ToInt32( GrdViewLedger.SelectedDataKey.Value);

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
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


    protected void frmViewAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            if (e.Exception == null && check==false)
            {
                lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
                GrdViewLedger.Visible = true;
                System.Threading.Thread.Sleep(1000);
                //MyAccordion.Visible = true;
                GrdViewLedger.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Details Updated Successfully.');", true);
            }
            else
            {

                StringBuilder script = new StringBuilder();
                script.Append("alert('Bank with this name already exists, Please try with a different name.');");

                if (e.Exception.InnerException != null)
                {
                    if ((e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1) ||
                        (e.Exception.InnerException.Message.IndexOf("Ledger Exists") > -1))
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

    protected void drpunuse_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string executive = ((DataRowView)frmV.DataItem)["unuse"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(executive);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);

        }
    }

    protected void drpunuseAdd_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string executive = ((DataRowView)frmV.DataItem)["unuse"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(executive);
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
        try
        {
            if (!DealerRequired())
            {
                if (((DropDownList)this.frmViewAdd.FindControl("drpLedgerCat")) != null)
                {
                    ((DropDownList)this.frmViewAdd.FindControl("drpLedgerCat")).Items.Remove(new ListItem("Dealer", "Dealer"));
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);

        }
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

        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connection = Request.Cookies["Company"].Value;

            string refDate = string.Empty;
            refDate = ((TextBox)this.frmViewAdd.FindControl("txtdueDateadd")).Text;
            if (refDate == null || refDate == "")
            {
                string obdate = ((TextBox)this.frmViewAdd.FindControl("txtOpenBalAdd")).Text;
                if (obdate != null && obdate != "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('OB due date is mandatory')", true);
                    check = true;
                    ModalPopupExtender1.Show();
                    frmViewAdd.Visible = true;
                    frmViewAdd.ChangeMode(FormViewMode.Insert);
                    e.Cancel = true;
                    return;

                }         

            }
            else if(refDate != null || refDate != "")
            {
                string obdate = ((TextBox)this.frmViewAdd.FindControl("txtOpenBalAdd")).Text;
                if (obdate == null && obdate == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter Opening balance or Otherwise set as zero.')", true);
                    check = true;
                    ModalPopupExtender1.Show();
                    frmViewAdd.Visible = true;
                    frmViewAdd.ChangeMode(FormViewMode.Insert);
                    e.Cancel = true;
                    return;

                }


            }
            else
            {
                string dt = Convert.ToDateTime(refDate).ToString("MM/dd/yyyy");
                EnableOpbalance = bl.getEnableOpBalanceConfig(connection);

                if (EnableOpbalance == "YES")
                {
                    if (!bl.IsValidDate(connection, Convert.ToDateTime(refDate)))
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Date has been Locked')", true);
                        check = true;
                        ModalPopupExtender1.Show();
                        frmViewAdd.Visible = true;
                        frmViewAdd.ChangeMode(FormViewMode.Insert);
                        e.Cancel = true;
                        return;
                        // break;
                    }

                }
            }
            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //string connection = Request.Cookies["Company"].Value;

            //string refDate = string.Empty;
            //refDate = ((TextBox)this.frmViewAdd.FindControl("txtdueDateadd")).Text;
            //string dt = Convert.ToDateTime(refDate).ToString("MM/dd/yyyy");
            //EnableOpbalance = bl.getEnableOpBalanceConfig(connection);

            //if (EnableOpbalance == "YES")
            //{
            //    if (!bl.IsValidDate(connection, Convert.ToDateTime(refDate)))
            //    {

            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Date has been Locked')", true);
            //        check = true;
            //        ModalPopupExtender1.Show();
            //        frmViewAdd.Visible = true;
            //        frmViewAdd.ChangeMode(FormViewMode.Insert);
            //        e.Cancel = true;
            //        return;
            //        // break;
            //    }

            //}
            //if (check == false)
            //{
                this.setInsertParameters(e);
            //}
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
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "DEALER")
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

    protected void frmViewAdd_ModeChanged(object sender, EventArgs e)
    {
        try
        {
            if (frmViewAdd.CurrentMode == FormViewMode.Insert)
            {
                {

                    BusinessLogic bl = new BusinessLogic(sDataSource);
                    string connection = Request.Cookies["Company"].Value;
                    EnableOpbalance = bl.getEnableOpBalanceConfig(connection);

                    if (EnableOpbalance == "NO")
                    {
                        if (this.frmViewAdd.FindControl("txtOpenBalAdd") != null)
                        {                     
                                ((TextBox)this.frmViewAdd.FindControl("txtOpenBalAdd")).Enabled = false;
                                ((TextBox)this.frmViewAdd.FindControl("txtdueDateadd")).Enabled = false;
                                ((DropDownList)this.frmViewAdd.FindControl("ddCRDRAdd")).Enabled = false;
                               ((ImageButton)this.frmViewAdd.FindControl("btnBillDate1")).Enabled = false;
                        }
                      
                        else
                        {
                            if (this.frmViewAdd.FindControl("txtOpenBalAdd") == null)
                            {
                                ((TextBox)this.frmViewAdd.FindControl("txtOpenBalAdd")).Enabled = false;
                                ((TextBox)this.frmViewAdd.FindControl("txtdueDateadd")).Enabled = false;
                                ((DropDownList)this.frmViewAdd.FindControl("ddCRDRAdd")).Enabled = false;
                                ((ImageButton)this.frmViewAdd.FindControl("btnBillDate1")).Enabled = false;

                            }

                        }
                    }
                }
            }

            if (frmViewAdd.CurrentMode == FormViewMode.Edit)
                {

                    BusinessLogic bl = new BusinessLogic(sDataSource);
                    string connection = Request.Cookies["Company"].Value;

                    string refDate = string.Empty;
                    refDate = ((TextBox)this.frmViewAdd.FindControl("txtdueDate")).Text;
                    string dt = Convert.ToDateTime(refDate).ToString("MM/dd/yyyy");
                    EnableOpbalance = bl.getEnableOpBalanceConfig(connection);
                    if (EnableOpbalance == "NO")
                    {
                        if (this.frmViewAdd.FindControl("txtOpenBal") != null)
                        {
                            //if (this.frmViewAdd.FindControl("txtOpenBalAdd") != null)
                            //{
                                ((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Enabled = false;
                                ((TextBox)this.frmViewAdd.FindControl("txtdueDate")).Enabled = false;
                                ((DropDownList)this.frmViewAdd.FindControl("ddCRDR")).Enabled = false;
                               ((ImageButton)this.frmViewAdd.FindControl("btnBillDate")).Enabled = false;

                            //}
                        }
                      
                        else
                        {
                            if (this.frmViewAdd.FindControl("txtOpenBal") == null)
                            {
                                ((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Enabled = false;
                                ((TextBox)this.frmViewAdd.FindControl("txtdueDate")).Enabled = false;
                                ((DropDownList)this.frmViewAdd.FindControl("ddCRDR")).Enabled = false;
                                ((ImageButton)this.frmViewAdd.FindControl("btnBillDate")).Enabled = false;

                            }

                        }
                    }
              
                }
            }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void frmViewAdd_DataBound(object sender, EventArgs e)
    {
        frmViewAdd_ModeChanged(sender, e);
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
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connection = Request.Cookies["Company"].Value;

            string refDate = string.Empty;
            refDate = ((TextBox)this.frmViewAdd.FindControl("txtdueDate")).Text;
            if (refDate == null || refDate == "")
            {
                string obdate = ((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text;
                if (obdate != null && obdate != "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('OB due date is mandatory')", true);
                    check = true;
                    ModalPopupExtender1.Show();
                    frmViewAdd.Visible = true;
                    frmViewAdd.ChangeMode(FormViewMode.Edit);
                    e.Cancel = true;
                    return;

                }

            }
            else
            {
                string dt = Convert.ToDateTime(refDate).ToString("MM/dd/yyyy");
                EnableOpbalance = bl.getEnableOpBalanceConfig(connection);

                if (EnableOpbalance == "YES")
                {
                    if (!bl.IsValidDate(connection, Convert.ToDateTime(refDate)))
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Date has been Locked')", true);
                        check = true;
                        ModalPopupExtender1.Show();
                        frmViewAdd.Visible = true;
                        frmViewAdd.ChangeMode(FormViewMode.Edit);
                        e.Cancel = true;
                        return;
                        // break;
                    }

                }
            }
             
            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //string connection = Request.Cookies["Company"].Value;

            //string refDate = string.Empty;
            //refDate = ((TextBox)this.frmViewAdd.FindControl("txtdueDate")).Text;
            //string dt = Convert.ToDateTime(refDate).ToString("MM/dd/yyyy");
            //EnableOpbalance = bl.getEnableOpBalanceConfig(connection);

            //if (EnableOpbalance == "YES")
            //{
            //    if (!bl.IsValidDate(connection, Convert.ToDateTime(refDate)))
            //    {

            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Date has been Locked')", true);
            //        check = true;
            //        ModalPopupExtender1.Show();
            //        frmViewAdd.Visible = true;
            //        frmViewAdd.ChangeMode(FormViewMode.Edit);
            //        e.Cancel = true;
            //        return;
            //        // break;
            //    }

            //}
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
                string DRORCR = ((HiddenField)e.Row.FindControl("DRORCR")).Value;
                double Debit = 0; // double.Parse(e.Row.Cells[5].Text);
                double Credit = 0; // double.Parse(e.Row.Cells[4].Text);
                double OpenBalance = double.Parse(((HiddenField)e.Row.FindControl("OpenBalance")).Value);

                if (DRORCR == "CR")
                {
                    Credit = Credit + OpenBalance;
                }
                else if (DRORCR == "DR")
                {
                    Debit = Debit + OpenBalance;
                }

                if (Debit > Credit)
                {
                    ((Label)e.Row.FindControl("lblBalance")).Text = (Debit - Credit).ToString() + " Dr";
                }
                else if (Credit > Debit)
                {
                    ((Label)e.Row.FindControl("lblBalance")).Text = (Credit - Debit).ToString() + " Cr";
                }
                else
                {
                    ((Label)e.Row.FindControl("lblBalance")).Text = "0";
                }

                if ((e.Row.Cells[0].Text == "Purchase A/c") || (e.Row.Cells[0].Text == "Cash A/c") || (e.Row.Cells[0].Text == "Sales A/c") || (e.Row.Cells[0].Text == "General Expenses") || (e.Row.Cells[0].Text == "CreditDebitNoteId"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                BusinessLogic bl = new BusinessLogic(GetConnectionString());

                if (bl.CheckIfLedgerUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "BANKINFO"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "BANKINFO"))
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
    bool check = false;
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

       
    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((DropDownList)this.frmViewAdd.FindControl("ddAccGroupAdd")) != null)
            e.InputParameters["GroupID"] = ((DropDownList)this.frmViewAdd.FindControl("ddAccGroupAdd")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtLdgrNameAdd")).Text != "")
            e.InputParameters["LedgerName"] = ((TextBox)this.frmViewAdd.FindControl("txtLdgrNameAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAliasNameAdd")).Text != "")
            e.InputParameters["AliasName"] = ((TextBox)this.frmViewAdd.FindControl("txtAliasNameAdd")).Text;
        else
            e.InputParameters["AliasName"] = ((TextBox)this.frmViewAdd.FindControl("txtLdgrNameAdd")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("ddCRDRAdd")).SelectedValue == "CR")
        {
            if (((TextBox)this.frmViewAdd.FindControl("txtOpenBalAdd")).Text != "")
            {
                e.InputParameters["OpenBalanceCR"] = ((TextBox)this.frmViewAdd.FindControl("txtOpenBalAdd")).Text;
                e.InputParameters["OpenBalanceDR"] = "0";
            }
        }
        else
        {
            if (((TextBox)this.frmViewAdd.FindControl("txtOpenBalAdd")).Text != "")
            {
                e.InputParameters["OpenBalanceDR"] = ((TextBox)this.frmViewAdd.FindControl("txtOpenBalAdd")).Text;
                e.InputParameters["OpenBalanceCR"] = "0";
            }
        }

        if (((TextBox)this.frmViewAdd.FindControl("txtContNameAdd")).Text != "")
            e.InputParameters["ContactName"] = ((TextBox)this.frmViewAdd.FindControl("txtContNameAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtPhoneAdd")).Text != "")
            e.InputParameters["Phone"] = ((TextBox)this.frmViewAdd.FindControl("txtPhoneAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd1Add")).Text != "")
            e.InputParameters["Add1"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd1Add")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd2Add")).Text != "")
            e.InputParameters["Add2"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd2Add")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd3Add")).Text != "")
            e.InputParameters["Add3"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd3Add")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("drpLedgerCatAdd")) != null)
            e.InputParameters["LedgerCategory"] = ((DropDownList)this.frmViewAdd.FindControl("drpLedgerCatAdd")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("drpInchargeAdd")) != null)
            e.InputParameters["ExecutiveIncharge"] = ((DropDownList)this.frmViewAdd.FindControl("drpInchargeAdd")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtTinAdd")).Text != "")
            e.InputParameters["TinNumber"] = ((TextBox)this.frmViewAdd.FindControl("txtTinAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtMobileAdd")).Text != "")
            e.InputParameters["Mobile"] = ((TextBox)this.frmViewAdd.FindControl("txtMobileAdd")).Text;
        else
            e.InputParameters["Mobile"] = "";

        if (((TextBox)this.frmViewAdd.FindControl("txtdueDateadd")).Text != "")
            e.InputParameters["OpDueDate"] = ((TextBox)this.frmViewAdd.FindControl("txtdueDateadd")).Text;
        else
            e.InputParameters["OpDueDate"] = "";

        if (((DropDownList)this.frmViewAdd.FindControl("drpIntTransAdd")) != null)
            e.InputParameters["Inttrans"] = ((DropDownList)this.frmViewAdd.FindControl("drpIntTransAdd")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("drpPaymentmadeAdd")) != null)
            e.InputParameters["Paymentmade"] = ((DropDownList)this.frmViewAdd.FindControl("drpPaymentmadeAdd")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("drpdcAdd")) != null)
            e.InputParameters["dc"] = ((DropDownList)this.frmViewAdd.FindControl("drpdcAdd")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtChequeNameAdd")).Text != "")
            e.InputParameters["ChequeName"] = ((TextBox)this.frmViewAdd.FindControl("txtChequeNameAdd")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("drpunuseAdd")) != null)
            e.InputParameters["unuse"] = ((DropDownList)this.frmViewAdd.FindControl("drpunuseAdd")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtEmailIdAdd")).Text != "")
            e.InputParameters["EmailId"] = ((TextBox)this.frmViewAdd.FindControl("txtEmailIdAdd")).Text;
        else
            e.InputParameters["EmailId"] = "";

        if (((DropDownList)this.frmViewAdd.FindControl("drpModeofContactAdd")) != null)
            e.InputParameters["ModeOfContact"] = ((DropDownList)this.frmViewAdd.FindControl("drpModeofContactAdd")).SelectedValue;


        e.InputParameters["BranchCode"] = "All";

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("ddAccGroup")) != null)
            e.InputParameters["GroupID"] = ((DropDownList)this.frmViewAdd.FindControl("ddAccGroup")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtLdgrName")).Text != "")
            e.InputParameters["LedgerName"] = ((TextBox)this.frmViewAdd.FindControl("txtLdgrName")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAliasName")).Text != "")
            e.InputParameters["AliasName"] = ((TextBox)this.frmViewAdd.FindControl("txtAliasName")).Text;
        else
            e.InputParameters["AliasName"] = ((TextBox)this.frmViewAdd.FindControl("txtLdgrName")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("ddCRDR")).SelectedValue == "CR")
        {
            if (((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text != "")
            {
                e.InputParameters["OpenBalanceCR"] = ((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text;
                e.InputParameters["OpenBalanceDR"] = "0";
            }
        }
        else
        {
            if (((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text != "")
            {
                e.InputParameters["OpenBalanceDR"] = ((TextBox)this.frmViewAdd.FindControl("txtOpenBal")).Text;
                e.InputParameters["OpenBalanceCR"] = "0";
            }
        }

        if (((TextBox)this.frmViewAdd.FindControl("txtContName")).Text != "")
            e.InputParameters["ContactName"] = ((TextBox)this.frmViewAdd.FindControl("txtContName")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtPhone")).Text != "")
            e.InputParameters["Phone"] = ((TextBox)this.frmViewAdd.FindControl("txtPhone")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd1")).Text != "")
            e.InputParameters["Add1"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd1")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd2")).Text != "")
            e.InputParameters["Add2"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd2")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAdd3")).Text != "")
            e.InputParameters["Add3"] = ((TextBox)this.frmViewAdd.FindControl("txtAdd3")).Text;

        e.InputParameters["LedgerID"] =Convert.ToInt32(GrdViewLedger.SelectedDataKey.Value);

        e.InputParameters["DRORCR"] = "NA";
        e.InputParameters["OpenBalance"] = "0";

        if (((DropDownList)this.frmViewAdd.FindControl("drpLedgerCat")) != null)
            e.InputParameters["LedgerCategory"] = ((DropDownList)this.frmViewAdd.FindControl("drpLedgerCat")).SelectedValue;
        if (((DropDownList)this.frmViewAdd.FindControl("drpIncharge")) != null)
            e.InputParameters["ExecutiveIncharge"] = ((DropDownList)this.frmViewAdd.FindControl("drpIncharge")).SelectedValue;
        if (((TextBox)this.frmViewAdd.FindControl("txtTin")).Text != "")
            e.InputParameters["TinNumber"] = ((TextBox)this.frmViewAdd.FindControl("txtTin")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtMobile")).Text != "")
            e.InputParameters["Mobile"] = ((TextBox)this.frmViewAdd.FindControl("txtMobile")).Text;
        else
            e.InputParameters["Mobile"] = "";

        if (((TextBox)this.frmViewAdd.FindControl("txtdueDate")).Text != "")
            e.InputParameters["OpDueDate"] = ((TextBox)this.frmViewAdd.FindControl("txtdueDate")).Text;
        else
            e.InputParameters["OpDueDate"] = "";

        if (((DropDownList)this.frmViewAdd.FindControl("drpIntTrans")) != null)
            e.InputParameters["Inttrans"] = ((DropDownList)this.frmViewAdd.FindControl("drpIntTrans")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("drpPaymentmade")) != null)
            e.InputParameters["Paymentmade"] = ((DropDownList)this.frmViewAdd.FindControl("drpPaymentmade")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("drpdc")) != null)
            e.InputParameters["dc"] = ((DropDownList)this.frmViewAdd.FindControl("drpdc")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtChequeName")).Text != "")
            e.InputParameters["ChequeName"] = ((TextBox)this.frmViewAdd.FindControl("txtChequeName")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("drpunuse")) != null)
            e.InputParameters["unuse"] = ((DropDownList)this.frmViewAdd.FindControl("drpunuse")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtEmailId")).Text != "")
            e.InputParameters["EmailId"] = ((TextBox)this.frmViewAdd.FindControl("txtEmailId")).Text;
        else
            e.InputParameters["EmailId"] = "";

        if (((DropDownList)this.frmViewAdd.FindControl("drpModeOfContact")) != null)
            e.InputParameters["ModeOfContact"] = ((DropDownList)this.frmViewAdd.FindControl("drpModeOfContact")).SelectedValue;

        e.InputParameters["BranchCode"] = "All";

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

    }

    protected void drpIncharge_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            //FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string executive = ((DataRowView)frmV.DataItem)["ExecutiveIncharge"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(executive);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);

        }
    }

    protected void drpLedgerCat_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            //FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string executive = ((DataRowView)frmV.DataItem)["LedgerCategory"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(executive);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);

        }
    }

    protected void drpModeofContactAdd_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            BusinessLogic bl = new BusinessLogic(GetConnectionString());
            string connection = Request.Cookies["Company"].Value;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string trans = ((DataRowView)frmV.DataItem)["ModeofContact"].ToString();

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

    protected void drpModeofContact_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            //FormView frmV = (FormView)ddl.NamingContainer;
            FormView frmV = (FormView)ddl.NamingContainer;
            if (frmV.DataItem != null)
            {
                string trans = ((DataRowView)frmV.DataItem)["ModeofContact"].ToString();

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

    protected void ddAccGroup_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            ddl.ClearSelection();

            ListItem li = ddl.Items.FindByText("Bank Accounts");
            if (li != null) li.Selected = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);

        }
    }

    protected void drpLedgerCatAdd_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            //FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string executive = ((DataRowView)frmV.DataItem)["LedgerCategory"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(executive);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);

        }
    }

}