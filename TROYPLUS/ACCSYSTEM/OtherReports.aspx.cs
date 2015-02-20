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
using System.Globalization;
using System.IO;
using System.Xml;
using System.Data.OleDb;
using System.Text;
using System.Net.NetworkInformation;
using System.Management;

//using System.Runtime.InteropServices;
//using Microsoft.Office.Interop.Excel;

public partial class OtherReports : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!Page.IsPostBack)
            {

                lblIP.Text = GetMAC().ToString();

                Label1.Text = GetMacAddr().ToString();

                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                Label2.Text = nics[0].GetPhysicalAddress().ToString();

                //if (!Page.User.IsInRole("BUSTRNRPT"))
                //    RowBusinessTrans.Visible = false;

                //if (!Page.User.IsInRole("YREDRPT"))
                //    RowYearEnd.Visible = false;

                if (!Page.User.IsInRole("GPRPT"))
                    RowGrossProfit.Visible = false;

                if (!Page.User.IsInRole("CSTSMRPT"))
                    RowCSTSummary.Visible = false;

                //if (!Page.User.IsInRole("CRTOWN"))
                //    RowCquery.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public static string GetMacAddr()
    {
        string id=" ";
        ManagementObjectSearcher query = null;
        ManagementObjectCollection queryCollection = null;


                    query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
                    queryCollection = query.Get();
                    foreach (ManagementObject mo in queryCollection)
                    {
                      if (mo["MacAddress"] != null)
                      {
                         id = mo["MacAddress"].ToString();
                      }
                    }

              return id;
    }

    public static string GetMacAdd()
    {

        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

        ManagementObjectCollection moc = mc.GetInstances();

        string MACAddress = "";

        foreach (ManagementObject mo in moc)
        {

            if (mo["MacAddress"] != null)
            {

                if ((bool)mo["IPEnabled"] == true)
                {

                    MACAddress = mo["MacAddress"].ToString();



                }

            }

        }

        return MACAddress;



    }

    private string GetMAC()
    {
        string macAddresses = "";

        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }

    public static string GetMacAddress()
    {

        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

        ManagementObjectCollection moc = mc.GetInstances();

        string MACAddress = "";

        foreach (ManagementObject mo in moc)
        {

            if (mo["MacAddress"] != null)
            {

                MACAddress = mo["MacAddress"].ToString();
                break;
            }

        }

        return MACAddress;

    }

    protected void ReportRunner_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();
            DataSet dsd = new DataSet();

            DataTable dt = new DataTable();
            DataTable dtt = new DataTable();
            ds = bl.GetDataForSQL(sDataSource, "select pm.ItemCode,pm.ProductName,pm.Stock,a.qty1 as QuantityMismatch from tblproductmaster pm,(select itemcode,sum(qty) as qty1 from (select Itemcode,openingStock as qty  from tblStock union all select Itemcode, qty  from tblpurchaseItems union all SELECT Itemcode, qty FROM tblExecution,tblCompProduct Where tblExecution.CompID = tblCompProduct.CompID AND tblExecution.InOut = 'OUT' union all select itemcode, 0-qty  from tblsalesItems union all SELECT Itemcode, 0-qty FROM tblExecution,tblCompProduct Where tblExecution.CompID = tblCompProduct.CompID AND tblExecution.InOut = 'IN' ) group by itemcode) a where pm.itemcode=a.itemcode and pm.stock <> a.qty1");
            //ExportToExcel("Stock Consistency Check.xls", ds, 1);
            if (ds != null)
            {
                dt = ds.Tables[0];
                //dsd.Tables.Add(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            ExportToExcel123();

            //dsd.Tables.Add(dt);
            //ds = bl.GetDataForSQL(sDataSource, "select * from tblsales where customerid not in (select ledgerid from tblledger)");
            ////ds = bl.GetDataForSQL(sDataSource, "select * from tblsales");
            //////ExportToExcel("Missing Ledger in sales.xls", ds, 2);
            //if (ds != null)
            //{
            //    dtt = ds.Tables[0];
            //    GridView2.DataSource = dtt;

            //    GridView2.DataBind();
            //    //dsd.Tables.Add(dtt);
            //}

            //string attachment = "attachment; filename=Top 1.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);

            //GridView1.RenderControl(htw);
            //GridView2.RenderControl(htw);
            ////GridView3.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();


            //     Spire.DataExport.XLS.CellExport cellExport1
            //        = new Spire.DataExport.XLS.CellExport();
            //    Spire.DataExport.XLS.WorkSheet workSheet1 = new Spire.DataExport.XLS.WorkSheet();
            //    Spire.DataExport.XLS.StripStyle stripStyle1 = new Spire.DataExport.XLS.StripStyle();
            //    Spire.DataExport.XLS.StripStyle stripStyle2 = new Spire.DataExport.XLS.StripStyle();
            //    Spire.DataExport.XLS.WorkSheet workSheet2 = new Spire.DataExport.XLS.WorkSheet();
            //    Spire.DataExport.XLS.StripStyle stripStyle3 = new Spire.DataExport.XLS.StripStyle();
            //    Spire.DataExport.XLS.StripStyle stripStyle4 = new Spire.DataExport.XLS.StripStyle();

            //    cellExport1.ActionAfterExport = Spire.DataExport.Common.ActionType.OpenView;
            //    cellExport1.AutoFitColWidth = true;
            //    cellExport1.AutoFormula = true;
            //    cellExport1.DataFormats.CultureName = "zh-CN";
            //    cellExport1.DataFormats.Currency = "$#,###,##0.00";
            //    cellExport1.DataFormats.DateTime = "yyyy-M-d H:mm";
            //    cellExport1.DataFormats.Float = "#,###,##0.00";
            //    cellExport1.DataFormats.Integer = "#,###,##0";
            //    cellExport1.DataFormats.Time = "H:mm";
            //    cellExport1.FileName = "Sheets.xls";
            //    cellExport1.SheetOptions.AggregateFormat.Font.Name = "Arial";
            //    cellExport1.SheetOptions.CustomDataFormat.Font.Name = "Arial";
            //    cellExport1.SheetOptions.DefaultFont.Name = "Arial";
            //    cellExport1.SheetOptions.FooterFormat.Font.Name = "Arial";
            //    cellExport1.SheetOptions.HeaderFormat.Font.Name = "Arial";
            //    cellExport1.SheetOptions.HyperlinkFormat.Font.Color = Spire.DataExport.XLS.CellColor.Blue;
            //    cellExport1.SheetOptions.HyperlinkFormat.Font.Name = "Arial";
            //    cellExport1.SheetOptions.HyperlinkFormat.Font.Underline = Spire.DataExport.XLS.XlsFontUnderline.Single;
            //    cellExport1.SheetOptions.NoteFormat.Alignment.Horizontal = Spire.DataExport.XLS.HorizontalAlignment.Left;
            //    cellExport1.SheetOptions.NoteFormat.Alignment.Vertical = Spire.DataExport.XLS.VerticalAlignment.Top;
            //    cellExport1.SheetOptions.NoteFormat.Font.Bold = true;
            //    cellExport1.SheetOptions.NoteFormat.Font.Name = "Tahoma";
            //    cellExport1.SheetOptions.NoteFormat.Font.Size = 8F;
            //    cellExport1.SheetOptions.TitlesFormat.Font.Bold = true;
            //    cellExport1.SheetOptions.TitlesFormat.Font.Name = "Arial";
            //    workSheet1.AutoFitColWidth = true;
            //    workSheet1.FormatsExport.CultureName = "zh-CN";
            //    workSheet1.FormatsExport.Currency = "￥#,###,##0.00";
            //    workSheet1.FormatsExport.DateTime = "yyyy-M-d H:mm";
            //    workSheet1.FormatsExport.Float = "#,###,##0.00";
            //    workSheet1.FormatsExport.Integer = "#,###,##0";
            //    workSheet1.FormatsExport.Time = "H:mm";
            //    stripStyle1.Borders.Bottom.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle1.Borders.Left.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle1.Borders.Right.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle1.Borders.Top.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle1.FillStyle.Background = Spire.DataExport.XLS.CellColor.LightGreen;
            //    stripStyle1.FillStyle.Pattern = Spire.DataExport.XLS.Pattern.Solid;
            //    stripStyle1.Font.Name = "Arial";
            //    stripStyle2.Borders.Bottom.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle2.Borders.Left.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle2.Borders.Right.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle2.Borders.Top.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle2.FillStyle.Background = Spire.DataExport.XLS.CellColor.LightTurquoise;
            //    stripStyle2.FillStyle.Pattern = Spire.DataExport.XLS.Pattern.Solid;
            //    stripStyle2.Font.Name = "Arial";
            //    workSheet1.ItemStyles.Add(stripStyle1);
            //    workSheet1.ItemStyles.Add(stripStyle2);
            //    workSheet1.ItemType = Spire.DataExport.XLS.CellItemType.Col;
            //    workSheet1.Options.AggregateFormat.Font.Name = "Arial";
            //    workSheet1.Options.CustomDataFormat.Font.Name = "Arial";
            //    workSheet1.Options.DefaultFont.Name = "Arial";
            //    workSheet1.Options.FooterFormat.Font.Name = "Arial";
            //    workSheet1.Options.HeaderFormat.Font.Bold = true;
            //    workSheet1.Options.HeaderFormat.Font.Color = Spire.DataExport.XLS.CellColor.Blue;
            //    workSheet1.Options.HeaderFormat.Font.Name = "Arial";
            //    workSheet1.Options.HyperlinkFormat.Font.Color = Spire.DataExport.XLS.CellColor.Blue;
            //    workSheet1.Options.HyperlinkFormat.Font.Name = "Arial";
            //    workSheet1.Options.HyperlinkFormat.Font.Underline = Spire.DataExport.XLS.XlsFontUnderline.Single;
            //    workSheet1.Options.NoteFormat.Alignment.Horizontal = Spire.DataExport.XLS.HorizontalAlignment.Left;
            //    workSheet1.Options.NoteFormat.Alignment.Vertical = Spire.DataExport.XLS.VerticalAlignment.Top;
            //    workSheet1.Options.NoteFormat.Font.Bold = true;
            //    workSheet1.Options.NoteFormat.Font.Name = "Tahoma";
            //    workSheet1.Options.NoteFormat.Font.Size = 8F;
            //    workSheet1.Options.TitlesFormat.Borders.Bottom.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    workSheet1.Options.TitlesFormat.Borders.Left.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    workSheet1.Options.TitlesFormat.Borders.Right.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    workSheet1.Options.TitlesFormat.Borders.Top.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    workSheet1.Options.TitlesFormat.FillStyle.Background = Spire.DataExport.XLS.CellColor.LightYellow;
            //    workSheet1.Options.TitlesFormat.FillStyle.Pattern = Spire.DataExport.XLS.Pattern.Solid;
            //    workSheet1.Options.TitlesFormat.Font.Bold = true;
            //    workSheet1.Options.TitlesFormat.Font.Name = "Arial";
            //    workSheet1.SheetName = "parts";
            //    workSheet1.SQLCommand = oleDbCommand1;
            //    workSheet1.StartDataCol = ((System.Byte)(0));
            //    workSheet2.AutoFitColWidth = true;
            //    workSheet2.FormatsExport.CultureName = "zh-CN";
            //workSheet2.FormatsExport.Currency = "￥#,###,##0.00";
            //workSheet2.FormatsExport.DateTime = "yyyy-M-d H:mm";
            //    workSheet2.FormatsExport.Float = "#,###,##0.00";
            //workSheet2.FormatsExport.Integer = "#,###,##0";
            //    workSheet2.FormatsExport.Time = "H:mm";
            //stripStyle3.Borders.Bottom.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //stripStyle3.Borders.Left.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //stripStyle3.Borders.Right.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle3.Borders.Top.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle3.FillStyle.Background = Spire.DataExport.XLS.CellColor.LightGreen;
            //stripStyle3.FillStyle.Pattern = Spire.DataExport.XLS.Pattern.Solid;
            //    stripStyle3.Font.Name = "Arial";
            //    stripStyle4.Borders.Bottom.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //stripStyle4.Borders.Left.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle4.Borders.Right.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle4.Borders.Top.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    stripStyle4.FillStyle.Background = Spire.DataExport.XLS.CellColor.LightTurquoise;
            //    stripStyle4.FillStyle.Foreground = Spire.DataExport.XLS.CellColor.White;
            //stripStyle4.FillStyle.Pattern = Spire.DataExport.XLS.Pattern.Solid;
            //    stripStyle4.Font.Name = "Arial";
            //    workSheet2.ItemStyles.Add(stripStyle3);
            //    workSheet2.ItemStyles.Add(stripStyle4);
            //workSheet2.ItemType = Spire.DataExport.XLS.CellItemType.Col;
            //    workSheet2.Options.AggregateFormat.Font.Name = "Arial";
            //workSheet2.Options.CustomDataFormat.Font.Name = "Arial";
            //    workSheet2.Options.DefaultFont.Name = "Arial";
            //    workSheet2.Options.FooterFormat.Font.Color = Spire.DataExport.XLS.CellColor.Blue;
            //    workSheet2.Options.FooterFormat.Font.Name = "Arial";
            //    workSheet2.Options.HeaderFormat.Font.Bold = true;
            //workSheet2.Options.HeaderFormat.Font.Color = Spire.DataExport.XLS.CellColor.Blue;
            //workSheet2.Options.HeaderFormat.Font.Name = "Arial";
            //workSheet2.Options.HyperlinkFormat.Font.Color = Spire.DataExport.XLS.CellColor.Blue;
            //workSheet2.Options.HyperlinkFormat.Font.Name = "Arial";
            //workSheet2.Options.HyperlinkFormat.Font.Underline = Spire.DataExport.XLS.XlsFontUnderline.Single;
            //workSheet2.Options.NoteFormat.Alignment.Horizontal = Spire.DataExport.XLS.HorizontalAlignment.Left;
            //    workSheet2.Options.NoteFormat.Alignment.Vertical = Spire.DataExport.XLS.VerticalAlignment.Top;
            //    workSheet2.Options.NoteFormat.Font.Bold = true;
            //    workSheet2.Options.NoteFormat.Font.Name = "Tahoma";
            //    workSheet2.Options.NoteFormat.Font.Size = 8F;
            //    workSheet2.Options.TitlesFormat.Borders.Bottom.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //    workSheet2.Options.TitlesFormat.Borders.Left.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //workSheet2.Options.TitlesFormat.Borders.Right.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //workSheet2.Options.TitlesFormat.Borders.Top.Style = Spire.DataExport.XLS.CellBorderStyle.Medium;
            //workSheet2.Options.TitlesFormat.FillStyle.Background = Spire.DataExport.XLS.CellColor.LightYellow;
            //workSheet2.Options.TitlesFormat.FillStyle.Pattern = Spire.DataExport.XLS.Pattern.Solid;
            //workSheet2.Options.TitlesFormat.Font.Bold = true;
            //    workSheet2.Options.TitlesFormat.Font.Name = "Arial";
            //workSheet2.SheetName = "country";
            //    workSheet2.SQLCommand = oleDbCommand2;
            //workSheet2.StartDataCol = ((System.Byte)(0));
            //cellExport1.Sheets.Add(workSheet1);
            //    cellExport1.Sheets.Add(workSheet2);
            //cellExport1.SQLCommand = oleDbCommand1;
            //    cellExport1.GetDataParams += new Spire.DataExport.Delegates.DataParamsEventHandler(cellExport1_GetDataParams);

            //oleDbConnection1.Open();
            //    try
            //{
            //    cellExport1.SaveToFile();
            //    }
            //finally
            //{
            //   oleDbConnection1.Close();
            //}
            //}

            //private void cellExport1_GetDataParams(object sender, Spire.DataExport.EventArgs.DataParamsEventArgs e)
            //{
            //    if ((e.Sheet == 0) && (e.Col == 6))
            //    {
            //        e.FormatText = (sender as Spire.DataExport.XLS.WorkSheet).ExportCell.DataFormats.Currency;
            //    }
            //}

            //Workbook book = new Workbook();
            //Worksheet sheet = book.Worksheets[0];
            //sheet.InsertDataTable(dt,  true, 1, 1);
            //Worksheet sheet2 = book.Worksheets[1];
            //sheet2.InsertDataTable(dtt, true, 1, 1);
            //book.SaveToFile("ToExcel.xls");
            //System.Diagnostics.Process.Start("ToExcel.xls");

            //ExcelHelper.ToExcel(ds, "test.xls", Page.Response);  

            //ds = bl.GetDataForSQL(sDataSource, "select * from tblPurchase where supplierid not in (select ledgerid from tblledger)");
            //ExportToExcel("Missing Ledger in Purchase.xls", ds, 3);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tblReceipt where creditorid not in (select ledgerid from tblledger)");
            //ExportToExcel("Missing Ledger in Receipt.xls", ds, 4);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tblPayment where debtorid not in (select ledgerid from tblledger)");
            //ExportToExcel("Missing Ledger in Payment.xls", ds, 5);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tblCreditDebitNote where ledgerid not in (select ledgerid from tblledger)");
            //ExportToExcel("Missing Ledger in Credit Debit Note.xls", ds, 6);

            //ds = bl.GetDataForSQL(sDataSource, "select creditorid,transno,transdate,narration,vouchertype from tbldaybook where creditorid not in (select ledgerid from tblledger)");
            //ExportToExcel("Missing Ledger in Daybook (Credit).xls", ds, 7);

            //ds = bl.GetDataForSQL(sDataSource, "select debtorid,transno,transdate,narration,vouchertype from tbldaybook where debtorid not in (select ledgerid from tblledger)");
            //ExportToExcel("Missing Ledger in Daybook (Debit).xls", ds, 8);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tbldaybook where vouchertype='Sales' and transno not in (select journalid from tblsales)");
            //ExportToExcel("Missing sales in Daybook.xls", ds, 9);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tbldaybook where vouchertype='Purchase' and transno not in (select journalid from tblPurchase)");
            //ExportToExcel("Missing Purchase in Daybook.xls", ds, 10);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tbldaybook where vouchertype='Receipt' and transno not in (select journalid from tblReceipt)");
            //ExportToExcel("Missing Receipt in Daybook.xls", ds, 11);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tbldaybook where vouchertype='Payment' and transno not in (select journalid from tblPayment)");
            //ExportToExcel("Missing Payment in Daybook.xls", ds, 12);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tbldaybook where vouchertype='Note' and transno not in (select transno from tblcreditdebitnote)");
            //ExportToExcel("Missing CreditDebitnote in Daybook.xls", ds, 13);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tblsales where journalid not in (select transno from tbldaybook)");
            //ExportToExcel("Missing Journel in Sales.xls", ds, 14);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tblpurchase where journalid not in (select transno from tbldaybook)");
            //ExportToExcel("Missing Journel in Purchase.xls", ds, 15);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tblreceipt where journalid not in (select transno from tbldaybook)");
            //ExportToExcel("Missing Journel in Receipt.xls", ds, 16);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tblpayment where journalid not in (select transno from tbldaybook)");
            //ExportToExcel("Missing Journel in Payment.xls", ds, 17);

            //ds = bl.GetDataForSQL(sDataSource, "select * from tblcreditdebitnote where transno not in (select transno from tbldaybook)");
            //ExportToExcel123();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //}

    protected void ExportToExcel123()
    {

        Response.Clear();

        Response.Buffer = true;



        Response.AddHeader("content-disposition",

         "attachment;filename=GridViewExport.xls");

        Response.Charset = "";

        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);



        PrepareForExport(GridView1);

        PrepareForExport(GridView2);



        Table tb = new Table();

        TableRow tr1 = new TableRow();

        TableCell cell1 = new TableCell();

        cell1.Controls.Add(GridView1);

        tr1.Cells.Add(cell1);

        TableCell cell3 = new TableCell();

        cell3.Controls.Add(GridView2);

        TableCell cell2 = new TableCell();

        cell2.Text = "&nbsp;";

        //if (rbPreference.SelectedValue == "2")
        //{

            //tr1.Cells.Add(cell2);

            //tr1.Cells.Add(cell3);

            //tb.Rows.Add(tr1);

        //}

        //else
        //{

            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);

            TableRow tr3 = new TableRow();

            tr3.Cells.Add(cell3);

            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.Rows.Add(tr3);

        //}

        tb.RenderControl(hw);



        //style to format numbers to string

        string style = @"<style> .textmode { mso-number-format:\@; } </style>";

        Response.Write(style);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();

    }



    protected void PrepareForExport(GridView Gridview)
    {

        //Gridview.AllowPaging = Convert.ToBoolean(rbPaging.SelectedItem.Value);

        Gridview.DataBind();



        //Change the Header Row back to white color

        //Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF");



        ////Apply style to Individual Cells

        //for (int k = 0; k < Gridview.HeaderRow.Cells.Count; k++)
        //{

        //    Gridview.HeaderRow.Cells[k].Style.Add("background-color", "green");

        //}



        //for (int i = 0; i < Gridview.Rows.Count; i++)
        //{

        //    GridViewRow row = Gridview.Rows[i];



        //    //Change Color back to white

        //    row.BackColor = System.Drawing.Color.White;



        //    //Apply text style to each Row

        //    row.Attributes.Add("class", "textmode");



        //    //Apply style to Individual Cells of Alternating Row

        //    if (i % 2 != 0)
        //    {

        //        for (int j = 0; j < Gridview.Rows[i].Cells.Count; j++)
        //        {

        //            row.Cells[j].Style.Add("background-color", "#C2D69B");

        //        }

        //    }

        //}

    }


    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //    }
    //}

    //protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Pager)
    //    {
    //        PresentationUtils.SetPagerButtonStates(GridView1, e.Row, this);
    //    }
    //}

    public void ExportToExcel(string filename, DataSet ds, int types)
    {
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                //rptData.DataSource = ds;
                //rptData.DataBind();
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
            else
            {
                if (types == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 2)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Missing Ledger in sales');", true);
                    return;
                }
                else if (types == 3)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Missing Ledger in Purchase');", true);
                    return;
                }
                else if (types == 4)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 5)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 6)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 7)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 8)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 9)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 11)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 12)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 13)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 14)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 15)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 16)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 17)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
                else if (types == 18)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found in Stock Consistency Check');", true);
                    return;
                }
            }
        }
    }

    public class ExcelHelper
{
    //Row limits older Excel version per sheet
        const int rowLimit = 65000;

