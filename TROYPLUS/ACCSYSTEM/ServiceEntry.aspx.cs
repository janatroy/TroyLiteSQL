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

public partial class ServiceEntry : System.Web.UI.Page
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
                    GrdViewSerEntry.Columns[8].Visible = false;
                    GrdViewSerEntry.Columns[7].Visible = false;
                }


                GrdViewSerEntry.PageSize = 8;

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "SRVENT"))
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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }

    protected void GrdViewSerEntry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow Row = GrdViewSerEntry.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic(connection);
            string recondate = Row.Cells[4].Text;
            hdServiceEntry.Value = Convert.ToString(GrdViewSerEntry.SelectedDataKey.Value);

            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.ReadOnly);
                return;

            }
            else
            {
                frmViewAdd.Visible = true;
                frmViewAdd.DataBind();
                frmViewAdd.ChangeMode(FormViewMode.Edit);
            }

            ModalPopupExtender2.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewSerEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    frmViewAdd.Visible = true;
        //    frmViewAdd.ChangeMode(FormViewMode.Edit);
        //    GrdViewSerEntry.Columns[8].Visible = false;
        //    lnkBtnAdd.Visible = false;

        //    //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
        //    //    Accordion1.SelectedIndex = 1;
        //}
    }
    protected void GrdViewSerEntry_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewSerEntry, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewSerEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gridView = (GridView)sender;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                int cellIndex = -1;
                foreach (DataControlField field in gridView.Columns)
                {
                    if (field.SortExpression == gridView.SortExpression)
                    {
                        cellIndex = gridView.Columns.IndexOf(field);
                    }
                    else if (field.SortExpression != "")
                    {
                        e.Row.Cells[gridView.Columns.IndexOf(field)].CssClass = "headerstyle";
                    }

                }

                if (cellIndex > -1)
                {
                    //  this is a header row,
                    //  set the sort style
                    e.Row.Cells[cellIndex].CssClass =
                        gridView.SortDirection == SortDirection.Ascending
                        ? "sortascheaderstyle" : "sortdescheaderstyle";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEditNote(connection, usernam, "SRVENT"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveEditNote(connection, usernam, "SRVENT"))
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

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
            //    return;
            //}

            frmViewAdd.ChangeMode(FormViewMode.Insert);
            frmViewAdd.Visible = true;
            ModalPopupExtender2.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewSerEntry.Visible = true;
            //MyAccordion.Visible = true;
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        try
        {
            ModalPopupExtender2.Show();
            DateTime startDate = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtStartDateAdd")).Text);
            DateTime endDate = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtEndDateAdd")).Text);
            int frequency = int.Parse(((DropDownList)this.frmViewAdd.FindControl("drpFrequencyAdd")).SelectedValue);

            TimeSpan ts = endDate.Subtract(startDate);

            DateTimeHelper.DateDifference findMonths = new DateTimeHelper.DateDifference(startDate, endDate);

            DateTime nextDate = startDate.AddMonths(frequency);

            if (DateTime.Compare(startDate, endDate) > 0)
            {
                e.Cancel = true;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('EndDate should not be less than StartDate. Please try again.');", true);
                ((Label)this.frmViewAdd.FindControl("lblError")).Text = "EndDate should not be less than StartDate. Please try again.";
                return;
            }

            if (DateTime.Compare(nextDate, endDate) > 0)
            {
                e.Cancel = true;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Next visit date will exceede the EndDate. Please try again.');", true);
                ((Label)this.frmViewAdd.FindControl("lblError")).Text = "Next visit date will exceede the EndDate. Please try again.";
                return;
            }

            int reminder = findMonths.TotalMonths % frequency;

            if (reminder != 0)
            {
                e.Cancel = true;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid EndDate, Please check the EndDate and try again.');", true);
                ((Label)this.frmViewAdd.FindControl("lblError")).Text = "Invalid EndDate, Please check the EndDate and try again.";
                return;
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
            this.setUpdateParameters(e);

            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bll = new BusinessLogic();
            string recondate = ((TextBox)this.frmViewAdd.FindControl("txtStartDate")).Text;
            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to Update Service with this Date. Please contact Supervisor.');", true);
                ((Label)this.frmViewAdd.FindControl("lblError")).Text = "You are not allowed to Update Service with this Date. Please contact Supervisor.";
                return;
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

            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bll = new BusinessLogic();
            string recondate = ((TextBox)this.frmViewAdd.FindControl("txtStartDateAdd")).Text;

            ViewState.Add("TransDate", recondate);

            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                ((Label)this.frmViewAdd.FindControl("lblError")).Text = "You are not allowed to Insert Service with this Date. Please contact Supervisor.";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to Insert Note with this Date. Please contact Supervisor.');", true);
                return;
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
                frmViewAdd.Visible = false;
                System.Threading.Thread.Sleep(1000);
                GrdViewSerEntry.DataBind();
                //MyAccordion.Visible = true;
                GrdViewSerEntry.Visible = true;
                ModalPopupExtender2.Hide();
            }
            else
            {
                StringBuilder script = new StringBuilder();
                //script.Append("alert('You are not allowed to Update this record. Please contact Supervisor.');");

                script.Append("You are not allowed to Update this record. Please contact Supervisor.");

                if (e.Exception.InnerException != null)
                {
                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") == -1)
                        ((Label)this.frmViewAdd.FindControl("lblError")).Text = script.ToString();
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);
                }

                e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                lnkBtnAdd.Visible = false;
                frmViewAdd.Visible = true;
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
                frmViewAdd.Visible = false;
                System.Threading.Thread.Sleep(1000);
                GrdViewSerEntry.DataBind();
                //MyAccordion.Visible = true;
                GrdViewSerEntry.Visible = true;
                ModalPopupExtender2.Hide();
            }
            else
            {
                e.KeepInEditMode = true;

                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    //script.Append("alert('You are not allowed to Update this record. Please contact Supervisor.');");
                    script.Append("You are not allowed to Update this record. Please contact Supervisor.");

                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") == -1)
                        ((Label)this.frmViewAdd.FindControl("lblError")).Text = script.ToString();
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);

                    e.ExceptionHandled = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {

    }
    protected void frmViewAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        try
        {
            ModalPopupExtender2.Show();
            DateTime startDate = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtStartDate")).Text);
            DateTime endDate = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtEndDate")).Text);
            int frequency = int.Parse(((DropDownList)this.frmViewAdd.FindControl("drpFrequency")).SelectedValue);

            TimeSpan ts = endDate.Subtract(startDate);

            DateTimeHelper.DateDifference findMonths = new DateTimeHelper.DateDifference(startDate, endDate);

            DateTime nextDate = startDate.AddMonths(frequency);

            if (DateTime.Compare(startDate, endDate) > 0)
            {
                e.Cancel = true;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('EndDate should not be less than StartDate and Try again.');", true);
                ((Label)this.frmViewAdd.FindControl("lblError")).Text = "EndDate should not be less than StartDate and Try again.";
                return;
            }

            if (DateTime.Compare(nextDate, endDate) > 0)
            {
                e.Cancel = true;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Next visit date will exceede the EndDate. Please try again.');", true);
                ((Label)this.frmViewAdd.FindControl("lblError")).Text = "Next visit date will exceede the EndDate. Please try again.";
                return;
            }

            int reminder = findMonths.TotalMonths % frequency;

            if (reminder != 0)
            {
                e.Cancel = true;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid EndDate, Please check the EndDate and try again.');", true);
                ((Label)this.frmViewAdd.FindControl("lblError")).Text = "Invalid EndDate, Please check the EndDate and try again.";
                return;
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
            GrdViewSerEntry.Visible = true;
            frmViewAdd.Visible = false;
            lnkBtnAdd.Visible = true;
            //MyAccordion.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("drpCustomer")) != null)
            e.InputParameters["CustomerID"] = ((DropDownList)this.frmViewAdd.FindControl("drpCustomer")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text != "")
            e.InputParameters["RefNumber"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtStartDate")).Text != "")
            e.InputParameters["StartDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtStartDate")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("txtEndDate")).Text != "")
            e.InputParameters["EndDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtEndDate")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("txtDetials")) != null)
            e.InputParameters["Details"] = ((TextBox)this.frmViewAdd.FindControl("txtDetials")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("drpFrequency")) != null)
            e.InputParameters["Frequency"] = ((DropDownList)this.frmViewAdd.FindControl("drpFrequency")).SelectedValue;

        e.InputParameters["ServiceID"] = GrdViewSerEntry.SelectedDataKey.Value;

    }
    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("drpCustomerAdd")) != null)
            e.InputParameters["CustomerID"] = ((DropDownList)this.frmViewAdd.FindControl("drpCustomerAdd")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefNoAdd")).Text != "")
            e.InputParameters["RefNumber"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNoAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtStartDateAdd")).Text != "")
            e.InputParameters["StartDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtStartDateAdd")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("txtEndDateAdd")).Text != "")
            e.InputParameters["EndDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtEndDateAdd")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("txtDetialsAdd")) != null)
            e.InputParameters["Details"] = ((TextBox)this.frmViewAdd.FindControl("txtDetialsAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("drpFrequencyAdd")).Text != "")
            e.InputParameters["Frequency"] = ((DropDownList)this.frmViewAdd.FindControl("drpFrequencyAdd")).SelectedValue;

    }

    protected void ComboBox2_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string debtorID = ((DataRowView)frmV.DataItem)["CustomerID"].ToString();

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

    protected void ddBanks_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string creditorID = ((DataRowView)frmV.DataItem)["CreditorID"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(creditorID);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkPayToAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            if (chk.SelectedItem.Text == "Cheque")
            {
                Panel test = (Panel)frmViewAdd.FindControl("PanelBankAdd");
                test.Visible = true;
            }
            else
            {
                Panel test = (Panel)frmViewAdd.FindControl("PanelBankAdd");
                test.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void chkPayTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            if (chk.SelectedItem.Text == "Cheque")
            {
                Panel test = (Panel)frmViewAdd.FindControl("PanelBank");

                if (test != null)
                    test.Visible = true;
            }
            else
            {
                Panel test = (Panel)frmViewAdd.FindControl("PanelBank");

                if (test != null)
                    test.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkPayTo_DataBound(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)chk.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string paymode = ((DataRowView)frmV.DataItem)["paymode"].ToString();
                chk.ClearSelection();

                ListItem li = chk.Items.FindByValue(paymode);
                if (li != null) li.Selected = true;

            }

            if (chk.SelectedItem != null)
            {
                if (chk.SelectedItem.Text == "Cheque")
                {
                    //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                    HtmlTable table = (HtmlTable)((Panel)frmV.FindControl("PanelBank")).FindControl("tblBank");

                    if (table != null)
                        table.Attributes.Add("class", "AdvancedSearch");
                }
                else
                {
                    if (frmV.FindControl("PanelBank") != null)
                    {
                        HtmlTable table = (HtmlTable)frmV.FindControl("PanelBank").FindControl("tblBank");

                        if (table != null)
                            table.Attributes.Add("class", "hidden");
                    }
                }
            }
            else
            {
                //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                //test.Visible = false;
                HtmlTable table = (HtmlTable)frmV.FindControl("tblBank");
                table.Attributes.Add("class", "hidden");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void rdoCDType_DataBound(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)chk.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string paymode = ((DataRowView)frmV.DataItem)["CDType"].ToString();
                chk.ClearSelection();

                ListItem li = chk.Items.FindByValue(paymode);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmViewAdd_ItemCreated(object sender, EventArgs e)
    {
        try
        {
            if (this.frmViewAdd.FindControl("txtStartDateAdd") != null)
            {
                //((TextBox)this.frmViewAdd.FindControl("txtStartDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                ((TextBox)this.frmViewAdd.FindControl("txtStartDateAdd")).Text = dtaa;

            }

            if (this.frmViewAdd.FindControl("txtEndDateAdd") != null)
            {
                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                ((TextBox)this.frmViewAdd.FindControl("txtEndDateAdd")).Text = dtaa;

                //((TextBox)this.frmViewAdd.FindControl("txtEndDateAdd")).Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Validate();

            if (Page.IsValid)
            {
                GrdViewSerEntry.DataBind();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewSerEntry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GrdViewSerEntry.SelectedIndex = e.RowIndex;
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
            if (GrdViewSerEntry.SelectedDataKey != null)
                e.InputParameters["ServiceID"] = GrdViewSerEntry.SelectedDataKey.Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_DataBound(object sender, EventArgs e)
    {

    }
    protected void frmViewAdd_ModeChanged(object sender, EventArgs e)
    {

    }

    private bool CheckDate(DateTime dateTime)
    {
        BusinessLogic objBus = new BusinessLogic();
        return objBus.IsValidDate(Session["Connection"].ToString(), dateTime);
    }
    protected void GrdViewSerEntry_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                GrdViewSerEntry.DataBind();
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();

                    script.Append("You are not allowed to Update this record. Please contact Supervisor.");

                    if (e.Exception.InnerException != null)
                    {
                        if (e.Exception.InnerException.Message.IndexOf("Invalid Date") == -1)
                            ((Label)this.frmViewAdd.FindControl("lblError")).Text = script.ToString();
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);
                    }


                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Service Entry Saved Successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Service Entry Updated Successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
