using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportExlSales : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    BusinessLogic objBL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!IsPostBack)
            {

                //if (Request.Cookies["Company"] != null)
                //    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                //BusinessLogic bl = new BusinessLogic(sDataSource);

                objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

                txtstdate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();

                txteddate.Text = DateTime.Now.ToShortDateString();


                string usernam = Request.Cookies["LoggedUserName"].Value;
                if ((usernam == "mis1") || (usernam == "mis2") || (usernam == "mis3") || (usernam == "mis4") || (usernam == "mis5") || (usernam == "mis6") || (usernam == "mis7") || (usernam == "mis8") || (usernam == "mis9") || (usernam == "mis10"))
                {
                    chkboxNlcvalue.Enabled = false;
                    chkboxNlcper.Enabled = false;
                }
                else
                {
                    chkboxNlcvalue.Enabled = true;
                    chkboxNlcper.Enabled = true;
                }

                //loadBrands();
                loadCategory();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private bool isValidLevels()
    {
        if ((chkboxQty.Checked == false) && (chkboxVal.Checked == false))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select any one on aggregate funtions like: Qty,Value');", true);
            return false;
        }
        return true;
    }

    protected string getfield2()
    {
        string field2 = "";

        if (chkboxVal.Checked)
        {
            if (field2 == "")
            {

                field2 = "";
            }
            else
            {

                field2 += " , ";
            }
                
            field2 += "tblSalesItems.Rate";
        }
        if (chkboxQty.Checked)
        {
            if (field2 == "")
            {

                field2 = "";
            }
            else
            {

                field2 += " , ";
            }

            field2 += "tblSalesItems.Qty";
        }
        if (field2 != "")
        {
            field2 += ",((tblSalesItems.Qty)*(tblSalesItems.Rate)) As Amount";
        }
        return field2;
    }

    protected void btnReport_Click(object sender, EventArgs e)
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

    public void bindDataCategoryWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateqtyTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        string options = string.Empty;
        Types = "CategoryWise";
        options = opttype.SelectedItem.Text; 

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());



        DataSet dstotal = new DataSet();

        dt.Columns.Add(new DataColumn("CategoryName"));
        dt.Columns.Add(new DataColumn("Brand"));
        dt.Columns.Add(new DataColumn("ProductName"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if(withsalreturn.Checked==true)
            dt.Columns.Add(new DataColumn("VoucherType"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);
      

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8889 = dt.NewRow();
                        dt.Rows.Add(dr_final8889);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["CategoryName"] = "Total : " + fLvlValue;
                        dr_final8["ProductName"] = "";
                        dr_final8["Brand"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));


                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);
                        

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["CategoryName"] = dr["CategoryName"];
                    dr_final12["ProductName"] = dr["ProductName"];
                    dr_final12["Brand"] = dr["Brand"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    dr_final12["BillNo"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    //dr_final12["BillDate"] = dr["BillDate"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + dr["Qty"];
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + Convert.ToDouble(dr["amount"]);
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }


                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["amount"]))) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]);

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"])));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]);

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"])));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]);

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = (((Convert.ToDouble(dr["amount"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);   
                        rateTotal = rateTotal - Convert.ToDouble(dr["rate1"]);
                        rateqtyTotal = rateqtyTotal - Convert.ToDouble(dr["rate1"]);
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + Convert.ToDouble(dr["rate1"]);
                        rateqtyTotal = rateqtyTotal + Convert.ToDouble(dr["rate1"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                                                       
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final897 = dt.NewRow();
            dr_final897["CategoryName"] = "Total : " + fLvlValue;
            dr_final897["ProductName"] = "";
            dr_final897["Brand"] = "";
            dr_final897["Model"] = "";
            dr_final897["Itemcode"] = "";
            dr_final897["BillNo"] = "";
            dr_final897["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final897["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final897["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final897["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxAvg.Checked == true)
                dr_final897["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final897["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final897["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final897["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final897["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final897["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final897["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final897["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final897["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final897["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final897["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;
            dt.Rows.Add(dr_final897);

            DataRow dr_final88 = dt.NewRow();
            dt.Rows.Add(dr_final88);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["CategoryName"] = "Grand Total : ";
            dr_final789["ProductName"] = "";
            dr_final789["Brand"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillNo"] = "";
            dr_final789["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateqtyTotal));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            //dt.Rows.Add(dstotal);

            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }

        //if (ds != null)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        ExportToExcel(dt);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        //    }
        //}
    }

    public void bindDataCategoryWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        double rateTotal = 0;
        double rateTotal1 = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        string options = string.Empty;
        Types = "CategoryWise";
        options = opttype.SelectedItem.Text;

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        DataSet dstotal = new DataSet();

        dt.Columns.Add(new DataColumn("CategoryName"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);


        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["CategoryName"] = fLvlValue;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        dt.Rows.Add(dr_final8);

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["CategoryName"] = dr["CategoryName"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["amount"]))) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]);

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"])));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]);

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"])));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]);

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = (((Convert.ToDouble(dr["amount"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                }
            }

            DataRow dr_final897 = dt.NewRow();
            dr_final897["CategoryName"] = fLvlValue;

            if (chkboxQty.Checked == true)
                dr_final897["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final897["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final897["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxAvg.Checked == true)
                dr_final897["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final897["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final897["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final897["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final897["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final897["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final897["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final897["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final897["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final897["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final897["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            dt.Rows.Add(dr_final897);

            DataRow dr_final88 = dt.NewRow();
            dt.Rows.Add(dr_final88);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["CategoryName"] = "Grand Total ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    public void bindDataCategoryWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "CategoryWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("CategoryName"));

        if(chkboxQty.Checked==true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                
                //if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                //{
                //    DataRow dr_final8 = dt.NewRow();
                //    dr_final8["CategoryName"] = "Total : " + fLvlValue;
                //    dr_final8["Qty"] = "";
                //    dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
                //    CategoryTotal = 0;
                //    dt.Rows.Add(dr_final8);

                //    DataRow dr_final888 = dt.NewRow();
                //    dt.Rows.Add(dr_final888);
                //}
                fLvlValue = fLvlValueTemp;
                
                DataRow dr_final12 = dt.NewRow();
                dr_final12["CategoryName"] = dr["CategoryName"];

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = Convert.ToDouble(dr["amount"]);

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLCValue"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["NLCValue"]))/(Convert.ToDouble(dr["NLCValue"])));
                
                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRPValue"]);
                
                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["MRPValue"])) / (Convert.ToDouble(dr["MRPValue"])));
                
                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DPValue"]);
                
                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["DPValue"])) / (Convert.ToDouble(dr["DPValue"])));
               
                CategoryTotal = CategoryTotal +  Convert.ToDouble(dr["amount"]);
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + Convert.ToDouble(dr["amount"]);
                dt.Rows.Add(dr_final12);
            }
        }

        //DataRow dr_final879 = dt.NewRow();
        //dt.Rows.Add(dr_final879);

        //DataRow dr_final89 = dt.NewRow();
        //dr_final89["CategoryName"] = "Total : " + fLvlValueTemp;
        //dr_final89["Qty"] = "";
        //dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
        //dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["CategoryName"] = "Grand Total : ";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataProductWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "ProductWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Product Name"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                //if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                //{
                //    DataRow dr_final8 = dt.NewRow();
                //    dr_final8["CategoryName"] = "Total : " + fLvlValue;
                //    dr_final8["Qty"] = "";
                //    dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
                //    CategoryTotal = 0;
                //    dt.Rows.Add(dr_final8);

                //    DataRow dr_final888 = dt.NewRow();
                //    dt.Rows.Add(dr_final888);
                //}
                fLvlValue = fLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["Product Name"] = dr["ProductName"];

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = Convert.ToDouble(dr["amount"]);

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLCValue"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["NLCValue"])) / (Convert.ToDouble(dr["NLCValue"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRPValue"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["MRPValue"])) / (Convert.ToDouble(dr["MRPValue"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DPValue"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["DPValue"])) / (Convert.ToDouble(dr["DPValue"])));

                CategoryTotal = CategoryTotal + Convert.ToDouble(dr["amount"]);
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + Convert.ToDouble(dr["amount"]);
                dt.Rows.Add(dr_final12);
            }
        }

        //DataRow dr_final879 = dt.NewRow();
        //dt.Rows.Add(dr_final879);

        //DataRow dr_final89 = dt.NewRow();
        //dr_final89["CategoryName"] = "Total : " + fLvlValueTemp;
        //dr_final89["Qty"] = "";
        //dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
        //dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Product Name"] = "Grand Total : ";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataProductWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateqtyTotal = 0;

        Types = "ProductWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Product Name"));
        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Brand"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8886 = dt.NewRow();
                        dt.Rows.Add(dr_final8886);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Product Name"] = "Total : " + fLvlValue;
                        dr_final8["Category"] = "";
                        dr_final8["Brand"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillDate"] = "";
                        dr_final8["BillNo"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Product Name"] = dr["ProductName"];
                    dr_final12["Category"] = dr["CategoryName"];
                    dr_final12["Brand"] = dr["Brand"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    //dr_final12["BillDate"] = dr["BillDate"];
                    dr_final12["BillNo"] = dr["BillNo"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"])-((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"])-((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - Convert.ToDouble(dr["amount"]);
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal - Convert.ToDouble(dr["rate1"]);
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + Convert.ToDouble(dr["amount"]);
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal + Convert.ToDouble(dr["rate1"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Product Name"] = "Total : " + fLvlValueTemp;
            dr_final89["Category"] = "";
            dr_final89["Brand"] = "";
            dr_final89["Model"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["BillDate"] = "";
            dr_final89["BillNo"] = "";

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Product Name"] = "Grand Total : ";
            dr_final789["Category"] = "";
            dr_final789["Brand"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillDate"] = "";
            dr_final789["BillNo"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateqtyTotal));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);


            ExportToExcel(dt);
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

    public void bindDataProductWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        Types = "ProductWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Product Name"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Product Name"] = fLvlValue;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;
                        dt.Rows.Add(dr_final8);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Product Name"] = dr["ProductName"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - Convert.ToDouble(dr["amount"]);
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total + Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - Convert.ToDouble(dr["rate1"]);
                        rateTotal1 = rateTotal1 - Convert.ToDouble(dr["rate1"]);
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + Convert.ToDouble(dr["amount"]);
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + Convert.ToDouble(dr["rate1"]);
                        rateTotal1 = rateTotal1 + Convert.ToDouble(dr["rate1"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                }
            }

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Product Name"] = fLvlValueTemp;

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Product Name"] = "Grand Total ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);


            ExportToExcel(dt);
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


    public void bindDataBrandWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandTotal = 0;
        double brandqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double  rateTotal = 0;
        double  rateqtyTotal = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        Types = "BrandWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("CategoryName"));
        dt.Columns.Add(new DataColumn("ProductName"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);


        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8889 = dt.NewRow();
                        dt.Rows.Add(dr_final8889);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "Total : " + fLvlValue;
                        dr_final8["CategoryName"] = "";
                        dr_final8["ProductName"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";
                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        brandTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Brand Name"] = dr["Productdesc"];
                    dr_final12["CategoryName"] = dr["CategoryName"];
                    dr_final12["ProductName"] = dr["ProductName"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    dr_final12["BillNo"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    //dr_final12["BillDate"] = dr["BillDate"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - Convert.ToDouble(dr["amount"]);
                        brandqtyTotal = brandqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal - Convert.ToDouble(dr["rate1"]);
                    }
                    else
                    {
                        brandTotal = brandTotal + Convert.ToDouble(dr["amount"]);
                        brandqtyTotal = brandqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal + Convert.ToDouble(dr["rate1"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Brand Name"] = "Total : " + fLvlValue;
            dr_final89["CategoryName"] = "";
            dr_final89["ProductName"] = "";
            dr_final89["Model"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["BillNo"] = "";
            dr_final89["BillDate"] = "";
            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            brandTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Brand Name"] = "Grand Total : ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(brandqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateqtyTotal));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(brandqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataBrandWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandTotal = 0;
        double brandqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateqtyTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        Types = "BrandWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Brand Name"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = fLvlValue;

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        brandTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        rateTotal = 0;
                        qtyTotal = 0;

                        dt.Rows.Add(dr_final8);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Brand Name"] = dr["Productdesc"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - Convert.ToDouble(dr["amount"]);
                        brandqtyTotal = brandqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - Convert.ToDouble(dr["rate1"]);
                        rateqtyTotal = rateqtyTotal - Convert.ToDouble(dr["rate1"]);
                    }
                    else
                    {
                        brandTotal = brandTotal + Convert.ToDouble(dr["amount"]);
                        brandqtyTotal = brandqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + Convert.ToDouble(dr["amount"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + Convert.ToDouble(dr["rate1"]);
                        rateqtyTotal = rateqtyTotal + Convert.ToDouble(dr["rate1"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    
                }
            }

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Brand Name"] = fLvlValue;

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            brandTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Brand Name"] = "Grand Total  ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(brandqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateqtyTotal));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(brandqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataBrandWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;



        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "BrandWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Brand Name"));
        

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();

                //if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                //{
                //    DataRow dr_final8 = dt.NewRow();
                //    dr_final8["CategoryName"] = "Total : " + fLvlValue;
                //    dr_final8["Qty"] = "";
                //    dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
                //    CategoryTotal = 0;
                //    dt.Rows.Add(dr_final8);

                //    DataRow dr_final888 = dt.NewRow();
                //    dt.Rows.Add(dr_final888);
                //}
                fLvlValue = fLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["Brand Name"] = dr["Productdesc"];

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = Convert.ToDouble(dr["amount"]);

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLCValue"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["NLCValue"])) / (Convert.ToDouble(dr["NLCValue"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRPValue"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["MRPValue"])) / (Convert.ToDouble(dr["MRPValue"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DPValue"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["DPValue"])) / (Convert.ToDouble(dr["DPValue"])));

                CategoryTotal = CategoryTotal + Convert.ToDouble(dr["amount"]);
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + Convert.ToDouble(dr["amount"]);
                dt.Rows.Add(dr_final12);
            }
        }

        //DataRow dr_final879 = dt.NewRow();
        //dt.Rows.Add(dr_final879);

        //DataRow dr_final89 = dt.NewRow();
        //dr_final89["CategoryName"] = "Total : " + fLvlValueTemp;
        //dr_final89["Qty"] = "";
        //dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
        //dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Brand Name"] = "Grand Total : ";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataBrandProductWiseNormal()
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
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double qty1Total = 0;

        double mrpvalTotal = 0;
        double nlcvalTotal = 0;
        double dpvalTotal = 0;
        double gpfmrpTotal = 0;
        double gpfnlcTotal = 0;
        double gpfdpTotal = 0;

        Types = "BrandProductWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Product Name"));
        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final889 = dt.NewRow();
                        dt.Rows.Add(dr_final889);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "";
                        dr_final8["Product Name"] = "Total : " + sLvlValue;
                        dr_final8["Category"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        producttot = 0;
                        qtyTotal = 0;
                        gpfordpTotal = 0;
                        gpfornlcTotal = 0;
                        dpvalueTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "Total : " + fLvlValue;
                        dr_final8["Product Name"] = "";
                        dr_final8["Category"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTot));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

                        brandTotal = 0;
                        gpfdpTotal = 0;
                        gpfnlcTotal = 0;
                        dpvalTotal = 0;
                        gpfmrpTotal = 0;
                        mrpvalTotal = 0;
                        nlcvalTotal = 0;
                        rateTot = 0;
                        qty1Total = 0;

                        brandTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Brand Name"] = dr["Productdesc"];
                    dr_final12["Product Name"] = dr["ProductName"];
                    dr_final12["Category"] = dr["CategoryName"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    dr_final12["BillNo"] = dr["BillNo"];
                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;


                    //dr_final12["BillDate"] = dr["BillDate"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"])-((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"])-((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - (Convert.ToDouble(dr["amount"]));
                        producttot = producttot - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["amount"]));

                        qty1Total = qty1Total - Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                        producttot = producttot + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["amount"]));

                        qty1Total = qty1Total + Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalTotal = mrpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalTotal = nlcvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalTotal = dpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpfmrpTotal = gpfmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfnlcTotal = gpfnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfdpTotal = gpfdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Brand Name"] = "";
            dr_final89["Product Name"] = "Total : " + sLvlValue;
            dr_final89["Category"] = "";
            dr_final89["Model"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["BillNo"] = "";
            dr_final89["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

            if (chkboxAvg.Checked == true)
                dr_final89["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            producttot = 0;
            qtyTotal = 0;
            gpfordpTotal = 0;
            gpfornlcTotal = 0;
            dpvalueTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799["Brand Name"] = "Total : " + fLvlValue;
            dr_final8799["Product Name"] = "";
            dr_final8799["Category"] = "";
            dr_final8799["Model"] = "";
            dr_final8799["Itemcode"] = "";
            dr_final8799["BillNo"] = "";
            dr_final8799["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final8799["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

            if (chkboxrate.Checked == true)
                dr_final8799["Sales Rate"] = (Convert.ToDouble(rateTot));

            if (chkboxVal.Checked == true)
                dr_final8799["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final8799["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final8799["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final8799["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

            if (chkboxNlcper.Checked == true)
                dr_final8799["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final8799["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

            if (chkboxMRPper.Checked == true)
                dr_final8799["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final8799["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

            if (chkboxDpper.Checked == true)
                dr_final8799["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

            if (chkgpmrp.Checked == true)
                dr_final8799["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final8799["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final8799["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

            brandTotal = 0;
            gpfdpTotal = 0;
            gpfnlcTotal = 0;
            dpvalTotal = 0;
            gpfmrpTotal = 0;
            mrpvalTotal = 0;
            nlcvalTotal = 0;
            rateTot = 0;
            qty1Total = 0;

            brandTotal = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Brand Name"] = "Grand Total : ";
            dr_final789["Product Name"] = "";
            dr_final789["Category"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillNo"] = "";
            dr_final789["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataBrandProductWiseGroupBy()
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
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double qty1Total = 0;

        double mrpvalTotal = 0;
        double nlcvalTotal = 0;
        double dpvalTotal = 0;
        double gpfmrpTotal = 0;
        double gpfnlcTotal = 0;
        double gpfdpTotal = 0;

        Types = "BrandProductWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Product Name"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = fLvlValue;
                        dr_final8["Product Name"] = sLvlValue;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        producttot = 0;
                        qtyTotal = 0;
                        gpfordpTotal = 0;
                        gpfornlcTotal = 0;
                        dpvalueTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);


                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "Total : " + fLvlValue;
                        dr_final8["Product Name"] = "";


                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTot));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

                        brandTotal = 0;
                        gpfdpTotal = 0;
                        gpfnlcTotal = 0;
                        dpvalTotal = 0;
                        gpfmrpTotal = 0;
                        mrpvalTotal = 0;
                        nlcvalTotal = 0;
                        rateTot = 0;
                        qty1Total = 0;

                        brandTotal = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final88877 = dt.NewRow();
                        dt.Rows.Add(dr_final88877);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Brand Name"] = dr["Productdesc"];
                    dr_final12["Product Name"] = dr["ProductName"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - (Convert.ToDouble(dr["amount"]));
                        producttot = producttot - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["amount"]));

                        qty1Total = qty1Total - Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                        producttot = producttot + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["amount"]));

                        qty1Total = qty1Total + Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalTotal = mrpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalTotal = nlcvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalTotal = dpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpfmrpTotal = gpfmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfnlcTotal = gpfnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfdpTotal = gpfdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                }
            }

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Brand Name"] = fLvlValue;
            dr_final89["Product Name"] = sLvlValue;

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

            if (chkboxAvg.Checked == true)
                dr_final89["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            producttot = 0;
            qtyTotal = 0;
            gpfordpTotal = 0;
            gpfornlcTotal = 0;
            dpvalueTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799["Brand Name"] = "Total : " + fLvlValue;
            dr_final8799["Product Name"] = "";

            if (chkboxQty.Checked == true)
                dr_final8799["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

            if (chkboxrate.Checked == true)
                dr_final8799["Sales Rate"] = (Convert.ToDouble(rateTot));

            if (chkboxVal.Checked == true)
                dr_final8799["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final8799["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final8799["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final8799["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

            if (chkboxNlcper.Checked == true)
                dr_final8799["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final8799["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

            if (chkboxMRPper.Checked == true)
                dr_final8799["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final8799["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

            if (chkboxDpper.Checked == true)
                dr_final8799["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

            if (chkgpmrp.Checked == true)
                dr_final8799["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final8799["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final8799["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

            brandTotal = 0;
            gpfdpTotal = 0;
            gpfnlcTotal = 0;
            dpvalTotal = 0;
            gpfmrpTotal = 0;
            mrpvalTotal = 0;
            nlcvalTotal = 0;
            rateTot = 0;
            qty1Total = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Brand Name"] = "Grand Total  ";
            dr_final789["Product Name"] = "";


            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataBrandProductWise()
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
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "BrandProductWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Product Name"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final889 = dt.NewRow();
                    dt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Brand Name"] = "";
                    dr_final8["Product Name"] = "Total : " + sLvlValue;

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";

                    
                    producttot = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Brand Name"] = "Total : " + fLvlValue;
                    dr_final8["Product Name"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";
                    
                    brandTotal = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["Brand Name"] = dr["Productdesc"];
                dr_final12["Product Name"] = dr["ProductName"];

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = Convert.ToDouble(dr["amount"]);

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["NLC"])) / (Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["MRP"])) / (Convert.ToDouble(dr["MRP"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["DP"])) / (Convert.ToDouble(dr["DP"])));

                brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                producttot = producttot + (Convert.ToDouble(dr["amount"]));
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + (Convert.ToDouble(dr["amount"]));
                dt.Rows.Add(dr_final12);
            }
        }

        DataRow dr_final879 = dt.NewRow();
        dt.Rows.Add(dr_final879);

        DataRow dr_final89 = dt.NewRow();
        dr_final89["Brand Name"] = "";
        dr_final89["Product Name"] = "Total : " + sLvlValue;

        if (chkboxQty.Checked == true)
            dr_final89["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        if (chkboxAvg.Checked == true)
            dr_final89["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final89["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final89["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final89["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final89["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final89["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final89["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final89["DP Per%"] = "";

        dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final8799 = dt.NewRow();
        dr_final8799["Brand Name"] = "Total : " + fLvlValue;
        dr_final8799["Product Name"] = "";

        if (chkboxQty.Checked == true)
            dr_final8799["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final8799["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

        if (chkboxAvg.Checked == true)
            dr_final8799["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final8799["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final8799["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final8799["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final8799["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final8799["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final8799["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final8799["DP Per%"] = "";

        dt.Rows.Add(dr_final8799);

        DataRow dr_final77879 = dt.NewRow();
        dt.Rows.Add(dr_final77879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Brand Name"] = "Grand Total : ";
        dr_final789["Product Name"] = "";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataBrandProductModelWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        double brandTotal = 0;
        double producttot = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        double modeltot = 0;
        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        Types = "BrandProductModelWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double qty1Total = 0;

        double mrpvalTotal = 0;
        double nlcvalTotal = 0;
        double dpvalTotal = 0;
        double gpfmrpTotal = 0;
        double gpfnlcTotal = 0;
        double gpfdpTotal = 0;

        double nlcvTotal = 0;
        double mrpvTotal = 0;
        double dpvTotal = 0;
        double gpmTotal = 0;
        double gpnTotal = 0;
        double gpdTotal = 0;
        double rate2Total = 0;
        double qty2Total = 0;

        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Product Name"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                    tLvlValueTemp = dr["model"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                    (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final889 = dt.NewRow();
                        dt.Rows.Add(dr_final889);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "";
                        dr_final8["Product Name"] = "";
                        dr_final8["Model"] = "Total : " + tLvlValue;
                        dr_final8["Category"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty2Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rate2Total));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty2Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvTotal) / (nlcvTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvTotal) / (mrpvTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvTotal) / (dpvTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdTotal));

                        qty2Total = 0;
                        gpdTotal = 0;
                        gpnTotal = 0;
                        dpvTotal = 0;
                        mrpvTotal = 0;
                        nlcvTotal = 0;
                        rate2Total = 0;
                        gpmTotal = 0;
                        modeltot = 0;

                        modeltot = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "";
                        dr_final8["Product Name"] = "Total : " + sLvlValue;
                        dr_final8["Model"] = "";
                        dr_final8["Category"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        producttot = 0;
                        qtyTotal = 0;
                        gpfordpTotal = 0;
                        gpfornlcTotal = 0;
                        dpvalueTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        rateTotal = 0;

                        producttot = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "Total : " + fLvlValue;
                        dr_final8["Product Name"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Category"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTot));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

                        brandTotal = 0;
                        gpfdpTotal = 0;
                        gpfnlcTotal = 0;
                        dpvalTotal = 0;
                        gpfmrpTotal = 0;
                        mrpvalTotal = 0;
                        nlcvalTotal = 0;
                        rateTot = 0;
                        qty1Total = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Brand Name"] = dr["Productdesc"];
                    dr_final12["Product Name"] = dr["ProductName"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Category"] = dr["CategoryName"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    dr_final12["BillNo"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    //dr_final12["BillDate"] = dr["BillDate"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"])-((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"])-((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - (Convert.ToDouble(dr["amount"]));
                        producttot = producttot - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        modeltot = modeltot - (Convert.ToDouble(dr["amount"]));
                        total = total - (Convert.ToDouble(dr["amount"]));
                        qty1Total = qty1Total - Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                        producttot = producttot + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        modeltot = modeltot + (Convert.ToDouble(dr["amount"]));
                        total = total + (Convert.ToDouble(dr["amount"]));
                        qty1Total = qty1Total + Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalTotal = mrpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalTotal = nlcvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalTotal = dpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpfmrpTotal = gpfmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfnlcTotal = gpfnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfdpTotal = gpfdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvTotal = mrpvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvTotal = nlcvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvTotal = dpvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmTotal = gpmTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnTotal = gpnTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdTotal = gpdTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    rate2Total = rate2Total + (Convert.ToDouble(dr["rate1"]));
                    qty2Total = qty2Total + Convert.ToDouble(dr["qty"]);


                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final899 = dt.NewRow();
            dr_final899["Brand Name"] = "";
            dr_final899["Product Name"] = "";
            dr_final899["Model"] = "Total : " + tLvlValue;
            dr_final899["Category"] = "";
            dr_final899["Itemcode"] = "";
            dr_final899["BillNo"] = "";
            dr_final899["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final899["Qty"] = Convert.ToString(Convert.ToDecimal(qty2Total));

            if (chkboxrate.Checked == true)
                dr_final899["Sales Rate"] = (Convert.ToDouble(rate2Total));

            if (chkboxVal.Checked == true)
                dr_final899["Sales Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

            if (chkboxAvg.Checked == true)
                dr_final899["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final899["Per%"] = (100 / (Convert.ToDouble(qty2Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final899["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvTotal));

            if (chkboxNlcper.Checked == true)
                dr_final899["NLC Per%"] = (brandTotal - nlcvTotal) / (nlcvTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final899["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvTotal));

            if (chkboxMRPper.Checked == true)
                dr_final899["MRP Per%"] = (brandTotal - mrpvTotal) / (mrpvTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final899["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvTotal));

            if (chkboxDpper.Checked == true)
                dr_final899["DP Per%"] = (brandTotal - dpvTotal) / (dpvTotal);

            if (chkgpmrp.Checked == true)
                dr_final899["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmTotal));

            if (chkgpnlc.Checked == true)
                dr_final899["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnTotal));

            if (chkgpdp.Checked == true)
                dr_final899["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdTotal));

            qty2Total = 0;
            gpdTotal = 0;
            gpnTotal = 0;
            dpvTotal = 0;
            mrpvTotal = 0;
            nlcvTotal = 0;
            rate2Total = 0;
            gpmTotal = 0;
            modeltot = 0;

            dt.Rows.Add(dr_final899);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Brand Name"] = "";
            dr_final89["Product Name"] = "Total : " + sLvlValue;
            dr_final89["Model"] = "";
            dr_final89["Category"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["BillNo"] = "";
            dr_final89["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

            if (chkboxAvg.Checked == true)
                dr_final89["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            producttot = 0;
            qtyTotal = 0;
            gpfordpTotal = 0;
            gpfornlcTotal = 0;
            dpvalueTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final88789 = dt.NewRow();
            dt.Rows.Add(dr_final88789);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799["Brand Name"] = "Total : " + fLvlValue;
            dr_final8799["Product Name"] = "";
            dr_final8799["Category"] = "";
            dr_final8799["Itemcode"] = "";
            dr_final8799["BillNo"] = "";
            dr_final8799["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final8799["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

            if (chkboxrate.Checked == true)
                dr_final8799["Sales Rate"] = (Convert.ToDouble(rateTot));

            if (chkboxVal.Checked == true)
                dr_final8799["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final8799["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final8799["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final8799["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

            if (chkboxNlcper.Checked == true)
                dr_final8799["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final8799["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

            if (chkboxMRPper.Checked == true)
                dr_final8799["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final8799["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

            if (chkboxDpper.Checked == true)
                dr_final8799["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

            if (chkgpmrp.Checked == true)
                dr_final8799["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final8799["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final8799["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

            brandTotal = 0;
            gpfdpTotal = 0;
            gpfnlcTotal = 0;
            dpvalTotal = 0;
            gpfmrpTotal = 0;
            mrpvalTotal = 0;
            nlcvalTotal = 0;
            rateTot = 0;
            qty1Total = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Brand Name"] = "Grand Total : ";
            dr_final789["Product Name"] = "";
            dr_final789["Model"] = "";
            dr_final789["Category"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillNo"] = "";
            dr_final789["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataBrandProductModelWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        double brandTotal = 0;
        double producttot = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        double modeltot = 0;
        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        Types = "BrandProductModelWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double qty1Total = 0;

        double mrpvalTotal = 0;
        double nlcvalTotal = 0;
        double dpvalTotal = 0;
        double gpfmrpTotal = 0;
        double gpfnlcTotal = 0;
        double gpfdpTotal = 0;

        double nlcvTotal = 0;
        double mrpvTotal = 0;
        double dpvTotal = 0;
        double gpmTotal = 0;
        double gpnTotal = 0;
        double gpdTotal = 0;
        double rate2Total = 0;
        double qty2Total = 0;

        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Product Name"));
        dt.Columns.Add(new DataColumn("Model"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                    tLvlValueTemp = dr["model"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                    (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = fLvlValue;
                        dr_final8["Product Name"] = sLvlValue;
                        dr_final8["Model"] = tLvlValue;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty2Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rate2Total));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty2Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvTotal) / (nlcvTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvTotal) / (mrpvTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvTotal) / (dpvTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdTotal));

                        qty2Total = 0;
                        gpdTotal = 0;
                        gpnTotal = 0;
                        dpvTotal = 0;
                        mrpvTotal = 0;
                        nlcvTotal = 0;
                        rate2Total = 0;
                        gpmTotal = 0;

                        modeltot = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "";
                        dr_final8["Product Name"] = "Total : "+sLvlValue;
                        dr_final8["Model"] = "";


                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        producttot = 0;
                        qtyTotal = 0;
                        gpfordpTotal = 0;
                        gpfornlcTotal = 0;
                        dpvalueTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        rateTotal = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "Total : " + fLvlValue;
                        dr_final8["Product Name"] = "";
                        dr_final8["Model"] = "";
                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTot));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

                        brandTotal = 0;
                        gpfdpTotal = 0;
                        gpfnlcTotal = 0;
                        dpvalTotal = 0;
                        gpfmrpTotal = 0;
                        mrpvalTotal = 0;
                        nlcvalTotal = 0;
                        rateTot = 0;
                        qty1Total = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Brand Name"] = dr["Productdesc"];
                    dr_final12["Product Name"] = dr["ProductName"];
                    dr_final12["Model"] = dr["Model"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - (Convert.ToDouble(dr["amount"]));
                        producttot = producttot - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        modeltot = modeltot - (Convert.ToDouble(dr["amount"]));
                        total = total + (Convert.ToDouble(dr["amount"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        qty1Total = qty1Total - Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                        producttot = producttot + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        modeltot = modeltot + (Convert.ToDouble(dr["amount"]));
                        total = total + (Convert.ToDouble(dr["amount"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        qty1Total = qty1Total + Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalTotal = mrpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalTotal = nlcvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalTotal = dpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpfmrpTotal = gpfmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfnlcTotal = gpfnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfdpTotal = gpfdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvTotal = mrpvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvTotal = nlcvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvTotal = dpvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmTotal = gpmTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnTotal = gpnTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdTotal = gpdTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    qty2Total = qty2Total + Convert.ToDouble(dr["qty"]);
                    rate2Total = rate2Total + (Convert.ToDouble(dr["rate1"]));
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final899 = dt.NewRow();
            dr_final899["Brand Name"] = fLvlValue;
            dr_final899["Product Name"] = sLvlValue;
            dr_final899["Model"] = tLvlValue;


            if (chkboxQty.Checked == true)
                dr_final899["Qty"] = Convert.ToString(Convert.ToDecimal(qty2Total));

            if (chkboxrate.Checked == true)
                dr_final899["Sales Rate"] = (Convert.ToDouble(rate2Total));

            if (chkboxVal.Checked == true)
                dr_final899["Sales Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

            if (chkboxAvg.Checked == true)
                dr_final899["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final899["Per%"] = (100 / (Convert.ToDouble(qty2Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final899["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvTotal));

            if (chkboxNlcper.Checked == true)
                dr_final899["NLC Per%"] = (brandTotal - nlcvTotal) / (nlcvTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final899["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvTotal));

            if (chkboxMRPper.Checked == true)
                dr_final899["MRP Per%"] = (brandTotal - mrpvTotal) / (mrpvTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final899["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvTotal));

            if (chkboxDpper.Checked == true)
                dr_final899["DP Per%"] = (brandTotal - dpvTotal) / (dpvTotal);

            if (chkgpmrp.Checked == true)
                dr_final899["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmTotal));

            if (chkgpnlc.Checked == true)
                dr_final899["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnTotal));

            if (chkgpdp.Checked == true)
                dr_final899["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdTotal));

            qty2Total = 0;
            gpdTotal = 0;
            gpnTotal = 0;
            dpvTotal = 0;
            mrpvTotal = 0;
            nlcvTotal = 0;
            rate2Total = 0;
            gpmTotal = 0;
            modeltot = 0;

            dt.Rows.Add(dr_final899);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Brand Name"] = "";
            dr_final89["Product Name"] = "Total : " + sLvlValue;
            dr_final89["Model"] = "";

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

            if (chkboxAvg.Checked == true)
                dr_final89["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            producttot = 0;
            qtyTotal = 0;
            gpfordpTotal = 0;
            gpfornlcTotal = 0;
            dpvalueTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final88789 = dt.NewRow();
            dt.Rows.Add(dr_final88789);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799["Brand Name"] = "Total : " + fLvlValue;
            dr_final8799["Product Name"] = "";
            dr_final8799["Model"] = "";


            if (chkboxQty.Checked == true)
                dr_final8799["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

            if (chkboxrate.Checked == true)
                dr_final8799["Sales Rate"] = (Convert.ToDouble(rateTot));

            if (chkboxVal.Checked == true)
                dr_final8799["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final8799["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final8799["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final8799["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

            if (chkboxNlcper.Checked == true)
                dr_final8799["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final8799["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

            if (chkboxMRPper.Checked == true)
                dr_final8799["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final8799["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

            if (chkboxDpper.Checked == true)
                dr_final8799["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

            if (chkgpmrp.Checked == true)
                dr_final8799["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final8799["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final8799["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

            brandTotal = 0;
            gpfdpTotal = 0;
            gpfnlcTotal = 0;
            dpvalTotal = 0;
            gpfmrpTotal = 0;
            mrpvalTotal = 0;
            nlcvalTotal = 0;
            rateTot = 0;
            qty1Total = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Brand Name"] = "Grand Total : ";
            dr_final789["Product Name"] = "";
            dr_final789["Model"] = "";


            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataBrandProductModelWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        double brandTotal = 0;
        double producttot = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        double modeltot = 0;
        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "BrandProductModelWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Product Name"));
        dt.Columns.Add(new DataColumn("Model"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                tLvlValueTemp = dr["model"].ToString().ToUpper().Trim();

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                {
                    DataRow dr_final889 = dt.NewRow();
                    dt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Brand Name"] = "";
                    dr_final8["Product Name"] = "";
                    dr_final8["Model"] = "Total : " + tLvlValue;

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";

                    modeltot = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Brand Name"] = "";
                    dr_final8["Product Name"] = "Total : " + sLvlValue;
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";

                    producttot = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Brand Name"] = "Total : " + fLvlValue;
                    dr_final8["Product Name"] = "";
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";

                    brandTotal = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["Brand Name"] = dr["Productdesc"];
                dr_final12["Product Name"] = dr["ProductName"];
                dr_final12["Model"] = dr["Model"];

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = Convert.ToDouble(dr["amount"]);

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["NLC"])) / (Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["MRP"])) / (Convert.ToDouble(dr["MRP"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["DP"])) / (Convert.ToDouble(dr["DP"])));

                brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                producttot = producttot + (Convert.ToDouble(dr["amount"]));
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                modeltot = modeltot + (Convert.ToDouble(dr["amount"]));
                total = total + (Convert.ToDouble(dr["amount"]));
                dt.Rows.Add(dr_final12);
            }
        }

        DataRow dr_final879 = dt.NewRow();
        dt.Rows.Add(dr_final879);

        DataRow dr_final899 = dt.NewRow();
        dr_final899["Brand Name"] = "";
        dr_final899["Product Name"] = "";
        dr_final899["Model"] = "Total : " + tLvlValue;

        if (chkboxQty.Checked == true)
            dr_final899["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final899["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

        if (chkboxAvg.Checked == true)
            dr_final899["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final899["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final899["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final899["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final899["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final899["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final899["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final899["DP Per%"] = "";

        dt.Rows.Add(dr_final899);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final89 = dt.NewRow();
        dr_final89["Brand Name"] = "";
        dr_final89["Product Name"] = "Total : " + sLvlValue;
        dr_final89["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final89["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        if (chkboxAvg.Checked == true)
            dr_final89["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final89["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final89["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final89["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final89["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final89["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final89["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final89["DP Per%"] = "";

        dt.Rows.Add(dr_final89);

        DataRow dr_final88789 = dt.NewRow();
        dt.Rows.Add(dr_final88789);

        DataRow dr_final8799 = dt.NewRow();
        dr_final8799["Brand Name"] = "Total : " + fLvlValue;
        dr_final8799["Product Name"] = "";

        if (chkboxQty.Checked == true)
            dr_final8799["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final8799["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

        if (chkboxAvg.Checked == true)
            dr_final8799["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final8799["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final8799["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final8799["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final8799["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final8799["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final8799["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final8799["DP Per%"] = "";

        dt.Rows.Add(dr_final8799);

        DataRow dr_final77879 = dt.NewRow();
        dt.Rows.Add(dr_final77879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Brand Name"] = "Grand Total : ";
        dr_final789["Product Name"] = "";
        dr_final789["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataBillWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateqtyTotal = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        Types = "BillWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Bill No"));
        dt.Columns.Add(new DataColumn("Bill Date"));
        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Brand"));
        dt.Columns.Add(new DataColumn("Product"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Itemcode"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["BillNo"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8889 = dt.NewRow();
                        dt.Rows.Add(dr_final8889);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Bill No"] = "Total : " + fLvlValue;
                        dr_final8["Category"] = "";
                        dr_final8["Product"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["Brand"] = "";
                        dr_final8["Bill Date"] = "";
                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Bill No"] = dr["BillNo"];

                    //dr_final12["Bill Date"] = dr["BillDate"];
                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["Bill Date"] = dtaa;

                    dr_final12["Category"] = dr["Categoryname"];
                    dr_final12["Product"] = dr["Productname"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    dr_final12["Brand"] = dr["productdesc"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]);

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]);

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["mrp"]))) / ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["mrp"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]);

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["dp"]))) / ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["dp"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"]))-(Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"]))-(Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - Convert.ToDouble(dr["rate"]);
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal - Convert.ToDouble(dr["rate1"]);
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + Convert.ToDouble(dr["rate"]);
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal + Convert.ToDouble(dr["rate1"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Bill No"] = "Total : " + fLvlValueTemp;
            dr_final89["Category"] = "";
            dr_final89["Product"] = "";
            dr_final89["Model"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["Brand"] = "";

            dr_final89["Bill Date"] = " ";
            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Bill No"] = "Grand Total : ";
            dr_final789["Category"] = "";
            dr_final789["Product"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["Brand"] = "";

            dr_final789["Bill Date"] = " ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateqtyTotal));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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


    public void bindDataBillWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        Types = "BillWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Bill No"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["BillNo"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Bill No"] = fLvlValue;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Bill No"] = dr["BillNo"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]);

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]);

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["mrp"]))) / ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["mrp"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]);

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["dp"]))) / ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["dp"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - Convert.ToDouble(dr["rate"]);
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - Convert.ToDouble(dr["rate1"]);
                        rateTotal1 = rateTotal1 - Convert.ToDouble(dr["rate1"]);
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + Convert.ToDouble(dr["rate"]);
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + Convert.ToDouble(dr["rate1"]);
                        rateTotal1 = rateTotal1 + Convert.ToDouble(dr["rate1"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                }
            }

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Bill No"] = fLvlValueTemp;

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Bill No"] = "Grand Total ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataBillWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;



        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "BillWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Bill No"));
        dt.Columns.Add(new DataColumn("Bill Date"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["BillNo"].ToString();

                fLvlValue = fLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["Bill No"] = dr["BillNo"];

                //dr_final12["Bill Date"] = dr["BillDate"];
                string aa = dr["BillDate"].ToString().ToUpper().Trim();
                string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                dr_final12["BillDate"] = dtaa;

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = (Convert.ToDouble(dr["rate"]));

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["mrp"]))) / ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["mrp"]))));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["dp"]))) / ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["dp"]))));

                CategoryTotal = CategoryTotal + Convert.ToDouble(dr["rate"]);
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + (Convert.ToDouble(dr["rate"]));
                dt.Rows.Add(dr_final12);
            }
        }

        //DataRow dr_final879 = dt.NewRow();
        //dt.Rows.Add(dr_final879);

        //DataRow dr_final89 = dt.NewRow();
        //dr_final89["CategoryName"] = "Total : " + fLvlValueTemp;
        //dr_final89["Qty"] = "";
        //dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
        //dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Bill No"] = "Grand Total : ";

        dr_final789["Bill Date"] = " ";
        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataBrandModelWise()
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
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "BrandModelWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Model"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["model"].ToString().ToUpper().Trim();

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final889 = dt.NewRow();
                    dt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Brand Name"] = "";
                    dr_final8["Model"] = "Total : " + sLvlValue;

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";


                    producttot = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Brand Name"] = "Total : " + fLvlValue;
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";

                    brandTotal = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["Brand Name"] = dr["Productdesc"];
                dr_final12["Model"] = dr["model"];

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = Convert.ToDouble(dr["amount"]);

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["NLC"])) / (Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["MRP"])) / (Convert.ToDouble(dr["MRP"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["DP"])) / (Convert.ToDouble(dr["DP"])));

                brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                producttot = producttot + (Convert.ToDouble(dr["amount"]));
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + (Convert.ToDouble(dr["amount"]));
                dt.Rows.Add(dr_final12);
            }
        }

        DataRow dr_final879 = dt.NewRow();
        dt.Rows.Add(dr_final879);

        DataRow dr_final89 = dt.NewRow();
        dr_final89["Brand Name"] = "";
        dr_final89["Model"] = "Total : " + sLvlValue;

        if (chkboxQty.Checked == true)
            dr_final89["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        if (chkboxAvg.Checked == true)
            dr_final89["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final89["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final89["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final89["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final89["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final89["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final89["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final89["DP Per%"] = "";

        dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final8799 = dt.NewRow();
        dr_final8799["Brand Name"] = "Total : " + fLvlValue;
        dr_final8799["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final8799["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final8799["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

        if (chkboxAvg.Checked == true)
            dr_final8799["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final8799["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final8799["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final8799["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final8799["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final8799["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final8799["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final8799["DP Per%"] = "";

        dt.Rows.Add(dr_final8799);

        DataRow dr_final77879 = dt.NewRow();
        dt.Rows.Add(dr_final77879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Brand Name"] = "Grand Total : ";
        dr_final789["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataBrandModelWiseNormal()
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
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double qty1Total = 0;

        double mrpvalTotal = 0;
        double nlcvalTotal = 0;
        double dpvalTotal = 0;
        double gpfmrpTotal = 0;
        double gpfnlcTotal = 0;
        double gpfdpTotal = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        Types = "BrandModelWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("ProductName"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["model"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final889 = dt.NewRow();
                        dt.Rows.Add(dr_final889);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "";
                        dr_final8["Model"] = "Total : " + sLvlValue;
                        dr_final8["Category"] = "";
                        dr_final8["ProductName"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        producttot = 0;
                        qtyTotal = 0;
                        gpfordpTotal = 0;
                        gpfornlcTotal = 0;
                        dpvalueTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        rateTotal = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "Total : " + fLvlValue;
                        dr_final8["Model"] = "";
                        dr_final8["Category"] = "";
                        dr_final8["ProductName"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTot));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

                        brandTotal = 0;
                        gpfdpTotal = 0;
                        gpfnlcTotal = 0;
                        dpvalTotal = 0;
                        gpfmrpTotal = 0;
                        mrpvalTotal = 0;
                        nlcvalTotal = 0;
                        rateTot = 0;
                        qty1Total = 0;

                        brandTotal = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Brand Name"] = dr["Productdesc"];
                    dr_final12["Model"] = dr["model"];
                    dr_final12["Category"] = dr["CategoryName"];
                    dr_final12["ProductName"] = dr["ProductName"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    dr_final12["BillNo"] = dr["BillNo"];
                    //dr_final12["BillDate"] = dr["BillDate"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]);

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]);

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]);

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"])-(Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"])-(Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - (Convert.ToDouble(dr["amount"]));
                        producttot = producttot - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["amount"]));

                        qty1Total = qty1Total - Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                        producttot = producttot + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["amount"]));

                        qty1Total = qty1Total + Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalTotal = mrpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalTotal = nlcvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalTotal = dpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpfmrpTotal = gpfmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfnlcTotal = gpfnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfdpTotal = gpfdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Brand Name"] = "";
            dr_final89["Model"] = "Total : " + sLvlValue;
            dr_final89["Category"] = "";
            dr_final89["ProductName"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["BillNo"] = "";
            dr_final89["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

            if (chkboxAvg.Checked == true)
                dr_final89["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            producttot = 0;
            qtyTotal = 0;
            gpfordpTotal = 0;
            gpfornlcTotal = 0;
            dpvalueTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799["Brand Name"] = "Total : " + fLvlValue;
            dr_final8799["Model"] = "";
            dr_final8799["Category"] = "";
            dr_final8799["ProductName"] = "";
            dr_final8799["Itemcode"] = "";
            dr_final8799["BillNo"] = "";
            dr_final8799["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final8799["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

            if (chkboxrate.Checked == true)
                dr_final8799["Sales Rate"] = (Convert.ToDouble(rateTot));

            if (chkboxVal.Checked == true)
                dr_final8799["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final8799["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final8799["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final8799["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

            if (chkboxNlcper.Checked == true)
                dr_final8799["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final8799["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

            if (chkboxMRPper.Checked == true)
                dr_final8799["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final8799["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

            if (chkboxDpper.Checked == true)
                dr_final8799["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

            if (chkgpmrp.Checked == true)
                dr_final8799["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final8799["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final8799["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

            brandTotal = 0;
            gpfdpTotal = 0;
            gpfnlcTotal = 0;
            dpvalTotal = 0;
            gpfmrpTotal = 0;
            mrpvalTotal = 0;
            nlcvalTotal = 0;
            rateTot = 0;
            qty1Total = 0;

            brandTotal = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Brand Name"] = "Grand Total : ";
            dr_final789["Model"] = "";
            dr_final789["Category"] = "";
            dr_final789["ProductName"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillNo"] = "";
            dr_final789["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataBrandModelWiseGroupBy()
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
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double qty1Total = 0;

        double mrpvalTotal = 0;
        double nlcvalTotal = 0;
        double dpvalTotal = 0;
        double gpfmrpTotal = 0;
        double gpfnlcTotal = 0;
        double gpfdpTotal = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        Types = "BrandModelWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Model"));       

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["model"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = fLvlValue;
                        dr_final8["Model"] = sLvlValue;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        producttot = 0;
                        qtyTotal = 0;
                        gpfordpTotal = 0;
                        gpfornlcTotal = 0;
                        dpvalueTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final88899 = dt.NewRow();
                        dt.Rows.Add(dr_final88899);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Brand Name"] = "Total : " + fLvlValue;
                        dr_final8["Model"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTot));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

                        brandTotal = 0;
                        gpfdpTotal = 0;
                        gpfnlcTotal = 0;
                        dpvalTotal = 0;
                        gpfmrpTotal = 0;
                        mrpvalTotal = 0;
                        nlcvalTotal = 0;
                        rateTot = 0;
                        qty1Total = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Brand Name"] = dr["Productdesc"];
                    dr_final12["Model"] = dr["model"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]);

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]);

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]);

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - (Convert.ToDouble(dr["amount"]));
                        producttot = producttot - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["amount"]));
                        qty1Total = qty1Total - Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                        producttot = producttot + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["amount"]));
                        qty1Total = qty1Total + Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalTotal = mrpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalTotal = nlcvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalTotal = dpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpfmrpTotal = gpfmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfnlcTotal = gpfnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfdpTotal = gpfdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                }
            }

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Brand Name"] = fLvlValue;
            dr_final89["Model"] =  sLvlValue;

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final89["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            dt.Rows.Add(dr_final89);

            producttot = 0;
            qtyTotal = 0;
            gpfordpTotal = 0;
            gpfornlcTotal = 0;
            dpvalueTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            rateTotal = 0;

            DataRow dr_final8887 = dt.NewRow();
            dt.Rows.Add(dr_final8887);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799["Brand Name"] = "Total : " + fLvlValue;
            dr_final8799["Model"] = "";

            if (chkboxQty.Checked == true)
                dr_final8799["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

            if (chkboxrate.Checked == true)
                dr_final8799["Sales Rate"] = (Convert.ToDouble(rateTot));

            if (chkboxVal.Checked == true)
                dr_final8799["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final8799["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final8799["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final8799["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

            if (chkboxNlcper.Checked == true)
                dr_final8799["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final8799["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

            if (chkboxMRPper.Checked == true)
                dr_final8799["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final8799["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

            if (chkboxDpper.Checked == true)
                dr_final8799["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

            if (chkgpmrp.Checked == true)
                dr_final8799["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final8799["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final8799["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

            brandTotal = 0;
            gpfdpTotal = 0;
            gpfnlcTotal = 0;
            dpvalTotal = 0;
            gpfmrpTotal = 0;
            mrpvalTotal = 0;
            nlcvalTotal = 0;
            rateTot = 0;
            qty1Total = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Brand Name"] = "Grand Total ";
            dr_final789["Model"] = "";


            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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


    public void bindDataPayModeWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateqtyTotal = 0;

        Types = "PayModeWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Pay Mode"));
        dt.Columns.Add(new DataColumn("ProductName"));
        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Brand"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["PayMode"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8886 = dt.NewRow();
                        dt.Rows.Add(dr_final8886);

                        DataRow dr_final8 = dt.NewRow();
                        if (fLvlValue.ToString() == "1")
                        {
                            dr_final8["Pay Mode"] = "Total : Cash ";
                        }
                        else if (fLvlValue.ToString() == "2")
                        {
                            dr_final8["Pay Mode"] = "Total : Bank / Credit Card";
                        }
                        else if (fLvlValue.ToString() == "3")
                        {
                            dr_final8["Pay Mode"] = "Total : Credit";
                        }
                        else if (fLvlValue.ToString() == "4")
                        {
                            dr_final8["Pay Mode"] = "Total : Multiple Payment";
                        }
                        dr_final8["ProductName"] = "";
                        dr_final8["Category"] = "";
                        dr_final8["Brand"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillDate"] = "";
                        dr_final8["BillNo"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();

                    if (fLvlValue.ToString() == "1")
                    {
                        dr_final12["Pay Mode"] = "Cash";
                    }
                    else if (fLvlValue.ToString() == "2")
                    {
                        dr_final12["Pay Mode"] = "Bank / Credit Card";
                    }
                    else if (fLvlValue.ToString() == "3")
                    {
                        dr_final12["Pay Mode"] = "Credit";
                    }
                    else if (fLvlValue.ToString() == "4")
                    {
                        dr_final12["Pay Mode"] = "Multiple Payment";
                    }
                    dr_final12["ProductName"] = dr["ProductName"];
                    dr_final12["Category"] = dr["Categoryname"];
                    dr_final12["Brand"] = dr["Productdesc"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    //dr_final12["BillDate"] = dr["BillDate"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    dr_final12["BillNo"] = dr["BillNo"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"]))-(Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"]))-(Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal - Convert.ToDouble(dr["rate1"]);
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal + Convert.ToDouble(dr["rate1"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            if (fLvlValue.ToString() == "1")
            {
                dr_final89["Pay Mode"] = "Total : Cash ";
            }
            else if (fLvlValue.ToString() == "2")
            {
                dr_final89["Pay Mode"] = "Total : Bank / Credit Card";
            }
            else if (fLvlValue.ToString() == "3")
            {
                dr_final89["Pay Mode"] = "Total : Credit";
            }
            else if (fLvlValue.ToString() == "4")
            {
                dr_final89["Pay Mode"] = "Total : Multiple Payment";
            }
            //dr_final89["Pay Mode"] = "Total : " + fLvlValueTemp;
            dr_final89["ProductName"] = "";
            dr_final89["Category"] = "";
            dr_final89["Brand"] = "";
            dr_final89["Model"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["BillDate"] = "";
            dr_final89["BillNo"] = "";
            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Pay Mode"] = "Grand Total : ";
            dr_final789["ProductName"] = "";
            dr_final789["Category"] = "";
            dr_final789["Brand"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillDate"] = "";
            dr_final789["BillNo"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateqtyTotal));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataPayModeWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;

        Types = "PayModeWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Pay Mode"));
        
        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["PayMode"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {

                        DataRow dr_final8 = dt.NewRow();
                        if (fLvlValue.ToString() == "1")
                        {
                            dr_final8["Pay Mode"] = "Cash ";
                        }
                        else if (fLvlValue.ToString() == "2")
                        {
                            dr_final8["Pay Mode"] = "Bank / Credit Card";
                        }
                        else if (fLvlValue.ToString() == "3")
                        {
                            dr_final8["Pay Mode"] = "Credit";
                        }
                        else if (fLvlValue.ToString() == "4")
                        {
                            dr_final8["Pay Mode"] = "Multiple Payment";
                        }

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();

                    if (fLvlValue.ToString() == "1")
                    {
                        dr_final12["Pay Mode"] = "Cash";
                    }
                    else if (fLvlValue.ToString() == "2")
                    {
                        dr_final12["Pay Mode"] = "Bank / Credit Card";
                    }
                    else if (fLvlValue.ToString() == "3")
                    {
                        dr_final12["Pay Mode"] = "Credit";
                    }
                    else if (fLvlValue.ToString() == "4")
                    {
                        dr_final12["Pay Mode"] = "Multiple Payment";
                    }

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                }
            }

            DataRow dr_final89 = dt.NewRow();
            if (fLvlValue.ToString() == "1")
            {
                dr_final89["Pay Mode"] = "Cash ";
            }
            else if (fLvlValue.ToString() == "2")
            {
                dr_final89["Pay Mode"] = "Bank / Credit Card";
            }
            else if (fLvlValue.ToString() == "3")
            {
                dr_final89["Pay Mode"] = "Credit";
            }
            else if (fLvlValue.ToString() == "4")
            {
                dr_final89["Pay Mode"] = "Multiple Payment";
            }

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Pay Mode"] = "Grand Total  ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataPayModeWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "PayModeWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Pay Mode"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["PayMode"].ToString();

                fLvlValue = fLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();

                if (dr["PayMode"].ToString() == "1")
                {
                    dr_final12["Pay Mode"] = "Cash";
                }
                else if (dr["PayMode"].ToString() == "2")
                {
                    dr_final12["Pay Mode"] = "Bank / Credit Card";
                }
                else if (dr["PayMode"].ToString() == "3")
                {
                    dr_final12["Pay Mode"] = "Credit";
                }
                else if (dr["PayMode"].ToString() == "4")
                {
                    dr_final12["Pay Mode"] = "Multiple Payment";
                }

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = (Convert.ToDouble(dr["rate"]));

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["NLC"])) / (Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["MRP"])) / (Convert.ToDouble(dr["MRP"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["DP"])) / (Convert.ToDouble(dr["DP"])));

                CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + (Convert.ToDouble(dr["rate"]));
                dt.Rows.Add(dr_final12);
            }
        }

        //DataRow dr_final879 = dt.NewRow();
        //dt.Rows.Add(dr_final879);

        //DataRow dr_final89 = dt.NewRow();
        //dr_final89["CategoryName"] = "Total : " + fLvlValueTemp;
        //dr_final89["Qty"] = "";
        //dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
        //dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Pay Mode"] = "Grand Total : ";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataExecutiveWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateqtyTotal = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        Types = "ExecutiveWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Executive"));
        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Product"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Brand"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options, salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Executivename"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8886 = dt.NewRow();
                        dt.Rows.Add(dr_final8886);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Executive"] = "Total : " + fLvlValue;
                        dr_final8["Product"] = "";
                        dr_final8["Category"] = "";
                        dr_final8["Brand"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillDate"] = "";
                        dr_final8["BillNo"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Executive"] = dr["Executivename"];
                    dr_final12["Product"] = dr["ProductName"];
                    dr_final12["Category"] = dr["Categoryname"];
                    dr_final12["Brand"] = dr["productdesc"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    //dr_final12["BillDate"] = dr["BillDate"];
                    dr_final12["BillNo"] = dr["BillNo"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + dr["qty"];
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"])));


                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal - Convert.ToDouble(dr["rate1"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal + Convert.ToDouble(dr["rate1"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Executive"] = "Total : " + fLvlValueTemp;
            dr_final89["Product"] = "";
            dr_final89["Category"] = "";
            dr_final89["Brand"] = "";
            dr_final89["Model"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["BillDate"] = "";
            dr_final89["BillNo"] = "";
            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;
            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Executive"] = "Grand Total : ";
            dr_final789["Product"] = "";
            dr_final789["Category"] = "";
            dr_final789["Brand"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillDate"] = "";
            dr_final789["BillNo"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateqtyTotal));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataExecutiveWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        Types = "ExecutiveWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Executive"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Executivename"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Executive"] = fLvlValue;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Executive"] = dr["Executivename"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                }
            }

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Executive"] = fLvlValueTemp;

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Executive"] = "Grand Total ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataExecutiveWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "ExecutiveWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Executive"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Executivename"].ToString();

                fLvlValue = fLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["Executive"] = dr["Executivename"];

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = (Convert.ToDouble(dr["rate"]));

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["NLC"])) / (Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["MRP"])) / (Convert.ToDouble(dr["MRP"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["DP"])) / (Convert.ToDouble(dr["DP"])));

                CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + (Convert.ToDouble(dr["rate"]));
                dt.Rows.Add(dr_final12);
            }
        }

        //DataRow dr_final879 = dt.NewRow();
        //dt.Rows.Add(dr_final879);

        //DataRow dr_final89 = dt.NewRow();
        //dr_final89["CategoryName"] = "Total : " + fLvlValueTemp;
        //dr_final89["Qty"] = "";
        //dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
        //dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Executive"] = "Grand Total : ";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataDateWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
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

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateqtyTotal = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "DateWise";
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Date"));
        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("ProductName"));
        dt.Columns.Add(new DataColumn("Brand"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));

        dt.Columns.Add(new DataColumn("Customer Name"));
        dt.Columns.Add(new DataColumn("Address1"));
        dt.Columns.Add(new DataColumn("Address2"));
        dt.Columns.Add(new DataColumn("Address3"));
        dt.Columns.Add(new DataColumn("Mobile"));
        dt.Columns.Add(new DataColumn("Phone"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }

        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options, salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["billdate"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8886 = dt.NewRow();
                        dt.Rows.Add(dr_final8886);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Date"] = "Total : " + fLvlValue;
                        dr_final8["ProductName"] = "";
                        dr_final8["Category"] = "";
                        dr_final8["Brand"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(dateTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(dateTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (dateTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (dateTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (dateTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        dateTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    //dr_final12["Date"] = dr["billdate"];
                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["Date"] = dtaa;


                    dr_final12["ProductName"] = dr["ProductName"];
                    dr_final12["Category"] = dr["CategoryName"];
                    dr_final12["Brand"] = dr["productdesc"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    dr_final12["BillNo"] = dr["BillNo"];
                    dr_final12["Customer Name"] = dr["CustomerName"];
                    dr_final12["Address1"] = dr["CustomerAddress"];
                    dr_final12["Address2"] = dr["Add2"];
                    dr_final12["Address3"] = dr["Add3"];
                    dr_final12["Mobile"] = dr["Mobile"];
                    dr_final12["Phone"] = dr["Phone"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + dr["Qty"];
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"]))-(Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"]))-(Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));


                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        dateTotal = dateTotal - (Convert.ToDouble(dr["rate"]));
                        CategoryQtyTotal = CategoryQtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal - Convert.ToDouble(dr["rate1"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                    }
                    else
                    {
                        dateTotal = dateTotal + (Convert.ToDouble(dr["rate"]));
                        CategoryQtyTotal = CategoryQtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal + Convert.ToDouble(dr["rate1"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Date"] = "Total : " + fLvlValueTemp;
            dr_final89["ProductName"] = "";
            dr_final89["Category"] = "";
            dr_final89["Brand"] = "";
            dr_final89["Model"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["BillNo"] = "";
            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(dateTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(dateTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (dateTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (dateTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (dateTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            dateTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;
            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Date"] = "Grand Total : ";
            dr_final789["ProductName"] = "";
            dr_final789["Category"] = "";
            dr_final789["Brand"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillNo"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryQtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateqtyTotal));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryQtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataDateWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
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

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        double rateTotal = 0;
        double rateTotal1 = 0;

        Types = "DateWise";
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Date"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["billdate"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {

                        DataRow dr_final8 = dt.NewRow();

                        string aaa = fLvlValue.ToString().ToUpper().Trim();
                        string dtaat = Convert.ToDateTime(aaa).ToString("dd/MM/yyyy");
                        dr_final8["Date"] = dtaat;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(dateTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(dateTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (dateTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (dateTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (dateTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        dateTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;
                        dt.Rows.Add(dr_final8);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["Date"] = dtaa;

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        dateTotal = dateTotal - (Convert.ToDouble(dr["rate"]));
                        CategoryQtyTotal = CategoryQtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        dateTotal = dateTotal + (Convert.ToDouble(dr["rate"]));
                        CategoryQtyTotal = CategoryQtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                }
            }

            DataRow dr_final89 = dt.NewRow();

            string aaaaa = fLvlValueTemp.ToString().ToUpper().Trim();
            string dtaatt = Convert.ToDateTime(aaaaa).ToString("dd/MM/yyyy");
            dr_final89["Date"] = dtaatt;

            //dr_final89["Date"] = fLvlValueTemp;

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(dateTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(dateTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (dateTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (dateTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (dateTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            dateTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Date"] = "Grand Total  ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryQtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryQtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataDateWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "DateWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Date"));
        

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["billdate"].ToString();

                fLvlValue = fLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();

                string aa = dr["BillDate"].ToString().ToUpper().Trim();
                string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                dr_final12["Date"] = dtaa;

                //dr_final12["Date"] = dr["billdate"];

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = (Convert.ToDouble(dr["rate"]));

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["NLC"])) / (Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["MRP"])) / (Convert.ToDouble(dr["MRP"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["DP"])) / (Convert.ToDouble(dr["DP"])));

                CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["rate"]));
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["rate"]));
                dt.Rows.Add(dr_final12);
            }
        }

        //DataRow dr_final879 = dt.NewRow();
        //dt.Rows.Add(dr_final879);

        //DataRow dr_final89 = dt.NewRow();
        //dr_final89["CategoryName"] = "Total : " + fLvlValueTemp;
        //dr_final89["Qty"] = "";
        //dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
        //dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Date"] = "Grand Total : ";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataMonthWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateqtyTotal = 0;

        Types = "MonthWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Month"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));
        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Product"));
        dt.Columns.Add(new DataColumn("Brand"));
        dt.Columns.Add(new DataColumn("Itemcode"));

        dt.Columns.Add(new DataColumn("Model"));
        

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options, salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["MonthName"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8886 = dt.NewRow();
                        dt.Rows.Add(dr_final8886);

                        DataRow dr_final8 = dt.NewRow();
                        if (fLvlValue.ToString() == "1")
                        {
                            dr_final8["Month"] = "Total : January";
                        }
                        else if (fLvlValue.ToString() == "2")
                        {
                            dr_final8["Month"] = "Total : February";
                        }
                        else if (fLvlValue.ToString() == "3")
                        {
                            dr_final8["Month"] = "Total : March";
                        }
                        else if (fLvlValue.ToString() == "4")
                        {
                            dr_final8["Month"] = "Total : April";
                        }
                        else if (fLvlValue.ToString() == "5")
                        {
                            dr_final8["Month"] = "Total : May";
                        }
                        else if (fLvlValue.ToString() == "6")
                        {
                            dr_final8["Month"] = "Total : June";
                        }
                        else if (fLvlValue.ToString() == "7")
                        {
                            dr_final8["Month"] = "Total : July";
                        }
                        else if (fLvlValue.ToString() == "8")
                        {
                            dr_final8["Month"] = "Total : August";
                        }
                        else if (fLvlValue.ToString() == "9")
                        {
                            dr_final8["Month"] = "Total : September";
                        }
                        else if (fLvlValue.ToString() == "10")
                        {
                            dr_final8["Month"] = "Total : October";
                        }
                        else if (fLvlValue.ToString() == "11")
                        {
                            dr_final8["Month"] = "Total : November ";
                        }
                        else if (fLvlValue.ToString() == "12")
                        {
                            dr_final8["Month"] = "Total : December ";
                        }
                        //dr_final8["Month"] = "Total : " + fLvlValue;
                        dr_final8["Category"] = "";
                        dr_final8["Brand"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillDate"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["Product"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();

                    if (dr["MonthName"].ToString() == "1")
                    {
                        dr_final12["Month"] = "January";
                    }
                    else if (dr["MonthName"].ToString() == "2")
                    {
                        dr_final12["Month"] = "February";
                    }
                    else if (dr["MonthName"].ToString() == "3")
                    {
                        dr_final12["Month"] = "March";
                    }
                    else if (dr["MonthName"].ToString() == "4")
                    {
                        dr_final12["Month"] = "April";
                    }
                    else if (dr["MonthName"].ToString() == "5")
                    {
                        dr_final12["Month"] = "May";
                    }
                    else if (dr["MonthName"].ToString() == "6")
                    {
                        dr_final12["Month"] = "June";
                    }
                    else if (dr["MonthName"].ToString() == "7")
                    {
                        dr_final12["Month"] = "July";
                    }
                    else if (dr["MonthName"].ToString() == "8")
                    {
                        dr_final12["Month"] = "August";
                    }
                    else if (dr["MonthName"].ToString() == "9")
                    {
                        dr_final12["Month"] = "September";
                    }
                    else if (dr["MonthName"].ToString() == "10")
                    {
                        dr_final12["Month"] = "October";
                    }
                    else if (dr["MonthName"].ToString() == "11")
                    {
                        dr_final12["Month"] = "November";
                    }
                    else if (dr["MonthName"].ToString() == "12")
                    {
                        dr_final12["Month"] = "December";
                    }

                    dr_final12["BillNo"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    //dr_final12["BillDate"] = dr["BillDate"];
                    dr_final12["Category"] = dr["Categoryname"];
                    dr_final12["Brand"] = dr["productdesc"];
                    dr_final12["Product"] = dr["Productname"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"]))-((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"]))-((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal - Convert.ToDouble(dr["rate1"]);
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal + Convert.ToDouble(dr["rate1"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            //dr_final89["Month"] = "Total : " + fLvlValueTemp;
            if (fLvlValue.ToString() == "1")
            {
                dr_final89["Month"] = "Total : January";
            }
            else if (fLvlValue.ToString() == "2")
            {
                dr_final89["Month"] = "Total : February";
            }
            else if (fLvlValue.ToString() == "3")
            {
                dr_final89["Month"] = "Total : March";
            }
            else if (fLvlValue.ToString() == "4")
            {
                dr_final89["Month"] = "Total : April";
            }
            else if (fLvlValue.ToString() == "5")
            {
                dr_final89["Month"] = "Total : May";
            }
            else if (fLvlValue.ToString() == "6")
            {
                dr_final89["Month"] = "Total : June";
            }
            else if (fLvlValue.ToString() == "7")
            {
                dr_final89["Month"] = "Total : July";
            }
            else if (fLvlValue.ToString() == "8")
            {
                dr_final89["Month"] = "Total : August";
            }
            else if (fLvlValue.ToString() == "9")
            {
                dr_final89["Month"] = "Total : September";
            }
            else if (fLvlValue.ToString() == "10")
            {
                dr_final89["Month"] = "Total : October";
            }
            else if (fLvlValue.ToString() == "11")
            {
                dr_final89["Month"] = "Total : November ";
            }
            else if (fLvlValue.ToString() == "12")
            {
                dr_final89["Month"] = "Total : December ";
            }
            dr_final89["Category"] = "";
            dr_final89["Brand"] = "";
            dr_final89["Model"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["BillDate"] = "";
            dr_final89["BillNo"] = "";
            dr_final89["Product"] = "";
            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Month"] = "Grand Total : ";
            dr_final789["Category"] = "";
            dr_final789["Brand"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillDate"] = "";
            dr_final789["BillNo"] = "";
            dr_final789["Product"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateqtyTotal));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataMonthWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        Types = "MonthWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Month"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["MonthName"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        if (fLvlValue.ToString() == "1")
                        {
                            dr_final8["Month"] = "January";
                        }
                        else if (fLvlValue.ToString() == "2")
                        {
                            dr_final8["Month"] = "February";
                        }
                        else if (fLvlValue.ToString() == "3")
                        {
                            dr_final8["Month"] = "March";
                        }
                        else if (fLvlValue.ToString() == "4")
                        {
                            dr_final8["Month"] = "April";
                        }
                        else if (fLvlValue.ToString() == "5")
                        {
                            dr_final8["Month"] = "May";
                        }
                        else if (fLvlValue.ToString() == "6")
                        {
                            dr_final8["Month"] = "June";
                        }
                        else if (fLvlValue.ToString() == "7")
                        {
                            dr_final8["Month"] = "July";
                        }
                        else if (fLvlValue.ToString() == "8")
                        {
                            dr_final8["Month"] = "August";
                        }
                        else if (fLvlValue.ToString() == "9")
                        {
                            dr_final8["Month"] = "September";
                        }
                        else if (fLvlValue.ToString() == "10")
                        {
                            dr_final8["Month"] = "October";
                        }
                        else if (fLvlValue.ToString() == "11")
                        {
                            dr_final8["Month"] = "November ";
                        }
                        else if (fLvlValue.ToString() == "12")
                        {
                            dr_final8["Month"] = "December ";
                        }

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();

                    if (dr["MonthName"].ToString() == "1")
                    {
                        dr_final12["Month"] = "January";
                    }
                    else if (dr["MonthName"].ToString() == "2")
                    {
                        dr_final12["Month"] = "February";
                    }
                    else if (dr["MonthName"].ToString() == "3")
                    {
                        dr_final12["Month"] = "March";
                    }
                    else if (dr["MonthName"].ToString() == "4")
                    {
                        dr_final12["Month"] = "April";
                    }
                    else if (dr["MonthName"].ToString() == "5")
                    {
                        dr_final12["Month"] = "May";
                    }
                    else if (dr["MonthName"].ToString() == "6")
                    {
                        dr_final12["Month"] = "June";
                    }
                    else if (dr["MonthName"].ToString() == "7")
                    {
                        dr_final12["Month"] = "July";
                    }
                    else if (dr["MonthName"].ToString() == "8")
                    {
                        dr_final12["Month"] = "August";
                    }
                    else if (dr["MonthName"].ToString() == "9")
                    {
                        dr_final12["Month"] = "September";
                    }
                    else if (dr["MonthName"].ToString() == "10")
                    {
                        dr_final12["Month"] = "October";
                    }
                    else if (dr["MonthName"].ToString() == "11")
                    {
                        dr_final12["Month"] = "November";
                    }
                    else if (dr["MonthName"].ToString() == "12")
                    {
                        dr_final12["Month"] = "December";
                    }


                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                }
            }

            DataRow dr_final89 = dt.NewRow();

            if (fLvlValue.ToString() == "1")
            {
                dr_final89["Month"] = "January";
            }
            else if (fLvlValue.ToString() == "2")
            {
                dr_final89["Month"] = "February";
            }
            else if (fLvlValue.ToString() == "3")
            {
                dr_final89["Month"] = "March";
            }
            else if (fLvlValue.ToString() == "4")
            {
                dr_final89["Month"] = "April";
            }
            else if (fLvlValue.ToString() == "5")
            {
                dr_final89["Month"] = "May";
            }
            else if (fLvlValue.ToString() == "6")
            {
                dr_final89["Month"] = "June";
            }
            else if (fLvlValue.ToString() == "7")
            {
                dr_final89["Month"] = "July";
            }
            else if (fLvlValue.ToString() == "8")
            {
                dr_final89["Month"] = "August";
            }
            else if (fLvlValue.ToString() == "9")
            {
                dr_final89["Month"] = "September";
            }
            else if (fLvlValue.ToString() == "10")
            {
                dr_final89["Month"] = "October";
            }
            else if (fLvlValue.ToString() == "11")
            {
                dr_final89["Month"] = "November ";
            }
            else if (fLvlValue.ToString() == "12")
            {
                dr_final89["Month"] = "December ";
            }

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Month"] = "Grand Total  ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataMonthWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "MonthWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Month"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["MonthName"].ToString();

                fLvlValue = fLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();

                if (dr["MonthName"].ToString() == "1")
                {
                    dr_final12["Month"] = "January";
                }
                else if (dr["MonthName"].ToString() == "2")
                {
                    dr_final12["Month"] = "February";
                }
                else if (dr["MonthName"].ToString() == "3")
                {
                    dr_final12["Month"] = "March";
                }
                else if (dr["MonthName"].ToString() == "4")
                {
                    dr_final12["Month"] = "April";
                }
                else if (dr["MonthName"].ToString() == "5")
                {
                    dr_final12["Month"] = "May";
                }
                else if (dr["MonthName"].ToString() == "6")
                {
                    dr_final12["Month"] = "June";
                }
                else if (dr["MonthName"].ToString() == "7")
                {
                    dr_final12["Month"] = "July";
                }
                else if (dr["MonthName"].ToString() == "8")
                {
                    dr_final12["Month"] = "August";
                }
                else if (dr["MonthName"].ToString() == "9")
                {
                    dr_final12["Month"] = "September";
                }
                else if (dr["MonthName"].ToString() == "10")
                {
                    dr_final12["Month"] = "October";
                }
                else if (dr["MonthName"].ToString() == "11")
                {
                    dr_final12["Month"] = "November";
                }
                else if (dr["MonthName"].ToString() == "12")
                {
                    dr_final12["Month"] = "December";
                }

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = (Convert.ToDouble(dr["rate"]));

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["NLC"])) / (Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["MRP"])) / (Convert.ToDouble(dr["MRP"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["DP"])) / (Convert.ToDouble(dr["DP"])));

                CategoryTotal = CategoryTotal + ( Convert.ToDouble(dr["rate"]));
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + (Convert.ToDouble(dr["rate"]));
                dt.Rows.Add(dr_final12);
            }
        }

        //DataRow dr_final879 = dt.NewRow();
        //dt.Rows.Add(dr_final879);

        //DataRow dr_final89 = dt.NewRow();
        //dr_final89["CategoryName"] = "Total : " + fLvlValueTemp;
        //dr_final89["Qty"] = "";
        //dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));
        //dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Month"] = "Grand Total : ";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataCustomerWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateqtyTotal = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "CustomerWise";
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Customer Name"));
        dt.Columns.Add(new DataColumn("Customer Address1"));
        dt.Columns.Add(new DataColumn("Customer Address2"));
        dt.Columns.Add(new DataColumn("Customer Address3"));
        dt.Columns.Add(new DataColumn("Category Name"));
        dt.Columns.Add(new DataColumn("Brand"));
        dt.Columns.Add(new DataColumn("ProductName"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["customername"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8886 = dt.NewRow();
                        dt.Rows.Add(dr_final8886);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Customer Name"] = "Total : " + fLvlValue;
                        dr_final8["ProductName"] = "";
                        dr_final8["Category Name"] = "";
                        dr_final8["Brand"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Customer Name"] = dr["customername"];
                    dr_final12["Customer Address1"] = dr["customeraddress"];
                    dr_final12["Customer Address2"] = dr["customeraddress2"];
                    dr_final12["Customer Address3"] = dr["customeraddress3"];
                    dr_final12["Category Name"] = dr["categoryname"];
                    dr_final12["Brand"] = dr["productdesc"];
                    dr_final12["ProductName"] = dr["ProductName"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    dr_final12["BillNo"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    //dr_final12["BillDate"] = dr["BillDate"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["NLC"])));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"]))-( (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"]))-((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["rate"]));

                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal - Convert.ToDouble(dr["rate1"]);
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["rate"]));

                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateqtyTotal = rateqtyTotal + Convert.ToDouble(dr["rate1"]);
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final88866 = dt.NewRow();
            dt.Rows.Add(dr_final88866);

            DataRow dr_final86 = dt.NewRow();
            dr_final86["Customer Name"] = "Total : " + fLvlValue;
            dr_final86["ProductName"] = "";
            dr_final86["Category Name"] = "";
            dr_final86["Brand"] = "";
            dr_final86["Model"] = "";
            dr_final86["Itemcode"] = "";
            dr_final86["BillNo"] = "";
            dr_final86["BillDate"] = "";
            if (chkboxQty.Checked == true)
                dr_final86["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final86["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final86["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final86["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final86["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final86["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final86["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final86["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final86["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final86["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final86["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final86["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final86["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            CategoryTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            dpvalueTotal = 0;
            gpformrpTotal = 0;
            gpfornlcTotal = 0;
            gpfordpTotal = 0;
            qtyTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final86);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Customer Name"] = "Grand Total : ";
            dr_final789["Category Name"] = "";
            dr_final789["Brand"] = "";
            dr_final789["ProductName"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillNo"] = "";
            dr_final789["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateqtyTotal));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataCustomerWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "CustomerWise";
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Customer Name"));      

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["customername"].ToString();

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Customer Name"] = fLvlValue;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        CategoryTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        dpvalueTotal = 0;
                        gpformrpTotal = 0;
                        gpfornlcTotal = 0;
                        gpfordpTotal = 0;
                        qtyTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);
                    }

                    fLvlValue = fLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Customer Name"] = dr["customername"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["aty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["rate"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = (Convert.ToDouble(dr["rate"]));
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["NLC"])));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["Qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = (Convert.ToDouble(dr["rate"])) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        CategoryTotal = CategoryTotal - (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);

                        total = total - (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                        total = total + (Convert.ToDouble(dr["rate"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["rate"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                }
            }

            DataRow dr_final86 = dt.NewRow();
            dr_final86["Customer Name"] = fLvlValue;

            if (chkboxQty.Checked == true)
                dr_final86["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxVal.Checked == true)
                dr_final86["Sales Value"] = Convert.ToString(Convert.ToDecimal(CategoryTotal));

            if (chkboxrate.Checked == true)
                dr_final86["Sales Rate"] = Convert.ToString(Convert.ToDecimal(rateTotal));

            if (chkboxPer.Checked == true)
                dr_final86["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(CategoryTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final86["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final86["NLC Per%"] = (CategoryTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final86["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final86["MRP Per%"] = (CategoryTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final86["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final86["DP Per%"] = (CategoryTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final86["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final86["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final86["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            dt.Rows.Add(dr_final86);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Customer Name"] = "Grand Total ";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataCustomerWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double CategoryTotal = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "CustomerWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Customer Name"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["customername"].ToString();

                fLvlValue = fLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["Customer Name"] = dr["customername"];

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = (Convert.ToDouble(dr["rate"]));

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["rate"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["NLC"])) / (Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["MRP"])) / (Convert.ToDouble(dr["MRP"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["rate"]) - Convert.ToDouble(dr["DP"])) / (Convert.ToDouble(dr["DP"])));

                CategoryTotal = CategoryTotal + (Convert.ToDouble(dr["rate"]));
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + (Convert.ToDouble(dr["rate"]));
                dt.Rows.Add(dr_final12);
            }
        }

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Customer Name"] = "Grand Total : ";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataCategoryBrandWiseGroupBy()
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
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        Types = "CategoryBrandWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double qty1Total = 0;

        double mrpvalTotal = 0;
        double nlcvalTotal = 0;
        double dpvalTotal = 0;
        double gpfmrpTotal = 0;
        double gpfnlcTotal = 0;
        double gpfdpTotal = 0;

        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Brand Name"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Categoryname"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = fLvlValue;
                        dr_final8["Brand Name"] = sLvlValue;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        producttot = 0;
                        qtyTotal = 0;
                        gpfordpTotal = 0;
                        gpfornlcTotal = 0;
                        dpvalueTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);
                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8887 = dt.NewRow();
                        dt.Rows.Add(dr_final8887);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "Total : " + fLvlValue;
                        dr_final8["Brand Name"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTot));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

                        brandTotal = 0;
                        gpfdpTotal = 0;
                        gpfnlcTotal = 0;
                        dpvalTotal = 0;
                        gpfmrpTotal = 0;
                        mrpvalTotal = 0;
                        nlcvalTotal = 0;
                        rateTot = 0;
                        qty1Total = 0;

                        brandTotal = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Category"] = dr["Categoryname"];
                    dr_final12["Brand Name"] = dr["Productdesc"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"])-(Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"])-(Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - (Convert.ToDouble(dr["amount"]));
                        producttot = producttot - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["amount"]));
                        qty1Total = qty1Total - Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                        producttot = producttot + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["amount"]));
                        qty1Total = qty1Total + Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalTotal = mrpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalTotal = nlcvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalTotal = dpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpfmrpTotal = gpfmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfnlcTotal = gpfnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfdpTotal = gpfdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                }
            }

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Category"] = fLvlValue;
            dr_final89["Brand Name"] = sLvlValue;

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

            if (chkboxAvg.Checked == true)
                dr_final89["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            producttot = 0;
            qtyTotal = 0;
            gpfordpTotal = 0;
            gpfornlcTotal = 0;
            dpvalueTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799["Category"] = "Total : " + fLvlValue;
            dr_final8799["Brand Name"] = "";

            if (chkboxQty.Checked == true)
                dr_final8799["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

            if (chkboxrate.Checked == true)
                dr_final8799["Sales Rate"] = (Convert.ToDouble(rateTot));

            if (chkboxVal.Checked == true)
                dr_final8799["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final8799["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final8799["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final8799["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

            if (chkboxNlcper.Checked == true)
                dr_final8799["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final8799["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

            if (chkboxMRPper.Checked == true)
                dr_final8799["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final8799["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

            if (chkboxDpper.Checked == true)
                dr_final8799["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

            if (chkgpmrp.Checked == true)
                dr_final8799["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final8799["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final8799["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

            brandTotal = 0;
            gpfdpTotal = 0;
            gpfnlcTotal = 0;
            dpvalTotal = 0;
            gpfmrpTotal = 0;
            mrpvalTotal = 0;
            nlcvalTotal = 0;
            rateTot = 0;
            qty1Total = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Category"] = "Grand Total : ";
            dr_final789["Brand Name"] = "";


            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataCategoryBrandWiseNormal()
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
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double qty1Total = 0;

        double mrpvalTotal = 0;
        double nlcvalTotal = 0;
        double dpvalTotal = 0;
        double gpfmrpTotal = 0;
        double gpfnlcTotal = 0;
        double gpfdpTotal = 0;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        Types = "CategoryBrandWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("ProductName"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["Categoryname"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final889 = dt.NewRow();
                        dt.Rows.Add(dr_final889);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "";
                        dr_final8["Brand Name"] = "Total : " + sLvlValue;
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["ProductName"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        producttot = 0;
                        qtyTotal = 0;
                        gpfordpTotal = 0;
                        gpfornlcTotal = 0;
                        dpvalueTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "Total : " + fLvlValue;
                        dr_final8["Brand Name"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["ProductName"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTot));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

                        brandTotal = 0;
                        gpfdpTotal = 0;
                        gpfnlcTotal = 0;
                        dpvalTotal = 0;
                        gpfmrpTotal = 0;
                        mrpvalTotal = 0;
                        nlcvalTotal = 0;
                        rateTot = 0;
                        qty1Total = 0;

                        brandTotal = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Category"] = dr["Categoryname"];
                    dr_final12["Brand Name"] = dr["Productdesc"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    dr_final12["ProductName"] = dr["ProductName"];
                    dr_final12["BillNo"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    //dr_final12["BillDate"] = dr["BillDate"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["DP"]));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - (Convert.ToDouble(dr["amount"]));
                        producttot = producttot - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        total = total - (Convert.ToDouble(dr["amount"]));

                        qty1Total = qty1Total - Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                        producttot = producttot + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        total = total + (Convert.ToDouble(dr["amount"]));

                        qty1Total = qty1Total + Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalTotal = mrpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalTotal = nlcvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalTotal = dpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpfmrpTotal = gpfmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfnlcTotal = gpfnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfdpTotal = gpfdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Category"] = "";
            dr_final89["Brand Name"] = "Total : " + sLvlValue;
            dr_final89["Model"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["ProductName"] = "";
            dr_final89["BillNo"] = "";
            dr_final89["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

            if (chkboxAvg.Checked == true)
                dr_final89["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            producttot = 0;
            qtyTotal = 0;
            gpfordpTotal = 0;
            gpfornlcTotal = 0;
            dpvalueTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            rateTotal = 0;

            dt.Rows.Add(dr_final89);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799["Category"] = "Total : " + fLvlValue;
            dr_final8799["Brand Name"] = "";
            dr_final8799["Model"] = "";
            dr_final8799["Itemcode"] = "";
            dr_final8799["ProductName"] = "";
            dr_final8799["BillNo"] = "";
            dr_final8799["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final8799["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

            if (chkboxrate.Checked == true)
                dr_final8799["Sales Rate"] = (Convert.ToDouble(rateTot));

            if (chkboxVal.Checked == true)
                dr_final8799["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final8799["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final8799["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final8799["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

            if (chkboxNlcper.Checked == true)
                dr_final8799["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final8799["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

            if (chkboxMRPper.Checked == true)
                dr_final8799["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final8799["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

            if (chkboxDpper.Checked == true)
                dr_final8799["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

            if (chkgpmrp.Checked == true)
                dr_final8799["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final8799["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final8799["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

            brandTotal = 0;
            gpfdpTotal = 0;
            gpfnlcTotal = 0;
            dpvalTotal = 0;
            gpfmrpTotal = 0;
            mrpvalTotal = 0;
            nlcvalTotal = 0;
            rateTot = 0;
            qty1Total = 0;

            brandTotal = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Category"] = "Grand Total : ";
            dr_final789["Brand Name"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["ProductName"] = "";
            dr_final789["BillNo"] = "";
            dr_final789["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataCategoryBrandWise()
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
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;

        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "CategoryBrandWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Brand Name"));
        
        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Categoryname"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final889 = dt.NewRow();
                    dt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Category"] = "";
                    dr_final8["Brand Name"] = "Total : " + sLvlValue;

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";


                    producttot = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Category"] = "Total : " + fLvlValue;
                    dr_final8["Brand Name"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";

                    brandTotal = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["Category"] = dr["Categoryname"];
                dr_final12["Brand Name"] = dr["Productdesc"];

                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = Convert.ToDouble(dr["amount"]);

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["NLC"])) / (Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["MRP"])) / (Convert.ToDouble(dr["MRP"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["DP"])) / (Convert.ToDouble(dr["DP"])));

                brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                producttot = producttot + (Convert.ToDouble(dr["amount"]));
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);

                total = total + (Convert.ToDouble(dr["amount"]));
                dt.Rows.Add(dr_final12);
            }
        }

        DataRow dr_final879 = dt.NewRow();
        dt.Rows.Add(dr_final879);

        DataRow dr_final89 = dt.NewRow();
        dr_final89["Category"] = "";
        dr_final89["Brand Name"] = "Total : " + sLvlValue;

        if (chkboxQty.Checked == true)
            dr_final89["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        if (chkboxAvg.Checked == true)
            dr_final89["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final89["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final89["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final89["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final89["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final89["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final89["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final89["DP Per%"] = "";

        dt.Rows.Add(dr_final89);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final8799 = dt.NewRow();
        dr_final8799["Category"] = "Total : " + fLvlValue;
        dr_final8799["Brand Name"] = "";

        if (chkboxQty.Checked == true)
            dr_final8799["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final8799["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

        if (chkboxAvg.Checked == true)
            dr_final8799["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final8799["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final8799["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final8799["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final8799["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final8799["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final8799["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final8799["DP Per%"] = "";

        dt.Rows.Add(dr_final8799);

        DataRow dr_final77879 = dt.NewRow();
        dt.Rows.Add(dr_final77879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Category"] = "Grand Total : ";
        dr_final789["Brand Name"] = "";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    public void bindDataCategoryBrandProductWiseNormal()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        double brandTotal = 0;
        double producttot = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        double modeltot = 0;
        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text; 
        Types = "CategoryBrandProductWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double qty1Total = 0;

        double mrpvalTotal = 0;
        double nlcvalTotal = 0;
        double dpvalTotal = 0;
        double gpfmrpTotal = 0;
        double gpfnlcTotal = 0;
        double gpfdpTotal = 0;

        double nlcvTotal = 0;
        double mrpvTotal = 0;
        double dpvTotal = 0;
        double gpmTotal = 0;
        double gpnTotal = 0;
        double gpdTotal = 0;
        double rate2Total = 0;
        double qty2Total = 0;

        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Product Name"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Itemcode"));
        dt.Columns.Add(new DataColumn("BillNo"));
        dt.Columns.Add(new DataColumn("BillDate"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if(ds!=null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                    tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                    (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final889 = dt.NewRow();
                        dt.Rows.Add(dr_final889);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "";
                        dr_final8["Brand Name"] = "";
                        dr_final8["Product Name"] = "Total : " + tLvlValue;
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty2Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rate2Total));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty2Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvTotal) / (nlcvTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvTotal) / (mrpvTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvTotal) / (dpvTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdTotal));

                        qty2Total = 0;
                        gpdTotal = 0;
                        gpnTotal = 0;
                        dpvTotal = 0;
                        mrpvTotal = 0;
                        nlcvTotal = 0;
                        rate2Total = 0;
                        gpmTotal = 0;
                        modeltot = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "";
                        dr_final8["Brand Name"] = "Total : " + sLvlValue;
                        dr_final8["Product Name"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        producttot = 0;
                        qtyTotal = 0;
                        gpfordpTotal = 0;
                        gpfornlcTotal = 0;
                        dpvalueTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        rateTotal = 0;

                        producttot = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "Total : " + fLvlValue;
                        dr_final8["Brand Name"] = "";
                        dr_final8["Product Name"] = "";
                        dr_final8["Model"] = "";
                        dr_final8["Itemcode"] = "";
                        dr_final8["BillNo"] = "";
                        dr_final8["BillDate"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTot));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

                        brandTotal = 0;
                        gpfdpTotal = 0;
                        gpfnlcTotal = 0;
                        dpvalTotal = 0;
                        gpfmrpTotal = 0;
                        mrpvalTotal = 0;
                        nlcvalTotal = 0;
                        rateTot = 0;
                        qty1Total = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Category"] = dr["CategoryName"];
                    dr_final12["Brand Name"] = dr["Productdesc"];
                    dr_final12["Product Name"] = dr["ProductName"];
                    dr_final12["Model"] = dr["Model"];
                    dr_final12["Itemcode"] = dr["Itemcode"];
                    dr_final12["BillNo"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["BillDate"] = dtaa;

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) *Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) *Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"])-((Convert.ToDouble(dr["Qty"]) *Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"])-((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - (Convert.ToDouble(dr["amount"]));
                        producttot = producttot - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        modeltot = modeltot - (Convert.ToDouble(dr["amount"]));
                        total = total - (Convert.ToDouble(dr["amount"]));
                        rate2Total = rate2Total - (Convert.ToDouble(dr["rate1"]));
                        qty2Total = qty2Total - Convert.ToDouble(dr["qty"]);
                        qty1Total = qty1Total - Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                        producttot = producttot + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        modeltot = modeltot + (Convert.ToDouble(dr["amount"]));
                        total = total + (Convert.ToDouble(dr["amount"]));
                        rate2Total = rate2Total + (Convert.ToDouble(dr["rate1"]));
                        qty2Total = qty2Total + Convert.ToDouble(dr["qty"]);
                        qty1Total = qty1Total + Convert.ToDouble(dr["qty"]);
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalTotal = mrpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalTotal = nlcvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalTotal = dpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpfmrpTotal = gpfmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfnlcTotal = gpfnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfdpTotal = gpfdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvTotal = mrpvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvTotal = nlcvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvTotal = dpvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmTotal = gpmTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnTotal = gpnTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdTotal = gpdTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                   

                    dt.Rows.Add(dr_final12);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final899 = dt.NewRow();
            dr_final899["Category"] = "";
            dr_final899["Brand Name"] = "";
            dr_final899["Product Name"] = "Total : " + tLvlValue;
            dr_final899["Model"] = "";
            dr_final899["Itemcode"] = "";
            dr_final899["BillNo"] = "";
            dr_final899["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final899["Qty"] = Convert.ToString(Convert.ToDecimal(qty2Total));

            if (chkboxrate.Checked == true)
                dr_final899["Sales Rate"] = (Convert.ToDouble(rate2Total));

            if (chkboxVal.Checked == true)
                dr_final899["Sales Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

            if (chkboxAvg.Checked == true)
                dr_final899["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final899["Per%"] = (100 / (Convert.ToDouble(qty2Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final899["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvTotal));

            if (chkboxNlcper.Checked == true)
                dr_final899["NLC Per%"] = (brandTotal - nlcvTotal) / (nlcvTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final899["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvTotal));

            if (chkboxMRPper.Checked == true)
                dr_final899["MRP Per%"] = (brandTotal - mrpvTotal) / (mrpvTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final899["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvTotal));

            if (chkboxDpper.Checked == true)
                dr_final899["DP Per%"] = (brandTotal - dpvTotal) / (dpvTotal);

            if (chkgpmrp.Checked == true)
                dr_final899["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmTotal));

            if (chkgpnlc.Checked == true)
                dr_final899["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnTotal));

            if (chkgpdp.Checked == true)
                dr_final899["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdTotal));

            qty2Total = 0;
            gpdTotal = 0;
            gpnTotal = 0;
            dpvTotal = 0;
            mrpvTotal = 0;
            nlcvTotal = 0;
            rate2Total = 0;
            gpmTotal = 0;
            modeltot = 0;

            dt.Rows.Add(dr_final899);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Category"] = "";
            dr_final89["Brand Name"] = "Total : " + sLvlValue;
            dr_final89["Product Name"] = "";
            dr_final89["Model"] = "";
            dr_final89["Itemcode"] = "";
            dr_final89["BillNo"] = "";
            dr_final89["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

            if (chkboxAvg.Checked == true)
                dr_final89["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            producttot = 0;
            qtyTotal = 0;
            gpfordpTotal = 0;
            gpfornlcTotal = 0;
            dpvalueTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            rateTotal = 0;
            dt.Rows.Add(dr_final89);

            DataRow dr_final88789 = dt.NewRow();
            dt.Rows.Add(dr_final88789);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799["Category"] = "Total : " + fLvlValue;
            dr_final8799["Brand Name"] = "";
            dr_final8799["Product Name"] = "";
            dr_final8799["Model"] = "";
            dr_final8799["Itemcode"] = "";
            dr_final8799["BillNo"] = "";
            dr_final8799["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final8799["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

            if (chkboxrate.Checked == true)
                dr_final8799["Sales Rate"] = (Convert.ToDouble(rateTot));

            if (chkboxVal.Checked == true)
                dr_final8799["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final8799["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final8799["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final8799["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

            if (chkboxNlcper.Checked == true)
                dr_final8799["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final8799["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

            if (chkboxMRPper.Checked == true)
                dr_final8799["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final8799["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

            if (chkboxDpper.Checked == true)
                dr_final8799["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

            if (chkgpmrp.Checked == true)
                dr_final8799["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final8799["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final8799["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

            brandTotal = 0;
            gpfdpTotal = 0;
            gpfnlcTotal = 0;
            dpvalTotal = 0;
            gpfmrpTotal = 0;
            mrpvalTotal = 0;
            nlcvalTotal = 0;
            rateTot = 0;
            qty1Total = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Category"] = "Grand Total : ";
            dr_final789["Brand Name"] = "";
            dr_final789["Product Name"] = "";
            dr_final789["Model"] = "";
            dr_final789["Itemcode"] = "";
            dr_final789["BillNo"] = "";
            dr_final789["BillDate"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataCategoryBrandProductWiseGroupBy()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        double brandTotal = 0;
        double producttot = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        double modeltot = 0;
        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;
        string options = string.Empty;
        options = opttype.SelectedItem.Text;
        Types = "CategoryBrandProductWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        double mrpvalueTotal = 0;
        double nlcvalueTotal = 0;
        double dpvalueTotal = 0;
        double gpformrpTotal = 0;
        double gpfornlcTotal = 0;
        double gpfordpTotal = 0;

        double rateTotal = 0;
        double rateTotal1 = 0;
        double rateTot = 0;

        double mrpTotal = 0;
        double nlcTotal = 0;
        double dpTotal = 0;
        double gpmrpTotal = 0;
        double gpnlcTotal = 0;
        double gpdpTotal = 0;
        double qtyTotal = 0;
        double qty1Total = 0;

        double mrpvalTotal = 0;
        double nlcvalTotal = 0;
        double dpvalTotal = 0;
        double gpfmrpTotal = 0;
        double gpfnlcTotal = 0;
        double gpfdpTotal = 0;

        double nlcvTotal = 0;
        double mrpvTotal = 0;
        double dpvTotal = 0;
        double gpmTotal = 0;
        double gpnTotal = 0;
        double gpdTotal = 0;
        double rate2Total = 0;
        double qty2Total = 0;

        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Product Name"));

        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxrate.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Rate"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Sales Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        if (chkgpmrp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for MRP"));

        if (chkgpnlc.Checked == true)
            dt.Columns.Add(new DataColumn("GP for NLC"));

        if (chkgpdp.Checked == true)
            dt.Columns.Add(new DataColumn("GP for DP"));

        string salrettype = string.Empty;
        if (withsalreturn.Checked == true)
        {
            salrettype = "YES";
        }
        else
        {
            salrettype = "NO";
        }
        ds = objBL.getSaleslistNormal(startDate, endDate, Types, options,salrettype);

        ds = objBL.getallhistoryrate(sDataSource, ds);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                    sLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                    tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                    (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                    {

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = fLvlValue;
                        dr_final8["Brand Name"] = sLvlValue;
                        dr_final8["Product Name"] = tLvlValue;

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty2Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rate2Total));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty2Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvTotal) / (nlcvTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvTotal) / (mrpvTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvTotal) / (dpvTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdTotal));

                        qty2Total = 0;
                        gpdTotal = 0;
                        gpnTotal = 0;
                        dpvTotal = 0;
                        mrpvTotal = 0;
                        nlcvTotal = 0;
                        rate2Total = 0;
                        gpmTotal = 0;
                        modeltot = 0;
                        dt.Rows.Add(dr_final8);

                    }

                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);

                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "";
                        dr_final8["Brand Name"] = "Total : " +sLvlValue;
                        dr_final8["Product Name"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTotal));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

                        producttot = 0;
                        qtyTotal = 0;
                        gpfordpTotal = 0;
                        gpfornlcTotal = 0;
                        dpvalueTotal = 0;
                        mrpvalueTotal = 0;
                        nlcvalueTotal = 0;
                        rateTotal = 0;

                        dt.Rows.Add(dr_final8);

                        DataRow dr_final8889 = dt.NewRow();
                        dt.Rows.Add(dr_final8889);
                    }

                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        dr_final8["Category"] = "Total : " + fLvlValue;
                        dr_final8["Brand Name"] = "";
                        dr_final8["Product Name"] = "";

                        if (chkboxQty.Checked == true)
                            dr_final8["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

                        if (chkboxrate.Checked == true)
                            dr_final8["Sales Rate"] = (Convert.ToDouble(rateTot));

                        if (chkboxVal.Checked == true)
                            dr_final8["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                        if (chkboxAvg.Checked == true)
                            dr_final8["Avg"] = "";

                        if (chkboxPer.Checked == true)
                            dr_final8["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

                        if (chkboxNlcvalue.Checked == true)
                            dr_final8["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

                        if (chkboxNlcper.Checked == true)
                            dr_final8["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

                        if (chkboxMRPvalue.Checked == true)
                            dr_final8["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

                        if (chkboxMRPper.Checked == true)
                            dr_final8["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

                        if (chkboxDpvalue.Checked == true)
                            dr_final8["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

                        if (chkboxDpper.Checked == true)
                            dr_final8["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

                        if (chkgpmrp.Checked == true)
                            dr_final8["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

                        if (chkgpnlc.Checked == true)
                            dr_final8["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

                        if (chkgpdp.Checked == true)
                            dr_final8["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

                        brandTotal = 0;
                        gpfdpTotal = 0;
                        gpfnlcTotal = 0;
                        dpvalTotal = 0;
                        gpfmrpTotal = 0;
                        mrpvalTotal = 0;
                        nlcvalTotal = 0;
                        rateTot = 0;
                        qty1Total = 0;
                        dt.Rows.Add(dr_final8);

                        DataRow dr_final888 = dt.NewRow();
                        dt.Rows.Add(dr_final888);
                    }
                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;

                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Category"] = dr["CategoryName"];
                    dr_final12["Brand Name"] = dr["Productdesc"];
                    dr_final12["Product Name"] = dr["ProductName"];

                    if (chkboxQty.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Qty"] = "-" + (Convert.ToDouble(dr["qty"]));
                        }
                        else
                        {
                            dr_final12["Qty"] = dr["Qty"];
                        }
                    }

                    if (chkboxrate.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Rate"] = "-" + (Convert.ToDouble(dr["rate1"]));
                        }
                        else
                        {
                            dr_final12["Sales Rate"] = (Convert.ToDouble(dr["rate1"]));
                        }
                    }

                    if (chkboxVal.Checked == true)
                    {
                        if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                        {
                            dr_final12["Sales Value"] = "-" + (Convert.ToDouble(dr["amount"]));
                        }
                        else
                        {
                            dr_final12["Sales Value"] = Convert.ToDouble(dr["amount"]);
                        }
                    }

                    if (chkboxAvg.Checked == true)
                        dr_final12["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                    if (chkboxNlcvalue.Checked == true)
                        dr_final12["NLC Value"] = (Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"]));

                    if (chkboxNlcper.Checked == true)
                        dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["NLC"]))));

                    if (chkboxMRPvalue.Checked == true)
                        dr_final12["MRP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));

                    if (chkboxMRPper.Checked == true)
                        dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["MRP"]))));

                    if (chkboxDpvalue.Checked == true)
                        dr_final12["DP Value"] = (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));

                    if (chkboxDpper.Checked == true)
                        dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]))) / (Convert.ToDouble(dr["qty"]) * (Convert.ToDouble(dr["DP"]))));

                    if (chkgpmrp.Checked == true)
                        dr_final12["GP for MRP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));

                    if (chkgpnlc.Checked == true)
                        dr_final12["GP for NLC"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["qty"]) * Convert.ToDouble(dr["NLC"])));

                    if (chkgpdp.Checked == true)
                        dr_final12["GP for DP"] = Convert.ToDouble(dr["amount"]) - ((Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    if (Convert.ToString(dr["vouchertype"]) == "Sales Return")
                    {
                        brandTotal = brandTotal - (Convert.ToDouble(dr["amount"]));
                        producttot = producttot - (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal - Convert.ToDouble(dr["qty"]);
                        modeltot = modeltot - (Convert.ToDouble(dr["amount"]));
                        total = total - (Convert.ToDouble(dr["amount"]));
                        qtyTotal = qtyTotal - Convert.ToDouble(dr["qty"]);
                        qty1Total = qty1Total - Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal - (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 - (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot - (Convert.ToDouble(dr["rate1"]));
                    }
                    else
                    {
                        brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                        producttot = producttot + (Convert.ToDouble(dr["amount"]));
                        CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                        modeltot = modeltot + (Convert.ToDouble(dr["amount"]));
                        total = total + (Convert.ToDouble(dr["amount"]));
                        qtyTotal = qtyTotal + Convert.ToDouble(dr["qty"]);
                        qty1Total = qty1Total + Convert.ToDouble(dr["qty"]);

                        rateTotal = rateTotal + (Convert.ToDouble(dr["rate1"]));
                        rateTotal1 = rateTotal1 + (Convert.ToDouble(dr["rate1"]));
                        rateTot = rateTot + (Convert.ToDouble(dr["rate1"]));
                    }

                    mrpvalTotal = mrpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalTotal = nlcvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalTotal = dpvalTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpfmrpTotal = gpfmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfnlcTotal = gpfnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfdpTotal = gpfdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvalueTotal = mrpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvalueTotal = nlcvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvalueTotal = dpvalueTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpformrpTotal = gpformrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpfornlcTotal = gpfornlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpfordpTotal = gpfordpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    
                    mrpTotal = mrpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcTotal = nlcTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpTotal = dpTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmrpTotal = gpmrpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnlcTotal = gpnlcTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdpTotal = gpdpTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));

                    mrpvTotal = mrpvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"]));
                    nlcvTotal = nlcvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"]));
                    dpvTotal = dpvTotal + (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"]));
                    gpmTotal = gpmTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["MRP"])));
                    gpnTotal = gpnTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["NLC"])));
                    gpdTotal = gpdTotal + (Convert.ToDouble(dr["amount"]) - (Convert.ToDouble(dr["Qty"]) * Convert.ToDouble(dr["DP"])));
                    rate2Total = rate2Total + (Convert.ToDouble(dr["rate1"]));
                    qty2Total = qty2Total + Convert.ToDouble(dr["qty"]);
                }
            }

            DataRow dr_final879 = dt.NewRow();
            dt.Rows.Add(dr_final879);

            DataRow dr_final899 = dt.NewRow();
            dr_final899["Category"] = fLvlValue;
            dr_final899["Brand Name"] = sLvlValue;
            dr_final899["Product Name"] = tLvlValue;

            if (chkboxQty.Checked == true)
                dr_final899["Qty"] = Convert.ToString(Convert.ToDecimal(qty2Total));

            if (chkboxrate.Checked == true)
                dr_final899["Sales Rate"] = (Convert.ToDouble(rate2Total));

            if (chkboxVal.Checked == true)
                dr_final899["Sales Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

            if (chkboxAvg.Checked == true)
                dr_final899["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final899["Per%"] = (100 / (Convert.ToDouble(qty2Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final899["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvTotal));

            if (chkboxNlcper.Checked == true)
                dr_final899["NLC Per%"] = (brandTotal - nlcvTotal) / (nlcvTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final899["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvTotal));

            if (chkboxMRPper.Checked == true)
                dr_final899["MRP Per%"] = (brandTotal - mrpvTotal) / (mrpvTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final899["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvTotal));

            if (chkboxDpper.Checked == true)
                dr_final899["DP Per%"] = (brandTotal - dpvTotal) / (dpvTotal);

            if (chkgpmrp.Checked == true)
                dr_final899["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmTotal));

            if (chkgpnlc.Checked == true)
                dr_final899["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnTotal));

            if (chkgpdp.Checked == true)
                dr_final899["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdTotal));

            qty2Total = 0;
            gpdTotal = 0;
            gpnTotal = 0;
            dpvTotal = 0;
            mrpvTotal = 0;
            nlcvTotal = 0;
            rate2Total = 0;
            modeltot = 0;
            gpmTotal = 0;
            dt.Rows.Add(dr_final899);

            DataRow dr_final8879 = dt.NewRow();
            dt.Rows.Add(dr_final8879);

            DataRow dr_final89 = dt.NewRow();
            dr_final89["Category"] = "";
            dr_final89["Brand Name"] = "Total : " +sLvlValue;
            dr_final89["Product Name"] = "";

            if (chkboxQty.Checked == true)
                dr_final89["Qty"] = Convert.ToString(Convert.ToDecimal(qtyTotal));

            if (chkboxrate.Checked == true)
                dr_final89["Sales Rate"] = (Convert.ToDouble(rateTotal));

            if (chkboxVal.Checked == true)
                dr_final89["Sales Value"] = Convert.ToString(Convert.ToDecimal(producttot));

            if (chkboxAvg.Checked == true)
                dr_final89["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final89["Per%"] = (100 / (Convert.ToDouble(qtyTotal) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final89["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalueTotal));

            if (chkboxNlcper.Checked == true)
                dr_final89["NLC Per%"] = (brandTotal - nlcvalueTotal) / (nlcvalueTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final89["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalueTotal));

            if (chkboxMRPper.Checked == true)
                dr_final89["MRP Per%"] = (brandTotal - mrpvalueTotal) / (mrpvalueTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final89["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalueTotal));

            if (chkboxDpper.Checked == true)
                dr_final89["DP Per%"] = (brandTotal - dpvalueTotal) / (dpvalueTotal);

            if (chkgpmrp.Checked == true)
                dr_final89["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpformrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final89["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfornlcTotal));

            if (chkgpdp.Checked == true)
                dr_final89["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfordpTotal));

            producttot = 0;
            qtyTotal = 0;
            gpfordpTotal = 0;
            gpfornlcTotal = 0;
            dpvalueTotal = 0;
            mrpvalueTotal = 0;
            nlcvalueTotal = 0;
            rateTotal = 0;
            dt.Rows.Add(dr_final89);

            DataRow dr_final88789 = dt.NewRow();
            dt.Rows.Add(dr_final88789);

            DataRow dr_final8799 = dt.NewRow();
            dr_final8799["Category"] = "Total : " + fLvlValue;
            dr_final8799["Brand Name"] = "";
            dr_final8799["Product Name"] = "";


            if (chkboxQty.Checked == true)
                dr_final8799["Qty"] = Convert.ToString(Convert.ToDecimal(qty1Total));

            if (chkboxrate.Checked == true)
                dr_final8799["Sales Rate"] = (Convert.ToDouble(rateTot));

            if (chkboxVal.Checked == true)
                dr_final8799["Sales Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

            if (chkboxAvg.Checked == true)
                dr_final8799["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final8799["Per%"] = (100 / (Convert.ToDouble(qty1Total) * Convert.ToDouble(brandTotal))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final8799["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcvalTotal));

            if (chkboxNlcper.Checked == true)
                dr_final8799["NLC Per%"] = (brandTotal - nlcvalTotal) / (nlcvalTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final8799["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpvalTotal));

            if (chkboxMRPper.Checked == true)
                dr_final8799["MRP Per%"] = (brandTotal - mrpvalTotal) / (mrpvalTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final8799["DP Value"] = Convert.ToString(Convert.ToDecimal(dpvalTotal));

            if (chkboxDpper.Checked == true)
                dr_final8799["DP Per%"] = (brandTotal - dpvalTotal) / (dpvalTotal);

            if (chkgpmrp.Checked == true)
                dr_final8799["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpfmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final8799["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpfnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final8799["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpfdpTotal));

            brandTotal = 0;
            gpfdpTotal = 0;
            gpfnlcTotal = 0;
            dpvalTotal = 0;
            gpfmrpTotal = 0;
            mrpvalTotal = 0;
            nlcvalTotal = 0;
            rateTot = 0;
            qty1Total = 0;

            dt.Rows.Add(dr_final8799);

            DataRow dr_final77879 = dt.NewRow();
            dt.Rows.Add(dr_final77879);

            DataRow dr_final789 = dt.NewRow();
            dr_final789["Category"] = "Grand Total  ";
            dr_final789["Brand Name"] = "";
            dr_final789["Product Name"] = "";

            if (chkboxQty.Checked == true)
                dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

            if (chkboxrate.Checked == true)
                dr_final789["Sales Rate"] = (Convert.ToDouble(rateTotal1));

            if (chkboxVal.Checked == true)
                dr_final789["Sales Value"] = Convert.ToString(Convert.ToDecimal(total));

            if (chkboxAvg.Checked == true)
                dr_final789["Avg"] = "";

            if (chkboxPer.Checked == true)
                dr_final789["Per%"] = (100 / (Convert.ToDouble(CategoryqtyTotal) * Convert.ToDouble(total))) * 100;

            if (chkboxNlcvalue.Checked == true)
                dr_final789["NLC Value"] = Convert.ToString(Convert.ToDecimal(nlcTotal));

            if (chkboxNlcper.Checked == true)
                dr_final789["NLC Per%"] = (total - nlcTotal) / (nlcTotal);

            if (chkboxMRPvalue.Checked == true)
                dr_final789["MRP Value"] = Convert.ToString(Convert.ToDecimal(mrpTotal));

            if (chkboxMRPper.Checked == true)
                dr_final789["MRP Per%"] = (total - mrpTotal) / (mrpTotal);

            if (chkboxDpvalue.Checked == true)
                dr_final789["DP Value"] = Convert.ToString(Convert.ToDecimal(dpTotal));

            if (chkboxDpper.Checked == true)
                dr_final789["DP Per%"] = (total - dpTotal) / (dpTotal);

            if (chkgpmrp.Checked == true)
                dr_final789["GP for MRP"] = Convert.ToString(Convert.ToDecimal(gpmrpTotal));

            if (chkgpnlc.Checked == true)
                dr_final789["GP for NLC"] = Convert.ToString(Convert.ToDecimal(gpnlcTotal));

            if (chkgpdp.Checked == true)
                dr_final789["GP for DP"] = Convert.ToString(Convert.ToDecimal(gpdpTotal));

            dt.Rows.Add(dr_final789);

            ExportToExcel(dt);
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

    public void bindDataCategoryBrandProductWise()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        double brandTotal = 0;
        double producttot = 0;
        double CategoryqtyTotal = 0;
        double total = 0;
        string brand = string.Empty;
        string product = string.Empty;
        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        double modeltot = 0;
        startDate = Convert.ToDateTime(txtstdate.Text);
        endDate = Convert.ToDateTime(txteddate.Text);
        string Types = string.Empty;

        Types = "CategoryBrandProductWise";
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Brand Name"));
        dt.Columns.Add(new DataColumn("Product Name"));
        
        if (chkboxQty.Checked == true)
            dt.Columns.Add(new DataColumn("Qty"));

        if (chkboxVal.Checked == true)
            dt.Columns.Add(new DataColumn("Value"));

        if (chkboxAvg.Checked == true)
            dt.Columns.Add(new DataColumn("Avg"));

        if (chkboxPer.Checked == true)
            dt.Columns.Add(new DataColumn("Per%"));

        if (chkboxNlcvalue.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Value"));

        if (chkboxNlcper.Checked == true)
            dt.Columns.Add(new DataColumn("NLC Per%"));

        if (chkboxMRPvalue.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Value"));

        if (chkboxMRPper.Checked == true)
            dt.Columns.Add(new DataColumn("MRP Per%"));

        if (chkboxDpvalue.Checked == true)
            dt.Columns.Add(new DataColumn("DP Value"));

        if (chkboxDpper.Checked == true)
            dt.Columns.Add(new DataColumn("DP Per%"));

        ds = objBL.getSaleslist(startDate, endDate, Types);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr_final11 = dt.NewRow();
            dt.Rows.Add(dr_final11);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["Productdesc"].ToString().ToUpper().Trim();
                tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                {
                    DataRow dr_final889 = dt.NewRow();
                    dt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Category"] = "";
                    dr_final8["Brand Name"] = "";
                    dr_final8["Product Name"] = "Total : " + tLvlValue;

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";

                    modeltot = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Category"] = "";
                    dr_final8["Brand Name"] = "Total : " + sLvlValue;
                    dr_final8["Product Name"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";

                    producttot = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dt.NewRow();
                    dr_final8["Category"] = "Total : " + fLvlValue;
                    dr_final8["Brand Name"] = "";
                    dr_final8["Product Name"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Qty"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

                    if (chkboxAvg.Checked == true)
                        dr_final8["Avg"] = "";

                    if (chkboxPer.Checked == true)
                        dr_final8["Per%"] = "";

                    if (chkboxNlcvalue.Checked == true)
                        dr_final8["NLC Value"] = "";

                    if (chkboxNlcper.Checked == true)
                        dr_final8["NLC Per%"] = "";

                    if (chkboxMRPvalue.Checked == true)
                        dr_final8["MRP Value"] = "";

                    if (chkboxMRPper.Checked == true)
                        dr_final8["MRP Per%"] = "";

                    if (chkboxDpvalue.Checked == true)
                        dr_final8["DP Value"] = "";

                    if (chkboxDpper.Checked == true)
                        dr_final8["DP Per%"] = "";

                    brandTotal = 0;
                    dt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;

                DataRow dr_final12 = dt.NewRow();
                dr_final12["Category"] = dr["CategoryName"];
                dr_final12["Brand Name"] = dr["Productdesc"];
                dr_final12["Product Name"] = dr["ProductName"];
                
                if (chkboxQty.Checked == true)
                    dr_final12["Qty"] = dr["Qty"];

                if (chkboxVal.Checked == true)
                    dr_final12["Value"] = Convert.ToDouble(dr["amount"]);

                if (chkboxAvg.Checked == true)
                    dr_final12["Avg"] = "";

                if (chkboxPer.Checked == true)
                    dr_final12["Per%"] = (100 / Convert.ToDouble(dr["amount"])) * 100;

                if (chkboxNlcvalue.Checked == true)
                    dr_final12["NLC Value"] = Convert.ToDouble(dr["NLC"]);

                if (chkboxNlcper.Checked == true)
                    dr_final12["NLC Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["NLC"])) / (Convert.ToDouble(dr["NLC"])));

                if (chkboxMRPvalue.Checked == true)
                    dr_final12["MRP Value"] = Convert.ToDouble(dr["MRP"]);

                if (chkboxMRPper.Checked == true)
                    dr_final12["MRP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["MRP"])) / (Convert.ToDouble(dr["MRP"])));

                if (chkboxDpvalue.Checked == true)
                    dr_final12["DP Value"] = Convert.ToDouble(dr["DP"]);

                if (chkboxDpper.Checked == true)
                    dr_final12["DP Per%"] = ((Convert.ToDouble(dr["amount"]) - Convert.ToDouble(dr["DP"])) / (Convert.ToDouble(dr["DP"])));

                brandTotal = brandTotal + (Convert.ToDouble(dr["amount"]));
                producttot = producttot + (Convert.ToDouble(dr["amount"]));
                CategoryqtyTotal = CategoryqtyTotal + Convert.ToDouble(dr["qty"]);
                modeltot = modeltot + (Convert.ToDouble(dr["amount"]));
                total = total + (Convert.ToDouble(dr["amount"]));
                dt.Rows.Add(dr_final12);
            }
        }

        DataRow dr_final879 = dt.NewRow();
        dt.Rows.Add(dr_final879);

        DataRow dr_final899 = dt.NewRow();
        dr_final899["Category"] = "";
        dr_final899["Brand Name"] = "";
        dr_final899["Product Name"] = "Total : " + tLvlValue;

        if (chkboxQty.Checked == true)
            dr_final899["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final899["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

        if (chkboxAvg.Checked == true)
            dr_final899["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final899["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final899["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final899["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final899["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final899["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final899["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final899["DP Per%"] = "";

        dt.Rows.Add(dr_final899);

        DataRow dr_final8879 = dt.NewRow();
        dt.Rows.Add(dr_final8879);

        DataRow dr_final89 = dt.NewRow();
        dr_final89["Category"] = "";
        dr_final89["Brand Name"] = "Total : " + sLvlValue;
        dr_final89["Product Name"] = "";

        if (chkboxQty.Checked == true)
            dr_final89["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final89["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        if (chkboxAvg.Checked == true)
            dr_final89["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final89["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final89["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final89["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final89["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final89["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final89["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final89["DP Per%"] = "";

        dt.Rows.Add(dr_final89);

        DataRow dr_final88789 = dt.NewRow();
        dt.Rows.Add(dr_final88789);

        DataRow dr_final8799 = dt.NewRow();
        dr_final8799["Category"] = "Total : " + fLvlValue;
        dr_final8799["Brand Name"] = "";
        dr_final8799["Product Name"] = "";

        if (chkboxQty.Checked == true)
            dr_final8799["Qty"] = "";

        if (chkboxVal.Checked == true)
            dr_final8799["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));

        if (chkboxAvg.Checked == true)
            dr_final8799["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final8799["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final8799["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final8799["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final8799["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final8799["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final8799["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final8799["DP Per%"] = "";

        dt.Rows.Add(dr_final8799);

        DataRow dr_final77879 = dt.NewRow();
        dt.Rows.Add(dr_final77879);

        DataRow dr_final789 = dt.NewRow();
        dr_final789["Category"] = "Grand Total : ";
        dr_final789["Brand Name"] = "";
        dr_final789["Product Name"] = "";

        if (chkboxQty.Checked == true)
            dr_final789["Qty"] = Convert.ToString(Convert.ToDecimal(CategoryqtyTotal));

        if (chkboxVal.Checked == true)
            dr_final789["Value"] = Convert.ToString(Convert.ToDecimal(total));

        if (chkboxAvg.Checked == true)
            dr_final789["Avg"] = "";

        if (chkboxPer.Checked == true)
            dr_final789["Per%"] = "";

        if (chkboxNlcvalue.Checked == true)
            dr_final789["NLC Value"] = "";

        if (chkboxNlcper.Checked == true)
            dr_final789["NLC Per%"] = "";

        if (chkboxMRPvalue.Checked == true)
            dr_final789["MRP Value"] = "";

        if (chkboxMRPper.Checked == true)
            dr_final789["MRP Per%"] = "";

        if (chkboxDpvalue.Checked == true)
            dr_final789["DP Value"] = "";

        if (chkboxDpper.Checked == true)
            dr_final789["DP Per%"] = "";

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

    protected void Btnexl_Click(object sender, EventArgs e)
    {
        if (!isValidLevels())
        {
            return;
        }

        if (optoption.SelectedItem.Text == "Old")
        {
            if (opt.SelectedItem.Text == "Category Wise")
            {
                bindDataCategoryWise();
            }
            else if (opt.SelectedItem.Text == "Product Wise")
            {
                bindDataProductWise();
            }
            else if (opt.SelectedItem.Text == "Brand Wise")
            {
                bindDataBrandWise();
            }
            else if (opt.SelectedItem.Text == "Brand / Product Wise")
            {
                bindDataBrandProductWise();
            }
            else if (opt.SelectedItem.Text == "Brand / Product / Model Wise")
            {
                bindDataBrandProductModelWise();
            }
            else if (opt.SelectedItem.Text == "Bill Wise")
            {
                bindDataBillWise();
            }
            else if (opt.SelectedItem.Text == "Brand / Model Wise")
            {
                bindDataBrandModelWise();
            }
            else if (opt.SelectedItem.Text == "Category / Brand Wise")
            {
                bindDataCategoryBrandWise();
            }
            else if (opt.SelectedItem.Text == "Category / Brand / Product Wise")
            {
                bindDataCategoryBrandProductWise();
            }
            else if (opt.SelectedItem.Text == "PayMode Wise")
            {
                bindDataPayModeWise();
            }
            else if (opt.SelectedItem.Text == "Executive Wise")
            {
                bindDataExecutiveWise();
            }
            else if (opt.SelectedItem.Text == "Date Wise")
            {
                bindDataDateWise();
            }
            else if (opt.SelectedItem.Text == "Month Wise")
            {
                bindDataMonthWise();
            }
            else if (opt.SelectedItem.Text == "Customer Wise")
            {
                bindDataCustomerWise();
            }
        }
        else if (optoption.SelectedItem.Text == "Normal")
        {
            if (opt.SelectedItem.Text == "Category Wise")
            {
                bindDataCategoryWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Product Wise")
            {
                bindDataProductWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Brand Wise")
            {
                bindDataBrandWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Brand / Product Wise")
            {
                bindDataBrandProductWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Brand / Product / Model Wise")
            {
                bindDataBrandProductModelWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Bill Wise")
            {
                bindDataBillWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Brand / Model Wise")
            {
                bindDataBrandModelWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Category / Brand Wise")
            {
                bindDataCategoryBrandWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Category / Brand / Product Wise")
            {
                bindDataCategoryBrandProductWiseNormal();
            }
            else if (opt.SelectedItem.Text == "PayMode Wise")
            {
                bindDataPayModeWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Executive Wise")
            {
                bindDataExecutiveWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Date Wise")
            {
                bindDataDateWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Month Wise")
            {
                bindDataMonthWiseNormal();
            }
            else if (opt.SelectedItem.Text == "Customer Wise")
            {
                bindDataCustomerWiseNormal();
            }
        }
        else if (optoption.SelectedItem.Text == "GroupBy")
        {
            if (opt.SelectedItem.Text == "Category Wise")
            {
                bindDataCategoryWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Product Wise")
            {
                bindDataProductWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Brand Wise")
            {
                bindDataBrandWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Brand / Product Wise")
            {
                bindDataBrandProductWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Brand / Product / Model Wise")
            {
                bindDataBrandProductModelWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Bill Wise")
            {
                bindDataBillWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Brand / Model Wise")
            {
                bindDataBrandModelWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Category / Brand Wise")
            {
                bindDataCategoryBrandWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Category / Brand / Product Wise")
            {
                bindDataCategoryBrandProductWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "PayMode Wise")
            {
                bindDataPayModeWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Executive Wise")
            {
                bindDataExecutiveWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Date Wise")
            {
                bindDataDateWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Month Wise")
            {
                bindDataMonthWiseGroupBy();
            }
            else if (opt.SelectedItem.Text == "Customer Wise")
            {
                bindDataCustomerWiseGroupBy();
            }
        }
    }

    protected string getfield()
    {
        string field1 = "";
                

        return field1;
    }

    //protected void LoadForBrand(object sender, EventArgs e)
    //{
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    string brand = ddlBrand.SelectedValue;
    //    //string CategoryID = cmbCategory.SelectedValue;

    //    DataSet ds = new DataSet();

    //    ds = bl.ListProdcutNameForBrand(brand, CategoryID);
    //    if (ds != null && ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlproduct.Items.Clear();
    //        ddlproduct.DataSource = ds;
    //        ddlproduct.DataTextField = "ProductName";
    //        ddlproduct.DataValueField = "ProductName";
    //        ddlproduct.DataBind();
    //    }
    //    ddlproduct_SelectedIndexChanged(this, null);
    //}

    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        loaProducts();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBrands();
        loaProducts();
    }

    private void loadBrands()
    {
        string cat = string.Empty;
        cat = ddlCategory.SelectedValue;

        BusinessLogic bl = new BusinessLogic(sDataSource);
        var ds = bl.ListBrandsForCategoryID(cat, "");

        ddlBrand.DataSource = ds;
        ddlBrand.DataTextField = "ProductDesc";
        ddlBrand.DataValueField = "ProductDesc";
        ddlBrand.DataBind();

        ddlBrand.Items.Insert(0, new ListItem("All", "All"));
    }

    private void loadCategory()
    {
        string connection = Request.Cookies["Company"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        var ds = bl.ListCategory(connection,"");

        ddlCategory.DataSource = ds;
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataValueField = "CategoryID";
        ddlCategory.DataBind();

        ddlCategory.Items.Insert(0, new ListItem("All", "All"));
    }

    private void loaProducts()
    {
        string brand = string.Empty;
        brand = ddlBrand.SelectedValue;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        var ds = bl.ListProdcutName(brand);

        ddlproduct.DataSource = ds;
        ddlproduct.DataTextField = "ProductName";
        ddlproduct.DataValueField = "ProductName";
        ddlproduct.DataBind();

        ddlproduct.Items.Insert(0, new ListItem("All", "All"));
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
        string brand=string.Empty;
        string Category = string.Empty;
        string product=string.Empty;

        Category = ddlCategory.SelectedItem.Text;
        brand = ddlBrand.SelectedValue;
        product = ddlproduct.SelectedValue;

        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dt.Columns.Add(new DataColumn("Category"));
        dt.Columns.Add(new DataColumn("Brand"));
        dt.Columns.Add(new DataColumn("Product Name"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Qty"));
        dt.Columns.Add(new DataColumn("Rate"));
        dt.Columns.Add(new DataColumn("Amount"));

        ds = objBL.getSalesreport(startDate, endDate, Category,brand, product);
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
                dr_final12["Product Name"] = dr["ProductName"];
                dr_final12["Model"] = dr["model"];
                dr_final12["Qty"] = dr["Qty"];
                dr_final12["Rate"] = dr["Rate"];
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
            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    protected string getCond()
    {
        string cond = "";

        
        return cond;
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            //string filename = "Sales Report.xls";
            string filename = "Sales Report_" + DateTime.Now.ToString() + ".xls";
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


 
}
