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
using System.Xml.Linq;
using System.Data.OleDb;

public partial class CategoryMaster : System.Web.UI.Page
{
    bool isOffline = false;

    public string connStr = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();


            //GrdCategory.PageSize = 5;


            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                lnkBtnAdd.Visible = false;
                GrdCategory.Columns[1].Visible = false;
                GrdCategory.Columns[2].Visible = false;
                isOffline = true;
            }


            //if (Request.QueryString["myname"] != null)
            //{
            //    string myNam = Request.QueryString["myname"].ToString();
            //    if (myNam == "NEWCAT")
            //    {
            //        GrdCategory.DataBind();
            //        GrdCategory.ShowFooter = true;
            //        lnkBtnAdd.Visible = false;
            //    }
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

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

    protected void GrdCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Cancel")
            {
                GrdCategory.FooterRow.Visible = false;
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

                    string Username = Request.Cookies["LoggedUserName"].Value;

                    int nextSeq = objBus.GetNextSequence(GetConnectionString(), "Select Max(CategoryID) from tblCategories");
                    string cateDes = ((TextBox)GrdCategory.FooterRow.FindControl("txtAddDescr")).Text;
                    string catelvl = ((TextBox)GrdCategory.FooterRow.FindControl("txtAddLvl")).Text;

                    if (nextSeq == -1)
                        return;
                    string sQl = string.Format("Insert Into tblCategories Values({0},'{1}',{2})", nextSeq + 1, cateDes, catelvl);
                    srcGridView.InsertParameters.Add("sQl", TypeCode.String, sQl);
                    srcGridView.InsertParameters.Add("connection", TypeCode.String, GetConnectionString());
                    srcGridView.InsertParameters.Add("Usernam", TypeCode.String, Username);
                    srcGridView.InsertParameters.Add("CategoryName", TypeCode.String, cateDes);
                    try
                    {
                        srcGridView.Insert();
                        System.Threading.Thread.Sleep(1000);
                        GrdCategory.DataBind();
                    //}
                    //catch (Exception ex)
                    //{
                    //    if (ex.InnerException != null)
                    //    {
                    //        StringBuilder script = new StringBuilder();
                    //        script.Append("alert('Category with this name already exists, Please try with a different name.');");

                    //        if (ex.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                    //        return;
                    //    }
                    //}
                    }
                    catch (Exception ex)
                    {
                        TroyLiteExceptionManager.HandleException(ex);
                        return;
                    }
                    lnkBtnAdd.Visible = true;
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
                //else
                //{
                //    string catDescr = ((TextBox)GrdCategory.HeaderRow.FindControl("txtCatDescr")).Text;
                //    //string catDescr = ((TextBox)GrdCategory.Rows[e.RowIndex].FindControl("txtCatDescr")).Text;

                //    string Username = Request.Cookies["LoggedUserName"].Value;
                //    string catId = GrdCategory.SelectedValue.ToString();
                //    string catlevel = ((TextBox)GrdCategory.HeaderRow.FindControl("txtCatLvl")).Text;

                //    //string catId = GrdCategory.DataKeys[e.RowIndex].Value.ToString();
                //    //string catlevel = ((TextBox)GrdCategory.Rows[e.RowIndex].FindControl("txtCatLvl")).Text;

                //    //srcGridView.UpdateMethod = "UpdateCategory";
                //    srcGridView.UpdateParameters.Add("connection", TypeCode.String, GetConnectionString());
                //    srcGridView.UpdateParameters.Add("CategoryName", TypeCode.String, catDescr);
                //    srcGridView.UpdateParameters.Add("CategoryID", TypeCode.Int32, catId);
                //    srcGridView.UpdateParameters.Add("CategoryLevel", TypeCode.String, catlevel);
                //    srcGridView.UpdateParameters.Add("Usernam", TypeCode.String, Username);

                //    try
                //    {
                //        srcGridView.Update();
                //        System.Threading.Thread.Sleep(1000);
                //        GrdCategory.DataBind();
                //    }
                //    catch (Exception ex)
                //    {
                //        if (ex.InnerException != null)
                //        {
                //            StringBuilder script = new StringBuilder();
                //            script.Append("alert('Category with this name already exists, Please try with a different name.');");

                //            if (ex.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                //            return;
                //        }
                //    }


                lnkBtnAdd.Visible = true;
            }
            else if (e.CommandName == "Edit")
            {
                lnkBtnAdd.Visible = false;
            }
            else if (e.CommandName == "Page")
            {
                lnkBtnAdd.Visible = true;



                int intCurIndex = GrdCategory.PageIndex;
                string Comm = e.CommandArgument.ToString();

                switch (Comm)
                {
                    case "First":
                        GrdCategory.PageIndex = 0;
                        break;
                    case "Prev":
                        GrdCategory.PageIndex = intCurIndex - 1;
                        break;
                    case "Next":
                        GrdCategory.PageIndex = intCurIndex + 1;
                        break;
                    case "Last":
                        GrdCategory.PageIndex = GrdCategory.PageCount;
                        break;
                }




            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    protected void GrdCategory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdCategory, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdCategory_DataBound(object sender, EventArgs e)
    {
        try
        {
            GrdCategory.Rows[0].Visible = false;

            if (GrdCategory.Rows.Count == 1 && !GrdCategory.FooterRow.Visible)
            {
                GrdCategory.Columns[0].HeaderText = "No Categoreis found!";
                GrdCategory.Columns[1].Visible = false;
                GrdCategory.Columns[2].Visible = false;
            }
            else
            {
                GrdCategory.Columns[0].HeaderText = "Product Category";

                if (!isOffline)
                    GrdCategory.Columns[1].Visible = true;
                else
                    GrdCategory.Columns[1].Visible = false;

                if (!isOffline)
                    GrdCategory.Columns[2].Visible = true;
                else
                    GrdCategory.Columns[2].Visible = false;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(GetConnectionString());

                if (bl.CheckIfCategoryUsed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
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

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdCategory.SelectedDataKey != null)
                e.InputParameters["CategoryID"] = GrdCategory.SelectedDataKey.Value;


            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
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
            GrdCategory.DataBind();
            GrdCategory.ShowFooter = true;
            lnkBtnAdd.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
                string catDescr = ((TextBox)GrdCategory.Rows[e.RowIndex].FindControl("txtCatDescr")).Text;

                //BusinessLogic bl = new BusinessLogic();
                //string connection = string.Empty;

                //if (Request.Cookies["Company"] != null)
                //    connection = Request.Cookies["Company"].Value;
                //else
                //    Response.Redirect("Login.aspx");

                //if (bl.CategoryExists(connection, catDescr))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cash Payment date cannot be future date')", true);
                //    return;
                //}

                string Username = Request.Cookies["LoggedUserName"].Value;
                string catId = GrdCategory.DataKeys[e.RowIndex].Value.ToString();
                string catlevel = ((TextBox)GrdCategory.Rows[e.RowIndex].FindControl("txtCatLvl")).Text;

                srcGridView.UpdateMethod = "UpdateCategory";
                srcGridView.UpdateParameters.Add("connection", TypeCode.String, GetConnectionString());
                srcGridView.UpdateParameters.Add("CategoryName", TypeCode.String, catDescr);
                srcGridView.UpdateParameters.Add("CategoryID", TypeCode.Int32, catId);
                srcGridView.UpdateParameters.Add("CategoryLevel", TypeCode.String, catlevel);
                srcGridView.UpdateParameters.Add("Usernam", TypeCode.String, Username);
                //try
                //{
                //    srcGridView.Update();
                //    System.Threading.Thread.Sleep(1000);
                //    GrdCategory.DataBind();
                //}
                //catch (Exception ex)
                //{
                //    if (ex.InnerException != null)
                //    {
                //        StringBuilder script = new StringBuilder();
                //        script.Append("alert('Category with this name already exists, Please try with a different name.');");

                //        if (ex.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                //        return;
                //    }
                //}
                lnkBtnAdd.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void srcGridView_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
    }

    protected void GrdCategory_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        try
        {
            GrdCategory.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdCategory_PreRender(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       
    }

        
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //srcGridView.SelectParameters.Add(new CookieParameter("connection", "Company"));
        srcGridView.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        srcGridView.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void GrdCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}
