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
using System.Xml;
using System.IO;
using SMSLibrary;

public partial class CreateFormula : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (!Page.IsPostBack)
            {
                GridViewFormula.PageSize = 8;


                BindGrid(string.Empty);
                loadProducts();
                if (Session["TemplateItems"] != null)
                {
                    Session["TemplateItems"] = null;
                }
                //cmdCanel.PostBackUrl = Request.UrlReferrer.AbsoluteUri;
                //Session["Filename"] = "Reports//" + hdFilename.Value + "_template.xml";


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "STKMST"))
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


            }

            BusinessLogic objChk = new BusinessLogic(sDataSource);

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                lnkBtnAdd.Visible = false;
                cmdUpdate.Enabled = false;
                cmdSave.Enabled = false;
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

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

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearch.Text = "";
            BindGrid("");
            //ddCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadProducts()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListProductsIt();
        cmbProdAdd.DataSource = ds;
        cmbProdAdd.DataBind();
        cmbProdAdd.DataTextField = "ProductName";
        cmbProdAdd.DataValueField = "ItemCode";
    }

    private void BindGrid(string strFormName)
    {

        DataSet ds = new DataSet();
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet dstt = bl.GetFormulaForName(strFormName);


        if (dstt != null)
        {
            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();



            dct = new DataColumn("Row");
            dttt.Columns.Add(dct);

            dct = new DataColumn("FormulaName");
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
                        drNew["FormulaName"] = Convert.ToString(dstt.Tables[0].Rows[i]["FormulaName"]);
                        dstd.Tables[0].Rows.Add(drNew);
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    drNew = dttt.NewRow();
                        //    drNew["Row"] = sno;
                        sno = sno + 1;
                        //}
                    }
                }



                GridViewFormula.DataSource = dstd.Tables[0].DefaultView;
                GridViewFormula.DataBind();
                PanelBill.Visible = true;
            }
        }
        else
        {
            GridViewFormula.DataSource = null;
            GridViewFormula.DataBind();
            PanelBill.Visible = true;
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string seach = string.Empty;

            seach = Convert.ToString(txtSearch.Text);

            DataSet ds = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            ds = bl.CreateFormulaSearch(seach);
          //  DataSet dsd = new DataSet();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridViewFormula.DataSource = ds.Tables[0].DefaultView;
                    GridViewFormula.DataBind();
                    PanelBill.Visible = true;
                }
            }
            else
            {
                GridViewFormula.DataSource = null;
                GridViewFormula.DataBind();
                PanelBill.Visible = true;

            }


            //BindGrid(BillNo, TransNo);
            ////Accordion1.SelectedIndex = 0;
            //GrdViewItems.DataSource = null;
            //GrdViewItems.DataBind();

            //lblTotalSum.Text = "0";
            //lblTotalDis.Text = "0";
            //lblTotalVAT.Text = "0";
            //lblTotalCST.Text = "0";
            //lblFreight.Text = "0";
            //lblNet.Text = "0";

            ////PanelBill.Visible = true;
            ////PanelCmd.Visible = false;
            ////lnkBtnAdd.Visible = true;

            //Reset();

            //ResetProduct();

            //cmbProdAdd.Enabled = true;
            //cmdUpdateProduct.Enabled = false;
            //cmdSaveProduct.Enabled = true;

            //Session["productDs"] = null;

            //cmdSave.Enabled = true;
            //cmdDelete.Enabled = false;
            //cmdUpdate.Enabled = false;
            //cmdPrint.Enabled = false;
            //errPanel.Visible = false;
            //ErrMsg.Text = "";
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
            heading1.Text = "List of Added components";
            string formulaName = txtFormulaName.Text.Trim().ToUpper();
            string ItemCode = cmbProdAdd.SelectedValue.Trim();
            string Qty = txtQtyAdd.Text;
            string inOut = ddType.SelectedValue;
            string unit = ddUnit.SelectedValue;

            BindItemsGrid();
            ModalPopupExtender1.Show();
            DataSet ds = (DataSet)GrdViewItems.DataSource;
            // DataSet ds = new DataSet();
            DataRow dr;
            DataColumn dc;

            if (ds == null)
            {
                ds = new DataSet();
                DataTable dt = new DataTable();
                DataRow drNew = dt.NewRow();
                dc = new DataColumn("FormulaId");
                dt.Columns.Add(dc);
                dc = new DataColumn("ProductName");
                dt.Columns.Add(dc);
                dc = new DataColumn("ProductDesc");
                dt.Columns.Add(dc);
                dc = new DataColumn("FormulaName");
                dt.Columns.Add(dc);
                dc = new DataColumn("ItemCode");
                dt.Columns.Add(dc);
                dc = new DataColumn("Qty");
                dt.Columns.Add(dc);
                dc = new DataColumn("InOut");
                dt.Columns.Add(dc);
                dc = new DataColumn("Unit_Of_Measure");
                dt.Columns.Add(dc);



                drNew["FormulaId"] = hdTempId.Value;
                drNew["ProductDesc"] = lblProdDescAdd.Text;
                drNew["ProductName"] = lblProdNameAdd.Text;
                drNew["FormulaName"] = formulaName;
                drNew["ItemCode"] = ItemCode;
                drNew["Qty"] = Qty;
                drNew["InOut"] = inOut;
                drNew["Unit_Of_Measure"] = unit;
                //if (dc["Row"] != null)
                //    dc["Row"] = co;// dr["Row"].ToString();



                ds.Tables.Add(dt);
                ds.Tables[0].Rows.Add(drNew);
                //ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_Formula.xml"));
                Session["TemplateItems"] = ds;
                BindItemsGrid();
                ResetProduct();
            }
            else
            {

                dr = ds.Tables[0].NewRow();

                if (hdMode.Value == "New")
                {
                    hdTempId.Value = (Convert.ToInt32(hdTempId.Value) + 1).ToString();
                }
                else
                {
                    hdTempId.Value = (Convert.ToInt32(hdTempId.Value) - 1).ToString();
                }

                dr["itemCode"] = cmbProdAdd.SelectedValue;
                dr["FormulaId"] = hdTempId.Value;
                dr["ProductName"] = lblProdNameAdd.Text;
                dr["ProductDesc"] = lblProdDescAdd.Text;
                dr["Qty"] = txtQtyAdd.Text.Trim();
                dr["InOut"] = ddType.SelectedValue;
                dr["Unit_Of_Measure"] = ddUnit.SelectedValue;

                ds.Tables[0].Rows.Add(dr);

                //ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_template.xml"));
                Session["TemplateItems"] = ds;

                BindItemsGrid();
                ResetProduct();


            }

            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //bl.InsertFormulaIteam(formulaName, ItemCode, Qty, inOut);
            //System.Threading.Thread.Sleep(1000);
            //BindItemsGrid();
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
            ModalPopupExtender1.Show();
            txtFormulaName.Enabled = true;
            PanelCmd.Visible = true;
            PanelItems.Visible = true;
            heading.Text = "New Product Specification";
            //        salesPanel.Visible = true;
            cmdSaveProduct.Visible = true;
            cmdSave.Enabled = true;
            cmdUpdate.Enabled = false;
            //cmdDelete.Enabled = false;
            //AccordionPane2.Visible = true;
            lnkBtnAdd.Visible = false;
            hdMode.Value = "New";
            Session["TemplateItems"] = null;
            Reset();
            //lblTotalSum.Text = "0";
            ResetProduct();
            //txtBillDate.Text = DateTime.Now.ToShortDateString();
            XmlDocument xDoc = new XmlDocument();

            hdFilename.Value = System.Guid.NewGuid().ToString();

            if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_template.xml")))
            {
                File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_template.xml"));
            }

            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();
            GrdViewItems.Columns[6].Visible = true;
            cmdSave.Enabled = true;
            //cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            GridViewFormula.Visible = false;
            //MyAccordion.Visible = false;
            cmdUpdate.Visible = false;
            cmdSave.Visible = true;
            prodPanel.Visible = true;
            prodPanel.Visible = true;
            //tabContol.Visible = true;
            Button1.Visible = true;
            GrdViewItems.Visible = false;
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
            DataSet ds = new DataSet();
            bool dupFlag = false;
            ModalPopupExtender1.Show();
            DataSet checkDs = (DataSet)Session["TemplateItems"];

            if (checkDs != null)
            {
                foreach (DataRow dR in checkDs.Tables[0].Rows)
                {
                    if (dR["itemCode"] != null)
                    {
                        if (dR["itemCode"].ToString().Trim() == cmbProdAdd.SelectedItem.Value.Trim())
                        {
                            dupFlag = true;
                            break;
                        }
                    }
                }
            }

            if (dupFlag)
            {
                cmbProdAdd.SelectedIndex = 0;
                ResetProduct();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected Product is already added.')", true);
            }

            if (cmbProdAdd.SelectedIndex != 0)
            {
                ds = bl.ListProductDetails(cmbProdAdd.SelectedItem.Value);
                //string category = lblledgerCategory.Text;
                if (ds != null)
                {
                    lblProdNameAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                    lblProdDescAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["productdesc"]) + " - " + Convert.ToString(ds.Tables[0].Rows[0]["model"]);
                }
                else
                {
                    lblProdNameAdd.Text = "";
                    lblProdDescAdd.Text = "";
                }
            }
            else
            {
                //err.Text = "Select the Product";
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void Reset()
    {
        //txtBillno.Text = "";
        //txtBillDate.Text = "";
        //txtFormName.Text = "";
        txtFormulaName.Text = "";

        foreach (Control control in cmbProdAdd.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        cmbProdAdd.ClearSelection();
        //txtCreditCardNo.Text = ""; // cmbBankName.SelectedItem.Text;
        //BindGrid(txtBillnoSrc.Text);
        //Accordion1.SelectedIndex = 1;
        GrdViewItems.DataSource = null;
        GrdViewItems.DataBind();
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {


                if (GrdViewItems.EditIndex != -1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Save the product details before Saving Stock Management defination.');", true);
                    return;
                }

                string FormulaName = txtFormulaName.Text.ToUpper();

                BindItemsGrid();
                DataSet ds = (DataSet)GrdViewItems.DataSource;
                ModalPopupExtender1.Show();
                if (ds != null)
                {

                    BusinessLogic bl = new BusinessLogic(sDataSource);

                    int InCount = 0;
                    int OutCount = 0;

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (dr["InOut"].ToString() == "Raw Material")
                                    InCount = InCount + 1;
                                else
                                    OutCount = OutCount + 1;
                            }
                        }
                    }

                    if ((InCount == 0) || (OutCount == 0))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Minimum one Raw Materials and one Products should be added.');", true);
                        return;
                    }
                    bl.UpdateFormulaItem(FormulaName, ds);

                    //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
                    //BusinessLogic bl = new BusinessLogic(sDataSource);
                    //int billNo = bl.InsertSales(sBilldate, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, iPaymode, sCreditCardno, iBank, dTotalAmt, purchaseReturn, prReason, Convert.ToInt32(executive), ds);
                    Reset();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Update Successfully.')", true);
                    //ResetProduct();
                    if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_template.xml")))
                        File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_template.xml"));


                    string usernam = Request.Cookies["LoggedUserName"].Value;

                    string salestype = string.Empty;
                    int ScreenNo = 0;
                    string ScreenName = string.Empty;

                    string connection = Request.Cookies["Company"].Value;

                    salestype = "Create Formula";
                    ScreenName = "Create Formula";


                    bool mobile = false;
                    bool Email = false;
                    string emailsubject = string.Empty;

                    string emailcontent = string.Empty;
                    if (hdEmailRequired.Value == "YES")
                    {
                        var toAddress = "";
                        var toAdd = "";
                        Int32 ModeofContact = 0;
                        int ScreenType = 0;


                        DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                        if (dsdd != null)
                        {
                            if (dsdd.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdd.Tables[0].Rows)
                                {
                                    ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                    mobile = Convert.ToBoolean(dr["mobile"]);
                                    Email = Convert.ToBoolean(dr["Email"]);
                                    emailsubject = Convert.ToString(dr["emailsubject"]);
                                    emailcontent = Convert.ToString(dr["emailcontent"]);

                                    if (ScreenType == 1)
                                    {

                                        toAddress = toAdd;

                                    }
                                    else
                                    {
                                        toAddress = dr["EmailId"].ToString();
                                    }
                                    if (Email == true)
                                    {
                                        string body = "\n";

                                        int index123 = emailcontent.IndexOf("@Branch");
                                        body = Request.Cookies["Company"].Value;
                                        if (index123 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                        }

                                        int sno = 1;
                                        string prd = string.Empty;
                                        int index322 = emailcontent.IndexOf("@Product");
                                        if (ds != null)
                                        {
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                //emailcontent = emailcontent.Remove(index322, 8).Insert(index322, body);

                                                foreach (DataRow drd in ds.Tables[0].Rows)
                                                {

                                                    //body = drd["PrdName"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["Rate"].ToString();
                                                    prd = prd + "\n";
                                                    prd = prd + drd["ItemCode"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["InOut"].ToString();
                                                    prd = prd + "\n";

                                                }
                                                if (index322 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index322, 8).Insert(index322, prd);
                                                }
                                            }
                                        }

                                        int index312 = emailcontent.IndexOf("@User");
                                        body = usernam;
                                        if (index312 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                        }

                                        int index2 = emailcontent.IndexOf("@FormulaName");
                                        body = FormulaName;
                                        if (index2 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index2, 12).Insert(index2, body);
                                        }

                                        string smtphostname = ConfigurationManager.AppSettings["SmtpHostName"].ToString();
                                        int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPortNumber"]);
                                        var fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

                                        string fromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();

                                        EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, emailcontent, fromPassword);

                                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email sent successfully')", true);

                                    }

                                }
                            }
                        }
                    }

                    string conn = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                    UtilitySMS utilSMS = new UtilitySMS(conn);
                    string UserID = Page.User.Identity.Name;

                    string smscontent = string.Empty;
                    if (hdSMSRequired.Value == "YES")
                    {
                        var toAddress = "";
                        var toAdd = "";
                        Int32 ModeofContact = 0;
                        int ScreenType = 0;

                        DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                        if (dsdd != null)
                        {
                            if (dsdd.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdd.Tables[0].Rows)
                                {
                                    ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                    mobile = Convert.ToBoolean(dr["mobile"]);
                                    smscontent = Convert.ToString(dr["smscontent"]);

                                    if (ScreenType == 1)
                                    {

                                        toAddress = toAdd;

                                    }
                                    else
                                    {
                                        toAddress = dr["mobile"].ToString();
                                    }
                                    if (mobile == true)
                                    {

                                        string body = "\n";

                                        int index123 = smscontent.IndexOf("@Branch");
                                        body = Request.Cookies["Company"].Value;
                                        if (index123 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                        }

                                        int sno = 1;
                                        string prd = string.Empty;
                                        int index322 = smscontent.IndexOf("@Product");
                                        if (ds != null)
                                        {
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                //emailcontent = emailcontent.Remove(index322, 8).Insert(index322, body);

                                                foreach (DataRow drd in ds.Tables[0].Rows)
                                                {

                                                    //body = drd["PrdName"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["Rate"].ToString();
                                                    prd = prd + "\n";
                                                    prd = prd + drd["ItemCode"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["InOut"].ToString();
                                                    prd = prd + "\n";

                                                }
                                                if (index322 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index322, 8).Insert(index322, prd);
                                                }
                                            }
                                        }

                                        int index312 = smscontent.IndexOf("@User");
                                        body = usernam;
                                        if (index312 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                        }

                                        int index2 = smscontent.IndexOf("@FormulaName");
                                        body = FormulaName;
                                        if (index2 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index2, 12).Insert(index2, body);
                                        }

                                        if (Session["Provider"] != null)
                                        {
                                            utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), toAddress, smscontent, true, UserID);
                                        }


                                    }

                                }
                            }
                        }
                    }


                    salesPanel.Visible = false;
                    lnkBtnAdd.Visible = true;
                    PanelBill.Visible = true;
                    PanelCmd.Visible = false;
                    hdMode.Value = "New";
                    //cmdPrint.Enabled = false;
                    //Accordion1.SelectedIndex = 0;
                    deleteFile();
                    BindGrid(string.Empty);
                    GridViewFormula.Visible = true;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Details Saved Successfully. Your Bill No. is " + billNo.ToString() + "')", true);
                    //Session["salesID"] = billNo.ToString();
                    //Session["PurchaseReturn"] = purchaseReturn;
                    //Response.Redirect("PrintSalesBill.aspx");
                    GridViewFormula.Visible = true;
                    //MyAccordion.Visible = true;
                    UpdatePanelFormula.Update();
                    ModalPopupExtender1.Hide();

                    UpdatePanel16.Update();
                    //tabContol.Visible = false;
                }
            }
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
            if (Page.IsValid)
            {
                if (GrdViewItems.EditIndex != -1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Save the product details before Saving Stock Management defination.');", true);
                    return;
                }
                string FormulaName = txtFormulaName.Text.ToUpper();

                BusinessLogic bl = new BusinessLogic(sDataSource);

                string cntB = bl.isDuplicateFormule(FormulaName);
                if (cntB != "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Name Of Product  " + cntB + "  Already Exists.Enter Different Name')", true);
                    return;
                }


                BindItemsGrid();
                DataSet ds = (DataSet)GrdViewItems.DataSource;

                int InCount = 0;
                int OutCount = 0;

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (dr["InOut"].ToString() == "Raw Material")
                                InCount = InCount + 1;
                            else
                                OutCount = OutCount + 1;
                        }
                    }
                }

                if ((InCount == 0) || (OutCount == 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Minimum one Raw Materials and one Products should be added.');", true);
                    return;
                }


                if (ds != null)
                {

                    //BusinessLogic bl = new BusinessLogic(sDataSource);
                    bl.InsertFormulaItem(FormulaName, ds);

                    //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
                    //BusinessLogic bl = new BusinessLogic(sDataSource);
                    //int billNo = bl.InsertSales(sBilldate, sCustomerID, sCustomerName, sCustomerAddress, sCustomerContact, iPaymode, sCreditCardno, iBank, dTotalAmt, purchaseReturn, prReason, Convert.ToInt32(executive), ds);
                    Reset();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product saved Successfully.')", true);
                    //ResetProduct();
                    if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_template.xml")))
                        File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_template.xml"));

                    string usernam = Request.Cookies["LoggedUserName"].Value;

                    string salestype = string.Empty;
                    int ScreenNo = 0;
                    string ScreenName = string.Empty;

                    string connection = Request.Cookies["Company"].Value;

                    salestype = "Create Formula";
                    ScreenName = "Create Formula";
                    

                    bool mobile = false;
                    bool Email = false;
                    string emailsubject = string.Empty;

                    string emailcontent = string.Empty;
                    if (hdEmailRequired.Value == "YES")
                    {
                        var toAddress = "";
                        var toAdd = "";
                        Int32 ModeofContact = 0;
                        int ScreenType = 0;


                        DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                        if (dsdd != null)
                        {
                            if (dsdd.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdd.Tables[0].Rows)
                                {
                                    ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                    mobile = Convert.ToBoolean(dr["mobile"]);
                                    Email = Convert.ToBoolean(dr["Email"]);
                                    emailsubject = Convert.ToString(dr["emailsubject"]);
                                    emailcontent = Convert.ToString(dr["emailcontent"]);

                                    if (ScreenType == 1)
                                    {
                                      
                                            toAddress = toAdd;
                                       
                                    }
                                    else
                                    {
                                        toAddress = dr["EmailId"].ToString();
                                    }
                                    if (Email == true)
                                    {
                                        //string subject = "Added - Customer Receipt in Branch " + Request.Cookies["Company"].Value;

                                        string body = "\n";

                                        int index123 = emailcontent.IndexOf("@Branch");
                                        body = Request.Cookies["Company"].Value;
                                        if (index123 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                        }

                                        int sno = 1;
                                        string prd = string.Empty;
                                        int index322 = emailcontent.IndexOf("@Product");
                                        if (ds != null)
                                        {
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                //emailcontent = emailcontent.Remove(index322, 8).Insert(index322, body);

                                                foreach (DataRow drd in ds.Tables[0].Rows)
                                                {

                                                    //body = drd["PrdName"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["Rate"].ToString();
                                                    prd = prd + "\n";
                                                    prd = prd + drd["ItemCode"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["InOut"].ToString();
                                                    prd = prd + "\n";

                                                }
                                                if (index322 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index322, 8).Insert(index322, prd);
                                                }
                                            }
                                        }

                                        int index312 = emailcontent.IndexOf("@User");
                                        body = usernam;
                                        if (index312 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                        }

                                        int index2 = emailcontent.IndexOf("@FormulaName");
                                        body = FormulaName;
                                        if (index2 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index2, 12).Insert(index2, body);
                                        }

                                        string smtphostname = ConfigurationManager.AppSettings["SmtpHostName"].ToString();
                                        int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPortNumber"]);
                                        var fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

                                        string fromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();

                                        EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, emailcontent, fromPassword);

                                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email sent successfully')", true);

                                    }

                                }
                            }
                        }
                    }

                    string conn = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                    UtilitySMS utilSMS = new UtilitySMS(conn);
                    string UserID = Page.User.Identity.Name;

                    string smscontent = string.Empty;
                    if (hdSMSRequired.Value == "YES")
                    {
                        var toAddress = "";
                        var toAdd = "";
                        Int32 ModeofContact = 0;
                        int ScreenType = 0;

                        DataSet dsdd = bl.GetDetailsForScreenNo(connection, ScreenName, "");
                        if (dsdd != null)
                        {
                            if (dsdd.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdd.Tables[0].Rows)
                                {
                                    ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                    mobile = Convert.ToBoolean(dr["mobile"]);
                                    smscontent = Convert.ToString(dr["smscontent"]);

                                    if (ScreenType == 1)
                                    {
                                        
                                            toAddress = toAdd;
                                        
                                    }
                                    else
                                    {
                                        toAddress = dr["mobile"].ToString();
                                    }
                                    if (mobile == true)
                                    {

                                        string body = "\n";

                                        int index123 = smscontent.IndexOf("@Branch");
                                        body = Request.Cookies["Company"].Value;
                                        if (index123 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                        }

                                        int sno = 1;
                                        string prd = string.Empty;
                                        int index322 = smscontent.IndexOf("@Product");
                                        if (ds != null)
                                        {
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                //emailcontent = emailcontent.Remove(index322, 8).Insert(index322, body);

                                                foreach (DataRow drd in ds.Tables[0].Rows)
                                                {

                                                    //body = drd["PrdName"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["Rate"].ToString();
                                                    prd = prd + "\n";
                                                    prd = prd + drd["ItemCode"].ToString() + "  " + drd["Qty"].ToString() + "  " + drd["InOut"].ToString();
                                                    prd = prd + "\n";

                                                }
                                                if (index322 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index322, 8).Insert(index322, prd);
                                                }
                                            }
                                        }

                                        int index312 = smscontent.IndexOf("@User");
                                        body = usernam;
                                        if (index312 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                        }

                                        int index2 = smscontent.IndexOf("@FormulaName");
                                        body = FormulaName;
                                        if (index2 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index2, 12).Insert(index2, body);
                                        }

                                        if (Session["Provider"] != null)
                                        {
                                            utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), toAddress, smscontent, true, UserID);
                                        }


                                    }

                                }
                            }
                        }
                    }

                    salesPanel.Visible = false;
                    lnkBtnAdd.Visible = true;
                    PanelBill.Visible = true;
                    PanelCmd.Visible = false;
                    hdMode.Value = "New";
                    //cmdPrint.Enabled = false;
                    //Accordion1.SelectedIndex = 0;
                    deleteFile();
                    BindGrid(string.Empty);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sales Details Saved Successfully. Your Bill No. is " + billNo.ToString() + "')", true);
                    //Session["salesID"] = billNo.ToString();
                    //Session["PurchaseReturn"] = purchaseReturn;
                    //Response.Redirect("PrintSalesBill.aspx");
                    GridViewFormula.Visible = true;
                    //MyAccordion.Visible = true;
                    //tabContol.Visible = false;
                    UpdatePanelFormula.Update();
                    ModalPopupExtender1.Hide();
                    UpdatePanel16.Update();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void deleteFile()
    {
        Session["TemplateItems"] = null;

        if (Session["FormulaFilename"] != null)
        {
            string delFilename = Session["FormulaFilename"].ToString();
            if (File.Exists(delFilename))
                File.Delete(delFilename);
        }
    }


    protected void GrdViewItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            /*
            BindItemsGrid();
            DataSet ds = (DataSet)GrdViewItems.DataSource;
            ds.Tables[0].Rows[GrdViewItems.Rows[e.RowIndex].DataItemIndex].Delete();
            ds.WriteXml(Server.MapPath("Reports\\" + hdFilename.Value + "_template.xml"));
            BindItemsGrid();*/
            ModalPopupExtender1.Show();
            if (Session["TemplateItems"] != null)
            {
                DataSet ds = (DataSet)Session["TemplateItems"];
                GridViewRow row = GrdViewItems.Rows[e.RowIndex];
                ds.Tables[0].Rows[GrdViewItems.Rows[e.RowIndex].DataItemIndex].Delete();
                ds.Tables[0].AcceptChanges();
                GrdViewItems.DataSource = ds;
                GrdViewItems.DataBind();
                Session["TemplateItems"] = ds;

                if (GrdViewItems.Rows.Count==0)
                {
                    heading1.Text = "";
                }
            }

      
           // Page_Load(sender, e);

            //if( GrdViewItems.Rows[e.RowIndex])

            //{
            //    heading1.Text = "";
            //}
            
           
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ModalPopupExtender1.Show();
            GrdViewItems.EditIndex = e.NewEditIndex;
            BindItemsGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                ModalPopupExtender1.Show();
                int i;
                i = GrdViewItems.Rows[e.RowIndex].DataItemIndex;

                string iD = GrdViewItems.DataKeys[e.RowIndex].Value.ToString();
                TextBox txtQtyEd = (TextBox)GrdViewItems.Rows[e.RowIndex].FindControl("txtQty");
                Label ddType = (Label)GrdViewItems.Rows[e.RowIndex].FindControl("lblInOut");

                GrdViewItems.EditIndex = -1;
                BindItemsGrid();
                DataSet ds = (DataSet)GrdViewItems.DataSource;

                ds.Tables[0].Rows[i]["Qty"] = txtQtyEd.Text;
                ds.Tables[0].Rows[i]["InOut"] = ddType.Text;

                Session["TemplateItems"] = ds;

                //BusinessLogic bl = new BusinessLogic(sDataSource);
                //bl.UpdateFormulaIteam(iD, txtQtyEd.Text.Trim(), ddType.SelectedValue.Trim());
                //System.Threading.Thread.Sleep(1000);
                //GrdViewItems.EditIndex = -1;
                BindItemsGrid();

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
                PresentationUtils.SetPagerButtonStates(GrdViewItems, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (hdMode.Value == "Edit")
        //{
        //    GrdViewItems.Columns[6].Visible = false;
        //}
    }
    protected void GrdViewItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void GrdViewSales_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GrdViewItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GrdViewItems.EditIndex = -1;
            BindItemsGrid();
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlPageSelector2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void GridViewFormula_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridViewFormula.PageIndex = e.NewPageIndex;
            BindGrid(string.Empty);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void formXml()
    {
        string formName = hdFormula.Value;
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = new DataSet();
        DataSet ds = new DataSet();
        int cnt = 0;
        ds = bl.GetFormulaForID(formName);

        Session["TemplateItems"] = ds;
        /*
        if (ds != null)
        {
            StringWriter sWriter;
            XmlTextWriter reportXMLWriter;
            XmlDocument xmlDoc;
            string filename = string.Empty;
            sWriter = new StringWriter();
            reportXMLWriter = new XmlTextWriter(sWriter);
            reportXMLWriter.Formatting = Formatting.Indented;
            reportXMLWriter.WriteStartDocument();
            reportXMLWriter.WriteStartElement("Formula");

            if (ds.Tables[0].Rows.Count == 0)
            {
                reportXMLWriter.WriteStartElement("FormulaItem");
                reportXMLWriter.WriteElementString("FormulaID", String.Empty);
                reportXMLWriter.WriteElementString("FromulaName", String.Empty);
                reportXMLWriter.WriteElementString("ProductName", String.Empty);
                reportXMLWriter.WriteElementString("ProductDesc", String.Empty);
                reportXMLWriter.WriteElementString("ItemCode", String.Empty);
                reportXMLWriter.WriteElementString("Qty", "0");
                reportXMLWriter.WriteElementString("InOut", String.Empty);

                reportXMLWriter.WriteEndElement();
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    reportXMLWriter.WriteStartElement("FormulaItem");
                    reportXMLWriter.WriteElementString("FormulaID", Convert.ToString(dr["FormulaID"]));
                    reportXMLWriter.WriteElementString("ItemCode", Convert.ToString(dr["ItemCode"]));
                    reportXMLWriter.WriteElementString("FormulaName", Convert.ToString(dr["FormulaName"]));
                    reportXMLWriter.WriteElementString("ProductName", Convert.ToString(dr["ProductName"]));
                    reportXMLWriter.WriteElementString("ProductDesc", Convert.ToString(dr["ProductDesc"]));
                    reportXMLWriter.WriteElementString("Qty", Convert.ToString(dr["Qty"]));
                    reportXMLWriter.WriteElementString("InOut", Convert.ToString(dr["InOut"]));

                    reportXMLWriter.WriteEndElement();
                }
            }
         
            reportXMLWriter.WriteEndElement();
            reportXMLWriter.WriteEndDocument();
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sWriter.ToString());
            filename = hdFilename.Value;
            xmlDoc.Save(Server.MapPath("Reports\\" + filename + "_Formula.xml"));
            Session["FormulaFileName"] = Server.MapPath("Reports\\" + filename + "_Formula.xml");

        }*/

    }

    protected void GridViewFormula_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (txtFormName.Text == "")
            //BindGrid("");
            //else
            //BindGrid(txtFormName.Text);
            heading.Text = "Update Product Specification";
            ModalPopupExtender1.Show();
            string formName = GridViewFormula.SelectedDataKey.Value.ToString();
            hdMode.Value = "Edit";
            hdFormula.Value = formName;
            formXml();

            BindItemsGrid();
            cmdSaveProduct.Visible = false;
            GrdViewItems.Columns[6].Visible = true;
            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //DataSet ds = bl.GetFormulaForID(formName);
            //GrdViewItems.DataSource = ds;
            //GrdViewItems.DataBind();
            txtFormulaName.Text = formName;
            txtFormulaName.Enabled = false;
            PanelCmd.Visible = true;
            salesPanel.Visible = true;
            PanelItems.Visible = true;
            PanelCmd.Visible = true;
            cmdSave.Enabled = false;
            cmdUpdate.Enabled = true;
            lnkBtnAdd.Visible = false;
            GridViewFormula.Visible = false;
            //MyAccordion.Visible = false;
            cmdUpdate.Visible = true;
            cmdSave.Visible = false;
            salesPanel.Visible = false;
            prodPanel.Visible = true;
            Button1.Visible = false;
            //tabContol.Visible = true;
            DisableForOffline();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void DisableForOffline()
    {
        string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        BusinessLogic objChk = new BusinessLogic(sDataSource);

        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            lnkBtnAdd.Visible = false;
            cmdUpdate.Enabled = false;
            cmdSave.Enabled = false;
            GrdViewItems.Columns[5].Visible = false;
        }
    }

    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        try
        {
            deleteFile();
            Reset();
            hdMode.Value = "New";
            lnkBtnAdd.Visible = true;
            salesPanel.Visible = false;
            PanelCmd.Visible = false;
            PanelBill.Visible = true;
            PanelItems.Visible = false;
            GridViewFormula.Visible = true;
            //MyAccordion.Visible = true;
            //tabContol.Visible = false;
            DisableForOffline();
            UpdatePanelFormula.Update();
            ModalPopupExtender1.Hide();
            UpdatePanel16.Update();
            titlehead.Text = "";
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

        txtQtyAdd.Text = "";
        // ddUnit.SelectedValue = "";

        foreach (Control control in cmbProdAdd.Controls)
        {
            if (control is HiddenField)
                ((HiddenField)control).Value = "0";
        }
        cmbProdAdd.ClearSelection();

    }

    private void BindItemsGrid()
    {
        string filename = string.Empty;
        filename = hdFilename.Value;
        DataSet xmlDs = new DataSet();
        //if (File.Exists(Server.MapPath("Reports\\" + filename + "_Formula.xml")))
        if (Session["TemplateItems"] != null)
        {
            //xmlDs.ReadXml(Server.MapPath("Reports\\" + filename + "_Formula.xml"));

            xmlDs = (DataSet)Session["TemplateItems"];

            if (xmlDs != null)
            {
                GrdViewItems.DataSource = xmlDs;
                GrdViewItems.DataBind();
            }
            else
            {
                GrdViewItems.DataSource = null;
                GrdViewItems.DataBind();
            }
        }

        //string formName = GridViewFormula.SelectedDataKey.Value.ToString();
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = bl.GetFormulaForID(formName);
        //GrdViewItems.DataSource = ds;
        //GrdViewItems.DataBind();
    }

    protected void GridViewFormula_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridViewFormula, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GridViewFormula_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "STKMST"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }
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
            GridViewFormula.PageIndex = ((DropDownList)sender).SelectedIndex;
            String strBillno = string.Empty;
            string strTransNo = string.Empty;


            BindGrid(string.Empty);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GrdViewItems.Visible = true;
        try
        {
            if (txtFormulaName.Text == null || txtFormulaName.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Name is Required.It cannot be Left blank.');", true);
                return;
            }
            else
            {
                salesPanel.Visible = true;
                ModalPopupExtender1.Show();
            }

            titlehead.Text = "Select the Component to be Added";
            Button1.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //private void loadEmp()
    //{
    //    BusinessLogic bl = new BusinessLogic(sDataSource);



    //    string connection = Request.Cookies["Company"].Value;

    //    string Username = Request.Cookies["LoggedUserName"].Value;

    //    ds = bl.InsertUnitRecord(connection, Username);

    //    //ds = bl.ListExecutive();
    //    ddUnit.DataSource = ds;
    //    ddUnit.DataBind();
    //    ddUnit.DataTextField = "empFirstName";
    //    ddUnit.DataValueField = "empno";

    //    //drpsIncharge.DataSource = ds;
    //    //drpsIncharge.DataBind();
    //    //drpsIncharge.DataTextField = "empFirstName";
    //    //drpsIncharge.DataValueField = "empno";
    //}


}
