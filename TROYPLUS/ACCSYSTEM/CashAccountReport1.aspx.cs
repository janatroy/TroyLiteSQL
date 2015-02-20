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

using System.Data.OleDb;
using System.Xml;
using System.IO;


public partial class CashAccountReport1 : System.Web.UI.Page
{
    public Double damt = 0.0;
    public Double camt = 0.0;
    public Double dDiffamt = 0.0;
    public Double cDiffamt = 0.0;
    public string sDataSource = string.Empty;

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

                //txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
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

                DateTime startDate, endDate;
                int iLedgerID = 0;


                startDate = Convert.ToDateTime(Request.QueryString["stDate"].ToString());

                endDate = Convert.ToDateTime(Request.QueryString["edDate"].ToString());

                iLedgerID = Convert.ToInt32(Request.QueryString["ledid"].ToString());


                ReportsBL.ReportClass rptCashReport;
                rptCashReport = new ReportsBL.ReportClass();

                CashPanel.Visible = true;
                //startDate = Convert.ToDateTime(txtStartDate.Text);
                //endDate = Convert.ToDateTime(txtEndDate.Text);


                lblStartDate.Text = startDate.ToString();
                lblEndDate.Text = endDate.ToString();

                DataSet ds = rptCashReport.generateReportDS(iLedgerID, startDate, endDate, sDataSource, 0);

                gvCash.DataSource = ds;
                gvCash.DataBind();
                CalculateDebitCredit();
                CashPanel.Visible = true;
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
            Response.Redirect("CashAccountReport.aspx");
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
            int iLedgerID = 1;

            ReportsBL.ReportClass rptCashReport;
            rptCashReport = new ReportsBL.ReportClass();

            CashPanel.Visible = true;
            //startDate = Convert.ToDateTime(txtStartDate.Text);
            //endDate = Convert.ToDateTime(txtEndDate.Text);


            //lblStartDate.Text = txtStartDate.Text;
            //lblEndDate.Text = txtEndDate.Text;
            double credit = 0;
            double debit = 0;

            //DataSet ds = rptCashReport.generateReportDS(iLedgerID, startDate, endDate, sDataSource, 0);

            //gvCash.DataSource = ds;
            //gvCash.DataBind();
            gvCash.Visible = false;
            CalculateDebitCredit();
            CashPanel.Visible = false;

            #region Export To Excel
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add(new DataColumn("Date"));
            //    dt.Columns.Add(new DataColumn("Particulars"));
            //    dt.Columns.Add(new DataColumn("Voucher Type"));
            //    dt.Columns.Add(new DataColumn("Debit"));
            //    dt.Columns.Add(new DataColumn("Credit"));

            //    DataRow dr_export1 = dt.NewRow();
            //    dt.Rows.Add(dr_export1);

            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DataRow dr_export = dt.NewRow();
            //        dr_export["Date"] = dr["Date"];
            //        dr_export["Particulars"] = dr["Particulars"];
            //        dr_export["Voucher Type"] = dr["VoucherType"];
            //        dr_export["Debit"] = dr["Debit"];
            //        debit = debit + Convert.ToDouble(dr["Debit"]);
            //        dr_export["Credit"] = dr["Credit"];
            //        credit = credit + Convert.ToDouble(dr["credit"]);
            //        dt.Rows.Add(dr_export);
            //    }

            //    //CalculateDebitCredit();

            //    DataRow dr_export2 = dt.NewRow();
            //    dr_export2["Date"] = "";
            //    dr_export2["Particulars"] = "";
            //    dr_export2["Voucher Type"] = "";
            //    dr_export2["Debit"] = "";
            //    dr_export2["Credit"] = "";
            //    dt.Rows.Add(dr_export2);

            //    DataRow dr_export213 = dt.NewRow();
            //    dr_export213["Date"] = "";
            //    dr_export213["Particulars"] = "Opening Balance";
            //    dr_export213["Voucher Type"] = "";
            //    dr_export213["Debit"] = Convert.ToDouble(lblOBDR.Text);
            //    dr_export213["Credit"] = Convert.ToDouble(lblOBCR.Text);
            //    dt.Rows.Add(dr_export213);

            //    DataRow dr_export21 = dt.NewRow();
            //    dr_export21["Date"] = "";
            //    dr_export21["Particulars"] = "";
            //    dr_export21["Voucher Type"] = "";
            //    dr_export21["Debit"] = "";
            //    dr_export21["Credit"] = "";
            //    dt.Rows.Add(dr_export21);

            //    DataRow dr_export23 = dt.NewRow();
            //    dr_export23["Date"] = "";
            //    dr_export23["Particulars"] = "Total";
            //    dr_export23["Voucher Type"] = "";
            //    dr_export23["Debit"] = Convert.ToDouble(lblDebitSum.Text);
            //    dr_export23["Credit"] = Convert.ToDouble(lblCreditSum.Text);
            //    dt.Rows.Add(dr_export23);

