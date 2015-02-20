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
using System.Xml;
using System.IO;
using System.Globalization;


public partial class BundlePurchase : System.Web.UI.Page
{

    private string sDataSource = string.Empty;
    private double amtTotal = 0;
    public double rateTotal = 0;
    public double vatTotal = 0;
    public double disTotal = 0;
    public double cstTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            GrdViewItems.Columns[12].Visible = false;
            GrdViewItems.Columns[13].Visible = true;
            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();
            if (Session["NEWPURCHASE"] != null)
            {
                if (Session["NEWPURCHASE"].ToString() == "Y")
                {
                    PanelCmd.Visible = true;
                    purchasePanel.Visible = true;
                    cmdSave.Enabled = true;
                    cmdUpdate.Enabled = false;
                    cmdDelete.Enabled = false;
                    //AccordionPane2.Visible = true;
                    lnkBtnAdd.Visible = false;
                    hdMode.Value = "New";
                    Reset();
                    lblTotalSum.Text = "0";
                    /*Start Purchase Loading / Unloading Freight Change - March 16*/
                    lblFreight.Text = "0";
                    /*End Purchase Loading / Unloading Freight Change - March 16*/
                    ResetProduct();
                    if (Session["PurchaseBillDate"] != null)
                        txtBillDate.Text = Session["PurchaseBillDate"].ToString();
                    Session["NEWPURCHASE"] = "N";
                    btnCancel.Enabled = true;
                    Session["PurchaseProductDs"] = null;
                    GrdViewItems.DataSource = null;
                    GrdViewItems.DataBind();

                    rowSalesRet.Visible = false;
                    PanelCmd.Visible = false;
                    purchasePanel.Visible = false;
                    lnkBtnAdd.Visible = true;

                }
            }
            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                cmdSaveProduct.Enabled = false;
                GrdViewItems.Columns[8].Visible = false;
                GrdViewItems.Columns[9].Visible = false;
                cmdSave.Enabled = false;
                cmdDelete.Enabled = false;
                cmdUpdate.Enabled = false;
                lnkBtnAdd.Visible = false;
            }


            if (!IsPostBack)
            {
                BindGrid("0", "0");
                GenerateRoleDs();
                hdFilename.Value = System.Guid.NewGuid().ToString();
                loadSupplier();
                loadProducts();
                loadBanks();
                txtBarcode.Attributes.Add("onKeyPress", " return clickButton(event,'" + cmdBarcode.ClientID + "')");
            }
            Session["Filename"] = "Reports//" + hdFilename.Value + "_Product.xml";
            err.Text = "";

            cmdBarcode.Click += new EventHandler(this.txtBarcode_Populated); //Jolo Barcode
            txtBarcode.Attributes.Add("onblur", "txtBarcode_Populated();"); //Jolo Barcode
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void loadBanks()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanks();
        cmbBankName.DataSource = ds;
        cmbBankName.DataBind();
        cmbBankName.DataTextField = "LedgerName";
        cmbBankName.DataValueField = "LedgerID";

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            String strBillno = string.Empty;
            String strTransno = string.Empty;

            strBillno = txtBillnoSrc.Text.Trim();
            strTransno = txtTransNo.Text.Trim();

            //Accordion1.SelectedIndex = 0;
            BindGrid(strBillno, strTransno);
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();
            lblTotalSum.Text = "0";
            purchasePanel.Visible = false;
            PanelBill.Visible = true;
            PanelCmd.Visible = false;
            delFlag.Value = "0";
            Session["PurchaseProductDs"] = null;
            lnkBtnAdd.Visible = true;
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
            Reset();
            ResetProduct();
            txtBillDate.Text = DateTime.Now.ToShortDateString();
            cmbProdAdd.Enabled = true;
            //cmdUpdateProduct.Enabled = false;
            cmdSaveProduct.Enabled = true;
            //btnCancel.Enabled = false;
            //PanelCmd.Visible = false;
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();
            GridView1.DataSource = null;
            GridView1.DataSource = null;
            Session["PurchaseProductDs"] = null;
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
            /*Start Purchase Loading / Unloading Freight Change - March 16*/
            lblFreight.Text = "0";
            /*End Purchase Loading / Unloading Freight Change - March 16*/
            PanelCmd.Visible = false;
            purchasePanel.Visible = false;
            lnkBtnAdd.Visible = true;
            PanelBill.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            int iPurchaseId = 0;
            string connection = string.Empty;

            if (Page.IsValid)
            {
                connection = Request.Cookies["Company"].Value;
                BusinessLogic bll = new BusinessLogic();
                string recondate = txtBillDate.Text.Trim();

                string salesReturn = string.Empty;
                string srReason = string.Empty;

                if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }
                string sBillno = string.Empty;

                int iSupplier = 0;
                int iPaymode = 0;
                string[] sDate;
                string sChequeno = string.Empty;
                int iBank = 0;
                int iPurchase = 0;
                string filename = string.Empty;
                double dTotalAmt = 0;
                iPurchase = Convert.ToInt32(hdPurchase.Value);
                sBillno = txtBillno.Text.Trim();
                DateTime sBilldate;
                string delim = "/";
                char[] delimA = delim.ToCharArray();
                CultureInfo culture = new CultureInfo("pt-BR");

                salesReturn = drpSalesReturn.SelectedItem.Text;
                srReason = txtSRReason.Text.Trim();

                try
                {
                    sDate = txtBillDate.Text.Trim().Split(delimA);
                    sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));
                }
                catch (Exception ex)
                {
                    Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
                    return;
                }
                iSupplier = Convert.ToInt32(cmbSupplier.SelectedItem.Value);
                iPaymode = Convert.ToInt32(cmdPaymode.SelectedItem.Value);
                if (lblTotalSum.Text != string.Empty || lblTotalSum.Text != "0")
                    dTotalAmt = Convert.ToDouble(lblTotalSum.Text);
                /*Start Purchase Loading / Unloading Freight Change - March 16*/
                double dFreight = 0;
                double dLU = 0;
                /*March18*/
                if (txtFreight.Text.Trim() != "")
                    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
                if (txtLU.Text.Trim() != "")
                    dLU = Convert.ToDouble(txtLU.Text.Trim());
                /*March18*/
                dTotalAmt = dTotalAmt + dFreight + dLU;
                /*End Purchase Loading / Unloading Freight Change - March 16*/
                if (iPaymode == 2)
                {
                    sChequeno = Convert.ToString(txtChequeNo.Text);
                    iBank = Convert.ToInt32(cmbBankName.SelectedItem.Value);
                    rvCheque.Enabled = true;
                    rvbank.Enabled = true;
                }
                else
                {
                    rvbank.Enabled = false;
                    rvCheque.Enabled = false;
                }
                filename = hdFilename.Value;
                if (Session["PurchaseProductDs"] != null)
                {
                    DataSet ds = (DataSet)Session["PurchaseProductDs"];


                    if (ds != null)
                    {
                        /*March 18*/
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            /*March 18*/
                            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
                            BusinessLogic bl = new BusinessLogic(sDataSource);
                            int cntB = bl.isDuplicateBill(sBillno, iSupplier);
                            if (cntB > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Duplicate Bill Number')", true);
                                return;
                            }
                            /*Start Purchase Loading / Unloading Freight Change - March 16*/
                            iPurchaseId = bl.InsertBundlePurchase(sBillno, sBilldate, iSupplier, iPaymode, sChequeno, iBank, dTotalAmt, salesReturn, srReason, dFreight, dLU, ds);
                            /*End Purchase Loading / Unloading Freight Change - March 16*/
                            Reset();
                            ResetProduct();
                            if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                                File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
                            purchasePanel.Visible = false;
                            lnkBtnAdd.Visible = true;
                            PanelBill.Visible = false;
                            PanelCmd.Visible = false;
                            hdMode.Value = "Edit";
                            cmdPrint.Enabled = false;
                            Session["purchaseID"] = iPurchaseId.ToString();
                            deleteFile();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Saved Successfully. Updated Bill No. is " + iPurchaseId.ToString() + "')", true);
                            Session["SalesReturn"] = salesReturn;

                            Session["PurchaseProductDs"] = null;
                            Response.Redirect("BundleProductPurchaseBill.aspx");
                            /*March 18*/
                        }
                        /*March 18*/
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No products is choosed for the bill')", true);
                        }
                        /*March 18*/
                    }
                    delFlag.Value = "0";

                    //Accordion1.SelectedIndex = 0;
                    btnCancel.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //cmdSave_Click
    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                int iPurchaseId = 0;
                string connection = Request.Cookies["Company"].Value;
                BusinessLogic bll = new BusinessLogic();
                string recondate = txtBillDate.Text.Trim();
                string salesReturn = string.Empty;
                string srReason = string.Empty;
                string user = string.Empty;

                salesReturn = drpSalesReturn.SelectedItem.Text;
                srReason = txtSRReason.Text.Trim();

                if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                    return;
                }
                string sBillno = string.Empty;
                string sInvoiceno = string.Empty;

                int iSupplier = 0;
                int iPaymode = 0;
                string sChequeno = string.Empty;
                int iBank = 0;
                int iPurchase = 0;
                string filename = string.Empty;
                double dTotalAmt = 0;

                iPurchase = Convert.ToInt32(hdPurchase.Value);
                sBillno = txtBillno.Text.Trim();

                sInvoiceno = txtInvoiveNo.Text.Trim();

                DateTime sBilldate;
                string[] sDate;

                DateTime sInvoicedate;
                string[] IDate;

                string delim = "/";
                char[] delimA = delim.ToCharArray();
                CultureInfo culture = new CultureInfo("pt-BR");
                try
                {
                    sDate = txtBillDate.Text.Trim().Split(delimA);

                    sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

                    IDate = txtInvoiveDate.Text.Trim().Split(delimA);
                    sInvoicedate = new DateTime(Convert.ToInt32(IDate[2].ToString()), Convert.ToInt32(IDate[1].ToString()), Convert.ToInt32(IDate[0].ToString()));


                }
                catch (Exception ex)
                {
                    Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
                    return;
                }
                iSupplier = Convert.ToInt32(cmbSupplier.SelectedItem.Value);
                iPaymode = Convert.ToInt32(cmdPaymode.SelectedItem.Value);
                dTotalAmt = Convert.ToDouble(lblTotalSum.Text);

                string narration2 = string.Empty;

                /*Start Purchase Loading / Unloading Freight Change - March 16*/
                double dFreight = 0;
                double dLU = 0;
                /*March18*/
                if (txtFreight.Text.Trim() != "")
                    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
                if (txtLU.Text.Trim() != "")
                    dLU = Convert.ToDouble(txtLU.Text.Trim());
                /*March18*/
                dTotalAmt = dTotalAmt + dFreight + dLU;
                /*End Purchase Loading / Unloading Freight Change - March 16*/

                if (iPaymode == 2)
                {
                    sChequeno = Convert.ToString(txtChequeNo.Text);
                    iBank = Convert.ToInt32(cmbBankName.SelectedItem.Value);
                }
                filename = hdFilename.Value;
                //BindProduct();
                if (Session["PurchaseProductDs"] != null)
                {
                    DataSet ds = (DataSet)Session["PurchaseProductDs"];

                    if (ds != null)
                    {
                        /*March 18*/
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            /*March 18*/
                            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

                            BusinessLogic bl = new BusinessLogic(sDataSource);
                            /*Start Purchase Loading / Unloading Freight Change - March 16*/
                            iPurchaseId = bl.UpdatePurchase(iPurchase, sBillno, sBilldate, iSupplier, iPaymode, sChequeno, iBank, dTotalAmt, salesReturn, srReason, dFreight, dLU, 0, "NO", ds, "NO", sInvoiceno, sInvoicedate, 0, 0, 0, 0, user, narration2);
                            /*End Purchase Loading / Unloading Freight Change - March 16*/
                            /*Start March 15 Modification */
                            if (iPurchaseId == -2)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase edit is not allowed for this transaction.')", true);
                                /*Start Purchase Stock Negative Change - March 16 -- (Commented the below method) */
                                //Reset();
                                //ResetProduct();
                                /*End Purchase Stock Negative Change - March 16*/
                                return;

                            }
                            /*End March 15 Modification */
                            Reset();
                            ResetProduct();

                            purchasePanel.Visible = false;
                            lnkBtnAdd.Visible = true;
                            //PanelBill.Visible = false;
                            PanelCmd.Visible = false;
                            hdMode.Value = "Edit";
                            cmdPrint.Enabled = false;
                            BindGrid("0", "0");
                            /*March 18*/
                        }
                        /*March 18*/
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No products is choosed for the bill')", true);
                        }
                        /*March 18*/
                    }
                    delFlag.Value = "0";
                    deleteFile();
                    //Accordion1.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Updated Successfully. Updated Bill No. is " + iPurchaseId.ToString() + "')", true);
                    Session["purchaseID"] = iPurchaseId.ToString();
                    deleteFile();
                    btnCancel.Enabled = false;
                    Session["SalesReturn"] = salesReturn;
                    Session["PurchaseProductDs"] = null;
                    Response.Redirect("BundleProductPurchaseBill.aspx");
                }
            }
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
                int iPurchase = 0;
                string sBillNo = txtBillno.Text.Trim();
                iPurchase = Convert.ToInt32(hdPurchase.Value);
                //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.ChkPurchaseBillNo(sDataSource, iPurchase) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to Delete this transaction.')", true);
                    return;
                }

                int del = bl.DeleteBundlePurchase(iPurchase, sBillNo);
                /*Start Purchase Stock Negative Change - March 16*/
                if (del == -2)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase edit is not allowed for this transaction.')", true);
                    return;
                }
                /*End Purchase Stock Negative Change - March 16*/
                Reset();
                ResetProduct();
                /*Start Purchase Stock Negative Change - March 16 -- (Commented the below method)*/
                //if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                //    File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
                /*Start Purchase Stock Negative Change - March 16 -- (Commented the below method)*/
                purchasePanel.Visible = false;
                lnkBtnAdd.Visible = true;
                //PanelBill.Visible = false;
                PanelCmd.Visible = false;
                hdMode.Value = "Delete";
                cmdPrint.Enabled = false;
                delFlag.Value = "0";
                deleteFile();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Deleted Successfully.  Bill No. is " + sBillNo.ToString() + "')", true);
                BindGrid("0", "0");
                btnCancel.Enabled = false;
                Session["PurchaseProductDs"] = null;

                PanelCmd.Visible = false;
                purchasePanel.Visible = false;
                lnkBtnAdd.Visible = true;

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
            //if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
            //    return;
            //}

            Session["PurchaseProductDs"] = null;
            PanelCmd.Visible = true;
            purchasePanel.Visible = true;
            cmdSave.Enabled = true;
            cmdUpdate.Enabled = false;
            cmdDelete.Enabled = false;
            //AccordionPane2.Visible = true;
            lnkBtnAdd.Visible = false;
            hdMode.Value = "New";
            Reset();
            lblTotalSum.Text = "0";
            ResetProduct();
            txtBillDate.Text = DateTime.Now.ToShortDateString();
            XmlDocument xDoc = new XmlDocument();

            if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
            {
                File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
            }
            btnCancel.Enabled = true;
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();
            PanelBill.Visible = false;
            rowSalesRet.Visible = false;
            GrdViewItems.Columns[13].Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void Reset()
    {
        txtBillno.Text = "";
        txtBillDate.Text = "";

        txtFreight.Text = "0";
        txtLU.Text = "0";
        /*Start Purchase Loading / Unloading Freight Change - March 16*/
        foreach (Control control in cmbSupplier.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        /*End Purchase Loading / Unloading Freight Change - March 16*/

        //cmbSupplier.SelectedItem.Text = "";
        cmbSupplier.ClearSelection();

        foreach (Control control in cmdPaymode.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "1";
        }
        cmdPaymode.ClearSelection();

        foreach (Control control in cmbBankName.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        cmbBankName.ClearSelection();

        foreach (Control control in cmbProdAdd.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        cmbProdAdd.ClearSelection();
        txtChequeNo.Text = ""; // cmbBankName.SelectedItem.Text;
        //BindGrid(txtBillnoSrc.Text);
        //Accordion1.SelectedIndex = 1;
        txtSRReason.Text = "";
        drpSalesReturn.SelectedIndex = 0;
        GrdViewItems.DataSource = null;
        GrdViewItems.DataBind();
    }
    private void loadSupplier()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListCreditorDebitor(sDataSource);
        cmbSupplier.DataSource = ds;
        cmbSupplier.DataBind();
        cmbSupplier.DataTextField = "LedgerName";
        cmbSupplier.DataValueField = "LedgerID";

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


    }
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

    protected void GrdViewPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewPurchase.PageIndex = e.NewPageIndex;
            String strBillno = string.Empty;
            string strTransNo = string.Empty;

            if (txtBillnoSrc.Text.Trim() != "")
                strBillno = txtBillnoSrc.Text.Trim();
            else
                strBillno = "0";

            if (txtTransNo.Text.Trim() != "")
                strTransNo = txtTransNo.Text.Trim();
            else
                strTransNo = "0";


            BindGrid(strBillno, strTransNo);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void BindGrid(string strBillno, string strTransNo)
    {


        DataSet ds = new DataSet();
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);

        object usernam = Session["LoggedUserName"];
        if (strBillno == "0" && strTransNo == "0")
            ds = bl.GetPurchase();
        else
            ds = bl.GetPurchaseForId(strBillno, strTransNo);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewPurchase.DataSource = ds.Tables[0].DefaultView;
                GrdViewPurchase.DataBind();
            }
        }
    }
    protected void GrdViewPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
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
            GrdViewItems.Columns[12].Visible = false;
            GrdViewItems.Columns[13].Visible = false;
            string strPaymode = string.Empty;
            int SupplierID = 0;
            int purchaseID = 0;
            GridViewRow row = GrdViewPurchase.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            /*Start Purchase Loading / Unloading Freight Change - March 16*/
            BusinessLogic bl = new BusinessLogic(sDataSource);
            /*End Purchase Loading / Unloading Freight Change - March 16*/

            StringBuilder script = new StringBuilder();
            script.Append("alert('You are not allowed to delete the record. Please contact Administrator.');");

            string recondate = row.Cells[3].Text;
            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                return;
            }



            if (row.Cells[1].Text != "&nbsp;")
                txtBillno.Text = row.Cells[1].Text;
            purchaseID = Convert.ToInt32(GrdViewPurchase.SelectedDataKey.Value);

            /*Start Purchase Loading / Unloading Freight Change - March 16*/
            DataSet ds = bl.GetPurchaseForId(purchaseID);
            if (ds.Tables[0].Rows[0]["Freight"] != null)
                txtFreight.Text = Convert.ToString(ds.Tables[0].Rows[0]["Freight"]);
            else
                txtFreight.Text = "0";

            if (ds.Tables[0].Rows[0]["LoadUnload"] != null)
                txtLU.Text = Convert.ToString(ds.Tables[0].Rows[0]["LoadUnload"]);
            else
                txtLU.Text = "0";
            /*End Purchase Loading / Unloading Freight Change - March 16*/

            if (row.Cells[4].Text != "&nbsp;")
            {
                SupplierID = Convert.ToInt32(row.Cells[5].Text);
                cmbSupplier.ClearSelection();
                ListItem li = cmbSupplier.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(SupplierID.ToString()));
                if (li != null) li.Selected = true;
            }

            Label pM = (Label)row.FindControl("lblPaymode"); //row.Cells[3].Text;
            strPaymode = pM.Text;
            if (paymodeVisible(strPaymode))
            {
                if (row.Cells[5].Text != "&nbsp;")
                    txtChequeNo.Text = row.Cells[6].Text;
                if (row.Cells[6].Text != "&nbsp;")
                {
                    cmbBankName.ClearSelection();
                    ListItem cli = cmbBankName.Items.FindByText(System.Web.HttpUtility.HtmlDecode(row.Cells[6].Text));
                    if (cli != null) cli.Selected = true;
                }

            }
            cmdPaymode.ClearSelection();
            ListItem pLi = cmdPaymode.Items.FindByText(strPaymode.Trim());
            if (pLi != null) pLi.Selected = true;
            if (row.Cells[2].Text != "&nbsp;")
                txtBillDate.Text = Convert.ToDateTime(row.Cells[3].Text).ToString("dd/MM/yyyy");
            if (row.Cells[9].Text != "&nbsp;")
                txtSRReason.Text = row.Cells[9].Text;
            if (row.Cells[8].Text != "&nbsp;")
            {
                drpSalesReturn.ClearSelection();
                drpSalesReturn.SelectedItem.Text = row.Cells[9].Text;
            }
            else
            {
                drpSalesReturn.SelectedIndex = 0;
            }
            if (txtBillnoSrc.Text == "")
                BindGrid("0", "0");
            else
                BindGrid(txtBillnoSrc.Text, txtTransNo.Text);
            //Accordion1.SelectedIndex = 1;

            hdPurchase.Value = purchaseID.ToString();
            DataSet itemDs = formXml();
            Session["PurchaseProductDs"] = itemDs;
            GrdViewItems.DataSource = itemDs;
            GrdViewItems.DataBind();
            //BindProduct();
            calcSum();
            hdMode.Value = "Edit";
            lnkBtnAdd.Visible = true;
            purchasePanel.Visible = true;
            PanelCmd.Visible = true;
            cmdSave.Enabled = false;
            cmdUpdate.Enabled = false;

            cmdDelete.Enabled = true;
            cmdPrint.Enabled = true;
            btnCancel.Enabled = true;
            //ResetProduct();

            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                cmdSave.Enabled = false;
                cmdDelete.Enabled = false;
                cmdUpdate.Enabled = false;
                lnkBtnAdd.Visible = false;
            }

            lnkBtnAdd.Visible = false;
            PanelBill.Visible = false;

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private DataSet formXml()
    {
        int purchaseID = 0;
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = new DataSet();
        purchaseID = Convert.ToInt32(hdPurchase.Value);
        /*March18*/
        DataSet itemDs = null;
        /*March18*/
        DataTable dt;
        DataRow dr;
        DataColumn dc;
        DataSet ds = new DataSet();

        double dTotal = 0;
        double dQty = 0;
        double dCoir = 0;
        double dRate = 0;
        double dNLP = 0;
        string strRole = string.Empty;
        string roleFlag = string.Empty;
        string strBundles = string.Empty;


        double stock = 0;

        string strItemCode = string.Empty;
        DataSet dsRole;
        ds = bl.GetBundlePurchaseItemsForId(purchaseID);
        if (ds != null)
        {

            dt = new DataTable();

            dc = new DataColumn("PurchaseID");
            dt.Columns.Add(dc);

            dc = new DataColumn("itemCode");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductName");
            dt.Columns.Add(dc);

            dc = new DataColumn("ProductDesc");
            dt.Columns.Add(dc);

            dc = new DataColumn("PurchaseRate");
            dt.Columns.Add(dc);

            dc = new DataColumn("NLP");
            dt.Columns.Add(dc);

            dc = new DataColumn("Qty");
            dt.Columns.Add(dc);

            dc = new DataColumn("Coir");
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
            /*March18*/
            itemDs = new DataSet();
            /*March18*/


            itemDs.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    dr = itemDs.Tables[0].NewRow();

                    if (dR["Qty"] != DBNull.Value)
                        dQty = Convert.ToDouble(dR["Qty"]);
                    if (dR["Coir"] != DBNull.Value)
                        dCoir = Convert.ToDouble(dR["Coir"]);

                    if (dR["PurchaseRate"] != DBNull.Value)
                        dRate = Convert.ToDouble(dR["PurchaseRate"]);

                    if (dR["NLP"] != DBNull.Value)
                    {
                        if (dR["NLP"].ToString() != "")
                            dNLP = Convert.ToDouble(dR["NLP"]);
                        else
                            dNLP = 0.0;
                    }


                    dTotal = dQty * dRate;
                    if (dR["ItemCode"] != DBNull.Value)
                    {
                        strItemCode = Convert.ToString(dR["ItemCode"]);
                        dr["itemCode"] = strItemCode;
                    }
                    if (dR["PurchaseID"] != DBNull.Value)
                    {
                        purchaseID = Convert.ToInt32(dR["PurchaseID"]);
                        dr["PurchaseID"] = Convert.ToString(purchaseID);
                    }
                    if (dR["ProductName"] != null)
                        dr["ProductName"] = Convert.ToString(dR["ProductName"]);
                    if (dR["ProductDesc"] != null)
                        dr["ProductDesc"] = Convert.ToString(dR["ProductDesc"]);
                    dr["Qty"] = dQty.ToString();
                    dr["Coir"] = dCoir.ToString();
                    dr["PurchaseRate"] = dRate.ToString();
                    dr["NLP"] = dNLP.ToString();
                    if (dR["Discount"] != null)
                        dr["Discount"] = Convert.ToString(dR["Discount"]);
                    if (dR["VAT"] != null)
                        dr["VAT"] = Convert.ToString(dR["VAT"]);

                    if (dR["CST"] != null)
                        dr["CST"] = Convert.ToString(dR["CST"]);


                    if (dR["isrole"] != null)
                    {
                        roleFlag = Convert.ToString(dR["isrole"]);
                        dr["IsRole"] = roleFlag;

                    }

                    //
                    if (roleFlag == "Y")
                    {

                        //dsRole = bl.ListRoles(billno, strItemCode);
                        //if (dsRole != null)
                        //{
                        //    if (dsRole.Tables[0] != null)
                        //    {
                        //        foreach (DataRow drole in dsRole.Tables[0].Rows)
                        //        {
                        //            strRole = strRole + drole["RoleID"].ToString() + "_" + drole["Qty"].ToString() + ",";

                        //        }

                        //    }
                        //}


                        ////if (strRole.EndsWith(","))
                        ////{
                        ////    strRole = strRole.Remove(strRole.Length - 1, 1);
                        ////}
                        strRole = Convert.ToString(dR["RoleID"]);
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
    private void BindProduct()
    {
        string filename = string.Empty;
        filename = hdFilename.Value;
        DataSet xmlDs = new DataSet();
        if (File.Exists(Server.MapPath("Reports\\" + filename + "_Product.xml")))
        {
            xmlDs.ReadXml(Server.MapPath("Reports\\" + filename + "_Product.xml"));
            if (xmlDs != null)
            {
                if (xmlDs.Tables.Count > 0)
                {
                    //GrdViewItems.DataSource = xmlDs;
                    //GrdViewItems.DataBind();
                    ViewState["xmlDs"] = xmlDs;
                    if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                    {

                        deleteFile();
                        if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                            File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
                    }

                }
                else
                {
                    GrdViewItems.DataSource = null;
                    GrdViewItems.DataBind();
                }
            }

        }

        if (ViewState["xmlDs"] != null)
        {
            GrdViewItems.DataSource = ViewState["xmlDs"];
            GrdViewItems.DataBind();
            calcSum();
        }


    }
    private void BindProductP()
    {
        string filename = string.Empty;
        filename = hdFilename.Value;
        DataSet xmlDs = new DataSet();
        if (File.Exists(Server.MapPath("Reports\\" + filename + "_Product.xml")))
        {
            xmlDs.ReadXml(Server.MapPath("Reports\\" + filename + "_Product.xml"));
            if (xmlDs != null)
            {
                if (xmlDs.Tables.Count > 0)
                {
                    GrdViewItems.DataSource = xmlDs;
                    GrdViewItems.DataBind();
                    calcSum();

                }
                else
                {
                    GrdViewItems.DataSource = null;
                    GrdViewItems.DataBind();
                }
            }
            //File.Delete(Server.MapPath(filename + "_Product.xml")); 
        }

    }
    private void calcSum()
    {
        Double sumAmt = 0;
        //Double sumTAmt = 0;
        Double sumVat = 0;
        Double sumDis = 0;
        Double sumCST = 0;
        Double sumNet = 0;
        DataSet ds = new DataSet();
        //ds.ReadXml(Server.MapPath("Reports\\" + hdFilename.Value + "_product.xml"));

        ds = (DataSet)GrdViewItems.DataSource;
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Total"] != null)
                        sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDouble(dr["Qty"]), Convert.ToDouble(dr["PurchaseRate"]), Convert.ToDouble(dr["Discount"]), Convert.ToDouble(dr["VAT"]), Convert.ToDouble(dr["CST"])));
                    //sumTAmt = sumTAmt + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["PurchaseRate"]));
                    sumDis = sumDis + GetDis();
                    sumVat = sumVat + GetVat();
                    sumCST = sumCST + GetCST();
                }
            }
        }
        /*Start Purchase Loading / Unloading Freight Change - March 16*/
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
        sumNet = sumNet + sumAmt + dFreight + dLU; ;
        /*End Purchase Loading / Unloading Freight Change - March 16*/

        lblTotalSum.Text = sumAmt.ToString("#0.00");
        lblTotalDis.Text = sumDis.ToString("#0.00");
        lblTotalVAT.Text = sumVat.ToString("#0.00");
        lblTotalCST.Text = sumCST.ToString("#0.00");
        lblNet.Text = sumNet.ToString("#0.00");
        /*Start Purchase Loading / Unloading Freight Change - March 16*/
        lblFreight.Text = sumLUFreight.ToString("#0.00");
        /*End Purchase Loading / Unloading Freight Change - March 16*/
    }


    protected void cmdPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                Session["purchaseID"] = hdPurchase.Value;
                delFlag.Value = "0";
                deleteFile();
                Response.Redirect("BundleProductPurchaseBill.aspx");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {



    }
    protected void GrdViewPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void BtnViewDetails_Click(object sender, EventArgs e)
    {

    }
    //public string GetTotal(Decimal qty, Decimal rate, Decimal discount, Decimal VAT)
    //{

    //    Decimal tot = 0;
    //    tot = (qty * rate) - ((qty * rate) * (discount / 100)) + ((qty * rate) * (VAT / 100));
    //    amtTotal = amtTotal + Convert.ToDouble(tot);
    //    disTotal = disTotal + discount; 
    //    rateTotal = rateTotal + rate;
    //    vatTotal = vatTotal + VAT;
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




    protected void GrdViewItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GrdViewItems.EditIndex = e.NewEditIndex;
            //for(int i;i<=GrdViewItems.Columns.Count-1;i++)
            //{
            //    if (i < 4)
            //        GrdViewItems.Columns[i].ReadOnly = true;
            //}
            if (Session["PurchaseProductDs"] != null)
            {
                DataSet ds = (DataSet)Session["PurchaseProductDs"];
                GrdViewItems.DataSource = ds;
                GrdViewItems.DataBind();
            }
            //BindProduct();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GrdViewItems.EditIndex = -1;
            if (Session["PurchaseProductDs"] != null)
            {
                DataSet ds = (DataSet)Session["PurchaseProductDs"];
                GrdViewItems.DataSource = ds;
                GrdViewItems.DataBind();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void drpSalesReturn_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpSalesReturn.SelectedItem.Text == "No")
            {
                rqSalesReturn.Enabled = false;
                rowSalesRet.Visible = false;

            }
            else
            {
                rqSalesReturn.Enabled = true;
                rowSalesRet.Visible = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdPaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnCancel.Enabled = true;
            if (cmdPaymode.SelectedItem.Value == "2")
            {
                //lblCheque.Visible = true;
                //txtChequeNo.Visible = true;
                //lblBankname.Visible = true;
                //cmbBankName.Visible = true;
                rvCheque.Enabled = true;
                rvbank.Enabled = true;
                pnlBank.Visible = true;
            }
            else
            {
                //lblCheque.Visible = false;
                //txtChequeNo.Visible = false;
                //lblBankname.Visible = false;
                //cmbBankName.Visible = false;
                rvCheque.Enabled = false;
                rvbank.Enabled = false;
                pnlBank.Visible = false;
            }
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

            if (Session["PurchaseProductDs"] != null)
            {
                checkDs = (DataSet)Session["PurchaseProductDs"];

                //foreach (DataRow dR in checkDs.Tables[0].Rows)
                //{
                //    if (dR["itemCode"] != null)
                //    {
                //        if (dR["itemCode"].ToString().Trim() == itemCode && dR["isRole"].ToString().Trim() != "Y")
                //        {
                //            dupFlag = true;
                //            break;
                //        }
                //    }
                //}

            }

            if (!dupFlag)
            {
                ds = bl.ListProductDetails(itemCode);
                if (ds != null)
                {
                    lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                    lblProdDescAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);
                    lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                    lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["vat"]);
                    txtNLPAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["NLP"]);
                    txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);
                    hdStock.Value = Convert.ToString(ds.Tables[0].Rows[0]["Stock"]);
                    lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                    if (lblCSTAdd.Text.Trim() == "")
                    {
                        lblCSTAdd.Text = "0";
                    }
                    err.Text = "";
                    txtQtyAdd.Text = "0";
                    txtCoirAdd.Text = "0";
                    if (ds.Tables[0].Rows[0]["Accept_Role"] != null && ds.Tables[0].Rows[0]["Accept_Role"] != DBNull.Value)
                    {
                        if (ds.Tables[0].Rows[0]["Accept_Role"].ToString() == "Y")
                        {
                            //pnlRole.Visible = true;
                            GenerateRoleDs();
                            hdRole.Value = "Y";
                        }
                        else
                        {
                            //txtRole.Text = "";
                            //GridView1.DataSource = null;
                            //GridView1.DataBind();
                            Session["data"] = null;
                            // pnlRole.Visible = false;
                            hdRole.Value = "N";

                        }
                    }
                    else
                    {
                        txtRole.Text = "";
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        Session["data"] = null;
                        pnlRole.Visible = false;
                        txtQtyAdd.Text = "0";
                        txtCoirAdd.Text = "0";
                        hdRole.Value = "N";
                    }
                }
                else
                {
                    lblProdNameAdd.Text = "";
                    lblProdDescAdd.Text = "";
                    lblDisAdd.Text = "";
                    lblVATAdd.Text = "";
                    txtRateAdd.Text = "";
                    txtNLPAdd.Text = "";
                    hdStock.Value = "";
                    err.Text = "Product code is not correct please choose the correct one";
                }
            }
            else
            {
                cmbProdAdd.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);
            }
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
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            BusinessLogic bl = new BusinessLogic(sDataSource);
            bool dupFlag = false;
            DataSet checkDs;
            string itemCode = string.Empty;
            DataSet ds = new DataSet();
            if (cmbProdAdd.SelectedIndex != 0)
            {
                btnCancel.Enabled = true;
                itemCode = cmbProdAdd.SelectedItem.Value;


                if (Session["PurchaseProductDs"] != null)
                {
                    checkDs = (DataSet)Session["PurchaseProductDs"];

                    //foreach (DataRow dR in checkDs.Tables[0].Rows)
                    //{
                    //    if (dR["itemCode"] != null)
                    //    {
                    //        if (dR["itemCode"].ToString().Trim() == itemCode && dR["isRole"].ToString().Trim() != "Y")
                    //        {
                    //            dupFlag = true;
                    //            break;
                    //        }
                    //    }
                    //}
                }
                if (!dupFlag)
                {
                    ds = bl.ListProductDetails(cmbProdAdd.SelectedItem.Value);
                    if (ds != null)
                    {
                        lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                        lblProdDescAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);
                        lblDisAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                        lblVATAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["vat"]);
                        txtRateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);
                        txtNLPAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["NLP"]);
                        hdStock.Value = Convert.ToString(ds.Tables[0].Rows[0]["Stock"]);
                        lblCSTAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["CST"]);
                        if (lblCSTAdd.Text.Trim() == "")
                        {
                            lblCSTAdd.Text = "0";
                        }
                        err.Text = "";
                        txtQtyAdd.Text = "0";
                        txtCoirAdd.Text = "0";
                        if (ds.Tables[0].Rows[0]["Accept_Role"] != null && ds.Tables[0].Rows[0]["Accept_Role"] != DBNull.Value)
                        {
                            if (ds.Tables[0].Rows[0]["Accept_Role"].ToString() == "Y")
                            {
                                //pnlRole.Visible = true;
                                GenerateRoleDs();
                                hdRole.Value = "Y";
                            }
                            else
                            {
                                //txtRole.Text = "";
                                //GridView1.DataSource = null;
                                //GridView1.DataBind();
                                Session["data"] = null;
                                // pnlRole.Visible = false;
                                hdRole.Value = "N";

                            }
                        }
                        else
                        {
                            txtRole.Text = "";
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            Session["data"] = null;
                            pnlRole.Visible = false;
                            txtQtyAdd.Text = "0";
                            txtCoirAdd.Text = "0";
                            hdRole.Value = "N";
                        }
                    }
                    else
                    {
                        lblProdNameAdd.Text = "";
                        lblProdDescAdd.Text = "";
                        lblDisAdd.Text = "";
                        lblVATAdd.Text = "";
                        txtRateAdd.Text = "";
                        hdStock.Value = "";
                        err.Text = "Product code is not correct please choose the correct one";
                    }
                }
                else
                {
                    cmbProdAdd.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);
                }
            }
            else
            {
                err.Text = "Select the Product";
            }
            delFlag.Value = "0";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void cmdSaveProduct_Click(object sender, EventArgs e)
    {
        try
        {
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bll = new BusinessLogic();
            string recondate = txtBillDate.Text.Trim();
            string roleFlag = hdRole.Value;
            DataSet dsRole = new DataSet();
            string strRole = string.Empty;
            string strQty = string.Empty;
            string sDiscount = "";
            string sVat = "";
            string sCST = "";
            Double amt = 0;
            double stock = 0;
            bool dupFlag = false;
            double iQty = 0;
            string itemCode = string.Empty;
            int dotcnt = 0;
            int dotcntQ = 0;
            DataSet ds;
            DataRow dr;
            DataColumn dc;
            DataColumn dcN;
            DataTable dt;
            DataRow drNew;
            string[] aItem = cmbProdAdd.SelectedItem.Text.Split('-');
            string[] arr = strRole.Split(',');
            char[] a1 = txtRateAdd.Text.ToCharArray();
            char[] q1 = txtQtyAdd.Text.ToCharArray();
            char[] q2 = txtCoirAdd.Text.ToCharArray();
            string[] aItemm = cmbProdAdd.SelectedItem.Text.Split('-');
            string[] arr2 = strRole.Split(',');
            //string[] aItem = cmbProdAdd.SelectedItem.Text.Split('-');




            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                return;
            }


            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] == '.')
                    dotcnt++;
            }

            if (txtRateAdd.Text.EndsWith(".") || dotcnt > 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Rate : " + txtRateAdd.Text + "')", true);
                return;
            }

            for (int k = 0; k < q1.Length; k++)
            {
                if (q1[k] == '.')
                    dotcntQ++;
            }
            if (txtQtyAdd.Text.EndsWith(".") || dotcntQ > 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Qty. : " + txtQtyAdd.Text + "')", true);
                return;
            }
            dotcntQ = 0;
            for (int k = 0; k < q2.Length; k++)
            {
                if (q2[k] == '.')
                    dotcntQ++;
            }
            if (txtCoirAdd.Text.EndsWith(".") || dotcntQ > 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Coir : " + txtCoirAdd.Text + "')", true);
                return;
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


            if (cmbProdAdd.SelectedItem.Value == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select The Product')", true);
                return;
            }

            BindProduct();

            //ds = (DataSet)GrdViewItems.DataSource;


            if (hdStock.Value != "")
                stock = Convert.ToDouble(hdStock.Value);

            if (txtQtyAdd.Text.Trim() != "")
                iQty = Convert.ToDouble(txtQtyAdd.Text.Trim());

            if (Session["PurchaseProductDs"] == null)
            {

                ds = new DataSet();
                dt = new DataTable();
                dc = new DataColumn("itemCode");
                dt.Columns.Add(dc);
                dc = new DataColumn("PurchaseID");
                dt.Columns.Add(dc);

                dc = new DataColumn("ProductName");
                dt.Columns.Add(dc);

                dc = new DataColumn("ProductDesc");
                dt.Columns.Add(dc);

                dc = new DataColumn("PurchaseRate");
                dt.Columns.Add(dc);

                dc = new DataColumn("NLP");
                dt.Columns.Add(dc);

                dc = new DataColumn("Qty");
                dt.Columns.Add(dc);

                dc = new DataColumn("Coir");
                dt.Columns.Add(dc);

                dc = new DataColumn("Discount");
                dt.Columns.Add(dc);

                dc = new DataColumn("VAT");
                dt.Columns.Add(dc);

                dc = new DataColumn("CST");
                dt.Columns.Add(dc);

                dc = new DataColumn("Total");
                dt.Columns.Add(dc);

                //dc = new DataColumn("Roles");
                //dt.Columns.Add(dc);


                dc = new DataColumn("isRole");
                dt.Columns.Add(dc);
                ds.Tables.Add(dt);



                drNew = dt.NewRow();
                drNew["itemCode"] = Convert.ToString(aItem[0]);
                drNew["PurchaseID"] = hdPurchase.Value;
                drNew["ProductName"] = cmbProdAdd.SelectedItem.Text;
                drNew["ProductDesc"] = lblProdDescAdd.Text;
                drNew["PurchaseRate"] = txtRateAdd.Text.Trim();
                drNew["NLP"] = txtNLPAdd.Text.Trim();
                drNew["Qty"] = txtQtyAdd.Text.Trim();
                drNew["Coir"] = txtCoirAdd.Text.Trim();

                drNew["Discount"] = sDiscount;

                //drNew["Roles"] = strRole;
                drNew["isRole"] = roleFlag;
                drNew["VAT"] = sVat;
                drNew["CST"] = sCST;
                drNew["Total"] = GetTotal(Convert.ToDouble(txtQtyAdd.Text.Trim()), Convert.ToDouble(txtRateAdd.Text.Trim()), Convert.ToDouble(lblDisAdd.Text), Convert.ToDouble(lblVATAdd.Text), Convert.ToDouble(lblCSTAdd.Text));


                ds.Tables[0].Rows.Add(drNew);
                Session["PurchaseProductDs"] = ds;
                //ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
                //BindProduct();
                //ResetProduct();
            }
            else
            {
                ds = (DataSet)Session["PurchaseProductDs"];
                itemCode = cmbProdAdd.SelectedItem.Value;
                //foreach (DataRow dR in ds.Tables[0].Rows)
                //{
                //    if (dR["itemCode"].ToString().Trim() == itemCode && dR["isRole"].ToString() != "Y")
                //    {
                //        dupFlag = true;
                //        break;
                //    }
                //}
                if (!dupFlag)
                {

                    dr = ds.Tables[0].NewRow();
                    dr["itemCode"] = Convert.ToString(aItem[0]);
                    dr["PurchaseID"] = hdPurchase.Value;
                    dr["ProductName"] = cmbProdAdd.SelectedItem.Value;
                    dr["ProductDesc"] = lblProdDescAdd.Text;
                    dr["PurchaseRate"] = txtRateAdd.Text.Trim();
                    dr["NLP"] = txtNLPAdd.Text.Trim();
                    dr["Qty"] = txtQtyAdd.Text.Trim();
                    dr["Coir"] = txtCoirAdd.Text.Trim();

                    dr["Discount"] = sDiscount;
                    //dr["Roles"] = strRole;
                    dr["isRole"] = roleFlag;
                    dr["VAT"] = sVat;
                    dr["CST"] = sCST;
                    dr["Total"] = GetTotal(Convert.ToDouble(txtQtyAdd.Text.Trim()), Convert.ToDouble(txtRateAdd.Text.Trim()), Convert.ToDouble(lblDisAdd.Text), Convert.ToDouble(lblVATAdd.Text), Convert.ToDouble(lblCSTAdd.Text));

                    ds.Tables[0].Rows.Add(dr);
                    //ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));

                    //BindProduct();
                    //ResetProduct();
                    //delFlag.Value = "0";

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Code is already present')", true);
                }

            }
            GrdViewItems.DataSource = ds;
            GrdViewItems.DataBind();
            calcSum();
            ResetProduct();

            txtRole.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            Session["data"] = null;
            pnlRole.Visible = false;
            txtQtyAdd.Text = "0";
            hdRole.Value = "N";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void ResetProduct()
    {
        lblProdNameAdd.Text = "";
        lblProdDescAdd.Text = "";
        lblDisAdd.Text = "0";
        lblVATAdd.Text = "0";
        lblDisAdd.Text = "0";
        lblCSTAdd.Text = "0";
        txtQtyAdd.Text = "0";
        txtRateAdd.Text = "0";
        txtNLPAdd.Text = "0";

        foreach (Control control in cmbProdAdd.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        cmbProdAdd.ClearSelection();



    }
    protected void GrdViewItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int i;
            i = GrdViewItems.Rows[e.RowIndex].DataItemIndex;
            TextBox txtQtyEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtQty");
            TextBox txtCoirED = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtCoir");
            TextBox txtRateEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtRate");
            TextBox txtNLPEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtNLP");
            TextBox txtVatEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtVat");
            TextBox txtDisEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtDiscount");
            TextBox txtCSTEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtCST");
            if (txtDisEd.Text == "")
                txtDisEd.Text = "0";
            if (txtVatEd.Text == "")
                txtVatEd.Text = "0";

            double dis = 0.0;
            double vat = 0.0;
            double cst = 0.0;
            if (txtDisEd.Text.Trim() == "")
                dis = 0;
            else
                dis = Convert.ToDouble(txtDisEd.Text);

            if (txtVatEd.Text.Trim() == "")
                vat = 0;
            else
                vat = Convert.ToDouble(txtVatEd.Text);

            if (txtCSTEd.Text.Trim() == "")
                cst = 0;
            else
                cst = Convert.ToDouble(txtCSTEd.Text);
            if (dis < 0 || dis > 100)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Discount')", true);
                txtDisEd.Text = "0";
                return;
            }

            if (vat < 0 || vat > 100)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid VAT')", true);
                txtVatEd.Text = "0";
                return;
            }
            if (cst < 0 || cst > 100)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid CST')", true);
                txtCSTEd.Text = "0";
                return;
            }

            GrdViewItems.EditIndex = -1;
            //BindProduct();
            if (Session["PurchaseProductDs"] != null)
            {
                GrdViewItems.DataSource = (DataSet)Session["PurchaseProductDs"];
                GrdViewItems.DataBind();
                DataSet ds = (DataSet)GrdViewItems.DataSource;
                ds.Tables[0].Rows[i]["Qty"] = txtQtyEd.Text;
                ds.Tables[0].Rows[i]["Coir"] = txtCoirED.Text;
                ds.Tables[0].Rows[i]["PurchaseRate"] = txtRateEd.Text;
                ds.Tables[0].Rows[i]["NLP"] = txtNLPEd.Text;
                ds.Tables[0].Rows[i]["Discount"] = txtDisEd.Text;
                ds.Tables[0].Rows[i]["VAT"] = txtVatEd.Text;
                ds.Tables[0].Rows[i]["CST"] = txtCSTEd.Text;

                GrdViewItems.DataSource = ds;
                GrdViewItems.DataBind();
                //ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
                //BindProduct();
                calcSum();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewItems.PageIndex = e.NewPageIndex;
            BindProduct();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (Session["PurchaseProductDs"] != null)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                GrdViewItems.DataSource = (DataSet)Session["PurchaseProductDs"];
                GrdViewItems.DataBind();
                DataSet ds = (DataSet)GrdViewItems.DataSource;

                //int rtype = bl.DeletetblBundle(Convert.ToInt32(ds.Tables[0].Rows[e.RowIndex].ItemArray[0].ToString()), ds.Tables[0].Rows[e.RowIndex].ItemArray[1].ToString(), Convert.ToInt32(ds.Tables[0].Rows[e.RowIndex].ItemArray[6].ToString()));
                //if (rtype == 1)
                //{


                ds.Tables[0].Rows[GrdViewItems.Rows[e.RowIndex].DataItemIndex].Delete();
                //ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
                //BindProduct();
                GrdViewItems.DataSource = ds;
                GrdViewItems.DataBind();
                /*March 18*/
                Session["PurchaseProductDs"] = ds;
                /*March 18*/
                calcSum();
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item delete is not allowed for this transaction.')", true);
                //}
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewItems_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates2(GrdViewItems, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (GrdViewItems.EditIndex == e.Row.RowIndex)
                {
                    CompareValidator cv = new CompareValidator();
                    cv.ID = "cDis";
                    cv.ControlToValidate = "txtDiscount";
                    cv.ValueToCompare = "100";
                    cv.Type = ValidationDataType.Double;
                    cv.Operator = ValidationCompareOperator.LessThanEqual;
                    cv.ErrorMessage = "Invalid Discount";
                    cv.SetFocusOnError = true;
                    e.Row.Cells[5].Controls.Add(cv);

                    CompareValidator cv2 = new CompareValidator();
                    cv2.ID = "cVat";
                    cv2.ControlToValidate = "txtVAT";
                    cv2.ValueToCompare = "100";
                    cv2.Type = ValidationDataType.Double;
                    cv2.Operator = ValidationCompareOperator.LessThanEqual;
                    cv2.ErrorMessage = "Invalid VAT";
                    cv2.SetFocusOnError = true;

                    e.Row.Cells[6].Controls.Add(cv2);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewPurchase_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewPurchase, e.Row, this);
            }
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
            GrdViewPurchase.PageIndex = ((DropDownList)sender).SelectedIndex;
            String strBillno = string.Empty;
            string strTransNo = string.Empty;

            if (txtBillnoSrc.Text.Trim() != "")
                strBillno = txtBillnoSrc.Text.Trim();
            else
                strBillno = "0";

            if (txtTransNo.Text.Trim() != "")
                strTransNo = txtTransNo.Text.Trim();
            else
                strTransNo = "0";

            BindGrid(strBillno, strTransNo);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void ddlPageSelector2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdViewItems.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindProduct();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void deleteFile()
    {
        if (Session["Filename"] != null)
        {
            string delFilename = Session["Filename"].ToString();
            if (File.Exists(Server.MapPath("Reports\\" + delFilename)))
                File.Delete(Server.MapPath("Reports\\" + delFilename));
        }
    }


    protected void GrdView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            DataSet ds = (DataSet)Session["data"];
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnCLick_Click(object sender, EventArgs e)
    {
        try
        {
            double dblRole = 0.0;
            double dblRate = 0.0;
            DataSet ds = (DataSet)Session["data"];
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = txtRole.Text;
            //  dr[1] = txtRate.Text;

            ds.Tables[0].Rows.Add(dr);
            GridView1.DataSource = ds;
            GridView1.DataBind();

            // txtRate.Text = "";
            if (txtRole.Text.Trim() != "")
            {
                dblRole = Convert.ToDouble(txtRole.Text);
            }

            if (txtQtyAdd.Text.Trim() != "")
                txtQtyAdd.Text = Convert.ToString(Convert.ToDouble(txtQtyAdd.Text) + dblRole);
            else
                txtQtyAdd.Text = Convert.ToString(dblRole);

            txtRole.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;
            DataSet ds = (DataSet)Session["data"];

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int i;
            i = GridView1.Rows[e.RowIndex].DataItemIndex;


            TextBox txtQty = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0];


            GridView1.EditIndex = -1;
            DataSet ds1 = (DataSet)Session["data"];

            GridView1.DataSource = ds1;
            GridView1.DataBind();

            DataSet ds = (DataSet)GridView1.DataSource;

            ds.Tables[0].Rows[i]["Qty"] = txtQty.Text;

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void GenerateRoleDs()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dcQty = new DataColumn("Qty");


        dt.Columns.Add(dcQty);


        ds.Tables.Add(dt);
        Session["data"] = ds;
    }
    protected void GrdView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            DataSet ds = (DataSet)Session["data"];
            double dblRole = 0;
            if (ds.Tables[0].Rows[GridView1.Rows[e.RowIndex].DataItemIndex]["Qty"] != null)
                if (ds.Tables[0].Rows[GridView1.Rows[e.RowIndex].DataItemIndex]["Qty"].ToString() != "")
                    dblRole = Convert.ToDouble(ds.Tables[0].Rows[GridView1.Rows[e.RowIndex].DataItemIndex]["Qty"]);

            ds.Tables[0].Rows[GridView1.Rows[e.RowIndex].DataItemIndex].Delete();
            if (txtQtyAdd.Text.Trim() != "")
                txtQtyAdd.Text = Convert.ToString(Convert.ToDouble(txtQtyAdd.Text) - dblRole);

            txtRole.Text = "";
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void GrdViewItems_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
