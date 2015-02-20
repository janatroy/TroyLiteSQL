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

public partial class BillCustomerView : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    double dNetRate = 0;
    double dVatRate = 0;
    double dDisRate = 0;
    double dCSTRate = 0;
    double dFrRate = 0;
    double dLURate = 0;
    double dSumGrand = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                int customerID = 0;
                string customer = string.Empty;
                if (Request.QueryString["ID"] != null)
                {
                    customerID = int.Parse(Request.QueryString["ID"].ToString());
                    customer = Convert.ToString(Request.QueryString["cname"]);
                    lblCustomer.Text = "Bill View For Customer " + customer;
                    DateTime startDate, endDate;
                    startDate = Convert.ToDateTime(Session["BillStartDate"]);
                    endDate = Convert.ToDateTime(Session["BillEndDate"]);

                    DataSet BillDs = bl.ThirdLevelSales(customerID, customer, startDate, endDate);
                    gvThird.DataSource = BillDs;
                    gvThird.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void gvThird_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dGrandRate = 0;
            double dDiscountRate = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "SalesDiscount") != DBNull.Value)
                {
                    dDiscountRate = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesDiscount"));
                    dGrandRate = dDiscountRate;
                }
                if (DataBinder.Eval(e.Row.DataItem, "SalesRate") != DBNull.Value)
                    dNetRate = dNetRate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalesRate"));


                if (DataBinder.Eval(e.Row.DataItem, "ActualVat") != DBNull.Value)
                {
                    dVatRate = dVatRate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualVat"));
                    dGrandRate = dGrandRate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualVat"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "ActualDiscount") != DBNull.Value)
                {
                    dDisRate = dDisRate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualDiscount"));
                    // dGrandRate = dGrandRate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualDiscount"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "ActualCST") != DBNull.Value)
                {
                    dCSTRate = dCSTRate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));
                    dGrandRate = dGrandRate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ActualCST"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "SumFreight") != DBNull.Value)
                {
                    dFrRate = dFrRate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumFreight"));
                    dGrandRate = dGrandRate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SumFreight"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "Loading") != DBNull.Value)
                {
                    dLURate = dLURate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Loading"));
                    dGrandRate = dGrandRate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Loading"));
                }
                dSumGrand = dSumGrand + dGrandRate;

                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = dGrandRate.ToString("f2");

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {


                e.Row.Cells[2].Font.Bold = true;
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[7].Font.Bold = true;
                e.Row.Cells[8].Font.Bold = true;

                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = dNetRate.ToString("f2");

                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = dDisRate.ToString("f2");

                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = dVatRate.ToString("f2");

                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dCSTRate.ToString("f2");

                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = dFrRate.ToString("f2");

                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = dLURate.ToString("f2");
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].Text = dSumGrand.ToString("f2");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
