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
using System.Globalization;

public partial class SalesdailyReport4 : System.Web.UI.Page
{

    public string sDataSource = string.Empty;
    double dSNetRate = 0;
    double dSVatRate = 0;

    double dSCSTRate = 0;
    double dSFrRate = 0;
    double dSLURate = 0;

    double dSQty = 0;

    double dSGrandRate = 0;
    double dSDiscountRate = 0;

    double dSCDiscountRate = 0;
    double dSCNetRate = 0;
    double dSCmgmtpfofit = 0;
    double dSCranchprofit = 0;
    double dSCVatRate = 0;

    double dSCCSTRate = 0;
    double dSCFrRate = 0;
    double dSCLURate = 0;
    double dSCGrandRate = 0;
    string tempBillno = "0";
    string strBillno = string.Empty;
    int cnt = 0;
    BusinessLogic objBL;
    string cond;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                lblBillDate.Text = DateTime.Now.ToShortDateString();

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
                                //lblTNGST.Text = Convert.ToString(dr["TINno"]);
                                lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                                lblPhone.Text = Convert.ToString(dr["Phone"]);
                                //lblGSTno.Text = Convert.ToString(dr["GSTno"]);

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


                divPrint.Visible = true;
                divmain.Visible = true;

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DateTime startDate, endDate;
                string category = string.Empty;

                // DateTime stdt = Convert.ToDateTime(txtStartDate.Text);
                //  DateTime etdt = Convert.ToDateTime(txtEndDate.Text);

                // if (Request.QueryString["startDate"] != null)
                //  {
                //     stdt = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
                // }
                // if (Request.QueryString["endDate"] != null)
                // {
                //     etdt = Convert.ToDateTime(Request.QueryString["endDate"].ToString());
                //}

                //  startDate = Convert.ToDateTime(stdt);
                //  endDate = Convert.ToDateTime(etdt);
                //  lblStartDate.Text = startDate.ToString("dd/MM/yyyy");
                //  lblEndDate.Text = endDate.ToString("dd/MM/yyyy");

                //if (Request.QueryString["category"] != null)
                //{
                //    category = Request.QueryString["category"].ToString();
                //}


                string intTrans = "";
                string purRet = "";
                string delNote = "";
                intTrans = "NO";
                purRet = "NO";
                delNote = "NO";

                //if (Request.QueryString["intTrans"] != null)
                //{
                //    intTrans = Request.QueryString["intTrans"].ToString();
                //}
                //if (Request.QueryString["purRet"] != null)
                //{
                //    purRet = Request.QueryString["purRet"].ToString();
                //}
                //if (Request.QueryString["delNote"] != null)
                //{
                //    delNote = Request.QueryString["delNote"].ToString();
                //}
                //if (chkIntTrans.Checked)
                //    intTrans = "YES";
                //else
                //    intTrans = "NO";

                //if (chkPurReturn.Checked)
                //    purRet = "YES";
                //else
                //    purRet = "NO";

                //if (chkDelNote.Checked)
                //    delNote = "YES";
                //else
                //    delNote = "NO";

                //  cond = Request.QueryString["cond"].ToString();
                // cond = Server.UrlDecode(cond);

                // binddata1();

                string Branch = string.Empty;

                if (Request.QueryString["BranchCode"] != null)
                {
                    Branch = Request.QueryString["BranchCode"].ToString();
                }

                //  lbl.Text = Branch;
                // date.Text = dtaa;

                bindDataDateWiseNormal();

                //  BindWME("", "");

                //DataSet BillDs = new DataSet();        
                //{
                //    BillDs = bl.FirstLevelDaywise(purRet, intTrans, delNote, Branch);
                //}
                //loadPriceList();
                //gvMain.DataSource = BillDs;
                //gvMain.DataBind();

                //div1.Visible = false;
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

