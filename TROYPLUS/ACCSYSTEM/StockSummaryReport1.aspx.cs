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

public partial class StockSummaryReport1 : System.Web.UI.Page
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
    double dSCVatRate = 0;

    double dSCCSTRate = 0;
    double dSCFrRate = 0;
    double dSCLURate = 0;
    double dSCGrandRate = 0;
    string tempBillno = "0";
    string strBillno = string.Empty;
    int cnt = 0;
    BusinessLogic objBL;

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


                divPrint.Visible = true;
                divmain.Visible = true;

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DateTime startDate, endDate;
                string category = string.Empty;


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

                startDate = Convert.ToDateTime(stdt);
                endDate = Convert.ToDateTime(etdt);

                if (Request.QueryString["category"] != null)
                {
                    category = Request.QueryString["category"].ToString();
                }

                string intTrans = "";
                string purRet = "";
                string delNote = "";

                if (Request.QueryString["intTrans"] != null)
                {
                    intTrans = Request.QueryString["intTrans"].ToString();
                }
                if (Request.QueryString["purRet"] != null)
                {
                    purRet = Request.QueryString["purRet"].ToString();
                }
                if (Request.QueryString["delNote"] != null)
                {
                    delNote = Request.QueryString["delNote"].ToString();
                }
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


                DataSet BillDs = new DataSet();

                if (category == "Daywise")
                {
                    BillDs = bl.FirstLevelDaywise(startDate, endDate, purRet, intTrans, delNote);

                }
                else if (category == "Categorywise")
                {
                    BillDs = bl.FirstLevelCategorywise(startDate, endDate, purRet, intTrans, delNote);
                }
                else if (category == "Brandwise")
                {
                    BillDs = bl.FirstLevelBrandwise(startDate, endDate, purRet, intTrans, delNote);
                }
                else if (category == "Modelwise")
                {
                    BillDs = bl.FirstLevelModelwise(startDate, endDate, purRet, intTrans, delNote);
                }
                else if (category == "Billwise")
                {
                    BillDs = bl.FirstLevelBillwise(startDate, endDate, purRet, intTrans, delNote);
                }
                else if (category == "Customerwise")
                {
                    BillDs = bl.FirstLevelCustomerwise(startDate, endDate, purRet, intTrans, delNote);
                }
                else if (category == "Executivewise")
                {
                    BillDs = bl.FirstLevelExecutivewise(startDate, endDate, purRet, intTrans, delNote);
                }
                /*Start Itemwise*/
                else if (category == "Itemwise")
                {
                    BillDs = bl.FirstLevelItemwise(startDate, endDate, purRet, intTrans, delNote);
                }
                /*End Itemwise*/
                gvMain.DataSource = BillDs;
                gvMain.DataBind();

                div1.Visible = false;
            }
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
            double brandTotal = 0;
            double producttot = 0;
            double CategoryqtyTotal = 0;
            double total = 0;
            string brand = string.Empty;
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
                field2 =field2+ "tblsales.billno,";
                GroupBy = GroupBy+"tblsales.billno";
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
                        ds = bl.SecondLevel(field2, Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()), purReturn, intTrans, delNote, GroupBy);
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


                DataSet BillDs = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (category == "Daywise")
                {
                    BillDs = bl.FirstLevelDaywise(startDate, endDate, purRet, intTrans, delNote);

                }
                else if (category == "Categorywise")
                {
                    BillDs = bl.FirstLevelCategorywise(startDate, endDate, purRet, intTrans, delNote);
                }
                else if (category == "Brandwise")
                {
                    BillDs = bl.FirstLevelBrandwise(startDate, endDate, purRet, intTrans, delNote);
                }
                else if (category == "Modelwise")
                {
                    BillDs = bl.FirstLevelModelwise(startDate, endDate, purRet, intTrans, delNote);
                }
                else if (category == "Billwise")
                {
                    BillDs = bl.FirstLevelBillwise(startDate, endDate, purRet, intTrans, delNote);
                }
                else if (category == "Customerwise")
                {
                    BillDs = bl.FirstLevelCustomerwise(startDate, endDate, purRet, intTrans, delNote);
                }
                else if (category == "Executivewise")
                {
                    BillDs = bl.FirstLevelExecutivewise(startDate, endDate, purRet, intTrans, delNote);
                }
                /*Start Itemwise*/
                else if (category == "Itemwise")
                {
                    BillDs = bl.FirstLevelItemwise(startDate, endDate, purRet, intTrans, delNote);
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
            string category = string.Empty;

            category = Convert.ToString(cmbDisplayCat.SelectedItem.Text);
            var secondLevel = cmbDisplayItem.SelectedItem.Text.Trim();

            if (Request.QueryString["category"] != null)
            {
                category = Request.QueryString["category"].ToString();
            }
            if (Request.QueryString["secondLevel"] != null)
            {
                secondLevel = Request.QueryString["secondLevel"].ToString();
            }



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
                e.Row.Cells[0].Text = category.Replace("wise", "");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

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
                if (DataBinder.Eval(e.Row.DataItem, "SumFreight") != DBNull.Value)
                    dFrRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumFreight"));
                if (DataBinder.Eval(e.Row.DataItem, "Loading") != DBNull.Value)
                    dLURate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Loading"));

                dGrandRate = dDiscountRate + dVatRate + dCSTRate; // +dFrRate + dLURate;


                dSNetRate = dSNetRate + dNetRate;
                dSVatRate = dSVatRate + dVatRate;
                dSDiscountRate = dSDiscountRate + dDisRate;
                dSCSTRate = dSCSTRate + dCSTRate;

                GridView gv = e.Row.FindControl("gvSecond") as GridView;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                Label lblLink = (Label)e.Row.FindControl("lblLink");

                DataSet ds = new DataSet();

                if (category == "Daywise")
                {
                    DateTime startDate;
                    startDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "LinkName"));

                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelDaywiseBillWise(startDate, purReturn, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelDaywiseModelWise(startDate, purReturn, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelDaywiseBrandWise(startDate, purReturn, intTrans, delNote);
                    else if (secondLevel == "Customerwise")
                        ds = bl.SecondLevelDaywiseCustWise(startDate, purReturn, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelDaywiseItemWise(startDate, purReturn, intTrans, delNote);
                    else if (secondLevel == "Daywise")
                        ds = bl.SecondLevelDaywiseDayWise(startDate, purReturn, intTrans, delNote);

                    lblLink.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName", "{0:dd/MM/yyyy}"));
                }
                else if (category == "Categorywise")
                {
                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelCategorywiseBillWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelCategorywiseModelWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelCategorywiseBrandWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Customerwise")
                        ds = bl.SecondLevelCategorywiseCustWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelCategorywiseItemWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Daywise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Category", "BillDate", purReturn, intTrans, delNote);

                }
                else if (category == "Brandwise")
                {
                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelBrandwiseBillWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelBrandwiseModelWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelBrandwiseBrandWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Customerwise")
                        ds = bl.SecondLevelBrandWiseCustomerWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductDesc", "CustomerName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelBrandWiseItemWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductDesc", "ProductName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Daywise")
                        ds = bl.SecondLevelBrandWiseDayWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "ProductDesc", "BillDate", purReturn, intTrans, delNote);

                }
                else if (category == "Modelwise")
                {
                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelModelwiseBillWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelModelwiseModelWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelModelwiseBrandWise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), purReturn, intTrans, delNote);
                    else if (secondLevel == "Customerwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "Model", "CustomerName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelModelWiseItemWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "Model", "ProductName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Daywise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Model", "BillDate", purReturn, intTrans, delNote);

                    //ds = bl.SecondLevelModelwise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
                }
                else if (category == "Billwise")
                {
                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "BillNo", purReturn, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "Model", purReturn, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "ProductDesc", purReturn, intTrans, delNote);
                    else if (secondLevel == "Customerwise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "CustomerName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelBillWiseItemWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "BillNo", "ProductName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Daywise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "BillNo", "BillDate", purReturn, intTrans, delNote);

                    //ds = bl.SecondLevelBillwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LinkName")));
                }
                else if (category == "Customerwise")
                {

                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "BillNo", purReturn, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "Model", purReturn, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "ProductDesc", purReturn, intTrans, delNote);
                    else if (secondLevel == "Customerwise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "CustomerName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelCustomerWiseItemWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "ProductName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Daywise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "CustomerName", "BillDate", purReturn, intTrans, delNote);

                    //ds = bl.SecondLevelCustomerwise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
                }
                else if (category == "Executivewise")
                {
                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "BillNo", purReturn, intTrans, delNote);
                    //ds = bl.SecondLevelExecutivewise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));
                    //else if (secondLevel == "Modelwise")
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "Model", purReturn, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "ProductDesc", purReturn, intTrans, delNote);
                    else if (secondLevel == "Customerwise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "CustomerName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "ProductName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Daywise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), "Executive", "BillDate", purReturn, intTrans, delNote);

                }
                /*Start Itemwise*/
                else if (category == "Itemwise")
                {

                    if (secondLevel == "Billwise")
                        ds = bl.SecondLevelGeneralSalesItemWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "BillNo", purReturn, intTrans, delNote);
                    else if (secondLevel == "Modelwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "Model", purReturn, intTrans, delNote);
                    else if (secondLevel == "Brandwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "ProductDesc", purReturn, intTrans, delNote);
                    else if (secondLevel == "Customerwise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "CustomerName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Itemwise")
                        ds = bl.SecondLevelGeneralProductWise(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "ProductName", purReturn, intTrans, delNote);
                    else if (secondLevel == "Daywise")
                        ds = bl.SecondLevelGeneralSales(Convert.ToDateTime(stDate), Convert.ToDateTime(eDate), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")).Trim(), "ProductName", "BillDate", purReturn, intTrans, delNote);

                    var tempRow = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray;
                    //Label lblProdName = (Label)e.Row.FindControl("lblProductName");
                    //lblProdName.Text = " | " + DataBinder.Eval(e.Row.DataItem, "ProductName").ToString();
                    //ds = bl.SecondLevelItemwise(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LinkName")), Convert.ToDateTime(txtStartDate.Text.Trim()), Convert.ToDateTime(txtEndDate.Text.Trim()));

                }
                /*End Itemwise*/
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
                lblFreightRate.Text = dSCFrRate.ToString("f2");
                lblLURate.Text = dSCLURate.ToString("f2");
                dSFrRate = dSFrRate + dSCFrRate;
                dSLURate = dSLURate + dSCLURate;
                dGrandRate = dGrandRate + dSCFrRate + dSCLURate;
                dSGrandRate = dSGrandRate + dGrandRate;
                lblTotal.Text = dGrandRate.ToString("f2");
                dGrandRate = 0;
                dSCFrRate = 0;
                dSCLURate = 0;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[1].Text = dSNetRate.ToString("f2");
                e.Row.Cells[2].Text = dSDiscountRate.ToString("f2");
                e.Row.Cells[3].Text = dSVatRate.ToString("f2");
                e.Row.Cells[4].Text = dSCSTRate.ToString("f2");
                e.Row.Cells[5].Text = dSFrRate.ToString("f2");
                e.Row.Cells[6].Text = dSLURate.ToString("f2");
                e.Row.Cells[7].Text = dSGrandRate.ToString("f2");
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
            double dQuantity = 0;
            double dGrandRate = 0;
            double dDiscountRate = 0;

            lblErr.Text = "";

            var secondLevel = cmbDisplayItem.SelectedItem.Text.Trim();

            if (Request.QueryString["secondLevel"] != null)
            {
                secondLevel = Request.QueryString["secondLevel"].ToString();
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = secondLevel.Replace("wise", "");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (DataBinder.Eval(e.Row.DataItem, "Quantity") != DBNull.Value)
                    dQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Quantity"));

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

                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label lblFreightRate = (Label)e.Row.FindControl("lblFreightRate");
                Label lblLURate = (Label)e.Row.FindControl("lblLURate");
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




                dGrandRate = dGrandRate + dDiscountRate + dVatRate + dCSTRate; // +dFrRate + dLURate;

                dSQty = dSQty + dQuantity;
                dSCNetRate = dSCNetRate + dNetRate;
                dSCVatRate = dSCVatRate + dVatRate;
                dSCDiscountRate = dSCDiscountRate + dDisRate;
                dSCCSTRate = dSCCSTRate + dCSTRate;


                dSCGrandRate = dSCGrandRate + dGrandRate;
                lblTotal.Text = dGrandRate.ToString("f2");
                dGrandRate = 0;
                //e.Row.Cells[6].Visible = false;
                //e.Row.Cells[7].Visible = false;

                cnt = cnt + 1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {


                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                //dSCGrandRate = dSCGrandRate + dSCFrRate + dSCLURate;

                e.Row.Cells[4].Text = dSQty.ToString("f2");
                e.Row.Cells[5].Text = dSCNetRate.ToString("f2");
                e.Row.Cells[6].Text = dSCDiscountRate.ToString("f2");
                e.Row.Cells[7].Text = dSCVatRate.ToString("f2");
                e.Row.Cells[8].Text = dSCCSTRate.ToString("f2");
                e.Row.Cells[9].Text = dSCFrRate.ToString("f2");
                e.Row.Cells[10].Text = dSCLURate.ToString("f2");
                e.Row.Cells[11].Text = dSCGrandRate.ToString("f2");

                dSCDiscountRate = 0;
                dSCNetRate = 0;
                dSCVatRate = 0;
                dSQty = 0;
                dSCCSTRate = 0;
                //dSCFrRate = 0;
                //dSCLURate = 0;
                dSCGrandRate = 0;


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

}
