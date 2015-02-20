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

public partial class MultipleEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
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
                    GrdViewJournal.Columns[8].Visible = false;
                    GrdViewJournal.Columns[7].Visible = false;
                }


                GrdViewJournal.PageSize = 7;


                BindGrid();
                //loadCreditorDebtor();

                if (Request.Cookies["Company"] != null)
                    hdDataSource.Value = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("~/frm_Login.aspx");


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void BindGrid()
    {

        string sDataSource = string.Empty;

        if (Request.Cookies["Company"] != null)
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");


        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.ListJournal(txtTransno.Text.Trim(), txtRefno.Text.Trim(), txtLedger.Text.Trim(), txtDate.Text, sDataSource);

        if (ds != null)
        {
            GrdViewJournal.DataSource = ds.Tables[0].DefaultView;
            GrdViewJournal.DataBind();
        }
        else
        {
            GrdViewJournal.DataSource = null;
            GrdViewJournal.DataBind();

        }

    }

    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sDataSource = string.Empty;

            if (Request.Cookies["Company"] != null)
                sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");


            DataSet ds = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            ds = bl.ListJournal(txtTransno.Text.Trim(), txtRefno.Text.Trim(), txtLedger.Text.Trim(), txtDate.Text, sDataSource);

            if (ds != null)
            {
                GrdViewJournal.DataSource = ds.Tables[0].DefaultView;
                GrdViewJournal.DataBind();
            }
            else
            {
                GrdViewJournal.EmptyDataText = "No journals found";
                GrdViewJournal.DataSource = null;
                GrdViewJournal.DataBind();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }



    protected void GrdViewJournal_Sorting(object sender, GridViewSortEventArgs e)
    {
        //DataTable dataTable = GrdViewJournal.DataSource as DataTable;

        //if (dataTable != null)
        //{
        //    DataView dataView = new DataView(dataTable);
        //    dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

        //    GrdViewJournal.DataSource = dataView;
        //    GrdViewJournal.DataBind();
        //}

        //BindGrid();


    }

    protected void ComboBox2_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)ddl.NamingContainer;

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
    protected void cmbCreditor_DataBound(object sender, EventArgs e)
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

    protected void cmbCreditorAdd_DataBound(object sender, EventArgs e)
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

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("cmbDebtorAdd1")) != null)
            e.InputParameters["DebitorID"] = ((DropDownList)this.frmViewAdd.FindControl("cmbDebtorAdd1")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("cmbCreditorAdd1")) != null)
            e.InputParameters["CreditorID"] = ((DropDownList)this.frmViewAdd.FindControl("cmbCreditorAdd1")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefnumAdd")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefnumAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtNarrAdd")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("txtNarrAdd")).Text;

        string sDataSource = string.Empty;

        if (Request.Cookies["Company"] != null)
            sDataSource = Request.Cookies["Company"].Value;

        e.InputParameters["VoucherType"] = "Journal";
        e.InputParameters["sPath"] = sDataSource;

        //if (e.InputParameters["DebitorID"] == e.InputParameters["CreditorID"])
        //  throw new Exception("Same Creditor and Debtor");

    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("cmbDebtor1")) != null)
            e.InputParameters["DebitorID"] = ((DropDownList)this.frmViewAdd.FindControl("cmbDebtor1")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("cmbCreditor1")) != null)
            e.InputParameters["CreditorID"] = ((DropDownList)this.frmViewAdd.FindControl("cmbCreditor1")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefnum")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefnum")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text);



        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtNarr")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("txtNarr")).Text;

        string sDataSource = string.Empty;

        if (Request.Cookies["Company"] != null)
            sDataSource = Request.Cookies["Company"].Value;

        e.InputParameters["sPath"] = sDataSource;
        e.InputParameters["VoucherType"] = "Journal";

        e.InputParameters["TransNo"] = GrdViewJournal.SelectedDataKey.Value;

        //if (e.InputParameters["DebitorID"].ToString() == e.InputParameters["CreditorID"].ToString())
        //throw new Exception("Same Creditor and Debtor");

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            frmViewAdd.Visible = false;
            ModalPopupExtender1.Hide();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {

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
            ModalPopupExtender1.Show();
            //if (this.frmViewAdd.FindControl("txtTransDateAdd") != null)
            //((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text = DateTime.Now.ToShortDateString();
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
            if (this.frmViewAdd.FindControl("txtTransDateAdd") != null)
            {
                if (ViewState["TransDate"] == null)
                    ((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
                else
                    ((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text = ViewState["TransDate"].ToString();
            }

            if (this.frmViewAdd != null)
            {
                if (this.frmViewAdd.FindControl("myRangeValidatorAdd") != null)
                {
                    ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidatorAdd")).MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                    ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidatorAdd")).MaximumValue = System.DateTime.Now.ToShortDateString();
                }
            }


            if (this.frmViewAdd.FindControl("myRangeValidator") != null)
            {
                ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidator")).MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidator")).MaximumValue = System.DateTime.Now.ToShortDateString();
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
            frmViewAdd.Visible = false;
            ModalPopupExtender1.Hide();
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journal Updated Successfully. New Transaction No : " + e.OutputParameters["NewTransNo"].ToString() + "');", true);
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journal Saved Successfully. Transaction No : " + e.OutputParameters["NewTransNo"].ToString() + "');", true);
                }
            }
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
            if (((DropDownList)this.frmViewAdd.FindControl("cmbDebtor1")).SelectedValue ==
            ((DropDownList)this.frmViewAdd.FindControl("cmbCreditor1")).SelectedValue)
            {
                e.Cancel = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Creditor and Debtor should not be same.');", true);
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
            txtLedger.Text = string.Empty;
            txtDate.Text = string.Empty;
            txtRefno.Text = string.Empty;

            string sDataSource = string.Empty;

            if (Request.Cookies["Company"] != null)
                sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bll = new BusinessLogic();
            string recondate = ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text;
            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid');", true);
                return;
            }

            /*if (((DropDownList)this.frmViewAdd.FindControl("cmbDebtor")).SelectedValue ==
            ((DropDownList)this.frmViewAdd.FindControl("cmbCreditor")).SelectedValue)
            {
                e.Cancel = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Creditor and Debtor should not be same.');", true);
                return;
            }*/

            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //DataSet ds = new DataSet();
            //ds = bl.ListJournal("", "", "", sDataSource);
            //if (ds != null)
            //{
            //    GrdViewJournal.PageIndex = 0;
            //    GrdViewJournal.DataSource = ds.Tables[0].DefaultView;
            //    GrdViewJournal.DataBind();
            //}
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            //cmdPrint.Enabled = true;
            BindGrid();
            //BindGrid();
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
            BusinessLogic bl = new BusinessLogic();
            string recondate = ((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text;

            /*
            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid');", true);
                return;
            }*/

            if (((DropDownList)this.frmViewAdd.FindControl("cmbDebtorAdd1")).SelectedValue ==
                ((DropDownList)this.frmViewAdd.FindControl("cmbCreditorAdd1")).SelectedValue)
            {
                e.Cancel = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Creditor and Debtor should not be same.');", true);
                return;
            }

            lnkBtnAdd.Visible = true;
            //BindGrid();
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
                //MyAccordion.Visible = true;
                GrdViewJournal.Visible = true;
                System.Threading.Thread.Sleep(1000);
                BindGrid();
            }
            else if (e.AffectedRows == 0)
            {
                e.KeepInInsertMode = true;
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") == -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);
                }
                else
                {
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
                BindGrid();
                //MyAccordion.Visible = true;
                GrdViewJournal.Visible = true;
            }
            else
            {
                if (e.Exception.InnerException != null)
                {

                    if (e.Exception.InnerException.Message.IndexOf("Same Creditor Debtor") == -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Creditor and Debtor should be different.');", true);

                    e.ExceptionHandled = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);
                }
                e.KeepInEditMode = true;
                lnkBtnAdd.Visible = false;
                frmViewAdd.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewJournal_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewJournal, e.Row, this);
            }
            //hdJournal.Value = Convert.ToString(GrdViewJournal.SelectedDataKey.Value);  
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdViewJournal.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewJournal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow Row = GrdViewJournal.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();
            string recondate = Row.Cells[2].Text;
            hdJournal.Value = Convert.ToString(GrdViewJournal.SelectedDataKey.Value);
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
                //GrdViewPayment.Columns[8].Visible = false;
                //lnkBtnAdd.Visible = false;
                ////MyAccordion.Visible = false;
                //GrdViewJournal.Visible = false;
                ModalPopupExtender1.Show();
            }
            //cmdPrint.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewJournal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //GridView gridView = (GridView)sender;

        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    int cellIndex = -1;
        //    foreach (DataControlField field in gridView.Columns)
        //    {
        //        if (field.SortExpression == gridView.SortExpression)
        //        {
        //            cellIndex = gridView.Columns.IndexOf(field);
        //        }
        //        else if (field.SortExpression != "")
        //        {
        //            e.Row.Cells[gridView.Columns.IndexOf(field)].CssClass = "headerstyle";
        //        }

        //    }

        //    if (cellIndex > -1)
        //    {
        //        //  this is a header row,
        //        //  set the sort style
        //        e.Row.Cells[cellIndex].CssClass =
        //            gridView.SortDirection == SortDirection.Ascending
        //            ? "sortascheaderstyle" : "sortdescheaderstyle";
        //    }
        //}

    }

    protected void GrdViewJournal_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }
    protected void GrdViewJournal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewJournal.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewJournal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int TransNo = (int)GrdViewJournal.DataKeys[e.RowIndex].Value;

            string sDataSource = string.Empty;

            if (Request.Cookies["Company"] != null)
                sDataSource = Request.Cookies["Company"].Value;
            else
                Response.Redirect("~/frm_Login.aspx");
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl1 = new BusinessLogic();
            string recondate = GrdViewJournal.Rows[e.RowIndex].Cells[2].Text; ;
            if (!bl1.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                //frmViewAdd.Visible = true;
                //frmViewAdd.ChangeMode(FormViewMode.ReadOnly);
                return;

            }
            BusinessLogic bl = new BusinessLogic(sDataSource);
            //bl.DeleteJournal(TransNo, sDataSource);
            BindGrid();
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
            if (GrdViewJournal.SelectedDataKey != null)
                e.InputParameters["TransNo"] = GrdViewJournal.SelectedDataKey.Value;
            e.InputParameters["sPath"] = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    bool IsFutureDate(DateTime refDate)
    {
        DateTime today = DateTime.Today;
        return (refDate.Date != today) && (refDate > today);
    }


}
