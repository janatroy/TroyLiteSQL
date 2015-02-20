﻿using System;
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

public partial class ServiceMGTEntry : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (hdCustomerID.Value != "0")
                drpCustomer.SelectedValue = hdCustomerID.Value;
            if (hdRefNumber.Value != "")
                txtRefNo.Text = hdRefNumber.Value;
            if (hdDueDate.Value.ToString() != "")
                txtDueDate.Text = hdDueDate.Value.ToString();
            if (hdServiceID.Value.ToString() != "")
                hdServiceID.Value = hdServiceID.Value.ToString();

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

                loadBanks(connStr);
                loadCustomerDealers(connStr);

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    GrdViewSerVisit.Columns[7].Visible = false;
                }

                myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();


                GrdViewSerVisit.PageSize = 8;

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic();

                if (bl.CheckUserHaveAddNote(connection, usernam, "SRVMGMT"))
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

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                int CustomerID = 0;
                int ServiceID = 0;
                DateTime DueDate;
                DateTime VisitDate;
                string VisitDetails = string.Empty;
                double Amount = 0.0;
                int PayMode;
                string Visited;
                string CreditCardNo;
                int iBank = 0;
                GridViewRow row = GrdViewSerVisit.SelectedRow;

                int VisitID = Convert.ToInt32(GrdViewSerVisit.SelectedDataKey.Value);

                CustomerID = int.Parse(drpCustomer.SelectedValue);
                ServiceID = int.Parse(hdServiceID.Value.ToString());
                DueDate = DateTime.Parse(txtDueDate.Text);
                VisitDate = DateTime.Parse(txtVisitDate.Text);
                VisitDetails = txtDetials.Text;
                Amount = double.Parse(txtAmount.Text);
                PayMode = int.Parse(drpPaymode.SelectedValue);
                Visited = chkVisited.Checked.ToString();
                CreditCardNo = txtCreditCardNo.Text;
                iBank = int.Parse(drpBankName.SelectedValue);

                BusinessLogic bl = new BusinessLogic(sDataSource);

                try
                {
                    bl.UpdateServiceVisit(connection, VisitID, CustomerID, ServiceID, DueDate, VisitDate, VisitDetails, Amount, PayMode, Visited, CreditCardNo, iBank);

                    //MyAccordion.Visible = true;
                    pnlVisitDetails.Visible = false;
                    lnkBtnAdd.Visible = true;
                    Reset();
                    GrdViewSerVisit.DataBind();
                    GrdViewSerVisit.Visible = true;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Service Visit Details Saved Successfully.');", true);
                    return;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured: " + ex.Message + "')", true);
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadCustomerDealers(string sDataSource)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListCustomersDealers(sDataSource);
        drpCustomer.DataSource = ds;
        drpCustomer.DataBind();
        drpCustomer.DataTextField = "LedgerName";
        drpCustomer.DataValueField = "LedgerID";
    }

    private void loadBanks(string sDataSource)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanks();
        drpBankName.DataSource = ds;
        drpBankName.DataBind();
        drpBankName.DataTextField = "LedgerName";
        drpBankName.DataValueField = "LedgerID";

    }

    public void Reset()
    {

        txtAmount.Text = "";
        txtCreditCardNo.Text = "";
        txtDetials.Text = "";
        txtDueDate.Text = "";
        txtRefNo.Text = "";
        txtVisitDate.Text = "";
        drpPaymode.SelectedIndex = 0;
        drpCustomer.SelectedIndex = 0;
        drpBankName.SelectedIndex = 0;

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //MyAccordion.Visible = true;
            pnlVisitDetails.Visible = false;
            lnkBtnAdd.Visible = true;
            Reset();
            GrdViewSerVisit.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewSerVisit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEditNote(connection, usernam, "SRVMGMT"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewSerVisit_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void GrdViewSerVisit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GrdViewSerVisit.SelectedIndex = e.RowIndex;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewSerVisit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = GrdViewSerVisit.SelectedRow;

            int VisitID = Convert.ToInt32(GrdViewSerVisit.SelectedDataKey.Value);

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            DataSet ds = bl.GetServiceVisitForId(sDataSource, VisitID);

            if (ds != null)
            {
                hdVisitID.Value = Convert.ToString(VisitID);
                //drpCustomer.DataBind();
                drpCustomer.ClearSelection();
                ListItem li2 = drpCustomer.Items.FindByValue(ds.Tables[0].Rows[0]["CustomerID"].ToString());
                if (li2 != null) li2.Selected = true;

                txtRefNo.Text = ds.Tables[0].Rows[0]["RefNumber"].ToString();
                txtDueDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["DueDate"].ToString()).ToShortDateString();
                txtVisitDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["VisitDate"].ToString()).ToShortDateString();
                hdServiceID.Value = ds.Tables[0].Rows[0]["VisitID"].ToString();
                string strPaymode = ds.Tables[0].Rows[0]["PayMode"].ToString();

                drpPaymode.ClearSelection();
                ListItem pLi = drpPaymode.Items.FindByText(strPaymode.Trim());

                if (ds.Tables[0].Rows[0]["Visited"].ToString() == "True")
                    chkVisited.Checked = true;
                else
                    chkVisited.Checked = false;

                drpPaymode.SelectedValue = strPaymode;

                if (pLi != null) pLi.Selected = true;


                if (strPaymode.Trim() == "Bank")
                    drpPaymode.SelectedValue = 2.ToString();

                if (paymodeVisible(strPaymode))
                {
                    if (ds.Tables[0].Rows[0]["CreditCardNo"] != null)
                        txtCreditCardNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CreditCardNo"]);
                    if (ds.Tables[0].Rows[0]["BankID"] != null)
                    {
                        drpBankName.ClearSelection();
                        ListItem cli = drpBankName.Items.FindByValue(HttpUtility.HtmlDecode(Convert.ToString(ds.Tables[0].Rows[0]["BankID"])));

                        if (cli != null) cli.Selected = true;
                    }
                }

                txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                txtCreditCardNo.Text = ds.Tables[0].Rows[0]["CreditCardNo"].ToString();

                ListItem liBank = drpCustomer.Items.FindByValue(ds.Tables[0].Rows[0]["CustomerID"].ToString());
                if (li2 != null) li2.Selected = true;

                txtDetials.Text = ds.Tables[0].Rows[0]["VisitDetails"].ToString();

                UpdateButton.Visible = true;
                SaveButton.Visible = false;
                CancelButton.Visible = true;
                lnkBtnAdd.Visible = false;
                //MyAccordion.Visible = false;
                drpCustomer.Enabled = false;
                txtDueDate.Enabled = false;
                GrdViewSerVisit.Visible = false;
                pnlVisitDetails.Visible = true;
                btnSearchService.Enabled = false;
                ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpPaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();

        try
        {

            if (drpPaymode.SelectedIndex == 1)
            {
                pnlBank.Visible = true;
                txtCreditCardNo.Focus();
            }
            else
                pnlBank.Visible = false;

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private bool paymodeVisible(string paymode)
    {
        if (paymode.ToUpper() != "2")
        {
            pnlBank.Visible = false;
            return false;
        }
        else
        {
            pnlBank.Visible = true;
            return true;
        }
    }

    protected void GrdViewSerVisit_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GrdViewSerVisit_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewSerVisit, e.Row, this);
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
            UpdateButton.Visible = false;
            SaveButton.Visible = true;
            btnSearchService.Enabled = true;
            drpCustomer.Enabled = true;
            bankPanel.Update();
            pnlBank.Visible = false;
            ModalPopupExtender1.Show();
            pnlVisitDetails.Visible = true;
            Reset();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdViewSerVisit.SelectedDataKey != null)
                e.InputParameters["VisitID"] = GrdViewSerVisit.SelectedDataKey.Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                int CustomerID = 0;
                int ServiceID = 0;
                DateTime DueDate;
                DateTime VisitDate;
                string VisitDetails = string.Empty;
                double Amount = 0.0;
                int PayMode;
                string Visited;
                string CreditCardNo;
                int iBank = 0;

                CustomerID = int.Parse(drpCustomer.SelectedValue);
                ServiceID = int.Parse(hdServiceID.Value.ToString());
                DueDate = DateTime.Parse(txtDueDate.Text);
                VisitDate = DateTime.Parse(txtVisitDate.Text);
                VisitDetails = txtDetials.Text;
                Amount = double.Parse(txtAmount.Text);
                PayMode = int.Parse(drpPaymode.SelectedValue);
                Visited = chkVisited.Checked.ToString();
                CreditCardNo = txtCreditCardNo.Text;
                iBank = int.Parse(drpBankName.SelectedValue);

                BusinessLogic bl = new BusinessLogic(sDataSource);

                try
                {
                    bl.InsertServiceVisit(connection, CustomerID, ServiceID, DueDate, VisitDate, VisitDetails, Amount, PayMode, Visited, CreditCardNo, iBank);

                    //MyAccordion.Visible = true;
                    pnlVisitDetails.Visible = false;
                    lnkBtnAdd.Visible = true;
                    Reset();
                    GrdViewSerVisit.DataBind();
                    GrdViewSerVisit.Visible = true;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Service Visit Details Saved Successfully.');", true);
                    return;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured: " + ex.Message + "')", true);
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
