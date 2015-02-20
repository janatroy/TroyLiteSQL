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
using System.IO;
using ClientLog;
using System.Xml;

public partial class Configuration : System.Web.UI.Page
{
    private Log _logfile;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string dbfileName = string.Empty;
            string filename = string.Empty;
            string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
            // DecryptAppSettings();
            //_logfile = new Log();
            lblMsg.Visible = true;

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

            dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            filename = Server.MapPath(localpath + dbfileName + ".offline");
            BusinessLogic objChk = new BusinessLogic();
            /*
            Response.Write(EncryptDecrypt.Encrypt("ftp://accounts.troy-plus.com"));
            Response.Write("</br>");
            Response.Write(  ConfigurationManager.AppSettings["OfflineRemotePath"].ToString());
            Response.Write("</br>");
            Response.Write(  EncryptDecrypt.Encrypt("troyplus"));
            Response.Write("</br>");
            Response.Write(EncryptDecrypt.Encrypt("Py8i4Euw57"));
            */
            if (!objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                btnOffline.Enabled = false;
                btnSyncOnline.Enabled = true;
                btnOnline.Enabled = true;
                btnContOffline.Enabled = false;
                lblMsg.Text = "The application is currently configured to work Offline.";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                btnSyncOnline.Enabled = false;
                btnOffline.Enabled = true;
                btnOnline.Enabled = false;
                btnContOffline.Enabled = true;
                lblMsg.Text = "The application is currently configured to work Online.";
                lblMsg.ForeColor = System.Drawing.Color.Orange;
            }

