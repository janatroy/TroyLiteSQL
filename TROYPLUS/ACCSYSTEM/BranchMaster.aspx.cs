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
using System.IO;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Data.OleDb;
using System.IO;
using ClosedXML.Excel;

public partial class BranchMaster : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

            //if (hdCustomerID.Value != "0")
            //    drpCustomer.SelectedValue = hdCustomerID.Value;
            //if (hdRefNumber.Value != "")
            //    txtRefNo.Text = hdRefNumber.Value;
            //if (hdDueDate.Value.ToString() != "")
            //    txtDueDate.Text = hdDueDate.Value.ToString();
            //if (hdServiceID.Value.ToString() != "")
            //    hdServiceID.Value = hdServiceID.Value.ToString();
           
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

                //myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

                GrdViewSerVisit.PageSize = 11;


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "BRNCHMAS"))
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
             
                   
                    GridViewRow row = GrdViewSerVisit.SelectedRow;

                    int BranchID = Convert.ToInt32(GrdViewSerVisit.SelectedDataKey.Value);

                    string BranchAddress1 = string.Empty;
                    string Branchcode = string.Empty;
                    string BranchAddress2 = string.Empty;
                    string BranchAddress3 = string.Empty;
                    string BranchLocation = string.Empty;
                    string mobile1 = string.Empty;
                    string mobile2 = string.Empty;
                    string emailid = string.Empty;
                    string IsActive = string.Empty;

                    IsActive = drpIsActive.SelectedValue;

                    string Username = Request.Cookies["LoggedUserName"].Value;

                    Branchcode = txtBranchcode.Text.Trim();
                    string BranchName = string.Empty;
                    BranchName = txtBranchName.Text.Trim();
                    BranchAddress1 = txtBranchAddress1.Text.Trim();
                    BranchAddress2 = txtBranchAddress2.Text.Trim();
                    BranchAddress3 = txtBranchAddress3.Text.Trim();
                    BranchLocation = txtBranchLocation.Text.Trim();
                    mobile1 = Txtmobile1.Text.Trim();
                    mobile2 = Txtmobile2.Text.Trim();
                    emailid = Txtemailid.Text.Trim();

                    BusinessLogic bl = new BusinessLogic(sDataSource);
                    string connection = Request.Cookies["Company"].Value;


                    bl.UpdateBranch(connection, BranchID, Branchcode, BranchName, BranchAddress1, BranchAddress2, BranchAddress3, BranchLocation, mobile1, mobile2, emailid, IsActive, Username);

                        //MyAccordion.Visible = true;
                        pnlVisitDetails.Visible = false;
                        lnkBtnAdd.Visible = true;
                        Reset();
                        GrdViewSerVisit.DataBind();
                        GrdViewSerVisit.Visible = true;
                
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Branch Details Updated Successfully.');", true);

                        return;
            }
            pnlVisitDetails.Visible = false;
            lnkBtnAdd.Visible = true;
            Reset();
            GrdViewSerVisit.DataBind();
            GrdViewSerVisit.Visible = true;
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
        //drpCustomer.DataSource = ds;
        //drpCustomer.DataBind();
        //drpCustomer.DataTextField = "LedgerName";
        //drpCustomer.DataValueField = "LedgerID";
    }

    private void loadBanks(string sDataSource)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = new DataSet();
        //ds = bl.ListBanks();
        //ddBankName.DataSource = ds;
        //ddBankName.DataBind();
        //ddBankName.DataTextField = "LedgerName";
        //ddBankName.DataValueField = "LedgerID";
    }

    protected void DamageLeaf_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("DamageCheque.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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

    protected void UnusedLeaf_Click(object sender, EventArgs e)
    {
        try
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;

            string filename = "UnUsed Cheque Leaf.xls";
            string fLvlValueTemp = string.Empty;
            string tLvlValueTemp = string.Empty;

            DataTable dtf = new DataTable();
            DataColumn dc;
            DataRow drddd;
            DataSet itemDs = new DataSet();
            BusinessLogic bl = new BusinessLogic(GetConnectionString());

            DataSet ds = bl.ListUnusedLeaf(GetConnectionString());

            dc = new DataColumn("ChequeNo");
            dtf.Columns.Add(dc);

            dc = new DataColumn("Bank");
            dtf.Columns.Add(dc);

            itemDs.Tables.Add(dtf);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ChequeNo"));
            dt.Columns.Add(new DataColumn("Bank"));

            if (ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["FromChequeNo"].ToString().ToUpper().Trim();
                    tLvlValueTemp = dr["ToChequeNo"].ToString().ToUpper().Trim();

                    int difff = Convert.ToInt32(tLvlValueTemp) - Convert.ToInt32(fLvlValueTemp);
                    int g = 0;
                    int ChequeNo = 0;

                    for (int k = 0; k <= difff; k++)
                    {
                        if (g == 0)
                        {
                            drddd = itemDs.Tables[0].NewRow();
                            DataRow dr_final8 = dt.NewRow();
                            dr_final8["ChequeNo"] = fLvlValueTemp;
                            drddd["ChequeNo"] = Convert.ToString(fLvlValueTemp);
                            dr_final8["Bank"] = dr["BankName"];
                            drddd["Bank"] = dr["BankName"];
                            dt.Rows.Add(dr_final8);
                            ChequeNo = Convert.ToInt32(fLvlValueTemp) + 1;
                            itemDs.Tables[0].Rows.Add(drddd);
                        }
                        else
                        {
                            drddd = itemDs.Tables[0].NewRow();
                            DataRow dr_final8 = dt.NewRow();
                            dr_final8["ChequeNo"] = Convert.ToString(ChequeNo);
                            drddd["ChequeNo"] = Convert.ToString(ChequeNo);
                            dr_final8["Bank"] = dr["BankName"];
                            drddd["Bank"] = dr["BankName"];
                            dt.Rows.Add(dr_final8);
                            ChequeNo = ChequeNo + 1;
                            itemDs.Tables[0].Rows.Add(drddd);
                            g = 1;
                        }
                        g = 1;
                    }
                }
            }

            DataSet dsd = bl.ListusedLeaf(GetConnectionString());

            if (dsd != null)
            {
                if (dsd.Tables[0] != null)
                {
                    DataTable dtttt = dsd.Tables[0];

                    if (itemDs.Tables[0] != null)
                    {
                        foreach (DataRow drd in dsd.Tables[0].Rows)
                        {
                            var billNo = Convert.ToUInt32(drd["ChequeNo"]);

                            for (int i = 0; i < itemDs.Tables[0].Rows.Count; i++)
                            {
                                if (billNo == Convert.ToUInt32(itemDs.Tables[0].Rows[i]["ChequeNo"]))
                                {
                                    itemDs.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                        itemDs.Tables[0].AcceptChanges();
                    }
                }
            }

            DataSet dsddd = bl.ListDamageChequeInfo(GetConnectionString(), "", "");

            if (dsddd != null)
            {
                if (dsddd.Tables[0] != null)
                {
                    DataTable dttttt = dsddd.Tables[0];

                    if (itemDs.Tables[0] != null)
                    {
                        foreach (DataRow drdddd in dsddd.Tables[0].Rows)
                        {
                            var billNo = Convert.ToUInt32(drdddd["ChequeNo"]);

                            for (int i = 0; i < itemDs.Tables[0].Rows.Count; i++)
                            {
                                //var billNoll = Convert.ToUInt32(itemDs.Tables[0].Rows[i]["ChequeNo"]);

                                if (billNo == Convert.ToUInt32(itemDs.Tables[0].Rows[i]["ChequeNo"]))
                                {
                                    itemDs.Tables[0].Rows[i].Delete();
                                }
                            }
                            itemDs.Tables[0].AcceptChanges();
                        }
                        itemDs.Tables[0].AcceptChanges();
                    }
                }
            }

            if (itemDs != null)
            {
                if (itemDs.Tables[0] != null)
                {
                    DataTable dtt = itemDs.Tables[0];

                    if (dtt.Rows.Count > 0)
                    {
                        System.IO.StringWriter tw = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                        DataGrid dgGrid = new DataGrid();
                        dgGrid.DataSource = dtt;
                        dgGrid.DataBind();

                        //Get the HTML for the control.
                        dgGrid.RenderControl(hw);

                        //Write the HTML back to the browser.
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                        this.EnableViewState = false;
                        Response.Write(tw.ToString());
                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void Reset()
    {
        txtBranchAddress2.Text = "";
        txtBranchLocation.Text = "";
        txtBranchAddress1.Text = "";
        txtBranchcode.Text = "";
        txtBranchcode.Enabled = true;
        txtBranchName.Enabled = true;
        txtBranchName.Text = "";
        txtBranchAddress3.Text = "";
        Txtmobile1.Text = "";
        Txtmobile2.Text = "";
        Txtemailid.Text = "";
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

            Session["conDs"] = "Close";
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
                BusinessLogic bl = new BusinessLogic(GetConnectionString());
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                //if (bl.ChequeLeafUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                //{
                //    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;

                //    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                //}
                if (bl.CheckUserHaveEdit(usernam, "BRNCHMAS"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

              //  string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckPriceListUsed(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PriceName"))))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                if (!bl.CheckPriceListUsed(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PriceName"))))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                string rate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PriceName"));
                if (rate == "MRP")
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                    //((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    //((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
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

    protected void UpdButton_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Dsd"] = "DSD";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewSerVisit_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GrdViewSerVisit.SelectedRow;
        try
        {

            int BranchId = Convert.ToInt32(GrdViewSerVisit.SelectedDataKey.Value);

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            DataSet ds = bl.GetBranchForId(sDataSource, BranchId);

            if (ds != null)
            {
                hdVisitID.Value = Convert.ToString(BranchId);

                txtBranchName.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                txtBranchcode.Text = ds.Tables[0].Rows[0]["Branchcode"].ToString();
                txtBranchAddress1.Text = ds.Tables[0].Rows[0]["BranchAddress1"].ToString();
                txtBranchAddress2.Text = ds.Tables[0].Rows[0]["BranchAddress2"].ToString();
                txtBranchAddress3.Text = ds.Tables[0].Rows[0]["BranchAddress3"].ToString();
                txtBranchLocation.Text = ds.Tables[0].Rows[0]["BranchLocation"].ToString();
                Txtmobile1.Text = ds.Tables[0].Rows[0]["mobile1"].ToString();
                Txtmobile2.Text = ds.Tables[0].Rows[0]["mobile2"].ToString();
                Txtemailid.Text = ds.Tables[0].Rows[0]["Emailid"].ToString();
                drpIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();

                txtBranchcode.Enabled = false;
                txtBranchName.Enabled = false;

                Title1.Text = "Edit Branch ";
               
                UpdateButton.Visible = true;
                SaveButton.Visible = false;
               
                CancelButton.Visible = true;
                lnkBtnAdd.Visible = false;
                //MyAccordion.Visible = false;

                GrdViewSerVisit.Visible = false;
                pnlVisitDetails.Visible = true;

                ModalPopupExtender1.Show();
                Session["Dsd"] = "D";
                
                
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
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
            
            //btnSearchService.Enabled = true;
            //drpCustomer.Enabled = true;
            //bankPanel.Update();
            //pnlBank.Visible = false;
            ModalPopupExtender1.Show();
            pnlVisitDetails.Visible = true;
            Reset();
           
            Title1.Text = "New Branch";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void GrdViewSerVisit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
 
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
            if (GrdViewSerVisit.SelectedDataKey != null)
                e.InputParameters["ID"] = GrdViewSerVisit.SelectedDataKey.Value;

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

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
            //Page.Form.Enctype = "multipart/form-data";

            if (Page.IsValid)
            {
                string BranchAddress1 = string.Empty;
                    string Branchcode = string.Empty;
                    string BranchAddress2 = string.Empty;
                    string BranchAddress3 = string.Empty;
                    string BranchLocation = string.Empty;
                    string txtmobile1 = string.Empty;
                    string txtmobile2 = string.Empty;
                    string txtemailid = string.Empty;
                    string IsActive = string.Empty;

                    string Username = Request.Cookies["LoggedUserName"].Value;

                    Branchcode = txtBranchcode.Text.Trim();
                    string BranchName = string.Empty;
                    BranchName = txtBranchName.Text.Trim();
                    BranchAddress1 = txtBranchAddress1.Text.Trim();
                    BranchAddress2 = txtBranchAddress2.Text.Trim();
                    BranchAddress3 = txtBranchAddress3.Text.Trim();
                    BranchLocation = txtBranchLocation.Text.Trim();
                    txtmobile1 = Txtmobile1.Text.Trim();
                    txtmobile2 = Txtmobile2.Text.Trim();
                    txtemailid = Txtemailid.Text.Trim();

                    IsActive = drpIsActive.SelectedValue;

                    BusinessLogic bl = new BusinessLogic(sDataSource);
                    string connection = Request.Cookies["Company"].Value;

                    if (bl.CheckIfBranchDuplicate(connection, BranchName,"BranchName"))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Branch - " + BranchName + " - already exists.');", true);
                        ModalPopupExtender1.Show();
                        return;
                    }

                    if (bl.CheckIfBranchDuplicate(connection, Branchcode, "Branchcode"))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Branch - " + Branchcode + " - already exists.');", true);
                        ModalPopupExtender1.Show();
                        return;
                    }

                    DataSet dstest = new DataSet();
                   
                    DataTable dtt;
                    DataRow drNewt;
                    DataColumn dct;

                    dtt = new DataTable();

                    dct = new DataColumn("LedgerName");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("AliasName");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("GroupID");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("ContactName");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("Inttrans");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("Branchcode");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("Add1");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("Add2");
                    dtt.Columns.Add(dct);

                    dct = new DataColumn("Add3");
                    dtt.Columns.Add(dct);

                    dstest.Tables.Add(dtt);


                    for (int i = 1; i <= 5; i++)
                    {
                        drNewt = dtt.NewRow();

                        if (i == 1)
                        {
                            drNewt["GroupID"] = 4;
                            drNewt["LedgerName"] = "Cash A/c - " + Branchcode;
                            drNewt["AliasName"] = "Cash A/c - " + Branchcode;
                            drNewt["ContactName"] = "Cash A/c - " + Branchcode;
                            drNewt["Inttrans"] = "NO";
                            drNewt["Branchcode"] = Branchcode;
                            drNewt["Add1"] = "";
                            drNewt["Add2"] = "";
                            drNewt["Add3"] = "";
                        }
                        else if (i == 2)
                        {
                            drNewt["GroupID"] = 6;
                            drNewt["LedgerName"] = "Sales A/c - " + Branchcode;
                            drNewt["AliasName"] = "Sales A/c - " + Branchcode;
                            drNewt["ContactName"] = "Sales A/c - " + Branchcode;
                            drNewt["Inttrans"] = "NO";
                            drNewt["Branchcode"] = Branchcode;
                            drNewt["Add1"] = "";
                            drNewt["Add2"] = "";
                            drNewt["Add3"] = "";
                        }
                        else if (i == 3)
                        {
                            drNewt["GroupID"] = 5;
                            drNewt["LedgerName"] = "Purchase A/c - " + Branchcode;
                            drNewt["AliasName"] = "Purchase A/c - " + Branchcode;
                            drNewt["ContactName"] = "Purchase A/c - " + Branchcode;
                            drNewt["Inttrans"] = "NO";
                            drNewt["Branchcode"] = Branchcode;
                            drNewt["Add1"] = "";
                            drNewt["Add2"] = "";
                            drNewt["Add3"] = "";
                        }
                        else if (i == 4)
                        {
                            drNewt["GroupID"] = 1;
                            drNewt["LedgerName"] = BranchName;
                            drNewt["AliasName"] = BranchName;
                            drNewt["ContactName"] = BranchName;
                            drNewt["Inttrans"] = "YES";
                            drNewt["Branchcode"] = Branchcode;
                            drNewt["Add1"] = BranchAddress1;
                            drNewt["Add2"] = BranchAddress2;
                            drNewt["Add3"] = BranchAddress3;
                        }
                        else if (i == 5)
                        {
                            drNewt["GroupID"] = 2;
                            drNewt["LedgerName"] = BranchName;
                            drNewt["AliasName"] = BranchName;
                            drNewt["ContactName"] = BranchName;
                            drNewt["Inttrans"] = "YES";
                            drNewt["Branchcode"] = Branchcode;
                            drNewt["Add1"] = BranchAddress1;
                            drNewt["Add2"] = BranchAddress2;
                            drNewt["Add3"] = BranchAddress3;
                        }
                        dstest.Tables[0].Rows.Add(drNewt);
                    }




                    DataSet dstesttt = new DataSet();

                    DataTable dtttt;
                    DataRow drNewttt;
                    DataColumn dcttt;

                    dtttt = new DataTable();

                    dcttt = new DataColumn("LedgerName");
                    dtttt.Columns.Add(dcttt);

                    dcttt = new DataColumn("AliasName");
                    dtttt.Columns.Add(dcttt);

                    dcttt = new DataColumn("GroupID");
                    dtttt.Columns.Add(dcttt);

                    dcttt = new DataColumn("ContactName");
                    dtttt.Columns.Add(dcttt);

                    dcttt = new DataColumn("Inttrans");
                    dtttt.Columns.Add(dcttt);

                    dcttt = new DataColumn("Branchcode");
                    dtttt.Columns.Add(dcttt);

                    dcttt = new DataColumn("Add1");
                    dtttt.Columns.Add(dcttt);

                    dcttt = new DataColumn("Add2");
                    dtttt.Columns.Add(dcttt);

                    dcttt = new DataColumn("Add3");
                    dtttt.Columns.Add(dcttt);

                    dstesttt.Tables.Add(dtttt);

                    DataSet dste = bl.ListExpenseInfo(connection, "", "", "");

                    if (dste != null)
                    {
                        if (dste.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dste.Tables[0].Rows)
                            {
                                drNewttt = dtttt.NewRow();
                                drNewttt["GroupID"] = 8;
                                drNewttt["LedgerName"] = dr["Expensehead"].ToString() + " - " + Branchcode;
                                drNewttt["AliasName"] = dr["Expensehead"].ToString() + " - " + Branchcode;
                                drNewttt["ContactName"] = dr["Expensehead"].ToString() + " - " + Branchcode;
                                drNewttt["Inttrans"] = "NO";
                                drNewttt["Branchcode"] = Branchcode;
                                drNewttt["Add1"] = "";
                                drNewttt["Add2"] = "";
                                drNewttt["Add3"] = "";

                                dstesttt.Tables[0].Rows.Add(drNewttt);
                            }
                        }
                    }






                    DataSet dstestt = new DataSet();

                    DataSet dst = bl.ListProducts(connection, "", "");
                    if (dst != null)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            

                            DataTable dttt;
                            DataRow drNewtt;
                            DataColumn dctt;

                            dttt = new DataTable();

                            dctt = new DataColumn("CategoryID");
                            dttt.Columns.Add(dctt);

                            dctt = new DataColumn("BranchName");
                            dttt.Columns.Add(dctt);

                            dctt = new DataColumn("Branchcode");
                            dttt.Columns.Add(dctt);

                            dctt = new DataColumn("ProductName");
                            dttt.Columns.Add(dctt);

                            dctt = new DataColumn("Stock");
                            dttt.Columns.Add(dctt);

                            dctt = new DataColumn("ProductDesc");
                            dttt.Columns.Add(dctt);

                            dctt = new DataColumn("Model");
                            dttt.Columns.Add(dctt);

                            dctt = new DataColumn("ItemCode");
                            dttt.Columns.Add(dctt);

                            dstestt.Tables.Add(dttt);

                            foreach (DataRow dr in dst.Tables[0].Rows)
                            {
                                drNewtt = dttt.NewRow();
                                drNewtt["CategoryID"] = Convert.ToInt32(dr["CategoryID"]);
                                drNewtt["BranchName"] = BranchName;
                                drNewtt["Branchcode"] = Branchcode;
                                drNewtt["Stock"] = "0";
                                drNewtt["ProductName"] = Convert.ToString(dr["ProductName"]);
                                drNewtt["ItemCode"] = Convert.ToString(dr["ItemCode"]);
                                drNewtt["Model"] = Convert.ToString(dr["Model"]);
                                drNewtt["ProductDesc"] = Convert.ToString(dr["ProductDesc"]);
                                dstestt.Tables[0].Rows.Add(drNewtt);
                            }
                        }
                    }

                    bl.InsertBranch(connection, Branchcode, BranchName, BranchAddress1, BranchAddress2, BranchAddress3, BranchLocation,txtmobile1,txtmobile2,txtemailid,IsActive, Username, dstest, dstestt, dstesttt);
                       
                    //MyAccordion.Visible = true;
                    pnlVisitDetails.Visible = false;
                    lnkBtnAdd.Visible = true;
                    Reset();
                    GrdViewSerVisit.DataBind();
                    GrdViewSerVisit.Visible = true;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Branch Details Saved Successfully.');", true);
                    return;
                
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please search a product and edit the prices');", true);
        //return;
        Response.Redirect("ProdMaster.aspx");
        
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            bindData();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }


    public void bindData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable("NewPrices");

        dt.Columns.Add(new DataColumn("ITEMCODE"));
        dt.Columns.Add(new DataColumn("PRICE"));
        dt.Columns.Add(new DataColumn("EFFECTIVEDATE"));
        dt.Columns.Add(new DataColumn("DISCOUNT"));
        double DISCOUNT = 0;

        DataRow dr_final12 = dt.NewRow();
        dr_final12["ITEMCODE"] = "";
        dr_final12["PRICE"] = "";
        dr_final12["EFFECTIVEDATE"] = "";
        dr_final12["DISCOUNT"] = DISCOUNT;
        dt.Rows.Add(dr_final12);

        ExportToExcel(dt);
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            //string filename = "Sales Report.xls";
            //string filename = "NewPrices _" + DateTime.Now.ToString() + ".xls";
            //System.IO.StringWriter tw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            //DataGrid dgGrid = new DataGrid();
            //dgGrid.DataSource = dt;
            //dgGrid.DataBind();
            ////dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            ////dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            ////dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
            //dgGrid.HeaderStyle.Font.Bold = true;
            ////Get the HTML for the control.
            //dgGrid.RenderControl(hw);
            ////Write the HTML back to the browser.
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            //this.EnableViewState = false;
            //Response.Write(tw.ToString());
            //Response.End();

            using (XLWorkbook wb = new XLWorkbook())
            {
                string filename = "NewPrices.xlsx";
                wb.Worksheets.Add(dt);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + "");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            //Page.Form.Enctype = "multipart/form-data";

            if (Page.IsValid)
            {

                if (Session["conDs"] == "Add")
                {
                    //if (FileUpload1.HasFile == false)
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Browse Excel before uploading.');", true);
                    //    ModalPopupExtender1.Show();
                    //    return;
                    //}

                    //string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                    //int CustomerID = 0;
                    //int ServiceID = 0;
                    //DateTime DueDate;
                    //DateTime VisitDate;
                    //string AccountNo = string.Empty;
                    //double Amount = 0.0;
                    //int PayMode;
                    //string PriceName = string.Empty;

                    //string Username = Request.Cookies["LoggedUserName"].Value;

                    //string Types = "New";
                    //PriceName = txtPriceList.Text.Trim();
                    //string Description = string.Empty;

                    //Description = txtDescription.Text.Trim();
                    //BusinessLogic bl = new BusinessLogic(sDataSource);



                    //if (bl.IsChequeAlreadyEntered(connection, BankID, FromNo, ToNo))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given Cheque No already entered for this bank.');", true);
                    //    ModalPopupExtender1.Show();
                    //    return;
                    //}

                    //if(bl.IsChequeNoNotLess(connection, BankID, FromNo, ToNo))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToCheque No Cannot be Less than FromChequeNo');", true);
                    //    return;
                    //}

                    //if (Convert.ToDouble(txtFromNoAdd.Text) > Convert.ToDouble(txtToNoAdd.Text))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToCheque No Cannot be Less than FromChequeNo');", true);
                    //    ModalPopupExtender1.Show();
                    //    return;
                    //}

                    //if (Convert.ToDouble(txtFromNoAdd.Text) == Convert.ToDouble(txtToNoAdd.Text))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('FromChequeNo Cannot be equal to ToCheque');", true);
                    //    ModalPopupExtender1.Show();
                    //    return;
                    //}

                    //string connection = Request.Cookies["Company"].Value;

                    //String strConnection = "ConnectionString";
                    //string connectionString = "";
                    //if (FileUpload1.HasFile)
                    //{
                    //    string datett = DateTime.Now.ToString();
                    //    string dtaa = Convert.ToDateTime(datett).ToString("dd-MM-yyyy-hh-mm-ss");
                    //    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName) + dtaa;
                    //    string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    //    string fileLocation = Server.MapPath("~/App_Data/" + fileName);
                    //    FileUpload1.SaveAs(fileLocation);
                    //    if (fileExtension == ".xls")
                    //    {
                    //        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                    //            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    //    }
                    //    else if (fileExtension == ".xlsx")
                    //    {
                    //        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    //            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                    //        //OleDbConnection Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";");
                    //    }
                    //    OleDbConnection con = new OleDbConnection(connectionString);
                    //    OleDbCommand cmd = new OleDbCommand();
                    //    cmd.CommandType = System.Data.CommandType.Text;
                    //    cmd.Connection = con;
                    //    OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                    //    DataTable dtExcelRecords = new DataTable();
                    //    con.Open();
                    //    DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    //    string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                    //    cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                    //    dAdapter.SelectCommand = cmd;
                    //    dAdapter.Fill(dtExcelRecords);
                    //    DataSet ds = new DataSet();
                    //    ds.Tables.Add(dtExcelRecords);

                    //    string usernam = Request.Cookies["LoggedUserName"].Value;
                    //    BusinessLogic objBL = new BusinessLogic();
                    //    objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());


                    //    //if(Convert.ToInt32(drpPriceList.SelectedIndex) == 0)
                    //    //{
                    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select any one Price List before upload');", true);
                    //    //    return;
                    //    //}

                    //    if (ds == null)
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Uploading Excel is Empty');", true);
                    //        ModalPopupExtender1.Show();
                    //        return;
                    //    }


                    //    foreach (DataRow dr in ds.Tables[0].Rows)
                    //    {
                    //        if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ItemCode Missing');", true);
                    //            ModalPopupExtender1.Show();
                    //            return;
                    //        }
                    //    }

                    //    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //    //{
                    //    //    string brand = Convert.ToString(dr["brand"]);
                    //    //    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    //    //    {

                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        if (!objBL.CheckIfbrandIsThere(brand))
                    //    //        {
                    //    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Brand with - " + brand + " - does not exists in the Brand Master');", true);
                    //    //            return;
                    //    //        }
                    //    //    }
                    //    //}


                    //    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //    //{
                    //    //    string category = Convert.ToString(dr["category"]);

                    //    //    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    //    //    {

                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        if (!objBL.CheckIfcategoryIsThere(category))
                    //    //        {
                    //    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Category with - " + category + " - does not exists in the Category Master');", true);
                    //    //            return;
                    //    //        }
                    //    //    }
                    //    //}

                    //    foreach (DataRow dr in ds.Tables[0].Rows)
                    //    {
                    //        string item = Convert.ToString(dr["ItemCode"]);

                    //        if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    //        {

                    //        }
                    //        else
                    //        {
                    //            if (!objBL.CheckIfItemCodeDuplicate(item))
                    //            {
                    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Code - " + item + " - does not exists in the Product master.');", true);
                    //                ModalPopupExtender1.Show();
                    //                return;
                    //            }
                    //        }
                    //    }

                    //    foreach (DataRow dr in ds.Tables[0].Rows)
                    //    {
                    //        string item = Convert.ToString(dr["ItemCode"]);
                    //        if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    //        {

                    //        }
                    //        else
                    //        {
                    //            if (objBL.CheckIfItemCodeDuplicatePriceList1(item, PriceName))
                    //            {
                    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product code - " + item + " - already exists in the price list.');", true);
                    //                ModalPopupExtender1.Show();
                    //                return;
                    //            }
                    //        }
                    //    }


                    //    int i = 1;
                    //    int ii = 1;
                    //    string itemc = string.Empty;
                    //    foreach (DataRow dr in ds.Tables[0].Rows)
                    //    {
                    //        itemc = Convert.ToString(dr["ItemCode"]);

                    //        if ((itemc == null) || (itemc == ""))
                    //        {
                    //        }
                    //        else
                    //        {
                    //            foreach (DataRow drd in ds.Tables[0].Rows)
                    //            {
                    //                if (ii == i)
                    //                {
                    //                }
                    //                else
                    //                {
                    //                    if (itemc == Convert.ToString(drd["ItemCode"]))
                    //                    {
                    //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product code - " + itemc + " - already exists in the excel.');", true);
                    //                        ModalPopupExtender1.Show();
                    //                        return;
                    //                    }
                    //                }
                    //                ii = ii + 1;
                    //            }
                    //        }
                    //        i = i + 1;
                    //        ii = 1;
                    //    }

                    //    foreach (DataRow dr in ds.Tables[0].Rows)
                    //    {
                    //        string Model = Convert.ToString(dr["ItemCode"]);
                    //        if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    //        {

                    //        }
                    //        else
                    //        {
                    //            if ((Convert.ToString(dr["PRICE"]) == null) || (Convert.ToString(dr["PRICE"]) == "") || (Convert.ToString(dr["EFFECTIVEDATE"]) == null) || (Convert.ToString(dr["EFFECTIVEDATE"]) == "") || (Convert.ToString(dr["DISCOUNT"]) == null) || (Convert.ToString(dr["DISCOUNT"]) == ""))
                    //            {
                    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill the empty in the excel sheet');", true);
                    //                ModalPopupExtender1.Show();
                    //                return;
                    //            }
                    //        }
                    //    }





                    //    try
                    //    {                            

                    //        DataSet dsd = bl.GetPriceListForName(connection, PriceName);

                    //        if (dsd != null)
                    //        {
                    //            if (dsd.Tables[0].Rows.Count > 0)
                    //            {
                    //                foreach (DataRow dr in dsd.Tables[0].Rows)
                    //                {
                    //                    bl.InsertBulkProductPrices(connection, ds, usernam, Convert.ToString(dr["PriceName"]), Convert.ToInt32(dr["Id"]));
                    //                }
                    //            }
                    //        }



                    //        //MyAccordion.Visible = true;
                    //        pnlVisitDetails.Visible = false;
                    //        lnkBtnAdd.Visible = true;
                    //        Reset();
                    //        GrdViewSerVisit.DataBind();
                    //        GrdViewSerVisit.Visible = true;

                    //        Session["conDs"] = "Close";

                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Price List Details Saved Successfully.');", true);
                    //        return;
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured: " + ex.Message + "')", true);
                    //    }
                    //    con.Close();
                    //}
                }
            }

            //MyAccordion.Visible = true;
            pnlVisitDetails.Visible = false;
            lnkBtnAdd.Visible = true;
            Reset();
            GrdViewSerVisit.DataBind();
            GrdViewSerVisit.Visible = true;

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
