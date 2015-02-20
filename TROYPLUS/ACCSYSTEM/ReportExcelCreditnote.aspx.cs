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
using AjaxControlToolkit;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Creditnote : System.Web.UI.Page
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

    protected void GridCredit_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridCredit, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void DateEndformat()
    {
        System.DateTime dt = System.DateTime.Now.Date;
        txtEndDt.Text = string.Format("{0:dd/MM/yyyy}", dt);
    }
    
    protected void btnGrid_Click(object sender, EventArgs e)
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
            if (txtStrtDt.Text != "" && txtEndDt.Text != "")
            {
                objBL.StartDate = String.Format("{0:MM/dd/yyyy}", txtStrtDt.Text);
                objBL.EndDate = String.Format("{0:MM/dd/yyyy}", txtEndDt.Text);

                if (opttype.SelectedItem.Text == "Credit Note")
                {
                    bindCrdtdata();
                }
                else if (opttype.SelectedItem.Text == "Debit Note")
                {
                    binddebitdata();
                }
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

    protected void binddebitdata()
    {
        int Refno = 0;
        decimal Gtotal = 0;
        DataSet ds = new DataSet();

        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(txtStrtDt.Text);
        endDate = Convert.ToDateTime(txtEndDt.Text);

        ds = objBL.getDebitsNote(startDate, endDate);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("RefNo"));
                dt.Columns.Add(new DataColumn("NoteDate"));
                dt.Columns.Add(new DataColumn("Debitor"));
                dt.Columns.Add(new DataColumn("Amount"));
                dt.Columns.Add(new DataColumn("Narration"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Refno == 0 || Refno != Convert.ToInt32(dr["RefNo"])))
                    {
                        DataRow dr_final1 = dt.NewRow();
                        dr_final1["RefNo"] = dr["RefNo"];
                        dr_final1["NoteDate"] = dr["NoteDate"];
                        dr_final1["Debitor"] = dr["Debitor"];
                        dr_final1["Amount"] = dr["Amount"];
                        dr_final1["Narration"] = dr["Narration"];
                        dt.Rows.Add(dr_final1);
                        Gtotal = Gtotal + Convert.ToDecimal(dr["Amount"]);
                    }
                }
                DataRow dr_final2 = dt.NewRow();
                dr_final2["RefNo"] = "";
                dr_final2["NoteDate"] = "";
                dr_final2["Debitor"] = "Grand Total:";
                dr_final2["Amount"] = Convert.ToDecimal(Gtotal);
                dr_final2["Narration"] = "";
                dt.Rows.Add(dr_final2);
                ExportToExcel(dt);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                txtStrtDt.Focus();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            txtStrtDt.Focus();
        }
    }

    private void BindGrid()
    {
        Total = 0;
        if (txtStrtDt.Text != "" && txtEndDt.Text != "")
        {
            objBL.StartDate = String.Format("{0:MM/dd/yyyy}", txtStrtDt.Text);
            objBL.EndDate = String.Format("{0:MM/dd/yyyy}", txtEndDt.Text);
            DataSet ds = new DataSet();
            ds = objBL.getCredits();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridCredit.DataSource = ds;
                GridCredit.DataBind();
                var lblTotalAmt = GridCredit.FooterRow.FindControl("lblTotalAmt") as Label;
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

    protected void GridCredit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridCredit.PageIndex = e.NewPageIndex;

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
            GridCredit.PageIndex = ((DropDownList)sender).SelectedIndex;

            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void bindCrdtdata()
    {
        int Refno = 0;
        decimal Gtotal = 0;
        DataSet ds = new DataSet();

        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(txtStrtDt.Text);
        endDate = Convert.ToDateTime(txtEndDt.Text);

        ds = objBL.getCreditsNote(startDate, endDate);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("RefNo"));
                dt.Columns.Add(new DataColumn("NoteDate"));
                dt.Columns.Add(new DataColumn("Creditor"));
                dt.Columns.Add(new DataColumn("Amount"));
                dt.Columns.Add(new DataColumn("Narration"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Refno == 0 || Refno != Convert.ToInt32(dr["RefNo"])))
                    {
                        DataRow dr_final1 = dt.NewRow();
                        dr_final1["RefNo"] = dr["RefNo"];
                        dr_final1["NoteDate"] = dr["NoteDate"];
                        dr_final1["Creditor"] = dr["Creditor"];
                        dr_final1["Amount"] = dr["Amount"];
                        dr_final1["Narration"] = dr["Narration"];
                        dt.Rows.Add(dr_final1);
                        Gtotal = Gtotal + Convert.ToDecimal(dr["Amount"]);
                    }
                }
                DataRow dr_final2 = dt.NewRow();
                dr_final2["RefNo"] = "";
                dr_final2["NoteDate"] = "";
                dr_final2["Creditor"] = "Grand Total:";
                dr_final2["Amount"] = Convert.ToDecimal(Gtotal);
                dr_final2["Narration"] = "";
                dt.Rows.Add(dr_final2);
                ExportToExcel(dt);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                txtStrtDt.Focus();
            }
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
            string filename = "CreditDebitnoteDownloadExcel.xls";
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
    protected void GridCredit_RowDataBound(object sender, GridViewRowEventArgs e)
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
