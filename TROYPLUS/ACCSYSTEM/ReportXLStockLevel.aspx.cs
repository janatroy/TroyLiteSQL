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
using System.Net.NetworkInformation;
using System.Management;

public partial class ReportXLStockLevel : System.Web.UI.Page
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
                //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                //Label2.Text = nics[0].GetPhysicalAddress().ToString();

                //string id=" ";
                //ManagementObjectSearcher query = null;
                //ManagementObjectCollection queryCollection = null;

                //query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
                //queryCollection = query.Get();
                //foreach (ManagementObject mo in queryCollection)
                //{
                //  if (mo["MacAddress"] != null)
                //  {
                //     id = mo["MacAddress"].ToString();
                //     Label2.Text = id;
                //  }
                //}



                //string M = string.Empty;
                //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

                //for (int j = 0; j <= 1; j++)
                //{

                //    PhysicalAddress address = Request.UserHostAddress;

                //    byte[] bytes = address.GetAddressBytes();

                //    for (int i = 0; i < bytes.Length; i++)
                //    {

                //        M = M + bytes.ToString("X2");

                //        if (i != bytes.Length - 1)
                //        {

                //            M = M + ("-");

                //        }

                //    }

                //}





                DateTime dtCurrent = DateTime.Now;
                DateTime dtNew = new DateTime(dtCurrent.Year, dtCurrent.Month, 1);

                txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //public static string GetMacAddress()
    //{

    //    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

    //    ManagementObjectCollection moc = mc.GetInstances();

    //    string MACAddress = "";

    //    foreach (ManagementObject mo in moc)
    //    {

    //        if (mo["MacAddress"] != null)
    //        {

    //            MACAddress = mo["MacAddress"].ToString();

    //        }

    //    }

    //    return MACAddress;

    //}

   protected void btnxls_Click(object sender, EventArgs e)
   {
        try
        {
            int ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

            if (ttoption == 1)
            {
                bindDataCategory(sDataSource);
            }
            else if (ttoption == 2)
            {
                bindDatabrand(sDataSource);
            }
            else if (ttoption == 3)
            {
                bindData(sDataSource);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }
   }

     public void bindData(string sDataSource)
     {
         
         string trange = string.Empty;
         string toption = string.Empty;

         DataSet ds = new DataSet();

         DateTime refDate = DateTime.Parse(txtStartDate.Text);

         int ttrange = Convert.ToInt32(cmbtrange.SelectedItem.Value);
         int ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

         int tstock = 0;
         int trol = 0;

         ds = objBL.getstocklevel(sDataSource, refDate, trange, toption);

         DataTable dt = new DataTable();

         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                 dt.Columns.Add(new DataColumn("Category"));
                  dt.Columns.Add(new DataColumn("Brand"));
                  dt.Columns.Add(new DataColumn("ProductName"));
                  dt.Columns.Add(new DataColumn("Model"));
                  dt.Columns.Add(new DataColumn("ItemCode"));
                  dt.Columns.Add(new DataColumn("Stock Level (ROL)"));
                  dt.Columns.Add(new DataColumn("Stock"));
                  dt.Columns.Add(new DataColumn("Stock Value"));

                  DataRow dr_final111 = dt.NewRow();
                  dt.Rows.Add(dr_final111);

                  //if (ds.Tables[0].Rows.Count > 0)
                  //{
                  //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                  //    {
                  //        tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                  //        if (tstock == 0)
                  //        {
                  //            ds.Tables[0].Rows[i].Delete();
                  //        }
                  //    }
                  //    ds.Tables[0].AcceptChanges();
                  //}

                  if (ds.Tables[0].Rows.Count > 0)
                  {
                      if (ttrange == 1)
                      {
                          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                          {
                              tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                              trol = Convert.ToInt32(ds.Tables[0].Rows[i]["rol"]);
                              if ((tstock > trol) || (tstock == trol))
                              {
                                  ds.Tables[0].Rows[i].Delete();
                              }
                          }
                          ds.Tables[0].AcceptChanges();
                      }
                      else if (ttrange == 2)
                      {
                          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                          {
                              tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                              trol = Convert.ToInt32(ds.Tables[0].Rows[i]["rol"]);
                              if ((tstock < trol) || (tstock == trol))
                              {
                                  ds.Tables[0].Rows[i].Delete();                              
                              }
                          }
                          ds.Tables[0].AcceptChanges();
                      }
                      else if (ttrange == 3)
                      {
                          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                          {
                              tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                              trol = Convert.ToInt32(ds.Tables[0].Rows[i]["rol"]);
                              if ((tstock > trol) || (tstock < trol))
                              {
                                  ds.Tables[0].Rows[i].Delete();
                              }
                          }
                          ds.Tables[0].AcceptChanges();
                      }
                  }

                  if (ds.Tables[0].Rows.Count > 0)
                  {
                      foreach (DataRow dr in ds.Tables[0].Rows)
                      {
                          DataRow dr_final6 = dt.NewRow();
                          dr_final6["Category"] = dr["Categoryname"];
                          dr_final6["Brand"] = dr["productdesc"];
                          dr_final6["ProductName"] = dr["ProductName"];
                          dr_final6["Model"] = dr["Model"];
                          dr_final6["ItemCode"] = dr["Itemcode"];
                          dr_final6["Stock Level (ROL)"] = dr["rol"];
                          dr_final6["Stock"] = dr["Stock"];
                          dr_final6["Stock Value"] = Convert.ToInt32(dr["Stock"]) * Convert.ToDouble(dr["Rate"]);
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
         else
         {
             ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
         }
     }

     public void bindDataCategory(string sDataSource)
     {

         string trange = string.Empty;
         string toption = string.Empty;

         DataSet ds = new DataSet();

         DateTime refDate = DateTime.Parse(txtStartDate.Text);

         int ttrange = Convert.ToInt32(cmbtrange.SelectedItem.Value);
         int ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

         int tstock = 0;
         int trol = 0;

         ds = objBL.getstocklevelcategory(sDataSource, refDate, trange, toption);

         DataTable dt = new DataTable();

         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                 dt.Columns.Add(new DataColumn("Category"));
                 //dt.Columns.Add(new DataColumn("Brand"));
                 //dt.Columns.Add(new DataColumn("ProductName"));
                 //dt.Columns.Add(new DataColumn("Model"));
                 //dt.Columns.Add(new DataColumn("ItemCode"));
                 dt.Columns.Add(new DataColumn("Category Level"));
                 dt.Columns.Add(new DataColumn("Stock"));

                 DataRow dr_final111 = dt.NewRow();
                 dt.Rows.Add(dr_final111);

                 //if (ds.Tables[0].Rows.Count > 0)
                 //{
                 //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                 //    {
                 //        tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                 //        if (tstock == 0)
                 //        {
                 //            ds.Tables[0].Rows[i].Delete();
                 //        }
                 //    }
                 //    ds.Tables[0].AcceptChanges();
                 //}

                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     if (ttrange == 1)
                     {
                         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                         {
                             tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                             trol = Convert.ToInt32(ds.Tables[0].Rows[i]["categorylevel"]);
                             if (tstock > trol)
                             {
                                 ds.Tables[0].Rows[i].Delete();
                             }
                         }
                         ds.Tables[0].AcceptChanges();
                     }
                     else if (ttrange == 2)
                     {
                         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                         {
                             tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                             trol = Convert.ToInt32(ds.Tables[0].Rows[i]["categorylevel"]);
                             if (tstock < trol)
                             {
                                 ds.Tables[0].Rows[i].Delete();
                             }
                         }
                         ds.Tables[0].AcceptChanges();
                     }
                     else if (ttrange == 3)
                     {
                         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                         {
                             tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                             trol = Convert.ToInt32(ds.Tables[0].Rows[i]["categorylevel"]);
                             if ((trol < 0) || (trol > 0))
                             {
                                 ds.Tables[0].Rows[i].Delete();
                             }
                         }
                         ds.Tables[0].AcceptChanges();
                     }
                 }

                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         DataRow dr_final6 = dt.NewRow();
                         dr_final6["Category"] = dr["Categoryname"];
                         //dr_final6["Brand"] = dr["productdesc"];
                         //dr_final6["ProductName"] = dr["ProductName"];
                         //dr_final6["Model"] = dr["Model"];
                         //dr_final6["ItemCode"] = dr["Itemcode"];
                         dr_final6["Category Level"] = dr["categorylevel"];
                         dr_final6["Stock"] = dr["Stock"];

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
         else
         {
             ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
         }
     }

     public void bindDatabrand(string sDataSource)
     {

         string trange = string.Empty;
         string toption = string.Empty;

         DataSet ds = new DataSet();

         DateTime refDate = DateTime.Parse(txtStartDate.Text);

         int ttrange = Convert.ToInt32(cmbtrange.SelectedItem.Value);
         int ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

         int tstock = 0;
         int trol = 0;

         ds = objBL.getstocklevelbrand(sDataSource, refDate, trange, toption);

         DataTable dt = new DataTable();

         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                 //dt.Columns.Add(new DataColumn("Category"));
                 dt.Columns.Add(new DataColumn("Brand"));
                 //dt.Columns.Add(new DataColumn("ProductName"));
                 //dt.Columns.Add(new DataColumn("Model"));
                 //dt.Columns.Add(new DataColumn("ItemCode"));
                 dt.Columns.Add(new DataColumn("Brand Level"));
                 dt.Columns.Add(new DataColumn("Stock"));

                 DataRow dr_final111 = dt.NewRow();
                 dt.Rows.Add(dr_final111);

                 //if (ds.Tables[0].Rows.Count > 0)
                 //{
                 //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                 //    {
                 //        tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                 //        if (tstock == 0)
                 //        {
                 //            ds.Tables[0].Rows[i].Delete();
                 //        }
                 //    }
                 //    ds.Tables[0].AcceptChanges();
                 //}

                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     if (ttrange == 1)
                     {
                         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                         {
                             tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                             trol = Convert.ToInt32(ds.Tables[0].Rows[i]["brandlevel"]);
                             if (tstock > trol)
                             {
                                 ds.Tables[0].Rows[i].Delete();
                             }
                         }
                         ds.Tables[0].AcceptChanges();
                     }
                     else if (ttrange == 2)
                     {
                         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                         {
                             tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                             trol = Convert.ToInt32(ds.Tables[0].Rows[i]["brandlevel"]);
                             if (tstock < trol)
                             {
                                 ds.Tables[0].Rows[i].Delete();
                             }
                         }
                         ds.Tables[0].AcceptChanges();
                     }
                     else if (ttrange == 3)
                     {
                         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                         {
                             tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
                             trol = Convert.ToInt32(ds.Tables[0].Rows[i]["brandlevel"]);
                             if ((trol < 0) || (trol > 0))
                             {
                                 ds.Tables[0].Rows[i].Delete();
                             }
                         }
                         ds.Tables[0].AcceptChanges();
                     }
                 }

                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         DataRow dr_final6 = dt.NewRow();
                         //dr_final6["Category"] = dr["Categoryname"];
                         dr_final6["Brand"] = dr["brandname"];
                         //dr_final6["ProductName"] = dr["ProductName"];
                         //dr_final6["Model"] = dr["Model"];
                         //dr_final6["ItemCode"] = dr["Itemcode"];
                         dr_final6["Brand Level"] = dr["brandlevel"];
                         dr_final6["Stock"] = dr["Stock"];

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
             string filename = "Stock Level.xls";
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