        private static string getWorkbookTemplate()
        {
            var sb = new StringBuilder();
            sb.Append("<xml version>\r\n<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n");
            sb.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n xmlns:x=\"urn:schemas- microsoft-com:office:excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">\r\n");
            sb.Append(" <Styles>\r\n <Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n <Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>");
            sb.Append("\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>\r\n <Protection/>\r\n </Style>\r\n <Style ss:ID=\"BoldColumn\">\r\n <Font ");
            sb.Append("x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n <Style ss:ID=\"s62\">\r\n <NumberFormat");
            sb.Append(" ss:Format=\"@\"/>\r\n </Style>\r\n <Style ss:ID=\"Decimal\">\r\n <NumberFormat ss:Format=\"0.0000\"/>\r\n </Style>\r\n ");
            sb.Append("<Style ss:ID=\"Integer\">\r\n <NumberFormat ss:Format=\"0\"/>\r\n </Style>\r\n <Style ss:ID=\"DateLiteral\">\r\n <NumberFormat ");
            sb.Append("ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n <Style ss:ID=\"s28\">\r\n");
            sb.Append("<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Top\" ss:ReadingOrder=\"LeftToRight\" ss:WrapText=\"1\"/>\r\n");
            sb.Append("<Font x:CharSet=\"1\" ss:Size=\"9\" ss:Color=\"#808080\" ss:Underline=\"Single\"/>\r\n");
            sb.Append("<Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/> </Style>\r\n</Styles>\r\n {0}</Workbook>");
            return sb.ToString();
        }

