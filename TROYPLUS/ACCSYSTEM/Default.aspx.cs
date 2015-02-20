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
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //if (Helper.IsTrialVersion())
                //{
                //imgTrial.Visible = true;
                //}
                //else
                //{
                imgTrial.Visible = false;
                //}

                string connStr = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

                //BusinessLogic objChk = new BusinessLogic(); 

                //if (!objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline"))) 
                //{
                //    divWarning.Visible = false;
                //    tblWarning.Visible = false;
                //}

                string filePath = Server.MapPath("Offline\\" + dbfileName + ".offline");

                if (File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-CLIENT"))
                {
                    divWarning.Visible = true;
                    tblWarning.Visible = true;
                }
                else if (!File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-CLIENT"))
                {
                    divWarning.Visible = false;
                    tblWarning.Visible = false;
                }
                else if (File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-SERVER"))
                {
                    divWarning.Visible = true;
                    tblWarning.Visible = true;
                }
                else if (!File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-SERVER"))
                {
                    divWarning.Visible = false;
                    tblWarning.Visible = false;
                }
                else
                {
                    divWarning.Visible = false;
                    tblWarning.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
}
