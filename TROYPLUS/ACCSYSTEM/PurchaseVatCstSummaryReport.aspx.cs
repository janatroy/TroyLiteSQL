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

public partial class PurchaseVatCstSummaryReport : System.Web.UI.Page
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

                loadLedger();
                loadVatCst();
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

                    if (companyInfo != null)
                    {
                        if (companyInfo.Tables[0].Rows.Count > 0)
                        {
                            //foreach (DataRow dr in companyInfo.Tables[0].Rows)
                            //{
                            //    lblTNGST.Text = Convert.ToString(dr["TINno"]);
                            //    lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                            //    lblPhone.Text = Convert.ToString(dr["Phone"]);
                            //    lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                            //    lblAddress.Text = Convert.ToString(dr["Address"]);
                            //    lblCity.Text = Convert.ToString(dr["city"]);
                            //    lblPincode.Text = Convert.ToString(dr["Pincode"]);
                            //    lblState.Text = Convert.ToString(dr["state"]);
                            //    lblBillDate.Text = DateTime.Now.ToShortDateString();
                            //}
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadVatCst()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListPurchaseVat();
        drpVat.DataSource = ds;
        drpVat.DataBind();
        drpVat.DataTextField = "vat";
        drpVat.DataValueField = "vat";
        ds = bl.ListPurchaseCst();
        drpCst.DataSource = ds;
        drpCst.DataBind();
        drpCst.DataTextField = "cst";
        drpCst.DataValueField = "cst";
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            System.Text.StringBuilder htmlcode = new System.Text.StringBuilder();
            htmlcode.Append("<html><body>");
            //htmlcode.Append("<form id=form1 runat=server>");
            htmlcode.Append("<div id=divPrint style=font-family:'Trebuchet MS'; font-size:10px;  >");

            htmlcode.Append("<Table id = table1 border=1px solid blue cellpadding=0 cellspacing=0 class=tblLeft width=100% >");
            DataSet dsVat = new DataSet();
            DataSet dsCst = new DataSet();
            DataSet ds1 = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            htmlcode.Append("<tr class=ReportHeadataRow style=text-align:left>");
            htmlcode.Append("<td> BILL NO");
            htmlcode.Append("</td>");
            htmlcode.Append("<td> Ledger Name");
            htmlcode.Append("</td>");
            htmlcode.Append("<td> TIN Number");
            htmlcode.Append("</td>");

            int i = 0;
            int j = 0;
            double TotSal = 0;
            dsVat = bl.ListPurchaseVat();

            for (i = 0; i < dsVat.Tables[0].Rows.Count; i++)
            {
                if (dsVat.Tables[0].Rows[i].ItemArray[0].ToString() == "0")
                {
                    htmlcode.Append("<td> Exempted");
                    htmlcode.Append("</td>");

                }
                else
                {
                    htmlcode.Append("<td>" + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + " Purchase Value");
                    htmlcode.Append("</td>");
                    htmlcode.Append("<td>" + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + " Input VAT");
                    htmlcode.Append("</td>");
                }
            }
            dsCst = bl.ListPurchaseCst();
            for (i = 1; i < dsCst.Tables[0].Rows.Count; i++)
            {
                htmlcode.Append("<td>" + dsCst.Tables[0].Rows[i].ItemArray[0].ToString() + " Purchase Value");
                htmlcode.Append("</td>");
                htmlcode.Append("<td>" + dsCst.Tables[0].Rows[i].ItemArray[0].ToString() + " Input CST");
                htmlcode.Append("</td>");
            }
            htmlcode.Append("<td> Total Sales");
            htmlcode.Append("</td>");
            htmlcode.Append("</tr>");



            Double[] Total;
            Total = new double[50];
            int TotCount = 0;

            ds1 = bl.ListPurchaseVatCstAmtDet(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToInt16(drpLedgerName.SelectedValue), Convert.ToInt16(drpVat.SelectedValue), Convert.ToInt16(drpCst.SelectedValue));
            if (ds1 != null)
            {
                for (j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    TotCount = 0;
                    htmlcode.Append("<tr class=ReportdataRow>");
                    htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[0].ToString());
                    htmlcode.Append("</td>");
                    htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[1].ToString());
                    htmlcode.Append("</td>");
                    htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[2].ToString());
                    htmlcode.Append("</td>");
                    TotSal = Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                    Total[TotCount] = Total[TotCount] + TotSal;
                    for (i = 0; i < dsVat.Tables[0].Rows.Count; i++)
                    {

                        TotCount = TotCount + 1;
                        if (ds1.Tables[0].Rows[j].ItemArray[6].ToString() == dsVat.Tables[0].Rows[i].ItemArray[0].ToString())
                        {
                            if (ds1.Tables[0].Rows[j].ItemArray[6].ToString() == "0")
                            {
                                if (ds1.Tables[0].Rows[j].ItemArray[7].ToString() == "0")
                                {
                                    htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                                    htmlcode.Append("</td>");
                                    Total[TotCount] = Total[TotCount] + Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                                    TotSal = TotSal + ((TotSal * Convert.ToDouble(Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString()))) / 100);
                                    //htmlcode.Append("<td>" + ((Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString())) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString())) / 100);
                                    //htmlcode.Append("</td>");

                                }
                                else
                                {

                                    htmlcode.Append("<td>");
                                    htmlcode.Append("</td>");
                                    Total[TotCount] = Total[TotCount] + 0;
                                }
                            }
                            else
                            {
                                htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                                htmlcode.Append("</td>");

                                TotSal = TotSal + ((TotSal * Convert.ToDouble(Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString()))) / 100);
                                htmlcode.Append("<td>" + ((Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString())) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString())) / 100);
                                htmlcode.Append("</td>");
                                Total[TotCount] = Total[TotCount] + Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                                TotCount = TotCount + 1;
                                Total[TotCount] = Total[TotCount] + (Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString()) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString()) / 100);
                            }
                        }
                        else
                        {
                            if (dsVat.Tables[0].Rows[i].ItemArray[0].ToString() == "0")
                            {
                                htmlcode.Append("<td>");
                                htmlcode.Append("</td>");
                                Total[TotCount] = Total[TotCount] + 0;
                            }
                            else
                            {
                                htmlcode.Append("<td>");
                                htmlcode.Append("</td>");
                                htmlcode.Append("<td>");
                                htmlcode.Append("</td>");
                                Total[TotCount] = Total[TotCount] + 0;
                                TotCount = TotCount + 1;
                                Total[TotCount] = Total[TotCount] + 0;

                            }
                        }
                    }
                    for (i = 1; i < dsCst.Tables[0].Rows.Count; i++)
                    {

                        TotCount = TotCount + 1;
                        if (ds1.Tables[0].Rows[j].ItemArray[7].ToString() == dsCst.Tables[0].Rows[i].ItemArray[0].ToString())
                        {
                            htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                            htmlcode.Append("</td>");
                            TotSal = TotSal + ((TotSal * Convert.ToDouble(Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[7].ToString()))) / 100);
                            htmlcode.Append("<td>" + ((Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString())) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[7].ToString())) / 100);
                            htmlcode.Append("</td>");
                            Total[TotCount] = Total[TotCount] + Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                            TotCount = TotCount + 1;
                            Total[TotCount] = Total[TotCount] + (Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString()) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[7].ToString()) / 100);
                        }
                        else
                        {
                            htmlcode.Append("<td>");
                            htmlcode.Append("</td>");
                            htmlcode.Append("<td>");
                            htmlcode.Append("</td>");
                            Total[TotCount] = Total[TotCount] + 0;
                            TotCount = TotCount + 1;
                            Total[TotCount] = Total[TotCount] + 0;
                        }
                    }
                    TotCount = TotCount + 1;
                    Total[TotCount] = Total[TotCount] + TotSal;
                    htmlcode.Append("<td>" + TotSal);
                    htmlcode.Append("</td>");
                    htmlcode.Append("</tr>");

                }
                htmlcode.Append("<tr class=ReportFooterRow>");
                htmlcode.Append("<td>");
                htmlcode.Append("</td>");
                htmlcode.Append("<td>");
                htmlcode.Append("</td>");
                htmlcode.Append("<td>");
                htmlcode.Append("</td>");
                TotCount = 1;
                for (i = 0; i < dsVat.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        htmlcode.Append("<td>" + Total[TotCount]);
                        htmlcode.Append("</td>");
                        TotCount = TotCount + 1;
                    }
                    else
                    {
                        htmlcode.Append("<td>" + Total[TotCount]);
                        htmlcode.Append("</td>");
                        TotCount = TotCount + 1;
                        htmlcode.Append("<td>" + Total[TotCount]);
                        htmlcode.Append("</td>");
                        TotCount = TotCount + 1;
                    }
                }
                for (i = 1; i < dsCst.Tables[0].Rows.Count; i++)
                {
                    htmlcode.Append("<td>" + Total[TotCount]);
                    htmlcode.Append("</td>");
                    TotCount = TotCount + 1;
                    htmlcode.Append("<td>" + Total[TotCount]);
                    htmlcode.Append("</td>");
                    TotCount = TotCount + 1;
                }
                htmlcode.Append("<td>" + Total[TotCount]);
                htmlcode.Append("</td>");
                htmlcode.Append("</tr>");
                htmlcode.Append("</div>");
                htmlcode.Append("</Table>");

                //htmlcode.Append("</form>");
                htmlcode.Append("</body></html>");

                string s = htmlcode.ToString();
                divReport.InnerHtml = htmlcode.ToString();

                ExportToExcel();
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
            System.Text.StringBuilder htmlcode = new System.Text.StringBuilder();
            htmlcode.Append("<html><body>");
            //htmlcode.Append("<form id=form1 runat=server>");
            htmlcode.Append("<div id=divPrint style=font-family:'Trebuchet MS'; font-size:10px;  >");

            htmlcode.Append("<Table id = table1 border=1px solid blue cellpadding=0 cellspacing=0 class=tblLeft width=100% >");
            DataSet dsVat = new DataSet();
            DataSet dsCst = new DataSet();
            DataSet ds1 = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            htmlcode.Append("<tr class=ReportHeadataRow style=text-align:left>");
            htmlcode.Append("<td> BILL NO");
            htmlcode.Append("</td>");
            htmlcode.Append("<td> Ledger Name");
            htmlcode.Append("</td>");
            htmlcode.Append("<td> TIN Number");
            htmlcode.Append("</td>");

            int i = 0;
            int j = 0;
            double TotSal = 0;
            dsVat = bl.ListPurchaseVat();

            if (dsVat != null)
            {
                for (i = 0; i < dsVat.Tables[0].Rows.Count; i++)
                {
                    if (dsVat.Tables[0].Rows[i].ItemArray[0].ToString() == "0")
                    {
                        htmlcode.Append("<td> Exempted");
                        htmlcode.Append("</td>");

                    }
                    else
                    {
                        htmlcode.Append("<td>" + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + " Purchase Value");
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td>" + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + " Input VAT");
                        htmlcode.Append("</td>");
                    }
                }
            }
            dsCst = bl.ListPurchaseCst();
            if (dsCst != null)
            {
                for (i = 1; i < dsCst.Tables[0].Rows.Count; i++)
                {
                    htmlcode.Append("<td>" + dsCst.Tables[0].Rows[i].ItemArray[0].ToString() + " Purchase Value");
                    htmlcode.Append("</td>");
                    htmlcode.Append("<td>" + dsCst.Tables[0].Rows[i].ItemArray[0].ToString() + " Input CST");
                    htmlcode.Append("</td>");
                }
            }
            htmlcode.Append("<td> Total Sales");
            htmlcode.Append("</td>");
            htmlcode.Append("</tr>");



            Double[] Total;
            Total = new double[50];
            int TotCount = 0;

            ds1 = bl.ListPurchaseVatCstAmtDet(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToInt16(drpLedgerName.SelectedValue), Convert.ToInt16(drpVat.SelectedValue), Convert.ToInt16(drpCst.SelectedValue));
            if (ds1 != null)
            {
                for (j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    TotCount = 0;
                    htmlcode.Append("<tr class=ReportdataRow>");
                    htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[0].ToString());
                    htmlcode.Append("</td>");
                    htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[1].ToString());
                    htmlcode.Append("</td>");
                    htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[2].ToString());
                    htmlcode.Append("</td>");
                    TotSal = Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                    Total[TotCount] = Total[TotCount] + TotSal;
                    for (i = 0; i < dsVat.Tables[0].Rows.Count; i++)
                    {

                        TotCount = TotCount + 1;
                        if (ds1.Tables[0].Rows[j].ItemArray[6].ToString() == dsVat.Tables[0].Rows[i].ItemArray[0].ToString())
                        {
                            if (ds1.Tables[0].Rows[j].ItemArray[6].ToString() == "0")
                            {
                                if (ds1.Tables[0].Rows[j].ItemArray[7].ToString() == "0")
                                {
                                    htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                                    htmlcode.Append("</td>");
                                    Total[TotCount] = Total[TotCount] + Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                                    TotSal = TotSal + ((TotSal * Convert.ToDouble(Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString()))) / 100);
                                    //htmlcode.Append("<td>" + ((Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString())) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString())) / 100);
                                    //htmlcode.Append("</td>");

                                }
                                else
                                {

                                    htmlcode.Append("<td>");
                                    htmlcode.Append("</td>");
                                    Total[TotCount] = Total[TotCount] + 0;
                                }
                            }
                            else
                            {
                                htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                                htmlcode.Append("</td>");

                                TotSal = TotSal + ((TotSal * Convert.ToDouble(Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString()))) / 100);
                                htmlcode.Append("<td>" + ((Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString())) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString())) / 100);
                                htmlcode.Append("</td>");
                                Total[TotCount] = Total[TotCount] + Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                                TotCount = TotCount + 1;
                                Total[TotCount] = Total[TotCount] + (Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString()) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString()) / 100);
                            }
                        }
                        else
                        {
                            if (dsVat.Tables[0].Rows[i].ItemArray[0].ToString() == "0")
                            {
                                htmlcode.Append("<td>");
                                htmlcode.Append("</td>");
                                Total[TotCount] = Total[TotCount] + 0;
                            }
                            else
                            {
                                htmlcode.Append("<td>");
                                htmlcode.Append("</td>");
                                htmlcode.Append("<td>");
                                htmlcode.Append("</td>");
                                Total[TotCount] = Total[TotCount] + 0;
                                TotCount = TotCount + 1;
                                Total[TotCount] = Total[TotCount] + 0;

                            }
                        }
                    }
                    for (i = 1; i < dsCst.Tables[0].Rows.Count; i++)
                    {

                        TotCount = TotCount + 1;
                        if (ds1.Tables[0].Rows[j].ItemArray[7].ToString() == dsCst.Tables[0].Rows[i].ItemArray[0].ToString())
                        {
                            htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                            htmlcode.Append("</td>");
                            TotSal = TotSal + ((TotSal * Convert.ToDouble(Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[7].ToString()))) / 100);
                            htmlcode.Append("<td>" + ((Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString())) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[7].ToString())) / 100);
                            htmlcode.Append("</td>");
                            Total[TotCount] = Total[TotCount] + Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
                            TotCount = TotCount + 1;
                            Total[TotCount] = Total[TotCount] + (Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString()) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[7].ToString()) / 100);
                        }
                        else
                        {
                            htmlcode.Append("<td>");
                            htmlcode.Append("</td>");
                            htmlcode.Append("<td>");
                            htmlcode.Append("</td>");
                            Total[TotCount] = Total[TotCount] + 0;
                            TotCount = TotCount + 1;
                            Total[TotCount] = Total[TotCount] + 0;
                        }
                    }
                    TotCount = TotCount + 1;
                    Total[TotCount] = Total[TotCount] + TotSal;
                    htmlcode.Append("<td>" + TotSal);
                    htmlcode.Append("</td>");
                    htmlcode.Append("</tr>");

                }
                htmlcode.Append("<tr class=ReportFooterRow>");
                htmlcode.Append("<td>");
                htmlcode.Append("</td>");
                htmlcode.Append("<td>");
                htmlcode.Append("</td>");
                htmlcode.Append("<td>");
                htmlcode.Append("</td>");
                TotCount = 1;
                for (i = 0; i < dsVat.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        htmlcode.Append("<td>" + Total[TotCount]);
                        htmlcode.Append("</td>");
                        TotCount = TotCount + 1;
                    }
                    else
                    {
                        htmlcode.Append("<td>" + Total[TotCount]);
                        htmlcode.Append("</td>");
                        TotCount = TotCount + 1;
                        htmlcode.Append("<td>" + Total[TotCount]);
                        htmlcode.Append("</td>");
                        TotCount = TotCount + 1;
                    }
                }
                for (i = 1; i < dsCst.Tables[0].Rows.Count; i++)
                {
                    htmlcode.Append("<td>" + Total[TotCount]);
                    htmlcode.Append("</td>");
                    TotCount = TotCount + 1;
                    htmlcode.Append("<td>" + Total[TotCount]);
                    htmlcode.Append("</td>");
                    TotCount = TotCount + 1;
                }
                htmlcode.Append("<td>" + Total[TotCount]);
                htmlcode.Append("</td>");
                htmlcode.Append("</tr>");
                htmlcode.Append("</div>");
                htmlcode.Append("</Table>");

                //htmlcode.Append("</form>");
                htmlcode.Append("</body></html>");

                string s = htmlcode.ToString();
                divReport.InnerHtml = htmlcode.ToString();
                divmain.Visible = false;

                div1.Visible = true;

                DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
                DateTime endDate = Convert.ToDateTime(txtEndDate.Text);
                int LedgerName = Convert.ToInt32(drpLedgerName.SelectedValue);
                int Vat = Convert.ToInt32(drpVat.SelectedValue);
                int Cst = Convert.ToInt32(drpCst.SelectedValue);

                Response.Write("<script language='javascript'> window.open('PurchaseVatCstSummaryReport1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + "&LedgerName=" + LedgerName + "&Vat=" + Vat + "&Cst=" + Cst + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
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

    protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
            div1.Visible = true;
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

            string file = "Purchase Vat/Cst Summary Report_" + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition",

             "attachment;filename=" + file);

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);





            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "&nbsp;";

            TableCell cell2 = new TableCell();

            cell2.Text = "&nbsp;";


            TableCell cell3 = new TableCell();

            cell3.Controls.Add(divReport);



            tr1.Cells.Add(cell1);

            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);

            TableRow tr3 = new TableRow();

            tr3.Cells.Add(cell3);



            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.Rows.Add(tr3);




            tb.RenderControl(hw);


            string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            Response.Write(style);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
        }
    }


    public void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public void gvSecond_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}
