using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class HirePurchaseReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();

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
            DateTime startDate, endDate;

            ReportsBL.ReportClass rptObj = new ReportsBL.ReportClass();
            string customer = drpCustomer.SelectedValue.ToString();
            //string frequency = drpFrequency.SelectedValue.ToString();
            DataSet tempSVisits = null;

            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            if (Session["TempVisits"] != null)
                Session["TempVisits"] = null;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            var dsServiceEntries = bl.ListHireDetails(startDate, endDate, customer, sDataSource);

            dt.Columns.Add(new DataColumn("BillNo"));
            dt.Columns.Add(new DataColumn("BillDate"));
            dt.Columns.Add(new DataColumn("Customer Name"));
            dt.Columns.Add(new DataColumn("Purchase Amount"));
            dt.Columns.Add(new DataColumn("Final Payment"));
            dt.Columns.Add(new DataColumn("Total Due"));
            dt.Columns.Add(new DataColumn("Total Payment"));
            dt.Columns.Add(new DataColumn("Cq Ret Payment"));
            dt.Columns.Add(new DataColumn("Total Outstanding"));

            double instdue = 0;
            double pay = 0;
            double custpay = 0;

            if (dsServiceEntries != null && dsServiceEntries.Tables[0].Rows.Count > 0)
            {
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                foreach (DataRow dr in dsServiceEntries.Tables[0].Rows)
                {
                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["BillNo"] = dr["slno"];
                    dr_final12["BillDate"] = dr["BillDate"];
                    dr_final12["Customer Name"] = dr["CustomerName"];
                    dr_final12["Purchase Amount"] = dr["puramt"];
                    dr_final12["Final Payment"] = dr["finpay"];
                    dr_final12["Total Due"] = "";
                    dr_final12["Total Payment"] = "";
                    dr_final12["Total Outstanding"] = "";
                    dt.Rows.Add(dr_final12);

                    DataRow dr_final113 = dt.NewRow();
                    dt.Rows.Add(dr_final113);

                    DataRow dr_final131 = dt.NewRow();
                    dr_final131["BillNo"] = "";
                    dr_final131["BillDate"] = "";
                    dr_final131["Customer Name"] = "Installment Dues";
                    dr_final131["Purchase Amount"] = "";
                    dr_final131["Final Payment"] = "";
                    dr_final131["Total Due"] = "";
                    dr_final131["Total Payment"] = "";
                    dr_final131["Total Outstanding"] = "";
                    dt.Rows.Add(dr_final131);

                    int ii = 1;
                    string dtaa1 = string.Empty;

                    for (int i = 0; i < Convert.ToInt32(dr["noinst"]); i++)
                    {
                        DataRow dr_final1313 = dt.NewRow();
                        dr_final1313["BillNo"] = "";
                        dr_final1313["BillDate"] = "";
                        string aa = dr["Startdate"].ToString().ToUpper().Trim();
                        string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");



                        //List<DateTime> result = new List<DateTime>();
                        //    startDate = startDate.AddMonths(1);
                        //    result.Add(startDate);


                        if (ii == 1)
                        {
                            dr_final1313["Customer Name"] = dtaa;
                        }
                        else
                        {
                            string dtt = Convert.ToDateTime(dtaa1).ToString("dd/MM/yyyy");
                            dr_final1313["Customer Name"] = dtt;
                        }


                        dr_final1313["Purchase Amount"] = "";
                        dr_final1313["Final Payment"] = "";
                        dr_final1313["Total Due"] = dr["eachpay"];
                        instdue = instdue + Convert.ToDouble(dr["eachpay"]);
                        dr_final1313["Total Payment"] = "";
                        dr_final1313["Total Outstanding"] = "";
                        dt.Rows.Add(dr_final1313);

                        if (ii == 1)
                        {
                            dtaa1 = Convert.ToDateTime(dtaa).AddMonths(1).ToString();
                        }
                        else
                        {
                            dtaa1 = Convert.ToDateTime(dtaa1).AddMonths(1).ToString();
                        }

                        ii = ii + 1;
                    }



                    int CustomerId = Convert.ToInt32(dr["CustomerId"]);
                    var dspay = bl.ListReceiptsCustomersId(sDataSource, CustomerId);

                    if (dspay != null)
                    {
                        DataRow dr_final112 = dt.NewRow();
                        dt.Rows.Add(dr_final112);

                        DataRow dr_final123 = dt.NewRow();
                        dr_final123["BillNo"] = "";
                        dr_final123["BillDate"] = "";
                        dr_final123["Customer Name"] = "Payment Received";
                        dr_final123["Purchase Amount"] = "";
                        dr_final123["Final Payment"] = "";
                        dr_final123["Total Due"] = "";
                        dr_final123["Total Payment"] = "";
                        dr_final123["Cq Ret Payment"] = "";
                        dr_final123["Total Outstanding"] = "";
                        dt.Rows.Add(dr_final123);

                        foreach (DataRow drr in dspay.Tables[0].Rows)
                        {
                            DataRow dr_final1 = dt.NewRow();
                            dr_final1["BillNo"] = "";
                            dr_final1["BillDate"] = "";
                            string aaa = drr["Transdate"].ToString().ToUpper().Trim();
                            string dtaaa = Convert.ToDateTime(aaa).ToString("dd/MM/yyyy");
                            dr_final1["Customer Name"] = dtaaa;
                            dr_final1["Purchase Amount"] = "";
                            dr_final1["Final Payment"] = "";
                            dr_final1["Total Due"] = "";
                            dr_final1["Total Payment"] = drr["Amount"];
                            pay = pay + Convert.ToDouble(drr["Amount"]);
                            dr_final1["Total Outstanding"] = "";
                            dt.Rows.Add(dr_final1);
                        }
                    }

                    DataRow dr_final1121 = dt.NewRow();
                    dt.Rows.Add(dr_final1121);

                    int CustomerIdd = Convert.ToInt32(dr["CustomerId"]);
                    var dscustpay = bl.ListPaymentCustomersId(sDataSource, CustomerIdd);
                    custpay = 0;
                    if (dscustpay != null)
                    {
                        DataRow dr_final112 = dt.NewRow();
                        dt.Rows.Add(dr_final112);

                        DataRow dr_final123 = dt.NewRow();
                        dr_final123["BillNo"] = "";
                        dr_final123["BillDate"] = "";
                        dr_final123["Customer Name"] = "Customer Payment";
                        dr_final123["Purchase Amount"] = "";
                        dr_final123["Final Payment"] = "";
                        dr_final123["Total Due"] = "";
                        dr_final123["Total Payment"] = "";
                        dr_final123["Cq Ret Payment"] = "";
                        dr_final123["Total Outstanding"] = "";
                        dt.Rows.Add(dr_final123);

                        foreach (DataRow drrd in dscustpay.Tables[0].Rows)
                        {
                            DataRow dr_final1 = dt.NewRow();
                            dr_final1["BillNo"] = "";
                            dr_final1["BillDate"] = "";
                            string aaa = drrd["Transdate"].ToString().ToUpper().Trim();
                            string dtaaa = Convert.ToDateTime(aaa).ToString("dd/MM/yyyy");
                            dr_final1["Customer Name"] = dtaaa;
                            dr_final1["Purchase Amount"] = "";
                            dr_final1["Final Payment"] = "";
                            dr_final1["Total Due"] = "";
                            dr_final1["Total Payment"] = "";
                            dr_final1["Cq Ret Payment"] = drrd["Amount"];
                            custpay = custpay + Convert.ToDouble(drrd["Amount"]);
                            dr_final1["Total Outstanding"] = "";
                            dt.Rows.Add(dr_final1);
                        }
                    }

                    DataRow dr_final112122 = dt.NewRow();
                    dt.Rows.Add(dr_final112122);

                    DataRow dr_final1312 = dt.NewRow();
                    dr_final1312["BillNo"] = "";
                    dr_final1312["BillDate"] = "";
                    dr_final1312["Customer Name"] = "Total";
                    dr_final1312["Purchase Amount"] = "";
                    dr_final1312["Final Payment"] = "";
                    dr_final1312["Total Due"] = instdue;
                    dr_final1312["Total Payment"] = pay;
                    dr_final1312["Cq Ret Payment"] = custpay;
                    dr_final1312["Total Outstanding"] = instdue - pay + custpay;
                    dt.Rows.Add(dr_final1312);

                    DataRow dr_final1132 = dt.NewRow();
                    dt.Rows.Add(dr_final1132);

                    instdue = 0;
                    pay = 0;
                }

                DataRow dr_final879 = dt.NewRow();
                dt.Rows.Add(dr_final879);

                ExportToExcel(dt);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
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
            string filename = "Hire Purchase _" + DateTime.Now.ToString() + ".xls";
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

            ReportsBL.ReportClass rptObj = new ReportsBL.ReportClass();
            string customer = drpCustomer.SelectedValue.ToString();
            //string frequency = drpFrequency.SelectedValue.ToString();
            DataSet tempSVisits = null;

            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);

            if (Session["TempVisits"] != null)
                Session["TempVisits"] = null;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            var dsServiceEntries = bl.ListHireDetails(startDate, endDate, customer, sDataSource);

            if (dsServiceEntries != null && dsServiceEntries.Tables[0].Rows.Count > 0)
            {
                gvSales.DataSource = dsServiceEntries;
                gvSales.DataBind();
            }
            else
            {
                gvSales.DataSource = null;
                gvSales.DataBind();
            }

            divPrint.Visible = true;
            divmain.Visible = true;
            div1.Visible = false;
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

            Response.AddHeader("content-disposition","attachment;filename=TimeSheetReport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Hire Purchase Report";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvSales);

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

        //dc = new DataColumn("Frequency");
        //dt.Columns.Add(dc);

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

    protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            

        }
    }


    protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                GridView gvv = e.Row.FindControl("gvPayment") as GridView;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                Label lblCustomerId = e.Row.FindControl("lblCustomer") as Label;
                int CustomerId = Convert.ToInt32(lblCustomerId.Text);

                //var dsinst = bl.getInstalmentDues(CustomerId, sDataSource);
                //gv.DataSource = dsinst;
                //gv.DataBind();

                var dspay = bl.ListReceiptsCustomersId(sDataSource, CustomerId);
                gvv.DataSource = dspay;
                gvv.DataBind();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
