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

public partial class LedgerReport : System.Web.UI.Page
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

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtEndDate.Text = dtaa;
                //txtEndDate.Text = DateTime.Now.ToShortDateString();
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
            LedgerPanel.Visible = false;
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
        drpLedgerName.Items.Clear();
        drpLedgerName.Items.Add(new ListItem(" --  All --", "0"));
        drpLedgerName.DataSource = ds;
        drpLedgerName.DataBind();

        drpLedgerName.DataTextField = "LedgerName";
        drpLedgerName.DataValueField = "LedgerID";

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate, endDate;
            int iLedgerID = 0;
            string sLedgerName = string.Empty;
            int iOrder = Convert.ToInt32(drpOrder.SelectedItem.Value);
            int iGroupID = 0;
            int iAccHeading = 0;

            ReportsBL.ReportClass rptLedgerAccount;

            LedgerPanel.Visible = false;
            iLedgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
            iGroupID = Convert.ToInt32(drpGroup.SelectedItem.Value);
            iAccHeading = Convert.ToInt32(drpHeading.SelectedItem.Value);

            sLedgerName = drpLedgerName.SelectedItem.Text;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            lblStartDate.Text = txtStartDate.Text;
            lblEndDate.Text = txtEndDate.Text;
            lblLedger.Text = drpLedgerName.SelectedItem.Text;
            rptLedgerAccount = new ReportsBL.ReportClass();
            DataSet ds;
            //rptLedgerAccount.generateReportXML(iLedgerID, startDate, endDate, sXmlNodeName, sDataSource, sXmlPath);
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

            bool summary = chkSummary.Checked;

            /*
             * chkSPR -(Purchase Return or Sales Return)
             * chkSP - (Normal Purchase or Sales)
             */
            if (!chkSPR.Checked && chkSP.Checked)  /* Only (Sales or Purchase) based on sType which is "Sales or Purchae" */
            {
                retFlag = "No";
                //ds = rptLedgerAccount.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);
                //gvLedger.DataSource = ds;
                //gvLedger.DataBind();

                //if (chkSummary.Checked)
                //{
                //    divSummary.Visible = true;
                //    divDetails.Visible = false;
                //    gvSummary.DataSource = ReturnSummary(ds);
                //    gvSummary.DataBind();
                //}
                //else
                //{
                //    divSummary.Visible = false;
                //    divDetails.Visible = true;
                //    gvLedger.DataSource = ds;
                //    gvLedger.DataBind();
                //}
                ////CalculateDebitCreditSalesPurchase(sType);
                //CalculateDebitCredit();

                Response.Write("<script language='javascript'> window.open('LedgerReport1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + "&iGroupID=" + iGroupID + "&iAccHeading=" + iAccHeading + "&iLedgerID=" + iLedgerID + "&sType=" + sType + "&retFlag=" + retFlag + "&iOrder=" + iOrder + "&sLedgerName=" + sLedgerName + "&summary=" + summary + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            }
            else if (chkSPR.Checked && chkSP.Checked) /* Both *****/
            {
                retFlag = "No";
                //ds = rptLedgerAccount.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

                //if (chkSummary.Checked)
                //{
                //    divSummary.Visible = true;
                //    divDetails.Visible = false;
                //    gvSummary.DataSource = ReturnSummary(ds);
                //    gvSummary.DataBind();
                //}
                //else
                //{
                //    divSummary.Visible = false;
                //    divDetails.Visible = true;
                //    gvLedger.DataSource = ds;
                //    gvLedger.DataBind();
                //}

                ////CalculateDebitCredit();
                //if (sType == "Sales")
                //    CalculateDebitCreditSalesPurchaseReturn(sType, "Sales Return");
                //else
                //    CalculateDebitCreditSalesPurchaseReturn(sType, "Purchase Return");

                Response.Write("<script language='javascript'> window.open('LedgerReport1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + "&iGroupID=" + iGroupID + "&iAccHeading=" + iAccHeading + "&iLedgerID=" + iLedgerID + "&sType=" + sType + "&retFlag=" + retFlag + "&iOrder=" + iOrder + "&sLedgerName=" + sLedgerName + "&summary=" + summary + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");

            }
            else if (chkSPR.Checked && !chkSP.Checked) /* ******Only Sales Return or Purchase Return */
            {
                retFlag = "Yes";
                //ds = rptLedgerAccount.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

                //if (chkSummary.Checked)
                //{
                //    divSummary.Visible = true;
                //    divDetails.Visible = false;
                //    gvSummary.DataSource = ReturnSummary(ds);
                //    gvSummary.DataBind();
                //}
                //else
                //{
                //    divSummary.Visible = false;
                //    divDetails.Visible = true;
                //    gvLedger.DataSource = ds;
                //    gvLedger.DataBind();
                //}

                //CalculateDebitCreditReturn(sType);
                //CalculateDebitCredit();

                Response.Write("<script language='javascript'> window.open('LedgerReport1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + "&iGroupID=" + iGroupID + "&iAccHeading=" + iAccHeading + "&iLedgerID=" + iLedgerID + "&sType=" + sType + "&retFlag=" + retFlag + "&iOrder=" + iOrder + "&sLedgerName=" + sLedgerName + "&summary=" + summary + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            }
            else if ((!chkSPR.Checked && !chkSP.Checked) && (sType == "Sales" || sType == "Purchase")) /* Both */
            {
                retFlag = "Both";
                //ds = rptLedgerAccount.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

                //if (chkSummary.Checked)
                //{
                //    divSummary.Visible = true;
                //    divDetails.Visible = false;
                //    gvSummary.DataSource = ReturnSummary(ds);
                //    gvSummary.DataBind();
                //}
                //else
                //{
                //    divSummary.Visible = false;
                //    divDetails.Visible = true;
                //    gvLedger.DataSource = ds;
                //    gvLedger.DataBind();
                //}

                //CalculateDebitCredit();

                Response.Write("<script language='javascript'> window.open('LedgerReport1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + "&iGroupID=" + iGroupID + "&iAccHeading=" + iAccHeading + "&iLedgerID=" + iLedgerID + "&sType=" + sType + "&retFlag=" + retFlag + "&iOrder=" + iOrder + "&sLedgerName=" + sLedgerName + "&summary=" + summary + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            }
            else
            {
                //ds = rptLedgerAccount.generateReportDSLedger(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, iOrder);

                //if (chkSummary.Checked)
                //{
                //    divSummary.Visible = true;
                //    divDetails.Visible = false;
                //    gvSummary.DataSource = ReturnSummary(ds);
                //    gvSummary.DataBind();
                //}
                //else
                //{
                //    divSummary.Visible = false;
                //    divDetails.Visible = true;
                //    gvLedger.DataSource = ds;
                //    gvLedger.DataBind();
                //}
                //CalculateDebitCredit();

                Response.Write("<script language='javascript'> window.open('LedgerReport1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + "&iGroupID=" + iGroupID + "&iAccHeading=" + iAccHeading + "&iLedgerID=" + iLedgerID + "&sType=" + sType + "&retFlag=" + retFlag + "&iOrder=" + iOrder + "&sLedgerName=" + sLedgerName + "&summary=" + summary + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            }


            div1.Visible = true;
            // CalculateDebitCredit();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

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
        ledgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);

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
        ledgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);

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
        ledgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
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
        ledgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);

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

                iLedgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
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

                ledgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);

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

                iLedgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
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

                ledgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);

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
            int iOrder = Convert.ToInt32(drpOrder.SelectedItem.Value);
            int iGroupID = 0;
            int iAccHeading = 0;

            BusinessLogic bl = new BusinessLogic();

            ReportsBL.ReportClass rptLedgerAccount;

            LedgerPanel.Visible = true;
            iLedgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
            iGroupID = Convert.ToInt32(drpGroup.SelectedItem.Value);
            iAccHeading = Convert.ToInt32(drpHeading.SelectedItem.Value);

            sLedgerName = drpLedgerName.SelectedItem.Text;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            lblStartDate.Text = txtStartDate.Text;
            lblEndDate.Text = txtEndDate.Text;
            lblLedger.Text = drpLedgerName.SelectedItem.Text;
            rptLedgerAccount = new ReportsBL.ReportClass();
            DataSet ds;
            //rptLedgerAccount.generateReportXML(iLedgerID, startDate, endDate, sXmlNodeName, sDataSource, sXmlPath);
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
            /*
             * chkSPR -(Purchase Return or Sales Return)
             * chkSP - (Normal Purchase or Sales)
             */
            if (!chkSPR.Checked && chkSP.Checked)  /* Only (Sales or Purchase) based on sType which is "Sales or Purchae" */
            {
                retFlag = "No";
                ds = bl.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);
                gvLedger.DataSource = ds;
                gvLedger.DataBind();

                if (chkSummary.Checked)
                {
                    divSummary.Visible = true;
                    divDetails.Visible = false;
                    gvSummary.DataSource = ReturnSummary(ds);
                    gvSummary.DataBind();
                }
                else
                {
                    divSummary.Visible = false;
                    divDetails.Visible = true;
                    gvLedger.DataSource = ds;
                    gvLedger.DataBind();
                }
                CalculateDebitCredit();
                double debit = 0.00;
                double credit = 0.00;
                double debitclo = 0;

                double creditclo = 0;
                //ExportToExcel();

                #region Export To Excel
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Date"));
                    dt.Columns.Add(new DataColumn("TransNo"));
                    dt.Columns.Add(new DataColumn("Particulars"));
                    dt.Columns.Add(new DataColumn("Ledger Name"));
                    dt.Columns.Add(new DataColumn("Ledger"));
                    dt.Columns.Add(new DataColumn("Voucher Type"));
                    dt.Columns.Add(new DataColumn("Debit"));
                    dt.Columns.Add(new DataColumn("Credit"));
                    dt.Columns.Add(new DataColumn("Balance"));

                    DataRow dr_export1 = dt.NewRow();
                    dt.Rows.Add(dr_export1);

                    DataSet dtt = new DataSet();

                    DataRow dr_export213 = dt.NewRow();
                    dr_export213["Date"] = "";
                    dr_export213["Particulars"] = "Ledger Name : " + drpLedgerName.SelectedItem.Text;
                    dr_export213["Ledger"] = "";
                    dr_export213["Voucher Type"] = "";
                    dr_export213["Debit"] = "";
                    dr_export213["Credit"] = "";
                    dt.Rows.Add(dr_export213);

                    DataRow dr_export123 = dt.NewRow();
                    dt.Rows.Add(dr_export123);

                    DataRow dr_export21312 = dt.NewRow();
                    dr_export21312["Date"] = "";
                    dr_export21312["Particulars"] = " Date From " + Convert.ToDateTime(startDate).ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(endDate).ToString("dd/MM/yyyy");
                    dr_export21312["Ledger"] = "";
                    dr_export21312["Voucher Type"] = "";
                    dr_export21312["Debit"] = "";
                    dr_export21312["Credit"] = "";
                    dt.Rows.Add(dr_export21312);

                    DataRow dr_export1231 = dt.NewRow();
                    dt.Rows.Add(dr_export1231);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["Date"] = dr["Date"];
                        dr_export["TransNo"] = dr["TransNo"];
                        dr_export["Particulars"] = dr["Particulars"];
                        dr_export["Ledger Name"] = dr["Ledger"];
                        dr_export["Ledger"] = dr["ParticularsI"];
                        dr_export["Voucher Type"] = dr["VoucherType"];
                        dr_export["Debit"] = dr["Debit"];
                        dr_export["Credit"] = dr["Credit"];

                        debit = debit + Convert.ToDouble(dr["Debit"]);
                        credit = credit + Convert.ToDouble(dr["Credit"]);
                        damt = damt + debit;
                        camt = camt + credit;
                        dDiffamt = debit - credit;
                        cDiffamt = credit - debit;
                        debitclo = debit - credit;
                        creditclo = credit - debit;

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

                    DataRow dr_export231 = dt.NewRow();
                    dr_export231["Date"] = "";
                    dr_export231["Particulars"] = "";
                    dr_export231["Ledger"] = "";
                    dr_export231["Voucher Type"] = "";
                    dr_export231["Debit"] = "";
                    dr_export231["Credit"] = "";
                    dt.Rows.Add(dr_export231);

                    DataRow dr_lastexport21 = dt.NewRow();
                    dr_lastexport21["Date"] = "Opening Balance : ";
                    dr_lastexport21["Debit"] = lblOBDR.Text;

                    dr_lastexport21["Credit"] = lblOBCR.Text;
                    dr_lastexport21["Balance"] = "";
                    dr_lastexport21["Ledger"] = "";
                    dr_lastexport21["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport21);

                    DataRow dr_export2 = dt.NewRow();
                    dr_export2["Date"] = "";
                    dr_export2["Particulars"] = "";
                    dr_export2["Ledger"] = "";
                    dr_export2["Voucher Type"] = "";
                    dr_export2["Debit"] = "";
                    dr_export2["Credit"] = "";
                    dt.Rows.Add(dr_export2);

                    DataRow dr_lastexport2 = dt.NewRow();
                    dr_lastexport2["Date"] = "Total : ";
                    dr_lastexport2["Debit"] = debit;
                    dr_lastexport2["Credit"] = credit;
                    dr_lastexport2["Balance"] = "";
                    dr_lastexport2["Ledger"] = "";
                    dr_lastexport2["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport2);

                    DataRow dr_lastexport322 = dt.NewRow();
                    dr_lastexport322["Date"] = "";
                    dr_lastexport322["Debit"] = "";
                    dr_lastexport322["Credit"] = "";
                    dr_lastexport322["Balance"] = "";
                    dr_lastexport322["Ledger"] = "";
                    dr_lastexport322["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport322);

                    DataRow dr_de1 = dt.NewRow();
                    dr_de1["Date"] = "Current Balance : ";
                    if (debitclo >= 0)
                    {
                        dr_de1["Debit"] = debitclo;
                        dr_de1["Credit"] = 0;
                    }
                    else if (creditclo >= 0)
                    {
                        dr_de1["Debit"] = 0;
                        dr_de1["Credit"] = creditclo;
                    }
                    dr_de1["Balance"] = "";
                    dr_de1["Ledger"] = "";
                    dr_de1["Voucher Type"] = "";
                    dt.Rows.Add(dr_de1);

                    DataRow dr_lastexport3 = dt.NewRow();
                    dr_lastexport3["Date"] = "";
                    dr_lastexport3["Debit"] = "";
                    dr_lastexport3["Credit"] = "";
                    dr_lastexport3["Balance"] = "";
                    dr_lastexport3["Ledger"] = "";
                    dr_lastexport3["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport3);

                    DataRow dr_de = dt.NewRow();
                    dr_de["Date"] = "Closing Balance : ";
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
                    else if (cDiffamt < 0)
                    {
                        dr_de["Debit"] = Math.Abs(cDiffamt);
                        dr_de["Credit"] = 0;
                    }
                    else if (dDiffamt < 0)
                    {
                        dr_de["Debit"] = 0;
                        dr_de["Credit"] = Math.Abs(dDiffamt);
                    }
                    dr_de["Balance"] = "";
                    dr_de["Ledger"] = "";
                    dr_de["Voucher Type"] = "";
                    dt.Rows.Add(dr_de);

                    ExportToExcel(dt);
                }
                #endregion
            }
            else if (chkSPR.Checked && chkSP.Checked) /* Both *****/
            {
                retFlag = "No";
                ds = bl.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

                if (chkSummary.Checked)
                {
                    divSummary.Visible = true;
                    divDetails.Visible = false;
                    gvSummary.DataSource = ReturnSummary(ds);
                    gvSummary.DataBind();
                }
                else
                {
                    divSummary.Visible = false;
                    divDetails.Visible = true;
                    gvLedger.DataSource = ds;
                    gvLedger.DataBind();
                }

                if (sType == "Sales")
                    CalculateDebitCreditSalesPurchaseReturn(sType, "Sales Return");
                else
                    CalculateDebitCreditSalesPurchaseReturn(sType, "Purchase Return");
                double debit = 0.00;
                double credit = 0.00;

                double debitclo = 0;

                double creditclo = 0;
                //ExportToExcel();

                #region Export To Excel
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Date"));
                    dt.Columns.Add(new DataColumn("TransNo"));
                    dt.Columns.Add(new DataColumn("Particulars"));
                    dt.Columns.Add(new DataColumn("Ledger Name"));
                    dt.Columns.Add(new DataColumn("Ledger"));
                    dt.Columns.Add(new DataColumn("Voucher Type"));
                    dt.Columns.Add(new DataColumn("Debit"));
                    dt.Columns.Add(new DataColumn("Credit"));
                    dt.Columns.Add(new DataColumn("Balance"));

                    DataRow dr_export1 = dt.NewRow();
                    dt.Rows.Add(dr_export1);

                    DataSet dtt = new DataSet();

                    DataRow dr_export213 = dt.NewRow();
                    dr_export213["Date"] = "";
                    dr_export213["Particulars"] = "Ledger Name : " + drpLedgerName.SelectedItem.Text;
                    dr_export213["Ledger"] = "";
                    dr_export213["Voucher Type"] = "";
                    dr_export213["Debit"] = "";
                    dr_export213["Credit"] = "";
                    dt.Rows.Add(dr_export213);

                    DataRow dr_export123 = dt.NewRow();
                    dt.Rows.Add(dr_export123);

                    DataRow dr_export21312 = dt.NewRow();
                    dr_export21312["Date"] = "";
                    dr_export21312["Particulars"] = " Date From " + Convert.ToDateTime(startDate).ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(endDate).ToString("dd/MM/yyyy");
                    dr_export21312["Ledger"] = "";
                    dr_export21312["Voucher Type"] = "";
                    dr_export21312["Debit"] = "";
                    dr_export21312["Credit"] = "";
                    dt.Rows.Add(dr_export21312);

                    DataRow dr_export1231 = dt.NewRow();
                    dt.Rows.Add(dr_export1231);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["Date"] = dr["Date"];
                        dr_export["TransNo"] = dr["TransNo"];
                        dr_export["Particulars"] = dr["Particulars"];
                        dr_export["Ledger Name"] = dr["Ledger"];
                        dr_export["Ledger"] = dr["ParticularsI"];
                        dr_export["Voucher Type"] = dr["VoucherType"];
                        dr_export["Debit"] = dr["Debit"];
                        dr_export["Credit"] = dr["Credit"];

                        debit = debit + Convert.ToDouble(dr["Debit"]);
                        credit = credit + Convert.ToDouble(dr["Credit"]);
                        damt = damt + debit;
                        camt = camt + credit;
                        dDiffamt = debit - credit;
                        cDiffamt = credit - debit;

                        debitclo = debit - credit;
                        creditclo = credit - debit;

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

                    DataRow dr_export231 = dt.NewRow();
                    dr_export231["Date"] = "";
                    dr_export231["Particulars"] = "";
                    dr_export231["Ledger"] = "";
                    dr_export231["Voucher Type"] = "";
                    dr_export231["Debit"] = "";
                    dr_export231["Credit"] = "";
                    dt.Rows.Add(dr_export231);

                    DataRow dr_lastexport21 = dt.NewRow();
                    dr_lastexport21["Date"] = "Opening Balance : ";
                    dr_lastexport21["Debit"] = lblOBDR.Text;

                    dr_lastexport21["Credit"] = lblOBCR.Text;
                    dr_lastexport21["Balance"] = "";
                    dr_lastexport21["Ledger"] = "";
                    dr_lastexport21["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport21);

                    DataRow dr_export2 = dt.NewRow();
                    dr_export2["Date"] = "";
                    dr_export2["Particulars"] = "";
                    dr_export2["Ledger"] = "";
                    dr_export2["Voucher Type"] = "";
                    dr_export2["Debit"] = "";
                    dr_export2["Credit"] = "";
                    dt.Rows.Add(dr_export2);

                    DataRow dr_lastexport2 = dt.NewRow();
                    dr_lastexport2["Date"] = "Total : ";
                    dr_lastexport2["Debit"] = debit;
                    dr_lastexport2["Credit"] = credit;
                    dr_lastexport2["Balance"] = "";
                    dr_lastexport2["Ledger"] = "";
                    dr_lastexport2["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport2);

                    DataRow dr_lastexport322 = dt.NewRow();
                    dr_lastexport322["Date"] = "";
                    dr_lastexport322["Debit"] = "";
                    dr_lastexport322["Credit"] = "";
                    dr_lastexport322["Balance"] = "";
                    dr_lastexport322["Ledger"] = "";
                    dr_lastexport322["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport322);

                    DataRow dr_de1 = dt.NewRow();
                    dr_de1["Date"] = "Current Balance : ";
                    if (debitclo >= 0)
                    {
                        dr_de1["Debit"] = debitclo;
                        dr_de1["Credit"] = 0;
                    }
                    else if (creditclo >= 0)
                    {
                        dr_de1["Debit"] = 0;
                        dr_de1["Credit"] = creditclo;
                    }
                    dr_de1["Balance"] = "";
                    dr_de1["Ledger"] = "";
                    dr_de1["Voucher Type"] = "";
                    dt.Rows.Add(dr_de1);

                    DataRow dr_lastexport3 = dt.NewRow();
                    dr_lastexport3["Date"] = "";
                    dr_lastexport3["Debit"] = "";
                    dr_lastexport3["Credit"] = "";
                    dr_lastexport3["Balance"] = "";
                    dr_lastexport3["Ledger"] = "";
                    dr_lastexport3["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport3);

                    DataRow dr_de = dt.NewRow();
                    dr_de["Date"] = "Closing Balance : ";
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
                    else if (cDiffamt < 0)
                    {
                        dr_de["Debit"] = Math.Abs(cDiffamt);
                        dr_de["Credit"] = 0;
                    }
                    else if (dDiffamt < 0)
                    {
                        dr_de["Debit"] = 0;
                        dr_de["Credit"] = Math.Abs(dDiffamt);
                    }
                    dr_de["Balance"] = "";
                    dr_de["Ledger"] = "";
                    dr_de["Voucher Type"] = "";
                    dt.Rows.Add(dr_de);

                    ExportToExcel(dt);
                }
                #endregion
            }
            else if (chkSPR.Checked && !chkSP.Checked) /* ******Only Sales Return or Purchase Return */
            {
                retFlag = "Yes";
                ds = bl.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

                if (chkSummary.Checked)
                {
                    divSummary.Visible = true;
                    divDetails.Visible = false;
                    gvSummary.DataSource = ReturnSummary(ds);
                    gvSummary.DataBind();
                }
                else
                {
                    divSummary.Visible = false;
                    divDetails.Visible = true;
                    gvLedger.DataSource = ds;
                    gvLedger.DataBind();
                }

                CalculateDebitCreditReturn(sType);

                double debit = 0.00;
                double credit = 0.00;
                double debitclo = 0;

                double creditclo = 0;
                //ExportToExcel();
                #region Export To Excel
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Date"));
                    dt.Columns.Add(new DataColumn("TransNo"));
                    dt.Columns.Add(new DataColumn("Particulars"));
                    dt.Columns.Add(new DataColumn("Ledger Name"));
                    dt.Columns.Add(new DataColumn("Ledger"));
                    dt.Columns.Add(new DataColumn("Voucher Type"));
                    dt.Columns.Add(new DataColumn("Debit"));
                    dt.Columns.Add(new DataColumn("Credit"));
                    dt.Columns.Add(new DataColumn("Balance"));

                    DataRow dr_export1 = dt.NewRow();
                    dt.Rows.Add(dr_export1);

                    DataSet dtt = new DataSet();

                    DataRow dr_export213 = dt.NewRow();
                    dr_export213["Date"] = "";
                    dr_export213["Particulars"] = "Ledger Name : " + drpLedgerName.SelectedItem.Text;
                    dr_export213["Ledger"] = "";
                    dr_export213["Voucher Type"] = "";
                    dr_export213["Debit"] = "";
                    dr_export213["Credit"] = "";
                    dt.Rows.Add(dr_export213);

                    DataRow dr_export123 = dt.NewRow();
                    dt.Rows.Add(dr_export123);

                    DataRow dr_export21312 = dt.NewRow();
                    dr_export21312["Date"] = "";
                    dr_export21312["Particulars"] = " Date From " + Convert.ToDateTime(startDate).ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(endDate).ToString("dd/MM/yyyy");
                    dr_export21312["Ledger"] = "";
                    dr_export21312["Voucher Type"] = "";
                    dr_export21312["Debit"] = "";
                    dr_export21312["Credit"] = "";
                    dt.Rows.Add(dr_export21312);

                    DataRow dr_export1231 = dt.NewRow();
                    dt.Rows.Add(dr_export1231);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["Date"] = dr["Date"];
                        dr_export["TransNo"] = dr["TransNo"];
                        dr_export["Particulars"] = dr["Particulars"];
                        dr_export["Ledger Name"] = dr["Ledger"];
                        dr_export["Ledger"] = dr["ParticularsI"];
                        dr_export["Voucher Type"] = dr["VoucherType"];
                        dr_export["Debit"] = dr["Debit"];
                        dr_export["Credit"] = dr["Credit"];

                        debit = debit + Convert.ToDouble(dr["Debit"]);
                        credit = credit + Convert.ToDouble(dr["Credit"]);
                        damt = damt + debit;
                        camt = camt + credit;
                        dDiffamt = debit - credit;
                        cDiffamt = credit - debit;

                        debitclo = debit - credit;
                        creditclo = credit - debit;

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

                    DataRow dr_export231 = dt.NewRow();
                    dr_export231["Date"] = "";
                    dr_export231["Particulars"] = "";
                    dr_export231["Ledger"] = "";
                    dr_export231["Voucher Type"] = "";
                    dr_export231["Debit"] = "";
                    dr_export231["Credit"] = "";
                    dt.Rows.Add(dr_export231);

                    DataRow dr_lastexport21 = dt.NewRow();
                    dr_lastexport21["Date"] = "Opening Balance : ";
                    dr_lastexport21["Debit"] = lblOBDR.Text;

                    dr_lastexport21["Credit"] = lblOBCR.Text;
                    dr_lastexport21["Balance"] = "";
                    dr_lastexport21["Ledger"] = "";
                    dr_lastexport21["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport21);

                    DataRow dr_export2 = dt.NewRow();
                    dr_export2["Date"] = "";
                    dr_export2["Particulars"] = "";
                    dr_export2["Ledger"] = "";
                    dr_export2["Voucher Type"] = "";
                    dr_export2["Debit"] = "";
                    dr_export2["Credit"] = "";
                    dt.Rows.Add(dr_export2);

                    DataRow dr_lastexport2 = dt.NewRow();
                    dr_lastexport2["Date"] = "Total : ";
                    dr_lastexport2["Debit"] = debit;
                    dr_lastexport2["Credit"] = credit;
                    dr_lastexport2["Balance"] = "";
                    dr_lastexport2["Ledger"] = "";
                    dr_lastexport2["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport2);

                    DataRow dr_lastexport322 = dt.NewRow();
                    dr_lastexport322["Date"] = "";
                    dr_lastexport322["Debit"] = "";
                    dr_lastexport322["Credit"] = "";
                    dr_lastexport322["Balance"] = "";
                    dr_lastexport322["Ledger"] = "";
                    dr_lastexport322["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport322);

                    DataRow dr_de1 = dt.NewRow();
                    dr_de1["Date"] = "Current Balance : ";
                    if (debitclo >= 0)
                    {
                        dr_de1["Debit"] = debitclo;
                        dr_de1["Credit"] = 0;
                    }
                    else if (creditclo >= 0)
                    {
                        dr_de1["Debit"] = 0;
                        dr_de1["Credit"] = creditclo;
                    }
                    dr_de1["Balance"] = "";
                    dr_de1["Ledger"] = "";
                    dr_de1["Voucher Type"] = "";
                    dt.Rows.Add(dr_de1);

                    DataRow dr_lastexport3 = dt.NewRow();
                    dr_lastexport3["Date"] = "";
                    dr_lastexport3["Debit"] = "";
                    dr_lastexport3["Credit"] = "";
                    dr_lastexport3["Balance"] = "";
                    dr_lastexport3["Ledger"] = "";
                    dr_lastexport3["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport3);

                    DataRow dr_de = dt.NewRow();
                    dr_de["Date"] = "Closing Balance : ";
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
                    else if (cDiffamt < 0)
                    {
                        dr_de["Debit"] = Math.Abs(cDiffamt);
                        dr_de["Credit"] = 0;
                    }
                    else if (dDiffamt < 0)
                    {
                        dr_de["Debit"] = 0;
                        dr_de["Credit"] = Math.Abs(dDiffamt);
                    }
                    dr_de["Balance"] = "";
                    dr_de["Ledger"] = "";
                    dr_de["Voucher Type"] = "";
                    dt.Rows.Add(dr_de);

                    ExportToExcel(dt);
                }
                #endregion
            }
            else if ((!chkSPR.Checked && !chkSP.Checked) && (sType == "Sales" || sType == "Purchase")) /* Both */
            {
                retFlag = "Both";
                ds = bl.generateReportDS(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, sType, retFlag, iOrder);

                if (chkSummary.Checked)
                {
                    divSummary.Visible = true;
                    divDetails.Visible = false;
                    gvSummary.DataSource = ReturnSummary(ds);
                    gvSummary.DataBind();
                }
                else
                {
                    divSummary.Visible = false;
                    divDetails.Visible = true;
                    gvLedger.DataSource = ds;
                    gvLedger.DataBind();
                }

                CalculateDebitCredit();

                double debit = 0.00;
                double credit = 0.00;
                double debitclo = 0;

                double creditclo = 0;
                //ExportToExcel();

                #region Export To Excel
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Date"));
                    dt.Columns.Add(new DataColumn("TransNo"));
                    dt.Columns.Add(new DataColumn("Particulars"));
                    dt.Columns.Add(new DataColumn("Ledger Name"));
                    dt.Columns.Add(new DataColumn("Ledger"));
                    dt.Columns.Add(new DataColumn("Voucher Type"));
                    dt.Columns.Add(new DataColumn("Debit"));
                    dt.Columns.Add(new DataColumn("Credit"));
                    dt.Columns.Add(new DataColumn("Balance"));

                    DataRow dr_export1 = dt.NewRow();
                    dt.Rows.Add(dr_export1);

                    DataSet dtt = new DataSet();

                    DataRow dr_export213 = dt.NewRow();
                    dr_export213["Date"] = "";
                    dr_export213["Particulars"] = "Ledger Name : " + drpLedgerName.SelectedItem.Text;
                    dr_export213["Ledger"] = "";
                    dr_export213["Voucher Type"] = "";
                    dr_export213["Debit"] = "";
                    dr_export213["Credit"] = "";
                    dt.Rows.Add(dr_export213);

                    DataRow dr_export123 = dt.NewRow();
                    dt.Rows.Add(dr_export123);

                    DataRow dr_export21312 = dt.NewRow();
                    dr_export21312["Date"] = "";
                    dr_export21312["Particulars"] = " Date From " + Convert.ToDateTime(startDate).ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(endDate).ToString("dd/MM/yyyy");
                    dr_export21312["Ledger"] = "";
                    dr_export21312["Voucher Type"] = "";
                    dr_export21312["Debit"] = "";
                    dr_export21312["Credit"] = "";
                    dt.Rows.Add(dr_export21312);

                    DataRow dr_export1231 = dt.NewRow();
                    dt.Rows.Add(dr_export1231);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["Date"] = dr["Date"];
                        dr_export["TransNo"] = dr["TransNo"];
                        dr_export["Particulars"] = dr["Particulars"];
                        dr_export["Ledger Name"] = dr["Ledger"];
                        dr_export["Ledger"] = dr["ParticularsI"];
                        dr_export["Voucher Type"] = dr["VoucherType"];
                        dr_export["Debit"] = dr["Debit"];
                        dr_export["Credit"] = dr["Credit"];

                        debit = debit + Convert.ToDouble(dr["Debit"]);
                        credit = credit + Convert.ToDouble(dr["Credit"]);
                        damt = damt + debit;
                        camt = camt + credit;
                        dDiffamt = debit - credit;
                        cDiffamt = credit - debit;

                        debitclo = debit - credit;
                        creditclo = credit - debit;

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

                    DataRow dr_export231 = dt.NewRow();
                    dr_export231["Date"] = "";
                    dr_export231["Particulars"] = "";
                    dr_export231["Ledger"] = "";
                    dr_export231["Voucher Type"] = "";
                    dr_export231["Debit"] = "";
                    dr_export231["Credit"] = "";
                    dt.Rows.Add(dr_export231);

                    DataRow dr_lastexport21 = dt.NewRow();
                    dr_lastexport21["Date"] = "Opening Balance : ";
                    dr_lastexport21["Debit"] = lblOBDR.Text;

                    dr_lastexport21["Credit"] = lblOBCR.Text;
                    dr_lastexport21["Balance"] = "";
                    dr_lastexport21["Ledger"] = "";
                    dr_lastexport21["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport21);

                    DataRow dr_export2 = dt.NewRow();
                    dr_export2["Date"] = "";
                    dr_export2["Particulars"] = "";
                    dr_export2["Ledger"] = "";
                    dr_export2["Voucher Type"] = "";
                    dr_export2["Debit"] = "";
                    dr_export2["Credit"] = "";
                    dt.Rows.Add(dr_export2);

                    DataRow dr_lastexport2 = dt.NewRow();
                    dr_lastexport2["Date"] = "Total : ";
                    dr_lastexport2["Debit"] = debit;
                    dr_lastexport2["Credit"] = credit;
                    dr_lastexport2["Balance"] = "";
                    dr_lastexport2["Ledger"] = "";
                    dr_lastexport2["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport2);

                    DataRow dr_lastexport322 = dt.NewRow();
                    dr_lastexport322["Date"] = "";
                    dr_lastexport322["Debit"] = "";
                    dr_lastexport322["Credit"] = "";
                    dr_lastexport322["Balance"] = "";
                    dr_lastexport322["Ledger"] = "";
                    dr_lastexport322["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport322);

                    DataRow dr_de1 = dt.NewRow();
                    dr_de1["Date"] = "Current Balance : ";
                    if (debitclo >= 0)
                    {
                        dr_de1["Debit"] = debitclo;
                        dr_de1["Credit"] = 0;
                    }
                    else if (creditclo >= 0)
                    {
                        dr_de1["Debit"] = 0;
                        dr_de1["Credit"] = creditclo;
                    }
                    dr_de1["Balance"] = "";
                    dr_de1["Ledger"] = "";
                    dr_de1["Voucher Type"] = "";
                    dt.Rows.Add(dr_de1);

                    DataRow dr_lastexport3 = dt.NewRow();
                    dr_lastexport3["Date"] = "";
                    dr_lastexport3["Debit"] = "";
                    dr_lastexport3["Credit"] = "";
                    dr_lastexport3["Balance"] = "";
                    dr_lastexport3["Ledger"] = "";
                    dr_lastexport3["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport3);

                    DataRow dr_de = dt.NewRow();
                    dr_de["Date"] = "Closing Balance : ";
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
                    else if (cDiffamt < 0)
                    {
                        dr_de["Debit"] = Math.Abs(cDiffamt);
                        dr_de["Credit"] = 0;
                    }
                    else if (dDiffamt < 0)
                    {
                        dr_de["Debit"] = 0;
                        dr_de["Credit"] = Math.Abs(dDiffamt);
                    }
                    dr_de["Balance"] = "";
                    dr_de["Ledger"] = "";
                    dr_de["Voucher Type"] = "";
                    dt.Rows.Add(dr_de);

                    ExportToExcel(dt);
                }
                #endregion
            }
            else
            {
                ds = bl.generateReportDSLedger(iAccHeading, iGroupID, iLedgerID, startDate, endDate, sDataSource, iOrder);

                if (chkSummary.Checked)
                {
                    divSummary.Visible = true;
                    divDetails.Visible = false;
                    gvSummary.DataSource = ReturnSummary(ds);
                    gvSummary.DataBind();
                }
                else
                {
                    divSummary.Visible = false;
                    divDetails.Visible = true;
                    gvLedger.DataSource = ds;
                    gvLedger.DataBind();
                }
                CalculateDebitCredit();
                double debit = 0.00;
                double credit = 0.00;

                double debitclo = 0.00;

                double creditclo = 0.00;

                //ExportToExcel();
                #region Export To Excel
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Date"));
                    dt.Columns.Add(new DataColumn("TransNo"));
                    dt.Columns.Add(new DataColumn("Particulars"));
                    dt.Columns.Add(new DataColumn("Ledger Name"));
                    dt.Columns.Add(new DataColumn("Ledger"));
                    dt.Columns.Add(new DataColumn("Voucher Type"));
                    dt.Columns.Add(new DataColumn("Debit"));
                    dt.Columns.Add(new DataColumn("Credit"));
                    dt.Columns.Add(new DataColumn("Balance"));

                    DataRow dr_export1 = dt.NewRow();
                    dt.Rows.Add(dr_export1);

                    DataSet dtt = new DataSet();
                    //dtt = bl.GetLedgerInfoForId(sDataSource, iLedgerID);

                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    foreach (DataRow drd in dtt.Tables[0].Rows)
                    //    {
                    DataRow dr_export213 = dt.NewRow();
                    dr_export213["Date"] = "";
                    dr_export213["Particulars"] = "Ledger Name : " + drpLedgerName.SelectedItem.Text;
                    dr_export213["Ledger"] = "";
                    dr_export213["Voucher Type"] = "";
                    dr_export213["Debit"] = "";
                    dr_export213["Credit"] = "";
                    dt.Rows.Add(dr_export213);
                    //    }
                    //}

                    DataRow dr_export123 = dt.NewRow();
                    dt.Rows.Add(dr_export123);

                    DataRow dr_export21312 = dt.NewRow();
                    dr_export21312["Date"] = "";
                    dr_export21312["Particulars"] = " Date From " + Convert.ToDateTime(startDate).ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(endDate).ToString("dd/MM/yyyy");
                    dr_export21312["Ledger"] = "";
                    dr_export21312["Voucher Type"] = "";
                    dr_export21312["Debit"] = "";
                    dr_export21312["Credit"] = "";
                    dt.Rows.Add(dr_export21312);

                    DataRow dr_export1231 = dt.NewRow();
                    dt.Rows.Add(dr_export1231);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["Date"] = dr["Date"];
                        dr_export["Particulars"] = dr["Particulars"];
                        dr_export["Ledger Name"] = dr["Ledger"];
                        dr_export["TransNo"] = dr["TransNo"];
                        dr_export["Ledger"] = dr["ParticularsI"];
                        dr_export["Voucher Type"] = dr["VoucherType"];
                        dr_export["Debit"] = dr["Debit"];
                        dr_export["Credit"] = dr["Credit"];

                        debit = debit + Convert.ToDouble(dr["Debit"]);
                        credit = credit + Convert.ToDouble(dr["Credit"]);
                        damt = damt + debit;
                        camt = camt + credit;
                        dDiffamt = debit - credit;
                        cDiffamt = credit - debit;

                        debitclo = debit - credit;
                        creditclo = credit - debit;

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

                    DataRow dr_export231 = dt.NewRow();
                    dr_export231["Date"] = "";
                    dr_export231["Particulars"] = "";
                    dr_export231["Ledger"] = "";
                    dr_export231["Voucher Type"] = "";
                    dr_export231["Debit"] = "";
                    dr_export231["Credit"] = "";
                    dt.Rows.Add(dr_export231);

                    DataRow dr_lastexport21 = dt.NewRow();
                    dr_lastexport21["Date"] = "Opening Balance : ";
                    dr_lastexport21["Debit"] = lblOBDR.Text;

                    dr_lastexport21["Credit"] = lblOBCR.Text;
                    dr_lastexport21["Balance"] = "";
                    dr_lastexport21["Ledger"] = "";
                    dr_lastexport21["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport21);

                    DataRow dr_export2 = dt.NewRow();
                    dr_export2["Date"] = "";
                    dr_export2["Particulars"] = "";
                    dr_export2["Ledger"] = "";
                    dr_export2["Voucher Type"] = "";
                    dr_export2["Debit"] = "";
                    dr_export2["Credit"] = "";
                    dt.Rows.Add(dr_export2);

                    DataRow dr_lastexport2 = dt.NewRow();
                    dr_lastexport2["Date"] = "Total : ";
                    dr_lastexport2["Debit"] = debit;
                    dr_lastexport2["Credit"] = credit;
                    dr_lastexport2["Balance"] = "";
                    dr_lastexport2["Ledger"] = "";
                    dr_lastexport2["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport2);

                    DataRow dr_lastexport322 = dt.NewRow();
                    dr_lastexport322["Date"] = "";
                    dr_lastexport322["Debit"] = "";
                    dr_lastexport322["Credit"] = "";
                    dr_lastexport322["Balance"] = "";
                    dr_lastexport322["Ledger"] = "";
                    dr_lastexport322["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport322);

                    DataRow dr_de1 = dt.NewRow();
                    dr_de1["Date"] = "Current Balance : ";
                    if (debitclo >= 0)
                    {
                        dr_de1["Debit"] = debitclo;
                        dr_de1["Credit"] = 0;
                    }
                    else if (creditclo >= 0)
                    {
                        dr_de1["Debit"] = 0;
                        dr_de1["Credit"] = creditclo;
                    }
                    dr_de1["Balance"] = "";
                    dr_de1["Ledger"] = "";
                    dr_de1["Voucher Type"] = "";
                    dt.Rows.Add(dr_de1);

                    DataRow dr_lastexport3 = dt.NewRow();
                    dr_lastexport3["Date"] = "";
                    dr_lastexport3["Debit"] = "";
                    dr_lastexport3["Credit"] = "";
                    dr_lastexport3["Balance"] = "";
                    dr_lastexport3["Ledger"] = "";
                    dr_lastexport3["Voucher Type"] = "";
                    dt.Rows.Add(dr_lastexport3);

                    DataRow dr_de = dt.NewRow();
                    dr_de["Date"] = "Closing Balance : ";
                    if ((dDiffamt >= 0) || (dDiffamt < 0))
                    {
                        if (dDiffamt >= 0)
                        {
                            dr_de["Debit"] = dDiffamt;
                            dr_de["Credit"] = 0;
                        }
                        if (dDiffamt < 0)
                        {
                            dr_de["Debit"] = 0;
                            dr_de["Credit"] = Math.Abs(dDiffamt);
                        }
                    }
                    else if ((cDiffamt >= 0)||(cDiffamt < 0))
                    {
                        if (cDiffamt >= 0)
                        {
                            dr_de["Debit"] = 0;
                            dr_de["Credit"] = cDiffamt;
                        }
                        if(cDiffamt < 0)
                        {
                            dr_de["Debit"] = Math.Abs(cDiffamt);
                            dr_de["Credit"] = 0;
                        }
                    }
                    
                    dr_de["Balance"] = "";
                    dr_de["Ledger"] = "";
                    dr_de["Voucher Type"] = "";
                    dt.Rows.Add(dr_de);

                    ExportToExcel(dt);
                }
                #endregion
            }
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

            string file= "Ledger Report_" + DateTime.Now.ToString() + ".xls";

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

            cell2.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;        Ledger Of " + lblLedger.Text;

            cell2.BackColor = System.Drawing.Color.LightSkyBlue;

            TableCell cell3 = new TableCell();

            cell3.Text = "&nbsp;";

            TableCell cell4 = new TableCell();

            cell4.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     Date From " + lblStartDate.Text + " To " + lblEndDate.Text;

            cell4.BackColor = System.Drawing.Color.LightSkyBlue;

            TableCell cell5 = new TableCell();

            if (chkSummary.Checked)
            {
                cell5.Controls.Add(gvSummary);
            }
            else
            {
                cell5.Controls.Add(gvLedger);
            }

            TableCell cell6 = new TableCell();

            cell6.Text = "&nbsp;";

            TableCell cell7 = new TableCell();

            cell7.Text = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                       Opening Balance : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + Convert.ToDouble(lblOBDR.Text) + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;             " + Convert.ToDouble(lblOBCR.Text);

            cell7.BackColor = System.Drawing.Color.LightSkyBlue;


            TableCell cell8 = new TableCell();

            cell8.Text = "&nbsp;";

            TableCell cell9 = new TableCell();

            cell9.Text = "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                   Total   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;        : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Convert.ToDouble(lblDebitSum.Text) + "  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         " + Convert.ToDouble(lblCreditSum.Text);

            cell9.BackColor = System.Drawing.Color.LightSkyBlue;

            TableCell cell10 = new TableCell();

            cell10.Text = "&nbsp;";

            TableCell cell11 = new TableCell();

            cell11.Text = "      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                 Current Balance : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Convert.ToDouble(lblDebitDiff.Text) + "  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         " + Convert.ToDouble(lblCreditDiff.Text);

            cell11.BackColor = System.Drawing.Color.LightSkyBlue;


            TableCell cell12 = new TableCell();

            cell12.Text = "&nbsp;";

            TableCell cell13 = new TableCell();

            cell13.Text = "             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;          Closing Balance : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Convert.ToDouble(lblClosDr.Text) + "   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;       " + Convert.ToDouble(lblClosCr.Text);

            cell13.BackColor = System.Drawing.Color.LightSkyBlue;


            tr1.Cells.Add(cell1);

            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);

            TableRow tr3 = new TableRow();

            tr3.Cells.Add(cell3);


            TableRow tr4 = new TableRow();

            tr4.Cells.Add(cell4);

            TableRow tr5 = new TableRow();

            tr5.Cells.Add(cell5);

            TableRow tr6 = new TableRow();

            tr6.Cells.Add(cell6);

            TableRow tr7 = new TableRow();

            tr7.Cells.Add(cell7);


            TableRow tr8 = new TableRow();

            tr8.Cells.Add(cell8);

            TableRow tr9 = new TableRow();

            tr9.Cells.Add(cell9);

            TableRow tr10 = new TableRow();

            tr10.Cells.Add(cell10);

            TableRow tr11 = new TableRow();

            tr11.Cells.Add(cell11);

            TableRow tr12 = new TableRow();

            tr12.Cells.Add(cell12);

            TableRow tr13 = new TableRow();

            tr13.Cells.Add(cell13);



            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.Rows.Add(tr3);

            tb.Rows.Add(tr4);

            tb.Rows.Add(tr5);

            tb.Rows.Add(tr6);

            tb.Rows.Add(tr7);

            tb.Rows.Add(tr8);

            tb.Rows.Add(tr9);

            tb.Rows.Add(tr10);

            tb.Rows.Add(tr11);

            tb.Rows.Add(tr12);

            tb.Rows.Add(tr13);

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


    public void ExportToExcel(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string file = "Ledger Report_" + DateTime.Now.ToString() + ".xls";
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

    protected void drpLedgerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int iLedgerID = 0;
            iLedgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
            chkSP.Checked = false;
            chkSPR.Checked = false;
            if (iLedgerID == 2)
            {
                dvSalesPurchase.Visible = true;
                lblSP.Text = "Show Sales ";
                lblSPR.Text = "Show Purchase Return ";
            }
            else if (iLedgerID == 3)
            {
                dvSalesPurchase.Visible = true;
                lblSP.Text = "Show Purchase ";
                lblSPR.Text = "Show Sales Return ";
            }
            else
            {
                dvSalesPurchase.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
