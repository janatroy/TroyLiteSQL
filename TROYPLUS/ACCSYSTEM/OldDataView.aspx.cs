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
using System.Web.Configuration;
public partial class OldDataView : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //SetDbName();
                string path = Server.MapPath("OldYear");
                string concat = string.Empty;
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                string curr = string.Empty;
                string monthname = string.Empty;
                if (DateTime.Now.Month == 12)
                    monthname = "December";
                else if (DateTime.Now.Month == 11)
                    monthname = "November";
                else if (DateTime.Now.Month == 10)
                    monthname = "October";
                else if (DateTime.Now.Month == 9)
                    monthname = "September";
                else if (DateTime.Now.Month == 8)
                    monthname = "August";
                else if (DateTime.Now.Month == 7)
                    monthname = "July";
                else if (DateTime.Now.Month == 6)
                    monthname = "June";
                else if (DateTime.Now.Month == 5)
                    monthname = "May";
                else if (DateTime.Now.Month == 4)
                    monthname = "April";
                else if (DateTime.Now.Month == 3)
                    monthname = "March";
                else if (DateTime.Now.Month == 2)
                    monthname = "February";
                else
                    monthname = "January";

                curr = DateTime.Now.Year + " " + monthname;
                FileInfo[] fi = dirInfo.GetFiles();
                //drpYear.Items.Add(curr);
                drpYear.Items.Add("Current Data");
                foreach (FileInfo fiTemp in fi)
                {
                    if (File.Exists(Server.MapPath("OldYear\\" + fiTemp.Name)))
                    {
                        string[] fil = fiTemp.Name.Split('_');
                        concat = fil[2].ToString().Remove(fil[2].Length - 4, 4);
                        concat = concat + " " + fil[1].ToString();
                        drpYear.Items.Add(new ListItem(concat, fiTemp.Name));
                    }

                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public string GetCurrentDBName(string con)
    {
        string str = con; // "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\DemoDev\\Accsys\\App_Data\\sairama.mdb;Persist Security Info=True;Jet OLEDB:Database Password=moonmoon";
        string str2 = string.Empty;
        if (str.Contains(".mdb"))
        {
            str2 = str.Substring(str.IndexOf("Data Source"), str.IndexOf("Persist", 0));
            str2 = str2.Substring(str2.LastIndexOf("\\") + 1, str2.IndexOf(";") - str2.LastIndexOf("\\"));
            if (str2.EndsWith(";"))
            {
                str2 = str2.Remove(str2.Length - 5, 5);
            }
        }
        return str2;
    }

    protected void btnMode_Click(object sender, EventArgs e)
    {
        string flag = "0";
        string DBname = string.Empty;
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        DBname = GetCurrentDBName(sDataSource); //ConfigurationSettings.AppSettings["DBName"].ToString();
        try
        {
            if (drpYear.SelectedItem.Text != "Current Data")
            {
                flag = "1";
                Session["CurrentYear"] = drpYear.SelectedItem.Text;
                string filename = string.Empty;
                filename = drpYear.SelectedItem.Value;
                File.Move(Server.MapPath("App_Data\\" + DBname + ".mdb"), Server.MapPath("App_Data\\" + DBname + "_curr.mdb"));
                File.Copy(Server.MapPath("OldYear\\" + filename), Server.MapPath("App_Data\\" + DBname + ".mdb"));
                //  File.Move(Server.MapPath("App_Data\\jandj_1.mdb"), Server.MapPath("App_Data\\jandj.mdb"));

            }
            else
            {

                if (File.Exists(Server.MapPath("App_Data\\" + DBname + "_curr.mdb")))
                {
                    Session["CurrentYear"] = "Current Year";
                    if (File.Exists(Server.MapPath("App_Data\\" + DBname + ".mdb")))
                        File.Delete(Server.MapPath("App_Data\\" + DBname + ".mdb"));
                    File.Move(Server.MapPath("App_Data\\" + DBname + "_curr.mdb"), Server.MapPath("App_Data\\" + DBname + ".mdb"));
                    File.Delete(Server.MapPath("App_Data\\" + DBname + "_curr.mdb"));
                }
            }
            lblMsg.Text = Session["CurrentYear"].ToString() + " Data is Set";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