            //    DataRow dr_export231 = dt.NewRow();
            //    dr_export231["Date"] = "";
            //    dr_export231["Particulars"] = "";
            //    dr_export231["Voucher Type"] = "";
            //    dr_export231["Debit"] = "";
            //    dr_export231["Credit"] = "";
            //    dt.Rows.Add(dr_export231);

            //    DataRow dr_export3 = dt.NewRow();
            //    dr_export3["Date"] = "";
            //    dr_export3["Particulars"] = "Closing Balance";
            //    dr_export3["Voucher Type"] = "";
            //    dr_export3["Debit"] = Convert.ToDouble(lblDebitDiff.Text);
            //    dr_export3["Credit"] = Convert.ToDouble(lblCreditDiff.Text);
            //    dt.Rows.Add(dr_export3);

            //    ExportToExcel("Cash Account Report.xls", dt);
            //}
            #endregion
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate, endDate;
            int iLedgerID = 1;

            ReportsBL.ReportClass rptCashReport;
            rptCashReport = new ReportsBL.ReportClass();

            CashPanel.Visible = true;
            //startDate = Convert.ToDateTime(txtStartDate.Text);
            //endDate = Convert.ToDateTime(txtEndDate.Text);


            //lblStartDate.Text = txtStartDate.Text;
            //lblEndDate.Text = txtEndDate.Text;

            //DataSet ds = rptCashReport.generateReportDS(iLedgerID, startDate, endDate, sDataSource, 0);

            //gvCash.DataSource = ds;
            //gvCash.DataBind();
            //CalculateDebitCredit();
            //CashPanel.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvCash_RowDataBound(object sender, GridViewRowEventArgs e)
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
                lblCreditSum.Text = camt.ToString("f2");// Convert.ToString(camt);
                e.Row.Cells[3].Text = debit.ToString("f2");
                e.Row.Cells[4].Text = credit.ToString("f2");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void CalculateDebitCredit()
    {


        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();

        if (Request.Cookies["Company"]  != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        double opCr = 0.0;
        double opDr = 0.0;
        double netOp = 0.0;
        DateTime startDate, endDate;

        startDate = Convert.ToDateTime(Request.QueryString["stDate"].ToString());

        endDate = Convert.ToDateTime(Request.QueryString["edDate"].ToString());

        //startDate = Convert.ToDateTime(txtStartDate.Text);
        //endDate = Convert.ToDateTime(txtEndDate.Text);

        //opCr = rpt.getLedgerOpeningBalance(1, "credit", sDataSource) + rpt.getOpeningBalance(1, "credit", Convert.ToDateTime(Session["startDate"]), sDataSource);
        opCr = rpt.getOpeningBalance(0, 0, 1, "credit", startDate, sDataSource);
        //opDr = rpt.getLedgerOpeningBalance(1, "debit", sDataSource) + rpt.getOpeningBalance(1, "debit", Convert.ToDateTime(Session["startDate"]), sDataSource);
        opDr = rpt.getOpeningBalance(0, 0, 1, "debit", startDate, sDataSource);

        if (opDr > opCr)
        {
            netOp = opDr - opCr;
            lblOBDR.Text = netOp.ToString("f2");
            lblOBCR.Text = "0.00";
            if (damt > camt)
            {
                dDiffamt = netOp + (damt - camt);
                cDiffamt = 0;
            }
            else
            {
                if (((camt - damt) - netOp) > 0)
                {
                    cDiffamt = (camt - damt) - netOp;
                    dDiffamt = 0;
                }
                else
                {
                    dDiffamt = Math.Abs((camt - damt) - netOp);
                    cDiffamt = 0;
                }
            }

            lblOBCR.Text = "0.00";
            lblOBDR.Text = netOp.ToString("f2");// Convert.ToString(netOp);

        }
        else
        {
            netOp = opCr - opDr;
            if (damt > camt)
            {
                if (((damt - camt) - netOp) > 0)
                {
                    dDiffamt = (damt - camt) - netOp;
                    cDiffamt = 0;
                }
                else
                {
                    cDiffamt = Math.Abs((damt - camt) - netOp);
                    dDiffamt = 0;
                }

            }
            else
            {
                cDiffamt = netOp + (camt - damt);
                dDiffamt = 0;
            }
            lblOBDR.Text = "0.00";
            lblOBCR.Text = netOp.ToString("f2");

        }

        if (dDiffamt > 0)
        {
            lblDebitDiff.Text = dDiffamt.ToString("f2");// Convert.ToString(dDiffamt);
            lblCreditDiff.Text = "0.00";
        }
        if (cDiffamt > 0)
        {
            lblDebitDiff.Text = "0.00";
            lblCreditDiff.Text = cDiffamt.ToString("f2");

        }
        lblBillDate.Text = DateTime.Now.ToShortDateString();

    }

}
