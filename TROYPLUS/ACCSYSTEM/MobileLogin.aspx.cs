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
using System.Management;
using System.Text;
using System.IO;
using System.Xml;

public partial class MobileLogin : System.Web.UI.Page
{
    private Hashtable listComp = new Hashtable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                if (Request.QueryString["Type"] != null)
                {
                    if (Request.QueryString["Type"].ToString() == "Demo")
                        DemoLogin();
                }

                foreach (ConnectionStringSettings company in System.Configuration.ConfigurationManager.ConnectionStrings)
                {
                    if (company.Name != "LocalSqlServer")
                    {
                        //string[] listDet = company.Name.Split(new char[] { ',' });

                        //BusinessLogic bl = new BusinessLogic(company.ConnectionString);
                        //DataSet companyInfo = bl.getCompanyInfo(); 
                        listComp.Add(company.ProviderName, company.Name);
                        //if (companyInfo != null)
                        //{
                        //    if (companyInfo.Tables[0].Rows.Count > 0)
                        //    {
                        //        foreach (DataRow dr in companyInfo.Tables[0].Rows)
                        //        {
                        //            ddCompany.Items.Add(Convert.ToString(dr["CompanyName"]));

                        //        }
                        //    }
                        //}
                    }
                }

                Session["CompanyList"] = listComp;
                txtLogin.Focus();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = string.Empty;
            string appVersion = "1.1.1";
            string dbfileName = string.Empty;

            if (Session["CompanyList"] != null)
            {

                listComp = (Hashtable)Session["CompanyList"];

                if (!listComp.Contains(txtCompany.Text.Trim().ToUpper()))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Company Code. Please try again.');", true);
                    return;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            HttpCookie cookie = new HttpCookie("Company");
            cookie.HttpOnly = true;
            cookie.Secure = true;

            if (txtCompany.Text != "")
            {
                cookie.Value = txtCompany.Text.ToUpper();
                Response.Cookies.Add(cookie);
            }
            else
                return;

            string localpath = ConfigurationManager.AppSettings["LocalPath"].ToString();

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

            dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            filename = Server.MapPath(localpath + dbfileName);

            if (File.Exists(filename + ".zip"))
            {
                GZip objZip = new GZip(filename + ".zip", filename, Action.UnZip);
                File.Delete(filename + ".zip");
            }

            bool isAuthenticated = IsAuthenticated(txtLogin.Text, txtPassword.Text);

            if (isAuthenticated == true)
            {
                string[] roles = GetRoles(txtLogin.Text);

                string roleData = string.Join("|", roles);

                FormsAuthentication.SignOut();

                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(txtLogin.Text, chkRemember.Checked);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, roleData);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);

                LoadAppSettings();
                //FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, false);

