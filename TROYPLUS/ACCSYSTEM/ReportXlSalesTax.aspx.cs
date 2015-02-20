using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportXlSalesTax : System.Web.UI.Page
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

                txtStrtDt.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDt.Text = DateTime.Now.ToShortDateString();

                txtStartDt.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEdDt.Text = DateTime.Now.ToShortDateString();

                txtStDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEdDate.Text = DateTime.Now.ToShortDateString();
            }
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
            bindData();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnRep_Click(object sender, EventArgs e)
    {
        try
        {
            bindDataPurReturn();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            bindDataSales();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnt_Click(object sender, EventArgs e)
    {
        try
        {
            bindDataSalesReturn();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void bindDataPurReturn()
    {
        DateTime startDate, endDate;
        DataSet ds = new DataSet();

        string intTrans = "";
        string salesRet = "";
        string delNote = "";

        string condi = "";

        intTrans = "NO";
        salesRet = "YES";
        delNote = "NO";

        if (optionpurret.SelectedItem.Text == "5%")
        {
            condi = " And tblSalesitems.vat = 5";
        }
        else if (optionpurret.SelectedItem.Text == "14.5%")
        {
            condi = " And tblSalesitems.vat = 14.5";
        }
        if (optionpurret.SelectedItem.Text == "All")
        {
            condi = "";
        }

        startDate = Convert.ToDateTime(txtStrtDt.Text.Trim());
        endDate = Convert.ToDateTime(txtEdDt.Text.Trim());

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        ds = objBL.SalesPurRetAnnuxere(startDate, endDate, salesRet, intTrans, delNote, condi);
        Double serialno = 1;
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SNo"));
                dt.Columns.Add(new DataColumn("Name of the buyer"));
                dt.Columns.Add(new DataColumn("Buyer Tin"));
                dt.Columns.Add(new DataColumn("Commodity Code"));
                dt.Columns.Add(new DataColumn("Invoice No"));
                dt.Columns.Add(new DataColumn("Invoice Date"));
                dt.Columns.Add(new DataColumn("Sales Value"));
                dt.Columns.Add(new DataColumn("Tax Rate"));
                dt.Columns.Add(new DataColumn("Vat CST Paid"));
                dt.Columns.Add(new DataColumn("Category"));

                DataRow dr_export1 = dt.NewRow();
                dt.Rows.Add(dr_export1);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["SNo"] = serialno;
                    dr_export["Name of the buyer"] = dr["LinkName"];
                    dr_export["Buyer Tin"] = dr["Tinnumber"];
                    dr_export["Commodity Code"] = "";
                    dr_export["Invoice No"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_export["Invoice Date"] = dtaa;

                    dr_export["Sales Value"] = dr["NetRate"];
                    dr_export["Tax Rate"] = dr["vat"];
                    dr_export["Vat CST Paid"] = dr["ActualVAT"];
                    dr_export["Category"] = "";
                    dt.Rows.Add(dr_export);

                    serialno = serialno + 1;
                }

                DataRow dr_export2 = dt.NewRow();
                dr_export2["SNo"] = "";
                dr_export2["Name of the buyer"] = "";
                dr_export2["Buyer Tin"] = "";
                dr_export2["Commodity Code"] = "";
                dr_export2["Invoice No"] = "";
                dr_export2["Invoice Date"] = "";
                dr_export2["Sales Value"] = "";
                dr_export2["Tax Rate"] = "";
                dr_export2["Vat CST Paid"] = "";
                dr_export2["Category"] = "";
                dt.Rows.Add(dr_export2);

                ExportToExcel(dt);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindData()
    {
        DateTime startDate, endDate;
        DataSet ds = new DataSet();
        double total14 = 0;
        double total5 = 0;
        string intTrans = "";
        string salesRet = "";
        string delNote = "";

        string condi = "";

        intTrans = "NO";
        salesRet = "NO";
        delNote = "NO";

        if (optionrate.SelectedItem.Text == "5%")
        {
            condi = " And tblPurchaseItems.vat = 5" ;
        }
        else if (optionrate.SelectedItem.Text == "14.5%")
        {
            condi = " And tblPurchaseItems.vat = 14.5" ;
        }
        if (optionrate.SelectedItem.Text == "All")
        {
            condi = "";
        }

        startDate = Convert.ToDateTime(txtStartDate.Text.Trim());
        endDate = Convert.ToDateTime(txtEndDate.Text.Trim());

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        ds = objBL.PurchaseAnnuxere(startDate, endDate, salesRet, intTrans, delNote, condi);
        Double serialno = 1;

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SNo"));
                dt.Columns.Add(new DataColumn("Name of the Seller"));
                dt.Columns.Add(new DataColumn("Seller Tin"));
                dt.Columns.Add(new DataColumn("Commodity Code"));
                dt.Columns.Add(new DataColumn("Invoice No"));
                dt.Columns.Add(new DataColumn("Invoice Date"));
                dt.Columns.Add(new DataColumn("Purchase Value"));
                dt.Columns.Add(new DataColumn("Tax Rate"));
                dt.Columns.Add(new DataColumn("Vat CST Paid"));
                dt.Columns.Add(new DataColumn("Category"));

                DataRow dr_export1 = dt.NewRow();
                dt.Rows.Add(dr_export1);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["SNo"] = serialno;
                    dr_export["Name of the Seller"] = dr["LinkName"];
                    dr_export["Seller Tin"] = dr["Tinnumber"];
                    dr_export["Commodity Code"] = "";
                    dr_export["Invoice No"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_export["Invoice Date"] = dtaa;

                    dr_export["Purchase Value"] = Convert.ToDouble(dr["NetPurchaseRate"]) - (Convert.ToDouble(dr["ActualDiscount"]) + Convert.ToDouble(dr["DiscAmt"]));

                    if (Convert.ToDouble(dr["vat"]) == 5)
                    {
                        total5 = total5 + Convert.ToDouble(dr["ActualVAT"]);
                    }
                    else if (Convert.ToDouble(dr["vat"]) == 14.5)
                    {
                        total14 = total14 + Convert.ToDouble(dr["ActualVAT"]);
                    }

                    dr_export["Tax Rate"] = dr["vat"];
                    dr_export["Vat CST Paid"] = dr["ActualVAT"];
                    dr_export["Category"] = "";
                    dt.Rows.Add(dr_export);

                    serialno = serialno + 1;
                }

                DataRow dr_export2 = dt.NewRow();
                dr_export2["SNo"] = "";
                dr_export2["Name of the Seller"] = "";
                dr_export2["Seller Tin"] = "";
                dr_export2["Commodity Code"] = "";
                dr_export2["Invoice No"] = "";
                dr_export2["Invoice Date"] = "";
                dr_export2["Purchase Value"] = "";
                dr_export2["Tax Rate"] = "";
                dr_export2["Vat CST Paid"] = "";
                dr_export2["Category"] = "";
                dt.Rows.Add(dr_export2);

                DataRow dr_export211 = dt.NewRow();
                dr_export211["SNo"] = "";
                dr_export211["Name of the Seller"] = "";
                dr_export211["Seller Tin"] = "";
                dr_export211["Commodity Code"] = "";
                dr_export211["Invoice No"] = "";
                dr_export211["Invoice Date"] = "";
                dr_export211["Purchase Value"] = "";
                dr_export211["Tax Rate"] = "Total 5% ";
                dr_export211["Vat CST Paid"] = total5;
                dr_export211["Category"] = "";
                dt.Rows.Add(dr_export211);

                DataRow dr_export213 = dt.NewRow();
                dr_export213["SNo"] = "";
                dr_export213["Name of the Seller"] = "";
                dr_export213["Seller Tin"] = "";
                dr_export213["Commodity Code"] = "";
                dr_export213["Invoice No"] = "";
                dr_export213["Invoice Date"] = "";
                dr_export213["Purchase Value"] = "";
                dr_export213["Tax Rate"] = "Total 14.5% ";
                dr_export213["Vat CST Paid"] = total14;
                dr_export213["Category"] = "";
                dt.Rows.Add(dr_export213);

                ExportToExcel(dt);
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

    public void bindDataSalesReturn()
    {
        DateTime startDate, endDate;
        DataSet ds = new DataSet();

        string intTrans = "";
        string salesRet = "";
        string delNote = "";

        string condi = "";

        intTrans = "NO";
        salesRet = "YES";
        delNote = "NO";

        if (optionpurret.SelectedItem.Text == "5%")
        {
            condi = " And tblPurchaseItems.vat = 5";
        }
        else if (optionpurret.SelectedItem.Text == "14.5%")
        {
            condi = " And tblPurchaseItems.vat = 14.5";
        }
        if (optionpurret.SelectedItem.Text == "All")
        {
            condi = "";
        }

        startDate = Convert.ToDateTime(txtStDate.Text.Trim());
        endDate = Convert.ToDateTime(txtEdDate.Text.Trim());

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        ds = objBL.PurchaseSalesRetAnnuxere(startDate, endDate, salesRet, intTrans, delNote, condi);
        Double serialno = 1;
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SNo"));
                dt.Columns.Add(new DataColumn("Name of the Seller"));
                dt.Columns.Add(new DataColumn("Seller Tin"));
                dt.Columns.Add(new DataColumn("Commodity Code"));
                dt.Columns.Add(new DataColumn("Invoice No"));
                dt.Columns.Add(new DataColumn("Invoice Date"));
                dt.Columns.Add(new DataColumn("Purchase Value"));
                dt.Columns.Add(new DataColumn("Tax Rate"));
                dt.Columns.Add(new DataColumn("Vat CST Paid"));
                dt.Columns.Add(new DataColumn("Category"));

                DataRow dr_export1 = dt.NewRow();
                dt.Rows.Add(dr_export1);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["SNo"] = serialno;
                    dr_export["Name of the Seller"] = dr["LinkName"];
                    dr_export["Seller Tin"] = dr["Tinnumber"];
                    dr_export["Commodity Code"] = "";
                    dr_export["Invoice No"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_export["Invoice Date"] = dtaa;

                    dr_export["Purchase Value"] = dr["NetPurchaseRate"];
                    dr_export["Tax Rate"] = dr["vat"];
                    dr_export["Vat CST Paid"] = dr["ActualVAT"];
                    dr_export["Category"] = "";
                    dt.Rows.Add(dr_export);

                    serialno = serialno + 1;
                }

                DataRow dr_export2 = dt.NewRow();
                dr_export2["SNo"] = "";
                dr_export2["Name of the Seller"] = "";
                dr_export2["Seller Tin"] = "";
                dr_export2["Commodity Code"] = "";
                dr_export2["Invoice No"] = "";
                dr_export2["Invoice Date"] = "";
                dr_export2["Purchase Value"] = "";
                dr_export2["Tax Rate"] = "";
                dr_export2["Vat CST Paid"] = "";
                dr_export2["Category"] = "";
                dt.Rows.Add(dr_export2);

                ExportToExcel(dt);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataSales()
    {
        DateTime startDate, endDate;
        DataSet ds = new DataSet();

        string intTrans = "";
        string salesRet = "";
        string delNote = "";

        string condi = "";

        intTrans = "NO";
        salesRet = "NO";
        delNote = "NO";

        if (optionsal.SelectedItem.Text == "5%")
        {
            condi = " And tblSalesitems.vat = 5";
        }
        else if (optionsal.SelectedItem.Text == "14.5%")
        {
            condi = " And tblSalesitems.vat = 14.5";
        }
        if (optionsal.SelectedItem.Text == "All")
        {
            condi = "";
        }

        startDate = Convert.ToDateTime(txtStartDt.Text.Trim());
        endDate = Convert.ToDateTime(txtEndDt.Text.Trim());

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        ds = objBL.SalesAnnuxere(startDate, endDate, salesRet, intTrans, delNote,condi);
        Double serialno = 1;

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SNo"));
                dt.Columns.Add(new DataColumn("Name of the buyer"));
                dt.Columns.Add(new DataColumn("Buyer Tin"));
                dt.Columns.Add(new DataColumn("Commodity Code"));
                dt.Columns.Add(new DataColumn("Invoice No"));
                dt.Columns.Add(new DataColumn("Invoice Date"));
                dt.Columns.Add(new DataColumn("Sales Value"));
                dt.Columns.Add(new DataColumn("Tax Rate"));
                dt.Columns.Add(new DataColumn("Vat CST Paid"));
                dt.Columns.Add(new DataColumn("Category"));

                DataRow dr_export1 = dt.NewRow();
                dt.Rows.Add(dr_export1);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["SNo"] = serialno;
                    dr_export["Name of the buyer"] = dr["LinkName"];
                    dr_export["Buyer Tin"] = dr["Tinnumber"];
                    dr_export["Commodity Code"] = "";
                    dr_export["Invoice No"] = dr["BillNo"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_export["Invoice Date"] = dtaa;

                    dr_export["Sales Value"] = dr["NetRate"];
                    dr_export["Tax Rate"] = dr["vat"];
                    dr_export["Vat CST Paid"] = dr["ActualVAT"];
                    dr_export["Category"] = "";
                    dt.Rows.Add(dr_export);

                    serialno = serialno + 1;
                }

                DataRow dr_export2 = dt.NewRow();
                dr_export2["SNo"] = "";
                dr_export2["Name of the buyer"] = "";
                dr_export2["Buyer Tin"] = "";
                dr_export2["Commodity Code"] = "";
                dr_export2["Invoice No"] = "";
                dr_export2["Invoice Date"] = "";
                dr_export2["Sales Value"] = "";
                dr_export2["Tax Rate"] = "";
                dr_export2["Vat CST Paid"] = "";
                dr_export2["Category"] = "";
                dt.Rows.Add(dr_export2);

                ExportToExcel(dt);
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

    protected string getCond()
    {
        string cond = "";
       
        return cond;
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            //string filename = "Sales Tax Filing.xls";
            string filename = "Sales Tax Filing_" + DateTime.Now.ToString() + ".xls";
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


    public DataSet GenerateGridColumns()
    {
        DataSet dstt = new DataSet();

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dstt = objBL.getexpensetypes();


        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        if (dstt.Tables[0].Rows.Count > 0)
        {
            dc = new DataColumn("Date");
            dt.Columns.Add(dc);
            for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
            {
                string ledger = dstt.Tables[0].Rows[i]["LedgerName"].ToString();
                dc = new DataColumn(ledger);
                dt.Columns.Add(dc);
             }
            dc = new DataColumn("Total");
            dt.Columns.Add(dc);
        }

        ds.Tables.Add(dt);
        return ds;

            //dt.Columns.Add(new DataColumn("DATE"));
            //dt.Columns.Add(new DataColumn("AD TV"));
            //dt.Columns.Add(new DataColumn("AD Flex"));
            //dt.Columns.Add(new DataColumn("AD Gift"));
            //dt.Columns.Add(new DataColumn("AD Dhinakaran"));
            //dt.Columns.Add(new DataColumn("AD Dinamalar"));
            //dt.Columns.Add(new DataColumn("AD Thanthi"));
            //dt.Columns.Add(new DataColumn("Bank Charges"));
            //dt.Columns.Add(new DataColumn("Bank Charges HDFC"));
            //dt.Columns.Add(new DataColumn("Bank Interest"));
            //dt.Columns.Add(new DataColumn("BBK"));
            //dt.Columns.Add(new DataColumn("Computer Maintenance"));
            //dt.Columns.Add(new DataColumn("Courier Exp"));
            //dt.Columns.Add(new DataColumn("Demo Salary"));
            //dt.Columns.Add(new DataColumn("Discount"));
            //dt.Columns.Add(new DataColumn("Exgratia Account"));
            //dt.Columns.Add(new DataColumn("Electricity"));
            //dt.Columns.Add(new DataColumn("Freight Inward"));
            //dt.Columns.Add(new DataColumn("Freight Outward"));
            //dt.Columns.Add(new DataColumn("Generator Exp"));
            //dt.Columns.Add(new DataColumn("Hospital"));
            //dt.Columns.Add(new DataColumn("HO Exp"));
            //dt.Columns.Add(new DataColumn("Income Tax Filing Fee"));
            //dt.Columns.Add(new DataColumn("Madurai Talk"));
            //dt.Columns.Add(new DataColumn("MISC-Exp"));
            //dt.Columns.Add(new DataColumn("Membership Fee"));
            //dt.Columns.Add(new DataColumn("Progress Fee"));
            //dt.Columns.Add(new DataColumn("Project"));
            //dt.Columns.Add(new DataColumn("Rent A/C"));
            //dt.Columns.Add(new DataColumn("Salary"));
            //dt.Columns.Add(new DataColumn("Sales Promotions"));
            //dt.Columns.Add(new DataColumn("Security Service"));
            //dt.Columns.Add(new DataColumn("ShowRoom Maintenance"));
            //dt.Columns.Add(new DataColumn("Staff Incentive"));
            //dt.Columns.Add(new DataColumn("Staff Welfare"));
            //dt.Columns.Add(new DataColumn("Stationary"));
            //dt.Columns.Add(new DataColumn("Subscriptios"));
            //dt.Columns.Add(new DataColumn("Telephone Exp"));
            //dt.Columns.Add(new DataColumn("Title"));
            //dt.Columns.Add(new DataColumn("Travelling And Conveyance"));
            //dt.Columns.Add(new DataColumn("Troyplus Exp"));
            //dt.Columns.Add(new DataColumn("VAT A/C"));
            //dt.Columns.Add(new DataColumn("Vehicle Maintanance"));
            //dt.Columns.Add(new DataColumn("Xerox And Printing"));
            //dt.Columns.Add(new DataColumn("Total"));
        
      
    }

    public DataSet UpdateColumnsData(DataSet dsGrid, DataSet debitData)
    {
        Double credit = 0.00;

        if (debitData != null)
        {

            if (true)
            {
                DataTable dt = debitData.Tables[0];

                DataView dv = dt.AsDataView();



                dt = dv.ToTable();
                int colIndex = 1;

                foreach (DataRow dr in dt.Rows)
                {
                    bool dupFlag = false;
                    string customer = dr["LedgerName"].ToString();
                    DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                    int rowIndex = 0;

                    if (dupFlag)
                    {
                      
                        double currAmount = 0.0;

                        if (dsGrid.Tables[0].Rows[rowIndex][colIndex] != null)
                        {
                            if (dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString() != "")
                                currAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString());
                        }

                        double totAmount = 0.0;

                        totAmount = currAmount + double.Parse(dr["Amount"].ToString());

                        dsGrid.Tables[0].Rows[rowIndex][colIndex] = totAmount.ToString("#0");
                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                    }
                    else
                    {
                        DataRow gridRow = dsGrid.Tables[0].NewRow();

                        string ledgernam = dr["LedgerName"].ToString().ToUpper().Trim();
                        for (int ii = 1; ii < dsGrid.Tables[0].Columns.Count; ii++)
                        {
                            string ledgerna = dsGrid.Tables[0].Columns[ii].ToString();
                            if (ledgernam == ledgerna)
                            {
                                gridRow[colIndex] = double.Parse(dr["Amt"].ToString()).ToString("#0");
                                credit = credit + double.Parse(dr["Amt"].ToString());
                            }
                            else
                            {
                                gridRow[colIndex] = ("#0");
                            }
                        }

                        dsGrid.Tables[0].Rows.Add(gridRow);
                    }

                }
            }
        }

        return dsGrid;
    }


    public void bindDataSubTot(string cond)
    {
        DateTime startDate, endDate, Transdt;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        string condtion = "";
        condtion = getCond();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        DataSet dsGird = GenerateGridColumns();
        DataSet dst = new DataSet();
        DataSet dts = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataSet dsG = new DataSet();
        dst = objBL.getexpensepayments(condtion, startDate, endDate);
        dsG = UpdateColumnsData(dsGird, dst);

        dts = objBL.getexpensepaymentsDate(condtion, startDate, endDate);
        if (dsG.Tables[0].Rows.Count > 0)
        {
            gvRepor.Visible = true;
            gvRepor.DataSource = dsG;
            gvRepor.DataBind();
        }

        dstt = objBL.getexpensetypes();


        if (dts.Tables[0].Rows.Count > 0)
        {
        DataTable dtt = new DataTable();
        DataColumn dc;
        if (dstt.Tables[0].Rows.Count > 0)
        {
            dc = new DataColumn("Date");
            dtt.Columns.Add(dc);
            for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
            {
                string ledger = dstt.Tables[0].Rows[i]["LedgerName"].ToString();
                dc = new DataColumn(ledger);
                dtt.Columns.Add(dc);
            }
            dc = new DataColumn("Total");
            dtt.Columns.Add(dc);
        }
        dsGir.Tables.Add(dtt);

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        double credit = 0.00;
        double Tottot = 0.00;

        DateTime Transd = Convert.ToDateTime(txtStartDate.Text);

        int gg = 1;

        foreach (DataRow dre in dts.Tables[0].Rows)
        {
            DataRow dr_final12 = dtt.NewRow();
            Transdt = Convert.ToDateTime(dre["Transdate"]);

            credit = 0.00;

            foreach (DataRow dr in dst.Tables[0].Rows)
            {
                Transd = Convert.ToDateTime(dr["Transdate"]);

                if (Transdt == Transd)
                {
                    string aa = dr["TransDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");

                    dr_final12["Date"] = dtaa;
                    
                    string ledgernam = dr["LedgerName"].ToString().ToUpper().Trim();
                    for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                    {
                        string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                        if (ledgernam == ledgerna)
                        {
                            dr_final12[ledgerna] = double.Parse(dr["Amt"].ToString());
                            credit = credit + double.Parse(dr["Amt"].ToString());
                            Tottot = Tottot + double.Parse(dr["Amt"].ToString());
                        }
                        else
                        {
                            if (dr_final12[ledgerna] == null)
                            {
                                dr_final12[ledgerna] = "";
                            }                    
                        }
                    }
                    dr_final12["Total"] = credit;                                    
                }               
            }
            dtt.Rows.Add(dr_final12);
        }

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final88 = dtt.NewRow();
        dr_final88["Date"] = "Total";
        dr_final88["Total"] = Tottot;
        dtt.Rows.Add(dr_final88);

        ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }

}
