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
using System.IO;
using System.Data;
using ClosedXML.Excel;


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

                loadBranch();
                loadPriceList();
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

   protected void btnxls_Click(object sender, EventArgs e)
   {
        try
        {
            if (drpBranchAdd.SelectedValue =="0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Please Select Branch. It cannot be Left blank.');", true);

                //ModalPopupExtender1.Show();
                form1.Visible = true;
                return;
            }
            else
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

         //if (chkboxMRP.Checked == true)
         //{
         //    field += ",rate";
         //}
         //if (chkboxDP.Checked == true)
         //{
         //    field += ",DealerRate";
         //}
         //if (chkboxNLC.Checked == true)
         //{
         //    field += ",NLC";
         //}

         string Branch = drpBranchAdd.SelectedValue;

         ds = objBL.GetAbsoluteProductlist(sDataSource, field, Method, Branch);

         DataTable dt = new DataTable("adsolute report");

         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                  dt.Columns.Add(new DataColumn("Brand"));
                  dt.Columns.Add(new DataColumn("ProductName"));
                  dt.Columns.Add(new DataColumn("ItemCode"));
                  dt.Columns.Add(new DataColumn("Model"));
                  dt.Columns.Add(new DataColumn("Rol"));
                  dt.Columns.Add(new DataColumn("Stock"));
                  dt.Columns.Add(new DataColumn("Branchcode"));

                  foreach (ListItem listItem1 in lstPricelist.Items)
                  {
                      if (listItem1.Selected)
                      {
                          string item1 = listItem1.Value;

                          dt.Columns.Add(new DataColumn(item1));
                      }
                  }

                  //if (chkboxMRP.Checked == true)
                  //{
                  //    dt.Columns.Add(new DataColumn("MRP"));
                  //    dt.Columns.Add(new DataColumn("MRPEffDate"));
                  //}
                  //if (chkboxDP.Checked == true)
                  //{
                  //    dt.Columns.Add(new DataColumn("DP"));
                  //    dt.Columns.Add(new DataColumn("DPEffDate"));
                  //}
                  //if (chkboxNLC.Checked == true)
                  //{
                  //    dt.Columns.Add(new DataColumn("NLC"));
                  //    dt.Columns.Add(new DataColumn("NLCEffDate"));
                  //}

                  DataRow dr_final123 = dt.NewRow();
                  dt.Rows.Add(dr_final123);

                  DataSet dst=new DataSet();

                  string itemcode = "";

                  foreach (DataRow dr in ds.Tables[0].Rows)
                  {
                      itemcode = Convert.ToString(dr["itemcode"]);

                      dst = objBL.GetAbsoluteProductpricelist(sDataSource, itemcode, Branch);


                          DataRow dr_final6 = dt.NewRow();
                          dr_final6["Brand"] = dr["brand"];
                          dr_final6["ProductName"] = dr["ProductName"];
                          dr_final6["Model"] = dr["Model"];
                          dr_final6["ItemCode"] = dr["Itemcode"];

                          dr_final6["Rol"] = dr["Rol"];
                          

                          if (dst != null)
                          {
                              if (dst.Tables[0].Rows.Count > 0)
                              {
                                  foreach (DataRow drt in dst.Tables[0].Rows)
                                  {
                                      dr_final6["Stock"] = drt["Stock"];
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

                          //if (chkboxMRP.Checked == true)
                          //{
                          //    dr_final6["MRP"] = dr["Rate"];
                          //    dr_final6["MRPEffDate"] = dr["MRPEffDate"];
                          //}
                          //if (chkboxDP.Checked == true)
                          //{
                          //    dr_final6["DP"] = dr["DealerRate"];
                          //    dr_final6["DPEffDate"] = dr["DPEffDate"];
                          //}
                          //if (chkboxNLC.Checked == true)
                          //{
                          //    dr_final6["NLC"] = dr["NLC"];
                          //    dr_final6["NLCEffDate"] = dr["NLCEffDate"];
                          //}
                          dt.Rows.Add(dr_final6);
                    
                  }

                  //DataSet dst = new DataSet();
                  //dst.Tables.Add(dt);

                  //DataTable dtt = new DataTable();
                  //dtt.Columns.Add(new DataColumn("Brand"));
                  //dtt.Columns.Add(new DataColumn("ProductName"));
                  //dtt.Columns.Add(new DataColumn("ItemCode"));
                  //dtt.Columns.Add(new DataColumn("Model"));
                  //dtt.Columns.Add(new DataColumn("Rol"));

                  //foreach (ListItem listItem1 in lstPricelist.Items)
                  //{
                  //    if (listItem1.Selected)
                  //    {
                  //        string item1 = listItem1.Value;

                  //        dtt.Columns.Add(new DataColumn(item1));
                  //    }
                  //}


                  //for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                  //{
                  //    DataRow dr_final6 = dt.NewRow();
                  //    dr_final6["Brand"] = dst.Tables[0].Rows[i]["brand"].ToString();
                  //    dr_final6["ProductName"] = dst.Tables[0].Rows[i]["ProductName"].ToString();
                  //    dr_final6["Model"] = dst.Tables[0].Rows[i]["Model"].ToString();
                  //    dr_final6["ItemCode"] = dst.Tables[0].Rows[i]["Itemcode"].ToString();
                  //    dr_final6["Rol"] = dst.Tables[0].Rows[i]["Rol"].ToString();

                  //    if (itemcode == dst.Tables[0].Rows[i]["Itemcode"].ToString())
                  //    {
                  //        dst.Tables[0].Rows[i].BeginEdit();
                  //        double val = (double.Parse(ds.Tables[0].Rows[i]["PendingAmount"].ToString()) - double.Parse(billAmount));
                  //        dst.Tables[0].Rows[i]["PendingAmount"] = val;
                  //        dst.Tables[0].Rows[i].EndEdit();

                  //        if (val == 0.0)
                  //            dst.Tables[0].Rows[i].Delete();
                  //    }
                  //    else
                  //    {

                  //    }

                  //    itemcode = dst.Tables[0].Rows[i]["Itemcode"].ToString();
                  //}
                  //dst.Tables[0].AcceptChanges();

                  //gvLedger.Visible = true;
                  //DataSet dstt = new DataSet();
                  //dstt.Tables.Add(dt);
                  //gvLedger.DataSource = dstt;
                  //gvLedger.DataBind();

                  ExportToExcel(dt);

                  //DataSet dstt = new DataSet();
                  //dstt.Tables.Add(dt);
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
                 string filename = "Absolete report.xlsx";
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


     public void bindDatareport(string sDataSource)
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

         //if (chkboxMRP.Checked == true)
         //{
         //    field += ",rate";
         //}
         //if (chkboxDP.Checked == true)
         //{
         //    field += ",DealerRate";
         //}
         //if (chkboxNLC.Checked == true)
         //{
         //    field += ",NLC";
         //}

         string Branch = drpBranchAdd.SelectedValue;

         ds = objBL.GetAbsoluteProductlist(sDataSource, field, Method, Branch);

         DataTable dt = new DataTable();

         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                 dt.Columns.Add(new DataColumn("Brand"));
                 dt.Columns.Add(new DataColumn("ProductName"));
                 dt.Columns.Add(new DataColumn("ItemCode"));
                 dt.Columns.Add(new DataColumn("Model"));
                 dt.Columns.Add(new DataColumn("Rol"));
                 dt.Columns.Add(new DataColumn("Stock"));
                 dt.Columns.Add(new DataColumn("Branchcode"));

                 foreach (ListItem listItem1 in lstPricelist.Items)
                 {
                     if (listItem1.Selected)
                     {
                         string item1 = listItem1.Value;

                         dt.Columns.Add(new DataColumn(item1));
                     }
                 }

                 //if (chkboxMRP.Checked == true)
                 //{
                 //    dt.Columns.Add(new DataColumn("MRP"));
                 //    dt.Columns.Add(new DataColumn("MRPEffDate"));
                 //}
                 //if (chkboxDP.Checked == true)
                 //{
                 //    dt.Columns.Add(new DataColumn("DP"));
                 //    dt.Columns.Add(new DataColumn("DPEffDate"));
                 //}
                 //if (chkboxNLC.Checked == true)
                 //{
                 //    dt.Columns.Add(new DataColumn("NLC"));
                 //    dt.Columns.Add(new DataColumn("NLCEffDate"));
                 //}

                 DataRow dr_final123 = dt.NewRow();
                 dt.Rows.Add(dr_final123);

                 DataSet dst = new DataSet();

                 string itemcode = "";

                 foreach (DataRow dr in ds.Tables[0].Rows)
                 {
                     itemcode = Convert.ToString(dr["itemcode"]);

                     dst = objBL.GetAbsoluteProductpricelist(sDataSource, itemcode, Branch);


                     DataRow dr_final6 = dt.NewRow();
                     dr_final6["Brand"] = dr["brand"];
                     dr_final6["ProductName"] = dr["ProductName"];
                     dr_final6["Model"] = dr["Model"];
                     dr_final6["ItemCode"] = dr["Itemcode"];

                     dr_final6["Rol"] = dr["Rol"];


                     if (dst != null)
                     {
                         if (dst.Tables[0].Rows.Count > 0)
                         {
                             foreach (DataRow drt in dst.Tables[0].Rows)
                             {
                                 dr_final6["Stock"] = drt["Stock"];
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

                     //if (chkboxMRP.Checked == true)
                     //{
                     //    dr_final6["MRP"] = dr["Rate"];
                     //    dr_final6["MRPEffDate"] = dr["MRPEffDate"];
                     //}
                     //if (chkboxDP.Checked == true)
                     //{
                     //    dr_final6["DP"] = dr["DealerRate"];
                     //    dr_final6["DPEffDate"] = dr["DPEffDate"];
                     //}
                     //if (chkboxNLC.Checked == true)
                     //{
                     //    dr_final6["NLC"] = dr["NLC"];
                     //    dr_final6["NLCEffDate"] = dr["NLCEffDate"];
                     //}
                     dt.Rows.Add(dr_final6);

                 }

                 //DataSet dst = new DataSet();
                 //dst.Tables.Add(dt);

                 //DataTable dtt = new DataTable();
                 //dtt.Columns.Add(new DataColumn("Brand"));
                 //dtt.Columns.Add(new DataColumn("ProductName"));
                 //dtt.Columns.Add(new DataColumn("ItemCode"));
                 //dtt.Columns.Add(new DataColumn("Model"));
                 //dtt.Columns.Add(new DataColumn("Rol"));

                 //foreach (ListItem listItem1 in lstPricelist.Items)
                 //{
                 //    if (listItem1.Selected)
                 //    {
                 //        string item1 = listItem1.Value;

                 //        dtt.Columns.Add(new DataColumn(item1));
                 //    }
                 //}


                 //for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                 //{
                 //    DataRow dr_final6 = dt.NewRow();
                 //    dr_final6["Brand"] = dst.Tables[0].Rows[i]["brand"].ToString();
                 //    dr_final6["ProductName"] = dst.Tables[0].Rows[i]["ProductName"].ToString();
                 //    dr_final6["Model"] = dst.Tables[0].Rows[i]["Model"].ToString();
                 //    dr_final6["ItemCode"] = dst.Tables[0].Rows[i]["Itemcode"].ToString();
                 //    dr_final6["Rol"] = dst.Tables[0].Rows[i]["Rol"].ToString();

                 //    if (itemcode == dst.Tables[0].Rows[i]["Itemcode"].ToString())
                 //    {
                 //        dst.Tables[0].Rows[i].BeginEdit();
                 //        double val = (double.Parse(ds.Tables[0].Rows[i]["PendingAmount"].ToString()) - double.Parse(billAmount));
                 //        dst.Tables[0].Rows[i]["PendingAmount"] = val;
                 //        dst.Tables[0].Rows[i].EndEdit();

                 //        if (val == 0.0)
                 //            dst.Tables[0].Rows[i].Delete();
                 //    }
                 //    else
                 //    {

                 //    }

                 //    itemcode = dst.Tables[0].Rows[i]["Itemcode"].ToString();
                 //}
                 //dst.Tables[0].AcceptChanges();

                 //gvLedger.Visible = true;
                 //DataSet dstt = new DataSet();
                 //dstt.Tables.Add(dt);
                 //gvLedger.DataSource = dstt;
                 //gvLedger.DataBind();

               //  ExportToExcel(dt);

                 DataSet dstt = new DataSet();
                 dstt.Tables.Add(dt);
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

     protected void btnReport_Click(object sender, EventArgs e)
     {
         try
         {
         if (drpBranchAdd.SelectedValue == "0")
         {
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Please Select Branch. It cannot be Left blank.');", true);

             //ModalPopupExtender1.Show();
             form1.Visible = true;
             return;
         }
         else
         {
             bindDatareport(sDataSource);
             DataSet dstt = new DataSet();

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

             string Branch = drpBranchAdd.SelectedValue;

             string cond1 = "";
             cond1 = getCond1();

             Response.Write("<script language='javascript'> window.open('ReportXLAbsolute1.aspx?Branch=" + Branch + "&cond1=" + Server.UrlEncode(cond1) + "&Method=" + Method + "' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");

         }
     }
         catch (Exception ex)
         {
             TroyLiteExceptionManager.HandleException(ex);
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
    
}