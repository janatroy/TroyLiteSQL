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

using System.IO;
using ClosedXML.Excel;

public partial class DayBookReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    double dDebit = 0;
    double dCredit = 0;
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

                loadBranch();
                BranchEnable_Disable();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BranchEnable_Disable()
    {
        string sCustomer = string.Empty;
        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        drpBranchAdd.ClearSelection();
        ListItem li = drpBranchAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        if (li != null) li.Selected = true;

        if (dsd.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
        {
            drpBranchAdd.Enabled = true;
        }
        else
        {
            drpBranchAdd.Enabled = false;
        }
    }

    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpBranchAdd.Items.Clear();
        drpBranchAdd.Items.Add(new ListItem("All", "0"));
        ds = bl.ListBranch();
        drpBranchAdd.DataSource = ds;
        drpBranchAdd.DataBind();
        drpBranchAdd.DataTextField = "BranchName";
        drpBranchAdd.DataValueField = "Branchcode";
    }

    protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
            div1.Visible = true;
            DayBookPanel.Visible = false;
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
            BusinessLogic bl = new BusinessLogic(sDataSource);

            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            ReportsBL.ReportClass rptDayBook;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            lblStartDate.Text = txtStartDate.Text;
            lblEndDate.Text = txtEndDate.Text;
            rptDayBook = new ReportsBL.ReportClass();
            DayBookPanel.Visible = true;
            DataSet ds = new DataSet();

            string Branch = drpBranchAdd.SelectedValue;

            ds = bl.generateDayBookDS(startDate, endDate, sDataSource, Branch);

            int i = 1;
            double debit = 0;
            double Credit = 0;

            #region Export To Excel
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable("DayBook");
                dt.Columns.Add(new DataColumn("Date"));
                dt.Columns.Add(new DataColumn("Branchcode"));
                dt.Columns.Add(new DataColumn("Particulars"));
                dt.Columns.Add(new DataColumn("Debit"));
                dt.Columns.Add(new DataColumn("Credit"));
                

                DataRow dr_export1 = dt.NewRow();
                dt.Rows.Add(dr_export1);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //if (i == 1)
                    //{
                    DataRow dr_export = dt.NewRow();
                    dr_export["Date"] = dr["Date"];
                    dr_export["Particulars"] = dr["Debitor"];
                    dr_export["Debit"] = dr["Debit"];
                    debit = debit + Convert.ToDouble(dr["Debit"]);
                    dr_export["Branchcode"] = dr["Branchcode"];
                    
                    dr_export["Credit"] = "";
                    dt.Rows.Add(dr_export);
                    //    i = 2;
                    //}
                    //else if (i == 2)
                    //{
                    DataRow dr_export3 = dt.NewRow();
                    dr_export3["Date"] = "";
                    dr_export3["Particulars"] = dr["Creditor"];
                    dr_export3["Debit"] = "";
                    dr_export3["Credit"] = dr["Credit"];
                    Credit = Credit + Convert.ToDouble(dr["Credit"]);
                    dt.Rows.Add(dr_export3);
                    i = 3;
                    //}
                    //else if (i == 3)
                    //{
                    DataRow dr_export123 = dt.NewRow();
                    dr_export123["Date"] = "";
                    dr_export123["Particulars"] = dr["Narration"];
                    dr_export123["Debit"] = "";
                    dr_export123["Credit"] = "";
                    dt.Rows.Add(dr_export123);
                    i = 4;
                    //}
                    //else if (i == 4)
                    //{
                    //DataRow dr_export123123 = dt.NewRow();
                    //dr_export123123["Date"] = "";
                    //dr_export123123["Particulars"] = "";
                    //dr_export123123["Debit"] = "";
                    //dr_export123123["Credit"] = "";
                    //dt.Rows.Add(dr_export123123);
                    i = 1;
                    //}
                }

                DataRow dr_export2 = dt.NewRow();
                dr_export2["Date"] = "";
                dr_export2["Particulars"] = "";
                dr_export2["Debit"] = "";
                dr_export2["Credit"] = "";
                dt.Rows.Add(dr_export2);

                DataRow dr_export312 = dt.NewRow();
                dr_export312["Date"] = "";
                dr_export312["Particulars"] = "Total ";
                dr_export312["Debit"] = debit;
                dr_export312["Credit"] = Credit;
                dt.Rows.Add(dr_export312);

                ExportToExcel("", dt);
            }
            #endregion
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    public void ExportToExcel(string filenam, DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            //System.IO.StringWriter tw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            //DataGrid dgGrid = new DataGrid();
            //dgGrid.DataSource = dt;
            //dgGrid.DataBind();
            //dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            //dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            //dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
            //dgGrid.HeaderStyle.Font.Bold = true;
            ////Get the HTML for the control.
            //dgGrid.RenderControl(hw);
            ////Write the HTML back to the browser.
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            //this.EnableViewState = false;
            //Response.Write(tw.ToString());
            //Response.End();

            using (XLWorkbook wb = new XLWorkbook())
            {
                string filename = "Cash Account Report.xlsx";
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate, endDate;

            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            ReportsBL.ReportClass rptDayBook;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            lblStartDate.Text = txtStartDate.Text;
            lblEndDate.Text = txtEndDate.Text;
            rptDayBook = new ReportsBL.ReportClass();
            DayBookPanel.Visible = true;
            DataSet ds = new DataSet();

            string Branch = drpBranchAdd.SelectedValue;

            //ds = rptDayBook.generateDayBookDS(startDate, endDate, sDataSource);
            //gvLedger.DataSource = ds;
            //gvLedger.DataBind();
            //CalculateDebitCredit();
            DayBookPanel.Visible = false;
            div1.Visible = true;

            Response.Write("<script language='javascript'> window.open('DayBookReport1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + "&Branch=" + Branch + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    protected void CalculateDebitCredit()
    {
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        DateTime stDate;
        stDate = Convert.ToDateTime(txtStartDate.Text.Trim());
        lblOB.Text = rpt.GetDayBookOB(stDate, sDataSource).ToString("N2");
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

                dDebit = dDebit + debit;
                dCredit = dCredit + credit;
                e.Row.Cells[2].Text = debit.ToString("f2");
                e.Row.Cells[3].Text = credit.ToString("f2");

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                lblSumDebit.Text = dDebit.ToString("N2");
                lblSumCredit.Text = dCredit.ToString("N2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
