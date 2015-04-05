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

public partial class SupplierReceipt : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!Page.IsPostBack)
            {
                string connStr = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                CheckSMSRequired();

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    GrdViewReceipt.Columns[8].Visible = false;
                    GrdViewReceipt.Columns[7].Visible = false;
                }


                GrdViewReceipt.PageSize = 8;


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "SUPPRCT"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New ";
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
            if (ddCriteria.SelectedItem.Text == "Transaction Date")
            {
                //txtdate.EnableViewState = 1;
                txtdate.Enabled = true;
            }
            else
            {
                txtdate.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void btnrec_Click(object sender, EventArgs e)
    {
        try
        {
            string SupRec = "SupRec";
            Response.Write("<script language='javascript'> window.open('ReportExcelReceipts.aspx?ID=" + SupRec + "' , 'window','toolbar=no,status=no,menu=no,location=no,height=320,width=700,left=320,top=220 ,resizable=yes, scrollbars=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpBranch_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string creditorID = ((DataRowView)frmV.DataItem)["BranchCode"].ToString();

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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
        GridSource.SelectParameters.Add(new CookieParameter("branch", "Branch"));
    }

    protected void GrdViewReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            /*//MyAccordion.Visible = false;
            frmViewAdd.Visible = true;
            frmViewAdd.DataBind();
            frmViewAdd.ChangeMode(FormViewMode.Edit);
            //GrdViewReceipt.Columns[7].Visible = false;
            lnkBtnAdd.Visible = false;
            GrdViewReceipt.Visible = false;
            //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
                //Accordion1.SelectedIndex = 1;*/
        }
    }

    protected void GrdViewReceipt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow Row = GrdViewReceipt.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();
            string recondate = Row.Cells[2].Text;
            //hd.Value = Convert.ToString(GrdViewReceipt.SelectedDataKey.Value);
            ModalPopupExtender1.Show();
            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.ReadOnly);
                return;

            }
            else
            {
                frmViewAdd.Visible = true;
                //GrdViewReceipt.Visible = false;
                ////MyAccordion.Visible = false;
                frmViewAdd.DataBind();
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                //lnkBtnAdd.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewReceipt_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewReceipt, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
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

                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "SUPPRCT"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "SUPPRCT"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveView(usernam, "SUPPRCT"))
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

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
            //    return;
            //}


            frmViewAdd.ChangeMode(FormViewMode.Insert);
            frmViewAdd.Visible = true;
            ModalPopupExtender1.Show();
            //if (frmViewAdd.CurrentMode == FormViewMode.Insert)
            //{
            //    //lnkBtnAdd.Visible = false;
            //    ////MyAccordion.Visible = false;
            //    //GrdViewReceipt.Visible = false;
            //}

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;

            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet dst = new DataSet();
            dst = bl.ListBranch(connection, usernam);
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).Items.Clear();
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).Items.Add(new ListItem("Select Branch", "0"));
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).DataSource = dst;
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).DataTextField = "BranchName";
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).DataValueField = "BranchCode";
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).DataBind();

            string sCustomer = string.Empty;

            DataSet ds = bl.GetBranch(connection, usernam);

            sCustomer = Convert.ToString(ds.Tables[0].Rows[0]["DefaultBranchCode"]);
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).ClearSelection();
            ListItem li = ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
            if (li != null) li.Selected = true;

            if (ds.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
            {
                ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).Enabled = true;
            }
            else
            {
                ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).Enabled = false;
            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmViewAdd_DataBound(object sender, EventArgs e)
    {
        frmViewAdd_ModeChanged(sender, e);
    }
    protected void frmViewAdd_ModeChanged(object sender, EventArgs e)
    {
        if (frmViewAdd.CurrentMode == FormViewMode.Insert)
        {
            string usernam = Request.Cookies["LoggedUserName"].Value;

            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connection = Request.Cookies["Company"].Value;

            DataSet dst = bl.ListBranch(connection, usernam);
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).Items.Clear();
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).Items.Add(new ListItem("All", "All"));
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).DataSource = dst;
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).DataTextField = "BranchName";
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).DataValueField = "BranchCode";
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).DataBind();

            string sCustomer = string.Empty;

            DataSet ds = bl.GetBranch(connection, usernam);

            sCustomer = Convert.ToString(ds.Tables[0].Rows[0]["DefaultBranchCode"]);
            ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).ClearSelection();
            ListItem li = ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
            if (li != null) li.Selected = true;

            if (ds.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
            {
                ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).Enabled = true;
            }
            else
            {
                ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).Enabled = false;
            }
        }
    }

    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            ////MyAccordion.Visible = true;
            //lnkBtnAdd.Visible = true;
            frmViewAdd.Visible = false;
            GrdViewReceipt.Columns[8].Visible = true;
            GrdViewReceipt.Visible = true;
            ModalPopupExtender1.Hide();
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

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;


            salestype = "Supplier Receipt";
            ScreenName = "Supplier Receipt";
            string connection = Request.Cookies["Company"].Value;

            string emailcontent = string.Empty;
            BusinessLogic bl = new BusinessLogic();

            bool mobile = false;
            bool Email = false;
            string emailsubject = string.Empty;

            string usernam = Request.Cookies["LoggedUserName"].Value;
            if (hdEmailRequired.Value == "YES")
            {
                DataSet dsd = bl.GetLedgerInfoForId(connection, Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("ComboBox2")).SelectedValue));
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
                                else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier"))
                                {
                                    if (ModeofContact == 2)
                                    {
                                        toAddress = toAdd;
                                    }
                                    else
                                    {
                                        continue;
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
                                if (index123 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                }

                                int index132 = emailcontent.IndexOf("@Narration");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text;
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

                                int index2 = emailcontent.IndexOf("@Date");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text;
                                if (index2 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                }

                                int index = emailcontent.IndexOf("@Supplier");
                                body = ((DropDownList)this.frmViewAdd.FindControl("ComboBox2")).SelectedItem.Text;
                                if (index >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index, 9).Insert(index, body);
                                }

                                int index1 = emailcontent.IndexOf("@Amount");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;
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
                DataSet dsd = bl.GetLedgerInfoForId(connection, Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("ComboBox2")).SelectedValue));
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
                                        continue;
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
                                if (index123 >= 0)
                                {
                                    smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                }

                                int index132 = smscontent.IndexOf("@Narration");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text;
                                if (index132 >= 0)
                                {
                                    smscontent = smscontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = smscontent.IndexOf("@Date");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text;
                                if (index2 >= 0)
                                {
                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                }

                                int index = smscontent.IndexOf("@Supplier");
                                body = ((DropDownList)this.frmViewAdd.FindControl("ComboBox2")).SelectedItem.Text;
                                if (index >= 0)
                                {
                                    smscontent = smscontent.Remove(index, 9).Insert(index, body);
                                }

                                int index1 = smscontent.IndexOf("@Amount");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;
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

    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            this.setInsertParameters(e);

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;


            salestype = "Supplier Receipt";
            ScreenName = "Supplier Receipt";
            string connection = Request.Cookies["Company"].Value;

            string emailcontent = string.Empty;
            BusinessLogic bl = new BusinessLogic();

            bool mobile = false;
            bool Email = false;
            string emailsubject = string.Empty;

            string usernam = Request.Cookies["LoggedUserName"].Value;
            if (hdEmailRequired.Value == "YES")
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
                                else if ((dr["Name1"].ToString() == "Customer") || (dr["Name1"].ToString() == "Ledger") || (dr["Name1"].ToString() == "Supplier"))
                                {
                                    if (ModeofContact == 2)
                                    {
                                        toAddress = toAdd;
                                    }
                                    else
                                    {
                                        continue;
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
                                //string subject = "Added - Customer Receipt in Branch " + Request.Cookies["Company"].Value;

                                string body = "\n";


                                int index123 = emailcontent.IndexOf("@Branch");
                                body = Request.Cookies["Company"].Value;
                                if (index123 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                }

                                int index132 = emailcontent.IndexOf("@Narration");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtNarrationAdd")).Text;
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

                                int index2 = emailcontent.IndexOf("@Date");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text;
                                if (index2 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                }

                                int index = emailcontent.IndexOf("@Supplier");
                                body = ((DropDownList)this.frmViewAdd.FindControl("ComboBox2Add")).SelectedItem.Text;
                                if (index >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index, 9).Insert(index, body);
                                }

                                int index1 = emailcontent.IndexOf("@Amount");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text;
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
                                        continue;
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
                                if (index123 >= 0)
                                {
                                    smscontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                }

                                int index132 = smscontent.IndexOf("@Narration");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtNarrationAdd")).Text;
                                if (index132 >= 0)
                                {
                                    smscontent = smscontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = smscontent.IndexOf("@Date");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text;
                                if (index2 >= 0)
                                {
                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                }

                                int index = smscontent.IndexOf("@Supplier");
                                body = ((DropDownList)this.frmViewAdd.FindControl("ComboBox2Add")).SelectedItem.Text;
                                if (index >= 0)
                                {
                                    smscontent = smscontent.Remove(index, 9).Insert(index, body);
                                }
                                int index1 = smscontent.IndexOf("@Amount");
                                body = ((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text;
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

    protected void frmViewAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

    }
    protected void frmViewAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                if (hdSMS.Value == "YES")
                {
                    BusinessLogic bl = new BusinessLogic();
                    string conn = bl.CreateConnectionString(Request.Cookies["Company"].Value);

                    UtilitySMS utilSMS = new UtilitySMS(conn);
                    string UserID = Page.User.Identity.Name;

                    if (Session["Provider"] != null)
                        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), hdMobile.Value, hdText.Value, true, UserID);
                    else
                    {
                        if (hdMobile.Value != "")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                    }
                }

                ////MyAccordion.Visible = true;
                //lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
                GrdViewReceipt.Visible = true;
                GrdViewReceipt.DataBind();
                ModalPopupExtender1.Hide();
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to enter the payment with this date. Please contact Supervisor.');");

                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                    else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);

                    e.ExceptionHandled = true;
                    e.KeepInInsertMode = true;
                    lnkBtnAdd.Visible = false;
                    frmViewAdd.Visible = true;
                    GrdViewReceipt.Visible = true;
                }
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
            if (e.OutputParameters["NewTransNo"] != null)
            {
                if (e.OutputParameters["NewTransNo"].ToString() != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Receipt Updated Successfully. New Transaction No : " + e.OutputParameters["NewTransNo"].ToString() + "');", true);
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
            if (e.OutputParameters["NewTransNo"] != null)
            {
                if (e.OutputParameters["NewTransNo"].ToString() != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Receipt Saved Successfully. Transaction No : " + e.OutputParameters["NewTransNo"].ToString() + "');", true);
                }
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
                if (hdSMS.Value == "YES")
                {
                    BusinessLogic bl = new BusinessLogic();
                    string conn = bl.CreateConnectionString(Request.Cookies["Company"].Value);

                    UtilitySMS utilSMS = new UtilitySMS(conn);
                    string UserID = Page.User.Identity.Name;

                    if (Session["Provider"] != null)
                        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), hdMobile.Value, hdText.Value, true, UserID);
                    else
                    {
                        if (hdMobile.Value != "")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                    }
                }

                ////MyAccordion.Visible = true;
                //lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
                ////MyAccordion.Visible = true;
                ModalPopupExtender1.Hide();
                GrdViewReceipt.DataBind();
                GrdViewReceipt.Visible = true;
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to Update this record. Please contact Supervisor.');");

                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                    else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);

                    e.ExceptionHandled = true;
                    e.KeepInEditMode = true;
                    lnkBtnAdd.Visible = false;
                    //frmViewAdd.Visible = true;
                    //GrdViewReceipt.Visible = true;
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
        try
        {
            //if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cheque")
            //{
            //    string ChequeNo = string.Empty;
            //    ChequeNo = ((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text;
            //    int bankname = 0;
            //    bankname = Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("ddBanks")).SelectedValue);

            //    if ((ChequeNo == "") && (bankname == 0))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Name And Cheque No Mandatory');", true);
            //        ModalPopupExtender1.Show();
            //        return;
            //    }
            //    else if (ChequeNo == "")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No Mandatory');", true);
            //        ModalPopupExtender1.Show();
            //        return;
            //    }
            //    else if (bankname == 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Name Mandatory');", true);
            //        ModalPopupExtender1.Show();
            //        return;
            //    }
            //}
            if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cheque")
            {
                ((CompareValidator)this.frmViewAdd.FindControl("cvBank")).Enabled = true;
                ((RequiredFieldValidator)this.frmViewAdd.FindControl("rvChequeNo")).Enabled = true;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tblBank");

                if (table != null)
                    table.Attributes.Add("class", "AdvancedSearch");

            }
            else
            {
                ((CompareValidator)this.frmViewAdd.FindControl("cvBank")).Enabled = false;
                ((RequiredFieldValidator)this.frmViewAdd.FindControl("rvChequeNo")).Enabled = false;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tblBank");

                if (table != null)
                    table.Attributes.Add("class", "hidden");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            ////MyAccordion.Visible = true;
            frmViewAdd.Visible = false;
            //lnkBtnAdd.Visible = true;
            GrdViewReceipt.Visible = true;
            GrdViewReceipt.Columns[8].Visible = true;
            ModalPopupExtender1.Hide();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((DropDownList)this.frmViewAdd.FindControl("ComboBox2")) != null)
            e.InputParameters["CreditorID"] = ((DropDownList)this.frmViewAdd.FindControl("ComboBox2")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNo")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDate")).Text);

        if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cash")
        {
            e.InputParameters["DebitorID"] = "1";
            e.InputParameters["Paymode"] = "Cash";
            e.InputParameters["ChequeNo"] = "";
        }
        else if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayTo")).SelectedValue == "Cheque")
        {
            Panel bnkPanel = (Panel)this.frmViewAdd.FindControl("PanelBank");

            if (bnkPanel != null)
            {
                e.InputParameters["DebitorID"] = ((DropDownList)bnkPanel.FindControl("ddBanks")).SelectedValue;
                e.InputParameters["Paymode"] = "Cheque";
            }

            if (((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text != "")
                e.InputParameters["ChequeNo"] = ((TextBox)this.frmViewAdd.FindControl("txtChequeNo")).Text;

        }

        if (((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("txtNarration")).Text;

        e.InputParameters["VoucherType"] = "Receipt";

        e.InputParameters["TransNo"] = Convert.ToInt32(GrdViewReceipt.SelectedDataKey.Value);

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

        if (hdSMSRequired.Value == "YES")
        {

            if (((TextBox)this.frmViewAdd.FindControl("txtMobile")).Text != "")
                hdMobile.Value = ((TextBox)this.frmViewAdd.FindControl("txtMobile")).Text;

            hdText.Value = "Thank you for Payment of Rs." + ((TextBox)this.frmViewAdd.FindControl("txtAmount")).Text;

        }

        if (((DropDownList)this.frmViewAdd.FindControl("drpBranch")) != null)
            e.InputParameters["BranchCode"] = ((DropDownList)this.frmViewAdd.FindControl("drpBranch")).SelectedValue;
    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("ComboBox2Add")) != null)
            e.InputParameters["CreditorID"] = ((DropDownList)this.frmViewAdd.FindControl("ComboBox2Add")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("txtRefNoAdd")).Text != "")
            e.InputParameters["RefNo"] = ((TextBox)this.frmViewAdd.FindControl("txtRefNoAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text != "")
            e.InputParameters["TransDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text);

        ViewState.Add("TransDate", DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text).ToString("dd/MM/yyyy"));

        if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayToAdd")).SelectedValue == "Cash")
        {
            e.InputParameters["DebitorID"] = "1";
            e.InputParameters["Paymode"] = "Cash";
        }
        else if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayToAdd")).SelectedValue == "Cheque")
        {
            Panel bnkPanel = (Panel)this.frmViewAdd.FindControl("PanelBankAdd");

            if (bnkPanel != null)
            {
                e.InputParameters["DebitorID"] = ((DropDownList)bnkPanel.FindControl("ddBanksAdd")).SelectedValue;
                e.InputParameters["Paymode"] = "Cheque";
            }
        }

        if (((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text != "")
            e.InputParameters["Amount"] = ((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtNarrationAdd")).Text != "")
            e.InputParameters["Narration"] = ((TextBox)this.frmViewAdd.FindControl("txtNarrationAdd")).Text;

        if (((TextBox)this.frmViewAdd.FindControl("txtChequeNoAdd")).Text != "")
            e.InputParameters["ChequeNo"] = ((TextBox)this.frmViewAdd.FindControl("txtChequeNoAdd")).Text;
        else
            e.InputParameters["ChequeNo"] = "";

        e.InputParameters["VoucherType"] = "Receipt";

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

        //if (((TextBox)this.frmViewAdd.FindControl("txtPhoneAdd")).Text != "")
        //    e.InputParameters["Phone"] = ((TextBox)this.frmViewAdd.FindControl("txtPhoneAdd")).Text;

        if (hdSMSRequired.Value == "YES")
        {

            if (((TextBox)this.frmViewAdd.FindControl("txtMobileAdd")).Text != "")
                hdMobile.Value = ((TextBox)this.frmViewAdd.FindControl("txtMobileAdd")).Text;

            hdText.Value = "Thank you for Payment of Rs." + ((TextBox)this.frmViewAdd.FindControl("txtAmountAdd")).Text;

        }

        if (((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")) != null)
            e.InputParameters["BranchCode"] = ((DropDownList)this.frmViewAdd.FindControl("drpBranchAdd")).SelectedValue;

    }

    protected void ComboBox2_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string debtorID = ((DataRowView)frmV.DataItem)["CreditorID"].ToString();

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

    protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;
            FormView frmV = (FormView)ddl.NamingContainer;

            string debtorID = ddl.SelectedValue;
            BusinessLogic objBus = new BusinessLogic();

            string Mobile = objBus.GetLedgerMobileForId(Request.Cookies["Company"].Value, int.Parse(debtorID));

            ((TextBox)frmViewAdd.FindControl("txtMobile")).Text = Mobile;

            ((TextBox)frmViewAdd.FindControl("txtAmount")).Focus();

            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ComboBox2Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;
            FormView frmV = (FormView)ddl.NamingContainer;

            string debtorID = ddl.SelectedValue;
            BusinessLogic objBus = new BusinessLogic();

            string Mobile = objBus.GetLedgerMobileForId(Request.Cookies["Company"].Value, int.Parse(debtorID));

            ((TextBox)frmViewAdd.FindControl("txtMobileAdd")).Text = Mobile;
            ((TextBox)frmViewAdd.FindControl("txtAmountAdd")).Focus();


            ModalPopupExtender1.Show();
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

            FormView frmV = (FormView)ddl.NamingContainer;

            if (frmV.DataItem != null)
            {
                string creditorID = ((DataRowView)frmV.DataItem)["DebtorID"].ToString();

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

    protected void chkPayTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            if (chk.SelectedItem.Text == "Cheque")
            {
                Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                test.Visible = true;
            }
            else
            {
                Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                test.Visible = false;
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
            ModalPopupExtender1.Show();
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

            FormView frmV = (FormView)chk.NamingContainer;

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
                    HtmlTable table = (HtmlTable)((Panel)frmV.FindControl("PanelBank")).FindControl("tblBank");
                    if (table != null)
                        table.Attributes.Add("class", "hidden");
                }
            }
            else
            {
                //Panel test = (Panel)frmViewAdd.FindControl("PanelBank");
                //test.Visible = false;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("tblBank");
                table.Attributes.Add("class", "hidden");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void chkPayToAdd_DataBound(object sender, EventArgs e)
    {
        try
        {
            RadioButtonList chk = (RadioButtonList)sender;

            FormView frmV = (FormView)chk.NamingContainer;

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
                    HtmlTable table = (HtmlTable)((Panel)frmV.FindControl("PanelBankAdd")).FindControl("tblBankAdd");
                    if (table != null)
                        table.Attributes.Add("class", "AdvancedSearch");
                }
                else
                {
                    HtmlTable table = (HtmlTable)((Panel)frmV.FindControl("PanelBankAdd")).FindControl("tblBankAdd");

                    if (table != null)
                        table.Attributes.Add("class", "hidden");
                }
            }
            else
            {
                HtmlTable table = (HtmlTable)frmV.FindControl("PanelBankAdd").FindControl("tblBankAdd");

                if (table != null)
                    table.Attributes.Add("class", "hidden");
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
                    DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                    string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                    ((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text = dtaa;

                    //((TextBox)this.frmViewAdd.FindControl("txtTransDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
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
        try
        {
            //if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayToAdd")).SelectedValue == "Cheque")
            //{
            //    string ChequeNo = string.Empty;
            //    ChequeNo = ((TextBox)this.frmViewAdd.FindControl("txtChequeNoAdd")).Text;
            //    int bankname = 0;
            //    bankname = Convert.ToInt32(((DropDownList)this.frmViewAdd.FindControl("ddBanksAdd")).SelectedValue);

            //    if ((ChequeNo == "") && (bankname == 0))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Name And Cheque No Mandatory');", true);
            //        ModalPopupExtender1.Show();
            //        return;
            //    }
            //    else if (ChequeNo == "")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cheque No Mandatory');", true);
            //        ModalPopupExtender1.Show();
            //        return;
            //    }
            //    else if (bankname == 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bank Name Mandatory');", true);
            //        ModalPopupExtender1.Show();
            //        return;
            //    }
            //}
            if (((RadioButtonList)this.frmViewAdd.FindControl("chkPayToAdd")).SelectedValue == "Cheque")
            {
                ((CompareValidator)this.frmViewAdd.FindControl("cvBankAdd")).Enabled = true;
                ((RequiredFieldValidator)this.frmViewAdd.FindControl("rvChequeNoAdd")).Enabled = true;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("PanelBankAdd").FindControl("tblBankAdd");

                if (table != null)
                    table.Attributes.Add("class", "AdvancedSearch");

            }
            else
            {
                ((CompareValidator)this.frmViewAdd.FindControl("PanelBankAdd").FindControl("cvBankAdd")).Enabled = false;
                ((RequiredFieldValidator)this.frmViewAdd.FindControl("PanelBankAdd").FindControl("rvChequeNoAdd")).Enabled = false;
                HtmlTable table = (HtmlTable)frmViewAdd.FindControl("PanelBankAdd").FindControl("tblBankAdd");
                if (table != null)
                    table.Attributes.Add("class", "hidden");
            }
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
            rvSearch.Enabled = true;
            Page.Validate();

            if (Page.IsValid)
            {
                GrdViewReceipt.DataBind();
            }
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
            GrdViewReceipt.SelectedIndex = e.RowIndex;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdViewReceipt.SelectedDataKey != null)
                e.InputParameters["TransNo"] = Convert.ToInt32(GrdViewReceipt.SelectedDataKey.Value);

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;

            BusinessLogic bl = new BusinessLogic();

            string connection = Request.Cookies["Company"].Value;
            salestype = "Supplier Receipt";
            ScreenName = "Supplier Receipt";

            string usernam = Request.Cookies["LoggedUserName"].Value;

            int DebitorID = 0;
            string TransDate = string.Empty;
            double Amount = 0;
            string PayTo = string.Empty;
            DataSet ds = bl.GetReceiptForId(connection, int.Parse(GrdViewReceipt.SelectedDataKey.Value.ToString()));
            if (ds != null)
            {
                TransDate = Convert.ToString(ds.Tables[0].Rows[0]["TransDate"].ToString());
                DebitorID = Convert.ToInt32(ds.Tables[0].Rows[0]["DebtorID"]);
                Amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Amount"]);
                PayTo = ds.Tables[0].Rows[0]["paymode"].ToString();
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
                                else if (dr["Name1"].ToString() == "Supplier")
                                {
                                    if (ModeofContact == 2)
                                    {
                                        toAddress = toAdd;
                                    }
                                    else
                                    {
                                        continue;
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
                                if (index123 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                }

                                int index132 = emailcontent.IndexOf("@PayMode");
                                body = PayTo;
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

                                int index2 = emailcontent.IndexOf("@Date");
                                body = TransDate.ToString();
                                if (index2 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
                                }

                                //int index = emailcontent.IndexOf("@Customer");
                                //body = ddReceivedFrom.SelectedItem.Text;
                                //emailcontent = emailcontent.Remove(index, 9).Insert(index, body);

                                int index1 = emailcontent.IndexOf("@Amount");
                                body = Convert.ToString(Amount);
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
                                        continue;
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
                                if (index123 >= 0)
                                {
                                    smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                }

                                int index132 = smscontent.IndexOf("@PayMode");
                                body = PayTo;
                                if (index132 >= 0)
                                {

                                    smscontent = smscontent.Remove(index132, 10).Insert(index132, body);
                                }

                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = smscontent.IndexOf("@Date");
                                body = TransDate.ToString();
                                if (index2 >= 0)
                                {
                                    emailcontent = smscontent.Remove(index2, 5).Insert(index2, body);
                                }

                                //int index = emailcontent.IndexOf("@Customer");
                                //body = ddReceivedFrom.SelectedItem.Text;
                                //emailcontent = emailcontent.Remove(index, 9).Insert(index, body);

                                int index1 = smscontent.IndexOf("@Amount");
                                body = Convert.ToString(Amount);
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

    protected void GrdViewReceipt_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                GrdViewReceipt.DataBind();
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to delete the record. Please contact Administrator.');");

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

    private void EmptyRow()
    {
        var ds = new DataSet();
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        var dt = new DataTable();

        DataRow drNew;
        DataColumn dc;

        dc = new DataColumn("TransNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("RefNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("TransDate");
        dt.Columns.Add(dc);

        dc = new DataColumn("LedgerName");
        dt.Columns.Add(dc);

        dc = new DataColumn("Debi");
        dt.Columns.Add(dc);

        dc = new DataColumn("Amount");
        dt.Columns.Add(dc);

        dc = new DataColumn("Narration");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        drNew = dt.NewRow();

        drNew["TransNo"] = "";
        drNew["RefNo"] = "";
        drNew["TransDate"] = string.Empty;
        drNew["LedgerName"] = string.Empty;
        string textvalue = null;
        drNew["Debi"] = string.Empty;
        drNew["Amount"] = Convert.ToDouble(textvalue);
        drNew["Narration"] = string.Empty;
        
        ds.Tables[0].Rows.Add(drNew);

        ds.Tables[0].AcceptChanges();

        GrdViewReceipt.DataSource = ds;
        GrdViewReceipt.DataBind();

    }

    protected void ddCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCriteria.SelectedItem.Text == "Transaction Date")
        {
            //txtdate.EnableViewState = 1;
            txtdate.Enabled = true;
        }
        else
        {
            txtdate.Enabled = false;
        }
    }

}
