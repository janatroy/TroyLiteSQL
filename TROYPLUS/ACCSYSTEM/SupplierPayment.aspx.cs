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

public partial class SupplierPayment : System.Web.UI.Page
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
                    GrdViewPayment.Columns[8].Visible = false;
                    GrdViewPayment.Columns[7].Visible = false;
                }

                GrdViewPayment.PageSize = 8;

                GrdViewPayment.Attributes.Add("bordercolor", "Black");

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

    protected void GrdViewPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow Row = GrdViewPayment.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();
            string recondate = Row.Cells[2].Text;
            hdPayment.Value = Convert.ToString(GrdViewPayment.SelectedDataKey.Value);
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
                //GrdViewPayment.Visible = false;
                ////MyAccordion.Visible = false;
                ModalPopupExtender1.Show();
                frmViewAdd.DataBind();
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                lnkBtnAdd.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    frmViewAdd.Visible = true;
        //    frmViewAdd.ChangeMode(FormViewMode.Edit);
        //    GrdViewPayment.Columns[8].Visible = false;
        //    lnkBtnAdd.Visible = false;

        //    //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
        //    //    Accordion1.SelectedIndex = 1;
        //}
    }
    protected void GrdViewPayment_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewPayment, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewPayment_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
                return;
            }

            frmViewAdd.ChangeMode(FormViewMode.Insert);
            frmViewAdd.Visible = true;
            //GrdViewPayment.Visible = false;
            ////MyAccordion.Visible = false;
            ModalPopupExtender1.Show();
            if (frmViewAdd.CurrentMode == FormViewMode.Insert)
            {
                //lnkBtnAdd.Visible = false;
            }
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
            GrdViewPayment.Visible = true;
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

    }

    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setUpdateParameters(e);

            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bll = new BusinessLogic();
            string recondate = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text;
            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to Update Payment with this Date. Please contact Supervisor.');", true);
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
            string recondate = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text;

            ViewState.Add("TransDate", recondate);

            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to Insert Payment with this Date. Please contact Supervisor.');", true);
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
                GrdViewPayment.DataBind();
                //MyAccordion.Visible = true;
                GrdViewPayment.Visible = true;
            }
            else
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('You are not allowed to Update this record. Please contact Supervisor.');");

                if (e.Exception.InnerException != null)
                {
                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") == -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);
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
                GrdViewPayment.DataBind();
                //MyAccordion.Visible = true;
                GrdViewPayment.Visible = true;
            }
            else
            {
                e.KeepInEditMode = true;

                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to Update this record. Please contact Supervisor.');");

                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") == -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);

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
        try
        {
            if (((RadioButtonList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkPayTo")).SelectedValue == "Cheque")
            {
                ((CompareValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cvBank")).Enabled = true;
                ((RequiredFieldValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("rvChequeNo")).Enabled = true;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("tblBank");
                table.Attributes.Add("class", "AdvancedSearch");

            }
            else
            {
                ((CompareValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("cvBank")).Enabled = false;
                ((RequiredFieldValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("rvChequeNo")).Enabled = false;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("tblBank");
                table.Attributes.Add("class", "hidden");

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

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewPayment.Visible = true;
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
        if (((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")) != null)
            e.InputParameters["DebitorID"] = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtRefNo")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text);

        if (((RadioButtonList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkPayTo")).SelectedValue == "Cash")
        {
            e.InputParameters["CreditorID"] = "1";
            e.InputParameters["Paymode"] = "Cash";
            e.InputParameters["ChequeNo"] = "";
        }
        else if (((RadioButtonList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("chkPayTo")).SelectedValue == "Cheque")
        {
            Panel bnkPanel = (Panel)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank");

            if (bnkPanel != null)
            {
                e.InputParameters["CreditorID"] = ((DropDownList)bnkPanel.FindControl("ddBanks")).SelectedValue;
                e.InputParameters["Paymode"] = "Cheque";
            }

            if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtChequeNo")).Text != "")
                e.InputParameters["ChequeNo"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtChequeNo")).Text;

        }

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtNarration")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtNarration")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditAddTab").FindControl("txtBill")).Text != "")
            e.InputParameters["Billno"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditAddTab").FindControl("txtBill")).Text;

        e.InputParameters["VoucherType"] = "Payment";

        e.InputParameters["TransNo"] = GrdViewPayment.SelectedDataKey.Value;

    }
    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")) != null)
            e.InputParameters["DebitorID"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtRefNoAdd")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtRefNoAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text);

        if (((RadioButtonList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkPayToAdd")).SelectedValue == "Cash")
        {
            e.InputParameters["CreditorID"] = "1";
            e.InputParameters["PaymentMode"] = "Cash";
        }
        else if (((RadioButtonList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkPayToAdd")).SelectedValue == "Cheque")
        {
            Panel bnkPanel = (Panel)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd");

            if (bnkPanel != null)
            {
                e.InputParameters["CreditorID"] = ((DropDownList)bnkPanel.FindControl("ddBanksAdd")).SelectedValue;
                e.InputParameters["PaymentMode"] = "Cheque";
            }
        }

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtAmountAdd")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtAmountAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtNarrationAdd")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtNarrationAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtChequeNoAdd")).Text != "")
            e.InputParameters["ChequeNo"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtChequeNoAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsAddTab").FindControl("txtBillAdd")).Text != "")
            e.InputParameters["Billno"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsAddTab").FindControl("txtBillAdd")).Text;
        else
            e.InputParameters["Billno"] = string.Empty;


        e.InputParameters["VoucherType"] = "Payment";

    }

    protected void ComboBox2_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string debtorID = ((DataRowView)frmV.DataItem)["DebtorID"].ToString();

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

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

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
                Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd");
                test.Visible = true;
            }
            else
            {
                Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd");
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
                Panel test = (Panel)frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank");

                if (test != null)
                    test.Visible = true;
            }
            else
            {
                Panel test = (Panel)frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank");

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
                    HtmlTable table = (HtmlTable)((Panel)frmV.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank")).FindControl("tblBank");

                    if (table != null)
                        table.Attributes.Add("class", "AdvancedSearch");
                }
                else
                {
                    if (frmV.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank") != null)
                    {
                        HtmlTable table = (HtmlTable)frmV.FindControl("tabEdit").FindControl("tabEditMain").FindControl("PanelBank").FindControl("tblBank");

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

    protected void chkPayToAdd_DataBound(object sender, EventArgs e)
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
                    HtmlTable table = (HtmlTable)((Panel)frmV.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd")).FindControl("tblBankAdd");

                    if (table != null)
                        table.Attributes.Add("class", "AdvancedSearch");
                }
                else
                {
                    HtmlTable table = (HtmlTable)frmV.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd").FindControl("tblBankAdd");

                    if (table != null)
                        table.Attributes.Add("class", "hidden");
                }
            }
            else
            {
                //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                //test.Visible = false;
                HtmlTable table = (HtmlTable)frmV.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd").FindControl("tblBankAdd");

                if (table != null)
                    table.Attributes.Add("class", "hidden");
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
            if (this.frmViewAdd.FindControl("tablInsert") != null)
            {
                if (this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd") != null)
                {

                    if (ViewState["TransDate"] == null)
                        ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
                    else
                        ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text = ViewState["TransDate"].ToString();

                }

                if (this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain") != null)
                {
                    if (this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("myRangeValidatorAdd") != null)
                    {
                        ((RangeValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("myRangeValidatorAdd")).MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                        ((RangeValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("myRangeValidatorAdd")).MaximumValue = System.DateTime.Now.ToShortDateString();
                    }
                }

            }

            if (this.frmViewAdd.FindControl("tabEdit") != null)
            {
                if (this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("myRangeValidator") != null)
                {
                    ((RangeValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("myRangeValidator")).MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                    ((RangeValidator)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("myRangeValidator")).MaximumValue = System.DateTime.Now.ToShortDateString();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (((RadioButtonList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("chkPayToAdd")).SelectedValue == "Cheque")
            {
                ((CompareValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cvBankAdd")).Enabled = true;
                ((RequiredFieldValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("rvChequeNoAdd")).Enabled = true;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("tblBankAdd");

                if (table != null)
                    table.Attributes.Add("class", "AdvancedSearch");

            }
            else
            {
                ((CompareValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("cvBankAdd")).Enabled = false;
                ((RequiredFieldValidator)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("rvChequeNoAdd")).Enabled = false;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("tblBankAdd");

                if (table != null)
                    table.Attributes.Add("class", "hidden");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            rvSearch.Enabled = true;
            Page.Validate();

            if (Page.IsValid)
            {
                GrdViewPayment.DataBind();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GrdViewPayment.SelectedIndex = e.RowIndex;
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
            if (GrdViewPayment.SelectedDataKey != null)
                e.InputParameters["TransNo"] = GrdViewPayment.SelectedDataKey.Value;

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
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
    protected void GrdViewPayment_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                GrdViewPayment.DataBind();
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to delete the record. Please contact Supervisor.');");

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
    protected void frmSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            if (e.OutputParameters["NewTransNo"] != null)
            {
                if (e.OutputParameters["NewTransNo"].ToString() != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Payment Saved Successfully. Transaction No : " + e.OutputParameters["NewTransNo"].ToString() + "');", true);
                }
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
            if (e.OutputParameters["NewTransNo"] != null)
            {
                if (e.OutputParameters["NewTransNo"].ToString() != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Payment Updated Successfully. New Transaction No : " + e.OutputParameters["NewTransNo"].ToString() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkboxAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked == true)
            {
                Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("Panelcopy");
                test.Visible = true;
                ModalPopupExtender1.Show();
            }
            else
            {
                Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("Panelcopy");
                test.Visible = false;
            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void combooldrefno_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sCustomer = string.Empty;
        string strPaymode = string.Empty;
        string strPaymodef = string.Empty;

        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DropDownList testcheck = (DropDownList)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("combooldrefno");
            int iRefID = Convert.ToInt32(testcheck.SelectedItem.Value);

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            DataSet customerDs = bl.GetPaymentForRef(connection, iRefID);
            string address = string.Empty;

            if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            {

                if (customerDs.Tables[0].Rows[0]["RefNo"] != null)
                    txtRefNoAdd.Text = customerDs.Tables[0].Rows[0]["RefNo"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["TransDate"] != null)
                {
                    string aa = customerDs.Tables[0].Rows[0]["TransDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    txtTransDateAdd.Text = dtaa + Environment.NewLine;
                }

                if (customerDs.Tables[0].Rows[0]["DebtorID"] != null)
                {
                    //ComboBox2Add.SelectedItem.Value = customerDs.Tables[0].Rows[0]["DebtorID"].ToString() + Environment.NewLine;
                    //ComboBox2Add.Text = customerDs.Tables[0].Rows[0]["Debi"].ToString() + Environment.NewLine;

                    sCustomer = Convert.ToString(customerDs.Tables[0].Rows[0]["DebtorID"]);
                    ComboBox2Add.ClearSelection();
                    ListItem li = ComboBox2Add.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                    if (li != null) li.Selected = true;
                }

                if (customerDs.Tables[0].Rows[0]["Amount"] != null)
                    txtAmountAdd.Text = customerDs.Tables[0].Rows[0]["Amount"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["Paymode"] != null)
                {
                    //if (customerDs.Tables[0].Rows[0]["Paymode"].ToString() == "Cash")
                    //    chkPayToAdd.SelectedItem.Value = "Cash" + Environment.NewLine;
                    //else
                    //    chkPayToAdd.SelectedItem.Value = "Cheque" + Environment.NewLine;

                    strPaymodef = customerDs.Tables[0].Rows[0]["Paymode"].ToString();
                    chkPayToAdd.ClearSelection();
                    ListItem piLi = chkPayToAdd.Items.FindByValue(strPaymodef.Trim());
                    if (piLi != null) piLi.Selected = true;




                }

                if (customerDs.Tables[0].Rows[0]["Narration"] != null)
                    txtNarrationAdd.Text = customerDs.Tables[0].Rows[0]["Narration"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["CreditorId"] != null)
                {
                    //ddBanksAdd.SelectedItem.Value = customerDs.Tables[0].Rows[0]["CreditorId"] + Environment.NewLine;

                    strPaymode = Convert.ToString(customerDs.Tables[0].Rows[0]["CreditorId"]);
                    ddBanksAdd.ClearSelection();
                    ListItem pLi = ddBanksAdd.Items.FindByValue(strPaymode.Trim());
                    if (pLi != null) pLi.Selected = true;
                }

                if (customerDs.Tables[0].Rows[0]["ChequeNo"] != null)
                    txtChequeNoAdd.Text = customerDs.Tables[0].Rows[0]["ChequeNo"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["BillNo"] != null)
                    txtBillAdd.Text = customerDs.Tables[0].Rows[0]["BillNo"].ToString() + Environment.NewLine;


                ModalPopupExtender1.Show();
                //if (strPaymodef == "Cheque")
                //{
                //    //RadioButtonList chk = (RadioButtonList)sender;
                //    if (strPaymodef == "Cheque")
                //    {
                //        //Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd");
                //        PanelBankAdd.Visible = true;
                //    }
                //}

                //chkPayToAdd.SelectedIndexChanged += new EventHandler(chkPayToAdd_SelectedIndexChanged);

                //chkPayToAdd_SelectedIndexChanged(this.chkPayToAdd, new EventArgs());


            }
            else
            {
                txtRefNoAdd.Text = string.Empty;
                txtTransDateAdd.Text = string.Empty;
                ComboBox2Add.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void GetRefNo_Click(object sender, EventArgs e)
    {
        try
        {
            cmdSaveContact.Visible = true;
            cmdUpdateContact.Visible = false;
            updatePnlContact.Update();

            txtContactedDate.Text = string.Empty;

            ModalPopupContact.Show();
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdCancelContact_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupContact.Hide();
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void cmdUpdateContact_Click(object sender, EventArgs e)
    {
        ModalPopupContact.Hide();
        ModalPopupExtender1.Show();

        string sCustomer = string.Empty;
        string strPaymode = string.Empty;
        string strPaymodef = string.Empty;

        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            //TextBox testcheck = (TextBox)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtoldref");
            int iRefID = Convert.ToInt32(txtContactedDate.Text);

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            DataSet customerDs = bl.GetPaymentForRef(connection, iRefID);
            string address = string.Empty;

            if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            {

                if (customerDs.Tables[0].Rows[0]["RefNo"] != null)
                    txtRefNoAdd.Text = customerDs.Tables[0].Rows[0]["RefNo"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["TransDate"] != null)
                {
                    string aa = customerDs.Tables[0].Rows[0]["TransDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    txtTransDateAdd.Text = dtaa + Environment.NewLine;
                }

                if (customerDs.Tables[0].Rows[0]["DebtorID"] != null)
                {
                    //ComboBox2Add.SelectedItem.Value = customerDs.Tables[0].Rows[0]["DebtorID"].ToString() + Environment.NewLine;
                    //ComboBox2Add.Text = customerDs.Tables[0].Rows[0]["Debi"].ToString() + Environment.NewLine;

                    sCustomer = Convert.ToString(customerDs.Tables[0].Rows[0]["DebtorID"]);
                    ComboBox2Add.ClearSelection();
                    ListItem li = ComboBox2Add.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                    if (li != null) li.Selected = true;
                }

                if (customerDs.Tables[0].Rows[0]["Amount"] != null)
                    txtAmountAdd.Text = customerDs.Tables[0].Rows[0]["Amount"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["Paymode"] != null)
                {
                    //if (customerDs.Tables[0].Rows[0]["Paymode"].ToString() == "Cash")
                    //    chkPayToAdd.SelectedItem.Value = "Cash" + Environment.NewLine;
                    //else
                    //    chkPayToAdd.SelectedItem.Value = "Cheque" + Environment.NewLine;

                    strPaymodef = customerDs.Tables[0].Rows[0]["Paymode"].ToString();
                    chkPayToAdd.ClearSelection();
                    ListItem piLi = chkPayToAdd.Items.FindByValue(strPaymodef.Trim());
                    if (piLi != null) piLi.Selected = true;

                }

                if (customerDs.Tables[0].Rows[0]["Narration"] != null)
                    txtNarrationAdd.Text = customerDs.Tables[0].Rows[0]["Narration"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["CreditorId"] != null)
                {
                    //ddBanksAdd.SelectedItem.Value = customerDs.Tables[0].Rows[0]["CreditorId"] + Environment.NewLine;

                    strPaymode = Convert.ToString(customerDs.Tables[0].Rows[0]["CreditorId"]);
                    ddBanksAdd.ClearSelection();
                    ListItem pLi = ddBanksAdd.Items.FindByValue(strPaymode.Trim());
                    if (pLi != null) pLi.Selected = true;
                }

                if (customerDs.Tables[0].Rows[0]["ChequeNo"] != null)
                    txtChequeNoAdd.Text = customerDs.Tables[0].Rows[0]["ChequeNo"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["BillNo"] != null)
                    txtBillAdd.Text = customerDs.Tables[0].Rows[0]["BillNo"].ToString() + Environment.NewLine;


                ModalPopupExtender1.Show();
                //if (strPaymodef == "Cheque")
                //{
                //    //RadioButtonList chk = (RadioButtonList)sender;
                //    if (strPaymodef == "Cheque")
                //    {
                //        //Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd");
                //        PanelBankAdd.Visible = true;
                //    }
                //}

                //chkPayToAdd.SelectedIndexChanged += new EventHandler(chkPayToAdd_SelectedIndexChanged);

                //chkPayToAdd_SelectedIndexChanged(this.chkPayToAdd, new EventArgs());


            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('RefNo Not Found');", true);
                ModalPopupContact.Show();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdSaveContact_Click(object sender, EventArgs e)
    {

        ModalPopupContact.Hide();
        ModalPopupExtender1.Show();

        string sCustomer = string.Empty;
        string strPaymode = string.Empty;
        string strPaymodef = string.Empty;

        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            //TextBox testcheck = (TextBox)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtoldref");
            int iRefID = Convert.ToInt32(txtContactedDate.Text);

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            DataSet customerDs = bl.GetPaymentForRef(connection, iRefID);
            string address = string.Empty;

            if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            {

                if (customerDs.Tables[0].Rows[0]["RefNo"] != null)
                    txtRefNoAdd.Text = customerDs.Tables[0].Rows[0]["RefNo"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["TransDate"] != null)
                {
                    string aa = customerDs.Tables[0].Rows[0]["TransDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    txtTransDateAdd.Text = dtaa + Environment.NewLine;
                }

                if (customerDs.Tables[0].Rows[0]["DebtorID"] != null)
                {
                    //ComboBox2Add.SelectedItem.Value = customerDs.Tables[0].Rows[0]["DebtorID"].ToString() + Environment.NewLine;
                    //ComboBox2Add.Text = customerDs.Tables[0].Rows[0]["Debi"].ToString() + Environment.NewLine;

                    sCustomer = Convert.ToString(customerDs.Tables[0].Rows[0]["DebtorID"]);
                    ComboBox2Add.ClearSelection();
                    ListItem li = ComboBox2Add.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                    if (li != null) li.Selected = true;
                }

                if (customerDs.Tables[0].Rows[0]["Amount"] != null)
                    txtAmountAdd.Text = customerDs.Tables[0].Rows[0]["Amount"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["Paymode"] != null)
                {
                    //if (customerDs.Tables[0].Rows[0]["Paymode"].ToString() == "Cash")
                    //    chkPayToAdd.SelectedItem.Value = "Cash" + Environment.NewLine;
                    //else
                    //    chkPayToAdd.SelectedItem.Value = "Cheque" + Environment.NewLine;

                    strPaymodef = customerDs.Tables[0].Rows[0]["Paymode"].ToString();
                    chkPayToAdd.ClearSelection();
                    ListItem piLi = chkPayToAdd.Items.FindByValue(strPaymodef.Trim());
                    if (piLi != null) piLi.Selected = true;




                }

                if (customerDs.Tables[0].Rows[0]["Narration"] != null)
                    txtNarrationAdd.Text = customerDs.Tables[0].Rows[0]["Narration"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["CreditorId"] != null)
                {
                    //ddBanksAdd.SelectedItem.Value = customerDs.Tables[0].Rows[0]["CreditorId"] + Environment.NewLine;

                    strPaymode = Convert.ToString(customerDs.Tables[0].Rows[0]["CreditorId"]);
                    ddBanksAdd.ClearSelection();
                    ListItem pLi = ddBanksAdd.Items.FindByValue(strPaymode.Trim());
                    if (pLi != null) pLi.Selected = true;
                }

                if (customerDs.Tables[0].Rows[0]["ChequeNo"] != null)
                    txtChequeNoAdd.Text = customerDs.Tables[0].Rows[0]["ChequeNo"].ToString() + Environment.NewLine;

                if (customerDs.Tables[0].Rows[0]["BillNo"] != null)
                    txtBillAdd.Text = customerDs.Tables[0].Rows[0]["BillNo"].ToString() + Environment.NewLine;


                ModalPopupExtender1.Show();
                //if (strPaymodef == "Cheque")
                //{
                //    //RadioButtonList chk = (RadioButtonList)sender;
                //    if (strPaymodef == "Cheque")
                //    {
                //        //Panel test = (Panel)frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("PanelBankAdd");
                //        PanelBankAdd.Visible = true;
                //    }
                //}

                //chkPayToAdd.SelectedIndexChanged += new EventHandler(chkPayToAdd_SelectedIndexChanged);

                //chkPayToAdd_SelectedIndexChanged(this.chkPayToAdd, new EventArgs());


            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('RefNo Not Found');", true);
                ModalPopupContact.Show();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
}
