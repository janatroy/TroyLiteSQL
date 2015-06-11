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
using System.Net.NetworkInformation;
using System.Management;

public partial class SalesReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double SumCashSales = 0.0d;
    BusinessLogic objBL;
    private string connection = string.Empty;
    string brncode;
    string usernam;
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

                loadBranch();
                BranchEnable_Disable();
            }
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
           // lstBranch.Items.Add(new ListItem("All", "0"));
            CheckBoxList1.Visible = true;
        }
        else
        {
            ds = bl.ListDefaultBranch(brncode);
            CheckBoxList1.Visible = false;
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

    public static string GetMacAdd()
    {

        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

        ManagementObjectCollection moc = mc.GetInstances();

        string MACAddress = "";

        foreach (ManagementObject mo in moc)
        {

            if (mo["MacAddress"] != null)
            {

                if ((bool)mo["IPEnabled"] == true)
                {

                    MACAddress = mo["MacAddress"].ToString();



                }

            }

        }

        return MACAddress;



    }

    public static string GetMacAddress()
    {

        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

        ManagementObjectCollection moc = mc.GetInstances();

        string MACAddress = "";

        foreach (ManagementObject mo in moc)
        {

            if (mo["MacAddress"] != null)
            {

                MACAddress = mo["MacAddress"].ToString();

            }

        }

        return MACAddress;

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
                    cond += " tblSales.BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond = cond.TrimEnd(',');
        cond = cond.Replace(",", "or");
        return cond;
    }


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

            string cond = "";
            cond = getCond();

            //if (optionmethod.SelectedItem.Text == "All")
            //{
            //    ds = rptSalesReport.generateSalesReportDS(startDate, endDate, sDataSource);
            //}
            //else
            //{
            //    ds = bl.generateSalesReportDSE(startDate, endDate, sDataSource, option);
            //}

            //gvSales.DataSource = ds;
            //gvSales.DataBind();
            SalesPanel.Visible = false;
            div1.Visible = true;
            if (lstBranch.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select any Branch')", true);
            }
            else
            {
                Response.Write("<script language='javascript'> window.open('SalesReport1.aspx?startDate=" + startDate + "&cond=" + Server.UrlEncode(cond) + "&endDate=" + endDate + "&option=" + option + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
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
        DataTable dt = new DataTable("sales report");
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

            condtion = " BillDate >= '" + dttt + "' and Billdate <= '" + dtt + "' ";
        }


        dt.Columns.Add(new DataColumn("BillDate"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("Customer"));
        dt.Columns.Add(new DataColumn("Paymode"));
        dt.Columns.Add(new DataColumn("ItemCode"));
        dt.Columns.Add(new DataColumn("ProductName"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("BranchCode"));
        dt.Columns.Add(new DataColumn("Qty"));
        dt.Columns.Add(new DataColumn("Rate"));
        dt.Columns.Add(new DataColumn("Discount"));
        dt.Columns.Add(new DataColumn("Vat"));
        dt.Columns.Add(new DataColumn("Cst"));
        dt.Columns.Add(new DataColumn("Value"));
        

        string groupBy = string.Empty;
        string ordrby = string.Empty;
        string selColumn = string.Empty;

        string sBillDate = string.Empty;
        string sBillNo = string.Empty;
        string sCustomerName = string.Empty;
        string sBranchCode = string.Empty;
        string sQry = string.Empty;
        string sPayMode = string.Empty;
        string sProductName = string.Empty;
        string sProductModel = string.Empty;
        string sProductDesc = string.Empty;
        string sConStr = string.Empty;
        string branch1 = getCond();

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
        //if (optionmethod.SelectedItem.Text == "Sales")
        //{
        //    sQry = "SELECT Billno,BillDate,Customername,paymode,BranchCode FROM tblSales WHERE (" + branch1 + ") and  " + condtion + " and tblsales.InternalTransfer in ('no','NO') and tblsales.purchaseReturn in ('no','NO') and tblsales.DeliveryNote in ('no','NO') Order by BillDate Desc";
        //}
        //else if (optionmethod.SelectedItem.Text == "Internal Transfer")
        //{
        //    sQry = "SELECT Billno,BillDate,Customername,paymode,BranchCode FROM tblSales WHERE (" + branch1 + ") and  " + condtion + " and tblsales.InternalTransfer in ('yes','YES') and tblsales.purchaseReturn in ('no','NO') and tblsales.DeliveryNote in ('no','NO') and tblsales.NormalSales in ('no','NO') and tblsales.ManualSales in ('no','NO') Order by BillDate Desc";
        //}
        //else if (optionmethod.SelectedItem.Text == "Delivery Note")
        //{
        //    sQry = "SELECT Billno,BillDate,Customername,paymode,BranchCode FROM tblSales WHERE (" + branch1 + ") and  " + condtion + " and tblsales.InternalTransfer in ('no','NO') and tblsales.purchaseReturn in ('no','NO') and tblsales.DeliveryNote in ('yes','YES') and tblsales.NormalSales in ('no','NO') and tblsales.ManualSales in ('no','NO') Order by BillDate Desc";
        //}
        //else if (optionmethod.SelectedItem.Text == "Purchase Return")
        //{
        //    sQry = "SELECT Billno,BillDate,Customername,paymode,BranchCode FROM tblSales WHERE (" + branch1 + ") and " + condtion + " and tblsales.InternalTransfer in ('no','NO') and tblsales.purchaseReturn in ('yes','YES') and tblsales.DeliveryNote in ('no','NO') and tblsales.NormalSales in ('no','NO') and tblsales.ManualSales in ('no','NO') Order by BillDate Desc";
        //}

        //ds = objBL.getSalesreport(startDate, endDate, "All", "All", "All");
        string branch = getCond();

        branch = " (" + branch + ")";

        ds = objBL.getSalesExporttoexcel(selColumn, field2, condtion, branch);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                tLvlValueTemp = dr["paymode"].ToString().ToUpper().Trim();

               
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
                dr_final12["Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"]);// Convert.ToDouble(dr["Amount"]);
                dr_final12["BranchCode"] = dr["BranchCode"];
                brandTotal = brandTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"]));
                catTotal = catTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"]));
                producttot = producttot + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"]));
                total = total + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["Rate"]));
                dt.Rows.Add(dr_final12);
            }
        }

        DataRow dr_final879 = dt.NewRow();
        dt.Rows.Add(dr_final879);

       

        DataRow dr_final789 = dt.NewRow();
        dr_final789["BillNo"] = "";
        dr_final789["BillDate"] = "Total : ";
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
        dr_final789["BranchCode"] = "";
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
            using (XLWorkbook wb = new XLWorkbook())
            {
                string filename = "Sales report.xlsx";
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
    //protected void lstBranch_SelectedIndexChanged(object sender, EventArgs e)
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
    //    }
    //}

    protected void lst_SelectedIndexChanged_1(object sender, EventArgs e)
    {
        if (CheckBoxList1.Items[0].Selected == true)
        {
            foreach (ListItem ls in lstBranch.Items)
            {
                ls.Selected = true;

            }

        }
        else
        {
            foreach (ListItem ls in lstBranch.Items)
            {
                ls.Selected = false;

            }

        }
    }
}
