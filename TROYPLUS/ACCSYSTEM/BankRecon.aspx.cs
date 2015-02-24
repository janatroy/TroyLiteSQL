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
using System.Collections.Generic;

public partial class BankRecon : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    private double amtTotal = 0;
    double disTotalRate = 0.0;
    public double rateTotal = 0;
    public double vatTotal = 0;
    public double disTotal = 0;
    public double cstTotal = 0;
    string BarCodeRequired = string.Empty;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                GrdViewPurchase.Columns[14].Visible = false;
                lnkBtnAdd.Visible = false;
                pnlSearch.Visible = false;
            }
            GrdViewPurchase.PageSize = 8;

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);

            if (bl.CheckUserHaveAdd(usernam, "BNKREC"))
            {
                lnkBtnAdd.Enabled = false;
                lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
            }
            else
            {
                lnkBtnAdd.Enabled = true;
                lnkBtnAdd.ToolTip = "Click to Add New ";
            }


            CheckSMSRequired();

            if (Session["SMSREQUIRED"] != null)
            {
                if (Session["SMSREQUIRED"].ToString() == "NO")
                    hdSMSRequired.Value = "NO";
                else
                    hdSMSRequired.Value = "YES";
            }
            else
            {
                hdSMSRequired.Value = "NO";
            }

            if (Session["EMAILREQUIRED"] != null)
            {
                if (Session["EMAILREQUIRED"].ToString() == "NO")
                    hdEmailRequired.Value = "NO";
                else
                    hdEmailRequired.Value = "YES";
            }
            else
            {
                hdEmailRequired.Value = "NO";
            }

            if (!IsPostBack)
            {
                BindCurrencyLabels();

                BindGrid("0", "0");
                //hdFilename.Value = System.Guid.NewGuid().ToString();

                //txtStartDate.Text = DateTime.Now.ToShortDateString();

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtStartDate.Text = dtaa;

                //txtEndDate.Text = DateTime.Now.ToShortDateString();

                fillCustbank();
                ddlbank.Enabled = true;
                ddlCustomer.Enabled = false;

            }
            //Session["Filename"] = "Reports//" + hdFilename.Value + "_Product.xml";
            //err.Text = "";
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
            txtTransNo.Text = "";
            txtBillnoSrc.Text = "";
           // ddCriteria.SelectedIndex = 0;
            BindGrid("", "");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private void CheckSMSRequired()
    {
        DataSet appSettings;
        string smsRequired = string.Empty;
        string emailRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "EMAILREQ")
                {
                    emailRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["EMAILREQUIRED"] = emailRequired.Trim().ToUpper();
                }

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "OWNERMOB")
                {
                    Session["OWNERMOB"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }

            }
        }
        else
        {
            BusinessLogic bl = new BusinessLogic();
            DataSet ds = bl.GetAppSettings(Request.Cookies["Company"].Value);

            if (ds != null)
                Session["AppSettings"] = ds;

            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "EMAILREQ")
                {
                    emailRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["EMAILREQUIRED"] = emailRequired.Trim().ToUpper();
                }

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "OWNERMOB")
                {
                    Session["OWNERMOB"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }

            }
        }
    }

    protected void opnbank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            if (chk.SelectedItem.Text == "Bank")
            {
                ddlbank.Enabled = true;
                ddlCustomer.Enabled = false;
            }
            else
            {
                ddlbank.Enabled = false;
                ddlCustomer.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void fillCustbank()
    {
        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.ListSundryDebtorsPaymentMade(sDataSource);

        DataSet dst = new DataSet();
        dst = bl.ListBanks();

        ddlCustomer.DataSource = ds;
        ddlCustomer.DataTextField = "LedgerName";
        ddlCustomer.DataValueField = "LedgerID";
        ddlCustomer.DataBind();
        //ddlCustomer.Items.Insert(0, "All");

        ddlbank.DataSource = dst;
        ddlbank.DataTextField = "LedgerName";
        ddlbank.DataValueField = "LedgerID";
        ddlbank.DataBind();
        //ddlbank.Items.Insert(0, "All");
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        //List<int> list = new List<int>();
        //if (ViewState["SelectedRecords"] != null)
        //{
        //    list = (List<int>)ViewState["SelectedRecords"];
        //}

        //int count = GrdViewItems.Rows.Count;
        //int a = 0;
        //String[] sel_case_keepinarray = new String[count];
        //TextBox chking_cas_number;

        //foreach (GridViewRow rowItem in GrdViewItems.Rows)
        //{
        //    chking_cas_number = (TextBox)(rowItem.Cells[0].FindControl("txtMRP"));

        //    var selectedKey = int.Parse(GrdViewItems.DataKeys[rowItem.RowIndex].Value.ToString());
        //    if (chking_cas_number.Text != "0")
        //    {
        //        if (!list.Contains(selectedKey))
        //        {
        //            list.Add(selectedKey);
        //            chking_cas_number.Text = ((TextBox)sender).Text;
        //        }
        //    }
        //    else
        //    {
        //        if (list.Contains(selectedKey))
        //        {
        //            list.Remove(selectedKey);
        //            chking_cas_number.Text = "0";
        //        }
        //    }
        //}

        //ViewState["SelectedRecords"] = list;
    }

 

    private void BindCurrencyLabels()
    {
        DataSet appSettings;
        string currency = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "CURRENCY")
                {
                    currency = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["CurrencyType"] = currency;
                }

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "BARCODE")
                {
                    BarCodeRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }

        if ((currency == "INR") || (currency.ToUpper() == "RS"))
        {
            
        }

        if (currency == "GBP")
        {
            

        }

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var strBillno = txtBillnoSrc.Text.Trim();
            var strTransno = txtTransNo.Text.Trim();

            //Accordion1.SelectedIndex = 0;
            BindGrid(strBillno, strTransno);
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();

            delFlag.Value = "0";
            Session["PurchaseProductDs"] = null;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdprodcancel_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupProduct.Hide();
            ModalPopupPurchase.Show();
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
            //Reset();

            //txtBillDate.Text = DateTime.Now.ToShortDateString();

            //btnCancel.Enabled = false;
            //PanelCmd.Visible = false;
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();

            ModalPopupPurchase.Hide();
            //UP1.Update();
            //UpdatePnlMaster.Update();
            BusinessLogic objChk = new BusinessLogic();
            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                GrdViewPurchase.Columns[14].Visible = false;
                lnkBtnAdd.Visible = false;
                pnlSearch.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void cmdSave_Click(object sender, EventArgs e)
    //{
    //    int iPurchaseId = 0;
    //    string connection = string.Empty;

    //    if (Page.IsValid)
    //    {

    //        if (Session["PurchaseProductDs"] == null)
    //        {
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products before save')", true);
    //            return;
    //        }

    //        connection = Request.Cookies["Company"].Value;
    //        BusinessLogic bll = new BusinessLogic();
    //        //string recondate = txtBillDate.Text.Trim();

    //        string salesReturn = string.Empty;
    //        string intTrans = string.Empty;
    //        string deliveryNote = string.Empty;
    //        string srReason = string.Empty;

    //        //if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
    //        //{

    //        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
    //        //    return;
    //        //}


            

    //        string sBillno = string.Empty;
    //        string sInvoiceno = string.Empty;

    //        int iSupplier = 0;
    //        int iPaymode = 0;
    //        string[] sDate;
    //        string[] IDate;

    //        string sChequeno = string.Empty;
    //        int iBank = 0;
    //        int iPurchase = 0;
    //        string filename = string.Empty;
    //        double dTotalAmt = 0;
    //        iPurchase = Convert.ToInt32(hdPurchase.Value);
    //        //sBillno = txtBillno.Text.Trim();
    //        DateTime sBilldate;
    //        DateTime sInvoicedate;

    //        string delim = "/";
    //        char[] delimA = delim.ToCharArray();
    //        CultureInfo culture = new CultureInfo("pt-BR");

    //        //salesReturn = drpSalesReturn.SelectedValue;
    //        //intTrans = drpIntTrans.SelectedValue;
    //        //deliveryNote = ddDeliveryNote.SelectedValue;
    //        //srReason = txtSRReason.Text.Trim();
    //        //iPaymode = Convert.ToInt32(cmdPaymode.SelectedItem.Value);
            
    //        //sInvoiceno = txtInvoiveNo.Text.Trim();


    //        int cnt = 0;

    //        if (intTrans == "YES")
    //            cnt = cnt + 1;
    //        if (deliveryNote == "YES")
    //            cnt = cnt + 1;
    //        if (salesReturn == "YES")
    //            cnt = cnt + 1;

    //        if (cnt > 1)
    //        {
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Either one of Sales Return or Delivery Note or Internal Transfer should be selected')", true);
    //            tabs2.ActiveTabIndex = 1;
    //            //updatePnlSales.Update();
    //            return;
    //        }

    //        //if (iPaymode == 2)
    //        //{
    //        //    sChequeno = Convert.ToString(txtChequeNo.Text);
    //        //    iBank = Convert.ToInt32(cmbBankName.SelectedItem.Value);
    //        //    rvCheque.Enabled = true;
    //        //    rvbank.Enabled = true;
    //        //}
    //        //else
    //        //{
    //        //    rvbank.Enabled = false;
    //        //    rvCheque.Enabled = false;
    //        //}

    //        Page.Validate("purchaseval");

    //        if (!Page.IsValid)
    //        {
    //            StringBuilder msg = new StringBuilder();

    //            foreach (IValidator validator in Page.Validators)
    //            {
    //                if (!validator.IsValid)
    //                {
    //                    msg.Append(" - " + validator.ErrorMessage);
    //                    msg.Append("\\n");
    //                }
    //            }

    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg.ToString() + "');", true);
    //            return;
    //        }

    //        try
    //        {
    //            //sDate = txtBillDate.Text.Trim().Split(delimA);
    //            //sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

    //            //IDate = txtInvoiveDate.Text.Trim().Split(delimA);
    //            //sInvoicedate = new DateTime(Convert.ToInt32(IDate[2].ToString()), Convert.ToInt32(IDate[1].ToString()), Convert.ToInt32(IDate[0].ToString()));
    //        }
    //        catch (Exception ex)
    //        {
    //            Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
    //            return;
    //        }
    //        //iSupplier = Convert.ToInt32(cmbSupplier.SelectedItem.Value);

    //        if (lblTotalSum.Text != string.Empty || lblTotalSum.Text != "0")
    //            dTotalAmt = Convert.ToDouble(lblTotalSum.Text);
    //        /*Start Purchase Loading / Unloading Freight Change - March 16*/
    //        double dFreight = 0;
    //        double dLU = 0;
    //        /*March18*/
    //        if (txtFreight.Text.Trim() != "")
    //            dFreight = Convert.ToDouble(txtFreight.Text.Trim());
    //        if (txtLU.Text.Trim() != "")
    //            dLU = Convert.ToDouble(txtLU.Text.Trim());
    //        /*March18*/
    //        dTotalAmt = dTotalAmt + dFreight + dLU;

    //        int BilitID = int.Parse(ddBilts.SelectedValue);
    //        /*End Purchase Loading / Unloading Freight Change - March 16*/

    //        filename = hdFilename.Value;
    //        if (Session["PurchaseProductDs"] != null)
    //        {
    //            DataSet ds = (DataSet)Session["PurchaseProductDs"];


    //            if (ds != null)
    //            {
    //                /*March 18*/
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    /*March 18*/
    //                    //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
    //                    BusinessLogic bl = new BusinessLogic(sDataSource);
    //                    int cntB = bl.isDuplicateBill(sBillno, iSupplier);
    //                    if (cntB > 0)
    //                    {
    //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Duplicate Bill Number')", true);
    //                        return;
    //                    }
    //                    /*Start Purchase Loading / Unloading Freight Change - March 16*/
    //                    /*Start InvoiceNo and InvoiceDate - Jan 26*/
    //                    //iPurchaseId = bl.InsertPurchase(sBillno, sBilldate, iSupplier, iPaymode, sChequeno, iBank, dTotalAmt, salesReturn, srReason, dFreight, dLU, BilitID, intTrans, ds, deliveryNote, sInvoiceno, sInvoicedate);
    //                    /*End InvoiceNo and InvoiceDate - Jan 26*/
    //                    /*End Purchase Loading / Unloading Freight Change - March 16*/
    //                    Reset();
    //                    ResetProduct();

    //                    hdMode.Value = "Edit";
    //                    Session["purchaseID"] = iPurchaseId.ToString();
    //                    deleteFile();
    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Saved Successfully. Updated Bill No. is " + iPurchaseId.ToString() + "')", true);
    //                    Session["SalesReturn"] = salesReturn.ToUpper();

    //                    Session["PurchaseProductDs"] = null;
    //                    Response.Redirect("ProductPurchaseBill.aspx?SID=" + iPurchaseId.ToString() + "&RT=" + salesReturn);
    //                    /*March 18*/
    //                }
    //                /*March 18*/
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No products is choosed for the bill')", true);
    //                }
    //                /*March 18*/
    //            }
    //            delFlag.Value = "0";

    //            //Accordion1.SelectedIndex = 0;
    //            btnCancel.Enabled = false;
    //        }
    //    }
    //}

    ////cmdSave_Click
    //protected void cmdUpdate_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        int iPurchaseId = 0;
    //        string connection = Request.Cookies["Company"].Value;
    //        BusinessLogic bll = new BusinessLogic();
    //        //string recondate = txtBillDate.Text.Trim();
    //        string salesReturn = string.Empty;
    //        string intTrans = string.Empty;
    //        string deliveryNote = string.Empty;
    //        string srReason = string.Empty;
    //        salesReturn = drpSalesReturn.SelectedItem.Text;
    //        intTrans = drpIntTrans.SelectedValue;
    //        deliveryNote = ddDeliveryNote.SelectedValue;
    //        srReason = txtSRReason.Text.Trim();

    //        if (Session["PurchaseProductDs"] == null)
    //        {
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products before save')", true);
    //            return;
    //        }

    //        //if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
    //        //{

    //        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
    //        //    return;
    //        //}

            

    //        int cnt = 0;

    //        if (intTrans == "YES")
    //            cnt = cnt + 1;
    //        if (deliveryNote == "YES")
    //            cnt = cnt + 1;
    //        if (salesReturn == "YES")
    //            cnt = cnt + 1;

    //        if (cnt > 1)
    //        {
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Either one of Sales Return or Delivery Note or Internal Transfer should be selected')", true);
    //            tabs2.ActiveTabIndex = 1;
    //            //updatePnlSales.Update();
    //            return;
    //        }

    //        string sBillno = string.Empty;
    //        string sInvoiceno = string.Empty;


    //        int iSupplier = 0;
    //        int iPaymode = 0;
    //        string sChequeno = string.Empty;
    //        int iBank = 0;
    //        int iPurchase = 0;
    //        string filename = string.Empty;
    //        double dTotalAmt = 0;

    //        iPurchase = Convert.ToInt32(hdPurchase.Value);
    //        //sBillno = txtBillno.Text.Trim();
    //        //sInvoiceno = txtInvoiveNo.Text.Trim();

    //        DateTime sBilldate;
    //        DateTime sInvoicedate;

    //        string[] sDate;
    //        string[] IDate;

    //        string delim = "/";
    //        char[] delimA = delim.ToCharArray();
    //        CultureInfo culture = new CultureInfo("pt-BR");
    //        //iPaymode = Convert.ToInt32(cmdPaymode.SelectedItem.Value);

    //        if (iPaymode == 2)
    //        {
    //            //sChequeno = Convert.ToString(txtChequeNo.Text);
    //            //iBank = Convert.ToInt32(cmbBankName.SelectedItem.Value);
    //            //rvbank.Enabled = true;
    //            //rvCheque.Enabled = true;
    //        }
    //        else
    //        {
    //            //rvbank.Enabled = false;
    //            //rvCheque.Enabled = false;
    //        }

    //        Page.Validate("purchaseval");

    //        if (!Page.IsValid)
    //        {
    //            StringBuilder msg = new StringBuilder();

    //            foreach (IValidator validator in Page.Validators)
    //            {
    //                if (!validator.IsValid)
    //                {
    //                    msg.Append(" - " + validator.ErrorMessage);
    //                    msg.Append("\\n");
    //                }
    //            }

    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg.ToString() + "');", true);
    //            return;
    //        }

    //        try
    //        {
    //            //sDate = txtBillDate.Text.Trim().Split(delimA);
    //            //sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

    //            //IDate = txtInvoiveDate.Text.Trim().Split(delimA);
    //            //sInvoicedate = new DateTime(Convert.ToInt32(IDate[2].ToString()), Convert.ToInt32(IDate[1].ToString()), Convert.ToInt32(IDate[0].ToString()));
    //        }
    //        catch (Exception ex)
    //        {
    //            Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
    //            return;
    //        }
    //        //iSupplier = Convert.ToInt32(cmbSupplier.SelectedItem.Value);

    //        dTotalAmt = Convert.ToDouble(lblTotalSum.Text);

    //        /*Start Purchase Loading / Unloading Freight Change - March 16*/
    //        double dFreight = 0;
    //        double dLU = 0;
    //        /*March18*/
    //        if (txtFreight.Text.Trim() != "")
    //            dFreight = Convert.ToDouble(txtFreight.Text.Trim());
    //        if (txtLU.Text.Trim() != "")
    //            dLU = Convert.ToDouble(txtLU.Text.Trim());
    //        /*March18*/
    //        dTotalAmt = dTotalAmt + dFreight + dLU;
    //        /*End Purchase Loading / Unloading Freight Change - March 16*/
    //        int BilitID = int.Parse(ddBilts.SelectedValue);

    //        filename = hdFilename.Value;
    //        //BindProduct();
    //        if (Session["PurchaseProductDs"] != null)
    //        {
    //            DataSet ds = (DataSet)Session["PurchaseProductDs"];

    //            if (ds != null)
    //            {
    //                /*March 18*/
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    /*March 18*/
    //                    //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

    //                    BusinessLogic bl = new BusinessLogic(sDataSource);
    //                    /*Start Purchase Loading / Unloading Freight Change - March 16*/
    //                    /*Start InvoiceNo, InvoiceDate*/
    //                    //iPurchaseId = bl.UpdatePurchase(iPurchase, sBillno, sBilldate, iSupplier, iPaymode, sChequeno, iBank, dTotalAmt, salesReturn, srReason, dFreight, dLU, BilitID, intTrans, ds, deliveryNote, sInvoiceno, sInvoicedate);
    //                    /*End Purchase Loading / Unloading Freight Change - March 16*/
    //                    /*Start March 15 Modification */
    //                    if (iPurchaseId == -2)
    //                    {
    //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase edit is not allowed for this transaction.')", true);
    //                        /*Start Purchase Stock Negative Change - March 16 -- (Commented the below method) */
    //                        //Reset();
    //                        //ResetProduct();
    //                        /*End Purchase Stock Negative Change - March 16*/
    //                        return;

    //                    }
    //                    /*End March 15 Modification */
    //                    Reset();
    //                    ResetProduct();

    //                    //purchasePanel.Visible = false;
    //                    lnkBtnAdd.Visible = true;
    //                    pnlSearch.Visible = true;
    //                    //PanelBill.Visible = false;
    //                    PanelCmd.Visible = false;
    //                    hdMode.Value = "Edit";
    //                    cmdPrint.Enabled = false;
    //                    BindGrid("0", "0");
    //                    /*March 18*/
    //                }
    //                /*March 18*/
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No products is choosed for the bill')", true);
    //                }
    //                /*March 18*/
    //            }
    //            delFlag.Value = "0";
    //            deleteFile();
    //            //Accordion1.SelectedIndex = 0;
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Updated Successfully. Updated Bill No. is " + iPurchaseId.ToString() + "')", true);
    //            Session["purchaseID"] = iPurchaseId.ToString();
    //            deleteFile();
    //            btnCancel.Enabled = false;
    //            Session["SalesReturn"] = salesReturn;
    //            Session["PurchaseProductDs"] = null;
    //            Response.Redirect("ProductPurchaseBill.aspx?SID=" + iPurchaseId.ToString() + "&RT=" + salesReturn.ToUpper());

    //        }
    //    }
    //}

    protected void GrdViewItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string strItemCode = string.Empty;
        //string strRoleFlag = string.Empty;
        //DataSet ds = new DataSet();
        //GridViewRow row = GrdViewItems.SelectedRow;

        //BusinessLogic bl = new BusinessLogic(sDataSource);
        //try
        //{
        //    hdCurrentRow.Value = Convert.ToString(row.DataItemIndex);

        //    if (row.Cells[0].Text != "&nbsp;")
        //    {
        //        strItemCode = row.Cells[0].Text.Trim().Replace("&quot;", "\"");
        //        cmbProdAdd.ClearSelection();
        //        ListItem li = cmbProdAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(strItemCode.Trim()));
        //        if (li != null) li.Selected = true;
        //        cmbProdAdd.Enabled = false;
        //        cmdSaveProduct.Visible = false;
        //        //Label2.Visible = false;
        //        cmdSaveProduct.Enabled = false;

        //        cmdUpdateProduct.Enabled = true;
        //        cmdUpdateProduct.Visible = true;
        //        //Label3.Visible = true;
        //        DataSet catData = bl.GetProductForId(sDataSource, strItemCode);

        //        if (catData != null)
        //        {
        //            cmbCategory.SelectedValue = catData.Tables[0].Rows[0]["CategoryID"].ToString();
        //            cmbModel.Enabled = false;
        //            cmbBrand.Enabled = false;
        //            cmbProdName.Enabled = false;
        //            BtnClearFilter.Enabled = false;
        //            cmbCategory.Enabled = false;
        //            LoadProducts(this, null);
        //        }

        //        if ((catData.Tables[0].Rows[0]["ItemCode"] != null) && (catData.Tables[0].Rows[0]["ItemCode"].ToString() != ""))
        //        {
        //            if (cmbProdAdd.Items.FindByValue(catData.Tables[0].Rows[0]["ItemCode"].ToString().Trim()) != null)
        //            {
        //                cmbProdAdd.ClearSelection();
        //                cmbProdAdd.Items.FindByValue(catData.Tables[0].Rows[0]["ItemCode"].ToString().Trim()).Selected = true;
        //            }
        //        }

        //        if ((catData.Tables[0].Rows[0]["ProductName"] != null) && (catData.Tables[0].Rows[0]["ProductName"].ToString() != ""))
        //        {
        //            if (cmbProdName.Items.FindByValue(catData.Tables[0].Rows[0]["ProductName"].ToString().Trim()) != null)
        //            {
        //                cmbProdName.ClearSelection();
        //                cmbProdName.Items.FindByValue(catData.Tables[0].Rows[0]["ProductName"].ToString().Trim()).Selected = true;
        //            }
        //        }

        //        if ((catData.Tables[0].Rows[0]["ProductDesc"] != null) && (catData.Tables[0].Rows[0]["ProductDesc"].ToString() != ""))
        //        {
        //            if (cmbBrand.Items.FindByValue(catData.Tables[0].Rows[0]["ProductDesc"].ToString().Trim()) != null)
        //            {
        //                cmbBrand.ClearSelection();
        //                if (!cmbBrand.Items.FindByValue(catData.Tables[0].Rows[0]["ProductDesc"].ToString().Trim()).Selected)
        //                    cmbBrand.Items.FindByValue(catData.Tables[0].Rows[0]["ProductDesc"].ToString().Trim()).Selected = true;
        //            }
        //        }

        //        if ((catData.Tables[0].Rows[0]["Model"] != null) && (catData.Tables[0].Rows[0]["Model"].ToString() != ""))
        //        {
        //            if (cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()) != null)
        //            {
        //                cmbModel.ClearSelection();
        //                if (!cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()).Selected)
        //                    cmbModel.Items.FindByValue(catData.Tables[0].Rows[0]["Model"].ToString().Trim()).Selected = true;

        //            }
        //        }

        //    }

        //    txtRateAdd.Text = row.Cells[2].Text;
        //    txtNLPAdd.Text = row.Cells[3].Text;
        //    txtQtyAdd.Text = row.Cells[4].Text;
        //    txtQtyAdd.Focus();
        //    /*Start March 15 Modification */
        //    //hdEditQty.Value = row.Cells[].Text;
        //    /*End March 15 Modification */
        //    if (row.Cells[5].Text != "&nbsp;")
        //        lblUnitMrmnt.Text = row.Cells[5].Text.Trim();
        //    else
        //        lblUnitMrmnt.Text = string.Empty;

        //    lblDisAdd.Text = row.Cells[6].Text;

        //    lblVATAdd.Text = row.Cells[7].Text;
        //    lblCSTAdd.Text = row.Cells[8].Text;
        //    lbldiscamt.Text = row.Cells[9].Text;
        //    lblProdNameAdd.Text = row.Cells[1].Text;
        //    lblProdDescAdd.Text = row.Cells[1].Text;


        //}
        //catch (Exception ex)
        //{
        //    err.Visible = true;
        //    err.Text = ex.Message.ToString().Trim();
        //}

        //updatePnlProduct.Update();
        //ModalPopupProduct.Show();

    }

    //protected void cmdDelete_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        string connection = Request.Cookies["Company"].Value;
    //        BusinessLogic bll = new BusinessLogic();
            //string recondate = txtBillDate.Text.Trim(); ;
            //if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            //{

            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
            //    return;
            //}
    //        int iPurchase = 0;
    //        //string sBillNo = txtBillno.Text.Trim();
    //        iPurchase = Convert.ToInt32(hdPurchase.Value);
    //        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
    //        BusinessLogic bl = new BusinessLogic(sDataSource);
    //        //int del = bl.DeletePurchase(iPurchase, sBillNo);
    //        /*Start Purchase Stock Negative Change - March 16*/
    //        //if (del == -2)
    //        //{
    //        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase edit is not allowed for this transaction.')", true);
    //        //    return;
    //        //}
    //        /*End Purchase Stock Negative Change - March 16*/
    //        Reset();
            
    //        /*Start Purchase Stock Negative Change - March 16 -- (Commented the below method)*/
    //        //if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
    //        //    File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
    //        /*Start Purchase Stock Negative Change - March 16 -- (Commented the below method)*/
    //        //purchasePanel.Visible = false;
    //        lnkBtnAdd.Visible = true;
    //        pnlSearch.Visible = true;
    //        //PanelBill.Visible = false;
            
    //        //purchasePanel.Visible = false;
    //        lnkBtnAdd.Visible = true;
    //        pnlSearch.Visible = true;
    //    }
    //}

    protected void GrdViewPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string connection = Request.Cookies["Company"].Value;
        //BusinessLogic bll = new BusinessLogic();
        //string recondate = GrdViewPurchase.Rows[e.RowIndex].Cells[3].Text;
        //if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
        //{

        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
        //    return;
        //}
        //int iPurchase = 0;
        //string sBillNo = GrdViewPurchase.Rows[e.RowIndex].Cells[2].Text.Trim();
        //iPurchase = Convert.ToInt32(GrdViewPurchase.DataKeys[e.RowIndex].Value.ToString());
        ////string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        //string usernam = Request.Cookies["LoggedUserName"].Value;
        //int del = bl.DeletePurchase(iPurchase, sBillNo, usernam);
        ///*Start Purchase Stock Negative Change - March 16*/
        //if (del == -2)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase edit is not allowed for this transaction.')", true);
        //    return;
        //}
        ///*End Purchase Stock Negative Change - March 16*/
        ////Reset();
        ////ResetProduct();
        ///*Start Purchase Stock Negative Change - March 16 -- (Commented the below method)*/
        ////if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
        ////    File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
        ///*Start Purchase Stock Negative Change - March 16 -- (Commented the below method)*/
        ////purchasePanel.Visible = false;
        ////lnkBtnAdd.Visible = true;
        ////pnlSearch.Visible = true;
        ////PanelBill.Visible = false;
        ////PanelCmd.Visible = false;
        ////hdMode.Value = "Delete";
        ////cmdPrint.Enabled = false;
        ////delFlag.Value = "0";
        ////deleteFile();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Deleted Successfully. Deleted Bill No. is " + sBillNo.ToString() + "')", true);
        //BindGrid("0", "0");
        ////btnCancel.Enabled = false;
        ////Session["PurchaseProductDs"] = null;

        ////PanelCmd.Visible = false;
        ////purchasePanel.Visible = false;
        ////lnkBtnAdd.Visible = true;
        ////pnlSearch.Visible = true;
    }




    protected void cmdMetho_Click(object sender, EventArgs e)
    {
        try
        {
            BindBankStatement();

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
            Session["Show"] = "No";
            ModalPopupPurchase.Show();

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtStartDate.Text = dtaa;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
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
        BusinessLogic bl = new BusinessLogic(sDataSource);     
        object usernam = Session["LoggedUserName"];

        if (strBillno == "0" && strTransNo == "0")
        {
            ds = bl.GetBankRec();   
        }
        else
        {
            ds = bl.GetBankRecID(strBillno, strTransNo);
        }
           

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewPurchase.DataSource = ds.Tables[0].DefaultView;
                GrdViewPurchase.DataBind();
            }
        }
        else
        {
            GrdViewPurchase.DataSource = null;
            GrdViewPurchase.DataBind();
        }

    }

    private void BindBankStatement()
    {
        //GrdViewItems.PageSize = 6;
        int iLedgerID = 0;
        DateTime startDate;

        DataSet ds = new DataSet();
        DataSet dsttt = new DataSet();
        BusinessLogic objBL = new BusinessLogic();

        string Types = string.Empty;
        if (opnbank.SelectedItem.Text == "Bank")
        {
            iLedgerID = Convert.ToInt32(ddlbank.SelectedItem.Value);
            Types = "Bank";
        }
        else if (opnbank.SelectedItem.Text == "Customer")
        {
            iLedgerID = Convert.ToInt32(ddlCustomer.SelectedItem.Value);
            Types = "Customer";
        }

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        startDate = Convert.ToDateTime(txtStartDate.Text);

        string usernam = Request.Cookies["LoggedUserName"].Value;

        dsttt = objBL.checkbankreconciliation(iLedgerID, startDate, sDataSource, usernam, Types);

        if (dsttt != null)
        {
            if (dsttt.Tables[0].Rows.Count > 0)
            {
                dsttt = objBL.getbankreconciliation1(iLedgerID, startDate, sDataSource, usernam);
                ds = objBL.getbankrecon(iLedgerID, startDate, sDataSource, usernam, Types);

                //if (dsttt != null)
                //{
                //    if (dsttt.Tables[0].Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dsttt.Tables[0].Rows.Count; i++)
                //        {
                //            int hhh = Convert.ToInt32(dsttt.Tables[0].Rows[i]["Transno"]);
                //            for (int ij = 0; ij < ds.Tables[0].Rows.Count; ij++)
                //            {
                //                if (hhh == Convert.ToInt32(ds.Tables[0].Rows[ij]["Transno"]))
                //                {
                //                    dsttt.Tables[0].Rows[i].Delete();
                //                }
                //            }
                //        }
                //        ds.Tables[0].Merge(dsttt.Tables[0]);
                //    }
                //}

                if (dsttt != null)
                {
                    if (dsttt.Tables[0].Rows.Count > 0)
                        ds.Tables[0].Merge(dsttt.Tables[0]);
                }
            }
            else
            {
                ds = objBL.getbankreconciliation(iLedgerID, startDate, sDataSource, usernam);
            }
        }
        else
        {
            ds = objBL.getbankreconciliation(iLedgerID, startDate, sDataSource, usernam);
        }

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewItems.DataSource = ds;
                GrdViewItems.DataBind();
                ModalPopupProduct.Show();
                updatePnlProduct.Update();
                //GrdViewItems.Visible = true;
            }
        }
        else
        {
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();
            updatePnlProduct.Update();
            //ModalPopupProduct.Show();
            ModalPopupProduct.Hide();
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            return;
        }


        //GrdViewItems.PageSize = 6;
    }

    protected void GrdViewPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }

    protected void GrdViewPurchase_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string strPaymode = string.Empty;
        //int SupplierID = 0;
        //int purchaseID = 0;
        //GridViewRow row = GrdViewPurchase.SelectedRow;
        //string connection = Request.Cookies["Company"].Value;
        ///*Start Purchase Loading / Unloading Freight Change - March 16*/
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        ///*End Purchase Loading / Unloading Freight Change - March 16*/

        ////rvBillDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
        ////rvBillDate.MaximumValue = System.DateTime.Now.ToShortDateString();

        ////valdate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
        ////valdate.MaximumValue = System.DateTime.Now.ToShortDateString();

        //StringBuilder script = new StringBuilder();
        //script.Append("alert('You are not allowed to delete the record. Please contact Administrator.');");

        //string recondate = row.Cells[3].Text;
        //if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
        //    return;
        //}

        ////txtInvoiveNo.Enabled = false;

        //purchaseID = Convert.ToInt32(GrdViewPurchase.SelectedDataKey.Value);

        ///*Start Purchase Loading / Unloading Freight Change - March 16*/
        //DataSet ds = bl.GetPurchaseForId(purchaseID);

        ////if (ds.Tables[0].Rows[0]["Billno"] != null)
        ////    txtBillno.Text = ds.Tables[0].Rows[0]["Billno"].ToString();

        ////if (ds.Tables[0].Rows[0]["PurchaseId"] != null)
        ////    txtInvoiveNo.Text = ds.Tables[0].Rows[0]["PurchaseId"].ToString();


        ////if (ds.Tables[0].Rows[0]["Freight"] != null)
        ////    txtFreight.Text = Convert.ToString(ds.Tables[0].Rows[0]["Freight"]);
        ////else
        ////    txtFreight.Text = "0";

        ////if (ds.Tables[0].Rows[0]["LoadUnload"] != null)
        ////    txtLU.Text = Convert.ToString(ds.Tables[0].Rows[0]["LoadUnload"]);
        ////else
        ////    txtLU.Text = "0";
        /////*End Purchase Loading / Unloading Freight Change - March 16*/

        ////if (ds.Tables[0].Rows[0]["SalesReturn"] != null)
        ////{
        ////    drpSalesReturn.ClearSelection();
        ////    drpSalesReturn.SelectedValue = ds.Tables[0].Rows[0]["SalesReturn"].ToString().ToUpper();
        ////}
        ////else
        ////{
        ////    drpSalesReturn.SelectedIndex = 0;
        ////}

        ////if (ds.Tables[0].Rows[0]["InternalTransfer"] != null && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["InternalTransfer"].ToString()))
        ////{
        ////    drpIntTrans.ClearSelection();
        ////    drpIntTrans.SelectedValue = ds.Tables[0].Rows[0]["InternalTransfer"].ToString().ToUpper();
        ////}
        ////else
        ////{
        ////    drpIntTrans.SelectedIndex = 0;
        ////}

        ////if (ds.Tables[0].Rows[0]["DeliveryNote"] != null && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DeliveryNote"].ToString()))
        ////{
        ////    ddDeliveryNote.ClearSelection();
        ////    ddDeliveryNote.SelectedValue = ds.Tables[0].Rows[0]["DeliveryNote"].ToString().ToUpper();
        ////}
        ////else
        ////{
        ////    ddDeliveryNote.SelectedIndex = 0;
        ////}


        ////if (ds.Tables[0].Rows[0]["BilitID"] != null)
        ////{
        ////    ddBilts.Items.Clear();
        ////    loadBilts(ds.Tables[0].Rows[0]["BilitID"].ToString());
        ////    ddBilts.SelectedValue = ds.Tables[0].Rows[0]["BilitID"].ToString();
        ////}
        ////else
        ////{
        ////    ddBilts.SelectedIndex = 0;
        ////}

        ////if (drpSalesReturn.SelectedValue == "NO")
        ////{
        ////    rowSalesRet.Visible = false;
        ////}
        ////else
        ////{
        ////    rowSalesRet.Visible = true;
        ////}

        ////if (drpSalesReturn.SelectedItem.Text == "NO")
        ////{
        ////    loadSupplier("Sundry Creditors");
        ////}
        ////else
        ////{
        ////    loadSupplier("Sundry Debtors");
        ////}

        ////if ((ds.Tables[0].Rows[0]["SupplierID"] != null) && (ds.Tables[0].Rows[0]["SupplierID"].ToString() != ""))
        ////{
        ////    SupplierID = Convert.ToInt32(ds.Tables[0].Rows[0]["SupplierID"].ToString());
        ////    cmbSupplier.ClearSelection();
        ////    ListItem li = cmbSupplier.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(SupplierID.ToString()));
        ////    if (li != null) li.Selected = true;
        ////}

        ////Label pM = (Label)row.FindControl("lblPaymode"); //row.Cells[3].Text;
        ////strPaymode = pM.Text;

        ////if (paymodeVisible(strPaymode))
        ////{
        ////    if (ds.Tables[0].Rows[0]["Chequeno"] != null)
        ////        txtChequeNo.Text = ds.Tables[0].Rows[0]["Chequeno"].ToString();

        ////    if (ds.Tables[0].Rows[0]["Creditor"] != null)
        ////    {
        ////        cmbBankName.ClearSelection();
        ////        ListItem cli = cmbBankName.Items.FindByText(ds.Tables[0].Rows[0]["Creditor"].ToString());
        ////        if (cli != null) cli.Selected = true;
        ////    }

        ////}

        ////if (ds.Tables[0].Rows[0]["Paymode"] != null)
        ////{
        ////    cmdPaymode.ClearSelection();

        ////    ListItem pLi = cmdPaymode.Items.FindByValue(ds.Tables[0].Rows[0]["Paymode"].ToString());
        ////    if (pLi != null) pLi.Selected = true;
        ////}

        ////if (ds.Tables[0].Rows[0]["Billdate"] != null)
        ////    txtBillDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Billdate"].ToString()).ToString("dd/MM/yyyy");

        ////if (ds.Tables[0].Rows[0]["Invoicedate"] != null)
        ////    txtInvoiveDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Invoicedate"].ToString()).ToString("dd/MM/yyyy");

        ////if (ds.Tables[0].Rows[0]["SalesReturnReason"] != null)
        ////    txtSRReason.Text = ds.Tables[0].Rows[0]["SalesReturnReason"].ToString();


        ////if (txtBillnoSrc.Text == "")
        ////    BindGrid("0", "0");
        ////else
        ////    BindGrid(txtBillnoSrc.Text, txtTransNo.Text);
        //////Accordion1.SelectedIndex = 1;

        ////hdPurchase.Value = purchaseID.ToString();
        ////DataSet itemDs = formXml();
        ////Session["PurchaseProductDs"] = itemDs;
        ////GrdViewItems.DataSource = itemDs;
        ////GrdViewItems.DataBind();
        //////BindProduct();
        ////calcSum();
        ////hdMode.Value = "Edit";




        //string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
        //dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        //if (bl.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        //{
        //    lnkBtnAdd.Visible = false;
        //    pnlSearch.Visible = false;
            

        //}

        //updatePnlPurchase.Update();
        //ModalPopupPurchase.Show();
        
    }



    //private DataSet formXml()
    //{
    //    int purchaseID = 0;
    //    //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    //DataSet ds = new DataSet();
    //    purchaseID = Convert.ToInt32(hdPurchase.Value);
    //    /*March18*/
    //    DataSet itemDs = null;
    //    /*March18*/
    //    DataTable dt;
    //    DataRow dr;
    //    DataColumn dc;
    //    DataSet ds = new DataSet();

    //    double dTotal = 0;
    //    double dQty = 0;
    //    double dRate = 0;
    //    double dNLP = 0;
    //    string strRole = string.Empty;
    //    string roleFlag = string.Empty;
    //    string strBundles = string.Empty;


    //    double stock = 0;

    //    string strItemCode = string.Empty;
    //    DataSet dsRole;
    //    ds = bl.GetPurchaseItemsForId(purchaseID);
    //    if (ds != null)
    //    {

    //        dt = new DataTable();

    //        dc = new DataColumn("PurchaseID");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("itemCode");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("ProductName");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("ProductDesc");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("PurchaseRate");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("NLP");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("Qty");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("Measure_Unit");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("Discount");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("VAT");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("CST");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("Discountamt");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("Roles");
    //        dt.Columns.Add(dc);

    //        dc = new DataColumn("IsRole");
    //        dt.Columns.Add(dc);


    //        dc = new DataColumn("Total");
    //        dt.Columns.Add(dc);
    //        /*March18*/
    //        itemDs = new DataSet();
    //        /*March18*/


    //        itemDs.Tables.Add(dt);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            foreach (DataRow dR in ds.Tables[0].Rows)
    //            {
    //                dr = itemDs.Tables[0].NewRow();

    //                if (dR["Qty"] != null)
    //                    dQty = Convert.ToDouble(dR["Qty"]);
    //                if (dR["PurchaseRate"] != null)
    //                    dRate = Convert.ToDouble(dR["PurchaseRate"]);

    //                if (dR["NLP"] != null)
    //                {
    //                    if (dR["NLP"].ToString() != "")
    //                        dNLP = Convert.ToDouble(dR["NLP"]);
    //                    else
    //                        dNLP = 0.0;
    //                }


    //                dTotal = dQty * dRate;
    //                if (dR["ItemCode"] != null)
    //                {
    //                    strItemCode = Convert.ToString(dR["ItemCode"]);
    //                    dr["itemCode"] = strItemCode;
    //                }
    //                if (dR["PurchaseID"] != null)
    //                {
    //                    purchaseID = Convert.ToInt32(dR["PurchaseID"]);
    //                    dr["PurchaseID"] = Convert.ToString(purchaseID);
    //                }

    //                if (dR["ProductName"] != null)
    //                    dr["ProductName"] = Convert.ToString(dR["ProductName"]);

    //                if (dR["ProductDesc"] != null)
    //                    dr["ProductDesc"] = Convert.ToString(dR["ProductDesc"]);

    //                if (dR["Measure_Unit"] != null)
    //                    dr["Measure_Unit"] = Convert.ToString(dR["Measure_Unit"]);

    //                dr["Qty"] = dQty.ToString();

    //                dr["PurchaseRate"] = dRate.ToString();

    //                dr["NLP"] = dNLP.ToString();

    //                if (dR["Discount"] != null)
    //                    dr["Discount"] = Convert.ToString(dR["Discount"]);

    //                if (dR["VAT"] != null)
    //                    dr["VAT"] = Convert.ToString(dR["VAT"]);

    //                if (dR["CST"] != null)
    //                    dr["CST"] = Convert.ToString(dR["CST"]);

    //                if (dR["discamt"] != null)
    //                    dr["Discountamt"] = Convert.ToString(dR["discamt"]);

    //                if (dR["isrole"] != null)
    //                {
    //                    roleFlag = Convert.ToString(dR["isrole"]);
    //                    dr["IsRole"] = roleFlag;

    //                }

    //                if (roleFlag == "Y")
    //                {
    //                    strRole = Convert.ToString(dR["RoleID"]);
    //                }
    //                else
    //                {
    //                    strRole = "NO ROLE";
    //                }

    //                if (hdStock.Value != "")
    //                    stock = Convert.ToDouble(hdStock.Value);
    //                dr["Roles"] = strRole;
    //                dr["Total"] = Convert.ToString(dTotal);
    //                itemDs.Tables[0].Rows.Add(dr);
    //                strRole = "";
    //            }
    //        }
    //    }
    //    return itemDs;
    //}

    private void BindProduct()
    {
        //if (Session["PurchaseProductDs"] != null)
        //{
        //    GrdViewItems.DataSource = (DataSet)Session["PurchaseProductDs"];
        //    GrdViewItems.DataBind();
        //}
    }
    private void BindProductP()
    {
        //string filename = string.Empty;
        //filename = hdFilename.Value;
        //DataSet xmlDs = new DataSet();
        //if (File.Exists(Server.MapPath("Reports\\" + filename + "_Product.xml")))
        //{
        //    xmlDs.ReadXml(Server.MapPath("Reports\\" + filename + "_Product.xml"));
        //    if (xmlDs != null)
        //    {
        //        if (xmlDs.Tables.Count > 0)
        //        {
        //            GrdViewItems.DataSource = xmlDs;
        //            GrdViewItems.DataBind();
        //            calcSum();

        //        }
        //        else
        //        {
        //            GrdViewItems.DataSource = null;
        //            GrdViewItems.DataBind();
        //        }
        //    }
        //    //File.Delete(Server.MapPath(filename + "_Product.xml")); 
        //}

    }
    private void calcSum()
    {
        Double sumAmt = 0;
        //Double sumTAmt = 0;
        Double sumVat = 0;
        Double sumDis = 0;
        Double sumRate = 0;
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
                        sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDouble(dr["Qty"]), Convert.ToDouble(dr["PurchaseRate"]), Convert.ToDouble(dr["Discount"]), Convert.ToDouble(dr["VAT"]), Convert.ToDouble(dr["CST"]), Convert.ToDouble(dr["Discountamt"])));
                    //sumTAmt = sumTAmt + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["PurchaseRate"]));
                    sumDis = sumDis + GetDis();
                    sumVat = sumVat + GetVat();
                    sumCST = sumCST + GetCST();
                    sumRate = sumRate + GetTotalRate();
                }
            }
        }
        /*Start Purchase Loading / Unloading Freight Change - March 16*/
        double dFreight = 0;
        double dLU = 0;
        double sumLUFreight = 0;
        
    }



    protected void GrdViewPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {



    }
    protected void GrdViewPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }



    public string GetTotal(double qty, double rate, double discount, double VAT, double CST, double discamt)
    {
        double dis = 0;
        double vat = 0;
        double cst = 0;
        double tot = 0;
        double disRate = 0;

        double vatat = 0;
        double cstat = 0;
        if (discamt == 0)
        {

            tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));
        }
        else
        {
            tot = ((qty * rate) - (discamt));
            vatat = (tot * (VAT / 100));
            cstat = (tot * (CST / 100));
            tot = tot + vatat + cstat;
        }



        // tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));

        if (discamt == 0)
        {
            disRate = (qty * rate) - ((qty * rate) * (discount / 100));
        }
        else
        {
            disRate = (qty * rate) - (discamt);
        }

        if (discamt == 0)
        {
            dis = ((qty * rate) * (discount / 100));
        }
        else
        {
            dis = ((qty * rate) * (discamt));
        }

        vat = (disRate * (VAT / 100));
        cst = (disRate * (CST / 100));
        amtTotal = amtTotal + Convert.ToDouble(tot);
        disTotal = dis;
        rateTotal = rateTotal + rate;
        vatTotal = vat;
        cstTotal = cst;
        disTotalRate = qty * rate;
        //hdTotalAmt.Value = amtTotal.ToString("#0.00");
        //lblGrandTotal.Text = Convert.ToString(Convert.ToDecimal(tot) +Convert.ToDecimal(hdTotalAmt.Value));
        return tot.ToString("#0.00");
    }

    public string GetTotalOld(double qty, double rate, double discount, double VAT, double CST)
    {
        double dis = 0;
        double vat = 0;
        double cst = 0;
        double tot = 0;
        double disRate = 0;

        tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));
        // tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));
        disRate = (qty * rate) - ((qty * rate) * (discount / 100));
        dis = ((qty * rate) * (discount / 100));

        vat = (disRate * (VAT / 100));
        cst = (disRate * (CST / 100));
        amtTotal = amtTotal + Convert.ToDouble(tot);
        disTotal = dis;
        rateTotal = rateTotal + rate;
        vatTotal = vat;
        cstTotal = cst;
        disTotalRate = qty * rate;
        //hdTotalAmt.Value = amtTotal.ToString("#0.00");
        //lblGrandTotal.Text = Convert.ToString(Convert.ToDecimal(tot) +Convert.ToDecimal(hdTotalAmt.Value));
        return tot.ToString("#0.00");
    }

    public double GetTotalRate()
    {
        return disTotalRate;
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

        
    }
    protected void GrdViewItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        
    }
 

    protected void GrdViewItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int i;
        //i = GrdViewItems.Rows[e.RowIndex].DataItemIndex;
        //TextBox txtQtyEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtQty");
        //TextBox txtRateEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtRate");
        //TextBox txtNLPEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtNLP");
        //TextBox txtVatEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtVat");
        //TextBox txtDisEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtDiscount");
        //TextBox txtCSTEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtCST");
        //if (txtDisEd.Text == "")
        //    txtDisEd.Text = "0";
        //if (txtVatEd.Text == "")
        //    txtVatEd.Text = "0";

        //double dis = 0.0;
        //double vat = 0.0;
        //double cst = 0.0;
        //if (txtDisEd.Text.Trim() == "")
        //    dis = 0;
        //else
        //    dis = Convert.ToDouble(txtDisEd.Text);

        //if (txtVatEd.Text.Trim() == "")
        //    vat = 0;
        //else
        //    vat = Convert.ToDouble(txtVatEd.Text);

        //if (txtCSTEd.Text.Trim() == "")
        //    cst = 0;
        //else
        //    cst = Convert.ToDouble(txtCSTEd.Text);
        //if (dis < 0 || dis > 100)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Discount')", true);
        //    txtDisEd.Text = "0";
        //    return;
        //}

        //if (vat < 0 || vat > 100)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid VAT')", true);
        //    txtVatEd.Text = "0";
        //    return;
        //}
        //if (cst < 0 || cst > 100)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid CST')", true);
        //    txtCSTEd.Text = "0";
        //    return;
        //}

        //GrdViewItems.EditIndex = -1;
        ////BindProduct();
        //if (Session["PurchaseProductDs"] != null)
        //{
        //    GrdViewItems.DataSource = (DataSet)Session["PurchaseProductDs"];
        //    GrdViewItems.DataBind();
        //    DataSet ds = (DataSet)GrdViewItems.DataSource;
        //    ds.Tables[0].Rows[i]["Qty"] = txtQtyEd.Text;
        //    ds.Tables[0].Rows[i]["PurchaseRate"] = txtRateEd.Text;
        //    ds.Tables[0].Rows[i]["NLP"] = txtNLPEd.Text;
        //    ds.Tables[0].Rows[i]["Discount"] = txtDisEd.Text;
        //    ds.Tables[0].Rows[i]["VAT"] = txtVatEd.Text;
        //    ds.Tables[0].Rows[i]["CST"] = txtCSTEd.Text;

        //    GrdViewItems.DataSource = ds;
        //    GrdViewItems.DataBind();
        //    //ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
        //    //BindProduct();
        //    calcSum();
        //}
    }


    protected void GrdViewItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //DataSet checkDs;
            //checkDs = (DataSet)Session["productDs"];

            //fnStoreGridData();

            RememberOldValues();

            GrdViewItems.PageIndex = e.NewPageIndex;
            BindBankStatement();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //if (Session["PurchaseProductDs"] != null)
        //{
        //    GrdViewItems.DataSource = (DataSet)Session["PurchaseProductDs"];
        //    GrdViewItems.DataBind();
        //    DataSet ds = (DataSet)GrdViewItems.DataSource;
        //    ds.Tables[0].Rows[GrdViewItems.Rows[e.RowIndex].DataItemIndex].Delete();
        //    ds.Tables[0].AcceptChanges();
        //    GrdViewItems.DataSource = ds;
        //    GrdViewItems.DataBind();x
        //    /*March 18*/
        //    Session["PurchaseProductDs"] = ds;
        //    /*March 18*/
        //    calcSum();
        //}
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
                //e.Row.Cells[3].Visible = false;
                TextBox txt = (TextBox)e.Row.FindControl("txtDate");
                TextBox txtet = (TextBox)e.Row.FindControl("txtResult");
                if ((txt.Text != "") || (txtet.Text != ""))
                {
                    e.Row.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    e.Row.ForeColor = System.Drawing.Color.SteelBlue;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewItems_PreRender(object sender, EventArgs e)
    {
        try
        {
            RePopulateValues();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void RememberOldValues()
    {
        ArrayList categoryIDList = new ArrayList();
        ArrayList quantityList = new ArrayList();

        string howManyItems= string.Empty;
        string index = "";

        foreach (GridViewRow row in GrdViewItems.Rows)
        {
            index = (string)GrdViewItems.DataKeys[row.RowIndex].Value;
            //bool result = ((CheckBox)row.FindControl("Replenishf")).Checked;
            
            string myTextBox = Convert.ToString(((TextBox)row.FindControl("txtDate")).Text);

            if (myTextBox != "")
            {
                howManyItems = myTextBox;
            }

            if (Session["CHECKED_ITEMS"] != null)
            {
                categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];
                quantityList = (ArrayList)Session["QTY_ITEMS"];
            }
            if (myTextBox != "")
            {
                if (!categoryIDList.Contains(index))
                {
                    categoryIDList.Add(index);
                    quantityList.Add(howManyItems);
                }
                else
                {
                    int j = categoryIDList.IndexOf(index);
                    quantityList[j] = howManyItems;
                }
            }
            else
            {
                categoryIDList.Remove(index);
                quantityList.Remove(myTextBox);
            }

            if (categoryIDList != null && categoryIDList.Count > 0)
            {
                Session["CHECKED_ITEMS"] = categoryIDList;
                Session["QTY_ITEMS"] = quantityList;
            }
        }
    }

    private void fnStoreGridData()
    {
        string Userna = string.Empty;

        

    }

    protected void cmdsave_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet ds;
            DataTable dt;
            DataRow drNew;

            DataColumn dc;

            ds = new DataSet();

            dt = new DataTable();

            dc = new DataColumn("TransNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("Date");
            dt.Columns.Add(dc);

            dc = new DataColumn("Debtor");
            dt.Columns.Add(dc);

            dc = new DataColumn("DebtorID");
            dt.Columns.Add(dc);

            dc = new DataColumn("Creditor");
            dt.Columns.Add(dc);

            dc = new DataColumn("CreditorID");
            dt.Columns.Add(dc);

            dc = new DataColumn("Amount");
            dt.Columns.Add(dc);

            dc = new DataColumn("Narration");
            dt.Columns.Add(dc);

            dc = new DataColumn("VoucherType");
            dt.Columns.Add(dc);

            dc = new DataColumn("ChequeNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("ReconcilatedBy");
            dt.Columns.Add(dc);

            dc = new DataColumn("Reconcilateddate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Result");
            dt.Columns.Add(dc);

            ds.Tables.Add(dt);

            DateTime textdate;
            Label lblDebtorID = null;
            Label lblCreditorID = null;
            //Label lblDebtorID = null;

            for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
            {
                TextBox txt = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDate");
                string text = txt.Text;

                TextBox tx = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtResult");
                string text2 = tx.Text;

                if (text == "" && text2 == "")
                {

                }
                else
                {
                    textdate = DateTime.Parse(text);
                    if (text != "")
                    {
                        drNew = dt.NewRow();
                        drNew["TransNo"] = GrdViewItems.Rows[vLoop].Cells[0].Text;
                        drNew["Date"] = GrdViewItems.Rows[vLoop].Cells[1].Text;
                        drNew["Debtor"] = GrdViewItems.Rows[vLoop].Cells[2].Text;

                        lblDebtorID = (Label)GrdViewItems.Rows[vLoop].FindControl("lblDebtorID");
                        drNew["DebtorID"] = lblDebtorID.Text;

                        drNew["Creditor"] = GrdViewItems.Rows[vLoop].Cells[4].Text;

                        lblCreditorID = (Label)GrdViewItems.Rows[vLoop].FindControl("lblCreditorID");
                        drNew["CreditorID"] = lblCreditorID.Text;

                        drNew["Amount"] = GrdViewItems.Rows[vLoop].Cells[6].Text;
                        drNew["Narration"] = GrdViewItems.Rows[vLoop].Cells[7].Text;
                        drNew["VoucherType"] = GrdViewItems.Rows[vLoop].Cells[8].Text;

                        string logdescription = GrdViewItems.Rows[vLoop].Cells[9].Text;
                        string value1 = string.Empty;

                        if (logdescription.Length > 20)
                        {
                            value1 = logdescription.Substring(0, 19);
                            drNew["ChequeNo"] = value1;
                        }
                        else
                        {
                            drNew["ChequeNo"] = GrdViewItems.Rows[vLoop].Cells[9].Text;
                        }

                        drNew["ReconcilatedBy"] = GrdViewItems.Rows[vLoop].Cells[10].Text;
                        drNew["Reconcilateddate"] = textdate;
                        drNew["Result"] = text2;
                        ds.Tables[0].Rows.Add(drNew);
                    }
                }
            }

            //foreach (GridViewRow gridRow in GrdViewItems.Rows)
            //{
            //    TextBox txt = (TextBox)gridRow.FindControl("txtDate");
            //    string text = txt.Text;
            //    drNew = dt.NewRow();
            //    drNew["TransNo"] = gridRow.Cells[0].Text;
            //    drNew["Date"] = gridRow.Cells[1].Text;
            //    drNew["Debtor"] = gridRow.Cells[2].Text;
            //    drNew["DebtorID"] = gridRow.Cells[3].Text;
            //    drNew["Creditor"] = gridRow.Cells[4].Text;
            //    drNew["CreditorID"] = gridRow.Cells[5].Text;
            //    drNew["Amount"] = gridRow.Cells[6].Text;
            //    drNew["Narration"] = gridRow.Cells[7].Text;
            //    drNew["VoucherType"] = gridRow.Cells[8].Text;
            //    drNew["ChequeNo"] = gridRow.Cells[9].Text;
            //    drNew["ReconcilatedBy"] = gridRow.Cells[10].Text;
            //    drNew["Reconcilateddate"] = text;
            //    ds.Tables[0].Rows.Add(drNew);
            //}

            string Userna = Request.Cookies["LoggedUserName"].Value;

            string iLedgerName = string.Empty;
            string Types = string.Empty;
            int iLedgerID = 0;
            DateTime startDate;
            if (opnbank.SelectedItem.Text == "Bank")
            {
                iLedgerID = Convert.ToInt32(ddlbank.SelectedItem.Value);
                Types = "Bank";
                iLedgerName = ddlbank.SelectedItem.Text;
            }
            else if (opnbank.SelectedItem.Text == "Customer")
            {
                iLedgerID = Convert.ToInt32(ddlCustomer.SelectedItem.Value);
                Types = "Customer";
                iLedgerName = ddlCustomer.SelectedItem.Text;
            }

            startDate = Convert.ToDateTime(txtStartDate.Text);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    BusinessLogic objBL = new BusinessLogic();
                    objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

                    objBL.InsertBankReconciliation(ds, Userna, iLedgerID, startDate, iLedgerName, Types);


                    //string salestype = string.Empty;
                    //int ScreenNo = 0;
                    //string ScreenName = string.Empty;

                    //salestype = "Bank Reconciliation";
                    //ScreenName = "Bank Reconciliation";

                 
                    //bool mobile = false;
                    //bool Email = false;
                    //string emailsubject = string.Empty;

                    //string emailcontent = string.Empty;
                    //if (hdEmailRequired.Value == "YES")
                    //{
                    //    DataSet dsd = objBL.GetLedgerInfoForId(connection, DebitorID);
                    //    var toAddress = "";
                    //    var toAdd = "";
                    //    Int32 ModeofContact = 0;
                    //    int ScreenType = 0;

                    //    if (dsd != null)
                    //    {
                    //        if (dsd.Tables[0].Rows.Count > 0)
                    //        {
                    //            foreach (DataRow dr in dsd.Tables[0].Rows)
                    //            {
                    //                toAdd = dr["EmailId"].ToString();
                    //                ModeofContact = Convert.ToInt32(dr["ModeofContact"]);
                    //            }
                    //        }
                    //    }


                    //    DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                    //    if (dsdd != null)
                    //    {
                    //        if (dsdd.Tables[0].Rows.Count > 0)
                    //        {
                    //            foreach (DataRow dr in dsdd.Tables[0].Rows)
                    //            {
                    //                ScreenType = Convert.ToInt32(dr["ScreenType"]);
                    //                mobile = Convert.ToBoolean(dr["mobile"]);
                    //                Email = Convert.ToBoolean(dr["Email"]);
                    //                emailsubject = Convert.ToString(dr["emailsubject"]);
                    //                emailcontent = Convert.ToString(dr["emailcontent"]);

                    //                if (ScreenType == 1)
                    //                {
                    //                    if (dr["Name1"].ToString() == "Sales Executive")
                    //                    {
                    //                        toAddress = toAdd;
                    //                    }
                    //                    else if (dr["Name1"].ToString() == "Customer")
                    //                    {
                    //                        if (ModeofContact == 2)
                    //                        {
                    //                            toAddress = toAdd;
                    //                        }
                    //                        else
                    //                        {
                    //                            break;
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        toAddress = toAdd;
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    toAddress = dr["EmailId"].ToString();
                    //                }
                    //                if (Email == true)
                    //                {
                    //                    //string subject = "Added - Customer Receipt in Branch " + Request.Cookies["Company"].Value;

                    //                    string body = "\n";
                    //                    //body += " Branch           : " + Request.Cookies["Company"].Value + "\n";
                    //                    //body += " Trans No         : " + GrdViewReceipt.SelectedDataKey.Value + "\n";
                    //                    //body += " User Name        : " + Request.Cookies["LoggedUserName"].Value + "\n";
                    //                    //body += " Trans Date       : " + TransDate + "\n";
                    //                    //body += " Amount           : " + Amount + "\n";
                    //                    //body += " Narration        : " + Narration + "\n";
                    //                    //body += " Customer         : " + ddReceivedFrom.SelectedItem.Text + "\n";

                    //                    int index123 = emailcontent.IndexOf("@Branch");
                    //                    body = Request.Cookies["Company"].Value;
                    //                    emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);

                    //                    int index132 = emailcontent.IndexOf("@Narration");
                    //                    body = Narration;
                    //                    emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);

                    //                    int index312 = emailcontent.IndexOf("@User");
                    //                    body = usernam;
                    //                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);

                    //                    int index2 = emailcontent.IndexOf("@Date");
                    //                    body = TransDate.ToString();
                    //                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);

                    //                    int index = emailcontent.IndexOf("@Customer");
                    //                    body = ddReceivedFrom.SelectedItem.Text;
                    //                    emailcontent = emailcontent.Remove(index, 9).Insert(index, body);

                    //                    int index1 = emailcontent.IndexOf("@Amount");
                    //                    body = Convert.ToString(Amount);
                    //                    emailcontent = emailcontent.Remove(index1, 7).Insert(index1, body);

                    //                    string smtphostname = ConfigurationManager.AppSettings["SmtpHostName"].ToString();
                    //                    int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPortNumber"]);
                    //                    var fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

                    //                    string fromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();

                    //                    EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, body, fromPassword);

                    //                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email sent successfully')", true);

                    //                }

                    //            }
                    //        }
                    //    }
                    //}


                    //BindGrid("0", "0");
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Reconciliated Successfully');", true);
                    //UpdatePnlMaster.Update();
                }
                else
                {
                    for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
                    {
                        if (ds != null)
                        {
                            if ((ds.Tables[0].Rows.Count < 0) || (ds.Tables[0].Rows.Count == 0))
                            {
                                BusinessLogic objBL = new BusinessLogic();
                                objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
                                objBL.DelBankReconciliation(ds, Userna, iLedgerID, startDate, iLedgerName, Types);


                                //BindGrid("0", "0");

                                //UpdatePnlMaster.Update();
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Reconciliated Successfully');", true);
                            }
                        }
                    }
                }
                BindGrid("0", "0");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Reconciliated Successfully');", true);
                UpdatePnlMaster.Update();
            }

            ModalPopupProduct.Hide();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void RePopulateValues()
    {
        ArrayList categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];
        ArrayList quantityList = (ArrayList)Session["QTY_ITEMS"];

        if (categoryIDList != null && categoryIDList.Count > 0)
        {

            foreach (GridViewRow row in GrdViewItems.Rows)
            {
                string index = (string)GrdViewItems.DataKeys[row.RowIndex].Value; //Row currently in

                if (categoryIDList.Contains(index))
                {                 
                        int i = categoryIDList.IndexOf(index);
                        
                        TextBox myTextBox = ((TextBox)row.FindControl("txtDate"));

                        string foo = Convert.ToString(quantityList[i]);
                        myTextBox.Text = foo;
                }
            }
        }
    }


    //protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //    }
        //GridView gridView = (GridView)sender;

        //if (e.Row.RowType == DataControlRowType.Header)
        //{

        //    foreach (DataControlField field in gridView.Columns)
        //    {

        //    }
        //}
    //}

    private string GetCurrencyType()
    {
        if (Session["CurrencyType"].ToString() == "INR")
        {
            return "Rs";
        }
        else if (Session["CurrencyType"].ToString() == "GBP")
        {
            return "£";
        }
        else
        {
            return string.Empty;
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
            BindBankStatement();
            //DataSet ds = new DataSet();
            //BusinessLogic objBL = new BusinessLogic();

            //GrdViewItems.PageSize = 6;

            //objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
            //string field = string.Empty;
            //string Method = "All";

            //DateTime startDate, endDate;

            //startDate = Convert.ToDateTime(txtStartDate.Text);
            //endDate = Convert.ToDateTime(txtEndDate.Text);

            //ds = objBL.getbankreconciliation(startDate, endDate, sDataSource);
            //GrdViewItems.DataSource = ds;
            //GrdViewItems.DataBind();
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

    /*
    protected void GrdView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //GridView1.EditIndex = -1;
        DataSet ds = (DataSet)Session["data"];
        //GridView1.DataSource = ds;
        //GridView1.DataBind();
    }
    protected void btnCLick_Click(object sender, EventArgs e)
    {
        double dblRole = 0.0;
        double dblRate = 0.0;
        DataSet ds = (DataSet)Session["data"];
        DataRow dr = ds.Tables[0].NewRow();
        dr[0] = txtRole.Text;
        //  dr[1] = txtRate.Text;

        ds.Tables[0].Rows.Add(dr);
        //GridView1.DataSource = ds;
        //GridView1.DataBind();

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
    protected void GrdView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView1.EditIndex = e.NewEditIndex;
        DataSet ds = (DataSet)Session["data"];

        //GridView1.DataSource = ds;
        //GridView1.DataBind();

    }
    protected void GrdView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int i;
        i = //GridView1.Rows[e.RowIndex].DataItemIndex;


        TextBox txtQty = (TextBox)//GridView1.Rows[e.RowIndex].Cells[1].Controls[0];


        //GridView1.EditIndex = -1;
        DataSet ds1 = (DataSet)Session["data"];

        //GridView1.DataSource = ds1;
        //GridView1.DataBind();

        DataSet ds = (DataSet)//GridView1.DataSource;

        ds.Tables[0].Rows[i]["Qty"] = txtQty.Text;

        //GridView1.DataSource = ds;
        //GridView1.DataBind();
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

        DataSet ds = (DataSet)Session["data"];
        double dblRole = 0;
        if (ds.Tables[0].Rows[//GridView1.Rows[e.RowIndex].DataItemIndex]["Qty"] != null)
            if (ds.Tables[0].Rows[//GridView1.Rows[e.RowIndex].DataItemIndex]["Qty"].ToString() != "")
                dblRole = Convert.ToDouble(ds.Tables[0].Rows[//GridView1.Rows[e.RowIndex].DataItemIndex]["Qty"]);

        ds.Tables[0].Rows[//GridView1.Rows[e.RowIndex].DataItemIndex].Delete();
        if (txtQtyAdd.Text.Trim() != "")
            txtQtyAdd.Text = Convert.ToString(Convert.ToDouble(txtQtyAdd.Text) - dblRole);

        txtRole.Text = "";
        //GridView1.DataSource = ds;
        //GridView1.DataBind();


    }
    */


    //private void EmptyRow()
    //{

        //var ds = new DataSet();
        //sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        //var dt = new DataTable();

        //DataRow drNew;
        //DataColumn dc;

        //dc = new DataColumn("itemCode");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("Billno");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("ProductName");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("NLP");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("Qty");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("PurchaseRate");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("Measure_unit");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("Discount");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("ExecCharge");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("VAT");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("CST");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("DiscountAmt");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("Roles");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("IsRole");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("Total");
        //dt.Columns.Add(dc);



        //dc = new DataColumn("Bundles");
        //dt.Columns.Add(dc);

        //dc = new DataColumn("Rods");
        //dt.Columns.Add(dc);



        //ds.Tables.Add(dt);
        //drNew = dt.NewRow();

        //string textvalue = null;

        //drNew["itemCode"] = string.Empty;
        //drNew["Billno"] = "";
        //drNew["ProductName"] = string.Empty;
        //drNew["NLP"] = string.Empty;
        //drNew["Qty"] = Convert.ToDouble(textvalue);
        //drNew["Measure_Unit"] = string.Empty;
        //drNew["PurchaseRate"] = Convert.ToDouble(textvalue);
        //drNew["Discount"] = Convert.ToDouble(textvalue);
        //drNew["ExecCharge"] = Convert.ToDouble(textvalue);
        //drNew["VAT"] = Convert.ToDouble(textvalue);
        //drNew["CST"] = Convert.ToDouble(textvalue);
        //drNew["DiscountAmt"] = Convert.ToDouble(textvalue);
        //drNew["Roles"] = "";
        //drNew["IsRole"] = "N";
        //drNew["Total"] = string.Empty;

        //drNew["Bundles"] = "";
        //drNew["Rods"] = "";


        //ds.Tables[0].Rows.Add(drNew);

        //ds.Tables[0].AcceptChanges();

        //GrdViewItems.Columns[12].Visible = false;
        //GrdViewItems.Columns[13].Visible = false;

        //GrdViewItems.Columns[10].Visible = false;

        //GrdViewItems.DataSource = ds;
        //GrdViewItems.DataBind();

        //GrdViewItems.Rows[0].Cells[2].Text = null;
        //GrdViewItems.Rows[0].Cells[4].Text = null;
        //GrdViewItems.Rows[0].Cells[3].Text = null;
        //GrdViewItems.Rows[0].Cells[5].Text = null;
        //GrdViewItems.Rows[0].Cells[7].Text = null;
        //GrdViewItems.Rows[0].Cells[9].Text = null;
        //GrdViewItems.Rows[0].Cells[10].Text = null;
        //GrdViewItems.Rows[0].Cells[8].Text = null;
        //GrdViewItems.Rows[0].Cells[6].Text = null;
        //}
    //}

}
