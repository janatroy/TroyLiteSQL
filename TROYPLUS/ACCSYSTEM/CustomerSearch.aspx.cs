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
using System.Data.OleDb;
using System.Text;

public partial class CustomerSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            srcArea.ConnectionString = connStr;

            if (!Page.IsPostBack)
            {
                txtUserId.Focus();
                txtUserId.Attributes.Add("onKeyPress", " return clickButton(event,'" + lnkBtnSearchId.ClientID + "')");
                txtCode.Attributes.Add("onKeyPress", " return clickButton(event,'" + lnkBtnSearchId.ClientID + "')");
                CheckBox1.Checked = true;
                //ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + txtUserId.ClientID + "').focus();</script>");
                ddArea.DataBind();
                if (Page.Request.Cookies["User"] != null)
                {
                    txtUserId.Text = Page.Request.Cookies["User"].Value;
                    ddArea.SelectedValue = Page.Request.Cookies["Area"].Value;
                    txtCode.Text = Page.Request.Cookies["Code"].Value;
                    CheckBox1.Checked = Convert.ToBoolean(Page.Request.Cookies["ActiveCustomer"].Value);
                    lnkBtnSearch_Click(this, null);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Response.Cookies.Add(new HttpCookie("User", txtUserId.Text));
            Page.Response.Cookies.Add(new HttpCookie("Area", ddArea.Text));
            Page.Response.Cookies.Add(new HttpCookie("Code", txtCode.Text));
            Page.Response.Cookies.Add(new HttpCookie("ActiveCustomer", CheckBox1.Checked.ToString()));
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindGrid()
    {
        FillUserGrid(GetCustomerForName(txtUserId.Text, txtCode.Text, ddArea.Text, CheckBox1.Checked));

    }

    private void FillUserGrid(DataView dvuserData)
    {

        GrdViewCust.DataSource = dvuserData;
        GrdViewCust.DataBind();

        if (dvuserData.Count == 0)
        {
            errordisplay1.AddItem("No customer found for the Search Criteria.", DisplayIcons.Information, false);
        }

    }

    private DataView GetCustomerForName(string Name, string Code, string Area, bool ActiveCustomer)
    {
        Name = "%" + Name + "%";
        //UserService userService = new UserService();
        CustomerData custData = new CustomerData();
        //custData = userService.GetListForLogonId(LogonId);
        custData = ReadRecords(Name, Code, Area, ActiveCustomer);

        DataView dvCustData = custData._CustomerData.DefaultView;
        return dvCustData;

    }

    protected void GrdViewCust_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerStates(GrdViewCust, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewCust_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int intCurIndex = GrdViewCust.PageIndex;

            switch (e.CommandArgument.ToString())
            {
                case "First":
                    GrdViewCust.PageIndex = 0;
                    break;
                case "Prev":
                    GrdViewCust.PageIndex = intCurIndex - 1;
                    break;
                case "Next":
                    GrdViewCust.PageIndex = intCurIndex + 1;
                    break;
                case "Last":
                    GrdViewCust.PageIndex = GrdViewCust.PageCount;
                    break;
            }

            // popultate the gridview control
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private CustomerData ReadRecords(string Name, string Code, string Area, bool ActiveCustomer)
    {
        OleDbConnection conn = null;
        OleDbDataReader reader = null;
        OleDbDataAdapter da = null;
        CustomerData data = new CustomerData();

        try
        {
            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/frm_Login.aspx");

            conn = new OleDbConnection(connStr);
            conn.Open();

            StringBuilder query = new StringBuilder();
            query.Append("Select area, code, name, category, doorno, address1, address2, place, phoneno, effectdate, installation, monthlycharge, entereddate, balance, prevailed,area + name + Cstr(code) as areanamecode FROM CustomerMaster ");

            if (Name != "")
            {
                query.AppendFormat(" Where (name like '{0}')", Name);
            }

            if (Code != "")
            {
                //query = "Select area, code, name, category, doorno, address1, address2, place, phoneno, effectdate, installation, monthlycharge, entereddate, balance, prevailed,area + name + Cstr(code) as areanamecode FROM CustomerMaster Where ("+ ddCriteria.SelectedValue +" like '" + Name + "')";
                query.AppendFormat(" And code={0} ", Code);
            }

            if (Area != "0")
            {
                query.AppendFormat(" And Area='{0}' ", Area.Replace("'", "''"));
            }

            if (ActiveCustomer == true)
            {
                query.Append(" And category <> 'DC' ");
            }
            else
            {
                query.Append(" And category = 'DC' ");
            }


            query.Append(" Order By name, code ");

            OleDbCommand cmd = new OleDbCommand(query.ToString(), conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data._CustomerData);
            return data;
            //reader = cmd.ExecuteReader();

            //datagrid.DataSource = reader;
            //datagrid.DataBind();
        }
        finally
        {
            //if (reader != null) 
            //    reader.Close();

            if (conn != null)
                conn.Close();
        }
    }

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdViewCust.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
