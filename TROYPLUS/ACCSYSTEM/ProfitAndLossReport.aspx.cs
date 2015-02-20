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

public partial class ProfitAndLossReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!IsPostBack)
            {
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtEndDate.Text = dtaa;

                //txtEndDate.Text = DateTime.Now.ToShortDateString();
            }
            // GeneratePL();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    /*March 17*/
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                /* March17 */
                DateTime startDate, endDate;
                startDate = Convert.ToDateTime(txtStartDate.Text);
                endDate = Convert.ToDateTime(txtEndDate.Text);
                /* March17 */
                //GeneratePL();
                divPrint.Visible = false;
                divmain.Visible = false;
                div1.Visible = true;

                Response.Write("<script language='javascript'> window.open('ProfitAndLossReport1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
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
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            /* March17 */
            GeneratePL();

            //if (assetDs != null)
            //{
            //    if (assetDs.Tables[0].Rows.Count > 0)
            //    {
            //        assetDs.Tables[0].Rows[0].Delete();
            //        //gvAssetBalance.DataSource = assetDs;
            //        //gvAssetBalance.DataBind();
            //        foreach (DataRow sumDr in assetDs.Tables[0].Rows)
            //        {
            //            assTot = assTot + Convert.ToDouble(sumDr["sum"]);
            //        }
            //    }
            //}
            //if (liabilityDs != null)
            //{
            //    if (liabilityDs.Tables[0].Rows.Count > 0)
            //    {
            //        liabilityDs.Tables[0].Rows[0].Delete();
            //        //gvLiabilityBalance.DataSource = liabilityDs;
            //        //gvLiabilityBalance.DataBind();
            //        foreach (DataRow sumCr in liabilityDs.Tables[0].Rows)
            //        {
            //            liaTot = liaTot + Convert.ToDouble(sumCr["sum"]);
            //        }
            //    }
            //}

            ExportToExcel();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void gvIDirectExp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvIDirectExp.PageIndex = e.NewPageIndex;
    //    ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();

    //    gvIDirectExp.DataSource = rpt.plGetExpenseIncomeSplit(sDataSource, "IDX");
    //    gvIDirectExp.DataBind();
    //}

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

             "attachment;filename=Profit and Loss.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);


            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Direct Expenses";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvDirectExp);

            TableCell cell3 = new TableCell();

            cell3.Text = "&nbsp;";

            TableCell cell4 = new TableCell();

            cell4.Text = "Direct Income";

            TableCell cell5 = new TableCell();

            cell5.Controls.Add(gvDirectInc);

            TableCell cell6 = new TableCell();

            cell6.Text = "&nbsp;";

            TableCell cell7 = new TableCell();

            cell7.Text = "Indirect Expenses";

            TableCell cell8 = new TableCell();

            cell8.Controls.Add(gvIDirectExp);

            TableCell cell9 = new TableCell();

            cell9.Text = "&nbsp;";

            TableCell cell10 = new TableCell();

            cell10.Text = "Income Income";

            TableCell cell11 = new TableCell();

            cell11.Controls.Add(gvIDirectInc);

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

            tb.RenderControl(hw);



            //style to format numbers to string

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

    public void GeneratePL()
    {
        double openingStockTotal = 0;
        double purchaseTot = 0.0;
        double purchaseReturnTot = 0.0;
        double salesTot = 0.0;
        double salesReturnTot = 0.0;
        double DExpensesTot = 0.0d;
        double IDExpensesTot = 0.0d;
        double closingstockTotal = 0.0;
        double DIncomeTot = 0.0d;
        double IDIncomeTot = 0.0d;
        double dGp = 0.0;
        double dGl = 0.0;
        double gPurchase = 0.0;
        double gSales = 0.0;
        double totExpense = 0.0;
        double totIncome = 0.0;
        double grProfitLoss = 0.0;
        double netProfitLoss = 0.0;
        /* March17 */
        DataSet purchaseRateDs = GetPurchaseForSales();
        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        DataRow[] SelectDataRow = purchaseRateDs.Tables[0].Select("SalesBillDate>='" + startDate + "' and SalesBillDate<='" + endDate + "'");
        /* March17 */
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        openingStockTotal = bl.GetPurchaseOpeningValue(startDate);  //rpt.plOpeningStock(sDataSource);
        //closingstockTotal = openingStockTotal + (bl.GetPurchaseBetween(startDate, endDate) -    // rpt.plGetClosingStockTotal(sDataSource); 
        double sumRate = 0;
        double sumQty = 0;
        double netRate = 0;
        double netQty = 0;
        double sumNet = 0;
        foreach (DataRow dr in SelectDataRow)
        {
            if (dr["PurchaseRate"] != null)
            {
                sumRate = Convert.ToDouble(dr["PurchaseRate"]);
            }
            if (dr["SalesQty"] != null)
            {
                sumQty = Convert.ToDouble(dr["SalesQty"]);
            }
            netRate = sumRate * sumQty;
            sumNet = sumNet + netRate;
        }

        salesReturnTot = bl.GetSalesReturnBetween(startDate, endDate);//rpt.plSalesReturnTotal(sDataSource);
        closingstockTotal = openingStockTotal + (bl.GetPurchaseBetween(startDate, endDate) - sumNet) + salesReturnTot;   // rpt.plGetClosingStockTotal(sDataSource); 
        purchaseTot = bl.GetPurchaseBetween(startDate, endDate); //rpt.plPurchaseTotal(sDataSource);

        purchaseReturnTot = bl.GetPurchaseReturnBetween(startDate, endDate); // rpt.plGetPurchaseReturnTotal(sDataSource);
        salesTot = bl.GetSalesBetween(startDate, endDate);  // rpt.plGetSalesTotal(sDataSource);


        DExpensesTot = bl.GetExpenseIncomeTotal(startDate, endDate, "DX"); // rpt.plGetExpenseIncomeTotal(sDataSource, "DX");
        IDExpensesTot = bl.GetExpenseIncomeTotal(startDate, endDate, "IDX");// rpt.plGetExpenseIncomeTotal(sDataSource, "IDX");
        DIncomeTot = bl.GetExpenseIncomeTotal(startDate, endDate, ""); // rpt.plGetExpenseIncomeTotal(sDataSource, "");
        IDIncomeTot = bl.GetExpenseIncomeTotal(startDate, endDate, "IDI"); //rpt.plGetExpenseIncomeTotal(sDataSource, "IDI");

        gPurchase = purchaseTot - purchaseReturnTot;
        gSales = salesTot - salesReturnTot;

        totExpense = gPurchase + (DExpensesTot - gPurchase);// +openingStockTotal;
        //totExpense = DExpensesTot + openingStockTotal;
        totIncome = gSales + (DIncomeTot - gSales); //+ closingstockTotal;
        grProfitLoss = totIncome - totExpense;


        if (grProfitLoss > 0)
            dGp = totIncome - totExpense;
        else
            dGl = totExpense - totIncome;


        lblGP.Text = dGp.ToString("f2");
        lblGL.Text = dGl.ToString("f2");
        if (dGp > 0)
        {
            netProfitLoss = dGp + (IDIncomeTot - IDExpensesTot);
            if (netProfitLoss > 0)
            {
                lblNetProfit.Text = netProfitLoss.ToString("f2");
                lblNetLoss.Text = "";
            }
            else
            {
                lblNetLoss.Text = Math.Abs(netProfitLoss).ToString("f2");
                lblNetProfit.Text = "";
            }
        }
        else
        {
            netProfitLoss = dGl + (IDExpensesTot - IDIncomeTot);
            if (netProfitLoss > 0)
                lblNetLoss.Text = Math.Abs(netProfitLoss).ToString("f2");
            else
                lblNetProfit.Text = netProfitLoss.ToString("f2");
        }

        lblOpeningStock.Text = openingStockTotal.ToString("f2");
        lblClosingStock.Text = closingstockTotal.ToString("f2");

        lblPurchaseTotal.Text = purchaseTot.ToString("f2");
        lblPurchaseReturnTotal.Text = purchaseReturnTot.ToString("f2");
        lblSalesTotal.Text = salesTot.ToString("f2");
        lblSalesReturnTotal.Text = salesReturnTot.ToString("f2");

        lblDXTotal.Text = (DExpensesTot - salesReturnTot).ToString("f2");
        gvIDirectExp.DataSource = rpt.plGetExpenseIncomeSplit(sDataSource, "IDX");
        gvIDirectExp.DataBind();

        gvDirectExp.DataSource = rpt.plGetExpenseIncomeSplit(sDataSource, "DX");
        gvDirectExp.DataBind();

        gvDirectInc.DataSource = rpt.plGetExpenseIncomeSplit(sDataSource, " ");
        gvDirectInc.DataBind();

        gvIDirectInc.DataSource = rpt.plGetExpenseIncomeSplit(sDataSource, "IDI");
        gvIDirectInc.DataBind();

        lblIDXExptotal.Text = IDExpensesTot.ToString("f2");

        lblDIncome.Text = (DIncomeTot - salesReturnTot).ToString("f2");
        lblIDIncome.Text = IDIncomeTot.ToString("f2");


        lblFirstMidTotal.Text = gPurchase.ToString("f2");
        lblSecondMidTotal.Text = gSales.ToString("f2");
        DataSet ds = new DataSet();

    }


    public DataSet GetPurchaseForSales()
    {
        double salesQty = 0;
        string salesItemCode = string.Empty;
        double salesRate = 0;
        double salesDiscount = 0;
        double salesVat = 0;
        double salesCst = 0;
        double salesLoading = 0;
        double salesFreight = 0;

        string salesExecutive = string.Empty;
        int salesCustomerID = 0;
        string salesCustomer = string.Empty;
        /* March17 */
        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        DateTime salesBillDate = new DateTime();
        /* March17 */
        int purchaseCnt = 0;
        DataRow[] SearchProductRow;
        DataRow[] LFRrow;
        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string company = string.Empty;
        DataSet salesDs = bl.GetAllSales();
        DataSet productDs = new DataSet();
        DataSet dsCharges = new DataSet();
        DataSet purchaseDs = bl.GetAllPurchase();
        DataSet openingStockDs = bl.GetAllOpeningStock();
        productDs = bl.GetProductGPForId();
        dsCharges = bl.GetPurchaseChargesTotal();
        int opCnt = 0;
        double opQty = 0;
        double purchaseQty = 0;
        DataSet GrossProfitDs = new DataSet();
        DataTable dt;
        DataRow dr;
        DataColumn dc, dateDc;
        DateTime billDate = new DateTime();
        try
        {
            dt = new DataTable();



            dc = new DataColumn("ItemCode");
            dt.Columns.Add(dc);


            dc = new DataColumn("SalesQty");
            dt.Columns.Add(dc);

            dateDc = new DataColumn("SalesBillDate");
            dateDc.DataType = Type.GetType("System.DateTime");
            dt.Columns.Add(dateDc);



            dc = new DataColumn("PurchaseRate");
            dt.Columns.Add(dc);



            GrossProfitDs.Tables.Add(dt);



            if (salesDs != null)
            {
                if (salesDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow SalesRow in salesDs.Tables[0].Rows)
                    {
                        salesQty = 0;
                        salesItemCode = string.Empty;


                        opCnt = 0;
                        if (SalesRow["ItemCode"] != null)
                        {
                            salesItemCode = Convert.ToString(SalesRow["ItemCode"]).Trim();
                        }
                        if (SalesRow["Qty"] != null)
                        {
                            salesQty = Convert.ToDouble(SalesRow["Qty"]);
                        }
                        if (SalesRow["BillDate"] != null)
                        {
                            salesBillDate = Convert.ToDateTime(SalesRow["BillDate"]);
                        }



                        dr = GrossProfitDs.Tables[0].NewRow();

                        dr["ItemCode"] = salesItemCode;





                        dr["SalesBillDate"] = salesBillDate;



                        if (Request.Cookies["Company"] != null)
                            company = Request.Cookies["Company"].Value;
                        if (openingStockDs != null)
                        {
                            foreach (DataRow OpRow in openingStockDs.Tables[0].Rows)
                            {
                                if (salesItemCode.Trim() == Convert.ToString(OpRow["ItemCode"]).Trim())
                                {
                                    opQty = Convert.ToDouble(OpRow["OpeningStock"]);
                                    if (opQty > 0)
                                    {
                                        if (salesQty >= opQty)
                                        {

                                            /* Start March 17*/
                                            SearchProductRow = productDs.Tables[0].Select("Itemcode='" + salesItemCode.Trim() + "'");
                                            /* End March 17*/
                                            salesQty = salesQty - opQty;
                                            if (SearchProductRow != null)
                                            {

                                                dr["SalesQty"] = opQty.ToString();
                                                if (SearchProductRow[0]["Rate"] != null)
                                                {
                                                    if (SearchProductRow[0]["Rate"] != DBNull.Value)
                                                    {
                                                        if (Convert.ToString(SearchProductRow[0]["Rate"]) != "")
                                                            dr["PurchaseRate"] = Convert.ToString(SearchProductRow[0]["Rate"]);
                                                        else
                                                            dr["PurchaseRate"] = "0";
                                                    }
                                                }

                                                GrossProfitDs.Tables[0].Rows.Add(dr);

                                                //bl.InsertGP(dr);


                                            }
                                            /* End March 17*/

                                            opQty = 0;
                                            openingStockDs.Tables[0].Rows[opCnt].BeginEdit();
                                            openingStockDs.Tables[0].Rows[opCnt]["OpeningStock"] = opQty;
                                            openingStockDs.Tables[0].Rows[opCnt].EndEdit();
                                            openingStockDs.Tables[0].Rows[opCnt].AcceptChanges();
                                        }
                                        else
                                        {

                                            /* Start March 17*/
                                            SearchProductRow = productDs.Tables[0].Select("Itemcode='" + salesItemCode.Trim() + "'");
                                            /* End March 17*/
                                            opQty = opQty - salesQty;
                                            dr["SalesQty"] = salesQty.ToString();

                                            if (SearchProductRow[0]["Rate"] != null)
                                            {
                                                if (SearchProductRow[0]["Rate"] != DBNull.Value)
                                                {
                                                    if (Convert.ToString(SearchProductRow[0]["Rate"]) != "")
                                                        dr["PurchaseRate"] = Convert.ToString(SearchProductRow[0]["Rate"]);
                                                    else
                                                        dr["PurchaseRate"] = "0";
                                                }
                                            }


                                            GrossProfitDs.Tables[0].Rows.Add(dr);

                                            //bl.InsertGP(dr);

                                            openingStockDs.Tables[0].Rows[opCnt].BeginEdit();
                                            openingStockDs.Tables[0].Rows[opCnt]["OpeningStock"] = opQty;
                                            openingStockDs.Tables[0].Rows[opCnt].EndEdit();
                                            openingStockDs.Tables[0].Rows[opCnt].AcceptChanges();
                                            salesQty = 0;
                                            /* End March 17*/
                                        }
                                    }
                                    break;
                                }
                                opCnt = opCnt + 1;
                            } //OPening Stock Row
                        }
                        if (salesQty > 0)
                        {

                            if (purchaseDs != null)
                            {
                                if (purchaseDs.Tables[0].Rows.Count > 0)
                                {

                                    foreach (DataRow purchaseRow in purchaseDs.Tables[0].Rows)
                                    {
                                        if (salesQty > 0)
                                        {

                                            if (salesItemCode.Trim() == Convert.ToString(purchaseRow["ItemCode"]).Trim())
                                            {
                                                purchaseQty = Convert.ToDouble(purchaseRow["Qty"]);
                                                if (purchaseQty > 0)
                                                {

                                                    dr = GrossProfitDs.Tables[0].NewRow();

                                                    dr["ItemCode"] = salesItemCode;
                                                    dr["SalesBillDate"] = salesBillDate;

                                                    if (salesQty >= purchaseQty)
                                                    {

                                                        salesQty = salesQty - purchaseQty;
                                                        dr["SalesQty"] = purchaseQty.ToString();

                                                        dr["PurchaseRate"] = Convert.ToString(purchaseRow["PurchaseRate"]);

                                                        /*Start March 17*/


                                                        /*End March 17*/
                                                        GrossProfitDs.Tables[0].Rows.Add(dr);
                                                        /*Start March 17*/
                                                        //bl.InsertGP(dr);
                                                        /*End March 17*/
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].BeginEdit();
                                                        purchaseDs.Tables[0].Rows[purchaseCnt]["Qty"] = "0";
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].EndEdit();
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].AcceptChanges();
                                                        //purchaseDs.Tables[0].Rows[purchaseCnt].Delete();
                                                        purchaseCnt = purchaseCnt + 1;
                                                    }
                                                    else
                                                    {
                                                        purchaseQty = purchaseQty - salesQty;
                                                        dr["SalesQty"] = salesQty.ToString();

                                                        dr["PurchaseRate"] = Convert.ToString(purchaseRow["PurchaseRate"]);

                                                        /*End March 17*/
                                                        GrossProfitDs.Tables[0].Rows.Add(dr);
                                                        /*Start March 17*/
                                                        //bl.InsertGP(dr);
                                                        /*End March 17*/
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].BeginEdit();
                                                        purchaseDs.Tables[0].Rows[purchaseCnt]["Qty"] = purchaseQty;
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].EndEdit();
                                                        purchaseDs.Tables[0].Rows[purchaseCnt].AcceptChanges();
                                                        salesQty = 0;
                                                        break;
                                                    }
                                                    //GrossProfitDs.Tables[0].Rows.Add(dr);
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            break;
                                        }

                                    } //Purchase Row


                                }
                            }



                        }

                    } //Sales Row 
                }
            }
            //bl.InsertGP(GrossProfitDs);

            return GrossProfitDs;
        }
        catch (Exception ex)
        {
            throw ex;
            //lblMessage.Text = ex.Message;
            return GrossProfitDs;
        }
    }
}

