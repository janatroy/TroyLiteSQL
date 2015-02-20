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

public partial class PrintAdjustmentReport : System.Web.UI.Page
{
    public Double amt = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            printPreview();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void printPreview()
    {
        string sFilename = string.Empty;
        DataSet ds = new DataSet();
        if (Session["dataSet"] != null)
        {
            sFilename = Session["dataSet"].ToString();
            ds.ReadXml(Server.MapPath(sFilename), XmlReadMode.InferSchema);
            gvAdjDetails.DataSource = ds;
            gvAdjDetails.DataBind();
        }
        if (Session["startDate"] != null)
        {
            lblStartDate.Text = Session["startDate"].ToString();
        }
        if (Session["endDate"] != null)
        {
            lblEndDate.Text = Session["endDate"].ToString();
        }
        lblDate.Text = DateTime.Now.ToShortDateString();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dataSet"] != null)
            {

                Response.Redirect("AdjustmentDetailsReport.aspx");

            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void gvAdjDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //amt = amt + 50;// Convert.ToDouble(e.Row.Cells[6].Text);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                amt = amt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            lblAmount.Text = amt.ToString();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
