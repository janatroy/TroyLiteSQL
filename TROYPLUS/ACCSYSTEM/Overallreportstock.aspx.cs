using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class Overallreportstock : System.Web.UI.Page
{
    Double amtDbl = 0;
    Double sumDbl = 0;
    Double grandDbl = 0;
    private string sDataSource = string.Empty;
    private string connection = string.Empty;
    string brncode;
    string usernam;
    protected void Page_Load(object sender, EventArgs e)
    {

        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        connection = Request.Cookies["Company"].Value;
        try
        {
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
                loadBranch();
                BranchEnable_Disable();
                loadPriceList();

                txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


            //DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            //string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            //txtStartDate.Text = dtaa;

            lblHeadDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        lstBranch.Items.Clear();

        brncode = Request.Cookies["Branch"].Value;
        if (brncode == "All")
        {
            ds = bl.ListBranch();
            lstBranch.Items.Add(new ListItem("All", "0"));
        }
        else
        {
            ds = bl.ListDefaultBranch(brncode);
        }
        lstBranch.DataSource = ds;
        lstBranch.DataTextField = "BranchName";
        lstBranch.DataValueField = "Branchcode";
        lstBranch.DataBind();
    }

    private void BranchEnable_Disable()
    {
        string sCustomer = string.Empty;
        connection = Request.Cookies["Company"].Value;
        usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        lstBranch.ClearSelection();
        ListItem li = lstBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        if (li != null) li.Selected = true;

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


    protected string getCond()
    {
        string cond = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond += " BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond = cond.TrimEnd(',');
        cond = cond.Replace(",", "or");
        return cond;
    }

    protected string getCond1()
    {
        string cond1 = "";
        foreach (ListItem listItem1 in lstPricelist.Items)
        {
            if (listItem1.Text != "All")
            {
                if (listItem1.Selected)
                {
                    cond1 += "  tblPriceList.PriceName='" + listItem1.Value + "' ,";
                }
            }
        }
        cond1 = cond1.TrimEnd(',');
        cond1 = cond1.Replace(",", "or");
        return cond1;
    }

    protected string getCond2()
    {
        string cond2 = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond2 += " S.BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond2 = cond2.TrimEnd(',');
        cond2 = cond2.Replace(",", "or");
        return cond2;
    }

    protected string getCond3()
    {
        string cond3 = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond3 += " P.BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond3 = cond3.TrimEnd(',');
        cond3 = cond3.Replace(",", "or");
        return cond3;
    }

    protected string getCond4()
    {
        string cond4 = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond4 += " SI.BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond4 = cond4.TrimEnd(',');
        cond4 = cond4.Replace(",", "or");
        return cond4;
    }


    protected string getCond5()
    {
        string cond5 = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond5 += listItem.Value + ",";
                }
            }
        }

        return cond5;
    }


    protected string getCond6()
    {
        string cond6 = "";
        foreach (ListItem listItem1 in lstPricelist.Items)
        {
            if (listItem1.Text != "All")
            {
                if (listItem1.Selected)
                {
                    cond6 += listItem1.Value + ",";
                }
            }
        }

        return cond6;
    }

    protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
            div1.Visible = true;
            divPrint.Visible = false;
            divPr.Visible = false;
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
       
   
    protected void btnxls_Click(object sender, EventArgs e)
    {
       


        
      //  int overallvalue = 0;
      //  int overallstock = 0;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DateTime refDate = DateTime.Parse(txtStartDate.Text);
        DateTime stdt = Convert.ToDateTime(txtStartDate.Text);

       // string cond;
        cond = getCond();
      //  string cond1;
        cond1 = getCond1();
       // string cond2;
        cond2 = getCond2();
       // string cond3;
        cond3 = getCond3();
       // string cond4;
        cond4 = getCond4();
       // string cond5;
        cond5 = getCond5();
       // string cond6;
        cond6 = getCond6();

        //if (Request.QueryString["refDate"] != null)
        //{
        //    stdt = Convert.ToDateTime(Request.QueryString["refDate"].ToString());
        //    lblHeadDate.Text = stdt.ToString("dd/MM/yyyy");


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

        lblbranch.Text = cond;


        DataSet ds = bl.GetProductlist1(sDataSource, cond);

        DataTable dt = new DataTable("Stock details");
        double openingStock = 0;

        if (ds != null)
        //  Model,tblProductMaster.CategoryID,tblCategories.CategoryName
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
              //  overallvalue = 0;
               // overallstock = 0;
                dt.Columns.Add(new DataColumn("ItemCode"));
                // dt.Columns.Add(new DataColumn("Model"));
                //  dt.Columns.Add(new DataColumn("CategoryName"));
                dt.Columns.Add(new DataColumn("openingStock"));
                dt.Columns.Add(new DataColumn("purchaseStock"));
                dt.Columns.Add(new DataColumn("salestock"));
                dt.Columns.Add(new DataColumn("Currentcalculatedstock"));
                dt.Columns.Add(new DataColumn("CurrentPhysicalstock"));


                DataSet dst = new DataSet();

                string itemcode = "";
                double opstock;
                double purstock;
                double salstock;
                double ovstock;
                string product = "";


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // overallvalue = 0;getoverallStockforallreport
                    // overallstock = 0;
                    itemcode = Convert.ToString(dr["itemcode"]);

                    product = Convert.ToString(dr["itemcode"]) + " - " + Convert.ToString(dr["Model"]) + " - " + Convert.ToString(dr["CategoryName"]);
                    //  model = Convert.ToString(dr["itemcode"]);
                    // category = Convert.ToString(dr["itemcode"]);
                    opstock = Convert.ToDouble(bl.getOpeningStockforallreport(sDataSource, itemcode, cond));
                    purstock = Convert.ToDouble(bl.getOpeningStockPurchaseforallreport(sDataSource, itemcode, stdt, cond3));
                    salstock = Convert.ToDouble(bl.getOpeningStockSalesforallreport(sDataSource, itemcode, stdt, cond2));
                    ovstock = Convert.ToDouble(bl.getoverallStockforallreport(sDataSource, itemcode, cond));

                    openingStock = opstock + purstock - salstock;

                   // openingStock = Convert.ToDouble(bl.getOpeningStockforallreport(sDataSource, itemcode, cond)) +
                 //  (Convert.ToDouble(bl.getOpeningStockPurchaseforallreport(sDataSource, itemcode, stdt, cond3)) - Convert.ToDouble(bl.getOpeningStockSalesforallreport(sDataSource, itemcode, stdt, cond4)));



                    DataRow dr_final6 = dt.NewRow();
                    dr_final6["ItemCode"] = product;
                    dr_final6["openingStock"] = opstock;
                    dr_final6["purchaseStock"] = purstock;
                    dr_final6["salestock"] = salstock;
                    dr_final6["Currentcalculatedstock"] = openingStock;
                    dr_final6["CurrentPhysicalstock"] = ovstock;

                    dt.Rows.Add(dr_final6);


                }
                //DataSet dst2 = new DataSet();
                //dst2.Tables.Add(dt);
                //gvCategory.DataSource = dst2;
                //int cc = dst2.Tables[0].Rows.Count;
                ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Please Select Branch. It cannot be Left blank.');", true);
                //gvCategory.DataBind();

                ExportToExcel(dt);

            }
        }
    }


    public void ExportToExcel(DataTable dt)
    {
       // ExportToExcel(dt);

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                string filename = "Stock Transaction BranchWise Summary Report.xlsx";
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstBranch.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select any one branch.Not to select All.')", true);
            }
            //else if (lstPricelist.SelectedIndex == -1)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select any PriceList')", true);
            //}
           // else
            {
                DateTime refDate = DateTime.Parse(txtStartDate.Text);

                string cond = "";
                cond = getCond();
                string cond1 = "";
                cond1 = getCond1();
                string cond2 = "";
                cond2 = getCond2();
                string cond3 = "";
                cond3 = getCond3();
                string cond4 = "";
                cond4 = getCond4();
                string cond5 = "";
                cond5 = getCond5();
                string cond6 = "";
                cond6 = getCond6();
                //Response.Write("<script language='javascript'> window.open('StockReport1.aspx?refDate=" + refDate + "&cond=" + Server.UrlEncode(cond) + "&cond1=" + Server.UrlEncode(cond1) + "' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                Response.Write("<script language='javascript'> window.open('Overallreportstock1.aspx?refDate=" + refDate + "&cond=" + Server.UrlEncode(cond) + "&cond1=" + Server.UrlEncode(cond1) + "&cond2=" + Server.UrlEncode(cond2) + "&cond3=" + Server.UrlEncode(cond3) + "&cond4=" + Server.UrlEncode(cond4) + "&cond5=" + cond5 + "&cond6=" + cond6 + "' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

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
                string cond = "";
                cond = getCond();
                string cond1 = "";
                cond1 = getCond1();
                string cond2 = "";
                cond2 = getCond2();
                string cond3 = "";
                cond3 = getCond3();
                string cond4 = "";
                cond4 = getCond4();
                DataSet ds = bl.getProducts(sDataSource, refDate, cond, cond1, cond2, cond3, cond4, "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                else
                {
                    //lblCategory.Text = "";
                }
                lblTotal.Text = sumDbl.ToString("f2");
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
                lblGrandTotal.Text = grandDbl.ToString("#0.00");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lstBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void lstBranch_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    foreach (ListItem li in lstBranch.Items)
    //    {
    //        if (lstBranch.SelectedIndex == 0)
    //        {
    //            if (li.Text != "All")
    //            {
    //                li.Selected = true;
    //            }
    //        }
    //        //else
    //        //{
    //        //    li.Selected = false;
    //        //}
    //    }
    //}

}
