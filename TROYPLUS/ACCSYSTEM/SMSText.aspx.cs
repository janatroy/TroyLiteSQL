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

public partial class SMSText : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string connStr = string.Empty;

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

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
                GrdSMSText.Columns[3].Visible = false;
                GrdSMSText.Columns[2].Visible = false;
            }

            string connection = Request.Cookies["Company"].Value;
            string usernam = Request.Cookies["LoggedUserName"].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);

            if (bl.CheckUserHaveAdd(usernam, "SMSDRFT"))
            {
                lnkBtnAdd.Enabled = false;
                lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
            }
            else
            {
                lnkBtnAdd.Enabled = true;
                lnkBtnAdd.ToolTip = "Click to Add New ";
            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdSMSText_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

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

    protected void GrdSMSText_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Cancel")
            {
                GrdSMSText.FooterRow.Visible = false;
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
                    int nextSeq = objBus.GetNextSequence(GetConnectionString(), "Select Max(ID) from tblSMSText");
                    string smsType = ((TextBox)GrdSMSText.FooterRow.FindControl("txtAddSMSType")).Text;
                    string smsText = ((TextBox)GrdSMSText.FooterRow.FindControl("txtAddSMSText")).Text;
                    string Username = Request.Cookies["LoggedUserName"].Value;
                    //srcGridView.InsertParameters.Add("Usernam", TypeCode.String, Username);

                    if (nextSeq == -1)
                        return;

                    string sQl = string.Format("Insert Into tblSMSText Values({0},'{1}','{2}')", nextSeq + 1, smsType, smsText);

                    srcGridView.InsertParameters.Add("sQl", TypeCode.String, sQl);
                    srcGridView.InsertParameters.Add("connection", TypeCode.String, GetConnectionString());
                    srcGridView.InsertParameters.Add("Usernam", TypeCode.String, Username);
                    srcGridView.InsertParameters.Add("types", TypeCode.String, "SMS");
                    srcGridView.InsertParameters.Add("smstext", TypeCode.String, smsText);

                    try
                    {
                        srcGridView.Insert();
                        System.Threading.Thread.Sleep(1000);
                        GrdSMSText.DataBind();
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            StringBuilder script = new StringBuilder();
                            script.Append("alert('SMS with this name already exists, Please try with a different name.');");

                            if (ex.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                            return;
                        }
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
            }
            else if (e.CommandName == "Edit")
            {
                lnkBtnAdd.Visible = false;
            }
            else if (e.CommandName == "Page")
            {
                lnkBtnAdd.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdSMSText_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdSMSText, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdSMSText_DataBound(object sender, EventArgs e)
    {
        try
        {
            GrdSMSText.Rows[0].Visible = false;

            if (GrdSMSText.Rows.Count == 1 && !GrdSMSText.FooterRow.Visible)
            {
                GrdSMSText.Columns[0].HeaderText = "No Text found.";
                GrdSMSText.Columns[1].HeaderText = "";
            }
            else
            {
                GrdSMSText.Columns[0].HeaderText = "Type";
                GrdSMSText.Columns[1].HeaderText = "SMS Text";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdSMSText_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdSMSText.SelectedDataKey != null)
                e.InputParameters["ID"] = GrdSMSText.SelectedDataKey.Value;
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
            GrdSMSText.DataBind();
            GrdSMSText.ShowFooter = true;
            lnkBtnAdd.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    protected void GrdSMSText_RowUpdating(object sender, GridViewUpdateEventArgs e)
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

                string smsType = ((TextBox)GrdSMSText.Rows[e.RowIndex].FindControl("txtSMSType")).Text;
                string smsText = ((TextBox)GrdSMSText.Rows[e.RowIndex].FindControl("txtSMSText")).Text;
                string smsId = GrdSMSText.DataKeys[e.RowIndex].Value.ToString();
                string Username = Request.Cookies["LoggedUserName"].Value;
                srcGridView.UpdateMethod = "UpdateSMSText";
                srcGridView.UpdateParameters.Add("connection", TypeCode.String, GetConnectionString());
                srcGridView.UpdateParameters.Add("SMSType", TypeCode.String, smsType);
                srcGridView.UpdateParameters.Add("SMSText", TypeCode.String, smsText);
                srcGridView.UpdateParameters.Add("ID", TypeCode.Int32, smsId);
                srcGridView.UpdateParameters.Add("Usernam", TypeCode.String, Username);
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

    protected void GrdSMSText_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(1000);
            GrdSMSText.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdSMSText_PreRender(object sender, EventArgs e)
    {

    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddCriteria_SelectedIndexChanged(object sender, EventArgs e)
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
}
