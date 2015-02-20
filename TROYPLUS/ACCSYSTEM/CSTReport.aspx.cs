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

public partial class CSTReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double grossPurchaseTotal = 0.0d;
    Double grossPurchaseReturnTotal = 0.0d;
    Double grossSalesTotal = 0.0d;
    Double grossSalesReturnTotal = 0.0d;
    public string CSTPurchaseString = string.Empty;
    public string CSTSalesString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
                BusinessLogic bl = new BusinessLogic(sDataSource);

                DataSet ds = new DataSet();
                DataSet salesDs = new DataSet();
                ds = bl.avlCST("purchase", sDataSource);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        CSTPurchaseString = CSTPurchaseString + dr["CST"].ToString() + ",";
                    }
                }
                if (CSTPurchaseString.EndsWith(","))
                {
                    CSTPurchaseString = CSTPurchaseString.Remove(CSTPurchaseString.Length - 1, 1);
                }

                salesDs = bl.avlCST("sales", sDataSource);
                if (salesDs != null && salesDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drS in salesDs.Tables[0].Rows)
                    {
                        CSTSalesString = CSTSalesString + drS["CST"].ToString() + ",";
                    }
                }
                if (CSTSalesString.EndsWith(","))
                {
                    CSTSalesString = CSTSalesString.Remove(CSTSalesString.Length - 1, 1);
                }
                hdPurchase.Value = CSTPurchaseString;
                hdSales.Value = CSTSalesString;
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
            dvCST.InnerHtml = "";
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
            purchaseDs = rpt.generatePurchaseCSTReport(sDataSource, startDate, endDate, "Yes");
            salesReturnDs = rpt.generatePurchaseCSTReport(sDataSource, startDate, endDate, "No");
            salesDs = rpt.generateSalesCSTReport(sDataSource, startDate, endDate, "Yes");
            purchaseReturnDs = rpt.generateSalesCSTReport(sDataSource, startDate, endDate, "No");

            if (salesDs != null && salesDs.Tables[0].Rows.Count > 0)
            {

                grdSalesCST.DataSource = salesDs;
                grdSalesCST.DataBind();

            }
            else
            {
                grdSalesCST.DataSource = null;
                grdSalesCST.DataBind();
            }
            if (salesReturnDs != null && salesReturnDs.Tables[0].Rows.Count > 0)
            {

                grdSalesReturnCST.DataSource = salesReturnDs;
                grdSalesReturnCST.DataBind();

            }
            else
            {
                grdSalesReturnCST.DataSource = null;
                grdSalesReturnCST.DataBind();
            }
            if (purchaseDs != null && purchaseDs.Tables[0].Rows.Count > 0)
            {

                grdPurchaseCST.DataSource = purchaseDs;
                grdPurchaseCST.DataBind();

            }
            else
            {
                grdPurchaseCST.DataSource = null;
                grdPurchaseCST.DataBind();
            }
            if (purchaseReturnDs != null && purchaseReturnDs.Tables[0].Rows.Count > 0)
            {

                grdPurchaseReturnCST.DataSource = purchaseReturnDs;
                grdPurchaseReturnCST.DataBind();

            }
            else
            {
                grdPurchaseReturnCST.DataSource = null;
                grdPurchaseReturnCST.DataBind();
            }

            CSTSum(purchaseDs, hdPurchase.Value, "Purchase");
            CSTSum(salesDs, hdSales.Value, "Sales");
            CSTSum(purchaseReturnDs, hdSales.Value, "Purchase Return");
            CSTSum(salesReturnDs, hdPurchase.Value, "Sales Return");
            double salesCST = 0;
            double purchaseCST = 0;
            double salesReturnCST = 0;
            double purchaseReturnCST = 0;
            if (sumSales.Text != "")
                salesCST = Convert.ToDouble(sumSales.Text);
            if (sumPurchase.Text != "")
                purchaseCST = Convert.ToDouble(sumPurchase.Text);
            if (sumSalesReturn.Text != "")
                salesReturnCST = Convert.ToDouble(sumSalesReturn.Text);
            if (sumPurchaseReturn.Text != "")
                purchaseReturnCST = Convert.ToDouble(sumPurchaseReturn.Text);

            double netCST = ((salesCST - salesReturnCST) - (purchaseCST - purchaseReturnCST));
            sumCSTToPay.Text = netCST.ToString("f2");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void CSTSum(DataSet ds, string CSTStr, string title)
    {
        string expr = string.Empty;
        double sCST = 0.0;
        DataRow[] pT;
        if (CSTStr != string.Empty)
        {
            string[] arr = CSTStr.Split(',');
            dvCST.InnerHtml = dvCST.InnerHtml + "<br><h5>" + title + "</h5>";
            for (int k = 0; k <= arr.Length - 1; k++)
            {
                if (arr[k].ToString() != "" && arr[k].ToString() != "0")
                {
                    expr = "CST=" + arr[k];
                    pT = ds.Tables[0].Select(expr);
                    if (pT.Length > 0)
                    {
                        sCST = 0;
                        foreach (DataRow dr in pT)
                        {
                            sCST = sCST + (Convert.ToDouble(dr["CSTrate"]) - Convert.ToDouble(dr["rate"]));
                        }
                        dvCST.InnerHtml = dvCST.InnerHtml + arr[k] + "% -  Total Amount Paid : " + sCST.ToString("f2") + "<br>";
                    }
                }

            }

        }
    }
    public void grdPurchaseCST_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double grossCST = 0.0d;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblCSTRate = e.Row.FindControl("lblCSTRate") as Label;
                Label lblCSTPaid = e.Row.FindControl("lblCSTPaid") as Label;

                if (lblRate != null && lblCSTRate != null && lblCSTRate.Text != "")
                    grossCST = Convert.ToDouble(lblCSTRate.Text) - Convert.ToDouble(lblRate.Text);

                grossPurchaseTotal = grossPurchaseTotal + grossCST;
                lblCSTPaid.Text = grossCST.ToString("f2");



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
    public void grdPurchaseReturnCST_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double grossCST = 0.0d;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblCSTRate = e.Row.FindControl("lblCSTRate") as Label;
                Label lblCSTPaid = e.Row.FindControl("lblCSTPaid") as Label;

                if (lblRate != null && lblCSTRate != null && lblCSTRate.Text != "")
                    grossCST = Convert.ToDouble(lblCSTRate.Text) - Convert.ToDouble(lblRate.Text);

                grossPurchaseReturnTotal = grossPurchaseReturnTotal + grossCST;
                lblCSTPaid.Text = grossCST.ToString("f2");

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
    public void grdSalesCST_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double grossCST = 0.0d;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblCSTRate = e.Row.FindControl("lblCSTRate") as Label;
                Label lblCSTPaid = e.Row.FindControl("lblCSTPaid") as Label;

                if (lblRate != null && lblCSTRate != null && lblCSTRate.Text != "")
                    grossCST = Convert.ToDouble(lblCSTRate.Text) - Convert.ToDouble(lblRate.Text);

                grossSalesTotal = grossSalesTotal + grossCST;
                lblCSTPaid.Text = grossCST.ToString("f2");

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
    public void grdSalesReturnCST_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double grossCST = 0.0d;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRate = e.Row.FindControl("lblRate") as Label;
                Label lblCSTRate = e.Row.FindControl("lblCSTRate") as Label;
                Label lblCSTPaid = e.Row.FindControl("lblCSTPaid") as Label;

                if (lblRate != null && lblCSTRate != null && lblCSTRate.Text != "")
                    grossCST = Convert.ToDouble(lblCSTRate.Text) - Convert.ToDouble(lblRate.Text);

                grossSalesReturnTotal = grossSalesReturnTotal + grossCST;
                lblCSTPaid.Text = grossCST.ToString("f2");

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
