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

public partial class SalesPerformanceDealerReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double sumValue = 0.0;
    Double sumValueR = 0.0;
    double SumBill = 0.0;
    double SumRetBill = 0.0;
    double SumTran = 0.0;
    double SumRetTran = 0.0;
    double SumItem = 0.0;
    double SumRetItem = 0.0;
    double SumAllTran = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!IsPostBack)
            {
                loadLedger();

                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtEndDate.Text = dtaa;

                divPrint.Visible = false;
                //txtEndDate.Text = DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadLedger()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListLedger();
        drpLedgerName.DataSource = ds;
        drpLedgerName.DataBind();
        drpLedgerName.DataTextField = "LedgerName";
        drpLedgerName.DataValueField = "LedgerID";
    }

    public void ExportToExcel()
    {
        try
        {
            Response.Clear();

            Response.Buffer = true;



            Response.AddHeader("content-disposition",

             "attachment;filename=SalesPerformanceDealer.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);


            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Sales - Billwise";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvSalesBill);

            TableCell cell3 = new TableCell();

            cell3.Text = "&nbsp;";

            TableCell cell4 = new TableCell();

            cell4.Text = "Total Sales Amount Billwise : " + lblTotalBill.Text;

            TableCell cell5 = new TableCell();

            cell5.Text = "&nbsp;";

            TableCell cell6 = new TableCell();

            cell6.Text = "Sales Return - Billwise";

            TableCell cell7 = new TableCell();

            cell7.Controls.Add(gvSalesReturnBill);


            TableCell cell8 = new TableCell();

            cell8.Text = "Total Sales Return Amount Billwise : " + lblTotalReturnBill.Text;

            TableCell cell9 = new TableCell();

            cell9.Text = "&nbsp;";


            TableCell cell10 = new TableCell();

            cell10.Text = "Sales Transactions";

            TableCell cell11 = new TableCell();

            cell11.Controls.Add(gvSales);

            TableCell cell12 = new TableCell();

            cell12.Text = "&nbsp;";

            TableCell cell13 = new TableCell();

            cell13.Text = "Total Sales Transactions" + lblTotalSalesTran.Text;

            TableCell cell14 = new TableCell();

            cell14.Text = "&nbsp;";

            TableCell cell15 = new TableCell();

            cell15.Text = "Sales Return Transactions";

            TableCell cell16 = new TableCell();

            cell16.Controls.Add(gvSalesReturn);

            TableCell cell17 = new TableCell();

            cell17.Text = "&nbsp;";

            TableCell cell18 = new TableCell();

            cell18.Text = "Total Sales Return Transaction : " + lblTotalSalesReturnTran.Text;

            TableCell cell19 = new TableCell();

            cell19.Text = "&nbsp;";

            TableCell cell20 = new TableCell();

            cell20.Text = "Sales - Itemwise";

            TableCell cell21 = new TableCell();

            cell21.Controls.Add(gvSalesItemwise);

            TableCell cell22 = new TableCell();

            cell22.Text = "&nbsp;";

            TableCell cell23 = new TableCell();

            cell23.Text = "Total Sales Amount Itemwise : " + lblSalesItemwise.Text;

            TableCell cell24 = new TableCell();

            cell24.Text = "&nbsp;";

            TableCell cell25 = new TableCell();

            cell20.Text = "Sales Return- Itemwise";

            TableCell cell26 = new TableCell();

            cell26.Controls.Add(gvSalesReturnItemwise);

            TableCell cell27 = new TableCell();

            cell27.Text = "&nbsp;";

            TableCell cell28 = new TableCell();

            cell28.Text = "Total Sales Return Amount Itemwise : " + lblSalesReturnItemwise.Text;

            TableCell cell29 = new TableCell();

            cell29.Text = "&nbsp;";

            TableCell cell30 = new TableCell();

            cell30.Text = "All Transactions";

            TableCell cell31 = new TableCell();

            cell31.Controls.Add(gvTran);

            TableCell cell32 = new TableCell();

            cell32.Text = "&nbsp;";

            TableCell cell33 = new TableCell();

            cell33.Text = "Total All Transaction : " + lblAllTran.Text;


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

            TableRow tr14 = new TableRow();

            tr14.Cells.Add(cell14);




            TableRow tr15 = new TableRow();

            tr15.Cells.Add(cell15);

            TableRow tr16 = new TableRow();

            tr16.Cells.Add(cell16);

            TableRow tr17 = new TableRow();

            tr17.Cells.Add(cell17);


            TableRow tr18 = new TableRow();

            tr18.Cells.Add(cell18);

            TableRow tr19 = new TableRow();

            tr19.Cells.Add(cell19);

            TableRow tr20 = new TableRow();

            tr20.Cells.Add(cell20);

            TableRow tr21 = new TableRow();

            tr21.Cells.Add(cell21);

            TableRow tr22 = new TableRow();

            tr22.Cells.Add(cell22);

            TableRow tr23 = new TableRow();

            tr23.Cells.Add(cell23);

            TableRow tr24 = new TableRow();

            tr24.Cells.Add(cell24);

            TableRow tr25 = new TableRow();

            tr25.Cells.Add(cell25);

            TableRow tr26 = new TableRow();

            tr26.Cells.Add(cell26);

            TableRow tr27 = new TableRow();

            tr27.Cells.Add(cell27);

            TableRow tr28 = new TableRow();

            tr28.Cells.Add(cell28);

            TableRow tr29 = new TableRow();

            tr29.Cells.Add(cell29);

            TableRow tr30 = new TableRow();

            tr30.Cells.Add(cell30);

            TableRow tr31 = new TableRow();

            tr31.Cells.Add(cell31);

            TableRow tr32 = new TableRow();

            tr32.Cells.Add(cell32);

            TableRow tr33 = new TableRow();

            tr33.Cells.Add(cell33);

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

            tb.Rows.Add(tr14);


            tb.Rows.Add(tr15);

            tb.Rows.Add(tr16);

            tb.Rows.Add(tr17);

            tb.Rows.Add(tr18);

            tb.Rows.Add(tr19);

            tb.Rows.Add(tr20);

            tb.Rows.Add(tr21);

            tb.Rows.Add(tr22);

            tb.Rows.Add(tr23);

            tb.Rows.Add(tr24);

            tb.Rows.Add(tr25);

            tb.Rows.Add(tr26);

            tb.Rows.Add(tr27);

            tb.Rows.Add(tr28);

            tb.Rows.Add(tr29);

            tb.Rows.Add(tr30);

            tb.Rows.Add(tr31);

            tb.Rows.Add(tr32);

            tb.Rows.Add(tr33);

           

            tb.RenderControl(hw);



            //style to format numbers to string

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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            lblAllTran.Text = "";
            divPrint.Visible = true;
            DateTime startDate, endDate;
            int iLedgerID = 0;
            string sLedgerName = string.Empty;
            Double totalBal = 0.0d;
            DataSet ds = new DataSet();
            ReportsBL.ReportClass rptSalesReport;
            rptSalesReport = new ReportsBL.ReportClass();

            iLedgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
            sLedgerName = drpLedgerName.SelectedItem.Text;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);



            ds = rptSalesReport.GenerateSalesBillwise(sDataSource, startDate, endDate, iLedgerID);
            gvSales.DataSource = ds;
            gvSales.DataBind();

            ds = rptSalesReport.GenerateSalesReturnBillwise(sDataSource, startDate, endDate, iLedgerID);
            gvSalesReturn.DataSource = ds;
            gvSalesReturn.DataBind();

            ds = rptSalesReport.GenerateSalesDetails(sDataSource, startDate, endDate, iLedgerID);
            gvSalesBill.DataSource = ds;
            gvSalesBill.DataBind();

            ds = rptSalesReport.GenerateSalesReturnDetails(sDataSource, startDate, endDate, iLedgerID);
            gvSalesReturnBill.DataSource = ds;
            gvSalesReturnBill.DataBind();

            ds = rptSalesReport.GenerateSalesItemwise(sDataSource, startDate, endDate, iLedgerID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvSalesItemwise.DataSource = ds;
                gvSalesItemwise.DataBind();
            }
            else
            {
                gvSalesItemwise.DataSource = null;
                gvSalesItemwise.DataBind();
                lblSales.Text = "0.00";
            }
            ds = rptSalesReport.GenerateSalesReturnItemwise(sDataSource, startDate, endDate, iLedgerID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvSalesReturnItemwise.DataSource = ds;
                gvSalesReturnItemwise.DataBind();
            }
            else
            {
                lblSalesReturn.Text = "0.00";
                gvSalesReturnItemwise.DataSource = null;
                gvSalesReturnItemwise.DataBind();
            }

            ds = rptSalesReport.GenerateCreditDebitTran(sDataSource, startDate, endDate, iLedgerID);
            gvTran.DataSource = ds;
            gvTran.DataBind();
            totalBal = Convert.ToDouble(lblSales.Text) - Convert.ToDouble(lblSalesReturn.Text);
            lblBalance.Text = totalBal.ToString("#0.00");
            lblTotalSalesTran.Text = lblTotalBill.Text;
            lblTotalSalesReturnTran.Text = lblTotalReturnBill.Text;

            Response.Write("<script language='javascript'> window.open('SalesPerformanceDealerReport1.aspx?iLedgerID=" + iLedgerID + "&sLedgerName=" + sLedgerName + "&startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            divPrint.Visible = false;
            //ExportToExcel();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblPurchaseID = e.Row.FindControl("lblBillno") as Label;
                Label lblPaymode = e.Row.FindControl("lblPaymode") as Label;
                if (lblPaymode.Text == "1")
                {
                    lblPaymode.Text = "Cash";
                }
                else if (lblPaymode.Text == "2")
                {
                    lblPaymode.Text = "Bank";
                }
                else
                {
                    lblPaymode.Text = "Credit";
                }
                int billno = Convert.ToInt32(lblPurchaseID.Text);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();
                DataSet ds = rptProduct.getProductsSales(billno, sDataSource);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                //SumCashSales = SumCashSales + Convert.ToDouble(lblTotalAmt.Text);
                //lblGrandCashSales.Text = "Rs. " + SumCashSales.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvSalesBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource =  Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label lblPaymode = e.Row.FindControl("lblPaymode") as Label;
                Label lblAmount = e.Row.FindControl("lblAmount") as Label;
                if (lblPaymode.Text == "1")
                {
                    lblPaymode.Text = "Cash";
                }
                else if (lblPaymode.Text == "2")
                {
                    lblPaymode.Text = "Bank";
                }
                else
                {
                    lblPaymode.Text = "Credit";
                }

                SumBill = SumBill + Convert.ToDouble(lblAmount.Text);
                lblTotalBill.Text = SumBill.ToString("#0.00");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvSalesReturnBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource =  Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAmount = e.Row.FindControl("lblAmount") as Label;

                Label lblPaymode = e.Row.FindControl("lblPaymode") as Label;
                if (lblPaymode.Text == "1")
                {
                    lblPaymode.Text = "Cash";
                }
                else if (lblPaymode.Text == "2")
                {
                    lblPaymode.Text = "Bank";
                }
                else
                {
                    lblPaymode.Text = "Credit";
                }
                SumRetBill = SumRetBill + Convert.ToDouble(lblAmount.Text);
                lblTotalReturnBill.Text = SumRetBill.ToString("#0.00");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double sumValue = 0.0;
            double sumRateQty = 0.0;
            double sumDis = 0.0;
            double sumVat = 0.0;
            double sumCst = 0.0;
            double discount = 0;
            double vat = 0;
            double cst = 0;
            double rate = 0;
            double qty = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblQty = e.Row.FindControl("lblQty") as Label;
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblDisc = e.Row.FindControl("lblDisc") as Label;
                Label lblVat = e.Row.FindControl("lblVat") as Label;
                Label lblCst = e.Row.FindControl("lblCst") as Label;
                Label lblValue = e.Row.FindControl("lblValue") as Label;
                discount = Convert.ToDouble(lblDisc.Text);
                vat = Convert.ToDouble(lblVat.Text);
                cst = Convert.ToDouble(lblCst.Text);
                rate = Convert.ToDouble(lblRate.Text);
                qty = Convert.ToDouble(lblQty.Text);

                sumRateQty = rate * qty;
                sumDis = sumRateQty - (sumRateQty * (discount / 100));

                sumVat = sumDis * (vat / 100);
                sumCst = sumDis * (cst / 100);

                sumValue = sumDis + sumVat + sumCst;

                lblValue.Text = sumValue.ToString("f2"); // Convert.ToString(sumValue);


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    //protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    Double sumValue = 0.0;
    //    Double sumRateQty = 0.0;
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        Label lblQty = e.Row.FindControl("lblQty") as Label;
    //        Label lblRate = e.Row.FindControl("lblRate") as Label;
    //        Label lblDisc = e.Row.FindControl("lblDisc") as Label;
    //        Label lblVat = e.Row.FindControl("lblVat") as Label;
    //        Label lblValue = e.Row.FindControl("lblValue") as Label;
    //        sumRateQty = Convert.ToDouble(lblRate.Text) * Convert.ToInt32(lblQty.Text);
    //        sumValue = sumRateQty - (sumRateQty * (Convert.ToDouble(lblDisc.Text) / 100)) + (sumRateQty * (Convert.ToDouble(lblVat.Text) / 100));
    //        lblValue.Text = Convert.ToString(sumValue);
    //        lblValue.Text = String.Format("{0:0.00}", lblValue.Text);

    //    }
    //}
    protected void gvSalesItemwise_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAmount = e.Row.FindControl("lblAmount") as Label;
                Label lblValue = e.Row.FindControl("lblAmount") as Label;

                sumValue = sumValue + Convert.ToDouble(lblValue.Text);

                lblSales.Text = sumValue.ToString("#0.00"); // String.Format("{0:0.00}", Convert.ToString(sumValue));
                SumItem = SumItem + Convert.ToDouble(lblAmount.Text);
                lblSalesItemwise.Text = SumItem.ToString("#0.00");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvSalesReturnItemwise_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAmount = e.Row.FindControl("lblAmount") as Label;
                Label lblValue = e.Row.FindControl("lblAmount") as Label;

                sumValueR = sumValueR + Convert.ToDouble(lblValue.Text);

                lblSalesReturn.Text = String.Format("{0:0.00}", Convert.ToString(sumValueR));
                SumRetItem = SumRetItem + Convert.ToDouble(lblAmount.Text);
                lblSalesReturnItemwise.Text = SumRetItem.ToString("#0.00");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void gvTran_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label creditor = e.Row.FindControl("lblCreditor") as Label;
                Label debtor = e.Row.FindControl("lblDebtor") as Label;
                Label lblAmount = e.Row.FindControl("lblAmount") as Label;

                if (drpLedgerName.SelectedItem.Text == debtor.Text)
                    SumAllTran = SumAllTran + Convert.ToDouble(lblAmount.Text);
                else
                    SumAllTran = SumAllTran - Convert.ToDouble(lblAmount.Text);

                if (SumAllTran > 0)
                    lblAllTran.Text = SumAllTran.ToString("#0.00") + " Dr";
                else
                    lblAllTran.Text = Math.Abs(SumAllTran).ToString("#0.00") + " Cr";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvSalesReturn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource =  Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProductsPurchase") as GridView;
                Label lblPurchaseID = e.Row.FindControl("lblBillno") as Label;
                Label lblPaymode = e.Row.FindControl("lblPaymode") as Label;
                if (lblPaymode.Text == "1")
                {
                    lblPaymode.Text = "Cash";
                }
                else if (lblPaymode.Text == "2")
                {
                    lblPaymode.Text = "Bank";
                }
                else
                {
                    lblPaymode.Text = "Credit";
                }
                int billno = Convert.ToInt32(lblPurchaseID.Text);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();
                DataSet ds = rptProduct.getProductsPurchase(billno, sDataSource);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                //SumCashSales = SumCashSales + Convert.ToDouble(lblTotalAmt.Text);
                //lblGrandCashSales.Text = "Rs. " + SumCashSales.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    //protected void gvProductsPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    Double sumValue = 0.0;
    //    Double sumRateQty = 0.0;
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        Label lblQty = e.Row.FindControl("lblQty") as Label;
    //        Label lblRate = e.Row.FindControl("lblRate") as Label;
    //        Label lblDisc = e.Row.FindControl("lblDisc") as Label;
    //        Label lblVat = e.Row.FindControl("lblVat") as Label;
    //        Label lblValue = e.Row.FindControl("lblValue") as Label;
    //        sumRateQty = Convert.ToDouble(lblRate.Text) * Convert.ToInt32(lblQty.Text);
    //        sumValue = sumRateQty - (sumRateQty * (Convert.ToDouble(lblDisc.Text) / 100)) + (sumRateQty * (Convert.ToDouble(lblVat.Text) / 100));
    //        lblValue.Text = Convert.ToString(sumValue);
    //        lblValue.Text = String.Format("{0:0.00}", lblValue.Text);

    //    }
    //}
    protected void gvProductsPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double sumValue = 0.0;
            double sumRateQty = 0.0;
            double sumDis = 0.0;
            double sumVat = 0.0;
            double sumCst = 0.0;
            double discount = 0;
            double vat = 0;
            double cst = 0;
            double rate = 0;
            double qty = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblQty = e.Row.FindControl("lblQty") as Label;
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblDisc = e.Row.FindControl("lblDisc") as Label;
                Label lblVat = e.Row.FindControl("lblVat") as Label;
                Label lblCst = e.Row.FindControl("lblCst") as Label;
                Label lblValue = e.Row.FindControl("lblValue") as Label;
                discount = Convert.ToDouble(lblDisc.Text);
                vat = Convert.ToDouble(lblVat.Text);
                cst = Convert.ToDouble(lblCst.Text);
                rate = Convert.ToDouble(lblRate.Text);
                qty = Convert.ToDouble(lblQty.Text);

                sumRateQty = rate * qty;
                sumDis = sumRateQty - (sumRateQty * (discount / 100));

                sumVat = sumDis * (vat / 100);
                sumCst = sumDis * (cst / 100);

                sumValue = sumDis + sumVat + sumCst;

                lblValue.Text = sumValue.ToString("f2"); // Convert.ToString(sumValue);


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