        private static string replaceXmlChar(string input)
        {
            input = input.Replace("&", "&");
            input = input.Replace("<", "<");
            input = input.Replace(">", ">");
            //input = input.Replace("\"", """);
            input = input.Replace("'", "&apos;");
            return input;
        }

        private static string getWorksheets(DataSet source)
        {
            var sw = new StringWriter();
            if (source == null || source.Tables.Count == 0)
            {
                sw.Write("<Worksheet ss:Name=\"Sheet1\"><Table><Row> <Cell  ss:StyleID=\"s62\"><Data ss:Type=\"String\"></Data> </Cell></Row></Table></Worksheet>");
                return sw.ToString();
            }
            foreach (DataTable dt in source.Tables)
            {
                if (dt.Rows.Count == 0)
                    sw.Write("<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) + "\"><Table><Row><Cell  ss:StyleID=\"s62\"> <Data ss:Type=\"String\"></Data></Cell></Row></Table></Worksheet>");
                else
                {
                    //write each row data
                    var sheetCount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((i % rowLimit) == 0)
                        {
                            //add close tags for previous sheet of the same data table
                            if ((i / rowLimit) > sheetCount)
                            {
                                sw.Write("</Table></Worksheet>");
                                sheetCount = (i / rowLimit);
                            }
                            sw.Write("<Worksheet ss:Name=\"" + replaceXmlChar(dt.TableName) +(((i / rowLimit) == 0) ? "" :Convert.ToString(i / rowLimit)) + "\"><Table>");
                            //write column name row
                            sw.Write("<Row>");
                            foreach (DataColumn dc in dt.Columns)
                                sw.Write(string.Format("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">{0}</Data></Cell>",replaceXmlChar(dc.ColumnName)));
                            sw.Write("</Row>\r\n");
                        }
                        sw.Write("<Row>\r\n");
                        foreach (DataColumn dc in dt.Columns)
                            sw.Write(string.Format("<Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">{0}</Data></Cell>",replaceXmlChar(dt.Rows[i][dc.ColumnName].ToString())));
                        sw.Write("</Row>\r\n");
                    }
                    sw.Write("</Table></Worksheet>");
                }
            }

            return sw.ToString();
        }
        public static string GetExcelXml(DataTable dtInput, string filename)
        {
            var excelTemplate = getWorkbookTemplate();
            var ds = new DataSet();
            ds.Tables.Add(dtInput.Copy());
            var worksheets = getWorksheets(ds);
            var excelXml = string.Format(excelTemplate, worksheets);
            return excelXml;
        }

        public static string GetExcelXml(DataSet dsInput, string filename)
        {
            var excelTemplate = getWorkbookTemplate();
            var worksheets = getWorksheets(dsInput);
            var excelXml = string.Format(excelTemplate, worksheets);
            return excelXml;
        }

        public static void ToExcel
		(DataSet dsInput, string filename, HttpResponse response)
        {
            var excelXml = GetExcelXml(dsInput, filename);
            response.Clear();
            response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            response.AppendHeader
		("Content-disposition", "attachment; filename=" + filename);
            response.Write(excelXml);
            response.Flush();
            response.End();
        }

        public static void ToExcel
		(DataTable dtInput, string filename, HttpResponse response)
        {
            var ds = new DataSet();
            ds.Tables.Add(dtInput.Copy());
            ToExcel(ds, filename, response);
        }
    }

}
