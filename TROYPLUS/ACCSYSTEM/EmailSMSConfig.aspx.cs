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

public partial class EmailSMSConfig : System.Web.UI.Page
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
                //loadProducts();
                //loadBillFormat();
                //GetSettingsInfo();
                BindDivisions();
                BindDiv();
                //GetCompanyInfo();
                //Label1.Text = Helper.GenerateUniqueIDForThisPC();
                DisableForOffline();

                //loadBanks();
                //GrdTransporter.PageSize = 5;
                //GrdUnitMnt.PageSize = 5;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (chk.Checked == false)
        {
            txtEmail.Visible = true;
            cmbCustomer.Visible = false;
        }
        else
        {
            cmbCustomer.Visible = true;
            txtEmail.Visible = false;
        }
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == false)
        {
            TextBox2.Visible = true;
            DropDownList2.Visible = false;
        }
        else
        {
            DropDownList2.Visible = true;
            TextBox2.Visible = false;
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
            //btnSave.Enabled = false;
            //btnAddIP.Enabled = false;
            BtnAddDivision.Enabled = false;
            btnDivSave.Enabled = false;
            btnDivUpdate.Enabled = false;
            GrdDiv.Columns[4].Visible = false;
            GrdDiv.Columns[5].Visible = false;
            //Button1.Enabled = false;
            GrdScreen.Columns[1].Visible = false;
            //lnkBtnAddTransporter.Enabled = false;
            //GrdTransporter.Columns[1].Visible = false;
        }
    }

    private void loadProducts()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListProducts();
        //cmbProdAdd.DataSource = ds;
        //cmbProdAdd.DataBind();
        //cmbProdAdd.DataTextField = "ProductName";
        //cmbProdAdd.DataValueField = "ItemCode";
    }
    private void loadBillFormat()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBillFormat();
        //cmdBill.DataSource = ds;
        //cmdBill.DataBind();
        //cmdBill.DataTextField = "Key";
        //cmdBill.DataValueField = "Key";
    }

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            GrdScreen.FooterRow.Visible = true;
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
            //GrdTransporter.DataBind();
            //GrdTransporter.ShowFooter = true;
            //lnkBtnAddTransporter.Visible = false;
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
            //GrdTransporter.Rows[0].Visible = false;

            //if (GrdTransporter.Rows.Count == 1 && !GrdTransporter.FooterRow.Visible)
            //{
            //    GrdTransporter.Columns[0].HeaderText = "No Transporters found!";
            //    GrdTransporter.Columns[1].Visible = false;
            //}
            //else
            //{
            //    GrdTransporter.Columns[0].HeaderText = "Transporter";
            //    if (!isOffline)
            //        GrdTransporter.Columns[1].Visible = true;
            //    else
            //        GrdTransporter.Columns[1].Visible = false;
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
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBankLedgerpaymnet();

        //ddBank1.DataSource = ds;
        //ddBank1.DataTextField = "LedgerName";
        //ddBank1.DataValueField = "LedgerID";
        //ddBank1.DataBind();

    }

    public void GetSettingsInfo()
    {
        if (Request.Cookies["Company"]  != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetSettings();
        ListItem liProd;
        ListItem liDiscType;
        ListItem liBill;
        //if (ds != null)
        //{
        //    if (ds.Tables[0] != null)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                if (dr["Key"].ToString() == "ITEMCODE")
        //                {
        //                    cmbProdAdd.ClearSelection();
        //                    liProd = cmbProdAdd.Items.FindByValue(Convert.ToString(dr["KeyValue"]));
        //                    if (liProd != null)
        //                        liProd.Selected = true;
        //                }
        //                else if (dr["Key"].ToString() == "IPBLOCKING")
        //                {
        //                    rdIPBlock.SelectedValue = dr["KeyValue"].ToString();

        //                    if (rdIPBlock.SelectedValue == "YES")
        //                        TabPanel2.Visible = true;
        //                    else
        //                        TabPanel2.Visible = false;
        //                }
        //                else if (dr["Key"].ToString() == "CURRENCY")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        ddCurrency.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "QTYRETURN")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdQtyReturn.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "BARCODE")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdoBarcode.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "STOCKEDIT")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdoStockEdit.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "QTYDATE")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        txtDate.Text = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "OWNERMOB")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        txtMobile.Text = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "VATRECDATE")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        txtVATReconDate.Text = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "VATAMOUNT")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        txtVATAmount.Text = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "DEALER")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdDealer.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "BLITREQ")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdoBLIT.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "CREDITEXD")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdoExceedCreditLimit.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "BILLFORMAT")
        //                {
        //                    cmdBill.ClearSelection();
        //                    liBill = cmdBill.Items.FindByValue(Convert.ToString(dr["KeyValue"]));
        //                    if (liBill != null)
        //                        liBill.Selected = true;
        //                }
        //                else if (dr["Key"].ToString() == "DISCTYPE")
        //                {
        //                    ddDiscType.ClearSelection();
        //                    liDiscType = ddDiscType.Items.FindByValue(Convert.ToString(dr["KeyValue"]));

        //                    if (liDiscType != null)
        //                        liDiscType.Selected = true;
        //                }
        //                else if (dr["Key"].ToString() == "SMSREQ")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdoSMSRqrd.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "BILLMETHOD")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        DropDownList1.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "OBSOLUTE")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        dpobsolute.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "ROUNDOFF")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        dproundoff.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "SALBILLNO")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        dpsalesbillno.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "AUTOLOCK")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        dpautolock.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "SAVELOG")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        dpsavelog.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "ENBLVAT")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        ddenablevat.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "EMAILREQ")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdoemailrequired.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "MACBLOCK")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdomacaddress.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "TINREQ")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdotinnomandatory.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //                else if (dr["Key"].ToString() == "ENBLDATE")
        //                {
        //                    if (dr["KeyValue"] != null)
        //                        rdvoudateenable.SelectedValue = dr["KeyValue"].ToString();
        //                }
        //            }
        //        }
        //    }
        //}
    }
    public void GetCompanyInfo()
    {
        if (Request.Cookies["Company"]  != null)
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
                        //txtCompanyName.Text = Convert.ToString(dr["CompanyName"]);
                        //txtAddress.Text = Convert.ToString(dr["Address"]); ;
                        //txtCity.Text = Convert.ToString(dr["City"]); ;
                        //txtState.Text = Convert.ToString(dr["State"]); ;
                        //txtPincode.Text = Convert.ToString(dr["Pincode"]); ;
                        //txtPhone.Text = Convert.ToString(dr["Phone"]); ;
                        //txtFAX.Text = Convert.ToString(dr["Fax"]); ;
                        //txtEmail.Text = Convert.ToString(dr["Email"]); ;
                        //txtTin.Text = Convert.ToString(dr["Tinno"]); ;
                        //txtCST.Text = Convert.ToString(dr["gstno"]); ;
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
                //PresentationUtils.SetPagerButtonStates(grdViewIP, e.Row, this);
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
                //PresentationUtils.SetPagerButtonStates(GridPosting, e.Row, this);
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

                string emailRequired = string.Empty;
                string macaddress = string.Empty;
                string tinnoman = string.Empty;
                //emailRequired = rdoemailrequired.SelectedValue;
                //macaddress = rdomacaddress.SelectedValue;
                //tinnoman = rdotinnomandatory.SelectedValue;
                //enabledate = rdvoudateenable.SelectedValue;

                //strBillMethod = DropDownList1.SelectedValue;
                //droundoff = dproundoff.SelectedValue;
                //dsalesseries = dpsalesbillno.SelectedValue;

                //strobsolute = dpobsolute.SelectedValue;
                //strCompany = txtCompanyName.Text.Trim();
                //strAddress = txtAddress.Text.Trim();
                //strCity = txtCity.Text.Trim();
                //strState = txtState.Text.Trim();
                //strPincode = txtPincode.Text.Trim();
                //strPhone = txtPhone.Text.Trim();
                //strFax = txtFAX.Text.Trim();
                //strEmail = txtEmail.Text.Trim();
                //strTin = txtTin.Text.Trim();
                //strCST = txtCST.Text.Trim();
                //currency = ddCurrency.SelectedValue;
                //blitRequired = rdoBLIT.SelectedValue;
                //itemCode = cmbProdAdd.SelectedItem.Value;
                //strIP = rdIPBlock.SelectedItem.Text;
                //strQtyReturn = rdQtyReturn.SelectedItem.Text;
                //strDate = txtDate.Text.Trim();
                //strBillFormat = cmdBill.SelectedItem.Text;
                //dealer = rdDealer.SelectedValue;
                //barcode = rdoBarcode.SelectedValue;
                //stockEdit = rdoStockEdit.SelectedValue;

                //autolock = dpautolock.SelectedValue;

                //savelog = dpsavelog.SelectedValue;
                //enablevat = ddenablevat.SelectedValue;

                //smsRequired = rdoSMSRqrd.SelectedValue;
                //strOwnerMob = txtMobile.Text;
                //strVATReconDate = txtVATReconDate.Text;
                //strVATAmount = txtVATAmount.Text;
                //discType = ddDiscType.SelectedValue;
                //exceedLimit = rdoExceedCreditLimit.SelectedItem.Text;

                //clsCompany clscmp = new clsCompany();
                //clscmp.Company = strCompany;
                //clscmp.Address = strAddress;
                //clscmp.City = strCity;
                //clscmp.State = strState;
                //clscmp.Pincode = strPincode;
                //clscmp.Phone = strPhone;
                //clscmp.Fax = strFax;
                //clscmp.Email = strEmail;
                //clscmp.TIN = strTin;
                //clscmp.CST = strCST;

                //BusinessLogic bl = new BusinessLogic(sDataSource);

                //try
                //{
                //    bl.InsertSettings(itemCode, strIP, strQtyReturn, strDate, strBillFormat, currency, dealer, barcode, stockEdit, smsRequired, blitRequired, strOwnerMob, strVATReconDate, strVATAmount, discType, exceedLimit, strBillMethod, strobsolute, droundoff, dsalesseries, autolock, savelog, enablevat, emailRequired, macaddress, tinnoman, enabledate);

                //    System.Threading.Thread.Sleep(1000);

                //    DataSet dsData = bl.GetAppSettings(Request.Cookies["Company"].Value);

                //    Session["AppSettings"] = dsData;

                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Company & Configuration Information Successfully Stored. Please Logout and Login again to refelect the Changes. Thank You.');", true);

                //}
                //catch (Exception ex)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Error Occured: '" + ex.Message + ");", true);
                //}

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

                //strCompany = txtCompanyName.Text.Trim();
                //strAddress = txtAddress.Text.Trim();
                //strCity = txtCity.Text.Trim();
                //strState = txtState.Text.Trim();
                //strPincode = txtPincode.Text.Trim();
                //strPhone = txtPhone.Text.Trim();
                //strFax = txtFAX.Text.Trim();
                //strEmail = txtEmail.Text.Trim();
                //strTin = txtTin.Text.Trim();
                //strCST = txtCST.Text.Trim();

                //clsCompany clscmp = new clsCompany();
                //clscmp.Company = strCompany;
                //clscmp.Address = strAddress;
                //clscmp.City = strCity;
                //clscmp.State = strState;
                //clscmp.Pincode = strPincode;
                //clscmp.Phone = strPhone;
                //clscmp.Fax = strFax;
                //clscmp.Email = strEmail;
                //clscmp.TIN = strTin;
                //clscmp.CST = strCST;

                ////string filename = fileuploadimages.PostedFile.FileName;
                ////fileuploadimages.SaveAs(Server.MapPath("App_Themes/NewTheme/Images/" + filename));

                //BusinessLogic bl = new BusinessLogic(sDataSource);

                //int affectedRows = bl.InsertCompanyInfo(clscmp);


                ////int imageSize;
                ////string imageType;
                ////Stream imageStream;

                ////// Gets the Size of the Image
                ////imageSize = fileuploadimages.PostedFile.ContentLength;

                ////// Gets the Image Type
                ////imageType = fileuploadimages.PostedFile.ContentType;

                ////// Reads the Image stream
                ////imageStream = fileuploadimages.PostedFile.InputStream;

                ////byte[] imageContent = new byte[imageSize];
                ////int intStatus;
                ////intStatus = imageStream.Read(imageContent, 0, imageSize);



                //if (fileuploadimages.PostedFile.FileName.Length > 0)
                //{
                //    //Get Filename from fileupload control
                //    string filename = Request.Cookies["Company"].Value + Path.GetFileName(fileuploadimages.PostedFile.FileName);
                //    //Save images into Images folder
                //    string flname = Request.Cookies["Company"].Value;
                //    //fileuploadimages.SaveAs(Server.MapPath("App_Themes/NewTheme/Images/" + filename));

                //    string fileLocation = Server.MapPath("~/App_Themes/NewTheme/Images/" + filename);
                //    fileuploadimages.SaveAs(fileLocation);


                //    //Getting dbconnection from web.config connectionstring
                //    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString());
                //    //Open the database connection
                //    //con.Open();
                //    //Query to insert images path and name into database
                //    //SqlCommand cmd = new SqlCommand("Insert into ImagesPath(ImageName,ImagePath) values(@ImageName,@ImagePath)", con);
                //    ////Passing parameters to query
                //    //cmd.Parameters.AddWithValue("@ImageName", filename);
                //    //cmd.Parameters.AddWithValue("@ImagePath", "App_Themes/NewTheme/Images/" + filename);
                //    //cmd.ExecuteNonQuery();

                //    string flpath = "App_Themes/NewTheme/Images/" + flname;
                //    bl.InsertImageSettings(strCompany, filename, flpath);
                //}



                //if (affectedRows == 1)
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Company Information Successfully Stored. Please Logout and Login again to refelect the Changes. Thank You.');", true);

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdScreen_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Cancel")
            {
                GrdScreen.FooterRow.Visible = false;
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

                    int ScreenNo = Convert.ToInt32(((TextBox)GrdScreen.FooterRow.FindControl("txtAddScreenNo")).Text);
                    string ScreenName = ((TextBox)GrdScreen.FooterRow.FindControl("txtAddScreenName")).Text;
                    string Subject = ((TextBox)GrdScreen.FooterRow.FindControl("txtAddSubject")).Text;
                    string Content = ((TextBox)GrdScreen.FooterRow.FindControl("txtAddContent")).Text;

                    string sQl = string.Format("Insert Into tblScreenMaster(ScreenNo,ScreenName,Subject,Content) Values({0},'{1}','{2}','{3}')", ScreenNo, ScreenName, Subject, Content);

                    srcGridView.InsertParameters.Add("sQl", TypeCode.String, sQl);
                    srcGridView.InsertParameters.Add("connection", TypeCode.String, GetConnectionString());

                    try
                    {
                        srcGridView.Insert();
                        System.Threading.Thread.Sleep(1000);
                        GrdScreen.DataBind();
                        lnkBtnAdd.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            StringBuilder script = new StringBuilder();
                            script.Append("alert('Screen with this name already exists, Please try with a different name.');");

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

    protected void GrdScreen_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(1000);
            GrdScreen.DataBind();
            lnkBtnAdd.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdScreen_RowUpdating(object sender, GridViewUpdateEventArgs e)
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

                string ScreenNo = ((TextBox)GrdScreen.Rows[e.RowIndex].FindControl("txtScreenNo")).Text;
                string ScreenName = ((TextBox)GrdScreen.Rows[e.RowIndex].FindControl("txtScreenName")).Text;
                string Subject = ((TextBox)GrdScreen.Rows[e.RowIndex].FindControl("txtSubject")).Text;
                string Content = ((TextBox)GrdScreen.Rows[e.RowIndex].FindControl("txtContent")).Text;
                string ScreenId = GrdScreen.DataKeys[e.RowIndex].Value.ToString();

                srcGridView.UpdateMethod = "UpdateScreen";
                srcGridView.UpdateParameters.Add("connection", TypeCode.String, GetConnectionString());
                srcGridView.UpdateParameters.Add("ScreenNo", TypeCode.Int32, ScreenNo);
                srcGridView.UpdateParameters.Add("ScreenName", TypeCode.String, ScreenName);
                srcGridView.UpdateParameters.Add("Subject", TypeCode.String, Subject);
                srcGridView.UpdateParameters.Add("ScreenId", TypeCode.Int32, ScreenId);
                srcGridView.UpdateParameters.Add("Content", TypeCode.String, Content);
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

        if (Request.Cookies["Company"]  != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        return connStr;
    }

    protected void GrdScreen_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdScreen, e.Row, this);
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
            //if (e.Exception == null)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    grdViewIP.DataBind();
            //}
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
                //System.Threading.Thread.Sleep(1000);
                //GridPosting.DataBind();
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
            //BusinessLogic objBus = new BusinessLogic();
            //objBus.AddIPAddresses(Request.Cookies["Company"].Value, txtAddIP.Text.Trim());
            //txtAddIP.Text = string.Empty;
            //System.Threading.Thread.Sleep(1000);
            //grdViewIP.DataBind();
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
            //BusinessLogic objBus = new BusinessLogic();
            //string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            //if (objBus.IsLedgerFoundinJournalPosting(connection, ddBank1.SelectedItem.Text))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Ledger - " + ddBank1.SelectedItem.Text + " - already added.');", true);
            //    return;
            //}
            //else
            //{
            //    objBus.AddPostingLedger(Request.Cookies["Company"].Value, ddBank1.SelectedItem.Text);
            //    ddBank1.SelectedIndex = 0;
            //    System.Threading.Thread.Sleep(1000);
            //    GridPosting.DataBind();
            //}
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
            //if (rdIPBlock.SelectedValue == "YES")
            //{
            //    TabPanel2.Visible = true;
            //    TabPanel2.Enabled = true;
            //}
            //else
            //    TabPanel2.Visible = false;
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
            //if (e.CommandName == "Cancel")
            //{
            //    //GrdTransporter.FooterRow.Visible = false;
            //    //lnkBtnAddTransporter.Visible = true;

            //}
            //else if (e.CommandName == "Insert")
            //{
            //    if (!Page.IsValid)
            //    {
            //        foreach (IValidator validator in Page.Validators)
            //        {
            //            if (!validator.IsValid)
            //            {
            //                //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        BusinessLogic objBus = new BusinessLogic();

            //        string Transporter = ((TextBox)GrdTransporter.FooterRow.FindControl("txtAddTransporter")).Text;

            //        string sQl = string.Format("Insert Into tblTransporter(Transporter) Values('{0}')", Transporter);

            //        srcGridTransporter.InsertParameters.Add("sQl", TypeCode.String, sQl);
            //        srcGridTransporter.InsertParameters.Add("connection", TypeCode.String, GetConnectionString());

            //        try
            //        {
            //            srcGridTransporter.Insert();
            //            System.Threading.Thread.Sleep(1000);
            //            GrdTransporter.DataBind();
            //        }
            //        catch (Exception ex)
            //        {
            //            if (ex.InnerException != null)
            //            {
            //                StringBuilder script = new StringBuilder();
            //                script.Append("alert('Transporter with this name already exists, Please try with a different name.');");

            //                if (ex.InnerException.Message.IndexOf("duplicate values in the index") > -1)
            //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

            //                return;
            //            }
            //        }

            //        lnkBtnAddTransporter.Visible = true;
            //    }


            //}
            //else if (e.CommandName == "Update")
            //{
            //    if (!Page.IsValid)
            //    {
            //        foreach (IValidator validator in Page.Validators)
            //        {
            //            if (!validator.IsValid)
            //            {
            //                //errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
            //            }
            //        }
            //        return;
            //    }
            //}
            //else if (e.CommandName == "Edit")
            //{
            //    lnkBtnAddTransporter.Visible = false;
            //}
            //else if (e.CommandName == "Page")
            //{
            //    //lnkBtnAdd.Visible = true;
            //}
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
            //System.Threading.Thread.Sleep(1000);
            //GrdTransporter.DataBind();
            //lnkBtnAddTransporter.Visible = true;
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

                //string Transporter = ((TextBox)GrdTransporter.Rows[e.RowIndex].FindControl("txtTransporter")).Text;
                //string Id = GrdTransporter.DataKeys[e.RowIndex].Value.ToString();

                //srcGridTransporter.UpdateMethod = "UpdateTransporter";
                //srcGridTransporter.UpdateParameters.Add("connection", TypeCode.String, GetConnectionString());
                //srcGridTransporter.UpdateParameters.Add("Transporter", TypeCode.String, Transporter);
                //srcGridTransporter.UpdateParameters.Add("TransporterID", TypeCode.Int32, Id);
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
            //if (e.Row.RowType == DataControlRowType.Pager)
            //{
            //    PresentationUtils.SetPagerButtonStates(GrdTransporter, e.Row, this);
            //}
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

        GrdDiv.DataSource = bl.ListEmailConfig();
        /*if(ds.Tables[0].Rows.Count > 1)
            GrdDiv.DataSource = ds;
        else
            GrdDiv.DataSource = null;*/

        GrdDiv.DataBind();
    }

    private void BindDiv()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        GridView1.DataSource = bl.ListSMSConfig();
        /*if(ds.Tables[0].Rows.Count > 1)
            GrdDiv.DataSource = ds;
        else
            GrdDiv.DataSource = null;*/

        GridView1.DataBind();
    }

    protected void btnDivSave_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            string email = string.Empty;
            string chkk = string.Empty;
            if (chk.Checked == true)
            {
                email = cmbCustomer.SelectedValue;
                chkk = "Y";
            }
            else
            {
                email = txtEmail.Text;
                chkk = "N";
            }

            bl.InsertEmailConfig(sDataSource, Convert.ToInt32(txtScreenNumber.Text), email, drpActive.SelectedValue, chkk);
            System.Threading.Thread.Sleep(1000);
            DataSet ds = bl.ListEmailConfig();
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

    protected void btnConfigSave_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            string email = string.Empty;
            string chkk = string.Empty;
            if (chk.Checked == true)
            {
                email = DropDownList2.SelectedValue;
                chkk = "Y";
            }
            else
            {
                email = TextBox2.Text;
                chkk = "N";
            }

            bl.InsertSMSConfig(sDataSource, Convert.ToInt32(TextBox1.Text), email, DropDownList1.SelectedValue, chkk);
            System.Threading.Thread.Sleep(1000);
            DataSet ds = bl.ListSMSConfig();
            GridView1.DataSource = ds;
            GridView1.DataBind();
            Panel1.Visible = false;
            GridView1.Visible = true;
            BtnAddConfig.Visible = true;
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
            DataSet ds = bl.GetConfigForId(sDataSource, int.Parse(GrdDiv.SelectedDataKey.Value.ToString()), "Email");

            if (ds != null)
            {
                txtScreenNumber.Text = ds.Tables[0].Rows[0]["ScreenNo"].ToString();

                if (ds.Tables[0].Rows[0]["Chk"].ToString() == "Y")
                {
                    txtEmail.Visible = false;
                    cmbCustomer.Visible = true;
                    cmbCustomer.SelectedValue = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    chk.Checked = true;
                }
                else
                {
                    txtEmail.Visible = true;
                    cmbCustomer.Visible = false;
                    txtEmail.Text = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    chk.Checked = false;
                }

                drpActive.SelectedValue = ds.Tables[0].Rows[0]["Active"].ToString();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Panel1.Visible = true;
            btnConfigSave.Visible = false;
            btnConfigUpdate.Visible = true;
            GridView1.Visible = false;
            BtnAddConfig.Visible = false;

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            HiddenField1.Value = GridView1.SelectedDataKey.Value.ToString();
            DataSet ds = bl.GetConfigForId(sDataSource, int.Parse(GridView1.SelectedDataKey.Value.ToString()), "SMS");

            if (ds != null)
            {
                TextBox1.Text = ds.Tables[0].Rows[0]["ScreenNo"].ToString();

                if (ds.Tables[0].Rows[0]["Chk"].ToString() == "Y")
                {
                    TextBox2.Visible = false;
                    DropDownList2.Visible = true;
                    DropDownList2.SelectedValue = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    CheckBox1.Checked = true;
                }
                else
                {
                    TextBox2.Visible = true;
                    DropDownList2.Visible = false;
                    TextBox2.Text = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    CheckBox1.Checked = false;
                }

                
                DropDownList1.SelectedValue = ds.Tables[0].Rows[0]["Active"].ToString();
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

    protected void btnConfigCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Panel1.Visible = false;
            GridView1.Visible = true;
            BtnAddConfig.Visible = true;
            ResetDiv();
            BindDiv();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void ResetDivisions()
    {
        hdDivision.Value = string.Empty;
        txtEmail.Text = string.Empty;
        drpActive.SelectedIndex = 0;
        txtScreenNumber.Text = string.Empty;
        chk.Checked = true;
        cmbCustomer.SelectedIndex = 0;
        cmbCustomer.Visible = true;
        txtEmail.Visible = false;
    }

    private void ResetDiv()
    {
        HiddenField1.Value = string.Empty;
        TextBox1.Text = string.Empty;
        DropDownList1.SelectedIndex = 0;
        TextBox2.Text = string.Empty;
        CheckBox1.Checked = true;
        DropDownList2.SelectedIndex = 0;
        DropDownList2.Visible = true;
        TextBox2.Visible = false;
    }
    protected void btnDivUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            string email = string.Empty;
            string chkk = string.Empty;
            if (chk.Checked == true)
            {
                email = cmbCustomer.SelectedValue;
                chkk = "Y";
            }
            else
            {
                email = txtEmail.Text;
                chkk = "N";
            }

            bl.UpdateEmailConfig(sDataSource, int.Parse(hdDivision.Value.ToString()), Convert.ToInt32(txtScreenNumber.Text), email, drpActive.SelectedValue, chkk);
            System.Threading.Thread.Sleep(1000);
            DataSet ds = bl.ListEmailConfig();
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

    protected void btnConfigUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            string email = string.Empty;
            string chkk = string.Empty;
            if ( CheckBox1.Checked == true)
            {
                email = DropDownList2.SelectedValue;
                chkk = "Y";
            }
            else
            {
                email = TextBox2.Text;
                chkk = "N";
            }

            bl.UpdateSMSConfig(sDataSource, int.Parse(HiddenField1.Value.ToString()), Convert.ToInt32(TextBox1.Text), email, DropDownList1.SelectedValue, chkk);
            System.Threading.Thread.Sleep(1000);
            DataSet ds = bl.ListSMSConfig();
            GridView1.DataSource = ds;
            GridView1.DataBind();
            Panel1.Visible = false;
            GridView1.Visible = true;
            BtnAddConfig.Visible = true;
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

    protected void BtnAddConfig_Click(object sender, EventArgs e)
    {
        try
        {
            ResetDiv();
            BtnAddConfig.Visible = false;
            btnConfigSave.Visible = true;
            btnConfigUpdate.Visible = false;
            Panel1.Visible = true;
            GridView1.Visible = false;
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindDiv();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridView1, e.Row, this);
            }
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




}
