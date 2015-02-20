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

public partial class DeleteItems : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            lblErr.Text = "";

            if (!Page.IsPostBack)
            {
                rdoType.SelectedValue = "Payment";
                divPayment.Visible = true;
                divReceipt.Visible = false;
                divJournal.Visible = false;
                divPurchase.Visible = false;
                divSales.Visible = false;
                BindPayment();
                divPaymentResult.Visible = true;

                if (CheckTblBundle())
                {
                    if (rdoType.Items.FindByValue("Purchase") != null)
                        rdoType.Items.Remove(new ListItem("Purchase"));

                    if (rdoType.Items.FindByValue("Sales") != null)
                        rdoType.Items.Remove(new ListItem("Sales"));

                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private bool CheckTblBundle()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        BusinessLogic bl = new BusinessLogic(sDataSource);

        if (bl.ChkTableBundle() > 0)
            return true;
        else
            return false;

    }

    protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdoType.SelectedValue == "Payment")
            {
                divPayment.Visible = true;
                divReceipt.Visible = false;
                divJournal.Visible = false;
                divPurchase.Visible = false;
                divSales.Visible = false;

                divPaymentResult.Visible = true;

                GrdViewJournal.DataSource = null;
                GrdViewJournal.DataBind();

                GrdViewPurchase.DataSource = null;
                GrdViewPurchase.DataBind();

                GrdViewReceipt.DataSource = null;
                GrdViewReceipt.DataBind();

                GrdViewSales.DataSource = null;
                GrdViewSales.DataBind();

                ChkSales.Checked = false;
                ChkJournal.Checked = false;
                ChkPurchase.Checked = false;
                ChkReceipt.Checked = false;

                BindPayment();

            }
            else if (rdoType.SelectedValue == "Receipt")
            {
                divPayment.Visible = false;
                divReceipt.Visible = true;
                divJournal.Visible = false;
                divPurchase.Visible = false;
                divSales.Visible = false;

                ChkSales.Checked = false;
                ChkJournal.Checked = false;
                ChkPurchase.Checked = false;
                ChkPayDel.Checked = false;

                divPaymentResult.Visible = false;
                divJournalResult.Visible = false;
                divPurchaseResult.Visible = false;
                divReceiptResult.Visible = true;
                divSalesResult.Visible = false;

                GrdViewPayment.DataSource = null;
                GrdViewPayment.DataBind();

                GrdViewJournal.DataSource = null;
                GrdViewJournal.DataBind();

                GrdViewPurchase.DataSource = null;
                GrdViewPurchase.DataBind();


                GrdViewSales.DataSource = null;
                GrdViewSales.DataBind();

                BindReceipt();
            }
            else if (rdoType.SelectedValue == "Journal")
            {
                divPayment.Visible = false;
                divReceipt.Visible = false;
                divJournal.Visible = true;
                divPurchase.Visible = false;
                divSales.Visible = false;

                ChkSales.Checked = false;
                ChkPayDel.Checked = false;
                ChkPurchase.Checked = false;
                ChkReceipt.Checked = false;

                divPaymentResult.Visible = false;
                divJournalResult.Visible = true;
                divPurchaseResult.Visible = false;
                divReceiptResult.Visible = false;
                divSalesResult.Visible = false;

                GrdViewPayment.DataSource = null;
                GrdViewPayment.DataBind();

                GrdViewPurchase.DataSource = null;
                GrdViewPurchase.DataBind();

                GrdViewReceipt.DataSource = null;
                GrdViewReceipt.DataBind();

                GrdViewSales.DataSource = null;
                GrdViewSales.DataBind();

                BindJournal();
            }
            else if (rdoType.SelectedValue == "Purchase")
            {
                divPayment.Visible = false;
                divReceipt.Visible = false;
                divJournal.Visible = false;
                divPurchase.Visible = true;
                divSales.Visible = false;

                divPaymentResult.Visible = false;
                divJournalResult.Visible = false;
                divPurchaseResult.Visible = true;
                divReceiptResult.Visible = false;
                divSalesResult.Visible = false;

                ChkSales.Checked = false;
                ChkJournal.Checked = false;
                ChkPayDel.Checked = false;
                ChkReceipt.Checked = false;

                GrdViewPayment.DataSource = null;
                GrdViewPayment.DataBind();

                GrdViewJournal.DataSource = null;
                GrdViewJournal.DataBind();

                GrdViewReceipt.DataSource = null;
                GrdViewReceipt.DataBind();

                GrdViewSales.DataSource = null;
                GrdViewSales.DataBind();

                BindPurchase("0");
            }
            else
            {
                divReceipt.Visible = false;
                divPayment.Visible = false;
                divJournal.Visible = false;
                divPurchase.Visible = false;
                divSales.Visible = true;

                ChkPayDel.Checked = false;
                ChkJournal.Checked = false;
                ChkPurchase.Checked = false;
                ChkReceipt.Checked = false;

                divPaymentResult.Visible = false;
                divJournalResult.Visible = false;
                divPurchaseResult.Visible = false;
                divReceiptResult.Visible = false;
                divSalesResult.Visible = true;

                GrdViewPayment.DataSource = null;
                GrdViewPayment.DataBind();

                GrdViewJournal.DataSource = null;
                GrdViewJournal.DataBind();

                GrdViewPurchase.DataSource = null;
                GrdViewPurchase.DataBind();

                GrdViewReceipt.DataSource = null;
                GrdViewReceipt.DataBind();


                BindSales(0);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }






    #region Grid Events

    #region Payment Grid Events

    protected void GrdViewPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int TransNo = (int)GrdViewPayment.DataKeys[e.RowIndex].Value;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.HidePayment(TransNo);
            BindPayment();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int TempTransNo = (Int32)GrdViewPayment.SelectedDataKey.Value;

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            bl.RestorePayment(TempTransNo);
            BindPayment();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewPayment.PageIndex = e.NewPageIndex;
            BindPayment();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    #endregion

    #region Sales Grid Events
    protected void GrdViewSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewSales.PageIndex = e.NewPageIndex;
            int strBillno = 0;
            if (txtBillnoSalesSearch.Text.Trim() != "")
                strBillno = Convert.ToInt32(txtBillnoSalesSearch.Text.Trim());
            else
                strBillno = 0;
            BindSales(strBillno);
            //errPanel.Visible = false;
            //ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewSales_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int Billno = (int)GrdViewSales.DataKeys[e.RowIndex].Value;

            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.HideSales(Billno);
            BindSales(0);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public DataSet formProduct(int salesID)
    {
        DataSet ds;
        DataSet dsRole;
        DataSet itemDs = new DataSet();
        DataTable dt;
        DataRow dr;
        DataColumn dc;

        double dTotal = 0;
        double dQty = 0;
        double dRate = 0;
        string strRole = string.Empty;
        string roleFlag = string.Empty;
        string strBundles = string.Empty;
        int iBundle = 0;
        int iRod = 0;

        double stock = 0;
        int billno = 0;
        string strItemCode = string.Empty;
        Session["roleDs"] = null;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetSalesItemsForId(salesID);


        if (ds != null)
        {
            dt = new DataTable();

            dc = new DataColumn("itemCode");
            dt.Columns.Add(dc);

            dc = new DataColumn("Billno");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductName");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductDesc");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Discount");
            dt.Columns.Add(dc);

            dc = new DataColumn("VAT");
            dt.Columns.Add(dc);

            dc = new DataColumn("CST");
            dt.Columns.Add(dc);

            dc = new DataColumn("Roles");
            dt.Columns.Add(dc);

            dc = new DataColumn("IsRole");
            dt.Columns.Add(dc);

            dc = new DataColumn("Bundles");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rods");
            dt.Columns.Add(dc);
            dc = new DataColumn("Total");
            dt.Columns.Add(dc);



            itemDs.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    dr = itemDs.Tables[0].NewRow();

                    if (dR["Qty"] != null)
                        dQty = Convert.ToDouble(dR["Qty"]);
                    if (dR["Rate"] != null)
                        dRate = Convert.ToDouble(dR["Rate"]);

                    dTotal = dQty * dRate;
                    if (dR["ItemCode"] != null)
                    {
                        strItemCode = Convert.ToString(dR["ItemCode"]);
                        dr["itemCode"] = strItemCode;
                    }
                    if (dR["Billno"] != null)
                    {
                        billno = Convert.ToInt32(dR["Billno"]);
                        dr["Billno"] = Convert.ToString(billno);
                    }
                    if (dR["ProductName"] != null)
                        dr["ProductName"] = Convert.ToString(dR["ProductName"]);
                    if (dR["ProductDesc"] != null)
                        dr["ProductDesc"] = Convert.ToString(dR["ProductDesc"]);
                    dr["Qty"] = dQty.ToString();
                    dr["Rate"] = dRate.ToString();
                    if (dR["Discount"] != null)
                        dr["Discount"] = Convert.ToString(dR["Discount"]);
                    if (dR["VAT"] != null)
                        dr["VAT"] = Convert.ToString(dR["VAT"]);

                    if (dR["CST"] != null)
                        dr["CST"] = Convert.ToString(dR["CST"]);

                    if (dR["Bundles"] != null)
                        dr["Bundles"] = Convert.ToString(dR["Bundles"]);
                    if (dR["Rods"] != null)
                        dr["Rods"] = Convert.ToString(dR["Rods"]);
                    if (dR["isrole"] != null)
                    {
                        roleFlag = Convert.ToString(dR["isrole"]);
                        dr["IsRole"] = roleFlag;

                    }

                    //
                    if (roleFlag == "Y")
                    {

                        dsRole = bl.ListRoles(billno, strItemCode);
                        if (dsRole != null)
                        {
                            if (dsRole.Tables[0] != null)
                            {
                                foreach (DataRow drole in dsRole.Tables[0].Rows)
                                {
                                    strRole = strRole + drole["RoleID"].ToString() + "_" + drole["Qty"].ToString() + ",";

                                }

                            }
                        }


                        if (strRole.EndsWith(","))
                        {
                            strRole = strRole.Remove(strRole.Length - 1, 1);
                        }

                    }
                    else
                    {
                        strRole = "NO ROLE";
                    }

                    //if (hdStock.Value != "")
                    //    stock = Convert.ToDouble(hdStock.Value);
                    dr["Roles"] = strRole;
                    dr["Total"] = Convert.ToString(dTotal);
                    itemDs.Tables[0].Rows.Add(dr);
                    strRole = "";
                }
            }
        }
        return itemDs;


    }
    public DataSet formTempProduct(int salesID)
    {
        DataSet ds;
        DataSet dsRole;
        DataSet itemDs = new DataSet();
        DataTable dt;
        DataRow dr;
        DataColumn dc;

        double dTotal = 0;
        double dQty = 0;
        double dRate = 0;
        string strRole = string.Empty;
        string roleFlag = string.Empty;
        string strBundles = string.Empty;
        int iBundle = 0;
        int iRod = 0;

        double stock = 0;
        int billno = 0;
        string strItemCode = string.Empty;
        Session["roleDs"] = null;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetTempSalesItemsForId(salesID);


        if (ds != null)
        {
            dt = new DataTable();

            dc = new DataColumn("itemCode");
            dt.Columns.Add(dc);

            dc = new DataColumn("Billno");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductName");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductDesc");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Discount");
            dt.Columns.Add(dc);

            dc = new DataColumn("VAT");
            dt.Columns.Add(dc);

            dc = new DataColumn("CST");
            dt.Columns.Add(dc);

            dc = new DataColumn("Roles");
            dt.Columns.Add(dc);

            dc = new DataColumn("IsRole");
            dt.Columns.Add(dc);

            dc = new DataColumn("Bundles");
            dt.Columns.Add(dc);

            dc = new DataColumn("Rods");
            dt.Columns.Add(dc);
            dc = new DataColumn("Total");
            dt.Columns.Add(dc);



            itemDs.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    dr = itemDs.Tables[0].NewRow();

                    if (dR["Qty"] != null)
                        dQty = Convert.ToDouble(dR["Qty"]);
                    if (dR["Rate"] != null)
                        dRate = Convert.ToDouble(dR["Rate"]);

                    dTotal = dQty * dRate;
                    if (dR["ItemCode"] != null)
                    {
                        strItemCode = Convert.ToString(dR["ItemCode"]);
                        dr["itemCode"] = strItemCode;
                    }
                    if (dR["Billno"] != null)
                    {
                        billno = Convert.ToInt32(dR["Billno"]);
                        dr["Billno"] = Convert.ToString(billno);
                    }
                    if (dR["ProductName"] != null)
                        dr["ProductName"] = Convert.ToString(dR["ProductName"]);
                    if (dR["ProductDesc"] != null)
                        dr["ProductDesc"] = Convert.ToString(dR["ProductDesc"]);
                    dr["Qty"] = dQty.ToString();
                    dr["Rate"] = dRate.ToString();
                    if (dR["Discount"] != null)
                        dr["Discount"] = Convert.ToString(dR["Discount"]);
                    if (dR["VAT"] != null)
                        dr["VAT"] = Convert.ToString(dR["VAT"]);

                    if (dR["CST"] != null)
                        dr["CST"] = Convert.ToString(dR["CST"]);

                    if (dR["Bundles"] != null)
                        dr["Bundles"] = Convert.ToString(dR["Bundles"]);
                    if (dR["Rods"] != null)
                        dr["Rods"] = Convert.ToString(dR["Rods"]);
                    if (dR["isrole"] != null)
                    {
                        roleFlag = Convert.ToString(dR["isrole"]);
                        dr["IsRole"] = roleFlag;

                    }

                    //
                    if (roleFlag == "Y")
                    {

                        dsRole = bl.ListTempRoles(billno, strItemCode);
                        if (dsRole != null)
                        {
                            if (dsRole.Tables[0] != null)
                            {
                                foreach (DataRow drole in dsRole.Tables[0].Rows)
                                {
                                    strRole = strRole + drole["RoleID"].ToString() + "_" + drole["Qty"].ToString() + ",";

                                }

                            }
                        }


                        if (strRole.EndsWith(","))
                        {
                            strRole = strRole.Remove(strRole.Length - 1, 1);
                        }

                    }
                    else
                    {
                        strRole = "NO ROLE";
                    }

                    //if (hdStock.Value != "")
                    //    stock = Convert.ToDouble(hdStock.Value);
                    dr["Roles"] = strRole;
                    dr["Total"] = Convert.ToString(dTotal);
                    itemDs.Tables[0].Rows.Add(dr);
                    strRole = "";
                }
            }
        }
        return itemDs;


    }
    protected void GrdViewSales_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int TempBillno = (Int32)GrdViewSales.SelectedDataKey.Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int result = bl.RestoreSales(TempBillno);
            if (result == -1)
            {
                lblErr.Text = "When restoring this sales bill some items will go to negative stock. *** Restoring is allowed only it has sufficient stock for the items associated with this bill ***";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Hide is Not allowed for this transaction.')", true);
                return;
            }
            else
            {
                lblErr.Text = "";
            }
            BindSales(0);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    #endregion

    #region Purchase Grid Events
    protected void GrdViewPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewPurchase.PageIndex = e.NewPageIndex;
            String strBillno = string.Empty;
            if (txtBillnoPurchaseSearch.Text.Trim() != "")
                strBillno = txtBillnoPurchaseSearch.Text.Trim();
            else
                strBillno = "0";
            BindPurchase(strBillno);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int purchaseID = (int)GrdViewPurchase.DataKeys[e.RowIndex].Value;
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int del = bl.HidePurchase(purchaseID);
            if (del == -1)
            {
                lblErr.Text = "When hiding this purchase bill some items will go to negative stock.*** Hiding is allowed only it has sufficient stock for the items associated with this bill ***";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Hide is Not allowed for this transaction.')", true);
                return;
            }
            else
            {
                lblErr.Text = "";
            }

            BindPurchase("0");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewPurchase_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int TempPurchaseID = (Int32)GrdViewPurchase.SelectedDataKey.Value;
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");
            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.RestorePurhase(TempPurchaseID);
            BindPurchase("0");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    #endregion

    #region Journal Grid Events
    protected void GrdViewJournal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewJournal.PageIndex = e.NewPageIndex;
            BindJournal();
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
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            BusinessLogic bl = new BusinessLogic(sDataSource);

            bl.HideJournal(TransNo);
            BindJournal();
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
            int TempTransNo = (Int32)GrdViewJournal.SelectedDataKey.Value;

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            bl.RestoreJournal(TempTransNo);

            BindJournal();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    #endregion

    #region Receipt Grid Events
    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdViewReceipt.SelectedDataKey != null)
                e.InputParameters["TransNo"] = GrdViewReceipt.SelectedDataKey.Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewReceipt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int TransNo = (int)GrdViewReceipt.DataKeys[e.RowIndex].Value;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            bl.HideReceipt(TransNo);
            BindReceipt();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewReceipt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int TempTransNo = (Int32)GrdViewReceipt.SelectedDataKey.Value;

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            bl.RestoreReceipt(TempTransNo);

            BindReceipt();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewReceipt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewReceipt.PageIndex = e.NewPageIndex;
            BindReceipt();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    #endregion
    #endregion

    #region Payment Methods
    private void BindPayment()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        string criteria = string.Empty;
        string value = string.Empty;
        bool deletedItems = false;

        if (txtPaymentSearch.Text != "")
            value = txtPaymentSearch.Text;

        if (ddPaymentCriteria.SelectedValue != "0")
            criteria = ddPaymentCriteria.SelectedValue;

        deletedItems = ChkPayDel.Checked;

        BusinessLogic bl = new BusinessLogic();
        DataSet ds = bl.lsPayments(sDataSource, criteria, value, deletedItems);
        GrdViewPayment.DataSource = ds;
        GrdViewPayment.DataBind();

        if (ChkPayDel.Checked)
        {
            GrdViewPayment.Columns[9].Visible = false;
            GrdViewPayment.Columns[8].Visible = true;
        }
        else
        {
            GrdViewPayment.Columns[8].Visible = false;
            GrdViewPayment.Columns[9].Visible = true;
        }
    }

    #endregion

    #region Sales Methods
    private void BindSales(int strBillno)
    {
        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        bool deletedItems = false;
        deletedItems = ChkSales.Checked;
        if (strBillno == 0)
            ds = bl.lsSales(deletedItems);
        else
            ds = bl.lsSalesForId(strBillno, deletedItems);



        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewSales.DataSource = ds.Tables[0].DefaultView;
                GrdViewSales.DataBind();
            }
        }
        else
        {
            GrdViewSales.DataSource = null;
            GrdViewSales.DataBind();

        }
        if (ChkSales.Checked)
        {
            GrdViewSales.Columns[11].Visible = false;
            GrdViewSales.Columns[10].Visible = true;
        }
        else
        {
            GrdViewSales.Columns[10].Visible = false;
            GrdViewSales.Columns[11].Visible = true;
        }

    }

    #endregion

    #region Purchase Methods

    private void BindPurchase(string strBillno)
    {
        DataSet ds = new DataSet();
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        bool deletedItems = false;
        deletedItems = ChkPurchase.Checked;
        if (strBillno == "0")
            ds = bl.lsPurchase(deletedItems);
        else
            ds = bl.lsPurchaseForId(strBillno, deletedItems);


        GrdViewPurchase.DataSource = ds;
        GrdViewPurchase.DataBind();

        if (ChkPurchase.Checked)
        {
            GrdViewPurchase.Columns[11].Visible = false;
            GrdViewPurchase.Columns[10].Visible = true;
        }
        else
        {
            GrdViewPurchase.Columns[10].Visible = false;
            GrdViewPurchase.Columns[11].Visible = true;
        }
    }


    #endregion

    #region Receipt Methods
    private void BindReceipt()
    {
        sDataSource = Request.Cookies["Company"].Value; // ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        string criteria = string.Empty;
        string value = string.Empty;
        bool deletedItems = false;

        if (txtReceiptSearch.Text != "")
            value = txtReceiptSearch.Text;

        if (ddReceiptCriteria.SelectedValue != "0")
            criteria = ddReceiptCriteria.SelectedValue;

        deletedItems = ChkReceipt.Checked;

        BusinessLogic bl = new BusinessLogic();
        DataSet ds = bl.lsReceipts(sDataSource, criteria, value, deletedItems);
        GrdViewReceipt.DataSource = ds;
        GrdViewReceipt.DataBind();

        if (ChkReceipt.Checked)
        {
            GrdViewReceipt.Columns[9].Visible = false;
            GrdViewReceipt.Columns[8].Visible = true;
        }
        else
        {
            GrdViewReceipt.Columns[8].Visible = false;
            GrdViewReceipt.Columns[9].Visible = true;
        }
    }


    #endregion

    #region Journal Methods
    public void BindJournal()
    {

        string sDataSource = string.Empty;

        if (Request.Cookies["Company"] != null)
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        bool deletedItems = false;
        deletedItems = ChkJournal.Checked;
        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.lsJournal(txtRefno.Text.Trim(), txtNaration.Text.Trim(), txtDate.Text, sDataSource, deletedItems);

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
        if (ChkJournal.Checked)
        {
            GrdViewJournal.Columns[7].Visible = false;
            GrdViewJournal.Columns[6].Visible = true;
        }
        else
        {
            GrdViewJournal.Columns[6].Visible = false;
            GrdViewJournal.Columns[7].Visible = true;
        }

    }

    #endregion

    #region Button Events
    protected void btnPaymentSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindPayment();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    protected void btnReceiptSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindReceipt();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnJournalSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindJournal();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnPurchaseSearch_Click(object sender, EventArgs e)
    {
        try
        {
            String strBillno = string.Empty;
            if (txtBillnoPurchaseSearch.Text.Trim() != "")
                strBillno = txtBillnoPurchaseSearch.Text.Trim();
            else
                strBillno = "0";
            BindPurchase(strBillno);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnSalesSearch_Click(object sender, EventArgs e)
    {
        try
        {
            int billno = 0;
            if (txtBillnoSalesSearch.Text.Trim() != "")
                billno = Convert.ToInt32(txtBillnoSalesSearch.Text.Trim());
            BindSales(billno);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    #endregion
}

