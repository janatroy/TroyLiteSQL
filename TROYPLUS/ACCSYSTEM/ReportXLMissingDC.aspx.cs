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


public partial class ReportXLMissingDC : System.Web.UI.Page
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

         int commno = 0;
         int sno = 0;

         DataSet ds = new DataSet();
         DataSet ddd = new DataSet();
         ds = objBL.getmissingdc(sDataSource, startDate,endDate);

         DataTable dt = new DataTable();
         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                 sno = 1;
                 dt.Columns.Add(new DataColumn("Bill No"));
                 dt.Columns.Add(new DataColumn("Bill Date"));
                 dt.Columns.Add(new DataColumn("Ledger Name"));
                 dt.Columns.Add(new DataColumn("Customer Address"));
                 dt.Columns.Add(new DataColumn("Customer Address2"));
                 dt.Columns.Add(new DataColumn("Customer Address3"));
                 dt.Columns.Add(new DataColumn("Customer Contacts"));
                 dt.Columns.Add(new DataColumn("Brand"));
                 dt.Columns.Add(new DataColumn("Item Code"));
                 dt.Columns.Add(new DataColumn("Product Name"));
                 dt.Columns.Add(new DataColumn("Model"));
                 dt.Columns.Add(new DataColumn("Qty"));
                 dt.Columns.Add(new DataColumn("Rate"));

                 DataRow dr_final113 = dt.NewRow();
                 dt.Rows.Add(dr_final113);

                 foreach (DataRow dr in ds.Tables[0].Rows)
                 {
                     DataRow dr_final6 = dt.NewRow();
                     dr_final6["Bill No"] = dr["BillNo"];

                     string aa = dr["BillDate"].ToString().ToUpper().Trim();
                     string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                     dr_final6["Bill Date"] = dtaa;

                     dr_final6["Ledger Name"] = dr["LedgerName"];
                     dr_final6["Customer Address"] = dr["CustomerAddress"];
                     dr_final6["Customer Address2"] = dr["CustomerAddress2"];
                     dr_final6["Customer Address3"] = dr["CustomerAddress3"];
                     dr_final6["Customer Contacts"] = dr["CustomerContacts"];

                     ddd = objBL.GetSalesItemsForId(Convert.ToInt32(dr["BillNo"]));

                     if(ddd != null)
                     {
                         if (ddd.Tables[0].Rows.Count > 0)
                         {
                             foreach (DataRow dd in ddd.Tables[0].Rows)
                             {
                                 DataSet dsddd = objBL.GetPurchaseForDcNo(Convert.ToInt32(dr["BillNo"]), Convert.ToString(dd["ItemCode"]));

                                 if (dsddd != null)
                                 {
                                     if (dsddd.Tables[0].Rows.Count > 0)
                                     {
                                         foreach (DataRow ddr in dsddd.Tables[0].Rows)
                                         {
                                             if (commno == Convert.ToInt32(dr["BillNo"]))
                                             {
                                                 break;
                                             }

                                             if (sno == 1)
                                             {
                                                 dr_final6["Brand"] = ddr["productdesc"].ToString();
                                                 dr_final6["Item Code"] = ddr["ItemCode"].ToString();
                                                 dr_final6["Product Name"] = ddr["ProductName"].ToString();
                                                 dr_final6["Model"] = ddr["Model"].ToString();
                                                 dr_final6["Qty"] = Convert.ToDouble(dd["Qty"]) - Convert.ToDouble(ddr["Qty"]);
                                                 dr_final6["Rate"] = "";
                                                 if (Convert.ToInt32(dr_final6["Qty"]) > 0)
                                                 {
                                                    dt.Rows.Add(dr_final6);
                                                 }
                                                 sno = sno + 1;

                                             }
                                             else
                                             {
                                                 DataRow dr_final122 = dt.NewRow();
                                                 dr_final122["Brand"] = ddr["productdesc"].ToString();
                                                 dr_final122["Item Code"] = ddr["ItemCode"].ToString();
                                                 dr_final122["Product Name"] = ddr["ProductName"].ToString();
                                                 dr_final122["Model"] = ddr["Model"].ToString();
                                                 dr_final122["Qty"] = Convert.ToDouble(dd["Qty"]) - Convert.ToDouble(ddr["Qty"]);
                                                 dr_final122["Rate"] = "";
                                                 if (Convert.ToInt32(dr_final122["Qty"]) > 0)
                                                 {
                                                     dt.Rows.Add(dr_final122);
                                                 }
                                                 sno = sno + 1;
                                             }
                                         }
                                     }
                                     else
                                     {
                                         if (commno == Convert.ToInt32(dr["BillNo"]))
                                         {
                                             break;
                                         }

                                         if (sno == 1)
                                         {
                                             dr_final6["Brand"] = dd["productdesc"].ToString();
                                             dr_final6["Item Code"] = dd["ItemCode"].ToString();
                                             dr_final6["Product Name"] = dd["ProductName"].ToString();
                                             dr_final6["Model"] = dd["Model"].ToString();
                                             dr_final6["Qty"] = Convert.ToDouble(dd["Qty"]);
                                             dr_final6["Rate"] = Convert.ToDouble(dd["Rate"]);
                                             dt.Rows.Add(dr_final6);
                                             sno = sno + 1;

                                         }
                                         else
                                         {
                                             DataRow dr_final122 = dt.NewRow();
                                             dr_final122["Brand"] = dd["productdesc"].ToString();
                                             dr_final122["Item Code"] = dd["ItemCode"].ToString();
                                             dr_final122["Product Name"] = dd["ProductName"].ToString();
                                             dr_final122["Model"] = dd["Model"].ToString();
                                             dr_final122["Qty"] = Convert.ToDouble(dd["Qty"]);
                                             dr_final122["Rate"] = Convert.ToDouble(dd["Rate"]);
                                             dt.Rows.Add(dr_final122);
                                             sno = sno + 1;
                                         }
                                     }
                                 }
                                 else
                                 {
                                     if (commno == Convert.ToInt32(dr["BillNo"]))
                                     {
                                         break;
                                     }

                                     if (sno == 1)
                                     {
                                         dr_final6["Brand"] = dd["productdesc"].ToString();
                                         dr_final6["Item Code"] = dd["ItemCode"].ToString();
                                         dr_final6["Product Name"] = dd["ProductName"].ToString();
                                         dr_final6["Model"] = dd["Model"].ToString();
                                         dr_final6["Qty"] = Convert.ToDouble(dd["Qty"]);
                                         dr_final6["Rate"] = Convert.ToDouble(dd["Rate"]);
                                         dt.Rows.Add(dr_final6);
                                         sno = sno + 1;

                                     }
                                     else
                                     {
                                         DataRow dr_final122 = dt.NewRow();
                                         dr_final122["Brand"] = dd["productdesc"].ToString();
                                         dr_final122["Item Code"] = dd["ItemCode"].ToString();
                                         dr_final122["Product Name"] = dd["ProductName"].ToString();
                                         dr_final122["Model"] = dd["Model"].ToString();
                                         dr_final122["Qty"] = Convert.ToDouble(dd["Qty"]);
                                         dr_final122["Rate"] = Convert.ToDouble(dd["Rate"]);
                                         dt.Rows.Add(dr_final122);
                                         sno = sno + 1;
                                     }
                                 }
                             }
                         }
                     }

                     sno = 1;
                     commno = Convert.ToInt32(dr["BillNo"]);

                     DataRow dr_final123 = dt.NewRow();
                     dt.Rows.Add(dr_final123);
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
             string filename = "Missing Dc.xls";
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