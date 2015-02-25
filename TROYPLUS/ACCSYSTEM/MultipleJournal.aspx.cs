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
using SMSLibrary;

public partial class MultipleJournal : System.Web.UI.Page
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
                cmdSave.Enabled = false;
                cmdUpdate.Enabled = false;
                lnkBtnAdd.Visible = false;
                pnlSearch.Visible = false;
            }

            GrdViewJournal.PageSize = 8;

            if (!IsPostBack)
            {
                lblBillNo.Text = "";
                //BindCurrencyLabels();

                BindGrid();
                //GenerateRoleDs();
                
                loadSupplier();
                //loadProducts();
                //loadBilts("0");
                //loadCategories();

                UpdatePnlMaster.Update();


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "MULJOU"))
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
            ddCriteria.SelectedIndex = 0;
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewJournal_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sDataSource = string.Empty;
            string textt = string.Empty;
            string dropd = string.Empty;

            if (Request.Cookies["Company"] != null)
                sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;


            DataSet ds = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            //ds = bl.ListJournal(txtTransno.Text.Trim(), txtRefno.Text.Trim(), txtLedger.Text.Trim(), txtDate.Text, sDataSource);

            ds = bl.ListJournalDatas(textt, dropd, sDataSource);

            if (ds != null)
            {
                GrdViewJournal.DataSource = ds.Tables[0].DefaultView;
                GrdViewJournal.DataBind();
            }
            else
            {
                GrdViewJournal.EmptyDataText = "No journals found";
                GrdViewJournal.DataSource = null;
                GrdViewJournal.DataBind();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "CURRENCY")
                {
                    currency = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["CurrencyType"] = currency;
                }

                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "BARCODE")
                {
                    BarCodeRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }
       

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Reset();
            updatePnlPurchase.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sDataSource = string.Empty;
            string textt = string.Empty;
            string dropd = string.Empty;

            if (Request.Cookies["Company"] != null)
                sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            textt = txtSearch.Text;
            dropd = ddCriteria.SelectedValue;

            DataSet ds = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            //ds = bl.ListJournal(txtTransno.Text.Trim(), txtRefno.Text.Trim(), txtLedger.Text.Trim(), txtDate.Text, sDataSource);

            ds = bl.ListJournalDatas(textt, dropd, sDataSource);

            if (ds != null)
            {
                GrdViewJournal.DataSource = ds.Tables[0].DefaultView;
                GrdViewJournal.DataBind();
            }
            else
            {
                GrdViewJournal.EmptyDataText = "No journals found";
                GrdViewJournal.DataSource = null;
                GrdViewJournal.DataBind();

            }
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

            Session["PurchaseProductDs"] = null;
            ////pnlRole.Visible = false;
            cmdSave.Enabled = true;
            //MyAccordion.Visible = true;
            cmdUpdate.Enabled = false;

            ModalPopupPurchase.Hide();

            BusinessLogic objChk = new BusinessLogic();
            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                cmdSave.Enabled = false;
                cmdUpdate.Enabled = false;
                lnkBtnAdd.Visible = false;
                pnlSearch.Visible = false;
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

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            string connection = string.Empty;

            if (Page.IsValid)
            {
                string usernam = Request.Cookies["LoggedUserName"].Value;

                //object usernam = Session["LoggedUserName"];

                connection = Request.Cookies["Company"].Value;
                BusinessLogic bll = new BusinessLogic();

                int idebtor1 = 0;
                int idebtor2 = 0;
                int idebtor3 = 0;
                int idebtor4 = 0;
                int idebtor5 = 0;
                int idebtor6 = 0;
                int iCreditor1 = 0;
                int iCreditor2 = 0;
                int iCreditor3 = 0;
                int iCreditor4 = 0;
                int iCreditor5 = 0;
                int iCreditor6 = 0;

                string[] sDate;
                string[] IDate1;
                string[] IDate2;
                string[] IDate4;
                string[] IDate5;
                string[] IDate6;

                string sBillno1;
                string sBillno2;
                string sBillno3;
                string sBillno4;
                string sBillno5;
                string sBillno6;

                string VoucherType = string.Empty;
                string sPath = string.Empty;

                string sNarration1;
                string sNarration2;
                string sNarration3;
                string sNarration4;
                string sNarration5;
                string sNarration6;

                double dTotalAmt1 = 0;
                double dTotalAmt2 = 0;
                double dTotalAmt3 = 0;
                double dTotalAmt4 = 0;
                double dTotalAmt5 = 0;
                double dTotalAmt6 = 0;

                DateTime sBilldate1;
                DateTime sBilldate2;
                DateTime sBilldate3;
                DateTime sBilldate4;
                DateTime sBilldate5;
                DateTime sBilldate6;

                int num = 0;

                int Newtrsns = 0;

                string delim = "/";
                char[] delimA = delim.ToCharArray();
                CultureInfo culture = new CultureInfo("pt-BR");

                //if (txtnum.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter No of Entries');", true);
                //    return;
                //}



                string cDate = string.Empty;
                idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
                sNarration1 = txtNarrAdd1.Text;
                iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
                if (txtAmountAdd1.Text == "")
                {
                    dTotalAmt1 = 0;
                }
                else
                {
                    dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
                }
                sBillno1 = txtRefnumAdd1.Text.Trim();
                cDate = txtTransDateAdd1.Text.Trim();

                if ((idebtor1 == 0) || (sNarration1 == "") || (iCreditor1 == 0) || (dTotalAmt1 == 0) || (sBillno1 == "") || (cDate == ""))
                {
                    if ((idebtor1 == 0) && (sNarration1 == "") && (iCreditor1 == 0) && (dTotalAmt1 == 0) && (sBillno1 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
                        return;
                    }
                }
                else
                {
                    if ((idebtor1 == 0) && (sNarration1 == "") && (iCreditor1 == 0) && (dTotalAmt1 == 0) && (sBillno1 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        num = 1;
                    }
                }


                idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
                cDate = txtTransDateAdd2.Text.Trim();
                iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
                if (txtAmountAdd2.Text == "")
                {
                    dTotalAmt2 = 0;
                }
                else
                {
                    dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
                }
                sBillno2 = txtRefnumAdd2.Text.Trim();
                sNarration2 = txtNarrAdd2.Text;
                cDate = txtTransDateAdd2.Text.Trim();
                if ((idebtor2 == 0) || (sNarration2 == "") || (iCreditor2 == 0) || (dTotalAmt2 == 0) || (sBillno2 == "") || (cDate == ""))
                {
                    if ((idebtor2 == 0) && (sNarration2 == "") && (iCreditor2 == 0) && (dTotalAmt2 == 0) && (sBillno2 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
                        return;
                    }
                }
                else
                {
                    if ((idebtor2 == 0) && (sNarration2 == "") && (iCreditor2 == 0) && (dTotalAmt2 == 0) && (sBillno2 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        num = 2;
                    }
                }


                idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);
                cDate = txtTransDateAdd3.Text.Trim();
                iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);
                if (txtAmountAdd3.Text == "")
                {
                    dTotalAmt3 = 0;
                }
                else
                {
                    dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);
                }
                sBillno3 = txtRefnumAdd3.Text.Trim();
                sNarration3 = txtNarrAdd3.Text;
                cDate = txtTransDateAdd3.Text.Trim();
                if ((idebtor3 == 0) || (sNarration3 == "") || (iCreditor3 == 0) || (dTotalAmt3 == 0) || (sBillno3 == "") || (cDate == ""))
                {
                    if ((idebtor3 == 0) && (sNarration3 == "") && (iCreditor3 == 0) && (dTotalAmt3 == 0) && (sBillno3 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
                        return;
                    }
                }
                else
                {
                    if ((idebtor3 == 0) && (sNarration3 == "") && (iCreditor3 == 0) && (dTotalAmt3 == 0) && (sBillno3 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        num = 3;
                    }
                }


                idebtor4 = Convert.ToInt32(cmbDebtorAdd4.SelectedItem.Value);
                cDate = txtTransDateAdd4.Text.Trim();
                iCreditor4 = Convert.ToInt32(cmbCreditorAdd4.SelectedItem.Value);
                if (txtAmountAdd4.Text == "")
                {
                    dTotalAmt4 = 0;
                }
                else
                {
                    dTotalAmt4 = Convert.ToDouble(txtAmountAdd4.Text);
                }
                sBillno4 = txtRefnumAdd4.Text.Trim();
                sNarration4 = txtNarrAdd4.Text;
                cDate = txtTransDateAdd4.Text.Trim();
                if ((idebtor4 == 0) || (sNarration4 == "") || (iCreditor4 == 0) || (dTotalAmt4 == 0) || (sBillno4 == "") || (cDate == ""))
                {
                    if ((idebtor4 == 0) && (sNarration4 == "") && (iCreditor4 == 0) && (dTotalAmt4 == 0) && (sBillno4 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
                        return;
                    }
                }
                else
                {
                    if ((idebtor4 == 0) && (sNarration4 == "") && (iCreditor4 == 0) && (dTotalAmt4 == 0) && (sBillno4 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        num = 4;
                    }
                }


                idebtor5 = Convert.ToInt32(cmbDebtorAdd5.SelectedItem.Value);
                cDate = txtTransDateAdd5.Text.Trim();
                iCreditor5 = Convert.ToInt32(cmbCreditorAdd5.SelectedItem.Value);
                if (txtAmountAdd5.Text == "")
                {
                    dTotalAmt5 = 0;
                }
                else
                {
                    dTotalAmt5 = Convert.ToDouble(txtAmountAdd5.Text);
                }
                sBillno5 = txtRefnumAdd5.Text.Trim();
                sNarration5 = txtNarrAdd5.Text;
                cDate = txtTransDateAdd5.Text.Trim();
                if ((idebtor5 == 0) || (sNarration5 == "") || (iCreditor5 == 0) || (dTotalAmt5 == 0) || (sBillno5 == "") || (cDate == ""))
                {
                    if ((idebtor5 == 0) && (sNarration5 == "") && (iCreditor5 == 0) && (dTotalAmt5 == 0) && (sBillno5 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
                        return;
                    }
                }
                else
                {
                    if ((idebtor5 == 0) && (sNarration5 == "") && (iCreditor5 == 0) && (dTotalAmt5 == 0) && (sBillno5 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        num = 5;
                    }
                }


                idebtor6 = Convert.ToInt32(cmbDebtorAdd6.SelectedItem.Value);
                cDate = txtTransDateAdd6.Text.Trim();
                iCreditor6 = Convert.ToInt32(cmbCreditorAdd6.SelectedItem.Value);
                if (txtAmountAdd6.Text == "")
                {
                    dTotalAmt6 = 0;
                }
                else
                {
                    dTotalAmt6 = Convert.ToDouble(txtAmountAdd6.Text);
                }
                sBillno6 = txtRefnumAdd6.Text.Trim();
                sNarration6 = txtNarrAdd6.Text;
                cDate = txtTransDateAdd6.Text.Trim();
                if ((idebtor6 == 0) || (sNarration6 == "") || (iCreditor6 == 0) || (dTotalAmt6 == 0) || (sBillno6 == "") || (cDate == ""))
                {
                    if ((idebtor6 == 0) && (sNarration6 == "") && (iCreditor6 == 0) && (dTotalAmt6 == 0) && (sBillno6 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
                        return;
                    }
                }
                else
                {
                    if ((idebtor6 == 0) && (sNarration6 == "") && (iCreditor6 == 0) && (dTotalAmt6 == 0) && (sBillno6 == "") && (cDate != ""))
                    {
                    }
                    else
                    {
                        num = 6;
                    }
                }


                //int num = Convert.ToInt32(txtnum.Text);

                if (num == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Nothing to save');", true);
                }

                if (num == 1)
                {
                    try
                    {
                        sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
                        sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

                        //IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
                        //sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

                        //IDate2 = txtTransDateAdd3.Text.Trim().Split(delimA);
                        //sBilldate3 = new DateTime(Convert.ToInt32(IDate2[2].ToString()), Convert.ToInt32(IDate2[1].ToString()), Convert.ToInt32(IDate2[0].ToString()));

                        //IDate4 = txtTransDateAdd4.Text.Trim().Split(delimA);
                        //sBilldate4 = new DateTime(Convert.ToInt32(IDate4[2].ToString()), Convert.ToInt32(IDate4[1].ToString()), Convert.ToInt32(IDate4[0].ToString()));

                        //IDate5 = txtTransDateAdd5.Text.Trim().Split(delimA);
                        //sBilldate5 = new DateTime(Convert.ToInt32(IDate5[2].ToString()), Convert.ToInt32(IDate5[1].ToString()), Convert.ToInt32(IDate5[0].ToString()));

                        //IDate6 = txtTransDateAdd6.Text.Trim().Split(delimA);
                        //sBilldate6 = new DateTime(Convert.ToInt32(IDate6[2].ToString()), Convert.ToInt32(IDate6[1].ToString()), Convert.ToInt32(IDate6[0].ToString()));
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
                        return;
                    }
                    idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
                    //idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
                    //idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);
                    //idebtor4 = Convert.ToInt32(cmbDebtorAdd4.SelectedItem.Value);
                    //idebtor5 = Convert.ToInt32(cmbDebtorAdd5.SelectedItem.Value);
                    //idebtor6 = Convert.ToInt32(cmbDebtorAdd6.SelectedItem.Value);

                    iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
                    //iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
                    //iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);
                    //iCreditor4 = Convert.ToInt32(cmbCreditorAdd4.SelectedItem.Value);
                    //iCreditor5 = Convert.ToInt32(cmbCreditorAdd5.SelectedItem.Value);
                    //iCreditor6 = Convert.ToInt32(cmbCreditorAdd6.SelectedItem.Value);

                    dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
                    //dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
                    //dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);
                    //dTotalAmt4 = Convert.ToDouble(txtAmountAdd4.Text);
                    //dTotalAmt5 = Convert.ToDouble(txtAmountAdd5.Text);
                    //dTotalAmt6 = Convert.ToDouble(txtAmountAdd6.Text);

                    sBillno1 = txtRefnumAdd1.Text.Trim();
                    //sBillno2 = txtRefnumAdd2.Text.Trim();
                    //sBillno3 = txtRefnumAdd3.Text.Trim();
                    //sBillno4 = txtRefnumAdd4.Text.Trim();
                    //sBillno5 = txtRefnumAdd5.Text.Trim();
                    //sBillno6 = txtRefnumAdd6.Text.Trim();

                    sNarration1 = txtNarrAdd1.Text;
                    //sNarration2 = txtNarrAdd2.Text;
                    //sNarration3 = txtNarrAdd3.Text;
                    //sNarration4 = txtNarrAdd4.Text;
                    //sNarration5 = txtNarrAdd5.Text;
                    //sNarration6 = txtNarrAdd6.Text;

                    VoucherType = "Journal";

                    if (Request.Cookies["Company"] != null)
                        sDataSource = Request.Cookies["Company"].Value;

                    sPath = sDataSource;

                    BusinessLogic bl = new BusinessLogic(sDataSource);


                    string conn = GetConnectionString();
                    bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, conn, usernam);
                    Reset();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);
                }
                else if (num == 2)
                {
                    try
                    {
                        sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
                        sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

                        IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
                        sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

                    }
                    catch (Exception ex)
                    {
                        Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
                        return;
                    }
                    idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
                    idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);


                    iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
                    iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);


                    dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
                    dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);


                    sBillno1 = txtRefnumAdd1.Text.Trim();
                    sBillno2 = txtRefnumAdd2.Text.Trim();


                    sNarration1 = txtNarrAdd1.Text;
                    sNarration2 = txtNarrAdd2.Text;

                    VoucherType = "Journal";

                    if (Request.Cookies["Company"] != null)
                        sDataSource = Request.Cookies["Company"].Value;

                    sPath = sDataSource;

                    BusinessLogic bl = new BusinessLogic(sDataSource);

                    bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno2, sBilldate2, idebtor2, iCreditor2, dTotalAmt2, sNarration2, VoucherType, sPath, usernam);

                    Reset();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);

                }
                else if (num == 3)
                {
                    try
                    {
                        sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
                        sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

                        IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
                        sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

                        IDate2 = txtTransDateAdd3.Text.Trim().Split(delimA);
                        sBilldate3 = new DateTime(Convert.ToInt32(IDate2[2].ToString()), Convert.ToInt32(IDate2[1].ToString()), Convert.ToInt32(IDate2[0].ToString()));

                    }
                    catch (Exception ex)
                    {
                        Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
                        return;
                    }
                    idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
                    idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
                    idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);

                    iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
                    iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
                    iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);

                    dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
                    dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
                    dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);

                    sBillno1 = txtRefnumAdd1.Text.Trim();
                    sBillno2 = txtRefnumAdd2.Text.Trim();
                    sBillno3 = txtRefnumAdd3.Text.Trim();

                    sNarration1 = txtNarrAdd1.Text;
                    sNarration2 = txtNarrAdd2.Text;
                    sNarration3 = txtNarrAdd3.Text;

                    VoucherType = "Journal";

                    if (Request.Cookies["Company"] != null)
                        sDataSource = Request.Cookies["Company"].Value;

                    sPath = sDataSource;

                    BusinessLogic bl = new BusinessLogic(sDataSource);

                    bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno2, sBilldate2, idebtor2, iCreditor2, dTotalAmt2, sNarration2, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno3, sBilldate3, idebtor3, iCreditor3, dTotalAmt3, sNarration3, VoucherType, sPath, usernam);

                    Reset();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);
                }
                else if (num == 4)
                {
                    try
                    {
                        sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
                        sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

                        IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
                        sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

                        IDate2 = txtTransDateAdd3.Text.Trim().Split(delimA);
                        sBilldate3 = new DateTime(Convert.ToInt32(IDate2[2].ToString()), Convert.ToInt32(IDate2[1].ToString()), Convert.ToInt32(IDate2[0].ToString()));

                        IDate4 = txtTransDateAdd4.Text.Trim().Split(delimA);
                        sBilldate4 = new DateTime(Convert.ToInt32(IDate4[2].ToString()), Convert.ToInt32(IDate4[1].ToString()), Convert.ToInt32(IDate4[0].ToString()));

                    }
                    catch (Exception ex)
                    {
                        Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
                        return;
                    }
                    idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
                    idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
                    idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);
                    idebtor4 = Convert.ToInt32(cmbDebtorAdd4.SelectedItem.Value);

                    iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
                    iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
                    iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);
                    iCreditor4 = Convert.ToInt32(cmbCreditorAdd4.SelectedItem.Value);

                    dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
                    dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
                    dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);
                    dTotalAmt4 = Convert.ToDouble(txtAmountAdd4.Text);

                    sBillno1 = txtRefnumAdd1.Text.Trim();
                    sBillno2 = txtRefnumAdd2.Text.Trim();
                    sBillno3 = txtRefnumAdd3.Text.Trim();
                    sBillno4 = txtRefnumAdd4.Text.Trim();

                    sNarration1 = txtNarrAdd1.Text;
                    sNarration2 = txtNarrAdd2.Text;
                    sNarration3 = txtNarrAdd3.Text;
                    sNarration4 = txtNarrAdd4.Text;

                    VoucherType = "Journal";

                    if (Request.Cookies["Company"] != null)
                        sDataSource = Request.Cookies["Company"].Value;

                    sPath = sDataSource;

                    BusinessLogic bl = new BusinessLogic(sDataSource);

                    bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno2, sBilldate2, idebtor2, iCreditor2, dTotalAmt2, sNarration2, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno3, sBilldate3, idebtor3, iCreditor3, dTotalAmt3, sNarration3, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno4, sBilldate4, idebtor4, iCreditor4, dTotalAmt4, sNarration4, VoucherType, sPath, usernam);

                    Reset();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);
                }
                else if (num == 5)
                {
                    try
                    {
                        sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
                        sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

                        IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
                        sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

                        IDate2 = txtTransDateAdd3.Text.Trim().Split(delimA);
                        sBilldate3 = new DateTime(Convert.ToInt32(IDate2[2].ToString()), Convert.ToInt32(IDate2[1].ToString()), Convert.ToInt32(IDate2[0].ToString()));

                        IDate4 = txtTransDateAdd4.Text.Trim().Split(delimA);
                        sBilldate4 = new DateTime(Convert.ToInt32(IDate4[2].ToString()), Convert.ToInt32(IDate4[1].ToString()), Convert.ToInt32(IDate4[0].ToString()));

                        IDate5 = txtTransDateAdd5.Text.Trim().Split(delimA);
                        sBilldate5 = new DateTime(Convert.ToInt32(IDate5[2].ToString()), Convert.ToInt32(IDate5[1].ToString()), Convert.ToInt32(IDate5[0].ToString()));

                    }
                    catch (Exception ex)
                    {
                        Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
                        return;
                    }
                    idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
                    idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
                    idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);
                    idebtor4 = Convert.ToInt32(cmbDebtorAdd4.SelectedItem.Value);
                    idebtor5 = Convert.ToInt32(cmbDebtorAdd5.SelectedItem.Value);

                    iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
                    iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
                    iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);
                    iCreditor4 = Convert.ToInt32(cmbCreditorAdd4.SelectedItem.Value);
                    iCreditor5 = Convert.ToInt32(cmbCreditorAdd5.SelectedItem.Value);

                    dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
                    dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
                    dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);
                    dTotalAmt4 = Convert.ToDouble(txtAmountAdd4.Text);
                    dTotalAmt5 = Convert.ToDouble(txtAmountAdd5.Text);

                    sBillno1 = txtRefnumAdd1.Text.Trim();
                    sBillno2 = txtRefnumAdd2.Text.Trim();
                    sBillno3 = txtRefnumAdd3.Text.Trim();
                    sBillno4 = txtRefnumAdd4.Text.Trim();
                    sBillno5 = txtRefnumAdd5.Text.Trim();

                    sNarration1 = txtNarrAdd1.Text;
                    sNarration2 = txtNarrAdd2.Text;
                    sNarration3 = txtNarrAdd3.Text;
                    sNarration4 = txtNarrAdd4.Text;
                    sNarration5 = txtNarrAdd5.Text;

                    VoucherType = "Journal";

                    if (Request.Cookies["Company"] != null)
                        sDataSource = Request.Cookies["Company"].Value;

                    sPath = sDataSource;

                    BusinessLogic bl = new BusinessLogic(sDataSource);

                    bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno2, sBilldate2, idebtor2, iCreditor2, dTotalAmt2, sNarration2, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno3, sBilldate3, idebtor3, iCreditor3, dTotalAmt3, sNarration3, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno4, sBilldate4, idebtor4, iCreditor4, dTotalAmt4, sNarration4, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno5, sBilldate5, idebtor5, iCreditor5, dTotalAmt5, sNarration5, VoucherType, sPath, usernam);

                    Reset();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);
                }
                else if (num == 6)
                {
                    try
                    {
                        sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
                        sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

                        IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
                        sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

                        IDate2 = txtTransDateAdd3.Text.Trim().Split(delimA);
                        sBilldate3 = new DateTime(Convert.ToInt32(IDate2[2].ToString()), Convert.ToInt32(IDate2[1].ToString()), Convert.ToInt32(IDate2[0].ToString()));

                        IDate4 = txtTransDateAdd4.Text.Trim().Split(delimA);
                        sBilldate4 = new DateTime(Convert.ToInt32(IDate4[2].ToString()), Convert.ToInt32(IDate4[1].ToString()), Convert.ToInt32(IDate4[0].ToString()));

                        IDate5 = txtTransDateAdd5.Text.Trim().Split(delimA);
                        sBilldate5 = new DateTime(Convert.ToInt32(IDate5[2].ToString()), Convert.ToInt32(IDate5[1].ToString()), Convert.ToInt32(IDate5[0].ToString()));

                        IDate6 = txtTransDateAdd6.Text.Trim().Split(delimA);
                        sBilldate6 = new DateTime(Convert.ToInt32(IDate6[2].ToString()), Convert.ToInt32(IDate6[1].ToString()), Convert.ToInt32(IDate6[0].ToString()));
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
                        return;
                    }
                    idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
                    idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
                    idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);
                    idebtor4 = Convert.ToInt32(cmbDebtorAdd4.SelectedItem.Value);
                    idebtor5 = Convert.ToInt32(cmbDebtorAdd5.SelectedItem.Value);
                    idebtor6 = Convert.ToInt32(cmbDebtorAdd6.SelectedItem.Value);

                    iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
                    iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
                    iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);
                    iCreditor4 = Convert.ToInt32(cmbCreditorAdd4.SelectedItem.Value);
                    iCreditor5 = Convert.ToInt32(cmbCreditorAdd5.SelectedItem.Value);
                    iCreditor6 = Convert.ToInt32(cmbCreditorAdd6.SelectedItem.Value);

                    dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
                    dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
                    dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);
                    dTotalAmt4 = Convert.ToDouble(txtAmountAdd4.Text);
                    dTotalAmt5 = Convert.ToDouble(txtAmountAdd5.Text);
                    dTotalAmt6 = Convert.ToDouble(txtAmountAdd6.Text);

                    sBillno1 = txtRefnumAdd1.Text.Trim();
                    sBillno2 = txtRefnumAdd2.Text.Trim();
                    sBillno3 = txtRefnumAdd3.Text.Trim();
                    sBillno4 = txtRefnumAdd4.Text.Trim();
                    sBillno5 = txtRefnumAdd5.Text.Trim();
                    sBillno6 = txtRefnumAdd6.Text.Trim();

                    sNarration1 = txtNarrAdd1.Text;
                    sNarration2 = txtNarrAdd2.Text;
                    sNarration3 = txtNarrAdd3.Text;
                    sNarration4 = txtNarrAdd4.Text;
                    sNarration5 = txtNarrAdd5.Text;
                    sNarration6 = txtNarrAdd6.Text;

                    VoucherType = "Journal";

                    if (Request.Cookies["Company"] != null)
                        sDataSource = Request.Cookies["Company"].Value;

                    sPath = sDataSource;

                    BusinessLogic bl = new BusinessLogic(sDataSource);

                    bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno2, sBilldate2, idebtor2, iCreditor2, dTotalAmt2, sNarration2, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno3, sBilldate3, idebtor3, iCreditor3, dTotalAmt3, sNarration3, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno4, sBilldate4, idebtor4, iCreditor4, dTotalAmt4, sNarration4, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno5, sBilldate5, idebtor5, iCreditor5, dTotalAmt5, sNarration5, VoucherType, sPath, usernam);
                    bl.InsertJournalMultiple(Newtrsns, sBillno6, sBilldate6, idebtor6, iCreditor6, dTotalAmt6, sNarration6, VoucherType, sPath, usernam);

                    Reset();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);
                }
            }
            ModalPopupPurchase.Hide();
            BindGrid();
            GrdViewJournal.DataBind();
            UpdatePnlMaster.Update();
            lblBillNo.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void cmdSave_Click(object sender, EventArgs e)
    //{
    //    string connection = string.Empty;

    //    if (Page.IsValid)
    //    {
    //        object usernam = Session["LoggedUserName"];

    //        connection = Request.Cookies["Company"].Value;
    //        BusinessLogic bll = new BusinessLogic();

    //        int idebtor1 = 0;
    //        int idebtor2 = 0;
    //        int idebtor3 = 0;
    //        int idebtor4 = 0;
    //        int idebtor5 = 0;
    //        int idebtor6 = 0;
    //        int iCreditor1 = 0;
    //        int iCreditor2 = 0;
    //        int iCreditor3 = 0;
    //        int iCreditor4 = 0;
    //        int iCreditor5 = 0;
    //        int iCreditor6 = 0;

    //        string[] sDate;
    //        string[] IDate1;
    //        string[] IDate2;
    //        string[] IDate4;
    //        string[] IDate5;
    //        string[] IDate6;

    //        string sBillno1;
    //        string sBillno2;
    //        string sBillno3;
    //        string sBillno4;
    //        string sBillno5;
    //        string sBillno6;

    //        string VoucherType = string.Empty;
    //        string sPath = string.Empty;

    //        string sNarration1;
    //        string sNarration2;
    //        string sNarration3;
    //        string sNarration4;
    //        string sNarration5;
    //        string sNarration6;

    //        double dTotalAmt1 = 0;
    //        double dTotalAmt2 = 0;
    //        double dTotalAmt3 = 0;
    //        double dTotalAmt4 = 0;
    //        double dTotalAmt5 = 0;
    //        double dTotalAmt6 = 0;

    //        DateTime sBilldate1;
    //        DateTime sBilldate2;
    //        DateTime sBilldate3;
    //        DateTime sBilldate4;
    //        DateTime sBilldate5;
    //        DateTime sBilldate6;

    //        int num = 0;

    //        int Newtrsns = 0;

    //        string delim = "/";
    //        char[] delimA = delim.ToCharArray();
    //        CultureInfo culture = new CultureInfo("pt-BR");

    //        //if (txtnum.Text == "")
    //        //{
    //        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter No of Entries');", true);
    //        //    return;
    //        //}



    //        string cDate = string.Empty;
    //        idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
    //        sNarration1 = txtNarrAdd1.Text;
    //        iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
    //        if (txtAmountAdd1.Text == "")
    //        {
    //            dTotalAmt1 = 0;
    //        }
    //        else
    //        {
    //            dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
    //        }
    //        sBillno1 = txtRefnumAdd1.Text.Trim();
    //        cDate = txtTransDateAdd1.Text.Trim();

    //        if ((idebtor1 == 0) || (sNarration1 == "") || (iCreditor1 == 0) || (dTotalAmt1 == 0) || (sBillno1 == "") || (cDate == ""))
    //        {
    //            if ((idebtor1 == 0) && (sNarration1 == "") && (iCreditor1 == 0) && (dTotalAmt1 == 0) && (sBillno1 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            if ((idebtor1 == 0) && (sNarration1 == "") && (iCreditor1 == 0) && (dTotalAmt1 == 0) && (sBillno1 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                num = 1;
    //            }
    //        }


    //        idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
    //        cDate = txtTransDateAdd2.Text.Trim();
    //        iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
    //        if (txtAmountAdd2.Text == "")
    //        {
    //            dTotalAmt2 = 0;
    //        }
    //        else
    //        {
    //            dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
    //        }
    //        sBillno2 = txtRefnumAdd2.Text.Trim();
    //        sNarration2 = txtNarrAdd2.Text;
    //        cDate = txtTransDateAdd2.Text.Trim();
    //        if ((idebtor2 == 0) || (sNarration2 == "") || (iCreditor2 == 0) || (dTotalAmt2 == 0) || (sBillno2 == "") || (cDate == ""))
    //        {
    //            if ((idebtor2 == 0) && (sNarration2 == "") && (iCreditor2 == 0) && (dTotalAmt2 == 0) && (sBillno2 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            if ((idebtor2 == 0) && (sNarration2 == "") && (iCreditor2 == 0) && (dTotalAmt2 == 0) && (sBillno2 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                num = 2;
    //            }
    //        }


    //        idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);
    //        cDate = txtTransDateAdd3.Text.Trim();
    //        iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);
    //        if (txtAmountAdd3.Text == "")
    //        {
    //            dTotalAmt3 = 0;
    //        }
    //        else
    //        {
    //            dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);
    //        }
    //        sBillno3 = txtRefnumAdd3.Text.Trim();
    //        sNarration3 = txtNarrAdd3.Text;
    //        cDate = txtTransDateAdd3.Text.Trim();
    //        if ((idebtor3 == 0) || (sNarration3 == "") || (iCreditor3 == 0) || (dTotalAmt3 == 0) || (sBillno3 == "") || (cDate == ""))
    //        {
    //            if ((idebtor3 == 0) && (sNarration3 == "") && (iCreditor3 == 0) && (dTotalAmt3 == 0) && (sBillno3 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            if ((idebtor3 == 0) && (sNarration3 == "") && (iCreditor3 == 0) && (dTotalAmt3 == 0) && (sBillno3 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                num = 3;
    //            }
    //        }


    //        idebtor4 = Convert.ToInt32(cmbDebtorAdd4.SelectedItem.Value);
    //        cDate = txtTransDateAdd4.Text.Trim();
    //        iCreditor4 = Convert.ToInt32(cmbCreditorAdd4.SelectedItem.Value);
    //        if (txtAmountAdd4.Text == "")
    //        {
    //            dTotalAmt4 = 0;
    //        }
    //        else
    //        {
    //            dTotalAmt4 = Convert.ToDouble(txtAmountAdd4.Text);
    //        }
    //        sBillno4 = txtRefnumAdd4.Text.Trim();
    //        sNarration4 = txtNarrAdd4.Text;
    //        cDate = txtTransDateAdd4.Text.Trim();
    //        if ((idebtor4 == 0) || (sNarration4 == "") || (iCreditor4 == 0) || (dTotalAmt4 == 0) || (sBillno4 == "") || (cDate == ""))
    //        {
    //            if ((idebtor4 == 0) && (sNarration4 == "") && (iCreditor4 == 0) && (dTotalAmt4 == 0) && (sBillno4 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            if ((idebtor4 == 0) && (sNarration4 == "") && (iCreditor4 == 0) && (dTotalAmt4 == 0) && (sBillno4 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                num = 4;
    //            }
    //        }


    //        idebtor5 = Convert.ToInt32(cmbDebtorAdd5.SelectedItem.Value);
    //        cDate = txtTransDateAdd5.Text.Trim();
    //        iCreditor5 = Convert.ToInt32(cmbCreditorAdd5.SelectedItem.Value);
    //        if (txtAmountAdd5.Text == "")
    //        {
    //            dTotalAmt5 = 0;
    //        }
    //        else
    //        {
    //            dTotalAmt5 = Convert.ToDouble(txtAmountAdd5.Text);
    //        }
    //        sBillno5 = txtRefnumAdd5.Text.Trim();
    //        sNarration5 = txtNarrAdd5.Text;
    //        cDate = txtTransDateAdd5.Text.Trim();
    //        if ((idebtor5 == 0) || (sNarration5 == "") || (iCreditor5 == 0) || (dTotalAmt5 == 0) || (sBillno5 == "") || (cDate == ""))
    //        {
    //            if ((idebtor5 == 0) && (sNarration5 == "") && (iCreditor5 == 0) && (dTotalAmt5 == 0) && (sBillno5 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            if ((idebtor5 == 0) && (sNarration5 == "") && (iCreditor5 == 0) && (dTotalAmt5 == 0) && (sBillno5 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                num = 5;
    //            }
    //        }


    //        idebtor6 = Convert.ToInt32(cmbDebtorAdd6.SelectedItem.Value);
    //        cDate = txtTransDateAdd6.Text.Trim();
    //        iCreditor6 = Convert.ToInt32(cmbCreditorAdd6.SelectedItem.Value);
    //        if (txtAmountAdd6.Text == "")
    //        {
    //            dTotalAmt6 = 0;
    //        }
    //        else
    //        {
    //            dTotalAmt6 = Convert.ToDouble(txtAmountAdd6.Text);
    //        }
    //        sBillno6 = txtRefnumAdd6.Text.Trim();
    //        sNarration6 = txtNarrAdd6.Text;
    //        cDate = txtTransDateAdd6.Text.Trim();
    //        if ((idebtor6 == 0) || (sNarration6 == "") || (iCreditor6 == 0) || (dTotalAmt6 == 0) || (sBillno6 == "") || (cDate == ""))
    //        {
    //            if ((idebtor6 == 0) && (sNarration6 == "") && (iCreditor6 == 0) && (dTotalAmt6 == 0) && (sBillno6 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Partially filled');", true);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            if ((idebtor6 == 0) && (sNarration6 == "") && (iCreditor6 == 0) && (dTotalAmt6 == 0) && (sBillno6 == "") && (cDate == ""))
    //            {
    //            }
    //            else
    //            {
    //                num = 6;
    //            }
    //        }


    //        //int num = Convert.ToInt32(txtnum.Text);

    //        if (num == 0)
    //        {
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Nothing to save');", true);
    //        }

    //        if (num == 1)
    //        {
    //            try
    //            {
    //                sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
    //                sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

    //                //IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
    //                //sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

    //                //IDate2 = txtTransDateAdd3.Text.Trim().Split(delimA);
    //                //sBilldate3 = new DateTime(Convert.ToInt32(IDate2[2].ToString()), Convert.ToInt32(IDate2[1].ToString()), Convert.ToInt32(IDate2[0].ToString()));

    //                //IDate4 = txtTransDateAdd4.Text.Trim().Split(delimA);
    //                //sBilldate4 = new DateTime(Convert.ToInt32(IDate4[2].ToString()), Convert.ToInt32(IDate4[1].ToString()), Convert.ToInt32(IDate4[0].ToString()));

    //                //IDate5 = txtTransDateAdd5.Text.Trim().Split(delimA);
    //                //sBilldate5 = new DateTime(Convert.ToInt32(IDate5[2].ToString()), Convert.ToInt32(IDate5[1].ToString()), Convert.ToInt32(IDate5[0].ToString()));

    //                //IDate6 = txtTransDateAdd6.Text.Trim().Split(delimA);
    //                //sBilldate6 = new DateTime(Convert.ToInt32(IDate6[2].ToString()), Convert.ToInt32(IDate6[1].ToString()), Convert.ToInt32(IDate6[0].ToString()));
    //            }
    //            catch (Exception ex)
    //            {
    //                Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
    //                return;
    //            }
    //            idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
    //            //idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
    //            //idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);
    //            //idebtor4 = Convert.ToInt32(cmbDebtorAdd4.SelectedItem.Value);
    //            //idebtor5 = Convert.ToInt32(cmbDebtorAdd5.SelectedItem.Value);
    //            //idebtor6 = Convert.ToInt32(cmbDebtorAdd6.SelectedItem.Value);

    //            iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
    //            //iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
    //            //iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);
    //            //iCreditor4 = Convert.ToInt32(cmbCreditorAdd4.SelectedItem.Value);
    //            //iCreditor5 = Convert.ToInt32(cmbCreditorAdd5.SelectedItem.Value);
    //            //iCreditor6 = Convert.ToInt32(cmbCreditorAdd6.SelectedItem.Value);

    //            dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
    //            //dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
    //            //dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);
    //            //dTotalAmt4 = Convert.ToDouble(txtAmountAdd4.Text);
    //            //dTotalAmt5 = Convert.ToDouble(txtAmountAdd5.Text);
    //            //dTotalAmt6 = Convert.ToDouble(txtAmountAdd6.Text);

    //            sBillno1 = txtRefnumAdd1.Text.Trim();
    //            //sBillno2 = txtRefnumAdd2.Text.Trim();
    //            //sBillno3 = txtRefnumAdd3.Text.Trim();
    //            //sBillno4 = txtRefnumAdd4.Text.Trim();
    //            //sBillno5 = txtRefnumAdd5.Text.Trim();
    //            //sBillno6 = txtRefnumAdd6.Text.Trim();

    //            sNarration1 = txtNarrAdd1.Text;
    //            //sNarration2 = txtNarrAdd2.Text;
    //            //sNarration3 = txtNarrAdd3.Text;
    //            //sNarration4 = txtNarrAdd4.Text;
    //            //sNarration5 = txtNarrAdd5.Text;
    //            //sNarration6 = txtNarrAdd6.Text;

    //            VoucherType = "Journal";

    //            if (Request.Cookies["Company"] != null)
    //                sDataSource = Request.Cookies["Company"].Value;

    //            sPath = sDataSource;

    //            BusinessLogic bl = new BusinessLogic(sDataSource);



    //            bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
    //            Reset();
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);
    //        }
    //        else if (num == 2)
    //        {
    //            try
    //            {
    //                sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
    //                sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

    //                IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
    //                sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

    //            }
    //            catch (Exception ex)
    //            {
    //                Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
    //                return;
    //            }
    //            idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
    //            idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);


    //            iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
    //            iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);


    //            dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
    //            dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);


    //            sBillno1 = txtRefnumAdd1.Text.Trim();
    //            sBillno2 = txtRefnumAdd2.Text.Trim();


    //            sNarration1 = txtNarrAdd1.Text;
    //            sNarration2 = txtNarrAdd2.Text;

    //            VoucherType = "Journal";

    //            if (Request.Cookies["Company"] != null)
    //                sDataSource = Request.Cookies["Company"].Value;

    //            sPath = sDataSource;

    //            BusinessLogic bl = new BusinessLogic(sDataSource);

    //            bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno2, sBilldate2, idebtor2, iCreditor2, dTotalAmt2, sNarration2, VoucherType, sPath, usernam);

    //            Reset();
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);

    //        }
    //        else if (num == 3)
    //        {
    //            try
    //            {
    //                sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
    //                sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

    //                IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
    //                sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

    //                IDate2 = txtTransDateAdd3.Text.Trim().Split(delimA);
    //                sBilldate3 = new DateTime(Convert.ToInt32(IDate2[2].ToString()), Convert.ToInt32(IDate2[1].ToString()), Convert.ToInt32(IDate2[0].ToString()));

    //            }
    //            catch (Exception ex)
    //            {
    //                Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
    //                return;
    //            }
    //            idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
    //            idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
    //            idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);

    //            iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
    //            iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
    //            iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);

    //            dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
    //            dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
    //            dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);

    //            sBillno1 = txtRefnumAdd1.Text.Trim();
    //            sBillno2 = txtRefnumAdd2.Text.Trim();
    //            sBillno3 = txtRefnumAdd3.Text.Trim();

    //            sNarration1 = txtNarrAdd1.Text;
    //            sNarration2 = txtNarrAdd2.Text;
    //            sNarration3 = txtNarrAdd3.Text;

    //            VoucherType = "Journal";

    //            if (Request.Cookies["Company"] != null)
    //                sDataSource = Request.Cookies["Company"].Value;

    //            sPath = sDataSource;

    //            BusinessLogic bl = new BusinessLogic(sDataSource);

    //            bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno2, sBilldate2, idebtor2, iCreditor2, dTotalAmt2, sNarration2, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno3, sBilldate3, idebtor3, iCreditor3, dTotalAmt3, sNarration3, VoucherType, sPath, usernam);

    //            Reset();
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);
    //        }
    //        else if (num == 4)
    //        {
    //            try
    //            {
    //                sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
    //                sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

    //                IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
    //                sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

    //                IDate2 = txtTransDateAdd3.Text.Trim().Split(delimA);
    //                sBilldate3 = new DateTime(Convert.ToInt32(IDate2[2].ToString()), Convert.ToInt32(IDate2[1].ToString()), Convert.ToInt32(IDate2[0].ToString()));

    //                IDate4 = txtTransDateAdd4.Text.Trim().Split(delimA);
    //                sBilldate4 = new DateTime(Convert.ToInt32(IDate4[2].ToString()), Convert.ToInt32(IDate4[1].ToString()), Convert.ToInt32(IDate4[0].ToString()));

    //            }
    //            catch (Exception ex)
    //            {
    //                Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
    //                return;
    //            }
    //            idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
    //            idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
    //            idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);
    //            idebtor4 = Convert.ToInt32(cmbDebtorAdd4.SelectedItem.Value);

    //            iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
    //            iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
    //            iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);
    //            iCreditor4 = Convert.ToInt32(cmbCreditorAdd4.SelectedItem.Value);

    //            dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
    //            dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
    //            dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);
    //            dTotalAmt4 = Convert.ToDouble(txtAmountAdd4.Text);

    //            sBillno1 = txtRefnumAdd1.Text.Trim();
    //            sBillno2 = txtRefnumAdd2.Text.Trim();
    //            sBillno3 = txtRefnumAdd3.Text.Trim();
    //            sBillno4 = txtRefnumAdd4.Text.Trim();

    //            sNarration1 = txtNarrAdd1.Text;
    //            sNarration2 = txtNarrAdd2.Text;
    //            sNarration3 = txtNarrAdd3.Text;
    //            sNarration4 = txtNarrAdd4.Text;

    //            VoucherType = "Journal";

    //            if (Request.Cookies["Company"] != null)
    //                sDataSource = Request.Cookies["Company"].Value;

    //            sPath = sDataSource;

    //            BusinessLogic bl = new BusinessLogic(sDataSource);

    //            bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno2, sBilldate2, idebtor2, iCreditor2, dTotalAmt2, sNarration2, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno3, sBilldate3, idebtor3, iCreditor3, dTotalAmt3, sNarration3, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno4, sBilldate4, idebtor4, iCreditor4, dTotalAmt4, sNarration4, VoucherType, sPath, usernam);

    //            Reset();
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);
    //        }
    //        else if (num == 5)
    //        {
    //            try
    //            {
    //                sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
    //                sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

    //                IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
    //                sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

    //                IDate2 = txtTransDateAdd3.Text.Trim().Split(delimA);
    //                sBilldate3 = new DateTime(Convert.ToInt32(IDate2[2].ToString()), Convert.ToInt32(IDate2[1].ToString()), Convert.ToInt32(IDate2[0].ToString()));

    //                IDate4 = txtTransDateAdd4.Text.Trim().Split(delimA);
    //                sBilldate4 = new DateTime(Convert.ToInt32(IDate4[2].ToString()), Convert.ToInt32(IDate4[1].ToString()), Convert.ToInt32(IDate4[0].ToString()));

    //                IDate5 = txtTransDateAdd5.Text.Trim().Split(delimA);
    //                sBilldate5 = new DateTime(Convert.ToInt32(IDate5[2].ToString()), Convert.ToInt32(IDate5[1].ToString()), Convert.ToInt32(IDate5[0].ToString()));

    //            }
    //            catch (Exception ex)
    //            {
    //                Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
    //                return;
    //            }
    //            idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
    //            idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
    //            idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);
    //            idebtor4 = Convert.ToInt32(cmbDebtorAdd4.SelectedItem.Value);
    //            idebtor5 = Convert.ToInt32(cmbDebtorAdd5.SelectedItem.Value);

    //            iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
    //            iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
    //            iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);
    //            iCreditor4 = Convert.ToInt32(cmbCreditorAdd4.SelectedItem.Value);
    //            iCreditor5 = Convert.ToInt32(cmbCreditorAdd5.SelectedItem.Value);

    //            dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
    //            dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
    //            dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);
    //            dTotalAmt4 = Convert.ToDouble(txtAmountAdd4.Text);
    //            dTotalAmt5 = Convert.ToDouble(txtAmountAdd5.Text);

    //            sBillno1 = txtRefnumAdd1.Text.Trim();
    //            sBillno2 = txtRefnumAdd2.Text.Trim();
    //            sBillno3 = txtRefnumAdd3.Text.Trim();
    //            sBillno4 = txtRefnumAdd4.Text.Trim();
    //            sBillno5 = txtRefnumAdd5.Text.Trim();

    //            sNarration1 = txtNarrAdd1.Text;
    //            sNarration2 = txtNarrAdd2.Text;
    //            sNarration3 = txtNarrAdd3.Text;
    //            sNarration4 = txtNarrAdd4.Text;
    //            sNarration5 = txtNarrAdd5.Text;

    //            VoucherType = "Journal";

    //            if (Request.Cookies["Company"] != null)
    //                sDataSource = Request.Cookies["Company"].Value;

    //            sPath = sDataSource;

    //            BusinessLogic bl = new BusinessLogic(sDataSource);

    //            bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno2, sBilldate2, idebtor2, iCreditor2, dTotalAmt2, sNarration2, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno3, sBilldate3, idebtor3, iCreditor3, dTotalAmt3, sNarration3, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno4, sBilldate4, idebtor4, iCreditor4, dTotalAmt4, sNarration4, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno5, sBilldate5, idebtor5, iCreditor5, dTotalAmt5, sNarration5, VoucherType, sPath, usernam);

    //            Reset();
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);
    //        }
    //        else if (num == 6)
    //        {
    //            try
    //            {
    //                sDate = txtTransDateAdd1.Text.Trim().Split(delimA);
    //                sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

    //                IDate1 = txtTransDateAdd2.Text.Trim().Split(delimA);
    //                sBilldate2 = new DateTime(Convert.ToInt32(IDate1[2].ToString()), Convert.ToInt32(IDate1[1].ToString()), Convert.ToInt32(IDate1[0].ToString()));

    //                IDate2 = txtTransDateAdd3.Text.Trim().Split(delimA);
    //                sBilldate3 = new DateTime(Convert.ToInt32(IDate2[2].ToString()), Convert.ToInt32(IDate2[1].ToString()), Convert.ToInt32(IDate2[0].ToString()));

    //                IDate4 = txtTransDateAdd4.Text.Trim().Split(delimA);
    //                sBilldate4 = new DateTime(Convert.ToInt32(IDate4[2].ToString()), Convert.ToInt32(IDate4[1].ToString()), Convert.ToInt32(IDate4[0].ToString()));

    //                IDate5 = txtTransDateAdd5.Text.Trim().Split(delimA);
    //                sBilldate5 = new DateTime(Convert.ToInt32(IDate5[2].ToString()), Convert.ToInt32(IDate5[1].ToString()), Convert.ToInt32(IDate5[0].ToString()));

    //                IDate6 = txtTransDateAdd6.Text.Trim().Split(delimA);
    //                sBilldate6 = new DateTime(Convert.ToInt32(IDate6[2].ToString()), Convert.ToInt32(IDate6[1].ToString()), Convert.ToInt32(IDate6[0].ToString()));
    //            }
    //            catch (Exception ex)
    //            {
    //                Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
    //                return;
    //            }
    //            idebtor1 = Convert.ToInt32(cmbDebtorAdd1.SelectedItem.Value);
    //            idebtor2 = Convert.ToInt32(cmbDebtorAdd2.SelectedItem.Value);
    //            idebtor3 = Convert.ToInt32(cmbDebtorAdd3.SelectedItem.Value);
    //            idebtor4 = Convert.ToInt32(cmbDebtorAdd4.SelectedItem.Value);
    //            idebtor5 = Convert.ToInt32(cmbDebtorAdd5.SelectedItem.Value);
    //            idebtor6 = Convert.ToInt32(cmbDebtorAdd6.SelectedItem.Value);

    //            iCreditor1 = Convert.ToInt32(cmbCreditorAdd1.SelectedItem.Value);
    //            iCreditor2 = Convert.ToInt32(cmbCreditorAdd2.SelectedItem.Value);
    //            iCreditor3 = Convert.ToInt32(cmbCreditorAdd3.SelectedItem.Value);
    //            iCreditor4 = Convert.ToInt32(cmbCreditorAdd4.SelectedItem.Value);
    //            iCreditor5 = Convert.ToInt32(cmbCreditorAdd5.SelectedItem.Value);
    //            iCreditor6 = Convert.ToInt32(cmbCreditorAdd6.SelectedItem.Value);

    //            dTotalAmt1 = Convert.ToDouble(txtAmountAdd1.Text);
    //            dTotalAmt2 = Convert.ToDouble(txtAmountAdd2.Text);
    //            dTotalAmt3 = Convert.ToDouble(txtAmountAdd3.Text);
    //            dTotalAmt4 = Convert.ToDouble(txtAmountAdd4.Text);
    //            dTotalAmt5 = Convert.ToDouble(txtAmountAdd5.Text);
    //            dTotalAmt6 = Convert.ToDouble(txtAmountAdd6.Text);

    //            sBillno1 = txtRefnumAdd1.Text.Trim();
    //            sBillno2 = txtRefnumAdd2.Text.Trim();
    //            sBillno3 = txtRefnumAdd3.Text.Trim();
    //            sBillno4 = txtRefnumAdd4.Text.Trim();
    //            sBillno5 = txtRefnumAdd5.Text.Trim();
    //            sBillno6 = txtRefnumAdd6.Text.Trim();

    //            sNarration1 = txtNarrAdd1.Text;
    //            sNarration2 = txtNarrAdd2.Text;
    //            sNarration3 = txtNarrAdd3.Text;
    //            sNarration4 = txtNarrAdd4.Text;
    //            sNarration5 = txtNarrAdd5.Text;
    //            sNarration6 = txtNarrAdd6.Text;

    //            VoucherType = "Journal";

    //            if (Request.Cookies["Company"] != null)
    //                sDataSource = Request.Cookies["Company"].Value;

    //            sPath = sDataSource;

    //            BusinessLogic bl = new BusinessLogic(sDataSource);

    //            bl.InsertJournalMultiple(Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno2, sBilldate2, idebtor2, iCreditor2, dTotalAmt2, sNarration2, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno3, sBilldate3, idebtor3, iCreditor3, dTotalAmt3, sNarration3, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno4, sBilldate4, idebtor4, iCreditor4, dTotalAmt4, sNarration4, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno5, sBilldate5, idebtor5, iCreditor5, dTotalAmt5, sNarration5, VoucherType, sPath, usernam);
    //            bl.InsertJournalMultiple(Newtrsns, sBillno6, sBilldate6, idebtor6, iCreditor6, dTotalAmt6, sNarration6, VoucherType, sPath, usernam);

    //            Reset();
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);



    //        }
    //    }
    //    ModalPopupPurchase.Hide();
    //    BindGrid();
    //    GrdViewJournal.DataBind();
    //    UpdatePnlMaster.Update();
    //}

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        //if (Page.IsValid)
        //{
        //    int iPurchaseId = 0;
        //    string connection = Request.Cookies["Company"].Value;
        //    BusinessLogic bll = new BusinessLogic();
        //    //string recondate = txtBillDate.Text.Trim();
        //    string salesReturn = string.Empty;
        //    string intTrans = string.Empty;
        //    string deliveryNote = string.Empty;
        //    string srReason = string.Empty;
        //    //salesReturn = drpSalesReturn.SelectedItem.Text;
        //    //intTrans = drpIntTrans.SelectedValue;
        //    //deliveryNote = ddDeliveryNote.SelectedValue;
        //    //srReason = txtSRReason.Text.Trim();

        //    if (Session["PurchaseProductDs"] == null)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select the products before save')", true);
        //        return;
        //    }

        //    //if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
        //    //{

        //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
        //    //    return;
        //    //}

        //    //if (Convert.ToDouble(txtfixedtotal.Text) != Convert.ToDouble(lblNet.Text))
        //    //{
        //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Grand Total not match with Fixed Total')", true);
        //    //    return;
        //    //}

        //    int cnt = 0;

        //    if (intTrans == "YES")
        //        cnt = cnt + 1;
        //    if (deliveryNote == "YES")
        //        cnt = cnt + 1;
        //    if (salesReturn == "YES")
        //        cnt = cnt + 1;

        //    if (cnt > 1)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Either one of Sales Return or Delivery Note or Internal Transfer should be selected')", true);
        //        //tabs2.ActiveTabIndex = 1;
        //        //updatePnlSales.Update();
        //        return;
        //    }

        //    string sBillno = string.Empty;
        //    string sInvoiceno = string.Empty;


        //    int iSupplier = 0;
        //    int iPaymode = 0;
        //    string sChequeno = string.Empty;
        //    int iBank = 0;
        //    int iPurchase = 0;
        //    string filename = string.Empty;
        //    double dTotalAmt = 0;

        //    //iPurchase = Convert.ToInt32(hdPurchase.Value);
        //    //sBillno = txtBillno.Text.Trim();
        //    //sInvoiceno = txtInvoiveNo.Text.Trim();

        //    DateTime sBilldate;
        //    DateTime sInvoicedate;

        //    string[] sDate;
        //    string[] IDate;

        //    string delim = "/";
        //    char[] delimA = delim.ToCharArray();
        //    CultureInfo culture = new CultureInfo("pt-BR");
        //    //iPaymode = Convert.ToInt32(cmdPaymode.SelectedItem.Value);

        //    //if (iPaymode == 2)
        //    //{
        //    //    sChequeno = Convert.ToString(txtChequeNo.Text);
        //    //    iBank = Convert.ToInt32(cmbBankName.SelectedItem.Value);
        //    //    rvbank.Enabled = true;
        //    //    rvCheque.Enabled = true;
        //    //}
        //    //else
        //    //{
        //    //    rvbank.Enabled = false;
        //    //    rvCheque.Enabled = false;
        //    //}

        //    Page.Validate("purchaseval");

        //    if (!Page.IsValid)
        //    {
        //        StringBuilder msg = new StringBuilder();

        //        foreach (IValidator validator in Page.Validators)
        //        {
        //            if (!validator.IsValid)
        //            {
        //                msg.Append(" - " + validator.ErrorMessage);
        //                msg.Append("\\n");
        //            }
        //        }

        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg.ToString() + "');", true);
        //        return;
        //    }

        //    try
        //    {
        //        //sDate = txtBillDate.Text.Trim().Split(delimA);
        //        //sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

        //        //IDate = txtInvoiveDate.Text.Trim().Split(delimA);
        //        //sInvoicedate = new DateTime(Convert.ToInt32(IDate[2].ToString()), Convert.ToInt32(IDate[1].ToString()), Convert.ToInt32(IDate[0].ToString()));
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
        //        return;
        //    }
        //    //iSupplier = Convert.ToInt32(cmbSupplier.SelectedItem.Value);

        //    //dTotalAmt = Convert.ToDouble(lblTotalSum.Text);

        //    ///*Start Purchase Loading / Unloading Freight Change - March 16*/
        //    //double dFreight = 0;
        //    //double dLU = 0;
        //    ///*March18*/
        //    //if (txtFreight.Text.Trim() != "")
        //    //    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
        //    //if (txtLU.Text.Trim() != "")
        //    //    dLU = Convert.ToDouble(txtLU.Text.Trim());
        //    ///*March18*/
        //    //dTotalAmt = dTotalAmt + dFreight + dLU;
        //    ///*End Purchase Loading / Unloading Freight Change - March 16*/
        //    //int BilitID = int.Parse(ddBilts.SelectedValue);

        //    //filename = hdFilename.Value;
        //    ////BindProduct();
        //    if (Session["PurchaseProductDs"] != null)
        //    {
        //        DataSet ds = (DataSet)Session["PurchaseProductDs"];

        //        if (ds != null)
        //        {
        //            /*March 18*/
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                /*March 18*/
        //                //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

        //                BusinessLogic bl = new BusinessLogic(sDataSource);
        //                /*Start Purchase Loading / Unloading Freight Change - March 16*/
        //                /*Start InvoiceNo, InvoiceDate*/
        //                //iPurchaseId = bl.UpdatePurchase(iPurchase, sBillno, sBilldate, iSupplier, iPaymode, sChequeno, iBank, dTotalAmt, salesReturn, srReason, dFreight, dLU, BilitID, intTrans, ds, deliveryNote, sInvoiceno, sInvoicedate);
        //                /*End Purchase Loading / Unloading Freight Change - March 16*/
        //                /*Start March 15 Modification */
        //                if (iPurchaseId == -2)
        //                {
        //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase edit is not allowed for this transaction.')", true);
        //                    /*Start Purchase Stock Negative Change - March 16 -- (Commented the below method) */
        //                    //Reset();
        //                    //ResetProduct();
        //                    /*End Purchase Stock Negative Change - March 16*/
        //                    return;

        //                }
        //                /*End March 15 Modification */
        //                Reset();
                        

        //                //purchasePanel.Visible = false;
        //                lnkBtnAdd.Visible = true;
        //                pnlSearch.Visible = true;
        //                //PanelBill.Visible = false;
        //                PanelCmd.Visible = false;
        //                //hdMode.Value = "Edit";
        //                //cmdPrint.Enabled = false;
        //                BindGrid();
        //                /*March 18*/
        //            }
        //            /*March 18*/
        //            else
        //            {
        //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No products is choosed for the bill')", true);
        //            }
        //            /*March 18*/
        //        }
        //        delFlag.Value = "0";
        //        deleteFile();
        //        //Accordion1.SelectedIndex = 0;
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Updated Successfully. Updated Bill No. is " + iPurchaseId.ToString() + "')", true);
        //        Session["purchaseID"] = iPurchaseId.ToString();
        //        deleteFile();
        //        btnCancel.Enabled = false;
        //        Session["SalesReturn"] = salesReturn;
        //        Session["PurchaseProductDs"] = null;
        //        Response.Redirect("ProductPurchaseBill.aspx?SID=" + iPurchaseId.ToString() + "&RT=" + salesReturn.ToUpper());

        //    }
        //}
    }



    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string connection = Request.Cookies["Company"].Value;
                BusinessLogic bll = new BusinessLogic();
                //string recondate = txtBillDate.Text.Trim(); ;
                //if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
                //{

                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                //    return;
                //}
                int iPurchase = 0;
                //string sBillNo = txtBillno.Text.Trim();
                //iPurchase = Convert.ToInt32(hdPurchase.Value);
                //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
                BusinessLogic bl = new BusinessLogic(sDataSource);
                //int del = bl.DeletePurchase(iPurchase, sBillNo);
                /*Start Purchase Stock Negative Change - March 16*/
                //if (del == -2)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase edit is not allowed for this transaction.')", true);
                //    return;
                //}
                /*End Purchase Stock Negative Change - March 16*/
                Reset();

                /*Start Purchase Stock Negative Change - March 16 -- (Commented the below method)*/
                //if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                //    File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
                /*Start Purchase Stock Negative Change - March 16 -- (Commented the below method)*/
                //purchasePanel.Visible = false;
                lnkBtnAdd.Visible = true;
                pnlSearch.Visible = true;
                //PanelBill.Visible = false;
                PanelCmd.Visible = false;
                //hdMode.Value = "Delete";
                //cmdPrint.Enabled = false;
                delFlag.Value = "0";
                deleteFile();
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Purchase Details Deleted Successfully.  Bill No. is " + sBillNo.ToString() + "')", true);
                BindGrid();
                btnCancel.Enabled = false;
                Session["PurchaseProductDs"] = null;

                PanelCmd.Visible = false;
                //purchasePanel.Visible = false;
                lnkBtnAdd.Visible = true;
                pnlSearch.Visible = true;
            }
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
            int TransNo = Convert.ToInt32(GrdViewJournal.DataKeys[e.RowIndex].Value);

            string sDataSource = string.Empty;

            if (Request.Cookies["Company"] != null)
                sDataSource = Request.Cookies["Company"].Value;
            else
                Response.Redirect("~/frm_Login.aspx");
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl1 = new BusinessLogic();
            string recondate = GrdViewJournal.Rows[e.RowIndex].Cells[2].Text; ;
            if (!bl1.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                //frmViewAdd.Visible = true;
                //frmViewAdd.ChangeMode(FormViewMode.ReadOnly);
                return;

            }

            string Username = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.DeleteJournal(TransNo, sDataSource, Username);
            BindGrid();

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;
            
            string usernam = Request.Cookies["LoggedUserName"].Value;

            salestype = "Journal";
            ScreenName = "Journal";
            int DebitorID = 0;
            string TransDate = string.Empty;
            double Amount = 0;
            string Narration = string.Empty;
            int CreditorID = 0;

            DataSet ds = bl.GetJournalForId((int)(GrdViewJournal.DataKeys[e.RowIndex].Value),sDataSource);
            if (ds != null)
            {
                TransDate = Convert.ToString(ds.Tables[0].Rows[0]["TransDate"].ToString());
                DebitorID = Convert.ToInt32(ds.Tables[0].Rows[0]["DebtorID"]);
                CreditorID = Convert.ToInt32(ds.Tables[0].Rows[0]["CreditorID"]);
                Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"]);
                Narration = ds.Tables[0].Rows[0]["Narration"].ToString();
            }

            bool mobile = false;
            bool Email = false;
            string emailsubject = string.Empty;

            string emailcontent = string.Empty;
            if (hdEmailRequired.Value == "YES")
            {
                DataSet dsd = bl.GetLedgerInfoForId(connection, DebitorID);
                var toAddress = "";
                var toAdd = "";
                Int32 ModeofContact = 0;
                int ScreenType = 0;

                if (dsd != null)
                {
                    if (dsd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsd.Tables[0].Rows)
                        {
                            toAdd = dr["EmailId"].ToString();
                            ModeofContact = Convert.ToInt32(dr["ModeofContact"]);
                        }
                    }
                }


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
                                if (dr["Name1"].ToString() == "Sales Executive")
                                {
                                    toAddress = toAdd;
                                }
                                else if ((dr["Name1"].ToString() == "Supplier") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Expense") || (dr["Name1"].ToString() == "Bank"))
                                {
                                    if (ModeofContact == 2)
                                    {
                                        toAddress = toAdd;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    toAddress = toAdd;
                                }
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
                                emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);

                                //int index132 = emailcontent.IndexOf("@Narration");
                                //body = Narration;
                                //emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);

                                int index312 = emailcontent.IndexOf("@User");
                                body = usernam;
                                emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);

                                int index2 = emailcontent.IndexOf("@Date");
                                body = TransDate.ToString();
                                emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);

                                int index221 = emailcontent.IndexOf("@Narration");
                                body = Narration;
                                emailcontent = emailcontent.Remove(index221, 10).Insert(index221, body);

                                //int index = emailcontent.IndexOf("@Supplier");
                                //body = ddReceivedFrom.SelectedItem.Text;
                                //emailcontent = emailcontent.Remove(index, 9).Insert(index, body);

                                int index1 = emailcontent.IndexOf("@Amount");
                                body = Convert.ToString(Amount);
                                emailcontent = emailcontent.Remove(index1, 7).Insert(index1, body);

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
                DataSet dsd = bl.GetLedgerInfoForId(connection, DebitorID);
                var toAddress = "";
                var toAdd = "";
                Int32 ModeofContact = 0;
                int ScreenType = 0;

                if (dsd != null)
                {
                    if (dsd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsd.Tables[0].Rows)
                        {
                            toAdd = dr["Mobile"].ToString();
                            ModeofContact = Convert.ToInt32(dr["ModeofContact"]);
                        }
                    }
                }


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
                                if (dr["Name1"].ToString() == "Sales Executive")
                                {
                                    toAddress = toAdd;
                                }
                                else if (dr["Name1"].ToString() == "Supplier")
                                {
                                    if (ModeofContact == 1)
                                    {
                                        toAddress = toAdd;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    toAddress = toAdd;
                                }
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
                                smscontent = smscontent.Remove(index123, 7).Insert(index123, body);

                                //int index132 = emailcontent.IndexOf("@Narration");
                                //body = Narration;
                                //emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);

                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                smscontent = smscontent.Remove(index312, 5).Insert(index312, body);

                                int index2 = smscontent.IndexOf("@Date");
                                body = TransDate.ToString();
                                smscontent = smscontent.Remove(index2, 5).Insert(index2, body);

                                int index221 = smscontent.IndexOf("@Narration");
                                body = Narration;
                                smscontent = smscontent.Remove(index221, 10).Insert(index221, body);

                                //int index = emailcontent.IndexOf("@Supplier");
                                //body = ddReceivedFrom.SelectedItem.Text;
                                //emailcontent = emailcontent.Remove(index, 9).Insert(index, body);

                                int index1 = smscontent.IndexOf("@Amount");
                                body = Convert.ToString(Amount);
                                smscontent = smscontent.Remove(index1, 7).Insert(index1, body);

                                if (Session["Provider"] != null)
                                {
                                    utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), toAddress, smscontent, true, UserID);
                                }


                            }

                        }
                    }
                }

            }


        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdCancelMethod_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupMethod.Hide();
            lblBillNo.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdMethod_Click(object sender, EventArgs e)
    {
        try
        {
            if (optionmethod.SelectedValue == "Single")
            {
                txtRefnum.Text = "";
                //txtTransDate.Text = DateTime.Now.ToShortDateString();

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtTransDate.Text = dtaa;

                cmbDebtor.SelectedIndex = 0;
                cmbCreditor.SelectedIndex = 0;
                txtAmount.Text = "";
                txtNarr.Text = "";
                lblBillNo.Text = "Add";
                ModalPopupMethod.Show();
                ModalPopupPurchase.Show();
                ModalPopupContact.Show();
                //ModalPopupExtender1.Hide();

                //cmbDebtorAdd2.Enabled = false;
            }
            else if (optionmethod.SelectedValue == "Multiple")
            {
                FirstGridViewRow();
                lblBillNo.Text = "";
                Session["Show"] = "No";
                //if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
                //    return;
                //}

                Session["PurchaseProductDs"] = null;

                cmdSave.Enabled = true;
                cmdSave.Visible = true;

                Reset();
                cmdUpdate.Visible = false;

                DateTime indianStd1 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa1 = Convert.ToDateTime(indianStd1).ToString("dd/MM/yyyy");
                txtTransDateAdd1.Text = dtaa1;

                DateTime indianStd2 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa2 = Convert.ToDateTime(indianStd2).ToString("dd/MM/yyyy");
                txtTransDateAdd2.Text = dtaa2;

                DateTime indianStd3 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa3 = Convert.ToDateTime(indianStd3).ToString("dd/MM/yyyy");
                txtTransDateAdd3.Text = dtaa3;

                DateTime indianStd4 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa4 = Convert.ToDateTime(indianStd4).ToString("dd/MM/yyyy");
                txtTransDateAdd4.Text = dtaa4;

                DateTime indianStd5 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa5 = Convert.ToDateTime(indianStd5).ToString("dd/MM/yyyy");
                txtTransDateAdd5.Text = dtaa5;

                DateTime indianStd6 = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa6 = Convert.ToDateTime(indianStd6).ToString("dd/MM/yyyy");
                txtTransDateAdd6.Text = dtaa6;

                //txtTransDateAdd1.Text = DateTime.Now.ToShortDateString();
                //txtTransDateAdd2.Text = DateTime.Now.ToShortDateString();
                //txtTransDateAdd3.Text = DateTime.Now.ToShortDateString();
                //txtTransDateAdd4.Text = DateTime.Now.ToShortDateString();
                //txtTransDateAdd5.Text = DateTime.Now.ToShortDateString();
                //txtTransDateAdd6.Text = DateTime.Now.ToShortDateString();

                XmlDocument xDoc = new XmlDocument();

                //if (File.Exists(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml")))
                //{
                //    File.Delete(Server.MapPath("Reports\\" + hdFilename.Value + "_Product.xml"));
                //}
                //btnCancel.Enabled = true;

                updatePnlPurchase.Update();
                ModalPopupMethod.Show();
                ModalPopupPurchase.Show();
                ModalPopupExtender1.Hide();
                ModalPopupExtender3.Show();
            }
            else if (optionmethod.SelectedValue == "DebitContra")
            {
                FirstGridViewRow1();

                //updatePnlPurchase.Update();
                ModalPopupMethod.Show();
                ModalPopupPurchase.Show();
                ModalPopupExtender1.Show();

                //if(txtEntries.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Entries number.')", true);
                //    return;
                //}
                drpDebtor.SelectedIndex = 0;

                //int Entries = Convert.ToInt32(txtEntries.Text);

                //DataSet ds = new DataSet();
                //DataTable dt;
                //DataRow drNew;
                //dt = new DataTable();
                //DataColumn dc;
                //int ii = 1;
                //string dtaa1 = string.Empty;

                //dc = new DataColumn("RefNo");
                //dt.Columns.Add(dc);

                //dc = new DataColumn("Date");
                //dt.Columns.Add(dc);

                ////dc = new DataColumn("Creditor");
                ////dt.Columns.Add(dc);

                //dc = new DataColumn("Amount");
                //dt.Columns.Add(dc);
                
                //dc = new DataColumn("Narration");
                //dt.Columns.Add(dc);

                //for (int i = 0; i < Convert.ToInt32(txtEntries.Text); i++)
                //{

                //    DataRow dr_final1312 = dt.NewRow();
                //    dr_final1312["RefNo"] = "";

                //    DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                //    string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                //    dr_final1312["Date"] = dtaa;

                //    dr_final1312["Amount"] = "";

                //    //dr_final1312["Creditor"] = 0;
                //    dr_final1312["Narration"] = "";

                //    dt.Rows.Add(dr_final1312);
                //}

                //ds.Tables.Add(dt);

                //GrdViewItems.DataSource = ds;
                //GrdViewItems.DataBind();
            }
            else if (optionmethod.SelectedValue == "CreditContra")
            {
                FirstGridViewRow2();

                //updatePnlPurchase.Update();
                ModalPopupMethod.Show();
                ModalPopupPurchase.Show();
                ModalPopupExtender2.Show();

                //if (txtEntries.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Entries number.')", true);
                //    return;
                //}
                drpCreditor1.SelectedIndex = 0;

                //int Entries = Convert.ToInt32(txtEntries.Text);

                //DataSet ds = new DataSet();
                //DataTable dt;
                //DataRow drNew;
                //dt = new DataTable();
                //DataColumn dc;
                //int ii = 1;
                //string dtaa1 = string.Empty;

                //dc = new DataColumn("RefNo");
                //dt.Columns.Add(dc);

                //dc = new DataColumn("Date");
                //dt.Columns.Add(dc);

                ////dc = new DataColumn("Creditor");
                ////dt.Columns.Add(dc);

                //dc = new DataColumn("Amount");
                //dt.Columns.Add(dc);

                //dc = new DataColumn("Narration");
                //dt.Columns.Add(dc);

                //for (int i = 0; i < Convert.ToInt32(txtEntries.Text); i++)
                //{

                //    DataRow dr_final1312 = dt.NewRow();
                //    dr_final1312["RefNo"] = "";

                //    DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                //    string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                //    dr_final1312["Date"] = dtaa;

                //    dr_final1312["Amount"] = "";

                //    //dr_final1312["Creditor"] = 0;
                //    dr_final1312["Narration"] = "";

                //    dt.Rows.Add(dr_final1312);
                //}

                //ds.Tables.Add(dt);

                //BulkEditGridView1.DataSource = ds;
                //BulkEditGridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void AddNewRow()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {

                    DropDownList DrpDebtor =
                     (DropDownList)gdm.Rows[rowIndex].Cells[1].FindControl("drpDebtorM");
                    DropDownList DrpCreditor =
                     (DropDownList)gdm.Rows[rowIndex].Cells[1].FindControl("drpCreditorM");
                    TextBox TextBoxRefNo =
                      (TextBox)gdm.Rows[rowIndex].Cells[2].FindControl("txtRefNoM");
                    TextBox TextBoxDate =
                      (TextBox)gdm.Rows[rowIndex].Cells[3].FindControl("txtDateM");
                    TextBox TextBoxAmount =
                      (TextBox)gdm.Rows[rowIndex].Cells[4].FindControl("txtAmountM");
                    TextBox TextBoxNarration =
                     (TextBox)gdm.Rows[rowIndex].Cells[5].FindControl("txtNarrationM");                           

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    
                    dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxRefNo.Text;
                    dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxDate.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = DrpDebtor.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = DrpCreditor.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col5"] = TextBoxAmount.Text;
                    dtCurrentTable.Rows[i - 1]["Col6"] = TextBoxNarration.Text;
                    
                    
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                gdm.DataSource = dtCurrentTable;
                gdm.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }

    private void AddNewRow1()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {

                    DropDownList DrpCreditor =
                     (DropDownList)GrdViewItems.Rows[rowIndex].Cells[1].FindControl("drpCreditor");
                    TextBox TextBoxRefNo =
                      (TextBox)GrdViewItems.Rows[rowIndex].Cells[2].FindControl("txtRefNo");
                    TextBox TextBoxDate =
                      (TextBox)GrdViewItems.Rows[rowIndex].Cells[3].FindControl("txtDate");
                    TextBox TextBoxAmount =
                      (TextBox)GrdViewItems.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                    TextBox TextBoxNarration =
                     (TextBox)GrdViewItems.Rows[rowIndex].Cells[4].FindControl("txtNarration");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxRefNo.Text;
                    dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxDate.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = DrpCreditor.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = TextBoxAmount.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = TextBoxNarration.Text;


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable1"] = dtCurrentTable;

                GrdViewItems.DataSource = dtCurrentTable;
                GrdViewItems.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData1();
    }

    private void AddNewRow2()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {

                    DropDownList DrpDebtor =
                     (DropDownList)BulkEditGridView1.Rows[rowIndex].Cells[1].FindControl("drpDebtor1");
                    TextBox TextBoxRefNo =
                      (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[2].FindControl("txtRefNo");
                    TextBox TextBoxDate =
                      (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[3].FindControl("txtDate");
                    TextBox TextBoxAmount =
                      (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                    TextBox TextBoxNarration =
                     (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[4].FindControl("txtNarration");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxRefNo.Text;
                    dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxDate.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = DrpDebtor.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = TextBoxAmount.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = TextBoxNarration.Text;


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable2"] = dtCurrentTable;

                BulkEditGridView1.DataSource = dtCurrentTable;
                BulkEditGridView1.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData2();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList DrpDebtor =
                     (DropDownList)gdm.Rows[rowIndex].Cells[1].FindControl("drpDebtorM");
                    DropDownList DrpCreditor =
                     (DropDownList)gdm.Rows[rowIndex].Cells[1].FindControl("drpCreditorM");
                    TextBox TextBoxRefNo =
                      (TextBox)gdm.Rows[rowIndex].Cells[2].FindControl("txtRefNoM");
                    TextBox TextBoxDate =
                      (TextBox)gdm.Rows[rowIndex].Cells[3].FindControl("txtDateM");
                    TextBox TextBoxAmount =
                      (TextBox)gdm.Rows[rowIndex].Cells[4].FindControl("txtAmountM");
                    TextBox TextBoxNarration =
                     (TextBox)gdm.Rows[rowIndex].Cells[5].FindControl("txtNarrationM");


                    TextBoxRefNo.Text = dt.Rows[i]["Col1"].ToString();
                    TextBoxDate.Text = dt.Rows[i]["Col2"].ToString();
                    DrpDebtor.SelectedValue = dt.Rows[i]["Col3"].ToString();
                    DrpCreditor.SelectedValue = dt.Rows[i]["Col4"].ToString();
                    TextBoxAmount.Text = dt.Rows[i]["Col5"].ToString();
                    TextBoxNarration.Text = dt.Rows[i]["Col6"].ToString();
                   
                    rowIndex++;

                }
            }
        }
    }

    private void SetPreviousData2()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable2"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList DrpDebtor =
                     (DropDownList)BulkEditGridView1.Rows[rowIndex].Cells[1].FindControl("drpDebtor1");
                    TextBox TextBoxRefNo =
                      (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[2].FindControl("txtRefNo");
                    TextBox TextBoxDate =
                      (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[3].FindControl("txtDate");
                    TextBox TextBoxAmount =
                      (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                    TextBox TextBoxNarration =
                     (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[4].FindControl("txtNarration");


                    TextBoxRefNo.Text = dt.Rows[i]["Col1"].ToString();
                    TextBoxDate.Text = dt.Rows[i]["Col2"].ToString();
                    DrpDebtor.SelectedValue = dt.Rows[i]["Col3"].ToString();
                    TextBoxAmount.Text = dt.Rows[i]["Col4"].ToString();
                    TextBoxNarration.Text = dt.Rows[i]["Col5"].ToString();

                    rowIndex++;

                }
            }
        }
    }

    private void SetPreviousData1()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList DrpCreditor =
                     (DropDownList)GrdViewItems.Rows[rowIndex].Cells[1].FindControl("drpCreditor");
                    TextBox TextBoxRefNo =
                      (TextBox)GrdViewItems.Rows[rowIndex].Cells[2].FindControl("txtRefNo");
                    TextBox TextBoxDate =
                      (TextBox)GrdViewItems.Rows[rowIndex].Cells[3].FindControl("txtDate");
                    TextBox TextBoxAmount =
                      (TextBox)GrdViewItems.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                    TextBox TextBoxNarration =
                     (TextBox)GrdViewItems.Rows[rowIndex].Cells[4].FindControl("txtNarration");


                    TextBoxRefNo.Text = dt.Rows[i]["Col1"].ToString();
                    TextBoxDate.Text = dt.Rows[i]["Col2"].ToString();
                    DrpCreditor.SelectedValue = dt.Rows[i]["Col3"].ToString();
                    TextBoxAmount.Text = dt.Rows[i]["Col4"].ToString();
                    TextBoxNarration.Text = dt.Rows[i]["Col5"].ToString();

                    rowIndex++;

                }
            }
        }
    }

    protected void gdm_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                gdm.DataSource = dt;
                gdm.DataBind();

                for (int i = 0; i < gdm.Rows.Count - 1; i++)
                {
                    gdm.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData();
            }
        }
    }

    protected void BulkEditGridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData2();
        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable2"] = dt;
                BulkEditGridView1.DataSource = dt;
                BulkEditGridView1.DataBind();

                for (int i = 0; i < BulkEditGridView1.Rows.Count - 1; i++)
                {
                    BulkEditGridView1.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData2();
            }
        }
    }

    protected void GrdViewItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData1();
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable1"] = dt;
                GrdViewItems.DataSource = dt;
                GrdViewItems.DataBind();

                for (int i = 0; i < GrdViewItems.Rows.Count - 1; i++)
                {
                    GrdViewItems.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData1();
            }
        }
    }

    private void SetRowData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList DrpDebtor =
                     (DropDownList)gdm.Rows[rowIndex].Cells[1].FindControl("drpDebtorM");
                    DropDownList DrpCreditor =
                     (DropDownList)gdm.Rows[rowIndex].Cells[1].FindControl("drpCreditorM");
                    TextBox TextBoxRefNo =
                      (TextBox)gdm.Rows[rowIndex].Cells[2].FindControl("txtRefNoM");
                    TextBox TextBoxDate =
                      (TextBox)gdm.Rows[rowIndex].Cells[3].FindControl("txtDateM");
                    TextBox TextBoxAmount =
                      (TextBox)gdm.Rows[rowIndex].Cells[4].FindControl("txtAmountM");
                    TextBox TextBoxNarration =
                     (TextBox)gdm.Rows[rowIndex].Cells[5].FindControl("txtNarrationM");
                 

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxRefNo.Text;
                    dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxDate.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = DrpDebtor.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = DrpCreditor.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col5"] = TextBoxAmount.Text;
                    dtCurrentTable.Rows[i - 1]["Col6"] = TextBoxNarration.Text;
                    
                    rowIndex++;

                }

                ViewState["CurrentTable"] = dtCurrentTable;
                gdm.DataSource = dtCurrentTable;
                gdm.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }

    private void SetRowData2()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList DrpDebtor =
                     (DropDownList)BulkEditGridView1.Rows[rowIndex].Cells[1].FindControl("drpDebtor1");
                    TextBox TextBoxRefNo =
                      (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[2].FindControl("txtRefNo");
                    TextBox TextBoxDate =
                      (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[3].FindControl("txtDate");
                    TextBox TextBoxAmount =
                      (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                    TextBox TextBoxNarration =
                     (TextBox)BulkEditGridView1.Rows[rowIndex].Cells[4].FindControl("txtNarration");


                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxRefNo.Text;
                    dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxDate.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = DrpDebtor.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = TextBoxAmount.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = TextBoxNarration.Text;

                    rowIndex++;

                }

                ViewState["CurrentTable2"] = dtCurrentTable;
                BulkEditGridView1.DataSource = dtCurrentTable;
                BulkEditGridView1.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData2();
    }


    private void SetRowData1()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    
                    DropDownList DrpCreditor =
                     (DropDownList)GrdViewItems.Rows[rowIndex].Cells[1].FindControl("drpCreditor");
                    TextBox TextBoxRefNo =
                      (TextBox)GrdViewItems.Rows[rowIndex].Cells[2].FindControl("txtRefNo");
                    TextBox TextBoxDate =
                      (TextBox)GrdViewItems.Rows[rowIndex].Cells[3].FindControl("txtDate");
                    TextBox TextBoxAmount =
                      (TextBox)GrdViewItems.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                    TextBox TextBoxNarration =
                     (TextBox)GrdViewItems.Rows[rowIndex].Cells[4].FindControl("txtNarration");


                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxRefNo.Text;
                    dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxDate.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = DrpCreditor.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = TextBoxAmount.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = TextBoxNarration.Text;

                    rowIndex++;

                }

                ViewState["CurrentTable1"] = dtCurrentTable;
                GrdViewItems.DataSource = dtCurrentTable;
                GrdViewItems.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData1();
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }

    protected void ButtonAdd1_Click(object sender, EventArgs e)
    {
        AddNewRow1();
    }

    protected void ButtonAdd2_Click(object sender, EventArgs e)
    {
        AddNewRow2();
    }

    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
        dt.Columns.Add(new DataColumn("Col3", typeof(string)));
        dt.Columns.Add(new DataColumn("Col4", typeof(string)));
        dt.Columns.Add(new DataColumn("Col5", typeof(string)));
        dt.Columns.Add(new DataColumn("Col6", typeof(string)));
        dt.Columns.Add(new DataColumn("Col7", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        dr["Col3"] = string.Empty;
        dr["Col4"] = string.Empty;
        dr["Col5"] = string.Empty;
        dr["Col6"] = string.Empty;
        dr["Col7"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;


        gdm.DataSource = dt;
        gdm.DataBind();
    }

    private void FirstGridViewRow1()
    {
        DataTable dtt = new DataTable();
        DataRow dr = null;
        dtt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dtt.Columns.Add(new DataColumn("Col1", typeof(string)));
        dtt.Columns.Add(new DataColumn("Col2", typeof(string)));
        dtt.Columns.Add(new DataColumn("Col3", typeof(string)));
        dtt.Columns.Add(new DataColumn("Col4", typeof(string)));
        dtt.Columns.Add(new DataColumn("Col5", typeof(string)));
        dtt.Columns.Add(new DataColumn("Col6", typeof(string)));
        dr = dtt.NewRow();
        dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        dr["Col3"] = string.Empty;
        dr["Col4"] = string.Empty;
        dr["Col5"] = string.Empty;
        dr["Col6"] = string.Empty;
        dtt.Rows.Add(dr);

        ViewState["CurrentTable1"] = dtt;


        GrdViewItems.DataSource = dtt;
        GrdViewItems.DataBind();
    }

    private void FirstGridViewRow2()
    {
        DataTable dttt = new DataTable();
        DataRow dr = null;
        dttt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dttt.Columns.Add(new DataColumn("Col1", typeof(string)));
        dttt.Columns.Add(new DataColumn("Col2", typeof(string)));
        dttt.Columns.Add(new DataColumn("Col3", typeof(string)));
        dttt.Columns.Add(new DataColumn("Col4", typeof(string)));
        dttt.Columns.Add(new DataColumn("Col5", typeof(string)));
        dttt.Columns.Add(new DataColumn("Col6", typeof(string)));
        dr = dttt.NewRow();
        dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        dr["Col3"] = string.Empty;
        dr["Col4"] = string.Empty;
        dr["Col5"] = string.Empty;
        dr["Col6"] = string.Empty;
        dttt.Rows.Add(dr);

        ViewState["CurrentTable2"] = dttt;


        BulkEditGridView1.DataSource = dttt;
        BulkEditGridView1.DataBind();
    }


    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            txtEntries.Text = "";
            ModalPopupMethod.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void Reset()
    {
        txtRefnumAdd1.Text = "";
        txtNarrAdd1.Text = "";
        txtAmountAdd1.Text = "";
        txtAmountAdd2.Text = "";
        txtAmountAdd3.Text = "";
        txtAmountAdd4.Text = "";
        txtAmountAdd5.Text = "";
        txtAmountAdd6.Text = "";
        txtNarrAdd2.Text = "";
        txtNarrAdd3.Text = "";
        txtNarrAdd4.Text = "";
        txtNarrAdd5.Text = "";
        txtNarrAdd6.Text = "";
        txtRefnumAdd2.Text = "";
        txtRefnumAdd3.Text = "";
        txtRefnumAdd4.Text = "";
        txtRefnumAdd5.Text = "";
        txtRefnumAdd6.Text = "";
        cmbDebtorAdd1.SelectedIndex = 0;
        cmbDebtorAdd2.SelectedIndex = 0;
        cmbDebtorAdd3.SelectedIndex = 0;
        cmbDebtorAdd4.SelectedIndex = 0;
        cmbDebtorAdd5.SelectedIndex = 0;
        cmbDebtorAdd6.SelectedIndex = 0;
        cmbCreditorAdd1.SelectedIndex = 0;
        cmbCreditorAdd2.SelectedIndex = 0;
        cmbCreditorAdd3.SelectedIndex = 0;
        cmbCreditorAdd4.SelectedIndex = 0;
        cmbCreditorAdd5.SelectedIndex = 0;
        cmbCreditorAdd6.SelectedIndex = 0;
        txtnum.Text ="";
        txtTransDateAdd1.Text ="";
        txtTransDateAdd2.Text = "";
        txtTransDateAdd3.Text = "";
        txtTransDateAdd4.Text = "";
        txtTransDateAdd5.Text = "";
        txtTransDateAdd6.Text = "";
    }

    private void loadSupplier()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListCreditorDebitorJ(sDataSource);

        cmbDebtorAdd1.Items.Clear();
        ListItem li = new ListItem("Select Ledger", "0");
        li.Attributes.Add("style", "color:Black");
        cmbDebtorAdd1.Items.Add(li);
        cmbDebtorAdd1.DataSource = ds;
        cmbDebtorAdd1.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbDebtorAdd1.DataBind();
        cmbDebtorAdd1.DataTextField = "LedgerName";
        cmbDebtorAdd1.DataValueField = "LedgerID";



        //grvStudentDetails.FindControl("drpPrd").Items.Clear();
        //ListItem lit = new ListItem("Select Ledger", "0");
        //lit.Attributes.Add("style", "color:Black");
        //grvStudentDetails.FindControl("drpPrd").Items.Add(lit);
        //grvStudentDetails.FindControl("drpPrd").DataSource = ds;
        //grvStudentDetails.FindControl("drpPrd").Items[0].Attributes.Add("background-color", "color:#bce1fe");
        //grvStudentDetails.FindControl("drpPrd").DataBind();
        //grvStudentDetails.FindControl("drpPrd").DataTextField = "LedgerName";
        //grvStudentDetails.FindControl("drpPrd").DataValueField = "LedgerID";

        cmbCreditorAdd1.Items.Clear();
        ListItem lif = new ListItem("Select Ledger", "0");
        lif.Attributes.Add("style", "color:Black");
        cmbCreditorAdd1.Items.Add(lif);
        cmbCreditorAdd1.DataSource = ds;
        cmbCreditorAdd1.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbCreditorAdd1.DataBind();
        cmbCreditorAdd1.DataTextField = "LedgerName";
        cmbCreditorAdd1.DataValueField = "LedgerID";

        cmbDebtorAdd2.Items.Clear();
        ListItem lifff = new ListItem("Select Ledger", "0");
        lifff.Attributes.Add("style", "color:Black");
        cmbDebtorAdd2.Items.Add(lifff);
        cmbDebtorAdd2.DataSource = ds;
        cmbDebtorAdd2.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbDebtorAdd2.DataBind();
        cmbDebtorAdd2.DataTextField = "LedgerName";
        cmbDebtorAdd2.DataValueField = "LedgerID";

        cmbCreditorAdd2.Items.Clear();
        ListItem lifww = new ListItem("Select Ledger", "0");
        lifww.Attributes.Add("style", "color:Black");
        cmbCreditorAdd2.Items.Add(lifww);
        cmbCreditorAdd2.DataSource = ds;
        cmbCreditorAdd2.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbCreditorAdd2.DataBind();
        cmbCreditorAdd2.DataTextField = "LedgerName";
        cmbCreditorAdd2.DataValueField = "LedgerID";

        cmbDebtorAdd3.Items.Clear();
        ListItem liw = new ListItem("Select Ledger", "0");
        liw.Attributes.Add("style", "color:Black");
        cmbDebtorAdd3.Items.Add(liw);
        cmbDebtorAdd3.DataSource = ds;
        cmbDebtorAdd3.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbDebtorAdd3.DataBind();
        cmbDebtorAdd3.DataTextField = "LedgerName";
        cmbDebtorAdd3.DataValueField = "LedgerID";

        cmbCreditorAdd3.Items.Clear();
        ListItem lifzz = new ListItem("Select Ledger", "0");
        lifzz.Attributes.Add("style", "color:Black");
        cmbCreditorAdd3.Items.Add(lifzz);
        cmbCreditorAdd3.DataSource = ds;
        cmbCreditorAdd3.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbCreditorAdd3.DataBind();
        cmbCreditorAdd3.DataTextField = "LedgerName";
        cmbCreditorAdd3.DataValueField = "LedgerID";

        cmbDebtorAdd4.Items.Clear();
        ListItem lie = new ListItem("Select Ledger", "0");
        lie.Attributes.Add("style", "color:Black");
        cmbDebtorAdd4.Items.Add(lie);
        cmbDebtorAdd4.DataSource = ds;
        cmbDebtorAdd4.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbDebtorAdd4.DataBind();
        cmbDebtorAdd4.DataTextField = "LedgerName";
        cmbDebtorAdd4.DataValueField = "LedgerID";

        cmbCreditorAdd4.Items.Clear();
        ListItem lifwe = new ListItem("Select Ledger", "0");
        lifwe.Attributes.Add("style", "color:Black");
        cmbCreditorAdd4.Items.Add(lifwe);
        cmbCreditorAdd4.DataSource = ds;
        cmbCreditorAdd4.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbCreditorAdd4.DataBind();
        cmbCreditorAdd4.DataTextField = "LedgerName";
        cmbCreditorAdd4.DataValueField = "LedgerID";

        cmbDebtorAdd5.Items.Clear();
        ListItem lifffe = new ListItem("Select Ledger", "0");
        lifffe.Attributes.Add("style", "color:Black");
        cmbDebtorAdd5.Items.Add(lifffe);
        cmbDebtorAdd5.DataSource = ds;
        cmbDebtorAdd5.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbDebtorAdd5.DataBind();
        cmbDebtorAdd5.DataTextField = "LedgerName";
        cmbDebtorAdd5.DataValueField = "LedgerID";

        cmbCreditorAdd5.Items.Clear();
        ListItem lifwwe = new ListItem("Select Ledger", "0");
        lifwwe.Attributes.Add("style", "color:Black");
        cmbCreditorAdd5.Items.Add(lifwwe);
        cmbCreditorAdd5.DataSource = ds;
        cmbCreditorAdd5.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbCreditorAdd5.DataBind();
        cmbCreditorAdd5.DataTextField = "LedgerName";
        cmbCreditorAdd5.DataValueField = "LedgerID";

        cmbDebtorAdd6.Items.Clear();
        ListItem lillw = new ListItem("Select Ledger", "0");
        lillw.Attributes.Add("style", "color:Black");
        cmbDebtorAdd6.Items.Add(lillw);
        cmbDebtorAdd6.DataSource = ds;
        cmbDebtorAdd6.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbDebtorAdd6.DataBind();
        cmbDebtorAdd6.DataTextField = "LedgerName";
        cmbDebtorAdd6.DataValueField = "LedgerID";

        cmbCreditorAdd6.Items.Clear();
        ListItem lifz = new ListItem("Select Ledger", "0");
        lifz.Attributes.Add("style", "color:Black");
        cmbCreditorAdd6.Items.Add(lifz);
        cmbCreditorAdd6.DataSource = ds;
        cmbCreditorAdd6.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbCreditorAdd6.DataBind();
        cmbCreditorAdd6.DataTextField = "LedgerName";
        cmbCreditorAdd6.DataValueField = "LedgerID";

        

        drpDebtor.Items.Clear();
        ListItem lifzhzz = new ListItem("Select Ledger", "0");
        lifzhzz.Attributes.Add("style", "color:Black");
        drpDebtor.Items.Add(lifzhzz);
        drpDebtor.DataSource = ds;
        drpDebtor.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        drpDebtor.DataBind();
        drpDebtor.DataTextField = "LedgerName";
        drpDebtor.DataValueField = "LedgerID";

        drpCreditor1.Items.Clear();
        ListItem lifzhzzd = new ListItem("Select Ledger", "0");
        lifzhzzd.Attributes.Add("style", "color:Black");
        drpCreditor1.Items.Add(lifzhzzd);
        drpCreditor1.DataSource = ds;
        drpCreditor1.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        drpCreditor1.DataBind();
        drpCreditor1.DataTextField = "LedgerName";
        drpCreditor1.DataValueField = "LedgerID";

        //((DropDownList)this.FindControl("GrdViewItems").FindControl("drpCreditor")).Items.Clear();
        ////drpCreditor.Items.Clear();
        //ListItem lifzzhd = new ListItem("Select Ledger", "0");
        //lifzzhd.Attributes.Add("style", "color:Black");
        //((DropDownList)this.FindControl("GrdViewItems").FindControl("drpCreditor")).Items.Add(lifzzhd);
        //((DropDownList)this.FindControl("GrdViewItems").FindControl("drpCreditor")).DataSource = ds;
        //((DropDownList)this.FindControl("GrdViewItems").FindControl("drpCreditor")).Items[0].Attributes.Add("background-color", "color:#bce1fe");
        //((DropDownList)this.FindControl("GrdViewItems").FindControl("drpCreditor")).DataBind();
        //((DropDownList)this.FindControl("GrdViewItems").FindControl("drpCreditor")).DataTextField = "LedgerName";
        //((DropDownList)this.FindControl("GrdViewItems").FindControl("drpCreditor")).DataValueField = "LedgerID";
        //drpCreditor.Items.Add(lifzzhd);
        //drpCreditor.DataSource = ds;
        //drpCreditor.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        //drpCreditor.DataBind();
        //drpCreditor.DataTextField = "LedgerName";
        //drpCreditor.DataValueField = "LedgerID";

        cmbCreditor.Items.Clear();
        ListItem lifzzh = new ListItem("Select Ledger", "0");
        lifzzh.Attributes.Add("style", "color:Black");
        cmbCreditor.Items.Add(lifzzh);
        cmbCreditor.DataSource = ds;
        cmbCreditor.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbCreditor.DataBind();
        cmbCreditor.DataTextField = "LedgerName";
        cmbCreditor.DataValueField = "LedgerID";

        cmbDebtor.Items.Clear();
        ListItem lifzhz = new ListItem("Select Ledger", "0");
        lifzhz.Attributes.Add("style", "color:Black");
        cmbDebtor.Items.Add(lifzhz);
        cmbDebtor.DataSource = ds;
        cmbDebtor.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbDebtor.DataBind();
        cmbDebtor.DataTextField = "LedgerName";
        cmbDebtor.DataValueField = "LedgerID";

    }

    private void loadSupplier1()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListCreditorDebitorJNotActive(sDataSource);

        cmbCreditor.Items.Clear();
        ListItem lifzzh = new ListItem("Select Ledger", "0");
        lifzzh.Attributes.Add("style", "color:Black");
        cmbCreditor.Items.Add(lifzzh);
        cmbCreditor.DataSource = ds;
        cmbCreditor.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbCreditor.DataBind();
        cmbCreditor.DataTextField = "LedgerName";
        cmbCreditor.DataValueField = "LedgerID";

        cmbDebtor.Items.Clear();
        ListItem lifzhz = new ListItem("Select Ledger", "0");
        lifzhz.Attributes.Add("style", "color:Black");
        cmbDebtor.Items.Add(lifzhz);
        cmbDebtor.DataSource = ds;
        cmbDebtor.Items[0].Attributes.Add("background-color", "color:#bce1fe");
        cmbDebtor.DataBind();
        cmbDebtor.DataTextField = "LedgerName";
        cmbDebtor.DataValueField = "LedgerID";

    }

    protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();

            ds = bl.ListCreditorDebitorJ(sDataSource);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl = (DropDownList)e.Row.FindControl("drpCreditor");
                ddl.Items.Clear();
                ListItem lifzzh = new ListItem("Select Ledger", "0");
                lifzzh.Attributes.Add("style", "color:Black");
                ddl.Items.Add(lifzzh);
                ddl.DataSource = ds;
                ddl.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl.DataBind();
                ddl.DataTextField = "LedgerName";
                ddl.DataValueField = "LedgerID";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gdm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();

            ds = bl.ListCreditorDebitorJ(sDataSource);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl = (DropDownList)e.Row.FindControl("drpCreditorM");
                ddl.Items.Clear();
                ListItem lifzzh = new ListItem("Select Ledger", "0");
                lifzzh.Attributes.Add("style", "color:Black");
                ddl.Items.Add(lifzzh);
                ddl.DataSource = ds;
                ddl.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl.DataBind();
                ddl.DataTextField = "LedgerName";
                ddl.DataValueField = "LedgerID";

                var ddll = (DropDownList)e.Row.FindControl("drpDebtorM");
                ddll.Items.Clear();
                ListItem lifzzhh = new ListItem("Select Ledger", "0");
                lifzzhh.Attributes.Add("style", "color:Black");
                ddll.Items.Add(lifzzhh);
                ddll.DataSource = ds;
                ddll.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddll.DataBind();
                ddll.DataTextField = "LedgerName";
                ddll.DataValueField = "LedgerID";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BulkEditGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();

            ds = bl.ListCreditorDebitorJ(sDataSource);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl = (DropDownList)e.Row.FindControl("drpDebtor1");
                ddl.Items.Clear();
                ListItem lifzzh = new ListItem("Select Ledger", "0");
                lifzzh.Attributes.Add("style", "color:Black");
                ddl.Items.Add(lifzzh);
                ddl.DataSource = ds;
                ddl.Items[0].Attributes.Add("background-color", "color:#bce1fe");
                ddl.DataBind();
                ddl.DataTextField = "LedgerName";
                ddl.DataValueField = "LedgerID";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadBilts(string ID)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.LitsOpenBilts(ID);
    }

    private void loadProducts()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListProducts();
    }

    protected void GrdViewJournal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewJournal.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void txtRefnumAdd1_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtTransDateAdd1.Text == "")
    //    {
    //        txtTransDateAdd1.Text = DateTime.Now.ToShortDateString();
    //        txtTransDateAdd1.Focus();
    //    }
    //}

    //protected void txtRefnumAdd2_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtTransDateAdd2.Text == "")
    //    {
    //        txtTransDateAdd2.Text = DateTime.Now.ToShortDateString();
    //        txtTransDateAdd2.Focus();
    //    }
    //}

    //protected void txtRefnumAdd3_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtTransDateAdd3.Text == "")
    //    {
    //        txtTransDateAdd3.Text = DateTime.Now.ToShortDateString();
    //        txtTransDateAdd3.Focus();
    //    }
    //}

    //protected void txtRefnumAdd4_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtTransDateAdd4.Text == "")
    //    {
    //        txtTransDateAdd4.Text = DateTime.Now.ToShortDateString();
    //        txtTransDateAdd4.Focus();
    //    }
    //}

    //protected void txtRefnumAdd5_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtTransDateAdd5.Text == "")
    //    {
    //        txtTransDateAdd5.Text = DateTime.Now.ToShortDateString();
    //        txtTransDateAdd5.Focus();
    //    }
    //}

    //protected void txtRefnumAdd6_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtTransDateAdd6.Text == "")
    //    {
    //        txtTransDateAdd6.Text = DateTime.Now.ToShortDateString();
    //        txtTransDateAdd6.Focus();
    //    }
    //}

    private void BindGrid()
    {
        string sDataSource = string.Empty;
        string textt = string.Empty;
        string dropd = string.Empty;

        if (Request.Cookies["Company"] != null)
            sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        textt = txtSearch.Text;
        dropd = ddCriteria.SelectedValue;

        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        //ds = bl.ListJournal(txtTransno.Text.Trim(), txtRefno.Text.Trim(), txtLedger.Text.Trim(), txtDate.Text, sDataSource);

        ds = bl.ListJournalDatas(textt, dropd, sDataSource);

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
    }

    protected void GrdViewJournal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "MULJOU"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "MULJOU"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveView(usernam, "MULJOU"))
                {
                    ((Image)e.Row.FindControl("lnkprint")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = true;
                }
                else
                {
                    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = false;
                }
            }
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
            string strPaymode = string.Empty;
            int transID = 0;
            GridViewRow row = GrdViewJournal.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();
            lblBillNo.Text = "Update";

            loadSupplier1();

            transID = Convert.ToInt32(GrdViewJournal.SelectedDataKey.Value);

            DataSet ds = bl.GetJournalForId(transID, sDataSource);

            if (ds.Tables[0].Rows[0]["RefNo"] != null)
                txtRefnum.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();

            if (ds.Tables[0].Rows[0]["Amount"] != null)
                txtAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
            else
                txtAmount.Text = "0";

            //if (ds.Tables[0].Rows[0]["LoadUnload"] != null)
            //    txtLU.Text = Convert.ToString(ds.Tables[0].Rows[0]["LoadUnload"]);
            //else
            //    txtLU.Text = "0";
            ///*End Purchase Loading / Unloading Freight Change - March 16*/

            //if (ds.Tables[0].Rows[0]["SalesReturn"] != null)
            //{
            //    drpSalesReturn.ClearSelection();
            //    drpSalesReturn.SelectedValue = ds.Tables[0].Rows[0]["SalesReturn"].ToString().ToUpper();
            //}
            //else
            //{
            //    drpSalesReturn.SelectedIndex = 0;
            //}

            //if (ds.Tables[0].Rows[0]["InternalTransfer"] != null && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["InternalTransfer"].ToString()))
            //{
            //    drpIntTrans.ClearSelection();
            //    drpIntTrans.SelectedValue = ds.Tables[0].Rows[0]["InternalTransfer"].ToString().ToUpper();
            //}
            //else
            //{
            //    drpIntTrans.SelectedIndex = 0;
            //}

            //if (ds.Tables[0].Rows[0]["DeliveryNote"] != null && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DeliveryNote"].ToString()))
            //{
            //    ddDeliveryNote.ClearSelection();
            //    ddDeliveryNote.SelectedValue = ds.Tables[0].Rows[0]["DeliveryNote"].ToString().ToUpper();
            //}
            //else
            //{
            //    ddDeliveryNote.SelectedIndex = 0;
            //}


            //if (ds.Tables[0].Rows[0]["BilitID"] != null)
            //{
            //    ddBilts.Items.Clear();
            //    loadBilts(ds.Tables[0].Rows[0]["BilitID"].ToString());
            //    ddBilts.SelectedValue = ds.Tables[0].Rows[0]["BilitID"].ToString();
            //}
            //else
            //{
            //    ddBilts.SelectedIndex = 0;
            //}

            //if (drpSalesReturn.SelectedValue == "NO")
            //{
            //    rowSalesRet.Visible = false;
            //}
            //else
            //{
            //    rowSalesRet.Visible = true;
            //}

            //if (drpSalesReturn.SelectedItem.Text == "NO")
            //{
            //    loadSupplier("Sundry Creditors");
            //}
            //else
            //{
            //    loadSupplier("Sundry Debtors");
            //}

            int DebtorID = 0;
            if ((ds.Tables[0].Rows[0]["DebtorID"] != null) && (ds.Tables[0].Rows[0]["DebtorID"].ToString() != ""))
            {
                DebtorID = Convert.ToInt32(ds.Tables[0].Rows[0]["DebtorID"].ToString());
                cmbDebtor.ClearSelection();
                ListItem li = cmbDebtor.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(DebtorID.ToString()));
                if (li != null) li.Selected = true;
            }

            int CreditorID = 0;
            if ((ds.Tables[0].Rows[0]["CreditorID"] != null) && (ds.Tables[0].Rows[0]["CreditorID"].ToString() != ""))
            {
                CreditorID = Convert.ToInt32(ds.Tables[0].Rows[0]["CreditorID"].ToString());
                cmbCreditor.ClearSelection();
                ListItem li = cmbCreditor.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(CreditorID.ToString()));
                if (li != null) li.Selected = true;
            }

            if (ds.Tables[0].Rows[0]["TransDate"] != null)
                txtTransDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["TransDate"].ToString()).ToString("dd/MM/yyyy");

            if (ds.Tables[0].Rows[0]["Narration"] != null)
                txtNarr.Text = ds.Tables[0].Rows[0]["Narration"].ToString();


            //if (txtBillnoSrc.Text == "")
            //    BindGrid("0", "0");
            //else
            //    BindGrid(txtBillnoSrc.Text, txtTransNo.Text);
            ////Accordion1.SelectedIndex = 1;

            //hdPurchase.Value = purchaseID.ToString();
            //DataSet itemDs = formXml();
            //Session["PurchaseProductDs"] = itemDs;
            //GrdViewItems.DataSource = itemDs;
            //GrdViewItems.DataBind();
            ////BindProduct();
            //calcSum();
            //hdMode.Value = "Edit";

            //cmdSave.Visible = false;
            //cmdUpdate.Visible = true;
            //cmdUpdate.Enabled = true;

            //cmdPrint.Enabled = true;


            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (bl.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                cmdSave.Enabled = false;
                //cmdDelete.Enabled = false;
                cmdUpdate.Enabled = false;
                lnkBtnAdd.Visible = false;
                pnlSearch.Visible = false;
            }
            ModalPopupMethod.Show();

            updatePnlPurchase.Update();
            ModalPopupPurchase.Show();
            ModalPopupContact.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdButton_Click(object sender, EventArgs e)
    {
        string connection = string.Empty;
        connection = Request.Cookies["Company"].Value;
        
        string[] sDate;
        DateTime sBilldate;
        
        string delim = "/";
        char[] delimA = delim.ToCharArray();
        CultureInfo culture = new CultureInfo("pt-BR");
        string sPath = string.Empty;

        if (Request.Cookies["Company"] != null)
            sDataSource = Request.Cookies["Company"].Value;

        sPath = sDataSource;
        string usernam = Request.Cookies["LoggedUserName"].Value;

        BusinessLogic bl = new BusinessLogic(sDataSource);

        for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
        {
            TextBox txttt = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtRefNo");
            TextBox txt = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtAmount");
            TextBox txtt = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtNarration");
            DropDownList txttd = (DropDownList)GrdViewItems.Rows[vLoop].FindControl("drpCreditor");
            TextBox txttdd = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDate");

            int col = vLoop + 1;

            if(txttt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill RefNo in row " + col + " ')", true);
                return;
            }
            else if (txt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Amount in row " + col + " ')", true);
                return;
            }
            else if (txtt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Narration in row " + col + " ')", true);
                return;
            }
            else if (txttd.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Creditor in row " + col + " ')", true);
                return;
            }
            else if (txttdd.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill date in row " + col + " ')", true);
                return;
                
            }

            if (!bl.IsValidDate(connection, Convert.ToDateTime(txttdd.Text)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid in row " + col + " ')", true);
                return;
            }

            if (txttd.SelectedItem.Text == drpDebtor.SelectedItem.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Creditor and Debtor should not be same in row " + col + " ')", true);
                return;
            }
        }

        

        DataSet ds;
        DataTable dt;
        DataRow drNew;

        DataColumn dc;

        ds = new DataSet();

        dt = new DataTable();

        dc = new DataColumn("RefNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("Date");
        dt.Columns.Add(dc);

        dc = new DataColumn("Debtor");
        dt.Columns.Add(dc);

        dc = new DataColumn("Creditor");
        dt.Columns.Add(dc);

        dc = new DataColumn("Amount");
        dt.Columns.Add(dc);

        dc = new DataColumn("Narration");
        dt.Columns.Add(dc);

        dc = new DataColumn("VoucherType");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        for (int vLoop = 0; vLoop < GrdViewItems.Rows.Count; vLoop++)
        {
            TextBox txttt = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtRefNo");
            TextBox txt = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtAmount");
            TextBox txtt = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtNarration");
            DropDownList txttd = (DropDownList)GrdViewItems.Rows[vLoop].FindControl("drpCreditor");
            TextBox txttdd = (TextBox)GrdViewItems.Rows[vLoop].FindControl("txtDate");

            sDate = txttdd.Text.Trim().Split(delimA);
            sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

            drNew = dt.NewRow();
            drNew["RefNo"] = txttt.Text;
            drNew["Date"] = sBilldate;
            drNew["Debtor"] = Convert.ToInt32(drpDebtor.SelectedItem.Value);
            drNew["Creditor"] = Convert.ToInt32(txttd.SelectedItem.Value);
            drNew["Amount"] = txt.Text;
            drNew["Narration"] = txtt.Text;
            drNew["VoucherType"] = "Journal";
            ds.Tables[0].Rows.Add(drNew);
        }

        bl.InsertContras(sPath, usernam, ds);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journal Saved Successfully.')", true);

        ModalPopupExtender1.Hide();
        ModalPopupMethod.Hide();
        ModalPopupPurchase.Hide();
        BindGrid();
        GrdViewJournal.DataBind();
        UpdatePnlMaster.Update();
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        string connection = string.Empty;
        connection = Request.Cookies["Company"].Value;

        string[] sDate;
        DateTime sBilldate;

        string delim = "/";
        char[] delimA = delim.ToCharArray();
        CultureInfo culture = new CultureInfo("pt-BR");
        string sPath = string.Empty;

        if (Request.Cookies["Company"] != null)
            sDataSource = Request.Cookies["Company"].Value;

        sPath = sDataSource;
        string usernam = Request.Cookies["LoggedUserName"].Value;

        BusinessLogic bl = new BusinessLogic(sDataSource);

        for (int vLoop = 0; vLoop < BulkEditGridView1.Rows.Count; vLoop++)
        {
            TextBox txttt = (TextBox)BulkEditGridView1.Rows[vLoop].FindControl("txtRefNo");
            TextBox txt = (TextBox)BulkEditGridView1.Rows[vLoop].FindControl("txtAmount");
            TextBox txtt = (TextBox)BulkEditGridView1.Rows[vLoop].FindControl("txtNarration");
            DropDownList txttd = (DropDownList)BulkEditGridView1.Rows[vLoop].FindControl("drpDebtor1");
            TextBox txttdd = (TextBox)BulkEditGridView1.Rows[vLoop].FindControl("txtDate");

            int col = vLoop + 1;

            if (txttt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill RefNo in row " + col + " ')", true);
                return;
            }
            else if (txt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Amount in row " + col + " ')", true);
                return;
            }
            else if (txtt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Narration in row " + col + " ')", true);
                return;
            }
            else if (txttd.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select debtor in row " + col + " ')", true);
                return;
            }
            else if (txttdd.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill date in row " + col + " ')", true);
                return;

            }

            if (!bl.IsValidDate(connection, Convert.ToDateTime(txttdd.Text)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid in row " + col + " ')", true);
                return;
            }

            if (txttd.SelectedItem.Text == drpCreditor1.SelectedItem.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Creditor and Debtor should not be same in row " + col + " ')", true);
                return;
            }
        }



        DataSet ds;
        DataTable dt;
        DataRow drNew;

        DataColumn dc;

        ds = new DataSet();

        dt = new DataTable();

        dc = new DataColumn("RefNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("Date");
        dt.Columns.Add(dc);

        dc = new DataColumn("Debtor");
        dt.Columns.Add(dc);

        dc = new DataColumn("Creditor");
        dt.Columns.Add(dc);

        dc = new DataColumn("Amount");
        dt.Columns.Add(dc);

        dc = new DataColumn("Narration");
        dt.Columns.Add(dc);

        dc = new DataColumn("VoucherType");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        for (int vLoop = 0; vLoop < BulkEditGridView1.Rows.Count; vLoop++)
        {
            TextBox txttt = (TextBox)BulkEditGridView1.Rows[vLoop].FindControl("txtRefNo");
            TextBox txt = (TextBox)BulkEditGridView1.Rows[vLoop].FindControl("txtAmount");
            TextBox txtt = (TextBox)BulkEditGridView1.Rows[vLoop].FindControl("txtNarration");
            DropDownList txttd = (DropDownList)BulkEditGridView1.Rows[vLoop].FindControl("drpDebtor1");
            TextBox txttdd = (TextBox)BulkEditGridView1.Rows[vLoop].FindControl("txtDate");

            sDate = txttdd.Text.Trim().Split(delimA);
            sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

            drNew = dt.NewRow();
            drNew["RefNo"] = txttt.Text;
            drNew["Date"] = sBilldate;
            drNew["Debtor"] = Convert.ToInt32(txttd.SelectedItem.Value);
            drNew["Creditor"] = Convert.ToInt32(drpCreditor1.SelectedItem.Value);
            drNew["Amount"] = txt.Text;
            drNew["Narration"] = txtt.Text;
            drNew["VoucherType"] = "Journal";
            ds.Tables[0].Rows.Add(drNew);
        }

        bl.InsertContras(sPath, usernam, ds);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journal Saved Successfully.')", true);

        ModalPopupExtender1.Hide();
        ModalPopupExtender2.Hide();
        ModalPopupMethod.Hide();
        ModalPopupPurchase.Hide();
        BindGrid();
        GrdViewJournal.DataBind();
        UpdatePnlMaster.Update();
    }


    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblBillNo.Text == "Update")
            {
                int idebtor = 0;
                int iCreditor = 0;

                string[] sDate;
                string sBillno;
                string VoucherType = string.Empty;
                string sPath = string.Empty;
                int iJournalId = 0;

                string sNarration = string.Empty;
                double dTotalAmt1 = 0;
                DateTime sBilldate1;

                string delim = "/";
                char[] delimA = delim.ToCharArray();
                CultureInfo culture = new CultureInfo("pt-BR");

                string sDataSource = string.Empty;

                if (Request.Cookies["Company"] != null)
                    sDataSource = Request.Cookies["Company"].Value;

                sDate = txtTransDate.Text.Trim().Split(delimA);
                sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

                idebtor = Convert.ToInt32(cmbDebtor.SelectedItem.Value);
                iCreditor = Convert.ToInt32(cmbCreditor.SelectedItem.Value);
                dTotalAmt1 = Convert.ToDouble(txtAmount.Text);
                sBillno = txtRefnum.Text.Trim();
                sNarration = txtNarr.Text;
                VoucherType = "Journal";
                string Username = Request.Cookies["LoggedUserName"].Value;

                int TransNo = Convert.ToInt32(GrdViewJournal.SelectedDataKey.Value);
                //out int NewTransNo;


                if (Request.Cookies["Company"] != null)
                    sDataSource = Request.Cookies["Company"].Value;

                sPath = sDataSource;

                BusinessLogic bl = new BusinessLogic(sDataSource);

                bl.UpdatJournal(TransNo, sBillno, sBilldate1, idebtor, iCreditor, dTotalAmt1, sNarration, VoucherType, sDataSource, Username);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journal Updated Successfully.')", true);


                ModalPopupPurchase.Hide();
                BindGrid();
                GrdViewJournal.DataBind();
                UpdatePnlMaster.Update();
            }
            else if (lblBillNo.Text == "Add")
            {
                string connection = string.Empty;
                connection = Request.Cookies["Company"].Value;
                int idebtor1 = 0;

                int iCreditor1 = 0;
                double dTotalAmt1 = 0;
                string VoucherType = string.Empty;
                string[] sDate;
                DateTime sBilldate1;
                string sNarration1 = string.Empty;

                int Newtrsns = 0;

                string delim = "/";
                char[] delimA = delim.ToCharArray();
                CultureInfo culture = new CultureInfo("pt-BR");
                string sPath = string.Empty;
                string sBillno1 = string.Empty;
                try
                {
                    sDate = txtTransDate.Text.Trim().Split(delimA);
                    sBilldate1 = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));
                }
                catch (Exception ex)
                {
                    Response.Write("<b><font face='Trebuchet MS' size=2 color=red>Invalid Bill Date Format</font></b>");
                    return;
                }
                idebtor1 = Convert.ToInt32(cmbDebtor.SelectedItem.Value);
                iCreditor1 = Convert.ToInt32(cmbCreditor.SelectedItem.Value);
                dTotalAmt1 = Convert.ToDouble(txtAmount.Text);
                sBillno1 = txtRefnum.Text.Trim();
                sNarration1 = txtNarr.Text;
                VoucherType = "Journal";

                if (Request.Cookies["Company"] != null)
                    sDataSource = Request.Cookies["Company"].Value;

                sPath = sDataSource;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                BusinessLogic bl = new BusinessLogic(sDataSource);

                bl.InsertJournal(out Newtrsns, sBillno1, sBilldate1, idebtor1, iCreditor1, dTotalAmt1, sNarration1, VoucherType, sPath, usernam);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journal Saved Successfully. Transaction No : " + Newtrsns + "')", true);

                ModalPopupContact.Hide();
                ModalPopupMethod.Hide();
                ModalPopupPurchase.Hide();
                BindGrid();
                GrdViewJournal.DataBind();
                UpdatePnlMaster.Update();

                txtRefnum.Text = "";
                txtTransDate.Text = DateTime.Now.ToShortDateString();
                cmbDebtor.SelectedIndex = 0;
                cmbCreditor.SelectedIndex = 0;
                txtAmount.Text = "";
                txtNarr.Text = "";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdCancelButton_Click(object sender, EventArgs e)
    {
        ModalPopupMethod.Show();
        ModalPopupPurchase.Hide();
        ModalPopupExtender1.Hide();
        ModalPopupExtender2.Hide();
        txtEntries.Text = "";
    }

    protected void Save1_Click(object sender, EventArgs e)
    {
        string connection = string.Empty;
        connection = Request.Cookies["Company"].Value;

        string[] sDate;
        DateTime sBilldate;

        string delim = "/";
        char[] delimA = delim.ToCharArray();
        CultureInfo culture = new CultureInfo("pt-BR");
        string sPath = string.Empty;

        if (Request.Cookies["Company"] != null)
            sDataSource = Request.Cookies["Company"].Value;

        sPath = sDataSource;
        string usernam = Request.Cookies["LoggedUserName"].Value;

        BusinessLogic bl = new BusinessLogic(sDataSource);

        for (int vLoop = 0; vLoop < gdm.Rows.Count; vLoop++)
        {
            TextBox txttt = (TextBox)gdm.Rows[vLoop].FindControl("txtRefNoM");
            TextBox txt = (TextBox)gdm.Rows[vLoop].FindControl("txtAmountM");
            TextBox txtt = (TextBox)gdm.Rows[vLoop].FindControl("txtNarrationM");
            DropDownList txttd = (DropDownList)gdm.Rows[vLoop].FindControl("drpCreditorM");
            DropDownList txttddd = (DropDownList)gdm.Rows[vLoop].FindControl("drpDebtorM");
            TextBox txttdd = (TextBox)gdm.Rows[vLoop].FindControl("txtDateM");

            int col = vLoop + 1;

            if (txttt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill RefNo in row " + col + " ')", true);
                return;
            }
            else if (txt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Amount in row " + col + " ')", true);
                return;
            }
            else if (txtt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Narration in row " + col + " ')", true);
                return;
            }
            else if (txttd.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Creditor in row " + col + " ')", true);
                return;
            }
            else if (txttddd.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Debtor in row " + col + " ')", true);
                return;
            }
            else if (txttdd.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill date in row " + col + " ')", true);
                return;

            }

            if (!bl.IsValidDate(connection, Convert.ToDateTime(txttdd.Text)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid in row " + col + " ')", true);
                return;
            }

            if (txttd.SelectedItem.Text == txttddd.SelectedItem.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Creditor and Debtor should not be same in row " + col + " ')", true);
                return;
            }
        }



        DataSet ds;
        DataTable dt;
        DataRow drNew;

        DataColumn dc;

        ds = new DataSet();

        dt = new DataTable();

        dc = new DataColumn("RefNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("Date");
        dt.Columns.Add(dc);

        dc = new DataColumn("Debtor");
        dt.Columns.Add(dc);

        dc = new DataColumn("Creditor");
        dt.Columns.Add(dc);

        dc = new DataColumn("Amount");
        dt.Columns.Add(dc);

        dc = new DataColumn("Narration");
        dt.Columns.Add(dc);

        dc = new DataColumn("VoucherType");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        for (int vLoop = 0; vLoop < gdm.Rows.Count; vLoop++)
        {
            TextBox txttt = (TextBox)gdm.Rows[vLoop].FindControl("txtRefNoM");
            TextBox txt = (TextBox)gdm.Rows[vLoop].FindControl("txtAmountM");
            TextBox txtt = (TextBox)gdm.Rows[vLoop].FindControl("txtNarrationM");
            DropDownList txttd = (DropDownList)gdm.Rows[vLoop].FindControl("drpCreditorM");
            DropDownList txttddd = (DropDownList)gdm.Rows[vLoop].FindControl("drpDebtorM");
            TextBox txttdd = (TextBox)gdm.Rows[vLoop].FindControl("txtDateM");

            sDate = txttdd.Text.Trim().Split(delimA);
            sBilldate = new DateTime(Convert.ToInt32(sDate[2].ToString()), Convert.ToInt32(sDate[1].ToString()), Convert.ToInt32(sDate[0].ToString()));

            drNew = dt.NewRow();
            drNew["RefNo"] = txttt.Text;
            drNew["Date"] = sBilldate;
            drNew["Debtor"] = Convert.ToInt32(txttddd.SelectedItem.Value);
            drNew["Creditor"] = Convert.ToInt32(txttd.SelectedItem.Value);
            drNew["Amount"] = txt.Text;
            drNew["Narration"] = txtt.Text;
            drNew["VoucherType"] = "Journal";
            ds.Tables[0].Rows.Add(drNew);
        }

        bl.InsertContras(sPath, usernam, ds);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Journals Saved Successfully.')", true);

        ModalPopupExtender1.Hide();
        ModalPopupMethod.Hide();
        ModalPopupPurchase.Hide();
        ModalPopupExtender3.Hide();
        BindGrid();
        GrdViewJournal.DataBind();
        UpdatePnlMaster.Update();
    }

    protected void Cancel1_Click(object sender, EventArgs e)
    {
        ModalPopupMethod.Show();
        ModalPopupPurchase.Hide();
        ModalPopupExtender1.Hide();
        ModalPopupExtender2.Hide();
        ModalPopupExtender3.Hide();
        txtEntries.Text = "";
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        ModalPopupMethod.Show();
        ModalPopupPurchase.Hide();
        ModalPopupExtender2.Hide();
        ModalPopupExtender1.Hide();
        txtEntries.Text = "";
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblBillNo.Text == "Add")
            {
                ModalPopupMethod.Show();
                ModalPopupContact.Hide();
                ModalPopupPurchase.Hide();
            }
            else if (lblBillNo.Text == "Update")
            {
                ModalPopupMethod.Hide();
                ModalPopupContact.Hide();
                ModalPopupPurchase.Hide();
            }

            lblBillNo.Text = "";
            txtEntries.Text = "";
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
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
    //    string CategoryID = cmbCategory.SelectedValue;
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    DataSet ds = new DataSet();
    //    ds = bl.ListProductsForCategoryID(CategoryID);
    //    cmbProdAdd.Items.Clear();
    //    cmbProdAdd.DataSource = ds;
    //    cmbProdAdd.Items.Insert(0, new ListItem("Select Product", "0"));
    //    cmbProdAdd.DataTextField = "ItemCode";
    //    cmbProdAdd.DataValueField = "ItemCode";

    //    cmbProdAdd.DataBind();

    //    ds = bl.ListModelsForCategoryID(CategoryID);
    //    cmbModel.Items.Clear();
    //    cmbModel.DataSource = ds;
    //    cmbModel.Items.Insert(0, new ListItem("Select Model", "0"));
    //    cmbModel.DataTextField = "Model";
    //    cmbModel.DataValueField = "Model";
    //    cmbModel.DataBind();

    //    ds = bl.ListBrandsForCategoryID(CategoryID);
    //    cmbBrand.Items.Clear();
    //    cmbBrand.DataSource = ds;
    //    cmbBrand.Items.Insert(0, new ListItem("Select Brand", "0"));
    //    cmbBrand.DataTextField = "ProductDesc";
    //    cmbBrand.DataValueField = "ProductDesc";
    //    cmbBrand.DataBind();

    //    ds = bl.ListProdNameForCategoryID(CategoryID);
    //    cmbProdName.Items.Clear();
    //    cmbProdName.DataSource = ds;
    //    cmbProdName.Items.Insert(0, new ListItem("Select ItemName", "0"));
    //    cmbProdName.DataTextField = "ProductName";
    //    cmbProdName.DataValueField = "ProductName";
    //    cmbProdName.DataBind();

    //    LoadForProduct(this, null);
    //}

    //protected void LoadForProductName(object sender, EventArgs e)
    //{
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    string prodName = cmbProdName.SelectedValue;
    //    string CategoryID = cmbCategory.SelectedValue;
    //    DataSet ds = new DataSet();

    //    ds = bl.ListProdcutsForProductName(prodName, CategoryID);
    //    cmbProdAdd.Items.Clear();
    //    cmbProdAdd.DataSource = ds;
    //    cmbProdAdd.DataTextField = "ItemCode";
    //    cmbProdAdd.DataValueField = "ItemCode";
    //    cmbProdAdd.DataBind();

    //    ds = bl.ListBrandsForProductName(prodName, CategoryID);
    //    cmbBrand.Items.Clear();
    //    cmbBrand.DataSource = ds;
    //    cmbBrand.DataTextField = "ProductDesc";
    //    cmbBrand.DataValueField = "ProductDesc";
    //    cmbBrand.DataBind();

    //    ds = bl.ListModelsForProductName(prodName, CategoryID);
    //    cmbModel.Items.Clear();
    //    cmbModel.DataSource = ds;
    //    cmbModel.DataTextField = "Model";
    //    cmbModel.DataValueField = "Model";
    //    cmbModel.DataBind();

    //    cmbProdAdd_SelectedIndexChanged(this, null);
    }

    protected void LoadForBrand(object sender, EventArgs e)
    {
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    string brand = cmbBrand.SelectedValue;
    //    string CategoryID = cmbCategory.SelectedValue;
    //    //DataSet catData = bl.GetProductForId(sDataSource, itemCode);
    //    //cmbProdAdd.SelectedValue = itemCode;
    //    //cmbModel.SelectedValue = itemCode;
    //    DataSet ds = new DataSet();
    //    ds = bl.ListModelsForBrand(brand, CategoryID);
    //    cmbModel.Items.Clear();
    //    cmbModel.DataSource = ds;
    //    cmbModel.DataTextField = "Model";
    //    cmbModel.DataValueField = "Model";
    //    cmbModel.DataBind();

    //    ds = bl.ListProdcutsForBrand(brand, CategoryID);
    //    cmbProdAdd.Items.Clear();
    //    cmbProdAdd.DataSource = ds;
    //    cmbProdAdd.DataTextField = "ItemCode";
    //    cmbProdAdd.DataValueField = "ItemCode";
    //    cmbProdAdd.DataBind();

    //    ds = bl.ListProdcutNameForBrand(brand, CategoryID);
    //    cmbProdName.Items.Clear();
    //    cmbProdName.DataSource = ds;
    //    cmbProdName.DataTextField = "ProductName";
    //    cmbProdName.DataValueField = "ProductName";
    //    cmbProdName.DataBind();

    //    cmbProdAdd_SelectedIndexChanged(this, null);

    //}

    //protected void LoadForModel(object sender, EventArgs e)
    //{
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    string model = cmbModel.SelectedValue;
    //    string CategoryID = cmbCategory.SelectedValue;
    //    DataSet ds = new DataSet();

    //    ds = bl.ListProdcutsForModel(model, CategoryID);
    //    cmbProdAdd.Items.Clear();
    //    cmbProdAdd.DataSource = ds;
    //    cmbProdAdd.DataTextField = "ItemCode";
    //    cmbProdAdd.DataValueField = "ItemCode";
    //    cmbProdAdd.DataBind();

    //    ds = bl.ListBrandsForModel(model, CategoryID);
    //    cmbBrand.Items.Clear();
    //    cmbBrand.DataSource = ds;
    //    cmbBrand.DataTextField = "ProductDesc";
    //    cmbBrand.DataValueField = "ProductDesc";
    //    cmbBrand.DataBind();

    //    ds = bl.ListProductNameForModel(model, CategoryID);
    //    cmbProdName.Items.Clear();
    //    cmbProdName.DataSource = ds;
    //    cmbProdName.DataTextField = "ProductName";
    //    cmbProdName.DataValueField = "ProductName";
    //    cmbProdName.DataBind();

    //    cmbProdAdd_SelectedIndexChanged(this, null);
    }

    protected void LoadForProduct(object sender, EventArgs e)
    {
        //string itemCode = cmbProdAdd.SelectedValue;
        //cmbModel.SelectedValue = itemCode;
        //cmbBrand.SelectedValue = itemCode;
        //cmbProdAdd_SelectedIndexChanged(this, null);
    }

    //private DataSet formXml()
    //{
        //int purchaseID = 0;
        ////string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        ////DataSet ds = new DataSet();
        ////purchaseID = Convert.ToInt32(hdPurchase.Value);
        ///*March18*/
        //DataSet itemDs = null;
        ///*March18*/
        //DataTable dt;
        //DataRow dr;
        //DataColumn dc;
        //DataSet ds = new DataSet();

        //double dTotal = 0;
        //double dQty = 0;
        //double dRate = 0;
        //double dNLP = 0;
        //string strRole = string.Empty;
        //string roleFlag = string.Empty;
        //string strBundles = string.Empty;


        //double stock = 0;

        //string strItemCode = string.Empty;
        //DataSet dsRole;
        //ds = bl.GetPurchaseItemsForId(purchaseID);
        //if (ds != null)
        //{

        //    dt = new DataTable();

        //    dc = new DataColumn("PurchaseID");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("itemCode");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("ProductName");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("ProductDesc");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("PurchaseRate");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("NLP");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("Qty");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("Measure_Unit");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("Discount");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("VAT");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("CST");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("Discountamt");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("Roles");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("IsRole");
        //    dt.Columns.Add(dc);


        //    dc = new DataColumn("Total");
        //    dt.Columns.Add(dc);
        //    /*March18*/
        //    itemDs = new DataSet();
        //    /*March18*/


        //    itemDs.Tables.Add(dt);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dR in ds.Tables[0].Rows)
        //        {
        //            dr = itemDs.Tables[0].NewRow();

        //            if (dR["Qty"] != null)
        //                dQty = Convert.ToDouble(dR["Qty"]);
        //            if (dR["PurchaseRate"] != null)
        //                dRate = Convert.ToDouble(dR["PurchaseRate"]);

        //            if (dR["NLP"] != null)
        //            {
        //                if (dR["NLP"].ToString() != "")
        //                    dNLP = Convert.ToDouble(dR["NLP"]);
        //                else
        //                    dNLP = 0.0;
        //            }


        //            dTotal = dQty * dRate;
        //            if (dR["ItemCode"] != null)
        //            {
        //                strItemCode = Convert.ToString(dR["ItemCode"]);
        //                dr["itemCode"] = strItemCode;
        //            }
        //            if (dR["PurchaseID"] != null)
        //            {
        //                purchaseID = Convert.ToInt32(dR["PurchaseID"]);
        //                dr["PurchaseID"] = Convert.ToString(purchaseID);
        //            }

        //            if (dR["ProductName"] != null)
        //                dr["ProductName"] = Convert.ToString(dR["ProductName"]);

        //            if (dR["ProductDesc"] != null)
        //                dr["ProductDesc"] = Convert.ToString(dR["ProductDesc"]);

        //            if (dR["Measure_Unit"] != null)
        //                dr["Measure_Unit"] = Convert.ToString(dR["Measure_Unit"]);

        //            dr["Qty"] = dQty.ToString();

        //            dr["PurchaseRate"] = dRate.ToString();

        //            dr["NLP"] = dNLP.ToString();

        //            if (dR["Discount"] != null)
        //                dr["Discount"] = Convert.ToString(dR["Discount"]);

        //            if (dR["VAT"] != null)
        //                dr["VAT"] = Convert.ToString(dR["VAT"]);

        //            if (dR["CST"] != null)
        //                dr["CST"] = Convert.ToString(dR["CST"]);

        //            if (dR["discamt"] != null)
        //                dr["Discountamt"] = Convert.ToString(dR["discamt"]);

        //            if (dR["isrole"] != null)
        //            {
        //                roleFlag = Convert.ToString(dR["isrole"]);
        //                dr["IsRole"] = roleFlag;

        //            }

        //            if (roleFlag == "Y")
        //            {
        //                strRole = Convert.ToString(dR["RoleID"]);
        //            }
        //            else
        //            {
        //                strRole = "NO ROLE";
        //            }

        //            if (hdStock.Value != "")
        //                stock = Convert.ToDouble(hdStock.Value);
        //            dr["Roles"] = strRole;
        //            dr["Total"] = Convert.ToString(dTotal);
        //            itemDs.Tables[0].Rows.Add(dr);
        //            strRole = "";
        //        }
        //    }


        //}
        //return itemDs;
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

    protected void GrdViewJournal_RowEditing(object sender, GridViewEditEventArgs e)
    {



    }
    protected void GrdViewJournal_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void BtnViewDetails_Click(object sender, EventArgs e)
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

    protected void GrdViewJournal_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewJournal, e.Row, this);
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
            GrdViewJournal.PageIndex = ((DropDownList)sender).SelectedIndex;
            String strBillno = string.Empty;
            string strTransNo = string.Empty;


            BindGrid();
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


    private void EmptyRow()
    {

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

        ////GrdViewItems.Columns[10].Visible = false;

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
    }


}
