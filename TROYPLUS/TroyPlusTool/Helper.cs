using System;
using System.Data;
using System.Configuration;
using System.Management;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
using System.Linq;
/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    public Helper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool CheckReadWriteAccces(string filePath, System.Security.AccessControl.FileSystemRights fileSystemRights)
    {
        FileInfo fileInfo = new FileInfo(filePath);

        string str = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToUpper();
        foreach (System.Security.AccessControl.FileSystemAccessRule rule in fileInfo.GetAccessControl().GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
        {
            if (str == rule.IdentityReference.Value.ToUpper())
                return ((rule.AccessControlType == System.Security.AccessControl.AccessControlType.Allow) && (fileSystemRights == (rule.FileSystemRights & fileSystemRights)));
        }

        return false;
    }


    /// <summary> 
    /// Make a file writteble 
    /// </summary> 
    /// <param name="path">File name to change</param> 
    public static void MakeWritable(string path)
    {
        if (!File.Exists(path))
            return;
        File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReadOnly);
    }

    public static bool IsGreaterThan10(GridView gv)
    {
        if (gv.PageCount > 10)
            return true;
        else
            return false;
    }

    

    public static string GenerateUniqueIDForThisPC()
    {

        /*
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

        string uniqueID = string.Empty;

        foreach (ManagementObject wmi_HD in searcher.Get())
        {
            foreach (PropertyData oPropert in wmi_HD.Properties)
            {
                if (oPropert.Value != null)
                {
                    if ((oPropert.Name == "Model"))
                        uniqueID = uniqueID + oPropert.Value;

                    if ((oPropert.Name == "Signature"))
                        uniqueID = uniqueID + oPropert.Value;

                    //if ((oPropert.Name == "SystemName"))
                    //    uniqueID = uniqueID + oPropert.Value;

                }
            }
        }*/

        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection moc = mc.GetInstances();
        string MACAddress = "";

        foreach (ManagementObject mo in moc)
        {
            if (mo["MacAddress"] != null)
            {
                MACAddress = mo["MacAddress"].ToString();
                break;
            }
        }

        return MACAddress;
    }

    public static string GetPasswordForDB(string connStr)
    {
        string connection = string.Empty;

        string encrptedString = connStr.Remove(0, connStr.Remove(connStr.LastIndexOf("Password=") + 9).Length);

        connection = EncryptDecrypt.DecryptString(encrptedString, "XY$TRO*YUHJ&*MWEE");

        return connection;

    }

    public static void SetFocus(Control control)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n<script language='JavaScript'>\r\n");
        sb.Append("<!--\r\n");
        sb.Append("function SetFocus()\r\n");
        sb.Append("{\r\n");
        sb.Append("\t if( document.");

        Control p = control.Parent;
        while (!(p is System.Web.UI.HtmlControls.HtmlForm)) p = p.Parent;

        sb.Append(p.ClientID);
        sb.Append("['");
        sb.Append(control.UniqueID);
        sb.Append("'] != null){\r\n");
        sb.Append("\tdocument.");
        sb.Append(p.ClientID);
        sb.Append("['");
        sb.Append(control.UniqueID);
        sb.Append("'].focus();}\r\n");
        sb.Append("}\r\n");
        sb.Append("window.Onscroll = SetFocus;\r\n");
        sb.Append("// -->\r\n");
        sb.Append("</script>");

        control.Page.RegisterClientScriptBlock("SetFocus", sb.ToString());
    }

    public static bool IsValidInstallationType()
    {
        string[] validTypes = new string[] { "ONLINE-OFFLINE-CLIENT", "ONLINE-OFFLINE-SERVER", "CLIENT", "SERVER" };
        var currentType = ConfigurationManager.AppSettings["InstallationType"].ToString();
        return validTypes.Contains(currentType);
    }

    public static bool IsThisClientInstallation()
    {
        if (IsValidInstallationType())
        {
            var currentType = ConfigurationManager.AppSettings["InstallationType"].ToString();

            if (currentType.Contains("CLIENT"))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public static DateTime GetIndianStandardDateTime()
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
    }

    public static string GetIndiaStandardDateTime()
    {
        DateTime dt = GetIndianStandardDateTime();
        return Convert.ToDateTime(dt).ToString("dd-MM-yyyy-hh-mm-ss");
    }

}
