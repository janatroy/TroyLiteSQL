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
using System.Text;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Management;

public partial class ReportXLAnalysis : System.Web.UI.Page
{
    int catid = 0;
    string brand = "";
    string model = "";
    string product = "";
    decimal Gtotal = 0;
    decimal Gttl = 0;
    decimal Pttls = 0;
    decimal Ittls = 0;
    decimal brandTotal = 0, catIDTotal = 0, modelTotal = 0;
    string grpBy = "", selCols = "", selLevels = "", field1 = "", field2 = "", ordrby = "", cond = "", Subordrby="";

    //DBClass objBL = new DBClass();
    BusinessLogic objBL = new BusinessLogic();
    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!Page.IsPostBack)
            {
                DateTime dtCurrent = DateTime.Now;
                DateTime dtNew = new DateTime(dtCurrent.Year, dtCurrent.Month, 1);
                txtSrtDate.Text = string.Format("{0:dd/MM/yyyy}", dtNew);

                //txtSrtDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEdDate.Text = DateTime.Now.ToShortDateString();




                //ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                //ManagementObjectCollection moc = mc.GetInstances();
                //string MACAddress = "";

                //foreach (ManagementObject mo in moc)
                //{
                //    if (mo["MacAddress"] != null)
                //    {
                //        MACAddress = mo["MacAddress"].ToString();
                //    }
                //}

                //lblIP.Text = MACAddress;




            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

   protected void btnxls_Click(object sender, EventArgs e)
   {
        try
        {
           bindData(sDataSource);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }
   }

   //protected void btngriddata_Click(object sender, EventArgs e)
   //{
   //    try
   //    {
   //        DateTime startDate, endDate;
   //        startDate = Convert.ToDateTime(txtSrtDate.Text);
   //        endDate = Convert.ToDateTime(txtEdDate.Text);
   //        string itemcode;
   //        itemcode = "";

   //        DataSet ds = new DataSet();
   //        ds = objBL.getstockstatement(sDataSource, startDate, endDate, itemcode);

   //        gvLedger.DataSource = ds;
   //        gvLedger.DataBind();
   //    }
   //    catch (Exception ex)
   //    {
   //        var error = ex.Message.Replace("'", "");
   //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Error Occured : " + @error + "');", true);
   //        return;
   //    }
   //}

     public void bindData(string sDataSource)
     {
         DateTime startDate, endDate, NewDate;
         startDate = Convert.ToDateTime(txtSrtDate.Text);
         endDate = Convert.ToDateTime(txtEdDate.Text);
         double purqtytot = 0;
         double purvaltot = 0;
         double salqtytot = 0;
         double salvaltot = 0;
         double salretqtytot = 0;
         double salretvaltot = 0;
         double purretqtytot = 0;
         double purretvaltot = 0;

         DataSet ds = new DataSet();
         string types = string.Empty;

         DataTable dt = new DataTable();
         
         dt.Columns.Add(new DataColumn("Date"));
         dt.Columns.Add(new DataColumn("Product Name"));
         dt.Columns.Add(new DataColumn("Op Qty"));
         dt.Columns.Add(new DataColumn("Op Value"));
         dt.Columns.Add(new DataColumn("Pur Qty"));
         dt.Columns.Add(new DataColumn("Pur Value"));
         dt.Columns.Add(new DataColumn("Sal Qty"));
         dt.Columns.Add(new DataColumn("Sal Value"));
         dt.Columns.Add(new DataColumn("Pur.Ret Qty"));
         dt.Columns.Add(new DataColumn("Pur.Ret Value"));
         dt.Columns.Add(new DataColumn("Sal.Ret Qty"));
         dt.Columns.Add(new DataColumn("Sal.Ret Value"));
         dt.Columns.Add(new DataColumn("Clo Qty"));
         dt.Columns.Add(new DataColumn("Clo Value"));

         TimeSpan ts = Convert.ToDateTime(endDate) - Convert.ToDateTime(startDate);
         int days = Convert.ToInt32(ts.TotalDays);

         NewDate = startDate;

         for (int i = 0; i < days+1; i++)
         {
             string aa = NewDate.ToString().ToUpper().Trim();
             string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
             
             types = "Purchase";

             DataRow dr_export1 = dt.NewRow();
             dt.Rows.Add(dr_export1);

             ds = objBL.getdetails(sDataSource, NewDate, types);
             if (ds != null)
             {
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         DataRow dr_export = dt.NewRow();
                         dr_export["Date"] = dtaa;
                         dr_export["Product Name"] = dr["productname"];
                         dr_export["Op Qty"] = "";
                         dr_export["Op Value"] = "";
                         dr_export["Pur Qty"] = dr["qty"];
                         purqtytot = purqtytot + Convert.ToDouble(dr["qty"]);
                         dr_export["Pur Value"] = dr["amount"];
                         purvaltot = purvaltot + Convert.ToDouble(dr["amount"]);
                         dr_export["Sal Qty"] = "";
                         dr_export["Sal Value"] = "";
                         dr_export["Pur.Ret Qty"] = "";
                         dr_export["Pur.Ret Value"] = "";
                         dr_export["Sal.Ret Qty"] = "";
                         dr_export["Sal.Ret Value"] = "";
                         dr_export["Clo Qty"] = "";
                         dr_export["Clo Value"] = "";
                         dt.Rows.Add(dr_export);
                     }
                 }
             }


             types = "Sales";
            
             ds = objBL.getdetails(sDataSource, NewDate, types);
             if (ds != null)
             {
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         DataRow dr_export = dt.NewRow();
                         dr_export["Date"] = dtaa;
                         dr_export["Product Name"] = dr["productname"];
                         dr_export["Op Qty"] = "";
                         dr_export["Op Value"] = "";
                         dr_export["Pur Qty"] = "";
                         dr_export["Pur Value"] = "";
                         dr_export["Sal Qty"] = dr["qty"];
                         salqtytot = salqtytot + Convert.ToDouble(dr["qty"]);
                         dr_export["Sal Value"] = dr["amount"];
                         salvaltot = salvaltot + Convert.ToDouble(dr["amount"]);
                         dr_export["Pur.Ret Qty"] = "";
                         dr_export["Pur.Ret Value"] = "";
                         dr_export["Sal.Ret Qty"] = "";
                         dr_export["Sal.Ret Value"] = "";
                         dr_export["Clo Qty"] = "";
                         dr_export["Clo Value"] = "";
                         dt.Rows.Add(dr_export);
                     }
                 }
             }

             types = "PurRet";

             ds = objBL.getdetails(sDataSource, NewDate, types);
             if (ds != null)
             {
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         DataRow dr_export = dt.NewRow();
                         dr_export["Date"] = dtaa;
                         dr_export["Product Name"] = dr["productname"];
                         dr_export["Op Qty"] = "";
                         dr_export["Op Value"] = "";
                         dr_export["Pur Qty"] = "";
                         dr_export["Pur Value"] = "";
                         dr_export["Sal Qty"] = "";                        
                         dr_export["Sal Value"] = "";                         
                         dr_export["Pur.Ret Qty"] = dr["qty"];
                         purretqtytot = purretqtytot + Convert.ToDouble(dr["qty"]);
                         dr_export["Pur.Ret Value"] = dr["amount"];
                         purretvaltot = purretvaltot + Convert.ToDouble(dr["amount"]);
                         dr_export["Sal.Ret Qty"] = "";
                         dr_export["Sal.Ret Value"] = "";
                         dr_export["Clo Qty"] = "";
                         dr_export["Clo Value"] = "";
                         dt.Rows.Add(dr_export);
                     }
                 }
             }

             types = "SalRet";

             ds = objBL.getdetails(sDataSource, NewDate, types);
             if (ds != null)
             {
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         DataRow dr_export = dt.NewRow();
                         dr_export["Date"] = dtaa;
                         dr_export["Product Name"] = dr["productname"];
                         dr_export["Op Qty"] = "";
                         dr_export["Op Value"] = "";
                         dr_export["Pur Qty"] = "";
                         dr_export["Pur Value"] = "";
                         dr_export["Sal Qty"] = "";
                         dr_export["Sal Value"] = "";
                         dr_export["Pur.Ret Qty"] = "";                         
                         dr_export["Pur.Ret Value"] = "";                         
                         dr_export["Sal.Ret Qty"] = dr["qty"];
                         salretqtytot = salretqtytot + Convert.ToDouble(dr["qty"]);
                         dr_export["Sal.Ret Value"] =dr["amount"];
                         salretvaltot = salretvaltot + Convert.ToDouble(dr["amount"]);
                         dr_export["Clo Qty"] = "";
                         dr_export["Clo Value"] = "";
                         dt.Rows.Add(dr_export);
                     }
                 }
             }

             //DataRow dr_export2 = dt.NewRow();
             //dt.Rows.Add(dr_export2);

             DataRow dr_export3 = dt.NewRow();
             dr_export3["Date"] = dtaa + " Total";
             dr_export3["Product Name"] = "";
             dr_export3["Op Qty"] = "";
             dr_export3["Op Value"] = "";
             dr_export3["Pur Qty"] = purqtytot;
             dr_export3["Pur Value"] = purvaltot;
             dr_export3["Sal Qty"] = salqtytot;
             dr_export3["Sal Value"] = salvaltot;
             dr_export3["Pur.Ret Qty"] = purretqtytot;
             dr_export3["Pur.Ret Value"] = purretvaltot;
             dr_export3["Sal.Ret Qty"] = salretqtytot;
             dr_export3["Sal.Ret Value"] = salretvaltot;
             dr_export3["Clo Qty"] = "";
             dr_export3["Clo Value"] = "";
             dt.Rows.Add(dr_export3);

             NewDate = NewDate.AddDays(1);
             purqtytot = 0;
             purvaltot = 0;
             salqtytot = 0;
             salvaltot = 0;
             purretqtytot = 0;
             purretvaltot = 0;
             salretqtytot = 0;
             salretvaltot = 0;
         }

         ExportToExcel(dt);
     }

     protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
     {

     }

     public void ExportToExcel(DataTable dt)
     {

         if (dt.Rows.Count > 0)
         {
             string filename = "Analysis Details.xls";
             System.IO.StringWriter tw = new System.IO.StringWriter();
             System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
             DataGrid dgGrid = new DataGrid();
             dgGrid.DataSource = dt;
             dgGrid.DataBind();
             dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
             dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
             dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
             dgGrid.HeaderStyle.Font.Bold = true;
             //Get the HTML for the control.
             dgGrid.RenderControl(hw);
             //Write the HTML back to the browser.
             Response.ContentType = "application/vnd.ms-excel";
             Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
             this.EnableViewState = false;
             Response.Write(tw.ToString());
             Response.End();
         }
     }
    
}