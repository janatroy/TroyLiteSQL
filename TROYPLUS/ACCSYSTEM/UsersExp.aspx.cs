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

public partial class UsersExp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                string connStr = string.Empty;


                GrdViewCust.PageSize = 8;

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
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtUserName.UniqueID, "Text"));
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

            Session["Show"] = "Edit";

            DataSet ds = bl.GetUserOptionsForId(username, connection, "");

            DataSet dsd = bl.GetUserInfo(username, connection);

            if (dsd != null)
            {
                if (dsd.Tables[0].Rows.Count == 1)
                {
                    txtUser.Text = dsd.Tables[0].Rows[0]["username"].ToString();
                    txtUser.Enabled = false;
                    txtEmail.Text = dsd.Tables[0].Rows[0]["email"].ToString();
                    chkAccLocked.Checked = bool.Parse(dsd.Tables[0].Rows[0]["Locked"].ToString());

                    chkboxdatelock.Checked = bool.Parse(dsd.Tables[0].Rows[0]["DateLock"].ToString());
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

            GrdViewItem.DataSource = ds;
            GrdViewItem.DataBind();

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

            string connection = string.Empty;
            string userid = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;

            txtUser.Enabled = true;
            txtUser.Text = "";
            chkAccLocked.Checked = false;

            chkboxdatelock.Checked = false;
            txtEmail.Text = "";
            BusinessLogic objBus = new BusinessLogic();

            string Types = string.Empty;

            ds = objBus.GetMasterRolesWithArea(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString, Types);

            Session["Show"] = "Add New";

            GrdViewItem.DataSource = ds;
            GrdViewItem.DataBind();

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
            DataSet ds;
            DataTable dt;
            DataRow drNew;

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

            string Userna = Request.Cookies["LoggedUserName"].Value;

            string userName = string.Empty;
            string Email = string.Empty;
            string connection = string.Empty;

            bool Locked = chkAccLocked.Checked;
            bool DateLock = chkboxdatelock.Checked;

            if (txtUser.Text != string.Empty)
                userName = txtUser.Text;
            if (txtEmail.Text != string.Empty)
                Email = txtEmail.Text;
            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;

            string UserGroup = string.Empty;
            bool DateLoc = true;
            DataSet dsd = new DataSet();
            int EmpNo = 0;

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    BusinessLogic objBL = new BusinessLogic();

                    objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

                    if (Session["Show"] == "Add New")
                    {
                        if (objBL.InsertUserOptions(ds, Userna, userName, Email, Locked, DateLock, dsd, "", EmpNo, UserGroup, DateLoc))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User And their Options Saved Successfully');", true);
                            //BindGrid();

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User already exists. Please try again.');", true);
                            return;
                        }
                    }
                    else if (Session["Show"] == "Edit")
                    {
                        objBL.UpdateUserOptions(connection, ds, Userna, userName, Email, Locked, DateLock, dsd, "", EmpNo, UserGroup, DateLoc);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User And their Options Updated Successfully');", true);
                        //BindGrid();
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

            //e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
