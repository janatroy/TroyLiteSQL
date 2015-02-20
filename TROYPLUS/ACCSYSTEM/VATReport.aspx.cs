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

public partial class VATReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double grossPurchaseTotal = 0.0d;
    Double grossPurchaseReturnTotal = 0.0d;
    Double grossSalesTotal = 0.0d;
    Double grossSalesReturnTotal = 0.0d;
    public string vatPurchaseString = string.Empty;
    public string vatSalesString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblVatRate = e.Row.FindControl("lblVatRate") as Label;
                Label lblVatPaid = e.Row.FindControl("lblVatPaid") as Label;

                if (lblRate != null && lblVatRate != null)
                    grossVat = Convert.ToDouble(lblVatRate.Text) - Convert.ToDouble(lblRate.Text);

                grossPurchaseTotal = grossPurchaseTotal + grossVat;
                lblVatPaid.Text = grossVat.ToString("f2");



            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossTotal = e.Row.FindControl("lblGrossTotal") as Label;
                lblGrossTotal.Text = grossPurchaseTotal.ToString("f2");
                sumPurchase.Text = lblGrossTotal.Text;
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

                grossPurchaseReturnTotal = grossPurchaseReturnTotal + grossVat;
                lblVatPaid.Text = grossVat.ToString("f2");

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossTotal = e.Row.FindControl("lblGrossTotal") as Label;
                lblGrossTotal.Text = grossPurchaseReturnTotal.ToString("f2");
                sumPurchaseReturn.Text = lblGrossTotal.Text;
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

                grossSalesTotal = grossSalesTotal + grossVat;
                lblVatPaid.Text = grossVat.ToString("f2");

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossTotal = e.Row.FindControl("lblGrossTotal") as Label;
                lblGrossTotal.Text = grossSalesTotal.ToString("f2");
                sumSales.Text = lblGrossTotal.Text;
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

                grossSalesReturnTotal = grossSalesReturnTotal + grossVat;
                lblVatPaid.Text = grossVat.ToString("f2");

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossTotal = e.Row.FindControl("lblGrossTotal") as Label;
                lblGrossTotal.Text = grossSalesReturnTotal.ToString("f2");
                sumSalesReturn.Text = lblGrossTotal.Text;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
