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

public partial class Journel : System.Web.UI.Page
{
    //DBClass objdb = new DBClass();
    BusinessLogic objBL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            txtStrtDt.Focus();
            if (!Page.IsPostBack)
            {
                DateEndformat();
                DateStartformat();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void DateStartformat()
    {
        DateTime dtCurrent = DateTime.Now;
        DateTime dtNew = new DateTime(dtCurrent.Year, dtCurrent.Month, 1);
        txtStrtDt.Text = string.Format("{0:dd/MM/yyyy}", dtNew);
    }

    protected void DateEndformat()
    {
        System.DateTime dt = System.DateTime.Now.Date;
        txtEndDt.Text = string.Format("{0:dd/MM/yyyy}", dt);
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

    protected void GridJournl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridJournl.PageIndex = e.NewPageIndex;

            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindGrid()
    {
        Total = 0;
        if (txtStrtDt.Text != "" && txtEndDt.Text != "")
        {
            //objBL.StartDate = String.Format("{0:MM/dd/yyyy}", txtStrtDt.Text);
            //objBL.EndDate = String.Format("{0:MM/dd/yyyy}", txtEndDt.Text);

            string aa = txtStrtDt.Text;
            objBL.StartDate = Convert.ToDateTime(aa).ToString("MM/dd/yyyy");

            string aaa = txtEndDt.Text;
            objBL.EndDate = Convert.ToDateTime(aaa).ToString("MM/dd/yyyy");


            DataSet ds = new DataSet();
            ds = objBL.getJournals();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridJournl.DataSource = ds;
                GridJournl.DataBind();
                var lblTotalAmt = GridJournl.FooterRow.FindControl("lblTotalAmt") as Label;
                if (lblTotalAmt != null)
                {
                    lblTotalAmt.Text = Total.ToString();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                txtStrtDt.Focus();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(TextBox), "MyScript", "alert('Please Enter Dates');", true);
            txtStrtDt.Focus();
        }
    }

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridJournl.PageIndex = ((DropDownList)sender).SelectedIndex;

            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridJournl_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridJournl, e.Row, this);
            }
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
            if (txtStrtDt.Text != "" && txtEndDt.Text != "")
            {
                objBL.StartDate = String.Format("{0:MM/dd/yyyy}", txtStrtDt.Text);
                objBL.EndDate = String.Format("{0:MM/dd/yyyy}", txtEndDt.Text);
                bindData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(TextBox), "MyScript", "alert('Please Enter Dates');", true);
                txtStrtDt.Focus();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void bindData()
    {
        decimal Gtotal = 0;
        DataSet ds = new DataSet();
        ds = objBL.getJournals();
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RefNo"));
            dt.Columns.Add(new DataColumn("TransNo"));
            dt.Columns.Add(new DataColumn("TransDate"));
            dt.Columns.Add(new DataColumn("Creditor"));
            dt.Columns.Add(new DataColumn("Debtor"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("Narration"));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataRow dr_final1 = dt.NewRow();
                dr_final1["TransNo"] = dr["TransNo"];
                dr_final1["TransDate"] = dr["TransDate"];
                dr_final1["RefNo"] = dr["RefNo"];
                dr_final1["Debtor"] = dr["Debtor"];
                dr_final1["Creditor"] = dr["Creditor"];
                dr_final1["Narration"] = dr["Narration"];
                dr_final1["Amount"] = dr["Amount"];
                dt.Rows.Add(dr_final1);
                Gtotal = Gtotal + Convert.ToDecimal(dr["Amount"]);
            }
            DataRow dr_final2 = dt.NewRow();
            dr_final2["TransDate"] = "";
            dr_final2["RefNo"] = "";
            dr_final2["Debtor"] = "Grand Total :";
            dr_final2["Creditor"] = "";
            dr_final2["Narration"] = "";
            dr_final2["Amount"] = Convert.ToDecimal(Gtotal);
            dt.Rows.Add(dr_final2);
            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            txtStrtDt.Focus();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            string filename = "JournelDownloadExcel.xls";
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

    decimal Total;
    protected void GridJournl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var NewAmt = e.Row.FindControl("lblAmt") as Label;
                if (NewAmt != null)
                {
                    Total += decimal.Parse(NewAmt.Text);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}