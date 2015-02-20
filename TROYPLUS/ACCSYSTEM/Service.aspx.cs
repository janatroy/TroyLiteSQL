using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Service : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    if (Request.QueryString["ID"].ToString() != "Select Cust/Dealer")
                    {
                        ddCriteria.SelectedValue = "Ledger";
                        txtSearch.Text = Request.QueryString["ID"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewSerEntry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow Row = GrdViewSerEntry.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic(connection);

            DataSet ds = bl.GetServiceDetailsForID(Row.Cells[0].Text);

            hdRefNo.Value = ds.Tables[0].Rows[0]["RefNumber"].ToString();
            hdDueDate.Value = ds.Tables[0].Rows[0]["DueDate"].ToString();
            hdCustomerID.Value = ds.Tables[0].Rows[0]["CustomerID"].ToString();
            string amount = ds.Tables[0].Rows[0]["Amount"].ToString();
            string serviceID = Convert.ToString(GrdViewSerEntry.SelectedDataKey.Value);

            GridViewRow row = GrdViewSerEntry.SelectedRow;

            DateTime endDate = DateTime.Parse(row.Cells[5].Text);
            DateTime dueDate = DateTime.Parse(hdDueDate.Value);

            if (DateTime.Compare(dueDate, endDate) > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Service DueDate is greater than Service EndDate.If you still need please change the Service EndDate using Service Entry Screen.');", true);
                return;
            }

            Page.RegisterStartupScript("ClientPostBackBlock", "<script type ='text/javascript'>window.opener.document.getElementById('ctl00_cplhControlPanel_hdServiceID').value = " + serviceID + ";window.opener.document.getElementById('ctl00_cplhControlPanel_hdCustomerID').value = " + hdCustomerID.Value + ";window.opener.document.getElementById('ctl00_cplhControlPanel_hdRefNumber').value = " + hdRefNo.Value + ";window.opener.document.getElementById('ctl00_cplhControlPanel_hdDueDate').value = '" + hdDueDate.Value.ToString() + "';var dd = window.opener.document.getElementById('ctl00_cplhControlPanel_drpCustomer'); for (var i=0; i< dd.options.length; i++){  if ( dd.options[i].value == " + hdCustomerID.Value.ToString() + " ){dd.options[i].selected = true; dd.disabled = true;}} ;window.opener.document.getElementById('ctl00_cplhControlPanel_txtRefNo').value =" + hdRefNo.Value.ToString() + "; window.opener.document.getElementById('ctl00_cplhControlPanel_txtDueDate').value= '" + hdDueDate.Value.ToString() + "'; window.opener.document.getElementById('ctl00_cplhControlPanel_txtAmount').value = " + amount + ";window.opener.document.getElementById('ctl00_cplhControlPanel_txtDueDate').disabled = true; window.close();</script>");
            //RegisterStartupScript("ClientPostBackBlock", "window.close();");


        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }

    protected void GrdViewSerEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewSerEntry.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewSerEntry_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewSerEntry, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewSerEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
