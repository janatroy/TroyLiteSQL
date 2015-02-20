using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class ServiceMgmntReport : System.Web.UI.Page
{
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

                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
            }
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
            ReportsBL.ReportClass rptObj = new ReportsBL.ReportClass();
            string customer = drpCustomer.SelectedValue.ToString();
            string frequency = drpFrequency.SelectedValue.ToString();
            DataSet tempSVisits = null;

            if (Session["TempVisits"] != null)
                Session["TempVisits"] = null;

            var dsServiceEntries = rptObj.ListServiceEntries(customer, frequency, sDataSource);

            if (dsServiceEntries != null && dsServiceEntries.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow drEntry in dsServiceEntries.Tables[0].Rows)
                {
                    GenerateTempServiceVisits(drEntry);
                }

                tempSVisits = (DataSet)Session["TempVisits"];

                var reportData = GenerateReportDataset();

                foreach (DataRow dr in tempSVisits.Tables[0].Rows)
                {
                    string serviceID = dr["ServiceID"].ToString();
                    DateTime dueDate = DateTime.Parse(dr["DueDate"].ToString());

                    var visitData = rptObj.GetServiceVisitDetails(serviceID, dueDate, sDataSource);

                    if (visitData == null || visitData.Tables[0].Rows.Count == 0)
                    {
                        var entryData = rptObj.GetServiceEntryDetails(serviceID, sDataSource);

                        DataRow drNew = reportData.Tables[0].NewRow();
                        drNew["ServiceID"] = serviceID;
                        drNew["RefNumber"] = entryData.Tables[0].Rows[0]["RefNumber"].ToString();
                        drNew["Details"] = entryData.Tables[0].Rows[0]["Details"].ToString();
                        drNew["Customer"] = entryData.Tables[0].Rows[0]["Customer"].ToString();
                        drNew["StartDate"] = DateTime.Parse(entryData.Tables[0].Rows[0]["StartDate"].ToString()).ToShortDateString();
                        drNew["EndDate"] = DateTime.Parse(entryData.Tables[0].Rows[0]["EndDate"].ToString()).ToShortDateString();
                        drNew["Amount"] = entryData.Tables[0].Rows[0]["Amount"].ToString();
                        drNew["Frequency"] = entryData.Tables[0].Rows[0]["Frequency"].ToString();
                        drNew["DueDate"] = dueDate.ToShortDateString();
                        drNew["VisitDate"] = string.Empty;
                        drNew["VisitDetails"] = string.Empty;
                        drNew["AmountReceived"] = string.Empty;
                        drNew["PayMode"] = string.Empty;
                        drNew["DiffDays"] = string.Empty;
                        drNew["DiffAmount"] = string.Empty;

                        reportData.Tables[0].Rows.Add(drNew);
                    }
                    else
                    {

                        if (visitData.Tables[0].Rows[0]["Visited"].ToString() == "True")
                        {
                            DataRow drNew = reportData.Tables[0].NewRow();

                            var dDate = DateTime.Parse(visitData.Tables[0].Rows[0]["DueDate"].ToString());
                            var vDate = DateTime.Parse(visitData.Tables[0].Rows[0]["VisitDate"].ToString());
                            var dAmount = double.Parse(visitData.Tables[0].Rows[0]["Amount"].ToString());
                            var rAmount = double.Parse(visitData.Tables[0].Rows[0]["AmountReceived"].ToString());

                            string diffDays = (vDate - dDate).TotalDays.ToString("#0");
                            string diffAmt = (dAmount - rAmount).ToString("#0");

                            drNew["ServiceID"] = serviceID;
                            drNew["RefNumber"] = visitData.Tables[0].Rows[0]["RefNumber"].ToString();
                            drNew["Details"] = visitData.Tables[0].Rows[0]["Details"].ToString();
                            drNew["Customer"] = visitData.Tables[0].Rows[0]["Customer"].ToString();
                            drNew["StartDate"] = DateTime.Parse(visitData.Tables[0].Rows[0]["StartDate"].ToString()).ToShortDateString();
                            drNew["EndDate"] = DateTime.Parse(visitData.Tables[0].Rows[0]["EndDate"].ToString()).ToShortDateString();
                            drNew["Amount"] = dAmount.ToString("#0");
                            drNew["DueDate"] = dDate.ToShortDateString();
                            drNew["VisitDate"] = vDate.ToShortDateString();
                            drNew["VisitDetails"] = visitData.Tables[0].Rows[0]["VisitDetails"].ToString();
                            drNew["Frequency"] = visitData.Tables[0].Rows[0]["Frequency"].ToString();
                            drNew["AmountReceived"] = rAmount.ToString("#0");
                            drNew["PayMode"] = visitData.Tables[0].Rows[0]["PayMode"].ToString();
                            drNew["DiffDays"] = diffDays;
                            drNew["DiffAmount"] = diffAmt;

                            reportData.Tables[0].Rows.Add(drNew);
                        }
                        else
                        {
                            var entryData = rptObj.GetServiceEntryDetails(serviceID, sDataSource);

                            DataRow drNew = reportData.Tables[0].NewRow();
                            drNew["ServiceID"] = serviceID;
                            drNew["RefNumber"] = entryData.Tables[0].Rows[0]["RefNumber"].ToString();
                            drNew["Details"] = entryData.Tables[0].Rows[0]["Details"].ToString();
                            drNew["Customer"] = entryData.Tables[0].Rows[0]["Customer"].ToString();
                            drNew["StartDate"] = DateTime.Parse(entryData.Tables[0].Rows[0]["StartDate"].ToString()).ToShortDateString();
                            drNew["EndDate"] = DateTime.Parse(entryData.Tables[0].Rows[0]["EndDate"].ToString()).ToShortDateString();
                            drNew["Amount"] = entryData.Tables[0].Rows[0]["Amount"].ToString();
                            drNew["Frequency"] = entryData.Tables[0].Rows[0]["Frequency"].ToString();
                            drNew["DueDate"] = dueDate.ToShortDateString();
                            drNew["VisitDate"] = string.Empty;
                            drNew["VisitDetails"] = string.Empty;
                            drNew["AmountReceived"] = string.Empty;
                            drNew["PayMode"] = string.Empty;
                            drNew["DiffDays"] = string.Empty;
                            drNew["DiffAmount"] = string.Empty;

                            reportData.Tables[0].Rows.Add(drNew);
                        }
                    }
                }

                DataTable dtData = reportData.Tables[0];

                EnumerableRowCollection<DataRow> query = from data in dtData.AsEnumerable()
                                                         where data.Field<DateTime>("DueDate") >= DateTime.Parse(txtStartDate.Text) && data.Field<DateTime>("DueDate") <= DateTime.Parse(txtEndDate.Text)
                                                         select data;
                if (chkMissedVisit.Checked)
                {
                    query = query.Where(x => x.Field<string>("VisitDate") == "");
                }

                DataView dv = query.AsDataView();

                gvReport.DataSource = dv;
                gvReport.DataBind();
            }
            else
            {
                gvReport.DataSource = null;
                gvReport.DataBind();
            }

            divPrint.Visible = true;


            ExportToExcel();
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
            ReportsBL.ReportClass rptObj = new ReportsBL.ReportClass();
            string customer = drpCustomer.SelectedValue.ToString();
            string frequency = drpFrequency.SelectedValue.ToString();
            DataSet tempSVisits = null;

            if (Session["TempVisits"] != null)
                Session["TempVisits"] = null;

            var dsServiceEntries = rptObj.ListServiceEntries(customer, frequency, sDataSource);

            if (dsServiceEntries != null && dsServiceEntries.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow drEntry in dsServiceEntries.Tables[0].Rows)
                {
                    GenerateTempServiceVisits(drEntry);
                }

                tempSVisits = (DataSet)Session["TempVisits"];

                var reportData = GenerateReportDataset();

                foreach (DataRow dr in tempSVisits.Tables[0].Rows)
                {
                    string serviceID = dr["ServiceID"].ToString();
                    DateTime dueDate = DateTime.Parse(dr["DueDate"].ToString());

                    var visitData = rptObj.GetServiceVisitDetails(serviceID, dueDate, sDataSource);

                    if (visitData == null || visitData.Tables[0].Rows.Count == 0)
                    {
                        var entryData = rptObj.GetServiceEntryDetails(serviceID, sDataSource);

                        DataRow drNew = reportData.Tables[0].NewRow();
                        drNew["ServiceID"] = serviceID;
                        drNew["RefNumber"] = entryData.Tables[0].Rows[0]["RefNumber"].ToString();
                        drNew["Details"] = entryData.Tables[0].Rows[0]["Details"].ToString();
                        drNew["Customer"] = entryData.Tables[0].Rows[0]["Customer"].ToString();
                        drNew["StartDate"] = DateTime.Parse(entryData.Tables[0].Rows[0]["StartDate"].ToString()).ToShortDateString();
                        drNew["EndDate"] = DateTime.Parse(entryData.Tables[0].Rows[0]["EndDate"].ToString()).ToShortDateString();
                        drNew["Amount"] = entryData.Tables[0].Rows[0]["Amount"].ToString();
                        drNew["Frequency"] = entryData.Tables[0].Rows[0]["Frequency"].ToString();
                        drNew["DueDate"] = dueDate.ToShortDateString();
                        drNew["VisitDate"] = string.Empty;
                        drNew["VisitDetails"] = string.Empty;
                        drNew["AmountReceived"] = string.Empty;
                        drNew["PayMode"] = string.Empty;
                        drNew["DiffDays"] = string.Empty;
                        drNew["DiffAmount"] = string.Empty;

                        reportData.Tables[0].Rows.Add(drNew);
                    }
                    else
                    {

                        if (visitData.Tables[0].Rows[0]["Visited"].ToString() == "True")
                        {
                            DataRow drNew = reportData.Tables[0].NewRow();

                            var dDate = DateTime.Parse(visitData.Tables[0].Rows[0]["DueDate"].ToString());
                            var vDate = DateTime.Parse(visitData.Tables[0].Rows[0]["VisitDate"].ToString());
                            var dAmount = double.Parse(visitData.Tables[0].Rows[0]["Amount"].ToString());
                            var rAmount = double.Parse(visitData.Tables[0].Rows[0]["AmountReceived"].ToString());

                            string diffDays = (vDate - dDate).TotalDays.ToString("#0");
                            string diffAmt = (dAmount - rAmount).ToString("#0");

                            drNew["ServiceID"] = serviceID;
                            drNew["RefNumber"] = visitData.Tables[0].Rows[0]["RefNumber"].ToString();
                            drNew["Details"] = visitData.Tables[0].Rows[0]["Details"].ToString();
                            drNew["Customer"] = visitData.Tables[0].Rows[0]["Customer"].ToString();
                            drNew["StartDate"] = DateTime.Parse(visitData.Tables[0].Rows[0]["StartDate"].ToString()).ToShortDateString();
                            drNew["EndDate"] = DateTime.Parse(visitData.Tables[0].Rows[0]["EndDate"].ToString()).ToShortDateString();
                            drNew["Amount"] = dAmount.ToString("#0");
                            drNew["DueDate"] = dDate.ToShortDateString();
                            drNew["VisitDate"] = vDate.ToShortDateString();
                            drNew["VisitDetails"] = visitData.Tables[0].Rows[0]["VisitDetails"].ToString();
                            drNew["Frequency"] = visitData.Tables[0].Rows[0]["Frequency"].ToString();
                            drNew["AmountReceived"] = rAmount.ToString("#0");
                            drNew["PayMode"] = visitData.Tables[0].Rows[0]["PayMode"].ToString();
                            drNew["DiffDays"] = diffDays;
                            drNew["DiffAmount"] = diffAmt;

                            reportData.Tables[0].Rows.Add(drNew);
                        }
                        else
                        {
                            var entryData = rptObj.GetServiceEntryDetails(serviceID, sDataSource);

                            DataRow drNew = reportData.Tables[0].NewRow();
                            drNew["ServiceID"] = serviceID;
                            drNew["RefNumber"] = entryData.Tables[0].Rows[0]["RefNumber"].ToString();
                            drNew["Details"] = entryData.Tables[0].Rows[0]["Details"].ToString();
                            drNew["Customer"] = entryData.Tables[0].Rows[0]["Customer"].ToString();
                            drNew["StartDate"] = DateTime.Parse(entryData.Tables[0].Rows[0]["StartDate"].ToString()).ToShortDateString();
                            drNew["EndDate"] = DateTime.Parse(entryData.Tables[0].Rows[0]["EndDate"].ToString()).ToShortDateString();
                            drNew["Amount"] = entryData.Tables[0].Rows[0]["Amount"].ToString();
                            drNew["Frequency"] = entryData.Tables[0].Rows[0]["Frequency"].ToString();
                            drNew["DueDate"] = dueDate.ToShortDateString();
                            drNew["VisitDate"] = string.Empty;
                            drNew["VisitDetails"] = string.Empty;
                            drNew["AmountReceived"] = string.Empty;
                            drNew["PayMode"] = string.Empty;
                            drNew["DiffDays"] = string.Empty;
                            drNew["DiffAmount"] = string.Empty;

                            reportData.Tables[0].Rows.Add(drNew);
                        }
                    }
                }

                DataTable dtData = reportData.Tables[0];

                EnumerableRowCollection<DataRow> query = from data in dtData.AsEnumerable()
                                                         where data.Field<DateTime>("DueDate") >= DateTime.Parse(txtStartDate.Text) && data.Field<DateTime>("DueDate") <= DateTime.Parse(txtEndDate.Text)
                                                         select data;
                if (chkMissedVisit.Checked)
                {
                    query = query.Where(x => x.Field<string>("VisitDate") == "");
                }

                DataView dv = query.AsDataView();

                gvReport.DataSource = dv;
                gvReport.DataBind();
            }
            else
            {
                gvReport.DataSource = null;
                gvReport.DataBind();
            }

            divPrint.Visible = false;
            divmain.Visible = false;
            div1.Visible = true;


            Response.Write("<script language='javascript'> window.open('ServiceMgmntReport1.aspx?customer=" + customer + "&frequency=" + frequency + "&dTSEStartDate=" + Convert.ToDateTime(txtStartDate.Text) + "&dTSEEndDate=" + Convert.ToDateTime(txtEndDate.Text) + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
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

    public void ExportToExcel()
    {
        try
        {
            Response.Clear();

            Response.Buffer = true;



            Response.AddHeader("content-disposition",

             "attachment;filename=TimeSheetReport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Service Management Report";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvReport);



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

            //ExportToExcel(ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private void GenerateTempServiceVisits(DataRow drServiceEntry)
    {

        DateTime sDate = DateTime.Parse(drServiceEntry["StartDate"].ToString());
        DateTime eDate = DateTime.Parse(drServiceEntry["EndDate"].ToString());
        string serviceID = drServiceEntry["ServiceID"].ToString();
        int frequency = int.Parse(drServiceEntry["Frequency"].ToString());
        DateTime dueDate = sDate.AddMonths(frequency);
        DataSet tempSVisits;

        if (Session["TempVisits"] != null)
            tempSVisits = (DataSet)Session["TempVisits"];
        else
            tempSVisits = GenerateTempVisitTable();

        int slNo = 0;

        while (dueDate <= eDate)
        {
            slNo++;
            DataRow dr = tempSVisits.Tables[0].NewRow();
            dr["ServiceID"] = serviceID;
            dr["SlNo"] = slNo;
            dr["DueDate"] = dueDate.ToShortDateString();

            dueDate = dueDate.AddMonths(frequency);

            tempSVisits.Tables[0].Rows.Add(dr);
        }

        Session["TempVisits"] = tempSVisits;

    }

    private DataSet GenerateTempVisitTable()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        dc = new DataColumn("ServiceID", typeof(Int32));
        dt.Columns.Add(dc);

        dc = new DataColumn("SlNo", typeof(Int32));
        dt.Columns.Add(dc);

        dc = new DataColumn("DueDate", typeof(DateTime));
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        return ds;

    }

    private DataSet GenerateReportDataset()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        dc = new DataColumn("ServiceID");
        dt.Columns.Add(dc);

        dc = new DataColumn("RefNumber");
        dt.Columns.Add(dc);

        dc = new DataColumn("Details");
        dt.Columns.Add(dc);

        dc = new DataColumn("Customer");
        dt.Columns.Add(dc);

        dc = new DataColumn("Frequency");
        dt.Columns.Add(dc);

        dc = new DataColumn("StartDate");
        dt.Columns.Add(dc);

        dc = new DataColumn("EndDate");
        dt.Columns.Add(dc);

        dc = new DataColumn("Amount");
        dt.Columns.Add(dc);

        dc = new DataColumn("DueDate", typeof(DateTime));
        dt.Columns.Add(dc);

        dc = new DataColumn("VisitDate");
        dt.Columns.Add(dc);

        dc = new DataColumn("VisitDetails");
        dt.Columns.Add(dc);

        dc = new DataColumn("AmountReceived");
        dt.Columns.Add(dc);

        dc = new DataColumn("PayMode");
        dt.Columns.Add(dc);

        dc = new DataColumn("DiffDays");
        dt.Columns.Add(dc);

        dc = new DataColumn("DiffAmount");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        return ds;

    }

}
