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

public partial class CSTSumaryReport1 : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();


                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

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

                dvCST.Visible = false;



                DateTime startDate;
                DateTime endDate;
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

                GetCSTReport(startDate, endDate);
                dvCST.Visible = true;
                div1.Visible = false;
            }

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
            /*March 17*/
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            /*March 17*/
            GetCSTReport(startDate, endDate);
            dvCST.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void GetCSTReport(DateTime startDate, DateTime endDate)
    {

        if (Request.Cookies["Company"]  != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        DataSet purchaseDs = new DataSet();
        DataSet salesDs = new DataSet();
        DataSet purchaseReturnDs = new DataSet();
        DataSet SalesReturnDs = new DataSet();

        string strPurchaseCST = string.Empty;
        string strSalesCST = string.Empty;

        double purchaseActual = 0.0;
        double salesActual = 0.0;
        double purchaseReturnActual = 0.0;
        double salesReturnActual = 0.0;
        double sumPurchaseActual = 0.0;
        double sumSalesActual = 0.0;
        double purchaseCST = 0.0;
        double salesCST = 0.0;
        double purchaseReturnCST = 0.0;
        double salesReturnCST = 0.0;
        double sumPurchaseCST = 0.0;
        double sumSalesCST = 0.0;
        double netPurchaseCST = 0.0;
        double netSalesCST = 0.0;
        double netPurchaseActual = 0.0;
        double netSalesActual = 0.0;
        double CST = 0;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet CSTDs = bl.avlCST("purchase", sDataSource);

        lblFromDate.Text = startDate.ToShortDateString();
        lblToDate.Text = endDate.ToShortDateString();

        if (CSTDs != null)
        {
            if (CSTDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in CSTDs.Tables[0].Rows)
                {
                    if (dr["CST"] != null && dr["CST"] != DBNull.Value)
                    {
                        CST = Convert.ToDouble(Convert.ToDouble(dr["CST"]).ToString("f2"));
                    }
                    if (CST > 0)
                    {
                        purchaseDs = bl.purchaseCSTSummary(startDate, endDate, CST, "No");
                        purchaseReturnDs = bl.salesCSTSummary(startDate, endDate, CST, "Yes");

                        if (purchaseDs != null)
                        {
                            if (purchaseDs.Tables[0] != null)
                            {
                                if (purchaseDs.Tables[0].Rows.Count > 0)
                                {
                                    if (purchaseDs.Tables[0].Rows[0]["CSTPaid"] != null && purchaseDs.Tables[0].Rows[0]["CSTPaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(purchaseDs.Tables[0].Rows[0]["CSTPaid"]) != string.Empty)
                                            purchaseCST = Convert.ToDouble(purchaseDs.Tables[0].Rows[0]["CSTPaid"]);
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
                                    if (purchaseReturnDs.Tables[0].Rows[0]["CSTPaid"] != null && purchaseReturnDs.Tables[0].Rows[0]["CSTPaid"] != DBNull.Value)
                                    {

                                        purchaseReturnCST = Convert.ToDouble(purchaseReturnDs.Tables[0].Rows[0]["CSTPaid"]);
                                    }
                                    if (purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"] != null && purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"]) != string.Empty)
                                            purchaseReturnActual = Convert.ToDouble(purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"]);
                                    }
                                }
                            }
                        }

                        sumPurchaseCST = purchaseCST - purchaseReturnCST;
                        sumPurchaseActual = purchaseActual - purchaseReturnActual;
                        if (sumPurchaseCST > 0 && sumPurchaseActual > 0)
                            strPurchaseCST = strPurchaseCST + "Input CST @" + CST.ToString("f2") + "%~" + sumPurchaseActual.ToString("f2") + "~" + sumPurchaseCST.ToString("f2") + "^";

                        netPurchaseCST = netPurchaseCST + sumPurchaseCST;
                        netPurchaseActual = netPurchaseActual + sumPurchaseActual;





                        purchaseCST = 0;
                        purchaseActual = 0;
                        purchaseReturnActual = 0;
                        purchaseReturnCST = 0;

                    }

                }
            }
        }
        CSTDs = bl.avlCST("sales", sDataSource);
        if (CSTDs != null)
        {
            if (CSTDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in CSTDs.Tables[0].Rows)
                {

                    if (dr["CST"] != null && dr["CST"] != DBNull.Value)
                    {
                        CST = Convert.ToDouble(Convert.ToDouble(dr["CST"]).ToString("f2"));
                    }
                    if (CST > 0)
                    {

                        SalesReturnDs = bl.purchaseCSTSummary(startDate, endDate, CST, "Yes");
                        salesDs = bl.salesCSTSummary(startDate, endDate, CST, "No");



                        if (salesDs != null)
                        {
                            if (salesDs.Tables[0] != null)
                            {
                                if (salesDs.Tables[0].Rows.Count > 0)
                                {
                                    if (salesDs.Tables[0].Rows[0]["CSTPaid"] != null && salesDs.Tables[0].Rows[0]["CSTPaid"] != DBNull.Value)
                                    {
                                        salesCST = Convert.ToDouble(salesDs.Tables[0].Rows[0]["CSTPaid"]);
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
                                    if (SalesReturnDs.Tables[0].Rows[0]["CSTPaid"] != null && SalesReturnDs.Tables[0].Rows[0]["CSTPaid"] != DBNull.Value)
                                    {
                                        salesReturnCST = Convert.ToDouble(SalesReturnDs.Tables[0].Rows[0]["CSTPaid"]);
                                    }
                                    if (SalesReturnDs.Tables[0].Rows[0]["Actualpaid"] != null && SalesReturnDs.Tables[0].Rows[0]["Actualpaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(SalesReturnDs.Tables[0].Rows[0]["Actualpaid"]) != string.Empty)
                                            salesReturnActual = Convert.ToDouble(SalesReturnDs.Tables[0].Rows[0]["Actualpaid"]);
                                    }
                                }
                            }
                        }

                        sumSalesCST = salesCST - salesReturnCST;
                        sumSalesActual = salesActual - salesReturnActual;
                        if (sumSalesCST > 0 && sumSalesActual > 0)
                            strSalesCST = strSalesCST + "Output CST @" + CST.ToString("f2") + "%~" + sumSalesActual.ToString("f2") + "~" + sumSalesCST.ToString("f2") + "^";
                        netSalesCST = netSalesCST + sumSalesCST;
                        netSalesActual = netSalesActual + sumSalesActual;

                        salesActual = 0;
                        salesReturnActual = 0;
                        salesCST = 0;
                        salesReturnCST = 0;
                    }

                }
            }
        }

        lblActualPurchase.Text = netPurchaseActual.ToString("f2");
        lblActualSales.Text = netSalesActual.ToString("f2");
        lblCSTPurchase.Text = netPurchaseCST.ToString("f2");
        lblCSTSales.Text = netSalesCST.ToString("f2");
        lblCSTPayable.Text = (Convert.ToDouble(lblCSTSales.Text) - Convert.ToDouble(lblCSTPurchase.Text)).ToString();
        //lblCSTPayable.Text = (netSalesCST - netPurchaseCST).ToString("f2");
        if (strPurchaseCST.EndsWith("^"))
        {
            strPurchaseCST = strPurchaseCST.Remove(strPurchaseCST.Length - 1, 1);
        }
        if (strSalesCST.EndsWith("^"))
        {
            strSalesCST = strSalesCST.Remove(strSalesCST.Length - 1, 1);
        }

        if (strSalesCST != "")
            GenerateSalesCST(strSalesCST);
        if (strPurchaseCST != "")
            GeneratePurchaceCST(strPurchaseCST);
    }
    public void GenerateSalesCST(string CSTStr)
    {
        DataSet grdDs = new DataSet();
        DataTable grdDt = new DataTable();
        DataTable dtNew = new DataTable();
        dtNew = GenerateDs();
        grdDs.Tables.Add(dtNew);
        string[] mainRec = CSTStr.Split('^');
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
        grdSalesCST.DataSource = grdDs;
        grdSalesCST.DataBind();
    }
    public void GeneratePurchaceCST(string CSTStr)
    {
        DataSet grdDs = new DataSet();
        DataTable grdDt = new DataTable();
        DataTable dtNew = new DataTable();
        dtNew = GenerateDs();
        grdDs.Tables.Add(dtNew);
        string[] mainRec = CSTStr.Split('^');
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
        grdPurchaseCST.DataSource = grdDs;
        grdPurchaseCST.DataBind();
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

        dc = new DataColumn("CST");
        dt.Columns.Add(dc);



        dr = dt.NewRow();
        dr["OutputTax"] = "";

        dr["Actual"] = "";
        dr["CST"] = "";

        dt.Rows.Add(dr);

        return dt;
    }
    public DataTable GenerateDs(string Outout, string Actual, string CST)
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

        dc = new DataColumn("CST");
        dt.Columns.Add(dc);  //ds.Tables.Add(dt);


        dr = dt.NewRow();
        dr["OutputTax"] = Outout;

        dr["Actual"] = Actual;
        dr["CST"] = CST;
        dt.Rows.Add(dr);


        return dt;
    }
    protected void btnCSTDetailedReport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("CSTReprt.aspx", true);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
