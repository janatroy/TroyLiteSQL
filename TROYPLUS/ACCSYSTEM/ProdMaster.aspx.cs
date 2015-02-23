using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Xml;
using SMSLibrary;
using System.Collections.Generic;

public partial class ProdMaster : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    string dbfileName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (!Page.IsPostBack)
            {
                //mrBillDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //mrBillDate.MaximumValue = System.DateTime.Now.ToShortDateString();

                //myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

                BindDropdownList();
                Session["contactDs"] = null;

                loadBanks();

                GrdViewProduct.PageSize = 11;
                loadSupplier();
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
                BindGrid("", "");
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
            BindGrid("", "");
            ddCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadSupplier()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListSundryDebtors(sDataSource);

        //cmbCustomer.Items.Clear();
        //cmbCustomer.Items.Add(new ListItem("Select Customer", "0"));
        //cmbCustomer.DataSource = ds;
        //cmbCustomer.DataBind();
        //cmbCustomer.DataTextField = "LedgerName";
        //cmbCustomer.DataValueField = "LedgerID";
        //cmbCustomer.Focus();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string textt = string.Empty;
            string dropd = string.Empty;

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;

            BindGrid(textt, dropd);

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BlkUpd_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ProductsUpdation.aspx?ID=Update");
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
            //txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtintamt.Text));
            //txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtintamt.Text));
            //UpdatePanel7.Update();

            //txtdown1.Text = Convert.ToString(Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtinipay.Text));
            //UpdatePanel16.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtupfront_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtintamt.Text));
            //txtdown.Text = Convert.ToString(Convert.ToDouble(txtupfront.Text) + Convert.ToDouble(txtemi.Text));
            //UpdatePanel13.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtemi_TextChanged(object sender, EventArgs e)
    {
        try
        {

            //txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtintamt.Text));
            //txtdown.Text = Convert.ToString(Convert.ToDouble(txtupfront.Text) + Convert.ToDouble(txtemi.Text));
            //UpdatePanel13.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtpuramt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (Convert.ToString(txtpuramt.Text).Length > 0)
            //{
            //    if (Convert.ToString(txtinipay.Text).Length > 0)
            //    {
            //        Txtlnamt.Text = Convert.ToString(Convert.ToDouble(txtpuramt.Text) - Convert.ToDouble(txtinipay.Text));
            //    }
            //    else
            //    {
            //        Txtlnamt.Text = Convert.ToString(txtpuramt.Text);
            //    }
            //    UpdatePanel4.Update();
            //}
            //txtintamt_TextChanged(this, null);
            //txtnoinst1_TextChanged(this, null);
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
            //if (Convert.ToString(txtinipay.Text).Length > 0)
            //{
            //    if (Convert.ToString(txtpuramt.Text).Length > 0)
            //    {
            //        Txtlnamt.Text = Convert.ToString(Convert.ToDouble(txtpuramt.Text) - Convert.ToDouble(txtinipay.Text));
            //    }
            //    else
            //    {
            //        Txtlnamt.Text = Convert.ToString(txtinipay.Text);
            //    }
            //    UpdatePanel4.Update();

            //    txtdown1.Text = Convert.ToString(Convert.ToDouble(txtdocchr.Text) + Convert.ToDouble(txtinipay.Text));
            //    UpdatePanel16.Update();

            //}
            //txtintamt_TextChanged(this, null);
            //txtnoinst1_TextChanged(this, null);
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
            //txtfinpay.Text = Convert.ToString(Convert.ToDouble(Txtlnamt.Text) + Convert.ToDouble(txtintamt.Text));
            //UpdatePanel7.Update();

            //txtnoinst1_TextChanged(this, null);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtnoinst1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //double txteachmonth = Convert.ToDouble(txtfinpay.Text) / Convert.ToDouble(txtnoinst1.Text);
            //Txteach.Text = txteachmonth.ToString("#0");
            //UpdatePanel8.Update();

            //if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
            //{
            //    DataSet ds = new DataSet();
            //    DataTable dt;
            //    DataRow drNew;
            //    dt = new DataTable();
            //    DataColumn dc;
            //    int ii = 1;
            //    string dtaa1 = string.Empty;

            //    dc = new DataColumn("ChequeNo");
            //    dt.Columns.Add(dc);

            //    dc = new DataColumn("DueDate");
            //    dt.Columns.Add(dc);

            //    dc = new DataColumn("Amount");
            //    dt.Columns.Add(dc);

            //    dc = new DataColumn("Cancelled");
            //    dt.Columns.Add(dc);

            //    dc = new DataColumn("Narration");
            //    dt.Columns.Add(dc);

            //    //for (int i = 0; i < Convert.ToDouble(txtnoinst1.Text); i++)
            //    //{

            //    //    DataRow dr_final1312 = dt.NewRow();
            //    //    dr_final1312["ChequeNo"] = "";

            //    //    string aa = txtduedate.Text.ToString().ToUpper().Trim();
            //    //    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");

            //    //    if (ii == 1)
            //    //    {
            //    //        dr_final1312["DueDate"] = dtaa;
            //    //    }
            //    //    else
            //    //    {
            //    //        string dtt = Convert.ToDateTime(dtaa1).ToString("dd/MM/yyyy");
            //    //        dr_final1312["DueDate"] = dtt;
            //    //    }


            //    //    dr_final1312["Amount"] = Txteach.Text;

            //    //    //CheckBox txthh = (CheckBox)GrdViewItems.FindControl("chkboxCancelled");
            //    //    //txthh.Checked = false;
            //    //    //bool check = false;
            //    //    //check = false;

            //    //    //if (txthh.Checked)
            //    //    //{
            //    //    //    check = txthh.Checked;
            //    //    //}
            //    //    //else
            //    //    //{
            //    //    //    check = false;
            //    //    //}

            //    //    dr_final1312["Cancelled"] = "N";
            //    //    dr_final1312["Narration"] = "";

            //    //    if (ii == 1)
            //    //    {
            //    //        dtaa1 = Convert.ToDateTime(dtaa).AddMonths(1).ToString();
            //    //    }
            //    //    else
            //    //    {
            //    //        dtaa1 = Convert.ToDateTime(dtaa1).AddMonths(1).ToString();
            //    //    }

            //    //    ii = ii + 1;

            //    //    dt.Rows.Add(dr_final1312);
            //    //}

            //    ds.Tables.Add(dt);

            //    GrdViewItems.DataSource = ds;
            //    GrdViewItems.DataBind();


            //    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
            //    {
            //        DropDownList txt = (DropDownList)GrdViewItems.Rows[vLoop].FindControl("chkboxCancelled");

            //        txt.Enabled = false;
            //    }
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtemiper_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //double txteachemi = Convert.ToDouble(Txteach.Text) * Convert.ToDouble(txtemiper.Text);
            //txtemi.Text = txteachemi.ToString("#0");
            //txtdown.Text = Convert.ToString(Convert.ToDouble(txtupfront.Text) + Convert.ToDouble(txtemi.Text));
            //UpdatePanel14.Update();
            //UpdatePanel13.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    //{
    //    if (GrdViewLead.SelectedDataKey != null)
    //        e.InputParameters["Slno"] = GrdViewLead.SelectedDataKey.Value;

    //    e.InputParameters["usernam"] = Request.Cookies["LoggedUserName"].Value;
    //}

    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);
    //    //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
    //    GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
    //    //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
    //    GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
    //    GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    //}


    private void BindGrid(string textSearch, string dropDown)
    {
        string connection = Request.Cookies["Company"].Value;

        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        object usernam = Session["LoggedUserName"];

        ds = bl.ListProducts(connection, textSearch, dropDown);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewProduct.DataSource = ds.Tables[0].DefaultView;
                GrdViewProduct.DataBind();
            }
        }
        else
        {
            GrdViewProduct.DataSource = null;
            GrdViewProduct.DataBind();
        }
    }


    private void BindDropdownList()
    {
        //LeadBusinessLogic bl = new LeadBusinessLogic(sDataSource);
        //DataSet ds = new DataSet();
        
        //ds = bl.GetDropdownList(sDataSource, "CONTACT");
        //cmbModeOfContact.DataSource = ds;
        //cmbModeOfContact.DataBind();
        //cmbModeOfContact.DataTextField = "TextValue";
        //cmbModeOfContact.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "PERRES");
        //cmbPersonalResp.DataSource = ds;
        //cmbPersonalResp.DataBind();
        //cmbPersonalResp.DataTextField = "TextValue";
        //cmbPersonalResp.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "BUSTYPE");
        //cmbBussType.DataSource = ds;
        //cmbBussType.DataBind();
        //cmbBussType.DataTextField = "TextValue";
        //cmbBussType.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "BRNCH");
        //cmbBranch.DataSource = ds;
        //cmbBranch.DataBind();
        //cmbBranch.DataTextField = "TextValue";
        //cmbBranch.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "LSTCMP");
        //cmbLastCompAction.DataSource = ds;
        //cmbLastCompAction.DataBind();
        //cmbLastCompAction.DataTextField = "TextValue";
        //cmbLastCompAction.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "NXTAXN");
        //cmbNextAction.DataSource = ds;
        //cmbNextAction.DataBind();
        //cmbNextAction.DataTextField = "TextValue";
        //cmbNextAction.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "CATEGRY");
        //cmbCategory.DataSource = ds;
        //cmbCategory.DataBind();
        //cmbCategory.DataTextField = "TextValue";
        //cmbCategory.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "STATUS");
        //cmbStatus.DataSource = ds;
        //cmbStatus.DataBind();
        //cmbStatus.DataTextField = "TextValue";
        //cmbStatus.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "INFO3");
        //ddlinfo3.DataSource = ds;
        //ddlinfo3.DataBind();
        //ddlinfo3.DataTextField = "TextValue";
        //ddlinfo3.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "INFO4");
        //ddlinfo4.DataSource = ds;
        //ddlinfo4.DataBind();
        //ddlinfo4.DataTextField = "TextValue";
        //ddlinfo4.DataValueField = "TextValue";

        //ds = bl.GetDropdownList(sDataSource, "INFO5");
        //ddlinfo5.DataSource = ds;
        //ddlinfo5.DataBind();
        //ddlinfo5.DataTextField = "TextValue";
        //ddlinfo5.DataValueField = "TextValue";

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
            Reset();
            
            BusinessLogic bl = new BusinessLogic(sDataSource);

            if (!DealerRequired())
            {
                rowDealerAdd.Visible = false;
                rowDealerAdd1.Visible = false;                   
            }

            TextBox txtStock = null;

            if (AllowStockEdit())
            {
                txtStockAdd.Enabled = true;
            }
            else
            {
                txtStockAdd.Enabled = false;
            }

            string connection = Request.Cookies["Company"].Value;
            DataSet ds = bl.ListPriceListInfo(connection, "", "");

            txtItemCodeAdd.Enabled = true;
            title1.Text = "Add New Product details";

            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dst = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("ID");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Row");
            dtt.Columns.Add(dct);

            dct = new DataColumn("PriceName");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Price");
            dtt.Columns.Add(dct);

            dct = new DataColumn("EffDate");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Discount");
            dtt.Columns.Add(dct);

            dst.Tables.Add(dtt);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        drNew = dtt.NewRow();
                        drNew["Row"] = Convert.ToInt32(ds.Tables[0].Rows[i]["Row"]);
                        drNew["ID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        drNew["PriceName"] = Convert.ToString(ds.Tables[0].Rows[i]["PriceName"]);
                        drNew["Price"] = "";
                        drNew["EffDate"] = "";
                        drNew["Discount"] = "";
                        dst.Tables[0].Rows.Add(drNew);
                    }
                }

                GrdViewItems.DataSource = dst.Tables[0].DefaultView;
                GrdViewItems.DataBind();
            }
            else
            {

                GrdViewItems.DataSource = null;
                GrdViewItems.DataBind();
            }

            BulkEditGridView1.DataSource = null;
            BulkEditGridView1.DataBind();

            ModalPopupExtender2.Show();
            UpdateButton.Visible = false;
            AddButton.Visible = true;
            UpdateButton.Visible = false;

            Session["Method"] = "Add";
            loadCategories();

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
        else if (Session["AppSettings"] == null)
        {
            BusinessLogic bl = new BusinessLogic();
            DataSet ds = bl.GetAppSettings(Request.Cookies["Company"].Value);

            if (ds != null)
                Session["AppSettings"] = ds;

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

    private bool AllowStockEdit()
    {
        DataSet appSettings;
        string dealerRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "STOCKEDIT")
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

    private void Reset()
    {
        //txtLeadNo.Text = "";
        //txtAddress.Text = "";
        //txtProspCustName.Text = "";
        //txtMobile.Text = "";
        //txtEmail.Text = "";
        //txtLandline.Text = "";
        //cmbModeOfContact.SelectedIndex = 0;
        //cmbPersonalResp.SelectedIndex = 0;
        //cmbBussType.SelectedIndex = 0;
        //cmbBranch.SelectedIndex = 0;
        //cmbStatus.SelectedIndex = 0;
        //cmbLastCompAction.SelectedIndex = 0;
        //cmbNextAction.SelectedIndex = 0;
        //cmbCategory.SelectedIndex = 0;

        //txtInfo1.Text = "";
        //txtInfo2.Text = "";
        //ddlinfo3.SelectedIndex = 0;

        //ddlinfo4.SelectedIndex = 0;
        //ddlinfo5.SelectedIndex = 0;

        txtAllowedPriceAdd.Text = "0";
        txtBarcodeAdd.Text = "";
        txtBuyDiscountAdd.Text = "0";
        txtBuyRateAdd.Text = "0";
        txtBuyUnitAdd.Text = "0";
        txtCommCodeAdd.Text = "";
        txtCSTAdd.Text = "0";
        txtDealerDiscountAdd.Text = "0";
        txtDealerRateAdd.Text = "0";
        txtDealerUnitAdd.Text = "0";
        txtDealerVATAdd.Text = "0";
        txtDiscountAdd.Text = "0";
        txtItemCodeAdd.Text = "";
        txtItemNameAdd.Text = "";
        txtModelAdd.Text = "";

        DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
        string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
        txtMrpDateAdd.Text = dtaa;
        txtDpDateAdd.Text = dtaa;
        txtNLCDateAdd.Text = dtaa;
        
        drpMeasureAdd.SelectedIndex = 0;
        drpComplexAdd.SelectedIndex = 0;
        
        txtProdDescAdd.SelectedIndex = 0;
        txtproductlevel.Text = "0";
        drpRoleTypeAdd.SelectedIndex = 0;
        txtROLAdd.Text = "1";
        txtStockAdd.Text = "0";
        txtUnitAdd.Text = "0";
        txtUnitRateAdd.Text = "0";
        txtVATAdd.Text = "0";

        txtBuyVATAdd.Text = "0";
        txtExecutiveCommissionAdd.Text = "0";
        txtNLCAdd.Text = "0";
        drpIsActiveAdd.SelectedValue = "YES";
        drpOutdatedAdd.SelectedValue = "N";
    }

    private void ShowLeadContactInfo()
    {
        string connStr = GetConnectionString();

        if (Session["LeadID"] != null && Session["LeadID"].ToString() != "0")
        {
            LeadBusinessLogic bl = new LeadBusinessLogic(connStr);
            DataSet ds = bl.ListLeadContact(Session["LeadID"].ToString());

            if (ds != null)
            {
                //GrdViewLeadContact.DataSource = ds.Tables[0];
                //GrdViewLeadContact.DataBind();
            }

        }
        else
        {
            //GrdViewLeadContact.DataSource = null;
            //GrdViewLeadContact.DataBind();
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

    protected void cmdSaveContact_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;
            ds = new DataSet();

            if (Session["contactDs"] == null)
            {

                dt = new DataTable();

                dc = new DataColumn("ContactRefID");
                dt.Columns.Add(dc);

                dc = new DataColumn("ContactedDate");
                dt.Columns.Add(dc);

                dc = new DataColumn("ContactSummary");
                dt.Columns.Add(dc);

                ds.Tables.Add(dt);

                drNew = dt.NewRow();

                drNew["ContactRefID"] = 1;
                //drNew["ContactedDate"] = txtContactedDate.Text;
                //drNew["ContactSummary"] = txtContactSummary.Text;
                ds.Tables[0].Rows.Add(drNew);
                Session["contactDs"] = ds;
            }
            else
            {
                ds = (DataSet)Session["contactDs"];

                int maxID = 0;

                if (ds.Tables[0].Rows.Count > 0)
                    maxID = int.Parse(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ContactRefID"].ToString());

                drNew = ds.Tables[0].NewRow();
                drNew["ContactRefID"] = maxID + 1;
                //drNew["ContactedDate"] = txtContactedDate.Text;
                //drNew["ContactSummary"] = txtContactSummary.Text;
                ds.Tables[0].Rows.Add(drNew);
                Session["contactDs"] = ds;
            }


            //this.GrdViewLeadContact.Visible = true;

            //GrdViewLeadContact.DataSource = ds.Tables[0];
            //GrdViewLeadContact.DataBind();

            //ModalPopupContact.Hide();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdUpdateContact_Click(object sender, EventArgs e)
    {
        try
        {
            var ds = (DataSet)Session["contactDs"];
            //int currentRow = int.Parse(hdCurrentRow.Value);

            //ds.Tables[0].Rows[currentRow]["ContactedDate"] = txtContactedDate.Text;
            //ds.Tables[0].Rows[currentRow]["ContactSummary"] = txtContactSummary.Text;
            //ds.Tables[0].Rows[currentRow].EndEdit();
            //ds.Tables[0].Rows[currentRow].AcceptChanges();

            //GrdViewLeadContact.DataSource = ds.Tables[0];
            //GrdViewLeadContact.DataBind();
            //ModalPopupContact.Hide();
            Session["contactDs"] = ds;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkAddContact_Click(object sender, EventArgs e)
    {
        //cmdSaveContact.Visible = true;
        //cmdUpdateContact.Visible = false;
        //updatePnlContact.Update();

        //txtContactedDate.Text = string.Empty;
        //txtContactSummary.Text = string.Empty;

        //ModalPopupContact.Show();
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


    //protected void GrdViewLeadContact_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Pager)
    //    {
    //        PresentationUtils.SetPagerButtonStates(GrdViewLeadContact, e.Row, this);
    //    }
    //}

    //protected void GrdViewLeadContact_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRow product = ((System.Data.DataRowView)e.Row.DataItem).Row;
    //    }
    //}


    protected void GrdViewLeadContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (Session["contactDs"] != null)
            {
                string connStr = string.Empty;
                DataSet ds;

                /*
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                GridViewRow row = GrdViewLeadContact.Rows[e.RowIndex];
                string refID = row.Cells[0].Text;
                LeadBusinessLogic bl = new LeadBusinessLogic(connStr);
                bl.DeleteLeadContact(refID);*/

                ds = (DataSet)Session["contactDs"];
                //ds.Tables[0].Rows[GrdViewLeadContact.Rows[e.RowIndex].DataItemIndex].Delete();
                //ds.Tables[0].AcceptChanges();
                //GrdViewLeadContact.DataSource = ds;
                //GrdViewLeadContact.DataBind();
                Session["contactDs"] = ds;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void cmdCancelContact_Click(object sender, EventArgs e)
    {
        //ModalPopupContact.Hide();
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "LeadManagement_" + DateTime.Now.ToString() + ".xls";

            LeadBusinessLogic bl = new LeadBusinessLogic(GetConnectionString());

            int leadid = 0;

            DataSet ds = bl.ListLeadMasterContacts(GetConnectionString(), txtSearch.Text, ddCriteria.SelectedValue);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("LeadID"));
                    dt.Columns.Add(new DataColumn("CreationDate"));
                    dt.Columns.Add(new DataColumn("ProspectCustName"));
                    dt.Columns.Add(new DataColumn("Address"));
                    dt.Columns.Add(new DataColumn("Mobile"));
                    dt.Columns.Add(new DataColumn("Landline"));
                    dt.Columns.Add(new DataColumn("Email"));
                    dt.Columns.Add(new DataColumn("ModeOfContact"));
                    dt.Columns.Add(new DataColumn("PersonalResponsible"));
                    dt.Columns.Add(new DataColumn("BusinessType"));
                    dt.Columns.Add(new DataColumn("Branch"));
                    dt.Columns.Add(new DataColumn("Status"));
                    dt.Columns.Add(new DataColumn("LastCompletedAction"));
                    dt.Columns.Add(new DataColumn("NextAction"));
                    dt.Columns.Add(new DataColumn("Category"));
                    dt.Columns.Add(new DataColumn("ContactedDate"));
                    dt.Columns.Add(new DataColumn("ContactSummary"));

                    DataRow dr_export1 = dt.NewRow();
                    dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (leadid == Convert.ToInt32(dr["LeadId"]))
                        {
                            DataRow dr_export111 = dt.NewRow();
                            dr_export111["LeadID"] = "";
                            dr_export111["CreationDate"] = "";
                            dr_export111["ProspectCustName"] = "";
                            dr_export111["Address"] = "";
                            dr_export111["Mobile"] = "";
                            dr_export111["Landline"] = "";
                            dr_export111["Email"] = "";
                            dr_export111["ModeOfContact"] = "";
                            dr_export111["PersonalResponsible"] = "";
                            dr_export111["BusinessType"] = "";
                            dr_export111["Branch"] = "";
                            dr_export111["Email"] = "";
                            dr_export111["Status"] = "";
                            dr_export111["LastCompletedAction"] = "";
                            dr_export111["NextAction"] = "";
                            dr_export111["Category"] = "";
                            dr_export111["ContactedDate"] = dr["ContactedDate"];
                            dr_export111["ContactSummary"] = dr["ContactSummary"];
                            dt.Rows.Add(dr_export111);
                        }
                        else
                        {
                            DataRow dr_export = dt.NewRow();
                            dr_export["LeadID"] = dr["LeadID"];
                            dr_export["CreationDate"] = dr["CreationDate"];
                            dr_export["ProspectCustName"] = dr["ProspectCustName"];
                            dr_export["Address"] = dr["Address"];
                            dr_export["Mobile"] = dr["Mobile"];
                            dr_export["Landline"] = dr["Landline"];
                            dr_export["Email"] = dr["Email"];
                            dr_export["ModeOfContact"] = dr["ModeOfContact"];
                            dr_export["PersonalResponsible"] = dr["PersonalResponsible"];
                            dr_export["BusinessType"] = dr["BusinessType"];
                            dr_export["Branch"] = dr["Branch"];
                            dr_export["Email"] = dr["Email"];
                            dr_export["Status"] = dr["Status"];
                            dr_export["LastCompletedAction"] = dr["LastCompletedAction"];
                            dr_export["NextAction"] = dr["NextAction"];
                            dr_export["Category"] = dr["Category"];
                            dr_export["ContactedDate"] = dr["ContactedDate"];
                            dr_export["ContactSummary"] = dr["ContactSummary"];
                            dt.Rows.Add(dr_export);
                        }
                        leadid = Convert.ToInt32(dr["LeadId"]);
                    }

                    ExportToExcel(filename, dt);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }

            //if (dt.Rows.Count > 0)
            //{
            //    DataTable dt = ds.Tables[0];

            //    System.IO.StringWriter tw = new System.IO.StringWriter();
            //    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

            //    DataGrid dgGrid = new DataGrid();
            //    dgGrid.DataSource = dt;
            //    dgGrid.DataBind();

            //    //Get the HTML for the control.
            //    dgGrid.RenderControl(hw);

            //    //Write the HTML back to the browser.
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            //    this.EnableViewState = false;
            //    Response.Write(tw.ToString());
            //    Response.End();
            //    UpdatePanelPage.Update();
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(string filename, DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();
            dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
            dgGrid.HeaderStyle.Font.Bold = true;
            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void lnkAddBills_Click(object sender, EventArgs e)
    {
        /*
        pnlEdit.Visible = false;
        BusinessLogic bl = new BusinessLogic();
        string conn = GetConnectionString();
        ModalPopupExtender2.Show();
        pnlEdit.Visible = true;
        if (txtAmount.Text == "")
        {

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter the Receipt Amount before Adding BillNo')", true);
            //CnrfmDel.ConfirmText = "Please enter the Receipt Amount before Adding BillNo";
            //CnrfmDel.TargetControlID = "lnkAddBills";
            txtAmount.Focus();
            return;
        }

        if (ddReceivedFrom.SelectedValue == "0")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select the Customer before Adding Bills')", true);
            //pnlEdit.Visible = true;
            txtAmount.Focus();
            return;
        }

        if (GrdBills.Rows.Count == 0)
        {
            var ds = bl.GetReceivedAmountId(conn, 0);
            GrdBills.DataSource = ds;
            GrdBills.DataBind();
            GrdBills.Rows[0].Visible = false;
            checkPendingBills(ds);
        }
        pnlEdit.Visible = true;
        GrdBills.FooterRow.Visible = true;
        lnkAddBills.Visible = true;
        Session["RMode"] = "Add";
        //lnkBtnAdd.Visible = false;*/
    }

    protected void GrdViewProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(GetConnectionString());

                if (bl.CheckIfProductUsed(((HiddenField)e.Row.FindControl("ldgID")).Value))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "PRDMST"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "PRDMST"))
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

    protected void GrdViewProduct_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }

    protected void GrdViewProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //GridViewRow row = GrdViewLead.Rows[e.RowIndex];

            string ItemCode = Convert.ToString(GrdViewProduct.DataKeys[e.RowIndex].Value);
            
            string connection = Request.Cookies["Company"].Value;

            string usernam = Request.Cookies["LoggedUserName"].Value;

            BusinessLogic bl = new BusinessLogic(GetConnectionString());
            bl.DeleteProduct(connection, ItemCode, usernam);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Deleted Successfully')", true);
            BindGrid("", "");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewProduct.PageIndex = e.NewPageIndex;

            string textt = string.Empty;
            string dropd = string.Empty;

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;

            BindGrid(textt, dropd);
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
            GrdViewProduct.PageIndex = ((DropDownList)sender).SelectedIndex;

            string textt = string.Empty;
            string dropd = string.Empty;

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;

            BindGrid(textt, dropd);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = GrdViewProduct.SelectedRow;
            BusinessLogic bl = new BusinessLogic(GetConnectionString());
            string ItemCode = Convert.ToString(GrdViewProduct.SelectedDataKey.Value);

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            string connection = Request.Cookies["Company"].Value;
            DataSet ds = bl.GetProductForId(connection, ItemCode);

            Session["Method"] = "Edit";
            loadCategories();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    title1.Text = "Update Product details";

                    if (ds.Tables[0].Rows[0]["ItemCode"] != null)
                        txtItemCodeAdd.Text = ds.Tables[0].Rows[0]["ItemCode"].ToString();

                    txtItemCodeAdd.Enabled = false;

                    if (ds.Tables[0].Rows[0]["Stock"] != null)
                        txtStockAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Stock"]);

                    if (ds.Tables[0].Rows[0]["ProductName"] != null)
                        txtItemNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductName"]);

                    if (ds.Tables[0].Rows[0]["Model"] != null)
                        txtModelAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Model"]);

                    if (ds.Tables[0].Rows[0]["ProductDesc"] != null)
                        txtProdDescAdd.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ProductDesc"]);

                    //string ProductDesc;
                    //if ((ds.Tables[0].Rows[0]["ProductDesc"] != null) && (ds.Tables[0].Rows[0]["ProductDesc"].ToString() != ""))
                    //{
                    //    ProductDesc = ds.Tables[0].Rows[0]["ProductDesc"].ToString();
                    //    txtProdDescAdd.ClearSelection();
                    //    ListItem li = txtProdDescAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(ProductDesc.ToString()));
                    //    if (li != null) li.Selected = true;
                    //}

                    if (ds.Tables[0].Rows[0]["Rate"] != null)
                        txtUnitRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);

                    if (ds.Tables[0].Rows[0]["MRPEffDate"] != null)
                        txtMrpDateAdd.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["MRPEffDate"].ToString()).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["Unit"] != null)
                        txtUnitAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Unit"]);

                    if (ds.Tables[0].Rows[0]["VAT"] != null)
                        txtVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["VAT"]);


                    if (ds.Tables[0].Rows[0]["Discount"] != null)
                        txtDiscountAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);

                    if (ds.Tables[0].Rows[0]["BuyUnit"] != null)
                        txtBuyUnitAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["BuyUnit"]);

                    if (ds.Tables[0].Rows[0]["BuyVAT"] != null)
                        txtBuyVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["BuyVAT"]);

                    if (ds.Tables[0].Rows[0]["BuyRate"] != null)
                        txtBuyRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["BuyRate"]);

                    if (ds.Tables[0].Rows[0]["BuyDiscount"] != null)
                        txtBuyDiscountAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["BuyDiscount"]);

                    if (ds.Tables[0].Rows[0]["DealerUnit"] != null)
                        txtDealerUnitAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerUnit"]);

                    if (ds.Tables[0].Rows[0]["DealerVAT"] != null)
                        txtDealerVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerVAT"]);

                    if (ds.Tables[0].Rows[0]["CST"] != null)
                        txtCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"].ToString());

                    if (ds.Tables[0].Rows[0]["DealerRate"] != null)
                        txtDealerRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerRate"]);

                    if (ds.Tables[0].Rows[0]["DealerDiscount"] != null)
                        txtDealerDiscountAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerDiscount"]);

                    if (ds.Tables[0].Rows[0]["DPEffDate"] != null)
                        txtDpDateAdd.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DPEffDate"].ToString()).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["NLCEffDate"] != null)
                        txtNLCDateAdd.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["NLCEffDate"].ToString()).ToString("dd/MM/yyyy");

                    if (ds.Tables[0].Rows[0]["CommodityCode"] != null)
                        txtCommCodeAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CommodityCode"]);

                    if (ds.Tables[0].Rows[0]["BarCode"] != null)
                        txtBarcodeAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["BarCode"]);

                    if (ds.Tables[0].Rows[0]["NLC"] != null)
                        txtNLCAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["NLC"]);

                    if (ds.Tables[0].Rows[0]["productlevel"] != null)
                        txtproductlevel.Text = Convert.ToString(ds.Tables[0].Rows[0]["productlevel"]);

                    if (ds.Tables[0].Rows[0]["Deviation"] != null)
                        txtAllowedPriceAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Deviation"]);

                    if (ds.Tables[0].Rows[0]["ExecutiveCommission"] != null)
                        txtExecutiveCommissionAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["ExecutiveCommission"]);

                    int CategoryID = 0;
                    if ((ds.Tables[0].Rows[0]["CategoryID"] != null) && (ds.Tables[0].Rows[0]["CategoryID"].ToString() != ""))
                    {
                        CategoryID = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryID"].ToString());
                        ddCategoryAdd.ClearSelection();
                        ListItem li = ddCategoryAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(CategoryID.ToString()));
                        if (li != null) li.Selected = true;
                    }

                    string Accept_Role;
                    if ((ds.Tables[0].Rows[0]["Accept_Role"] != null) && (ds.Tables[0].Rows[0]["Accept_Role"].ToString() != ""))
                    {
                        Accept_Role = Convert.ToString(ds.Tables[0].Rows[0]["Accept_Role"].ToString());
                        drpRoleTypeAdd.ClearSelection();
                        ListItem li = drpRoleTypeAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(Accept_Role.ToString()));
                        if (li != null) li.Selected = true;
                    }

                    string Complex;
                    if ((ds.Tables[0].Rows[0]["Complex"] != null) && (ds.Tables[0].Rows[0]["Complex"].ToString() != ""))
                    {
                        Complex = Convert.ToString(ds.Tables[0].Rows[0]["Complex"].ToString());
                        drpComplexAdd.ClearSelection();
                        ListItem li = drpComplexAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(Complex.ToString()));
                        if (li != null) li.Selected = true;
                    }

                    string Measure_Unit;
                    if ((ds.Tables[0].Rows[0]["Measure_Unit"] != null) && (ds.Tables[0].Rows[0]["Measure_Unit"].ToString() != ""))
                    {
                        Measure_Unit = Convert.ToString(ds.Tables[0].Rows[0]["Measure_Unit"].ToString());
                        drpMeasureAdd.ClearSelection();
                        ListItem li = drpMeasureAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(Measure_Unit.ToString()));
                        if (li != null) li.Selected = true;
                    }

                    string IsActive;
                    if ((ds.Tables[0].Rows[0]["IsActive"] != null) && (ds.Tables[0].Rows[0]["IsActive"].ToString() != ""))
                    {
                        IsActive = Convert.ToString(ds.Tables[0].Rows[0]["IsActive"].ToString());
                        drpIsActiveAdd.ClearSelection();
                        ListItem li = drpIsActiveAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(IsActive.ToString()));
                        if (li != null) li.Selected = true;
                    }

                    string Outdated;
                    if ((ds.Tables[0].Rows[0]["Outdated"] != null) && (ds.Tables[0].Rows[0]["Outdated"].ToString() != ""))
                    {
                        Outdated = Convert.ToString(ds.Tables[0].Rows[0]["Outdated"].ToString());
                        drpOutdatedAdd.ClearSelection();
                        ListItem li = drpOutdatedAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(Outdated.ToString()));
                        if (li != null) li.Selected = true;
                    }

                    string block;
                    if ((ds.Tables[0].Rows[0]["block"] != null) && (ds.Tables[0].Rows[0]["block"].ToString() != ""))
                    {
                        block = Convert.ToString(ds.Tables[0].Rows[0]["block"].ToString());
                        drpblockadd.ClearSelection();
                        ListItem li = drpblockadd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(block.ToString()));
                        if (li != null) li.Selected = true;
                    }


                    DataSet dst = bl.ListProductPrices(connection, ItemCode);
                    if (dst != null && dst.Tables[0].Rows.Count > 0)
                    {
                        GrdViewItems.DataSource = dst.Tables[0];
                        GrdViewItems.DataBind();                            
                    }
                    else
                    {
                        GrdViewItems.DataSource = null;
                        GrdViewItems.DataBind();
                    }

                    DataSet dstt = bl.ListProductPriceHistory(connection, ItemCode);
                    if (dstt != null && dstt.Tables[0].Rows.Count > 0)
                    {
                        DataTable dttt;
                        DataRow drNew;
                        DataColumn dct;
                        DataSet dstd = new DataSet();
                        dttt = new DataTable();

                        dct = new DataColumn("ID");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Row");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("PriceName");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Price");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("EffDate");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Discount");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("UserName");
                        dttt.Columns.Add(dct);

                        dstd.Tables.Add(dttt);

                        int sno = 1;
                        if (dstt != null)
                        {
                            if (dstt.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                                {
                                    drNew = dttt.NewRow();
                                    drNew["Row"] = sno;
                                    drNew["ID"] = Convert.ToInt32(dstt.Tables[0].Rows[i]["ID"]);
                                    drNew["PriceName"] = Convert.ToString(dstt.Tables[0].Rows[i]["PriceName"]);
                                    drNew["Price"] = Convert.ToDouble(dstt.Tables[0].Rows[i]["Price"]);

                                    string dtaa = Convert.ToDateTime(dstt.Tables[0].Rows[i]["EffDate"]).ToString("dd/MM/yyyy");

                                    drNew["EffDate"] = dtaa;
                                    drNew["Discount"] = Convert.ToDouble(dstt.Tables[0].Rows[i]["Discount"]);
                                    drNew["UserName"] = Convert.ToString(dstt.Tables[0].Rows[i]["UserName"]);
                                    dstd.Tables[0].Rows.Add(drNew);
                                    sno = sno + 1;
                                }
                            }
                        }

                        BulkEditGridView1.DataSource = dstd.Tables[0];
                        BulkEditGridView1.DataBind();                            
                    }
                    else
                    {
                        BulkEditGridView1.DataSource = null;
                        BulkEditGridView1.DataBind();
                    }
                    

                    UpdateButton.Visible = true;
                    AddButton.Visible = false;
                    ModalPopupExtender2.Show();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GrdViewProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewProduct, e.Row, this);
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
            ModalPopupExtender2.Hide();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        DateTime creationDate;
        int LeadID = 0;
        DataSet dsContact;

        try
        {
            //if (Session["contactDs"] == null || ((DataSet)Session["contactDs"]).Tables[0].Rows.Count < 1)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter atleast one Lead Contact and try again.')", true);
            //    return;
            //}

            if (Page.IsValid)
            {
               
                GridViewRow row = GrdViewProduct.SelectedRow;

                string No = Convert.ToString(GrdViewProduct.SelectedDataKey.Value);

                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                string ItemCode = string.Empty;
                string ProductName = string.Empty;
                string Model = string.Empty;
                int CategoryID = 0;
                string ProductDesc = string.Empty;
                int ROL = 0;
                double Stock = 0;
                double Rate = 0;
                int Unit = 0;
                int BuyUnit = 0;
                double VAT = 0;
                int Discount = 0;
                double BuyRate = 0;
                double BuyVAT = 0;
                int BuyDiscount = 0;
                int DealerUnit = 0;
                double DealerRate = 0;
                double DealerVAT = 0;
                int DealerDiscount = 0;
                string Complex = string.Empty;
                string Measure_Unit = string.Empty;
                string Accept_Role = string.Empty;
                double CST = 0;
                string Barcode = string.Empty;
                Double ExecutiveCommission = 0;
                string CommodityCode = "";
                double NLC = 0;
                string block = string.Empty;
                int productlevel = 0;
                DateTime MRPEffDate;
                DateTime DPEffDate;
                DateTime NLCEffDate;
                string Outdated = string.Empty;
                int Deviation = 0;
                string IsActive = string.Empty;

                string Username = Request.Cookies["LoggedUserName"].Value;

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string PriceName = "";
                string Price = "";
                string EffDate = "";

                string tDiscount = "";

                for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                {
                    int col = vLoop + 1;

                    TextBox Price1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtPrice");
                    Price = Price1.Text;
                    TextBox EffDate1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtEffDate");
                    EffDate = EffDate1.Text;
                    TextBox Discount1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDiscount1");
                    tDiscount = Discount1.Text;

                    if (Price == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Price in row " + col + " ');", true);
                        return;
                    }
                    else if (EffDate == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Effective Date in row " + col + " ');", true);
                        return;
                    }
                    else if (tDiscount == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Discount in row " + col + " ');", true);
                        return;
                    }
                }



                DataSet dstest = new DataSet();
                string ID = "";

                DataTable dtt;
                DataRow drNewt;
                DataColumn dct;

                dtt = new DataTable();

                dct = new DataColumn("ID");
                dtt.Columns.Add(dct);

                dct = new DataColumn("PriceName");
                dtt.Columns.Add(dct);

                dct = new DataColumn("Price");
                dtt.Columns.Add(dct);

                dct = new DataColumn("EffDate");
                dtt.Columns.Add(dct);

                dct = new DataColumn("Discount");
                dtt.Columns.Add(dct);

                dstest.Tables.Add(dtt);


                for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                {
                    TextBox txt1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtId");
                    ID = txt1.Text;
                    Label PriceName1 = (Label)GrdViewItems.Rows[vLoop].FindControl("txtPriceName");
                    PriceName = PriceName1.Text;
                    TextBox Price1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtPrice");
                    Price = Price1.Text;
                    TextBox EffDate1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtEffDate");
                    EffDate = EffDate1.Text;
                    TextBox Discount1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDiscount1");
                    tDiscount = Discount1.Text;

                    drNewt = dtt.NewRow();
                    drNewt["ID"] = ID;
                    drNewt["PriceName"] = PriceName;
                    drNewt["Price"] = Price;
                    drNewt["EffDate"] = EffDate;
                    drNewt["Discount"] = tDiscount;
                    dstest.Tables[0].Rows.Add(drNewt);
                }

                CategoryID = Convert.ToInt32(ddCategoryAdd.SelectedValue);
                ItemCode = txtItemCodeAdd.Text.Trim();
                Stock = Convert.ToDouble(txtStockAdd.Text);
                ProductName = txtItemNameAdd.Text.Trim();
                ROL = Convert.ToInt32(txtROLAdd.Text);
                Model = txtModelAdd.Text.Trim();
                ProductDesc = txtProdDescAdd.SelectedValue;
                Rate = Convert.ToDouble(txtUnitRateAdd.Text);
                MRPEffDate = DateTime.Parse(txtMrpDateAdd.Text);
                Unit = Convert.ToInt32(txtUnitAdd.Text);
                VAT = Convert.ToDouble(txtVATAdd.Text);
                Discount = Convert.ToInt32(txtDiscountAdd.Text);
                BuyUnit = Convert.ToInt32(txtBuyUnitAdd.Text);
                BuyVAT = Convert.ToDouble(txtBuyVATAdd.Text);
                BuyRate = Convert.ToDouble(txtBuyRateAdd.Text);
                BuyDiscount = Convert.ToInt32(txtBuyDiscountAdd.Text);
                DealerUnit = Convert.ToInt32(txtDealerUnitAdd.Text);
                DealerVAT = Convert.ToDouble(txtDealerVATAdd.Text);
                CST = Convert.ToDouble(txtCSTAdd.Text);
                DealerRate = Convert.ToDouble(txtDealerRateAdd.Text);
                DPEffDate = DateTime.Parse(txtDpDateAdd.Text);
                DealerDiscount = Convert.ToInt32(txtDealerDiscountAdd.Text);
                Measure_Unit = drpMeasureAdd.SelectedValue;
                Complex = drpComplexAdd.SelectedValue;
                Accept_Role = drpRoleTypeAdd.SelectedValue;
                Barcode = txtBarcodeAdd.Text.Trim();
                CommodityCode = txtCommCodeAdd.Text.Trim();
                ExecutiveCommission = Convert.ToDouble(txtExecutiveCommissionAdd.Text);
                NLC = Convert.ToDouble(txtNLCAdd.Text);
                NLCEffDate = DateTime.Parse(txtNLCDateAdd.Text);
                block = drpblockadd.SelectedValue;
                productlevel = Convert.ToInt32(txtproductlevel.Text);
                Outdated = drpOutdatedAdd.SelectedValue;
                Deviation = Convert.ToInt32(txtAllowedPriceAdd.Text);
                IsActive = drpIsActiveAdd.SelectedValue;


                bl.UpdateProduct(connection, ItemCode, ProductName, Model, CategoryID, ProductDesc, ROL, Stock, Rate, Unit, VAT, Discount, BuyRate, BuyVAT, BuyDiscount, BuyUnit, DealerUnit, DealerRate, DealerVAT, DealerDiscount, Complex, Measure_Unit, Accept_Role, CST, Barcode, ExecutiveCommission, CommodityCode, NLC, block, productlevel, MRPEffDate, DPEffDate, NLCEffDate, Username, Outdated, Deviation, IsActive,dstest);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Updated successfully.')", true);

                BindGrid("", "");
                UpdatePanelPage.Update();
                GrdViewProduct.DataBind();

                ModalPopupExtender2.Hide();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }
    }

    protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[3].Visible = false;
            //TextBox txt = (TextBox)e.Row.FindControl("txtDate");
            //TextBox txtet = (TextBox)e.Row.FindControl("txtResult");
            //if ((txt.Text != "") || (txtet.Text != ""))
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Blue;
            //}
            //else
            //{
            //    e.Row.ForeColor = System.Drawing.Color.SteelBlue;
            //}
        }
    }

    protected void drpPaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if ((drpPaymode.SelectedValue == "2") || (drpPaymode.SelectedValue == "3"))
            //{
            //    TabPanel2.Visible = true;
            //    TabPanel2.Enabled = true;
            //    if (drpPaymode.SelectedValue == "2")
            //    {
            //        lllklkl.Visible = true;
            //        lllklkll.Visible = false;
            //        lllklklll.Visible = false;
            //    }
            //    else if (drpPaymode.SelectedValue == "3")
            //    {
            //        lllklkl.Visible = true;
            //        lllklkll.Visible = true;
            //        lllklklll.Visible = true;
            //    }
            //}
            //else
            //    TabPanel2.Visible = false;
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
        try
        {
            //int iLedgerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
            //BusinessLogic bl = new BusinessLogic(sDataSource);

            //DataSet customerDs = bl.getAddressInfo(iLedgerID);
            //string address = string.Empty;

            //if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            //{
            //    if (customerDs.Tables[0].Rows[0]["Add1"] != null)
            //        txtMobile.Text = Convert.ToString(customerDs.Tables[0].Rows[0]["Mobile"]);
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadBanks()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = new DataSet();
        //ds = bl.ListBankLedgerpaymnet();
        //drpBankName.DataSource = ds;
        //drpBankName.DataTextField = "LedgerName";
        //drpBankName.DataValueField = "LedgerID";
        //drpBankName.DataBind();

    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        try
        { 
        DateTime creationDate;

            if (Page.IsValid)
            {
                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                string ItemCode = string.Empty;
                string ProductName = string.Empty;
                string Model = string.Empty;
                int CategoryID = 0; 
                string ProductDesc = string.Empty; 
                int ROL = 0;
                double Stock = 0;
                double Rate = 0;
                int Unit = 0;
                int BuyUnit = 0;
                double VAT = 0;
                int Discount = 0;
                double BuyRate = 0;
                double BuyVAT = 0; 
                int BuyDiscount = 0; 
                int DealerUnit = 0;
                double DealerRate = 0; 
                double DealerVAT = 0;
                int DealerDiscount = 0; 
                string Complex  = string.Empty; 
                string Measure_Unit  = string.Empty; 
                string Accept_Role  = string.Empty; 
                double CST = 0;
                string Barcode  = string.Empty; 
                Double ExecutiveCommission = 0;
                string CommodityCode = "";
                double NLC = 0;
                string block = string.Empty; 
                int productlevel = 0; 
                DateTime MRPEffDate; 
                DateTime DPEffDate; 
                DateTime NLCEffDate; 
                string Outdated = string.Empty; 
                int Deviation = 0;
                string IsActive = string.Empty; 

                string Username = Request.Cookies["LoggedUserName"].Value;

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string PriceName = "";
                string Price = "";
                string EffDate = "";

                string ItemCod = txtItemCodeAdd.Text.Trim();
                if (bl.CheckIfItemCodeDuplicate(ItemCod))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product code \\'" + ItemCod + "\\' already exists in the product master.');", true);
                    return;
                }

                string tDiscount = "";
                
                    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                    {                        
                        TextBox Price1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtPrice");
                        Price = Price1.Text;
                        TextBox EffDate1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtEffDate");
                        EffDate = EffDate1.Text;
                        TextBox Discount1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDiscount1");
                        tDiscount = Discount1.Text;

                        int col = vLoop + 1;

                        if (Price == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Price in row " + col + " ');", true);
                            return;
                        }
                        else if (EffDate == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Effective Date in row " + col + " ');", true);
                            return;
                        }
                        else if (tDiscount == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Discount in row " + col + " ');", true);
                            return;
                        }                        
                    }



                DataSet dstest = new DataSet();
                string ID = "";

                DataTable dtt;
                DataRow drNewt;
                DataColumn dct;
                    
                dtt = new DataTable();

                dct = new DataColumn("ID");
                dtt.Columns.Add(dct);

                dct = new DataColumn("PriceName");
                dtt.Columns.Add(dct);

                dct = new DataColumn("Price");
                dtt.Columns.Add(dct);

                dct = new DataColumn("EffDate");
                dtt.Columns.Add(dct);

                dct = new DataColumn("Discount");
                dtt.Columns.Add(dct);

                dstest.Tables.Add(dtt);

                
                for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                {
                    TextBox txt1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtId");
                    ID = txt1.Text;
                    Label PriceName1 = (Label)GrdViewItems.Rows[vLoop].FindControl("txtPriceName");
                    PriceName = PriceName1.Text;
                    TextBox Price1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtPrice");
                    Price = Price1.Text;
                    TextBox EffDate1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtEffDate");
                    EffDate = EffDate1.Text;
                    TextBox Discount1 = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDiscount1");
                    tDiscount = Discount1.Text;
                    
                    drNewt = dtt.NewRow();
                    drNewt["ID"] = ID;
                    drNewt["PriceName"] = PriceName;
                    drNewt["Price"] = Price;
                    drNewt["EffDate"] = EffDate;
                    drNewt["Discount"] = tDiscount;
                    dstest.Tables[0].Rows.Add(drNewt);
                }
                
                CategoryID = Convert.ToInt32(ddCategoryAdd.SelectedValue);
                ItemCode = txtItemCodeAdd.Text.Trim();
                Stock = 0;
                ProductName = txtItemNameAdd.Text.Trim();
                ROL = Convert.ToInt32(txtROLAdd.Text);
                Model = txtModelAdd.Text.Trim();
                ProductDesc = txtProdDescAdd.SelectedValue;
                Rate = Convert.ToDouble(txtUnitRateAdd.Text);
                MRPEffDate = DateTime.Parse(txtMrpDateAdd.Text);
                Unit = Convert.ToInt32(txtUnitAdd.Text);
                VAT = Convert.ToDouble(txtVATAdd.Text);
                Discount = Convert.ToInt32(txtDiscountAdd.Text);
                BuyUnit = Convert.ToInt32(txtBuyUnitAdd.Text);
                BuyVAT = Convert.ToDouble(txtBuyVATAdd.Text);
                BuyRate = Convert.ToDouble(txtBuyRateAdd.Text);
                BuyDiscount = Convert.ToInt32(txtBuyDiscountAdd.Text);
                DealerUnit = Convert.ToInt32(txtDealerUnitAdd.Text);
                DealerVAT = Convert.ToDouble(txtDealerVATAdd.Text);
                CST = Convert.ToDouble(txtCSTAdd.Text);
                DealerRate = Convert.ToDouble(txtDealerRateAdd.Text);
                DPEffDate = DateTime.Parse(txtDpDateAdd.Text);
                DealerDiscount = Convert.ToInt32(txtDealerDiscountAdd.Text);
                Measure_Unit = drpMeasureAdd.SelectedValue;        
                Complex = drpComplexAdd.SelectedValue;
                Accept_Role = drpRoleTypeAdd.SelectedValue;
                Barcode = txtBarcodeAdd.Text.Trim();      
                CommodityCode = txtCommCodeAdd.Text.Trim();  
                ExecutiveCommission = Convert.ToDouble(txtExecutiveCommissionAdd.Text);        
                NLC = Convert.ToDouble(txtNLCAdd.Text);
                NLCEffDate = DateTime.Parse(txtNLCDateAdd.Text);
                block = drpblockadd.SelectedValue;
                productlevel = Convert.ToInt32(txtproductlevel.Text);         
                Outdated = drpOutdatedAdd.SelectedValue;
                Deviation = Convert.ToInt32(txtAllowedPriceAdd.Text);       
                IsActive = drpIsActiveAdd.SelectedValue;

                bl.InsertProduct(connection, ItemCode, ProductName, Model, CategoryID, ProductDesc, ROL, Stock, Rate, Unit, BuyUnit, VAT, Discount, BuyRate, BuyVAT, BuyDiscount, DealerUnit, DealerRate, DealerVAT, DealerDiscount, Complex, Measure_Unit, Accept_Role, CST, Barcode, ExecutiveCommission, CommodityCode, NLC, block, productlevel, MRPEffDate, DPEffDate, NLCEffDate, Username, Outdated, Deviation, IsActive, dstest);

                GrdViewProduct.DataBind();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Details saved successfully.')", true);

                BindGrid("", "");
                UpdatePanelPage.Update();

                ModalPopupExtender2.Hide();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }

    }

    private void loadCategories()
    {
        string method = string.Empty;

        if (Session["Method"] == "Add")
        {
            method = "Add";
        }
        else if (Session["Method"] == "Edit")
        {
            method = "Edit";
        }

        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic();
        DataSet ds = new DataSet();
        ddCategoryAdd.Items.Clear();
        ds = bl.ListCategory(sDataSource, method);
        ddCategoryAdd.Items.Insert(0, new ListItem("Select Category", "0"));
        ddCategoryAdd.DataTextField = "CategoryName";
        ddCategoryAdd.DataValueField = "CategoryID";
        ddCategoryAdd.DataSource = ds;
        ddCategoryAdd.DataBind();

        DataSet dst = new DataSet();
        txtProdDescAdd.Items.Clear();
        dst = bl.ListBrandsProducts(sDataSource, method);
        txtProdDescAdd.Items.Insert(0, new ListItem("Select Brand", "0"));
        txtProdDescAdd.DataTextField = "BrandName";
        txtProdDescAdd.DataValueField = "BrandName";
        txtProdDescAdd.DataSource = dst;
        txtProdDescAdd.DataBind();
    }

}