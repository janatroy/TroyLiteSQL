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


public partial class ReportXLZeroSales1 : System.Web.UI.Page
{

    private string sDataSource = string.Empty;
    private string Connection = string.Empty;
    BusinessLogic objBL = new BusinessLogic();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            Connection = Request.Cookies["Company"].Value;
            if (!IsPostBack)
            {

                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

                    if (companyInfo != null)
                    {
                        if (companyInfo.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in companyInfo.Tables[0].Rows)
                            {
                                lblTNGST.Text = Convert.ToString(dr["TINno"]);
                                lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                                lblPhone.Text = Convert.ToString(dr["Phone"]);
                                lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                                lblAddress.Text = Convert.ToString(dr["Address"]);
                                lblCity.Text = Convert.ToString(dr["city"]);
                                lblPincode.Text = Convert.ToString(dr["Pincode"]);
                                lblState.Text = Convert.ToString(dr["state"]);

                            }
                        }
                    }
                }
            }

            lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            // txtStartDate.Text = dtaa;

            //  lblHeadDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());



            divPrint.Visible = true;
            divPr.Visible = true;
            DataSet dstt = new DataSet();
            DataSet ds = new DataSet();
            DataSet dsttt = new DataSet();
            string connection = Request.Cookies["Company"].Value;

            string Method = Convert.ToString(Request.QueryString["Method"].ToString());
            DateTime startdate = Convert.ToDateTime(Request.QueryString["startdate"].ToString());
            DateTime enddate = Convert.ToDateTime(Request.QueryString["enddate"].ToString());
            string branch = Convert.ToString(Request.QueryString["Branch"].ToString());
            int value = Convert.ToInt32(Request.QueryString["Value"].ToString());

            ds = objBL.getzerosalesreport(connection,sDataSource, startdate, enddate, Method, value, branch);

            DataTable dt = new DataTable("Sales");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt.Columns.Add(new DataColumn("Brand"));
                    dt.Columns.Add(new DataColumn("ProductName"));
                    dt.Columns.Add(new DataColumn("Model"));
                    dt.Columns.Add(new DataColumn("ItemCode"));
                    dt.Columns.Add(new DataColumn("BillNo"));
                    dt.Columns.Add(new DataColumn("BillDate"));
                    dt.Columns.Add(new DataColumn("LedgerName"));
                    dt.Columns.Add(new DataColumn("LedgerDetails"));
                    dt.Columns.Add(new DataColumn("Internal Transfer"));
                    dt.Columns.Add(new DataColumn("Delivery Note"));
                    dt.Columns.Add(new DataColumn("Purchase Return"));
                    dt.Columns.Add(new DataColumn("Qty"));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_final6 = dt.NewRow();
                        dr_final6["Brand"] = dr["brand"];
                        dr_final6["ProductName"] = dr["ProductName"];
                        dr_final6["Model"] = dr["Model"];
                        dr_final6["ItemCode"] = dr["Itemcode"];
                        dr_final6["BillNo"] = dr["BillNo"];
                        dr_final6["BillDate"] = dr["BillDate"];
                        dr_final6["LedgerName"] = dr["LedgerName"];
                        dr_final6["LedgerDetails"] = dr["customeraddress"];
                        dr_final6["Internal Transfer"] = dr["InternalTransfer"];
                        dr_final6["Delivery Note"] = dr["DeliveryNote"];
                        dr_final6["Purchase Return"] = dr["PurchaseReturn"];
                        dr_final6["Qty"] = dr["Qty"];


                        dt.Rows.Add(dr_final6);
                    }

                    Grdreport.Visible = true;
                    Grdreport.DataSource = dt;
                    Grdreport.DataBind();
                  //  ExportToExcel(dt);
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

    protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
            // div1.Visible = true;
            divPrint.Visible = false;
            divPr.Visible = false;

            //Response.Write("<script language='javascript'> window.open('StockReport.aspx' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnxls_Click(object sender, EventArgs e)
    {

    }

    public void ExportToExcel(DataTable dt)
    {


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            divPrint.Visible = true;
            divPr.Visible = true;
            //ReportsBL.ReportClass rptStock = new ReportsBL.ReportClass();
            //DataSet ds = rptStock.getCategory(sDataSource);
            //Grdreport.DataSource = ds;
            //Grdreport.DataBind();

            //  div1.Visible = false;

            // BindData();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
  
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }



    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}