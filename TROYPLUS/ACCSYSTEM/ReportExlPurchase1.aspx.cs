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


public partial class ReportExlPurchase1 : System.Web.UI.Page
{
     private string sDataSource = string.Empty;
    private string Connection = string.Empty;
    BusinessLogic objBL = new BusinessLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            Connection = Request.Cookies["Company"].Value;
            if (!IsPostBack)
            {

                lblHeading.Text = " Purchase Comprehency Report";

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
                    DataSet ds1 = bl.getImageInfo();
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            {
                                Image1.ImageUrl = "App_Themes/NewTheme/images/" + ds1.Tables[0].Rows[i]["img_filename"];
                                Image1.Height = 95;
                                Image1.Width = 114;
                            }
                        }
                        else
                        {
                            Image1.Height = 95;
                            Image1.Width = 114;
                            Image1.ImageUrl = "App_Themes/NewTheme/images/TESTLogo.png";
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



            divPrint.Visible = true;
            divPr.Visible = true;
            DataSet dstt = new DataSet();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
           // DateTime startDate, endDate;
            double brandTotal = 0;
            double catTotal = 0;
            double total = 0;
            double producttot = 0;
            string fLvlValueTemp = string.Empty;
            string fLvlValue = string.Empty;
            string tLvlValueTemp = string.Empty;
            string tLvlValue = string.Empty;
            string sLvlValueTemp = string.Empty;
            string sLvlValue = string.Empty;
            string connection = Request.Cookies["Company"].Value;

            string brand = Convert.ToString(Request.QueryString["brand"].ToString());
            string Category = Convert.ToString(Request.QueryString["category"].ToString());
            string product = Convert.ToString(Request.QueryString["product"].ToString());
            string Branch = Convert.ToString(Request.QueryString["Branch"].ToString());

            DateTime startDate = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
            DateTime endDate = Convert.ToDateTime(Request.QueryString["enddate"].ToString());


            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            dt.Columns.Add(new DataColumn("Category"));
            dt.Columns.Add(new DataColumn("Brand"));
            dt.Columns.Add(new DataColumn("Product Name"));
            dt.Columns.Add(new DataColumn("Model"));
            dt.Columns.Add(new DataColumn("Branchcode"));
            dt.Columns.Add(new DataColumn("Qty"));
            dt.Columns.Add(new DataColumn("Rate"));
            dt.Columns.Add(new DataColumn("Amount"));

            ds = objBL.getPurchasereport(startDate, endDate, Category, brand, product, Branch);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                    tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                       (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                       (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final889 = dt.NewRow();
                        dt.Rows.Add(dr_final889);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "";
                        dr_final8["Brand"] = "";
                        dr_final8["Product Name"] = "Total : " + tLvlValue;
                        dr_final8["Model"] = "";
                        dr_final8["Qty"] = "";
                        dr_final8["Rate"] = "";
                        dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(producttot));
                        producttot = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "";
                        dr_final8["Brand"] = "Total : " + sLvlValue;
                        dr_final8["Product Name"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Qty"] = "";
                        dr_final8["Rate"] = "";
                        dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                        brandTotal = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        //DataRow dr_final889 = dt.NewRow();
                        //dt.Rows.Add(dr_final889);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "Total : " + fLvlValue;
                        dr_final8["Brand"] = "";
                        dr_final8["Product Name"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Qty"] = "";
                        dr_final8["Rate"] = "";
                        dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(catTotal));
                        catTotal = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Category"] = dr["Categoryname"];
                    dr_final12["Brand"] = dr["Brand"];
                    dr_final12["Branchcode"] = dr["Branchcode"];
                    dr_final12["Product Name"] = dr["ProductName"];
                    dr_final12["Model"] = dr["model"];
                    dr_final12["Qty"] = dr["Qty"];
                    dr_final12["Rate"] = dr["purchaseRate"];
                    dr_final12["Amount"] = Convert.ToDouble(dr["Amount"]);
                    brandTotal = brandTotal + (Convert.ToDouble(dr["Amount"]));
                    catTotal = catTotal + (Convert.ToDouble(dr["Amount"]));
                    producttot = producttot + (Convert.ToDouble(dr["Amount"]));
                    total = total + (Convert.ToDouble(dr["Amount"]));
                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final88 = dt.NewRow();
            dr_final88["Category"] = "";
            dr_final88["Brand"] = "";
            dr_final88["Product Name"] = "Total : " + tLvlValueTemp;
            dr_final88["Model"] = "";
            dr_final88["Qty"] = "";
            dr_final88["Rate"] = "";
            dr_final88["Amount"] = Convert.ToString(Convert.ToDecimal(producttot));
            dt.Rows.Add(dr_final88);

            DataRow dr_final79 = dt.NewRow();
            dt.Rows.Add(dr_final79);

            DataRow dr_final89 = dt.NewRow();
            dr_final88["Category"] = "";
            dr_final89["Brand"] = "Total : " + sLvlValueTemp;
            dr_final89["Product Name"] = "";
            dr_final89["Model"] = "";
            dr_final89["Qty"] = "";
            dr_final89["Rate"] = "";
            dr_final89["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final869 = dt.NewRow();
            dr_final869["Category"] = "Total : " + fLvlValueTemp;
            dr_final869["Brand"] = "";
            dr_final869["Product Name"] = "";
            dr_final869["Model"] = "";
            dr_final869["Qty"] = "";
            dr_final869["Rate"] = "";
            dr_final869["Amount"] = Convert.ToString(Convert.ToDecimal(catTotal));
            dt.Rows.Add(dr_final869);

            DataRow dr_final9 = dt.NewRow();
            dt.Rows.Add(dr_final9);

            DataRow dr_final789 = dt.NewRow();
            dr_final88["Category"] = "";
            dr_final789["Brand"] = "Grand Total : ";
            dr_final789["Product Name"] = "";
            dr_final789["Model"] = "";
            dr_final789["Qty"] = "";
            dr_final789["Rate"] = "";
            dr_final789["Amount"] = Convert.ToString(Convert.ToDecimal(total));
            dt.Rows.Add(dr_final789);

            if (ds.Tables[0].Rows.Count > 0)
            {
               // ExportToExcel(dt);
                dstt.Tables.Add(dt);
                ReportsBL.ReportClass rptStock = new ReportsBL.ReportClass();
                // DataSet ds = new DataSet(sDataSource);
                //  DataSet ds = rptStock.getCategory(sDataSource);
                Grdreport.Visible = true;
                Grdreport.DataSource = dstt;
                Grdreport.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
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

    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataSet ddd = new DataSet();
            // DateTime refDate = DateTime.Parse(txtStartDate.Text);

            BusinessLogic bl = new BusinessLogic(sDataSource);
            //    ds = bl.getProductsstock(sDataSource, refDate);
            double Amount = 0;

            DataTable dt = new DataTable();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("Category"));
                dt.Columns.Add(new DataColumn("Brand"));
                dt.Columns.Add(new DataColumn("Product Name"));
                dt.Columns.Add(new DataColumn("Item Code"));
                dt.Columns.Add(new DataColumn("Model"));
                dt.Columns.Add(new DataColumn("Qty"));
                dt.Columns.Add(new DataColumn("Rate"));
                dt.Columns.Add(new DataColumn("Amount"));

                DataRow dr_final113 = dt.NewRow();
                dt.Rows.Add(dr_final113);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_final122 = dt.NewRow();
                    dr_final122["Category"] = dr["CategoryName"].ToString();
                    dr_final122["Brand"] = dr["productdesc"].ToString();
                    dr_final122["Product Name"] = dr["ProductName"].ToString();
                    dr_final122["Item Code"] = dr["ItemCode"].ToString();
                    dr_final122["Model"] = dr["Model"].ToString();
                    dr_final122["Qty"] = Convert.ToDouble(dr["Stock"]);
                    dr_final122["Rate"] = Convert.ToDouble(dr["Rate"]);
                    dr_final122["Amount"] = Convert.ToDouble(dr["Stock"]) * Convert.ToDouble(dr["Rate"]);
                    Amount = Amount + (Convert.ToDouble(dr["Stock"]) * Convert.ToDouble(dr["Rate"]));
                    dt.Rows.Add(dr_final122);
                }

                DataRow dr_final12213 = dt.NewRow();
                dr_final12213["Category"] = "";
                dr_final12213["Brand"] = "";
                dr_final12213["Product Name"] = "";
                dr_final12213["Item Code"] = "";
                dr_final12213["Model"] = "";
                dr_final12213["Qty"] = "";
                dr_final12213["Rate"] = "";
                dr_final12213["Amount"] = "";
                dt.Rows.Add(dr_final12213);

                DataRow dr_final123 = dt.NewRow();
                dr_final12213["Category"] = "";
                dr_final123["Product Name"] = "";
                dr_final123["Brand"] = "";
                dr_final123["Item Code"] = "";
                dr_final123["Model"] = "";
                dr_final123["Qty"] = "";
                dr_final123["Rate"] = "";
                dr_final123["Amount"] = Amount;
                dt.Rows.Add(dr_final123);

                ExportToExcel(dt);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            string filename = "Stock Report.xls";
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            divPrint.Visible = true;
            divPr.Visible = true;
            //ReportsBL.ReportClass rptStock = new ReportsBL.ReportClass();
            //DataSet ds = rptStock.getCategory(sDataSource);
            //Grdreport.DataSource = ds;
            //Grdreport.DataBind();

            //  div1.Visible = false;

            // BindData();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    string cond;
    string cond1;
    string cond2;
    string cond3;
    string cond4;
    string cond5;
    string cond6;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            // sumDbl = 0;
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            //    string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();

                BusinessLogic bl = new BusinessLogic(sDataSource);





                cond1 = Request.QueryString["cond1"].ToString();
                cond1 = Server.UrlDecode(cond1);





                DataSet ds = bl.GetProductlist(sDataSource, cond);

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

                        char[] commaSeparator = new char[] { ',' };
                        string[] result;
                        result = cond6.Split(commaSeparator, StringSplitOptions.None);

                        foreach (string str in result)
                        {
                            dt.Columns.Add(new DataColumn(str));
                        }
                        dt.Columns.Remove("Column1");

                        char[] commaSeparator1 = new char[] { ',' };
                        string[] result1;
                        result1 = cond5.Split(commaSeparator, StringSplitOptions.None);

                        foreach (string str1 in result1)
                        {
                            dt.Columns.Add(new DataColumn(str1));
                        }
                        dt.Columns.Remove("Column1");
                        DataRow dr_final123 = dt.NewRow();
                        dt.Rows.Add(dr_final123);

                        DataSet dst = new DataSet();

                        string itemcode = "";

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            itemcode = Convert.ToString(dr["itemcode"]);

                            // dst = bl.getProducts(sDataSource, refDate, cond, cond1, cond2, cond3, cond4, itemcode);

                            DataRow dr_final6 = dt.NewRow();
                            dr_final6["Brand"] = dr["brand"];
                            dr_final6["ProductName"] = dr["ProductName"];
                            dr_final6["Model"] = dr["Model"];
                            dr_final6["ItemCode"] = dr["Itemcode"];

                            if (dst != null)
                            {
                                if (dst.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow drt in dst.Tables[0].Rows)
                                    {
                                        char[] commaSeparator2 = new char[] { ',' };
                                        string[] result2;
                                        result2 = cond6.Split(commaSeparator, StringSplitOptions.None);

                                        foreach (string str2 in result2)
                                        {
                                            string item1 = str2;
                                            string item123 = Convert.ToString(drt["pricename"]);

                                            if (item123 == item1)
                                            {
                                                dr_final6[item1] = drt["price"];
                                            }
                                        }


                                        char[] commaSeparator3 = new char[] { ',' };
                                        string[] result3;
                                        result3 = cond5.Split(commaSeparator, StringSplitOptions.None);

                                        foreach (string str3 in result3)
                                        {
                                            string item11 = str3;
                                            string item1231 = Convert.ToString(drt["BranchCode"]);

                                            if (item1231 == item11)
                                            {
                                                dr_final6[item11] = drt["Stock"];
                                            }
                                        }


                                    }
                                }
                            }
                            dt.Rows.Add(dr_final6);
                        }
                        DataSet dst2 = new DataSet();
                        dst2.Tables.Add(dt);
                       

                    }
                }

             
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}