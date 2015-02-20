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
using System.Data.OleDb;
using System.IO;

public partial class CashDetailsReport : System.Web.UI.Page
{
    string reportPath = string.Empty;
    System.IO.StreamWriter logfile = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string connStr = string.Empty;
            if (!Page.IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/frm_Login.aspx");

                srcArea.ConnectionString = connStr;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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

    private void GenerateReport()
    {

        string area = string.Empty;
        string startDate = string.Empty;
        string endDate = string.Empty;
        string orderBy = string.Empty;

        if (ddArea.SelectedValue != "0")
            area = ddArea.SelectedValue;

        if (txtStartDate.Text != "")
            startDate = txtStartDate.Text;

        if (txtEndDate.Text != "")
            endDate = txtEndDate.Text;

        //orderBy = ddOrderBy.SelectedValue;

        StringBuilder query = new StringBuilder();

        if (area != "")
            query.AppendFormat("Select ca.billno, ca.code,ca.area,ca.amount,ca.discount,ca.reason,Format(ca.date_paid, 'dd/mm/yyyy') as date_paid,Format(ca.date_entered, 'dd/mm/yyyy')as date_entered from CashDetails ca Where ca.area = '{0}' and ca.date_entered >= Format(#{1}#, 'dd/mm/yyyy') and ca.date_entered <= Format(#{2}#, 'dd/mm/yyyy') Order by {3}", area.Replace("'", "''"), startDate, endDate, "ca.area , ca.billno ,ca.date_paid ");
        else
            query.AppendFormat("Select ca.billno, ca.code,ca.area,ca.amount,ca.discount,ca.reason,Format(ca.date_paid, 'dd/mm/yyyy') as date_paid,Format(ca.date_entered, 'dd/mm/yyyy')as date_entered from CashDetails ca Where ca.date_entered >= Format(#{0}#, 'dd/mm/yyyy') and ca.date_entered <= Format(#{1}#, 'dd/mm/yyyy') Order by {2}", startDate, endDate, "ca.area , ca.billno ,ca.date_paid ");

        string connStr = string.Empty;
        if (Request.Cookies["Company"]  != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("frm_login.aspx");

        OleDbConnection conn = new OleDbConnection(connStr);
        OleDbDataAdapter da = new OleDbDataAdapter(query.ToString(), conn);
        DataSet ds = new DataSet();
        da.Fill(ds);



        if (ds != null)
        {
            if (File.Exists(reportPath))
                File.Delete(reportPath);

            StringBuilder title = new StringBuilder();
            WriteToLog(title.ToString());

            title.Append("                                 ");
            title.Append("CASH ENTRIES");
            WriteToLog(title.ToString());

            title = new StringBuilder();
            title.Append("                                 ");
            title.Append("-------------");
            WriteToLog(title.ToString());

            title = new StringBuilder();
            title.AppendFormat("Area : {0}", area);
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
                data.AppendFormat("{0}", FormatStringLarge(dr["code"].ToString(), 8));
                data.AppendFormat("{0}", FormatStringLarge(dr["billno"].ToString(), 10));
                data.AppendFormat("{0}", FormatStringLarge(dr["area"].ToString(), 15));
                data.AppendFormat("{0}", FormatStringLarge(dr["discount"].ToString(), 12));
                data.AppendFormat("{0}", FormatStringLarge(dr["reason"].ToString(), 12));
                data.AppendFormat("{0}", FormatStringLarge(dr["date_paid"].ToString().Remove(10, dr["date_paid"].ToString().Length - 10), 13));
                data.AppendFormat("{0}", FormatStringLarge(dr["amount"].ToString(), 8));
                totalBalance = totalBalance + int.Parse(dr["amount"].ToString());
                WriteToLog(data.ToString());
            }

            WriteToLog("");
            WriteToLog("                                                                    	   ----------");
            WriteToLog("                                                                    	    " + totalBalance.ToString());
            WriteToLog("                                                                    	   ----------");
            WriteToLog("");
            StringBuilder footer = new StringBuilder();
            footer.Append("                                ");
            footer.Append("END OF THE REPORT");
            WriteToLog(footer.ToString());

            try
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", "attachment;filename=CashEntries_Report.txt");
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


    private void WriteHeader()
    {
        StringBuilder header = new StringBuilder();
        WriteToLog(header.ToString());

        header.AppendFormat("{0}", "Sl");
        header.Append("    ");
        header.AppendFormat("{0}", "Code");
        header.Append("    ");
        header.AppendFormat("{0}", "billno");
        header.Append("    ");
        header.AppendFormat("{0}", "Area");
        header.Append("           ");
        header.AppendFormat("{0}", "Discount");
        header.Append("    ");
        header.AppendFormat("{0}", "Reason");
        header.Append("      ");
        header.AppendFormat("{0}", "DatePaid     ");
        header.AppendFormat("{0}", "Amount");
        WriteToLog(header.ToString());

        header = new StringBuilder();

        header.AppendFormat("{0}", "----  ");
        header.AppendFormat("{0}", "------- ");
        header.AppendFormat("{0}", "--------  ");
        header.AppendFormat("{0}", "-------------- ");
        header.AppendFormat("{0}", "----------- ");
        header.AppendFormat("{0}", "----------- ");
        header.AppendFormat("{0}", "------------ ");
        header.Append("-------");

        WriteToLog(header.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddArea.SelectedValue != "0")
                reportPath = @"c:\inetpub\vhosts\lathaconsultancy.com\httpdocs\Reports\" + ddArea.SelectedValue.Replace("'", "") + "_CashEntry_Report.txt";
            else
                reportPath = @"c:\inetpub\vhosts\lathaconsultancy.com\httpdocs\Reports\AllCashEntry_CashEntry_Report.txt";

            GenerateReport();
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
}
