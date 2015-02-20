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

public partial class HirePurchaseNew : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
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
                string connStr = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                loadCustomerDealers(connStr);
                loadSupplier();

                LoadProducts(this, null);
                loadCategories();

                //UpdatePanel16.Update();
                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    GrdViewSerVisit.Columns[7].Visible = false;
                }

                //myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

                GrdViewSerVisit.PageSize = 8;

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "HIPUR"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New ";
                }
                mrBillDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                mrBillDate.MaximumValue = System.DateTime.Now.ToShortDateString();

                //mrduedate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //mrduedate.MaximumValue = System.DateTime.Now.ToShortDateString();

                txtBillDate.Text = DateTime.Now.ToShortDateString();

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

                GridViewRow row = GrdViewSerVisit.SelectedRow;

                int BillNo = Convert.ToInt32(GrdViewSerVisit.SelectedDataKey.Value);

                string Username = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                double dlnAmt = 0;
                double dpurAmt = 0;
                double ddochr = 0;
                double dintamt = 0;
                double dfinpay = 0;
                double dnoinst = 0;
                double dpay = 0;
                double deachpay = 0;
                string txtoth = string.Empty;

                string sBilldate = string.Empty;
                string dpaydate = string.Empty;
                string dstartdate = string.Empty;
                string sCustomerName = string.Empty;
                int sCustomerID = 0;

                sBilldate = txtBillDate.Text.Trim();
                dlnAmt = Convert.ToDouble(Txtlnamt.Text);
                sCustomerName = cmbCustomer.SelectedItem.Text;
                sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                dpurAmt = Convert.ToDouble(txtpuramt.Text);
                ddochr = Convert.ToDouble(txtdocchr.Text);
                dintamt = Convert.ToDouble(txtintamt.Text);
                dfinpay = Convert.ToDouble(txtfinpay.Text);
                dnoinst = Convert.ToDouble(txtnoinst1.Text);
                txtoth = txtothers.Text;
                dpay = Convert.ToDouble(txtinipay.Text);
                dpaydate = txtdatepay.Text.Trim();
                dstartdate = txtduedate.Text.Trim();
                deachpay = Convert.ToDouble(Txteach.Text);

                int iSlno = 0;
                int iSllno = Convert.ToInt32(lblBillNo.Text);
                try
                {
                    //iSlno = bl.UpdateHirePurchase(iSllno, sBilldate, sCustomerID, sCustomerName, dpurAmt, dlnAmt, ddochr, dintamt, dfinpay, dnoinst, txtoth, dpay, dpaydate, dstartdate, deachpay, Username);

                    //pnlVisitDetails.Visible = false;
                    ModalPopupExtender1.Hide();
                    lnkBtnAdd.Visible = true;
                    Reset();
                    GrdViewSerVisit.DataBind();
                    GrdViewSerVisit.Visible = true;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Hire Purchase Updated Successfully');", true);
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

    private string GetConnectionString()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        return connStr;
    }

    public void Reset()
    {
            Txtlnamt.Text = "0";
            txtpuramt.Text = "0";
            txtdocchr.Text = "0";
            txtintamt.Text = "0";
            txtfinpay.Text = "0";
            txtnoinst1.Text = "0";
            txtinipay.Text = "0";
            txtdatepay.Text = "";
            txtduedate.Text = "";
            Txteach.Text = "0";
            txtothers.Text = "";
            txtBillDate.Text = DateTime.Now.ToShortDateString();
            
        cmbCustomer.SelectedIndex = 0;
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //MyAccordion.Visible = true;
            //pnlVisitDetails.Visible = false;
            ModalPopupExtender1.Hide();
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
                BusinessLogic bl = new BusinessLogic(GetConnectionString());
                string connection = Request.Cookies["Company"].Value;

                //if (bl.ChequeLeafUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                //{
                //    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;

                //    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                //    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                //}

                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "HIPUR"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "HIPUR"))
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

    private void loadCategories()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic();
        DataSet ds = new DataSet();
        ds = bl.ListCategory(sDataSource, "");
        //cmbCategory.DataTextField = "CategoryName";
        //cmbCategory.DataValueField = "CategoryID";
        //cmbCategory.DataSource = ds;
        //cmbCategory.DataBind();
    }

    protected void LoadProducts(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        //string CategoryID = cmbCategory.SelectedValue;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        //ds = bl.ListProductsForCategoryID(CategoryID);
        //cmbProdAdd.Items.Clear();
        //cmbProdAdd.DataSource = ds;
        //cmbProdAdd.Items.Insert(0, new ListItem("Select ItemCode", "0"));
        //cmbProdAdd.DataTextField = "ItemCode";
        //cmbProdAdd.DataValueField = "ItemCode";
        //cmbProdAdd.DataBind();

        ////ds = bl.ListModelsForCategoryID(CategoryID);
        //cmbModel.Items.Clear();
        //cmbModel.DataSource = ds;
        //cmbModel.Items.Insert(0, new ListItem("Select Model", "0"));
        //cmbModel.DataTextField = "Model";
        //cmbModel.DataValueField = "Model";
        //cmbModel.DataBind();

        ////ds = bl.ListBrandsForCategoryID(CategoryID);
        //cmbBrand.Items.Clear();
        //cmbBrand.DataSource = ds;
        //cmbBrand.Items.Insert(0, new ListItem("Select Brand", "0"));
        //cmbBrand.DataTextField = "ProductDesc";
        //cmbBrand.DataValueField = "ProductDesc";
        //cmbBrand.DataBind();

        ////ds = bl.ListProdNameForCategoryID(CategoryID);
        //cmbProdName.Items.Clear();
        //cmbProdName.DataSource = ds;
        //cmbProdName.Items.Insert(0, new ListItem("Select ItemName", "0"));
        //cmbProdName.DataTextField = "ProductName";
        //cmbProdName.DataValueField = "ProductName";
        //cmbProdName.DataBind();

        //LoadForProduct(this, null);


    }

    protected void LoadForBrand(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        //string brand = cmbBrand.SelectedValue;
        //string CategoryID = cmbCategory.SelectedValue;
        //DataSet catData = bl.GetProductForId(sDataSource, itemCode);
        //cmbProdAdd.SelectedValue = itemCode;
        //cmbModel.SelectedValue = itemCode;
        DataSet ds = new DataSet();
        //ds = bl.ListModelsForBrand(brand, CategoryID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //cmbModel.Items.Clear();
            //cmbModel.DataSource = ds;
            //cmbModel.DataTextField = "Model";
            //cmbModel.DataValueField = "Model";
            //cmbModel.DataBind();
        }

        //ds = bl.ListProdcutsForBrand(brand, CategoryID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //cmbProdAdd.Items.Clear();
            //cmbProdAdd.DataSource = ds;
            //cmbProdAdd.DataTextField = "ItemCode";
            //cmbProdAdd.DataValueField = "ItemCode";
            //cmbProdAdd.DataBind();
        }

        //ds = bl.ListProdcutNameForBrand(brand, CategoryID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //cmbProdName.Items.Clear();
            //cmbProdName.DataSource = ds;
            //cmbProdName.DataTextField = "ProductName";
            //cmbProdName.DataValueField = "ProductName";
            //cmbProdName.DataBind();
        }
        cmbProdAdd_SelectedIndexChanged(this, null);

    }

    protected void LoadForModel(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        //string model = cmbModel.SelectedValue;
        //string CategoryID = cmbCategory.SelectedValue;
        DataSet ds = new DataSet();

        //ds = bl.ListProdcutsForModel(model, CategoryID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //cmbProdAdd.Items.Clear();
            //cmbProdAdd.DataSource = ds;
            //cmbProdAdd.DataTextField = "ItemCode";
            //cmbProdAdd.DataValueField = "ItemCode";
            //cmbProdAdd.DataBind();
        }

        //ds = bl.ListBrandsForModel(model, CategoryID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //cmbBrand.Items.Clear();
            //cmbBrand.DataSource = ds;
            //cmbBrand.DataTextField = "ProductDesc";
            //cmbBrand.DataValueField = "ProductDesc";
            //cmbBrand.DataBind();
        }

        //ds = bl.ListProductNameForModel(model, CategoryID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //cmbProdName.Items.Clear();
            //cmbProdName.DataSource = ds;
            //cmbProdName.DataTextField = "ProductName";
            //cmbProdName.DataValueField = "ProductName";
            //cmbProdName.DataBind();
        }
        cmbProdAdd_SelectedIndexChanged(this, null);
    }

    protected void LoadForProductName(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        //string prodName = cmbProdName.SelectedValue;
        //string CategoryID = cmbCategory.SelectedValue;
        DataSet ds = new DataSet();

        //ds = bl.ListProdcutsForProductName(prodName, CategoryID);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //cmbProdAdd.Items.Clear();
            //cmbProdAdd.DataSource = ds;
            //cmbProdAdd.DataTextField = "ItemCode";
            //cmbProdAdd.DataValueField = "ItemCode";
            //cmbProdAdd.DataBind();
        }

        //ds = bl.ListBrandsForProductName(prodName, CategoryID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //cmbBrand.Items.Clear();
            //cmbBrand.DataSource = ds;
            //cmbBrand.DataTextField = "ProductDesc";
            //cmbBrand.DataValueField = "ProductDesc";
            //cmbBrand.DataBind();
        }

        //ds = bl.ListModelsForProductName(prodName, CategoryID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //cmbModel.Items.Clear();
            //cmbModel.DataSource = ds;
            //cmbModel.DataTextField = "Model";
            //cmbModel.DataValueField = "Model";
            //cmbModel.DataBind();
        }
        cmbProdAdd_SelectedIndexChanged(this, null);
    }

    protected void cmbProdAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();
            DataSet roleDs = new DataSet();
            string itemCode = string.Empty;
            DataSet checkDs;

            //if (cmbProdAdd.SelectedItem != null)
            //{
            //    itemCode = cmbProdAdd.SelectedItem.Value.Trim();

            //    DataSet catData = bl.GetProductForId(sDataSource, cmbProdAdd.SelectedItem.Value.Trim());

            //    if (catData != null)
            //    {
            //        if ((catData.Tables[0].Rows[0]["Model"] != null) && (catData.Tables[0].Rows[0]["Model"].ToString() != ""))
            //        {
            //            if (cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()) != null)
            //            {
            //                cmbModel.ClearSelection();
            //                if (!cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()).Selected)
            //                    cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()).Selected = true;

            //            }
            //}

            //        if ((catData.Tables[0].Rows[0]["ProductDesc"] != null) && (catData.Tables[0].Rows[0]["ProductDesc"].ToString() != ""))
            //        {
            //            //if (cmbBrand.Items.FindByValue(catData.Tables[0].Rows[0]["ProductDesc"].ToString().Trim()) != null)
            //            //{
            //            //    cmbBrand.ClearSelection();
            //            //    if (!cmbBrand.Items.FindByValue(catData.Tables[0].Rows[0]["ProductDesc"].ToString().Trim()).Selected)
            //            //        cmbBrand.Items.FindByValue(catData.Tables[0].Rows[0]["ProductDesc"].ToString().Trim()).Selected = true;
            //            //}
            //        }

            //        if ((catData.Tables[0].Rows[0]["ProductName"] != null) && (catData.Tables[0].Rows[0]["ProductName"].ToString() != ""))
            //        {
            //            if (cmbProdName.Items.FindByValue(catData.Tables[0].Rows[0]["ProductName"].ToString().Trim()) != null)
            //            {
            //                cmbProdName.ClearSelection();
            //                if (!cmbProdName.Items.FindByValue(catData.Tables[0].Rows[0]["ProductName"].ToString().Trim()).Selected)
            //                    cmbProdName.Items.FindByValue(catData.Tables[0].Rows[0]["ProductName"].ToString().Trim()).Selected = true;
            //            }
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void LoadForProduct(object sender, EventArgs e)
    {
        //string itemCode = cmbProdAdd.SelectedValue;
        //cmbModel.SelectedValue = itemCode;
        //cmbBrand.SelectedValue = itemCode;
        cmbProdAdd_SelectedIndexChanged(sender, e);
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
            //rowstk.Visible = true;

            string strItemCode = string.Empty;

            GridViewRow row = GrdViewSerVisit.SelectedRow;

            int Billno = Convert.ToInt32(GrdViewSerVisit.SelectedDataKey.Value);

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            strItemCode = row.Cells[0].Text.Replace("&quot;", "\"").Trim();

            DataSet ds = bl.GetHirePurchaseForId(Billno);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["slno"] != null)
                        lblBillNo.Text = ds.Tables[0].Rows[0]["slno"].ToString();

                    if (ds.Tables[0].Rows[0]["lnamt"] != null)
                        Txtlnamt.Text = Convert.ToString(ds.Tables[0].Rows[0]["lnamt"]);

                    if (ds.Tables[0].Rows[0]["puramt"] != null)
                        txtpuramt.Text = Convert.ToString(ds.Tables[0].Rows[0]["puramt"]);

                    if (ds.Tables[0].Rows[0]["dochr"] != null)
                        txtdocchr.Text = Convert.ToString(ds.Tables[0].Rows[0]["dochr"]);

                    if (ds.Tables[0].Rows[0]["intamt"] != null)
                        txtintamt.Text = Convert.ToString(ds.Tables[0].Rows[0]["intamt"]);

                    if (ds.Tables[0].Rows[0]["finpay"] != null)
                        txtfinpay.Text = Convert.ToString(ds.Tables[0].Rows[0]["finpay"]);

                    if (ds.Tables[0].Rows[0]["noinst"] != null)
                        txtnoinst1.Text = Convert.ToString(ds.Tables[0].Rows[0]["noinst"]);

                    if (ds.Tables[0].Rows[0]["inipay"] != null)
                        txtinipay.Text = Convert.ToString(ds.Tables[0].Rows[0]["inipay"]);

                    if (ds.Tables[0].Rows[0]["paydate"] != null)
                        txtdatepay.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["paydate"].ToString()).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["startdate"] != null)
                        txtduedate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["startdate"].ToString()).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["eachpay"] != null)
                        Txteach.Text = Convert.ToString(ds.Tables[0].Rows[0]["eachpay"]);

                    if (ds.Tables[0].Rows[0]["others"] != null)
                        txtothers.Text = Convert.ToString(ds.Tables[0].Rows[0]["others"]);

                    if (ds.Tables[0].Rows[0]["BillDate"] != null)
                        txtBillDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BillDate"].ToString()).ToString("dd/MM/yyyy");

                    int CustomerID = 0;
                    if ((ds.Tables[0].Rows[0]["CustomerID"] != null) && (ds.Tables[0].Rows[0]["CustomerID"].ToString() != ""))
                    {
                        CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerID"].ToString());
                        cmbCustomer.ClearSelection();
                        ListItem li = cmbCustomer.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(CustomerID.ToString()));
                        if (li != null) li.Selected = true;
                    }

                    //txtBillDate.Text
                    //Txtlnamt.Text

                    //cmbCustomer.SelectedItem.Value
                    //txtpuramt.Text
                    //txtdocchr.Text
                    //txtintamt.Text
                    //txtfinpay.Text
                    //txtnoinst1.Text
                    //txtothers.Text
                    //txtinipay.Text
                    //txtdatepay.Text
                    //txtduedate.Text
                    //Txteach.Text

                    UpdateButton.Visible = true;
                    SaveButton.Visible = false;
                    CancelButton.Visible = true;
                    lnkBtnAdd.Visible = false;
                    GrdViewSerVisit.Visible = false;
                    //pnlVisitDetails.Visible = true;

                    ModalPopupExtender1.Show();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLeadContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (Session["contactDs"] != null)
            {
                string connStr = string.Empty;
                DataSet ds;


                ds = (DataSet)Session["contactDs"];
                //ds.Tables[0].Rows[GrdViewLeadContact.Rows[e.RowIndex].DataItemIndex].Delete();
                //ds.Tables[0].AcceptChanges();
                //GrdViewLeadContact.DataSource = ds;
                //GrdViewLeadContact.DataBind();
                //Session["contactDs"] = ds;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewLeadContact_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        //GridViewRow row = GrdViewLeadContact.SelectedRow;

        //hdCurrentRow.Value = Convert.ToString(row.DataItemIndex);

        //txtContactedDate.Text = row.Cells[1].Text;
        //txtContactSummary.Text = row.Cells[2].Text;
        //cmdSaveContact.Visible = false;
        //cmdUpdateContact.Visible = true;
        //ModalPopupContact.Show();

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
            //pnlVisitDetails.Visible = true;
            Reset();
            lblBillNo.Text = "- TBA -";
            txtBillDate.Focus();
            //UpdatePanel16.Update();
            //rowstk.Visible = false;
            //txtOpeningStock.Enabled = true;
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
                e.InputParameters["Slno"] = GrdViewSerVisit.SelectedDataKey.Value;

            e.InputParameters["usernam"] = Request.Cookies["LoggedUserName"].Value;
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
            Page.Validate("salesval");

            if (Page.IsValid)
            {
                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                int dcatid = 0;
                string AccountNo = string.Empty;
                int dopening = 0;
                string ditemCode = string.Empty;
                string dProductName = string.Empty;
                string dProductDesc = string.Empty;
                string dmodel = string.Empty;

                string Username = Request.Cookies["LoggedUserName"].Value;

                BusinessLogic bl = new BusinessLogic(sDataSource);

                double dlnAmt = 0;
                double dpurAmt = 0;
                double ddochr = 0;
                double dintamt = 0;
                double dfinpay = 0;
                double dnoinst = 0;
                double dpay = 0;
                double deachpay = 0;
                string txtoth = string.Empty;

                string sBilldate = string.Empty;
                string dpaydate = string.Empty;
                string dstartdate = string.Empty;
                string sCustomerName = string.Empty;
                int sCustomerID = 0;

                sBilldate = txtBillDate.Text.Trim();
                dlnAmt = Convert.ToDouble(Txtlnamt.Text);
                sCustomerName = cmbCustomer.SelectedItem.Text;
                sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                dpurAmt = Convert.ToDouble(txtpuramt.Text);
                ddochr = Convert.ToDouble(txtdocchr.Text);
                dintamt = Convert.ToDouble(txtintamt.Text);
                dfinpay = Convert.ToDouble(txtfinpay.Text);
                dnoinst = Convert.ToDouble(txtnoinst1.Text);
                txtoth = txtothers.Text;
                dpay = Convert.ToDouble(txtinipay.Text);
                dpaydate = txtdatepay.Text.Trim();
                dstartdate = txtduedate.Text.Trim();
                deachpay = Convert.ToDouble(Txteach.Text);


                try
                {
                    //int billNo = bl.InsertHirePurchase(sBilldate, sCustomerID, sCustomerName, dpurAmt,dlnAmt, ddochr, dintamt, dfinpay, dnoinst, txtoth,dpay,dpaydate,dstartdate,deachpay, Username);
                    //pnlVisitDetails.Visible = false;
                    ModalPopupExtender1.Hide();
                    lnkBtnAdd.Visible = true;
                    Reset();
                    GrdViewSerVisit.DataBind();
                    GrdViewSerVisit.Visible = true;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Hire Purchase added successfully ');", true);
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
    protected void txtBillDate_TextChanged(object sender, EventArgs e)
    {
        //if (cmbCustomer.SelectedItem.Value != "0")
        //{
        //    GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
        //    ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
        //    UpdatePanel11.Update();
        //}
    }
    protected void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        //        try
        //      {

        BusinessLogic bl = new BusinessLogic(sDataSource);

        if (lblBillNo.Text != "- TBA -")
        {
            //string receivedBill = "";
            //var salesData = bl.GetSalesForId(int.Parse(lblBillNo.Text));

            //if (salesData.Tables[0].Rows[0]["Paymode"] != null && salesData.Tables[0].Rows[0]["Paymode"].ToString() == "3")
            //{
            //    receivedBill = bl.IsAmountPaidForBill(lblBillNo.Text);

            //    if (receivedBill != string.Empty)
            //    {
            //        if (cmbCustomer.Items.FindByValue(salesData.Tables[0].Rows[0]["DebtorID"].ToString()) != null)
            //            cmbCustomer.SelectedValue = salesData.Tables[0].Rows[0]["DebtorID"].ToString();

            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
            //        UpdatePanel21.Update();
            //        return;
            //    }
            //}

        }
    }

    private void loadSupplier()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListSundryDebtors(sDataSource);

        cmbCustomer.Items.Clear();
        cmbCustomer.Items.Add(new ListItem("Select Customer", "0"));
        cmbCustomer.DataSource = ds;
        cmbCustomer.DataBind();
        cmbCustomer.DataTextField = "LedgerName";
        cmbCustomer.DataValueField = "LedgerID";
        cmbCustomer.Focus();
    }


    protected void txtpuramt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(txtpuramt.Text).Length > 0)
            {
                if (Convert.ToString(txtinipay.Text).Length > 0)
                {
                    Txtlnamt.Text = Convert.ToString(Convert.ToDouble(txtpuramt.Text) - Convert.ToDouble(txtinipay.Text));
                }
                else
                {
                    Txtlnamt.Text = Convert.ToString(txtpuramt.Text);
                }
                UpdatePanel4.Update();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtinipay_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(txtinipay.Text).Length > 0)
            {
                if (Convert.ToString(txtpuramt.Text).Length > 0)
                {
                    Txtlnamt.Text = Convert.ToString(Convert.ToDouble(txtpuramt.Text) - Convert.ToDouble(txtinipay.Text));
                }
                else
                {
                    Txtlnamt.Text = Convert.ToString(txtinipay.Text);
                }
                UpdatePanel4.Update();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtdocchr_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtintamt.Text));
            UpdatePanel7.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtintamt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtintamt.Text));
            UpdatePanel7.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void txtDueDate_TextChanged(object sender, EventArgs e)
    {
        if (cmbCustomer.SelectedItem.Value != "0")
        {
            //GetCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
            //ExamimeCustomerCreditHistory(cmbCustomer.SelectedItem.Value);
            //UpdatePanel11.Update();
        }
    }

    protected void txtnoinst1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Txteach.Text = Convert.ToString(Convert.ToDouble(txtfinpay.Text) / Convert.ToDouble(txtnoinst1.Text));
            UpdatePanel8.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

   
}