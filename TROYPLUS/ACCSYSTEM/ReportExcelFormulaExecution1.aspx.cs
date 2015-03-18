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

public partial class ReportExcelFormulaExecution1 : System.Web.UI.Page
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
            //  txtStartDate.Text = dtaa;

            // lblHeadDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());



            divPrint.Visible = true;
            divPr.Visible = true;

            string connection = Request.Cookies["Company"].Value;
            //ReportsBL.ReportClass rptStock = new ReportsBL.ReportClass();
            //DataSet ds = rptStock.getCategory(sDataSource);
            productid = Convert.ToString(Request.QueryString["FormulaName"].ToString());
          DateTime  date = Convert.ToDateTime(Request.QueryString["startdate"].ToString());
           DateTime date1 = Convert.ToDateTime(Request.QueryString["enddate"].ToString());
          string inout = Convert.ToString(Request.QueryString["Inout"].ToString());
          string Branch = Convert.ToString(Request.QueryString["Branch"].ToString());
          bool IsPros = Convert.ToBoolean(Request.QueryString["ispros"].ToString());


            DataSet ds = objBL.listCompProductsreport(connection, productid, date, date1, inout, Branch, IsPros);

            //if (!IsPostBack)
            //{

            if (ds != null)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grdformula.DataSource = ds;
                    Grdformula.DataBind();
                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' No data found');", true);
                    return;

                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' No data found');", true);
                return;

            }
         

            //div1.Visible = false;
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
            ReportsBL.ReportClass rptStock = new ReportsBL.ReportClass();
            DataSet ds = rptStock.getCategory(sDataSource);
            Grdformula.DataSource = ds;
            Grdformula.DataBind();

            //  div1.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    string cond;
    string cond1;
    string productid;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                }
            }
            catch (Exception ex)
            {
                TroyLiteExceptionManager.HandleException(ex);
            }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


}