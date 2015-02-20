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

public partial class BillSummaryReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();


            if (!IsPostBack)
            {

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dNetRate = 0;
            double dVatRate = 0;
            double dDisRate = 0;
            double dCSTRate = 0;
            double dFrRate = 0;
            double dLURate = 0;
            double dGrandRate = 0;
            double dDiscountRate = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "NetRate") != DBNull.Value)
                    dNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetRate"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualVat") != DBNull.Value)
                    dVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualVat"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualDiscount") != DBNull.Value)
                    dDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "SalesDiscount") != DBNull.Value)
                    dDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualCST") != DBNull.Value)
                    dCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));
                if (DataBinder.Eval(e.Row.DataItem, "SumFreight") != DBNull.Value)
                    dFrRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumFreight"));
                if (DataBinder.Eval(e.Row.DataItem, "Loading") != DBNull.Value)
                    dLURate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Loading"));

                Label lblTotal = (Label)e.Row.FindControl("lblTotal");

                dGrandRate = dDiscountRate + dVatRate + dCSTRate + dFrRate + dLURate;
                lblTotal.Text = dGrandRate.ToString("f2");
                GridView gv = e.Row.FindControl("gvSecond") as GridView;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                DateTime startDate, endDate;
                startDate = Convert.ToDateTime(txtStartDate.Text);
                endDate = Convert.ToDateTime(txtEndDate.Text);
                Session["BillStartDate"] = startDate;
                Session["BillEndDate"] = endDate;
                DataSet ds = bl.SecondLevelSales(startDate, endDate);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        gv.DataBind();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void gvSecond_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        { 
            double dNetRate = 0;
            double dVatRate = 0;
            double dDisRate = 0;
            double dCSTRate = 0;
            double dFrRate = 0;
            double dLURate = 0;
            double dGrandRate = 0;
            double dDiscountRate = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "SalesRate") != DBNull.Value)
                    dNetRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesRate"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualVat") != DBNull.Value)
                    dVatRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualVat"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualDiscount") != DBNull.Value)
                    dDisRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "SalesDiscount") != DBNull.Value)
                    dDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesDiscount"));
                if (DataBinder.Eval(e.Row.DataItem, "ActualCST") != DBNull.Value)
                    dCSTRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));
                if (DataBinder.Eval(e.Row.DataItem, "SumFreight") != DBNull.Value)
                    dFrRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumFreight"));
                if (DataBinder.Eval(e.Row.DataItem, "Loading") != DBNull.Value)
                    dLURate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Loading"));

                Label lblTotal = (Label)e.Row.FindControl("lblTotal");

                dGrandRate = dDiscountRate + dVatRate + dCSTRate + dFrRate + dLURate;
                lblTotal.Text = dGrandRate.ToString("f2");
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
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet BillDs = bl.FirstLevelSales(startDate, endDate);
            if (BillDs != null)
            {
                if (BillDs.Tables[0].Rows.Count > 0)
                {

                    gvMain.DataSource = BillDs;
                    gvMain.DataBind();

                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
