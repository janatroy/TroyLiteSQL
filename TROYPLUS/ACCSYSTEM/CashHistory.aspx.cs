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

public partial class CashHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = string.Empty;
        try
        {
            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            srcCashHistory.ConnectionString = connStr;
            srcAdjustments.ConnectionString = connStr;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdCash_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdCash_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(grdCash, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdCash_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdCash_PreRender(object sender, EventArgs e)
    {

    }

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {

    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {

    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {

    }

    protected void grdAdjustment_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        { 
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(grdAdjustment, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdAdjustment_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdAdjustment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