            if (!Page.IsPostBack)
            {
                btnOffline.Attributes.Add("onclick", "return ConfirmOffline();");
                btnOnline.Attributes.Add("onclick", "return ConfirmOnline();");
                btnContOffline.Attributes.Add("onclick", "return ConfirmContinueOffline();");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private bool UploadDB()
    {

        string server = string.Empty;
        string remotepath = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = string.Empty;
        string connStr = string.Empty;
        string remoteBackUp = string.Empty;
        string backUpFileName = string.Empty;

        localpath = ConfigurationManager.AppSettings["LocalPath"].ToString();
        server = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["Server"].ToString());
        remotepath = ConfigurationManager.AppSettings["RemotePath"].ToString();
        username = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["username"].ToString());
        password = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["password"].ToString());
        remoteBackUp = ConfigurationManager.AppSettings["RemoteBackUpPath"].ToString();

        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName);

        backUpFileName = remoteBackUp + Request.Cookies["Company"].Value + "/" + dbfileName + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString();

        if (ZipDBFile(filename, filename + ".zip"))
        {

            _logfile.WriteToLog("SRC : " + filename);
            _logfile.WriteToLog("DST : " + remotepath + dbfileName);
            _logfile.WriteToLog("Upload Started... ");

            FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
            ftp2.Upload(filename + ".zip", remotepath + dbfileName + ".zip");

            _logfile.WriteToLog("Upload Successfull... ");

            if (ftp2.FtpFileExists(remotepath + dbfileName))
            {
                ftp2.FtpRename(remotepath + dbfileName, backUpFileName);
                _logfile.WriteToLog("Server DB file BackedUp : " + backUpFileName);
            }
            else
            {
                _logfile.WriteToLog("Server DB file not found : " + remotepath + dbfileName);
            }

            File.Delete(filename + ".zip");

            return true;

        }
        else
        {
            _logfile.WriteToLog("Zip unsuccessfull : " + filename);
            return false;
        }

    }

    public bool ZipDBFile(string localfileName, string zipFileName)
    {
        try
        {
            GZip objZip = new GZip(localfileName, zipFileName, Action.Zip);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private void RenameLocalFile()
    {
        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        string connStr = string.Empty;

        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName + ".offline");

        //File.Move(filename, Server.MapPath(localpath + dbfileName + ".online"));

        File.Copy(filename, Server.MapPath(localpath + dbfileName + ".online"), true);

        //Helper.CheckReadWriteAccces(filename, System.Security.AccessControl.FileSystemRights.Delete);

        //Check the file actually exists
        if (File.Exists(filename))
        {
            Helper.MakeWritable(filename);
            //If its readonly set it back to normal   //Need to "AND" it as it can also be archive, hidden etc 
            if ((File.GetAttributes(filename) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                File.SetAttributes(filename, FileAttributes.Normal);
            //Delete the file
            File.Delete(filename);
        }

        _logfile.WriteToLog(" File Renamed From : " + filename + " To : " + Server.MapPath(localpath + dbfileName + ".online") + " Successfully.");

    }

    private void SendOfflineFileToServer()
    {

        string server = string.Empty;
        string remotepath = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        string connStr = string.Empty;

        server = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["Server"].ToString());
        remotepath = ConfigurationManager.AppSettings["OfflineRemotePath"].ToString();
        username = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["username"].ToString());
        password = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["password"].ToString());

        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName + ".offline");

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
        ftp2.Upload(filename, remotepath + dbfileName + ".offline");
        _logfile.WriteToLog(" Local file " + filename + " Sent Successfully to " + remotepath + dbfileName + ".offline");

    }

    private void SendOnlineFile()
    {
        string server = string.Empty;
        string remotepath = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        string connStr = string.Empty;

        server = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["Server"].ToString());
        remotepath = ConfigurationManager.AppSettings["OfflineRemotePath"].ToString();
        username = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["username"].ToString());
        password = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["password"].ToString());

        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName + ".online");

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
        ftp2.Upload(filename, remotepath + dbfileName + ".online");
        _logfile.WriteToLog(" Local file " + filename + " Sent Successfully to " + remotepath + dbfileName + ".online");

    }


    private void DownloadDB()
    {

        string server = string.Empty;
        string remotepath = string.Empty;
        string remoteFile = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string localpath = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;

        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        server = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["Server"].ToString());
        remotepath = ConfigurationManager.AppSettings["RemotePath"].ToString();
        remoteFile = dbfileName;
        username = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["username"].ToString());
        password = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["password"].ToString());
        localpath = ConfigurationManager.AppSettings["LocalPath"].ToString();
        filename = Server.MapPath(localpath + dbfileName);

        FileInfo info = new FileInfo(filename);
        info.CopyTo(Path.ChangeExtension(info.FullName + info.Extension, DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()), false);
        File.Delete(info.Name);

        _logfile.WriteToLog("DB File " + info.FullName + " renamed Successfully... ");

        _logfile.WriteToLog("SRC : " + remotepath + remoteFile);
        _logfile.WriteToLog("DST : " + filename);
        _logfile.WriteToLog("Download Started... ");

        Helper.MakeWritable(filename);
        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);

        if (ftp2.FtpFileExists(remotepath + remoteFile + ".zip"))
        {
            ftp2.Download(remotepath + remoteFile + ".zip", filename + ".zip", true);
            _logfile.WriteToLog("Download File : " + remotepath + remoteFile + ".zip");
        }
        else
        {
            ftp2.Download(remotepath + remoteFile, filename, true);
            _logfile.WriteToLog("Download File : " + remotepath + remoteFile);
        }

        _logfile.WriteToLog("Download Completed... ");

    }

    private bool CheckConfigFile()
    {
        string server = string.Empty;
        string remotepath = string.Empty;
        string remoteFile = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string localpath = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;

        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        string xmlfileName = "Configuration.xml";

        server = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["Server"].ToString());
        remotepath = ConfigurationManager.AppSettings["RemotePath"].ToString();
        remoteFile = xmlfileName;
        username = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["username"].ToString());
        password = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["password"].ToString());
        localpath = ConfigurationManager.AppSettings["LocalPath"].ToString();
        filename = Server.MapPath(localpath + xmlfileName);

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);
        ftp2.Download(remotepath + remoteFile, filename, true);

        DataSet ds = new DataSet();
        ds.ReadXml(filename);

        File.Delete(filename);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["DBName"].ToString().ToLower() == dbfileName.ToLower())
            {
                if (dr["Key"].ToString() == EncryptDecrypt.Encrypt(Helper.GenerateUniqueIDForThisPC()))
                {
                    _logfile.WriteToLog("License is matching.");
                    return true;
                }
            }
        }

        _logfile.WriteToLog("License match:");
        _logfile.WriteToLog("License:" + EncryptDecrypt.Encrypt(Helper.GenerateUniqueIDForThisPC()));
        return false;

    }

    private bool IsInitialChecksOK()
    {
        string server = string.Empty;
        string remotepath = string.Empty;
        string remoteFile = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;


        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        // DecryptAppSettings();

        server = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["Server"].ToString());
        remotepath = ConfigurationManager.AppSettings["OfflineRemotePath"].ToString();
        username = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["username"].ToString());
        password = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["password"].ToString());

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
        remoteFile = dbfileName;

        DirectoryInfo dInfo = new DirectoryInfo(Server.MapPath(localpath));
        FileInfo[] files = dInfo.GetFiles(dbfileName + ".*");
        //Check for 2 temp files locally
        if (files.Length > 1)
        {
            _logfile.WriteToLog("Initial Check Failed at Step: 1. Please contact Administrator.");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Initial Check Failed at Step: 1. Please contact Administrator.')", true);
            return false;
        }
        else if (files.Length == 0)
        {
            _logfile.WriteToLog("No Offline file found on the Client PC. Please contact Administrator.");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Offline file found on the Client PC. Please contact Administrator.')", true);
            return false;
        }
        else
        {

        }

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);

        int fileCount = 0;

        if (ftp2.FtpFileExists(remotepath + remoteFile + ".offline"))
        {
            fileCount = fileCount + 1;
        }

        if (ftp2.FtpFileExists(remotepath + remoteFile + ".online"))
        {
            fileCount = fileCount + 1;
        }
        //Check for 2 temp files on the server
        if (fileCount > 1)
        {
            _logfile.WriteToLog("Initial Check Failed at Step : 2. Please contact Administrator.");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Initial Check Failed at Step : 2. Please contact Administrator.')", true);
            return false;
        }

        //Check for same file on the server and client

        if (!ftp2.FtpFileExists(remotepath + files[0].Name))
        {
            _logfile.WriteToLog("Initial Check Failed at Step : 3. Please contact Administrator.");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Initial Check Failed at Step : 3. Please contact Administrator.')", true);
            return false;
        }

        return true;

    }

    private void CreateOnlineFile()
    {

        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        string onlineFile = string.Empty;
        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName + ".offline");
        onlineFile = Server.MapPath(localpath + dbfileName + ".online");

        //Check the file actually exists
        if (File.Exists(onlineFile))
        {
            Helper.MakeWritable(onlineFile);
            //If its readonly set it back to normal   //Need to "AND" it as it can also be archive, hidden etc 
            if ((File.GetAttributes(onlineFile) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                File.SetAttributes(onlineFile, FileAttributes.Normal);
            //Delete the file
            File.Delete(onlineFile);
        }

        //Check the file actually exists
        if (File.Exists(filename))
        {
            Helper.MakeWritable(filename);
            //If its readonly set it back to normal   //Need to "AND" it as it can also be archive, hidden etc 
            if ((File.GetAttributes(filename) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                File.SetAttributes(filename, FileAttributes.Normal);
            //Delete the file
            File.Delete(filename);
        }


        StreamWriter SW;
        SW = File.CreateText(onlineFile);

        ManagementObjectSearcher query1 = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
        ManagementObjectCollection queryCollection1 = query1.Get();

        foreach (ManagementObject mo in queryCollection1)
        {
            SW.WriteLine("Name : " + mo["name"].ToString());
            SW.WriteLine("Version : " + mo["version"].ToString());
            SW.WriteLine("Manufacturer : " + mo["Manufacturer"].ToString());
            SW.WriteLine("Computer Name : " + mo["csname"].ToString());
            SW.WriteLine("Windows Directory : " + mo["WindowsDirectory"].ToString());
            SW.WriteLine("Date :" + DateTime.Now.ToString());
        }

        SW.Close();
        _logfile.WriteToLog("Local online File Created " + onlineFile + " Successfully.");

    }

    private void CreateOfflineFile()
    {

        string dbfileName = string.Empty;
        string filename = string.Empty;
        string localpath = ConfigurationManager.AppSettings["OfflineLocalPath"].ToString();
        string onlineFile = string.Empty;
        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        filename = Server.MapPath(localpath + dbfileName + ".offline");
        onlineFile = Server.MapPath(localpath + dbfileName + ".online");

        //Check the file actually exists
        if (File.Exists(onlineFile))
        {
            Helper.MakeWritable(onlineFile);
            //If its readonly set it back to normal   //Need to "AND" it as it can also be archive, hidden etc 
            if ((File.GetAttributes(onlineFile) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                File.SetAttributes(onlineFile, FileAttributes.Normal);
            //Delete the file
            File.Delete(onlineFile);
        }

        //Check the file actually exists
        if (File.Exists(filename))
        {
            Helper.MakeWritable(filename);
            //If its readonly set it back to normal   //Need to "AND" it as it can also be archive, hidden etc 
            if ((File.GetAttributes(filename) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                File.SetAttributes(filename, FileAttributes.Normal);
            //Delete the file
            File.Delete(filename);
        }


        StreamWriter SW;
        SW = File.CreateText(filename);

        ManagementObjectSearcher query1 = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
        ManagementObjectCollection queryCollection1 = query1.Get();

        foreach (ManagementObject mo in queryCollection1)
        {
            SW.WriteLine("Name : " + mo["name"].ToString());
            SW.WriteLine("Version : " + mo["version"].ToString());
            SW.WriteLine("Manufacturer : " + mo["Manufacturer"].ToString());
            SW.WriteLine("Computer Name : " + mo["csname"].ToString());
            SW.WriteLine("Windows Directory : " + mo["WindowsDirectory"].ToString());
            SW.WriteLine("Date :" + DateTime.Now.ToString());
        }

        SW.Close();
        _logfile.WriteToLog("Local Offline File Created " + filename + " Successfully.");

    }

    protected void btnOnline_Click(object sender, EventArgs e)
    {
        try
        {
            //Upload the file to server
            _logfile.WriteToLog("Online Configuration Started...");

            if (IsInitialChecksOK())
            {
                if (CheckConfigFile())
                {
                    if (UploadDB())
                    {
                        //Rename the local file from offline to online
                        CreateOnlineFile();
                        //Delete the offline file on the server to make it as online
                        DeleteFile();
                        // send online file
                        SendOnlineFile();
                    }

                    _logfile.WriteToLog("Online Configuration Completed...");

                    btnOffline.Enabled = true;
                    btnOnline.Enabled = false;
                    btnContOffline.Enabled = true;
                    lblMsg.Text = "The application is currently configured to work Online.";
                    lblMsg.ForeColor = System.Drawing.Color.Orange;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application was successfully configured to work Online. To work Online please switch it back to Offline.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to configure the system to Offline. Please contact Administrator.');", true);
                }
            }
            else
            {
                _logfile.WriteToLog("Initial Check Failed...");
            }

        //}
        //catch (Exception ex)
        //{
        //    _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());
        //    if (ex.InnerException != null)
        //        _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());

        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application is unable to configur to work Online. Please contact Administrator.');", true);

        //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void DeleteFile()
    {
        string server = string.Empty;
        string remotepath = string.Empty;
        string remoteFile = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;

        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        server = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["Server"].ToString());
        remotepath = ConfigurationManager.AppSettings["OfflineRemotePath"].ToString();
        remoteFile = dbfileName;
        username = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["username"].ToString());
        password = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["password"].ToString());

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);

        _logfile.WriteToLog("Server File to Delete : " + remotepath + remoteFile + ".offline");

        if (ftp2.FtpFileExists(remotepath + remoteFile + ".offline"))
        {
            ftp2.FtpDelete(remotepath + remoteFile + ".offline");
            _logfile.WriteToLog("File Delete Successfully");
        }

    }

    private void DeleteOnlineFile()
    {
        string server = string.Empty;
        string remotepath = string.Empty;
        string remoteFile = string.Empty;
        string username = string.Empty;
        string password = string.Empty;
        string dbfileName = string.Empty;
        string filename = string.Empty;

        // DecryptAppSettings();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

        dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        server = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["Server"].ToString());
        remotepath = ConfigurationManager.AppSettings["OfflineRemotePath"].ToString();
        remoteFile = dbfileName;
        username = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["username"].ToString());
        password = EncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["password"].ToString());

        FtpClient.FtpClient ftp2 = new FtpClient.FtpClient(server, username, password);

        _logfile.WriteToLog("Server File to Delete : " + remotepath + remoteFile + ".online");

        if (ftp2.FtpFileExists(remotepath + remoteFile + ".online"))
        {
            ftp2.FtpDelete(remotepath + remoteFile + ".online");
            _logfile.WriteToLog("File Delete Successfully");
        }

    }


    protected void btnContOffline_Click(object sender, EventArgs e)
    {
        try
        {
            _logfile.WriteToLog("Conti Offline Configuration Started...");

            if (IsInitialChecksOK())
            {
                if (CheckConfigFile())
                {
                    CreateOfflineFile();
                    DeleteOnlineFile();
                    SendOfflineFileToServer();

                    _logfile.WriteToLog("Conti Offline Configuration Completed...");

                    btnOffline.Enabled = false;
                    btnOnline.Enabled = true;
                    btnContOffline.Enabled = false;
                    lblMsg.Text = "The application is currently configured to work Offline.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application was successfully configured to work Offline. To work Online please switch it back to Online.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to configure the system to Offline. Please contact Administrator.');", true);
                }
            }
            else
            {
                _logfile.WriteToLog("Initial Check Failed...");
            }

        //}
        //catch (Exception ex)
        //{
        //    _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());
        //    if (ex.InnerException != null)
        //        _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());

        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application is unable to configur to work Online. Please contact Administrator.');", true);

        //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnOffline_Click(object sender, EventArgs e)
    {
        try
        {

            _logfile.WriteToLog("Offline Configuration Started...");
            //Download the file

            if (IsInitialChecksOK())
            {

                if (CheckConfigFile())
                {

                    DownloadDB();
                    //Create a temp file on the local machine in Offline Folder
                    CreateOfflineFile();

                    //Delete temp online file on the server
                    DeleteOnlineFile();

                    // Send the temp file in Offline Folder to server Offline Folder
                    SendOfflineFileToServer();

                    _logfile.WriteToLog("Offline Configuration Completed...");

                    btnOffline.Enabled = false;
                    btnOnline.Enabled = true;
                    btnContOffline.Enabled = false;
                    lblMsg.Text = "The application is currently configured to work Offline.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application was successfully configured to work Offline. To work Online please switch it back to Online.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to configure the system to Offline. Please contact Administrator.');", true);
                }
            }
            else
            {
                _logfile.WriteToLog("Initial Check Failed...");
            }

        //}
        //catch (Exception ex)
        //{
        //    _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());
        //    if (ex.InnerException != null)
        //        _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());

        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application is unable to configure to work Offline. Please contact Administrator.');", true);

        //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSyncOnline_Click(object sender, EventArgs e)
    {
        try
        {
            //Upload the file to server
            _logfile.WriteToLog("Synchronise Configuration Started...");

            if (IsInitialChecksOK())
            {
                if (CheckConfigFile())
                {

                    UploadDB();

                    _logfile.WriteToLog("Synchronisation Configuration Completed...");

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Online Synchronisation Completed Successfully.')", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not allowed to configure the system to Offline. Please contact Administrator.');", true);
                }
            }
            else
            {
                _logfile.WriteToLog("Initial Check Failed...");
            }

        //}
        //catch (Exception ex)
        //{
        //    _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());
        //    if (ex.InnerException != null)
        //        _logfile.WriteToLog("Exception Msg : " + ex.Message + " Trace : " + ex.StackTrace.ToString());

        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The application is unable to configur to work Online. Please contact Administrator.');", true);

        //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
