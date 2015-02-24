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
using System.Data.OleDb;
using DataAccessLayer;
using SMSLibrary;

public partial class ReconDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            string connStr = string.Empty;

            myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
            myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                lnkBtnUpdate.Enabled = false;
            }

            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = objChk.CreateConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString);

            try
            {
                manager.Open();
                object reconDate = manager.ExecuteScalar(CommandType.Text, "Select recon_date from last_recon");

                if (reconDate != null)
                    txtReconDate.Text = DateTime.Parse(reconDate.ToString()).ToString("dd/MM/yyyy");

                //if (Request.Cookies["Company"] != null)
                //    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                BusinessLogic bl = new BusinessLogic(connStr);

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

                DataSet ds = bl.GetSettings();
                
                if (ds != null)
                {
                    if (ds.Tables[0] != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (dr["KeyName"].ToString() == "ENBLDATE")
                                {
                                    if (dr["KeyValue"] != null)
                                        rdvoudateenable.SelectedValue = dr["KeyValue"].ToString();
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

    }

    protected void lnkBtnUpdate_Click(object sender, EventArgs e)
    {
        BusinessLogic objBus = new BusinessLogic();
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = objBus.CreateConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString);
        
        string enabledate = string.Empty;
        enabledate = rdvoudateenable.SelectedValue;

        try
        {
            manager.Open();
            manager.ExecuteNonQuery(CommandType.Text, "Update last_recon Set recon_date='" + Convert.ToDateTime(txtReconDate.Text).ToString("yyyy-MM-dd") + "'");

            manager.ExecuteNonQuery(CommandType.Text, "UPDATE tblSettings SET KEYVALUE = '" + enabledate.ToString() + "' WHERE KEYNAME='ENBLDATE' ");

            string salestype = string.Empty;
            int ScreenNo = 0;
            string ScreenName = string.Empty;

            salestype = "Lock Transaction";
            ScreenName = "Lock Transaction";


            string usernam = Request.Cookies["LoggedUserName"].Value;

            bool mobile = false;
            bool Email = false;
            string emailsubject = string.Empty;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();

            string emailcontent = string.Empty;
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
                            Email = Convert.ToBoolean(dr["Email"]);
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
                            if (Email == true)
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

                                int index2 = emailcontent.IndexOf("@Date");
                                body = txtReconDate.Text;
                                if (index2 >= 0)
                                {
                                    emailcontent = emailcontent.Remove(index2, 5).Insert(index2, body);
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
                                    smscontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                }

                                int index312 = smscontent.IndexOf("@User");
                                body = usernam;
                                if (index312 >= 0)
                                {
                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                }

                                int index2 = smscontent.IndexOf("@Date");
                                body = txtReconDate.Text;
                                if (index2 >= 0)
                                {
                                    smscontent = smscontent.Remove(index2, 5).Insert(index2, body);
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


            //errorDisplay.AddItem("Recon Date updated successfully." , DisplayIcons.GreenTick,false);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Recon Date updated successfully.');", true);
            return;

            

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
}
