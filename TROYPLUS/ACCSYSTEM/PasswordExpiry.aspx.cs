using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Password_Expiry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
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
                    btnPwdSave.Visible = false;
                }

                txtConPwd.Attributes.Add("onKeyPress", " return clickButton(event,'" + btnPwdSave.ClientID + "')");
                txtConPwd.Focus();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private string GetDiscType()
    {
        DataSet appSettings;
        string discType = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "PWDEXPDAY")
                {
                    discType = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["DISCTYPE"] = discType.Trim().ToUpper();
                }

            }
        }
        else if (Session["AppSettings"] == null)
        {
            BusinessLogic bl = new BusinessLogic();
            DataSet ds = bl.GetAppSettings(Request.Cookies["Company"].Value);

            if (ds != null)
                Session["AppSettings"] = ds;

            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "PWDEXPDAY")
                {
                    discType = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["DISCTYPE"] = discType.Trim().ToUpper();
                }

            }
        }

        return discType;

    }
    Int32 pwdexpday;
    protected void btnPwdSave_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder msg = new StringBuilder();

            if (!Page.IsValid)
            {
                foreach (IValidator validator in Page.Validators)
                {
                    if (!validator.IsValid)
                    {
                        msg.Append(" - " + validator.ErrorMessage);
                        msg.Append("\\n");
                    }
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg.ToString() + "');", true);
                return;
            }

            BusinessLogic objBus = new BusinessLogic();

            if (txtNewPwd.Text != txtConPwd.Text)
            {
                //errorDisplay.AddItem("Your new password and Confirm password dosent match.", DisplayIcons.Information, false);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your new password and Confirm password dosent match.');", true);
                return;
            }

            if (Request.Cookies["LoggedUserName"].Value != null && Request.Cookies["Company"] != null)
            {
                string password = objBus.GetPassword(Request.Cookies["LoggedUserName"].Value, Request.Cookies["Company"].Value);

                if (password != txtOldPwd.Text)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your have entered incorrect password.');", true);
                    //errorDisplay.AddItem("Your have entered incorrect password.", DisplayIcons.Information, false);
                    return;
                }

                else if (password == txtNewPwd.Text)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('New password cannot be same as old password.');", true);
                    //errorDisplay.AddItem("Your have entered incorrect password.", DisplayIcons.Information, false);
                    return;
                }
                else
                {
                    string discType = GetDiscType();
                    if (discType != "" && discType != "0")
                    {
                        pwdexpday = Convert.ToInt32(discType);
                    }
                    DateTime dt = DateTime.Now;

                    //DateTime dt = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time").ToShortDateString("");

                    DateTime ts;
                    ts = dt.AddDays(pwdexpday);

                    objBus.ChangePassword(Request.Cookies["LoggedUserName"].Value, txtNewPwd.Text, Request.Cookies["Company"].Value, ts);
                    //errorDisplay.AddItem("Your password has been changed successfully.", DisplayIcons.GreenTick, false);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your password has been changed successfully.');", true);

                    txtOldPwd.Text = string.Empty;
                    txtNewPwd.Text = string.Empty;
                    txtConPwd.Text = string.Empty;
                    Response.Redirect("DashBoard.aspx");
                    //return;
                }
            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnPwdCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }

}