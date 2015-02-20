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
using System.Text;
using System.IO;
public partial class QueryRunner : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    string dbfileName = string.Empty;

    string stype;
    string stblenme;
    string desc;
    string qname;
    string qnamed;
    string qry;
    string qrydesc;
    BusinessLogic objBL = new BusinessLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (!Page.IsPostBack)
            {
                txtQuery.Enabled = false;
                txtmissingledsales.Enabled = false;
                TextBox1.Enabled = false;
                TextBox2.Enabled = false;
                TextBox3.Enabled = false;
                TextBox4.Enabled = false;
                TextBox5.Enabled = false;
                TextBox6.Enabled = false;
                TextBox7.Enabled = false;
                TextBox8.Enabled = false;
                TextBox9.Enabled = false;
                TextBox10.Enabled = false;
                TextBox11.Enabled = false;
                TextBox12.Enabled = false;
                TextBox13.Enabled = false;
                TextBox14.Enabled = false;
                TextBox15.Enabled = false;
                TextBox16.Enabled = false;
                txtQuery.Text = "select pm.ItemCode,pm.ProductName,pm.Stock,a.qty1 as QuantityMismatch from tblproductmaster pm,(select itemcode,sum(qty) as qty1 from (select Itemcode,openingStock as qty  from tblStock union all select Itemcode, qty  from tblpurchaseItems union all SELECT Itemcode, qty FROM tblExecution,tblCompProduct Where tblExecution.CompID = tblCompProduct.CompID AND tblExecution.InOut = 'OUT' union all select itemcode, 0-qty  from tblsalesItems union all SELECT Itemcode, 0-qty FROM tblExecution,tblCompProduct Where tblExecution.CompID = tblCompProduct.CompID AND tblExecution.InOut = 'IN' ) group by itemcode) a where pm.itemcode=a.itemcode and pm.stock <> a.qty1";
                txtmissingledsales.Text = "select billno,billdate,customername,journalid,customerid from tblsales where customerid not in (select ledgerid from tblledger)";
                TextBox1.Text = "select billno,billdate,journalid,supplierid,purchaseid from tblPurchase where supplierid not in (select ledgerid from tblledger)";
                TextBox2.Text = "select receiptno,creditorid,paymode,journalid from tblReceipt where creditorid not in (select ledgerid from tblledger)";
                TextBox3.Text = "select paymode,journalid,voucherno,billno,debtorid from tblPayment where debtorid not in (select ledgerid from tblledger)";
                TextBox4.Text = "select cdtype,refno,amount,notedate,ledgerid,transno from tblCreditDebitNote where ledgerid not in (select ledgerid from tblledger)";
                TextBox5.Text = "select creditorid,transno,transdate,narration,vouchertype from tbldaybook where creditorid not in (select ledgerid from tblledger)";
                TextBox6.Text = "select debtorid,transno,transdate,narration,vouchertype from tbldaybook where debtorid not in (select ledgerid from tblledger)";
                TextBox7.Text = "select transno,transdate,debtorid,creditorid,amount,vouchertype,refno,chequeno,narration from tbldaybook where vouchertype='Sales' and transno not in (select journalid from tblsales)";
                TextBox8.Text = "select transno,transdate,debtorid,creditorid,amount,vouchertype,refno,chequeno,narration from tbldaybook where vouchertype='Purchase' and transno not in (select journalid from tblPurchase)";
                TextBox9.Text = "select transno,transdate,debtorid,creditorid,amount,vouchertype,refno,chequeno,narration from tbldaybook where vouchertype='Receipt' and transno not in (select journalid from tblReceipt)";
                TextBox10.Text = "select transno,transdate,debtorid,creditorid,amount,vouchertype,refno,chequeno,narration from tbldaybook where vouchertype='Payment' and transno not in (select journalid from tblPayment)";
                TextBox11.Text = "select transno,transdate,debtorid,creditorid,amount,vouchertype,refno,chequeno,narration from tbldaybook where vouchertype='Note' and transno not in (select transno from tblcreditdebitnote)";
                TextBox12.Text = "select billno,billdate,customername,journalid,customerid from tblsales where journalid not in (select transno from tbldaybook)";
                TextBox13.Text = "select billno,billdate,journalid,supplierid,purchaseid from tblpurchase where journalid not in (select transno from tbldaybook)";
                TextBox14.Text = "select receiptno,creditorid,paymode,journalid from tblreceipt where journalid not in (select transno from tbldaybook)";
                TextBox15.Text = "select paymode,journalid,voucherno,billno,debtorid from tblpayment where journalid not in (select transno from tbldaybook)";
                TextBox16.Text = "select cdtype,refno,amount,notedate,ledgerid,transno from tblcreditdebitnote where transno not in (select transno from tbldaybook)";
                TextBox17.Text = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName,tblProductMaster.productdesc, tblProductMaster.Model, tblProductMaster.CategoryID,tblProductMaster.Stock FROM tblProductMaster where tblProductMaster.Stock < 0 ";

                BusinessLogic bl = new BusinessLogic(sDataSource);

                DataSet ds = bl.GetDataForSQL(sDataSource, txtQuery.Text);
                gvProducts.DataSource = ds;
                gvProducts.DataBind();

                DataSet dst = bl.GetDataForSQL(sDataSource, TextBox5.Text);
                ReportGridView1.DataSource = dst;
                ReportGridView1.DataBind();

                DataSet dsttt = bl.GetDataForSQL(sDataSource, TextBox6.Text);
                ReportGridView2.DataSource = dsttt;
                ReportGridView2.DataBind();

                DataSet dstttt = bl.GetDataForSQL(sDataSource, txtmissingledsales.Text);
                ReportGridView3.DataSource = dstttt;
                ReportGridView3.DataBind();

                DataSet dstttttt = bl.GetDataForSQL(sDataSource, TextBox1.Text);
                ReportGridView4.DataSource = dstttttt;
                ReportGridView4.DataBind();

                DataSet dsttttt = bl.GetDataForSQL(sDataSource, TextBox2.Text);
                ReportGridView5.DataSource = dsttttt;
                ReportGridView5.DataBind();

                DataSet dsttttttt = bl.GetDataForSQL(sDataSource, TextBox3.Text);
                ReportGridView6.DataSource = dsttttttt;
                ReportGridView6.DataBind();

                DataSet dstttttttdd = bl.GetDataForSQL(sDataSource, TextBox4.Text);
                ReportGridView7.DataSource = dstttttttdd;
                ReportGridView7.DataBind();

                DataSet dstttttd = bl.GetDataForSQL(sDataSource, TextBox7.Text);
                ReportGridView8.DataSource = dstttttd;
                ReportGridView8.DataBind();

                DataSet dstttttddd = bl.GetDataForSQL(sDataSource, TextBox8.Text);
                ReportGridView9.DataSource = dstttttddd;
                ReportGridView9.DataBind();

                DataSet dstttttdt = bl.GetDataForSQL(sDataSource, TextBox9.Text);
                ReportGridView10.DataSource = dstttttdt;
                ReportGridView10.DataBind();

                DataSet dstttttddt = bl.GetDataForSQL(sDataSource, TextBox10.Text);
                ReportGridView11.DataSource = dstttttddt;
                ReportGridView11.DataBind();

                DataSet dstttttddtd = bl.GetDataForSQL(sDataSource, TextBox11.Text);
                ReportGridView12.DataSource = dstttttddtd;
                ReportGridView12.DataBind();

                DataSet dstttttddtdd = bl.GetDataForSQL(sDataSource, TextBox12.Text);
                ReportGridView13.DataSource = dstttttddtdd;
                ReportGridView13.DataBind();

                DataSet dstttttddtddd = bl.GetDataForSQL(sDataSource, TextBox13.Text);
                ReportGridView14.DataSource = dstttttddtddd;
                ReportGridView14.DataBind();

                DataSet dstttttddtdddd = bl.GetDataForSQL(sDataSource, TextBox14.Text);
                ReportGridView15.DataSource = dstttttddtdddd;
                ReportGridView15.DataBind();

                DataSet dstttttddtddddd = bl.GetDataForSQL(sDataSource, TextBox15.Text);
                ReportGridView16.DataSource = dstttttddtddddd;
                ReportGridView16.DataBind();

                DataSet dstttttddtddddtd = bl.GetDataForSQL(sDataSource, TextBox16.Text);
                ReportGridView17.DataSource = dstttttddtddddtd;
                ReportGridView17.DataBind();

                DataSet ddd = bl.GetDataForSQL(sDataSource, TextBox17.Text);
                ReportGridView18.DataSource = ddd;
                ReportGridView18.DataBind();

                ExportToExcel();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void GetQueryForID(string ID)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.GetQueryForID(ID);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtQuery.Text = ds.Tables[0].Rows[0]["Query"].ToString();
                //lblDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            }
        }
    }

    protected void cmbQuries_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if(cmbQuries.SelectedValue != "0")
        //{
        //    GetQueryForID(cmbQuries.SelectedValue);
        //    //BtnEdit.Visible = true;
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, txtQuery.Text);

        try
        {
            ExportToExcel("Stock Consistency Check.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, txtmissingledsales.Text);

        try
        {
            ExportToExcel("Missing Ledger in sales.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox1.Text);

        try
        {
            ExportToExcel("Missing Ledger in Purchase.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox2.Text);

        try
        {
            ExportToExcel("Missing Ledger in Receipt.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox3.Text);

        try
        {
            ExportToExcel("Missing Ledger in Payment.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox4.Text);

        try
        {
            ExportToExcel("Missing Ledger in Credit Debit Note.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox5.Text);

        try
        {
            ExportToExcel("Missing Ledger in Daybook (Credit).xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox6.Text);

        try
        {
            ExportToExcel("Missing Ledger in Daybook (Debit).xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox7.Text);

        try
        {
            ExportToExcel("Missing sales in Daybook.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox8.Text);

        try
        {
            ExportToExcel("Missing Purchase in Daybook.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox9.Text);

        try
        {
            ExportToExcel("Missing Receipt in Daybook.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button12_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox10.Text);

        try
        {
            ExportToExcel("Missing Payment in Daybook.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button13_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox11.Text);

        try
        {
            ExportToExcel("Missing CreditDebitnote in Daybook.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button14_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox12.Text);

        try
        {
            ExportToExcel("Missing Journel in Sales.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button15_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox13.Text);

        try
        {
            ExportToExcel("Missing Journel in Purchase.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button16_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox14.Text);

        try
        {
            ExportToExcel("Missing Journel in Receipt.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button17_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox15.Text);

        try
        {
            ExportToExcel("Missing Journel in Payment.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button18_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox16.Text);

        try
        {
            ExportToExcel("Missing Journel in CreditDebitnote.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void Button19_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.GetDataForSQL(sDataSource, TextBox17.Text);

        try
        {
            ExportToExcel("Negative Stock.xls", ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel()
    {
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = bl.GetDataForSQL(sDataSource, TextBox1.Text);
        //DataTable dt = ds.Tables[0];
        //GrdViewSales.DataSource = dt;
        //GrdViewSales.DataBind();

        //DataSet dst = bl.GetDataForSQL(sDataSource, txtmissingledsales.Text);
        //DataTable dtt = dst.Tables[0];
        //GridView1.DataSource = dtt;
        //GridView1.DataBind();

        //DataSet dsttt = bl.GetDataForSQL(sDataSource, txtQuery.Text);
        //DataTable dtttt = dsttt.Tables[0];
        //GridView2.DataSource = dtttt;
        //GridView2.DataBind();

        try
        {
            Response.Clear();

            Response.Buffer = true;



            Response.AddHeader("content-disposition",

             "attachment;filename=GridViewExport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);



            //PrepareForExport(GridView1);

            //PrepareForExport(GridView2);



            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Stock Consistency Check";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvProducts);

            TableCell cell3 = new TableCell();

            cell3.Text = "&nbsp;";

            TableCell cell4 = new TableCell();

            cell4.Text = "Missing Ledger in Daybook (Credit)";


            TableCell cell5 = new TableCell();

            cell5.Controls.Add(ReportGridView1);

            TableCell cell6 = new TableCell();

            cell6.Text = "&nbsp;";

            TableCell cell7 = new TableCell();

            cell7.Text = "Missing Ledger in Daybook (Debit)";

            TableCell cell8 = new TableCell();

            cell8.Controls.Add(ReportGridView2);

            TableCell cell9 = new TableCell();

            cell9.Text = "&nbsp;";

            TableCell cell10 = new TableCell();

            cell10.Text = "Missing Ledger in sales";

            TableCell cell11 = new TableCell();

            cell11.Controls.Add(ReportGridView3);

            TableCell cell12 = new TableCell();

            cell12.Text = "&nbsp;";

            TableCell cell13 = new TableCell();

            cell13.Text = "Missing Ledger in Purchase";

            TableCell cell14 = new TableCell();

            cell14.Controls.Add(ReportGridView4);

            TableCell cell15 = new TableCell();

            cell15.Text = "&nbsp;";

            TableCell cell16 = new TableCell();

            cell16.Text = "Missing Ledger in Receipt";

            TableCell cell17 = new TableCell();

            cell17.Controls.Add(ReportGridView5);

            TableCell cell18 = new TableCell();

            cell18.Text = "&nbsp;";









            TableCell cell19 = new TableCell();

            cell19.Text = "&nbsp;";

            TableCell cell20 = new TableCell();

            cell20.Text = "Missing Ledger in Payment";

            TableCell cell21 = new TableCell();

            cell21.Controls.Add(ReportGridView6);

            TableCell cell22 = new TableCell();

            cell22.Text = "&nbsp;";

            TableCell cell23 = new TableCell();

            cell23.Text = "&nbsp;";

            TableCell cell24 = new TableCell();

            cell24.Text = "Missing Ledger in Credit Debit Note";

            TableCell cell25 = new TableCell();

            cell25.Controls.Add(ReportGridView7);

            TableCell cell26 = new TableCell();

            cell26.Text = "&nbsp;";

            TableCell cell27 = new TableCell();

            cell27.Text = "&nbsp;";

            TableCell cell28 = new TableCell();

            cell28.Text = "Missing sales in Daybook";

            TableCell cell29 = new TableCell();

            cell29.Controls.Add(ReportGridView8);

            TableCell cell30 = new TableCell();

            cell30.Text = "&nbsp;";

            TableCell cell31 = new TableCell();

            cell31.Text = "&nbsp;";

            TableCell cell32 = new TableCell();

            cell32.Text = "Missing Purchase in Daybook";

            TableCell cell33 = new TableCell();

            cell33.Controls.Add(ReportGridView9);

            TableCell cell34 = new TableCell();

            cell34.Text = "&nbsp;";

            TableCell cell35 = new TableCell();

            cell35.Text = "&nbsp;";

            TableCell cell36 = new TableCell();

            cell36.Text = "Missing Receipt in Daybook";

            TableCell cell37 = new TableCell();

            cell37.Controls.Add(ReportGridView10);

            TableCell cell38 = new TableCell();

            cell38.Text = "&nbsp;";

            TableCell cell39 = new TableCell();

            cell39.Text = "&nbsp;";

            TableCell cell40 = new TableCell();

            cell40.Text = "Missing Payment in Daybook";

            TableCell cell41 = new TableCell();

            cell41.Controls.Add(ReportGridView11);

            TableCell cell42 = new TableCell();

            cell42.Text = "&nbsp;";

            TableCell cell43 = new TableCell();

            cell43.Text = "&nbsp;";

            TableCell cell44 = new TableCell();

            cell44.Text = "Missing CreditDebitnote in Daybook";

            TableCell cell45 = new TableCell();

            cell45.Controls.Add(ReportGridView12);

            TableCell cell46 = new TableCell();

            cell46.Text = "&nbsp;";

            TableCell cell47 = new TableCell();

            cell47.Text = "&nbsp;";

            TableCell cell48 = new TableCell();

            cell48.Text = "Missing Journel in Purchase";

            TableCell cell49 = new TableCell();

            cell49.Controls.Add(ReportGridView13);

            TableCell cell50 = new TableCell();

            cell50.Text = "&nbsp;";

            TableCell cell51 = new TableCell();

            cell51.Text = "&nbsp;";

            TableCell cell52 = new TableCell();

            cell52.Text = "Missing Journel in Sales";

            TableCell cell53 = new TableCell();

            cell53.Controls.Add(ReportGridView14);

            TableCell cell54 = new TableCell();

            cell54.Text = "&nbsp;";

            TableCell cell55 = new TableCell();

            cell55.Text = "&nbsp;";

            TableCell cell56 = new TableCell();

            cell56.Text = "Missing Journel in Receipt";

            TableCell cell57 = new TableCell();

            cell57.Controls.Add(ReportGridView15);

            TableCell cell58 = new TableCell();

            cell58.Text = "&nbsp;";

            TableCell cell59 = new TableCell();

            cell59.Text = "&nbsp;";

            TableCell cell60 = new TableCell();

            cell60.Text = "Missing Journel in Payment";

            TableCell cell61 = new TableCell();

            cell61.Controls.Add(ReportGridView16);

            TableCell cell62 = new TableCell();

            cell62.Text = "&nbsp;";

            TableCell cell63 = new TableCell();

            cell63.Text = "&nbsp;";

            TableCell cell64 = new TableCell();

            cell64.Text = "Missing Journel in CreditDebitnote";

            TableCell cell65 = new TableCell();

            cell65.Controls.Add(ReportGridView17);

            TableCell cell66 = new TableCell();

            cell66.Text = "&nbsp;";

            TableCell cell67 = new TableCell();

            cell67.Text = "Negative Stock";

            TableCell cell68 = new TableCell();

            cell68.Controls.Add(ReportGridView18);

            TableCell cell69 = new TableCell();

            cell69.Text = "&nbsp;";

            //if (rbPreference.SelectedValue == "2")
            //{

            //    tr1.Cells.Add(cell2);

            //    tr1.Cells.Add(cell3);

            //    tb.Rows.Add(tr1);

            //}

            //else
            //{

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

            TableRow tr34 = new TableRow();

            tr34.Cells.Add(cell34);

            TableRow tr35 = new TableRow();

            tr35.Cells.Add(cell35);

            TableRow tr36 = new TableRow();

            tr36.Cells.Add(cell36);

            TableRow tr37 = new TableRow();

            tr37.Cells.Add(cell37);

            TableRow tr38 = new TableRow();

            tr38.Cells.Add(cell38);

            TableRow tr39 = new TableRow();

            tr39.Cells.Add(cell39);

            TableRow tr40 = new TableRow();

            tr40.Cells.Add(cell40);

            TableRow tr41 = new TableRow();

            tr41.Cells.Add(cell41);

            TableRow tr42 = new TableRow();

            tr42.Cells.Add(cell42);

            TableRow tr43 = new TableRow();

            tr43.Cells.Add(cell43);

            TableRow tr44 = new TableRow();

            tr44.Cells.Add(cell44);

            TableRow tr45 = new TableRow();

            tr45.Cells.Add(cell45);

            TableRow tr46 = new TableRow();

            tr46.Cells.Add(cell46);

            TableRow tr47 = new TableRow();

            tr47.Cells.Add(cell47);

            TableRow tr48 = new TableRow();

            tr48.Cells.Add(cell48);

            TableRow tr49 = new TableRow();

            tr49.Cells.Add(cell49);

            TableRow tr50 = new TableRow();

            tr50.Cells.Add(cell50);

            TableRow tr51 = new TableRow();

            tr51.Cells.Add(cell51);

            TableRow tr52 = new TableRow();

            tr52.Cells.Add(cell52);

            TableRow tr53 = new TableRow();

            tr53.Cells.Add(cell53);

            TableRow tr54 = new TableRow();

            tr54.Cells.Add(cell54);

            TableRow tr55 = new TableRow();

            tr55.Cells.Add(cell55);

            TableRow tr56 = new TableRow();

            tr56.Cells.Add(cell56);

            TableRow tr57 = new TableRow();

            tr57.Cells.Add(cell57);

            TableRow tr58 = new TableRow();

            tr58.Cells.Add(cell58);

            TableRow tr59 = new TableRow();

            tr59.Cells.Add(cell59);

            TableRow tr60 = new TableRow();

            tr60.Cells.Add(cell60);

            TableRow tr61 = new TableRow();

            tr61.Cells.Add(cell61);

            TableRow tr62 = new TableRow();

            tr62.Cells.Add(cell62);

            TableRow tr63 = new TableRow();

            tr63.Cells.Add(cell63);

            TableRow tr64 = new TableRow();

            tr64.Cells.Add(cell64);

            TableRow tr65 = new TableRow();

            tr65.Cells.Add(cell65);

            TableRow tr66 = new TableRow();

            tr66.Cells.Add(cell66);

            TableRow tr67 = new TableRow();

            tr67.Cells.Add(cell67);

            TableRow tr68 = new TableRow();

            tr68.Cells.Add(cell68);

            TableRow tr69 = new TableRow();

            tr69.Cells.Add(cell69);


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
            tb.Rows.Add(tr34);
            tb.Rows.Add(tr35);
            tb.Rows.Add(tr36);
            tb.Rows.Add(tr37);
            tb.Rows.Add(tr38);
            tb.Rows.Add(tr39);
            tb.Rows.Add(tr40);
            tb.Rows.Add(tr41);
            tb.Rows.Add(tr42);
            tb.Rows.Add(tr43);
            tb.Rows.Add(tr44);
            tb.Rows.Add(tr45);
            tb.Rows.Add(tr46);
            tb.Rows.Add(tr47);
            tb.Rows.Add(tr48);
            tb.Rows.Add(tr49);
            tb.Rows.Add(tr50);
            tb.Rows.Add(tr51);
            tb.Rows.Add(tr52);
            tb.Rows.Add(tr53);
            tb.Rows.Add(tr54);
            tb.Rows.Add(tr55);
            tb.Rows.Add(tr56);
            tb.Rows.Add(tr57);
            tb.Rows.Add(tr58);
            tb.Rows.Add(tr59);
            tb.Rows.Add(tr60);
            tb.Rows.Add(tr61);
            tb.Rows.Add(tr62);
            tb.Rows.Add(tr63);
            tb.Rows.Add(tr64);
            tb.Rows.Add(tr65);
            tb.Rows.Add(tr66);
            tb.Rows.Add(tr67);
            tb.Rows.Add(tr68);
            tb.Rows.Add(tr69);
            //}

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

    public void ExportToExcel(string filename, DataSet ds)
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found');", true);
                return;
            }
        }
    }


    //protected void BtnEdit_Click(object sender, EventArgs e)
    //{
        //txtDesc.Visible = true;
        //BtnSave.Visible = true;
       
    //}
    protected void Btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds1 = new DataSet();
            string qnamed = "";
            //qnamed = ddlqueries.SelectedItem.Value;
            //if (ddlqueries.SelectedValue != "0")
            //{
            //    qnamed = ddlqueries.SelectedItem.Value;
            //}
            bl.DeleteDataForSQL(sDataSource, qnamed);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('SQL Report Deleted')", true);
            return;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        
            return;
        }

    }
    protected void btnsavenew_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds1 = new DataSet();
            string qname = "";
            string qry = "";
            string qrydesc = "";
            //qname = txtboxqrynme.Text;
            //qrydesc = txtboxdescrip.Text;
            //qry = txtboxqry.Text;
            bl.InsertDataForSQL(sDataSource, qname, qry, qrydesc);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('SQL Report Created')", true);
            return;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        
            return;
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string desc = "";

            //desc = txtDesc.Text;

            //if (txtDesc.Text == "")
            //{
            //    desc = lblDescription.Text;
            //}
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds1 = new DataSet();
            //bl.UpdateDataForSQL(sDataSource, cmbQuries.SelectedValue, cmbQuries.SelectedItem.Text, txtQuery.Text, txtDesc.Text);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Report Saved')", true);
            return;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);        
            return;
        }
    }
   
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        //txtboxqrynme.Text = "";
        //txtboxdescrip.Text = "";
        //txtboxqry.Text = "";

    }
    protected void ddlqueries_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlqueries.SelectedValue != "0")
        //{
        //    GetQueryForID(ddlqueries.SelectedValue);

        //}
    }
}