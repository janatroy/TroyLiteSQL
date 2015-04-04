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
using System.Data;
using ClosedXML.Excel;
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




                loadBranch();
                loadPriceList();

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

    private void loadPriceList()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        lstPricelist.Items.Clear();
        //lstPricelist.Items.Add(new ListItem("All", "0"));
        ds = bl.ListPriceList(connection);
        lstPricelist.DataSource = ds;
        lstPricelist.DataTextField = "PriceName";
        lstPricelist.DataValueField = "PriceName";
        lstPricelist.DataBind();
    }

    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpBranchAdd.Items.Clear();
        drpBranchAdd.Items.Add(new ListItem("Select Branch", "0"));
        ds = bl.ListBranch();
        drpBranchAdd.DataSource = ds;
        drpBranchAdd.DataTextField = "BranchName";
        drpBranchAdd.DataValueField = "Branchcode";
        drpBranchAdd.DataBind();
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
            if (drpBranchAdd.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select Branch. It cannot be left Blank');", true);
            }
            else
            {
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
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }


   }

   protected string getCond1()
   {
       string cond1 = "";
       foreach (ListItem listItem1 in lstPricelist.Items)
       {
           if (listItem1.Selected)
           {
               cond1 += listItem1.Value + ",";
           }
       }

       return cond1;
   }

     public void bindData(string sDataSource)
     {
         
         string trange = string.Empty;
         string toption = string.Empty;
         string Branch = drpBranchAdd.SelectedValue;
        // string lstPricelist = lstPricelist.items;

         DataSet ds = new DataSet();

         DataSet dst = new DataSet();

         DateTime refDate = DateTime.Parse(txtStartDate.Text);

         int ttrange = Convert.ToInt32(cmbtrange.SelectedItem.Value);
         int ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

         int tstock = 0;
         int trol = 0;
         string itemcode = string.Empty;

         ds = objBL.getstocklevel(sDataSource, refDate, trange, toption, Branch);

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
                  dt.Columns.Add(new DataColumn("ROL"));
                  dt.Columns.Add(new DataColumn("Stock"));
                  dt.Columns.Add(new DataColumn("Branchcode"));
                  dt.Columns.Add(new DataColumn("Stock Value"));

                  foreach (ListItem listItem1 in lstPricelist.Items)
                  {
                      if (listItem1.Selected)
                      {
                          string item1 = listItem1.Value;

                          dt.Columns.Add(new DataColumn(item1));
                      }
                  }

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
                          itemcode = Convert.ToString(dr["itemcode"]);

                          dst = objBL.GetAbsoluteProductpricelist(sDataSource, itemcode, Branch);

                          DataRow dr_final6 = dt.NewRow();
                          dr_final6["Category"] = dr["Categoryname"];
                          dr_final6["Brand"] = dr["productdesc"];
                          dr_final6["ProductName"] = dr["ProductName"];
                          dr_final6["Model"] = dr["Model"];
                          dr_final6["ItemCode"] = dr["Itemcode"];
                          dr_final6["ROL"] = dr["rol"];
                          dr_final6["Stock"] = dr["Stock"];

                          if (dst != null)
                          {
                              if (dst.Tables[0].Rows.Count > 0)
                              {
                                  foreach (DataRow drt in dst.Tables[0].Rows)
                                  {
                                      dr_final6["Branchcode"] = drt["Branchcode"];

                                      foreach (ListItem listItem1 in lstPricelist.Items)
                                      {
                                          if (listItem1.Selected)
                                          {
                                              string item1 = listItem1.Value;
                                              string item123 = Convert.ToString(drt["pricename"]);

                                              if (item123 == item1)
                                              {
                                                  dr_final6[item1] = drt["price"];
                                              }

                                          }
                                      }
                                  }
                              }
                          }


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

         string Branch = drpBranchAdd.SelectedValue;

         int tstock = 0;
         int trol = 0;

         ds = objBL.getstocklevelcategory(sDataSource, refDate, trange, toption, Branch);
         DataSet dst = new DataSet();

         string Category = string.Empty;

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
                 dt.Columns.Add(new DataColumn("Category %"));
                 dt.Columns.Add(new DataColumn("Stock"));

                 dt.Columns.Add(new DataColumn("Branchcode"));
                 

                 foreach (ListItem listItem1 in lstPricelist.Items)
                 {
                     if (listItem1.Selected)
                     {
                         string item1 = listItem1.Value + " Value %";

                         dt.Columns.Add(new DataColumn(item1));
                     }
                 }

                 //dt.Columns.Add(new DataColumn("Stock Value %"));

                 //DataRow dr_final111 = dt.NewRow();
                 //dt.Rows.Add(dr_final111);

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
                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         Category = Convert.ToString(dr["Categoryname"]);

                         

                         DataRow dr_final6 = dt.NewRow();
                         dr_final6["Category"] = dr["Categoryname"];
                         //dr_final6["Brand"] = dr["productdesc"];
                         //dr_final6["ProductName"] = dr["ProductName"];
                         //dr_final6["Model"] = dr["Model"];
                         //dr_final6["ItemCode"] = dr["Itemcode"];
                         dr_final6["Category %"] = dr["categorylevel"];
                         dr_final6["Stock"] = dr["Stock"];
                         dr_final6["Branchcode"] = drpBranchAdd.SelectedValue;
                       

                                     foreach (ListItem listItem1 in lstPricelist.Items)
                                     {
                                         if (listItem1.Selected)
                                         {
                                             dst = objBL.GetAbsoluteCategorypricelist(sDataSource, Category, Branch, "Category", listItem1.Value);

                                             if (dst != null)
                                             {
                                                 if (dst.Tables[0].Rows.Count > 0)
                                                 {
                                                     foreach (DataRow drt in dst.Tables[0].Rows)
                                                     {
                                                         

                                                        string item1 = listItem1.Value + " Value %";
                                                        //string item123 = Convert.ToString(drt["pricename"]);

                                                        //if (item123 == item1)
                                                        //{
                                                            dr_final6[item1] = Convert.ToDouble(drt["price"])/100;
                                                        //}

                                                }
                                            }
                                        }
                                    }
                         }


                         dt.Rows.Add(dr_final6);
                     }

                     DataSet dstt = new DataSet();
                     dstt.Tables.Add(dt);

                     double troll = 0;


                     if (dstt.Tables[0].Rows.Count > 0)
                     {
                         if (ttrange == 1)
                         {
                             for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                             {
                                 tstock = Convert.ToInt32(dstt.Tables[0].Rows[i]["Stock"]);
                                 trol = Convert.ToInt32(dstt.Tables[0].Rows[i]["category %"]);

                                 troll = Convert.ToDouble(dstt.Tables[0].Rows[i]["price"]) / 100;

                                 if (troll > trol)
                                 {
                                     dstt.Tables[0].Rows[i].Delete();
                                 }
                             }
                             dstt.Tables[0].AcceptChanges();
                         }
                         else if (ttrange == 2)
                         {
                             for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                             {
                                 tstock = Convert.ToInt32(dstt.Tables[0].Rows[i]["Stock"]);
                                 trol = Convert.ToInt32(dstt.Tables[0].Rows[i]["category %"]);

                                 troll = Convert.ToDouble(dstt.Tables[0].Rows[i]["price"]) / 100;

                                 if (troll < trol)
                                 {
                                     dstt.Tables[0].Rows[i].Delete();
                                 }
                             }
                             dstt.Tables[0].AcceptChanges();
                         }
                         else if (ttrange == 3)
                         {
                             for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                             {
                                 tstock = Convert.ToInt32(dstt.Tables[0].Rows[i]["Stock"]);
                                 trol = Convert.ToInt32(dstt.Tables[0].Rows[i]["category %"]);

                                 troll = Convert.ToDouble(dstt.Tables[0].Rows[i]["price"]) / 100;

                                 if ((trol < 0) || (trol > 0))
                                 {
                                     dstt.Tables[0].Rows[i].Delete();
                                 }
                             }
                             dstt.Tables[0].AcceptChanges();
                         }
                         else
                         {

                         }
                     }

                     //DataTable dtt = new DataTable();
                     //dtt.(dstt);

                     DataTable dtt = dstt.Tables[0];

                     ExportToExcel(dtt);
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

         string Branch = drpBranchAdd.SelectedValue;

         ds = objBL.getstocklevelbrand(sDataSource, refDate, trange, toption, Branch);

         DataSet dst = new DataSet();

         string itemcode = string.Empty;

         DataTable dt = new DataTable("Srock report");

         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                 //dt.Columns.Add(new DataColumn("Category"));
                 dt.Columns.Add(new DataColumn("Brand"));
                 //dt.Columns.Add(new DataColumn("ProductName"));
                 //dt.Columns.Add(new DataColumn("Model"));
                 //dt.Columns.Add(new DataColumn("ItemCode"));
                 dt.Columns.Add(new DataColumn("Brand %"));
                 dt.Columns.Add(new DataColumn("Stock"));

                 dt.Columns.Add(new DataColumn("Branchcode"));
               //  dt.Columns.Add(new DataColumn("Stock Value"));

                 foreach (ListItem listItem1 in lstPricelist.Items)
                 {
                     if (listItem1.Selected)
                     {
                         string item1 = listItem1.Value + " Value %";

                         dt.Columns.Add(new DataColumn(item1));
                     }
                 }

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
                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         brand = Convert.ToString(dr["BrandName"]);

                         //dst = objBL.GetAbsoluteProductpricelist(sDataSource, itemcode, Branch);

                         DataRow dr_final6 = dt.NewRow();
                         //dr_final6["Category"] = dr["Categoryname"];
                         dr_final6["Brand"] = dr["brandname"];
                         //dr_final6["ProductName"] = dr["ProductName"];
                         //dr_final6["Model"] = dr["Model"];
                         dr_final6["BranchCode"] = Branch;
                         dr_final6["Brand %"] = dr["brandlevel"];
                         dr_final6["Stock"] = dr["Stock"];

                         //if (dst != null)
                         //{
                         //    if (dst.Tables[0].Rows.Count > 0)
                         //    {
                         //        foreach (DataRow drt in dst.Tables[0].Rows)
                         //        {
                         //            dr_final6["Branchcode"] = drt["Branchcode"];

                         //            foreach (ListItem listItem1 in lstPricelist.Items)
                         //            {
                         //                if (listItem1.Selected)
                         //                {
                         //                    string item1 = listItem1.Value;
                         //                    string item123 = Convert.ToString(drt["pricename"]);

                         //                    if (item123 == item1)
                         //                    {
                         //                        dr_final6[item1] = drt["price"];
                         //                    }

                         //                }
                         //            }
                         //        }
                         //    }
                         //}
                         foreach (ListItem listItem1 in lstPricelist.Items)
                         {
                             if (listItem1.Selected)
                             {
                                 dst = objBL.GetAbsoluteCategorypricelist(sDataSource, brand, Branch, "brand", listItem1.Value);

                                 if (dst != null)
                                 {
                                     if (dst.Tables[0].Rows.Count > 0)
                                     {
                                         foreach (DataRow drt in dst.Tables[0].Rows)
                                         {


                                             string item1 = listItem1.Value + " Value %";
                                             //string item123 = Convert.ToString(drt["pricename"]);

                                             //if (item123 == item1)
                                             //{
                                             dr_final6[item1] = Convert.ToDouble(drt["price"]) / 100;
                                             //}

                                         }
                                     }
                                 }
                             }
                         }

                         dt.Rows.Add(dr_final6);
                     }
                     DataSet dstt = new DataSet();
                     dstt.Tables.Add(dt);
                     double troll;


                     if (dstt.Tables[0].Rows.Count > 0)
                     {
                         if (ttrange == 1)
                         {
                             for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                             {
                                 tstock = Convert.ToInt32(dstt.Tables[0].Rows[i]["Stock"]);
                                 trol = Convert.ToInt32(dstt.Tables[0].Rows[i]["brand %"]);

                                 troll = Convert.ToDouble(dstt.Tables[0].Rows[i]["price"]) / 100;
                                 if (tstock > trol)
                                 {
                                     dstt.Tables[0].Rows[i].Delete();
                                 }
                             }
                             dstt.Tables[0].AcceptChanges();
                         }
                         else if (ttrange == 2)
                         {
                             for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                             {
                                 tstock = Convert.ToInt32(dstt.Tables[0].Rows[i]["Stock"]);
                                 trol = Convert.ToInt32(dstt.Tables[0].Rows[i]["brand %"]);

                                 troll = Convert.ToDouble(dstt.Tables[0].Rows[i]["price"]) / 100;
                                 if (tstock < trol)
                                 {
                                     dstt.Tables[0].Rows[i].Delete();
                                 }
                             }
                             dstt.Tables[0].AcceptChanges();
                         }
                         else if (ttrange == 3)
                         {
                             for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                             {
                                 tstock = Convert.ToInt32(dstt.Tables[0].Rows[i]["Stock"]);
                                 trol = Convert.ToInt32(dstt.Tables[0].Rows[i]["brand %"]);

                                 troll = Convert.ToDouble(dstt.Tables[0].Rows[i]["price"]) / 100;
                                 if ((trol < 0) || (trol > 0))
                                 {
                                     dstt.Tables[0].Rows[i].Delete();
                                 }
                             }
                             dstt.Tables[0].AcceptChanges();
                         }
                     }
                     DataTable dtt = dstt.Tables[0];
                     ExportToExcel(dtt);
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
             using (XLWorkbook wb = new XLWorkbook())
             {
                 string filename = "Stock report.xlsx";
                 wb.Worksheets.Add(dt);
                 Response.Clear();
                 Response.Buffer = true;
                 Response.Charset = "";
                 Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                 Response.AddHeader("content-disposition", "attachment;filename=" + filename + "");
                 using (MemoryStream MyMemoryStream = new MemoryStream())
                 {
                     wb.SaveAs(MyMemoryStream);
                     MyMemoryStream.WriteTo(Response.OutputStream);
                     Response.Flush();
                     Response.End();
                 }
             }

         }
     }

     protected void lst_SelectedIndexChanged_1(object sender, EventArgs e)
     {
         if (CheckBoxList1.Items[0].Selected == true)
         {
             foreach (ListItem ls in lstPricelist.Items)
             {
                 ls.Selected = true;

             }

         }
         else
         {
             foreach (ListItem ls in lstPricelist.Items)
             {
                 ls.Selected = false;

             }

         }
     }

     protected void btnReport_Click(object sender, EventArgs e)
     {
         try
         {
             if (drpBranchAdd.SelectedValue == "0")
             {
                 ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select Branch. It cannot be left Blank');", true);
             }
             else
             {
                 int ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

                 //if (ttoption == 1)
                 //{
                 //    bindDataCategory(sDataSource);
                 //}
                 //else if (ttoption == 2)
                 //{
                 //    bindDatabrand(sDataSource);
                 //}
                 //else if (ttoption == 3)
                 //{
                 //    bindData(sDataSource);
                 //}

                 string trange = string.Empty;
                 string toption = string.Empty;
                 string Branch = drpBranchAdd.SelectedValue;



                 DateTime refDate = DateTime.Parse(txtStartDate.Text);

                 int ttrange = Convert.ToInt32(cmbtrange.SelectedItem.Value);
                 ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

                 string cond1 = "";
                 cond1 = getCond1();
                 //int tstock = 0;
                 //int trol = 0;
                 //string itemcode = string.Empty;

                 Response.Write("<script language='javascript'> window.open('ReportXLStockLevel1.aspx?Branch=" + Branch + "&refdate=" + refDate + "&range=" + ttrange + "&option=" + ttoption + "&cond1=" + Server.UrlEncode(cond1) + "' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
             }
         }
         catch (Exception ex)
         {
             TroyLiteExceptionManager.HandleException(ex);
             return;
         }
     }
    
}