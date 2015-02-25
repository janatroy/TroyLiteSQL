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
using SMSLibrary;

public partial class ChequeBook : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                string connStr = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                loadBanks(connStr);
                loadCustomerDealers(connStr);

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    GrdViewSerVisit.Columns[7].Visible = false;
                }

                //myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                //myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

                GrdViewSerVisit.PageSize = 3;


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "CHQMST"))
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

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearch.Text = "";
            ddCriteria.SelectedIndex = 0;
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

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                int CustomerID = 0;
                int ServiceID = 0;
                DateTime DueDate;
                DateTime VisitDate;
                string AccountNo = string.Empty;
                double Amount = 0.0;
                int PayMode;
                string Visited;
                string CreditCardNo;
                int iBank = 0;
                GridViewRow row = GrdViewSerVisit.SelectedRow;

                int ChequeBookID = Convert.ToInt32(GrdViewSerVisit.SelectedDataKey.Value);

                string BankName = string.Empty;
                string FromNo = string.Empty;
                string ToNo = string.Empty;
                int BankID = 0;

                string Username = Request.Cookies["LoggedUserName"].Value;
                AccountNo = txtAccNoAdd.Text;
                string Types = "Update";

                FromNo = txtFromNoAdd.Text;
                ToNo = txtToNoAdd.Text;
                BankID = int.Parse(ddBankName.SelectedValue);

                BankName = ddBankName.SelectedItem.Text;
                string ddBankID = ddBankName.SelectedValue;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.IsChequeAlreadyEntered(connection, ddBankID, FromNo, ToNo))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given Cheque No already entered for this bank.');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                //if(bl.IsChequeNoNotLess(connection, BankID, FromNo, ToNo))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToCheque No Cannot be Less than FromChequeNo');", true);
                //    return;
                //}

                if (Convert.ToDouble(txtFromNoAdd.Text) > Convert.ToDouble(txtToNoAdd.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToCheque No Cannot be Less than FromChequeNo');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                if (Convert.ToDouble(txtFromNoAdd.Text) == Convert.ToDouble(txtToNoAdd.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('FromChequeNo Cannot be equal to ToCheque');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                try
                {
                    bl.UpdateCheque(connection, ChequeBookID, BankName, AccountNo, BankID, FromNo, ToNo, Username, Types);


                    string salestype = string.Empty;
                    int ScreenNo = 0;
                    string ScreenName = string.Empty;

                    salestype = "Cheque Book";
                    ScreenName = "Cheque Book";


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


                                    toAddress = dr["EmailId"].ToString();

                                    if (Email == true)
                                    {
                                        string body = "\n";


                                        int index123 = emailcontent.IndexOf("@Branch");
                                        body = Request.Cookies["Company"].Value;
                                        if (index123 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                        }

                                        int index132 = emailcontent.IndexOf("@Bank");
                                        body = BankName;
                                        if (index132 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);
                                        }

                                        int index312 = emailcontent.IndexOf("@User");
                                        body = Username;
                                        if (index312 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                        }

                                        int index2 = emailcontent.IndexOf("@AccountNo");
                                        body = AccountNo.ToString();
                                        if (index2 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                        }

                                        int index = emailcontent.IndexOf("@FromNo");
                                        body = FromNo;
                                        if (index >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index, 9).Insert(index, body);
                                        }

                                        int index1 = emailcontent.IndexOf("@ToNo");
                                        body = ToNo;
                                        if (index1 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index1, 7).Insert(index1, body);
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

                                        int index132 = smscontent.IndexOf("@Bank");
                                        body = BankName;

                                        if (index132 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index132, 10).Insert(index132, body);
                                        }

                                        int index312 = smscontent.IndexOf("@User");
                                        body = Username;
                                        if (index312 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                        }

                                        int index2 = smscontent.IndexOf("@AccountNo");
                                        body = AccountNo.ToString();
                                        if (index2 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                        }

                                        int index = smscontent.IndexOf("@FromNo");
                                        body = FromNo;
                                        if (index >= 0)
                                        {
                                            smscontent = smscontent.Remove(index, 9).Insert(index, body);
                                        }

                                        int index1 = smscontent.IndexOf("@ToNo");
                                        body = ToNo;
                                        if (index1 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index1, 7).Insert(index1, body);
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


                    //MyAccordion.Visible = true;
                    pnlVisitDetails.Visible = false;
                    lnkBtnAdd.Visible = true;
                    Reset();
                    GrdViewSerVisit.DataBind();
                    GrdViewSerVisit.Visible = true;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque Book Details Updated Successfully.');", true);
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

    private void loadBanks(string sDataSource)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanks();
        ddBankName.DataSource = ds;
        ddBankName.DataBind();
        ddBankName.DataTextField = "LedgerName";
        ddBankName.DataValueField = "LedgerID";
    }

    protected void DamageLeaf_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("DamageCheque.aspx");
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

    protected void UnusedLeaf_Click(object sender, EventArgs e)
    {
        try
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;

            string filename = "UnUsed Cheque Leaf.xls";
            string fLvlValueTemp = string.Empty;
            string tLvlValueTemp = string.Empty;

            DataTable dtf = new DataTable();
            DataColumn dc;
            DataRow drddd;
            DataSet itemDs = new DataSet();
            BusinessLogic bl = new BusinessLogic(GetConnectionString());

            DataSet ds = bl.ListUnusedLeaf(GetConnectionString());

            dc = new DataColumn("ChequeNo");
            dtf.Columns.Add(dc);

            dc = new DataColumn("Bank");
            dtf.Columns.Add(dc);

            itemDs.Tables.Add(dtf);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ChequeNo"));
            dt.Columns.Add(new DataColumn("Bank"));

            if (ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["FromChequeNo"].ToString().ToUpper().Trim();
                    tLvlValueTemp = dr["ToChequeNo"].ToString().ToUpper().Trim();

                    int difff = Convert.ToInt32(tLvlValueTemp) - Convert.ToInt32(fLvlValueTemp);
                    int g = 0;
                    int ChequeNo = 0;

                    for (int k = 0; k <= difff; k++)
                    {
                        if (g == 0)
                        {
                            drddd = itemDs.Tables[0].NewRow();
                            DataRow dr_final8 = dt.NewRow();
                            dr_final8["ChequeNo"] = fLvlValueTemp;
                            drddd["ChequeNo"] = Convert.ToString(fLvlValueTemp);
                            dr_final8["Bank"] = dr["BankName"];
                            drddd["Bank"] = dr["BankName"];
                            dt.Rows.Add(dr_final8);
                            ChequeNo = Convert.ToInt32(fLvlValueTemp) + 1;
                            itemDs.Tables[0].Rows.Add(drddd);
                        }
                        else
                        {
                            drddd = itemDs.Tables[0].NewRow();
                            DataRow dr_final8 = dt.NewRow();
                            dr_final8["ChequeNo"] = Convert.ToString(ChequeNo);
                            drddd["ChequeNo"] = Convert.ToString(ChequeNo);
                            dr_final8["Bank"] = dr["BankName"];
                            drddd["Bank"] = dr["BankName"];
                            dt.Rows.Add(dr_final8);
                            ChequeNo = ChequeNo + 1;
                            itemDs.Tables[0].Rows.Add(drddd);
                            g = 1;
                        }
                        g = 1;
                    }
                }
            }

            DataSet dsd = bl.ListusedLeaf(GetConnectionString());

            if (dsd != null)
            {
                if (dsd.Tables[0] != null)
                {
                    DataTable dtttt = dsd.Tables[0];

                    if (itemDs.Tables[0] != null)
                    {
                        foreach (DataRow drd in dsd.Tables[0].Rows)
                        {
                            var billNo = Convert.ToInt32(drd["ChequeNo"]);

                            for (int i = 0; i < itemDs.Tables[0].Rows.Count; i++)
                            {
                                if (billNo == Convert.ToInt32(itemDs.Tables[0].Rows[i]["ChequeNo"]))
                                {
                                    itemDs.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                        itemDs.Tables[0].AcceptChanges();
                    }
                }
            }

            DataSet dsddd = bl.ListDamageChequeInfo(GetConnectionString(), "", "");

            if (dsddd != null)
            {
                if (dsddd.Tables[0] != null)
                {
                    DataTable dttttt = dsddd.Tables[0];

                    if (itemDs.Tables[0] != null)
                    {
                        foreach (DataRow drdddd in dsddd.Tables[0].Rows)
                        {
                            var billNo = Convert.ToInt32(drdddd["ChequeNo"]);

                            for (int i = 0; i < itemDs.Tables[0].Rows.Count; i++)
                            {
                                //var billNoll = Convert.ToUInt32(itemDs.Tables[0].Rows[i]["ChequeNo"]);

                                if (billNo == Convert.ToInt32(itemDs.Tables[0].Rows[i]["ChequeNo"]))
                                {
                                    itemDs.Tables[0].Rows[i].Delete();
                                }
                            }
                            itemDs.Tables[0].AcceptChanges();
                        }
                        itemDs.Tables[0].AcceptChanges();
                    }
                }
            }

            if (itemDs != null)
            {
                if (itemDs.Tables[0] != null)
                {
                    DataTable dtt = itemDs.Tables[0];

                    if (dtt.Rows.Count > 0)
                    {
                        System.IO.StringWriter tw = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                        DataGrid dgGrid = new DataGrid();
                        dgGrid.DataSource = dtt;
                        dgGrid.DataBind();

                        //Get the HTML for the control.
                        dgGrid.RenderControl(hw);

                        //Write the HTML back to the browser.
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                        this.EnableViewState = false;
                        Response.Write(tw.ToString());
                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void Reset()
    {
        txtAccNoAdd.Text = "";
        txtFromNoAdd.Text = "";
        txtToNoAdd.Text = "";
        //txtDueDate.Text = "";
        //txtRefNo.Text = "";
        //txtVisitDate.Text = "";
        //drpPaymode.SelectedIndex = 0;
        //drpCustomer.SelectedIndex = 0;
        ddBankName.SelectedIndex = 0;

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //MyAccordion.Visible = true;
            pnlVisitDetails.Visible = false;
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

                if (bl.ChequeLeafUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;

                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "CHQMST"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "CHQMST"))
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
        GridViewRow row = GrdViewSerVisit.SelectedRow;
        try
        {

            int ChequeBookId = Convert.ToInt32(GrdViewSerVisit.SelectedDataKey.Value);

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            DataSet ds = bl.GetChequeInfoForId(sDataSource, ChequeBookId);

            if (ds != null)
            {
                hdVisitID.Value = Convert.ToString(ChequeBookId);

                txtAccNoAdd.Text = ds.Tables[0].Rows[0]["AccountNo"].ToString();

                if (ds.Tables[0].Rows[0]["BankID"] != null)
                {
                    ddBankName.ClearSelection();
                    ListItem cli = ddBankName.Items.FindByValue(HttpUtility.HtmlDecode(Convert.ToString(ds.Tables[0].Rows[0]["BankID"])));

                    if (cli != null) cli.Selected = true;
                }


                txtFromNoAdd.Text = ds.Tables[0].Rows[0]["FromChequeNo"].ToString();
                txtToNoAdd.Text = ds.Tables[0].Rows[0]["ToChequeNo"].ToString();



                UpdateButton.Visible = true;
                SaveButton.Visible = false;
                CancelButton.Visible = true;
                lnkBtnAdd.Visible = false;
                //MyAccordion.Visible = false;

                GrdViewSerVisit.Visible = false;
                pnlVisitDetails.Visible = true;

                ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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
            pnlVisitDetails.Visible = true;
            Reset();
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
                e.InputParameters["ChequeBookID"] = GrdViewSerVisit.SelectedDataKey.Value;

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

            e.InputParameters["Types"] = "Delete";

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;

            salestype = "Cheque Book";
            ScreenName = "Cheque Book";

            BusinessLogic bl = new BusinessLogic();

            string usernam = Request.Cookies["LoggedUserName"].Value;
            string connection = Request.Cookies["Company"].Value;

            string AccountNo = string.Empty;
            string BankName = string.Empty;
            double FromChequeNo = 0;
            double ToChequeNo = 0;
            DataSet ds = bl.GetChequeInfoForId(connection, int.Parse(GrdViewSerVisit.SelectedDataKey.Value.ToString()));
            if (ds != null)
            {
                BankName = Convert.ToString(ds.Tables[0].Rows[0]["TransDate"].ToString());
                AccountNo = Convert.ToString(ds.Tables[0].Rows[0]["DebtorID"]);
                FromChequeNo = Convert.ToDouble(ds.Tables[0].Rows[0]["FromChequeNo"]);
                ToChequeNo = Convert.ToDouble(ds.Tables[0].Rows[0]["paymode"]);
            }

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


                            toAddress = dr["EmailId"].ToString();

                            if (Email == true)
                            {
                                string body = "\n";


                                int index123 = emailcontent.IndexOf("@Branch");
                                body = Request.Cookies["Company"].Value;
                                if (index123 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                }

                                int index132 = emailcontent.IndexOf("@Bank");
                                body = BankName;
                                if (index132 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = emailcontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = emailcontent.IndexOf("@AccountNo");
                                body = AccountNo.ToString();
                                if (index2 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                }

                                int index = emailcontent.IndexOf("@FromNo");
                                body = FromChequeNo.ToString();
                                if (index >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index, 9).Insert(index, body);
                                }

                                int index1 = emailcontent.IndexOf("@ToNo");
                                body = ToChequeNo.ToString();
                                if (index1 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index1, 7).Insert(index1, body);
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

                                int index132 = emailcontent.IndexOf("@Bank");
                                body = BankName;
                                if (index132 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = emailcontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = smscontent.IndexOf("@AccountNo");
                                body = AccountNo.ToString();
                                if (index2 >= 0)
                                {
                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                }

                                int index = smscontent.IndexOf("@FromNo");
                                body = FromChequeNo.ToString();
                                if (index >= 0)
                                {
                                    smscontent = smscontent.Remove(index, 9).Insert(index, body);
                                }

                                int index1 = smscontent.IndexOf("@ToNo");
                                body = ToChequeNo.ToString();
                                if (index1 >= 0)
                                {
                                    smscontent = smscontent.Remove(index1, 7).Insert(index1, body);
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
            if (Page.IsValid)
            {
                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                int CustomerID = 0;
                int ServiceID = 0;
                DateTime DueDate;
                DateTime VisitDate;
                string AccountNo = string.Empty;
                double Amount = 0.0;
                int PayMode;
                string BankName = string.Empty;
                string FromNo = string.Empty;
                string ToNo = string.Empty;
                string BankID = string.Empty;

                string Username = Request.Cookies["LoggedUserName"].Value;
                AccountNo = txtAccNoAdd.Text;
                string Types = "New";

                FromNo = txtFromNoAdd.Text.Trim();
                ToNo = txtToNoAdd.Text.Trim();
                BankID = ddBankName.SelectedValue;

                int ddBankID = 0;
                ddBankID = int.Parse(ddBankName.SelectedValue);

                BankName = ddBankName.SelectedItem.Text;

                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.IsChequeAlreadyEntered(connection, BankID, FromNo, ToNo))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given Cheque No already entered for this bank.');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                //if(bl.IsChequeNoNotLess(connection, BankID, FromNo, ToNo))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToCheque No Cannot be Less than FromChequeNo');", true);
                //    return;
                //}

                if (Convert.ToDouble(txtFromNoAdd.Text) > Convert.ToDouble(txtToNoAdd.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToCheque No Cannot be Less than FromChequeNo');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                if (Convert.ToDouble(txtFromNoAdd.Text) == Convert.ToDouble(txtToNoAdd.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('FromChequeNo Cannot be equal to ToCheque');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                try
                {
                    bl.InsertCheque(connection, BankName, AccountNo, ddBankID, FromNo, ToNo, Username, Types);

                    //MyAccordion.Visible = true;
                    pnlVisitDetails.Visible = false;
                    lnkBtnAdd.Visible = true;
                    Reset();
                    GrdViewSerVisit.DataBind();
                    GrdViewSerVisit.Visible = true;

                    string salestype = string.Empty;
                    int ScreenNo = 0;
                    string ScreenName = string.Empty;

                    salestype = "Cheque Book";
                    ScreenName = "Cheque Book";

                    
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

                                    
                                        toAddress = dr["EmailId"].ToString();
                                   
                                    if (Email == true)
                                    {
                                        string body = "\n";
                                       

                                        int index123 = emailcontent.IndexOf("@Branch");
                                        body = Request.Cookies["Company"].Value;
                                        if (index123 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                        }

                                        int index132 = emailcontent.IndexOf("@Bank");
                                        body = BankName;
                                        if (index132 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);
                                        }

                                        int index312 = emailcontent.IndexOf("@User");
                                        body = Username;
                                        if (index312 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                        }

                                        int index2 = emailcontent.IndexOf("@AccountNo");
                                        body = AccountNo.ToString();
                                        if (index2 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                        }

                                        int index = emailcontent.IndexOf("@FromNo");
                                        body = FromNo;
                                        if (index >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index, 9).Insert(index, body);
                                        }

                                        int index1 = emailcontent.IndexOf("@ToNo");
                                        body = ToNo;
                                        if (index1 >= 0)
                                        {
                                            emailcontent = emailcontent.Remove(index1, 7).Insert(index1, body);
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

                                        int index132 = smscontent.IndexOf("@Bank");
                                        body = BankName;
                                        if (index132 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index132, 10).Insert(index132, body);
                                        }

                                        int index312 = smscontent.IndexOf("@User");
                                        body = Username;
                                        if (index312 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                        }

                                        int index2 = smscontent.IndexOf("@AccountNo");
                                        body = AccountNo.ToString();
                                        if (index2 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                        }

                                        int index = smscontent.IndexOf("@FromNo");
                                        body = FromNo;
                                        if (index >= 0)
                                        {
                                            smscontent = smscontent.Remove(index, 9).Insert(index, body);
                                        }

                                        int index1 = smscontent.IndexOf("@ToNo");
                                        body = ToNo;
                                        if (index1 >= 0)
                                        {
                                            smscontent = smscontent.Remove(index1, 7).Insert(index1, body);
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


                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque Book Details Saved Successfully.');", true);
                    
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured: " + ex.Message + "')", true);
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
