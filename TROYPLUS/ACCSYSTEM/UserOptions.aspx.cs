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
using SMSLibrary;

public partial class UserOptions : System.Web.UI.Page
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

                //txtpassword.Attributes["type"] = "password";
                GrdViewCust.PageSize = 8;
                loadEmp();
                loadBranch();
                //BindGrid();

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
                    GrdViewCust.Columns[2].Visible = false;
                    GrdViewCust.Columns[3].Visible = false;
                }


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "ULOCK"))
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
    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpBranch.Items.Clear();
        drpBranch.Items.Add(new ListItem("Select Branch", "0"));
        ds = bl.ListBranch();
        drpBranch.DataSource = ds;
        drpBranch.DataBind();
        drpBranch.DataTextField = "BranchName";
        drpBranch.DataValueField = "Branchcode";
    }


    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtUserName.Text = "";
            loadEmp();
            //ddlSearchCriteria.SelectedIndex = 0;
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
                //if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "CREDITEXD")
                //{
                //    Session["CREDITEXD"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString().Trim().ToUpper();
                //    hdCREDITEXD.Value = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString().Trim().ToUpper();
                //}

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
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtUserName.UniqueID, "Text"));
    }

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

    protected void GrdViewCust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //if (Page.IsValid)
        //{
        //    string connection = Request.Cookies["Company"].Value;
        //    string recondate = string.Empty;
        //    BusinessLogic bl = new BusinessLogic();

        //    string username = Convert.ToString(GrdViewCust.DataKeys[e.RowIndex].Value.ToString());

        //    string UserID = Page.User.Identity.Name;

        //    string usernam = Request.Cookies["LoggedUserName"].Value;

        //    bl.DeleteUserOptions(connection, username);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User and Options Deleted Successfully')", true);
        //    BindGrid();
        //}
    }

    private void BindGrid()
    {
        string connection = Request.Cookies["Company"].Value;

        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic();

        object usernam = Session["LoggedUserName"];

        string txtSearch = string.Empty;
        ds = bl.ListUsers(txtSearch, connection);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewCust.DataSource = ds.Tables[0].DefaultView;
                GrdViewCust.DataBind();
            }
        }
        else
        {
            GrdViewCust.DataSource = null;
            GrdViewCust.DataBind();
        }
    }

    protected void GrdViewCust_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "ULOCK"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "ULOCK"))
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

    protected void GrdViewCust_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewCust, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void GrdViewCust_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strPaymode = string.Empty;
            string username = string.Empty;
            GridViewRow row = GrdViewCust.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();

            username = Convert.ToString(GrdViewCust.SelectedDataKey.Value);
            lbloption.Text = "Edit";
            Session["Show"] = "Edit";


            DataSet ds = bl.GetUserOptionsForId(username, connection, "CUSTOMERS");
            GrdViewItem.DataSource = ds;
            GrdViewItem.DataBind();

            DataSet dst = bl.GetUserOptionsForId(username, connection, "SUPPLIERS");
            GridSupplier.DataSource = dst;
            GridSupplier.DataBind();

            DataSet dstd = bl.GetUserOptionsForId(username, connection, "BANKING");
            GridBANKING.DataSource = dstd;
            GridBANKING.DataBind();

            DataSet dstdd = bl.GetUserOptionsForId(username, connection, "EXPENSES");
            GridEXPENSES.DataSource = dstdd;
            GridEXPENSES.DataBind();

            DataSet dstfd = bl.GetUserOptionsForId(username, connection, "INVENTORY");
            GridINVENTORY.DataSource = dstfd;
            GridINVENTORY.DataBind();

            DataSet dstf = bl.GetUserOptionsForId(username, connection, "ACCOUNTS");
            GridACCOUNTS.DataSource = dstf;
            GridACCOUNTS.DataBind();

            DataSet dstddf = bl.GetUserOptionsForId(username, connection, "RESOURCE");
            GridRESOURCE.DataSource = dstddf;
            GridRESOURCE.DataBind();

            DataSet dstfdd = bl.GetUserOptionsForId(username, connection, "SERVICE");
            GridSERVICE.DataSource = dstfdd;
            GridSERVICE.DataBind();

            DataSet dstfdf = bl.GetUserOptionsForId(username, connection, "OTHER");
            GridOTHER.DataSource = dstfdf;
            GridOTHER.DataBind();

            DataSet dstdfd = bl.GetUserOptionsForId(username, connection, "REPORT");
            GridREPORT.DataSource = dstdfd;
            GridREPORT.DataBind();

            DataSet dstdfdd = bl.GetUserOptionsForId(username, connection, "SECURITY");
            GridSECURITY.DataSource = dstdfdd;
            GridSECURITY.DataBind();

            DataSet dstdfddlead = bl.GetUserOptionsForId(username, connection, "LEADMANAGEMENT");
            GridLead.DataSource = dstdfddlead;
            GridLead.DataBind();

            DataSet dstdfddmanu = bl.GetUserOptionsForId(username, connection, "MANUFACTURE");
            GridMANUFACTURE.DataSource = dstdfddmanu;
            GridMANUFACTURE.DataBind();

            DataSet dstdfddpro = bl.GetUserOptionsForId(username, connection, "PROJECT");
            GridPROJECT.DataSource = dstdfddpro;
            GridPROJECT.DataBind();

            DataSet dastdfdd = bl.GetUserOptionsForId(username, connection, "CONFIG");
            GridConfig.DataSource = dastdfdd;
            GridConfig.DataBind();

            DataSet dsd = bl.GetUserInfo(username, connection);

            if (dsd != null)
            {
                if (dsd.Tables[0].Rows.Count == 1)
                {
                    txtUser.Text = dsd.Tables[0].Rows[0]["username"].ToString();
                    txtUser.Enabled = false;
                    txtEmail.Text = dsd.Tables[0].Rows[0]["email"].ToString();
                    chkAccLocked.Checked = bool.Parse(dsd.Tables[0].Rows[0]["Locked"].ToString());
                    //txtpassword.Text = dsd.Tables[0].Rows[0]["userpwd"].ToString();
                    //txtconfirmpassword.Text = dsd.Tables[0].Rows[0]["userpwd"].ToString();
                    //txtpassword.TextMode = TextBoxMode.Password;
                    txtpassword.Attributes.Add("value", dsd.Tables[0].Rows[0]["userpwd"].ToString());
                    txtconfirmpassword.Attributes.Add("value", dsd.Tables[0].Rows[0]["userpwd"].ToString());
                    //txtpassword.Enabled = false;
                    //txtconfirmpassword.Enabled = false;
                    chkboxdatelock.Checked = bool.Parse(dsd.Tables[0].Rows[0]["DateLock"].ToString());
                    txtUserGroup.Text = dsd.Tables[0].Rows[0]["UserGroup"].ToString();
                    chkhidedeviation.Checked = bool.Parse(dsd.Tables[0].Rows[0]["HideDeviation"].ToString());
                    chkBranch.Checked = bool.Parse(dsd.Tables[0].Rows[0]["BranchCheck"].ToString());

                    if (dsd.Tables[0].Rows[0]["EmpNo"] != null)
                    {
                        string sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["EmpNo"]);
                        drpIncharge.ClearSelection();
                        ListItem li = drpIncharge.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                        if (li != null) li.Selected = true;
                    }

                    DataSet dsd1 = bl.GetUserbranch(username, connection);
                    if (dsd1.Tables[0].Rows[0]["DefaultBranchCode"] != null)
                    {
                        string sBranch = Convert.ToString(dsd1.Tables[0].Rows[0]["DefaultBranchCode"]);
                        drpBranch.ClearSelection();
                        ListItem li = drpBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sBranch));
                        if (li != null) li.Selected = true;
                    }
                }
            }

            //DataSet dsd;
            //DataTable dt;
            //DataRow drNew;

            //DataColumn dc;

            //dsd = new DataSet();

            //dt = new DataTable();

            //dc = new DataColumn("UserName");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("Role");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("RoleDesc");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("Section");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("Area");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("Add");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("Edit");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("Delete");
            //dt.Columns.Add(dc);

            //dc = new DataColumn("View");
            //dt.Columns.Add(dc);

            //dsd.Tables.Add(dt);
            //bool Add = false;
            //bool Edit = false;
            //bool Delete = false;

            //bool Views = false;

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    drNew = dt.NewRow();
            //    drNew["Role"] = dr["Role"].ToString();
            //    drNew["RoleDesc"] = dr["RoleDesc"].ToString();
            //    drNew["Section"] = dr["Section"].ToString();
            //    drNew["Area"] = dr["Area"].ToString();
            //    if (Convert.ToBoolean(dr["Add"]) == true)
            //    {
            //        CheckBox txt = (CheckBox)GrdViewItem.FindControl("chkboxAdd");
            //        if (txt.Checked)
            //        {
            //            Add = txt.Checked;
            //        }
            //        else
            //        {
            //            Add = false;
            //        }
            //    }
            //    else
            //    {
            //        Add = false;
            //    }
            //    drNew["Edit"] = Edit;
            //    drNew["Delete"] = Delete;
            //    drNew["View"] = Views;
            //    dsd.Tables[0].Rows.Add(drNew);
            //}



            ModalPopupGet.Show();
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
            DataSet ds = new DataSet();
            lbloption.Text = "New";

            string connection = string.Empty;
            string userid = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;

            txtUser.Enabled = true;
            txtpassword.Enabled = true;
            txtconfirmpassword.Enabled = true;
            txtpassword.Text = "";
            txtpassword.Attributes.Add("value", "");

            txtconfirmpassword.Text = "";
            txtconfirmpassword.Attributes.Add("value", "");

            drpIncharge.SelectedIndex = 0;
            drpBranch.SelectedIndex = 0;
            chkBranch.Checked = false;
            chkhidedeviation.Checked = false;
            txtUser.Text = "";
            chkAccLocked.Checked = false;

            txtUserGroup.Text = "";
            chkboxdatelock.Checked = false;
            txtEmail.Text = "";
            BusinessLogic objBus = new BusinessLogic();

            Session["Show"] = "Add New";

            ds = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "CUSTOMERS");
            GrdViewItem.DataSource = ds;
            GrdViewItem.DataBind();

            DataSet dst = new DataSet();
            DataSet dstt = new DataSet();
            DataSet dsttt = new DataSet();
            DataSet dstttt = new DataSet();
            DataSet dsttttt = new DataSet();
            DataSet dstttttt = new DataSet();
            DataSet dstttttttd = new DataSet();
            DataSet dstttttttdd = new DataSet();
            DataSet dstttttd = new DataSet();
            DataSet dstttttdf = new DataSet();
            DataSet dstttttdff = new DataSet();
            DataSet dstttttdffpro = new DataSet();
            DataSet dstttttdffmanu = new DataSet();
            DataSet dstttttdfflead = new DataSet();

            dst = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "SUPPLIERS");
            GridSupplier.DataSource = dst;
            GridSupplier.DataBind();

            dstt = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "BANKING");
            GridBANKING.DataSource = dstt;
            GridBANKING.DataBind();

            dsttt = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "EXPENSES");
            GridEXPENSES.DataSource = dsttt;
            GridEXPENSES.DataBind();

            dstttt = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "INVENTORY");
            GridINVENTORY.DataSource = dstttt;
            GridINVENTORY.DataBind();

            dsttttt = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "ACCOUNTS");
            GridACCOUNTS.DataSource = dsttttt;
            GridACCOUNTS.DataBind();

            dstttttt = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "RESOURCE");
            GridRESOURCE.DataSource = dstttttt;
            GridRESOURCE.DataBind();

            dstttttttdd = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "SERVICE");
            GridSERVICE.DataSource = dstttttttdd;
            GridSERVICE.DataBind();

            dstttttd = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "OTHER");
            GridOTHER.DataSource = dstttttd;
            GridOTHER.DataBind();

            dstttttdf = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "REPORT");
            GridREPORT.DataSource = dstttttdf;
            GridREPORT.DataBind();

            dstttttdff = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "SECURITY");
            GridSECURITY.DataSource = dstttttdff;
            GridSECURITY.DataBind();

            dstttttdfflead = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "LEADMANAGEMENT");
            GridLead.DataSource = dstttttdfflead;
            GridLead.DataBind();

            dstttttdffmanu = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "MANUFACTURE");
            GridMANUFACTURE.DataSource = dstttttdffmanu;
            GridMANUFACTURE.DataBind();

            dstttttdffpro = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "PROJECT");
            GridPROJECT.DataSource = dstttttdffpro;
            GridPROJECT.DataBind();


            DataSet datast = new DataSet();
            datast = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, "CONFIG");
            GridConfig.DataSource = datast;

            GridConfig.DataBind();


            ModalPopupGet.Show();
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
            ModalPopupGet.Hide();
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
            if (lbloption.Text == "New")
            {
                if ((txtpassword.Text == "") && (txtconfirmpassword.Text == ""))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter Password And Confirm Password.');", true);
                    return;
                }

                if (txtpassword.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter Password.');", true);
                    return;
                }

                if (txtconfirmpassword.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter Confirm password.');", true);
                    return;
                }

                if (txtpassword.Text != txtconfirmpassword.Text)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your password and Confirm password doesnt match.');", true);
                    return;
                }

                if (drpBranch.SelectedValue == "0")
                {
                    if (chkBranch.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select branch');", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select default branch');", true);
                        return;
                    }
                }

            }

            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataSet dsBranch;
            DataTable dtBranch;
            DataColumn dc;

            ds = new DataSet();

            dt = new DataTable();

            dc = new DataColumn("UserName");
            dt.Columns.Add(dc);

            dc = new DataColumn("Role");
            dt.Columns.Add(dc);

            dc = new DataColumn("RoleDesc");
            dt.Columns.Add(dc);

            dc = new DataColumn("Section");
            dt.Columns.Add(dc);

            dc = new DataColumn("Area");
            dt.Columns.Add(dc);

            dc = new DataColumn("Orderno");
            dt.Columns.Add(dc);

            dc = new DataColumn("Add");
            dt.Columns.Add(dc);

            dc = new DataColumn("Edit");
            dt.Columns.Add(dc);

            dc = new DataColumn("Delete");
            dt.Columns.Add(dc);

            dc = new DataColumn("View");
            dt.Columns.Add(dc);

            ds.Tables.Add(dt);
            bool Add = false;
            bool Edit = false;
            bool Delete = false;

            DataSet dsroles = new DataSet();
            DataTable dtt = new DataTable();
            DataRow drNewt;
            DataColumn dcc;
            dcc = new DataColumn("UserName");
            dtt.Columns.Add(dcc);

            dcc = new DataColumn("Role");
            dtt.Columns.Add(dcc);
            dsroles.Tables.Add(dtt);

            bool Views = false;
            Label lblDebtorID = null;

            for (int vLoop = 0; vLoop < GrdViewItem.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GrdViewItem.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GrdViewItem.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GrdViewItem.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GrdViewItem.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GrdViewItem.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GrdViewItem.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GrdViewItem.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GrdViewItem.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GrdViewItem.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GrdViewItem.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridSupplier.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridSupplier.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridSupplier.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridSupplier.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridSupplier.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridSupplier.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridSupplier.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridSupplier.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridSupplier.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridSupplier.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridSupplier.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridBANKING.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridBANKING.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridBANKING.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridBANKING.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridBANKING.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridBANKING.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridBANKING.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridBANKING.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridBANKING.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridBANKING.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridBANKING.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridEXPENSES.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridEXPENSES.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridEXPENSES.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridEXPENSES.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridEXPENSES.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridEXPENSES.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridEXPENSES.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridEXPENSES.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridEXPENSES.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridEXPENSES.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridEXPENSES.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridINVENTORY.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridINVENTORY.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridINVENTORY.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridINVENTORY.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridINVENTORY.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridINVENTORY.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridINVENTORY.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridINVENTORY.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridINVENTORY.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridINVENTORY.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridINVENTORY.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridACCOUNTS.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridACCOUNTS.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridACCOUNTS.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridACCOUNTS.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridACCOUNTS.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridACCOUNTS.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridACCOUNTS.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridACCOUNTS.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridACCOUNTS.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridACCOUNTS.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridACCOUNTS.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridRESOURCE.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridRESOURCE.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridRESOURCE.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridRESOURCE.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridRESOURCE.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridRESOURCE.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridRESOURCE.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridRESOURCE.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridRESOURCE.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridRESOURCE.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridRESOURCE.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridSERVICE.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridSERVICE.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridSERVICE.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridSERVICE.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridSERVICE.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridSERVICE.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridSERVICE.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridSERVICE.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridSERVICE.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridSERVICE.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridSERVICE.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridOTHER.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridOTHER.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridOTHER.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridOTHER.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridOTHER.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridOTHER.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridOTHER.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridOTHER.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridOTHER.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridOTHER.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridOTHER.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridREPORT.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridREPORT.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridREPORT.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridREPORT.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridREPORT.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridREPORT.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridREPORT.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridREPORT.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridREPORT.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridREPORT.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridREPORT.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridSECURITY.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridSECURITY.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridSECURITY.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridSECURITY.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridSECURITY.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridSECURITY.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridSECURITY.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridSECURITY.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridSECURITY.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridSECURITY.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridSECURITY.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }

            for (int vLoop = 0; vLoop < GridConfig.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridConfig.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridConfig.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridConfig.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridConfig.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridConfig.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridConfig.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridConfig.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridConfig.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridConfig.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridConfig.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }





            for (int vLoop = 0; vLoop < GridPROJECT.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridPROJECT.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridPROJECT.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridPROJECT.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridPROJECT.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridPROJECT.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridPROJECT.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridPROJECT.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridPROJECT.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridPROJECT.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridPROJECT.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }






            for (int vLoop = 0; vLoop < GridMANUFACTURE.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridMANUFACTURE.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridMANUFACTURE.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridMANUFACTURE.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridMANUFACTURE.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridMANUFACTURE.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridMANUFACTURE.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridMANUFACTURE.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridMANUFACTURE.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridMANUFACTURE.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridMANUFACTURE.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }



            for (int vLoop = 0; vLoop < GridLead.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridLead.Rows[vLoop].FindControl("chkboxAdd");
                if (txt.Checked)
                {
                    Add = txt.Checked;
                }
                else
                {
                    Add = false;
                }

                CheckBox txttt = (CheckBox)GridLead.Rows[vLoop].FindControl("chkboxEdit");
                if (txttt.Checked)
                {
                    Edit = txttt.Checked;
                }
                else
                {
                    Edit = false;
                }

                CheckBox txth = (CheckBox)GridLead.Rows[vLoop].FindControl("chkboxDel");
                if (txth.Checked)
                {
                    Delete = txth.Checked;
                }
                else
                {
                    Delete = false;
                }

                CheckBox txthh = (CheckBox)GridLead.Rows[vLoop].FindControl("chkboxView");
                if (txthh.Checked)
                {
                    Views = txthh.Checked;
                }
                else
                {
                    Views = false;
                }

                if ((txt.Checked == true) || (txttt.Checked == true) || (txth.Checked == true) || (txthh.Checked == true))
                {
                    drNewt = dtt.NewRow();
                    drNewt["UserName"] = txtUser.Text;
                    drNewt["Role"] = GridLead.Rows[vLoop].Cells[3].Text;
                    dsroles.Tables[0].Rows.Add(drNewt);
                }

                drNew = dt.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["Role"] = GridLead.Rows[vLoop].Cells[3].Text;
                drNew["RoleDesc"] = GridLead.Rows[vLoop].Cells[2].Text;
                drNew["Section"] = GridLead.Rows[vLoop].Cells[1].Text;
                drNew["Area"] = GridLead.Rows[vLoop].Cells[0].Text;

                lblDebtorID = (Label)GridLead.Rows[vLoop].FindControl("lblDebtorID");
                drNew["Orderno"] = lblDebtorID.Text;

                drNew["Add"] = Add;
                drNew["Edit"] = Edit;
                drNew["Delete"] = Delete;
                drNew["View"] = Views;
                ds.Tables[0].Rows.Add(drNew);
            }




            string Userna = Request.Cookies["LoggedUserName"].Value;

            string userName = string.Empty;
            string Email = string.Empty;
            string connection = string.Empty;
            string defaultbranch = string.Empty;
            string UserGroup = string.Empty;
            bool brncheck = chkBranch.Checked;
            bool Locked = chkAccLocked.Checked;
            bool DateLock = chkboxdatelock.Checked;

            if (txtUser.Text != string.Empty)
                userName = txtUser.Text;
            if (txtEmail.Text != string.Empty)
                Email = txtEmail.Text;
            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;

            int EmpNo = 0;
            if (drpIncharge.Text.Trim() != string.Empty)
                EmpNo = Convert.ToInt32(drpIncharge.SelectedValue);

            if (drpBranch.Text.Trim() != string.Empty)
                defaultbranch = Convert.ToString(drpBranch.SelectedValue);

            if (txtUserGroup.Text.Trim() != string.Empty)
                UserGroup = txtUserGroup.Text.Trim();
            else
                UserGroup = "Users";

            bool HideDeviation = chkhidedeviation.Checked;


            DataSet debrninsert = new DataSet();
            dsBranch = new DataSet();
            dtBranch = new DataTable();

            dc = new DataColumn("UserName");
            dtBranch.Columns.Add(dc);

            dc = new DataColumn("BranchCode");
            dtBranch.Columns.Add(dc);

            dc = new DataColumn("DefaultBranchCode");
            dtBranch.Columns.Add(dc);

            dsBranch.Tables.Add(dtBranch);

            if (chkBranch.Checked == true)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                DataSet dsbr = new DataSet();
                dsbr = bl.ListBranch();

                for (int vLoop = 0; vLoop < dsbr.Tables[0].Rows.Count; vLoop++)
                {
                    drNew = dtBranch.NewRow();
                    drNew["UserName"] = txtUser.Text;
                    drNew["BranchCode"] = dsbr.Tables[0].Rows[vLoop]["Branchcode"].ToString();
                    drNew["DefaultBranchCode"] = drpBranch.SelectedValue;
                    dsBranch.Tables[0].Rows.Add(drNew);
                }
            }
            else if (chkBranch.Checked == false)
            {
                drNew = dtBranch.NewRow();
                drNew["UserName"] = txtUser.Text;
                drNew["BranchCode"] = drpBranch.SelectedValue;
                drNew["DefaultBranchCode"] = drpBranch.SelectedValue;
                dsBranch.Tables[0].Rows.Add(drNew);
            }






            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    BusinessLogic objBL = new BusinessLogic();

                    objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

                    if (Session["Show"] == "Add New")
                    {
                        if (objBL.InsertUserOptions(ds, Userna, userName, Email, Locked, DateLock, dsroles, txtpassword.Text, EmpNo, UserGroup, HideDeviation, dsBranch, brncheck, defaultbranch))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User And their Options Created sucessfully.');", true);
                            //BindGrid();

                            string salestype = string.Empty;
                            int ScreenNo = 0;
                            string ScreenName = string.Empty;

                            salestype = "Users";
                            ScreenName = "Users";

                            string emailcontent = string.Empty;
                            BusinessLogic bl = new BusinessLogic();

                            bool mobile = false;
                            bool Email1 = false;
                            string emailsubject = string.Empty;

                            string usernam = Request.Cookies["LoggedUserName"].Value;
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
                                            Email1 = Convert.ToBoolean(dr["Email"]);
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
                                            if (Email1 == true)
                                            {
                                                //string subject = "Added - Customer Receipt in Branch " + Request.Cookies["Company"].Value;

                                                string body = "\n";


                                                int index123 = emailcontent.IndexOf("@Branch");
                                                body = Request.Cookies["Company"].Value;
                                                if (index123 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                                }


                                                int index312 = emailcontent.IndexOf("@User");
                                                body = usernam;
                                                if (index312 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                                }

                                                int index2 = emailcontent.IndexOf("@UserName");
                                                body = userName;
                                                if (index2 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index2, 9).Insert(index2, body);
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


                                                int index312 = smscontent.IndexOf("@User");
                                                body = usernam;
                                                if (index312 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                                }

                                                int index2 = emailcontent.IndexOf("@UserName");
                                                body = userName;
                                                if (index2 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index2, 9).Insert(index2, body);
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
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User already exists. Please try again.');", true);
                            return;
                        }
                    }
                    else if (Session["Show"] == "Edit")
                    {
                        objBL.UpdateUserOptions(connection, ds, Userna, userName, Email, Locked, DateLock, dsroles, txtpassword.Text, EmpNo, UserGroup, HideDeviation, dsBranch, brncheck, defaultbranch);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User And their Options Updated Successfully');", true);
                        //BindGrid();

                        string salestype = string.Empty;
                        int ScreenNo = 0;
                        string ScreenName = string.Empty;

                        salestype = "Users";
                        ScreenName = "Users";

                        string emailcontent = string.Empty;
                        BusinessLogic bl = new BusinessLogic();

                        bool mobile = false;
                        bool Email1 = false;
                        string emailsubject = string.Empty;

                        string usernam = Request.Cookies["LoggedUserName"].Value;
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
                                        Email1 = Convert.ToBoolean(dr["Email"]);
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
                                        if (Email1 == true)
                                        {
                                            //string subject = "Added - Customer Receipt in Branch " + Request.Cookies["Company"].Value;

                                            string body = "\n";


                                            int index123 = emailcontent.IndexOf("@Branch");
                                            body = Request.Cookies["Company"].Value;
                                            if (index123 >= 0)
                                            {
                                                emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                            }



                                            int index312 = emailcontent.IndexOf("@User");
                                            body = usernam;
                                            if (index312 >= 0)
                                            {
                                                emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                            }

                                            int index2 = emailcontent.IndexOf("@UserName");
                                            body = userName;
                                            if (index2 >= 0)
                                            {
                                                emailcontent = emailcontent.Remove(index2, 9).Insert(index2, body);
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



                                            int index312 = smscontent.IndexOf("@User");
                                            body = usernam;
                                            if (index312 >= 0)
                                            {
                                                smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                            }

                                            int index2 = emailcontent.IndexOf("@UserName");
                                            body = userName;
                                            if (index2 >= 0)
                                            {
                                                smscontent = smscontent.Remove(index2, 9).Insert(index2, body);
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
                }
                else
                {

                }
            }

            ModalPopupGet.Hide();
            GrdViewCust.DataBind();
            UpdatePanelPage.Update();
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
            if (GrdViewCust.SelectedDataKey != null)
                e.InputParameters["username"] = GrdViewCust.SelectedDataKey.Value;

            string connection = "";
            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;

            BusinessLogic objBL = new BusinessLogic();

            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;

            salestype = "Users";
            ScreenName = "Users";

            string emailcontent = string.Empty;
            BusinessLogic bl = new BusinessLogic();

            bool mobile = false;
            bool Email1 = false;
            string emailsubject = string.Empty;

            string usernam = Request.Cookies["LoggedUserName"].Value;
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
                            Email1 = Convert.ToBoolean(dr["Email"]);
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
                            if (Email1 == true)
                            {

                                string body = "\n";


                                int index123 = emailcontent.IndexOf("@Branch");
                                body = Request.Cookies["Company"].Value;
                                if (index123 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                }



                                int index312 = emailcontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = emailcontent.IndexOf("@UserName");
                                body = GrdViewCust.SelectedDataKey.Value.ToString();
                                if (index2 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index2, 9).Insert(index2, body);
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
                                smscontent = smscontent.Remove(index123, 7).Insert(index123, body);



                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                smscontent = smscontent.Remove(index312, 5).Insert(index312, body);

                                int index2 = emailcontent.IndexOf("@UserName");
                                body = GrdViewCust.SelectedDataKey.Value.ToString();
                                smscontent = smscontent.Remove(index2, 9).Insert(index2, body);

                                if (Session["Provider"] != null)
                                {
                                    utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), toAddress, smscontent, true, UserID);
                                }


                            }

                        }
                    }
                }
            }

            //e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
