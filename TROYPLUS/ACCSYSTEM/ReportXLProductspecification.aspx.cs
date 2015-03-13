//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class ReportXLProductspecification : System.Web.UI.Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {

//    }
//}


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

public partial class ReportXLProductspecification : System.Web.UI.Page
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
    string grpBy = "", selCols = "", selLevels = "", field1 = "", field2 = "", ordrby = "", cond = "", Subordrby = "";

    //DBClass objBL = new DBClass();
    BusinessLogic objBL = new BusinessLogic();
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

                DateTime date = Convert.ToDateTime("01-01-1900");
                DataSet ds = new DataSet();

                // drpproduct_SelectedIndexChanged(sender, e);

                Loadproduct();
                string connection = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connection = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("Login.aspx");

                //  ds = objBL.GetTaskList(connection, "", "");

                //if (ds != null)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        DataTable dt = new DataTable("Tasks Details");
                //        dt.Columns.Add(new DataColumn("Task Id"));
                //        dt.Columns.Add(new DataColumn("Task Date"));
                //        dt.Columns.Add(new DataColumn("Task Name"));
                //        dt.Columns.Add(new DataColumn("Expected Start Date"));
                //        dt.Columns.Add(new DataColumn("Expected End Date"));
                //        dt.Columns.Add(new DataColumn("Owner"));
                //        dt.Columns.Add(new DataColumn("Project Name"));
                //        dt.Columns.Add(new DataColumn("Is Active"));
                //        dt.Columns.Add(new DataColumn("Task Description"));
                //        dt.Columns.Add(new DataColumn("BranchCode"));

                //        DataRow dr_final123 = dt.NewRow();
                //        dt.Rows.Add(dr_final123);

                //        foreach (DataRow dr in ds.Tables[0].Rows)
                //        {
                //            DataRow dr_final1 = dt.NewRow();
                //            dr_final1["Task Id"] = dr["Task_Id"];

                //            string aa = dr["Task_Date"].ToString().ToUpper().Trim();
                //            string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                //            dr_final1["Task Date"] = dtaa;

                //            dr_final1["Task Name"] = dr["Task_Name"];

                //            string aad = dr["Expected_Start_Date"].ToString().ToUpper().Trim();
                //            string dtaad = Convert.ToDateTime(aad).ToString("dd/MM/yyyy");
                //            dr_final1["Expected Start Date"] = dtaad;

                //            string aat = dr["Expected_End_Date"].ToString().ToUpper().Trim();
                //            string dtaat = Convert.ToDateTime(aat).ToString("dd/MM/yyyy");
                //            dr_final1["Expected End Date"] = dtaat;

                //            dr_final1["Owner"] = dr["empfirstname"];
                //            dr_final1["Project Name"] = dr["ProjectName"];
                //            dr_final1["Is Active"] = dr["IsActive"];
                //            dr_final1["Task Description"] = dr["Task_Description"];
                //            dr_final1["BranchCode"] = dr["BranchCode"];
                //            dt.Rows.Add(dr_final1);
                //        }
                //        DataRow dr_final2 = dt.NewRow();
                //        dt.Rows.Add(dr_final2);
                //        ExportToExcel(dt);
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                //    }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                //}
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
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            DateTime date = Convert.ToDateTime("01-01-1900");
            DataSet ds = new DataSet();


            BusinessLogic bl = new BusinessLogic(sDataSource);
            string Product_Id = string.Empty;

            //string connection1 = Request.Cookies["Company"].Value;


            //Product_Id = Convert.ToString(drpproduct.SelectedItem.Text);
            //if (GrdWME.SelectedDataKey.Value != null && GrdWME.SelectedDataKey.Value.ToString() != "")
            //    Project_Id = Convert.ToInt32(GrdWME.SelectedDataKey.Value.ToString());

            //drpproduct.Items.Clear();
            //drpproduct.Items.Add(new ListItem("---All---", "0"));
            //DataSet ds1 = bl.Getproductspecifacation(connection1, Product_Id);
            //drpproduct.DataSource = ds1;
            //drpproduct.DataBind();
            //drpproduct.DataTextField = "FormulaName";
            //drpproduct.DataValueField = "FormulaName";
           // UpdatePanel123.Update();



            string connection = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;
            else
                Response.Redirect("Login.aspx");


            string productid = Convert.ToString(drpproduct.SelectedItem.Text);
            ds = objBL.Getproductlistspecification(connection,productid);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable("Prodcuct specification");
                    dt.Columns.Add(new DataColumn("Product Name"));
                    dt.Columns.Add(new DataColumn("Itemcode"));
                    dt.Columns.Add(new DataColumn("Qty"));
                    dt.Columns.Add(new DataColumn("In/Out"));
                    dt.Columns.Add(new DataColumn("Unit Of Measure"));
                    dt.Columns.Add(new DataColumn("ProductName"));
                    dt.Columns.Add(new DataColumn("ProductDesc"));
                    dt.Columns.Add(new DataColumn("Model"));
                    dt.Columns.Add(new DataColumn("CurrentStock"));
                    dt.Columns.Add(new DataColumn("BranchCode"));
                
                    DataRow dr_final123 = dt.NewRow();
                    dt.Rows.Add(dr_final123);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_final1 = dt.NewRow();
                        dr_final1["Product Name"] = dr["FormulaName"];
                        dr_final1["Itemcode"] = dr["ItemCode"];
                        dr_final1["Qty"] = dr["Qty"];
                        dr_final1["In/Out"] = dr["InOut"];
                        dr_final1["Unit Of Measure"] = dr["Unit_Of_Measure"];
                        dr_final1["ProductName"] = dr["ProductName"];
                        dr_final1["ProductDesc"] = dr["ProductDesc"];
                        dr_final1["Model"] = dr["Model"];
                        dr_final1["CurrentStock"] = dr["Stock"];
                       
                        //dr_final1["Task Description"] = dr["Task_Description"];
                        dr_final1["BranchCode"] = dr["BranchCode"];
                        dt.Rows.Add(dr_final1);
                    }
                    DataRow dr_final2 = dt.NewRow();                
                    dt.Rows.Add(dr_final2);
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
       catch (Exception ex)
       {
           TroyLiteExceptionManager.HandleException(ex);
           return;
       }
    }

    public void bindData(string sDataSource)
    {

        //string trange = string.Empty;
        //string toption = string.Empty;

        //DataSet ds = new DataSet();

        //DateTime refDate = DateTime.Parse(txtStartDate.Text);

        //int ttrange = Convert.ToInt32(cmbtrange.SelectedItem.Value);
        //int ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

        //int tstock = 0;
        //int trol = 0;

        //ds = objBL.getstocklevel(sDataSource, refDate, trange, toption);

        //DataTable dt = new DataTable();

        //if (ds != null)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        dt.Columns.Add(new DataColumn("Category"));
        //        dt.Columns.Add(new DataColumn("Brand"));
        //        dt.Columns.Add(new DataColumn("ProductName"));
        //        dt.Columns.Add(new DataColumn("Model"));
        //        dt.Columns.Add(new DataColumn("ItemCode"));
        //        dt.Columns.Add(new DataColumn("Stock Level (ROL)"));
        //        dt.Columns.Add(new DataColumn("Stock"));
        //        dt.Columns.Add(new DataColumn("Stock Value"));

        //        DataRow dr_final111 = dt.NewRow();
        //        dt.Rows.Add(dr_final111);

        //        //if (ds.Tables[0].Rows.Count > 0)
        //        //{
        //        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        //    {
        //        //        tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
        //        //        if (tstock == 0)
        //        //        {
        //        //            ds.Tables[0].Rows[i].Delete();
        //        //        }
        //        //    }
        //        //    ds.Tables[0].AcceptChanges();
        //        //}

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ttrange == 1)
        //            {
        //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                {
        //                    tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
        //                    trol = Convert.ToInt32(ds.Tables[0].Rows[i]["rol"]);
        //                    if ((tstock > trol) || (tstock == trol))
        //                    {
        //                        ds.Tables[0].Rows[i].Delete();
        //                    }
        //                }
        //                ds.Tables[0].AcceptChanges();
        //            }
        //            else if (ttrange == 2)
        //            {
        //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                {
        //                    tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
        //                    trol = Convert.ToInt32(ds.Tables[0].Rows[i]["rol"]);
        //                    if ((tstock < trol) || (tstock == trol))
        //                    {
        //                        ds.Tables[0].Rows[i].Delete();
        //                    }
        //                }
        //                ds.Tables[0].AcceptChanges();
        //            }
        //            else if (ttrange == 3)
        //            {
        //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                {
        //                    tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
        //                    trol = Convert.ToInt32(ds.Tables[0].Rows[i]["rol"]);
        //                    if ((tstock > trol) || (tstock < trol))
        //                    {
        //                        ds.Tables[0].Rows[i].Delete();
        //                    }
        //                }
        //                ds.Tables[0].AcceptChanges();
        //            }
        //        }

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                DataRow dr_final6 = dt.NewRow();
        //                dr_final6["Category"] = dr["Categoryname"];
        //                dr_final6["Brand"] = dr["productdesc"];
        //                dr_final6["ProductName"] = dr["ProductName"];
        //                dr_final6["Model"] = dr["Model"];
        //                dr_final6["ItemCode"] = dr["Itemcode"];
        //                dr_final6["Stock Level (ROL)"] = dr["rol"];
        //                dr_final6["Stock"] = dr["Stock"];
        //                dr_final6["Stock Value"] = Convert.ToInt32(dr["Stock"]) * Convert.ToDouble(dr["Rate"]);
        //                dt.Rows.Add(dr_final6);
        //            }
        //            ExportToExcel(dt);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        //        }

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        //    }
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        //}
    }

    public void bindDataCategory(string sDataSource)
    {

    //    string trange = string.Empty;
    //    string toption = string.Empty;

    //    DataSet ds = new DataSet();

    //    DateTime refDate = DateTime.Parse(txtStartDate.Text);

    //    int ttrange = Convert.ToInt32(cmbtrange.SelectedItem.Value);
    //    int ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

    //    int tstock = 0;
    //    int trol = 0;

    //    ds = objBL.getstocklevelcategory(sDataSource, refDate, trange, toption);

    //    DataTable dt = new DataTable();

    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            dt.Columns.Add(new DataColumn("Category"));
    //            //dt.Columns.Add(new DataColumn("Brand"));
    //            //dt.Columns.Add(new DataColumn("ProductName"));
    //            //dt.Columns.Add(new DataColumn("Model"));
    //            //dt.Columns.Add(new DataColumn("ItemCode"));
    //            dt.Columns.Add(new DataColumn("Category Level"));
    //            dt.Columns.Add(new DataColumn("Stock"));

    //            DataRow dr_final111 = dt.NewRow();
    //            dt.Rows.Add(dr_final111);

    //            //if (ds.Tables[0].Rows.Count > 0)
    //            //{
    //            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            //    {
    //            //        tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
    //            //        if (tstock == 0)
    //            //        {
    //            //            ds.Tables[0].Rows[i].Delete();
    //            //        }
    //            //    }
    //            //    ds.Tables[0].AcceptChanges();
    //            //}

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                if (ttrange == 1)
    //                {
    //                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                    {
    //                        tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
    //                        trol = Convert.ToInt32(ds.Tables[0].Rows[i]["categorylevel"]);
    //                        if (tstock > trol)
    //                        {
    //                            ds.Tables[0].Rows[i].Delete();
    //                        }
    //                    }
    //                    ds.Tables[0].AcceptChanges();
    //                }
    //                else if (ttrange == 2)
    //                {
    //                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                    {
    //                        tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
    //                        trol = Convert.ToInt32(ds.Tables[0].Rows[i]["categorylevel"]);
    //                        if (tstock < trol)
    //                        {
    //                            ds.Tables[0].Rows[i].Delete();
    //                        }
    //                    }
    //                    ds.Tables[0].AcceptChanges();
    //                }
    //                else if (ttrange == 3)
    //                {
    //                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                    {
    //                        tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
    //                        trol = Convert.ToInt32(ds.Tables[0].Rows[i]["categorylevel"]);
    //                        if ((trol < 0) || (trol > 0))
    //                        {
    //                            ds.Tables[0].Rows[i].Delete();
    //                        }
    //                    }
    //                    ds.Tables[0].AcceptChanges();
    //                }
    //            }

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                foreach (DataRow dr in ds.Tables[0].Rows)
    //                {
    //                    DataRow dr_final6 = dt.NewRow();
    //                    dr_final6["Category"] = dr["Categoryname"];
    //                    //dr_final6["Brand"] = dr["productdesc"];
    //                    //dr_final6["ProductName"] = dr["ProductName"];
    //                    //dr_final6["Model"] = dr["Model"];
    //                    //dr_final6["ItemCode"] = dr["Itemcode"];
    //                    dr_final6["Category Level"] = dr["categorylevel"];
    //                    dr_final6["Stock"] = dr["Stock"];

    //                    dt.Rows.Add(dr_final6);
    //                }
    //                ExportToExcel(dt);
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
    //            }

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
    //    }
    }

    public void bindDatabrand(string sDataSource)
    {

     //   string trange = string.Empty;
     //   string toption = string.Empty;

     //   DataSet ds = new DataSet();

     ////   DateTime refDate = DateTime.Parse(txtStartDate.Text);

     //  // int ttrange = Convert.ToInt32(cmbtrange.SelectedItem.Value);
     // //  int ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

     //   int tstock = 0;
     //   int trol = 0;

     // //  ds = objBL.getstocklevelbrand(sDataSource, refDate, trange, toption);

     //   DataTable dt = new DataTable();

     //   if (ds != null)
     //   {
     //       if (ds.Tables[0].Rows.Count > 0)
     //       {
     //           //dt.Columns.Add(new DataColumn("Category"));
     //           dt.Columns.Add(new DataColumn("Brand"));
     //           //dt.Columns.Add(new DataColumn("ProductName"));
     //           //dt.Columns.Add(new DataColumn("Model"));
     //           //dt.Columns.Add(new DataColumn("ItemCode"));
     //           dt.Columns.Add(new DataColumn("Brand Level"));
     //           dt.Columns.Add(new DataColumn("Stock"));

     //           DataRow dr_final111 = dt.NewRow();
     //           dt.Rows.Add(dr_final111);

     //           //if (ds.Tables[0].Rows.Count > 0)
     //           //{
     //           //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
     //           //    {
     //           //        tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
     //           //        if (tstock == 0)
     //           //        {
     //           //            ds.Tables[0].Rows[i].Delete();
     //           //        }
     //           //    }
     //           //    ds.Tables[0].AcceptChanges();
     //           //}

     //           if (ds.Tables[0].Rows.Count > 0)
     //           {
     //               if (ttrange == 1)
     //               {
     //                   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
     //                   {
     //                       tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
     //                       trol = Convert.ToInt32(ds.Tables[0].Rows[i]["brandlevel"]);
     //                       if (tstock > trol)
     //                       {
     //                           ds.Tables[0].Rows[i].Delete();
     //                       }
     //                   }
     //                   ds.Tables[0].AcceptChanges();
     //               }
     //               else if (ttrange == 2)
     //               {
     //                   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
     //                   {
     //                       tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
     //                       trol = Convert.ToInt32(ds.Tables[0].Rows[i]["brandlevel"]);
     //                       if (tstock < trol)
     //                       {
     //                           ds.Tables[0].Rows[i].Delete();
     //                       }
     //                   }
     //                   ds.Tables[0].AcceptChanges();
     //               }
     //               else if (ttrange == 3)
     //               {
     //                   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
     //                   {
     //                       tstock = Convert.ToInt32(ds.Tables[0].Rows[i]["Stock"]);
     //                       trol = Convert.ToInt32(ds.Tables[0].Rows[i]["brandlevel"]);
     //                       if ((trol < 0) || (trol > 0))
     //                       {
     //                           ds.Tables[0].Rows[i].Delete();
     //                       }
     //                   }
     //                   ds.Tables[0].AcceptChanges();
     //               }
     //           }

     //           if (ds.Tables[0].Rows.Count > 0)
     //           {
     //               foreach (DataRow dr in ds.Tables[0].Rows)
     //               {
     //                   DataRow dr_final6 = dt.NewRow();
     //                   //dr_final6["Category"] = dr["Categoryname"];
     //                   dr_final6["Brand"] = dr["brandname"];
     //                   //dr_final6["ProductName"] = dr["ProductName"];
     //                   //dr_final6["Model"] = dr["Model"];
     //                   //dr_final6["ItemCode"] = dr["Itemcode"];
     //                   dr_final6["Brand Level"] = dr["brandlevel"];
     //                   dr_final6["Stock"] = dr["Stock"];

     //                   dt.Rows.Add(dr_final6);
     //               }
     //               ExportToExcel(dt);
     //           }
     //           else
     //           {
     //               ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
     //           }

     //       }
     //       else
     //       {
     //           ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
     //       }
     //   }
     //   else
     //   {
     //       ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
     //   }
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
                string filename = "Product Specification Details.xlsx";
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

    protected void drpproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string Product_Id = string.Empty;

            string connection = Request.Cookies["Company"].Value;


            Product_Id = Convert.ToString(drpproduct.SelectedItem.Text);
            //if (GrdWME.SelectedDataKey.Value != null && GrdWME.SelectedDataKey.Value.ToString() != "")
            //    Project_Id = Convert.ToInt32(GrdWME.SelectedDataKey.Value.ToString());

            drpoption.Items.Clear();
            drpoption.Items.Add(new ListItem("---All---", "0"));
            DataSet ds = bl.Getproductspecifacation(connection, Product_Id);
            drpoption.DataSource = ds;
            drpoption.DataBind();
            drpoption.DataTextField = "FormulaName";
            drpoption.DataValueField = "FormulaName";
            UpdatePanel123.Update();
        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void Loadproduct()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpproduct.Items.Clear();
        drpproduct.Items.Add(new ListItem("---All---", "0"));

      // string connection = Request.Cookies["Company"].Value;

        ds = bl.Loadproduct(connection);
        drpproduct.DataSource = ds;
        drpproduct.DataBind();
        drpproduct.DataTextField = "FormulaName";
        drpproduct.DataValueField = "FormulaName";
    }
}