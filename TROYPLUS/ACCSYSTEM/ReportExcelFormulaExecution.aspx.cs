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

public partial class ReportExcelFormulaExecution : System.Web.UI.Page
{
    BusinessLogic objBL;
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!IsPostBack)
            {

                objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

                // BusinessLogic bl = new BusinessLogic();

                // DateTime date = Convert.ToDateTime("01-01-1900");
                DataSet ds = new DataSet();

                string connection = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connection = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("Login.aspx");


                Loadproduct();
                loadBranch();
                // ds = objBL.listCompProductsreport();
                //// bl.ReportFormulaItem();

                // if (ds != null)
                // {
                //     if (ds.Tables[0].Rows.Count > 0)
                //     {
                //         DataTable dt = new DataTable("Product Manufacturing Details");
                //         dt.Columns.Add(new DataColumn("Component ID"));
                //         dt.Columns.Add(new DataColumn("Component Name"));
                //         dt.Columns.Add(new DataColumn("Item Code"));
                //         dt.Columns.Add(new DataColumn("Quantity"));
                //         dt.Columns.Add(new DataColumn("Date"));
                //         dt.Columns.Add(new DataColumn("IN/OUT"));
                //         dt.Columns.Add(new DataColumn("Is Released"));
                //         dt.Columns.Add(new DataColumn("BranchCode"));
                //         //dt.Columns.Add(new DataColumn("Project Status"));
                //         //dt.Columns.Add(new DataColumn("Expected Start Date"));
                //         //dt.Columns.Add(new DataColumn("Expected End Date"));
                //         //dt.Columns.Add(new DataColumn("Expected Effort Days"));



                //         DataRow dr_final123 = dt.NewRow();
                //         dt.Rows.Add(dr_final123);

                //         foreach (DataRow dr in ds.Tables[0].Rows)
                //         {
                //             DataRow dr_final1 = dt.NewRow();
                //             dr_final1["Component ID"] = dr["CompID"];

                //             dr_final1["Component Name"] = dr["FormulaName"];
                //             dr_final1["Item Code"] = dr["ItemCode"];
                //             dr_final1["IN/OUT"] = dr["InOut"];
                //             dr_final1["Quantity"] = dr["Qty"];
                //             dr_final1["Is Released"] = dr["IsReleased"];
                //             dr_final1["BranchCode"] = dr["BranchCode"];

                //             string aat = dr["CDate"].ToString().ToUpper().Trim();
                //             string dtaat = Convert.ToDateTime(aat).ToString("dd/MM/yyyy");
                //             dr_final1["Date"] = dtaat;
                //             //dr_final1["Project Description"] = dr["Project_Description"];

                //             dt.Rows.Add(dr_final1);
                //         }
                //         DataRow dr_final2 = dt.NewRow();
                //         dt.Rows.Add(dr_final2);
                //          ExportToExcel(dt);
                //     }
                //     else
                //     {
                //         ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                //     }
                // }
                // else
                // {
                //     ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                // }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void Loadproduct()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpproduct.Items.Clear();
        drpproduct.Items.Add(new ListItem("---All---", "0"));

        // string connection = Request.Cookies["Company"].Value;

        ds = bl.Loadproduct(connection);
        drpproduct.DataSource = ds;
        drpproduct.DataBind();
        drpproduct.DataTextField = "FormulaName";
        drpproduct.DataValueField = "FormulaName";
    }

    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpBranch.Items.Clear();
        drpBranch.Items.Add(new ListItem("Select Branch", "0"));
        ds = bl.ListBranchexecution(connection);
        drpBranch.DataSource = ds;
        drpBranch.DataBind();
        drpBranch.DataTextField = "BranchName";
        drpBranch.DataValueField = "Branchcode";
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                string filename = "Product Manufacturing details.xlsx";
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

    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            // BusinessLogic bl = new BusinessLogic();

            // DateTime date = Convert.ToDateTime("01-01-1900");
            DataSet ds = new DataSet();

            string connection = string.Empty;

            string productId = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;
            else
                Response.Redirect("Login.aspx");

           // DateTime date = txtStartDate.Text.ToString("dd/MM/yyyy");

            DateTime date = Convert.ToDateTime(txtStartDate.Text.ToString());

          //  DateTime date1 = Convert.ToDateTime(txtEndDate.Text).ToString("dd/MM/yyyy");

            DateTime date1 = Convert.ToDateTime(txtEndDate.Text.ToString());

                productId = Convert.ToString(drpproduct.SelectedItem.Text);

                string inout = Convert.ToString(drpinout.SelectedItem.Text);

                string Branch = Convert.ToString(drpBranch.SelectedValue);

                bool IsPros =Convert.ToBoolean( rdoIsPros.Checked.ToString());


                ds = objBL.listCompProductsreport(connection, productId, date, date1, inout, Branch, IsPros);
            // bl.ReportFormulaItem();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable("Product Manufacturing Details");
                   // dt.Columns.Add(new DataColumn("Component ID"));
                    dt.Columns.Add(new DataColumn("Product Name"));
                    dt.Columns.Add(new DataColumn("Item Code"));
                    dt.Columns.Add(new DataColumn("Quantity"));
                    dt.Columns.Add(new DataColumn("ProductName"));
                    dt.Columns.Add(new DataColumn("ProductDesc"));
                    dt.Columns.Add(new DataColumn("Model"));
                    dt.Columns.Add(new DataColumn("CurrentStock"));
                    dt.Columns.Add(new DataColumn("Date"));
                    dt.Columns.Add(new DataColumn("IN/OUT"));
                    dt.Columns.Add(new DataColumn("Is Released"));
                    dt.Columns.Add(new DataColumn("Comment"));
                    dt.Columns.Add(new DataColumn("BranchCode"));
                    //dt.Columns.Add(new DataColumn("Project Status"));
                    //dt.Columns.Add(new DataColumn("Expected Start Date"));
                    //dt.Columns.Add(new DataColumn("Expected End Date"));
                    //dt.Columns.Add(new DataColumn("Expected Effort Days"));



                    DataRow dr_final123 = dt.NewRow();
                    dt.Rows.Add(dr_final123);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_final1 = dt.NewRow();
                      //  dr_final1["Component ID"] = dr["CompID"];

                        dr_final1["Product Name"] = dr["FormulaName"];
                        dr_final1["Item Code"] = dr["ItemCode"];
                        dr_final1["IN/OUT"] = dr["InOut"];
                        dr_final1["Quantity"] = dr["Qty"];
                        dr_final1["ProductName"] = dr["ProductName"];
                        dr_final1["ProductDesc"] = dr["ProductDesc"];
                        dr_final1["Model"] = dr["Model"];
                        dr_final1["CurrentStock"] = dr["Stock"];
                        dr_final1["Is Released"] = dr["IsReleased"];
                        dr_final1["Comment"] = dr["Comments"];
                        dr_final1["BranchCode"] = dr["BranchCode"];

                        string aat = dr["CDate"].ToString().ToUpper().Trim();
                        string dtaat = Convert.ToDateTime(aat).ToString("dd/MM/yyyy");
                        dr_final1["Date"] = dtaat;
                        //dr_final1["Project Description"] = dr["Project_Description"];

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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {

            DateTime date = Convert.ToDateTime(txtStartDate.Text.ToString());

            //  DateTime date1 = Convert.ToDateTime(txtEndDate.Text).ToString("dd/MM/yyyy");

            DateTime date1 = Convert.ToDateTime(txtEndDate.Text.ToString());

          string  productId = Convert.ToString(drpproduct.SelectedItem.Text);

            string inout = Convert.ToString(drpinout.SelectedItem.Text);

            string Branch = Convert.ToString(drpBranch.SelectedValue);

            bool IsPros = Convert.ToBoolean(rdoIsPros.Checked.ToString());
            //string productid = Convert.ToString(drpproduct.SelectedItem.Text);

            Response.Write("<script language='javascript'> window.open('ReportExcelFormulaExecution1.aspx?FormulaName=" + productId + "&startdate=" + date + "&enddate=" + date1 + "&Inout=" + inout + "&Branch=" + Branch + "&ispros=" + IsPros + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}