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
using System.Xml.Linq;
using System.IO;

public partial class CompanyInfo : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    bool isOffline = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!IsPostBack)
            {
                loadProducts();
                loadBillFormat();
                GetSettingsInfo();
                BindDivisions();
                GetCompanyInfo();
                loadPriceList();
                loadPriceListPurchase();
                Label1.Text = Helper.GenerateUniqueIDForThisPC();
                DisableForOffline();
                BindGrid();
                loadBanks();
                //GrdTransporter.PageSize = 5;
                //GrdUnitMnt.PageSize = 5;


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private void BindGrid()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataTable dt;
        DataColumn dc;             

        ds = bl.ListBranchCodeConfigsales();
        ds1 = bl.ListBranchCode();       

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                EditableGrid.DataSource = ds;
                EditableGrid.DataBind();
            }
        }

        if (ds1 != null)
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                if (ds != null)
                {
                    ds.Merge(ds1);
                    EditableGrid.DataSource = ds;
                }
                else
                {
                    EditableGrid.DataSource = ds1;
                }
                EditableGrid.DataBind();
            }
        }
    }


    private void DisableForOffline()
    {
        string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        BusinessLogic objChk = new BusinessLogic(sDataSource);

        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            isOffline = true;
            lnkBtnAdd.Enabled = false;
            btnSave.Enabled = false;
            btnAddIP.Enabled = false;
            BtnAddDivision.Enabled = false;
            btnDivSave.Enabled = false;
            btnDivUpdate.Enabled = false;
            GrdDiv.Columns[4].Visible = false;
            GrdDiv.Columns[5].Visible = false;
            Button1.Enabled = false;
            GrdUnitMnt.Columns[1].Visible = false;
            lnkBtnAddTransporter.Enabled = false;
            GrdTransporter.Columns[1].Visible = false;
        }
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

    private void loadPriceList()
    {
        string connection = Request.Cookies["Company"].Value;
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListPriceListInfo(connection, "", "");
        ddPriceList.DataSource = ds;
        ddPriceList.DataBind();
        ddPriceList.DataTextField = "PriceName";
        ddPriceList.DataValueField = "PriceName";
    }

    private void loadPriceListPurchase()
    {
        string connection = Request.Cookies["Company"].Value;
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListPriceListInfo(connection, "", "");
        drpPurPriceList.DataSource = ds;
        drpPurPriceList.DataBind();
        drpPurPriceList.DataTextField = "PriceName";
        drpPurPriceList.DataValueField = "PriceName";
    }

    private void loadBillFormat()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBillFormat();
        cmdBill.DataSource = ds;
        cmdBill.DataBind();
        cmdBill.DataTextField = "KeyName";
        cmdBill.DataValueField = "KeyName";
    }

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            GrdUnitMnt.FooterRow.Visible = true;
            lnkBtnAdd.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkBtnAddTransporter_Click(object sender, EventArgs e)
    {
        try
        {
            GrdTransporter.DataBind();
            GrdTransporter.ShowFooter = true;
            lnkBtnAddTransporter.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdTransporter_DataBound(object sender, EventArgs e)
    {
        try
        {
            GrdTransporter.Rows[0].Visible = false;

            if (GrdTransporter.Rows.Count == 1 && !GrdTransporter.FooterRow.Visible)
            {
                GrdTransporter.Columns[0].HeaderText = "No Transporters found!";
                GrdTransporter.Columns[1].Visible = false;
            }
            else
            {
                GrdTransporter.Columns[0].HeaderText = "Transporter";
                if (!isOffline)
                    GrdTransporter.Columns[1].Visible = true;
                else
                    GrdTransporter.Columns[1].Visible = false;
            }
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
        ds = bl.ListBankLedgerpaymnet();

        ddBank1.DataSource = ds;
        ddBank1.DataTextField = "LedgerName";
        ddBank1.DataValueField = "LedgerID";
        ddBank1.DataBind();

    }

    public void GetSettingsInfo()
    {
        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetSettings();
        ListItem liProd;
        ListItem liDiscType;
        ListItem liBill;
        if (ds != null)
        {
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["KeyName"].ToString() == "ITEMCODE")
                        {
                            cmbProdAdd.ClearSelection();
                            liProd = cmbProdAdd.Items.FindByValue(Convert.ToString(dr["KeyValue"]));
                            if (liProd != null)
                                liProd.Selected = true;
                        }
                        else if (dr["KeyName"].ToString() == "IPBLOCKING")
                        {
                            rdIPBlock.SelectedValue = dr["KeyValue"].ToString();

                            if (rdIPBlock.SelectedValue == "YES")
                                TabPanel2.Visible = true;
                            else
                                TabPanel2.Visible = false;
                        }
                        else if (dr["KeyName"].ToString() == "CURRENCY")
                        {
                            if (dr["KeyValue"] != null)
                                ddCurrency.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "QTYRETURN")
                        {
                            if (dr["KeyValue"] != null)
                                rdQtyReturn.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "BARCODE")
                        {
                            if (dr["KeyValue"] != null)
                                rdoBarcode.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "STOCKEDIT")
                        {
                            if (dr["KeyValue"] != null)
                                rdoStockEdit.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "QTYDATE")
                        {
                            if (dr["KeyValue"] != null)
                                txtDate.Text = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "OWNERMOB")
                        {
                            if (dr["KeyValue"] != null)
                                txtMobile.Text = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "VATRECDATE")
                        {
                            if (dr["KeyValue"] != null)
                                txtVATReconDate.Text = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "VATAMOUNT")
                        {
                            if (dr["KeyValue"] != null)
                                txtVATAmount.Text = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "DEALER")
                        {
                            if (dr["KeyValue"] != null)
                                rdDealer.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "BLITREQ")
                        {
                            if (dr["KeyValue"] != null)
                                rdoBLIT.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "CREDITEXD")
                        {
                            if (dr["KeyValue"] != null)
                                rdoExceedCreditLimit.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "BILLFORMAT")
                        {
                            cmdBill.ClearSelection();
                            liBill = cmdBill.Items.FindByValue(Convert.ToString(dr["KeyValue"]));
                            if (liBill != null)
                                liBill.Selected = true;
                        }
                        else if (dr["KeyName"].ToString() == "DISCTYPE")
                        {
                            ddDiscType.ClearSelection();
                            liDiscType = ddDiscType.Items.FindByValue(Convert.ToString(dr["KeyValue"]));

                            if (liDiscType != null)
                                liDiscType.Selected = true;
                        }
                        else if (dr["KeyName"].ToString() == "SMSREQ")
                        {
                            if (dr["KeyValue"] != null)
                                rdoSMSRqrd.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "BILLMETHOD")
                        {
                            if (dr["KeyValue"] != null)
                                DropDownList1.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "OBSOLUTE")
                        {
                            if (dr["KeyValue"] != null)
                                dpobsolute.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "ROUNDOFF")
                        {
                            if (dr["KeyValue"] != null)
                                dproundoff.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "SALBILLNO")
                        {
                            if (dr["KeyValue"] != null)
                                dpsalesbillno.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "AUTOLOCK")
                        {
                            if (dr["KeyValue"] != null)
                                dpautolock.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "SAVELOG")
                        {
                            if (dr["KeyValue"] != null)
                                dpsavelog.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "ENBLVAT")
                        {
                            if (dr["KeyValue"] != null)
                                ddenablevat.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "EMAILREQ")
                        {
                            if (dr["KeyValue"] != null)
                                rdoemailrequired.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "MACBLOCK")
                        {
                            if (dr["KeyValue"] != null)
                                rdomacaddress.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "TINREQ")
                        {
                            if (dr["KeyValue"] != null)
                                rdotinnomandatory.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "ENBLDATE")
                        {
                            if (dr["KeyValue"] != null)
                                rdvoudateenable.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "SDISCOUNT")
                        {
                            if (dr["KeyValue"] != null)
                                RadioButtonDiscount.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "OPBAL")
                        {
                            if (dr["KeyValue"] != null)
                                RadioButtonOpening.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "PRICE")
                        {
                            if (dr["KeyValue"] != null)
                                ddPriceList.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "PWDEXPDAY")
                        {
                            if (dr["KeyValue"] != null)
                                txtExpDay.Text = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "PURPRILST")
                        {
                            if (dr["KeyValue"] != null)
                                drpPurPriceList.SelectedValue = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "PURRNDOFF")
                        {
                            if (dr["KeyValue"] != null)
                                txtPurRnd.Text = dr["KeyValue"].ToString();
                        }
                        else if (dr["KeyName"].ToString() == "SAPPROCESS")
                        {
                            if (dr["KeyValue"] != null)
                                chksap.SelectedValue = dr["KeyValue"].ToString();
                        }
                    }
                }
            }
        }
    }
    public void GetCompanyInfo()
    {
        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.getCompanyDetails();
        if (ds != null)
        {
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txtCompanyName.Text = Convert.ToString(dr["CompanyName"]);
                        txtAddress.Text = Convert.ToString(dr["Address"]); ;
                        txtCity.Text = Convert.ToString(dr["City"]); ;
                        txtState.Text = Convert.ToString(dr["State"]); ;
                        txtPincode.Text = Convert.ToString(dr["Pincode"]); ;
                        txtPhone.Text = Convert.ToString(dr["Phone"]); ;
                        txtFAX.Text = Convert.ToString(dr["Fax"]); ;
                        txtEmail.Text = Convert.ToString(dr["Email"]); ;
                        txtTin.Text = Convert.ToString(dr["Tinno"]); ;
                        txtCST.Text = Convert.ToString(dr["gstno"]); ;
                    }
                }
            }
        }
    }


    protected void grdViewIP_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(grdViewIP, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridPosting_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridPosting, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public string GetCurrentDBName(string con)
    {
        string str = con; // "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\DemoDev\\Accsys\\App_Data\\sairama.mdb;Persist Security Info=True;Jet OLEDB:Database Password=moonmoon";
        string str2 = string.Empty;
        if (str.Contains(".mdb"))
        {
            str2 = str.Substring(str.IndexOf("Data Source"), str.IndexOf("Persist", 0));
            str2 = str2.Substring(str2.LastIndexOf("\\") + 1, str2.IndexOf(";") - str2.LastIndexOf("\\"));
            if (str2.EndsWith(";"))
            {
                str2 = str2.Remove(str2.Length - 5, 5);
            }
        }
        return str2;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                string strCompany = string.Empty;
                string strAddress = string.Empty;
                string strCity = string.Empty;
                string strState = string.Empty;
                string strPincode = string.Empty;
                string strPhone = string.Empty;
                string strFax = string.Empty;
                string strEmail = string.Empty;
                string strTin = string.Empty;
                string strCST = string.Empty;
                string strOwnerMob = string.Empty;
                string strVATReconDate = string.Empty;
                string strVATAmount = string.Empty;

                string itemCode = string.Empty;
                string strIP = string.Empty;
                string strQtyReturn = string.Empty;
                string strDate = string.Empty;
                string strBillFormat = string.Empty;
                string strBillMethod = string.Empty;
                string droundoff = string.Empty;
                string dsalesseries = string.Empty;
                string DBname = string.Empty;
                string path = string.Empty;
                string currency = string.Empty;
                string dealer = string.Empty;
                string barcode = string.Empty;
                string stockEdit = string.Empty;
                string smsRequired = string.Empty;
                string blitRequired = string.Empty;
                string discType = string.Empty;
                string exceedLimit = string.Empty;
                string strobsolute = string.Empty;
                string autolock = string.Empty;
                string savelog = string.Empty;
                string enablevat = string.Empty;
                string enabledate = string.Empty;
                string sapcheck = string.Empty;

                string emailRequired = string.Empty;
                string macaddress = string.Empty;
                string tinnoman = string.Empty;
                string salesdiscount = string.Empty;
                string openingbalance = string.Empty;
                string deviationprice = string.Empty;
                string pwdexpday = string.Empty;
                string purchasepricelist = string.Empty;
                string purchaseround = string.Empty;

                salesdiscount = RadioButtonDiscount.SelectedValue;
                openingbalance = RadioButtonOpening.SelectedValue;

                emailRequired = rdoemailrequired.SelectedValue;
                macaddress = rdomacaddress.SelectedValue;
                tinnoman = rdotinnomandatory.SelectedValue;
                enabledate = rdvoudateenable.SelectedValue;

                strBillMethod = DropDownList1.SelectedValue;
                droundoff = dproundoff.SelectedValue;
                dsalesseries = dpsalesbillno.SelectedValue;

                strobsolute = dpobsolute.SelectedValue;
                strCompany = txtCompanyName.Text.Trim();
                strAddress = txtAddress.Text.Trim();
                strCity = txtCity.Text.Trim();
                strState = txtState.Text.Trim();
                strPincode = txtPincode.Text.Trim();
                strPhone = txtPhone.Text.Trim();
                strFax = txtFAX.Text.Trim();
                strEmail = txtEmail.Text.Trim();
                strTin = txtTin.Text.Trim();
                strCST = txtCST.Text.Trim();
                currency = ddCurrency.SelectedValue;
                blitRequired = rdoBLIT.SelectedValue;
                itemCode = cmbProdAdd.SelectedItem.Value;
                strIP = rdIPBlock.SelectedItem.Text;
                strQtyReturn = rdQtyReturn.SelectedItem.Text;
                strDate = txtDate.Text.Trim();
                strBillFormat = cmdBill.SelectedItem.Text;
                dealer = rdDealer.SelectedValue;
                sapcheck = chksap.SelectedValue;
                barcode = rdoBarcode.SelectedValue;
                stockEdit = rdoStockEdit.SelectedValue;

                autolock = dpautolock.SelectedValue;

                savelog = dpsavelog.SelectedValue;
                enablevat = ddenablevat.SelectedValue;
                deviationprice = ddPriceList.SelectedValue;

                smsRequired = rdoSMSRqrd.SelectedValue;
                strOwnerMob = txtMobile.Text;
                strVATReconDate = txtVATReconDate.Text;
                strVATAmount = txtVATAmount.Text;
                discType = ddDiscType.SelectedValue;
                exceedLimit = rdoExceedCreditLimit.SelectedItem.Text;
                pwdexpday = txtExpDay.Text.Trim();
                purchasepricelist = drpPurPriceList.SelectedValue;
                purchaseround = txtPurRnd.Text.Trim();

                clsCompany clscmp = new clsCompany();
                clscmp.Company = strCompany;
                clscmp.Address = strAddress;
                clscmp.City = strCity;
                clscmp.State = strState;
                clscmp.Pincode = strPincode;
                clscmp.Phone = strPhone;
                clscmp.Fax = strFax;
                clscmp.Email = strEmail;
                clscmp.TIN = strTin;
                clscmp.CST = strCST;

                BusinessLogic bl = new BusinessLogic(sDataSource);

                try
                {
                    bl.InsertSettings(itemCode, strIP, strQtyReturn, strDate, strBillFormat, currency, dealer, barcode, stockEdit, smsRequired, blitRequired, strOwnerMob, strVATReconDate, strVATAmount, discType, exceedLimit, strBillMethod, strobsolute, droundoff, dsalesseries, autolock, savelog, enablevat, emailRequired, macaddress, tinnoman, enabledate, salesdiscount, openingbalance, deviationprice, pwdexpday, purchasepricelist, purchaseround, sapcheck);

                    System.Threading.Thread.Sleep(1000);

                    DataSet dsData = bl.GetAppSettings(Request.Cookies["Company"].Value);

                    Session["AppSettings"] = dsData;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Company & Configuration Information Successfully Stored. Please Logout and Login again to refelect the Changes. Thank You.');", true);

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Error Occured: '" + ex.Message + ");", true);
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnCompanyInfo_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("Login.aspx");

                string strCompany = string.Empty;
                string strAddress = string.Empty;
                string strCity = string.Empty;
                string strState = string.Empty;
                string strPincode = string.Empty;
                string strPhone = string.Empty;
                string strFax = string.Empty;
                string strEmail = string.Empty;
                string strTin = string.Empty;
                string strCST = string.Empty;

                strCompany = txtCompanyName.Text.Trim();
                strAddress = txtAddress.Text.Trim();
                strCity = txtCity.Text.Trim();
                strState = txtState.Text.Trim();
                strPincode = txtPincode.Text.Trim();
                strPhone = txtPhone.Text.Trim();
                strFax = txtFAX.Text.Trim();
                strEmail = txtEmail.Text.Trim();
                strTin = txtTin.Text.Trim();
                strCST = txtCST.Text.Trim();

                clsCompany clscmp = new clsCompany();
                clscmp.Company = strCompany;
                clscmp.Address = strAddress;
                clscmp.City = strCity;
                clscmp.State = strState;
                clscmp.Pincode = strPincode;
                clscmp.Phone = strPhone;
                clscmp.Fax = strFax;
                clscmp.Email = strEmail;
                clscmp.TIN = strTin;
                clscmp.CST = strCST;

                //string filename = fileuploadimages.PostedFile.FileName;
                //fileuploadimages.SaveAs(Server.MapPath("App_Themes/NewTheme/Images/" + filename));

                BusinessLogic bl = new BusinessLogic(sDataSource);

                int affectedRows = bl.InsertCompanyInfo(clscmp);


                //int imageSize;
                //string imageType;
                //Stream imageStream;

                //// Gets the Size of the Image
                //imageSize = fileuploadimages.PostedFile.ContentLength;

                //// Gets the Image Type
                //imageType = fileuploadimages.PostedFile.ContentType;

                //// Reads the Image stream
                //imageStream = fileuploadimages.PostedFile.InputStream;

                //byte[] imageContent = new byte[imageSize];
                //int intStatus;
                //intStatus = imageStream.Read(imageContent, 0, imageSize);



                if (fileuploadimages.PostedFile.FileName.Length > 0)
                {
                    //Get Filename from fileupload control
                    string filename = Request.Cookies["Company"].Value + Path.GetFileName(fileuploadimages.PostedFile.FileName);
                    //Save images into Images folder
                    string flname = Request.Cookies["Company"].Value;
                    //fileuploadimages.SaveAs(Server.MapPath("App_Themes/NewTheme/Images/" + filename));

                    string fileLocation = Server.MapPath("~/App_Themes/NewTheme/Images/" + filename);
                    fileuploadimages.SaveAs(fileLocation);


                    //Getting dbconnection from web.config connectionstring
                    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString());
                    //Open the database connection
                    //con.Open();
                    //Query to insert images path and name into database
                    //SqlCommand cmd = new SqlCommand("Insert into ImagesPath(ImageName,ImagePath) values(@ImageName,@ImagePath)", con);
                    ////Passing parameters to query
                    //cmd.Parameters.AddWithValue("@ImageName", filename);
                    //cmd.Parameters.AddWithValue("@ImagePath", "App_Themes/NewTheme/Images/" + filename);
                    //cmd.ExecuteNonQuery();

                    string flpath = "App_Themes/NewTheme/Images/" + flname;
                    bl.InsertImageSettings(strCompany, filename, flpath);
                }



                if (affectedRows == 1)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Company Information Successfully Stored. Please Logout and Login again to refelect the Changes. Thank You.');", true);

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdUnitMnt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Cancel")
            {
                GrdUnitMnt.FooterRow.Visible = false;
                lnkBtnAdd.Visible = true;
            }
            else if (e.CommandName == "Insert")
            {
                if (!Page.IsValid)
                {
                    foreach (IValidator validator in Page.Validators)
                    {
                        if (!validator.IsValid)
                        {
                            //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
                        }
                    }
                }
                else
                {
                    BusinessLogic objBus = new BusinessLogic();

                    string unit = ((TextBox)GrdUnitMnt.FooterRow.FindControl("txtAddUnit")).Text;

                    string sQl = string.Format("Insert Into tblUnitMnt(Unit) Values('{0}')", unit);

                    srcGridView.InsertParameters.Add("sQl", TypeCode.String, sQl);
                    srcGridView.InsertParameters.Add("connection", TypeCode.String, GetConnectionString());

                    try
                    {
                        srcGridView.Insert();
                        System.Threading.Thread.Sleep(1000);
                        GrdUnitMnt.DataBind();
                        lnkBtnAdd.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            StringBuilder script = new StringBuilder();
                            script.Append("alert('Unit with this name already exists, Please try with a different name.');");

                            if (ex.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                            return;
                        }
                    }
                    //lnkBtnAdd.Visible = true;
                }


            }
            else if (e.CommandName == "Update")
            {
                if (!Page.IsValid)
                {
                    foreach (IValidator validator in Page.Validators)
                    {
                        if (!validator.IsValid)
                        {
                            //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
                        }
                    }
                    return;
                }
            }
            else if (e.CommandName == "Edit")
            {
                lnkBtnAdd.Visible = false;
            }
            else if (e.CommandName == "Page")
            {
                //lnkBtnAdd.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdUnitMnt_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(1000);
            GrdUnitMnt.DataBind();
            lnkBtnAdd.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdUnitMnt_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (!Page.IsValid)
            {
                foreach (IValidator validator in Page.Validators)
                {
                    if (!validator.IsValid)
                    {
                        //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
                    }
                }

            }
            else
            {

                string unit = ((TextBox)GrdUnitMnt.Rows[e.RowIndex].FindControl("txtUnit")).Text;
                string Id = GrdUnitMnt.DataKeys[e.RowIndex].Value.ToString();

                srcGridView.UpdateMethod = "UpdateUnitMnt";
                srcGridView.UpdateParameters.Add("connection", TypeCode.String, GetConnectionString());
                srcGridView.UpdateParameters.Add("Unit", TypeCode.String, unit);
                srcGridView.UpdateParameters.Add("ID", TypeCode.Int32, Id);
                //lnkBtnAdd.Visible = true;

            }
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

    protected void GrdUnitMnt_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdUnitMnt, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void AddConnStr(string csName, string path)
    {
        try
        {
            System.Configuration.Configuration webConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringsSection dbConnString = webConfig.ConnectionStrings;

            dbConnString.ConnectionStrings.Add(new ConnectionStringSettings(csName, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=True;Jet OLEDB:Database Password=moonmoon", "System.Data.OleDb"));
            dbConnString.ConnectionStrings.Remove(Request.Cookies["Company"].Value);
            HttpCookie cookie = new HttpCookie("Company");
            cookie.HttpOnly = true;
            cookie.Secure = true;
            cookie.Value = csName;
            Response.Cookies.Add(cookie);

            webConfig.Save();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    protected void grdViewIP_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                System.Threading.Thread.Sleep(1000);
                grdViewIP.DataBind();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridPosting_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                System.Threading.Thread.Sleep(1000);
                GridPosting.DataBind();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnAddIP_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic objBus = new BusinessLogic();
            objBus.AddIPAddresses(Request.Cookies["Company"].Value, txtAddIP.Text.Trim());
            txtAddIP.Text = string.Empty;
            System.Threading.Thread.Sleep(1000);
            grdViewIP.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnAddPostLedger_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic objBus = new BusinessLogic();
            string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (objBus.IsLedgerFoundinJournalPosting(connection, ddBank1.SelectedItem.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Ledger - " + ddBank1.SelectedItem.Text + " - already added.');", true);
                return;
            }
            else
            {
                objBus.AddPostingLedger(Request.Cookies["Company"].Value, ddBank1.SelectedItem.Text);
                ddBank1.SelectedIndex = 0;
                System.Threading.Thread.Sleep(1000);
                GridPosting.DataBind();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void rdIPBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdIPBlock.SelectedValue == "YES")
            {
                TabPanel2.Visible = true;
                TabPanel2.Enabled = true;
            }
            else
                TabPanel2.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdTransporter_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Cancel")
            {
                GrdTransporter.FooterRow.Visible = false;
                lnkBtnAddTransporter.Visible = true;

            }
            else if (e.CommandName == "Insert")
            {
                if (!Page.IsValid)
                {
                    foreach (IValidator validator in Page.Validators)
                    {
                        if (!validator.IsValid)
                        {
                            //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
                        }
                    }
                }
                else
                {
                    BusinessLogic objBus = new BusinessLogic();

                    string Transporter = ((TextBox)GrdTransporter.FooterRow.FindControl("txtAddTransporter")).Text;

                    string sQl = string.Format("Insert Into tblTransporter(Transporter) Values('{0}')", Transporter);

                    srcGridTransporter.InsertParameters.Add("sQl", TypeCode.String, sQl);
                    srcGridTransporter.InsertParameters.Add("connection", TypeCode.String, GetConnectionString());

                    try
                    {
                        srcGridTransporter.Insert();
                        System.Threading.Thread.Sleep(1000);
                        GrdTransporter.DataBind();
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            StringBuilder script = new StringBuilder();
                            script.Append("alert('Transporter with this name already exists, Please try with a different name.');");

                            if (ex.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                            return;
                        }
                    }

                    lnkBtnAddTransporter.Visible = true;
                }


            }
            else if (e.CommandName == "Update")
            {
                if (!Page.IsValid)
                {
                    foreach (IValidator validator in Page.Validators)
                    {
                        if (!validator.IsValid)
                        {
                            //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
                        }
                    }
                    return;
                }
            }
            else if (e.CommandName == "Edit")
            {
                lnkBtnAddTransporter.Visible = false;
            }
            else if (e.CommandName == "Page")
            {
                //lnkBtnAdd.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdTransporter_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(1000);
            GrdTransporter.DataBind();
            lnkBtnAddTransporter.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdTransporter_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (!Page.IsValid)
            {
                foreach (IValidator validator in Page.Validators)
                {
                    if (!validator.IsValid)
                    {
                        //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
                    }
                }

            }
            else
            {

                string Transporter = ((TextBox)GrdTransporter.Rows[e.RowIndex].FindControl("txtTransporter")).Text;
                string Id = GrdTransporter.DataKeys[e.RowIndex].Value.ToString();

                srcGridTransporter.UpdateMethod = "UpdateTransporter";
                srcGridTransporter.UpdateParameters.Add("connection", TypeCode.String, GetConnectionString());
                srcGridTransporter.UpdateParameters.Add("Transporter", TypeCode.String, Transporter);
                srcGridTransporter.UpdateParameters.Add("TransporterID", TypeCode.Int32, Id);
                //lnkBtnAdd.Visible = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdTransporter_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdTransporter, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindDivisions()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        GrdDiv.DataSource = bl.ListDivisions();
        /*if(ds.Tables[0].Rows.Count > 1)
            GrdDiv.DataSource = ds;
        else
            GrdDiv.DataSource = null;*/

        GrdDiv.DataBind();
    }

    protected void btnDivSave_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            bl.InsertDivision(sDataSource, txtDivision.Text, txtDivAddress.Text, txtDivCity.Text, txtDivState.Text, txtDivPinNo.Text, txtDivPhoneNo.Text, txtDivFax.Text, txtDivEmail.Text, txtDivTinNo.Text, txtDivGSTNo.Text);
            System.Threading.Thread.Sleep(1000);
            DataSet ds = bl.ListDivisions();
            GrdDiv.DataSource = ds;
            GrdDiv.DataBind();
            pnlDivsion.Visible = false;
            GrdDiv.Visible = true;
            BtnAddDivision.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlDivsion.Visible = true;
            btnDivSave.Visible = false;
            btnDivUpdate.Visible = true;
            GrdDiv.Visible = false;
            BtnAddDivision.Visible = false;

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            hdDivision.Value = GrdDiv.SelectedDataKey.Value.ToString();
            DataSet ds = bl.GetDivisionForId(sDataSource, int.Parse(GrdDiv.SelectedDataKey.Value.ToString()));

            if (ds != null)
            {
                txtDivision.Text = ds.Tables[0].Rows[0]["DivisionName"].ToString();
                txtDivAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtDivCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                txtDivState.Text = ds.Tables[0].Rows[0]["State"].ToString();
                txtDivPinNo.Text = ds.Tables[0].Rows[0]["PinCode"].ToString();
                txtDivPhoneNo.Text = ds.Tables[0].Rows[0]["Phone"].ToString();
                txtDivEmail.Text = ds.Tables[0].Rows[0]["eMail"].ToString();
                txtDivFax.Text = ds.Tables[0].Rows[0]["Fax"].ToString();
                txtDivTinNo.Text = ds.Tables[0].Rows[0]["TINNo"].ToString();
                txtDivGSTNo.Text = ds.Tables[0].Rows[0]["GSTNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnAddDivision_Click(object sender, EventArgs e)
    {
        try
        {
            ResetDivisions();
            BtnAddDivision.Visible = false;
            btnDivSave.Visible = true;
            btnDivUpdate.Visible = false;
            pnlDivsion.Visible = true;
            GrdDiv.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnDivCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlDivsion.Visible = false;
            GrdDiv.Visible = true;
            BtnAddDivision.Visible = true;
            ResetDivisions();
            BindDivisions();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void ResetDivisions()
    {
        hdDivision.Value = string.Empty;
        txtDivision.Text = string.Empty;
        txtDivAddress.Text = string.Empty;
        txtDivCity.Text = string.Empty;
        txtDivState.Text = string.Empty;
        txtDivPinNo.Text = string.Empty;
        txtDivPhoneNo.Text = string.Empty;
        txtDivEmail.Text = string.Empty;
        txtDivFax.Text = string.Empty;
        txtDivTinNo.Text = string.Empty;
        txtDivGSTNo.Text = string.Empty;
    }

    protected void btnDivUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            bl.UpdateDivision(sDataSource, int.Parse(hdDivision.Value.ToString()), txtDivision.Text, txtDivAddress.Text, txtDivCity.Text, txtDivState.Text, txtDivPinNo.Text, txtDivPhoneNo.Text, txtDivFax.Text, txtDivEmail.Text, txtDivTinNo.Text, txtDivGSTNo.Text);
            System.Threading.Thread.Sleep(1000);
            DataSet ds = bl.ListDivisions();
            GrdDiv.DataSource = ds;
            GrdDiv.DataBind();
            pnlDivsion.Visible = false;
            GrdDiv.Visible = true;
            BtnAddDivision.Visible = true;
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
            GrdDiv.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindDivisions();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdDiv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdDiv.PageIndex = e.NewPageIndex;
            BindDivisions();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdDiv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdDiv, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdDiv_DataBound(object sender, EventArgs e)
    {

        //GrdDiv.Rows[0].Visible = false;
        /*
        if (GrdDiv.Rows.Count == 1 && !GrdDiv.FooterRow.Visible)
        {
            GrdDiv.Columns[0].HeaderText = "No Divisions found!";
            GrdDiv.Columns[1].Visible = false;
            GrdDiv.Columns[2].Visible = false;
            GrdDiv.Columns[3].Visible = false;
            GrdDiv.Columns[4].Visible = false;
            GrdDiv.Columns[5].Visible = false;
        }
        else
            GrdDiv.Columns[0].HeaderText = "No Divsions found.";
         * */
    }


    protected void GrdDiv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int divisionID = (int)GrdDiv.DataKeys[e.RowIndex].Value;
            bl.DeleteDivision(sDataSource, divisionID);
            GrdDiv.DataSource = bl.ListDivisions();
            GrdDiv.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void btnSalebill_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                string strCompany = string.Empty;

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string branchcode = string.Empty;
                try
                {
                    if (EditableGrid.Rows.Count > 0)
                    {
                        foreach (GridViewRow gr in EditableGrid.Rows)
                        {
                            TextBox txtNorSal = (TextBox)gr.Cells[1].FindControl("txtNorSal");
                            TextBox txtManSal = (TextBox)gr.Cells[2].FindControl("txtManSal");
                            TextBox txtPurRtn = (TextBox)gr.Cells[3].FindControl("txtPurRtn");
                            TextBox txtIntTfn = (TextBox)gr.Cells[4].FindControl("txtIntTfn");
                            TextBox txtDlNte = (TextBox)gr.Cells[5].FindControl("txtDlNte");
                            TextBox txtDlRtn = (TextBox)gr.Cells[6].FindControl("txtDlRtn");
                            branchcode = gr.Cells[0].Text.Replace("&quot;", "\"");

                            if (txtNorSal.Text != "")
                            {
                                bl.InsertSalesBillNo(Convert.ToInt32(txtNorSal.Text), Convert.ToInt32(txtManSal.Text), Convert.ToInt32(txtPurRtn.Text), Convert.ToInt32(txtIntTfn.Text), Convert.ToInt32(txtDlNte.Text), Convert.ToInt32(txtDlRtn.Text), branchcode);
                            }
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Bill No Information Successfully Stored. Please Logout and Login again to refelect the Changes. Thank You.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Error Occured: '" + ex.Message + ");", true);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