                if (Session["AppSettings"] != null)
                {
                    DataSet ds = (DataSet)Session["AppSettings"];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["KEY"].ToString() == "VERSION")
                        {
                            if (ds.Tables[0].Rows[i]["KEYVALUE"].ToString() != appVersion)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Application and Database Version should be same. Please Contact Administrator.');", true);
                                return;
                            }
                        }
                    }

                }

                if (System.Configuration.ConfigurationManager.AppSettings["InstallationType"].ToString() == "SERVER")
                {
                    if (!IsValidIPRequest())
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not a Valid User, Only Ristricted Users are allowed to Login. Please Contact Administrator.');", true);
                        return;
                    }
                }

                IsSMSRequired();
                //FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, false);
                Response.Redirect(FormsAuthentication.DefaultUrl, true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Login. Please check the username and password');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void DemoLogin()
    {
        string filename = string.Empty;
        string appVersion = "1.1.1";
        string dbfileName = string.Empty;
        HttpCookie cookie = new HttpCookie("Company");
        cookie.HttpOnly = true;
        cookie.Secure = true;

        cookie.Value = "DEMO";
        Response.Cookies.Add(cookie);



        string localpath = ConfigurationManager.AppSettings["LocalPath"].ToString();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName);

        if (File.Exists(filename + ".zip"))
        {
            GZip objZip = new GZip(filename + ".zip", filename, Action.UnZip);
            File.Delete(filename + ".zip");
        }

        bool isAuthenticated = IsAuthenticated("admin", "admin123");

        if (isAuthenticated == true)
        {
            string[] roles = GetRoles("admin");

            string roleData = string.Join("|", roles);

            FormsAuthentication.SignOut();

            HttpCookie authCookie = FormsAuthentication.GetAuthCookie("admin", false);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, roleData);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);

            LoadAppSettings();
            //FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, false);

            if (Session["AppSettings"] != null)
            {
                DataSet ds = (DataSet)Session["AppSettings"];

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["KEY"].ToString() == "VERSION")
                    {
                        if (ds.Tables[0].Rows[i]["KEYVALUE"].ToString() != appVersion)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Application and Database Version should be same. Please Contact Administrator.');", true);
                            return;
                        }
                    }
                }

            }

            if (Helper.GetDecryptedKey("InstallationType") == "SERVER")
            {
                if (!IsValidIPRequest())
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not a Valid User, Only Ristricted Users are allowed to Login. Please Contact Administrator.');", true);
                    return;
                }
            }

            IsSMSRequired();
            //FormsAuthentication.RedirectFromLoginPage(txtLogin.Text, false);
            Response.Redirect(FormsAuthentication.DefaultUrl, true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Login. Please check the username and password');", true);
            return;
        }
    }

    protected void lnkSignUp_Click(object sender, EventArgs e)
    {

    }

    private string[] GetRoles(string username)
    {
        // Lookup code omitted for clarity
        // This code would typically look up the role list from a database
        // table.
        // If the user was being authenticated against Active Directory,
        // the Security groups and/or distribution lists that the user
        // belongs to may be used instead

        // This GetRoles method returns a pipe delimited string containing
        // roles rather than returning an array, because the string format
        // is convenient for storing in the authentication ticket /
        // cookie, as user data

        BusinessLogic dbManager = new BusinessLogic();
        string[] roles = dbManager.GetRoles(GetConnectionString(), username);

        return roles;
    }

    private string GetConnectionString()
    {
        string connStr = string.Empty;

        string test = Request.Cookies["Company"].Value;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;
        else
            Response.Redirect("~/Login.aspx");

        return connStr;
    }

    private bool IsAuthenticated(string username, string password)
    {
        // Lookup code omitted for clarity
        // This code would typically validate the user name and password
        // combination against a SQL database or Active Directory
        // Simulate an authenticated user
        BusinessLogic objBusLogic = new BusinessLogic();

        DataSet ds = objBusLogic.checkUserCredentials(GetConnectionString(), username, password);

        if (ds != null)
        {
            Session["UserId"] = ds.Tables[0].Rows[0]["UserID"].ToString();
            Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
            Session["UserGroup"] = ds.Tables[0].Rows[0]["UserGroup"].ToString();
        }


        if (ds != null)
            return true;
        else
            return false;

    }

    private void LoadAppSettings()
    {
        BusinessLogic bl = new BusinessLogic();
        DataSet ds = bl.GetAppSettings(Request.Cookies["Company"].Value);

        if (ds != null)
            Session["AppSettings"] = ds;
    }

    private bool IsValidIPRequest()
    {
        // Lookup code omitted for clarity
        // This code would typically validate the user name and password
        // combination against a SQL database or Active Directory
        // Simulate an authenticated user
        DataSet appSettings;
        bool isIPBlockRequired = false;
        bool retVal = false;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "IPBLOCKING")
                {
                    if (appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString() == "YES")
                        isIPBlockRequired = true;
                }
            }
        }

        if (isIPBlockRequired)
        {
            BusinessLogic objBusLogic = new BusinessLogic();

            DataSet ds = objBusLogic.GetIPAddresses(Request.Cookies["Company"].Value);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["IP"].ToString() == Request.UserHostAddress)
                    return true;
            }
        }
        else
        {
            return true;
        }

        return retVal;

    }

    private void IsSMSRequired()
    {
        string sUrl = "http://licence.lathaconsultancy.com/Secure/SMSConfiguration.xml";
        StringBuilder oBuilder = new StringBuilder();
        StringWriter oStringWriter = new StringWriter(oBuilder);
        XmlTextReader oXmlReader = new XmlTextReader(sUrl);
        XmlTextWriter oXmlWriter = new XmlTextWriter(oStringWriter);

        /*
        while (oXmlReader.Read())
        {
            oXmlWriter.WriteNode(oXmlReader, true);
        }*/
        //oXmlReader.Close();
        //oXmlWriter.Close();
        //Response.Write(oBuilder.ToString());
        DataSet ds = new DataSet();

        ds.ReadXml(oXmlReader);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string code = dr[0].ToString();

            if (code.ToUpper() == Request.Cookies["Company"].Value)
            {
                string provider = dr[1].ToString();
                string priority = dr[2].ToString();
                string senderID = dr[3].ToString();
                string userName = dr[4].ToString();
                string password = dr[5].ToString();

                Session["Provider"] = provider;
                Session["Priority"] = priority;
                Session["SenderID"] = senderID;
                Session["UserName"] = userName;
                Session["Password"] = password;

                return;
            }
            else
            {
                Session["Provider"] = null;
                Session["Priority"] = null;
                Session["SenderID"] = null;
                Session["UserName"] = null;
                Session["Password"] = null;
            }
        }

    }

}
