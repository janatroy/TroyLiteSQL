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
public partial class ReportExcelTaskTypes : System.Web.UI.Page
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

            ds = objBL.ListTaskTypesInfo(connection, "", "");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Task Type Name"));                
                
                    DataRow dr_final123 = dt.NewRow();
                    dt.Rows.Add(dr_final123);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_final1 = dt.NewRow();
                        dr_final1["Task Type Name"] = dr["Task_Type_Name"];
                        dt.Rows.Add(dr_final1);
                    }
                    DataRow dr_final2 = dt.NewRow();                
                    dt.Rows.Add(dr_final2);
                    ExportToExcel(dt);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
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

        ds = objBL.ListProducts(connection, "", "");

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

            ds = objBL.ListProducts(connection, "", "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ProductName"));
                dt.Columns.Add(new DataColumn("ItemCode"));
                dt.Columns.Add(new DataColumn("Model"));
                dt.Columns.Add(new DataColumn("Brand"));
                dt.Columns.Add(new DataColumn("MRP"));
                dt.Columns.Add(new DataColumn("MRP Date"));
                dt.Columns.Add(new DataColumn("DP"));
                dt.Columns.Add(new DataColumn("DP Date"));
                dt.Columns.Add(new DataColumn("NLC"));
                dt.Columns.Add(new DataColumn("NLC Date"));
                dt.Columns.Add(new DataColumn("Vat"));
                dt.Columns.Add(new DataColumn("StockLevel"));

                DataRow dr_final123 = dt.NewRow();
                dt.Rows.Add(dr_final123);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_final1 = dt.NewRow();
                    dr_final1["ProductName"] = dr["ProductName"];
                    dr_final1["ItemCode"] = dr["ItemCode"];
                    dr_final1["Model"] = dr["Model"];
                    dr_final1["Brand"] = dr["productdesc"];
                    dr_final1["MRP"] = dr["rate"];
                    dr_final1["MRP Date"] = dr["MRPEffDate"];
                    dr_final1["DP"] = dr["Dealerrate"];
                    dr_final1["DP Date"] = dr["DPEffDate"];
                    dr_final1["NLC"] = dr["NLC"];
                    dr_final1["NLC Date"] = dr["NLCEffDate"];
                    dr_final1["Vat"] = dr["Vat"];
                    dr_final1["NLC"] = dr["rol"];
                    dr_final1["StockLevel"] = dr["rol"];
                    dt.Rows.Add(dr_final1);
                }
                DataRow dr_final2 = dt.NewRow();
                dr_final2["ProductName"] = "";
                dr_final2["ItemCode"] = "";
                dr_final2["Model"] = "";
                dr_final2["Brand"] = "";
                dr_final2["MRP"] = "";
                dr_final2["MRP Date"] = "";
                dr_final2["DP"] = "";
                dr_final2["DP Date"] = "";
                dr_final2["NLC"] = "";
                dr_final2["NLC Date"] = "";
                dr_final2["Vat"] = "";
                dr_final2["NLC"] = "";
                dr_final2["StockLevel"] = "";
                dt.Rows.Add(dr_final2);
                ExportToExcel(dt);
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
            string filename = "Task Type details.xls";
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
