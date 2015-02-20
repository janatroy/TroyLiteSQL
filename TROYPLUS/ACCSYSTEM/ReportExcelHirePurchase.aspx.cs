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
public partial class ReportExcelHirePurchase : System.Web.UI.Page
{
    //DBClass objdb = new DBClass();
    BusinessLogic objBL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            DateTime date = Convert.ToDateTime("01-01-1900");
            DataSet ds = new DataSet();

            string connection = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;
            else
                Response.Redirect("Login.aspx");

            ds = objBL.GetHireList(connection, "", "");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Bill No"));
                    dt.Columns.Add(new DataColumn("Bill Date"));
                    dt.Columns.Add(new DataColumn("Customer Name"));
                    dt.Columns.Add(new DataColumn("Purchase Amount"));
                    dt.Columns.Add(new DataColumn("Start Due date"));
                    dt.Columns.Add(new DataColumn("Initial Payment"));
                    dt.Columns.Add(new DataColumn("Loan Amount"));
                    dt.Columns.Add(new DataColumn("Interest Amount"));
                    dt.Columns.Add(new DataColumn("No of Inst"));
                    dt.Columns.Add(new DataColumn("Document Charges"));
                    dt.Columns.Add(new DataColumn("Final Payment"));
                    dt.Columns.Add(new DataColumn("Each Month Payment"));
                    dt.Columns.Add(new DataColumn("Date of Payment"));
                    dt.Columns.Add(new DataColumn("Upfront Interest"));
                    dt.Columns.Add(new DataColumn("Advance Emi"));
                    dt.Columns.Add(new DataColumn("Others"));

                    DataRow dr_final123 = dt.NewRow();
                    dt.Rows.Add(dr_final123);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_final1 = dt.NewRow();
                        dr_final1["Bill No"] = dr["slno"];
                        dr_final1["Customer Name"] = dr["CustomerName"];
                        dr_final1["Bill Date"] = dr["BillDate"];
                        dr_final1["Purchase Amount"] = dr["puramt"];
                        dr_final1["Document Charges"] = dr["dochr"];
                        dr_final1["No of Inst"] = dr["noinst"];
                        dr_final1["Loan Amount"] = dr["lnamt"];
                        dr_final1["Interest Amount"] = dr["intamt"];
                        dr_final1["Initial Payment"] = dr["inipay"];
                        dr_final1["Each Month Payment"] = dr["eachpay"];
                        dr_final1["Date of Payment"] = dr["paydate"];
                        dr_final1["Others"] = dr["Others"];
                        dr_final1["Start Due date"] = dr["startdate"];
                        dr_final1["Final Payment"] = dr["finpay"];
                        dr_final1["Upfront Interest"] = dr["Upfront"];
                        dr_final1["Advance Emi"] = dr["emi"];
                        dt.Rows.Add(dr_final1);
                    }

                    ExportToExcel(dt);
                }
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

    protected void GridCust_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridCust, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindGrid()
    {
        CreditLimitTotal = 0;
        OpenBalanceDRTotal = 0;
        DataSet ds = new DataSet();

        string connection = string.Empty;

        if (Request.Cookies["Company"] != null)
            connection = Request.Cookies["Company"].Value;
        else
            Response.Redirect("Login.aspx");

        ds = objBL.ListOpeningProductStock(connection, "", "");

        if (ds.Tables[0].Rows.Count > 0)
        {
            GridCust.DataSource = ds;
            GridCust.DataBind();
        }
    }

    protected void btnData_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridCust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridCust.PageIndex = e.NewPageIndex;

            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            GridCust.PageIndex = ((DropDownList)sender).SelectedIndex;


            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }


    }

    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime date = Convert.ToDateTime("01-01-1900");
            DataSet ds = new DataSet();

            string connection = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;
            else
                Response.Redirect("Login.aspx");

            ds = objBL.GetHireList(connection, "", "");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Bill No"));
                    dt.Columns.Add(new DataColumn("Bill Date"));
                    dt.Columns.Add(new DataColumn("Customer Name"));
                    dt.Columns.Add(new DataColumn("Purchase Amount"));
                    dt.Columns.Add(new DataColumn("Start Due date"));
                    dt.Columns.Add(new DataColumn("Initial Payment"));
                    dt.Columns.Add(new DataColumn("Loan Amount"));
                    dt.Columns.Add(new DataColumn("Interest Amount"));
                    dt.Columns.Add(new DataColumn("No of Inst"));
                    dt.Columns.Add(new DataColumn("Document Charges"));
                    dt.Columns.Add(new DataColumn("Final Payment"));
                    dt.Columns.Add(new DataColumn("Each Month Payment"));
                    dt.Columns.Add(new DataColumn("Date of Payment"));
                    dt.Columns.Add(new DataColumn("Others"));

                    DataRow dr_final123 = dt.NewRow();
                    dt.Rows.Add(dr_final123);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_final1 = dt.NewRow();
                        dr_final1["Bill No"] = dr["slno"];
                        dr_final1["Customer Name"] = dr["CustomerName"];
                        dr_final1["Bill Date"] = dr["BillDate"];
                        dr_final1["Purchase Amount"] = dr["puramt"];
                        dr_final1["Document Charges"] = dr["dochr"];
                        dr_final1["No of Inst"] = dr["noinst"];
                        dr_final1["Loan Amount"] = dr["lnamt"];
                        dr_final1["Interest Amount"] = dr["intamt"];
                        dr_final1["Initial Payment"] = dr["inipay"];
                        dr_final1["Each Month Payment"] = dr["eachpay"];
                        dr_final1["Date of Payment"] = dr["paydate"];
                        dr_final1["Others"] = dr["Others"];
                        dr_final1["Start Due date"] = dr["startdate"];
                        dr_final1["Final Payment"] = dr["finpay"];
                        dt.Rows.Add(dr_final1);
                    }

                    ExportToExcel(dt);
                }
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

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            //string filename = "Hire Purchase.xls";
            string filename = "Hire Purchase_" + DateTime.Now.ToString() + ".xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
            dgGrid.HeaderStyle.Font.Bold = true;
            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }

    }
    decimal CreditLimitTotal;
    decimal OpenBalanceDRTotal;

    protected void GridCust_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
}
