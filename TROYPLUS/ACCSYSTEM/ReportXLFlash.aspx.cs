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
using System.Data.OleDb;

public partial class ReportXLFlash : System.Web.UI.Page
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
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        //string connString = "";
        //OleDbTransaction transaction = null;
        //string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
        //string path = fileuploadExcel.PostedFile.FileName;
        ////Connection String to Excel Workbook

        //if (strFileType.Trim() == ".xls")
        //{
        //    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        //}
        //else if (strFileType.Trim() == ".xlsx")
        //{
        //    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //}
        //string query = "SELECT [IP] FROM [Sheet1$]";
        //OleDbConnection conn = new OleDbConnection(connString);
        ////transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
        ////if (conn.State == ConnectionState.Closed)
        ////    conn.Open();
        //OleDbCommand cmd = new OleDbCommand(query, conn);
        //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //cmd.Transaction = transaction;
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //grvExcelData.DataSource = ds.Tables[0];
        //grvExcelData.DataBind();
        //da.Dispose();
        //conn.Close();
        //conn.Dispose();
    }

   protected void btnxls_Click(object sender, EventArgs e)
   {
       BusinessLogic bl = new BusinessLogic(sDataSource);
       string Access = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
       string Excel = Server.MapPath("E:/Book1.xlsx");
       string connect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Excel + ";Extended Properties=Excel 8.0;";
       using (OleDbConnection connection = new OleDbConnection(bl.CreateConnectionString(sDataSource)))
       {
           using (OleDbCommand cmd = new OleDbCommand())
           {
               cmd.Connection = connection;
               cmd.CommandText = "INSERT INTO [MS Access;Database=" + Access + "].[tblIpAddress] SELECT * FROM [Sheet1$]";
               connection.Open();
               cmd.ExecuteNonQuery();
           }
       }

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

   protected void GenerateCashPurchase(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rptCash.generatePurchaseReport(1, sDataSource, startDate, endDate, sType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "CashPurchase", 1);
   }
   protected void GenerateChequePurchase(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rptCash.generatePurchaseReport(2, sDataSource, startDate, endDate, sType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "ChequePurchase", 2);

   }
   protected void GenarateCreditPurchase(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rptCash.generatePurchaseReport(3, sDataSource, startDate, endDate, sType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "CreditPurchase", 3);
   }
   protected void GenerateCashSales(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rptCash.generateSalesReport(1, sDataSource, startDate, endDate, sType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "CashSales", 1);
   }
   protected void GenerateChequeSales(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rptCash.generateSalesReport(2, sDataSource, startDate, endDate, sType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "ChequeSales", 2);
   }
   protected void GenarateCreditSales(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rptCash.generateSalesReport(3, sDataSource, startDate, endDate, sType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "CreditSales", 3);
   }

   protected void GenerateCashPaid(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rptCash.generateCashPaid(sDataSource, startDate, endDate, sType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "CashPaid", 0);
   }
   protected void GenerateChequePaid(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rpt.generateChequePaid(sDataSource, startDate, endDate, sType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "ChequePaid", 0);
   }
   protected void GenerateCashReceived(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rpt.generateCashReceived(sDataSource, startDate, endDate, pType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "CashReceived", 0);
   }
   protected void GenerateChequeReceived(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rpt.generateChequeReceived(sDataSource, startDate, endDate, pType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "ChequeReceived", 0);
   }

   protected void GenerateJournal(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rpt.generateChequeReceived(sDataSource, startDate, endDate, pType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "Journal", 0);
   }

   protected void GenerateCreditDebitNote(string sDataSource, DateTime startDate, DateTime endDate)
   {
       //DataSet ds = rpt.generateChequeReceived(sDataSource, startDate, endDate, pType);

       DataSet ds = objBL.getflashreport(sDataSource, startDate, endDate, "CreditDebitNote", 0);
   }

   protected void btngriddata_Click(object sender, EventArgs e)
   {
       try
       {
           DateTime startDate, endDate;
           startDate = Convert.ToDateTime(txtSrtDate.Text);
           endDate = Convert.ToDateTime(txtEdDate.Text);
           
           string Types = string.Empty;
           int paymode = 0;
           DataSet ds = new DataSet();

           GenerateCashPurchase(sDataSource, startDate, endDate);
           GenerateChequePurchase(sDataSource, startDate, endDate);
           GenarateCreditPurchase(sDataSource, startDate, endDate);
           GenerateCashSales(sDataSource, startDate, endDate);
           GenerateChequeSales(sDataSource, startDate, endDate);
           GenarateCreditSales(sDataSource, startDate, endDate);

           GenerateCashPaid(sDataSource, startDate, endDate);
           GenerateChequePaid(sDataSource, startDate, endDate);

           GenerateCashReceived(sDataSource, startDate, endDate);
           GenerateChequeReceived(sDataSource, startDate, endDate);

           //GenerateItemwiseSales(sDataSource, startDate, endDate, purchasereturn);
           //GenerateItemwisePurchase(sDataSource, startDate, endDate, salesreturn);

           //GenerateGrossProfit(sDataSource, startDate, endDate, salesreturn, purchasereturn);
           //GenerateVat(sDataSource, startDate, endDate, salesreturn);




       }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }
   }

     public void bindData(string sDataSource)
     {
         DateTime startDate, endDate;
         startDate = Convert.ToDateTime(txtSrtDate.Text);
         endDate = Convert.ToDateTime(txtEdDate.Text);

         DataSet ds = new DataSet();
         ds = objBL.getFlashstatement(sDataSource, startDate,endDate);

         DataTable dt = new DataTable();

         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                 dt.Columns.Add(new DataColumn(" "));
                 dt.Columns.Add(new DataColumn("Voucher Nos"));
                 dt.Columns.Add(new DataColumn("Total"));
                 dt.Columns.Add(new DataColumn("Cash"));
                 dt.Columns.Add(new DataColumn("Credit"));
                 dt.Columns.Add(new DataColumn("Bank"));
                 dt.Columns.Add(new DataColumn("Finance"));
                 dt.Columns.Add(new DataColumn("Exchange"));
                 dt.Columns.Add(new DataColumn("Card"));

                 DataRow dr_final678 = dt.NewRow();
                 dr_final678[" "] = "";
                 dr_final678["Voucher Nos"] = "";
                 dr_final678["Total"] = "";
                 dr_final678["Cash"] = "";
                 dr_final678["Credit"] = "";
                 dr_final678["Bank"] = "";
                 dr_final678["Finance"] = "";
                 dr_final678["Exchange"] = "";
                 dr_final678["Card"] = "";
                 dt.Rows.Add(dr_final678);

                 foreach (DataRow dr in ds.Tables[0].Rows)
                 {
                     DataRow dr_final6 = dt.NewRow();
                     dr_final6[" "] = "Sales";
                     dr_final6["Voucher Nos"] = "";
                     dr_final6["Total"] = dr["Total"];
                     dr_final6["Cash"] = dr["Cash"];
                     dr_final6["Credit"] = dr["Credit"];
                     dr_final6["Bank"] = dr["Bank"];
                     dr_final6["Finance"] = "";
                     dr_final6["Exchange"] = "";
                     dr_final6["Card"] = "";                    
                     dt.Rows.Add(dr_final6);
                 }
                 ExportToExcel(dt);
             }
             else
             {
                 ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
             }
         }
         else
         {
             ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
         }
     }

     protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
     {

     }

     public void ExportToExcel(DataTable dt)
     {

         if (dt.Rows.Count > 0)
         {
             string filename = "Flash Statement.xls";
             System.IO.StringWriter tw = new System.IO.StringWriter();
             System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
             DataGrid dgGrid = new DataGrid();
             dgGrid.DataSource = dt;
             dgGrid.DataBind();

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