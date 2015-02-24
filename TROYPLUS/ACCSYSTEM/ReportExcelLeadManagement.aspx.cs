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
using System.IO;
using System.Data;
using ClosedXML.Excel;
public partial class ReportExcelLeadManagement : System.Web.UI.Page
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
            //DateEndformat();
            //DateStartformat();


            if (!IsPostBack)
            {
                txtStrtDt.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDt.Text = DateTime.Now.ToShortDateString();
                loadCategory();
                loadArea();
                loadInformation3();
                loadInformation4();
                loadEmp();
                loadLeadActivity();
                loadFollowupActivity();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadCategory()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpCategory.Items.Clear();
        drpCategory.Items.Add(new ListItem("Select Category", "0"));
        ds = bl.ListCategory();
        drpCategory.DataSource = ds;
        drpCategory.DataBind();
        drpCategory.DataTextField = "TextValue";
        drpCategory.DataValueField = "ID";
    }

    private void loadArea()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpArea.Items.Clear();
        drpArea.Items.Add(new ListItem("Select Location", "0"));
        ds = bl.ListArea();
        drpArea.DataSource = ds;
        drpArea.DataBind();
        drpArea.DataTextField = "TextValue";
        drpArea.DataValueField = "ID";
    }

    private void loadInformation3()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpInformation3.Items.Clear();
        drpInformation3.Items.Add(new ListItem("Select Additional Information 3", "0"));
        ds = bl.ListInformation3();
        drpInformation3.DataSource = ds;
        drpInformation3.DataBind();
        drpInformation3.DataTextField = "TextValue";
        drpInformation3.DataValueField = "ID";
    }

    private void loadInformation4()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpInformation4.Items.Clear();
        drpInformation4.Items.Add(new ListItem("Select Additional Information 4", "0"));
        ds = bl.ListInformation4();
        drpInformation4.DataSource = ds;
        drpInformation4.DataBind();
        drpInformation4.DataTextField = "TextValue";
        drpInformation4.DataValueField = "ID";
    }

    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpIncharge.Items.Clear();
        drpIncharge.Items.Add(new ListItem("Select Employee Responsible", "0"));
        ds = bl.ListExecutive();
        drpIncharge.DataSource = ds;
        drpIncharge.DataBind();
        drpIncharge.DataTextField = "empFirstName";
        drpIncharge.DataValueField = "empno";
    }
    private void loadLeadActivity()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpactivityName.Items.Clear();
        drpactivityName.Items.Add(new ListItem("Select Lead Activity", "0"));
        ds = bl.ListActivityName();
        drpactivityName.DataSource = ds;
        drpactivityName.DataBind();
        drpactivityName.DataTextField = "TextValue";
        drpactivityName.DataValueField = "TextValue";
    }

    private void loadFollowupActivity()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpnxtActivity.Items.Clear();
        drpnxtActivity.Items.Add(new ListItem("Select Follow-up Activity", "0"));
        ds = bl.ListActivityName();
        drpnxtActivity.DataSource = ds;
        drpnxtActivity.DataBind();
        drpnxtActivity.DataTextField = "TextValue";
        drpnxtActivity.DataValueField = "TextValue";
    }

    protected void DateEndformat()
    {
        System.DateTime dt = System.DateTime.Now.Date;
        txtEndDt.Text = string.Format("{0:dd/MM/yyyy}", dt);
    }

    protected void GridCust_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                // PresentationUtils.SetPagerButtonStates(GridCust, e.Row, this);
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

    protected void DateStartformat()
    {
        DateTime dtCurrent = DateTime.Now;
        DateTime dtNew = new DateTime(dtCurrent.Year, dtCurrent.Month, 1);
        txtStrtDt.Text = string.Format("{0:dd/MM/yyyy}", dtNew);
    }

    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate;
            DateTime endDate;

            DataSet ds = new DataSet();

            startDate = Convert.ToDateTime(txtStrtDt.Text.Trim());
            endDate = Convert.ToDateTime(txtEndDt.Text.Trim());

            string connection = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;
            else
                Response.Redirect("Login.aspx");

            BusinessLogic bl = new BusinessLogic();

            string condtion = "";
            condtion = getCond();

           // ds = objBL.GetLeadManagementList(connection, startDate, endDate);
            ds = objBL.GetLeadManagementListFilter(connection, condtion);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable("Lead Management");
                    dt.Columns.Add(new DataColumn("Lead No"));
                    dt.Columns.Add(new DataColumn("Lead Name"));
                    dt.Columns.Add(new DataColumn("Customer Name"));
                    dt.Columns.Add(new DataColumn("Address"));
                    dt.Columns.Add(new DataColumn("Mobile"));
                    dt.Columns.Add(new DataColumn("Telephone"));
                    dt.Columns.Add(new DataColumn("Record Status"));
                    dt.Columns.Add(new DataColumn("Closing Date"));
                    dt.Columns.Add(new DataColumn("Employee Name"));
                    dt.Columns.Add(new DataColumn("Start Date"));
                    dt.Columns.Add(new DataColumn("Lead Status"));
                    dt.Columns.Add(new DataColumn("Contact Name"));
                    dt.Columns.Add(new DataColumn("Predicted Closing Date"));
                    dt.Columns.Add(new DataColumn("Competitor Name"));
                    dt.Columns.Add(new DataColumn("Activity Name"));
                    dt.Columns.Add(new DataColumn("Activity Date"));
                    dt.Columns.Add(new DataColumn("Activity Location"));
                    dt.Columns.Add(new DataColumn("Next Activity"));
                    dt.Columns.Add(new DataColumn("Next Activity Date"));
                    //dt.Columns.Add(new DataColumn("Activity Employee Name"));
                    //dt.Columns.Add(new DataColumn("Mode of Contact"));
                    //dt.Columns.Add(new DataColumn("Product Name"));

                    DataRow dr_final123 = dt.NewRow();
                    dt.Rows.Add(dr_final123);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_final1 = dt.NewRow();
                        dr_final1["Lead No"] = dr["Lead_No"];
                        dr_final1["Lead Name"] = dr["Lead_Name"];
                        dr_final1["Customer Name"] = dr["BP_Name"];
                        dr_final1["Address"] = dr["Address"];
                        dr_final1["Telephone"] = dr["Telephone"];
                        dr_final1["Record Status"] = dr["Doc_Status"];

                        string aad = dr["Closing_Date"].ToString().ToUpper().Trim();
                        string dtaad = Convert.ToDateTime(aad).ToString("dd/MM/yyyy");
                        if (dtaad != "01/01/2000")
                        {
                            dr_final1["Closing Date"] = dtaad;
                        }


                        dr_final1["Employee Name"] = dr["Emp_Name"];

                        string aa = dr["Start_Date"].ToString().ToUpper().Trim();
                        string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                        dr_final1["Start Date"] = dtaa;

                        dr_final1["Lead Status"] = dr["Lead_Status"];
                        dr_final1["Contact Name"] = dr["Contact_Name"];

                        string aae = dr["Predicted_Closing_Date"].ToString().ToUpper().Trim();
                        string dtaae = Convert.ToDateTime(aae).ToString("dd/MM/yyyy");
                        dr_final1["Predicted Closing Date"] = dtaae;

                        dr_final1["Competitor Name"] = dr["Competitor_Name"];
                        dr_final1["Activity Name"] = dr["Activity_Name"];

                        string aaee = dr["Activity_Date"].ToString().ToUpper().Trim();
                        string dtaaee = Convert.ToDateTime(aaee).ToString("dd/MM/yyyy");
                        dr_final1["Activity Date"] = dtaaee;

                        dr_final1["Activity Location"] = dr["Activity_Location"];
                        dr_final1["Next Activity"] = dr["Next_Activity"];

                        string aaeee = dr["NextActivity_Date"].ToString().ToUpper().Trim();
                        string dtaaeee = Convert.ToDateTime(aaeee).ToString("dd/MM/yyyy");
                        dr_final1["Next Activity Date"] = dtaaeee;

                        //dr_final1["Activity Employee Name"] = dr["Emp_Name"];
                        //dr_final1["Mode of Contact"] = dr["ModeofContact"];
                        //dr_final1["Product Name"] = dr["Product_Name"];

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

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                string filename = "Lead Management.xlsx";
                wb.Worksheets.Add(dt);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + "");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string connection = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connection = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("Login.aspx");


                DateTime startDate, endDate;

                string status = string.Empty;
                string empname = string.Empty;
                string area = string.Empty;
                string category = string.Empty;
                string actname = string.Empty;
                string nxtactname = string.Empty;
                string info3 = string.Empty;
                string info4 = string.Empty;

                startDate = Convert.ToDateTime(txtStrtDt.Text.Trim());
                endDate = Convert.ToDateTime(txtEndDt.Text.Trim());
                status = drpStatus.SelectedItem.Text;
                empname = drpIncharge.SelectedValue.ToString();
                area = drpArea.SelectedValue.ToString();
                category = drpCategory.SelectedValue.ToString();
                actname = drpactivityName.SelectedValue.ToString();
                nxtactname = drpnxtActivity.SelectedValue.ToString();
                info3 = drpInformation3.SelectedValue.ToString();
                info4 = drpInformation4.SelectedValue.ToString();

                string condtion = "";
                condtion = getCond();

                DataSet BillDs = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);

                //if (drpStatus.SelectedItem.Text == "Select Lead Status" && drpIncharge.SelectedItem.Text == "Select Employee Responsible" && drpArea.SelectedItem.Text == "Select Location" && drpCategory.SelectedItem.Text == "Select Category" && drpactivityName.SelectedItem.Text == "Select Lead Activity" && drpnxtActivity.SelectedItem.Text == "Select Follow-up Activity" && drpInformation3.SelectedItem.Text == "Select Additional Information 3" && drpInformation4.SelectedItem.Text == "Select Additional Information 4")
                //{
                //    BillDs = bl.GetLeadManagementList(connection, startDate, endDate);
                //}                
                //else
                //{
                BillDs = bl.GetLeadManagementListFilter(connection, condtion);
                //}

                Response.Write("<script language='javascript'> window.open('LeadManagementReport.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + "&status=" + Convert.ToString(status) + "&empname=" + Convert.ToString(empname) + "&area=" + Convert.ToString(area) + "&category=" + Convert.ToString(category) + "&actname=" + Convert.ToString(actname) + "&nxtactname=" + Convert.ToString(nxtactname) + "&info3=" + Convert.ToString(info3) + "&info4=" + Convert.ToString(info4) + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                //Response.Write("<script language='javascript'> window.open('LeadManagementReport.aspx?&cond=" + Convert.ToString(condtion) + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected string getCond()
    {
        string cond = "";
        //Where BillDate between CDate('" + StartDate + "') and CDate('" + EndDate + "') and Internaltransfer='" + Internals + "' and purchaseReturn='" + Models + "' and CustomerID=" + Catids + " and CategoryID=" + Categorys + " and PayMode=" + Stocks + " and ProductName='" + PrdctNme + "' and ProductDesc='" + Brands + "' and tblProductMaster.ItemCode='" + Productcd + "'");
        if (txtStrtDt.Text != "" && txtEndDt.Text != "")
        {

            objBL.StartDate = txtStrtDt.Text;

            objBL.StartDate = string.Format("{0:MM/dd/yyyy}", txtStrtDt.Text);
            objBL.EndDate = txtEndDt.Text;
            objBL.EndDate = string.Format("{0:MM/dd/yyyy}", txtEndDt.Text);


            string aa = txtStrtDt.Text;
            string dt = Convert.ToDateTime(aa).ToString("MM/dd/yyyy");

            string aaa = txtEndDt.Text;
            string dtt = Convert.ToDateTime(aaa).ToString("MM/dd/yyyy");

            cond = " Start_Date >='" + dt + "' and Start_Date <= '" + dtt + "' ";

        }
        if ((drpStatus.SelectedItem.Text != "Select Lead Status"))
        {
            objBL.Models = drpStatus.SelectedItem.Text;
            cond += " and Doc_Status='" + drpStatus.SelectedItem.Text.ToUpper() + "'";
        }
        if ((drpIncharge.SelectedItem.Text != "Select Employee Responsible"))
        {
            objBL.Models = drpIncharge.SelectedValue.ToString();
            cond += " and Emp_Id=" + Convert.ToInt32(drpIncharge.SelectedItem.Value) + "";
        }
        if ((drpArea.SelectedItem.Text != "Select Location"))
        {
            objBL.Models = drpArea.SelectedValue.ToString();
            cond += " and Area=" + Convert.ToInt32(drpArea.SelectedItem.Value) + "";
        }
        if ((drpCategory.SelectedItem.Text != "Select Category"))
        {
            objBL.Models = drpCategory.SelectedValue.ToString();
            cond += " and Category=" + Convert.ToInt32(drpCategory.SelectedItem.Value) + "";
        }
        if ((drpactivityName.SelectedItem.Text != "Select Lead Activity"))
        {
            objBL.Models = drpactivityName.SelectedValue.ToString();
            cond += " and Activity_Name_Id=" + Convert.ToInt32(drpactivityName.SelectedItem.Value) + "";
        }
        if ((drpnxtActivity.SelectedItem.Text != "Select Follow-up Activity"))
        {
            objBL.Models = drpnxtActivity.SelectedValue.ToString();
            cond += " and Next_Activity_Id=" + Convert.ToInt32(drpnxtActivity.SelectedItem.Value) + "";
        }
        if ((drpInformation3.SelectedItem.Text != "Select Additional Information 3"))
        {
            objBL.Models = drpInformation3.SelectedValue.ToString();
            cond += " and Information3=" + Convert.ToInt32(drpInformation3.SelectedItem.Value) + "";
        }
        if ((drpInformation4.SelectedItem.Text != "Select Additional Information 4"))
        {
            objBL.Models = drpInformation4.SelectedValue.ToString();
            cond += " and Information4=" + Convert.ToInt32(drpInformation4.SelectedItem.Value) + "";
        }
        return cond;
    }
}
