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
using System.Management;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

public partial class Login : System.Web.UI.Page
{

    private Hashtable listComp = new Hashtable();

    [DllImport("Iphlpapi.dll")]
    private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
    [DllImport("Ws2_32.dll")]
    private static extern Int32 inet_addr(string ip);

    string mac_dest = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.btnLogin); 
          //  ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "callme();", true);
            
                string userip = Request.UserHostAddress;
                string strClientIP = Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP);
                Int32 lhost = inet_addr("");
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");
                if (mac_src == "0")
                {
                    if (userip == "127.0.0.1")
                        Response.Write("visited Localhost!");
                    else
                        Response.Write("the IP from" + userip + "" + "<br>");
                    return;
                }

                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }

                Response.Write("welcome" + userip + "<br>" + " MAC address :" + mac_dest + "."

                 + "<br>");
            

            if (!Page.IsPostBack)
            {

                if (Request.QueryString["Type"] != null)
                {
                    if (Request.QueryString["Type"].ToString() == "Demo")
                        DemoLogin();
                }

                foreach (ConnectionStringSettings company in System.Configuration.ConfigurationManager.ConnectionStrings)
                {
                    if (company.Name != "LocalSqlServer" && company.ProviderName != "" && company.Name != "")
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
                Response.Cookies["Expanded"].Expires = DateTime.Now.AddYears(-34);
                Response.Cookies["Selected"].Expires = DateTime.Now.AddYears(-34);

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
        string filename = string.Empty;
        string appVersion = "1.1.1";
        string dbfileName = string.Empty;

        string mac1 = string.Empty;

        string mac = string.Empty;
        //  GetMACAddress();
      //  mac = macAddress.Value;
        mac = mac_dest;

       // 

        Session["macAddress"] = mac;
        mac1 = Session["macAddress"].ToString();
        
      //  BusinessLogic bl1 = new BusinessLogic();
       // bl1.macaddressretrive(mac);



        try
        {
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

            //if (((HiddenField)this.Master.FindControl("hdIsInternetExplorer")).Value != "True")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('For Security Reasons we strongly recommend TROYPLUS Software should be used with Internet Explorer. Request you to please close this browser and use Internet Explorer.');", true);
            //    return;
            //}

            Response.Cookies.Clear();

            foreach (HttpCookie ck in Response.Cookies)
            {
                ck.Expires = DateTime.Now.AddDays(-1);
                Response.AppendCookie(ck);
            }

            HttpCookie cookie = new HttpCookie("Company");


            if (txtCompany.Text != "")            
            {
                cookie.Value = txtCompany.Text;

                if (Response.Cookies["Company"] == null)
                    Response.Cookies.Add(cookie);
                else
                    Response.SetCookie(cookie);
            }
            else
                return;


            string sCustomer = string.Empty;

            BusinessLogic bl = new BusinessLogic();
            DataSet ds1 = bl.GetBranch(txtCompany.Text, txtLogin.Text);
            DataSet dss = new DataSet();


            drpBranch.Items.Clear();
            drpBranch.Items.Add(new ListItem("Select Branch", "0"));
            dss = bl.ListBranchLogin(txtCompany.Text);
            drpBranch.DataSource = dss;
            drpBranch.DataBind();
            drpBranch.DataTextField = "BranchName";
            drpBranch.DataValueField = "Branchcode";


            if (ds1.Tables[0].Rows.Count > 0)
            {
                sCustomer = Convert.ToString(ds1.Tables[0].Rows[0]["DefaultBranchCode"]);
                drpBranch.ClearSelection();
                ListItem li = drpBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
                if (li != null) li.Selected = true;
                txtPassword.Focus();
                HttpCookie cookie1 = new HttpCookie("Branch");


                if (ds1.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
                {
                    drpBranch.Enabled = true;

                    cookie1.Value = "All";
                    if (Response.Cookies["Branch"] == null)
                        Response.Cookies.Add(cookie1);
                    else
                        Response.SetCookie(cookie1);
                }
                else
                {
                    drpBranch.Enabled = false;

                    cookie1.Value = drpBranch.SelectedValue;
                    if (Response.Cookies["Branch"] == null)
                        Response.Cookies.Add(cookie1);
                    else
                        Response.SetCookie(cookie1);
                }
            }


            //HttpCookie cookie1 = new HttpCookie("Branch");
            //if (drpBranch.SelectedValue != "0")
            //{
            //    cookie1.Value = drpBranch.SelectedValue;

            //    if (Response.Cookies["Branch"] == null)
            //        Response.Cookies.Add(cookie1);
            //    else
            //        Response.SetCookie(cookie1);
            //}
            //else
            //    return;


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


           // BusinessLogic bl = new BusinessLogic();

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

                if (Response.Cookies[FormsAuthentication.FormsCookieName] == null)
                    Response.Cookies.Add(authCookie);
                else
                    Response.SetCookie(authCookie);

                LoadAppSettings();


                if (Session["AppSettings"] != null)
                {
                    DataSet ds = (DataSet)Session["AppSettings"];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["KEYNAME"].ToString() == "VERSION")
                        {
                            if (ds.Tables[0].Rows[i]["KEYNAME"].ToString() == "CURRENCY")
                            {
                                Session["CurrencyType"] = ds.Tables[0].Rows[i]["KEYVALUE"].ToString();
                            }

                            if (ds.Tables[0].Rows[i]["KEYNAME"].ToString() == "OWNERMOB")
                            {
                                Session["OWNERMOB"] = ds.Tables[0].Rows[i]["KEYVALUE"].ToString();
                            }

                            if (ds.Tables[0].Rows[i]["KEYVALUE"].ToString() != appVersion)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Application and Database Version should be same. Please Contact Administrator.');", true);
                            }
                        }
                    }

                }

                string id = Helper.GetDecryptedKey("InstallationType");


                //if ((Helper.GetDecryptedKey("InstallationType") == "ONLINE-OFFLINE-SERVER") ||
                //    (Helper.GetDecryptedKey("InstallationType") == "SERVER"))

                if ((System.Configuration.ConfigurationManager.AppSettings["InstallationType"].ToString() == "ONLINE-OFFLINE-SERVER") ||
                    (System.Configuration.ConfigurationManager.AppSettings["InstallationType"].ToString() == "SERVER"))
                {
                    if (!IsValidIPRequest())
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not a Valid User, Only Ristricted Users are allowed to Login. Please Contact Administrator.');", true);
                        return;
                    }
                }

                IsSMSRequired();

                CheckDateLock();

                if (!(CheckPasswordExpiry(txtLogin.Text)))
                {
                    Response.Redirect("PasswordExpiry.aspx");
                }

                int expdays = 10;
                if ((expdays == 0) || (expdays < 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your password is expired. Please Contact Administrator.');", true);
                    return;
                }
                else if ((expdays < 7) && (expdays > 0))
                {
                    DialogResult dr = MessageBox.Show("Your password will expire within 7 days. Do you want to change the password now?", "Password Expiry Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        Response.Redirect("ChangePassword.aspx");
                    }
                    else
                    {
                        if (!bl.GetSalesRole(Request.Cookies["Company"].Value, txtLogin.Text))
                        {
                            Response.Redirect("Default.aspx");
                        }
                        else
                        {
                            Response.Redirect(FormsAuthentication.DefaultUrl, true);
                        }
                    }
                }
                else
                {
                    if (!bl.GetSalesRole(Request.Cookies["Company"].Value, txtLogin.Text))
                    {
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        Response.Redirect(FormsAuthentication.DefaultUrl, true);
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Login. Please check the username and password');", true);
                Response.Cookies.Clear();
                return;


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }

    }

    private bool CheckPasswordExpiry(string userName)
    {
        BusinessLogic bl = new BusinessLogic();

        string ExpiryDate = bl.GetExpiryDate(userName, Request.Cookies["Company"].Value);

        if (ExpiryDate == null || ExpiryDate.ToString() == string.Empty)
        {
            return false;
        }
        else
        {
            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            ExpiryDate = Convert.ToDateTime(ExpiryDate).ToString("dd/MM/yyyy");

            TimeSpan ts = Convert.ToDateTime(ExpiryDate) - Convert.ToDateTime(dtaa);
            int days = Convert.ToInt32(ts.TotalDays);

            if (days > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
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

            HttpCookie authCookie = FormsAuthentication.GetAuthCookie("admin", true);
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
                    if (ds.Tables[0].Rows[i]["KEY"].ToString() == "CURRENCY")
                    {
                        Session["CurrencyType"] = ds.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    }

                    if (ds.Tables[0].Rows[i]["KEY"].ToString() == "VERSION")
                    {
                        if (ds.Tables[0].Rows[i]["KEYVALUE"].ToString() != appVersion)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Application and Database Version should be same. Please Contact Administrator.');", true);
                        }
                    }

                }

            }

            if ((Helper.GetDecryptedKey("InstallationType") == "ONLINE-OFFLINE-SERVER") ||
                (Helper.GetDecryptedKey("InstallationType") == "SERVER"))
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
        try
        {

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[txtCompany.Text.ToUpper()].ConnectionString;
        //connStr = System.Configuration.ConfigurationManager.ConnectionStrings[drpBranch.Text].ConnectionString;
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
            HttpCookie cookieUserID = new HttpCookie("UserId");
            HttpCookie cookieLoggedUser = new HttpCookie("LoggedUserName");
            HttpCookie cookieUserGroup = new HttpCookie("UserGroup");

            cookieUserID.Value = ds.Tables[0].Rows[0]["UserID"].ToString();
            cookieLoggedUser.Value = ds.Tables[0].Rows[0]["UserName"].ToString();
            cookieUserGroup.Value = ds.Tables[0].Rows[0]["UserGroup"].ToString();

            Response.Cookies.Add(cookieUserID);
            Response.Cookies.Add(cookieLoggedUser);
            Response.Cookies.Add(cookieUserGroup);
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

    private void CheckDateLock()
    {
        BusinessLogic bl = new BusinessLogic();
        bl.checkdatelock(Request.Cookies["Company"].Value);

    }

    private int CheckPasswordExpiry()
    {
        BusinessLogic bl = new BusinessLogic();

        string ExpiryDate = bl.GetExpiryDate(txtLogin.Text, Request.Cookies["Company"].Value);

        DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
        string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

        TimeSpan ts = Convert.ToDateTime(ExpiryDate) - Convert.ToDateTime(dtaa);
        int days = Convert.ToInt32(ts.TotalDays);
        return days;

        //if (Convert.ToInt32(t.ToString("#0")) == 0)
        //{
        //    return true;
        //}
        //else if (Convert.ToInt32(t.ToString("#0")) < 0)
        //{
        //    return true;
        //}
        //else if (Convert.ToInt32(t.ToString("#0")) == 7)
        //{
        //    return retval;
        //}
        //else if ((Convert.ToInt32(t.ToString("#0")) < 7) && (Convert.ToInt32(t.ToString("#0")) > 0))
        //{
        //    return retval;
        //}
        //else
        //{
        //    return true;
        //}
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
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "CURRENCY")
                {
                    Session["CurrencyType"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "IPBLOCKING")
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
                if (ds.Tables[0].Rows[i]["IP"].ToString() == macAddress.Value)
                    return true;
            }
        }
        else
        {
            return true;
        }

        return retVal;

    }

    public string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
                sMacAddress = sMacAddress.Replace(":", "");

            }
           
           // return sMacAddress;
        } return sMacAddress;
    }

    private void IsSMSRequired()
    {
        try
        {
            string sUrl = "http://licence.jandjgroups.com/Secure/SMSConfiguration.xml";
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
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    //private void loadBranch()
    //{
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    DataSet ds = new DataSet();
    //    string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

    //drpBranch.Items.Clear();
    //drpBranch.Items.Add(new ListItem("Select Branch", "0"));
    //ds = bl.ListBranch();
    //drpBranch.DataSource = ds;
    //drpBranch.DataBind();
    //drpBranch.DataTextField = "BranchName";
    //drpBranch.DataValueField = "Branchcode";
    //}

}
