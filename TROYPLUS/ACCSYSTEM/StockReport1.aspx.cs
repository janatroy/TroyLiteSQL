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
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

public partial class StockReport1 : System.Web.UI.Page
{
    Double amtDbl = 0;
    Double sumDbl = 0;
    Double grandDbl = 0;
    private string sDataSource = string.Empty;
    private string Connection = string.Empty;
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
                                // lblTNGST.Text = Convert.ToString(dr["TINno"]);
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

                    DataSet ds = bl.getImageInfo();
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                Image1.ImageUrl = "App_Themes/NewTheme/images/" + ds.Tables[0].Rows[i]["img_filename"];
                                Image1.Height = 95;
                                Image1.Width = 95;
                            }
                        }
                        else
                        {
                            Image1.Height = 95;
                            Image1.Width = 95;
                            Image1.ImageUrl = "App_Themes/NewTheme/images/TESTLogo.png";
                        }
                    }
                }
            }

            lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtStartDate.Text = dtaa;

            //lblHeadDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());



            divPrint.Visible = true;
            divPr.Visible = true;


            //ReportsBL.ReportClass rptStock = new ReportsBL.ReportClass();
            //DataSet ds = rptStock.getCategory(sDataSource);
            //gvCategory.DataSource = ds;
            //gvCategory.DataBind();
            BindData();
            div1.Visible = false;
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
            div1.Visible = true;
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
            DateTime refDate = DateTime.Parse(txtStartDate.Text);

            BusinessLogic bl = new BusinessLogic(sDataSource);
            ds = bl.getProductsstock(sDataSource, refDate);
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
            //gvCategory.DataSource = ds;
            //gvCategory.DataBind();

            div1.Visible = false;

            BindData();
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
            sumDbl = 0;
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            //    string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblCatID = e.Row.FindControl("lblCategoryID") as Label;
                Label lblTotal = e.Row.FindControl("lblTotal") as Label;
                //Label lblCategory = e.Row.FindControl("lblCategory") as Label;
                Label lblGrand = e.Row.FindControl("lblGrandTotal") as Label;
                // int catID = Convert.ToInt32(lblCatID.Text);
                int catID = Convert.ToInt32(gvCategory.DataKeys[e.Row.RowIndex].Value);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();

                BusinessLogic bl = new BusinessLogic(sDataSource);

                DateTime refDate = DateTime.Parse(txtStartDate.Text);
                DateTime stdt = Convert.ToDateTime(txtStartDate.Text);

                //if (Request.QueryString["refDate"] != null)
                //{
                //    stdt = Convert.ToDateTime(Request.QueryString["refDate"].ToString());
                //    cond = Request.QueryString["cond"].ToString();
                //    cond = Server.UrlDecode(cond);
                //    cond1 = Request.QueryString["cond1"].ToString();
                //    cond1 = Server.UrlDecode(cond1);
                //    cond2 = Request.QueryString["cond2"].ToString();
                //    cond2 = Server.UrlDecode(cond2);
                //    cond3 = Request.QueryString["cond3"].ToString();
                //    cond3 = Server.UrlDecode(cond3);
                //    cond4 = Request.QueryString["cond4"].ToString();
                //    cond4 = Server.UrlDecode(cond4);
                //    cond5 = Request.QueryString["cond5"].ToString();
                //    cond5 = cond5.ToString();
                //    cond6 = Request.QueryString["cond6"].ToString();
                //    cond6 = cond6.ToString();
                //}
                refDate = Convert.ToDateTime(stdt);


                //DataSet dss = bl.getProducts(sDataSource, refDate, cond, cond1, cond2, cond3, cond4, "");
                //DataTable customerTable = dss.Tables[0];


                //ConvertToCrossTab(customerTable);
                //if (dss.Tables[0].Rows.Count > 0)
                //{
                //    gv.DataSource = dss;
                //    gv.DataBind();

                //}
                //else
                //{
                //    lblCategory.Text = "";
                //}
                lblTotal.Text = sumDbl.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private void BindData()
    {
        int overallvalue = 0;
        int overallstock = 0;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DateTime refDate = DateTime.Parse(txtStartDate.Text);
        DateTime stdt = Convert.ToDateTime(txtStartDate.Text);

        if (Request.QueryString["refDate"] != null)
        {
            stdt = Convert.ToDateTime(Request.QueryString["refDate"].ToString());
            lblHeadDate.Text = stdt.ToString("dd/MM/yyyy");

            cond = Request.QueryString["cond"].ToString();
            cond = Server.UrlDecode(cond);
            cond1 = Request.QueryString["cond1"].ToString();
            cond1 = Server.UrlDecode(cond1);

            //cond2 = Request.QueryString["cond2"].ToString();
            //cond2 = Server.UrlDecode(cond2);

            cond2 = cond.Replace("BranchCode", "S.BranchCode");
            cond3 = cond.Replace("BranchCode", "P.BranchCode");
            cond4 = cond.Replace("BranchCode", "SI.BranchCode");

            //cond3 = Request.QueryString["cond3"].ToString();
            //cond3 = Server.UrlDecode(cond3);
            //cond4 = Request.QueryString["cond4"].ToString();
            //cond4 = Server.UrlDecode(cond4);
            cond5 = Request.QueryString["cond5"].ToString();
            cond5 = cond5.ToString();
            cond6 = Request.QueryString["cond6"].ToString();
            cond6 = cond6.ToString();
        }
        refDate = Convert.ToDateTime(stdt);


        DataSet ds = bl.GetProductlist(sDataSource, cond);

        DataTable dt = new DataTable();


        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                overallvalue = 0;
                overallstock = 0;
                dt.Columns.Add(new DataColumn("Brand"));
                dt.Columns.Add(new DataColumn("ItemCode"));
                dt.Columns.Add(new DataColumn("ProductName"));            
                dt.Columns.Add(new DataColumn("Model"));
                dt.Columns.Add(new DataColumn("CategoryName"));
                //dt.Columns.Add(new DataColumn("Rol"));

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
                    dt.Columns.Add(new DataColumn(str1 + " - Value"));
                }

                dt.Columns.Add(new DataColumn("Overall Stock"));
                dt.Columns.Add(new DataColumn("Overall Value"));
                dt.Columns.Remove("Column1");
                dt.Columns.Remove(" - Value");

                DataRow dr_final123 = dt.NewRow();
                dt.Rows.Add(dr_final123);

                DataSet dst = new DataSet();

                string itemcode = "";

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    overallvalue = 0;
                    overallstock = 0;
                    itemcode = Convert.ToString(dr["itemcode"]);

                    dst = bl.getProducts(sDataSource, refDate, cond, cond1, cond2, cond3, cond4, itemcode);

                    DataRow dr_final6 = dt.NewRow();
                    dr_final6["Brand"] = dr["brand"];
                    dr_final6["ProductName"] = dr["ProductName"];
                    dr_final6["Model"] = dr["Model"];
                    dr_final6["ItemCode"] = dr["Itemcode"];
                    dr_final6["CategoryName"] = dr["CategoryName"];
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
                                        overallstock = overallstock + Convert.ToInt32(dr_final6[item11]);


                                        dr_final6[item1231 + " - Value"] = Convert.ToInt32(drt["price"]) * Convert.ToInt32(drt["Stock"]);
                                        overallvalue = overallvalue + Convert.ToInt32(dr_final6[item1231 + " - Value"]);
                                    }
                                }
                            }
                        }
                        dr_final6["Overall Stock"] = overallstock;
                        lblGrandStockTotal.Text = Convert.ToString(Convert.ToInt32(lblGrandStockTotal.Text) + Convert.ToInt32(dr_final6["Overall Stock"]));
                        dr_final6["Overall Value"] = overallvalue;
                        lblGrandValueTotal.Text = Convert.ToString(Convert.ToInt32(lblGrandValueTotal.Text) + Convert.ToInt32(dr_final6["Overall Value"]));
                    }

                    //dr_final6["Overall Value"] = tot;// Convert.ToInt32(dr_final6[result3 + " - Value"]) + Convert.ToInt32(dr_final6[result3 + " - Value"]);
                    dt.Rows.Add(dr_final6);
                }
                DataSet dst2 = new DataSet();
                dst2.Tables.Add(dt);
                ReportGridView1.DataSource = dst2;
                ReportGridView1.DataBind();

            }
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label amt = e.Row.FindControl("lblAmount") as Label;
                Label rate = e.Row.FindControl("lblRate") as Label;

                Label Stock = e.Row.FindControl("lblStock") as Label;

                amtDbl = Double.Parse(rate.Text) * Double.Parse(Stock.Text);
                amt.Text = amtDbl.ToString("f2");


                sumDbl = sumDbl + amtDbl;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label labelTotal = e.Row.FindControl("lblTotal") as Label;
                labelTotal.Text = Convert.ToString(sumDbl);
                grandDbl = grandDbl + sumDbl;
                lblGrandValueTotal.Text = grandDbl.ToString("#0.00");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ReportGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int colCount = e.Row.Cells.Count;

            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;

            for (int i = 5; i <= colCount - 1; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            int colCount = e.Row.Cells.Count - 1;
            int totstock = colCount - 1;

            e.Row.Cells[totstock].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[colCount].HorizontalAlign = HorizontalAlign.Right;

            e.Row.Cells[totstock].Text = lblGrandStockTotal.Text;
            e.Row.Cells[colCount].Text = lblGrandValueTotal.Text;
        }
    }
}
