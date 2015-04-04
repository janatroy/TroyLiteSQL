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
using SMSLibrary;

public partial class CreditDebitNote : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
        try
        {
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

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    GrdViewNote.Columns[6].Visible = false;
                    GrdViewNote.Columns[7].Visible = false;
                }


                GrdViewNote.PageSize = 9;

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "CDNOTE"))
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


                //string connection = Request.Cookies["Company"].Value;
                BusinessLogic bll = new BusinessLogic();
                if (!bll.IsLedgerFound(connection))
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Create a Ledger with LedgerName - CreditDebitNoteId.');", true);
                    //return;

                    HiddenField1.Value = "1";
                }
                else
                {
                    HiddenField1.Value = "0";
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


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }

    protected void GrdViewNote_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow Row = GrdViewNote.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();
            string recondate = Row.Cells[2].Text;
            hdPayment.Value = Convert.ToString(GrdViewNote.SelectedDataKey.Value);

            var receivedBill = bl.IsAmountPaidForBill(connection, hdPayment.Value);

            if (receivedBill != string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                return;
            }
            else if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.ReadOnly);
                return;
            }


            else
            {
                frmViewAdd.Visible = true;
                frmViewAdd.DataBind();
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewNote_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Select")
        //{
        //    frmViewAdd.Visible = true;
        //    frmViewAdd.ChangeMode(FormViewMode.Edit);
        //    GrdViewNote.Columns[8].Visible = false;
        //    lnkBtnAdd.Visible = false;

        //    //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
        //    //    Accordion1.SelectedIndex = 1;
        //}
    }
    protected void GrdViewNote_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewNote, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewNote_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gridView = (GridView)sender;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                int cellIndex = -1;
                foreach (DataControlField field in gridView.Columns)
                {
                    if (field.SortExpression == gridView.SortExpression)
                    {
                        cellIndex = gridView.Columns.IndexOf(field);
                    }
                    else if (field.SortExpression != "")
                    {
                        e.Row.Cells[gridView.Columns.IndexOf(field)].CssClass = "headerstyle";
                    }

                }

                if (cellIndex > -1)
                {
                    //  this is a header row,
                    //  set the sort style
                    e.Row.Cells[cellIndex].CssClass =
                        gridView.SortDirection == SortDirection.Ascending
                        ? "sortascheaderstyle" : "sortdescheaderstyle";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic();

                //BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEditNote(connection, usernam, "CDNOTE"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDeleteNote(connection, usernam, "CDNOTE"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveViewNote(connection, usernam, "CDNOTE"))
                {
                    ((Image)e.Row.FindControl("lnkprint")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = true;
                }
                //else
                //{
                //    ((ImageButton)e.Row.FindControl("btnViewDisabled")).Visible = false;
                //}
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
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();



            frmViewAdd.ChangeMode(FormViewMode.Insert);
            frmViewAdd.Visible = true;

            //BusinessLogic bl = new BusinessLogic(connStr);
            //string connection = Request.Cookies["Company"].Value;

            if (HiddenField2.Value == "1")
            {
                string usernam = Request.Cookies["LoggedUserName"].Value;
                bl.InsertLedgerInfo(connection, "CreditDebitNoteId", "CreditDebitNoteId", 1, 0, 0, 0, "", "CreditDebitNoteId", "", "", "", "", "Customer", 0, "", "", "NO", "NO", "NO", "CreditDebitNoteId", usernam, "NO", "", 3, "", "All");
                HiddenField1.Value = "1";
            }

            // ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");

            ModalPopupExtender1.Show();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }



    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        ddCriteria.SelectedIndex = 0;
    }


    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewNote.Visible = true;
            //MyAccordion.Visible = true;
            lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }

    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setUpdateParameters(e);

            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bll = new BusinessLogic();
            //string recondate = ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text;

            string recondate = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text;

            if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to Update Note with this Date. Please contact Supervisor.');", true);
                return;
            }

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;


            salestype = "Credit/Debit Note";
            ScreenName = "Credit/Debit Note";

            string emailcontent = string.Empty;
            BusinessLogic bl = new BusinessLogic();

            bool mobile = false;
            bool Email = false;
            string emailsubject = string.Empty;

            string usernam = Request.Cookies["LoggedUserName"].Value;
            if (hdEmailRequired.Value == "YES")
            {
                DataSet dsd = bl.GetLedgerInfoForId(connection, Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedValue));
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
                                else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier") || (dr["Name1"].ToString() == "Bank") || (dr["Name1"].ToString() == "Expense"))
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

                                int index132 = emailcontent.IndexOf("@Narration");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtNarration")).Text;
                                emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);

                                int index312 = emailcontent.IndexOf("@User");
                                body = usernam;
                                emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);

                                int index2 = emailcontent.IndexOf("@Date");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text;
                                emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);

                                int index = emailcontent.IndexOf("@Ledger");
                                body = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedItem.Text;
                                emailcontent = emailcontent.Remove(index, 7).Insert(index, body);

                                int index1 = emailcontent.IndexOf("@Amount");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtAmount")).Text;
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
                DataSet dsd = bl.GetLedgerInfoForId(connection, Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedValue));
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
                                else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier"))
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

                                int index132 = smscontent.IndexOf("@Narration");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtNarration")).Text;
                                smscontent = smscontent.Remove(index132, 10).Insert(index132, body);

                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                smscontent = smscontent.Remove(index312, 5).Insert(index312, body);

                                int index2 = smscontent.IndexOf("@Date");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text;
                                smscontent = smscontent.Remove(index2, 5).Insert(index2, body);

                                int index = smscontent.IndexOf("@Ledger");
                                body = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedItem.Text;
                                emailcontent = smscontent.Remove(index, 7).Insert(index, body);

                                int index1 = smscontent.IndexOf("@Amount");
                                body = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtAmount")).Text;
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

    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (true)
            {
                this.setInsertParameters(e);

                string connection = Request.Cookies["Company"].Value;
                BusinessLogic bll = new BusinessLogic();
                //string recondate = ((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text;

                string recondate = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text;

                ViewState.Add("TransDate", recondate);

                if (!bll.IsValidDate(connection, Convert.ToDateTime(recondate)))
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to Insert Note with this Date. Please contact Supervisor.');", true);
                    return;
                }

                string salestype = string.Empty;
                int ScreenNo = 0;
                string ScreenName = string.Empty;

                string content = string.Empty;
                string Subject1 = string.Empty;
                string emailcontent = string.Empty;

                salestype = "Credit/Debit Note";
                ScreenName = "Credit/Debit Note";

                BusinessLogic bl = new BusinessLogic();

                bool mobile = false;
                bool Email = false;
                string emailsubject = string.Empty;

                string usernam = Request.Cookies["LoggedUserName"].Value;
                if (hdEmailRequired.Value == "YES")
                {
                    DataSet dsd = bl.GetLedgerInfoForId(connection, Convert.ToInt32(ComboBox2Add.SelectedValue));
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
                                    else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier") || (dr["Name1"].ToString() == "Bank") || (dr["Name1"].ToString() == "Expense"))
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

                                    int index132 = emailcontent.IndexOf("@Narration");
                                    body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtNarrationAdd")).Text;
                                    emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);

                                    int index312 = emailcontent.IndexOf("@User");
                                    body = usernam;
                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);

                                    int index2 = emailcontent.IndexOf("@Date");
                                    body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text;
                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);

                                    int index = emailcontent.IndexOf("@Ledger");
                                    body = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).SelectedItem.Text;
                                    emailcontent = emailcontent.Remove(index, 9).Insert(index, body);

                                    int index1 = emailcontent.IndexOf("@Amount");
                                    body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtAmountAdd")).Text;
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
                    DataSet dsd = bl.GetLedgerInfoForId(connection, Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("ComboBox2Add")).SelectedValue));
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
                                    else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier"))
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

                                    int index132 = smscontent.IndexOf("@Narration");
                                    body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtNarrationAdd")).Text;
                                    smscontent = smscontent.Remove(index132, 10).Insert(index132, body);

                                    int index312 = smscontent.IndexOf("@User");
                                    body = usernam;
                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);

                                    int index2 = smscontent.IndexOf("@Date");
                                    body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text;
                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);

                                    int index = smscontent.IndexOf("@Ledger");
                                    body = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).SelectedItem.Text;
                                    smscontent = smscontent.Remove(index, 9).Insert(index, body);

                                    int index1 = smscontent.IndexOf("@Amount");
                                    body = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtAmountAdd")).Text;
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
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmViewAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void frmViewAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                frmViewAdd.Visible = false;
                System.Threading.Thread.Sleep(1000);
                GrdViewNote.DataBind();
                ModalPopupExtender1.Hide();

            }
            else
            {
                StringBuilder script = new StringBuilder();
                script.Append("alert('You are not allowed to Update this record. Please contact Supervisor.');");

                if (e.Exception.InnerException != null)
                {
                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") == -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);
                }

                e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                lnkBtnAdd.Visible = false;
                frmViewAdd.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                frmViewAdd.Visible = false;
                System.Threading.Thread.Sleep(1000);
                GrdViewNote.DataBind();
                //ModalPopupExtender1.Hide();
                GrdViewNote.Visible = true;
                lnkBtnAdd.Visible = true;
            }
            else
            {
                e.KeepInEditMode = true;

                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to Update this record. Please contact Supervisor.');");

                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") == -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);

                    e.ExceptionHandled = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {

    }
    protected void frmViewAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewNote.Visible = true;
            frmViewAdd.Visible = false;
            lnkBtnAdd.Visible = true;
            //MyAccordion.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")) != null)
            e.InputParameters["LedgerID"] = ((DropDownList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("ComboBox2")).SelectedValue;

        //if (((DropDownList)this.frmViewAdd.FindControl("ComboBox2")) != null)
        //    e.InputParameters["LedgerID"] = ((DropDownList)this.frmViewAdd.FindControl("ComboBox2")).SelectedValue;

        //if (((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text != "")
        //    e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtRefNo")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtRefNo")).Text;

        //if (((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text != "")
        //    e.InputParameters["NoteDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text != "")
            e.InputParameters["NoteDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtTransDate")).Text);

        if (((RadioButtonList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("rdoCDType")) != null)
            e.InputParameters["CDType"] = ((RadioButtonList)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("rdoCDType")).SelectedValue;

        //if (((RadioButtonList)this.frmViewAdd.FindControl("rdoCDType")) != null)
        //{
        //    e.InputParameters["CDType"] = ((RadioButtonList)this.frmViewAdd.FindControl("rdoCDType")).SelectedValue;
        //}


        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtAmount")).Text;


        //if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
        //    e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        //if (((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text != "")
        //    e.InputParameters["Note"] = ((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtNarration")).Text != "")
            e.InputParameters["Note"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("txtNarration")).Text;


        //if (((HiddenField)this.frmViewAdd.FindControl("hdNoteID")) != null)
        //    e.InputParameters["NoteID"] = ((HiddenField)this.frmViewAdd.FindControl("hdNoteID")).Value;

        if (((HiddenField)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("hdNoteID")) != null)
            e.InputParameters["NoteID"] = ((HiddenField)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditMain").FindControl("hdNoteID")).Value;


        e.InputParameters["TransNo"] = Convert.ToInt32(GrdViewNote.SelectedDataKey.Value);

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

        if (((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditAddTab").FindControl("txtBill")).Text != "")
            e.InputParameters["BillNo"] = ((TextBox)this.frmViewAdd.FindControl("tabEdit").FindControl("tabEditAddTab").FindControl("txtBill")).Text;

    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")) != null)
            e.InputParameters["LedgerID"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("ComboBox2Add")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtRefNoAdd")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtRefNoAdd")).Text;

        //if (((DropDownList)this.frmViewAdd.FindControl("ComboBox2Add")) != null)
        //    e.InputParameters["LedgerID"] = ((DropDownList)this.frmViewAdd.FindControl("ComboBox2Add")).SelectedValue;

        //if (((TextBox)this.frmViewAdd.FindControl("txtRefNoAdd")).Text != "")
        //    e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNoAdd")).Text;

        //if (((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text != "")
        //    e.InputParameters["NoteDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text != "")
            e.InputParameters["NoteDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtTransDateAdd")).Text);

        //if (((RadioButtonList)this.frmViewAdd.FindControl("rdoCDTypeAdd")) != null)
        //{
        //    e.InputParameters["CDType"] = ((RadioButtonList)this.frmViewAdd.FindControl("rdoCDTypeAdd")).SelectedValue;
        //}

        if (((RadioButtonList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("rdoCDTypeAdd")).Text != null)
            e.InputParameters["CDType"] = ((RadioButtonList)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("rdoCDTypeAdd")).SelectedValue;


        //if (((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text != "")
        //    e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtAmountAdd")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtAmountAdd")).Text;


        //if (((TextBox)this.frmViewAdd.FindControl("txtNarrationAdd")).Text != "")
        //    e.InputParameters["Note"] = ((TextBox)this.frmViewAdd.FindControl("txtNarrationAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtNarrationAdd")).Text != "")
            e.InputParameters["Note"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsMain").FindControl("txtNarrationAdd")).Text;


        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsAddTab").FindControl("txtBillAdd")).Text != "")
            e.InputParameters["BillNo"] = ((TextBox)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsAddTab").FindControl("txtBillAdd")).Text;
        else
            e.InputParameters["BillNo"] = string.Empty;

        //((Button)this.frmViewAdd.FindControl("tablInsert").FindControl("tabInsAddTab").FindControl("InsertButton")).Attributes["onclick"] = "javascript:return ConfirmSave();";
    }

    protected void ComboBox2_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            //FormView frmV = (FormView)ddl.NamingContainer;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string debtorID = ((DataRowView)frmV.DataItem)["LedgerID"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(debtorID);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddBanks_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string creditorID = ((DataRowView)frmV.DataItem)["CreditorID"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(creditorID);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkPayToAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            if (chk.SelectedItem.Text == "Cheque")
            {
                Panel test = (Panel)frmViewAdd.FindControl("PanelBankAdd");
                test.Visible = true;
            }
            else
            {
                Panel test = (Panel)frmViewAdd.FindControl("PanelBankAdd");
                test.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void chkPayTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            if (chk.SelectedItem.Text == "Cheque")
            {
                Panel test = (Panel)frmViewAdd.FindControl("PanelBank");

                if (test != null)
                    test.Visible = true;
            }
            else
            {
                Panel test = (Panel)frmViewAdd.FindControl("PanelBank");

                if (test != null)
                    test.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void chkPayTo_DataBound(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)chk.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string paymode = ((DataRowView)frmV.DataItem)["paymode"].ToString();
                chk.ClearSelection();

                ListItem li = chk.Items.FindByValue(paymode);
                if (li != null) li.Selected = true;

            }

            if (chk.SelectedItem != null)
            {
                if (chk.SelectedItem.Text == "Cheque")
                {
                    //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                    HtmlTable table = (HtmlTable)((Panel)frmV.FindControl("PanelBank")).FindControl("tblBank");

                    if (table != null)
                        table.Attributes.Add("class", "AdvancedSearch");
                }
                else
                {
                    if (frmV.FindControl("PanelBank") != null)
                    {
                        HtmlTable table = (HtmlTable)frmV.FindControl("PanelBank").FindControl("tblBank");

                        if (table != null)
                            table.Attributes.Add("class", "hidden");
                    }
                }
            }
            else
            {
                //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                //test.Visible = false;
                HtmlTable table = (HtmlTable)frmV.FindControl("tblBank");
                table.Attributes.Add("class", "hidden");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void rdoCDType_DataBound(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            //FormView frmV = (FormView)chk.NamingContainer;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)chk.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string paymode = ((DataRowView)frmV.DataItem)["CDType"].ToString();
                chk.ClearSelection();

                ListItem li = chk.Items.FindByValue(paymode);
                if (li != null) li.Selected = true;

                if (this.frmViewAdd.FindControl("pnlHeader") != null)
                {
                    if (chk.SelectedItem.Text == "Credit")
                        ((Label)this.frmViewAdd.FindControl("pnlHeader")).Text = "Credit Note";
                    else
                        ((Label)this.frmViewAdd.FindControl("pnlHeader")).Text = "Debit Note";
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmViewAdd_ItemCreated(object sender, EventArgs e)
    {
        try
        {
            if (this.frmViewAdd.FindControl("txtTransDateAdd") != null)
            {
                if (ViewState["TransDate"] == null)
                {
                    //((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");

                    DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                    string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                    ((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text = dtaa;
                }
                else
                    ((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text = ViewState["TransDate"].ToString();
            }

            if (this.frmViewAdd.FindControl("myRangeValidatorAdd") != null)
            {
                ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidatorAdd")).MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidatorAdd")).MaximumValue = System.DateTime.Now.ToShortDateString();
            }


            if (this.frmViewAdd.FindControl("myRangeValidator") != null)
            {
                ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidator")).MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                ((RangeValidator)this.frmViewAdd.FindControl("myRangeValidator")).MaximumValue = System.DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            rvSearch.Enabled = true;
            Page.Validate();

            if (Page.IsValid)
            {
                GrdViewNote.DataBind();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewNote_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GrdViewNote.SelectedIndex = e.RowIndex;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    bool check = false;
    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic();
            string connection = Request.Cookies["Company"].Value;
            hdPayment.Value = Convert.ToString(GrdViewNote.SelectedDataKey.Value);


            var receivedBill = bl.IsAmountPaidForBill(connection, hdPayment.Value);

            if (receivedBill != string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Bill has been cleared against the Payment made by Customer. Please unallocate the Bill from the Payment. Receipt Trans. No. is " + receivedBill + "')", true);
                check = true;
                e.Cancel = true;
                return;
            }
            else
            {

                if (GrdViewNote.SelectedDataKey != null)
                    e.InputParameters["TransNo"] = Convert.ToInt32(GrdViewNote.SelectedDataKey.Value);


                e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

                string salestype = string.Empty;
                int ScreenNo = 0;
                string ScreenName = string.Empty;

                string content = string.Empty;
                string Subject1 = string.Empty;
                string emailcontent = string.Empty;

                salestype = "Credit/Debit Note";
                ScreenName = "Credit/Debit Note";

                //BusinessLogic bl = new BusinessLogic();
                //string connection = Request.Cookies["Company"].Value;

                int DebitorID = 0;
                int TransNo = 0;
                string Note = string.Empty;

                string TransDate = string.Empty;
                double Amount = 0;
                string PayTo = string.Empty;
                DataSet ds = bl.GetCreditDebitNoteForId(connection, int.Parse(GrdViewNote.SelectedDataKey.Value.ToString()));
                if (ds != null)
                {
                    TransDate = Convert.ToString(ds.Tables[0].Rows[0]["TransDate"].ToString());
                    Note = Convert.ToString(ds.Tables[0].Rows[0]["Note"].ToString());
                    DebitorID = Convert.ToInt32(ds.Tables[0].Rows[0]["LedgerID"]);
                    Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"]);
                    TransNo = Convert.ToInt32(ds.Tables[0].Rows[0]["TransNo"]);
                }


                bool mobile = false;
                bool Email = false;
                string emailsubject = string.Empty;

                string usernam = Request.Cookies["LoggedUserName"].Value;
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
                                    else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier") || (dr["Name1"].ToString() == "Bank") || (dr["Name1"].ToString() == "Expense"))
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

                                    int index132 = emailcontent.IndexOf("@Narration");
                                    body = Note;
                                    emailcontent = emailcontent.Remove(index132, 10).Insert(index132, body);

                                    int index312 = emailcontent.IndexOf("@User");
                                    body = usernam;
                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);

                                    int index2 = emailcontent.IndexOf("@Date");
                                    body = TransDate;
                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);

                                    //int index = emailcontent.IndexOf("@Ledger");
                                    //body = ComboBox2Add.SelectedItem.Text;
                                    //emailcontent = emailcontent.Remove(index, 9).Insert(index, body);

                                    int index1 = emailcontent.IndexOf("@Amount");
                                    body = Amount.ToString();
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
                                    else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier") || (dr["Name1"].ToString() == "Bank") || (dr["Name1"].ToString() == "Expense"))
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

                                    int index132 = smscontent.IndexOf("@Narration");
                                    body = Note;
                                    smscontent = smscontent.Remove(index132, 10).Insert(index132, body);

                                    int index312 = smscontent.IndexOf("@User");
                                    body = usernam;
                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);

                                    int index2 = smscontent.IndexOf("@Date");
                                    body = TransDate;
                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);

                                    //int index = emailcontent.IndexOf("@Ledger");
                                    //body = ComboBox2Add.SelectedItem.Text;
                                    //emailcontent = emailcontent.Remove(index, 9).Insert(index, body);

                                    int index1 = smscontent.IndexOf("@Amount");
                                    body = Amount.ToString();
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
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_DataBound(object sender, EventArgs e)
    {

    }
    protected void frmViewAdd_ModeChanged(object sender, EventArgs e)
    {

    }

    private bool CheckDate(DateTime dateTime)
    {
        BusinessLogic objBus = new BusinessLogic();
        return objBus.IsValidDate(Session["Connection"].ToString(), dateTime);
    }
    protected void GrdViewNote_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                if (check == false)
                {
                    GrdViewNote.DataBind();
                }
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to delete the record. Please contact Supervisor.');");

                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                    e.ExceptionHandled = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            //if (e.Exception.InnerException != null)
            //{
            //    StringBuilder script = new StringBuilder();
            //    script.Append("confirm('Already Save note for selected customer for selected date.Do you want to continue?');");

            //    if (e.Exception.InnerException.Message.IndexOf("Already entered") > -1)
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "Confirm()", true);
            //    e.ExceptionHandled = true;               
            //}

            if (e.Exception == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Note Saved Successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Note Updated Successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
