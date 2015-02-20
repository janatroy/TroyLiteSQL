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


public partial class ReportXLAbsolute : System.Web.UI.Page
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

     public void bindData(string sDataSource)
     {
         string Method = string.Empty;
         DataSet ds = new DataSet();
         string field = string.Empty;

         if (chkoption.SelectedValue == "All ItemList")
         {
             Method = "All";
         }
         else if (chkoption.SelectedValue == "Obsolete ItemList")
         {
             Method = "Absolute";
         }
         else if (chkoption.SelectedValue == "Other Than Obsolete")
         {
             Method = "NotAbsolute";
         }

         if (chkboxMRP.Checked == true)
         {
             field += ",rate";
         }
         if (chkboxDP.Checked == true)
         {
             field += ",DealerRate";
         }
         if (chkboxNLC.Checked == true)
         {
             field += ",NLC";
         }

         ds = objBL.GetAbsoluteProductlist(sDataSource, field, Method);

         DataTable dt = new DataTable();

         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                  dt.Columns.Add(new DataColumn("Brand"));
                  dt.Columns.Add(new DataColumn("ProductName"));
                  dt.Columns.Add(new DataColumn("ItemCode"));
                  dt.Columns.Add(new DataColumn("Model"));
                  dt.Columns.Add(new DataColumn("Rol (Stock Level)"));
                  if (chkboxMRP.Checked == true)
                  {
                      dt.Columns.Add(new DataColumn("MRP"));
                      dt.Columns.Add(new DataColumn("MRPEffDate"));
                  }
                  if (chkboxDP.Checked == true)
                  {
                      dt.Columns.Add(new DataColumn("DP"));
                      dt.Columns.Add(new DataColumn("DPEffDate"));
                  }
                  if (chkboxNLC.Checked == true)
                  {
                      dt.Columns.Add(new DataColumn("NLC"));
                      dt.Columns.Add(new DataColumn("NLCEffDate"));
                  }

                  DataRow dr_final123 = dt.NewRow();
                  dt.Rows.Add(dr_final123);

                  foreach (DataRow dr in ds.Tables[0].Rows)
                  {
                      DataRow dr_final6 = dt.NewRow();
                      dr_final6["Brand"] = dr["brand"];
                      dr_final6["ProductName"] = dr["ProductName"];
                      dr_final6["Model"] = dr["Model"];
                      dr_final6["ItemCode"] = dr["Itemcode"];

                      dr_final6["Rol (Stock Level)"] = dr["Rol"];
                  
                      if (chkboxMRP.Checked == true)
                      {
                          dr_final6["MRP"] = dr["Rate"];
                          dr_final6["MRPEffDate"] = dr["MRPEffDate"];
                      }
                      if (chkboxDP.Checked == true)
                      {
                          dr_final6["DP"] = dr["DealerRate"];
                          dr_final6["DPEffDate"] = dr["DPEffDate"];
                      }
                      if (chkboxNLC.Checked == true)
                      {
                          dr_final6["NLC"] = dr["NLC"];
                          dr_final6["NLCEffDate"] = dr["NLCEffDate"];
                      }
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
             string filename = "Obsolete Models.xls";
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