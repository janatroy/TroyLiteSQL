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
public partial class ReportExcelProjectManagement : System.Web.UI.Page
{
    //DBClass objdb = new DBClass();
    BusinessLogic objBL;
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
            //loadCodes();
            //loadModels();
            if (!IsPostBack)
            {
                loadCategory();
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
    
    protected void drpProjectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void drpIncharge_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void drpTaskType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void drpTaskStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void loadBrands()
    {
        
    }

    private void loadCategory()
    {
        string connection = Request.Cookies["Company"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet dst = new DataSet();

        dst = bl.GetProjectList(connection, "", "");
        drpProjectCode.DataSource = dst;
        drpProjectCode.DataBind();
        drpProjectCode.DataTextField = "Project_Name";
        drpProjectCode.DataValueField = "Project_Id";

        drpProjectCode.Items.Insert(0, new ListItem("All", "All"));

        DataSet ds = new DataSet();
        ds = bl.ListExecutive();
        drpIncharge.DataSource = ds;
        drpIncharge.DataBind();
        drpIncharge.DataTextField = "empFirstName";
        drpIncharge.DataValueField = "empno";
        drpIncharge.Items.Insert(0, new ListItem("All", "All"));

        DataSet dsd = new DataSet();
        dsd = bl.ListTaskTypesInfo(connection, "", "");
        drpTaskType.DataSource = dsd;
        drpTaskType.DataBind();
        drpTaskType.DataTextField = "Task_Type_Name";
        drpTaskType.DataValueField = "Task_Type_Id";
        drpTaskType.Items.Insert(0, new ListItem("All", "All"));

        DataSet dsdd = new DataSet();
        dsdd = bl.ListTaskStatusInfo(connection, "", "");
        drpTaskStatus.DataSource = dsdd;
        drpTaskStatus.DataBind();
        drpTaskStatus.DataTextField = "Task_Status_Name";
        drpTaskStatus.DataValueField = "Task_Status_Id";
        drpTaskStatus.Items.Insert(0, new ListItem("All", "All"));
    }

    private void loaProducts()
    {
        

        //ddlproduct.Items.Insert(0, new ListItem("All", "All"));
    }

    private void loadCodes()
    {
        
    }

    //private void loadModels()
    //{
    //    //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
    //    BusinessLogic bl = new BusinessLogic();
    //    DataSet ds = new DataSet();
    //    ds = bl.ListAllModels();
    //    cmbProdAdd.DataTextField = "Model";
    //    cmbProdAdd.DataValueField = "Model";
    //    cmbProdAdd.DataSource = ds;
    //    cmbProdAdd.DataBind();
    //}

    private void BindGrid()
    {
        CreditLimitTotal = 0;
        OpenBalanceDRTotal = 0;
        DataSet ds = new DataSet();

        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        BusinessLogic bl = new BusinessLogic();

        
        DataSet dsSales = bl.ListProductHistory(connStr.Trim(), "");

        if (dsSales != null)
        {
            if (dsSales.Tables[0].Rows.Count > 0)
            {
                GridCust.DataSource = dsSales;
                GridCust.DataBind();
            }
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

            BusinessLogic bl = new BusinessLogic();

            string condtion = "";
            condtion = getCond();

            ds = objBL.GetProjectManagementList(connection, condtion, "");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Task Id"));
                    dt.Columns.Add(new DataColumn("Task Date"));
                    dt.Columns.Add(new DataColumn("Expected Start Date"));
                    dt.Columns.Add(new DataColumn("Expected End Date"));
                    dt.Columns.Add(new DataColumn("Owner"));
                    dt.Columns.Add(new DataColumn("Project Name"));
                    dt.Columns.Add(new DataColumn("Task Type"));
                    dt.Columns.Add(new DataColumn("Actual Start Date"));
                    dt.Columns.Add(new DataColumn("Actual End Date"));
                    dt.Columns.Add(new DataColumn("Task Status"));
                    dt.Columns.Add(new DataColumn("Is Active"));
                    dt.Columns.Add(new DataColumn("Task Description"));

                    DataRow dr_final123 = dt.NewRow();
                    dt.Rows.Add(dr_final123);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_final1 = dt.NewRow();
                        dr_final1["Task Id"] = dr["TaskId"];

                        string aa = dr["Task_Date"].ToString().ToUpper().Trim();
                        string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                        dr_final1["Task Date"] = dtaa;

                        string aad = dr["Expected_Start_Date"].ToString().ToUpper().Trim();
                        string dtaad = Convert.ToDateTime(aad).ToString("dd/MM/yyyy");
                        dr_final1["Expected Start Date"] = dtaad;

                        string aat = dr["Expected_End_Date"].ToString().ToUpper().Trim();
                        string dtaat = Convert.ToDateTime(aat).ToString("dd/MM/yyyy");
                        dr_final1["Expected End Date"] = dtaat;

                        string aadt = dr["Actual_Start_Date"].ToString().ToUpper().Trim();
                        string dtaadt = Convert.ToDateTime(aadt).ToString("dd/MM/yyyy");
                        dr_final1["Actual Start Date"] = dtaadt;

                        string aatdd = dr["Actual_End_Date"].ToString().ToUpper().Trim();
                        string dtaatdd = Convert.ToDateTime(aatdd).ToString("dd/MM/yyyy");
                        dr_final1["Actual End Date"] = dtaatdd;

                        dr_final1["Owner"] = dr["empfirstname"];
                        dr_final1["Project Name"] = dr["ProjectName"];
                        dr_final1["Task Type"] = dr["Task_Type_Name"];
                        dr_final1["Task Status"] = dr["Task_Status_Name"];
                        dr_final1["Is Active"] = dr["IsActive"];
                        dr_final1["Task Description"] = dr["Task_Description"];
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

    protected string getCond()
    {
        string cond = " 1=1 ";

        if (drpProjectCode.SelectedIndex > 0)
        {
            cond += " and tblProjects.Project_Name='" + drpProjectCode.SelectedItem.Text + "' ";
        }
        if (drpIncharge.SelectedIndex > 0)
        {
            cond += " and tblEmployee.empfirstname='" + drpIncharge.SelectedItem.Text + "' ";
        }
        if (drpTaskStatus.SelectedIndex > 0)
        {
            cond += " and tblTaskStatus.Task_Status_Name='" + drpTaskStatus.SelectedItem.Text + "' ";
        }
        if (drpTaskType.SelectedIndex > 0)
        {
            cond += " and tblTaskTypes.Task_Type_Name='" + drpTaskType.SelectedItem.Text + "' ";
        }
        return cond;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            string filename = "Project Management.xls";
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