    private void BindWME(string textSearch, string dropDown)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string branch = string.Empty;
        string total = string.Empty;
        string connection = Request.Cookies["Company"].Value;
        string sMonth = DateTime.Now.ToString("MM");
        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);
        var start = startDate.ToString("yyyy-MM-dd");
        var endDate = startDate.AddMonths(1).AddDays(-1);
        var end = endDate.ToString("yyyy-MM-dd");

        int year = DateTime.Now.Year;
        var firstDay = new DateTime(year, 1, 1);
        var firstday1 = firstDay.ToString("yyyy-MM-dd");
        var lastDay = new DateTime(year, 12, 31);
        var lastday1 = lastDay.ToString("yyyy-MM-dd");

        string usernam = Request.Cookies["LoggedUserName"].Value;
        double Tottot = 0.00;
        double Totgp = 0.00;
        double Totdp = 0.00;
        double totgpmon = 0.00;
        double totdpmon = 0.00;
        double dailysalestot = 0.00;
        int dailyqty = 0;
        int monthlyqty = 0;


        string Branch = string.Empty;
        string dtaa = string.Empty;
        //   BusinessLogic bl = new BusinessLogic(sDataSource);

        string intTrans = "";
        string purRet = "";
        string delNote = "";
        intTrans = "NO";
        purRet = "NO";
        delNote = "NO";
        string GroupBy1 = string.Empty;
        string condi = string.Empty;
        string condii = string.Empty;
        if (Request.QueryString["command"] != null)
        {
            string command = Request.QueryString["command"].ToString();

            if (command == "dailysales")
            {
                string todaydate = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                condi = "tblsales.billdate='" + todaydate + "'";
                condii = "s.billdate='" + todaydate + "'";
                GroupBy1 = "billdate,";

                head.Text = "Today's Sales - Real-Time Summary Report -";
                lbl.Text = Branch;
                date.Text = todaydate;

            }

            if (command == "monthlysales")
            {
                string sMonth1 = DateTime.Now.ToString("MM");
                DateTime now1 = DateTime.Now;
                var startDate1 = new DateTime(now.Year, now.Month, 1);
                var start1 = startDate.ToString("yyyy-MM-dd");
                var fromdate = startDate1.ToString("dd-MM-yyyy");
                var endDate1 = startDate.AddMonths(1).AddDays(-1);
                var end1 = endDate.ToString("yyyy-MM-dd");
                var enddate2 = endDate1.ToString("dd-MM-yyyy");

                condi = "tblsales.billdate>='" + start1 + "' and tblsales.billdate<='" + end1 + "'";
                condii = "s.billdate>='" + start1 + "' and s.billdate<='" + end1 + "'";
                GroupBy1 = "billdate,";

                head.Text = "This Month Sales - Real-Time Summary Report -";
                lbl.Text = Branch;
                date.Text = "Date From " + fromdate + " and end date " + enddate2 + "";

            }
            if (command == "annualsales")
            {
                int year1 = DateTime.Now.Year;
                var firstDay1 = new DateTime(year, 4, 1);
                var firstday11 = firstDay1.ToString("yyyy-MM-dd");
                var start11 = firstDay1.ToString("yyyy-MM-dd");
                int year11 = year1 + 1;
                var lastDay1 = new DateTime(year11, 3, 31);
                var lastday11 = lastDay1.ToString("yyyy-MM-dd");
                var enddate22 = lastDay1.ToString("dd-MM-yyyy");


                condi = "tblsales.billdate>='" + firstday11 + "' and tblsales.billdate<='" + lastday11 + "'";
                condii = "s.billdate>='" + firstday11 + "' and s.billdate<='" + lastday11 + "'";
                GroupBy1 = "billdate,";

                head.Text = "Annual Sales - Real-Time Summary Report -";
                lbl.Text = Branch;
                date.Text = "Date From " + start11 + " and end date " + enddate22 + "";

            }
        }

        if (Request.QueryString["BranchCode"] != null)
        {
            Branch = Request.QueryString["BranchCode"].ToString();
        }

        lbl.Text = Branch;
        // date.Text = dtaa;


        //  loadPriceList();
        //  gvMain.DataSource = BillDs;
        //  gvMain.DataBind();

        //  div1.Visible = false;


        string sCustomer = string.Empty;
        connection = Request.Cookies["Company"].Value;
        usernam = Request.Cookies["LoggedUserName"].Value;
        // BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);
        DataSet ds = new DataSet();
        // DataSet BillDs = new DataSet();
        {
            ds = bl.FirstLevelDaywise(purRet, intTrans, delNote, Branch, condi);
        }
        DataTable dt;
        DataRow drNew;
        DataSet dstt = new DataSet();

        DataColumn dc;

        DataSet dst = new DataSet();

        dt = new DataTable();
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("LinkName"));
        // dt.Columns.Add(new DataColumn("TotalWORndOff"));
        dt.Columns.Add(new DataColumn("Quantity"));
        dt.Columns.Add(new DataColumn("Managementprofit"));
        dt.Columns.Add(new DataColumn("BranchProfit"));
        dt.Columns.Add(new DataColumn("BranchCode"));
        dt.Columns.Add(new DataColumn("billSales"));
        //dt.Columns.Add(new DataColumn("DailySalesfordp"));
        //dt.Columns.Add(new DataColumn("montlySalesforgp"));
        //dt.Columns.Add(new DataColumn("montlySalesfordp"));
        //dt.Columns.Add(new DataColumn("AnnualSales"));
        //dt.Columns.Add(new DataColumn("Annualsalesquantity"));
        //dt.Columns.Add(new DataColumn("AnnualSalesforgp"));
        //dt.Columns.Add(new DataColumn("AnnualSalesfordp"));
        //dt.Columns.Add(new DataColumn("Amount"));



        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_final113 = dt.NewRow();

                    dr_final113["BillNo"] = dr["BillNo"].ToString();
                    dr_final113["LinkName"] = dr["LinkName"].ToString();
                    dr_final113["BranchCode"] = dr["BranchCode"].ToString();
                    string branch1 = dr["BranchCode"].ToString();

                    //BusinessLogic bl = new BusinessLogic(sDataSource);
                    DataSet db = bl.getdetailedsalesreport1(connection, dr["BranchCode"].ToString(), dr["BillNo"].ToString(), condii);
                    if (db != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drd in db.Tables[0].Rows)
                            {
                                dr_final113["billSales"] = (Convert.ToDecimal(drd["billSales"])).ToString("#0.00");
                                dr_final113["Managementprofit"] = ((Convert.ToDecimal(drd["billSales"])) - (Convert.ToDecimal(drd["DailySalesforgp"]))).ToString("#0.00");
                                DataSet db1 = bl.getdetailedsalesreport2(connection, dr["BranchCode"].ToString(), dr["BillNo"].ToString(), condii);
                                if (db1 != null)
                                {
                                    if (db1.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow drdd in db1.Tables[0].Rows)
                                        {
                                            dr_final113["BranchProfit"] = ((Convert.ToDecimal(drd["billSales"])) - (Convert.ToDecimal(drdd["DailySalesfordp"]))).ToString("#0.00");
                                        }
                                    }
                                    else
                                    {
                                        dr_final113["DailySalesfordp"] = 0;
                                    }
                                }
                                dr_final113["Quantity"] = drd["Todaysalesquantity"].ToString();
                            }
                        }
                        else
                        {
                            dr_final113["DailySales"] = 0;
                            dr_final113["DailySalesforgp"] = 0;
                            dr_final113["DailySalesfordp"] = 0;
                            dr_final113["Todaysalesquantity"] = 0;
                        }
                    }

                    else
                    {
                        dr_final113["DailySales"] = 0;
                        dr_final113["DailySalesforgp"] = 0;
                        dr_final113["DailySalesfordp"] = 0;
                        dr_final113["Todaysalesquantity"] = 0;
                    }
                    Tottot = Tottot + Convert.ToDouble(dr_final113["billSales"]);
                    dailysalestot = dailysalestot + Convert.ToDouble(dr_final113["Managementprofit"]);
                    // dailyqty = dailyqty + Convert.ToDouble(dr_final113["BranchProfit"]);
                    //monthlyqty = monthlyqty + Convert.ToInt32(dr_final113["monthlysalesquantity"]);
                    Totgp = Totgp + (Convert.ToDouble(dr_final113["BranchProfit"]));
                    //Totdp = Totdp + (Convert.ToDouble(dr_final113["DailySalesfordp"]));
                    //totgpmon = totgpmon + (Convert.ToDouble(dr_final113["montlySalesforgp"]));
                    //totdpmon = totdpmon + (Convert.ToDouble(dr_final113["montlySalesfordp"]));
                    dt.Rows.Add(dr_final113);

                }
                //DataRow dr_final11 = dt.NewRow();
                //dt.Rows.Add(dr_final11);

                //DataRow dr_final88 = dt.NewRow();
                //dr_final88["Branchcode"] = "Total";
                //dr_final88["billSales"] = Tottot.ToString("#0.00");
                //dr_final88["Managementprofit"] = dailysalestot.ToString("#0.00");
                //dr_final88["BranchProfit"] = Totgp.ToString("#0.00");
                //dr_final88["DailySales"] = dailysalestot.ToString("#0.00");
                //dr_final88["Todaysalesquantity"] = dailyqty.ToString("#0");
                //dr_final88["monthlysalesquantity"] = monthlyqty.ToString("#0");
                //dt.Rows.Add(dr_final88);

                dst.Tables.Add(dt);
                gvMain.DataSource = dst;
                gvMain.DataBind();
                div1.Visible = false;
            }
            else
            {
                gvMain.DataSource = null;
                gvMain.DataBind();
            }
        }
        else
        {
            gvMain.DataSource = null;
            gvMain.DataBind();
        }
    }

    public void binddata1()
    {
        string Branch = string.Empty;
        string dtaa = string.Empty;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        string intTrans = "";
        string purRet = "";
        string delNote = "";
        intTrans = "NO";
        purRet = "NO";
        delNote = "NO";

        if (Request.QueryString["BranchCode"] != null)
        {
            Branch = Request.QueryString["BranchCode"].ToString();
        }

        lbl.Text = Branch;
        date.Text = dtaa;



        DataSet BillDs = new DataSet();
        {
            // BillDs = bl.FirstLevelDaywise(purRet, intTrans, delNote, Branch);
        }
        loadPriceList();
        gvMain.DataSource = BillDs;
        gvMain.DataBind();

        div1.Visible = false;
    }

    protected string getCond6()
    {
        string cond6 = "";
        foreach (ListItem listItem1 in lstPricelist.Items)
        {
            if (listItem1.Text == listItem1.Text)
            {
                if (listItem1.Selected)
                {
                    cond6 += listItem1.Value + ",";
                }
                else
                {
                    cond6 += listItem1.Value + ",";
                }
            }
        }

        return cond6;
    }

    public void bindData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandTotal = 0;
        double producttot = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string item = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double netTotal = 0;
        double vatTotal = 0;
        double discountTotal = 0;
        double cstTotal = 0;
        double freightTotal = 0;
        double loadTotal = 0;

        double qtyTotal = 0;
        double qty1Total = 0;

        double rate1Total = 0;
        double net1Total = 0;
        double vat1Total = 0;
        double discount1Total = 0;
        double cst1Total = 0;
        double freight1Total = 0;
        double load1Total = 0;
        string cond6 = "";
        cond6 = getCond6();


        string GroupBy = string.Empty;
        string field2 = string.Empty;

        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        string Types = string.Empty;
        string options = string.Empty;

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        if ((cmbDisplayCat.SelectedItem.Text) == "Daywise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblsales.billdate,";
            GroupBy = "tblsales.billdate,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Categorywise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblcategories.categoryname,";
            GroupBy = "tblcategories.categoryname,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Brandwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblproductmaster.productdesc,";
            GroupBy = "tblproductmaster.productdesc,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Modelwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblproductmaster.model,";
            GroupBy = "tblproductmaster.model,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Billwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblsales.billno,";
            GroupBy = "tblsales.billno,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Customerwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblsales.Customername,";
            GroupBy = "tblsales.Customername,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Executivewise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblsales.executivename,";
            GroupBy = "tblsales.executivename,";
        }
        else if ((cmbDisplayCat.SelectedItem.Text) == "Itemwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayCat.SelectedItem.Text));
            field2 = "tblproductmaster.productname,";
            GroupBy = "tblproductmaster.productname,";
        }


        if ((cmbDisplayItem.SelectedItem.Text) == "Brandwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayItem.SelectedItem.Text));
            field2 = field2 + "tblproductmaster.productdesc,";
            GroupBy = GroupBy + "tblproductmaster.productdesc";
        }
        else if ((cmbDisplayItem.SelectedItem.Text) == "Modelwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayItem.SelectedItem.Text));
            field2 = field2 + "tblproductmaster.model,";
            GroupBy = GroupBy + "tblproductmaster.model";
        }
        else if ((cmbDisplayItem.SelectedItem.Text) == "Billwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayItem.SelectedItem.Text));
            field2 = field2 + "tblsales.billno,";
            GroupBy = GroupBy + "tblsales.billno";
        }
        else if ((cmbDisplayItem.SelectedItem.Text) == "Customerwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayItem.SelectedItem.Text));
            field2 = field2 + "tblsales.Customername,";
            GroupBy = GroupBy + "tblsales.Customername";
        }
        else if ((cmbDisplayItem.SelectedItem.Text) == "Itemwise")
        {
            dt.Columns.Add(new DataColumn(cmbDisplayItem.SelectedItem.Text));
            field2 = field2 + "tblproductmaster.productname,";
            GroupBy = GroupBy + "tblproductmaster.productname";
        }

        dt.Columns.Add(new DataColumn("Sales Rate"));
        dt.Columns.Add(new DataColumn("Qty"));
        dt.Columns.Add(new DataColumn("Net Rate"));
        dt.Columns.Add(new DataColumn("Discount Rate"));
        dt.Columns.Add(new DataColumn("Vat Rate"));
        dt.Columns.Add(new DataColumn("CST Rate"));
        dt.Columns.Add(new DataColumn("Freight"));
        dt.Columns.Add(new DataColumn("Loading/Unloading"));
        dt.Columns.Add(new DataColumn("Total"));
        char[] commaSeparator = new char[] { ',' };
        string[] result;
        result = cond6.Split(commaSeparator, StringSplitOptions.None);

        foreach (string str in result)
        {
            //dt.Columns.Add(new DataColumn(str));
            DataColumn colInt32pricelst = new DataColumn(str);
            colInt32pricelst.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(colInt32pricelst);
        }
        dt.Columns.Remove("Column1");


        double dNetRate = 0;
        double dVatRate = 0;
        double dDisRate = 0;
        double dCSTRate = 0;
        double dFrRate = 0;
        double dLURate = 0;
        double dGrandRate = 0;
        double dDiscountRate = 0;
        lblErr.Text = "";
        string category = string.Empty;
        category = Convert.ToString(cmbDisplayCat.SelectedItem.Text);
        var secondLevel = cmbDisplayItem.SelectedItem.Text.Trim();

        string intTrans = "";
        string purReturn = "";
        string delNote = "";

        if (chkIntTrans.Checked)
            intTrans = "YES";
        else
            intTrans = "NO";

        if (chkPurReturn.Checked)
            purReturn = "YES";
        else
            purReturn = "NO";

        if (chkDelNote.Checked)
            delNote = "YES";
        else
            delNote = "NO";


        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Cells[0].Text = category.Replace("wise", "");
        //}
        //else if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    if (DataBinder.Eval(e.Row.DataItem, "NetRate") != DBNull.Value)
        //        dNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate"));
        //    if (DataBinder.Eval(e.Row.DataItem, "ActualVat") != DBNull.Value)
        //        dVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualVat"));
        //    if (DataBinder.Eval(e.Row.DataItem, "ActualDiscount") != DBNull.Value)
        //        dDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualDiscount"));
        //    if (DataBinder.Eval(e.Row.DataItem, "SalesDiscount") != DBNull.Value)
        //        dDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesDiscount"));
        //    if (DataBinder.Eval(e.Row.DataItem, "ActualCST") != DBNull.Value)
        //        dCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));
        //    if (DataBinder.Eval(e.Row.DataItem, "SumFreight") != DBNull.Value)
        //        dFrRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumFreight"));
        //    if (DataBinder.Eval(e.Row.DataItem, "Loading") != DBNull.Value)
        //        dLURate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Loading"));

        //    dGrandRate = dDiscountRate + dVatRate + dCSTRate; // +dFrRate + dLURate;


        //    dSNetRate = dSNetRate + dNetRate;
        //    dSVatRate = dSVatRate + dVatRate;
        //    dSDiscountRate = dSDiscountRate + dDisRate;
        //    dSCSTRate = dSCSTRate + dCSTRate;

        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        //GridView gv = e.Row.FindControl("gvSecond") as GridView;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        //Label lblLink = (Label)e.Row.FindControl("lblLink");

        //DataSet ds = new DataSet();

        //if (category == "Daywise")
        //{
        //    //DateTime startDate;
        //    //startDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "LinkName"));

        //    if (secondLevel == "Billwise")
        //        ds = bl.SecondLevelDaywiseBillWise(startDate, purReturn, intTrans, delNote);
        //    else if (secondLevel == "Modelwise")
        //        ds = bl.SecondLevelDaywiseModelWise(startDate, purReturn, intTrans, delNote);
        //    else if (secondLevel == "Brandwise")
        //        ds = bl.SecondLevelDaywiseBrandWise(startDate, purReturn, intTrans, delNote);
        //    else if (secondLevel == "Customerwise")
        //        ds = bl.SecondLevelDaywiseCustWise(startDate, purReturn, intTrans, delNote);
        //    else if (secondLevel == "Itemwise")
        //        ds = bl.SecondLevelDaywiseItemWise(startDate, purReturn, intTrans, delNote);
        //    else if (secondLevel == "Daywise")
        //        ds = bl.SecondLevelDaywiseDayWise(startDate, purReturn, intTrans, delNote);

        //    //lblLink.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName", "{0:dd/MM/yyyy}"));
        //}
        //else if (category == "Categorywise")
        //{
        //    if (secondLevel == "Billwise")
        ds = bl.SecondLevel(field2, Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote, GroupBy, "");
        //    else if (secondLevel == "Modelwise")
        //        ds = bl.SecondLevelCategorywiseModelWise("categoryname", Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote);
        //    else if (secondLevel == "Brandwise")
        //        ds = bl.SecondLevelCategorywiseBrandWise("categoryname", Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote);
        //    else if (secondLevel == "Customerwise")
        //        ds = bl.SecondLevelCategorywiseCustWise("categoryname", Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote);
        //    else if (secondLevel == "Itemwise")
        //        ds = bl.SecondLevelCategorywiseItemWise("categoryname", Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote);
        //    else if (secondLevel == "Daywise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), category, "categoryname", "BillDate", purReturn, intTrans, delNote);

        //}
        //else if (category == "Brandwise")
        //{
        //    if (secondLevel == "Billwise")
        //        ds = bl.SecondLevelBrandwiseBillWise(category, Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote);
        //    else if (secondLevel == "Modelwise")
        //        ds = bl.SecondLevelBrandwiseModelWise(category, Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote);
        //    else if (secondLevel == "Brandwise")
        //        ds = bl.SecondLevelBrandwiseBrandWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote);
        //    else if (secondLevel == "Customerwise")
        //        ds = bl.SecondLevelBrandWiseCustomerWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductDesc", "CustomerName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Itemwise")
        //        ds = bl.SecondLevelBrandWiseItemWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductDesc", "ProductName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Daywise")
        //        ds = bl.SecondLevelBrandWiseDayWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "ProductDesc", "BillDate", purReturn, intTrans, delNote);

        //}
        //else if (category == "Modelwise")
        //{
        //    if (secondLevel == "Billwise")
        //        ds = bl.SecondLevelModelwiseBillWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote);
        //    else if (secondLevel == "Modelwise")
        //        ds = bl.SecondLevelModelwiseModelWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote);
        //    else if (secondLevel == "Brandwise")
        //        ds = bl.SecondLevelModelwiseBrandWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote);
        //    else if (secondLevel == "Customerwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "Model", "CustomerName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Itemwise")
        //        ds = bl.SecondLevelModelWiseItemWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "Model", "ProductName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Daywise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Model", "BillDate", purReturn, intTrans, delNote);

        //    //ds = bl.SecondLevelModelwise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
        //}
        //else if (category == "Billwise")
        //{
        //    if (secondLevel == "Billwise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "BillNo", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Modelwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "Model", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Brandwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "ProductDesc", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Customerwise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "CustomerName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Itemwise")
        //        ds = bl.SecondLevelBillWiseItemWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "ProductName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Daywise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "BillNo", "BillDate", purReturn, intTrans, delNote);

        //    //ds = bl.SecondLevelBillwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LinkName")));
        //}
        //else if (category == "Customerwise")
        //{

        //    if (secondLevel == "Billwise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "BillNo", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Modelwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "Model", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Brandwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "ProductDesc", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Customerwise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "CustomerName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Itemwise")
        //        ds = bl.SecondLevelCustomerWiseItemWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "ProductName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Daywise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "BillDate", purReturn, intTrans, delNote);

        //    //ds = bl.SecondLevelCustomerwise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
        //}
        //else if (category == "Executivewise")
        //{
        //    if (secondLevel == "Billwise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "BillNo", purReturn, intTrans, delNote);
        //    //ds = bl.SecondLevelExecutivewise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
        //    //else if (secondLevel == "Modelwise")
        //    else if (secondLevel == "Modelwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "Model", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Brandwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "ProductDesc", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Customerwise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "CustomerName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Itemwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "ProductName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Daywise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "BillDate", purReturn, intTrans, delNote);

        //}
        ///*Start Itemwise*/
        //else if (category == "Itemwise")
        //{

        //    if (secondLevel == "Billwise")
        //        ds = bl.SecondLevelGeneralSalesItemWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "BillNo", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Modelwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "Model", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Brandwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "ProductDesc", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Customerwise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "CustomerName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Itemwise")
        //        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "ProductName", purReturn, intTrans, delNote);
        //    else if (secondLevel == "Daywise")
        //        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "BillDate", purReturn, intTrans, delNote);

        //    //var tempRow = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray;
        //    //Label lblProdName = (Label)e.Row.FindControl("lblProductName");
        //    //lblProdName.Text = " | " + DataBinder.Eval(e.Row.DataItem, "ProductName").ToString();
        //    //ds = bl.SecondLevelItemwise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));

        //}



        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((cmbDisplayCat.SelectedItem.Text) == "Daywise")
                    {
                        fLvlValueTemp = dr["billdate"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Categorywise")
                    {
                        fLvlValueTemp = dr["Categoryname"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Brandwise")
                    {
                        fLvlValueTemp = dr["productdesc"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Modelwise")
                    {
                        fLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Billwise")
                    {
                        fLvlValueTemp = dr["Billno"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Customerwise")
                    {
                        fLvlValueTemp = dr["Customername"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Executivewise")
                    {
                        fLvlValueTemp = dr["Executivename"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayCat.SelectedItem.Text) == "Itemwise")
                    {
                        fLvlValueTemp = dr["productname"].ToString().ToUpper().Trim();
                    }


                    if ((cmbDisplayItem.SelectedItem.Text) == "Brandwise")
                    {
                        sLvlValueTemp = dr["productdesc"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayItem.SelectedItem.Text) == "Modelwise")
                    {
                        sLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayItem.SelectedItem.Text) == "Billwise")
                    {
                        sLvlValueTemp = dr["Billno"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayItem.SelectedItem.Text) == "Customerwise")
                    {
                        sLvlValueTemp = dr["Customername"].ToString().ToUpper().Trim();
                    }
                    else if ((cmbDisplayItem.SelectedItem.Text) == "Itemwise")
                    {
                        sLvlValueTemp = dr["productname"].ToString().ToUpper().Trim();
                    }


                    //if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    //(sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    //{
                    //    DataRow dr_final889 = dt.NewRow();
                    //    dt.Rows.Add(dr_final889);

                    //    DataRow dr_final8 = dt.NewRow();

                    //    dr_final8[cmbDisplayCat.SelectedItem.Text] = "";
                    //    dr_final8[cmbDisplayItem.SelectedItem.Text] = "Total : " + sLvlValue;
                    //    dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));
                    //    dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));
                    //    dr_final8["Net Rate"] = (Convert.ToDouble(netTotal));
                    //    dr_final8["Discount Rate"] = (Convert.ToDouble(discountTotal));
                    //    dr_final8["Vat Rate"] = (Convert.ToDouble(vatTotal));
                    //    dr_final8["CST Rate"] = (Convert.ToDouble(cstTotal));
                    //    dr_final8["Freight"] = (Convert.ToDouble(freightTotal));
                    //    dr_final8["Loading/Unloading"] = (Convert.ToDouble(loadTotal));
                    //    dr_final8["Total"] = Convert.ToString(Convert.ToDecimal(producttot));

                    //    producttot = 0;
                    //    qtyTotal = 0;
                    //    netTotal = 0;
                    //    discountTotal = 0;
                    //    vatTotal = 0;
                    //    cstTotal = 0;
                    //    freightTotal = 0;
                    //    rateTotal = 0;
                    //    loadTotal = 0;
                    //    dt.Rows.Add(dr_final8);

                    //    DataRow dr_final888 = dt.NewRow();
                    //    dt.Rows.Add(dr_final888);
                    //}

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final879 = dt.NewRow();
                        dt.Rows.Add(dr_final879);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8[cmbDisplayCat.SelectedItem.Text] = "Total : " + fLvlValue;
                        dr_final8[cmbDisplayItem.SelectedItem.Text] = "";

                        dr_final8["Sales Rate"] = (Convert.ToDouble(rate1Total));
                        dr_final8["Qty"] = Convert.ToDouble(qty1Total);
                        dr_final8["Net Rate"] = (Convert.ToDouble(net1Total));
                        dr_final8["Discount Rate"] = (Convert.ToDouble(discount1Total));
                        dr_final8["Vat Rate"] = (Convert.ToDouble(vat1Total));
                        dr_final8["CST Rate"] = (Convert.ToDouble(cst1Total));
                        dr_final8["Freight"] = (Convert.ToDouble(freight1Total));
                        dr_final8["Loading/Unloading"] = (Convert.ToDouble(load1Total));
                        dr_final8["Total"] = Convert.ToDouble(brandTotal);



                        brandTotal = 0;
                        rate1Total = 0;
                        net1Total = 0;
                        vat1Total = 0;
                        discount1Total = 0;
                        cst1Total = 0;
                        freight1Total = 0;
                        load1Total = 0;
                        qty1Total = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12[cmbDisplayCat.SelectedItem.Text] = fLvlValueTemp;
                    dr_final12[cmbDisplayItem.SelectedItem.Text] = sLvlValueTemp;

                    dr_final12["Sales Rate"] = (Convert.ToDouble(dr["SRate"]));
                    dr_final12["Qty"] = Convert.ToDouble(dr["quantity"]);
                    dr_final12["Net Rate"] = (Convert.ToDouble(dr["NetRate"]));
                    dr_final12["Discount Rate"] = (Convert.ToDouble(dr["actualdiscount"]));
                    dr_final12["Vat Rate"] = (Convert.ToDouble(dr["ActualVat"]));
                    dr_final12["CST Rate"] = (Convert.ToDouble(dr["Actualcst"]));
                    dr_final12["Freight"] = (Convert.ToDouble(dr["sumfreight"]));
                    dr_final12["Loading/Unloading"] = (Convert.ToDouble(dr["Loading"]));
                    dr_final12["Total"] = (Convert.ToDouble(dr["NetRate"])) + (Convert.ToDouble(dr["ActualVat"])) + (Convert.ToDouble(dr["Actualcst"])) - (Convert.ToDouble(dr["actualdiscount"])) + Convert.ToDouble(dr["Loading"]) + (Convert.ToDouble(dr["sumfreight"]));

                    brandTotal = brandTotal + ((Convert.ToDouble(dr["NetRate"])) + (Convert.ToDouble(dr["ActualVat"])) + (Convert.ToDouble(dr["Actualcst"])) - (Convert.ToDouble(dr["actualdiscount"])) + Convert.ToDouble(dr["Loading"]) + (Convert.ToDouble(dr["sumfreight"])));
                    load1Total = load1Total + (Convert.ToDouble(dr["Loading"]));
                    freight1Total = freight1Total + (Convert.ToDouble(dr["sumfreight"]));
                    cst1Total = cst1Total + (Convert.ToDouble(dr["Actualcst"]));
                    vat1Total = vat1Total + (Convert.ToDouble(dr["ActualVat"]));
                    discount1Total = discount1Total + (Convert.ToDouble(dr["actualdiscount"]));
                    net1Total = net1Total + (Convert.ToDouble(dr["NetRate"]));
                    rate1Total = rate1Total + (Convert.ToDouble(dr["SRate"]));

                    loadTotal = loadTotal + (Convert.ToDouble(dr["Loading"]));
                    freightTotal = freightTotal + (Convert.ToDouble(dr["sumfreight"]));
                    cstTotal = cstTotal + (Convert.ToDouble(dr["Actualcst"]));
                    vatTotal = vatTotal + (Convert.ToDouble(dr["ActualVat"]));
                    discountTotal = discountTotal + (Convert.ToDouble(dr["actualdiscount"]));
                    netTotal = netTotal + (Convert.ToDouble(dr["NetRate"]));
                    rateTotal = rateTotal + (Convert.ToDouble(dr["SRate"]));

                    CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["quantity"]);
                    total = total + ((Convert.ToDouble(dr["NetRate"])) + (Convert.ToDouble(dr["ActualVat"])) + (Convert.ToDouble(dr["Actualcst"])) - (Convert.ToDouble(dr["actualdiscount"])) + Convert.ToDouble(dr["Loading"]) + (Convert.ToDouble(dr["sumfreight"])));
                    qty1Total = qty1Total + Convert.ToDouble(dr["quantity"]);


                    dt.Rows.Add(dr_final12);
                }
            }

            //DataRow dr_final879 = dt.NewRow();
            //dt.Rows.Add(dr_final879);

            //DataRow dr_final89 = dt.NewRow();
            //dr_final89[cmbDisplayCat.SelectedItem.Text] = "";
            //dr_final89[cmbDisplayItem.SelectedItem.Text] = "Total : " + sLvlValue;

            //dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));
            //dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));
            //dr_final89["Net Rate"] = (Convert.ToDouble(netTotal));
            //dr_final89["Discount Rate"] = (Convert.ToDouble(discountTotal));
            //dr_final89["Vat Rate"] = (Convert.ToDouble(vatTotal));
            //dr_final89["CST Rate"] = (Convert.ToDouble(cstTotal));
            //dr_final89["Freight"] = (Convert.ToDouble(freightTotal));
            //dr_final89["Loading/Unloading"] = (Convert.ToDouble(loadTotal));
            //dr_final89["Total"] = Convert.ToString(Convert.ToDecimal(producttot));

            //producttot = 0;
            //qtyTotal = 0;
            //netTotal = 0;
            //discountTotal = 0;
            //vatTotal = 0;
            //cstTotal = 0;
            //freightTotal = 0;
            //rateTotal = 0;
            //loadTotal = 0;

            //dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799[cmbDisplayCat.SelectedItem.Text] = "Total : " + fLvlValue;
            dr_final8799[cmbDisplayItem.SelectedItem.Text] = "";

            dr_final8799["Sales Rate"] = (Convert.ToDouble(rate1Total));
            dr_final8799["Qty"] = Convert.ToDouble(qty1Total);
            dr_final8799["Net Rate"] = (Convert.ToDouble(net1Total));
            dr_final8799["Discount Rate"] = (Convert.ToDouble(discount1Total));
            dr_final8799["Vat Rate"] = (Convert.ToDouble(vat1Total));
            dr_final8799["CST Rate"] = (Convert.ToDouble(cst1Total));
            dr_final8799["Freight"] = (Convert.ToDouble(freight1Total));
            dr_final8799["Loading/Unloading"] = (Convert.ToDouble(load1Total));
            dr_final8799["Total"] = Convert.ToDouble(brandTotal);


            brandTotal = 0;
            rate1Total = 0;
            net1Total = 0;
            vat1Total = 0;
            discount1Total = 0;
            cst1Total = 0;
            freight1Total = 0;
            load1Total = 0;
            qty1Total = 0;
            rateTot = 0;
            qty1Total = 0;

            brandTotal = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789[cmbDisplayCat.SelectedItem.Text] = "Grand Total : ";
            dr_final789[cmbDisplayItem.SelectedItem.Text] = "";

            dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal));
            dr_final789["Qty"] = Convert.ToDouble(CategoryqtyTotal);
            dr_final789["Net Rate"] = (Convert.ToDouble(netTotal));
            dr_final789["Discount Rate"] = (Convert.ToDouble(discountTotal));
            dr_final789["Vat Rate"] = (Convert.ToDouble(vatTotal));
            dr_final789["CST Rate"] = (Convert.ToDouble(cstTotal));
            dr_final789["Freight"] = (Convert.ToDouble(freightTotal));
            dr_final789["Loading/Unloading"] = (Convert.ToDouble(loadTotal));
            dr_final789["Total"] = Convert.ToDouble(total);

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            bindData();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    public void ExportToExcel()
    {

        try
        {
            Response.Clear();

            Response.Buffer = true;

            string file = "Sales Summary Report_" + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition",

             "attachment;filename=" + file);

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);





            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "&nbsp;";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvMain);


            tr1.Cells.Add(cell1);

            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);



            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);



            tb.RenderControl(hw);


            string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            Response.Write(style);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();

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
            divmain.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                divPrint.Visible = true;
                divmain.Visible = true;

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DateTime startDate, endDate;
                string category = string.Empty;


                startDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                endDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                category = Convert.ToString(cmbDisplayCat.SelectedItem.Text);
                string intTrans = "";
                string purRet = "";
                string delNote = "";

                if (chkIntTrans.Checked)
                    intTrans = "YES";
                else
                    intTrans = "NO";

                if (chkPurReturn.Checked)
                    purRet = "YES";
                else
                    purRet = "NO";

                if (chkDelNote.Checked)
                    delNote = "YES";
                else
                    delNote = "NO";

                cond = Request.QueryString["cond"].ToString();
                cond = Server.UrlDecode(cond);

                DataSet BillDs = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (category == "Daywise")
                {
                    BillDs = bl.FirstLevelDaywise(startDate, endDate, purRet, intTrans,"", delNote, cond);

                }
                else if (category == "Categorywise")
                {
                    BillDs = bl.FirstLevelCategorywise(startDate, endDate, purRet, intTrans, delNote,"", cond);
                }
                else if (category == "Brandwise")
                {
                    BillDs = bl.FirstLevelBrandwise(startDate, endDate, purRet, intTrans, delNote,"", cond);
                }
                else if (category == "Modelwise")
                {
                    BillDs = bl.FirstLevelModelwise(startDate, endDate, purRet, intTrans, delNote, "", cond);
                }
                else if (category == "Billwise")
                {
                    BillDs = bl.FirstLevelBillwise(startDate, endDate, purRet, intTrans, delNote, "", cond);
                }
                else if (category == "Customerwise")
                {
                    BillDs = bl.FirstLevelCustomerwise(startDate, endDate, purRet, intTrans, delNote, "", cond);
                }
                else if (category == "Executivewise")
                {
                    BillDs = bl.FirstLevelExecutivewise(startDate, endDate, purRet, intTrans, delNote, "", cond);
                }
                /*Start Itemwise*/
                else if (category == "Itemwise")
                {
                    BillDs = bl.FirstLevelItemwise(startDate, endDate, purRet, intTrans, delNote, "", cond);
                }
                /*End Itemwise*/
                gvMain.DataSource = BillDs;
                gvMain.DataBind();

                div1.Visible = false;
                //ExportToExcel();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    public void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dNetRate = 0;
            double dVatRate = 0;
            double dDisRate = 0;
            double dCSTRate = 0;
            double dFrRate = 0;
            double dLURate = 0;

            double dGrandRate = 0;
            double dDiscountRate = 0;
            lblErr.Text = "";
            string cond6 = "";
            cond6 = getCond6();
            string category = string.Empty;

            string category1 = string.Empty;

            category = Convert.ToString(cmbDisplayCat.SelectedItem.Text);

            if (Request.QueryString["command"] != null)
            {
                string command = Request.QueryString["command"].ToString();
                if (command != "annualsales")
                {
                    category1 = "Datewise";
                }
                else if (command == "annualsales")
                {
                    category1 = "Monthwise";
                }
            }
            var secondLevel = cmbDisplayItem.SelectedItem.Text.Trim();

            if (Request.QueryString["category"] != null)
            {
                category = Request.QueryString["category"].ToString();
            }
            if (Request.QueryString["secondLevel"] != null)
            {
                secondLevel = Request.QueryString["secondLevel"].ToString();
            }

            secondLevel = "Itemwise";



            string intTrans = "";
            string purReturn = "";
            string delNote = "";

            if (chkIntTrans.Checked)
                intTrans = "YES";
            else
                intTrans = "NO";

            if (chkPurReturn.Checked)
                purReturn = "YES";
            else
                purReturn = "NO";

            if (chkDelNote.Checked)
                delNote = "YES";
            else
                delNote = "NO";

            if (Request.QueryString["intTrans"] != null)
            {
                intTrans = Request.QueryString["intTrans"].ToString();
            }
            if (Request.QueryString["purRet"] != null)
            {
                purReturn = Request.QueryString["purRet"].ToString();
            }
            if (Request.QueryString["delNote"] != null)
            {
                delNote = Request.QueryString["delNote"].ToString();
            }


            DateTime stDate, eDate;
            DateTime stdt = Convert.ToDateTime(txtStartDate.Text);
            DateTime etdt = Convert.ToDateTime(txtEndDate.Text);

            if (Request.QueryString["startDate"] != null)
            {
                stdt = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
            }
            if (Request.QueryString["endDate"] != null)
            {
                etdt = Convert.ToDateTime(Request.QueryString["endDate"].ToString());
            }

            stDate = Convert.ToDateTime(stdt);
            eDate = Convert.ToDateTime(etdt);




            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = category1.Replace("wise", "");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // To hide the first column
                string vToCheck = e.Row.Cells[0].ToString();
                if (string.IsNullOrEmpty(vToCheck))
                {
                    //  gvMain.Columns[0].Visible = false;
                    gvMain.Rows[0].Visible = false;
                }

                //    System.Web.UI.HtmlControls.HtmlImage Plus = (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("imdivTotal : 25/06/2015 00:00:00  ");
                // Image img = (Image)e.Row.FindControl("imdiv  ");

                // img.ImageUrl = "Images/plus.gif";
                //  img.Visible = false;
                //  Plus.Visible = false;

                if (DataBinder.Eval(e.Row.DataItem, "billSales") != DBNull.Value)
                    dNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "billSales"));


                //e.Row.Cells[0].Controls[0].Visible = false;

                if (dNetRate == Convert.ToDouble("0.00"))
                {
                    e.Row.Cells[0].Controls[0].Visible = false;


                    //System.Web.UI.WebControls.ImageButton imgBtnDelete = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imdiv");
                    //e.Row.FindControl("imgBtnDelete").Visible = false;

                    //e.Row.Cells[0].Visible = false;

                    //Image img = (Image)e.Row.FindControl("imdiv");
                    //if (img != null)
                    //{
                    //    img.Visible = false;
                    //}

                    //System.Web.UI.HtmlControls.HtmlImage Plus = (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("imdiv TOTAL : ");
                    //Image img = (Image)e.Row.FindControl("imdiv");
                    //if (img != null)
                    //{
                    //    img.ImageUrl = "App_Themes/DefaultTheme/Images/plus.gif";
                    //    img.Visible = false;
                    //}

                    //Plus.Visible = false;
                    //gvMain.Columns.Clear();
                    //  gvMain.Rows.Count=
                    //  gvMain.Columns[0].Visible = false;
                    //gvMain.Rows[0].ID = false;

                    // gvMain.Rows[3].Visible = false;
                }
                if (DataBinder.Eval(e.Row.DataItem, "Quantity") != DBNull.Value)
                    dVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                if (DataBinder.Eval(e.Row.DataItem, "Managementprofit") != DBNull.Value)
                    dDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Managementprofit"));
                if (DataBinder.Eval(e.Row.DataItem, "BranchProfit") != DBNull.Value)
                    dDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "BranchProfit"));
                //if (DataBinder.Eval(e.Row.DataItem, "ActualCST") != DBNull.Value)
                //    dCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));
                //if (DataBinder.Eval(e.Row.DataItem, "SumFreight") != DBNull.Value)
                //    dFrRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumFreight"));
                //if (DataBinder.Eval(e.Row.DataItem, "Loading") != DBNull.Value)
                //    dLURate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Loading"));

                dSNetRate = dSNetRate + dNetRate;
                dSVatRate = dSVatRate + dVatRate;
                dSDiscountRate = dSDiscountRate + dDisRate;
                dSCSTRate = dSCSTRate + dDiscountRate;

                //    dGrandRate = dNetRate - dDisRate;
                // dGrandRate = dDiscountRate + dVatRate + dCSTRate;// +dFrRate + dLURate;




                GridView gv = e.Row.FindControl("gvSecond") as GridView;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                HyperLink lblLink = (HyperLink)e.Row.FindControl("lblLink");
                Label lblBranchCode = (Label)e.Row.FindControl("lblBranchCode");
                Label lblBillNo = (Label)e.Row.FindControl("lblBillNo");
                DataSet ds = new DataSet();
                string brcode = "tblSales.BranchCode='" + lblBranchCode.Text + "'";
                if (category == "Daywise")
                {
                    string start1 = string.Empty;
                    DateTime startDate;
                    // var start1 = startDate.ToString("yyyy-MM-dd");

                    startDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "LinkName"));


                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelDaywiseModelWise(startDate, purReturn, intTrans, delNote, "", brcode, lblBillNo.Text);

                    //else if (secondLevel == "Modelwise")
                    //    ds = bl.SecondLevelDaywiseBillWise(startDate, purReturn, intTrans, delNote, brcode, lblBillNo.Text);
                    //else if (secondLevel == "Brandwise")
                    //    ds = bl.SecondLevelDaywiseBrandWise(startDate, purReturn, intTrans, delNote, brcode, lblBillNo.Text);
                    //else if (secondLevel == "Customerwise")
                    //    ds = bl.SecondLevelDaywiseCustWise(startDate, purReturn, intTrans, delNote, brcode, lblBillNo.Text);
                    else if (secondLevel == "Itemwise")

                        if (Request.QueryString["command"] != null)
                        {
                            string command = Request.QueryString["command"].ToString();
                            if (command != "annualsales")
                            {
                                ds = bl.SecondLevelDaywiseItemWisedashboardNEW(startDate, purReturn, intTrans, delNote, brcode, startDate);
                            }
                            else if (command == "annualsales")
                            {
                                var endDate1 = startDate.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                                ds = bl.SecondLevelDaywiseItemWisedashboardNEWAnnual(startDate, purReturn, intTrans, delNote, brcode, startDate, lastDayOfTheMonth);
                            }
                        }
                    //else if (secondLevel == "Daywise")
                    //    ds = bl.SecondLevelDaywiseDayWise(startDate, purReturn, intTrans, delNote, cond);

                    if (Request.QueryString["command"] != null)
                    {
                        string command = Request.QueryString["command"].ToString();
                        if (command == "annualsales")
                        {
                            DateTime thisMonth = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "LinkName", "{0:dd/MM/yyyy}"));
                            lblLink.Text = thisMonth.ToString("MMMM", new CultureInfo("en-GB"));
                        }
                        else if (command != "annualsales")
                        {
                            lblLink.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName", "{0:dd/MM/yyyy}"));
                        }
                    }
                }
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        gv.DataBind();
                    }
                }

                Label lblFreightRate = (Label)e.Row.FindControl("lblFreightRate");
                Label lblLURate = (Label)e.Row.FindControl("lblLURate");
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                //lblFreightRate.Text = dSCFrRate.ToString("f2");
                //lblLURate.Text = dSCLURate.ToString("f2");
                //dSFrRate = dSFrRate + dSCFrRate;
                //dSLURate = dSLURate + dSCLURate;
                //dGrandRate = dGrandRate + dSCFrRate + dSCLURate;
                //dSGrandRate = dSGrandRate + dGrandRate;
                //lblTotal.Text = dGrandRate.ToString("f2");
                dGrandRate = 0;
                dSCFrRate = 0;
                dSCLURate = 0;



            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[0].Text = "Grand Total ";
                e.Row.Cells[3].Text = totalll.Value;
                e.Row.Cells[2].Text = qtyy.Value;
                e.Row.Cells[4].Text = mangg.Value;
                e.Row.Cells[5].Text = brnchh.Value;
                //  e.Row.Cells[7].Text = dSFrRate.ToString("f2");
                // e.Row.Cells[8].Text = dSLURate.ToString("f2");
                // e.Row.Cells[9].Text = dSGrandRate.ToString("f2");
                strBillno = "";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void gvSecond_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dNetRate = 0;
            double dVatRate = 0;
            double dDisRate = 0;
            double dCSTRate = 0;
            double dFrRate = 0;
            double dLURate = 0;
            double mana = 0;
            double bnch = 0;
            double dQuantity = 0;
            double dGrandRate = 0;
            string itemcode = string.Empty;
            string itemcode1 = string.Empty;
            string second1 = string.Empty;
            int billno = 0;
            double dDiscountRate = 0;
            double dLURate1 = 0;
            string dLURate2 = string.Empty;

            lblErr.Text = "";

            var secondLevel = cmbDisplayItem.SelectedItem.Text.Trim();

            secondLevel = "Itemwise";

            second1 = "BillNowise";

            if (Request.QueryString["secondLevel"] != null)
            {
                secondLevel = Request.QueryString["secondLevel"].ToString();
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = second1.Replace("wise", "");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (DataBinder.Eval(e.Row.DataItem, "Quantity") != DBNull.Value)
                    dQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                if (DataBinder.Eval(e.Row.DataItem, "GroupItem") != DBNull.Value)
                    itemcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "GroupItem"));
                if (DataBinder.Eval(e.Row.DataItem, "item") != DBNull.Value)
                    itemcode1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "item"));
                if (DataBinder.Eval(e.Row.DataItem, "BillNo") != DBNull.Value)
                    billno = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "BillNo"));

                if (DataBinder.Eval(e.Row.DataItem, "NetRate") != DBNull.Value)
                    dNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualVat") != DBNull.Value)
                    dVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualVat"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualDiscount") != DBNull.Value)
                    dDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "SalesDiscount") != DBNull.Value)
                    dDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualCST") != DBNull.Value)
                    dCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));
                //if (DataBinder.Eval(e.Row.DataItem, "Managementprofit") != DBNull.Value)
                //    mana = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Managementprofit"));
                //if (DataBinder.Eval(e.Row.DataItem, "BranchProfit") != DBNull.Value)
                //    bnch = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "BranchProfit"));



                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label lblTotal1 = (Label)e.Row.FindControl("lblTotalmrp");
                Label lblTotal2 = (Label)e.Row.FindControl("lblTotaldp");
                // Label lblTotal3 = (Label)e.Row.FindControl("lblTotalnlc");//lblTotalgp
                Label lblTotalgp = (Label)e.Row.FindControl("lblTotalgp");
                Label lblFreightRate = (Label)e.Row.FindControl("lblFreightRate");
                Label lblLURate = (Label)e.Row.FindControl("lblLURate");
                Label management = (Label)e.Row.FindControl("lblmanagement");
                Label branch = (Label)e.Row.FindControl("lblbranch");

                dGrandRate = dNetRate - dDisRate;// +dFrRate + dLURate;

                dSQty = dSQty + dQuantity;
                dSCNetRate = dSCNetRate + dNetRate;
                dSCVatRate = dSCVatRate + dVatRate;
                dSCDiscountRate = dSCDiscountRate + dDisRate;
                dSCCSTRate = dSCCSTRate + dCSTRate;


                dSCGrandRate = dSCGrandRate + dGrandRate - dSCDiscountRate;
                lblTotal.Text = dGrandRate.ToString("f2");
                //foreach (string str2 in dLURate2)




                /*
                if ( (DataBinder.Eval(e.Row.DataItem, "GroupItem")).ToString().Trim() == tempBillno)
                {
                    //lblFreightRate.Visible = false;
                    //lblLURate.Visible = false;
                    
                }
                else
                {*/
                strBillno = strBillno + tempBillno + ",";
                string delim = ",";
                char[] delimA = delim.ToCharArray();
                string[] arr = strBillno.Split(delimA);
                int chkcnt = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].ToString() != "")
                    {
                        if ((DataBinder.Eval(e.Row.DataItem, "GroupItem")).ToString().Trim() != arr[i].Trim())
                        {
                            chkcnt = 0;
                        }
                        else
                        {
                            chkcnt = chkcnt + 1;
                            //break;
                        }
                    }
                }
                DataSet ds = new DataSet();
                BusinessLogic bl = new BusinessLogic();
                string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                string Branch = Request.QueryString["BranchCode"].ToString();

                ds = bl.getpricefordashboard(connection, billno, Branch, itemcode1);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drt in ds.Tables[0].Rows)
                    {
                        int price = Convert.ToInt32(drt["Price"].ToString());
                        string pricename = drt["PriceName"].ToString();
                        string item = drt["Itemcode"].ToString();
                        int bill = Convert.ToInt32(drt["billno"].ToString());
                        int originalprice = Convert.ToInt32(drt["TotalMrp"].ToString());


                        string item123 = pricename;
                        if ((itemcode1 == item) && (billno == bill))
                        {
                            management.Text = Convert.ToDouble(originalprice - price).ToString("#0.00");
                            dSCmgmtpfofit = dSCmgmtpfofit + Convert.ToDouble(management.Text);
                        }
                        DataSet ds1 = bl.getpricefordashboard1(connection, billno, Branch, itemcode1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drtt in ds1.Tables[0].Rows)
                            {
                                int price1 = Convert.ToInt32(drtt["Price"].ToString());
                                string pricename1 = drtt["PriceName"].ToString();
                                string item1 = drtt["Itemcode"].ToString();
                                int bill1 = Convert.ToInt32(drtt["billno"].ToString());
                                int originalprice1 = Convert.ToInt32(drtt["TotalMrp"].ToString());


                                //  string item123 = pricename;
                                if ((itemcode1 == item1) && (billno == bill1))
                                {
                                    branch.Text = Convert.ToDouble(originalprice1 - price1).ToString("#0.00");
                                    dSCranchprofit = dSCranchprofit + Convert.ToDouble(branch.Text);
                                }
                            }
                        }

                        if ((itemcode == item) && (billno == bill))
                        {
                            if (item123 == "MRP")
                            {
                                lblTotal1.Text = Convert.ToString(drt["Price"].ToString());
                            }
                            // if (item123 == "NLC")
                            //  {
                            //    lblTotal3.Text = Convert.ToString(drt["Price"].ToString());
                            //   lblTotalgp.Text = Convert.ToString(dGrandRate - (price * dQuantity));
                            //  }
                            if (item123 == "DP")
                            {
                                lblTotal2.Text = Convert.ToString(drt["Price"].ToString());
                                lblTotalgp.Text = Convert.ToString(dGrandRate - (price * dQuantity));
                            }
                        }
                        //lblTotalgp.Text =Convert.ToInt32(lblTotal.Text - lblTotal3.Text);
                    }
                }

                if (true)
                {
                    if (lblFreightRate.Text.Trim() != "")
                    {
                        dSCFrRate = dSCFrRate + Convert.ToDouble(lblFreightRate.Text.Trim());
                        dGrandRate = dGrandRate + Convert.ToDouble(lblFreightRate.Text.Trim());
                    }
                    if (lblLURate.Text.Trim() != "")
                    {
                        dSCLURate = dSCLURate + Convert.ToDouble(lblLURate.Text.Trim());
                        dGrandRate = dGrandRate + Convert.ToDouble(lblLURate.Text.Trim());
                    }
                }


                lblFreightRate.Visible = true;
                lblLURate.Visible = true;
                tempBillno = DataBinder.Eval(e.Row.DataItem, "GroupItem").ToString().Trim();
                //}

                // }




                // dGrandRate = dGrandRate + dDiscountRate + dVatRate + dCSTRate;


                //  lblTotal1.Text = dLURate1.ToString("f2");
                dGrandRate = 0;
                //e.Row.Cells[6].Visible = false;
                //e.Row.Cells[7].Visible = false;

                cnt = cnt + 1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {


                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
                //dSCGrandRate = dSCGrandRate + dSCFrRate + dSCLURate;

                e.Row.Cells[6].Text = dSQty.ToString();
                e.Row.Cells[7].Text = dSCNetRate.ToString("f2");
                e.Row.Cells[8].Text = dSCDiscountRate.ToString("f2");
                e.Row.Cells[9].Text = dSCVatRate.ToString("f2");
                e.Row.Cells[10].Text = dSCCSTRate.ToString("f2");
                e.Row.Cells[11].Text = dSCFrRate.ToString("f2");
                e.Row.Cells[12].Text = dSCLURate.ToString("f2");
                e.Row.Cells[13].Text = dSCGrandRate.ToString("f2");
                e.Row.Cells[19].Text = dSCmgmtpfofit.ToString("f2");// dSCGrandRate.ToString("f2");
                e.Row.Cells[20].Text = dSCranchprofit.ToString("f2");

                dSCDiscountRate = 0;
                dSCNetRate = 0;
                dSCVatRate = 0;
                dSQty = 0;
                dSCCSTRate = 0;
                //dSCFrRate = 0;
                //dSCLURate = 0;
                dSCGrandRate = 0;
                dSCmgmtpfofit = 0;
                dSCranchprofit = 0;

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
            //string filename = "Sales Report.xls";
            string filename = "Sales Summary" + DateTime.Now.ToString() + ".xls";
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

    DateTime firstDayOfTheMonth;
    DateTime lastDayOfTheMonth;

    public void bindDataDateWiseNormal()
    {
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        DataTable dt = new DataTable("sales report");
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryQtyTotal = 0;
        double dateTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double Tottot = 0;
        double dailysalestot = 0;
        double dpTotal = 0;
        double Totgp = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        int year = DateTime.Now.Year;
        var firstDay = new DateTime(year, 1, 1);
        var firstday1 = firstDay.ToString("yyyy-MM-dd");
        var lastDay = new DateTime(year, 12, 31);
        var lastday1 = lastDay.ToString("yyyy-MM-dd");

        double rateTotal = 0;
        double rateqtyTotal = 0;
        string Branch = string.Empty;

        string GroupBy1 = string.Empty;
        string condi = string.Empty;
        string condii = string.Empty;

        string connection = Request.Cookies["Company"].Value;
        connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic();

        if (Request.QueryString["BranchCode"] != null)
        {
            Branch = Request.QueryString["BranchCode"].ToString();
        }
        if (Request.QueryString["command"] != null)
        {
            string command = Request.QueryString["command"].ToString();
            string intTrans = "";
            string purRet = "";
            string delNote = "";
            intTrans = "NO";
            purRet = "NO";
            delNote = "NO";
            //  startDate = Convert.ToDateTime(txtstdate.Text);
            //  endDate = Convert.ToDateTime(txteddate.Text);
            string Types = string.Empty;

            Types = "DateWise";
            string options = string.Empty;
            string sCustomer = string.Empty;


            if (command == "dailysales")
            {
                string todaydate = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                condi = "tblsales.billdate='" + todaydate + "'";
                condii = "s.billdate='" + todaydate + "'";
                GroupBy1 = "billdate,";

                head.Text = "Today's Sales - Real-Time Summary Report -";
                lbl.Text = Branch;
                date.Text = Convert.ToString(DateTime.Now.ToString("dd-MM-yyyy"));

                DataSet dsd = bl.GetBranch(connection, usernam);
                {
                    ds = bl.FirstLevelDaywise(connection, purRet, intTrans, delNote, Branch, condi);
                }
                objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            }

            if (command == "monthlysales")
            {
                string sMonth1 = DateTime.Now.ToString("MM");
                DateTime now1 = DateTime.Now;
                var startDate1 = new DateTime(now1.Year, now1.Month, 1);
                var start1 = startDate1.ToString("yyyy-MM-dd");
                var fromdate = startDate1.ToString("dd-MM-yyyy");
                var endDate1 = startDate1.AddMonths(1).AddDays(-1);
                var end1 = endDate1.ToString("yyyy-MM-dd");
                var enddate2 = endDate1.ToString("dd-MM-yyyy");

                condi = "tblsales.billdate>='" + start1 + "' and tblsales.billdate<='" + end1 + "'";
                condii = "s.billdate>='" + start1 + "' and s.billdate<='" + end1 + "'";
                GroupBy1 = "billdate,";

                head.Text = "This Month's Sales - Real-Time Summary Report -";
                lbl.Text = Branch;
                date.Text = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(now1.Month) + ", " + now1.Year;
                //date.Text = "Date From " + fromdate + " and end date " + enddate2 + "";

                DataSet dsd = bl.GetBranch(connection, usernam);
                {
                    ds = bl.FirstLevelDaywise(connection, purRet, intTrans, delNote, Branch, condi);
                }
                objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            }
            if (command == "annualsales")
            {
                int year1 = DateTime.Now.Year;
                var firstDay1 = new DateTime(year, 4, 1);
                var firstday11 = firstDay1.ToString("yyyy-MM-dd");
                var start11 = firstDay1.ToString("yyyy-MM-dd");
                int year11 = year1 + 1;
                var lastDay1 = new DateTime(year11, 3, 31);
                var lastday11 = lastDay1.ToString("yyyy-MM-dd");
                var enddate22 = lastDay1.ToString("dd-MM-yyyy");



                //

                condi = "tblsales.billdate>='" + firstday11 + "' and tblsales.billdate<='" + lastday11 + "'";
                condii = "s.billdate>='" + firstday11 + "' and s.billdate<='" + lastday11 + "'";
                GroupBy1 = "billdate,";

                head.Text = "Annual Sales - Real-Time Summary Report -";
                lbl.Text = Branch;
                //date.Text = "Date From " + start11 + " and end date " + enddate22 + "";
                date.Text = Convert.ToString(firstDay1.Year);

                DataSet dmon = bl.GetMonth(connection, Convert.ToDateTime(firstday11), Convert.ToDateTime(lastday11));

                if (dmon.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < dmon.Tables[0].Rows.Count; k++)
                    {
                        int year12 = DateTime.Now.Year;

                        int year13 = year12 + 1;

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "April")
                        {
                            firstDayOfTheMonth = new DateTime(year12, 04, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "May")
                        {
                            firstDayOfTheMonth = new DateTime(year12, 05, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "June")
                        {
                            firstDayOfTheMonth = new DateTime(year12, 06, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "July")
                        {
                            firstDayOfTheMonth = new DateTime(year12, 07, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "August")
                        {
                            firstDayOfTheMonth = new DateTime(year12, 08, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "September")
                        {
                            firstDayOfTheMonth = new DateTime(year12, 09, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "October")
                        {
                            firstDayOfTheMonth = new DateTime(year12, 10, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "November")
                        {
                            firstDayOfTheMonth = new DateTime(year12, 11, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "December")
                        {
                            firstDayOfTheMonth = new DateTime(year12, 12, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "January")
                        {
                            firstDayOfTheMonth = new DateTime(year13, 01, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "February")
                        {
                            firstDayOfTheMonth = new DateTime(year13, 02, 1);
                        }

                        if (dmon.Tables[0].Rows[k]["MonthName"].ToString() == "March")
                        {
                            firstDayOfTheMonth = new DateTime(year13, 03, 1);
                        }

                        ds1 = bl.FirstLevelDaywiseAnnual(connection, purRet, intTrans, delNote, Branch, Convert.ToDateTime(firstDayOfTheMonth.ToString("yyyy-MM-dd")));
                        if (ds1 != null)
                        {
                            ds.Merge(ds1);
                        }
                    }
                }
            }
        }

        ////dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("LinkName"));
        // dt.Columns.Add(new DataColumn("TotalWORndOff"));
        dt.Columns.Add(new DataColumn("Quantity"));
        dt.Columns.Add(new DataColumn("Managementprofit"));
        dt.Columns.Add(new DataColumn("BranchProfit"));
        dt.Columns.Add(new DataColumn("BranchCode"));
        dt.Columns.Add(new DataColumn("billSales"));



        //   string Branch = string.Empty;
        //   Branch = DropDownList1.SelectedValue;

        // ds = objBL.getSaleslistNormal(startDate, endDate, Types, options, salrettype, Branch);

        //  ds = objBL.getallhistoryrate(sDataSource, ds, Branch);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //   DataRow dr_final11 = dt.NewRow();
                //  dt.Rows.Add(dr_final11);
                if (Request.QueryString["command"] != null)
                {
                    string command = Request.QueryString["command"].ToString();
                    if (command != "annualsales")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            fLvlValueTemp = dr["LinkName"].ToString();
                            if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                            {
                                // DataRow dr_final8886 = dt.NewRow();
                                //  dt.Rows.Add(dr_final8886);

                                DataRow dr_final8 = dt.NewRow();
                                // dr_final8["LinkName"] = "     TOTAL :   ";// "     TOTAL :   " + fLvlValue;
                                //dr_final8["billSales"] = Tottot;
                                //dr_final8["Managementprofit"] = dailysalestot;
                                ////   dr_final89["DailySalesfordp"] = "";
                                //dr_final8["BranchProfit"] = Totgp;
                                //dr_final8["Quantity"] = rateTotal;

                                //gpformrpTotal = gpformrpTotal + Tottot;


                                Tottot = 0;
                                dailysalestot = 0;
                                Totgp = 0;
                                dpvalueTotal = 0;
                                //  gpformrpTotal = 0;
                                //  gpfornlcTotal = 0;
                                // gpfordpTotal = 0;
                                // qtyTotal = 0;
                                rateTotal = 0;

                                dt.Rows.Add(dr_final8);

                                //  DataRow dr_final888 = dt.NewRow();
                                //  dt.Rows.Add(dr_final888);
                            }

                            fLvlValue = fLvlValueTemp;
                            DataRow dr_final113 = dt.NewRow();

                            ////dr_final113["BillNo"] = dr["BillNo"].ToString();
                            dr_final113["LinkName"] = dr["LinkName"].ToString();
                            dr_final113["BranchCode"] = dr["BranchCode"].ToString();
                            string branch1 = dr["BranchCode"].ToString();

                            //BusinessLogic bl = new BusinessLogic(sDataSource);
                            DataSet db = bl.getdetailedsalesreport1NEW(connection, dr["BranchCode"].ToString(), Convert.ToDateTime(dr["LinkName"].ToString()));
                            if (db != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow drd in db.Tables[0].Rows)
                                    {
                                        dr_final113["billSales"] = (Convert.ToDecimal(drd["billSales"])).ToString("#0.00");
                                        dr_final113["Managementprofit"] = ((Convert.ToDecimal(drd["billSales"])) - (Convert.ToDecimal(drd["DailySalesforgp"]))).ToString("#0.00");
                                        DataSet db1 = bl.getdetailedsalesreport2NEW(connection, dr["BranchCode"].ToString(), Convert.ToDateTime(dr["LinkName"].ToString()));
                                        if (db1 != null)
                                        {
                                            if (db1.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataRow drdd in db1.Tables[0].Rows)
                                                {
                                                    dr_final113["BranchProfit"] = ((Convert.ToDecimal(drd["billSales"])) - (Convert.ToDecimal(drdd["DailySalesfordp"]))).ToString("#0.00");
                                                }
                                            }
                                            else
                                            {
                                                dr_final113["DailySalesfordp"] = 0;
                                            }
                                        }
                                        dr_final113["Quantity"] = drd["Todaysalesquantity"].ToString();
                                    }
                                }
                                else
                                {
                                    dr_final113["DailySales"] = 0;
                                    dr_final113["DailySalesforgp"] = 0;
                                    dr_final113["DailySalesfordp"] = 0;
                                    dr_final113["Todaysalesquantity"] = 0;
                                }
                            }

                            else
                            {
                                dr_final113["DailySales"] = 0;
                                dr_final113["DailySalesforgp"] = 0;
                                dr_final113["DailySalesfordp"] = 0;
                                dr_final113["Todaysalesquantity"] = 0;
                            }
                            Tottot = Tottot + Convert.ToDouble(dr_final113["billSales"]);
                            gpformrpTotal = gpformrpTotal + Convert.ToDouble(dr_final113["billSales"]);
                            dailysalestot = dailysalestot + Convert.ToDouble(dr_final113["Managementprofit"]);
                            gpfornlcTotal = gpfornlcTotal + Convert.ToDouble(dr_final113["Managementprofit"]);
                            qtyTotal = qtyTotal + Convert.ToInt32(dr_final113["Quantity"]);
                            rateTotal = rateTotal + Convert.ToInt32(dr_final113["Quantity"]);
                            // dailyqty = dailyqty + Convert.ToDouble(dr_final113["BranchProfit"]);
                            //monthlyqty = monthlyqty + Convert.ToInt32(dr_final113["monthlysalesquantity"]);
                            Totgp = Totgp + (Convert.ToDouble(dr_final113["BranchProfit"]));
                            gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr_final113["BranchProfit"]));
                            //Totdp = Totdp + (Convert.ToDouble(dr_final113["DailySalesfordp"]));
                            //totgpmon = totgpmon + (Convert.ToDouble(dr_final113["montlySalesforgp"]));
                            //totdpmon = totdpmon + (Convert.ToDouble(dr_final113["montlySalesfordp"]));
                            dt.Rows.Add(dr_final113);

                        }
                    }
                    else if (command == "annualsales")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            fLvlValueTemp = dr["LinkName"].ToString();
                            if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                            {
                                // DataRow dr_final8886 = dt.NewRow();
                                //  dt.Rows.Add(dr_final8886);

                                DataRow dr_final8 = dt.NewRow();
                                // dr_final8["LinkName"] = "     TOTAL :   ";// "     TOTAL :   " + fLvlValue;
                                //dr_final8["billSales"] = Tottot;
                                //dr_final8["Managementprofit"] = dailysalestot;
                                ////   dr_final89["DailySalesfordp"] = "";
                                //dr_final8["BranchProfit"] = Totgp;
                                //dr_final8["Quantity"] = rateTotal;

                                //gpformrpTotal = gpformrpTotal + Tottot;


                                Tottot = 0;
                                dailysalestot = 0;
                                Totgp = 0;
                                dpvalueTotal = 0;
                                //  gpformrpTotal = 0;
                                //  gpfornlcTotal = 0;
                                // gpfordpTotal = 0;
                                // qtyTotal = 0;
                                rateTotal = 0;

                                dt.Rows.Add(dr_final8);

                                //  DataRow dr_final888 = dt.NewRow();
                                //  dt.Rows.Add(dr_final888);
                            }

                            fLvlValue = fLvlValueTemp;
                            DataRow dr_final113 = dt.NewRow();
                            string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(dr["LinkName"].ToString()));
                            ////dr_final113["BillNo"] = dr["BillNo"].ToString();
                            int year12 = DateTime.Now.Year;
                            int year13 = year12 + 1;



                            if (monthName == "April")
                            {
                                firstDayOfTheMonth = new DateTime(year12, 04, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "May")
                            {
                                firstDayOfTheMonth = new DateTime(year12, 05, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "June")
                            {
                                firstDayOfTheMonth = new DateTime(year12, 06, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "July")
                            {
                                firstDayOfTheMonth = new DateTime(year12, 07, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "August")
                            {
                                firstDayOfTheMonth = new DateTime(year12, 08, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "September")
                            {
                                firstDayOfTheMonth = new DateTime(year12, 09, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "October")
                            {
                                firstDayOfTheMonth = new DateTime(year12, 10, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "November")
                            {
                                firstDayOfTheMonth = new DateTime(year12, 11, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "December")
                            {
                                firstDayOfTheMonth = new DateTime(year12, 12, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "January")
                            {
                                firstDayOfTheMonth = new DateTime(year13, 01, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "February")
                            {
                                firstDayOfTheMonth = new DateTime(year13, 02, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }

                            if (monthName == "March")
                            {
                                firstDayOfTheMonth = new DateTime(year13, 03, 1);
                                var endDate1 = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                                lastDayOfTheMonth = Convert.ToDateTime(endDate1.ToString("yyyy-MM-dd"));
                            }


                            dr_final113["LinkName"] = firstDayOfTheMonth;// dr["LinkName"].ToString();
                            dr_final113["BranchCode"] = dr["BranchCode"].ToString();
                            string branch1 = dr["BranchCode"].ToString();

                            //BusinessLogic bl = new BusinessLogic(sDataSource);
                            DataSet db = bl.getdetailedsalesreport1NEWAnnual(connection, dr["BranchCode"].ToString(), Convert.ToDateTime(firstDayOfTheMonth.ToString("yyyy-MM-dd")), Convert.ToDateTime(lastDayOfTheMonth.ToString("yyyy-MM-dd")));
                            if (db != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow drd in db.Tables[0].Rows)
                                    {
                                        dr_final113["billSales"] = (Convert.ToDecimal(drd["billSales"])).ToString("#0.00");
                                        dr_final113["Managementprofit"] = ((Convert.ToDecimal(drd["billSales"])) - (Convert.ToDecimal(drd["DailySalesforgp"]))).ToString("#0.00");
                                        //DataSet db1 = bl.getdetailedsalesreport2NEW(connection, dr["BranchCode"].ToString(), Convert.ToDateTime(dr["LinkName"].ToString()));
                                        DataSet db1 = bl.getdetailedsalesreport2NEW(connection, dr["BranchCode"].ToString(), Convert.ToDateTime(firstDayOfTheMonth.ToString("yyyy-MM-dd")));
                                        if (db1 != null)
                                        {
                                            if (db1.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataRow drdd in db1.Tables[0].Rows)
                                                {
                                                    dr_final113["BranchProfit"] = ((Convert.ToDecimal(drd["billSales"])) - (Convert.ToDecimal(drdd["DailySalesfordp"]))).ToString("#0.00");
                                                }
                                            }
                                            else
                                            {
                                                dr_final113["DailySalesfordp"] = 0;
                                            }
                                        }
                                        else
                                        {
                                            dr_final113["BranchProfit"] = 0;
                                        }
                                        dr_final113["Quantity"] = drd["Todaysalesquantity"].ToString();
                                    }
                                }
                                else
                                {
                                    dr_final113["billSales"] = 0;
                                    dr_final113["Managementprofit"] = 0;
                                    dr_final113["BranchProfit"] = 0;
                                    dr_final113["Quantity"] = 0;
                                }
                            }

                            else
                            {
                                dr_final113["billSales"] = 0;
                                dr_final113["Managementprofit"] = 0;
                                dr_final113["BranchProfit"] = 0;
                                dr_final113["Quantity"] = 0;
                            }
                            Tottot = Tottot + Convert.ToDouble(dr_final113["billSales"]);
                            gpformrpTotal = gpformrpTotal + Convert.ToDouble(dr_final113["billSales"]);
                            dailysalestot = dailysalestot + Convert.ToDouble(dr_final113["Managementprofit"]);
                            gpfornlcTotal = gpfornlcTotal + Convert.ToDouble(dr_final113["Managementprofit"]);
                            qtyTotal = qtyTotal + Convert.ToInt32(dr_final113["Quantity"]);
                            rateTotal = rateTotal + Convert.ToInt32(dr_final113["Quantity"]);
                            // dailyqty = dailyqty + Convert.ToDouble(dr_final113["BranchProfit"]);
                            //monthlyqty = monthlyqty + Convert.ToInt32(dr_final113["monthlysalesquantity"]);
                            Totgp = Totgp + (Convert.ToDouble(dr_final113["BranchProfit"]));
                            gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr_final113["BranchProfit"]));
                            //Totdp = Totdp + (Convert.ToDouble(dr_final113["DailySalesfordp"]));
                            //totgpmon = totgpmon + (Convert.ToDouble(dr_final113["montlySalesforgp"]));
                            //totdpmon = totdpmon + (Convert.ToDouble(dr_final113["montlySalesfordp"]));
                            dt.Rows.Add(dr_final113);

                        }
                    }
                }
            }

            //   DataRow dr_final879 = dt.NewRow();
            //  dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            //dr_final89["LinkName"] = "     TOTAL :   ";// "     TOTAL :   " + fLvlValueTemp;
            //dr_final89["billSales"] = Tottot;
            //dr_final89["Managementprofit"] = dailysalestot;
            //   dr_final89["DailySalesfordp"] = "";
            //dr_final89["BranchProfit"] = Totgp;
            //dr_final89["Quantity"] = rateTotal;
            //   dr_final89["Itemcode"] = "";
            //  dr_final89["BillNo"] = "";


            Totgp = 0;
            dailysalestot = 0;
            Tottot = 0;
            dpvalueTotal = 0;
            // gpformrpTotal = 0;
            //  gpfornlcTotal = 0;
            // gpfordpTotal = 0;
            // qtyTotal = 0;
            rateTotal = 0;
            //dt.Rows.Add(dr_final89);

            //  DataRow dr_final8709 = dt.NewRow();
            //  dt.Rows.Add(dr_final8709);


            //  DataRow dr_final789 = dt.NewRow();
            // dr_final789["LinkName"] = "Grand Total : ";
            //   dr_final789["billSales"] = gpformrpTotal;
            //  dr_final789["Managementprofit"] = gpfornlcTotal;
            //  dr_final789["BranchProfit"] = gpfordpTotal;
            //   dr_final789["Quantity"] = qtyTotal;
            //   dr_final789["Model"] = "";
            //  dr_final789["Itemcode"] = "";
            //  dr_final789["BillNo"] = "";
            totalll.Value = Convert.ToString(gpformrpTotal.ToString("f2"));
            qtyy.Value = Convert.ToString(qtyTotal);
            mangg.Value = Convert.ToString(gpfornlcTotal.ToString("f2"));
            brnchh.Value = Convert.ToString(gpfordpTotal.ToString("f2"));



            //  dt.Rows.Add(dr_final789);
            DataSet dst = new DataSet();
            dst.Tables.Add(dt);
            gvMain.DataSource = dst;
            gvMain.DataBind();
            div1.Visible = false;

            // ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ExportToExcel(dt);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        //}
    }

}