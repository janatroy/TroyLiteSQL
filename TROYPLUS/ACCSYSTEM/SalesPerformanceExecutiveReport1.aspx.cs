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

public partial class SalesPerformanceExecutiveReport1 : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double amt = 0.0d;
    Double salesamt = 0.0d;
    Double salesreturnamt = 0.0d;
    public double sumComm = 0;
    public double sumComm2 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtEndDate.Text = dtaa;

            //txtEndDate.Text = DateTime.Now.ToShortDateString();



            ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
            divPrint.Visible = true;
            DateTime startDate, endDate;
            int iExecID = 0;
            if (drpIncharge.SelectedItem != null)
                iExecID = Convert.ToInt32(drpIncharge.SelectedItem.Value);

            DateTime stdt = Convert.ToDateTime(txtStartDate.Text);
            DateTime etdt = Convert.ToDateTime(txtEndDate.Text);

            if (Request.QueryString["startDate"] != null)
            {
                stdt = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
            }
            if (Request.QueryString["endDate"] != null)
            {
                etdt = Convert.ToDateTime(Request.QueryString["endDate"].ToString());
            }

            startDate = Convert.ToDateTime(stdt);
            endDate = Convert.ToDateTime(etdt);

            if (Request.QueryString["iExecID"] != null)
            {
                iExecID = Convert.ToInt32(Request.QueryString["iExecID"].ToString());
            }

            DataSet ds = new DataSet();
            if (iExecID != 0)
                ds = rpt.GenerateExecutiveSales(sDataSource, iExecID, startDate, endDate);
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Executives Found')", true);
                return;
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvSales.DataSource = ds;
                gvSales.DataBind();
            }
            else
            {
                lblSales.Text = "0.00";
                gvSales.DataSource = null;
                gvSales.DataBind();
            }
            //ds = rpt.GenerateExecutiveSalesReturn(sDataSource, iExecID,startDate, endDate);
            // if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //gvSalesReturn.DataSource = ds;
            //gvSalesReturn.DataBind();
            //}
            // else
            // {
            //     lblSalesReturn.Text = "0.00";
            //     gvSalesReturn.DataSource = null;
            //     gvSalesReturn.DataBind();
            // }
            lblSalesReturn.Text = "0.00";
            Double bal = 0.0d;
            bal = Convert.ToDouble(lblSales.Text) - Convert.ToDouble(lblSalesReturn.Text);
            lblBalance.Text = bal.ToString("#0.00");

            DataSet dsMain = rpt.GetLedgerExecutive(sDataSource, iExecID);
            DataSet dsItem = new DataSet();

            dsItem = rpt.GenerateSalesExecutiveItemwise(sDataSource, startDate, endDate, Convert.ToString(iExecID));
            gvSalesItemwise.DataSource = dsItem;
            gvSalesItemwise.DataBind();

            //string ledgerID = string.Empty;
            //if (dsMain != null)
            //{
            //    if (dsMain.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow maindr in dsMain.Tables[0].Rows)
            //        {
            //            if (maindr["LedgerID"] != null && maindr["LedgerID"].ToString() != "")
            //            {
            //                ledgerID = ledgerID + Convert.ToString(maindr["LedgerID"]) + ",";

            //            }

            //        }


            //        if (ledgerID != string.Empty)
            //        {
            //            ledgerID = ledgerID.Remove(ledgerID.LastIndexOf(',', ledgerID.Length - 1));
            //            dsItem = rpt.GenerateSalesExecutiveItemwise(sDataSource, startDate, endDate, ledgerID);
            //            gvSalesItemwise.DataSource = dsItem;
            //            gvSalesItemwise.DataBind();

            //            //dsItem = rpt.GenerateSalesReturnExecutiveItemwise(sDataSource, startDate, endDate, ledgerID);

            //            //gvSalesReturnItemwise.DataSource = dsItem;
            //            //gvSalesReturnItemwise.DataBind();

            //            double commision = 0;
            //            commision = Convert.ToDouble(lblTotalSalesComm.Text);//- Convert.ToDouble(hdSalesReturn.Value);

            //            lblTotalSalesComm.Text = commision.ToString("f2");
            //        }
            //    }
            //    else
            //    {
            //        gvSalesItemwise.DataSource = null;
            //        gvSalesItemwise.DataBind();
            //        gvSalesReturnItemwise.DataSource = null;
            //        gvSalesReturnItemwise.DataBind();
            //    }
            //}
            //else
            //{

            //    gvSalesItemwise.DataSource = null;
            //    gvSalesItemwise.DataBind();
            //    gvSalesReturnItemwise.DataSource = null;
            //    gvSalesReturnItemwise.DataBind();
            //}

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

             "attachment;filename=SalesPerformanceExecutive.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);


            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Sales - Dealerwise";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvSales);

            TableCell cell3 = new TableCell();

            cell3.Text = "&nbsp;";

            TableCell cell4 = new TableCell();

            cell4.Controls.Add(gvSalesReturn);

            TableCell cell5 = new TableCell();

            cell5.Text = "&nbsp;";

            TableCell cell6 = new TableCell();

            cell6.Text = "Sales - Itemwise";

            TableCell cell7 = new TableCell();

            cell7.Controls.Add(gvSalesItemwise);

            TableCell cell8 = new TableCell();

            cell8.Text = "&nbsp;";

            TableCell cell9 = new TableCell();

            cell9.Controls.Add(gvSalesReturnItemwise);

            TableCell cell10 = new TableCell();

            cell10.Text = "&nbsp;";

            TableCell cell11 = new TableCell();

            cell11.Text = "Total Sales : " + lblSales.Text;

            TableCell cell12 = new TableCell();

            cell12.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblSalesReturn.Text;

            TableCell cell13 = new TableCell();

            cell13.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblBalance.Text;

            TableCell cell14 = new TableCell();

            cell14.Text = "Total Sales Commision:" + lblTotalSalesComm.Text;

           

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
            ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
            divPrint.Visible = true;
            DateTime startDate, endDate;
            int iExecID = 0;
            if (drpIncharge.SelectedItem != null)
                iExecID = Convert.ToInt32(drpIncharge.SelectedItem.Value);
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            DataSet ds = new DataSet();
            if (iExecID != 0)
                ds = rpt.GenerateExecutiveSales(sDataSource, iExecID, startDate, endDate);
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Executives Found')", true);
                return;
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvSales.DataSource = ds;
                gvSales.DataBind();
            }
            else
            {
                lblSales.Text = "0.00";
                gvSales.DataSource = null;
                gvSales.DataBind();
            }
            //ds = rpt.GenerateExecutiveSalesReturn(sDataSource, iExecID,startDate, endDate);
            // if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //gvSalesReturn.DataSource = ds;
            //gvSalesReturn.DataBind();
            //}
            // else
            // {
            //     lblSalesReturn.Text = "0.00";
            //     gvSalesReturn.DataSource = null;
            //     gvSalesReturn.DataBind();
            // }
            lblSalesReturn.Text = "0.00";
            Double bal = 0.0d;
            bal = Convert.ToDouble(lblSales.Text) - Convert.ToDouble(lblSalesReturn.Text);
            lblBalance.Text = bal.ToString("#0.00");

            DataSet dsMain = rpt.GetLedgerExecutive(sDataSource, iExecID);
            DataSet dsItem = new DataSet();

            dsItem = rpt.GenerateSalesExecutiveItemwise(sDataSource, startDate, endDate, Convert.ToString(iExecID));
            gvSalesItemwise.DataSource = dsItem;
            gvSalesItemwise.DataBind();

            //string ledgerID = string.Empty;
            //if (dsMain != null)
            //{
            //    if (dsMain.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow maindr in dsMain.Tables[0].Rows)
            //        {
            //            if (maindr["LedgerID"] != null && maindr["LedgerID"].ToString() != "")
            //            {
            //                ledgerID = ledgerID + Convert.ToString(maindr["LedgerID"]) + ",";

            //            }

            //        }


            //        if (ledgerID != string.Empty)
            //        {
            //            ledgerID = ledgerID.Remove(ledgerID.LastIndexOf(',', ledgerID.Length - 1));
            //            dsItem = rpt.GenerateSalesExecutiveItemwise(sDataSource, startDate, endDate, ledgerID);
            //            gvSalesItemwise.DataSource = dsItem;
            //            gvSalesItemwise.DataBind();

            //            //dsItem = rpt.GenerateSalesReturnExecutiveItemwise(sDataSource, startDate, endDate, ledgerID);

            //            //gvSalesReturnItemwise.DataSource = dsItem;
            //            //gvSalesReturnItemwise.DataBind();

            //            double commision = 0;
            //            commision = Convert.ToDouble(lblTotalSalesComm.Text);//- Convert.ToDouble(hdSalesReturn.Value);

            //            lblTotalSalesComm.Text = commision.ToString("f2");
            //        }
            //    }
            //    else
            //    {
            //        gvSalesItemwise.DataSource = null;
            //        gvSalesItemwise.DataBind();
            //        gvSalesReturnItemwise.DataSource = null;
            //        gvSalesReturnItemwise.DataBind();
            //    }
            //}
            //else
            //{

            //    gvSalesItemwise.DataSource = null;
            //    gvSalesItemwise.DataBind();
            //    gvSalesReturnItemwise.DataSource = null;
            //    gvSalesReturnItemwise.DataBind();
            //}

            ExportToExcel();
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
            ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
            divPrint.Visible = true;
            DateTime startDate, endDate;
            int iExecID = 0;
            if (drpIncharge.SelectedItem != null)
                iExecID = Convert.ToInt32(drpIncharge.SelectedItem.Value);
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            DataSet ds = new DataSet();
            if (iExecID != 0)
                ds = rpt.GenerateExecutiveSales(sDataSource, iExecID, startDate, endDate);
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Executives Found')", true);
                return;
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvSales.DataSource = ds;
                gvSales.DataBind();
            }
            else
            {
                lblSales.Text = "0.00";
                gvSales.DataSource = null;
                gvSales.DataBind();
            }
            //ds = rpt.GenerateExecutiveSalesReturn(sDataSource, iExecID,startDate, endDate);
            // if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //gvSalesReturn.DataSource = ds;
            //gvSalesReturn.DataBind();
            //}
            // else
            // {
            //     lblSalesReturn.Text = "0.00";
            //     gvSalesReturn.DataSource = null;
            //     gvSalesReturn.DataBind();
            // }
            lblSalesReturn.Text = "0.00";
            Double bal = 0.0d;
            bal = Convert.ToDouble(lblSales.Text) - Convert.ToDouble(lblSalesReturn.Text);
            lblBalance.Text = bal.ToString("#0.00");

            DataSet dsMain = rpt.GetLedgerExecutive(sDataSource, iExecID);
            DataSet dsItem = new DataSet();

            dsItem = rpt.GenerateSalesExecutiveItemwise(sDataSource, startDate, endDate, Convert.ToString(iExecID));
            gvSalesItemwise.DataSource = dsItem;
            gvSalesItemwise.DataBind();

            //string ledgerID = string.Empty;
            //if (dsMain != null)
            //{
            //    if (dsMain.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow maindr in dsMain.Tables[0].Rows)
            //        {
            //            if (maindr["LedgerID"] != null && maindr["LedgerID"].ToString() != "")
            //            {
            //                ledgerID = ledgerID + Convert.ToString(maindr["LedgerID"]) + ",";

            //            }

            //        }


            //        if (ledgerID != string.Empty)
            //        {
            //            ledgerID = ledgerID.Remove(ledgerID.LastIndexOf(',', ledgerID.Length - 1));
            //            dsItem = rpt.GenerateSalesExecutiveItemwise(sDataSource, startDate, endDate, ledgerID);
            //            gvSalesItemwise.DataSource = dsItem;
            //            gvSalesItemwise.DataBind();

            //            //dsItem = rpt.GenerateSalesReturnExecutiveItemwise(sDataSource, startDate, endDate, ledgerID);

            //            //gvSalesReturnItemwise.DataSource = dsItem;
            //            //gvSalesReturnItemwise.DataBind();

            //            double commision = 0;
            //            commision = Convert.ToDouble(lblTotalSalesComm.Text);//- Convert.ToDouble(hdSalesReturn.Value);

            //            lblTotalSalesComm.Text = commision.ToString("f2");
            //        }
            //    }
            //    else
            //    {
            //        gvSalesItemwise.DataSource = null;
            //        gvSalesItemwise.DataBind();
            //        gvSalesReturnItemwise.DataSource = null;
            //        gvSalesReturnItemwise.DataBind();
            //    }
            //}
            //else
            //{

            //    gvSalesItemwise.DataSource = null;
            //    gvSalesItemwise.DataBind();
            //    gvSalesReturnItemwise.DataSource = null;
            //    gvSalesReturnItemwise.DataBind();
            //}

            div1.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        double amt = 0;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblValue = e.Row.FindControl("lblAmount") as Label;

                amt = Convert.ToDouble(lblValue.Text);
                salesamt = salesamt + amt;

                lblSales.Text = salesamt.ToString("f3");
                lblValue.Text = amt.ToString("f3");

                lblValue = e.Row.FindControl("lblCommission") as Label;
                amt = Convert.ToDouble(lblValue.Text);
                sumComm = sumComm + amt;
                lblTotalSalesComm.Text = sumComm.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvSalesReturn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //double amt = 0;

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblValue = e.Row.FindControl("lblAmount") as Label;
        //    amt = Convert.ToDouble(lblValue.Text);
        //    salesreturnamt = salesreturnamt + amt;
        //    lblSalesReturn.Text = salesreturnamt.ToString("f2");
        //    lblValue.Text = amt.ToString("f2");
        //}
    }
    protected void gvSalesItemwise_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //double amt = 0;
        //double comm = 0;
        //double commInput = 0;

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    Label lblValue = e.Row.FindControl("lblCommision") as Label;
        //    amt = Convert.ToDouble(lblValue.Text);
        //    salesamt = salesreturnamt + amt;
        //    lblTotalSalesComm.Text = salesamt.ToString("f2");
        //    lblValue.Text = amt.ToString("f2");
        //    //Label lblValue = e.Row.FindControl("lblAmount") as Label;
        //    //Label lblComm = e.Row.FindControl("lblCommision") as Label;
        //    //amt = Convert.ToDouble(lblValue.Text);
        //    //if (txtCommistion.Text.Trim() != "")
        //    //{
        //    //    commInput = Convert.ToDouble(txtCommistion.Text.Trim());

        //    //    comm = amt * (Convert.ToDouble(commInput) / 100);
        //    //}
        //    //salesamt = salesamt + amt;

        //    ////lblComm.Text = comm.ToString("f2");

        //    //sumComm = sumComm + comm;
        //    //lblTotalSalesComm.Text = sumComm.ToString();
        //    //lblValue.Text = amt.ToString("f2");
        //}
    }
    protected void gvSalesReturnItemwise_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //double amt = 0;
        //double comm = 0;
        //double commInput = 0;

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    Label lblValue = e.Row.FindControl("lblAmount") as Label;
        //    Label lblComm = e.Row.FindControl("lblCommision") as Label;
        //    amt = Convert.ToDouble(lblValue.Text);
        //    if (txtCommistion.Text.Trim() != "")
        //    {
        //        commInput = Convert.ToDouble(txtCommistion.Text.Trim());

        //        comm = amt * (Convert.ToDouble(commInput) / 100);
        //    }
        //    salesamt = salesamt + amt;

        //    lblComm.Text = comm.ToString("f2");

        //    sumComm2 = sumComm2 + comm;
        //    hdSalesReturn.Value = sumComm2.ToString();
        //    lblValue.Text = amt.ToString("f2");

        //}
    }
}
