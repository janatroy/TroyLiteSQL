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

public partial class CustomerDetails : System.Web.UI.Page
{
    private enum mode
    {
        mode_Edit,
        mode_Add
    };
    mode mMode;


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

            mMode = ParseQueryString();

            if (!Page.IsPostBack)
            {
                lnkBtncancel.PostBackUrl = Request.UrlReferrer.ToString();

                switch (mMode)
                {
                    case mode.mode_Add:
                        txtCustName.Focus();
                        break;
                    case mode.mode_Edit:
                        LoadCustomerData();
                        ddArea.Enabled = false;
                        break;
                }

                System.Text.StringBuilder cashScript = new System.Text.StringBuilder();
                cashScript.Append("window.open('CashHistory.aspx?Code=");
                cashScript.Append(txtCustCode.Text);
                cashScript.Append("&Area=");
                cashScript.Append(Server.UrlEncode(ddArea.SelectedValue.Replace("'", "''")));
                cashScript.Append(" ','Cash', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=600,width=900,left=10,top=10, scrollbars=yes');");
                btnCashHistory.Attributes.Add("OnClick", cashScript.ToString());

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private CustomerData ReadRecords(string Name)
    {
        OleDbConnection conn = null;
        OleDbDataAdapter da = null;
        CustomerData data = new CustomerData();

        try
        {
            string connStr = string.Empty;

            if (Request.Cookies["Company"]  != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = "Select area, code, name, category, doorno, address1, address2, place, phoneno, effectdate, installation, monthlycharge, entereddate, balance, prevailed FROM CustomerMaster Where (area + name + Cstr(code) = '" + Name + "')";

            OleDbCommand cmd = new OleDbCommand(query, conn);
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

    private void LoadCustomerData()
    {
        string Customer = "";
        CustomerData data = new CustomerData();

        if (Request.QueryString["ID"] != null || Request.QueryString["ID"] != "")
            Customer = Request.QueryString["ID"];

        if (Customer != "")
        {
            data = ReadRecords(Customer);

            if (data._CustomerData.Count > 0)
            {
                CustomerData.CustomerDataRow dataRow = data._CustomerData[0];

                if (dataRow.name != null)
                    txtCustName.Text = dataRow.name;

                if (dataRow.phoneno != null)
                    txtPhoneNo.Text = dataRow.phoneno;

                if (dataRow.doorno != null)
                    txtDoorNo.Text = dataRow.doorno;
                if (dataRow.address1 != null)
                    txtAdd1.Text = dataRow.address1;

                if (dataRow.address2 != null)
                    txtAdd2.Text = dataRow.address2;

                if (dataRow.place != null)
                    txtPlace.Text = dataRow.place;

                if (dataRow.balance != null)
                    txtBalance.Text = dataRow.balance.ToString();

                if (dataRow.installation != 0)
                    txtInstCrge.Text = dataRow.installation.ToString();

                if (dataRow.effectdate != null)
                {
                    txtEffDate.Text = dataRow.effectdate.ToString("dd/MM/yyyy");
                }
                if (dataRow.monthlycharge != 0)
                    txtMnthCrge.Text = dataRow.monthlycharge.ToString();

                chkPrev.Checked = dataRow.prevailed;

                if (dataRow.code != 0)
                    txtCustCode.Text = dataRow.code.ToString();

                if (!dataRow.IsareaNull())
                {
                    ddArea.DataBind();
                    string area = dataRow.area.Trim();
                    if (ddArea.Items.FindByValue(area) != null)
                        ddArea.Items.FindByValue(area).Selected = true;
                }
                if (!dataRow.IscategoryNull())
                {

                    string category = dataRow.category.Trim();
                    if (ddCategory.Items.FindByValue(category) != null)
                        ddCategory.Items.FindByValue(category).Selected = true;

                    Session["Category"] = dataRow.category.Trim();
                }
            }

        }

    }

    private mode ParseQueryString()
    {
        string modestring = null;

        try
        {
            modestring = Request["ID"];

            if (modestring == "")
                modestring = "AddNew";
            else
                modestring = "Edit";
        }
        finally
        {
            if (modestring == null || modestring == "")
            {
                //throw new ParameterValidationError("Id missing");
            }
        }

        switch (modestring)
        {
            case "AddNew":
                return mode.mode_Add;
            default:
                return mode.mode_Edit;
        }

    }

    protected void lnkBtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string custName = String.Empty;
            string area = String.Empty;
            string category = String.Empty;
            string doorNo = String.Empty;
            string address1 = String.Empty;
            string address2 = String.Empty;
            string place = String.Empty;
            string phoneNo = String.Empty;
            string effDate = String.Empty;
            string Installation = String.Empty;
            string monthlyCharge = String.Empty;
            string prevelage = String.Empty;
            string enteredDate = string.Empty;
            int custCode = 0;

            _UserControl_errordisplay errDisp = (_UserControl_errordisplay)this.FindControl("errorDisplay");

            if (Session["Category"] != null)
            {
                if (Session["Category"].ToString() != ddCategory.SelectedValue)
                {
                    txtEffDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    StringBuilder script = new StringBuilder();
                    script.Append("<script language='Javascript'>alert('You have changed the Category, effective date has been set to todays Date.')</script>");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You have changed the Category, effective date has been set to todays Date.')", true);
                }

            }

            if (!Page.IsValid)
            {
                foreach (IValidator validator in Page.Validators)
                {
                    if (!validator.IsValid)
                    {
                        errDisp.AddItem(validator.ErrorMessage, DisplayIcons.Error, true);
                    }
                }
            }
            else
            {


                if (txtCustName.Text != "")
                    custName = txtCustName.Text;

                if (ddArea.SelectedValue != "0")
                    area = ddArea.SelectedValue;

                if (ddCategory.Text != "0")
                    category = ddCategory.SelectedValue;

                if (txtDoorNo.Text != "")
                    doorNo = txtDoorNo.Text;

                if (txtAdd1.Text != "")
                    address1 = txtAdd1.Text;

                if (txtAdd2.Text != "")
                    address2 = txtAdd2.Text;

                if (txtPlace.Text != "")
                    place = txtPlace.Text;

                if (txtPhoneNo.Text != "")
                    phoneNo = txtPhoneNo.Text;

                if (txtEffDate.Text != "")
                    effDate = txtEffDate.Text;

                if (txtInstCrge.Text != "")
                    Installation = txtInstCrge.Text;

                if (txtMnthCrge.Text != "")
                    monthlyCharge = txtMnthCrge.Text;

                if (txtCustName.Text != "")
                    custName = txtCustName.Text;

                prevelage = chkPrev.Checked.ToString();
                enteredDate = DateTime.Now.ToString("dd/MM/yyyy");

                string sql = string.Empty;

                if (mMode == mode.mode_Edit)
                {

                    sql = "UPDATE CustomerMaster SET area='" + area.Replace("'", "''") + "',name='" + custName + "', category='" +
                    category + "',doorno='" + doorNo + "',address1='" + address1 + "', address2='" + address2 +
                    "',place='" + place + "', phoneno='" + phoneNo + "',effectdate='" + effDate + "',installation=" +
                    Installation + ",monthlycharge=" + monthlyCharge + ",prevailed=" + prevelage +
                    " WHERE area = '" + area.Replace("'", "''") + "'" + " and code = " + custCode.ToString();

                    ExecuteNonQuery(sql);
                    Session["Category"] = ddCategory.SelectedValue;
                    errorDisplay.AddItem("Customer Details Saved successfully", DisplayIcons.GreenTick, false);

                }
                else if (mMode == mode.mode_Add)
                {
                    int maxCode = GetMaxCustoCode(area);
                    custCode = maxCode + 1;
                    sql = string.Format("INSERT INTO CustomerMaster VALUES('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}',{13},{14})", area.Replace("'", "''"), custCode, custName, category, doorNo, address1, address2, place, phoneNo, effDate, Installation, monthlyCharge, enteredDate, 0, prevelage);

                    ExecuteNonQuery(sql);
                    errorDisplay.AddItem("Customer Details Saved successfully. Customer Code is : " + custCode.ToString(), DisplayIcons.GreenTick, false);
                    Session["Category"] = null;
                    ResetFormControlValues(this);

                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void ResetFormControlValues(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.Controls.Count > 0)
            {
                ResetFormControlValues(c);
            }
            else
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((TextBox)c).Text = "";
                        break;
                    case "System.Web.UI.WebControls.RadioButtonList":
                        {
                            //((RadioButtonList)c).SelectedValue = "Y";
                            for (int j = 0; j < ((RadioButtonList)c).Items.Count; j++)
                            {
                                ((RadioButtonList)c).Items[j].Selected = false;

                            }
                            break;
                        }
                    case "System.Web.UI.WebControls.DropDownList":
                        ((DropDownList)c).SelectedValue = "0";
                        break;

                }
            }
        }
    }
    private int GetMaxCustoCode(string area)
    {
        OleDbConnection conn = null;
        OleDbDataReader reader = null;
        OleDbDataAdapter da = null;
        DataSet data = new DataSet();

        try
        {
            string connStr = string.Empty;
            if (Request.Cookies["Company"]  != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            string query = string.Format("Select MAX(Code) FROM CustomerMaster Where area= '{0}' Group by area", area.Replace("'", "''"));

            OleDbCommand cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            da.Fill(data);

            if (data.Tables[0].Rows.Count > 0)
                return int.Parse(data.Tables[0].Rows[0][0].ToString());
            else
                return 0;
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

    private void ExecuteNonQuery(string sql)
    {
        OleDbConnection conn = null;
        try
        {
            string connStr = string.Empty;
            if (Request.Cookies["Company"]  != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            conn = new OleDbConnection(connStr);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
        }
        finally
        {
            if (conn != null) conn.Close();
        }
    }

    protected void btnCashHistory_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddCash_Click(object sender, EventArgs e)
    {

    }
    protected void lnkBtncancel_Click(object sender, EventArgs e)
    {

    }

}
