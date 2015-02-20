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
using System.Net.NetworkInformation;
using System.Management;

public partial class SalesReport1 : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double SumCashSales = 0.0d;
    BusinessLogic objBL;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            //printPreview();
            if (!IsPostBack)
            {

                //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                //Label2.Text = nics[0].GetPhysicalAddress().ToString();


                //Label1.Text = GetMacAddress().ToString();

                //Label3.Text = GetMacAdd().ToString();

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                lblBillDate.Text = DateTime.Now.ToShortDateString();
                SalesPanel.Visible = false;

                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtEndDate.Text = dtaa;

                //txtEndDate.Text = DateTime.Now.ToShortDateString();

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



                DateTime startDate;
                DateTime endDate;

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                SalesPanel.Visible = true;
                string option = string.Empty;

                DateTime stdt = Convert.ToDateTime(txtStartDate.Text);
                DateTime etdt = Convert.ToDateTime(txtEndDate.Text);

                if (Request.QueryString["startDate"] != null)
                    stdt = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
                if (Request.QueryString["endDate"] != null)
                    etdt = Convert.ToDateTime(Request.QueryString["endDate"].ToString());
                if (Request.QueryString["option"] != null)
                    option = Request.QueryString["option"].ToString();

                startDate = Convert.ToDateTime(stdt);
                endDate = Convert.ToDateTime(etdt);
                lblStartDate.Text = startDate.ToString();
                lblEndDate.Text = endDate.ToString();
                ReportsBL.ReportClass rptSalesReport;

                rptSalesReport = new ReportsBL.ReportClass();
                DataSet ds = new DataSet();

                if (option == "All")
                {
                    ds = rptSalesReport.generateSalesReportDS(startDate, endDate, sDataSource);
                }
                else
                {
                    ds = bl.generateSalesReportDSE(startDate, endDate, sDataSource, option);
                }

                gvSales.DataSource = ds;
                gvSales.DataBind();
                SalesPanel.Visible = true;
                div1.Visible = false;


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //public static string GetMacAdd()
    //{

    //    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

    //    ManagementObjectCollection moc = mc.GetInstances();

    //    string MACAddress = "";

    //    foreach (ManagementObject mo in moc)
    //    {

    //        if (mo["MacAddress"] != null)
    //        {

    //            if ((bool)mo["IPEnabled"] == true)
    //            {

    //                MACAddress = mo["MacAddress"].ToString();



    //            }

    //        }

    //    }

    //    return MACAddress;



    //}

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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate, endDate;
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            SalesPanel.Visible = true;

            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            lblStartDate.Text = txtStartDate.Text;
            lblEndDate.Text = txtEndDate.Text;
            ReportsBL.ReportClass rptSalesReport;
            string option = string.Empty;
            option = optionmethod.SelectedItem.Text;
            rptSalesReport = new ReportsBL.ReportClass();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();

            if (optionmethod.SelectedItem.Text == "All")
            {
                ds = rptSalesReport.generateSalesReportDS(startDate, endDate, sDataSource);
            }
            else
            {
                ds = bl.generateSalesReportDSE(startDate, endDate, sDataSource, option);
            }

            gvSales.DataSource = ds;
            gvSales.DataBind();
            SalesPanel.Visible = true;
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
            SalesPanel.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //public static PhysicalAddress GetMacAddress()
    //{
    //    foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
    //    {
    //        // Only consider Ethernet network interfaces
    //        if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
    //            nic.OperationalStatus == OperationalStatus.Up)
    //        {
    //            return nic.GetPhysicalAddress();
    //        }
    //    }
    //    return null;
    //}

    protected void btnExl_Click(object sender, EventArgs e)
    {
        try
        {
            //MessageBox.Show(GetMacAddress().ToString());
            //ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found "+ GetMacAddress().ToString()+"');", true);
            bindData();
            //Response.Redirect("SalesSummaryReport.aspx?myname=" + "NEWCUS",'Graph','height=700,width=1000,left=172,top=10');", true);
            //ResponseHelper.Redirect("SalesSummaryReport.aspx", "_blank", "menubar=0,width=100,height=100");
            //string NEWCUS = string.Empty;
            //Response.Write("SalesSummaryReport.asp", "", "height=700,width=1000,left=172,top=10,scrollbars=yes,toolbars=no");
            //Response.Write("<script language='javascript'> window.open('SalesReport.aspx' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            //Response.Write("<script language='javascript'> window.open('" + DropDownList1.SelectedValue + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            //response.write "<script>"
            //response.write "open("submitted_fr.asp"","ThankYou"",""width=450 ,height=200,scrollbars=no,toolbars=no");
            //response.write "window.opener=top;window.close();</script>"
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('SalesSummaryReport.aspx','Graph','height=700,width=1000,left=172,top=10');", true);
            //window.open('BillCustomerView.aspx?ID=' + ID + '&cname=' + Customername ,'','letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void bindData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        double brandTotal = 0;
        double catTotal = 0;
        double total = 0;
        double producttot = 0;
        string brand = string.Empty;
        string Category = string.Empty;
        string product = string.Empty;

        //startDate = Convert.ToDateTime(txtStartDate.Text);
        //endDate = Convert.ToDateTime(txtEndDate.Text);
        string condtion = "";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        if (txtStartDate.Text != "" && txtEndDate.Text != "")
        {
            objBL.StartDate = txtStartDate.Text;
            objBL.StartDate = string.Format("{0:MM/dd/yyyy}", txtStartDate.Text);

            objBL.EndDate = txtEndDate.Text;
            objBL.EndDate = string.Format("{0:MM/dd/yyyy}", txtEndDate.Text);

            string aa = txtStartDate.Text;
            string dttt = Convert.ToDateTime(aa).ToString("MM/dd/yyyy");

            string aaa = txtEndDate.Text;
            string dtt = Convert.ToDateTime(aaa).ToString("MM/dd/yyyy");

            condtion = " BillDate >= #" + dttt + "# and Billdate <= #" + dtt + "# ";
        }


        dt.Columns.Add(new DataColumn("BillDate"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("Customer"));
        dt.Columns.Add(new DataColumn("Paymode"));
        dt.Columns.Add(new DataColumn("ItemCode"));
        dt.Columns.Add(new DataColumn("ProductName"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Qty"));
        dt.Columns.Add(new DataColumn("Rate"));
        dt.Columns.Add(new DataColumn("Discount"));
        dt.Columns.Add(new DataColumn("Vat"));
        dt.Columns.Add(new DataColumn("Cst"));
        dt.Columns.Add(new DataColumn("Value"));

        string groupBy = string.Empty;
        string ordrby = string.Empty;
        string selColumn = string.Empty;
        string field2 = " tblProductMaster.productdesc as brand,tblProductMaster.productname,tblProductMaster.model,(((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100)*tblSalesItems.qty*tblSalesItems.rate) + (((tblSalesItems.Qty*tblSalesItems.rate)- ((tblSalesItems.discount/100)*tblSalesItems.Qty*tblSalesItems.Rate)) * tblSalesItems.VAT/100) + (((tblSalesItems.Qty*tblSalesItems.Rate)-((tblSalesItems.discount/100)*tblSalesItems.Qty*tblSalesItems.Rate)) * tblSalesItems.CST/100))) as amount,tblSalesItems.Rate,tblSalesItems.Qty,tblCategories.categoryname,tblSalesItems.Itemcode,tblSales.Billno,tblSales.billdate,tblSales.Customername,tblSalesItems.Discount, tblSalesItems.Vat,tblSalesITems.cst,tblSales.paymode ";
        if (optionmethod.SelectedItem.Text == "All")
        {
            condtion = condtion;
        }
        else if (optionmethod.SelectedItem.Text == "Sales")
        {
            condtion = condtion + " and tblsales.InternalTransfer in ('no','NO') and tblsales.purchaseReturn in ('no','NO') and tblsales.DeliveryNote in ('no','NO') ";
        }
        else if (optionmethod.SelectedItem.Text == "Internal Transfer")
        {
            condtion = condtion + " and tblsales.InternalTransfer in ('yes','YES') and tblsales.purchaseReturn in ('no','NO') and tblsales.DeliveryNote in ('no','NO') and tblsales.NormalSales in ('no','NO') and tblsales.ManualSales in ('no','NO') ";
        }
        else if (optionmethod.SelectedItem.Text == "Delivery Note")
        {
            condtion = condtion + " and tblsales.InternalTransfer in ('no','NO') and tblsales.purchaseReturn in ('no','NO') and tblsales.DeliveryNote in ('yes','YES') and tblsales.NormalSales in ('no','NO') and tblsales.ManualSales in ('no','NO') ";
        }
        else if (optionmethod.SelectedItem.Text == "Purchase Return")
        {
            condtion = condtion + " and tblsales.InternalTransfer in ('no','NO') and tblsales.purchaseReturn in ('yes','YES') and tblsales.DeliveryNote in ('no','NO') and tblsales.NormalSales in ('no','NO') and tblsales.ManualSales in ('no','NO') ";
        }

        //ds = objBL.getSalesreport(startDate, endDate, "All", "All", "All");

        ds= objBL.getSales1(selColumn, field2, condtion, "", "");

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                tLvlValueTemp = dr["paymode"].ToString().ToUpper().Trim();

                //if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                //   (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                //   (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                //{
                //    DataRow dr_final889 = dt.NewRow();
                //    dt.Rows.Add(dr_final889);

                //    DataRow dr_final8 = dt.NewRow();
                //    dr_final8["Category"] = "";
                //    dr_final8["Brand"] = "";
                //    dr_final8["Product Name"] = "Total : " + tLvlValue;
                //    dr_final8["Model"] = "";
                //    dr_final8["Qty"] = "";
                //    dr_final8["Rate"] = "";
                //    dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(producttot));
                //    producttot = 0;
                //    dt.Rows.Add(dr_final8);

                //    DataRow dr_final888 = dt.NewRow();
                //    dt.Rows.Add(dr_final888);
                //}

                //if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                //(sLvlValue != "" && sLvlValue != sLvlValueTemp))
                //{
                //    DataRow dr_final8 = dt.NewRow();
                //    dr_final8["Category"] = "";
                //    dr_final8["Brand"] = "Total : " + sLvlValue;
                //    dr_final8["Product Name"] = "";
                //    dr_final8["Model"] = "";
                //    dr_final8["Qty"] = "";
                //    dr_final8["Rate"] = "";
                //    dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                //    brandTotal = 0;
                //    dt.Rows.Add(dr_final8);

                //    DataRow dr_final888 = dt.NewRow();
                //    dt.Rows.Add(dr_final888);
                //}

                //if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                //{
                //    //DataRow dr_final889 = dt.NewRow();
                //    //dt.Rows.Add(dr_final889);

                //    DataRow dr_final8 = dt.NewRow();
                //    dr_final8["Category"] = "Total : " + fLvlValue;
                //    dr_final8["Brand"] = "";
                //    dr_final8["Product Name"] = "";
                //    dr_final8["Model"] = "";
                //    dr_final8["Qty"] = "";
                //    dr_final8["Rate"] = "";
                //    dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(catTotal));
                //    catTotal = 0;
                //    dt.Rows.Add(dr_final8);

                //    DataRow dr_final888 = dt.NewRow();
                //    dt.Rows.Add(dr_final888);
                //}
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["BillNo"] = dr["BillNo"];
                dr_final12["BillDate"] = dr["BillDate"];
                dr_final12["Customer"] = dr["CustomerName"];
                if (tLvlValueTemp == "1")
                {
                    dr_final12["Paymode"] = "Cash";
                }
                else if (tLvlValueTemp == "2")
                {
                    dr_final12["Paymode"] = "Bank";
                }
                else if (tLvlValueTemp == "3")
                {
                    dr_final12["Paymode"] = "Credit";
                }
                else if (tLvlValueTemp == "4")
                {
                    dr_final12["Paymode"] = "Multiple Payment";
                }
                dr_final12["ItemCode"] = dr["ItemCode"];
                dr_final12["ProductName"] = dr["ProductName"];
                dr_final12["Model"] = dr["model"];
                dr_final12["Qty"] = dr["Qty"];
                dr_final12["Rate"] = dr["Rate"];
                dr_final12["Vat"] = dr["vat"];
                dr_final12["Cst"] = dr["cst"];
                dr_final12["Discount"] = dr["discount"];
                dr_final12["Value"] = Convert.ToDouble(dr["Amount"]);
                brandTotal = brandTotal + (Convert.ToDouble(dr["Amount"]));
                catTotal = catTotal + (Convert.ToDouble(dr["Amount"]));
                producttot = producttot + (Convert.ToDouble(dr["Amount"]));
                total = total + (Convert.ToDouble(dr["Amount"]));
                dt.Rows.Add(dr_final12);
            }
        }

        DataRow dr_final879 = dt.NewRow();
        dt.Rows.Add(dr_final879);

        //DataRow dr_final88 = dt.NewRow();
        //dr_final88["Category"] = "";
        //dr_final88["Brand"] = "";
        //dr_final88["Product Name"] = "Total : " + tLvlValueTemp;
        //dr_final88["Model"] = "";
        //dr_final88["Qty"] = "";
        //dr_final88["Rate"] = "";
        //dr_final88["Amount"] = Convert.ToString(Convert.ToDecimal(producttot));
        //dt.Rows.Add(dr_final88);

        //DataRow dr_final79 = dt.NewRow();
        //dt.Rows.Add(dr_final79);

        //DataRow dr_final89 = dt.NewRow();
        //dr_final88["Category"] = "";
        //dr_final89["Brand"] = "Total : " + sLvlValueTemp;
        //dr_final89["Product Name"] = "";
        //dr_final89["Model"] = "";
        //dr_final89["Qty"] = "";
        //dr_final89["Rate"] = "";
        //dr_final89["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
        //dt.Rows.Add(dr_final89);

        //DataRow dr_final8879 = dt.NewRow();
        //dt.Rows.Add(dr_final8879);

        //DataRow dr_final869 = dt.NewRow();
        //dr_final869["Category"] = "Total : " + fLvlValueTemp;
        //dr_final869["Brand"] = "";
        //dr_final869["Product Name"] = "";
        //dr_final869["Model"] = "";
        //dr_final869["Qty"] = "";
        //dr_final869["Rate"] = "";
        //dr_final869["Amount"] = Convert.ToString(Convert.ToDecimal(catTotal));
        //dt.Rows.Add(dr_final869);

        //DataRow dr_final9 = dt.NewRow();
        //dt.Rows.Add(dr_final9);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["BillNo"] = "";
        dr_final789["BillDate"] = "";
        dr_final789["Customer"] = "";
        dr_final789["Paymode"] = "";
        dr_final789["ItemCode"] = "";
        dr_final789["ProductName"] = "";
        dr_final789["Model"] = "";
        dr_final789["Qty"] = "";
        dr_final789["Rate"] = "";
        dr_final789["Vat"] = "";
        dr_final789["Cst"] = "";
        dr_final789["Discount"] = "";
        dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));
        dt.Rows.Add(dr_final789);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            //string filename = "Sales Report.xls";
            string filename = "Daily Sales _" + DateTime.Now.ToString() + ".xls";
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

    protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double sumValue = 0.0;
            double sumRateQty = 0.0;
            double sumDis = 0.0;
            double sumVat = 0.0;
            double sumCst = 0.0;
            double discount = 0;
            double vat = 0;
            double cst = 0;
            double rate = 0;
            double qty = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblQty = e.Row.FindControl("lblQty") as Label;
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblDisc = e.Row.FindControl("lblDisc") as Label;
                Label lblVat = e.Row.FindControl("lblVat") as Label;
                Label lblCst = e.Row.FindControl("lblCst") as Label;
                Label lblValue = e.Row.FindControl("lblValue") as Label;
                discount = Convert.ToDouble(lblDisc.Text);
                vat = Convert.ToDouble(lblVat.Text);
                cst = Convert.ToDouble(lblCst.Text);
                rate = Convert.ToDouble(lblRate.Text);
                qty = Convert.ToDouble(lblQty.Text);

                sumRateQty = rate * qty;
                sumDis = sumRateQty - (sumRateQty * (discount / 100));

                sumVat = sumDis * (vat / 100);
                sumCst = sumDis * (cst / 100);

                sumValue = sumDis + sumVat + sumCst;

                lblValue.Text = sumValue.ToString("f2"); // Convert.ToString(sumValue);


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource =  Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblPurchaseID = e.Row.FindControl("lblBillno") as Label;
                Label lblPaymode = e.Row.FindControl("lblPaymode") as Label;
                if (lblPaymode.Text == "1")
                {
                    lblPaymode.Text = "Cash";
                }
                else if (lblPaymode.Text == "2")
                {
                    lblPaymode.Text = "Bank";
                }
                else
                {
                    lblPaymode.Text = "Credit";
                }
                if (lblPurchaseID.Text != "")
                {
                    int billno = Convert.ToInt32(lblPurchaseID.Text);
                    ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();
                    if (Request.Cookies["Company"] != null)
                        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                    DataSet ds = rptProduct.getProductsSales(billno, sDataSource);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        gv.DataBind();
                    }
                }
                //SumCashSales = SumCashSales + Convert.ToDouble(lblTotalAmt.Text);
                //lblGrandCashSales.Text = "Rs. " + SumCashSales.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
