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

public partial class BundleSalesNew : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    double amtTotal = 0.0;
    double disTotal = 0.0;
    double vatTotal = 0.0;
    double cstTotal = 0.0;
    double rateTotal = 0.0;
    string dbfileName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        { 
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
        BusinessLogic objChk = new BusinessLogic();
        GrdViewItems.Columns[12].Visible = false;
        GrdViewItems.Columns[14].Visible = false;
        
            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                cmdSaveProduct.Enabled = false;
                GrdViewItems.Columns[14].Visible = false;
                GrdViewItems.Columns[13].Visible = false;
                cmdSave.Enabled = false;
                cmdDelete.Enabled = false;
                cmdUpdate.Enabled = false;
                lnkBtnAdd.Visible = false;
            }

            if (!IsPostBack)
            {
                txtBarcode.Attributes.Add("onKeyPress", " return clickButton(event,'" + cmdBarcode.ClientID + "')");
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string BillFormat = bl.getConfigInfo();
                if (BillFormat == "STEEL CEMENT")
                {
                    colheadBundles.Visible = true;
                    colheadRods.Visible = true;
                    headBundles.Visible = true;
                    headRods.Visible = true;
                    dvBundle.Visible = true;
                    dvRod.Visible = true;
                }
                else
                {
                    colheadBundles.Visible = false;
                    colheadRods.Visible = false;
                    headBundles.Visible = false;
                    headRods.Visible = false;
                    dvBundle.Visible = false;
                    dvRod.Visible = false;
                }
                if (Session["NEWSALES"] != null)
                {
                    if (Session["NEWSALES"].ToString() == "Y")
                    {
                        lnkBtnAdd.Visible = true;
                        // hdMode.Value = "New";
                        Reset();
                        lblTotalSum.Text = "0";
                        lblTotalDis.Text = "0";
                        lblTotalVAT.Text = "0";
                        lblTotalCST.Text = "0";
                        lblFreight.Text = "0";
                        lblNet.Text = "0";
                        ResetProduct();
                        txtBillDate.Text = DateTime.Now.ToShortDateString();
                        cmbProdAdd.Enabled = true;
                        cmdUpdateProduct.Enabled = false;
                        cmdSaveProduct.Enabled = true;
                        cmdCancel.Enabled = false;
                        PanelCmd.Visible = true;
                        GrdViewItems.DataSource = null;
                        GrdViewItems.DataBind();
                        GridView1.DataSource = null;
                        GridView1.DataSource = null;
                        Session["productDs"] = null;
                        Session["roledata"] = null;
                        Session["roleDs"] = null;
                        cmdSave.Enabled = true;
                        cmdDelete.Enabled = false;
                        cmdUpdate.Enabled = false;
                        cmdPrint.Enabled = false;
                        txtBillDate.Text = DateTime.Now.ToShortDateString();
                        if (Session["BillDate"] != null)
                            txtBillDate.Text = Session["BillDate"].ToString();

                    }
                    else
                    {

                        txtBillDate.Text = DateTime.Now.ToShortDateString();
                    }
                }
                else
                {

                    txtBillDate.Text = DateTime.Now.ToShortDateString();
                }
                loadBanks();
                loadEmp();
                loadCustomer();
                loadProducts();

                txtBillDate.Focus();
                BindGrid(0);
                pnlSalesForm.Visible = false;
                PanelBill.Visible = true;
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
            cmdBarcode.Click += new EventHandler(this.txtBarcode_Populated); //Jolo Barcode
            txtBarcode.Attributes.Add("onblur", "txtBarcode_Populated();"); //Jolo Barcode
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    #region Bind Methods
    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListExecutive();
        drpIncharge.DataSource = ds;
        drpIncharge.DataBind();
        drpIncharge.DataTextField = "empFirstName";
        drpIncharge.DataValueField = "empno";
    }
    private void loadCustomer()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListCreditorDebitor(sDataSource);
        cmbCustomer.DataSource = ds;
        cmbCustomer.DataBind();
        cmbCustomer.DataTextField = "LedgerName";
        cmbCustomer.DataValueField = "LedgerID";
    }
    private void loadBanks()
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
    private void loadProducts()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListProducts();
        cmbProdAdd.DataSource = ds;
        cmbProdAdd.DataBind();
        cmbProdAdd.DataTextField = "ProductName";
        cmbProdAdd.DataValueField = "ItemCode";

        DataSet dsBundleStock = new DataSet();
        String EmtS = string.Empty;
        dsBundleStock = bl.ListBundleStock(0, EmtS, 0, 0);
        drpQtyAdd.DataSource = dsBundleStock;
        drpQtyAdd.DataBind();
        drpQtyAdd.DataTextField = "ComQty";
        drpQtyAdd.DataValueField = "BundleNo";
        drpQtyAdd.Items.Clear();
    }
    private void BindGrid(int strBillno)
    {


        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        if (strBillno == 0)
            ds = bl.GetSales();
        else
            ds = bl.GetSalesForId(strBillno);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewSales.DataSource = ds.Tables[0].DefaultView;
                GrdViewSales.DataBind();
                PanelBill.Visible = true;
            }
        }
        else
        {
            GrdViewSales.DataSource = null;
            GrdViewSales.DataBind();
            PanelBill.Visible = true;
        }
    }
    #endregion

    #region ComboBox Events
    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            GrdViewSales.PageIndex = ((DropDownList)sender).SelectedIndex;
            int strBillno = 0;
            //if (txtBillnoSrc.Text.Trim() != "")
            //    strBillno = Convert.ToInt32(txtBillnoSrc.Text.Trim());

            BindGrid(strBillno);
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void drpPaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpPaymode.SelectedIndex == 1)
                pnlBank.Visible = true;
            else
                pnlBank.Visible = false;
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cmdCancel.Enabled = true;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int iLedgerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
            DataSet ds = bl.GetExecutive(iLedgerID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                drpIncharge.ClearSelection();
                ListItem li = drpIncharge.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["executiveincharge"]));
                if (li != null) li.Selected = true;

                lblledgerCategory.Text = "[ " + Convert.ToString(ds.Tables[0].Rows[0]["LedgerCategory"]) + " ]";
            }
            DataSet customerDs = bl.getAddressInfo(iLedgerID);
            if (customerDs != null && customerDs.Tables[0].Rows.Count > 0)
            {
                if (customerDs.Tables[0].Rows[0]["Add1"] != null || customerDs.Tables[0].Rows[0]["Add2"] != null || customerDs.Tables[0].Rows[0]["Add3"] != null || customerDs.Tables[0].Rows[0]["phone"] != null)
                    if (customerDs.Tables[0].Rows[0]["Add1"].ToString() != "" || customerDs.Tables[0].Rows[0]["Add2"].ToString() != "" || customerDs.Tables[0].Rows[0]["Add3"].ToString() != "" || customerDs.Tables[0].Rows[0]["phone"].ToString() != "")
                        txtAddress.Text = Convert.ToString(customerDs.Tables[0].Rows[0]["Add1"]) + " " + Convert.ToString(customerDs.Tables[0].Rows[0]["Add2"]) + " " + Convert.ToString(customerDs.Tables[0].Rows[0]["Add3"]) + " Ph:" + Convert.ToString(customerDs.Tables[0].Rows[0]["Phone"]);
                    else
                        txtAddress.Text = "";
                hdContact.Value = Convert.ToString(customerDs.Tables[0].Rows[0]["Phone"]);
            }
            errPanel.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void txtBarcode_Populated(object sender, EventArgs e) //Jolo Barcode
    {
        try
        { 
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        bool dupFlag = false;
        DataSet checkDs;
        string itemCode = string.Empty;
        DataSet ds = new DataSet();
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        itemCode = bl.GetItemCode(connection, this.txtBarcode.Text);

        if ((itemCode == string.Empty) || (itemCode == "0"))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product not found. Please try again.');", true);
            return;
        }

        cmbProdAdd.SelectedValue = itemCode;

        DataSet roleDs = new DataSet();
        cmdDelete.Enabled = false;

            if (cmbProdAdd.SelectedIndex != 0)
            {

                itemCode = cmbProdAdd.SelectedItem.Value;
                double chk = bl.getStockInfo(itemCode);
                if (chk <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Current Stock Limit " + chk + "')", true);
                    return;
                }
                if (Session["productDs"] != null)
                {
                    checkDs = (DataSet)Session["productDs"];

                    //foreach (DataRow dR in checkDs.Tables[0].Rows)
                    //{
                    //    if (dR["itemCode"] != null)
                    //    {
                    //        if (dR["itemCode"].ToString().Trim() == itemCode)
                    //        {
                    //            dupFlag = true;
                    //            break;
                    //        }
                    //    }
                    //}
                }
                if (!dupFlag)
                {
                    hdOpr.Value = "New";
                    hdCurrRole.Value = "";
                    ds = bl.ListProductDetails(itemCode);

                    string category = lblledgerCategory.Text;

                    if (ds != null)
                    {
                        lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                        lblProdDescAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);
                        if (category == "Dealer")
                        {
                            lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerDiscount"]);
                            lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Dealervat"]);
                            txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerRate"]);
                        }
                        else
                        {
                            lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                            lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["vat"]);
                            lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                            txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);

                        }
                        hdStock.Value = Convert.ToString(ds.Tables[0].Rows[0]["Stock"]);

                        if (ds.Tables[0].Rows[0]["Accept_Role"] != null && ds.Tables[0].Rows[0]["Accept_Role"] != DBNull.Value)
                        {
                            if (ds.Tables[0].Rows[0]["Accept_Role"].ToString() == "Y")
                            {
                                roleDs = bl.getRoleInfo(ds.Tables[0].Rows[0]["itemcode"].ToString());
                                Session["roles"] = roleDs;
                                hdRole.Value = "Y";
                                pnlRole.Visible = true;
                                if (roleDs != null)
                                {
                                    drpRoleAvl.DataSource = roleDs;
                                    drpRoleAvl.DataBind();
                                    drpRoleAvl.Items.Insert(0, new ListItem(" -- Please Select -- ", "0"));
                                }
                                else
                                {
                                    //Session["roles"] = null;
                                    pnlRole.Visible = false;
                                    drpRoleAvl.DataSource = null;
                                    drpRoleAvl.DataBind();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No stock for the item')", true);
                                }
                                txtQtyAdd.ReadOnly = true;
                                Session["roledata"] = null;
                                GridView1.DataSource = null;
                                GridView1.DataBind();
                            }
                            else
                            {
                                txtQtyAdd.ReadOnly = false;
                                Session["roles"] = null;
                                Session["roledata"] = null;
                                GridView1.DataSource = null;
                                GridView1.DataBind();
                                pnlRole.Visible = false;
                                hdRole.Value = "N";
                            }
                        }
                        else
                        {
                            txtQtyAdd.ReadOnly = false;
                            Session["roles"] = null;
                            Session["roledata"] = null;
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            pnlRole.Visible = false;
                            hdRole.Value = "N";

                        }

                    }
                }
                else
                {
                    cmbProdAdd.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);
                }
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmbProdAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        { 
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        DataSet roleDs = new DataSet();
        string itemCode = string.Empty;
        DataSet checkDs;
        bool dupFlag = false;
        cmdDelete.Enabled = false;
        drpQtyAdd.Items.Clear();
        txtQtyAdd.Text = "0";
        drpQtyAdd.Items.Add("0");


            if (cmbProdAdd.SelectedIndex != 0)
            {

                itemCode = cmbProdAdd.SelectedItem.Value;
                double chk = bl.getStockInfo(itemCode);
                if (chk <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Current Stock Limit " + chk + "')", true);
                    return;
                }
                if (Session["productDs"] != null)
                {
                    checkDs = (DataSet)Session["productDs"];

                    //foreach (DataRow dR in checkDs.Tables[0].Rows)
                    //{
                    //    if (dR["itemCode"] != null)
                    //    {
                    //        if (dR["itemCode"].ToString().Trim() == itemCode)
                    //        {
                    //            dupFlag = true;
                    //            break;
                    //        }
                    //    }
                    //}
                }
                if (!dupFlag)
                {
                    hdOpr.Value = "New";
                    hdCurrRole.Value = "";
                    ds = bl.ListProductDetails(cmbProdAdd.SelectedItem.Value);

                    string category = lblledgerCategory.Text;

                    if (ds != null)
                    {
                        lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                        lblProdDescAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);
                        if (category == "Dealer")
                        {
                            lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerDiscount"]);
                            lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Dealervat"]);
                            txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["DealerRate"]);
                        }
                        else
                        {
                            lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                            lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["vat"]);
                            lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                            txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);

                        }
                        hdStock.Value = Convert.ToString(ds.Tables[0].Rows[0]["Stock"]);


                        DataSet dsBundleStock = new DataSet();

                        dsBundleStock = bl.ListBundleStock(0, cmbProdAdd.SelectedItem.Value, 0, 0);
                        drpQtyAdd.DataSource = dsBundleStock;
                        drpQtyAdd.DataTextField = "ComQty";
                        drpQtyAdd.DataValueField = "BundleNo";
                        drpQtyAdd.DataBind();

                        if (ds.Tables[0].Rows[0]["Accept_Role"] != null && ds.Tables[0].Rows[0]["Accept_Role"] != DBNull.Value)
                        {
                            if (ds.Tables[0].Rows[0]["Accept_Role"].ToString() == "Y")
                            {
                                roleDs = bl.getRoleInfo(ds.Tables[0].Rows[0]["itemcode"].ToString());
                                Session["roles"] = roleDs;
                                hdRole.Value = "Y";
                                pnlRole.Visible = true;
                                if (roleDs != null)
                                {
                                    drpRoleAvl.DataSource = roleDs;
                                    drpRoleAvl.DataBind();
                                    drpRoleAvl.Items.Insert(0, new ListItem(" -- Please Select -- ", "0"));
                                }
                                else
                                {
                                    //Session["roles"] = null;
                                    pnlRole.Visible = false;
                                    drpRoleAvl.DataSource = null;
                                    drpRoleAvl.DataBind();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No stock for the item')", true);
                                }
                                txtQtyAdd.ReadOnly = true;
                                Session["roledata"] = null;
                                GridView1.DataSource = null;
                                GridView1.DataBind();
                            }
                            else
                            {
                                txtQtyAdd.ReadOnly = false;
                                Session["roles"] = null;
                                Session["roledata"] = null;
                                GridView1.DataSource = null;
                                GridView1.DataBind();
                                pnlRole.Visible = false;
                                hdRole.Value = "N";
                            }
                        }
                        else
                        {
                            txtQtyAdd.ReadOnly = false;
                            Session["roles"] = null;
                            Session["roledata"] = null;
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            pnlRole.Visible = false;
                            hdRole.Value = "N";

                        }

                    }
                }
                else
                {
                    cmbProdAdd.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);
                }
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void drpRoleAvl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int roleID = Convert.ToInt32(drpRoleAvl.SelectedItem.Value);
            BusinessLogic bl = new BusinessLogic(sDataSource);

            txtIntialQty.Text = Convert.ToString(bl.getRoleInfoIntialQty(roleID));
            txtIntialQty.Focus();
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    #endregion

    #region Button Events

    protected void cmdUpdateProduct_Click(object sender, EventArgs e)
    {
        try
        { 
        string connection = string.Empty;
        string recondate = string.Empty;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        double stock = 0;
        DataTable dt;
        DataRow drNew;
        DataColumn dc;

        string sDiscount = "";
        string sVat = "";
        string sCST = "";
        double dTotal = 0;
        string[] prodItem;
        string roleFlag = string.Empty;
        DataSet dsRole = new DataSet();
        string strRole = string.Empty;
        string strQty = string.Empty;
        bool dupFlag = false;
        string itemCode = string.Empty;
        int curRow = 0;

            if (Page.IsValid)
            {
                stock = bl.getStockInfo(cmbProdAdd.SelectedItem.Value);


                if (Request.Cookies["Company"]  != null)
                    connection = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("Login.aspx");
                recondate = txtBillDate.Text.Trim();

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }
                prodItem = cmbProdAdd.SelectedItem.Text.Split('-');
                double chk = bl.getStockInfo(Convert.ToString(prodItem[0]));
                double curQty = Convert.ToDouble(txtQtyAdd.Text);
                /*Start March 15 Modification */
                double QtyEdit = Convert.ToDouble(hdEditQty.Value);
                chk = chk + QtyEdit;
                /*End March 15 Modification */
                if (curQty > chk)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected qty is greater than stock.Current Stock : " + chk + "')", true);
                    return;
                }

                DataSet ds = (DataSet)Session["productDs"];
                pnlProduct.Visible = true;
                pnlRole.Visible = false;
                txtQtyAdd.ReadOnly = false;
                roleFlag = hdRole.Value;
                if (roleFlag == "Y")
                {

                    dsRole = (DataSet)Session["roledata"];
                    if (dsRole != null)
                    {
                        if (dsRole.Tables[0] != null)
                        {
                            foreach (DataRow drole in dsRole.Tables[0].Rows)
                            {
                                strRole = strRole + drole["RoleID"].ToString() + "_" + drole["Qty"].ToString() + ",";
                                strQty = strQty + drole["Qty"].ToString() + ",";
                            }

                        }
                    }

                }
                else
                {
                    strRole = "NO ROLE";
                }
                if (strRole.EndsWith(","))
                {
                    strRole = strRole.Remove(strRole.Length - 1, 1);
                }
                if (strQty.EndsWith(","))
                {
                    strQty = strQty.Remove(strQty.Length - 1, 1);
                }
                if (lblDisAdd.Text.Trim() != "")
                    sDiscount = lblDisAdd.Text;
                else
                    sDiscount = "0";

                if (lblVATAdd.Text.Trim() != "")
                    sVat = lblVATAdd.Text;
                else
                    sVat = "0";

                if (lblCSTAdd.Text.Trim() != "")
                    sCST = lblCSTAdd.Text;
                else
                    sCST = "0";

                ds = (DataSet)Session["productDs"];


                // prodItem = cmbProdAdd.SelectedItem.Text.Split('-');

                curRow = Convert.ToInt32(hdCurrentRow.Value);

                ds.Tables[0].Rows[curRow].BeginEdit();
                dTotal = Convert.ToDouble(txtQtyAdd.Text) * Convert.ToDouble(txtRateAdd.Text);
                ds.Tables[0].Rows[curRow]["itemCode"] = Convert.ToString(prodItem[0]);
                ds.Tables[0].Rows[curRow]["Billno"] = hdsales.Value;
                ds.Tables[0].Rows[curRow]["ProductName"] = cmbProdAdd.SelectedItem.Value;
                ds.Tables[0].Rows[curRow]["ProductDesc"] = lblProdDescAdd.Text;
                ds.Tables[0].Rows[curRow]["Qty"] = txtQtyAdd.Text.Trim();
                ds.Tables[0].Rows[curRow]["Rate"] = txtRateAdd.Text.Trim();
                ds.Tables[0].Rows[curRow]["Discount"] = sDiscount;
                ds.Tables[0].Rows[curRow]["VAT"] = sVat;
                ds.Tables[0].Rows[curRow]["CST"] = sCST;
                // ds.Tables[0].Rows[curRow]["Roles"] = strRole;
                ds.Tables[0].Rows[curRow]["IsRole"] = roleFlag;
                ds.Tables[0].Rows[curRow]["Total"] = Convert.ToString(dTotal);
                ds.Tables[0].Rows[curRow]["Bundles"] = txtBundle.Text;
                ds.Tables[0].Rows[curRow]["Rods"] = txtRod.Text;
                ds.Tables[0].Rows[curRow].EndEdit();
                ds.Tables[0].Rows[curRow].AcceptChanges();
                string strDrpRole = string.Empty;

                foreach (ListItem roleLst in drpRoleAvl.Items)
                {
                    if (roleLst.Value != "0")
                        strDrpRole = strDrpRole + roleLst.Value + "_" + roleLst.Text + ",";
                }
                if (strDrpRole.EndsWith(","))
                {
                    strDrpRole = strDrpRole.Remove(strDrpRole.Length - 1, 1);
                }
                hdRoleSelection.Value = strDrpRole.Trim();

                GrdViewItems.DataSource = ds;
                GrdViewItems.DataBind();
                calcSum();

                //Session["roledata"] = null;

                ResetProduct();
                cmbProdAdd.Enabled = true;
                cmdUpdateProduct.Enabled = false;
                cmdSaveProduct.Enabled = true;
                calcSum();
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void cmdSaveProduct_Click(object sender, EventArgs e)
    {
        string connection = string.Empty;
        string recondate = string.Empty;
        double stock = 0;
        DataTable dt;
        DataRow drNew;
        DataColumn dc;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string sDiscount = "";
        string sVat = "";
        string sCST = "";
        double dTotal = 0;
        string[] prodItem;
        string roleFlag = string.Empty;
        DataSet dsRole = new DataSet();
        string strRole = string.Empty;
        string strQty = string.Empty;
        bool dupFlag = false;
        DataSet ds;
        hdOpr.Value = "New";
        string itemCode = string.Empty;
        try
        {
            if (Page.IsValid)
            {
                if (Request.Cookies["Company"]  != null)
                    connection = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("Login.aspx");

                recondate = txtBillDate.Text.Trim();

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }
                prodItem = cmbProdAdd.SelectedItem.Text.Split('-');
                double chk = bl.getStockInfo(Convert.ToString(prodItem[0]));
                double curQty = Convert.ToDouble(txtQtyAdd.Text);

                if (curQty > chk)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected qty is greater than stock.Current Stock : " + chk + "')", true);
                    return;
                }
                if (Session["productDs"] != null)
                {
                    DataSet checkDs = (DataSet)Session["productDs"];

                    foreach (DataRow dR in checkDs.Tables[0].Rows)
                    {
                        if (dR["itemCode"] != null)
                        {
                            if (dR["itemCode"].ToString().Trim() == cmbProdAdd.SelectedItem.Value)
                            {
                                if (dR["BundleNo"].ToString().Trim() == drpQtyAdd.SelectedItem.Value)
                                {
                                    dupFlag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (!dupFlag)
                {
                    cmbProdAdd.Enabled = true;
                    pnlProduct.Visible = true;
                    pnlRole.Visible = false;
                    txtQtyAdd.ReadOnly = false;
                    roleFlag = hdRole.Value;
                    if (roleFlag == "Y")
                    {

                        dsRole = (DataSet)Session["roledata"];
                        if (dsRole != null)
                        {
                            if (dsRole.Tables[0] != null)
                            {
                                foreach (DataRow drole in dsRole.Tables[0].Rows)
                                {
                                    strRole = strRole + drole["RoleID"].ToString() + "_" + drole["Qty"].ToString() + ",";
                                    strQty = strQty + drole["Qty"].ToString() + ",";
                                }

                            }
                        }

                    }
                    else
                    {
                        strRole = "NO ROLE";
                    }
                    if (strRole.EndsWith(","))
                    {
                        strRole = strRole.Remove(strRole.Length - 1, 1);
                    }
                    if (strQty.EndsWith(","))
                    {
                        strQty = strQty.Remove(strQty.Length - 1, 1);
                    }

                    if (hdStock.Value != "")
                        stock = Convert.ToDouble(hdStock.Value);

                    if (lblDisAdd.Text.Trim() != "")
                        sDiscount = lblDisAdd.Text;
                    else
                        sDiscount = "0";

                    if (lblVATAdd.Text.Trim() != "")
                        sVat = lblVATAdd.Text;
                    else
                        sVat = "0";

                    if (lblCSTAdd.Text.Trim() != "")
                        sCST = lblCSTAdd.Text;
                    else
                        sCST = "0";





                    if (Session["productDs"] == null)
                    {
                        ds = new DataSet();
                        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

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

                        dc = new DataColumn("Total");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Bundles");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Rods");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("BundleNo");
                        dt.Columns.Add(dc);

                        ds.Tables.Add(dt);

                        //prodItem = cmbProdAdd.SelectedItem.Text.Split('-');

                        drNew = dt.NewRow();
                        dTotal = Convert.ToDouble(txtQtyAdd.Text) * Convert.ToDouble(txtRateAdd.Text);
                        drNew["itemCode"] = Convert.ToString(prodItem[0]);
                        drNew["Billno"] = hdsales.Value;
                        drNew["ProductName"] = cmbProdAdd.SelectedItem.Value;
                        drNew["ProductDesc"] = lblProdDescAdd.Text;
                        drNew["Qty"] = txtQtyAdd.Text.Trim();
                        drNew["Rate"] = txtRateAdd.Text.Trim();
                        drNew["Discount"] = sDiscount;
                        drNew["VAT"] = sVat;
                        drNew["CST"] = sCST;
                        drNew["Roles"] = hdCurrRole.Value;   //strRole;
                        drNew["IsRole"] = roleFlag;
                        drNew["Total"] = Convert.ToString(dTotal);
                        drNew["Bundles"] = txtBundle.Text;
                        drNew["Rods"] = txtRod.Text;
                        drNew["BundleNo"] = drpQtyAdd.SelectedItem.Value;
                        ds.Tables[0].Rows.Add(drNew);
                        Session["productDs"] = ds;

                    }
                    else
                    {
                        ds = (DataSet)Session["productDs"];


                        prodItem = cmbProdAdd.SelectedItem.Text.Split('-');


                        drNew = ds.Tables[0].NewRow();
                        dTotal = Convert.ToDouble(txtQtyAdd.Text) * Convert.ToDouble(txtRateAdd.Text);
                        drNew["itemCode"] = Convert.ToString(prodItem[0]);
                        drNew["Billno"] = hdsales.Value;
                        drNew["ProductName"] = cmbProdAdd.SelectedItem.Value;
                        drNew["ProductDesc"] = lblProdDescAdd.Text;
                        drNew["Qty"] = txtQtyAdd.Text.Trim();
                        drNew["Rate"] = txtRateAdd.Text.Trim();
                        drNew["Discount"] = sDiscount;
                        drNew["VAT"] = sVat;
                        drNew["CST"] = sCST;
                        drNew["Roles"] = hdCurrRole.Value; // strRole;
                        drNew["IsRole"] = roleFlag;
                        drNew["Total"] = Convert.ToString(dTotal);
                        drNew["Bundles"] = txtBundle.Text;
                        drNew["BundleNo"] = drpQtyAdd.SelectedItem.Value;
                        drNew["Rods"] = txtRod.Text;
                        ds.Tables[0].Rows.Add(drNew);

                    }


                    GrdViewItems.DataSource = ds;
                    GrdViewItems.DataBind();
                    calcSum();
                    ResetProduct();

                    //Session["roledata"] = null;

                }
                else
                {
                    cmbProdAdd.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code and stock already present')", true);
                }
            }
            errPanel.Visible = false;

            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void cmdSave_Click(object sender, EventArgs e)
    {


        string connection = Request.Cookies["Company"].Value;

        //if (!Helper.IsLicenced(connection))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
        //    return;
        //}

        string recondate = string.Empty;
        string purchaseReturn = string.Empty;
        string prReason = string.Empty;
        string executive = string.Empty;
        string sBilldate = string.Empty;
        string sCustomerAddress = string.Empty;
        string sCustomerContact = string.Empty;
        int sCustomerID = 0;
        double dTotalAmt = 0;
        string sCustomerName = string.Empty;
        int iPaymode = 0;
        string sCreditCardno = string.Empty;
        double dFreight = 0;
        double dLU = 0;
        int iBank = 0;
        int iSales = 0;
        DataSet ds;
        try
        {
            if (Page.IsValid)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                recondate = txtBillDate.Text.Trim(); ;

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }

                purchaseReturn = drpPurchaseReturn.SelectedItem.Text;
                prReason = txtPRReason.Text;
                executive = drpIncharge.SelectedItem.Value;
                iSales = Convert.ToInt32(hdsales.Value);
                sBilldate = txtBillDate.Text.Trim();
                iPaymode = Convert.ToInt32(drpPaymode.SelectedItem.Value);
                dTotalAmt = Convert.ToDouble(hdTotalAmt.Value.Trim());
                sCustomerAddress = txtAddress.Text.Trim();
                sCustomerContact = hdContact.Value.Trim();
                sCustomerName = cmbCustomer.SelectedItem.Text;
                sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                dTotalAmt = Convert.ToDouble(lblTotalSum.Text);
                /*March18*/
                if (txtFreight.Text.Trim() != "")
                    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
                if (txtLU.Text.Trim() != "")
                    dLU = Convert.ToDouble(txtLU.Text.Trim());
                /*March18*/
                dTotalAmt = dTotalAmt + dFreight + dLU;
                if (iPaymode == 2)
                {
                    sCreditCardno = Convert.ToString(txtCreditCardNo.Text);
                    iBank = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    rvbank.Enabled = true;
                    rvCredit.Enabled = true;
                }
                else
                {
                    rvbank.Enabled = false;
                    rvCredit.Enabled = false;
                }
                //ds = (DataSet)GrdViewItems.DataSource;
                if (Session["productDs"] != null)
                {
                    ds = (DataSet)Session["productDs"];

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int billNo = bl.InsertBundleSales(sBilldate, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, iPaymode, sCreditCardno, iBank, dTotalAmt, purchaseReturn, prReason, Convert.ToInt32(executive), dFreight, dLU, ds);
                            if (billNo == -1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock Limit is Less')", true);
                                return;
                            }
                            else
                            {
                                Reset();
                                ResetProduct();
                                cmdPrint.Enabled = false;
                                Session["salesID"] = billNo.ToString();
                                Session["PurchaseReturn"] = purchaseReturn;
                                Session["productDs"] = null;
                                Session["roledata"] = null;
                                Session["roleDs"] = null;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Details Saved Successfully. Your Bill No. is " + billNo.ToString() + "')", true);
                                Response.Redirect("BundlePrintProductSalesBill.aspx");
                            }
                        }
                        else
                        {
                            cmdSaveProduct.Enabled = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before save')", true);
                            return;
                        }
                    }
                }
                else
                {
                    cmdSaveProduct.Enabled = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before update')", true);
                    return;
                }
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        string connection = Request.Cookies["Company"].Value;
        string recondate = string.Empty;
        string purchaseReturn = string.Empty;
        string prReason = string.Empty;
        string executive = string.Empty;
        string sBilldate = string.Empty;
        string sCustomerAddress = string.Empty;
        string sCustomerContact = string.Empty;
        int sCustomerID = 0;
        double dTotalAmt = 0;
        string sCustomerName = string.Empty;
        int iPaymode = 0;
        string sCreditCardno = string.Empty;
        double dFreight = 0;
        double dLU = 0;
        int iBank = 0;
        int iSales = 0;
        DataSet ds;
        try
        {
            if (Page.IsValid)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                recondate = txtBillDate.Text.Trim(); ;

                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }

                dFreight = Convert.ToDouble(txtFreight.Text);
                dLU = Convert.ToDouble(txtLU.Text);
                purchaseReturn = drpPurchaseReturn.SelectedItem.Text;
                prReason = txtPRReason.Text;
                executive = drpIncharge.SelectedItem.Value;
                iSales = Convert.ToInt32(hdsales.Value);
                sBilldate = txtBillDate.Text.Trim();
                iPaymode = Convert.ToInt32(drpPaymode.SelectedItem.Value);
                dTotalAmt = Convert.ToDouble(hdTotalAmt.Value.Trim());
                sCustomerAddress = txtAddress.Text.Trim();
                sCustomerContact = hdContact.Value.Trim();
                sCustomerName = cmbCustomer.SelectedItem.Text;
                sCustomerID = Convert.ToInt32(cmbCustomer.SelectedItem.Value);
                dTotalAmt = Convert.ToDouble(lblTotalSum.Text);
                dTotalAmt = dTotalAmt + dFreight + dLU;
                //sOtherCusName = txtOtherCusName.Text;// krishnavelu 26 June

                if (iPaymode == 2)
                {
                    sCreditCardno = Convert.ToString(txtCreditCardNo.Text);
                    iBank = Convert.ToInt32(drpBankName.SelectedItem.Value);
                    rvbank.Enabled = true;
                    rvCredit.Enabled = true;
                }
                else
                {
                    rvbank.Enabled = false;
                    rvCredit.Enabled = false;
                }
                //ds = (DataSet)GrdViewItems.DataSource;
                int bill = Convert.ToInt32(hdsales.Value);
                if (Session["productDs"] != null)
                {
                    ds = (DataSet)Session["productDs"];

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //int billNo = bl.UpdateSalesNew(bill, sBilldate, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, iPaymode, sCreditCardno, iBank, dTotalAmt, purchaseReturn, prReason, Convert.ToInt32(executive), dFreight, dLU, ds);
                            int billNo = 0;
                            if (billNo == -1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock Limit is Less')", true);
                                return;
                            }
                            else
                            {
                                Reset();
                                ResetProduct();
                                Session["salesID"] = billNo.ToString();
                                Session["PurchaseReturn"] = purchaseReturn;
                                Session["productDs"] = null;
                                Session["roledata"] = null;
                                Session["roleDs"] = null;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Details Saved Successfully. Your Bill No. is " + billNo.ToString() + "')", true);
                                Response.Redirect("BundlePrintProductSalesBill.aspx");
                            }
                        }
                        else
                        {
                            cmdSaveProduct.Enabled = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before update')", true);
                            return;
                        }
                    }
                }
                else
                {
                    cmdSaveProduct.Enabled = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products for the bill before update')", true);
                    return;
                }
            }

            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string connection = Request.Cookies["Company"].Value;
                BusinessLogic bll = new BusinessLogic();
                string recondate = txtBillDate.Text.Trim(); ;
                if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }
                int iSales = 0;
                int sBillNo = Convert.ToInt32(hdsales.Value);
                iSales = Convert.ToInt32(hdsales.Value);
                //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
                BusinessLogic bl = new BusinessLogic(sDataSource);
                bl.DeleteBundleSales(sBillNo);
                GrdViewItems.DataSource = null;
                GrdViewItems.DataBind();
                pnlProduct.Visible = false;
                Reset();
                ResetProduct();

                //lnkBtnAdd.Visible = true;

                //hdMode.Value = "Delete";
                //cmdPrint.Enabled = false;
                cmdDelete.Enabled = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Details Deleted Successfully. Bill No. is " + sBillNo.ToString() + "')", true);
                BindGrid(0);
                Session["roleDs"] = null;
                Session["ProductDs"] = null;
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
            lblTotalSum.Text = "0";
            lblTotalDis.Text = "0";
            lblTotalVAT.Text = "0";
            lblTotalCST.Text = "0";
            lblFreight.Text = "0";
            lblNet.Text = "0";

            pnlSalesForm.Visible = false;
            lnkBtnAdd.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void cmdPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string connection = Request.Cookies["Company"].Value;
                BusinessLogic bl = new BusinessLogic();
                string recondate = txtBillDate.Text.Trim();
                if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }

                Session["salesID"] = hdsales.Value;
                Session["roleDs"] = null;
                Response.Redirect("BundlePrintProductSalesBill.aspx");
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //lnkBtnAdd.Visible = true;
            // hdMode.Value = "New";
            Reset();
            // lblTotalSum.Text = "0";
            ResetProduct();
            txtBillDate.Text = DateTime.Now.ToShortDateString();
            cmbProdAdd.Enabled = true;
            cmdUpdateProduct.Enabled = false;
            cmdSaveProduct.Enabled = true;
            //cmdCancel.Enabled = false; 
            //PanelCmd.Visible = false;
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();
            GridView1.DataSource = null;
            GridView1.DataSource = null;
            Session["productDs"] = null;
            Session["roledata"] = null;
            Session["roleDs"] = null;
            pnlRole.Visible = false;

            cmdSave.Enabled = true;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            cmdPrint.Enabled = false;
            lblTotalDis.Text = "0";
            lblTotalCST.Text = "0";
            lblTotalSum.Text = "0";
            lblTotalVAT.Text = "0";
            lblNet.Text = "0";
            lblFreight.Text = "0";
            errPanel.Visible = false;
            ErrMsg.Text = "";

            pnlSalesForm.Visible = false;
            PanelBill.Visible = true;
            lnkBtnAdd.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            pnlRole.Visible = false;
            Session["roleDs"] = null;
            drpRoleAvl.SelectedIndex = 0;
            txtRoleQty.Text = "";
            txtIntialQty.Text = ""; //roleDs.Tables[0].Rows[0]["Qty_bought"].ToString();
            cmdSaveProduct.Enabled = true;
            cmbProdAdd.Enabled = true;
            cmdUpdateProduct.Enabled = false;
            ResetProduct();
            errPanel.Visible = false;
            ErrMsg.Text = "";
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
            lnkBtnAdd.Visible = false;
            pnlSalesForm.Visible = true;
            PanelBill.Visible = false;
            // hdMode.Value = "New";
            Reset();
            // lblTotalSum.Text = "0";
            lblTotalSum.Text = "0";
            lblTotalDis.Text = "0";
            lblTotalVAT.Text = "0";

            ResetProduct();
            txtBillDate.Text = DateTime.Now.ToShortDateString();
            cmbProdAdd.Enabled = true;
            cmdUpdateProduct.Enabled = false;
            cmdSaveProduct.Enabled = true;
            cmdCancel.Enabled = true;
            PanelCmd.Visible = true;
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();
            GridView1.DataSource = null;
            GridView1.DataSource = null;
            Session["productDs"] = null;
            Session["roledata"] = null;
            Session["roleDs"] = null;
            cmdSave.Enabled = true;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            cmdPrint.Enabled = false;
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnCLick_Click(object sender, EventArgs e)
    {
        double qtyAvl = 0.0;
        double qtyUsed = 0.0;
        string roleID = string.Empty;
        double qtyCurr = 0.0;
        int curRow = 0;
        DataSet dS;
        DataSet checkDs;
        string strRole = string.Empty;
        string strBindRole = string.Empty;
        try
        {
            if (Page.IsValid)
            {
                qtyAvl = Convert.ToDouble(drpRoleAvl.SelectedItem.Text);
                qtyUsed = Convert.ToDouble(txtRoleQty.Text.Trim());
                roleID = drpRoleAvl.SelectedItem.Value;
                txtIntialQty.Focus();
                if (qtyUsed > qtyAvl)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Quantity used should be less than the  Quantity available')", true);
                    return;
                }
                else
                {

                    if (txtQtyAdd.Text.Trim() != "")
                        txtQtyAdd.Text = Convert.ToString(Convert.ToDouble(txtQtyAdd.Text) + qtyUsed);
                    else
                        txtQtyAdd.Text = Convert.ToString(qtyUsed);

                    drpRoleAvl.Items.Remove(new ListItem(drpRoleAvl.SelectedItem.Text, drpRoleAvl.SelectedItem.Value));
                    qtyCurr = qtyAvl - qtyUsed;

                    ListItem li = new ListItem(qtyCurr.ToString(), roleID);
                    if (qtyCurr > 0)
                        drpRoleAvl.Items.Add(li);
                    string strDrpRole = string.Empty;

                    foreach (ListItem roleLst in drpRoleAvl.Items)
                    {
                        if (roleLst.Value != "0")
                            strDrpRole = strDrpRole + roleLst.Value + "_" + roleLst.Text + ",";
                    }
                    if (strDrpRole.EndsWith(","))
                    {
                        strDrpRole = strDrpRole.Remove(strDrpRole.Length - 1, 1);
                    }
                    hdRoleSelection.Value = strDrpRole.Trim();

                    DataSet ds = new DataSet();

                    strRole = roleID + "_" + txtRoleQty.Text;

                    if (hdOpr.Value == "Edit")
                    {
                        if (Session["productDs"] != null)
                        {
                            dS = (DataSet)Session["productDs"];


                            curRow = Convert.ToInt32(hdCurrentRow.Value);
                            dS.Tables[0].Rows[curRow].BeginEdit();
                            if (dS.Tables[0].Rows[curRow]["Roles"].ToString().EndsWith(","))
                                dS.Tables[0].Rows[curRow]["Roles"] = dS.Tables[0].Rows[curRow]["Roles"] + strRole;
                            else
                                dS.Tables[0].Rows[curRow]["Roles"] = dS.Tables[0].Rows[curRow]["Roles"] + "," + strRole;
                            strBindRole = strBindRole + dS.Tables[0].Rows[curRow]["Roles"] + ",";
                            dS.Tables[0].Rows[curRow].EndEdit();
                            dS.Tables[0].Rows[curRow].AcceptChanges();
                            Session["productDs"] = dS;
                            //GrdViewItems.DataSource = (DataSet)Session["productDs"];
                            //GrdViewItems.DataBind(); 
                        }

                        //strBindRole = strBindRole + strRole;

                    }
                    else
                    {

                        if (hdCurrRole.Value.Trim() != "")
                        {

                            hdCurrRole.Value = hdCurrRole.Value + strRole + ",";
                            strBindRole = hdCurrRole.Value;

                        }
                        else
                        {
                            hdCurrRole.Value = strRole + ",";
                            strBindRole = hdCurrRole.Value;
                        }
                    }
                    if (strBindRole.EndsWith(","))
                    {
                        strBindRole = strBindRole.Remove(strBindRole.Length - 1, 1);
                    }
                    string[] roleSpl;
                    string[] roleArr;
                    DataRow dr;

                    DataTable dt = new DataTable();

                    DataColumn dcRole = new DataColumn("RoleID");
                    DataColumn dcQty = new DataColumn("Qty");

                    dt.Columns.Add(dcQty);
                    dt.Columns.Add(dcRole);

                    ds.Tables.Add(dt);

                    roleArr = strBindRole.Split(',');
                    for (int k = 0; k < roleArr.Length; k++)
                    {
                        if (roleArr[k].Trim() != "")
                        {
                            roleSpl = roleArr[k].Split('_');
                            dr = ds.Tables[0].NewRow();
                            dr[0] = roleSpl[1].ToString();
                            dr[1] = roleSpl[0].ToString();
                            ds.Tables[0].Rows.Add(dr);
                        }
                    }


                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                    Session["roleDs"] = ds;

                    txtRoleQty.Text = "";
                    txtIntialQty.Text = "";
                }
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    #endregion

    #region General Methods
    private bool paymodeVisible(string paymode)
    {
        if (paymode.ToUpper() != "BANK")
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
    private void Reset()
    {

        txtAddress.Text = "";
        cmbCustomer.SelectedIndex = 0;
        drpPaymode.SelectedIndex = 0;
        drpBankName.SelectedIndex = 0;
        drpIncharge.SelectedIndex = 0;
        drpPurchaseReturn.SelectedIndex = 0;
        txtCreditCardNo.Text = "";
        txtPRReason.Text = "";
        lblledgerCategory.Text = "";
        txtFreight.Text = "0";
        txtLU.Text = "0";

    }
    private void ResetProduct()
    {
        lblProdNameAdd.Text = "";
        lblProdDescAdd.Text = "";
        lblDisAdd.Text = "0";
        lblVATAdd.Text = "0";
        lblCSTAdd.Text = "0";
        lblDisAdd.Text = "0";
        txtQtyAdd.Text = "0";
        txtRateAdd.Text = "0";
        //lblTotalDis.Text = "0"; 
        //lblTotalCST.Text = "0";
        //lblTotalSum.Text = "0";
        //lblTotalVAT.Text = "0";
        //lblNet.Text = "0";
        //lblFreight.Text = "0"; 

        cmbProdAdd.SelectedIndex = 0;
        txtRod.Text = "0";
        txtBundle.Text = "0";

    }

    //public string GetTotal(double qty, double rate, double discount, double VAT, double CST)
    //{
    //    double tot = 0;
    //    tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));
    //    amtTotal = amtTotal + Convert.ToDouble(tot);
    //    disTotal = disTotal + discount;
    //    rateTotal = rateTotal + rate;
    //    vatTotal = vatTotal + VAT;
    //    cstTotal = cstTotal + CST;
    //    hdTotalAmt.Value = amtTotal.ToString("#0.00");
    //    //lblGrandTotal.Text = Convert.ToString(Convert.ToDecimal(tot) +Convert.ToDecimal(hdTotalAmt.Value));
    //    return tot.ToString("#0.00");
    //}
    public string GetTotal(double qty, double rate, double discount, double VAT, double CST)
    {
        double dis = 0;
        double vat = 0;
        double cst = 0;
        double tot = 0;
        tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));

        // tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));
        dis = (qty * rate) - ((qty * rate) * (discount / 100));
        vat = dis + (dis * (VAT / 100));
        cst = dis + (dis * (CST / 100));
        amtTotal = amtTotal + Convert.ToDouble(tot);
        disTotal = dis;
        rateTotal = rateTotal + rate;
        vatTotal = vat;
        cstTotal = cst;
        hdTotalAmt.Value = amtTotal.ToString("#0.00");
        //lblGrandTotal.Text = Convert.ToString(Convert.ToDecimal(tot) +Convert.ToDecimal(hdTotalAmt.Value));
        return tot.ToString("#0.00");
    }
    public double GetSum()
    {
        return amtTotal;// Convert.ToDouble(hdTotalAmt.Value);
    }
    public double GetDis()
    {
        return disTotal;
    }
    public double GetRate()
    {
        return rateTotal;
    }
    public double GetVat()
    {
        return vatTotal;
    }
    public double GetCST()
    {
        return cstTotal;
    }
    private void GenerateRoleDs()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dcQty = new DataColumn("Qty");
        DataColumn dcRole = new DataColumn("RoleID");

        dt.Columns.Add(dcQty);
        dt.Columns.Add(dcRole);

        ds.Tables.Add(dt);
        Session["roledata"] = ds;
    }
    #endregion

    #region GridViewItems
    protected void GrdViewItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strItemCode = string.Empty;
        string strRoleFlag = string.Empty;
        DataSet roleDs;
        DataSet dsRole;
        DataSet ds = new DataSet();
        int billno = 0;
        string strRole;
        GridViewRow row = GrdViewItems.SelectedRow;
        string[] splRole;

        BusinessLogic bl = new BusinessLogic(sDataSource);
        try
        {
            hdCurrentRow.Value = Convert.ToString(row.DataItemIndex);
            if (row.Cells[0].Text != "&nbsp;")
            {
                strItemCode = row.Cells[0].Text;
                cmbProdAdd.ClearSelection();
                ListItem li = cmbProdAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(strItemCode.Trim()));
                if (li != null) li.Selected = true;
                cmbProdAdd.Enabled = false;
                cmdSaveProduct.Enabled = false;
                cmdUpdateProduct.Enabled = true;

            }
            txtRateAdd.Text = row.Cells[4].Text;
            txtQtyAdd.Text = row.Cells[3].Text;
            /*Start March 15 Modification */
            hdEditQty.Value = row.Cells[3].Text;
            /*End March 15 Modification */
            lblDisAdd.Text = row.Cells[5].Text;
            lblVATAdd.Text = row.Cells[6].Text;
            lblCSTAdd.Text = row.Cells[7].Text;
            lblProdNameAdd.Text = row.Cells[1].Text;
            lblProdDescAdd.Text = row.Cells[2].Text;
            hdRole.Value = row.Cells[8].Text;
            txtBundle.Text = row.Cells[10].Text;
            txtRod.Text = row.Cells[11].Text;
            strRoleFlag = hdRole.Value;

            billno = Convert.ToInt32(hdsales.Value);
            hdOpr.Value = "Edit";
            if (strRoleFlag == "Y")
            {
                roleDs = bl.getRoleInfo(strItemCode);
                //Session["roles"] = roleDs;
                hdRole.Value = "Y";
                pnlRole.Visible = true;

                txtQtyAdd.ReadOnly = true;
                hdCurrRole.Value = GrdViewItems.DataKeys[row.DataItemIndex].Value.ToString();
                if (hdCurrRole.Value.EndsWith(","))
                {
                    hdCurrRole.Value = hdCurrRole.Value.Remove(hdCurrRole.Value.Length - 1, 1);
                }

                splRole = hdCurrRole.Value.Split(',');
                dsRole = new DataSet();
                DataColumn dc;
                DataTable dt;
                DataRow dr;
                double qtyCurr = 0;
                string[] splt2;
                dt = new DataTable();

                dc = new DataColumn("RoleID");
                dt.Columns.Add(dc);
                dc = new DataColumn("Qty");
                dt.Columns.Add(dc);
                ds.Tables.Add(dt);

                for (int m = 0; m < splRole.Length; m++)
                {
                    dr = ds.Tables[0].NewRow();
                    if (splRole[m].ToString() != "")
                    {
                        splt2 = splRole[m].ToString().Split('_');
                        dr[0] = splt2[0].ToString();
                        dr[1] = splt2[1].ToString();
                        ds.Tables[0].Rows.Add(dr);
                        /*
                                            foreach (DataRow dR in roleDs.Tables[0].Rows)
                                            {

                                                dR.BeginEdit();
                                                if (dR["RoleID"].ToString() == splt2[0].ToString())
                                                {
                                                    qtyCurr = Convert.ToDouble(dR["Qty_Available"]) - Convert.ToDouble(splt2[1]);
                                                    dR["Qty_Available"] = qtyCurr; 
                                                }
                                                dR.EndEdit();
                                                dR.AcceptChanges(); 
                                            }

                                            */

                    }


                }
                string currRoleSelection = hdRoleSelection.Value;
                string[] arrRole;
                string[] arrInRole;
                ListItem newLst;

                if (currRoleSelection != "")
                {
                    drpRoleAvl.DataSource = null;
                    drpRoleAvl.DataBind();
                    drpRoleAvl.Items.Clear();

                    arrRole = currRoleSelection.Split(',');
                    for (int n = 0; n < arrRole.Length; n++)
                    {
                        if (arrRole[n].ToString() != string.Empty)
                        {
                            arrInRole = arrRole[n].Split('_');
                            newLst = new ListItem(arrInRole[1].ToString(), arrInRole[0].ToString());
                            drpRoleAvl.Items.Add(newLst);
                        }
                    }
                }
                else
                {
                    drpRoleAvl.DataSource = roleDs;
                    drpRoleAvl.DataBind();
                }
                drpRoleAvl.Items.Insert(0, new ListItem(" -- Please Select -- ", "0"));

                GridView1.DataSource = ds;
                GridView1.DataBind();

                Session["roleDs"] = ds;


            }
            else
            {

                txtQtyAdd.ReadOnly = false;
                Session["roles"] = null;
                Session["roledata"] = null;
                Session["roleDs"] = null;
                GridView1.DataSource = null;
                GridView1.DataBind();
                pnlRole.Visible = false;
                hdRole.Value = "N";

            }

            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds;
        int billno = 0;
        string strItemcode = string.Empty;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        try
        {
            if (Session["productDs"] != null)
            {

                GridViewRow row = GrdViewItems.Rows[e.RowIndex];

                // strRole = GrdViewItems.DataKeys[row.DataItemIndex].Value.ToString();
                strItemcode = row.Cells[0].Text;
                billno = Convert.ToInt32(hdsales.Value);
                int rowsAff = bl.DeleteSalesProduct(billno, strItemcode);
                if (rowsAff == -1)
                {
                    ds = (DataSet)Session["productDs"];
                    ds.Tables[0].Rows[GrdViewItems.Rows[e.RowIndex].DataItemIndex].Delete();
                    GrdViewItems.DataSource = ds;
                    GrdViewItems.DataBind();
                    Session["productDs"] = ds;
                    cmdSaveProduct.Enabled = true;
                    calcSum();
                }

            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    #endregion

    #region GridSales
    protected void GrdViewSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewSales.PageIndex = e.NewPageIndex;
            int strBillno = 0;
            if (txtBillnoSrc.Text.Trim() != "")
                strBillno = Convert.ToInt32(txtBillnoSrc.Text.Trim());
            else
                strBillno = 0;
            BindGrid(strBillno);
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewSales_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewSales, e.Row, this);
            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string paymode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paymode"));
                Label payMode = (Label)e.Row.FindControl("lblPaymode");
                if (paymode == "1")
                    payMode.Text = "Cash";
                else if (paymode == "2")
                    payMode.Text = "Bank";
                else
                    payMode.Text = "Credit";

            }
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewSales_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strPaymode = string.Empty;
        string sCustomer = string.Empty;
        int salesID = 0;
        string connection = Request.Cookies["Company"].Value;
        GridViewRow row = GrdViewSales.SelectedRow;
        DataSet itemDs = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string recondate = row.Cells[1].Text;
        cmdPrint.Enabled = true;
        cmdUpdate.Enabled = false;
        cmdDelete.Enabled = true;
        cmdSave.Enabled = false;
        PanelCmd.Visible = true;
        lnkBtnAdd.Visible = false;
        cmdCancel.Enabled = true;

        PanelBill.Visible = false;
        pnlSalesForm.Visible = true;

        try
        {

            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                return;
            }

            if (row.Cells[0].Text != "&nbsp;")
                salesID = Convert.ToInt32(row.Cells[0].Text);

            DataSet ds = bl.GetSalesForId(salesID);

            hdsales.Value = salesID.ToString();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["BillDate"] != null)
                        txtBillDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BillDate"]).ToString("dd/MM/yyyy");



                    if (ds.Tables[0].Rows[0]["Customername"] != null)
                    {
                        sCustomer = Convert.ToString(ds.Tables[0].Rows[0]["Customername"]);
                        cmbCustomer.ClearSelection();
                        ListItem li = cmbCustomer.Items.FindByText(System.Web.HttpUtility.HtmlDecode(sCustomer.Trim()));
                        if (li != null) li.Selected = true;
                    }

                    if (ds.Tables[0].Rows[0]["CustomerAddress"] != null)
                        txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerAddress"]);
                    else
                        txtAddress.Text = "";

                    Label pM = (Label)row.FindControl("lblPaymode");
                    strPaymode = pM.Text;

                    drpPaymode.ClearSelection();
                    ListItem pLi = drpPaymode.Items.FindByText(strPaymode.Trim());
                    if (pLi != null) pLi.Selected = true;

                    if (paymodeVisible(strPaymode))
                    {
                        if (ds.Tables[0].Rows[0]["CreditCardNo"] != null)
                            txtCreditCardNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CreditCardNo"]);
                        if (ds.Tables[0].Rows[0]["Debtor"] != null)
                        {
                            drpBankName.ClearSelection();
                            ListItem cli = drpBankName.Items.FindByText(HttpUtility.HtmlDecode(Convert.ToString(ds.Tables[0].Rows[0]["Debtor"])));
                            if (cli != null) cli.Selected = true;
                            //rvbank.Enabled = true;
                            //rvCredit.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[0]["PurchaseReturn"] != null)
                    {
                        drpPurchaseReturn.ClearSelection();
                        drpPurchaseReturn.SelectedItem.Text = Convert.ToString(ds.Tables[0].Rows[0]["PurchaseReturn"]);
                    }

                    if (ds.Tables[0].Rows[0]["PurchaseReturnReason"] != null)
                        txtPRReason.Text = Convert.ToString(ds.Tables[0].Rows[0]["PurchaseReturnReason"]);
                    else
                        txtPRReason.Text = "";


                    if (ds.Tables[0].Rows[0]["Executive"] != null)
                    {
                        drpIncharge.ClearSelection();
                        ListItem cli = drpIncharge.Items.FindByValue(Convert.ToString(ds.Tables[0].Rows[0]["Executive"]));
                        if (cli != null) cli.Selected = true;
                    }
                    else
                        drpIncharge.SelectedIndex = 0;
                    if (ds.Tables[0].Rows[0]["Freight"] != null)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["Freight"]) != "")
                            txtFreight.Text = Convert.ToString(ds.Tables[0].Rows[0]["Freight"]);
                        else
                            txtFreight.Text = "0";

                    }
                    else
                        txtFreight.Text = "0";

                    if (ds.Tables[0].Rows[0]["LoadUnload"] != null)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["LoadUnload"]) != "")
                            txtLU.Text = Convert.ToString(ds.Tables[0].Rows[0]["LoadUnload"]);
                        else
                            txtLU.Text = "0";
                    }
                    else
                        txtLU.Text = "0";

                    //hdContact.Value = row.Cells[13].Text;
                    itemDs = formProduct(salesID);
                    pnlProduct.Visible = true;
                    Session["productDs"] = itemDs;

                    GrdViewItems.DataSource = itemDs;
                    GrdViewItems.DataBind();
                    calcSum();

                    errPanel.Visible = false;
                    ErrMsg.Text = "";

                    if (bl.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                    {
                        cmdSaveProduct.Enabled = false;
                        GrdViewItems.Columns[14].Visible = false;
                        GrdViewItems.Columns[13].Visible = false;
                        cmdSave.Enabled = false;
                        cmdDelete.Enabled = false;
                        cmdUpdate.Enabled = false;
                        lnkBtnAdd.Visible = false;
                    }
                    else
                    {
                        GrdViewItems.Columns[15].Visible = false;
                    }

                }
            }
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

            dc = new DataColumn("BundleNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("Total");
            dt.Columns.Add(dc);

            DataSet tblBundle = new DataSet();

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

                    tblBundle = bl.ListBundleStock(billno, strItemCode, 0, Convert.ToInt32(dQty));

                    if (dR["isrole"] != null)
                    {
                        roleFlag = Convert.ToString(dR["isrole"]);
                        dr["IsRole"] = roleFlag;

                    }

                    if (tblBundle != null)
                    {
                        dr["BundleNo"] = tblBundle.Tables[0].Rows[0].ItemArray[0];
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

                    if (hdStock.Value != "")
                        stock = Convert.ToDouble(hdStock.Value);
                    dr["Roles"] = strRole;
                    dr["Total"] = Convert.ToString(dTotal);
                    itemDs.Tables[0].Rows.Add(dr);
                    strRole = "";
                }
            }
        }
        return itemDs;


    }
    #endregion

    #region GrdRole
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds;
        double qtyCurr = 0;
        double qtyRole = 0;
        double qtyDel = 0;
        int roleID = 0;
        int cnt = 0;
        try
        {
            if (Session["roleDs"] != null)
            {
                ds = (DataSet)Session["roleDs"];
                qtyDel = Convert.ToDouble(ds.Tables[0].Rows[GridView1.Rows[e.RowIndex].DataItemIndex]["Qty"]);
                if (txtQtyAdd.Text.Trim() != "")
                    qtyCurr = Convert.ToDouble(txtQtyAdd.Text) - qtyDel;

                txtQtyAdd.Text = qtyCurr.ToString();
                roleID = Convert.ToInt32(ds.Tables[0].Rows[GridView1.Rows[e.RowIndex].DataItemIndex]["roleID"]);
                ds.Tables[0].Rows[GridView1.Rows[e.RowIndex].DataItemIndex].Delete();

                foreach (ListItem li in drpRoleAvl.Items)
                {
                    if (li.Value == roleID.ToString())
                    {
                        qtyRole = Convert.ToDouble(li.Text);
                        qtyRole = qtyRole + qtyDel;
                        drpRoleAvl.Items.Remove(li);
                        drpRoleAvl.Items.Add(new ListItem(qtyRole.ToString(), roleID.ToString()));

                        cnt = cnt + 1;
                        break;
                    }

                }
                if (cnt == 0)
                {
                    drpRoleAvl.Items.Add(new ListItem(qtyDel.ToString(), roleID.ToString()));
                }
                string roleS = string.Empty;
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    roleS = roleS + dR["RoleID"].ToString() + "_" + dR["Qty"].ToString() + ",";
                }



                hdCurrRole.Value = roleS;
                DataSet dS;
                if (Session["productDs"] != null)
                {
                    dS = (DataSet)Session["productDs"];


                    int curRow = Convert.ToInt32(hdCurrentRow.Value);
                    dS.Tables[0].Rows[curRow].BeginEdit();
                    //if (dS.Tables[0].Rows[curRow]["Roles"].ToString().EndsWith(","))
                    //    dS.Tables[0].Rows[curRow]["Roles"] = dS.Tables[0].Rows[curRow]["Roles"] + strRole;
                    //else
                    dS.Tables[0].Rows[curRow]["Roles"] = roleS;

                    dS.Tables[0].Rows[curRow].EndEdit();
                    dS.Tables[0].Rows[curRow].AcceptChanges();
                    Session["productDs"] = dS;
                    //GrdViewItems.DataSource = (DataSet)Session["productDs"];
                    //GrdViewItems.DataBind(); 
                }

                GridView1.DataSource = ds;
                GridView1.DataBind();

            }

            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    #endregion

    private void calcSum()
    {
        Double sumAmt = 0;
        //Double sumTAmt = 0;
        Double sumVat = 0;
        Double sumCST = 0;
        Double sumDis = 0;
        Double sumNet = 0;
        DataSet ds = new DataSet();
        // ds.ReadXml(Server.MapPath("Reports\\" + hdFilename.Value + "_productsales.xml"));
        ds = (DataSet)GrdViewItems.DataSource;
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Total"] != null)
                        sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDouble(dr["Qty"]), Convert.ToDouble(dr["Rate"]), Convert.ToDouble(dr["Discount"]), Convert.ToDouble(dr["VAT"]), Convert.ToDouble(dr["CST"])));
                    //sumTAmt = sumTAmt + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"]));
                    sumDis = sumDis + GetDis();
                    sumVat = sumVat + GetVat();
                    sumCST = sumCST + GetCST();
                }
            }
        }
        double dFreight = 0;
        double dLU = 0;
        double sumLUFreight = 0;
        if (txtFreight.Text.Trim() != "")
        {
            dFreight = Convert.ToDouble(txtFreight.Text.Trim());
        }
        if (txtLU.Text.Trim() != "")
        {
            dLU = Convert.ToDouble(txtLU.Text.Trim());
        }
        sumLUFreight = dFreight + dLU;
        sumNet = sumNet + sumAmt + dFreight + dLU;
        lblTotalSum.Text = sumAmt.ToString("#0.00");
        lblTotalDis.Text = sumDis.ToString("#0.00");
        lblTotalVAT.Text = sumVat.ToString("#0.00");
        lblTotalCST.Text = sumCST.ToString("#0.00");
        lblFreight.Text = sumLUFreight.ToString("#0.00"); // dFreight.ToString("#0.00");
        lblNet.Text = sumNet.ToString("#0.00");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            int BillNo = 0;
            BillNo = Convert.ToInt32(txtBillnoSrc.Text);
            BindGrid(BillNo);
            //Accordion1.SelectedIndex = 0;
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();

            lblTotalSum.Text = "0";
            lblTotalDis.Text = "0";
            lblTotalVAT.Text = "0";
            lblTotalCST.Text = "0";
            lblFreight.Text = "0";
            lblNet.Text = "0";

            PanelBill.Visible = true;
            //PanelCmd.Visible = false;
            //lnkBtnAdd.Visible = true;

            Reset();

            ResetProduct();

            cmbProdAdd.Enabled = true;
            cmdUpdateProduct.Enabled = false;
            cmdSaveProduct.Enabled = true;

            GridView1.DataSource = null;
            GridView1.DataSource = null;
            Session["productDs"] = null;
            Session["roledata"] = null;
            cmdSave.Enabled = true;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            cmdPrint.Enabled = false;
            errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void drpQtyAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            String[] QtyProd;
            QtyProd = drpQtyAdd.SelectedItem.Text.Split('-');
            txtQtyAdd.Text = QtyProd[0];
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}

