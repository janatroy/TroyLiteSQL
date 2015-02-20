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
public partial class Customers : System.Web.UI.Page
{
    //DBClass objdb = new DBClass();
    BusinessLogic objBL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
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

    private void BindGrid()
    {
        CreditLimitTotal = 0;
        OpenBalanceDRTotal = 0;
        DataSet ds = new DataSet();
        string txtsearch = txtSearch.Text;
        string dropdown = ddCriteria.SelectedValue;

        ds = objBL.getCustomers(txtsearch, dropdown);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridCust.DataSource = ds;
            GridCust.DataBind();

            var lblCredits = GridCust.FooterRow.FindControl("lblCredit") as Label;
            if (lblCredits != null)
            {
                lblCredits.Text = CreditLimitTotal.ToString();
            }

            var lblBalance = GridCust.FooterRow.FindControl("lblOpenBal") as Label;
            if (lblBalance != null)
            {
                lblBalance.Text = OpenBalanceDRTotal.ToString();
            }
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

    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            //decimal GtotalOpenDR = 0;
            //decimal GtotalOpenCR = 0;
            //decimal GtotalExecute = 0;
            decimal GtotalCreditlimit = 0;
            ////int GtotalCreditdays = 0;
            DateTime date = Convert.ToDateTime("01-01-1900");
            DataSet ds = new DataSet();

            string txtsearch = txtSearch.Text;
            string dropdown = ddCriteria.SelectedValue;

            ds = objBL.getCustomers(txtsearch, dropdown);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("LedgerName"));
                dt.Columns.Add(new DataColumn("AliasName"));
                dt.Columns.Add(new DataColumn("Address"));
                dt.Columns.Add(new DataColumn("TINnumber"));
                dt.Columns.Add(new DataColumn("CreditLimit"));
                //dt.Columns.Add(new DataColumn("OpenBalanceDR"));
                dt.Columns.Add(new DataColumn("OpenBal"));
                dt.Columns.Add(new DataColumn("Phone"));
                dt.Columns.Add(new DataColumn("LedgerCategory"));
                dt.Columns.Add(new DataColumn("ExecutiveIncharge"));
                dt.Columns.Add(new DataColumn("Mobile"));
                dt.Columns.Add(new DataColumn("CreditDays"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_final1 = dt.NewRow();
                    dr_final1["LedgerName"] = dr["LedgerName"];
                    dr_final1["AliasName"] = dr["AliasName"];
                    dr_final1["Address"] = dr["Address"];
                    dr_final1["TINnumber"] = dr["TINnumber"];
                    dr_final1["CreditLimit"] = dr["CreditLimit"];
                    //dr_final1["OpenBalanceDR"] = dr["OpenBalanceDR"];
                    dr_final1["OpenBal"] = dr["OpenBal"];
                    dr_final1["Phone"] = dr["Phone"];
                    dr_final1["LedgerCategory"] = dr["LedgerCategory"];
                    dr_final1["ExecutiveIncharge"] = dr["ExecutiveIncharge"];
                    dr_final1["CreditDays"] = dr["CreditDays"];
                    dt.Rows.Add(dr_final1);

                    GtotalCreditlimit = GtotalCreditlimit + Convert.ToDecimal(dr["CreditLimit"]);
                    //GtotalOpenDR = GtotalOpenDR + Convert.ToDecimal(dr["OpenBalanceDR"]);
                    //GtotalOpenCR = GtotalOpenCR + Convert.ToDecimal(dr["OpenBal"]);
                    //GtotalExecute = GtotalExecute + Convert.ToDecimal(dr["ExecutiveIncharge"]);
                    //if(!DBNull.Value.Equals(dr["CreditDays"]))
                    //{
                    //    GtotalCreditdays = GtotalCreditdays + Convert.ToInt32(dr["CreditDays"]);
                    //}
                }
                DataRow dr_final2 = dt.NewRow();
                dr_final2["LedgerName"] = "";
                dr_final2["AliasName"] = "";
                dr_final2["Address"] = "";
                dr_final2["TINnumber"] = "Grand Total:";
                dr_final2["CreditLimit"] = Convert.ToDecimal(GtotalCreditlimit);
                //dr_final2["OpenBalanceDR"] = Convert.ToDecimal(GtotalOpenDR);
                //dr_final2["OpenBal"] = Convert.ToDecimal(GtotalOpenCR);
                dr_final2["OpenBal"] = "";
                dr_final2["Phone"] = "";
                dr_final2["LedgerCategory"] = "";
                dr_final2["ExecutiveIncharge"] = "";
                dr_final2["CreditDays"] = "";
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
            string filename = "CustomersDownloadExcel.xls";
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
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string DRORCR = ((HiddenField)e.Row.FindControl("DRORCR")).Value;
                //string led = ((Label)e.Row.FindControl("lblLedger")).Text;

                var lblCredit = e.Row.FindControl("lblCreditLimit") as Label;
                if (lblCredit != null)
                {
                    CreditLimitTotal += decimal.Parse(lblCredit.Text);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblBalanceDR = e.Row.FindControl("lblOpenBalance") as Label;
                if (lblBalanceDR != null)
                {
                    OpenBalanceDRTotal += decimal.Parse(lblBalanceDR.Text);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
