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

public partial class ManualSalesBook : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

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

                //loadBanks(connStr);
                //loadCustomerDealers(connStr);

                //if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                //{
                //    lnkBtnAdd.Visible = false;
                //    GrdViewManualBook.Columns[7].Visible = false;
                //}

                //myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

                gridViewUnUsedLeafs.PageSize = 8;


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                //if (bl.CheckUserHaveAdd(usernam, "CHQMST"))
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
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }



    protected void btnCancelUnused_Click(object sender, EventArgs e)
    {
        try
        {
            
            ModalPopupUnused.Hide();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnCancelDamangedLeaf_Click(object sender, EventArgs e)
    {
        try
        {
            divDamangedLeaf.Visible = false;
            ModalPopupAddDamaged.Hide();
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
            if (Page.IsValid)
            {
                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                string BookName = string.Empty;
                
                GridViewRow row = GrdViewManualBook.SelectedRow;
                
                int BookId = Convert.ToInt32(GrdViewManualBook.SelectedDataKey.Value);

                string BankName = string.Empty;
                string FromNo = string.Empty;
                string ToNo = string.Empty;
                
                string Username = Request.Cookies["LoggedUserName"].Value;
                BookName = txtBookNameAdd.Text;
                string Types = "Update";

                FromNo = txtFromNoAdd.Text;
                ToNo = txtBookToAdd.Text;
                
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if(row.Cells[1].Text.ToUpper().Trim() != BookName.ToUpper().Trim()){
                
                    if (bl.IsManualSaleBookAlreadyEntered(connection, BookName, FromNo, ToNo))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given Book No already entered with the another book having the same name.');", true);
                        ModalPopupExtender1.Show();
                        return;
                    }
                }

                //if(bl.IsChequeNoNotLess(connection, BankID, FromNo, ToNo))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToCheque No Cannot be Less than FromChequeNo');", true);
                //    return;
                //}

                if (Convert.ToDouble(txtFromNoAdd.Text) > Convert.ToDouble(txtBookToAdd.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToBook No Cannot be Less than From BookNo');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                if (Convert.ToDouble(txtFromNoAdd.Text) == Convert.ToDouble(txtBookToAdd.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('FromBook No Cannot be equal to ToBook');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                try
                {
                    bl.UpdateManualSalesBook(connection, BookId, BookName, int.Parse( FromNo), int.Parse( ToNo), Username, Types);


                    //MyAccordion.Visible = true;
                    pnlVisitDetails.Visible = false;
                    lnkBtnAdd.Visible = true;
                    Reset();
                    GrdViewManualBook.DataBind();
                    GrdViewManualBook.Visible = true;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Manual Book Details Updated Successfully.');", true);
                    return;
                }
                catch (Exception ex)
                {
                    TroyLiteExceptionManager.HandleException(ex);
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
        //drpCustomer.DataSource = ds;
        //drpCustomer.DataBind();
        //drpCustomer.DataTextField = "LedgerName";
        //drpCustomer.DataValueField = "LedgerID";
    }

    //private void loadBanks(string sDataSource)
    //{
    //    //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    DataSet ds = new DataSet();
    //    ds = bl.ListBanks();
    //    ddBankName.DataSource = ds;
    //    ddBankName.DataBind();
    //    ddBankName.DataTextField = "LedgerName";
    //    ddBankName.DataValueField = "LedgerID";
    //}

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
        txtFromNoAdd.Text = "";
        txtBookNameAdd.Text = "";
        txtBookToAdd.Text = "";
        //txtDueDate.Text = "";
        //txtRefNo.Text = "";
        //txtVisitDate.Text = "";
        //drpPaymode.SelectedIndex = 0;
        //drpCustomer.SelectedIndex = 0;
        //ddBankName.SelectedIndex = 0;

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //MyAccordion.Visible = true;
            pnlVisitDetails.Visible = false;
            lnkBtnAdd.Visible = true;
            Reset();
            GrdViewManualBook.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewManualBook_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(GetConnectionString());
                string connection = Request.Cookies["Company"].Value;

                //if (bl.ChequeLeafUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                //{
                //    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;

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

    protected void GrdViewManualBook_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void GrdViewManualBook_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GrdViewManualBook.SelectedIndex = e.RowIndex;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewManualBook_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridViewRow row = GrdViewManualBook.SelectedRow;
        //try
        //{

        //    int BookId = Convert.ToInt32(GrdViewManualBook.SelectedDataKey.Value);

        //    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        //    BusinessLogic bl = new BusinessLogic(sDataSource);

        //    DataSet ds = bl.GetManualSaleBookInfoForId(sDataSource, BookId);

        //    if (ds != null)
        //    {
        //        hdVisitID.Value = Convert.ToString(BookId);

        //        txtBookNameAdd.Text = ds.Tables[0].Rows[0]["BookName"].ToString();
        //        txtFromNoAdd.Text = ds.Tables[0].Rows[0]["BookFrom"].ToString();
        //        txtBookToAdd.Text = ds.Tables[0].Rows[0]["BookTo"].ToString();

        //        UpdateButton.Visible = true;
        //        SaveButton.Visible = false;
        //        CancelButton.Visible = true;
        //        lnkBtnAdd.Visible = false;
        //        //MyAccordion.Visible = false;

        //        GrdViewManualBook.Visible = false;
        //        pnlVisitDetails.Visible = true;

        //        ModalPopupExtender1.Show();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    TroyLiteExceptionManager.HandleException(ex);
        //}
    }

    protected void SaveDamangedLeafButton_Click(object sender, EventArgs e)
    {
        string connection = Request.Cookies["Company"].Value;
        string UserID = Request.Cookies["LoggedUserName"].Value;
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        int BookId = int.Parse(hdBookID.Value);
        int bookStart = 0;
        int bookEnd = 0;
        int damangedLeaf = int.Parse(txtDamagedLeaf.Text);

        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet ds = bl.GetManualSaleBookInfoForId(sDataSource, BookId);

        bookStart = int.Parse( ds.Tables[0].Rows[0]["BookFrom"].ToString());
        bookEnd = int.Parse( ds.Tables[0].Rows[0]["BookTo"].ToString());

        if (damangedLeaf >= bookStart && damangedLeaf <= bookEnd)
        {
            if (!bl.IsDamangedLeafAlreadyEntered(sDataSource, BookId, damangedLeaf))
            {
                bl.InsertManualSalesLeaf(sDataSource, BookId, damangedLeaf, txtComments.Text, UserID);

                divDamangedLeaf.Visible = false;
                txtDamagedLeaf.Text = string.Empty;
                txtComments.Text = string.Empty;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Leaf Details Saved Successfully.');", true);
            }
            else
            {
                divDamangedLeaf.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Leaf Details already added for this book.');", true);
            }
        }
        else
        {
            divDamangedLeaf.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Incorrect Leaf Details, please check the leaf no and try again.');", true);
            ModalPopupAddDamaged.Show();
        }

    }

    protected void GrdViewManualBook_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (((System.Web.UI.WebControls.Image)(e.CommandSource as ImageButton)).ID == "btnShowUnusedLeafs")
        {
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = row.RowIndex;

            int BookId = Convert.ToInt32(GrdViewManualBook.DataKeys[rowIndex].Value.ToString());

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            DataSet ds = bl.ListManualSalesBookInfo(sDataSource, BookId);
            gridViewUnUsedLeafs.DataSource = ds;
            gridViewUnUsedLeafs.DataBind();

            ModalPopupUnused.Show();

        }
        else if (((System.Web.UI.WebControls.Image)(e.CommandSource as ImageButton)).ID == "btnAddDamagedLeaf")
        {
            //frmViewAdd.Visible = true;
            //frmViewAdd.ChangeMode(FormViewMode.Edit);
            //GrdViewBranches.Columns[8].Visible = false;
            //lnkBtnAdd.Visible = false;
            divDamangedLeaf.Visible = true;
            ModalPopupAddDamaged.Show();
            //ModalPopupExtender1.Hide();
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = row.RowIndex;

            hdBookID.Value = GrdViewManualBook.DataKeys[rowIndex].Value.ToString();
            //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
            //    Accordion1.SelectedIndex = 1;
        }
        else if (((System.Web.UI.WebControls.Image)(e.CommandSource as ImageButton)).ID == "btnEdit")
        {
            GridViewRow row = GrdViewManualBook.SelectedRow;
            
            try
            {

                int BookId = Convert.ToInt32(GrdViewManualBook.SelectedDataKey.Value);

                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                BusinessLogic bl = new BusinessLogic(sDataSource);

                DataSet ds = bl.GetManualSaleBookInfoForId(sDataSource, BookId);

                if (ds != null)
                {
                    hdVisitID.Value = Convert.ToString(BookId);

                    txtBookNameAdd.Text = ds.Tables[0].Rows[0]["BookName"].ToString();
                    txtFromNoAdd.Text = ds.Tables[0].Rows[0]["BookFrom"].ToString();
                    txtBookToAdd.Text = ds.Tables[0].Rows[0]["BookTo"].ToString();

                    UpdateButton.Visible = true;
                    SaveButton.Visible = false;
                    CancelButton.Visible = true;
                    lnkBtnAdd.Visible = false;
                    //MyAccordion.Visible = false;

                    GrdViewManualBook.Visible = false;
                    pnlVisitDetails.Visible = true;

                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                TroyLiteExceptionManager.HandleException(ex);
            }
        }
    }

    protected void GrdViewManualBook_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewManualBook, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gridViewUnUsedLeafs_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(gridViewUnUsedLeafs, e.Row, this);
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
            if (GrdViewManualBook.SelectedDataKey != null)
                e.InputParameters["BookId"] = GrdViewManualBook.SelectedDataKey.Value;

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

            e.InputParameters["Types"] = "Delete";
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
                string AccountNo = string.Empty;
                string BookName = string.Empty;
                string FromNo = string.Empty;
                string ToNo = string.Empty;
                string BankID = string.Empty;

                string Username = Request.Cookies["LoggedUserName"].Value;
                //AccountNo = txtAccNoAdd.Text;
                string Types = "New";

                FromNo = txtFromNoAdd.Text.Trim();
                ToNo = txtBookToAdd.Text.Trim();
                BookName = txtBookNameAdd.Text.Trim();

                int ddBankID = 0;
                //ddBankID = int.Parse(ddBankName.SelectedValue);

                //BankName = ddBankName.SelectedItem.Text;

                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.IsManualSaleBookAlreadyEntered(connection, BookName, FromNo, ToNo))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given Book No already entered with the another book having the same name');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                //if(bl.IsChequeNoNotLess(connection, BankID, FromNo, ToNo))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToCheque No Cannot be Less than FromChequeNo');", true);
                //    return;
                //}

                if (Convert.ToDouble(txtFromNoAdd.Text) > Convert.ToDouble(txtBookToAdd.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('To Book No Cannot be Less than From Book No');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                if (Convert.ToDouble(txtFromNoAdd.Text) == Convert.ToDouble(txtBookToAdd.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('From Book No Cannot be equal to To Book');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                try
                {
                    bl.InsertManualSalesBook(connection, int.Parse( FromNo), int.Parse( ToNo), BookName, Username, Types);
                    
                    //MyAccordion.Visible = true;
                    pnlVisitDetails.Visible = false;
                    lnkBtnAdd.Visible = true;
                    Reset();
                    GrdViewManualBook.DataBind();
                    GrdViewManualBook.Visible = true;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Manual Sales Book Details Saved Successfully.');", true);
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