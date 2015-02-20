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


public partial class ReportXLZeroSales : System.Web.UI.Page
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
                txtvalue.Text = "0";

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
       try
       {
           DateTime startDate, endDate;
           startDate = Convert.ToDateTime(txtSrtDate.Text);
           endDate = Convert.ToDateTime(txtEdDate.Text);
           string itemcode;
           itemcode = "";

           DataSet ds = new DataSet();
           ds = objBL.getstockstatement(sDataSource, startDate, endDate, itemcode);

           gvLedger.DataSource = ds;
           gvLedger.DataBind();
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

         string Method = string.Empty;
         double val = 0;
         DataSet ds = new DataSet();

         if (chkoption.SelectedValue == "Only Value Details")
         {
             Method = "Only";
         }
         else if (chkoption.SelectedValue == "Whole Bill Details")
         {
             Method = "Whole";
         }

         val = Convert.ToDouble(txtvalue.Text);
         ds = objBL.getzerosales(sDataSource, startDate, endDate, Method, val);

         DataTable dt = new DataTable();

         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                 dt.Columns.Add(new DataColumn("Brand"));
                 dt.Columns.Add(new DataColumn("ProductName"));
                 dt.Columns.Add(new DataColumn("Model"));
                 dt.Columns.Add(new DataColumn("ItemCode"));
                 dt.Columns.Add(new DataColumn("BillNo"));
                 dt.Columns.Add(new DataColumn("BillDate"));
                 dt.Columns.Add(new DataColumn("LedgerName"));
                 dt.Columns.Add(new DataColumn("LedgerDetails"));
                 dt.Columns.Add(new DataColumn("Internal Transfer"));
                 dt.Columns.Add(new DataColumn("Delivery Note"));
                 dt.Columns.Add(new DataColumn("Purchase Return"));
                 dt.Columns.Add(new DataColumn("Qty"));

                 foreach (DataRow dr in ds.Tables[0].Rows)
                 {
                     DataRow dr_final6 = dt.NewRow();
                     dr_final6["Brand"] = dr["brand"];
                     dr_final6["ProductName"] = dr["ProductName"];
                     dr_final6["Model"] = dr["Model"];
                     dr_final6["ItemCode"] = dr["Itemcode"];
                     dr_final6["BillNo"] = dr["BillNo"];
                     dr_final6["BillDate"] = dr["BillDate"];
                     dr_final6["LedgerName"] = dr["LedgerName"];
                     dr_final6["LedgerDetails"] = dr["customeraddress"];
                     dr_final6["Internal Transfer"] = dr["InternalTransfer"];
                     dr_final6["Delivery Note"] = dr["DeliveryNote"];
                     dr_final6["Purchase Return"] = dr["PurchaseReturn"];
                     dr_final6["Qty"] = dr["Qty"];


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
             string filename = "Sales.xls";
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