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

public partial class ReportXLBankRecon1 : System.Web.UI.Page
{
    

     private string sDataSource = string.Empty;
    private string Connection = string.Empty;
    BusinessLogic objBL = new BusinessLogic();
 ////   private string sDataSource = string.Empty;
 //   private string Connection = string.Empty;
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
                    lblHeading.Text = "Bank Reconciliation Report ";
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
            DataSet dsttt = new DataSet();
            string connection = Request.Cookies["Company"].Value;

            string btnlist = Convert.ToString(Request.QueryString["list"].ToString());
            int iLedgerID = Convert.ToInt32(Request.QueryString["ledger"].ToString());
            string Types = Convert.ToString(Request.QueryString["type"].ToString());


            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            //startDate = Convert.ToDateTime(txtSrtDate.Text);

            string usernam = Request.Cookies["LoggedUserName"].Value;

            if (btnlist == "All")
            {
                dsttt = objBL.checkbankreconciliation1(iLedgerID, sDataSource, usernam, Types);

                if (dsttt != null)
                {
                    if (dsttt.Tables[0].Rows.Count > 0)
                    {
                        dsttt = objBL.getbankreconciliation2(iLedgerID, sDataSource, usernam);
                        ds = objBL.getbankrecon1(iLedgerID, sDataSource, usernam, Types);

                        if (dsttt != null)
                        {
                            if (dsttt.Tables[0].Rows.Count > 0)
                                ds.Tables[0].Merge(dsttt.Tables[0]);
                        }
                    }
                    else
                    {
                        ds = objBL.getbankreconciliation3(iLedgerID, sDataSource, usernam);
                    }
                }
                else
                {
                    ds = objBL.getbankreconciliation3(iLedgerID, sDataSource, usernam);
                }

                if (ds != null)
                {
                    DataTable dt = new DataTable("bank ercon");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt.Columns.Add(new DataColumn("TransNo"));
                        dt.Columns.Add(new DataColumn("TransDate"));
                        dt.Columns.Add(new DataColumn("Name"));
                        dt.Columns.Add(new DataColumn("Ledger Name"));
                        dt.Columns.Add(new DataColumn("Branch"));
                        dt.Columns.Add(new DataColumn("Amount"));
                        dt.Columns.Add(new DataColumn("Narration"));
                        dt.Columns.Add(new DataColumn("Voucher Type"));
                        dt.Columns.Add(new DataColumn("ChequeNo"));
                        dt.Columns.Add(new DataColumn("Reconcilated By"));
                        dt.Columns.Add(new DataColumn("Reconcilated Date"));
                        dt.Columns.Add(new DataColumn("Remarks"));

                        DataRow dr_final679 = dt.NewRow();
                        dt.Rows.Add(dr_final679);

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DataRow dr_final6 = dt.NewRow();
                            dr_final6["TransNo"] = dr["TransNo"];
                            dr_final6["TransDate"] = dr["TransDate"];
                            dr_final6["Name"] = dr["Debtor"];
                            dr_final6["Ledger Name"] = dr["Creditor"];
                            dr_final6["Branch"] = dr["BranchCode"];
                            dr_final6["Amount"] = dr["Amount"];
                            dr_final6["Narration"] = dr["Narration"];
                            dr_final6["ChequeNo"] = dr["ChequeNo"];
                            dr_final6["Voucher Type"] = dr["VoucherType"];
                            dr_final6["Reconcilated By"] = dr["ReconcilatedBy"];
                            dr_final6["Reconcilated Date"] = dr["Reconcilateddate"];
                            dr_final6["Remarks"] = dr["Result"];
                            dt.Rows.Add(dr_final6);
                        }
                       // ExportToExcel(dt);
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
            else if (btnlist == "Pending")
            {
                dsttt = objBL.getbankreconciliation2(iLedgerID, sDataSource, usernam);

                if (dsttt != null)
                {
                    DataTable dt = new DataTable("Bank recon");
                    if (dsttt.Tables[0].Rows.Count > 0)
                    {
                        dt.Columns.Add(new DataColumn("TransNo"));
                        dt.Columns.Add(new DataColumn("TransDate"));
                        dt.Columns.Add(new DataColumn("Name"));
                        dt.Columns.Add(new DataColumn("Ledger Name"));
                        dt.Columns.Add(new DataColumn("Branch"));
                        dt.Columns.Add(new DataColumn("Amount"));
                        dt.Columns.Add(new DataColumn("Narration"));
                        dt.Columns.Add(new DataColumn("Voucher Type"));
                        dt.Columns.Add(new DataColumn("ChequeNo"));
                        dt.Columns.Add(new DataColumn("Reconcilated By"));
                        dt.Columns.Add(new DataColumn("Reconcilated Date"));
                        dt.Columns.Add(new DataColumn("Remarks"));

                        DataRow dr_final679 = dt.NewRow();
                        dt.Rows.Add(dr_final679);

                        foreach (DataRow dr in dsttt.Tables[0].Rows)
                        {
                            DataRow dr_final6 = dt.NewRow();
                            dr_final6["TransNo"] = dr["TransNo"];
                            dr_final6["TransDate"] = dr["TransDate"];
                            dr_final6["Name"] = dr["Debtor"];
                            dr_final6["Ledger Name"] = dr["Creditor"];
                            dr_final6["Branch"] = dr["BranchCode"];
                            dr_final6["Amount"] = dr["Amount"];
                            dr_final6["Narration"] = dr["Narration"];
                            dr_final6["ChequeNo"] = dr["ChequeNo"];
                            dr_final6["Voucher Type"] = dr["VoucherType"];
                            dr_final6["Reconcilated By"] = dr["ReconcilatedBy"];
                            dr_final6["Reconcilated Date"] = dr["Reconcilateddate"];
                            dr_final6["Remarks"] = dr["Result"];
                            dt.Rows.Add(dr_final6);
                        }
                       // ExportToExcel(dt);
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
             else if (btnlist == "Reconciliated")
            {
                ds = objBL.getbankrecon1(iLedgerID, sDataSource, usernam, Types);

                if (ds != null)
                {
                    DataTable dt = new DataTable("Bank recon");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt.Columns.Add(new DataColumn("TransNo"));
                        dt.Columns.Add(new DataColumn("TransDate"));
                        dt.Columns.Add(new DataColumn("Name"));
                        dt.Columns.Add(new DataColumn("Ledger Name"));
                        dt.Columns.Add(new DataColumn("BranchCode"));
                        dt.Columns.Add(new DataColumn("Amount"));
                        dt.Columns.Add(new DataColumn("Narration"));
                        dt.Columns.Add(new DataColumn("Voucher Type"));
                        dt.Columns.Add(new DataColumn("ChequeNo"));
                        dt.Columns.Add(new DataColumn("Reconcilated By"));
                        dt.Columns.Add(new DataColumn("Reconcilated Date"));
                        dt.Columns.Add(new DataColumn("Remarks"));

                        DataRow dr_final679 = dt.NewRow();
                        dt.Rows.Add(dr_final679);

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DataRow dr_final6 = dt.NewRow();
                            dr_final6["TransNo"] = dr["TransNo"];
                            dr_final6["TransDate"] = dr["TransDate"];
                            dr_final6["Name"] = dr["Debtor"];
                            dr_final6["Ledger Name"] = dr["Creditor"];
                            dr_final6["Branch"] = dr["BranchCode"];
                            dr_final6["Amount"] = dr["Amount"];
                            dr_final6["Narration"] = dr["Narration"];
                            dr_final6["ChequeNo"] = dr["ChequeNo"];
                            dr_final6["Voucher Type"] = dr["VoucherType"];
                            dr_final6["Reconcilated By"] = dr["ReconcilatedBy"];
                            dr_final6["Reconcilated Date"] = dr["Reconcilateddate"];
                            dr_final6["Remarks"] = dr["Result"];
                            dt.Rows.Add(dr_final6);
                        }
                       // ExportToExcel(dt);
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
             if (dstt.Tables[0].Rows.Count > 0)
             {
                // ReportsBL.ReportClass rptStock = new ReportsBL.ReportClass();
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
                        //ReportGridView1.DataSource = dst2;
                        //ReportGridView1.DataBind();

                    }
                }

                //   DataSet dss = bl.getProducts(sDataSource, refDate, cond, cond1, cond2, cond3, cond4, "");
                //                DataTable customerTable = dss.Tables[0];


                //ConvertToCrossTab(customerTable);
                //if (dss.Tables[0].Rows.Count > 0)
                //{
                // //   gv.DataSource = dss;
                // //   gv.DataBind();

                //    //ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //    //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report.rdlc");
                //    //Customers dsCustomers = GetData("select top 20 * from customers");
                //    //ReportDataSource datasource = new ReportDataSource("Stock", ds.Tables[0]);
                //    //ReportViewer1.LocalReport.DataSources.Clear();
                //    //ReportViewer1.LocalReport.DataSources.Add(datasource);

                //}
                //else
                //{
                //    //lblCategory.Text = "";
                //}
                // lblTotal.Text = sumDbl.ToString("f2");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    //private void BindData()
    //{
    //    BusinessLogic bl = new BusinessLogic(sDataSource);

    //    //DateTime refDate = DateTime.Parse(txtStartDate.Text);
    //    //DateTime stdt = Convert.ToDateTime(txtStartDate.Text);

    //    if (Request.QueryString["refDate"] != null)
    //    {
    //        //    stdt = Convert.ToDateTime(Request.QueryString["refDate"].ToString());
    //        //    cond = Request.QueryString["cond"].ToString();
    //        //    cond = Server.UrlDecode(cond);
    //        //    cond1 = Request.QueryString["cond1"].ToString();
    //        //    cond1 = Server.UrlDecode(cond1);
    //        //    cond2 = Request.QueryString["cond2"].ToString();
    //        //    cond2 = Server.UrlDecode(cond2);
    //        //    cond3 = Request.QueryString["cond3"].ToString();
    //        //    cond3 = Server.UrlDecode(cond3);
    //        //    cond4 = Request.QueryString["cond4"].ToString();
    //        //    cond4 = Server.UrlDecode(cond4);
    //        //    cond5 = Request.QueryString["cond5"].ToString();
    //        //    cond5 = cond5.ToString();
    //        //    cond6 = Request.QueryString["cond6"].ToString();
    //        //    cond6 = cond6.ToString();
    //        //}
    //        //refDate = Convert.ToDateTime(stdt);


    //        DataSet ds = bl.GetProductlist(sDataSource, cond);

    //        DataTable dt = new DataTable();


    //        if (ds != null)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                dt.Columns.Add(new DataColumn("ItemCode"));
    //                dt.Columns.Add(new DataColumn("ProductName"));
    //                dt.Columns.Add(new DataColumn("Brand"));
    //                dt.Columns.Add(new DataColumn("Model"));
    //                //dt.Columns.Add(new DataColumn("Rol"));

    //                char[] commaSeparator = new char[] { ',' };
    //                string[] result;
    //                result = cond6.Split(commaSeparator, StringSplitOptions.None);

    //                foreach (string str in result)
    //                {
    //                    dt.Columns.Add(new DataColumn(str));
    //                }
    //                dt.Columns.Remove("Column1");

    //                char[] commaSeparator1 = new char[] { ',' };
    //                string[] result1;
    //                result1 = cond5.Split(commaSeparator, StringSplitOptions.None);

    //                foreach (string str1 in result1)
    //                {
    //                    dt.Columns.Add(new DataColumn(str1));
    //                }
    //                dt.Columns.Remove("Column1");
    //                DataRow dr_final123 = dt.NewRow();
    //                dt.Rows.Add(dr_final123);

    //                DataSet dst = new DataSet();

    //                string itemcode = "";

    //                foreach (DataRow dr in ds.Tables[0].Rows)
    //                {
    //                    itemcode = Convert.ToString(dr["itemcode"]);

    //                    dst = bl.getProducts(sDataSource, refDate, cond, cond1, cond2, cond3, cond4, itemcode);

    //                    DataRow dr_final6 = dt.NewRow();
    //                    dr_final6["Brand"] = dr["brand"];
    //                    dr_final6["ProductName"] = dr["ProductName"];
    //                    dr_final6["Model"] = dr["Model"];
    //                    dr_final6["ItemCode"] = dr["Itemcode"];

    //                    if (dst != null)
    //                    {
    //                        if (dst.Tables[0].Rows.Count > 0)
    //                        {
    //                            foreach (DataRow drt in dst.Tables[0].Rows)
    //                            {
    //                                char[] commaSeparator2 = new char[] { ',' };
    //                                string[] result2;
    //                                result2 = cond6.Split(commaSeparator, StringSplitOptions.None);

    //                                foreach (string str2 in result2)
    //                                {
    //                                    string item1 = str2;
    //                                    string item123 = Convert.ToString(drt["pricename"]);

    //                                    if (item123 == item1)
    //                                    {
    //                                        dr_final6[item1] = drt["price"];
    //                                    }
    //                                }


    //                                char[] commaSeparator3 = new char[] { ',' };
    //                                string[] result3;
    //                                result3 = cond5.Split(commaSeparator, StringSplitOptions.None);

    //                                foreach (string str3 in result3)
    //                                {
    //                                    string item11 = str3;
    //                                    string item1231 = Convert.ToString(drt["BranchCode"]);

    //                                    if (item1231 == item11)
    //                                    {
    //                                        dr_final6[item11] = drt["Stock"];
    //                                    }
    //                                }


    //                            }
    //                        }
    //                    }
    //                    dt.Rows.Add(dr_final6);
    //                }
    //                DataSet dst2 = new DataSet();
    //                dst2.Tables.Add(dt);
    //                //ReportGridView1.DataSource = dst2;
    //                //ReportGridView1.DataBind();

    //            }
    //        }
    //    }
    //}

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