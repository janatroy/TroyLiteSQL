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
using System.Xml.Linq;

public partial class ComplaintMgmt : System.Web.UI.Page
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
                    //GrdViewComplaint.Columns[8].Visible = false;
                    //GrdViewComplaint.Columns[7].Visible = false;
                }

                BindGrid();
                //loadCreditorDebtor();

                if (Request.Cookies["Company"] != null)
                    hdDataSource.Value = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("~/Login.aspx");


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

        if (Request.Cookies["Company"]  != null)
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");


        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.ListComplaints(sDataSource, txtSearch.Text, ddCriteria.SelectedValue, chkActive.Checked);

        if (ds != null)
        {
            GrdViewComplaint.DataSource = ds.Tables[0].DefaultView;
            GrdViewComplaint.DataBind();
        }
        else
        {
            GrdViewComplaint.DataSource = null;
            GrdViewComplaint.DataBind();

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
            ds = bl.ListComplaints(sDataSource, txtSearch.Text, ddCriteria.SelectedValue, chkActive.Checked);

            if (ds != null)
            {
                GrdViewComplaint.DataSource = ds.Tables[0].DefaultView;
                GrdViewComplaint.DataBind();
            }
            else
            {
                GrdViewComplaint.EmptyDataText = "No Complaints found";
                GrdViewComplaint.DataSource = null;
                GrdViewComplaint.DataBind();

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



    protected void GrdViewComplaint_Sorting(object sender, GridViewSortEventArgs e)
    {
        //DataTable dataTable = GrdViewComplaint.DataSource as DataTable;

        //if (dataTable != null)
        //{
        //    DataView dataView = new DataView(dataTable);
        //    dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

        //    GrdViewComplaint.DataSource = dataView;
        //    GrdViewComplaint.DataBind();
        //}

        //BindGrid();


    }

    protected void drpCustomer_DataBound(object sender, EventArgs e)
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
    protected void drpAssignedTo_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string creditorID = ((DataRowView)frmV.DataItem)["AssignedTo"].ToString();

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


        if (((TextBox)this.frmViewAdd.FindControl("txtComplaintDateAdd")) != null)
            e.InputParameters["ComplaintDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtComplaintDateAdd")).Text);

        if (((DropDownList)this.frmViewAdd.FindControl("drpCustomerAdd")).Text != "")
            e.InputParameters["CustomerID"] = ((DropDownList)this.frmViewAdd.FindControl("drpCustomerAdd")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("drpStatusAdd")).Text != "")
            e.InputParameters["ComplaintStatus"] = ((DropDownList)this.frmViewAdd.FindControl("drpStatusAdd")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("drpAssignedToAdd")).Text != "")
            e.InputParameters["AssignedTo"] = ((DropDownList)this.frmViewAdd.FindControl("drpAssignedToAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtComplaintDetialsAdd")).Text != "")
            e.InputParameters["ComplaintDetails"] = ((TextBox)this.frmViewAdd.FindControl("txtComplaintDetialsAdd")).Text;

        if (((DropDownList)this.frmViewAdd.FindControl("drpBilledAdd")) != null)
            e.InputParameters["IsBilled"] = ((DropDownList)this.frmViewAdd.FindControl("drpBilledAdd")).SelectedValue;

        string sDataSource = string.Empty;

        if (Request.Cookies["Company"]  != null)
            sDataSource = Request.Cookies["Company"].Value;

        e.InputParameters["sPath"] = sDataSource;

    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("drpCustomer")) != null)
            e.InputParameters["CustomerID"] = int.Parse(((DropDownList)this.frmViewAdd.FindControl("drpCustomer")).SelectedValue);

        if (((DropDownList)this.frmViewAdd.FindControl("drpStatus")) != null)
            e.InputParameters["ComplaintStatus"] = ((DropDownList)this.frmViewAdd.FindControl("drpStatus")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("drpAssignedTo")) != null)
            e.InputParameters["AssignedTo"] = int.Parse(((DropDownList)this.frmViewAdd.FindControl("drpAssignedTo")).SelectedValue);

        if (((TextBox)this.frmViewAdd.FindControl("txtComplaintDate")).Text != "")
            e.InputParameters["ComplaintDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtComplaintDate")).Text);

        if (((DropDownList)this.frmViewAdd.FindControl("drpBilled")).SelectedValue != "0")
            e.InputParameters["IsBilled"] = ((DropDownList)this.frmViewAdd.FindControl("drpBilled")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtComplaintDetials")) != null)
            e.InputParameters["ComplaintDetails"] = ((TextBox)this.frmViewAdd.FindControl("txtComplaintDetials")).Text;

        string sDataSource = string.Empty;

        if (Request.Cookies["Company"]  != null)
            sDataSource = Request.Cookies["Company"].Value;

        e.InputParameters["sPath"] = sDataSource;

        e.InputParameters["ComplaintID"] = int.Parse(GrdViewComplaint.SelectedDataKey.Value.ToString());

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
            if (this.frmViewAdd.FindControl("txtComplaintDateAdd") != null)
                ((TextBox)this.frmViewAdd.FindControl("txtComplaintDateAdd")).Text = DateTime.Now.ToShortDateString();
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
                if (this.frmViewAdd.FindControl("txtComplaintDateAdd") != null)
                {
                    if (ViewState["TransDate"] == null)
                        ((TextBox)this.frmViewAdd.FindControl("txtComplaintDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
                    else
                        ((TextBox)this.frmViewAdd.FindControl("txtComplaintDateAdd")).Text = ViewState["TransDate"].ToString();
                }

                if (this.frmViewAdd != null)
                {
                    if (this.frmViewAdd.FindControl("myRangeValidatorAdd") != null)
                    {
                        ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidatorAdd")).MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                        ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidatorAdd")).MaximumValue = System.DateTime.Now.ToShortDateString();
                    }
                }

            }

            if (this.frmViewAdd.FindControl("tabEdit") != null)
            {
                if (this.frmViewAdd.FindControl("myRangeValidator") != null)
                {
                    ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidator")).MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                    ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidator")).MaximumValue = System.DateTime.Now.ToShortDateString();
                }
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
            lnkBtnAdd.Visible = true;
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Complaint Updated Successfully')", true);
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
            if (e.OutputParameters["ComplaintID"] != null)
            {
                if (e.OutputParameters["ComplaintID"].ToString() != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Complaint Saved Successfully. Complaint No : " + e.OutputParameters["ComplaintID"].ToString() + "');", true);
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

    }


    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setUpdateParameters(e);

            string sDataSource = string.Empty;

            if (Request.Cookies["Company"] != null)
                sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bll = new BusinessLogic();
            string recondate = ((TextBox)this.frmViewAdd.FindControl("txtComplaintDate")).Text;
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
            //    GrdViewComplaint.PageIndex = 0;
            //    GrdViewComplaint.DataSource = ds.Tables[0].DefaultView;
            //    GrdViewComplaint.DataBind();
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
            string recondate = ((TextBox)this.frmViewAdd.FindControl("txtComplaintDateAdd")).Text;


            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid');", true);
                return;
            }


            //lnkBtnAdd.Visible = true;
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
                GrdViewComplaint.Visible = true;
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
                GrdViewComplaint.Visible = true;
            }
            else
            {
                if (e.Exception.InnerException != null)
                {

                    if (e.Exception.InnerException != null)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);

                    e.ExceptionHandled = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.Message + "');", true);
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

    protected void GrdViewComplaint_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewComplaint, e.Row, this);
            }
            //hdJournal.Value = Convert.ToString(GrdViewComplaint.SelectedDataKey.Value); 
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
            GrdViewComplaint.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewComplaint_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow Row = GrdViewComplaint.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();
            string recondate = Row.Cells[1].Text;
            hdJournal.Value = Convert.ToString(GrdViewComplaint.SelectedDataKey.Value);
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
                ModalPopupExtender1.Show();
            }
            //cmdPrint.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewComplaint_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GrdViewComplaint_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }
    protected void GrdViewComplaint_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewComplaint.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewComplaint_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int TransNo = (int)GrdViewComplaint.DataKeys[e.RowIndex].Value;

            string sDataSource = string.Empty;

            if (Request.Cookies["Company"] != null)
                sDataSource = Request.Cookies["Company"].Value;
            else
                Response.Redirect("~/frm_Login.aspx");
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl1 = new BusinessLogic();
            string recondate = GrdViewComplaint.Rows[e.RowIndex].Cells[1].Text; ;
            if (!bl1.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                //frmViewAdd.Visible = true;
                //frmViewAdd.ChangeMode(FormViewMode.ReadOnly);
                return;

            }
            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.DeleteComplaint(TransNo, sDataSource);
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
            if (GrdViewComplaint.SelectedDataKey != null)
                e.InputParameters["TransNo"] = GrdViewComplaint.SelectedDataKey.Value;
            e.InputParameters["sPath"] = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
