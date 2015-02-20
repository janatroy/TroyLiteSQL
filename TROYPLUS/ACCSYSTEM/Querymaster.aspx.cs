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
using System.Text;
using System.IO;
public partial class Querymaster : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    string dbfileName = string.Empty;

    string stype;
    string stblenme;
    string desc;
    string qname;
    string qnamed;
    string qry;
    string qrydesc;
    BusinessLogic objBL = new BusinessLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (!Page.IsPostBack)
            {
                BindQueries();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindQueries()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListQueries();
        /*drpExecIncharge.DataSource = ds;
        drpExecIncharge.DataBind();
        drpExecIncharge.DataTextField = "empFirstName";
        drpExecIncharge.DataValueField = "empno";*/

        cmbQuries.DataSource = ds;
        cmbQuries.DataBind();
        cmbQuries.DataTextField = "QueryName";
        cmbQuries.DataValueField = "ID";

        ddlqueries.DataSource = ds;
        ddlqueries.DataBind();
        ddlqueries.DataTextField = "QueryName";
        ddlqueries.DataValueField = "ID";
    }


    private void GetQueryForID(string ID)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.GetQueryForID(ID);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtQuery.Text = ds.Tables[0].Rows[0]["Query"].ToString();
                lblDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            }
        }
    }

    protected void cmbQuries_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbQuries.SelectedValue != "0")
            {
                GetQueryForID(cmbQuries.SelectedValue);
                //BtnEdit.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    
    protected void BtnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            string qry = txtQuery.Text;

            string[] itemAr = txtQuery.Text.Split(' ');
            string firstword = Convert.ToString(itemAr[0]).Trim();

            //if ((firstword != "select") && (firstword != "Select") && (firstword != "SELECT"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Query should start with Select')", true);
            //    return;
            //}

            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //DataSet ds = bl.GetDataForSQL(sDataSource, txtQuery.Text);

            //try
            //{
            //    ExportToExcel(ds);
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('"+ ex.Message+"');", true);
            //}

            BusinessLogic bl = new BusinessLogic(sDataSource);

            string sQl = string.Format(txtQuery.Text);

            bl.RunQuerySQL(sDataSource, sQl);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(DataSet ds)
    {
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                string filename = cmbQuries.SelectedItem.Text + DateTime.Now.ToString("ddMMyyyy") + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                //rptData.DataSource = ds;
                //rptData.DataBind();
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found');", true);
                return;
            }
        }
    }


    //protected void BtnEdit_Click(object sender, EventArgs e)
    //{
        //txtDesc.Visible = true;
        //BtnSave.Visible = true;
       
    //}

    protected void Btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds1 = new DataSet();
            string qnamed = "";
            qnamed = ddlqueries.SelectedItem.Value;
            if (ddlqueries.SelectedValue != "0")
            {
                qnamed = ddlqueries.SelectedItem.Value;
            }
            bl.DeleteDataForSQL(sDataSource, qnamed);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('SQL Report Deleted')", true);
            return;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }

    }
    protected void btnsavenew_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds1 = new DataSet();
            string qname = "";
            string qry = "";
            string qrydesc = "";
            qname = txtboxqrynme.Text;
            qrydesc = txtboxdescrip.Text;
            qry = txtboxqry.Text;

            string[] itemAr = txtboxqry.Text.Split(' ');
            string firstword = Convert.ToString(itemAr[0]).Trim();

            if ((firstword != "select") && (firstword != "Select") && (firstword != "SELECT"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Query should start with Select')", true);
                return;
            }

            bl.InsertDataForSQL(sDataSource, qname, qry, qrydesc);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('SQL Report Created')", true);
            return;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        
            return;
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string desc = "";

            //desc = txtDesc.Text;

            //if (txtDesc.Text == "")
            //{
            //    desc = lblDescription.Text;
            //}
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds1 = new DataSet();
            //bl.UpdateDataForSQL(sDataSource, cmbQuries.SelectedValue, cmbQuries.SelectedItem.Text, txtQuery.Text, txtDesc.Text);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Report Saved')", true);
            return;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        
            return;
        }
    }
   
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtboxqrynme.Text = "";
            txtboxdescrip.Text = "";
            txtboxqry.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    protected void ddlqueries_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlqueries.SelectedValue != "0")
            {
                GetQueryForID(ddlqueries.SelectedValue);

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}