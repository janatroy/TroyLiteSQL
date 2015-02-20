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
using System.Text;
using System.IO;

public partial class CustomerReport : System.Web.UI.Page
{
    string reportPath = string.Empty;
    System.IO.StreamWriter logfile = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            srcArea.ConnectionString = connStr;

            if (!Page.IsPostBack)
            {
                txtBalance.Text = "0";
                CheckBox1.Checked = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void lnkBtnSearchId_Click(object sender, EventArgs e)
    {
        StringBuilder msg = new StringBuilder();

        try
        {
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg.ToString() + "')", true);
                return;
            }
            else
            {

                reportPath = @"c:\inetpub\vhosts\lathaconsultancy.com\httpdocs\Reports\" + ddArea.SelectedValue.Replace("'", "") + "_Report.txt";
                GenerateReport();
                errDisp.AddItem("Report Generated Successfully.", DisplayIcons.GreenTick, true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void WriteToLog(string message)
    {
        if (!File.Exists(reportPath))
            logfile = File.CreateText(reportPath);
        else
            logfile = File.AppendText(reportPath);

        logfile.WriteLine("{0}", message);

        logfile.Flush();
        logfile.Close();

    }

    private string FormatString(string inputString)
    {
        StringBuilder str = new StringBuilder();

        if (inputString.Length < 16)
        {
            str.Append(inputString);

            for (int i = 0; i < 16 - inputString.Length; i++)
            {
                str.Append(" ");
            }
        }

        return str.ToString();

    }

    private string FormatStringLarge(string inputString, int len)
    {
        StringBuilder str = new StringBuilder();

        if (inputString.Length < len)
        {
            str.Append(inputString);

            for (int i = 0; i < len - inputString.Length; i++)
            {
                str.Append(" ");
            }
        }
        else if (inputString.Length > len)
        {
            inputString = inputString.Remove(len);
            str.Append(inputString);

        }
        else
        {
            str.Append(inputString);
        }

        return str.ToString();

    }

    private void WriteHeader()
    {
        StringBuilder header = new StringBuilder();
        WriteToLog(header.ToString());

        header.AppendFormat("{0}", "Sl");
        header.Append("    ");
        header.AppendFormat("{0}", "Code");
        header.Append("      ");
        header.AppendFormat("{0}", "Name");
        header.Append("                   ");
        header.AppendFormat("{0}", "Door No");
        header.Append("         ");
        header.AppendFormat("{0}", "Address");
        header.Append("           ");
        header.AppendFormat("{0}", "Balance");

        WriteToLog(header.ToString());

        header = new StringBuilder();

        header.AppendFormat("{0}", "----");
        header.Append("  ");
        header.AppendFormat("{0}", "-------   ");
        header.AppendFormat("{0}", "--------------------   ");
        header.AppendFormat("{0}", "----------     ");
        header.Append("---------------   ");
        header.AppendFormat("{0}", "-------");

        WriteToLog(header.ToString());
    }

    private void GenerateReport()
    {

        string area = string.Empty;
        string orderBy = string.Empty;



        if (ddArea.SelectedValue != "0")
            area = ddArea.SelectedValue;

        orderBy = ddOrderBy.SelectedValue;

        string query = string.Empty;
        StringBuilder doorNoOrder = new StringBuilder();
        doorNoOrder.Append("val(left(doorno,instr(doorno+'-','-')-1)), val(left(right(doorno,len(doorno)-instr(doorno,'-')),instr(right(doorno,len(doorno)-instr(doorno,'-'))+'/','/')-1))");
        doorNoOrder.Append(", val(left(right(doorno,len(doorno)- instr(doorno,'-')-instr(right(doorno,len(doorno)-instr(doorno,'-')),'/')), instr(right(doorno,len(doorno)- instr(doorno,'-')-instr(right(doorno,len(doorno)-instr(doorno,'-')),'/'))+'/','/')-1)),doorno");

        if (!CheckBox1.Checked)
        {
            if (orderBy == "doorno")
                query = string.Format("Select code,name,doorno,address1,balance from CustomerMaster Where category = 'DC' and area = '{0}' and balance {1} {2} Order by {3}", area.Replace("'", "''"), ddOper.SelectedValue, txtBalance.Text, doorNoOrder.ToString());
            else
                query = string.Format("Select code,name,doorno,address1,balance from CustomerMaster Where category = 'DC' and area = '{0}' and balance {1} {2} Order by {3}", area.Replace("'", "''"), ddOper.SelectedValue, txtBalance.Text, orderBy);
        }
        else
        {
            if (orderBy == "doorno")
                query = string.Format("Select code,name,doorno,address1,balance from CustomerMaster Where category In ('NC','RC') and area = '{0}' and balance {1} {2} Order by {3}", area.Replace("'", "''"), ddOper.SelectedValue, txtBalance.Text, doorNoOrder.ToString());
            else
                query = string.Format("Select code,name,doorno,address1,balance from CustomerMaster Where category In ('NC','RC') and area = '{0}' and balance {1} {2} Order by {3}", area.Replace("'", "''"), ddOper.SelectedValue, txtBalance.Text, orderBy);

        }

        string connStr = string.Empty;
        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        OleDbConnection conn = new OleDbConnection(connStr);
        OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds);



        if (ds != null)
        {
            if (File.Exists(reportPath))
                File.Delete(reportPath);

            StringBuilder title = new StringBuilder();
            WriteToLog(title.ToString());

            title.Append("                                     ");
            title.Append("DUE LIST");
            WriteToLog(title.ToString());

            title = new StringBuilder();
            title.Append("                                     ");
            title.Append("--------");
            WriteToLog(title.ToString());

            title = new StringBuilder();
            title.AppendFormat("Area : {0}", area);
            WriteToLog(title.ToString());

            title = new StringBuilder();
            title.AppendFormat("Order By : {0}", orderBy);
            WriteToLog(title.ToString());

            title = new StringBuilder();
            WriteToLog(title.ToString());

            WriteHeader();
            int i = 0;
            int totalBalance = 0;
            int j = 1;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if ((i / 25) == j)
                {
                    j = j + 1;
                    WriteToLog("\f");
                    WriteHeader();
                }
                WriteToLog("");

                i = i + 1;
                StringBuilder data = new StringBuilder();
                data.AppendFormat("{0}", FormatStringLarge(i.ToString(), 6));
                data.AppendFormat("{0}", FormatStringLarge(dr["code"].ToString(), 10));
                data.AppendFormat("{0}", FormatStringLarge(dr["Name"].ToString(), 23));
                data.AppendFormat("{0}", FormatStringLarge(dr["Doorno"].ToString(), 18));
                data.AppendFormat("{0}", FormatStringLarge(dr["address1"].ToString(), 18));
                data.AppendFormat("{0}", FormatString(dr["Balance"].ToString()));
                totalBalance = totalBalance + int.Parse(dr["Balance"].ToString());
                WriteToLog(data.ToString());
            }

            WriteToLog("");
            WriteToLog("                                                                    	----------");
            WriteToLog("                                                                    	" + totalBalance.ToString());
            WriteToLog("                                                                    	----------");
            WriteToLog("");
            StringBuilder footer = new StringBuilder();
            footer.Append("                                ");
            footer.Append("END OF THE REPORT");
            WriteToLog(footer.ToString());

            try
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", "attachment;filename=Customer_Duelist_Report.txt");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.ContentType = "application/vnd.text";
                Response.TransmitFile(reportPath);
                Response.Flush();
                Response.Buffer = false;
                Response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



    }

}
