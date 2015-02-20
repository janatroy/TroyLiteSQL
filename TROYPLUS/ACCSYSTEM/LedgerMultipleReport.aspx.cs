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

public partial class LedgerMultipleReport : System.Web.UI.Page
{

    public Double damt = 0.0;
    public Double camt = 0.0;
    public Double dDiffamt = 0.0;
    public Double cDiffamt = 0.0;
    public string sDataSource = string.Empty;
    /*Start Ledger Report March 16 */
    double OpBalance = 0.0;
    double dLedger = 0;
    double cLedger = 0;
    /*End Ledger Report March 16*/
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
                loadHeading();
                loadGroup("0");
                loadLedger("0");
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadHeading()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListAccHeading();
        drpHeading.DataSource = ds;
        drpHeading.DataBind();
        drpHeading.DataTextField = "Heading";
        drpHeading.DataValueField = "HeadingID";

    }


    private void loadGroup(string HeadingID)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListGroupForHeading(HeadingID);
        drpGroup.Items.Clear();
        drpGroup.Items.Add(new ListItem(" --  All --", "0"));
        drpGroup.DataSource = ds;
        drpGroup.DataBind();
        drpGroup.DataTextField = "GroupName";
        drpGroup.DataValueField = "GroupID";

    }

    private void loadLedger(string GroupID)
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListLedgerForGroup(GroupID);
        list1.Items.Clear();
        list1.Items.Add(new ListItem(" --  All --", "0"));
        list1.DataSource = ds;
        list1.DataBind();

        list1.DataTextField = "LedgerName";
        list1.DataValueField = "LedgerID";

    }
    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    DateTime startDate, endDate;
    //    int iLedgerID = 0;
    //    string sLedgerName = string.Empty;
    //    int iOrder = Convert.ToInt32(drpOrder.SelectedItem.Value);
    //    int iGroupID = 0;
    //    int iAccHeading = 0;

    //    ReportsBL.ReportClass rptLedgerAccount;

    //    LedgerPanel.Visible = true;
    //    iLedgerID = Convert.ToInt32(list1.SelectedItem.Value);
    //    iGroupID = Convert.ToInt32(drpGroup.SelectedItem.Value);
    //    iAccHeading = Convert.ToInt32(drpHeading.SelectedItem.Value);

    //    sLedgerName = list1.SelectedItem.Text;
    //    startDate = Convert.ToDateTime(txtStartDate.Text);
    //    endDate = Convert.ToDateTime(txtEndDate.Text);
    //    lblStartDate.Text = txtStartDate.Text;
    //    lblEndDate.Text = txtEndDate.Text;
    //    lblLedger.Text = list1.SelectedItem.Text;
    //    rptLedgerAccount = new ReportsBL.ReportClass();
    //    DataSet ds;
    //    //rptLedgerAccount.generateReportXML(iLedgerID, startDate, endDate, sXmlNodeName, sDataSource, sXmlPath);
    //    string sType = string.Empty;
    //    string retFlag = string.Empty;
    //    if (iLedgerID == 2)
    //    {
    //        dvSalesPurchase.Visible = true;
    //        sType = "Sales";
    //    }
    //    else if (iLedgerID == 3)
    //    {
    //        dvSalesPurchase.Visible = true;
    //        sType = "Purchase";
    //    }
    //    else
    //    {
    //        dvSalesPurchase.Visible = false;
    //    }
    //    /*
    //     * chkSPR -(Purchase Return or Sales Return)
    //     * chkSP - (Normal Purchase or Sales)
    //     */
    //    if (!chkSPR.Checked && chkSP.Checked)  /* Only (Sales or Purchase) based on sType which is "Sales or Purchae" */
    //    {
    //        retFlag = "No";
    //        ds = rptLedgerAccount.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);
    //        gvLedger.DataSource = ds;
    //        gvLedger.DataBind();

    //        if (chkSummary.Checked)
    //        {
    //            divSummary.Visible = true;
    //            divDetails.Visible = false;
    //            gvSummary.DataSource = ReturnSummary(ds);
    //            gvSummary.DataBind();
    //        }
    //        else
    //        {
    //            divSummary.Visible = false;
    //            divDetails.Visible = true;
    //            gvLedger.DataSource = ds;
    //            gvLedger.DataBind();
    //        }
    //        //CalculateDebitCreditSalesPurchase(sType);
    //        CalculateDebitCredit();
    //    }
    //    else if (chkSPR.Checked && chkSP.Checked) /* Both *****/
    //    {
    //        retFlag = "No";
    //        ds = rptLedgerAccount.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

    //        if (chkSummary.Checked)
    //        {
    //            divSummary.Visible = true;
    //            divDetails.Visible = false;
    //            gvSummary.DataSource = ReturnSummary(ds);
    //            gvSummary.DataBind();
    //        }
    //        else
    //        {
    //            divSummary.Visible = false;
    //            divDetails.Visible = true;
    //            gvLedger.DataSource = ds;
    //            gvLedger.DataBind();
    //        }

    //        //CalculateDebitCredit();
    //        if (sType == "Sales")
    //            CalculateDebitCreditSalesPurchaseReturn(sType, "Sales Return");
    //        else
    //            CalculateDebitCreditSalesPurchaseReturn(sType, "Purchase Return");
    //    }
    //    else if (chkSPR.Checked && !chkSP.Checked) /* ******Only Sales Return or Purchase Return */
    //    {
    //        retFlag = "Yes";
    //        ds = rptLedgerAccount.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

    //        if (chkSummary.Checked)
    //        {
    //            divSummary.Visible = true;
    //            divDetails.Visible = false;
    //            gvSummary.DataSource = ReturnSummary(ds);
    //            gvSummary.DataBind();
    //        }
    //        else
    //        {
    //            divSummary.Visible = false;
    //            divDetails.Visible = true;
    //            gvLedger.DataSource = ds;
    //            gvLedger.DataBind();
    //        }

    //        CalculateDebitCreditReturn(sType);
    //        //CalculateDebitCredit();
    //    }
    //    else if ((!chkSPR.Checked && !chkSP.Checked) && (sType == "Sales" || sType == "Purchase")) /* Both */
    //    {
    //        retFlag = "Both";
    //        ds = rptLedgerAccount.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

    //        if (chkSummary.Checked)
    //        {
    //            divSummary.Visible = true;
    //            divDetails.Visible = false;
    //            gvSummary.DataSource = ReturnSummary(ds);
    //            gvSummary.DataBind();
    //        }
    //        else
    //        {
    //            divSummary.Visible = false;
    //            divDetails.Visible = true;
    //            gvLedger.DataSource = ds;
    //            gvLedger.DataBind();
    //        }

    //        CalculateDebitCredit();
    //    }
    //    else
    //    {
    //        ds = rptLedgerAccount.generateReportDSLedger(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, iOrder);

    //        if (chkSummary.Checked)
    //        {
    //            divSummary.Visible = true;
    //            divDetails.Visible = false;
    //            gvSummary.DataSource = ReturnSummary(ds);
    //            gvSummary.DataBind();
    //        }
    //        else
    //        {
    //            divSummary.Visible = false;
    //            divDetails.Visible = true;
    //            gvLedger.DataSource = ds;
    //            gvLedger.DataBind();
    //        }
    //        CalculateDebitCredit();
    //    }



    //    // CalculateDebitCredit();
    //}

    private DataSet ReturnSummary(DataSet dsData)
    {

        DataSet ds;
        DataTable dt;
        DataRow drNew;
        DataColumn dc;


        ds = new DataSet();
        dt = new DataTable();

        dc = new DataColumn("Ledger");
        dt.Columns.Add(dc);

        dc = new DataColumn("Debit");
        dt.Columns.Add(dc);

        dc = new DataColumn("Credit");
        dt.Columns.Add(dc);

        dc = new DataColumn("VoucherType");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        ArrayList lsLedger = new ArrayList();

        foreach (DataRow dr in dsData.Tables[0].Rows)
        {
            if (!lsLedger.Contains(dr["Ledger"].ToString()))
                lsLedger.Add(dr["Ledger"].ToString());
        }

        foreach (string ledger in lsLedger)
        {
            drNew = dt.NewRow();
            drNew["Ledger"] = ledger;
            drNew["Debit"] = "0.00";
            drNew["Credit"] = "0.00";
            drNew["VoucherType"] = string.Empty;
            ds.Tables[0].Rows.Add(drNew);

        }

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string ledger = ds.Tables[0].Rows[i]["Ledger"].ToString();
            double credit = 0.00;
            double debit = 0.00;

            foreach (DataRow dr in dsData.Tables[0].Rows)
            {
                if (dr["Ledger"].ToString() == ledger)
                {
                    credit = credit + double.Parse(dr["Credit"].ToString());
                    debit = debit + double.Parse(dr["Debit"].ToString());
                }
            }

            ds.Tables[0].Rows[i]["Debit"] = debit;
            ds.Tables[0].Rows[i]["Credit"] = credit;

        }

        return ds;

    }

    protected void CalculateDebitCreditSalesPurchase(string sType)
    {

        DateTime startDate;
        double opCr = 0.0;
        double opDr = 0.0;
        double netOp = 0.0;
        double cbDr = 0.00;
        double cbCr = 0.00;
        int ledgerID = 0;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        ledgerID = Convert.ToInt32(list1.SelectedItem.Value);

        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();


        if (sType == "Purchase")
            opDr = rpt.getOpeningBalanceSalesPurchase(ledgerID, sType, startDate, sDataSource);
        else
            opCr = rpt.getOpeningBalanceSalesPurchase(ledgerID, sType, startDate, sDataSource);


        cbDr = opDr + Convert.ToDouble(lblDebitDiff.Text);
        cbCr = opCr + Convert.ToDouble(lblCreditDiff.Text);

        if (opDr > opCr)
        {
            netOp = opDr - opCr;
            lblOBDR.Text = netOp.ToString("f2"); // Convert.ToString(netOp);
            lblOBCR.Text = "0.00";
        }
        else
        {
            netOp = opCr - opDr;
            lblOBDR.Text = "0.00";
            lblOBCR.Text = netOp.ToString("f2"); // Convert.ToString(netOp);
        }
        if (cbDr > cbCr)
        {

            cbDr = cbDr - cbCr;
            lblClosDr.Text = cbDr.ToString("f2"); // Convert.ToString(cbDr);
            lblClosCr.Text = "0.00";
        }
        else
        {
            cbCr = cbCr - cbDr;
            lblClosCr.Text = cbCr.ToString("f2");// Convert.ToString(cbCr);
            lblClosDr.Text = "0.00";
        }

        lblBillDate.Text = DateTime.Now.ToShortDateString();

    }

    protected void CalculateDebitCreditReturn(string sType)
    {

        DateTime startDate;
        double opCr = 0.0;
        double opDr = 0.0;
        double netOp = 0.0;
        double cbDr = 0.00;
        double cbCr = 0.00;
        int ledgerID = 0;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        ledgerID = Convert.ToInt32(list1.SelectedItem.Value);

        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();


        if (sType == "Sales")
            opDr = rpt.getOpeningBalanceReturn(ledgerID, "Sales Return", startDate, sDataSource);
        else
            opCr = rpt.getOpeningBalanceReturn(ledgerID, "Purchase Return", startDate, sDataSource);


        cbDr = opDr + Convert.ToDouble(lblDebitDiff.Text);
        cbCr = opCr + Convert.ToDouble(lblCreditDiff.Text);

        if (opDr > opCr)
        {
            netOp = opDr - opCr;
            lblOBDR.Text = netOp.ToString("f2"); // Convert.ToString(netOp);
            lblOBCR.Text = "0.00";
        }
        else
        {
            netOp = opCr - opDr;
            lblOBDR.Text = "0.00";
            lblOBCR.Text = netOp.ToString("f2"); // Convert.ToString(netOp);
        }
        if (cbDr > cbCr)
        {

            cbDr = cbDr - cbCr;
            lblClosDr.Text = cbDr.ToString("f2"); // Convert.ToString(cbDr);
            lblClosCr.Text = "0.00";
        }
        else
        {
            cbCr = cbCr - cbDr;
            lblClosCr.Text = cbCr.ToString("f2");// Convert.ToString(cbCr);
            lblClosDr.Text = "0.00";
        }

        lblBillDate.Text = DateTime.Now.ToShortDateString();

    }

    protected void CalculateDebitCredit()
    {

        DateTime startDate;
        double opCr = 0.0;
        double opDr = 0.0;
        double netOp = 0.0;
        double cbDr = 0.00;
        double cbCr = 0.00;
        int ledgerID = 0;
        int GroupID = 0;
        int AccHeadingID = 0;

        startDate = Convert.ToDateTime(txtStartDate.Text);
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        ledgerID = Convert.ToInt32(list1.SelectedItem.Value);
        GroupID = Convert.ToInt32(drpGroup.SelectedItem.Value);
        AccHeadingID = Convert.ToInt32(drpHeading.SelectedItem.Value);

        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        opCr = rpt.getOpeningBalance(AccHeadingID, GroupID, ledgerID, "credit", startDate, sDataSource);
        //opCr = rpt.getLedgerOpeningBalance(ledgerID, "credit", sDataSource); // +rpt.getOpeningBalance(ledgerID, "credit", startDate, sDataSource);
        opDr = rpt.getOpeningBalance(AccHeadingID, GroupID, ledgerID, "debit", startDate, sDataSource);
        //opDr = rpt.getLedgerOpeningBalance(ledgerID, "debit", sDataSource); // +rpt.getOpeningBalance(ledgerID, "debit", startDate, sDataSource);
        cbDr = opDr + Convert.ToDouble(lblDebitDiff.Text);
        cbCr = opCr + Convert.ToDouble(lblCreditDiff.Text);

        if (opDr > opCr)
        {
            netOp = opDr - opCr;
            lblOBDR.Text = netOp.ToString("f2"); // Convert.ToString(netOp);
            lblOBCR.Text = "0.00";
        }
        else
        {
            netOp = opCr - opDr;
            lblOBDR.Text = "0.00";
            lblOBCR.Text = netOp.ToString("f2"); // Convert.ToString(netOp);
        }
        if (cbDr > cbCr)
        {

            cbDr = cbDr - cbCr;
            lblClosDr.Text = cbDr.ToString("f2"); // Convert.ToString(cbDr);
            lblClosCr.Text = "0.00";
        }
        else
        {
            cbCr = cbCr - cbDr;
            lblClosCr.Text = cbCr.ToString("f2");// Convert.ToString(cbCr);
            lblClosDr.Text = "0.00";
        }

        lblBillDate.Text = DateTime.Now.ToShortDateString();

    }

    protected void CalculateDebitCreditSalesPurchaseReturn(string sType, string sReturnType)
    {

        DateTime startDate;
        double opCr = 0.0;
        double opDr = 0.0;
        double netOp = 0.0;
        double cbDr = 0.00;
        double cbCr = 0.00;

        double opSalesReturn = 0;
        double opPurchaseReturn = 0;

        int ledgerID = 0;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        ledgerID = Convert.ToInt32(list1.SelectedItem.Value);

        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();


        if (sType == "Purchase")
            opDr = rpt.getOpeningBalanceSalesPurchase(ledgerID, sType, startDate, sDataSource);
        else
            opCr = rpt.getOpeningBalanceSalesPurchase(ledgerID, sType, startDate, sDataSource);

        if (sReturnType == "Sales Return")
            opSalesReturn = rpt.getOpeningBalanceReturn(ledgerID, sReturnType, startDate, sDataSource);
        else
            opPurchaseReturn = rpt.getOpeningBalanceReturn(ledgerID, sReturnType, startDate, sDataSource);


        opDr = opDr - opPurchaseReturn;
        opCr = opCr - opSalesReturn;


        cbDr = opDr + Convert.ToDouble(lblDebitDiff.Text);
        cbCr = opCr + Convert.ToDouble(lblCreditDiff.Text);

        if (opDr > opCr)
        {
            netOp = opDr - opCr;
            lblOBDR.Text = netOp.ToString("f2"); // Convert.ToString(netOp);
            lblOBCR.Text = "0.00";
        }
        else
        {
            netOp = opCr - opDr;
            lblOBDR.Text = "0.00";
            lblOBCR.Text = netOp.ToString("f2"); // Convert.ToString(netOp);
        }
        if (cbDr > cbCr)
        {

            cbDr = cbDr - cbCr;
            lblClosDr.Text = cbDr.ToString("f2"); // Convert.ToString(cbDr);
            lblClosCr.Text = "0.00";
        }
        else
        {
            cbCr = cbCr - cbDr;
            lblClosCr.Text = cbCr.ToString("f2");// Convert.ToString(cbCr);
            lblClosDr.Text = "0.00";
        }

        lblBillDate.Text = DateTime.Now.ToShortDateString();

    }

    protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double debit = 0;
            double credit = 0;

            /*Start Ledger Report*/
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int iLedgerID = 0;
                int iGroupID = 0;
                int iAccHeadingID = 0;

                iLedgerID = Convert.ToInt32(list1.SelectedItem.Value);
                iGroupID = Convert.ToInt32(drpGroup.SelectedItem.Value);
                iAccHeadingID = Convert.ToInt32(drpHeading.SelectedItem.Value);

                string sType = string.Empty;

                if (iLedgerID == 2)
                    sType = "Sales";
                else if (iLedgerID == 3)
                    sType = "Purchase";

                double opSalesReturn = 0;
                double opPurchaseReturn = 0;
                int ledgerID = 0;
                double opCr = 0.0;
                double opDr = 0.0;
                DateTime startDate;
                startDate = Convert.ToDateTime(txtStartDate.Text);
                ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();

                ledgerID = Convert.ToInt32(list1.SelectedItem.Value);

                if (chkSPR.Checked && chkSP.Checked) /* Both *****/
                {
                    if (sType == "Purchase")
                    {

                        opDr = rpt.getOpeningBalanceSalesPurchase(iLedgerID, sType, startDate, sDataSource);
                        opPurchaseReturn = rpt.getOpeningBalanceReturn(ledgerID, "Purchase Return", startDate, sDataSource);
                        if (opDr - opPurchaseReturn >= 0)
                        {
                            opDr = opDr - opPurchaseReturn;
                            opCr = 0;
                        }
                        else
                        {
                            opCr = opPurchaseReturn - opDr;
                            opDr = 0;
                        }
                    }
                    else
                    {
                        opSalesReturn = rpt.getOpeningBalanceReturn(ledgerID, "Sales Return", startDate, sDataSource);
                        opCr = rpt.getOpeningBalanceSalesPurchase(iLedgerID, sType, startDate, sDataSource);
                        if (opCr - opSalesReturn >= 0)
                        {
                            opCr = opCr - opSalesReturn;
                            opDr = 0;
                        }
                        else
                        {
                            opDr = opSalesReturn - opCr;
                            opCr = 0;

                        }
                    }
                }
                else if (chkSPR.Checked && !chkSP.Checked)
                {
                    if (sType == "Purchase")
                    {
                        opCr = rpt.getOpeningBalanceReturn(ledgerID, "Purchase Return", startDate, sDataSource);
                    }
                    else
                    {
                        opDr = rpt.getOpeningBalanceReturn(ledgerID, "Sales Return", startDate, sDataSource);
                    }
                }
                else
                {
                    opCr = rpt.getOpeningBalance(iAccHeadingID, iGroupID, ledgerID, "credit", startDate, sDataSource);
                    opDr = rpt.getOpeningBalance(iAccHeadingID, iGroupID, ledgerID, "debit", startDate, sDataSource);
                }

                OpBalance = opDr - opCr;
                if (OpBalance >= 0)
                {
                    dLedger = dLedger + OpBalance;
                }
                else
                {
                    cLedger = cLedger + OpBalance;
                }

            }
            /*End Ledger Report*/
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                debit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                credit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
                damt = damt + debit;
                camt = camt + credit;

                lblDebitSum.Text = damt.ToString("f2");  //Convert.ToString(damt);
                lblCreditSum.Text = camt.ToString("f2");

                dDiffamt = damt - camt; // +dLedger;
                cDiffamt = camt - damt; // +cLedger;
                //dLedger = 0;
                //cLedger = 0;
                e.Row.Cells[3].Text = debit.ToString("f2");
                e.Row.Cells[4].Text = credit.ToString("f2");
                /*Start Ledger Report*/
                Label lblBal = (Label)e.Row.FindControl("lblBalance");
                /*End Ledger Report*/
                if (dDiffamt >= 0)
                {
                    lblDebitDiff.Text = dDiffamt.ToString("f2"); // Convert.ToString(dDiffamt);
                    lblCreditDiff.Text = "0.00";
                    /*Start Ledger Report*/
                    dDiffamt = dDiffamt + OpBalance;
                    if (dDiffamt >= 0)
                    {
                        lblBal.Text = dDiffamt.ToString("f2") + " Dr";
                        lblBal.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        lblBal.Text = Math.Abs(dDiffamt).ToString("f2") + " Cr";
                        lblBal.ForeColor = System.Drawing.Color.Blue;
                    }
                    //OpBalance = 0;
                    /*End Ledger Report*/

                }
                if (cDiffamt > 0)
                {
                    lblDebitDiff.Text = "0.00";
                    lblCreditDiff.Text = cDiffamt.ToString("f2"); // Convert.ToString(cDiffamt);
                    /*Start Ledger Report*/
                    cDiffamt = cDiffamt - OpBalance;
                    if (cDiffamt > 0)
                    {
                        lblBal.Text = cDiffamt.ToString("f2") + " Cr";
                        lblBal.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblBal.Text = Math.Abs(cDiffamt).ToString("f2") + " Dr";
                        lblBal.ForeColor = System.Drawing.Color.Blue;
                    }
                    /*End Ledger Report*/
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double debit = 0;
            double credit = 0;

            /*Start Ledger Report*/
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int iLedgerID = 0;
                int iGroupID = 0;
                int iAccHeadingID = 0;

                iLedgerID = Convert.ToInt32(list1.SelectedItem.Value);
                iGroupID = Convert.ToInt32(drpGroup.SelectedItem.Value);
                iAccHeadingID = Convert.ToInt32(drpHeading.SelectedItem.Value);

                string sType = string.Empty;

                if (iLedgerID == 2)
                    sType = "Sales";
                else if (iLedgerID == 3)
                    sType = "Purchase";

                double opSalesReturn = 0;
                double opPurchaseReturn = 0;
                int ledgerID = 0;
                double opCr = 0.0;
                double opDr = 0.0;
                DateTime startDate;
                startDate = Convert.ToDateTime(txtStartDate.Text);
                ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();

                ledgerID = Convert.ToInt32(list1.SelectedItem.Value);

                if (chkSPR.Checked && chkSP.Checked) /* Both *****/
                {
                    if (sType == "Purchase")
                    {

                        opDr = rpt.getOpeningBalanceSalesPurchase(iLedgerID, sType, startDate, sDataSource);
                        opPurchaseReturn = rpt.getOpeningBalanceReturn(ledgerID, "Purchase Return", startDate, sDataSource);
                        if (opDr - opPurchaseReturn >= 0)
                        {
                            opDr = opDr - opPurchaseReturn;
                            opCr = 0;
                        }
                        else
                        {
                            opCr = opPurchaseReturn - opDr;
                            opDr = 0;
                        }
                    }
                    else
                    {
                        opSalesReturn = rpt.getOpeningBalanceReturn(ledgerID, "Sales Return", startDate, sDataSource);
                        opCr = rpt.getOpeningBalanceSalesPurchase(iLedgerID, sType, startDate, sDataSource);
                        if (opCr - opSalesReturn >= 0)
                        {
                            opCr = opCr - opSalesReturn;
                            opDr = 0;
                        }
                        else
                        {
                            opDr = opSalesReturn - opCr;
                            opCr = 0;

                        }
                    }
                }
                else if (chkSPR.Checked && !chkSP.Checked)
                {
                    if (sType == "Purchase")
                    {
                        opCr = rpt.getOpeningBalanceReturn(ledgerID, "Purchase Return", startDate, sDataSource);
                    }
                    else
                    {
                        opDr = rpt.getOpeningBalanceReturn(ledgerID, "Sales Return", startDate, sDataSource);
                    }
                }
                else
                {
                    opCr = rpt.getOpeningBalance(iAccHeadingID, iGroupID, ledgerID, "credit", startDate, sDataSource);
                    opDr = rpt.getOpeningBalance(iAccHeadingID, iGroupID, ledgerID, "debit", startDate, sDataSource);
                }

                OpBalance = opDr - opCr;
                if (OpBalance >= 0)
                {
                    dLedger = dLedger + OpBalance;
                }
                else
                {
                    cLedger = cLedger + OpBalance;
                }

            }
            /*End Ledger Report*/
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                debit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                credit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
                damt = damt + debit;
                camt = camt + credit;

                lblDebitSum.Text = damt.ToString("f2");  //Convert.ToString(damt);
                lblCreditSum.Text = camt.ToString("f2");

                dDiffamt = damt - camt; // +dLedger;
                cDiffamt = camt - damt; // +cLedger;
                //dLedger = 0;
                //cLedger = 0;
                e.Row.Cells[1].Text = debit.ToString("f2");
                e.Row.Cells[2].Text = credit.ToString("f2");
                /*Start Ledger Report*/
                Label lblBal = (Label)e.Row.FindControl("lblBalance");
                /*End Ledger Report*/
                if (dDiffamt >= 0)
                {
                    lblDebitDiff.Text = dDiffamt.ToString("f2"); // Convert.ToString(dDiffamt);
                    lblCreditDiff.Text = "0.00";
                    /*Start Ledger Report*/
                    dDiffamt = dDiffamt + OpBalance;
                    if (dDiffamt >= 0)
                    {
                        lblBal.Text = dDiffamt.ToString("f2") + " Dr";
                        lblBal.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        lblBal.Text = Math.Abs(dDiffamt).ToString("f2") + " Cr";
                        lblBal.ForeColor = System.Drawing.Color.Blue;
                    }
                    //OpBalance = 0;
                    /*End Ledger Report*/

                }
                if (cDiffamt > 0)
                {
                    lblDebitDiff.Text = "0.00";
                    lblCreditDiff.Text = cDiffamt.ToString("f2"); // Convert.ToString(cDiffamt);
                    /*Start Ledger Report*/
                    cDiffamt = cDiffamt - OpBalance;
                    if (cDiffamt > 0)
                    {
                        lblBal.Text = cDiffamt.ToString("f2") + " Cr";
                        lblBal.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblBal.Text = Math.Abs(cDiffamt).ToString("f2") + " Dr";
                        lblBal.ForeColor = System.Drawing.Color.Blue;
                    }
                    /*End Ledger Report*/
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpHeading_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadGroup(drpHeading.SelectedValue);
            loadLedger(drpGroup.SelectedValue);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadLedger(drpGroup.SelectedValue);
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
            DateTime startDate, endDate;
            int iLedgerID = 0;
            string sLedgerName = string.Empty;
            //int iOrder = Convert.ToInt32(drpOrder.SelectedItem.Value);
            int iOrder = 0;
            int iGroupID = 0;
            int iAccHeading = 0;

            DataSet ds;
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Date"));
            dt.Columns.Add(new DataColumn("Particulars"));
            dt.Columns.Add(new DataColumn("Ledger"));
            dt.Columns.Add(new DataColumn("Voucher Type"));
            dt.Columns.Add(new DataColumn("Debit"));
            dt.Columns.Add(new DataColumn("Credit"));
            dt.Columns.Add(new DataColumn("Balance"));


            BusinessLogic bl = new BusinessLogic();

            ReportsBL.ReportClass rptLedgerAccount;

            LedgerPanel.Visible = true;

            for (int i = list1.Items.Count - 1; i >= 0; i--)
            {
                if (list1.Items[i].Selected == true)
                {
                    iLedgerID = Convert.ToInt32(list1.Items[i].Value);
                    iGroupID = Convert.ToInt32(drpGroup.SelectedItem.Value);
                    iAccHeading = Convert.ToInt32(drpHeading.SelectedItem.Value);

                    sLedgerName = list1.Items[i].Text;
                    startDate = Convert.ToDateTime(txtStartDate.Text);
                    endDate = Convert.ToDateTime(txtEndDate.Text);
                    lblStartDate.Text = txtStartDate.Text;
                    lblEndDate.Text = txtEndDate.Text;
                    lblLedger.Text = list1.Items[i].Text;
                    rptLedgerAccount = new ReportsBL.ReportClass();

                    string sType = string.Empty;
                    string retFlag = string.Empty;
                    if (iLedgerID == 2)
                    {
                        dvSalesPurchase.Visible = true;
                        sType = "Sales";
                    }
                    else if (iLedgerID == 3)
                    {
                        dvSalesPurchase.Visible = true;
                        sType = "Purchase";
                    }
                    else
                    {
                        dvSalesPurchase.Visible = false;
                    }

                    if (!chkSPR.Checked && chkSP.Checked)
                    {
                        retFlag = "No";
                        ds = bl.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);
                        double debit = 0.00;
                        double credit = 0.00;

                        damt = 0.0;

                        camt = 0.0;
                        dDiffamt = 0.0;
                        cDiffamt = 0.0;
                        #region Export To Excel
                        if (ds.Tables[0].Rows.Count > 0)
                        {


                            DataRow dr_export1 = dt.NewRow();
                            dt.Rows.Add(dr_export1);

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DataRow dr_export = dt.NewRow();
                                dr_export["Date"] = dr["Date"];
                                dr_export["Particulars"] = dr["Particulars"];
                                dr_export["Ledger"] = dr["ParticularsI"];
                                dr_export["Voucher Type"] = dr["VoucherType"];
                                dr_export["Debit"] = dr["Debit"];
                                dr_export["Credit"] = dr["Credit"];

                                debit = Convert.ToDouble(dr["Debit"]);
                                credit = Convert.ToDouble(dr["Credit"]);
                                damt = damt + debit;
                                camt = camt + credit;
                                dDiffamt = damt - camt;
                                cDiffamt = camt - damt;
                                if (dDiffamt >= 0)
                                {
                                    dDiffamt = dDiffamt + OpBalance;
                                    if (dDiffamt >= 0)
                                    {
                                        dr_export["Balance"] = dDiffamt + " Dr";
                                    }
                                    else
                                    {
                                        dr_export["balance"] = Math.Abs(dDiffamt) + " Cr";
                                    }
                                }
                                if (cDiffamt > 0)
                                {
                                    cDiffamt = cDiffamt - OpBalance;
                                    if (cDiffamt > 0)
                                    {
                                        dr_export["Balance"] = cDiffamt + " Cr";
                                    }
                                    else
                                    {
                                        dr_export["Balance"] = Math.Abs(cDiffamt) + " Dr";
                                    }
                                }

                                dt.Rows.Add(dr_export);
                            }

                            DataRow dr_export2 = dt.NewRow();
                            dr_export2["Date"] = "";
                            dr_export2["Particulars"] = "";
                            dr_export2["Ledger"] = "";
                            dr_export2["Voucher Type"] = "";
                            dr_export2["Debit"] = "";
                            dr_export2["Credit"] = "";
                            dt.Rows.Add(dr_export2);

                            DataRow dr_lastexport2 = dt.NewRow();
                            dr_lastexport2["Date"] = "Total";
                            dr_lastexport2["Debit"] = damt;
                            dr_lastexport2["Credit"] = camt;
                            dr_lastexport2["Balance"] = "";
                            dr_lastexport2["Ledger"] = "";
                            dr_lastexport2["Voucher Type"] = "";
                            dt.Rows.Add(dr_lastexport2);

                            DataRow dr_lastexport3 = dt.NewRow();
                            dr_lastexport3["Date"] = "";
                            dr_lastexport3["Debit"] = "";
                            dr_lastexport3["Credit"] = "";
                            dr_lastexport3["Balance"] = "";
                            dr_lastexport3["Ledger"] = "";
                            dr_lastexport3["Voucher Type"] = "";
                            dt.Rows.Add(dr_lastexport3);

                            DataRow dr_de = dt.NewRow();
                            dr_de["Date"] = "Balance";
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
                            dr_de["Ledger"] = "";
                            dr_de["Voucher Type"] = "";
                            dt.Rows.Add(dr_de);

                        }
                        #endregion
                    }
                    else if (chkSPR.Checked && chkSP.Checked)
                    {
                        retFlag = "No";
                        ds = bl.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);
                        double debit = 0.00;
                        double credit = 0.00;

                        damt = 0.0;

                        camt = 0.0;
                        dDiffamt = 0.0;
                        cDiffamt = 0.0;
                        #region Export To Excel
                        if (ds.Tables[0].Rows.Count > 0)
                        {



                            DataRow dr_export1 = dt.NewRow();
                            dt.Rows.Add(dr_export1);

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DataRow dr_export = dt.NewRow();
                                dr_export["Date"] = dr["Date"];
                                dr_export["Particulars"] = dr["Particulars"];
                                dr_export["Ledger"] = dr["ParticularsI"];
                                dr_export["Voucher Type"] = dr["VoucherType"];
                                dr_export["Debit"] = dr["Debit"];
                                dr_export["Credit"] = dr["Credit"];

                                debit = Convert.ToDouble(dr["Debit"]);
                                credit = Convert.ToDouble(dr["Credit"]);
                                damt = damt + debit;
                                camt = camt + credit;
                                dDiffamt = damt - camt;
                                cDiffamt = camt - damt;
                                if (dDiffamt >= 0)
                                {
                                    dDiffamt = dDiffamt + OpBalance;
                                    if (dDiffamt >= 0)
                                    {
                                        dr_export["Balance"] = dDiffamt + " Dr";
                                    }
                                    else
                                    {
                                        dr_export["balance"] = Math.Abs(dDiffamt) + " Cr";
                                    }
                                }
                                if (cDiffamt > 0)
                                {
                                    cDiffamt = cDiffamt - OpBalance;
                                    if (cDiffamt > 0)
                                    {
                                        dr_export["Balance"] = cDiffamt + " Cr";
                                    }
                                    else
                                    {
                                        dr_export["Balance"] = Math.Abs(cDiffamt) + " Dr";
                                    }
                                }

                                dt.Rows.Add(dr_export);
                            }

                            DataRow dr_export2 = dt.NewRow();
                            dr_export2["Date"] = "";
                            dr_export2["Particulars"] = "";
                            dr_export2["Ledger"] = "";
                            dr_export2["Voucher Type"] = "";
                            dr_export2["Debit"] = "";
                            dr_export2["Credit"] = "";
                            dt.Rows.Add(dr_export2);

                            DataRow dr_lastexport2 = dt.NewRow();
                            dr_lastexport2["Date"] = "Total";
                            dr_lastexport2["Debit"] = damt;
                            dr_lastexport2["Credit"] = camt;
                            dr_lastexport2["Balance"] = "";
                            dr_lastexport2["Ledger"] = "";
                            dr_lastexport2["Voucher Type"] = "";
                            dt.Rows.Add(dr_lastexport2);

                            DataRow dr_lastexport3 = dt.NewRow();
                            dr_lastexport3["Date"] = "";
                            dr_lastexport3["Debit"] = "";
                            dr_lastexport3["Credit"] = "";
                            dr_lastexport3["Balance"] = "";
                            dr_lastexport3["Ledger"] = "";
                            dr_lastexport3["Voucher Type"] = "";
                            dt.Rows.Add(dr_lastexport3);

                            DataRow dr_de = dt.NewRow();
                            dr_de["Date"] = "Balance";
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
                            dr_de["Ledger"] = "";
                            dr_de["Voucher Type"] = "";
                            dt.Rows.Add(dr_de);

                        }
                        #endregion
                    }
                    else if (chkSPR.Checked && !chkSP.Checked)
                    {
                        retFlag = "Yes";
                        ds = bl.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

                        double debit = 0.00;
                        double credit = 0.00;

                        damt = 0.0;

                        camt = 0.0;
                        dDiffamt = 0.0;
                        cDiffamt = 0.0;
                        #region Export To Excel
                        if (ds.Tables[0].Rows.Count > 0)
                        {



                            DataRow dr_export1 = dt.NewRow();
                            dt.Rows.Add(dr_export1);

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DataRow dr_export = dt.NewRow();
                                dr_export["Date"] = dr["Date"];
                                dr_export["Particulars"] = dr["Particulars"];
                                dr_export["Ledger"] = dr["ParticularsI"];
                                dr_export["Voucher Type"] = dr["VoucherType"];
                                dr_export["Debit"] = dr["Debit"];
                                dr_export["Credit"] = dr["Credit"];

                                debit = Convert.ToDouble(dr["Debit"]);
                                credit = Convert.ToDouble(dr["Credit"]);
                                damt = damt + debit;
                                camt = camt + credit;
                                dDiffamt = damt - camt;
                                cDiffamt = camt - damt;
                                if (dDiffamt >= 0)
                                {
                                    dDiffamt = dDiffamt + OpBalance;
                                    if (dDiffamt >= 0)
                                    {
                                        dr_export["Balance"] = dDiffamt + " Dr";
                                    }
                                    else
                                    {
                                        dr_export["balance"] = Math.Abs(dDiffamt) + " Cr";
                                    }
                                }
                                if (cDiffamt > 0)
                                {
                                    cDiffamt = cDiffamt - OpBalance;
                                    if (cDiffamt > 0)
                                    {
                                        dr_export["Balance"] = cDiffamt + " Cr";
                                    }
                                    else
                                    {
                                        dr_export["Balance"] = Math.Abs(cDiffamt) + " Dr";
                                    }
                                }

                                dt.Rows.Add(dr_export);
                            }

                            DataRow dr_export2 = dt.NewRow();
                            dr_export2["Date"] = "";
                            dr_export2["Particulars"] = "";
                            dr_export2["Ledger"] = "";
                            dr_export2["Voucher Type"] = "";
                            dr_export2["Debit"] = "";
                            dr_export2["Credit"] = "";
                            dt.Rows.Add(dr_export2);

                            DataRow dr_lastexport2 = dt.NewRow();
                            dr_lastexport2["Date"] = "Total";
                            dr_lastexport2["Debit"] = damt;
                            dr_lastexport2["Credit"] = camt;
                            dr_lastexport2["Balance"] = "";
                            dr_lastexport2["Ledger"] = "";
                            dr_lastexport2["Voucher Type"] = "";
                            dt.Rows.Add(dr_lastexport2);

                            DataRow dr_lastexport3 = dt.NewRow();
                            dr_lastexport3["Date"] = "";
                            dr_lastexport3["Debit"] = "";
                            dr_lastexport3["Credit"] = "";
                            dr_lastexport3["Balance"] = "";
                            dr_lastexport3["Ledger"] = "";
                            dr_lastexport3["Voucher Type"] = "";
                            dt.Rows.Add(dr_lastexport3);

                            DataRow dr_de = dt.NewRow();
                            dr_de["Date"] = "Balance";
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
                            dr_de["Ledger"] = "";
                            dr_de["Voucher Type"] = "";
                            dt.Rows.Add(dr_de);


                        }
                        #endregion
                    }
                    else if ((!chkSPR.Checked && !chkSP.Checked) && (sType == "Sales" || sType == "Purchase")) /* Both */
                    {
                        retFlag = "Both";
                        ds = bl.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

                        double debit = 0.00;
                        double credit = 0.00;

                        damt = 0.0;

                        camt = 0.0;
                        dDiffamt = 0.0;
                        cDiffamt = 0.0;
                        #region Export To Excel
                        if (ds.Tables[0].Rows.Count > 0)
                        {



                            DataRow dr_export1 = dt.NewRow();
                            dt.Rows.Add(dr_export1);

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DataRow dr_export = dt.NewRow();
                                dr_export["Date"] = dr["Date"];
                                dr_export["Particulars"] = dr["Particulars"];
                                dr_export["Ledger"] = dr["ParticularsI"];
                                dr_export["Voucher Type"] = dr["VoucherType"];
                                dr_export["Debit"] = dr["Debit"];
                                dr_export["Credit"] = dr["Credit"];

                                debit = Convert.ToDouble(dr["Debit"]);
                                credit = Convert.ToDouble(dr["Credit"]);
                                damt = damt + debit;
                                camt = camt + credit;
                                dDiffamt = damt - camt;
                                cDiffamt = camt - damt;
                                if (dDiffamt >= 0)
                                {
                                    dDiffamt = dDiffamt + OpBalance;
                                    if (dDiffamt >= 0)
                                    {
                                        dr_export["Balance"] = dDiffamt + " Dr";
                                    }
                                    else
                                    {
                                        dr_export["balance"] = Math.Abs(dDiffamt) + " Cr";
                                    }
                                }
                                if (cDiffamt > 0)
                                {
                                    cDiffamt = cDiffamt - OpBalance;
                                    if (cDiffamt > 0)
                                    {
                                        dr_export["Balance"] = cDiffamt + " Cr";
                                    }
                                    else
                                    {
                                        dr_export["Balance"] = Math.Abs(cDiffamt) + " Dr";
                                    }
                                }

                                dt.Rows.Add(dr_export);
                            }

                            DataRow dr_export2 = dt.NewRow();
                            dr_export2["Date"] = "";
                            dr_export2["Particulars"] = "";
                            dr_export2["Ledger"] = "";
                            dr_export2["Voucher Type"] = "";
                            dr_export2["Debit"] = "";
                            dr_export2["Credit"] = "";
                            dt.Rows.Add(dr_export2);

                            DataRow dr_lastexport2 = dt.NewRow();
                            dr_lastexport2["Date"] = "Total";
                            dr_lastexport2["Debit"] = damt;
                            dr_lastexport2["Credit"] = camt;
                            dr_lastexport2["Balance"] = "";
                            dr_lastexport2["Ledger"] = "";
                            dr_lastexport2["Voucher Type"] = "";
                            dt.Rows.Add(dr_lastexport2);

                            DataRow dr_lastexport3 = dt.NewRow();
                            dr_lastexport3["Date"] = "";
                            dr_lastexport3["Debit"] = "";
                            dr_lastexport3["Credit"] = "";
                            dr_lastexport3["Balance"] = "";
                            dr_lastexport3["Ledger"] = "";
                            dr_lastexport3["Voucher Type"] = "";
                            dt.Rows.Add(dr_lastexport3);

                            DataRow dr_de = dt.NewRow();
                            dr_de["Date"] = "Balance";
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
                            dr_de["Ledger"] = "";
                            dr_de["Voucher Type"] = "";
                            dt.Rows.Add(dr_de);


                        }
                        #endregion
                    }
                    else
                    {
                        ds = bl.generateReportDSLedger(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, iOrder);

                        double debit = 0.00;
                        double credit = 0.00;
                        damt = 0.0;

                        camt = 0.0;
                        dDiffamt = 0.0;
                        cDiffamt = 0.0;



                        #region Export To Excel
                        if (ds.Tables[0].Rows.Count > 0)
                        {



                            DataRow dr_export1 = dt.NewRow();
                            dt.Rows.Add(dr_export1);

                            DataSet dtt = new DataSet();
                            dtt = bl.GetLedgerInfoForId(sDataSource, iLedgerID);

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow drd in dtt.Tables[0].Rows)
                                {
                                    DataRow dr_export213 = dt.NewRow();
                                    dr_export213["Date"] = "";
                                    dr_export213["Particulars"] = "Ledger Name : " + drd["LedgerName"].ToString();
                                    dr_export213["Ledger"] = "";
                                    dr_export213["Voucher Type"] = "";
                                    dr_export213["Debit"] = "";
                                    dr_export213["Credit"] = "";
                                    dt.Rows.Add(dr_export213);
                                }
                            }

                            DataRow dr_export123 = dt.NewRow();
                            dt.Rows.Add(dr_export123);

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {

                                DataRow dr_export = dt.NewRow();
                                dr_export["Date"] = dr["Date"];
                                dr_export["Particulars"] = dr["Particulars"];
                                dr_export["Ledger"] = dr["ParticularsI"];
                                dr_export["Voucher Type"] = dr["VoucherType"];
                                dr_export["Debit"] = dr["Debit"];
                                dr_export["Credit"] = dr["Credit"];

                                debit = Convert.ToDouble(dr["Debit"]);
                                credit = Convert.ToDouble(dr["Credit"]);
                                damt = damt + debit;
                                camt = camt + credit;
                                dDiffamt = damt - camt;
                                cDiffamt = camt - damt;
                                if (dDiffamt >= 0)
                                {
                                    dDiffamt = dDiffamt + OpBalance;
                                    if (dDiffamt >= 0)
                                    {
                                        dr_export["Balance"] = dDiffamt + " Dr";
                                    }
                                    else
                                    {
                                        dr_export["balance"] = Math.Abs(dDiffamt) + " Cr";
                                    }
                                }
                                if (cDiffamt > 0)
                                {
                                    cDiffamt = cDiffamt - OpBalance;
                                    if (cDiffamt > 0)
                                    {
                                        dr_export["Balance"] = cDiffamt + " Cr";
                                    }
                                    else
                                    {
                                        dr_export["Balance"] = Math.Abs(cDiffamt) + " Dr";
                                    }
                                }

                                dt.Rows.Add(dr_export);
                            }

                            DataRow dr_lastexport2 = dt.NewRow();
                            dr_lastexport2["Date"] = "Total";
                            dr_lastexport2["Debit"] = damt;
                            dr_lastexport2["Credit"] = camt;
                            dr_lastexport2["Balance"] = "";
                            dr_lastexport2["Ledger"] = "";
                            dr_lastexport2["Voucher Type"] = "";
                            dt.Rows.Add(dr_lastexport2);

                            DataRow dr_de = dt.NewRow();
                            dr_de["Date"] = "Balance";
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
                            dr_de["Ledger"] = "";
                            dr_de["Voucher Type"] = "";
                            dt.Rows.Add(dr_de);



                            DataRow drl = dt.NewRow();
                            drl["Date"] = "";
                            drl["Debit"] = "";
                            drl["Credit"] = "";
                            drl["Balance"] = "";
                            drl["Ledger"] = "";
                            drl["Voucher Type"] = "";
                            dt.Rows.Add(drl);
                        }
                        #endregion
                    }

                }
            }
            ExportToExcel("Ledger Report.xls", dt);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(string filename, DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
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

    //protected void drpLedgerName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int iLedgerID = 0;
    //    iLedgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
    //    chkSP.Checked = false;
    //    chkSPR.Checked = false;
    //    if (iLedgerID == 2)
    //    {
    //        dvSalesPurchase.Visible = true;
    //        lblSP.Text = "Show Sales ";
    //        lblSPR.Text = "Show Purchase Return ";
    //    }
    //    else if (iLedgerID == 3)
    //    {
    //        dvSalesPurchase.Visible = true;
    //        lblSP.Text = "Show Purchase ";
    //        lblSPR.Text = "Show Sales Return ";
    //    }
    //    else
    //    {
    //        dvSalesPurchase.Visible = false;
    //    }
    //}
}
