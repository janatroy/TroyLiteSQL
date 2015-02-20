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
using System.Text;
using System.IO;

public partial class VATReprt : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double grossPurchaseTotal = 0.0d;
    Double grossVatTotal = 0.0d;
    Double grossTotal = 0.0d;
    Double grossPurchaseReturnTotal = 0.0d;
    Double grossSalesTotal = 0.0d;
    Double grossSalesReturnTotal = 0.0d;

    Double grossSalReturnTotal = 0.0d;
    Double grossVSalesTotal = 0.0d;
    Double grossReturnTotal = 0.0d;
    Double grossVatReturnTotal = 0.0d;
    Double grossVatSalReturnTotal = 0.0d;
    Double grossVatSalesTotal = 0.0d;

    public string vatPurchaseString = string.Empty;
    public string vatSalesString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

                if (Session["startDate"] != null)
                {
                    txtStartDate.Text = Session["startDate"].ToString();
                }
                if (Session["endDate"] != null)
                {
                    txtEndDate.Text = Session["endDate"].ToString();
                }

                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
                DataSet ds = new DataSet();
                DataSet salesDs = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                ds = bl.avlVAT("purchase", sDataSource);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        vatPurchaseString = vatPurchaseString + dr["VAT"].ToString() + ",";
                    }
                }
                if (vatPurchaseString.EndsWith(","))
                {
                    vatPurchaseString = vatPurchaseString.Remove(vatPurchaseString.Length - 1, 1);
                }

                salesDs = bl.avlVAT("sales", sDataSource);
                if (salesDs != null && salesDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drS in salesDs.Tables[0].Rows)
                    {
                        vatSalesString = vatSalesString + drS["VAT"].ToString() + ",";
                    }
                }
                if (vatSalesString.EndsWith(","))
                {
                    vatSalesString = vatSalesString.Remove(vatSalesString.Length - 1, 1);
                }
                hdPurchase.Value = vatPurchaseString;
                hdSales.Value = vatSalesString;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
            DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime endDate = Convert.ToDateTime(txtEndDate.Text);
            DataSet dsPurchase = new DataSet();
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            //DataSet salesReturnDs = rpt.generateSalesVATReport(sDataSource, startDate, endDate, "Yes");

            dsPurchase.Tables.Add(rpt.generatePurchaseVATTable(sDataSource, startDate, endDate, "Yes"));

            StringBuilder msg = new StringBuilder();

            ArrayList dsValidation = rpt.getMissingCommodityCodes(sDataSource, startDate, endDate, "Yes");

            if (dsValidation.Count > 0)
            {
                msg.Append("Please enter the commodity code for the following Products: ");
                msg.Append("\\n");

                int x = 0;

                foreach (string item in dsValidation)
                {
                    x = x + 1;
                    msg.Append(x + ". " + item);
                    msg.Append("\\n");
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg.ToString() + "');", true);
                return;
            }

            DataTable salesTable = rpt.generateSalesVATTable(sDataSource, startDate, endDate, "Yes");
            //ordersTable.TableName = "tea";
            dsPurchase.Tables.Add(salesTable);
            ExcelHelper.ToExcel(dsPurchase, "VATReport.xls", Page.Response);

            /*StringWriter tw = new StringWriter();
            Html32TextWriter hw = new Html32TextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dsExport;
            hw.WriteLine("<b><u><font size='5'> Student Marking Report </font></u></b>");
 
            dgGrid.HeaderStyle.Font.Bold = true;
            dgGrid.DataBind();
            dgGrid.RenderControl(hw);
 
            Response.ContentType = "application/vnd.ms-excel";
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();*/

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public System.IO.StringWriter ExportToExcelXML(DataSet source)
    {
        System.IO.StringWriter excelDoc;



        excelDoc = new System.IO.StringWriter();

        StringBuilder ExcelXML = new StringBuilder();

        ExcelXML.Append("<xml version>\r\n<Workbook ");

        ExcelXML.Append(
            "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n");

        ExcelXML.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n ");

        ExcelXML.Append("xmlns:x=\"urn:schemas- microsoft-com:office:");

        ExcelXML.Append("excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:");

        ExcelXML.Append("office:spreadsheet\">\r\n <Styles>\r\n ");

        ExcelXML.Append("<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n ");

        ExcelXML.Append("<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>");

        ExcelXML.Append("\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>");

        ExcelXML.Append("\r\n <Protection/>\r\n </Style>\r\n ");

        ExcelXML.Append("<Style ss:ID=\"BoldColumn\">\r\n <Font ");

        ExcelXML.Append("x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n ");

        ExcelXML.Append("<Style ss:ID=\"StringLiteral\">\r\n <NumberFormat");

        ExcelXML.Append(" ss:Format=\"@\"/>\r\n </Style>\r\n <Style ");

        ExcelXML.Append("ss:ID=\"Decimal\">\r\n <NumberFormat ");

        ExcelXML.Append("ss:Format=\"0.0000\"/>\r\n </Style>\r\n ");

        ExcelXML.Append("<Style ss:ID=\"Integer\">\r\n <NumberFormat ");

        ExcelXML.Append("ss:Format=\"0\"/>\r\n </Style>\r\n <Style ");

        ExcelXML.Append("ss:ID=\"DateLiteral\">\r\n <NumberFormat ");
        ExcelXML.Append("ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n ");

        ExcelXML.Append("<Style ss:ID=\"s28\">\r\n");

        ExcelXML.Append("<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Top\" ss:ReadingOrder=\"LeftToRight\" ss:WrapText=\"1\"/>\r\n");

        ExcelXML.Append("<Font x:CharSet=\"1\" ss:Size=\"9\" ss:Color=\"#808080\" ss:Underline=\"Single\"/>\r\n");

        ExcelXML.Append("<Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/></Style>\r\n");

        ExcelXML.Append("</Styles>\r\n ");

        string startExcelXML = ExcelXML.ToString();

        const string endExcelXML = "</Workbook>";

        int rowCount = 0;

        int sheetCount = 1;

        excelDoc.Write(startExcelXML);

        excelDoc.Write("<Worksheet ss:Name=\"Report_Sheet" + sheetCount + "\">");

        excelDoc.Write("<Table>");

        ///Header Part
        // Add any Header for the report
        ///

        excelDoc.Write("<Row ss:AutoFitHeight=\"0\" ss:Height=\"6.75\"/>\r\n");

        excelDoc.Write("<Row><Cell ss:MergeAcross=\"10\" ss:StyleID=\"s34\"><Data ss:Type=\"String\">");

        excelDoc.Write("HEADER TEXT");

        excelDoc.Write("</Data></Cell>");

        excelDoc.Write("<Cell ss:MergeAcross=\"1\" ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");

        excelDoc.Write("Report Date");

        excelDoc.Write("</Data></Cell>");

        excelDoc.Write("<Cell ss:MergeAcross=\"1\" ss:StyleID=\"DateLiteral\"><Data ss:Type=\"String\">");

        excelDoc.Write(DateTime.Now.ToShortDateString());

        excelDoc.Write("</Data></Cell></Row>");

        excelDoc.Write("<Row ss:AutoFitHeight=\"0\" ss:Height=\"10\"/>\r\n");

        ///Complete

        excelDoc.Write("<Row>");

        for (int x = 0; x < source.Tables[0].Columns.Count; x++)
        {

            excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");

            excelDoc.Write(source.Tables[0].Columns[x].ColumnName);

            excelDoc.Write("</Data></Cell>");

        }

        excelDoc.Write("</Row>");

        foreach (DataRow x in source.Tables[0].Rows)
        {

            rowCount++;

            //if the number of rows is > 63000 create a new page to continue output

            if (rowCount == 63000)
            {

                rowCount = 0;

                sheetCount++;

                excelDoc.Write("</Table>");

                excelDoc.Write(" </Worksheet>");

                excelDoc.Write("<Worksheet ss:Name=\"Report_Sheet" + sheetCount + "\">");

                excelDoc.Write("<Table>");



                excelDoc.Write("<Row>");

                for (int xi = 0; xi < source.Tables[0].Columns.Count; xi++)
                {

                    excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");

                    excelDoc.Write(source.Tables[0].Columns[xi].ColumnName);

                    excelDoc.Write("</Data></Cell>");

                }

                excelDoc.Write("</Row>");

            }

            excelDoc.Write("<Row>");

            for (int y = 0; y < source.Tables[0].Columns.Count; y++)
            {

                string XMLstring = x[y].ToString();

                XMLstring = XMLstring.Trim();

                XMLstring = XMLstring.Replace("&", "&");

                XMLstring = XMLstring.Replace(">", ">");

                XMLstring = XMLstring.Replace("<", "<");

                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" + "<Data ss:Type=\"String\">");

                excelDoc.Write(XMLstring);

                excelDoc.Write("</Data></Cell>");

            }

            excelDoc.Write("</Row>");

        }

        ///Ending Tag

        ///

        excelDoc.Write("<Row ss:Height=\"15\"><Cell ss:HRef=www.sachinkumark.com\\ss:MergeAcross=\"2\" ss:StyleID=\"s28\"><Data ss:Type=\"String\">");

        excelDoc.Write("www.sachinkumark.com");

        excelDoc.Write("</Data></Cell></Row>");

        excelDoc.Write("<Row ss:Height=\"15\"><Cell ss:MergeAcross=\"6\" ss:StyleID=\"s28\"><Data ss:Type=\"String\">");

        excelDoc.Write("Copyright © 2007");

        excelDoc.Write("</Data></Cell></Row>");

        ///Complete

        excelDoc.Write("</Table>");

        excelDoc.Write(" </Worksheet>");


        excelDoc.Write(endExcelXML);

        return excelDoc;




    }

    //

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            dvVAT.InnerHtml = "";
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            DateTime startDate, endDate;
            DataSet salesDs = new DataSet();
            DataSet salesReturnDs = new DataSet();
            DataSet purchaseDs = new DataSet();
            DataSet purchaseReturnDs = new DataSet();

            divPrint.Visible = true;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
            purchaseDs = rpt.generatePurchaseVATReport(sDataSource, startDate, endDate, "Yes");
            salesReturnDs = rpt.generatePurchaseVATReport(sDataSource, startDate, endDate, "No");
            salesDs = rpt.generateSalesVATReport(sDataSource, startDate, endDate, "Yes");
            purchaseReturnDs = rpt.generateSalesVATReport(sDataSource, startDate, endDate, "No");

            if (salesDs != null && salesDs.Tables[0].Rows.Count > 0)
            {

                grdSalesVAT.DataSource = salesDs;
                grdSalesVAT.DataBind();

            }
            else
            {
                grdSalesVAT.DataSource = null;
                grdSalesVAT.DataBind();
            }
            if (salesReturnDs != null && salesReturnDs.Tables[0].Rows.Count > 0)
            {

                grdSalesReturnVAT.DataSource = salesReturnDs;
                grdSalesReturnVAT.DataBind();

            }
            else
            {
                grdSalesReturnVAT.DataSource = null;
                grdSalesReturnVAT.DataBind();
            }
            if (purchaseDs != null && purchaseDs.Tables[0].Rows.Count > 0)
            {

                grdPurchaseVat.DataSource = purchaseDs;
                grdPurchaseVat.DataBind();

            }
            else
            {
                grdPurchaseVat.DataSource = null;
                grdPurchaseVat.DataBind();
            }
            if (purchaseReturnDs != null && purchaseReturnDs.Tables[0].Rows.Count > 0)
            {

                grdPurchaseReturnVAT.DataSource = purchaseReturnDs;
                grdPurchaseReturnVAT.DataBind();

            }
            else
            {
                grdPurchaseReturnVAT.DataSource = null;
                grdPurchaseReturnVAT.DataBind();
            }

            vatSum(purchaseDs, hdPurchase.Value, "Purchase");
            vatSum(salesDs, hdSales.Value, "Sales");
            vatSum(purchaseReturnDs, hdSales.Value, "Purchase Return");
            vatSum(salesReturnDs, hdPurchase.Value, "Sales Return");
            double salesVat = 0;
            double purchaseVat = 0;
            double salesReturnVat = 0;
            double purchaseReturnVat = 0;
            if (sumSales.Text != "")
                salesVat = Convert.ToDouble(sumSales.Text);
            if (sumPurchase.Text != "")
                purchaseVat = Convert.ToDouble(sumPurchase.Text);
            if (sumSalesReturn.Text != "")
                salesReturnVat = Convert.ToDouble(sumSalesReturn.Text);
            if (sumPurchaseReturn.Text != "")
                purchaseReturnVat = Convert.ToDouble(sumPurchaseReturn.Text);

            double netVat = ((salesVat - salesReturnVat) - (purchaseVat - purchaseReturnVat));
            sumVatToPay.Text = netVat.ToString("f2");
            Session["startDate"] = txtStartDate.Text;
            Session["endDate"] = txtEndDate.Text;
            pnlContent.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void vatSum(DataSet ds, string vatStr, string title)
    {
        string expr = string.Empty;
        double sVAT = 0.0;
        DataRow[] pT;
        if (vatStr != string.Empty)
        {
            string[] arr = vatStr.Split(',');
            dvVAT.InnerHtml = dvVAT.InnerHtml + "<br><h5>" + title + "</h5>";
            for (int k = 0; k <= arr.Length - 1; k++)
            {
                if (arr[k].ToString() != "" && arr[k].ToString() != "0")
                {
                    expr = "VAT=" + arr[k];
                    pT = ds.Tables[0].Select(expr);
                    if (pT.Length > 0)
                    {
                        sVAT = 0;
                        foreach (DataRow dr in pT)
                        {
                            sVAT = sVAT + (Convert.ToDouble(dr["Vatrate"]) - Convert.ToDouble(dr["rate"]));
                        }
                        dvVAT.InnerHtml = dvVAT.InnerHtml + arr[k] + "% -  Total Amount Paid : " + sVAT.ToString("f2") + "<br>";
                    }
                }

            }

        }
    }
    public void grdPurchaseVat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double grossVat = 0.0d;
            //double Rate = 0.0d;
            //double VatRate = 0.0d;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblVatRate = e.Row.FindControl("lblVatRate") as Label;
                Label lblVatPaid = e.Row.FindControl("lblVatPaid") as Label;

                if (lblRate != null && lblVatRate != null)
                    grossVat = Convert.ToDouble(lblVatRate.Text) - Convert.ToDouble(lblRate.Text);


                grossVatTotal = grossVatTotal + Convert.ToDouble(lblVatRate.Text);
                grossTotal = grossTotal + Convert.ToDouble(lblRate.Text);

                grossPurchaseTotal = grossPurchaseTotal + grossVat;
                lblVatPaid.Text = grossVat.ToString("f2");


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossTotal = e.Row.FindControl("lblGrossTotal") as Label;
                Label lblGrossVatRate = e.Row.FindControl("lblGrossVatRate") as Label;
                Label lblGrossRate = e.Row.FindControl("lblGrossRate") as Label;

                lblGrossTotal.Text = grossPurchaseTotal.ToString("f2");
                sumPurchase.Text = lblGrossTotal.Text;

                lblGrossVatRate.Text = grossVatTotal.ToString("f2");
                lblGrossRate.Text = grossTotal.ToString("f2");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void grdPurchaseReturnVAT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double grossVat = 0.0d;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblVatRate = e.Row.FindControl("lblVatRate") as Label;
                Label lblVatPaid = e.Row.FindControl("lblVatPaid") as Label;

                if (lblRate != null && lblVatRate != null)
                    grossVat = Convert.ToDouble(lblVatRate.Text) - Convert.ToDouble(lblRate.Text);

                grossVatReturnTotal = grossVatReturnTotal + Convert.ToDouble(lblVatRate.Text);
                grossReturnTotal = grossReturnTotal + Convert.ToDouble(lblRate.Text);

                grossPurchaseReturnTotal = grossPurchaseReturnTotal + grossVat;
                lblVatPaid.Text = grossVat.ToString("f2");

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossTotal = e.Row.FindControl("lblGrossTotal") as Label;
                Label lblGrossVatRate = e.Row.FindControl("lblGrossVatRate") as Label;
                Label lblGrossRate = e.Row.FindControl("lblGrossRate") as Label;

                lblGrossTotal.Text = grossPurchaseReturnTotal.ToString("f2");
                sumPurchaseReturn.Text = lblGrossTotal.Text;

                lblGrossVatRate.Text = grossVatReturnTotal.ToString("f2");
                lblGrossRate.Text = grossReturnTotal.ToString("f2");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void grdSalesVAT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double grossVat = 0.0d;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblVatRate = e.Row.FindControl("lblVatRate") as Label;
                Label lblVatPaid = e.Row.FindControl("lblVatPaid") as Label;

                if (lblRate != null && lblVatRate != null)
                    grossVat = Convert.ToDouble(lblVatRate.Text) - Convert.ToDouble(lblRate.Text);

                grossVatSalesTotal = grossVatSalesTotal + Convert.ToDouble(lblVatRate.Text);
                grossVSalesTotal = grossVSalesTotal + Convert.ToDouble(lblRate.Text);

                grossSalesTotal = grossSalesTotal + grossVat;
                lblVatPaid.Text = grossVat.ToString("f2");

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossTotal = e.Row.FindControl("lblGrossTotal") as Label;
                Label lblGrossVatRate = e.Row.FindControl("lblGrossVatRate") as Label;
                Label lblGrossRate = e.Row.FindControl("lblGrossRate") as Label;

                lblGrossTotal.Text = grossSalesTotal.ToString("f2");
                sumSales.Text = lblGrossTotal.Text;

                lblGrossVatRate.Text = grossVatSalesTotal.ToString("f2");
                lblGrossRate.Text = grossVSalesTotal.ToString("f2");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void grdSalesReturnVAT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double grossVat = 0.0d;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblVatRate = e.Row.FindControl("lblVatRate") as Label;
                Label lblVatPaid = e.Row.FindControl("lblVatPaid") as Label;

                if (lblRate != null && lblVatRate != null)
                    grossVat = Convert.ToDouble(lblVatRate.Text) - Convert.ToDouble(lblRate.Text);

                grossVatSalReturnTotal = grossVatSalReturnTotal + Convert.ToDouble(lblVatRate.Text);
                grossSalReturnTotal = grossSalReturnTotal + Convert.ToDouble(lblRate.Text);

                grossSalesReturnTotal = grossSalesReturnTotal + grossVat;
                lblVatPaid.Text = grossVat.ToString("f2");

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossTotal = e.Row.FindControl("lblGrossTotal") as Label;
                Label lblGrossVatRate = e.Row.FindControl("lblGrossVatRate") as Label;
                Label lblGrossRate = e.Row.FindControl("lblGrossRate") as Label;

                lblGrossTotal.Text = grossSalesReturnTotal.ToString("f2");
                sumSalesReturn.Text = lblGrossTotal.Text;

                lblGrossVatRate.Text = grossVatSalReturnTotal.ToString("f2");
                lblGrossRate.Text = grossSalReturnTotal.ToString("f2");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
