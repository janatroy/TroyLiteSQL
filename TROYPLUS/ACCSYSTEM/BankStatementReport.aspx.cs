﻿using System;
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
using System.Data;
using ClosedXML.Excel;
public partial class BankStatementReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    public Double damt = 0.0;
    public Double camt = 0.0;
    public Double dDiffamt = 0.0;
    public Double cDiffamt = 0.0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!IsPostBack)
            {
                //AccessDataSource1.DataFile = sDataSource; 
                if (Request.Cookies["Company"] != null)
                {
                    DataSet companyInfo = new DataSet();
                    BusinessLogic bl = new BusinessLogic(sDataSource);
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);
                    lblBillDate.Text = DateTime.Now.ToShortDateString();

                    txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();

                    DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                    string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                    txtEndDate.Text = dtaa;

                    //txtEndDate.Text = DateTime.Now.ToShortDateString();

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
                loadBanks();
            }
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

            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            //    string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");

          //  ReportsBL.ReportClass rptBankReport;
            bnkPanel.Visible = true;

            iLedgerID = Convert.ToInt32(drpBankName.SelectedItem.Value);
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            lblStartDate.Text = txtStartDate.Text;
            lblEndDate.Text = txtEndDate.Text;
           // rptBankReport = new ReportsBL.ReportClass();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = bl.generateReportDS(iLedgerID, startDate, endDate, sDataSource, 0);

            double credit = 0;
            double debit = 0;


            #region Export To Excel
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable("Bank Statement");
                dt.Columns.Add(new DataColumn("Date"));
                dt.Columns.Add(new DataColumn("Particulars"));
                dt.Columns.Add(new DataColumn("BranchCode"));
                dt.Columns.Add(new DataColumn("Voucher Type"));             
                dt.Columns.Add(new DataColumn("Debit"));
                dt.Columns.Add(new DataColumn("Credit"));

                DataRow dr_export1 = dt.NewRow();
                dt.Rows.Add(dr_export1);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["Date"] = dr["Date"];
                    dr_export["Particulars"] = dr["Particulars"];
                    dr_export["BranchCode"] = dr["BranchCode"];
                    dr_export["Voucher Type"] = dr["VoucherType"];
                    dr_export["Debit"] = dr["Debit"];
                    debit = debit + Convert.ToDouble(dr["Debit"]);
                    dr_export["Credit"] = dr["Credit"];
                    credit = credit + Convert.ToDouble(dr["credit"]);
                    dt.Rows.Add(dr_export);
                }

                DataRow dr_export2 = dt.NewRow();
                dr_export2["Date"] = "";
                dr_export2["Particulars"] = "";
                dr_export2["BranchCode"] = "";
                dr_export2["Voucher Type"] = "";
                dr_export2["Debit"] = "";
                dr_export2["Credit"] = "";
                dt.Rows.Add(dr_export2);

                DataRow dr_export213 = dt.NewRow();
                dr_export213["Date"] = "";
                dr_export213["Particulars"] = "Total";
                dr_export213["BranchCode"] = "";
                dr_export213["Voucher Type"] = "";
                dr_export213["Debit"] = debit;
                dr_export213["Credit"] = credit;
                dt.Rows.Add(dr_export213);

                DataRow dr_export21 = dt.NewRow();
                dr_export21["Date"] = "";
                dr_export21["Particulars"] = "";
                dr_export21["BranchCode"] = "";
                dr_export21["Voucher Type"] = "";
                dr_export21["Debit"] = "";
                dr_export21["Credit"] = "";
                dt.Rows.Add(dr_export21);

                double Diffamt = 0;
                Diffamt = debit - credit;

                DataRow dr_export23 = dt.NewRow();
                dr_export23["Date"] = "";
                dr_export23["Particulars"] = "Difference";
                dr_export23["BranchCode"] = "";
                dr_export23["Voucher Type"] = "";
                if (Diffamt >= 0)
                {
                    dr_export23["Debit"] = Diffamt;
                    dr_export23["Credit"] = 0;
                }
                else if (Diffamt <= 0)
                {
                    dr_export23["Debit"] = 0;
                    if (Diffamt < 0)
                    {
                        dr_export23["Credit"] = -Diffamt;
                    }
                    else
                    {
                        dr_export23["Credit"] = Diffamt;
                    }
                }
                dt.Rows.Add(dr_export23);

                ExportToExcel("Bank Statement.xlsx", dt);
            }
            #endregion
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
            bnkPanel.Visible = false;
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
            using (XLWorkbook wb = new XLWorkbook())
            {
               // string filename = "Absolete report.xlsx";
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

    private void loadBanks()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        drpBankName.Items.Clear();
      //  drpBankName.Items.Add(new ListItem("Select Bank", "0"));
        ds = bl.ListBanks();
        drpBankName.DataSource = ds;
        drpBankName.Items.Add(new ListItem("Select Bank", "1"));
        drpBankName.DataBind();
        drpBankName.DataTextField = "LedgerName";
        drpBankName.DataValueField = "LedgerID";

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate, endDate;
            int iLedgerID = 0;

            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            //    string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");

            ReportsBL.ReportClass rptBankReport;
            bnkPanel.Visible = true;

            iLedgerID = Convert.ToInt32(drpBankName.SelectedItem.Value);

            if (drpBankName.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select the Bank. It cannot be left blank');", true);
                return;
            }
            

            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            lblStartDate.Text = txtStartDate.Text;
            lblEndDate.Text = txtEndDate.Text;
            rptBankReport = new ReportsBL.ReportClass();
            //DataSet ds = rptBankReport.generateReportDS(iLedgerID, startDate, endDate, sDataSource, 0);
            //gvBank.DataSource = ds;
            //gvBank.DataBind();
            bnkPanel.Visible = false;

            div1.Visible = true;

            Response.Write("<script language='javascript'> window.open('BankStatementReport1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + "&iLedgerID=" + iLedgerID + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvBank_RowDataBound(object sender, GridViewRowEventArgs e)
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

                lblDebitSum.Text = damt.ToString("f2");// Convert.ToString(damt);
                lblCreditSum.Text = camt.ToString("f2");// Convert.ToString(camt);

                dDiffamt = damt - camt;
                cDiffamt = camt - damt;

                e.Row.Cells[3].Text = debit.ToString("f2");
                e.Row.Cells[4].Text = credit.ToString("f2");

            }
            else
            {
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

                if (cDiffamt == 0 && dDiffamt == 0)
                {
                    lblDebitDiff.Text = "0.00";
                    lblCreditDiff.Text = "0.00";
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }



}
