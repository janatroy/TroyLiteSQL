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


public partial class ReportXLCreditBill : System.Web.UI.Page
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

   protected void btngriddata_Click(object sender, EventArgs e)
   {
       //try
       //{
       //    DateTime startDate, endDate;
       //    startDate = Convert.ToDateTime(txtSrtDate.Text);
       //    endDate = Convert.ToDateTime(txtEdDate.Text);
       //    string itemcode;
       //    itemcode = "";

       //    DataSet ds = new DataSet();
       //    ds = objBL.getstockstatement(sDataSource, startDate, endDate, itemcode);

       //    gvLedger.DataSource = ds;
       //    gvLedger.DataBind();
       //}
       //catch (Exception ex)
       //{
       //    var error = ex.Message.Replace("'", "");
       //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Error Occured : " + @error + "');", true);
       //    return;
       //}
   }

     public void bindData(string sDataSource)
     {
         DateTime startDate, endDate;
         startDate = Convert.ToDateTime(txtSrtDate.Text);
         endDate = Convert.ToDateTime(txtEdDate.Text);

         string connStr = string.Empty;

         if (Request.Cookies["Company"] != null)
             connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
         else
             Response.Redirect("~/Login.aspx");

         DataSet ds = new DataSet();
         DataSet receivedData = new DataSet();
         ds = objBL.getCreditSales(startDate, endDate);
         DataTable dt = new DataTable();

         if (ds.Tables[0].Rows.Count > 0)
         {
              dt.Columns.Add(new DataColumn("BillNo"));
              dt.Columns.Add(new DataColumn("BillDate"));
              dt.Columns.Add(new DataColumn("Customer Name"));
              dt.Columns.Add(new DataColumn("Total"));
              dt.Columns.Add(new DataColumn("Cash"));
              dt.Columns.Add(new DataColumn("Bank"));
              dt.Columns.Add(new DataColumn("Card"));
              dt.Columns.Add(new DataColumn("Finance"));
              dt.Columns.Add(new DataColumn("Exchange"));
              dt.Columns.Add(new DataColumn("Balance"));
              double cash = 0;
              double Bank = 0;
              DataRow dr_final6789 = dt.NewRow();
              dt.Rows.Add(dr_final6789);

              foreach (DataRow dr in ds.Tables[0].Rows)
              {
                  DataRow dr_final6 = dt.NewRow();
                  dr_final6["BillNo"] = dr["BillNo"];
                  int billNo = Convert.ToInt32(dr["BillNo"]);
                  dr_final6["BillDate"] = dr["BillDate"];
                  dr_final6["Customer Name"] = dr["CustomerName"];
                  dr_final6["Total"] = dr["Amount"];

                  receivedData = objBL.GetCustomerReceivedAmount(connStr, billNo, "Cash");
                  if (receivedData != null)
                  {
                      for (int i = 0; i < receivedData.Tables[0].Rows.Count; i++)
                      {
                          dr_final6["Cash"] = receivedData.Tables[0].Rows[i]["TotalAmount"].ToString();
                          cash = Convert.ToDouble(receivedData.Tables[0].Rows[i]["TotalAmount"].ToString());
                      }
                  }
                  receivedData = objBL.GetCustomerReceivedAmount(connStr, billNo, "Cheque");
                  if (receivedData != null)
                  {
                      for (int i = 0; i < receivedData.Tables[0].Rows.Count; i++)
                      {
                          dr_final6["Bank"] = receivedData.Tables[0].Rows[i]["TotalAmount"].ToString();
                          Bank = Convert.ToDouble(receivedData.Tables[0].Rows[i]["TotalAmount"].ToString());
                      }
                  }
                  dr_final6["Card"] = "";
                  dr_final6["Finance"] = "";
                  dr_final6["Exchange"] = "";
                  dr_final6["Balance"] = Convert.ToDouble(dr["Amount"]) - (cash + Bank);
                  dt.Rows.Add(dr_final6);
              }
              ExportToExcel(dt);
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
             string filename = "Stock Statement.xls";
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