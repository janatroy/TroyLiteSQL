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

public partial class ReportXLStockLevel1 : System.Web.UI.Page
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



    private string sDataSource = string.Empty;
    private string Connection = string.Empty;
    BusinessLogic objBL = new BusinessLogic();
    string cond1;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            Connection = Request.Cookies["Company"].Value;
            if (!IsPostBack)
            {

                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

                    if (companyInfo != null)
                    {
                        if (companyInfo.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in companyInfo.Tables[0].Rows)
                            {
                                lblTNGST.Text = Convert.ToString(dr["TINno"]);
                                lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                                lblPhone.Text = Convert.ToString(dr["Phone"]);
                                lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                                lblAddress.Text = Convert.ToString(dr["Address"]);
                                lblCity.Text = Convert.ToString(dr["city"]);
                                lblPincode.Text = Convert.ToString(dr["Pincode"]);
                                lblState.Text = Convert.ToString(dr["state"]);

                            }
                        }
                    }
                }
            }

            lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            // txtStartDate.Text = dtaa;

            //  lblHeadDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

            lblHeading.Text = "Stock Level Report";

            divPrint.Visible = true;
            divPr.Visible = true;
            DataSet dstt = new DataSet();
            string connection = Request.Cookies["Company"].Value;

            string branch = Convert.ToString(Request.QueryString["Branch"].ToString());
            int range = Convert.ToInt32(Request.QueryString["range"].ToString());
            int option = Convert.ToInt32(Request.QueryString["option"].ToString());
            DateTime refdate = Convert.ToDateTime(Request.QueryString["refdate"].ToString());

            cond1 = Request.QueryString["cond1"].ToString();
            cond1 = Server.UrlDecode(cond1);

            if (option == 1)
            {
                bindDataCategory(sDataSource);
            }
            else if (option == 2)
            {
               bindDatabrand(sDataSource);
            }
            else if (option == 3)
            {
                bindData(sDataSource);
            }


        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

   protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
           // div1.Visible = true;
            divPrint.Visible = false;
            divPr.Visible = false;

            //Response.Write("<script language='javascript'> window.open('StockReport.aspx' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

   public void bindData(string sDataSource)
   {

       string trange = string.Empty;
       string toption = string.Empty;
       string Branch = Convert.ToString(Request.QueryString["Branch"].ToString());

       string connection = Request.Cookies["Company"].Value;

       DataSet ds = new DataSet();

       DataSet dst = new DataSet();
       DataSet dstt = new DataSet();

       DateTime refDate = Convert.ToDateTime(Request.QueryString["refdate"].ToString());

       int ttrange = Convert.ToInt32(Request.QueryString["range"].ToString());
       int ttoption = Convert.ToInt32(Request.QueryString["option"].ToString());

       cond1 = Request.QueryString["cond1"].ToString();
       cond1 = Server.UrlDecode(cond1);

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

               //foreach (ListItem listItem1 in lstPricelist.Items)
               //{
               //    if (listItem1.Selected)
               //    {
               //        string item1 = listItem1.Value;

               //        dt.Columns.Add(new DataColumn(item1));
               //    }
               //}


               char[] commaSeparator = new char[] { ',' };
               string[] result;
               result = cond1.Split(commaSeparator, StringSplitOptions.None);

               foreach (string str in result)
               {
                   dt.Columns.Add(new DataColumn(str));
               }
               dt.Columns.Remove("Column1");


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

                       dst = objBL.GetAbsoluteProductpricelist1(connection,sDataSource, itemcode, Branch);

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

                                   //foreach (ListItem listItem1 in lstPricelist.Items)
                                   //{
                                   //    if (listItem1.Selected)
                                   //    {
                                   //        string item1 = listItem1.Value;
                                   //        string item123 = Convert.ToString(drt["pricename"]);

                                   //        if (item123 == item1)
                                   //        {
                                   //            dr_final6[item1] = drt["price"];
                                   //        }

                                   //    }
                                   //}
                                   char[] commaSeparator2 = new char[] { ',' };
                                   string[] result2;
                                   result2 = cond1.Split(commaSeparator, StringSplitOptions.None);

                                   foreach (string str2 in result2)
                                   {
                                       string item1 = str2;
                                       string item123 = Convert.ToString(drt["pricename"]);

                                       if (item123 == item1)
                                       {
                                           dr_final6[item1] = drt["price"];
                                       }
                                   }
                               }
                           }
                       }


                       dr_final6["Stock Value"] = Convert.ToInt32(dr["Stock"]) * Convert.ToDouble(dr["Rate"]);
                       dt.Rows.Add(dr_final6);
                   }
                   dstt.Tables.Add(dt);
                  // ExportToExcel(dt);
                   Grdreport.Visible = true;
                   Grdreport.DataSource = dstt;
                   Grdreport.DataBind();
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
       string Branch = Convert.ToString(Request.QueryString["Branch"].ToString());
       int ttrange = Convert.ToInt32(Request.QueryString["range"].ToString());
       int ttoption = Convert.ToInt32(Request.QueryString["option"].ToString());
       DateTime refDate = Convert.ToDateTime(Request.QueryString["refdate"].ToString());

       //DateTime refDate = DateTime.Parse(txtStartDate.Text);

      // int ttrange = Convert.ToInt32(cmbtrange.SelectedItem.Value);
    //   int ttoption = Convert.ToInt32(cmbtoption.SelectedItem.Value);

     //  string Branch = drpBranchAdd.SelectedValue;

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


               //foreach (ListItem listItem1 in lstPricelist.Items)
               //{
               //    if (listItem1.Selected)
               //    {
               //        string item1 = listItem1.Value + " Value %";

               //        dt.Columns.Add(new DataColumn(item1));
               //    }
               //}
               char[] commaSeparator = new char[] { ',' };
               string[] result;
               result = cond1.Split(commaSeparator, StringSplitOptions.None);

               foreach (string str in result)
               {
                   string strr = str + " Value %";
                   dt.Columns.Add(new DataColumn(strr));
               }
               dt.Columns.Remove(" Value %");

               //dt.Columns.Add(new DataColumn("Stock Value %"));

             //  DataRow dr_final111 = dt.NewRow();
              // dt.Rows.Add(dr_final111);

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
                       dr_final6["Branchcode"] = Branch;


                       string connection = Request.Cookies["Company"].Value;

                       char[] commaSeparator2 = new char[] { ',' };
                       string[] result2;
                       result2 = cond1.Split(commaSeparator, StringSplitOptions.None);

                       foreach (string str2 in result2)
                       {
                           dst = objBL.GetAbsoluteCategorypricelist1(connection, sDataSource, Category, Branch, "Category", str2);

                           if (dst != null)
                           {
                               if (dst.Tables[0].Rows.Count > 0)
                               {
                                   foreach (DataRow drt in dst.Tables[0].Rows)
                                   {


                                       string item1 = str2 + " Value %";


                                       dr_final6[item1] = Convert.ToDouble(drt["price"]) / 100;



                                   }
                               }
                           }
                       }

                       //foreach (string str2 in result2)
                       //{
                       //    //if (str2)
                       //    //{
                       //    dst = objBL.GetAbsoluteCategorypricelist1(connection, sDataSource, brand, Branch, "Category", str2);

                       //    if (dst != null)
                       //    {
                       //        if (dst.Tables[0].Rows.Count > 0)
                       //        {
                       //            foreach (DataRow drt in dst.Tables[0].Rows)
                       //            {


                       //                string item1 = str2 + " Value %";
                       //                //string item123 = Convert.ToString(drt["pricename"]);

                       //                //if (item123 == item1)
                       //                //{
                       //                dr_final6[item1] = Convert.ToDouble(drt["price"]) / 100;
                       //                //}

                       //            }
                       //        }
                       //        //}
                       //    }
                       //}
                       



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

                   //DataTable dtt = dstt.Tables[0];
                   //DataSet dsttt = new DataSet();
                   //dsttt.Tables.Add(dtt);
                   // ExportToExcel(dt);
                   Grdreport.Visible = true;
                   Grdreport.DataSource = dstt;
                   Grdreport.DataBind();
                  // ExportToExcel(dtt);
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

       string Branch = Convert.ToString(Request.QueryString["Branch"].ToString());
       int ttrange = Convert.ToInt32(Request.QueryString["range"].ToString());
       int ttoption = Convert.ToInt32(Request.QueryString["option"].ToString());
       DateTime refDate = Convert.ToDateTime(Request.QueryString["refdate"].ToString());

       int tstock = 0;
       int trol = 0;

     //  string Branch = drpBranchAdd.SelectedValue;

       ds = objBL.getstocklevelbrand(sDataSource, refDate, trange, toption, Branch);

       DataSet dst = new DataSet();

       string itemcode = string.Empty;

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
               dt.Columns.Add(new DataColumn("Brand %"));
               dt.Columns.Add(new DataColumn("Stock"));

               dt.Columns.Add(new DataColumn("Branchcode"));
               //  dt.Columns.Add(new DataColumn("Stock Value"));

               //foreach (ListItem listItem1 in lstPricelist.Items)
               //{
               //    if (listItem1.Selected)
               //    {
               //        string item1 = listItem1.Value + " Value %";

               //        dt.Columns.Add(new DataColumn(item1));
               //    }
               //}


               cond1 = Request.QueryString["cond1"].ToString();
               cond1 = Server.UrlDecode(cond1);

               char[] commaSeparator = new char[] { ',' };
               string[] result;
               result = cond1.Split(commaSeparator, StringSplitOptions.None);

               foreach (string str in result)
               {
                   string strr = str + " Value %";

                   dt.Columns.Add(new DataColumn(strr));
               }
               dt.Columns.Remove(" Value %");

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
                       string connection = Request.Cookies["Company"].Value;

                       char[] commaSeparator2 = new char[] { ',' };
                       string[] result2;
                       result2 = cond1.Split(commaSeparator, StringSplitOptions.None);

                       foreach (string str2 in result2)
                       {
                           //if (str2)
                           //{
                           dst = objBL.GetAbsoluteCategorypricelist1(connection,sDataSource, brand, Branch, "brand", str2);

                               if (dst != null)
                               {
                                   if (dst.Tables[0].Rows.Count > 0)
                                   {
                                       foreach (DataRow drt in dst.Tables[0].Rows)
                                       {


                                           string item1 = str2 + " Value %";
                                           //string item123 = Convert.ToString(drt["pricename"]);

                                           //if (item123 == item1)
                                           //{
                                           dr_final6[item1] = Convert.ToDouble(drt["price"]) / 100;
                                           //}

                                       }
                                   }
                               //}
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
                   //DataTable dtt = dstt.Tables[0];
                   //DataSet dsttt = new DataSet();
                   //dsttt.Tables.Add(dtt);
                   // ExportToExcel(dt);
                   Grdreport.Visible = true;
                   Grdreport.DataSource = dstt;
                   Grdreport.DataBind();
                 //  ExportToExcel(dtt);
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

}