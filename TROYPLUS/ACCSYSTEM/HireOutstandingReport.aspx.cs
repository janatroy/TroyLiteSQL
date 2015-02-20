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


public partial class HireOutstandingReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    public Double damt = 0.0;
    public Double camt = 0.0;
    public Double dDiffamt = 0.0;
    public Double cDiffamt = 0.0;
    /*Start Outstanding Report March 16 */
    double OpBalance = 0.0;
    double dLedger = 0;
    double cLedger = 0;
    /*End Outstanding Report March 16 */
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);

                lblBillDate.Text = DateTime.Now.ToShortDateString();

                DateTime dtCurrent = DateTime.Now;
                DateTime dtNew = new DateTime(dtCurrent.Year, dtCurrent.Month, 1);
                //txtSrtDate.Text = string.Format("{0:dd/MM/yyyy}", dtNew);
                txtSrtDate.Text = DateTime.Now.ToShortDateString();

                //txtSrtDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                //txtEdDate.Text = DateTime.Now.ToShortDateString();

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
                loadSundrys();






                OutPanel.Visible = false;
                div1.Visible = true;

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
            OutPanel.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadSundrys()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListCreditorDebitor();
        //drpLedgerName.DataSource = ds;
        //drpLedgerName.DataBind();
        //drpLedgerName.DataTextField = "GroupName";
        //drpLedgerName.DataValueField = "GroupID";

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate;
            DateTime endDate;

            int iGroupID = 0;
            string sGroupName = string.Empty;
            string sFilename = string.Empty;
            startDate = Convert.ToDateTime(txtSrtDate.Text);
            //endDate = Convert.ToDateTime(txtEdDate.Text);
            BusinessLogic bl = new BusinessLogic();
            DataSet ds = new DataSet();
            //iGroupID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
            //sGroupName = drpLedgerName.SelectedItem.Text;
            //lblSundry.Text = drpLedgerName.SelectedItem.Text;
            ds = bl.generateHireOutStandingReportDSe(iGroupID, sDataSource, startDate);

            gvLedger.DataSource = ds;
            gvLedger.DataBind();

            OutPanel.Visible = true;
            div1.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btndetails_Click(object sender, EventArgs e)
    {
        try
        {
            int iGroupID = 0;
            string sGroupName = string.Empty;
            string sFilename = string.Empty;
            string balan = string.Empty;
            DateTime startDate;
            DateTime endDate;

            startDate = Convert.ToDateTime(txtSrtDate.Text);
            //endDate = Convert.ToDateTime(txtEdDate.Text);

            BusinessLogic bl = new BusinessLogic();

            //ReportsBL.ReportClass rptOutstandingReport;
            DataSet ds = new DataSet();
            OutPanel.Visible = true;
            //iGroupID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
            //sGroupName = drpLedgerName.SelectedItem.Text;
            //lblSundry.Text = drpLedgerName.SelectedItem.Text;
            //rptOutstandingReport = new ReportsBL.ReportClass();
            //ds = bl.generateOutStandingReportDSe(iGroupID, sDataSource, startDate, endDate);

            double debit = 0;
            double credit = 0;
            DateTime Transdt;
            string datet = string.Empty;

            string nme = string.Empty;
            string connStr = string.Empty;
            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            #region Export To Excel
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Ledgername"));
                dt.Columns.Add(new DataColumn("Bill No"));
                dt.Columns.Add(new DataColumn("Bill Date"));
                dt.Columns.Add(new DataColumn("Mobile"));
                dt.Columns.Add(new DataColumn("Debit"));
                dt.Columns.Add(new DataColumn("Credit"));
                dt.Columns.Add(new DataColumn("Balance"));

                DataRow dr_export1 = dt.NewRow();
                dt.Rows.Add(dr_export1);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["Ledgername"] = dr["Ledgername"];
                    dr_export["Debit"] = dr["Debit"];
                    dr_export["Credit"] = dr["Credit"];
                    dr_export["Mobile"] = dr["phone"];

                    debit = Convert.ToDouble(dr["Debit"]);
                    credit = Convert.ToDouble(dr["Credit"]);
                    damt = damt + debit;
                    camt = camt + credit;
                    dDiffamt = damt - camt;
                    cDiffamt = camt - damt;
                    if (dDiffamt >= 0)
                    {
                        dr_export["Balance"] = dDiffamt + " Dr";
                    }
                    else if (cDiffamt > 0)
                    {
                        dr_export["Balance"] = cDiffamt + " Cr";
                    }

                    var customerID = dr["LedgerID"].ToString();
                    var dsSales = bl.ListCreditSales(connStr.Trim(), customerID);
                    var receivedData = bl.GetCustReceivedAmount(connStr);
                    if (dsSales != null)
                    {
                        foreach (DataRow drt in receivedData.Tables[0].Rows)
                        {
                            var billNo = drt["BillNo"].ToString();
                            var billAmount = drt["TotalAmount"].ToString();
                            for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                            {
                                if (billNo.Trim() == dsSales.Tables[0].Rows[i]["BillNo"].ToString())
                                {
                                    dsSales.Tables[0].Rows[i].BeginEdit();
                                    double val = (double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
                                    dsSales.Tables[0].Rows[i]["Amount"] = val;
                                    dsSales.Tables[0].Rows[i].EndEdit();

                                    if (val == 0.0)
                                        dsSales.Tables[0].Rows[i].Delete();

                                }
                            }

                            dsSales.Tables[0].AcceptChanges();
                        }
                        for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                        {
                            if (nme == "")
                            {
                                nme = dsSales.Tables[0].Rows[i]["BillNo"].ToString();
                                Transdt = Convert.ToDateTime(dsSales.Tables[0].Rows[i]["BillDate"]);
                                //datet = Convert.ToDateTime(dsSales.Tables[0].Rows[i]["BillDate"]).ToString();

                                string aa = dsSales.Tables[0].Rows[i]["BillDate"].ToString().ToUpper().Trim();
                                datet = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");

                            }
                            else
                            {
                                string aa = dsSales.Tables[0].Rows[i]["BillDate"].ToString().ToUpper().Trim();
                                datet = datet + " , " + Convert.ToDateTime(aa).ToString("dd/MM/yyyy");



                                nme = nme + " , " + dsSales.Tables[0].Rows[i]["BillNo"].ToString();
                                //datet = datet + " , " + dsSales.Tables[0].Rows[i]["BillDate"].ToString();
                                //datet = datet + " , " + Convert.ToDateTime(dsSales.Tables[0].Rows[i]["BillDate"]).ToString();
                            }
                        }
                        dr_export["Bill No"] = nme;
                        dr_export["Bill Date"] = datet;
                        nme = string.Empty;
                        datet = string.Empty;
                    }

                    //dt.Rows.Add(dr_export);

                    if (balan != dr_export["Balance"].ToString())
                        dt.Rows.Add(dr_export);

                    if (dDiffamt >= 0)
                    {
                        balan = dDiffamt + " Dr";
                    }
                    else if (cDiffamt > 0)
                    {
                        balan = cDiffamt + " Cr";
                    }
                }
                DataRow dr_lastexport1 = dt.NewRow();
                dr_lastexport1["Ledgername"] = "";
                dr_lastexport1["Debit"] = "";
                dr_lastexport1["Credit"] = "";
                dr_lastexport1["Balance"] = "";
                dr_lastexport1["Bill No"] = "";
                dr_lastexport1["Bill Date"] = "";
                dr_lastexport1["Mobile"] = "";
                dt.Rows.Add(dr_lastexport1);

                DataRow dr_lastexport2 = dt.NewRow();
                dr_lastexport2["Ledgername"] = "Total";
                dr_lastexport2["Debit"] = damt;
                dr_lastexport2["Credit"] = camt;
                dr_lastexport2["Balance"] = "";
                dr_lastexport2["Bill No"] = "";
                dr_lastexport2["Bill Date"] = "";
                dr_lastexport2["Mobile"] = "";
                dt.Rows.Add(dr_lastexport2);

                DataRow dr_lastexport3 = dt.NewRow();
                dr_lastexport3["Ledgername"] = "";
                dr_lastexport3["Debit"] = "";
                dr_lastexport3["Credit"] = "";
                dr_lastexport3["Balance"] = "";
                dr_lastexport3["Bill No"] = "";
                dr_lastexport3["Bill Date"] = "";
                dr_lastexport3["Mobile"] = "";
                dt.Rows.Add(dr_lastexport3);

                DataRow dr_de = dt.NewRow();
                dr_de["Ledgername"] = "Difference";
                if (dDiffamt >= 0)
                {
                    dr_de["Debit"] = dDiffamt;
                    dr_de["Credit"] = 0;
                }
                else if (cDiffamt >= 0)
                {
                    dr_de["Debit"] = 0;
                    dr_de["Credit"] = cDiffamt;
                }
                dr_de["Balance"] = "";
                dr_de["Bill No"] = "";
                dr_de["Bill Date"] = "";
                dr_de["Mobile"] = "";
                dt.Rows.Add(dr_de);

                ExportToExcel(dt);
            }
            #endregion
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public DataSet GenerateGridColumns()
    {
              
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        dt.Columns.Add(new DataColumn("Ledgername"));
        dt.Columns.Add(new DataColumn("Bill No"));
        dt.Columns.Add(new DataColumn("Bill Date"));
        dt.Columns.Add(new DataColumn("Mobile"));
        dt.Columns.Add(new DataColumn("Debit"));
        dt.Columns.Add(new DataColumn("Credit"));
        dt.Columns.Add(new DataColumn("Balance"));
        ds.Tables.Add(dt);

        return ds;

    }

    public DataSet GetCustDebitData()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet debitData = bl.GetCustDebitData();

        return debitData;
    }

    public DataSet GetReceivedAmount(DataSet dsGrid)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet creditData = bl.GetAllReceivedAmount();

        
        if (creditData != null)
        {
            foreach (DataRow dr in creditData.Tables[0].Rows)
            {
                //bool dupFlag = false;

                string customer = dr["Creditor"].ToString();
                string billNo = dr["BillNo"].ToString();
                //DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                double currAmount = double.Parse(dr["Amount"].ToString());

                int rowIndex = 0;

                foreach (DataRow dR in dsGrid.Tables[0].Rows)
                {
                    if (currAmount > 0)
                    {
                        if (dR["BillNo"].ToString() != "")
                        {
                            if (dR["BillNo"].ToString().Trim() == billNo.Trim())
                            {

                                double debitAmount = 0.0;

                                if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    debitAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"].ToString());
                                else
                                    continue;

                                debitAmount = debitAmount - currAmount;

                                if (debitAmount <= 0)
                                {
                                    if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    {
                                        dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = "0";
                                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                    }
                                }
                                else
                                {
                                    if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    {
                                        dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = (debitAmount).ToString("#0");
                                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                    }
                                }

                                currAmount = debitAmount;
                            }
                        }
                    }

                    rowIndex++;
                }

                dsGrid.Tables[0].AcceptChanges();

                if (currAmount == 0)
                {
                    for (int i = 0; i < dsGrid.Tables[0].Rows.Count; i++)
                    {
                        int rIndex = 0;

                        if (dsGrid.Tables[0].Rows[i]["BillNo"] != null)
                        {
                            if (dsGrid.Tables[0].Rows[i]["BillNo"].ToString().Trim() == billNo.Trim())
                            {
                                dsGrid.Tables[0].Rows[i].Delete();
                                dsGrid.Tables[0].AcceptChanges();
                            }

                        }

                        rIndex++;
                    }

                }


            }
        }

        return dsGrid;
    }

    public DataSet GetCreditData(DataSet dsGrid)
    {

        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet creditData = bl.GetAllCustCreditData();

        //DataSet receivedData = bl.GetAllReceivedAmount();

        if (creditData != null)
        {
            foreach (DataRow dr in creditData.Tables[0].Rows)
            {
                //bool dupFlag = false;

                string customer = dr["Creditor"].ToString();
                //DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                double currAmount = double.Parse(dr["Amount"].ToString());
                double openBalCR = double.Parse(dr["OpenbalanceCR"].ToString());

                currAmount = currAmount + openBalCR;

                bool flag = true;

                int rowIndex = 0;

                foreach (DataRow dR in dsGrid.Tables[0].Rows)
                {
                    if (currAmount > 0)
                    {
                        if (dR["Customer"] != null)
                        {
                            if (dR["Customer"].ToString().Trim() == customer.Trim())
                            {

                                if (dR["Customer"].ToString().Trim() == customer.Trim())
                                {

                                    double debitAmount = 0.0;

                                    if (dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales")
                                        debitAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"].ToString());
                                    //else if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    //    continue;
                                    else
                                        debitAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex]["Amount"].ToString());

                                    if (flag == true)
                                    {
                                        debitAmount = currAmount - (double.Parse(dsGrid.Tables[0].Rows[rowIndex]["OpenbalanceDR"].ToString()) + debitAmount);
                                        flag = false;
                                    }
                                    else
                                        debitAmount = currAmount - debitAmount;



                                    if (debitAmount > 0)
                                    {
                                        if (dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales")
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = "0";
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                        else
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["Amount"] = "0";
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        if (dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales")
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = (-(debitAmount)).ToString("#0");
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                        else
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["Amount"] = (-(debitAmount)).ToString("#0");
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                    }

                                    currAmount = debitAmount;
                                }
                            }
                        }
                    }

                    rowIndex++;
                }

                dsGrid.Tables[0].AcceptChanges();

                if (currAmount > 0)
                {
                    for (int i = 0; i < dsGrid.Tables[0].Rows.Count; i++)
                    {
                        int rIndex = 0;

                        if (dsGrid.Tables[0].Rows[i]["Customer"] != null)
                        {
                            if (dsGrid.Tables[0].Rows[i]["Customer"].ToString().Trim() == customer.Trim())
                            {
                                dsGrid.Tables[0].Rows[i].Delete();
                                dsGrid.Tables[0].AcceptChanges();
                            }

                        }

                        rIndex++;
                    }

                }


            }
        }

        return dsGrid;
    }

    public DataSet UpdateColumnsData(DataSet dsGrid, DataSet debitData)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);


        DateTime endDate = DateTime.Parse(DateTime.Now.ToShortDateString());



        if (debitData != null)
        {

            //IEnumerable<DataRow> query = null; 

            //if(cmbCategory.SelectedValue != "0")
            //    query = from data in debitData.Tables[0].AsEnumerable() where data.Field<string>("CategoryName") == cmbCategory.SelectedItem.Text select data;
            //else
            //    query = from data in debitData.Tables[0].AsEnumerable() select data;

            //if (query.Count<DataRow>() > 0)
            if (true)
            {

                //DataTable dt = query.CopyToDataTable<DataRow>();

                DataTable dt = debitData.Tables[0];

                DataView dv = dt.AsDataView();


                    //dv.RowFilter = "CategoryName='" ;

                    //dv.RowFilter = "Model='" + cmbModel.SelectedItem.Text + "'";

                    //dv.RowFilter = "ProductDesc='" + cmbBrand.SelectedItem.Text + "'";

                    //dv.RowFilter = "itemcode='" + cmbProduct.SelectedValue + "'";

                //dv.RowFilter = "LedgerName='" + dt["Customer"] + "'";
                //dv.RowFilter = "Bill No='" + cmbProduct.SelectedValue + "'";
                //dv.RowFilter = "Bill date='" + cmbProduct.SelectedValue + "'";
                //dv.RowFilter = "Debit='" + cmbProduct.SelectedValue + "'";
                //dv.RowFilter = "Credit='" + cmbProduct.SelectedValue + "'";
                //dv.RowFilter = "Mobile='" + cmbProduct.SelectedValue + "'";
                //dv.RowFilter = "Balance='" + cmbProduct.SelectedValue + "'";

                //DataRow dr_export = dt.NewRow();
                //dr_export["SNo"] = serialno;

                dt = dv.ToTable();

                foreach (DataRow dr in dt.Rows)
                {
                    bool dupFlag = false;
                    string fiLevel = "";
                    string seLevel = "";
                    string thLevel = "";
                    string foLevel = "";
                    string fifLevel = "";
                    string customer = dr["Customer"].ToString();
                    //if (firstLevel != "None")
                    //{
                    //    fiLevel = dr[firstLevel].ToString();
                    //}
                    //if (secondLevel != "None")
                    //{
                    //    seLevel = dr[secondLevel].ToString();
                    //}
                    //if (thirdLevel != "None")
                    //{
                    //    thLevel = dr[thirdLevel].ToString();
                    //}
                    //if (fourthLevel != "None")
                    //{
                    //    foLevel = dr[fourthLevel].ToString();
                    //}

                    //if (fifthLevel != "None")
                    //{
                    //    fifLevel = dr[fifthLevel].ToString();
                    //}

                    //string model = dr["Model"].ToString();
                    //string category = dr["CategoryName"].ToString();

                    DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                    //DateTimeHelper.DateDifference dateHelper = new DateTimeHelper.DateDifference(refDate, purchaseDate);

                    int diffDays = int.Parse((endDate - transDate).TotalDays.ToString());
                    int rowIndex = 0;

                    foreach (DataRow dR in dsGrid.Tables[0].Rows)
                    {
                        //if ((dR["Customer"] != null) || (dR["Model"] != null) || (dR["CategoryName"] != null))
                        //if ((dR["Customer"] != null) && (dR["Customer"].ToString().Trim() == customer))
                        //{
                        //    dupFlag = true;
                        //    break;
                        //}
                        //if ((firstLevel != "None") && (dR[firstLevel] != null) && (dR[firstLevel].ToString().Trim() == fiLevel))
                        //{
                        //    dupFlag = true;
                        //    break;
                        //}
                        //if ((secondLevel != "None") && (dR[secondLevel] != null) && (dR[secondLevel].ToString().Trim() == seLevel))
                        //{
                        //    dupFlag = true;
                        //    break;
                        //}
                        //if ((thirdLevel != "None") && (dR[thirdLevel] != null) && (dR[thirdLevel].ToString().Trim() == thLevel))
                        //{
                        //    dupFlag = true;
                        //    break;
                        //}
                        //if ((fourthLevel != "None") && (dR[fourthLevel] != null) && (dR[fourthLevel].ToString().Trim() == foLevel))
                        //{
                        //    dupFlag = true;
                        //    break;
                        //}
                        //if ((fifthLevel != "None") && (dR[fifthLevel] != null) && (dR[fifthLevel].ToString().Trim() == fifLevel))
                        //{
                        //    dupFlag = true;
                        //    break;
                        //}
                        rowIndex++;
                        //if ((dR[fiLevelCol] != null) && (dR[seLevelCol] != null) && (dR[thLevelCol] != null) && (dR[foLevelCol] != null))
                        //{
                        //    if (
                        //        (dR[fiLevelCol].ToString().Trim() == fiLevel) &&
                        //        (dR[seLevelCol].ToString().Trim() == seLevel) &&
                        //        (dR[thLevelCol].ToString().Trim() == thLevel) &&
                        //        (dR[foLevelCol].ToString().Trim() == foLevel))
                        //    {
                        //        dupFlag = true;
                        //        break;
                        //    }
                        //    rowIndex++;
                        //}
                    }

                    if (dupFlag)
                    {
                        int colIndex = diffDays ;
                        colIndex = colIndex + 2;

                        double currAmount = 0.0;

                        if (dsGrid.Tables[0].Rows[rowIndex][colIndex] != null)
                        {
                            if (dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString() != "")
                                currAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString());
                        }

                        double totAmount = 0.0;

                        if (dr["VoucherType"].ToString() == "Sales")
                            //totAmount = currAmount + (double.Parse(dr["NetRate"].ToString()) - double.Parse(dr["ActualDiscount"].ToString()) + double.Parse(dr["ActualVAT"].ToString()) + double.Parse(dr["ActualCST"].ToString()));
                            totAmount = currAmount + double.Parse(dr["ItemTotal"].ToString());
                        else
                            totAmount = currAmount + double.Parse(dr["Amount"].ToString());

                        dsGrid.Tables[0].Rows[rowIndex][colIndex] = totAmount.ToString("#0");
                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                    }
                    else
                    {
                        DataRow gridRow = dsGrid.Tables[0].NewRow();

                        //gridRow["Customer"] = customer;
                        //if (firstLevel != "None")
                        //    gridRow[firstLevel] = fiLevel;
                        //if (secondLevel != "None")
                        //    gridRow[secondLevel] = seLevel;
                        //if (thirdLevel != "None")
                        //    gridRow[thirdLevel] = thLevel;
                        //if (fourthLevel != "None")
                        //    gridRow[fourthLevel] = foLevel;
                        //if (fifthLevel != "None")
                        //    gridRow[fifthLevel] = fifLevel;

                        //if (selLevels.IndexOf("Customer") < 0)
                        //    gridRow["Customer"] = dr["Customer"].ToString();
                        //if (selLevels.IndexOf("ProductDesc") < 0)
                        //    gridRow["ProductDesc"] = dr["ProductDesc"].ToString();
                        //if (selLevels.IndexOf("CategoryName") < 0)
                        //    gridRow["CategoryName"] = dr["CategoryName"].ToString();
                        //if (selLevels.IndexOf("Model") < 0)
                        //    gridRow["Model"] = dr["Model"].ToString();
                        //if (selLevels.IndexOf("ItemCode") < 0)
                        //    gridRow["ItemCode"] = dr["ItemCode"].ToString();
                        ////gridRow["Model"] = model;
                        ////gridRow["CategoryName"] = category;

                        //int colIndex = diffDays / duration;
                        //colIndex = colIndex + 2;
                        //if (colIndex >= maxColIndex)
                        //{
                        //    colIndex = maxColIndex;
                        //}


                        gridRow["Ledgername"] = dr["Ledgername"].ToString();
                        gridRow["Bill No"] = dr["BillNo"].ToString();
                        gridRow["Bill Date"] = dr["TransDate"].ToString();
                        //gridRow["Mobile"] = dr["Mobile"].ToString();
                        //gridRow["Debit"] = dr["Debit"].ToString();
                        //gridRow["Credit"] = dr["Credit"].ToString();
                        gridRow["Balance"] = dr["Amount"].ToString();


                        //if (dr["VoucherType"].ToString() == "Sales")
                            //gridRow[colIndex] = (double.Parse(dr["NetRate"].ToString()) - double.Parse(dr["ActualDiscount"].ToString()) + double.Parse(dr["ActualVAT"].ToString()) + double.Parse(dr["ActualCST"].ToString()));
                            //gridRow[colIndex] = double.Parse(dr["ItemTotal"].ToString()).ToString("#0");
                        //else
                            //gridRow[colIndex] = double.Parse(dr["Amount"].ToString()).ToString("#0");

                        /*
                        if (dr["OpeningBalance"] != null)
                        {
                            double maxValue = 0.0;

                            if (gridRow[colIndex].ToString() != "")
                                maxValue = double.Parse(gridRow[colIndex].ToString());

                            gridRow[maxColIndex] = maxValue + double.Parse(dr["OpeningBalance"].ToString());

                        }*/

                        dsGrid.Tables[0].Rows.Add(gridRow);
                    }

                }
            }
        }

        return dsGrid;
    }

    public DataSet ConsolidatedGridColumns()
    {
        
        DataTable dt = new DataTable();
        DataColumn dc;

        DataSet ds = new DataSet();

        dt.Columns.Add(new DataColumn("Ledgername"));
        dt.Columns.Add(new DataColumn("Bill No"));
        dt.Columns.Add(new DataColumn("Bill Date"));
        dt.Columns.Add(new DataColumn("Mobile"));
        dt.Columns.Add(new DataColumn("Debit"));
        dt.Columns.Add(new DataColumn("Credit"));
        dt.Columns.Add(new DataColumn("Balance"));

        ds.Tables.Add(dt);

        return ds;

    }

    public DataSet UpdateFinalData(DataSet dsGrid, DataSet dsFinal)
    {
        foreach (DataRow dr in dsGrid.Tables[0].Rows)
        {
            bool dupFlag = false;
            string firlvlDRVal = "";
            //if (firstLevel != "None")
            //    firlvlDRVal = dr[firstLevel].ToString();
            //string selvlDRVal = "";
            //if (secondLevel != "None")
            //    selvlDRVal = dr[secondLevel].ToString();
            //string thlvlDRVal = "";
            //if (thirdLevel != "None")
            //    thlvlDRVal = dr[thirdLevel].ToString();
            //string folvlDRVal = "";
            //if (fourthLevel != "None")
            //    folvlDRVal = dr[fourthLevel].ToString();
            //string fiflvlDRVal = "";
            //if (fifthLevel != "None")
            //    fiflvlDRVal = dr[fifthLevel].ToString();
            int rowIndex = 0;

            //if (firlvlDRVal == string.Empty)
            //{
            //    firlvlDRVal = "OTHERS";
            //}
            string allDRVal = "";
            //if (dr["Customer"].ToString() != "")
            //    allDRVal += dr["Customer"].ToString().Trim() + ",";
            //else
            //    allDRVal += "OTHERS,";
            //if (dr["ItemCode"].ToString() != "")
            //    allDRVal += dr["ItemCode"].ToString().Trim();
            //else
            //    allDRVal += "OTHERS";


            foreach (DataRow df in dsFinal.Tables[0].Rows)
            {
                if (allDRVal != null)
                {
                    string allDFVal = "";
                    //if (firstLevel != "None")
                    //    allDFVal += df[firstLevel].ToString().Trim() + ",";
                    //else
                    //    allDFVal += "OTHERS,";
                    //if (df["Customer"].ToString() != "")
                    //    allDFVal += df["Customer"].ToString().Trim() + ",";
                    //else
                    //    allDFVal += "OTHERS,";
                    //if (df["ItemCode"].ToString() != "")
                    //    allDFVal += df["ItemCode"].ToString().Trim();
                    //else
                    //    allDFVal += "OTHERS";

                    if (allDFVal.Trim() == allDRVal.Trim())
                    {
                        //if (firstLevel!="None" && (((df[firstLevel].ToString().Trim()) + "," + (df[firstLevel].ToString().Trim()) 
                        //    + "," + (df[firstLevel].ToString().Trim()) + "," + (df[firstLevel].ToString().Trim())) 
                        //    == alllvlDRVal.Trim()))
                        //{
                        dupFlag = true;
                        break;
                    }
                    rowIndex++;
                }
            }

            if (!dupFlag)
            {

                DataRow newRow = dsFinal.Tables[0].NewRow();
                int cnt = 0;
                //newRow[0] = firlvlDRVal;
                //newRow[1] = selvlDRVal;
                //newRow[2] = thlvlDRVal;
                //newRow[3] = folvlDRVal;

                //if (firlvlDRVal != "")
                //{
                //    newRow[cnt] = firlvlDRVal;
                //    cnt++;
                //}
                //if (selvlDRVal != "")
                //{
                //    newRow[cnt] = selvlDRVal;
                //    cnt++;
                //}
                //if (thlvlDRVal != "")
                //{
                //    newRow[cnt] = thlvlDRVal;
                //    cnt++;
                //}
                //if (folvlDRVal != "")
                //{
                //    newRow[cnt] = folvlDRVal;
                //    cnt++;
                //}

                //if (fiflvlDRVal != "")
                //{
                //    newRow[cnt] = fiflvlDRVal;
                //    cnt++;
                //}

                //if (selLevels.IndexOf("Customer") < 0)
                //{
                //    newRow[cnt] = dr["Customer"];
                //    cnt++;
                //}
                //if (selLevels.IndexOf("ProductDesc") < 0)
                //{
                //    newRow[cnt] = dr["ProductDesc"];
                //    cnt++;
                //}
                //if (selLevels.IndexOf("CategoryName") < 0)
                //{
                //    newRow[cnt] = dr["CategoryName"];
                //    cnt++;
                //}
                //if (selLevels.IndexOf("Model") < 0)
                //{
                //    newRow[cnt] = dr["Model"];
                //    cnt++;
                //}
                //if (selLevels.IndexOf("ItemCode") < 0)
                //{
                //    newRow[cnt] = dr["ItemCode"];
                //    cnt++;
                //}
                for (int i = dsGrid.Tables[0].Columns.Count - 1; i > 4; i--)
                {
                    if ((dr[i] != null) && (dr[i].ToString() != ""))
                    {
                        double amount = double.Parse(dr[i].ToString());
                        newRow[i] = amount;
                    }
                    else
                    {
                        newRow[i] = "0";
                    }
                }

                dsFinal.Tables[0].Rows.Add(newRow);
            }
            else
            {

                for (int i = dsGrid.Tables[0].Columns.Count - 1; i > 5; i--)
                {

                    if (dr[i].ToString() != "")
                    {
                        double amount = 0;
                        double existamount = 0;

                        if (dr[i].ToString() != "")
                            amount = double.Parse(dr[i].ToString());

                        if ((dsFinal.Tables[0].Rows[rowIndex][i - 1] != null) && (dsFinal.Tables[0].Rows[rowIndex][i - 1].ToString() != ""))
                            existamount = double.Parse(dsFinal.Tables[0].Rows[rowIndex][i - 1].ToString());

                        dsFinal.Tables[0].Rows[rowIndex][i - 1] = existamount + amount;
                    }


                }
            }

        }

        return dsFinal;
    }

    private DataSet CalculateTotal(DataSet dsGrid)
    {

        DataView dv = dsGrid.Tables[0].AsDataView();

        string sortString = "";
        //if (firstLevel.Trim() != "None")
        //    sortString = firstLevel.Trim() + ",";
        //if (secondLevel.Trim() != "None")
        //    sortString = secondLevel.Trim() + ",";
        //if (thirdLevel.Trim() != "None")
        //    sortString = thirdLevel.Trim() + ",";
        //if (fourthLevel.Trim() != "None")
        //    sortString = fourthLevel.Trim() + ",";
        //if (fifthLevel.Trim() != "None")
        //    sortString = fifthLevel.Trim() + ",";
        //if (sortString.Contains(","))
        //    sortString = sortString.Substring(0, sortString.Length - 1);

        dv.Sort = sortString;

        DataTable dt = dv.ToTable();

        DataSet ds = new DataSet();

        ds.Tables.Add(dt);

        dsGrid = ds;

        DataRow footerRow = dsGrid.Tables[0].NewRow();
        double total = 0.0;

        footerRow[0] = "Total";

        for (int i = dsGrid.Tables[0].Columns.Count - 1; i >= 5; i--)
        {
            double amount = 0.0;

            for (int j = 0; j < dsGrid.Tables[0].Rows.Count; j++)
            {
                if (dsGrid.Tables[0].Rows[j][i].ToString() != "")
                {
                    double colAmount = double.Parse(dsGrid.Tables[0].Rows[j][i].ToString());

                    amount = amount + colAmount;

                }
            }

            footerRow[i] = amount.ToString();

            total = total + amount;

        }


        dsGrid.Tables[0].Rows.Add(footerRow);


        DataColumn dc = new DataColumn("Total");
        dsGrid.Tables[0].Columns.Add(dc);

        DataSet newData = new DataSet();

        DataTable d1 = dsGrid.Tables[0].Clone();

        newData.Tables.Add(d1);

        for (int j = 0; j < dsGrid.Tables[0].Rows.Count; j++)
        {
            double rowTotal = 0.0;

            for (int i = dsGrid.Tables[0].Columns.Count - 1; i > 5; i--)
            {
                if (dsGrid.Tables[0].Rows[j][i].ToString() != "")
                {
                    double colAmount = double.Parse(dsGrid.Tables[0].Rows[j][i].ToString());
                    rowTotal = rowTotal + colAmount;
                }
            }

            if (rowTotal > 0)
            {
                dsGrid.Tables[0].Rows[j]["Total"] = rowTotal.ToString();
                dsGrid.Tables[0].Rows[j].EndEdit();
                dsGrid.Tables[0].Rows[j].AcceptChanges();
                DataRow dr1 = newData.Tables[0].NewRow();

                for (int i = dsGrid.Tables[0].Columns.Count - 1; i >= 0; i--)
                {
                    dr1[i] = dsGrid.Tables[0].Rows[j][i].ToString();
                }

                newData.Tables[0].Rows.Add(dr1);
            }
            //else
            //{
            //    dsGrid.Tables[0].Rows[j].Delete();
            //    dsGrid.Tables[0].AcceptChanges();
            //}
        }
        
        return newData;
    }

    protected void btndetail_Click(object sender, EventArgs e)
    {
        try
        {
            int iGroupID = 0;
            string sGroupName = string.Empty;
            string sFilename = string.Empty;

            BusinessLogic bl = new BusinessLogic();

            OutPanel.Visible = true;
            //iGroupID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
            //sGroupName = drpLedgerName.SelectedItem.Text;
            //lblSundry.Text = drpLedgerName.SelectedItem.Text;

            //ds = bl.generateOutStandingReportDSe(iGroupID, sDataSource);

            string connStr = string.Empty;
            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");


            DataSet dsGird = GenerateGridColumns();
            DataSet ds = GetCustDebitData();
            ds = GetReceivedAmount(ds);
            ds = GetCreditData(ds);
            dsGird = UpdateColumnsData(dsGird, ds);
            DataSet dsFinal = ConsolidatedGridColumns();
            ds = UpdateFinalData(dsGird, dsFinal);
            ds = CalculateTotal(ds);



            #region Export To Excel
            if (dsGird.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Ledgername"));
                dt.Columns.Add(new DataColumn("Bill No"));
                dt.Columns.Add(new DataColumn("Bill Date"));
                dt.Columns.Add(new DataColumn("Mobile"));
                dt.Columns.Add(new DataColumn("Debit"));
                dt.Columns.Add(new DataColumn("Credit"));
                dt.Columns.Add(new DataColumn("Balance"));

                DataRow dr_export1 = dt.NewRow();
                dt.Rows.Add(dr_export1);

                foreach (DataRow dr in dsGird.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["Ledgername"] = dr["Ledgername"];
                    dr_export["Bill No"] = dr["Bill No"];
                    dr_export["Bill Date"] = dr["Bill Date"];
                    dr_export["Debit"] = "";
                    dr_export["Credit"] = "";
                    dr_export["Mobile"] = "";
                    dr_export["Balance"] = dr["Balance"];

                    dt.Rows.Add(dr_export);

                }
                DataRow dr_lastexport1 = dt.NewRow();
                dr_lastexport1["Ledgername"] = "";
                dr_lastexport1["Debit"] = "";
                dr_lastexport1["Credit"] = "";
                dr_lastexport1["Balance"] = "";
                dr_lastexport1["Bill No"] = "";
                dr_lastexport1["Bill Date"] = "";
                dr_lastexport1["Mobile"] = "";
                dt.Rows.Add(dr_lastexport1);

                ExportToExcel(dt);
            }
            #endregion
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
            string file = "Outstanding List Report_" + DateTime.Now.ToString() + ".xls";
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
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + file + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
    }

    protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double debit = 0;
            double credit = 0;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                debit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                credit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
                damt = damt + debit;
                camt = camt + credit;



                lblDebitSum.Text = damt.ToString("f2");  //Convert.ToString(damt);
                lblCreditSum.Text = camt.ToString("f2");
                //lblCreditSum.Text = String.Format("{0:f2}", lblCreditSum.Text);
                //lblDebitSum.Text = String.Format("{0:f2}", lblDebitSum.Text);
                dDiffamt = damt - camt;
                cDiffamt = camt - damt;
                e.Row.Cells[1].Text = debit.ToString("f2");
                e.Row.Cells[2].Text = credit.ToString("f2");
                /*Start Outstanding Report*/
                Label lblBal = (Label)e.Row.FindControl("lblBalance");
                /*End Outstanding Report*/
                if (dDiffamt >= 0)
                {
                    lblDebitDiff.Text = dDiffamt.ToString("f2");
                    lblCreditDiff.Text = "0.00";
                    lblBal.Text = dDiffamt.ToString("f2") + " Dr";
                    lblBal.ForeColor = System.Drawing.Color.Blue;

                }
                if (cDiffamt > 0)
                {
                    lblDebitDiff.Text = "0.00";
                    lblCreditDiff.Text = cDiffamt.ToString("f2");
                    lblBal.Text = cDiffamt.ToString("f2") + " Cr";
                    lblBal.ForeColor = System.Drawing.Color.Red;

                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
