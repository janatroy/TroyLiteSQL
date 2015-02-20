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

public partial class VATSumaryReport1 : System.Web.UI.Page
{

    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                dvVat.Visible = false;
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                if (Session["startDate"] != null)
                {
                    txtStartDate.Text = Session["startDate"].ToString();
                }
                if (Session["endDate"] != null)
                {
                    txtEndDate.Text = Session["endDate"].ToString();
                }

                //if (month > 4)
                //{
                //    strFDate = "01-April-" + Convert.ToString(year);
                //    strTDate = "31-March-" + Convert.ToString(year+1);
                //    sDate = "01/04/" + Convert.ToString(year);
                //    eDate = "31/03/" + Convert.ToString(year+1);
                //}
                //else
                //{
                //    strFDate = "01-April-" + Convert.ToString(year - 1);
                //    strTDate = "31-March-" + Convert.ToString(year);
                //    sDate = "01/04/" + Convert.ToString(year-1);
                //    eDate = "31/03/" + Convert.ToString(year);
                //}


                //lblFromDate.Text = strFDate;
                //lblToDate.Text = strTDate;
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

                    if (companyInfo != null)
                    {
                        if (companyInfo.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in companyInfo.Tables[0].Rows)
                            {

                                lblCompany.Text = Convert.ToString(dr["CompanyName"]);



                            }
                        }
                    }
                }




                /*March 17*/
                DateTime startDate, endDate;

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

                /*March 17*/
                GetVatReport(startDate, endDate);
                dvVat.Visible = true;
                Session["startDate"] = startDate;
                Session["endDate"] = endDate;
                dvVat.Visible = true;
                div1.Visible = false;

            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void btnVATDetailedReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("VATReprt.aspx", true);
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            /*March 17*/
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            /*March 17*/
            GetVatReport(startDate, endDate);
            dvVat.Visible = true;
            Session["startDate"] = txtStartDate.Text;
            Session["endDate"] = txtEndDate.Text;
            dvVat.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void GetVatReport(DateTime startDate, DateTime endDate)
    {

        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        DataSet purchaseDs = new DataSet();
        DataSet salesDs = new DataSet();
        DataSet purchaseReturnDs = new DataSet();
        DataSet SalesReturnDs = new DataSet();

        string strPurchaseVat = string.Empty;
        string strSalesVat = string.Empty;

        double purchaseActual = 0.0;
        double salesActual = 0.0;
        double purchaseReturnActual = 0.0;
        double salesReturnActual = 0.0;
        double sumPurchaseActual = 0.0;
        double sumSalesActual = 0.0;
        double purchaseVAT = 0.0;
        double salesVAT = 0.0;
        double purchaseReturnVAT = 0.0;
        double salesReturnVAT = 0.0;
        double sumPurchaseVAT = 0.0;
        double sumSalesVAT = 0.0;
        double netPurchaseVAT = 0.0;
        double netSalesVAT = 0.0;
        double netPurchaseActual = 0.0;
        double netSalesActual = 0.0;
        double vat = 0;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet vatDs = bl.avlVAT("purchase", sDataSource);

        lblFromDate.Text = startDate.ToShortDateString();
        lblToDate.Text = endDate.ToShortDateString();

        if (vatDs != null)
        {
            if (vatDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in vatDs.Tables[0].Rows)
                {
                    if (dr["vat"] != null && dr["vat"] != DBNull.Value)
                    {
                        vat = Convert.ToDouble(dr["vat"]);
                    }

                    if (vat > 0)
                    {
                        purchaseDs = bl.purchaseVatSummary(startDate, endDate, vat, "No");
                        purchaseReturnDs = bl.salesVatSummary(startDate, endDate, vat, "Yes");

                        if (purchaseDs != null)
                        {
                            if (purchaseDs.Tables[0] != null)
                            {
                                if (purchaseDs.Tables[0].Rows.Count > 0)
                                {
                                    if (purchaseDs.Tables[0].Rows[0]["VatPaid"] != null && purchaseDs.Tables[0].Rows[0]["VatPaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(purchaseDs.Tables[0].Rows[0]["VatPaid"]) != string.Empty)
                                            purchaseVAT = Convert.ToDouble(purchaseDs.Tables[0].Rows[0]["VatPaid"]);
                                    }
                                    if (purchaseDs.Tables[0].Rows[0]["Actualpaid"] != null && purchaseDs.Tables[0].Rows[0]["Actualpaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(purchaseDs.Tables[0].Rows[0]["Actualpaid"]) != string.Empty)
                                            purchaseActual = Convert.ToDouble(purchaseDs.Tables[0].Rows[0]["Actualpaid"]);
                                    }
                                }
                            }
                        }
                        if (purchaseReturnDs != null)
                        {
                            if (purchaseReturnDs.Tables[0] != null)
                            {
                                if (purchaseReturnDs.Tables[0].Rows.Count > 0)
                                {
                                    if (purchaseReturnDs.Tables[0].Rows[0]["VatPaid"] != null && purchaseReturnDs.Tables[0].Rows[0]["VatPaid"] != DBNull.Value)
                                    {

                                        purchaseReturnVAT = Convert.ToDouble(purchaseReturnDs.Tables[0].Rows[0]["VatPaid"]);
                                    }
                                    if (purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"] != null && purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"]) != string.Empty)
                                            purchaseReturnActual = Convert.ToDouble(purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"]);
                                    }
                                }
                            }
                        }

                        sumPurchaseVAT = purchaseVAT - purchaseReturnVAT;
                        sumPurchaseActual = purchaseActual - purchaseReturnActual;
                        if (sumPurchaseVAT > 0 && sumPurchaseActual > 0)
                            strPurchaseVat = strPurchaseVat + "Input VAT @" + vat + "%~" + sumPurchaseActual.ToString("f2") + "~" + sumPurchaseVAT.ToString("f2") + "^";

                        netPurchaseVAT = netPurchaseVAT + sumPurchaseVAT;
                        netPurchaseActual = netPurchaseActual + sumPurchaseActual;





                        purchaseVAT = 0;
                        purchaseActual = 0;
                        purchaseReturnActual = 0;
                        purchaseReturnVAT = 0;

                    }

                }
            }
        }
        vatDs = bl.avlVAT("sales", sDataSource);
        if (vatDs != null)
        {
            if (vatDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in vatDs.Tables[0].Rows)
                {

                    if (dr["vat"] != null && dr["vat"] != DBNull.Value)
                    {
                        vat = Convert.ToDouble(dr["vat"]);
                    }
                    if (vat > 0)
                    {

                        SalesReturnDs = bl.purchaseVatSummary(startDate, endDate, vat, "Yes");
                        salesDs = bl.salesVatSummary(startDate, endDate, vat, "No");



                        if (salesDs != null)
                        {
                            if (salesDs.Tables[0] != null)
                            {
                                if (salesDs.Tables[0].Rows.Count > 0)
                                {
                                    if (salesDs.Tables[0].Rows[0]["VatPaid"] != null && salesDs.Tables[0].Rows[0]["VatPaid"] != DBNull.Value)
                                    {
                                        salesVAT = Convert.ToDouble(salesDs.Tables[0].Rows[0]["VatPaid"]);
                                    }
                                    if (salesDs.Tables[0].Rows[0]["Actualpaid"] != null && salesDs.Tables[0].Rows[0]["Actualpaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(salesDs.Tables[0].Rows[0]["Actualpaid"]) != string.Empty)
                                            salesActual = Convert.ToDouble(salesDs.Tables[0].Rows[0]["Actualpaid"]);
                                    }
                                }
                            }
                        }
                        if (SalesReturnDs != null)
                        {
                            if (SalesReturnDs.Tables[0] != null)
                            {
                                if (SalesReturnDs.Tables[0].Rows.Count > 0)
                                {
                                    if (SalesReturnDs.Tables[0].Rows[0]["VatPaid"] != null && SalesReturnDs.Tables[0].Rows[0]["VatPaid"] != DBNull.Value)
                                    {
                                        salesReturnVAT = Convert.ToDouble(SalesReturnDs.Tables[0].Rows[0]["VatPaid"]);
                                    }
                                    if (SalesReturnDs.Tables[0].Rows[0]["Actualpaid"] != null && SalesReturnDs.Tables[0].Rows[0]["Actualpaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(SalesReturnDs.Tables[0].Rows[0]["Actualpaid"]) != string.Empty)
                                            salesReturnActual = Convert.ToDouble(SalesReturnDs.Tables[0].Rows[0]["Actualpaid"]);
                                    }
                                }
                            }
                        }

                        sumSalesVAT = salesVAT - salesReturnVAT;
                        sumSalesActual = salesActual - salesReturnActual;
                        if (sumSalesVAT > 0 && sumSalesActual > 0)
                            strSalesVat = strSalesVat + "Output VAT @" + vat + "%~" + sumSalesActual.ToString("f2") + "~" + sumSalesVAT.ToString("f2") + "^";
                        netSalesVAT = netSalesVAT + sumSalesVAT;
                        netSalesActual = netSalesActual + sumSalesActual;

                        salesActual = 0;
                        salesReturnActual = 0;
                        salesVAT = 0;
                        salesReturnVAT = 0;
                    }

                }
            }
        }

        lblActualPurchase.Text = netPurchaseActual.ToString("f2");
        lblActualSales.Text = netSalesActual.ToString("f2");
        lblVatPurchase.Text = netPurchaseVAT.ToString("f2");
        lblVatSales.Text = netSalesVAT.ToString("f2");

        lblVatPayable.Text = (Convert.ToDouble(lblVatSales.Text) - Convert.ToDouble(lblVatPurchase.Text)).ToString();//(netSalesVAT - netPurchaseVAT).ToString("f2");
        if (strPurchaseVat.EndsWith("^"))
        {
            strPurchaseVat = strPurchaseVat.Remove(strPurchaseVat.Length - 1, 1);
        }
        if (strSalesVat.EndsWith("^"))
        {
            strSalesVat = strSalesVat.Remove(strSalesVat.Length - 1, 1);
        }

        if (strSalesVat != "")
            GenerateSalesVat(strSalesVat);
        if (strPurchaseVat != "")
            GeneratePurchaceVat(strPurchaseVat);
    }
    public void GenerateSalesVat(string vatStr)
    {
        DataSet grdDs = new DataSet();
        DataTable grdDt = new DataTable();
        DataTable dtNew = new DataTable();
        dtNew = GenerateDs();
        grdDs.Tables.Add(dtNew);
        string[] mainRec = vatStr.Split('^');
        for (int k = 0; k <= mainRec.Length - 1; k++)
        {
            string[] childRec = mainRec[k].Split('~');
            grdDt = GenerateDs(childRec[0].ToString(), childRec[1].ToString(), childRec[2].ToString());
            if (grdDt != null)
            {
                for (int m = 0; m <= grdDt.Rows.Count - 1; m++)
                {

                    if (grdDt != null && grdDt.Rows.Count > 0)
                        grdDs.Tables[0].ImportRow(grdDt.Rows[m]);
                }
            }
        }
        grdSalesVat.DataSource = grdDs;
        grdSalesVat.DataBind();
    }
    public void GeneratePurchaceVat(string vatStr)
    {
        DataSet grdDs = new DataSet();
        DataTable grdDt = new DataTable();
        DataTable dtNew = new DataTable();
        dtNew = GenerateDs();
        grdDs.Tables.Add(dtNew);
        string[] mainRec = vatStr.Split('^');
        for (int k = 0; k <= mainRec.Length - 1; k++)
        {
            string[] childRec = mainRec[k].Split('~');
            grdDt = GenerateDs(childRec[0].ToString(), childRec[1].ToString(), childRec[2].ToString());
            if (grdDt != null)
            {
                for (int m = 0; m <= grdDt.Rows.Count - 1; m++)
                {

                    if (grdDt != null && grdDt.Rows.Count > 0)
                        grdDs.Tables[0].ImportRow(grdDt.Rows[m]);
                }
            }
        }
        grdPurchaseVat.DataSource = grdDs;
        grdPurchaseVat.DataBind();
    }
    public DataTable GenerateDs()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;


        dc = new DataColumn("OutputTax");
        dt.Columns.Add(dc);

        dc = new DataColumn("Actual");
        dt.Columns.Add(dc);

        dc = new DataColumn("Vat");
        dt.Columns.Add(dc);



        dr = dt.NewRow();
        dr["OutputTax"] = "";

        dr["Actual"] = "";
        dr["Vat"] = "";

        dt.Rows.Add(dr);

        return dt;
    }
    public DataTable GenerateDs(string Outout, string Actual, string VAT)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;
        int days = 0;
        dc = new DataColumn("OutputTax");
        dt.Columns.Add(dc);

        dc = new DataColumn("Actual");
        dt.Columns.Add(dc);

        dc = new DataColumn("Vat");
        dt.Columns.Add(dc);  //ds.Tables.Add(dt);


        dr = dt.NewRow();
        dr["OutputTax"] = Outout;

        dr["Actual"] = Actual;
        dr["Vat"] = VAT;
        dt.Rows.Add(dr);


        return dt;
    }
}
